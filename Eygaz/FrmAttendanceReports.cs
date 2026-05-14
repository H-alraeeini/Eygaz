using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class FrmAttendanceReports : MetroFramework.Forms.MetroForm
    {
        Func f = new Func();
        AttendanceHelper helper = new AttendanceHelper();
        private Label lblHijriFrom;
        private Label lblHijriTo;

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
                EnsureHijriDateLabels();
                DtFrom.ValueChanged += DateRange_ValueChanged;
                DtTo.ValueChanged += DateRange_ValueChanged;
                UpdateHijriDateLabels();

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
            if (lblHijriFrom != null) lblHijriFrom.Visible = showDateRange;
            if (lblHijriTo != null) lblHijriTo.Visible = showDateRange;
            if (showDateRange) UpdateHijriDateLabels();
        }

        private void EnsureHijriDateLabels()
        {
            if (lblHijriFrom == null)
            {
                lblHijriFrom = new Label();
                lblHijriFrom.AutoSize = true;
                lblHijriFrom.ForeColor = Color.DarkSlateBlue;
                lblHijriFrom.Font = new Font("Tahoma", 7.5F, FontStyle.Bold);
                lblHijriFrom.Location = new System.Drawing.Point(360, 63);
                lblHijriFrom.Name = "lblHijriFrom";
                lblHijriFrom.RightToLeft = RightToLeft.No;
                PnlFilter.Controls.Add(lblHijriFrom);
            }

            if (lblHijriTo == null)
            {
                lblHijriTo = new Label();
                lblHijriTo.AutoSize = true;
                lblHijriTo.ForeColor = Color.DarkSlateBlue;
                lblHijriTo.Font = new Font("Tahoma", 7.5F, FontStyle.Bold);
                lblHijriTo.Location = new System.Drawing.Point(210, 63);
                lblHijriTo.Name = "lblHijriTo";
                lblHijriTo.RightToLeft = RightToLeft.No;
                PnlFilter.Controls.Add(lblHijriTo);
            }
        }

        private void DateRange_ValueChanged(object sender, EventArgs e)
        {
            if (CmbReportType.SelectedIndex == 1 || CmbReportType.SelectedIndex == 3)
                UpdateHijriDateLabels();
        }

        private void UpdateHijriDateLabels()
        {
            if (lblHijriFrom != null)
                lblHijriFrom.Text = AttendanceHelper.ToHijriDateDisplayArabic(DtFrom.Value.Date);
            if (lblHijriTo != null)
                lblHijriTo.Text = AttendanceHelper.ToHijriDateDisplayArabic(DtTo.Value.Date);
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

        /// <summary>نص توضيحي للطباعة حسب نوع التقرير والفلاتر الحالية.</summary>
        private string BuildPrintHeader()
        {
            int rt = CmbReportType.SelectedIndex;
            var lines = new List<string>
            {
                "تقارير الحضور والغياب",
                CmbReportType.Text ?? ""
            };

            string classText = "";
            try
            {
                if (CmbClass.SelectedValue != null && !string.IsNullOrWhiteSpace(CmbClass.SelectedValue.ToString()))
                    classText = CmbClass.Text ?? "";
            }
            catch { }

            if (rt == 0)
            {
                lines.Add($"الطالب: {CmbStudent.Text ?? ""}");
                if (!string.IsNullOrWhiteSpace(classText))
                    lines.Add($"الفصل: {classText}");
            }
            else if (rt == 1 || rt == 3)
            {
                lines.Add(string.IsNullOrWhiteSpace(classText) ? "الفصل: الكل" : $"الفصل: {classText}");
                lines.Add($"من {DtFrom.Value:yyyy-MM-dd} إلى {DtTo.Value:yyyy-MM-dd} (ميلادي)");
                lines.Add($"هجرياً: من {AttendanceHelper.ToHijriDateDisplayArabic(DtFrom.Value.Date)} إلى {AttendanceHelper.ToHijriDateDisplayArabic(DtTo.Value.Date)}");
            }
            else if (rt == 2)
            {
                if (!string.IsNullOrWhiteSpace(classText))
                    lines.Add($"الفصل: {classText}");
            }

            if (rt == 0 || rt == 2 || rt == 4)
                lines.Add($"الشهر: {CmbMonth.Text} — السنة: {CmbYear.Text}");

            return string.Join("\r\n", lines);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (GVReport.DataSource == null || ((DataTable)GVReport.DataSource).Rows.Count == 0)
            {
                MessageBox.Show("لا توجد بيانات لتصديرها. اعرض التقرير أولاً.", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.Title = "حفظ تقرير الحضور والغياب — Excel";
                    sfd.FileName = "تقرير_الحضور_والغياب.xlsx";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        GrdReport.ExportToXlsx(sfd.FileName);
                        MessageBox.Show("تم التصدير بنجاح.", "تصدير", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء التصدير: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (GVReport.DataSource == null || ((DataTable)GVReport.DataSource).Rows.Count == 0)
            {
                MessageBox.Show("لا توجد بيانات لطباعتها. اعرض التقرير أولاً.", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string headerText = BuildPrintHeader();
                using (PrintingSystem printingSystem = new PrintingSystem())
                {
                    PrintableComponentLink link = new PrintableComponentLink(printingSystem)
                    {
                        Component = GVReport,
                        Margins = new Margins(50, 50, 80, 50),
                        PaperKind = PaperKind.A4,
                    };

                    PageHeaderFooter phf = link.PageHeaderFooter as PageHeaderFooter;
                    if (phf != null)
                    {
                        phf.Header.Content.Clear();
                        phf.Header.Content.AddRange(new[] { "", "", headerText });
                        phf.Header.Font = new Font("Tahoma", 11f, FontStyle.Bold);
                        phf.Header.LineAlignment = BrickAlignment.Center;
                    }

                    link.CreateDocument();
                    link.ShowRibbonPreviewDialog(GVReport.LookAndFeel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء الطباعة: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
