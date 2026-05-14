using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class FrmGradeEntry : MetroFramework.Forms.MetroForm
    {
        // ???? ????? ???????? ????? Unicode ?????? ????? ????? ????? (???? ??? ??? ???????).
        private static class T
        {
            public const string InitialHint =
                "\u0627\u062e\u062a\u0631\u0020\u0627\u0644\u0641\u0635\u0644\u0020\u0648\u0627\u0644\u062a\u0631\u0645\u0020\u0648\u0627\u0644\u0633\u0646\u0629\u0020\u0648\u0627\u0644\u0634\u0647\u0631\u0020\u062b\u0645\u0020\u0627\u0636\u063a\u0637\u0020\u062a\u062d\u0645\u064a\u0644\u0020\u0627\u0644\u0637\u0644\u0627\u0628\u002e";
            public const string PickClassTerm =
                "\u064a\u0631\u062c\u0649\u0020\u0627\u062e\u062a\u064a\u0627\u0631\u0020\u0627\u0644\u0641\u0635\u0644\u0020\u0648\u0627\u0644\u062a\u0631\u0645\u002e";
            public const string PickYearMonthLoad =
                "\u064a\u0631\u062c\u0649\u0020\u0627\u062e\u062a\u064a\u0627\u0631\u0020\u0627\u0644\u0633\u0646\u0629\u0020\u0648\u0627\u0644\u0634\u0647\u0631\u0020\u0644\u0645\u062a\u0627\u0628\u0639\u0629\u0020\u0627\u0644\u062f\u0631\u062c\u0627\u062a\u002e";
            public const string PickYearMonthSave =
                "\u064a\u0631\u062c\u0649\u0020\u0627\u062e\u062a\u064a\u0627\u0631\u0020\u0627\u0644\u0633\u0646\u0629\u0020\u0648\u0627\u0644\u0634\u0647\u0631\u002e";
            public const string UnsavedBody =
                "\u0644\u062f\u064a\u0643\u0020\u062a\u063a\u064a\u064a\u0631\u0627\u062a\u0020\u063a\u064a\u0631\u0020\u0645\u062d\u0641\u0648\u0638\u0629\u002e\u0020\u0647\u0644\u0020\u062a\u0631\u064a\u062f\u0020\u0627\u0644\u0645\u062a\u0627\u0628\u0639\u0629\u0020\u0648\u0641\u0642\u062f\u0627\u0646\u0647\u0627\u061f";
            public const string UnsavedTitle = "\u062a\u0646\u0628\u064a\u0647";
            public const string ColStudentName = "\u0627\u0633\u0645\u0020\u0627\u0644\u0637\u0627\u0644\u0628";
            public const string ColLastSurah = "\u0622\u062e\u0631\u0020\u0633\u0648\u0631\u0629";
            public const string NoPermission =
                "\u0644\u064a\u0633\u0020\u0644\u062f\u064a\u0643\u0020\u0635\u0644\u0627\u062d\u064a\u0629\u0020\u0625\u062f\u0627\u0631\u0629\u0020\u0627\u0644\u062f\u0631\u062c\u0627\u062a\u002e";
            public const string FillSelection =
                "\u064a\u0631\u062c\u0649\u0020\u062a\u0639\u0628\u0626\u0629\u0020\u0628\u064a\u0627\u0646\u0627\u062a\u0020\u0627\u0644\u0627\u062e\u062a\u064a\u0627\u0631\u002e";
            public const string MaxScoreInvalid =
                "\u0627\u0644\u062f\u0631\u062c\u0629\u0020\u0627\u0644\u0639\u0638\u0645\u0649\u0020\u063a\u064a\u0631\u0020\u0635\u062d\u064a\u062d\u0629\u002e";
            public const string NoStudents = "\u0644\u0627\u0020\u064a\u0648\u062c\u062f\u0020\u0637\u0644\u0627\u0628\u002e";
            public const string NoSubjects = "\u0644\u0627\u0020\u062a\u0648\u062c\u062f\u0020\u0645\u0648\u0627\u062f\u0020\u0644\u0644\u062d\u0641\u0638\u002e";
            public const string BadScorePrefix =
                "\u062f\u0631\u062c\u0629\u0020\u063a\u064a\u0631\u0020\u0635\u062d\u064a\u062d\u0629\u0020\u0644\u0644\u0637\u0627\u0644\u0628\u003a\u0020";
            public const string BadScoreSubject = "\u0020\u0627\u0644\u0645\u0627\u062f\u0629\u003a\u0020";
            public const string BlankThere = "\u0647\u0646\u0627\u0643\u0020";
            public const string BlankSuffix =
                "\u0020\u062e\u0644\u0627\u064a\u0627\u0020\u062f\u0631\u062c\u0627\u062a\u0020\u0641\u0627\u0631\u063a\u0629\u002e\u0020\u0647\u0644\u0020\u062a\u0631\u064a\u062f\u0020\u0627\u0644\u0645\u062a\u0627\u0628\u0639\u0629\u061f";
            public const string ConfirmSaveTitle = "\u062a\u0623\u0643\u064a\u062f\u0020\u0627\u0644\u062d\u0641\u0638";
            public const string SaveFailPrefix = "\u0641\u0634\u0644\u0020\u0627\u0644\u062d\u0641\u0638\u003a\u0020";
            public const string SaveSuccess = "\u062a\u0645\u0020\u062d\u0641\u0638\u0020\u0627\u0644\u062f\u0631\u062c\u0627\u062a\u0020\u0628\u0646\u062c\u0627\u062d\u002e";
            public const string Loaded1 = "\u062a\u0645\u0020\u062a\u062d\u0645\u064a\u0644\u0020";
            public const string Loaded2 = "\u0020\u0637\u0627\u0644\u0628\u0627\u064b\u0020\u0648\u0020";
            public const string Loaded3 = "\u0020\u0645\u0627\u062f\u0629\u002e\u0020\u062f\u0631\u062c\u0627\u062a\u0020\u0645\u0633\u062c\u0644\u0629\u003a\u0020";
        }

        private readonly Func f = new Func();
        private readonly AttendanceHelper helper = new AttendanceHelper();
        private readonly Dictionary<string, int> subjectColumnToId = new Dictionary<string, int>();
        private bool isDirty;
        private int lastSurahSubjectId;
        private Label lblHijriDate;

        public FrmGradeEntry()
        {
            InitializeComponent();
        }

        private void FrmGradeEntry_Load(object sender, EventArgs e)
        {
            f.DataCombo(CmbClass, "Classes", "ClassName", "Id", " WHERE IsActive = 0 ORDER BY ClassName");
            f.DataCombo(CmbSubject, "Subjects", "SubjectName", "Id", " WHERE IsActive = 0 ORDER BY SubjectName");
            CmbSubject.Enabled = false;
            CmbTerm.Items.AddRange(new object[] { "First", "Second", "Final" });
            CmbTerm.SelectedIndex = 0;

            int currentYear = DateTime.Today.Year;
            for (int yr = currentYear - 2; yr <= currentYear + 3; yr++)
                CmbGradeYear.Items.Add(yr);
            CmbGradeYear.SelectedItem = currentYear;

            var arCulture = new CultureInfo("ar-SA");
            CmbGradeMonth.Items.Clear();
            for (int m = 1; m <= 12; m++)
                CmbGradeMonth.Items.Add($"{m} - {arCulture.DateTimeFormat.GetMonthName(m)}");
            CmbGradeMonth.SelectedIndex = DateTime.Today.Month - 1;

            DtExamDate.Value = DateTime.Today;
            EnsureHijriDateLabel();
            DtExamDate.ValueChanged += DtExamDate_ValueChanged;
            UpdateHijriDateLabel();
            TxtMaxScore.Text = "100";
            LblStatus.Text = T.InitialHint;

            CmbClass.SelectedIndexChanged += Filters_SelectedIndexChanged;
            CmbTerm.SelectedIndexChanged += Filters_SelectedIndexChanged;
            CmbGradeYear.SelectedIndexChanged += Filters_SelectedIndexChanged;
            CmbGradeMonth.SelectedIndexChanged += Filters_SelectedIndexChanged;
            GrdStudents.RowStyle += GrdStudents_RowStyle;
            GrdStudents.CellValueChanged += GrdStudents_CellValueChanged;
        }

        private void EnsureHijriDateLabel()
        {
            if (lblHijriDate != null) return;

            lblHijriDate = new Label();
            lblHijriDate.AutoSize = true;
            lblHijriDate.ForeColor = Color.DarkSlateBlue;
            lblHijriDate.Font = new Font("Tahoma", 8F, FontStyle.Bold);
            lblHijriDate.Location = new Point(DtExamDate.Left, 44);
            lblHijriDate.Name = "lblHijriDate";
            Controls.Add(lblHijriDate);
            lblHijriDate.RightToLeft = RightToLeft.No;
            lblHijriDate.BringToFront();
        }

        private void DtExamDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateHijriDateLabel();
        }

        private void UpdateHijriDateLabel()
        {
            if (lblHijriDate == null) return;
            string hijri = AttendanceHelper.ToHijriDateDisplayArabic(DtExamDate.Value.Date);
            lblHijriDate.Text = hijri;
        }

        private int GetSelectedGradeYear()
        {
            if (CmbGradeYear.SelectedItem == null) return 0;
            return Convert.ToInt32(CmbGradeYear.SelectedItem);
        }

        private int GetSelectedGradeMonth() => CmbGradeMonth.SelectedIndex + 1;

        private void BtnLoadStudents_Click(object sender, EventArgs e)
        {
            if (CmbClass.SelectedValue == null || CmbTerm.SelectedItem == null)
            {
                MessageBox.Show(T.PickClassTerm);
                return;
            }

            int gradeYear = GetSelectedGradeYear();
            int gradeMonth = GetSelectedGradeMonth();
            if (gradeYear <= 0 || gradeMonth < 1 || gradeMonth > 12)
            {
                MessageBox.Show(T.PickYearMonthLoad);
                return;
            }

            if (isDirty)
            {
                DialogResult confirm = MessageBox.Show(
                    T.UnsavedBody,
                    T.UnsavedTitle,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;
            }

            int classId = Convert.ToInt32(CmbClass.SelectedValue);
            string term = CmbTerm.SelectedItem.ToString();

            DataTable dt = helper.GetGradeEntryMatrix(classId, term, gradeYear, gradeMonth);
            if (dt == null) return;

            subjectColumnToId.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                if (!col.ColumnName.StartsWith("Sub_", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (int.TryParse(col.ColumnName.Substring(4), out int subjectId))
                    subjectColumnToId[col.ColumnName] = subjectId;
            }

            lastSurahSubjectId = helper.GetPreferredLastSurahSubjectId(classId, term, gradeYear, gradeMonth);

            GVStudents.DataSource = dt;
            GrdStudents.BestFitColumns();
            if (GrdStudents.Columns["StudentId"] != null) GrdStudents.Columns["StudentId"].Visible = false;
            if (GrdStudents.Columns["StudentName"] != null) GrdStudents.Columns["StudentName"].Caption = T.ColStudentName;
            if (GrdStudents.Columns["StudentName"] != null) GrdStudents.Columns["StudentName"].OptionsColumn.ReadOnly = true;
            if (GrdStudents.Columns["LastSurah"] != null) GrdStudents.Columns["LastSurah"].Caption = T.ColLastSurah;

            foreach (var col in subjectColumnToId)
            {
                if (GrdStudents.Columns[col.Key] == null) continue;
                string subjectName = dt.Columns[col.Key].Caption;
                GrdStudents.Columns[col.Key].Caption = string.IsNullOrWhiteSpace(subjectName) ? col.Key : subjectName;
            }

            if (lastSurahSubjectId > 0)
            {
                int examId = helper.GetExamId(classId, lastSurahSubjectId, term, gradeYear, gradeMonth);
                if (examId > 0)
                    TxtMaxScore.Text = helper.GetExamMaxScore(examId).ToString("0.##");
            }
            else
            {
                TxtMaxScore.Text = "100";
            }

            int existingScores = 0;
            foreach (DataRow row in dt.Rows)
            {
                foreach (string scoreColumn in subjectColumnToId.Keys)
                {
                    object val = row[scoreColumn];
                    if (val != DBNull.Value && !string.IsNullOrWhiteSpace(val.ToString()))
                        existingScores++;
                }
            }

            LblStatus.Text = T.Loaded1 + dt.Rows.Count + T.Loaded2 + subjectColumnToId.Count + T.Loaded3 + existingScores + ".";
            isDirty = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!AuthSession.HasPermission("grades.manage"))
            {
                MessageBox.Show(T.NoPermission);
                return;
            }

            if (CmbClass.SelectedValue == null || CmbTerm.SelectedItem == null)
            {
                MessageBox.Show(T.FillSelection);
                return;
            }

            int gradeYearSave = GetSelectedGradeYear();
            int gradeMonthSave = GetSelectedGradeMonth();
            if (gradeYearSave <= 0 || gradeMonthSave < 1 || gradeMonthSave > 12)
            {
                MessageBox.Show(T.PickYearMonthSave);
                return;
            }

            if (!double.TryParse(TxtMaxScore.Text, out double maxScore) || maxScore <= 0)
            {
                MessageBox.Show(T.MaxScoreInvalid);
                return;
            }

            DataTable students = GVStudents.DataSource as DataTable;
            if (students == null || students.Rows.Count == 0)
            {
                MessageBox.Show(T.NoStudents);
                return;
            }

            if (subjectColumnToId.Count == 0)
            {
                MessageBox.Show(T.NoSubjects);
                return;
            }

            int classId = Convert.ToInt32(CmbClass.SelectedValue);
            string term = CmbTerm.SelectedItem.ToString();

            int blankCount = 0;
            int totalCells = students.Rows.Count * subjectColumnToId.Count;
            foreach (DataRow row in students.Rows)
            {
                foreach (var subject in subjectColumnToId)
                {
                    object scoreObj = row[subject.Key];
                    string scoreText = scoreObj == DBNull.Value ? "" : scoreObj.ToString().Trim();

                    if (string.IsNullOrWhiteSpace(scoreText))
                    {
                        blankCount++;
                        continue;
                    }

                    if (!double.TryParse(scoreText, out double score) || score < 0 || score > maxScore)
                    {
                        string subjectName = GrdStudents.Columns[subject.Key] == null
                            ? subject.Key
                            : GrdStudents.Columns[subject.Key].Caption;
                        MessageBox.Show(T.BadScorePrefix + row["StudentName"] + T.BadScoreSubject + subjectName);
                        return;
                    }
                }
            }

            if (totalCells > 0 && (blankCount * 100.0 / totalCells) > 30.0)
            {
                DialogResult confirm = MessageBox.Show(
                    T.BlankThere + blankCount + T.BlankSuffix,
                    T.ConfirmSaveTitle,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;
            }

            bool ok = helper.SaveGradeEntryMatrix(
                classId,
                term,
                gradeYearSave,
                gradeMonthSave,
                students,
                subjectColumnToId,
                lastSurahSubjectId > 0 ? (int?)lastSurahSubjectId : null,
                DtExamDate.Value.ToString("yyyy-MM-dd"),
                maxScore,
                TxtDescription.Text.Trim(),
                out string errorMessage);
            if (!ok)
            {
                MessageBox.Show(T.SaveFailPrefix + errorMessage);
                return;
            }

            MessageBox.Show(T.SaveSuccess);
            isDirty = false;
            BtnLoadStudents_Click(sender, e);
        }

        private void GrdStudents_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            DataRow row = GrdStudents.GetDataRow(e.RowHandle);
            if (row == null) return;

            foreach (string scoreColumn in subjectColumnToId.Keys)
            {
                if (!row.Table.Columns.Contains(scoreColumn)) continue;
                object value = row[scoreColumn];
                if (value != DBNull.Value && !string.IsNullOrWhiteSpace(value.ToString()))
                {
                    e.Appearance.BackColor = Color.LightBlue;
                    break;
                }
            }
        }

        private void GrdStudents_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0) isDirty = true;
        }

        private void Filters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isDirty) return;
        }
    }
}
