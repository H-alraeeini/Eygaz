using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class FrmComprehensiveGradeReport : MetroFramework.Forms.MetroForm
    {
        private readonly Func f = new Func();
        private readonly AttendanceHelper helper = new AttendanceHelper();
        private DataTable viewTable;

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
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            if (CmbClass.SelectedValue == null || CmbTerm.SelectedItem == null)
            {
                MessageBox.Show("Ì—ÃÏ «Œ Ì«— «·›’· Ê«· —„.", " ‰»ÌÂ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int classId = Convert.ToInt32(CmbClass.SelectedValue);
            string term = CmbTerm.SelectedItem.ToString();
            string studentSearch = TxtSearch.Text.Trim();

            DataTable raw = helper.GetComprehensiveGradeSheetRaw(classId, term, studentSearch);
            viewTable = BuildComputedSheet(raw);
            GVSheet.DataSource = viewTable;
            ConfigureGridColumns();
            FillSummary(viewTable);
        }

        private DataTable BuildComputedSheet(DataTable raw)
        {
            DataTable table = new DataTable();
            table.Columns.Add("„", typeof(int));
            table.Columns.Add("«·«”„", typeof(string));
            table.Columns.Add("¬Œ— ”Ê—…", typeof(string));
            table.Columns.Add("«·„Ã„Ê⁄", typeof(double));
            table.Columns.Add("«·‰”»… %", typeof(double));
            table.Columns.Add("«· — Ì»", typeof(int));
            table.Columns.Add("TopFlag", typeof(bool));

            if (raw == null || raw.Rows.Count == 0)
                return table;

            var subjects = raw.AsEnumerable()
                .Select(r => r["SubjectName"] == DBNull.Value ? "" : r["SubjectName"].ToString())
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
                row["„"] = 0;
                row["«·«”„"] = group.Key.StudentName;
                row["¬Œ— ”Ê—…"] = ResolveLastSurah(group);
                row["«· — Ì»"] = 0;
                row["TopFlag"] = false;

                double total = 0;
                foreach (string subject in subjects)
                {
                    DataRow subjectRow = group.FirstOrDefault(r =>
                        string.Equals(
                            r["SubjectName"] == DBNull.Value ? "" : r["SubjectName"].ToString(),
                            subject,
                            StringComparison.Ordinal));

                    if (subjectRow == null || subjectRow["Score"] == DBNull.Value)
                    {
                        row[subject] = "ó";
                        continue;
                    }

                    double score = Convert.ToDouble(subjectRow["Score"]);
                    row[subject] = score.ToString("0.#", CultureInfo.InvariantCulture);
                    total += ConvertTo70(score);
                }

                double denominator = subjects.Count * 70.0;
                row["«·„Ã„Ê⁄"] = Math.Round(total, 1, MidpointRounding.AwayFromZero);
                row["«·‰”»… %"] = denominator <= 0 ? 0 : Math.Round((total / denominator) * 100.0, 1, MidpointRounding.AwayFromZero);

                table.Rows.Add(row);
            }

            var ordered = table.AsEnumerable()
                .OrderByDescending(r => r.Field<double>("«·„Ã„Ê⁄"))
                .ThenBy(r => r.Field<string>("«·«”„"))
                .ToList();

            int serial = 1;
            int rank = 0;
            double? prevTotal = null;
            for (int i = 0; i < ordered.Count; i++)
            {
                DataRow row = ordered[i];
                double total = row.Field<double>("«·„Ã„Ê⁄");
                if (!prevTotal.HasValue || Math.Abs(total - prevTotal.Value) > 0.0001)
                    rank = i + 1;

                row["„"] = serial++;
                row["«· — Ì»"] = rank;
                row["TopFlag"] = rank == 1;
                prevTotal = total;
            }

            DataView dv = table.DefaultView;
            dv.Sort = "«·„Ã„Ê⁄ DESC, «·«”„ ASC";
            return dv.ToTable();
        }

        private static double ConvertTo70(double scoreFrom100)
        {
            return Math.Round((scoreFrom100 / 100.0) * 70.0, 1, MidpointRounding.AwayFromZero);
        }

        private static string ResolveLastSurah(System.Collections.Generic.IEnumerable<DataRow> group)
        {
            DataRow hifzRow = group.FirstOrDefault(r =>
                string.Equals(
                    r["SubjectName"] == DBNull.Value ? "" : r["SubjectName"].ToString(),
                    "«·Õ›Ÿ",
                    StringComparison.Ordinal));

            if (hifzRow != null && hifzRow["LastSurah"] != DBNull.Value)
                return hifzRow["LastSurah"].ToString();

            DataRow any = group.FirstOrDefault(r =>
                r["LastSurah"] != DBNull.Value &&
                !string.IsNullOrWhiteSpace(r["LastSurah"].ToString()));

            return any == null ? "" : any["LastSurah"].ToString();
        }

        private void ConfigureGridColumns()
        {
            GrdSheet.BestFitColumns();
            GrdSheet.OptionsBehavior.Editable = false;
            GrdSheet.OptionsView.ShowGroupPanel = false;
            GrdSheet.OptionsView.ColumnAutoWidth = false;

            foreach (DevExpress.XtraGrid.Columns.GridColumn col in GrdSheet.Columns)
            {
                if (col.FieldName == "TopFlag")
                {
                    col.Visible = false;
                    continue;
                }

                if (col.FieldName == "„" || col.FieldName == "«·«”„" || col.FieldName == "¬Œ— ”Ê—…" ||
                    col.FieldName == "«·„Ã„Ê⁄" || col.FieldName == "«·‰”»… %" || col.FieldName == "«· — Ì»")
                    continue;

                col.Caption = col.FieldName + " /100";
            }
        }

        private void FillSummary(DataTable table)
        {
            if (table == null || table.Rows.Count == 0)
            {
                LblSummary.Text = "·«  ÊÃœ »Ì«‰« .";
                return;
            }

            var rows = table.AsEnumerable().ToList();
            int totalStudents = rows.Count;
            double avg = Math.Round(rows.Average(r => r.Field<double>("«·‰”»… %")), 1);
            double top = Math.Round(rows.Max(r => r.Field<double>("«·‰”»… %")), 1);
            double low = Math.Round(rows.Min(r => r.Field<double>("«·‰”»… %")), 1);
            int passed = rows.Count(r => r.Field<double>("«·‰”»… %") >= 60);

            LblSummary.Text = $"≈Ã„«·Ì «·ÿ·«»: {totalStudents} | „ Ê”ÿ «·›’·: {avg}% | √⁄·Ï œ—Ã…: {top}% | √œ‰Ï œ—Ã…: {low}% | ⁄œœ «·‰«ÃÕÌ‰: {passed}";
        }

        private void GrdSheet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            DataRow row = GrdSheet.GetDataRow(e.RowHandle);
            if (row == null) return;

            double percent = row.Field<double>("«·‰”»… %");
            if (percent < 60) e.Appearance.BackColor = Color.LightCoral;
            else if (percent < 75) e.Appearance.BackColor = Color.Moccasin;
            else if (percent < 90) e.Appearance.BackColor = Color.PaleGreen;
            else e.Appearance.BackColor = Color.LightGreen;

            if (row.Field<bool>("TopFlag"))
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            GVSheet.ShowRibbonPrintPreview();
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files|*.xlsx";
                sfd.FileName = "ﬂ‘›_œ—Ã« _«·ÿ·«».xlsx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GVSheet.ExportToXlsx(sfd.FileName);
                    MessageBox.Show(" „ «· ’œÌ— ≈·Ï Excel »‰Ã«Õ.");
                }
            }
        }

        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF Files|*.pdf";
                sfd.FileName = "ﬂ‘›_œ—Ã« _«·ÿ·«».pdf";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GVSheet.ExportToPdf(sfd.FileName);
                    MessageBox.Show(" „ «· ’œÌ— ≈·Ï PDF »‰Ã«Õ.");
                }
            }
        }
    }
}
