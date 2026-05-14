using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
            DtExamDate.Value = DateTime.Today;
            EnsureHijriDateLabel();
            DtExamDate.ValueChanged += DtExamDate_ValueChanged;
            UpdateHijriDateLabel();
            TxtMaxScore.Text = "100";
            LblStatus.Text = "«Œ — «·›’· + «· —„ À„ «÷€ÿ  Õ„Ì· «·ÿ·«».";

            CmbClass.SelectedIndexChanged += Filters_SelectedIndexChanged;
            CmbTerm.SelectedIndexChanged += Filters_SelectedIndexChanged;
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
            lblHijriDate.Location = new Point(222, 44);
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

        private void BtnLoadStudents_Click(object sender, EventArgs e)
        {
            if (CmbClass.SelectedValue == null || CmbTerm.SelectedItem == null)
            {
                MessageBox.Show("Ì—ÃÏ «Œ Ì«— «·›’· + «· —„.");
                return;
            }

            if (isDirty)
            {
                DialogResult confirm = MessageBox.Show(
                    "·œÌﬂ  €ÌÌ—«  €Ì— „Õ›ÊŸ…° Â·  —Ìœ «·„ «»⁄… Ê›ﬁœ«‰Â«ø",
                    " ‰»ÌÂ",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;
            }

            int classId = Convert.ToInt32(CmbClass.SelectedValue);
            string term = CmbTerm.SelectedItem.ToString();

            DataTable dt = helper.GetGradeEntryMatrix(classId, term);
            if (dt == null) return;

            subjectColumnToId.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                if (!col.ColumnName.StartsWith("Sub_", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (int.TryParse(col.ColumnName.Substring(4), out int subjectId))
                    subjectColumnToId[col.ColumnName] = subjectId;
            }

            lastSurahSubjectId = helper.GetPreferredLastSurahSubjectId(classId, term);

            GVStudents.DataSource = dt;
            GrdStudents.BestFitColumns();
            if (GrdStudents.Columns["StudentId"] != null) GrdStudents.Columns["StudentId"].Visible = false;
            if (GrdStudents.Columns["StudentName"] != null) GrdStudents.Columns["StudentName"].Caption = "«”„ «·ÿ«·»";
            if (GrdStudents.Columns["StudentName"] != null) GrdStudents.Columns["StudentName"].OptionsColumn.ReadOnly = true;
            if (GrdStudents.Columns["LastSurah"] != null) GrdStudents.Columns["LastSurah"].Caption = "¬Œ— ”Ê—…";

            foreach (var col in subjectColumnToId)
            {
                if (GrdStudents.Columns[col.Key] == null) continue;
                string subjectName = dt.Columns[col.Key].Caption;
                GrdStudents.Columns[col.Key].Caption = string.IsNullOrWhiteSpace(subjectName) ? col.Key : subjectName;
            }

            if (lastSurahSubjectId > 0)
            {
                int examId = helper.GetExamId(classId, lastSurahSubjectId, term);
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

            LblStatus.Text = $" „  Õ„Ì· {dt.Rows.Count} ÿ«·»« Ê {subjectColumnToId.Count} „«œ… ? œ—Ã«  „”Ã·…: {existingScores}.";
            isDirty = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!AuthSession.HasPermission("grades.manage"))
            {
                MessageBox.Show("·Ì” ·œÌﬂ ’·«ÕÌ… ≈œ«—… «·œ—Ã« .");
                return;
            }

            if (CmbClass.SelectedValue == null || CmbTerm.SelectedItem == null)
            {
                MessageBox.Show("Ì—ÃÏ  ⁄»∆… »Ì«‰«  «·«Œ »«—.");
                return;
            }

            if (!double.TryParse(TxtMaxScore.Text, out double maxScore) || maxScore <= 0)
            {
                MessageBox.Show("«·œ—Ã… «·⁄Ÿ„Ï €Ì— ’ÕÌÕ….");
                return;
            }

            DataTable students = GVStudents.DataSource as DataTable;
            if (students == null || students.Rows.Count == 0)
            {
                MessageBox.Show("·« ÌÊÃœ ÿ·«».");
                return;
            }

            if (subjectColumnToId.Count == 0)
            {
                MessageBox.Show("·«  ÊÃœ „Ê«œ ··Õ›Ÿ.");
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
                        MessageBox.Show("œ—Ã… €Ì— ’ÕÌÕ… ··ÿ«·»: " + row["StudentName"] + " ? «·„«œ…: " + subjectName);
                        return;
                    }
                }
            }

            if (totalCells > 0 && (blankCount * 100.0 / totalCells) > 30.0)
            {
                DialogResult confirm = MessageBox.Show(
                    $"Â‰«ﬂ {blankCount} Œ·Ì… œ—Ã«  ›«—€…. Â·  —Ìœ «·„ «»⁄…ø",
                    " √ﬂÌœ «·Õ›Ÿ",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;
            }

            bool ok = helper.SaveGradeEntryMatrix(
                classId,
                term,
                students,
                subjectColumnToId,
                lastSurahSubjectId > 0 ? (int?)lastSurahSubjectId : null,
                DtExamDate.Value.ToString("yyyy-MM-dd"),
                maxScore,
                TxtDescription.Text.Trim(),
                out string errorMessage);
            if (!ok)
            {
                MessageBox.Show("›‘· «·Õ›Ÿ: " + errorMessage);
                return;
            }

            MessageBox.Show(" „ Õ›Ÿ «·œ—Ã«  »‰Ã«Õ.");
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


