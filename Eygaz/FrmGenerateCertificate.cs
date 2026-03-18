using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Text.Json;

namespace Eygaz
{
    public partial class FrmGenerateCertificate : MetroFramework.Forms.MetroForm
    {
        Func f = new Func();
        AttendanceHelper helper = new AttendanceHelper();
        private List<CertificateElement> templateElements = new List<CertificateElement>();
        private Dictionary<string, string> fieldValues = new Dictionary<string, string>();
        private bool dataLoaded = false;

        // بيانات الشهادة الحالية
        private double currentPct = 0;
        private int currentTotal = 0;
        private int currentAbsent = 0;
        private int currentPresent = 0;
        private string currentTeacher = "";

        public FrmGenerateCertificate()
        {
            InitializeComponent();
        }

        // =============================================
        // تحميل الشاشة
        // =============================================
        private void FrmGenerateCertificate_Load(object sender, EventArgs e)
        {
            try
            {
                f.DataCombo(CmbClass, "Classes", "ClassName", "Id", " WHERE IsActive = 1 ORDER BY ClassName");
                LoadTemplates();

                BtnPreview.Enabled = false;
                BtnPrint.Enabled = false;
                BtnSaveRecord.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // تحميل القوالب في القائمة المنسدلة
        // =============================================
        private void LoadTemplates()
        {
            try
            {
                DataTable dt = helper.GetCertificateTemplates();
                if (dt != null && dt.Rows.Count > 0)
                {
                    CmbTemplate.DataSource = dt;
                    CmbTemplate.DisplayMember = "TemplateName";
                    CmbTemplate.ValueMember = "Id";
                }
            }
            catch { }
        }

        // =============================================
        // عند اختيار الفصل → تحميل الطلاب
        // =============================================
        private void CmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CmbClass.SelectedValue != null && CmbClass.SelectedValue.ToString() != "")
                {
                    int classId = Convert.ToInt32(CmbClass.SelectedValue);
                    f.DataCombo(CmbStudent, "Students", "FullName", "Id",
                        $" WHERE ClassId = {classId} AND IsActive = 1 ORDER BY FullName");
                }
            }
            catch { }
        }

        // =============================================
        // توليد الشهادة
        // =============================================
        private void BtnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbClass.SelectedValue == null)
                {
                    MessageBox.Show("يرجى اختيار الفصل", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (CmbStudent.SelectedValue == null)
                {
                    MessageBox.Show("يرجى اختيار الطالب", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (CmbTemplate.SelectedValue == null)
                {
                    MessageBox.Show("يرجى اختيار القالب", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int studentId = Convert.ToInt32(CmbStudent.SelectedValue);
                int classId = Convert.ToInt32(CmbClass.SelectedValue);
                int templateId = Convert.ToInt32(CmbTemplate.SelectedValue);

                // تحميل بيانات الطالب
                DataTable stats = helper.GetStudentCertificateStats(studentId, classId);
                if (stats == null || stats.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات حضور لهذا الطالب", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dr = stats.Rows[0];
                txtStudentName.Text = dr["StudentName"].ToString();
                txtClassName.Text = dr["ClassName"].ToString();

                currentTotal = Convert.ToInt32(dr["TotalSessions"]);
                currentPresent = Convert.ToInt32(dr["PresentCount"]);
                currentAbsent = Convert.ToInt32(dr["AbsentCount"]);
                currentPct = Convert.ToDouble(dr["AttendancePercentage"]);
                txtAttendPct.Text = currentPct.ToString("F1") + "%";

                // تلوين النسبة
                if (currentPct >= 90)
                    txtAttendPct.ForeColor = Color.ForestGreen;
                else if (currentPct >= 75)
                    txtAttendPct.ForeColor = Color.DarkOrange;
                else
                    txtAttendPct.ForeColor = Color.Red;

                currentTeacher = helper.GetMainTeacher(studentId, classId) ?? "";
                txtTeacherName.Text = currentTeacher;

                // تحميل القالب
                DataTable tmpl = helper.GetCertificateTemplateById(templateId);
                if (tmpl == null || tmpl.Rows.Count == 0)
                {
                    MessageBox.Show("لم يتم العثور على القالب", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string json = tmpl.Rows[0]["DesignJson"].ToString();
                // Deserialize template JSON using System.Text.Json
                templateElements = JsonSerializer.Deserialize<List<CertificateElement>>(json) ?? new List<CertificateElement>();

                // تعبئة قيم الحقول الديناميكية
                fieldValues.Clear();
                fieldValues["StudentName"] = txtStudentName.Text;
                fieldValues["ClassName"] = txtClassName.Text;
                fieldValues["TeacherName"] = currentTeacher;
                fieldValues["AttendancePercentage"] = currentPct.ToString("F1") + "%";
                fieldValues["IssueDate"] = DateTime.Today.ToString("yyyy/MM/dd");

                dataLoaded = true;
                BtnPreview.Enabled = true;
                BtnPrint.Enabled = true;
                BtnSaveRecord.Enabled = true;

                // رسم المعاينة
                pnlPreview.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // رسم المعاينة
        // =============================================
        private void pnlPreview_Paint(object sender, PaintEventArgs e)
        {
            if (!dataLoaded || templateElements.Count == 0) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            DrawElements(g, templateElements, fieldValues);
        }

        // =============================================
        // رسم العناصر مع استبدال الحقول الديناميكية
        // =============================================
        private void DrawElements(Graphics g, List<CertificateElement> elems, Dictionary<string, string> values)
        {
            foreach (var elem in elems)
            {
                Rectangle rect = new Rectangle(elem.X, elem.Y, elem.Width, elem.Height);

                if (elem.ElementType == "Shape")
                {
                    Color shapeColor = Color.Gold;
                    try { shapeColor = ColorTranslator.FromHtml(elem.ShapeColor ?? "#DAA520"); } catch { }

                    using (SolidBrush brush = new SolidBrush(shapeColor))
                    {
                        if (elem.ShapeType == "Ellipse")
                            g.FillEllipse(brush, rect);
                        else
                            g.FillRectangle(brush, rect);
                    }
                }
                else if (elem.ElementType == "Image")
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(elem.ImagePath) && System.IO.File.Exists(elem.ImagePath))
                        {
                            using (Image img = Image.FromFile(elem.ImagePath))
                            {
                                g.DrawImage(img, rect);
                            }
                        }
                    }
                    catch { }
                }
                else // Text or DynamicField
                {
                    string displayText = elem.Text;

                    // استبدال الحقول الديناميكية بالقيم الفعلية
                    if (elem.ElementType == "DynamicField" && !string.IsNullOrEmpty(elem.FieldName))
                    {
                        if (values.ContainsKey(elem.FieldName))
                            displayText = values[elem.FieldName];
                        else
                            displayText = elem.FieldName;
                    }

                    Color fontColor = Color.Black;
                    try { fontColor = ColorTranslator.FromHtml(elem.FontColor ?? "#000000"); } catch { }

                    FontStyle style = elem.FontBold ? FontStyle.Bold : FontStyle.Regular;
                    using (Font font = new Font(elem.FontFamily ?? "Arial", elem.FontSize > 0 ? elem.FontSize : 14, style))
                    using (SolidBrush brush = new SolidBrush(fontColor))
                    {
                        StringFormat sf = new StringFormat();
                        switch (elem.Alignment)
                        {
                            case "Left": sf.Alignment = StringAlignment.Near; break;
                            case "Right": sf.Alignment = StringAlignment.Far; break;
                            default: sf.Alignment = StringAlignment.Center; break;
                        }
                        sf.LineAlignment = StringAlignment.Center;

                        g.DrawString(displayText, font, brush, rect, sf);
                    }
                }
            }
        }

        // =============================================
        // معاينة الشهادة (PrintPreview)
        // =============================================
        private void BtnPreview_Click(object sender, EventArgs e)
        {
            if (!dataLoaded) return;

            try
            {
                PrintDocument doc = CreatePrintDocument();
                PrintPreviewDialog preview = new PrintPreviewDialog();
                preview.Document = doc;
                preview.Width = 900;
                preview.Height = 700;
                preview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء المعاينة: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // طباعة الشهادة
        // =============================================
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (!dataLoaded) return;

            try
            {
                PrintDocument doc = CreatePrintDocument();
                PrintDialog dlg = new PrintDialog();
                dlg.Document = doc;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    doc.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء الطباعة: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // إنشاء مستند الطباعة
        // =============================================
        private PrintDocument CreatePrintDocument()
        {
            PrintDocument doc = new PrintDocument();
            doc.DefaultPageSettings.Landscape = true;
            doc.PrintPage += (s, ev) =>
            {
                DrawElements(ev.Graphics, templateElements, fieldValues);
            };
            return doc;
        }

        // =============================================
        // حفظ سجل الشهادة
        // =============================================
        private void BtnSaveRecord_Click(object sender, EventArgs e)
        {
            if (!dataLoaded) return;

            try
            {
                int studentId = Convert.ToInt32(CmbStudent.SelectedValue);
                int classId = Convert.ToInt32(CmbClass.SelectedValue);
                string issueDate = DateTime.Today.ToString("yyyy-MM-dd");

                bool saved = helper.SaveCertificate(studentId, classId, "شهادة حسب القالب",
                    issueDate, currentPct, currentTotal, currentAbsent, currentPresent,
                    currentTeacher, "تم الإصدار بقالب: " + CmbTemplate.Text);

                if (saved)
                    MessageBox.Show("تم حفظ سجل الشهادة بنجاح", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("حدث خطأ أثناء الحفظ", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
