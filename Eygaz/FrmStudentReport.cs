using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DevExpress.XtraPrinting;

namespace Eygaz
{
    public partial class FrmStudentReport : MetroFramework.Forms.MetroForm
    {
        Func f = new Func();

        public FrmStudentReport()
        {
            InitializeComponent();
        }

        private void FrmStudentReport_Load(object sender, EventArgs e)
        {
            try
            {
                // تعبئة قائمة الفصول — نفس منطق الشاشات الأخرى (FrmStudent، الدرجات، الحضور): IsActive = 0 تعني ظهور السجل في القوائم
                f.DataComboWithNull(CmbClass, "Classes", "ClassName", "Id", " where IsActive=0 order by Id ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء تحميل البيانات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string query = @"
                    SELECT 
                        s.Id AS [رقم الطالب],
                        s.FullName AS [اسم الطالب],
                        c.ClassName AS [اسم الفصل],
                        s.Phone AS [رقم الهاتف],
                        s.Address AS [العنوان]
                    FROM Students s
                    LEFT JOIN Classes c ON s.ClassId = c.Id
                    WHERE 1 = 1 ";

                // فلترة حسب الفصل
                if (CmbClass.SelectedValue != null && CmbClass.SelectedValue.ToString() != "0")
                {
                    query += $" AND s.ClassId = {CmbClass.SelectedValue} ";
                }

                // فلترة حسب اسم الطالب
                if (!string.IsNullOrWhiteSpace(TxtStudentName.Text))
                {
                    query += $" AND s.FullName LIKE '%{TxtStudentName.Text.Trim()}%' ";
                }

                query += " ORDER BY c.ClassName, s.FullName";

                DataTable dt = f.GetData(query);
                GVReport.DataSource = dt;

                if (dt != null && dt.Rows.Count > 0)
                {
                    GrdReport.BestFitColumns();

                    // ترتيب الأعمدة لليمين (RTL) للشبكة
                    if (Func.vRtL && GrdReport.Columns.Count > 0)
                    {
                        int maxCol = GrdReport.Columns.Count;
                        for (int i = 0; i < maxCol; i++)
                            GrdReport.Columns[i].VisibleIndex = maxCol - i - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء عرض التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (GVReport.DataSource == null || ((DataTable)GVReport.DataSource).Rows.Count == 0)
            {
                MessageBox.Show("لا توجد بيانات لتصديرها.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.Title = "حفظ التقرير كملف Excel";
                    saveFileDialog.FileName = "تقرير_الطلاب_والفصول.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        GrdReport.ExportToXlsx(saveFileDialog.FileName);
                        MessageBox.Show("تم التصدير بنجاح!", "تصدير", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        System.Diagnostics.Process.Start(saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء التصدير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (GVReport.DataSource == null || ((DataTable)GVReport.DataSource).Rows.Count == 0)
            {
                MessageBox.Show("لا توجد بيانات لطباعتها.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string filterText = "الكل";
                if (CmbClass.SelectedValue != null &&
                    !string.Equals(CmbClass.SelectedValue.ToString(), "0", StringComparison.Ordinal))
                {
                    filterText = CmbClass.Text;
                }

                string headerText =
                    $"تقرير الطلاب والفصول\r\nالفصل الدراسي: {filterText}";

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
                        // ترويسة LTR ثلاثية: [يسار الصفحة، الوسط، يمين الصفحة] — نضع العربية على اليمين لقراءة من اليمين لليسار.
                        phf.Header.Content.AddRange(new string[] { "", "", headerText });
                        phf.Header.Font = new Font("Tahoma", 12f, FontStyle.Bold);
                        phf.Header.LineAlignment = BrickAlignment.Center;
                    }

                    link.CreateDocument();
                    link.ShowRibbonPreviewDialog(GVReport.LookAndFeel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء الطباعة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
