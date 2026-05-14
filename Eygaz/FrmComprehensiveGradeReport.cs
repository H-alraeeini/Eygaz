using DevExpress.LookAndFeel;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class FrmComprehensiveGradeReport : MetroFramework.Forms.MetroForm
    {
        private const string ColSerial = "\u0645";
        private const string ColName = "\u0627\u0644\u0627\u0633\u0645";
        private const string ColLastSurah = "\u0622\u062E\u0631 \u0633\u0648\u0631\u0629";
        private const string ColTotal = "\u0627\u0644\u0645\u062C\u0645\u0648\u0639";
        private const string ColPercent = "\u0627\u0644\u0646\u0633\u0628\u0629 %";
        private const string ColRank = "\u0627\u0644\u062A\u0631\u062A\u064A\u0628";
        private const string SubjectHifz = "\u0627\u0644\u062D\u0641\u0638";

        /// <summary>???? ??? ????? ?????? ???? ????? ?????? ?? ???????? ???? ???? ?? ??? ?????.</summary>
        private const int SubjectPrintMinColumnWidth = 70;

        /// <summary>????? ???? ????? ????? ???? ????? ??? ???? ???? ???????.</summary>
        private const int ColumnContentPaddingPx = 18;

        private static readonly TextFormatFlags MeasureTextFlags =
            TextFormatFlags.SingleLine | TextFormatFlags.NoPadding | TextFormatFlags.RightToLeft;

        private readonly Func f = new Func();
        private readonly AttendanceHelper helper = new AttendanceHelper();
        private DataTable viewTable;
        private Dictionary<string, double> subjectMaxCaps = new Dictionary<string, double>(StringComparer.Ordinal);

        public FrmComprehensiveGradeReport()
        {
            InitializeComponent();
        }

        private void FrmComprehensiveGradeReport_Load(object sender, EventArgs e)
        {
            f.DataCombo(CmbClass, "Classes", "ClassName", "Id", " WHERE IsActive = 0 ORDER BY ClassName");
            CmbTerm.Items.Clear();
            CmbTerm.Items.AddRange(new object[] { "First", "Second", "Final" });
            CmbTerm.SelectedIndex = 0;
            GrdSheet.RowCellStyle += GrdSheet_RowCellStyle;
            ConfigureGradeSheetPrintDefaultsOnView();
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            if (CmbClass.SelectedValue == null || CmbTerm.SelectedItem == null)
            {
                MessageBox.Show("\u064A\u0631\u062C\u0649 \u0627\u062E\u062A\u064A\u0627\u0631 \u0627\u0644\u0641\u0635\u0644 \u0648\u0627\u0644\u062A\u0631\u0645.", "\u062A\u0646\u0628\u064A\u0647", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int classId = Convert.ToInt32(CmbClass.SelectedValue);
            string term = CmbTerm.SelectedItem.ToString();
            string studentSearch = TxtSearch.Text.Trim();

            DataTable raw = helper.GetComprehensiveGradeSheetRaw(classId, term, studentSearch);
            subjectMaxCaps = BuildSubjectMaxCaps(raw);
            viewTable = BuildComputedSheet(raw);
            GVSheet.DataSource = viewTable;
            ConfigureGridColumns();
            FillSummary(viewTable);
        }

        private DataTable BuildComputedSheet(DataTable raw)
        {
            DataTable table = new DataTable();
            table.Columns.Add(ColSerial, typeof(int));
            table.Columns.Add(ColName, typeof(string));
            table.Columns.Add(ColLastSurah, typeof(string));
            table.Columns.Add(ColTotal, typeof(double));
            table.Columns.Add(ColPercent, typeof(double));
            table.Columns.Add(ColRank, typeof(int));
            table.Columns.Add("TopFlag", typeof(bool));

            if (raw == null || raw.Rows.Count == 0)
                return table;

            var subjects = raw.AsEnumerable()
                .Select(r => r["SubjectName"] == DBNull.Value ? "" : (r["SubjectName"].ToString() ?? "").Trim())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct()
                .OrderBy(s => s)
                .ToList();

            foreach (string subject in subjects)
                table.Columns.Add(subject, typeof(string));

            var grouped = raw.AsEnumerable()
                .GroupBy(r => new
                {
                    StudentId = Convert.ToInt32(r["StudentId"]),
                    StudentName = r["StudentName"]?.ToString() ?? ""
                })
                .OrderBy(g => g.Key.StudentName)
                .ToList();

            foreach (var group in grouped)
            {
                DataRow row = table.NewRow();
                row[ColSerial] = 0;
                row[ColName] = group.Key.StudentName;
                row[ColLastSurah] = ResolveLastSurah(group);
                row[ColRank] = 0;
                row["TopFlag"] = false;

                int assignedSubjects = group.Count(r =>
                    r["SubjectName"] != DBNull.Value &&
                    !string.IsNullOrWhiteSpace(r["SubjectName"]?.ToString()));

                double totalRaw = 0;
                foreach (string subject in subjects)
                {
                    DataRow subjectRow = group.FirstOrDefault(r =>
                        string.Equals(
                            (r["SubjectName"] == DBNull.Value ? "" : r["SubjectName"].ToString() ?? "").Trim(),
                            subject,
                            StringComparison.Ordinal));

                    if (subjectRow == null)
                    {
                        row[subject] = "\u2014";
                        continue;
                    }

                    if (subjectRow["Score"] == DBNull.Value)
                    {
                        row[subject] = "0";
                        continue;
                    }

                    double score = Convert.ToDouble(subjectRow["Score"]);
                    row[subject] = score.ToString("0.#", CultureInfo.InvariantCulture);
                    totalRaw += score;
                }

                row[ColTotal] = Math.Round(totalRaw, 1, MidpointRounding.AwayFromZero);
                row[ColPercent] = assignedSubjects <= 0
                    ? 0
                    : Math.Round(totalRaw / assignedSubjects, 4, MidpointRounding.AwayFromZero);

                table.Rows.Add(row);
            }

            var ordered = table.AsEnumerable()
                .OrderByDescending(r => r.Field<double>(ColTotal))
                .ThenBy(r => r.Field<string>(ColName))
                .ToList();

            int serial = 1;
            int rank = 0;
            double? prevTotal = null;
            for (int i = 0; i < ordered.Count; i++)
            {
                DataRow row = ordered[i];
                double totalVal = row.Field<double>(ColTotal);
                if (!prevTotal.HasValue || Math.Abs(totalVal - prevTotal.Value) > 0.0001)
                    rank = i + 1;

                row[ColSerial] = serial++;
                row[ColRank] = rank;
                row["TopFlag"] = rank == 1;
                prevTotal = totalVal;
            }

            DataView dv = table.DefaultView;
            dv.Sort = $"{ColTotal} DESC, {ColName} ASC";
            return dv.ToTable();
        }

        private static Dictionary<string, double> BuildSubjectMaxCaps(DataTable raw)
        {
            var caps = new Dictionary<string, double>(StringComparer.Ordinal);
            if (raw == null) return caps;

            foreach (DataRow r in raw.Rows)
            {
                string name = r["SubjectName"] == DBNull.Value ? "" : (r["SubjectName"].ToString() ?? "").Trim();
                if (string.IsNullOrEmpty(name)) continue;

                double cap = 100;
                if (r["MaxScore"] != DBNull.Value)
                {
                    double m = Convert.ToDouble(r["MaxScore"]);
                    if (m > 0) cap = m;
                }

                if (!caps.ContainsKey(name) || cap > caps[name])
                    caps[name] = cap;
            }

            return caps;
        }

        private double GetMaxScoreForRow(DataRow subjectRow, string subjectName)
        {
            if (subjectRow != null && subjectRow["MaxScore"] != DBNull.Value)
            {
                double m = Convert.ToDouble(subjectRow["MaxScore"]);
                if (m > 0) return m;
            }

            if (subjectMaxCaps != null && subjectMaxCaps.TryGetValue(subjectName, out double cap) && cap > 0)
                return cap;

            return 100;
        }

        private static string ResolveLastSurah(IEnumerable<DataRow> group)
        {
            DataRow hifzRow = group.FirstOrDefault(r =>
                string.Equals(
                    (r["SubjectName"] == DBNull.Value ? "" : r["SubjectName"].ToString() ?? "").Trim(),
                    SubjectHifz,
                    StringComparison.Ordinal));

            if (hifzRow != null && hifzRow["LastSurah"] != DBNull.Value)
                return hifzRow["LastSurah"].ToString();

            DataRow any = group.FirstOrDefault(r =>
                r["LastSurah"] != DBNull.Value &&
                !string.IsNullOrWhiteSpace(r["LastSurah"].ToString()));

            return any == null ? "" : any["LastSurah"].ToString();
        }

        private void ConfigureGradeSheetPrintDefaultsOnView()
        {
            var op = GrdSheet.OptionsPrint;
            op.AutoWidth = false;
            op.AllowMultilineHeaders = true;
            op.PrintHorzLines = true;
            op.PrintVertLines = true;
        }

        private void ConfigureGridColumns()
        {
            GrdSheet.OptionsBehavior.Editable = false;
            GrdSheet.OptionsView.ShowGroupPanel = false;
            GrdSheet.OptionsView.ColumnAutoWidth = false;
            GrdSheet.OptionsCustomization.AllowColumnResizing = true;
            ConfigureGradeSheetPrintDefaultsOnView();

            foreach (GridColumn col in GrdSheet.Columns)
            {
                if (col.FieldName == "TopFlag")
                {
                    col.Visible = false;
                    continue;
                }

                bool isSubject = !(col.FieldName == ColSerial || col.FieldName == ColName || col.FieldName == ColLastSurah ||
                    col.FieldName == ColTotal || col.FieldName == ColPercent || col.FieldName == ColRank);

                if (col.FieldName == ColName)
                    col.MinWidth = 120;
                else if (col.FieldName == ColLastSurah)
                    col.MinWidth = 36;
                else if (col.FieldName == ColTotal || col.FieldName == ColPercent || col.FieldName == ColRank ||
                         col.FieldName == ColSerial)
                    col.MinWidth = 34;
                else if (isSubject)
                    col.MinWidth = SubjectPrintMinColumnWidth;

                col.OptionsColumn.AllowSize = true;
                col.OptionsColumn.FixedWidth = false;

                if (isSubject)
                {
                    double cap = 100;
                    if (subjectMaxCaps != null && subjectMaxCaps.TryGetValue(col.FieldName, out double c) && c > 0)
                        cap = c;
                    col.Caption = col.FieldName + " /" + cap.ToString("0.#", CultureInfo.InvariantCulture);
                }
            }

            ResizeCompactColumnsToMeasuredContent();

            foreach (GridColumn col in GrdSheet.Columns)
            {
                if (!col.Visible) continue;

                bool isSubject = !(col.FieldName == ColSerial || col.FieldName == ColName || col.FieldName == ColLastSurah ||
                    col.FieldName == ColTotal || col.FieldName == ColPercent || col.FieldName == ColRank ||
                    col.FieldName == "TopFlag");

                if (!isSubject) continue;
                if (col.Width < SubjectPrintMinColumnWidth)
                    col.Width = SubjectPrintMinColumnWidth;
            }
        }

        private static int MaxTextWidthPx(Font font, IEnumerable<string> texts)
        {
            int max = 0;
            foreach (string t in texts)
            {
                if (string.IsNullOrEmpty(t)) continue;
                int w = TextRenderer.MeasureText(t, font, Size.Empty, MeasureTextFlags).Width;
                if (w > max) max = w;
            }

            return max;
        }

        private static IEnumerable<string> DistinctTextsFromColumn(DataTable data, string columnName)
        {
            if (data == null || !data.Columns.Contains(columnName))
                yield break;

            var seen = new HashSet<string>(StringComparer.Ordinal);
            foreach (DataRow row in data.Rows)
            {
                object v = row[columnName];
                if (v == null || v == DBNull.Value) continue;
                string s = v.ToString()?.Trim();
                if (string.IsNullOrEmpty(s)) continue;

                double d;
                if (columnName == ColTotal && double.TryParse(Convert.ToString(v, CultureInfo.InvariantCulture),
                        NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                    s = Math.Round(d, 1).ToString("0.#", CultureInfo.InvariantCulture);
                else if (columnName == ColPercent &&
                         double.TryParse(Convert.ToString(v, CultureInfo.InvariantCulture),
                             NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                    s = Math.Round(d, 4).ToString("0.####", CultureInfo.InvariantCulture);
                else if ((columnName == ColRank || columnName == ColSerial) &&
                         int.TryParse(Convert.ToString(v, CultureInfo.InvariantCulture),
                             NumberStyles.Integer, CultureInfo.InvariantCulture, out int k))
                    s = k.ToString(CultureInfo.InvariantCulture);

                if (seen.Add(s))
                    yield return s;
            }
        }

        private void ResizeCompactColumnsToMeasuredContent()
        {
            DataTable data = GVSheet?.DataSource as DataTable ?? viewTable;
            if (data == null || data.Rows.Count == 0) return;

            Font headerFont = GrdSheet.Appearance.HeaderPanel.Font ?? GrdSheet.GridControl.Font;
            Font cellFont = GrdSheet.Appearance.Row.Font ?? GrdSheet.GridControl.Font;

            void Fit(string field, int absoluteMaxPx, int hardMinPx)
            {
                GridColumn col = GrdSheet.Columns[field];
                if (col == null || !col.Visible) return;

                int wCaption = MaxTextWidthPx(headerFont, new[] { col.Caption });
                int wCells = MaxTextWidthPx(cellFont, DistinctTextsFromColumn(data, field));
                int w = Math.Max(wCaption, wCells);
                w += ColumnContentPaddingPx;
                w = Math.Max(hardMinPx, Math.Min(w, absoluteMaxPx));
                if (col.MinWidth > w)
                    col.MinWidth = Math.Max(hardMinPx, w);
                col.Width = w;
            }

            Fit(ColSerial, 72, 38);
            Fit(ColRank, 110, 42);
            Fit(ColTotal, 120, 46);
            Fit(ColPercent, 130, 50);
            Fit(ColLastSurah, 280, 44);
        }

        private void FillSummary(DataTable table)
        {
            if (table == null || table.Rows.Count == 0)
            {
                LblSummary.Text = "\u0644\u0627 \u062A\u0648\u062C\u062F \u0628\u064A\u0627\u0646\u0627\u062A.";
                return;
            }

            var rows = table.AsEnumerable().ToList();
            int totalStudents = rows.Count;
            double avg = Math.Round(rows.Average(r => r.Field<double>(ColPercent)), 1);
            double top = Math.Round(rows.Max(r => r.Field<double>(ColPercent)), 1);
            double low = Math.Round(rows.Min(r => r.Field<double>(ColPercent)), 1);
            int passed = rows.Count(r => r.Field<double>(ColPercent) >= 70);

            LblSummary.Text = $"\u0625\u062C\u0645\u0627\u0644\u064A \u0627\u0644\u0637\u0644\u0627\u0628: {totalStudents} | \u0645\u062A\u0648\u0633\u0637 \u0627\u0644\u0641\u0635\u0644: {avg}% | \u0623\u0639\u0644\u0649 \u062F\u0631\u062C\u0629: {top}% | \u0623\u062F\u0646\u0649 \u062F\u0631\u062C\u0629: {low}% | \u0639\u062F\u062F \u0627\u0644\u0646\u0627\u062C\u062D\u064A\u0646: {passed}";
        }

        private static Color BandColorForPercent(double p)
        {
            if (p < 70) return ColorTranslator.FromHtml("#b71c1c");
            if (p < 75) return ColorTranslator.FromHtml("#e53935");
            if (p < 80) return ColorTranslator.FromHtml("#fb8c00");
            if (p < 85) return ColorTranslator.FromHtml("#fdd835");
            if (p < 90) return ColorTranslator.FromHtml("#7cb342");
            return ColorTranslator.FromHtml("#2e7d32");
        }

        private bool IsSubjectColumnField(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName) || fieldName == "TopFlag") return false;
            return fieldName != ColSerial && fieldName != ColName && fieldName != ColLastSurah &&
                   fieldName != ColTotal && fieldName != ColPercent && fieldName != ColRank;
        }

        private void GrdSheet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            DataRow row = GrdSheet.GetDataRow(e.RowHandle);
            if (row == null) return;

            string field = e.Column?.FieldName;
            if (IsSubjectColumnField(field))
            {
                object cell = row[field];
                if (cell != null && cell != DBNull.Value)
                {
                    string s = cell.ToString();
                    if (!string.IsNullOrWhiteSpace(s) && s != "\u2014" &&
                        double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out double score) &&
                        subjectMaxCaps != null &&
                        subjectMaxCaps.TryGetValue(field, out double cap) && cap > 0)
                    {
                        double p = score / cap * 100.0;
                        e.Appearance.BackColor = BandColorForPercent(p);
                    }
                }
            }
            else if (field == ColPercent)
            {
                e.Appearance.BackColor = BandColorForPercent(row.Field<double>(ColPercent));
            }

            if (row.Field<bool>("TopFlag") &&
                (field == ColName || field == ColSerial || field == ColRank))
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            ConfigureGradeSheetPrintDefaultsOnView();

            using (PrintingSystem ps = new PrintingSystem())
            {
                PrintableComponentLink link = new PrintableComponentLink(ps);
                link.Component = GVSheet;
                link.Landscape = true;
                link.PaperKind = PaperKind.A4;
                link.Margins = new Margins(32, 32, 32, 32);
                link.CreateDocument();
                // ShowRibbonPreview ??? ????? ?? using ??? ????? PrintingSystem ????? ?????? ????????.
                link.ShowRibbonPreviewDialog(GVSheet.LookAndFeel);
            }
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files|*.xlsx";
                sfd.FileName = "\u0643\u0634\u0641_\u062F\u0631\u062C\u0627\u062A_\u0627\u0644\u0637\u0644\u0627\u0628.xlsx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GVSheet.ExportToXlsx(sfd.FileName);
                    MessageBox.Show("\u062A\u0645 \u0627\u0644\u062A\u0635\u062F\u064A\u0631 \u0625\u0644\u0649 Excel \u0628\u0646\u062C\u0627\u062D.");
                }
            }
        }

        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF Files|*.pdf";
                sfd.FileName = "\u0643\u0634\u0641_\u062F\u0631\u062C\u0627\u062A_\u0627\u0644\u0637\u0644\u0627\u0628.pdf";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GVSheet.ExportToPdf(sfd.FileName);
                    MessageBox.Show("\u062A\u0645 \u0627\u0644\u062A\u0635\u062F\u064A\u0631 \u0625\u0644\u0649 PDF \u0628\u0646\u062C\u0627\u062D.");
                }
            }
        }
    }
}
