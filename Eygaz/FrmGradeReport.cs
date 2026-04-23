using DevExpress.XtraReports;
using System;
using System.Data;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class FrmGradeReport : MetroFramework.Forms.MetroForm
    {
        private readonly Func f = new Func();
        private readonly AttendanceHelper helper = new AttendanceHelper();

        public FrmGradeReport()
        {
            InitializeComponent();
        }

        private void FrmGradeReport_Load(object sender, EventArgs e)
        {
            f.DataCombo(CmbClass, "Classes", "ClassName", "Id", " WHERE IsActive = 0 ORDER BY ClassName");
            f.DataComboWithNull(CmbSubject, "Subjects", "SubjectName", "Id", " WHERE IsActive = 0 ORDER BY SubjectName");
            CmbTerm.Items.Clear();
            CmbTerm.Items.AddRange(new object[] { "First", "Second", "Final" });
            CmbTerm.SelectedIndex = 0;
        }

        private void CmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CmbClass.SelectedValue == null) return;
                int classId = Convert.ToInt32(CmbClass.SelectedValue);
                f.DataComboWithNull(CmbStudent, "Students", "FullName", "Id", $" WHERE ClassId = {classId} AND IsActive = 0 ORDER BY FullName");
            }
            catch { }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            if (CmbClass.SelectedValue == null || CmbTerm.SelectedItem == null)
                return;

            int classId = Convert.ToInt32(CmbClass.SelectedValue);
            int studentId = CmbStudent.SelectedValue == null ? 0 : Convert.ToInt32(CmbStudent.SelectedValue);
            int subjectId = CmbSubject.SelectedValue == null ? 0 : Convert.ToInt32(CmbSubject.SelectedValue);
            string term = CmbTerm.SelectedItem.ToString();

            DataTable report = helper.GetGradeReport(classId, term, studentId, subjectId);
            GVReport.DataSource = report;
            GrdReport.BestFitColumns();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            GVReport.ShowRibbonPrintPreview();
        }
    }
}
