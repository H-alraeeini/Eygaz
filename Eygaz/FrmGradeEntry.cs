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
            LblStatus.Text = "???? ????? + ????? ?? ???? ????? ??????.";

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
                MessageBox.Show("???? ?????? ????? + ?????.");
                return;
            }

            int gradeYear = GetSelectedGradeYear();
            int gradeMonth = GetSelectedGradeMonth();
            if (gradeYear <= 0 || gradeMonth < 1 || gradeMonth > 12)
            {
                MessageBox.Show("\u064a\u0631\u062c\u0649 \u0627\u062e\u062a\u064a\u0627\u0631 \u0627\u0644\u0633\u0646\u0629 \u0648\u0627\u0644\u0634\u0647\u0631 \u0644\u0645\u062a\u0627\u0628\u0639\u0629 \u0627\u0644\u062f\u0631\u062c\u0627\u062a.");
                return;
            }

            if (isDirty)
            {
                DialogResult confirm = MessageBox.Show(
                    "???? ??????? ??? ?????? ?? ???? ???????? ????????",
                    "?????",
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
            if (GrdStudents.Columns["StudentName"] != null) GrdStudents.Columns["StudentName"].Caption = "??? ??????";
            if (GrdStudents.Columns["StudentName"] != null) GrdStudents.Columns["StudentName"].OptionsColumn.ReadOnly = true;
            if (GrdStudents.Columns["LastSurah"] != null) GrdStudents.Columns["LastSurah"].Caption = "??? ????";

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

            LblStatus.Text = $"?? ????? {dt.Rows.Count} ?????? ? {subjectColumnToId.Count} ???? ? ????? ?????: {existingScores}.";
            isDirty = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!AuthSession.HasPermission("grades.manage"))
            {
                MessageBox.Show("??? ???? ?????? ????? ???????.");
                return;
            }

            if (CmbClass.SelectedValue == null || CmbTerm.SelectedItem == null)
            {
                MessageBox.Show("???? ????? ?????? ????????.");
                return;
            }

            int gradeYearSave = GetSelectedGradeYear();
            int gradeMonthSave = GetSelectedGradeMonth();
            if (gradeYearSave <= 0 || gradeMonthSave < 1 || gradeMonthSave > 12)
            {
                MessageBox.Show("\u064a\u0631\u062c\u0649 \u0627\u062e\u062a\u064a\u0627\u0631 \u0627\u0644\u0633\u0646\u0629 \u0648\u0627\u0644\u0634\u0647\u0631.");
                return;
            }

            if (!double.TryParse(TxtMaxScore.Text, out double maxScore) || maxScore <= 0)
            {
                MessageBox.Show("?????? ?????? ??? ?????.");
                return;
            }

            DataTable students = GVStudents.DataSource as DataTable;
            if (students == null || students.Rows.Count == 0)
            {
                MessageBox.Show("?? ???? ????.");
                return;
            }

            if (subjectColumnToId.Count == 0)
            {
                MessageBox.Show("?? ???? ???? ?????.");
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
                        MessageBox.Show("???? ??? ????? ??????: " + row["StudentName"] + " ? ??????: " + subjectName);
                        return;
                    }
                }
            }

            if (totalCells > 0 && (blankCount * 100.0 / totalCells) > 30.0)
            {
                DialogResult confirm = MessageBox.Show(
                    $"???? {blankCount} ???? ????? ?????. ?? ???? ????????",
                    "????? ?????",
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
                MessageBox.Show("??? ?????: " + errorMessage);
                return;
            }

            MessageBox.Show("?? ??? ??????? ?????.");
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
            // user warned on explicit load; no extra popup here
        }
    }
}


