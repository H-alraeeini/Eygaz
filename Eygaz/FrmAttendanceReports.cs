using System;
using System.Data;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class FrmAttendanceReports : MetroFramework.Forms.MetroForm
    {
        Func f = new Func();
        AttendanceHelper helper = new AttendanceHelper();

        public FrmAttendanceReports()
        {
            InitializeComponent();
        }

        private void FrmAttendanceReports_Load(object sender, EventArgs e)
        {
            try
            {
                // أنواع التقارير
                CmbReportType.Items.Clear();
                CmbReportType.Items.Add("تقرير حضور طالب");
                CmbReportType.Items.Add("تقرير الغياب");
                CmbReportType.Items.Add("تقرير حضور الفصل");
                CmbReportType.Items.Add("تقرير المتأخرين");
                CmbReportType.Items.Add("التقرير الشهري الشامل");
                CmbReportType.SelectedIndex = 0;

                // الفصول
                f.DataComboWithNull(CmbClass, "Classes", "ClassName", "Id", " WHERE IsActive = 0 ORDER BY ClassName");

                // الأشهر
                CmbMonth.Items.Clear();
                string[] months = { "يناير", "فبراير", "مارس", "أبريل", "مايو", "يونيو",
                                    "يوليو", "أغسطس", "سبتمبر", "أكتوبر", "نوفمبر", "ديسمبر" };
                for (int i = 0; i < months.Length; i++)
                    CmbMonth.Items.Add(months[i]);
                CmbMonth.SelectedIndex = DateTime.Today.Month - 1;

                // السنوات
                CmbYear.Items.Clear();
                for (int y = DateTime.Today.Year - 3; y <= DateTime.Today.Year + 1; y++)
                    CmbYear.Items.Add(y.ToString());
                CmbYear.SelectedItem = DateTime.Today.Year.ToString();

                // التواريخ
                DtFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DtTo.Value = DateTime.Today;

                UpdateFilterVisibility();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // تغيير نوع التقرير → إظهار/إخفاء الفلاتر المناسبة
        // =============================================
        private void CmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFilterVisibility();
        }

        private void UpdateFilterVisibility()
        {
            int rt = CmbReportType.SelectedIndex;
            // 0=طالب, 1=غياب, 2=فصل, 3=متأخرين, 4=شهري شامل

            CmbStudent.Visible = (rt == 0);
            lblStudent.Visible = (rt == 0);

            CmbClass.Visible = (rt == 0 || rt == 1 || rt == 2 || rt == 3);
            lblClass.Visible = CmbClass.Visible;

            bool showMonthYear = (rt == 0 || rt == 2 || rt == 4);
            CmbMonth.Visible = showMonthYear;
            lblMonth.Visible = showMonthYear;
            CmbYear.Visible = showMonthYear;
            lblYear.Visible = showMonthYear;

            bool showDateRange = (rt == 1 || rt == 3);
            DtFrom.Visible = showDateRange;
            lblDateFrom.Visible = showDateRange;
            DtTo.Visible = showDateRange;
            lblDateTo.Visible = showDateRange;
        }

        // =============================================
        // تحميل الطلاب عند اختيار الفصل
        // =============================================
        private void CmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CmbClass.SelectedValue != null && CmbClass.SelectedValue.ToString() != "")
                {
                    int classId = Convert.ToInt32(CmbClass.SelectedValue);
                    f.DataCombo(CmbStudent, "Students", "FullName", "Id",
                        $" WHERE ClassId = {classId} AND IsActive = 0 ORDER BY FullName");
                }
            }
            catch { }
        }

        // =============================================
        // إنشاء التقرير
        // =============================================
        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable result = null;
                int reportType = CmbReportType.SelectedIndex;

                int month = CmbMonth.SelectedIndex + 1;
                int year = int.Parse(CmbYear.SelectedItem?.ToString() ?? DateTime.Today.Year.ToString());
                string dateFrom = DtFrom.Value.ToString("yyyy-MM-dd");
                string dateTo = DtTo.Value.ToString("yyyy-MM-dd");

                switch (reportType)
                {
                    case 0: // تقرير حضور طالب
                        if (CmbStudent.SelectedValue == null)
                        {
                            MessageBox.Show("يرجى اختيار الطالب", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        int studentId = Convert.ToInt32(CmbStudent.SelectedValue);
                        result = helper.GetStudentMonthlyReport(studentId, month, year);
                        break;

                    case 1: // تقرير الغياب
                        int classIdAbsent = CmbClass.SelectedValue != null ? Convert.ToInt32(CmbClass.SelectedValue) : 0;
                        result = helper.GetAbsentReport(classIdAbsent, dateFrom, dateTo);
                        break;

                    case 2: // تقرير حضور الفصل
                        if (CmbClass.SelectedValue == null)
                        {
                            MessageBox.Show("يرجى اختيار الفصل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        int classIdReport = Convert.ToInt32(CmbClass.SelectedValue);
                        result = helper.GetClassMonthlyReport(classIdReport, month, year);
                        break;

                    case 3: // تقرير المتأخرين
                        int classIdLate = CmbClass.SelectedValue != null ? Convert.ToInt32(CmbClass.SelectedValue) : 0;
                        result = helper.GetLateReport(classIdLate, dateFrom, dateTo);
                        break;

                    case 4: // التقرير الشهري الشامل
                        result = helper.GetMonthlyOverviewReport(month, year);
                        break;
                }

                if (result != null && result.Rows.Count > 0)
                {
                    GVReport.DataSource = result;
                    GrdReport.BestFitColumns();

                    // RTL
                    if (Func.vRtL && GrdReport.Columns.Count > 0)
                    {
                        int maxCol = GrdReport.Columns.Count;
                        for (int i = 0; i < maxCol; i++)
                            GrdReport.Columns[i].VisibleIndex = maxCol - i - 1;
                    }
                }
                else
                {
                    GVReport.DataSource = null;
                    MessageBox.Show("لا توجد بيانات للتقرير المطلوب", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء إنشاء التقرير: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
