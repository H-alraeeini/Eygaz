using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Text.Json;
namespace Eygaz
{
    public partial class FrmCertificateDesigner : MetroFramework.Forms.MetroForm
    {
        AttendanceHelper helper = new AttendanceHelper();
        private List<CertificateElement> elements = new List<CertificateElement>();
        private int selectedIndex = -1;
        private bool isDragging = false;
        private Point dragOffset;
        private int currentTemplateId = -1;
        private bool updatingProperties = false;

        public FrmCertificateDesigner()
        {
            InitializeComponent();
        }

        // =============================================
        // تحميل الشاشة
        // =============================================
        private void FrmCertificateDesigner_Load(object sender, EventArgs e)
        {
            cmbAlignment.SelectedIndex = 1; // Center
            LoadTemplateList();
        }

        // =============================================================================
        //  إضافة العناصر
        // =============================================================================

        private void BtnAddText_Click(object sender, EventArgs e)
        {
            AddElement("Text", "نص جديد", "");
        }

        private void BtnAddImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var elem = new CertificateElement();
                elem.ElementType = "Image";
                elem.ImagePath = ofd.FileName;
                elem.X = 50;
                elem.Y = 50;
                elem.Width = 150;
                elem.Height = 100;
                elements.Add(elem);
                SelectElement(elements.Count - 1);
                pnlCanvas.Invalidate();
            }
        }

        private void BtnAddShape_Click(object sender, EventArgs e)
        {
            var elem = new CertificateElement();
            elem.ElementType = "Shape";
            elem.ShapeType = "Rectangle";
            elem.ShapeColor = "#DAA520"; // Gold
            elem.X = 20;
            elem.Y = 20;
            elem.Width = 600;
            elem.Height = 3;
            elements.Add(elem);
            SelectElement(elements.Count - 1);
            pnlCanvas.Invalidate();
        }

        // حقول ديناميكية
        private void BtnAddStudentName_Click(object sender, EventArgs e)
        {
            AddElement("DynamicField", "[StudentName]", "StudentName");
        }

        private void BtnAddClassName_Click(object sender, EventArgs e)
        {
            AddElement("DynamicField", "[ClassName]", "ClassName");
        }

        private void BtnAddTeacherName_Click(object sender, EventArgs e)
        {
            AddElement("DynamicField", "[TeacherName]", "TeacherName");
        }

        private void BtnAddAttendPct_Click(object sender, EventArgs e)
        {
            AddElement("DynamicField", "[AttendancePercentage]", "AttendancePercentage");
        }

        private void BtnAddIssueDate_Click(object sender, EventArgs e)
        {
            AddElement("DynamicField", "[IssueDate]", "IssueDate");
        }

        private void AddElement(string type, string text, string fieldName)
        {
            var elem = new CertificateElement();
            elem.ElementType = type;
            elem.Text = text;
            elem.FieldName = fieldName;
            elem.X = 50 + (elements.Count * 10) % 200;
            elem.Y = 50 + (elements.Count * 20) % 200;
            elem.Width = 250;
            elem.Height = 35;
            elem.FontSize = type == "DynamicField" ? 18 : 14;
            elem.FontBold = type == "DynamicField";

            if (type == "DynamicField")
                elem.FontColor = "#1565C0"; // أزرق داكن

            elements.Add(elem);
            SelectElement(elements.Count - 1);
            pnlCanvas.Invalidate();
        }

        // =============================================================================
        //  رسم العناصر على الـ Canvas
        // =============================================================================

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // رسم حدود الصفحة
            using (Pen borderPen = new Pen(Color.LightGray, 1))
            {
                g.DrawRectangle(borderPen, 0, 0, pnlCanvas.Width - 1, pnlCanvas.Height - 1);
            }

            for (int i = 0; i < elements.Count; i++)
            {
                var elem = elements[i];
                Rectangle rect = new Rectangle(elem.X, elem.Y, elem.Width, elem.Height);

                if (elem.ElementType == "Shape")
                {
                    Color shapeColor = ColorTranslator.FromHtml(elem.ShapeColor ?? "#DAA520");
                    using (SolidBrush brush = new SolidBrush(shapeColor))
                    {
                        if (elem.ShapeType == "Ellipse")
                            g.FillEllipse(brush, rect);
                        else if (elem.ShapeType == "Line")
                            g.FillRectangle(brush, rect);
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
                        else
                        {
                            using (SolidBrush brush = new SolidBrush(Color.LightGray))
                            {
                                g.FillRectangle(brush, rect);
                            }
                            using (Font f = new Font("Tahoma", 10))
                            {
                                g.DrawString("🖼 صورة", f, Brushes.Gray, rect,
                                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                            }
                        }
                    }
                    catch
                    {
                        using (SolidBrush brush = new SolidBrush(Color.LightGray))
                        {
                            g.FillRectangle(brush, rect);
                        }
                    }
                }
                else // Text or DynamicField
                {
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

                        string displayText = elem.Text;
                        if (elem.ElementType == "DynamicField")
                        {
                            // عرض بصيغة مميزة
                            displayText = "{{ " + elem.FieldName + " }}";
                        }

                        g.DrawString(displayText, font, brush, rect, sf);
                    }
                }

                // إطار العنصر المحدد
                if (i == selectedIndex)
                {
                    using (Pen selPen = new Pen(Color.FromArgb(0, 120, 215), 2))
                    {
                        selPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        g.DrawRectangle(selPen, rect);
                    }

                    // مقابض التغيير
                    int hs = 6; // handle size
                    Rectangle[] handles = {
                        new Rectangle(rect.Left - hs/2, rect.Top - hs/2, hs, hs),
                        new Rectangle(rect.Right - hs/2, rect.Top - hs/2, hs, hs),
                        new Rectangle(rect.Left - hs/2, rect.Bottom - hs/2, hs, hs),
                        new Rectangle(rect.Right - hs/2, rect.Bottom - hs/2, hs, hs)
                    };
                    foreach (var h in handles)
                        g.FillRectangle(Brushes.White, h);
                    foreach (var h in handles)
                        g.DrawRectangle(Pens.DodgerBlue, h);
                }
            }
        }

        // =============================================================================
        //  Drag & Drop (السحب والإفلات)
        // =============================================================================

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            pnlCanvas.MouseDown += Canvas_MouseDown;
            pnlCanvas.MouseMove += Canvas_MouseMove;
            pnlCanvas.MouseUp += Canvas_MouseUp;
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            // البحث من الأخير للأول (العنصر الأعلى أولاً)
            for (int i = elements.Count - 1; i >= 0; i--)
            {
                var elem = elements[i];
                Rectangle rect = new Rectangle(elem.X, elem.Y, elem.Width, elem.Height);
                if (rect.Contains(e.Location))
                {
                    SelectElement(i);
                    isDragging = true;
                    dragOffset = new Point(e.X - elem.X, e.Y - elem.Y);
                    return;
                }
            }

            // لم يتم النقر على أي عنصر → إلغاء التحديد
            selectedIndex = -1;
            pnlCanvas.Invalidate();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging || selectedIndex < 0) return;

            var elem = elements[selectedIndex];
            elem.X = Math.Max(0, e.X - dragOffset.X);
            elem.Y = Math.Max(0, e.Y - dragOffset.Y);

            // تحديث لوحة الخصائص
            updatingProperties = true;
            numPosX.Value = elem.X;
            numPosY.Value = elem.Y;
            updatingProperties = false;

            pnlCanvas.Invalidate();
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        // =============================================================================
        //  تحديد عنصر وعرض خصائصه
        // =============================================================================

        private void SelectElement(int index)
        {
            selectedIndex = index;
            if (index < 0 || index >= elements.Count) return;

            updatingProperties = true;
            var elem = elements[index];

            txtElementText.Text = elem.Text;
            numPosX.Value = Math.Min(elem.X, (int)numPosX.Maximum);
            numPosY.Value = Math.Min(elem.Y, (int)numPosY.Maximum);
            numWidth.Value = Math.Max((int)numWidth.Minimum, Math.Min(elem.Width, (int)numWidth.Maximum));
            numHeight.Value = Math.Max((int)numHeight.Minimum, Math.Min(elem.Height, (int)numHeight.Maximum));
            numFontSize.Value = Math.Max((int)numFontSize.Minimum, Math.Min((int)elem.FontSize, (int)numFontSize.Maximum));
            chkBold.Checked = elem.FontBold;

            try { pnlColorPreview.BackColor = ColorTranslator.FromHtml(elem.FontColor ?? "#000000"); }
            catch { pnlColorPreview.BackColor = Color.Black; }

            switch (elem.Alignment)
            {
                case "Left": cmbAlignment.SelectedIndex = 0; break;
                case "Center": cmbAlignment.SelectedIndex = 1; break;
                case "Right": cmbAlignment.SelectedIndex = 2; break;
            }

            updatingProperties = false;

            // التبديل إلى تبويب الخصائص
            tabControl1.SelectedTab = tabProperties;
            pnlCanvas.Invalidate();
        }

        // =============================================================================
        //  تحديث خصائص العنصر المحدد
        // =============================================================================

        private void txtElementText_TextChanged(object sender, EventArgs e)
        {
            if (updatingProperties || selectedIndex < 0) return;
            elements[selectedIndex].Text = txtElementText.Text;
            pnlCanvas.Invalidate();
        }

        private void numPosX_ValueChanged(object sender, EventArgs e)
        {
            if (updatingProperties || selectedIndex < 0) return;
            elements[selectedIndex].X = (int)numPosX.Value;
            pnlCanvas.Invalidate();
        }

        private void numPosY_ValueChanged(object sender, EventArgs e)
        {
            if (updatingProperties || selectedIndex < 0) return;
            elements[selectedIndex].Y = (int)numPosY.Value;
            pnlCanvas.Invalidate();
        }

        private void numWidth_ValueChanged(object sender, EventArgs e)
        {
            if (updatingProperties || selectedIndex < 0) return;
            elements[selectedIndex].Width = (int)numWidth.Value;
            pnlCanvas.Invalidate();
        }

        private void numHeight_ValueChanged(object sender, EventArgs e)
        {
            if (updatingProperties || selectedIndex < 0) return;
            elements[selectedIndex].Height = (int)numHeight.Value;
            pnlCanvas.Invalidate();
        }

        private void numFontSize_ValueChanged(object sender, EventArgs e)
        {
            if (updatingProperties || selectedIndex < 0) return;
            elements[selectedIndex].FontSize = (float)numFontSize.Value;
            pnlCanvas.Invalidate();
        }

        private void chkBold_CheckedChanged(object sender, EventArgs e)
        {
            if (updatingProperties || selectedIndex < 0) return;
            elements[selectedIndex].FontBold = chkBold.Checked;
            pnlCanvas.Invalidate();
        }

        private void BtnPickColor_Click(object sender, EventArgs e)
        {
            if (selectedIndex < 0) return;
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                elements[selectedIndex].FontColor = ColorTranslator.ToHtml(cd.Color);
                pnlColorPreview.BackColor = cd.Color;
                pnlCanvas.Invalidate();
            }
        }

        private void cmbAlignment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (updatingProperties || selectedIndex < 0) return;
            string[] alignments = { "Left", "Center", "Right" };
            if (cmbAlignment.SelectedIndex >= 0 && cmbAlignment.SelectedIndex < alignments.Length)
            {
                elements[selectedIndex].Alignment = alignments[cmbAlignment.SelectedIndex];
                pnlCanvas.Invalidate();
            }
        }

        private void BtnDeleteElement_Click(object sender, EventArgs e)
        {
            if (selectedIndex < 0 || selectedIndex >= elements.Count) return;

            if (MessageBox.Show("هل تريد حذف هذا العنصر؟", "تأكيد",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                elements.RemoveAt(selectedIndex);
                selectedIndex = -1;
                pnlCanvas.Invalidate();
            }
        }

        // Replace the usage of JavaScriptSerializer with System.Text.Json.JsonSerializer

        private void BtnSaveTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtTemplateName.Text.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("يرجى إدخال اسم القالب", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTemplateName.Focus();
                    return;
                }

                // Convert elements to JSON
                string json = JsonSerializer.Serialize(elements);

                bool saved;
                if (currentTemplateId > 0)
                {
                    saved = helper.UpdateCertificateTemplate(currentTemplateId, name, json);
                }
                else
                {
                    int newId = helper.SaveCertificateTemplate(name, json);
                    saved = newId > 0;
                    if (saved) currentTemplateId = newId;
                }

                if (saved)
                {
                    MessageBox.Show("تم حفظ القالب بنجاح", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTemplateList();
                }
                else
                {
                    MessageBox.Show("حدث خطأ أثناء الحفظ", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLoadTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTemplates.SelectedItem == null)
                {
                    MessageBox.Show("يرجى اختيار قالب من القائمة", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string selectedText = lstTemplates.SelectedItem.ToString();
                int templateId = GetTemplateIdFromListItem(selectedText);
                if (templateId <= 0) return;

                DataTable dt = helper.GetCertificateTemplateById(templateId);
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("لم يتم العثور على القالب", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var row = dt.Rows[0];
                currentTemplateId = templateId;
                txtTemplateName.Text = row["TemplateName"].ToString();

                string json = row["DesignJson"].ToString();
                elements = JsonSerializer.Deserialize<List<CertificateElement>>(json) ?? new List<CertificateElement>();

                selectedIndex = -1;
                pnlCanvas.Invalidate();

                MessageBox.Show("تم تحميل القالب بنجاح", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء تحميل القالب: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeleteTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTemplates.SelectedItem == null)
                {
                    MessageBox.Show("يرجى اختيار قالب من القائمة", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("هل تريد حذف هذا القالب؟", "تأكيد",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                string selectedText = lstTemplates.SelectedItem.ToString();
                int templateId = GetTemplateIdFromListItem(selectedText);
                if (templateId <= 0) return;

                if (helper.DeleteCertificateTemplate(templateId))
                {
                    if (currentTemplateId == templateId)
                    {
                        currentTemplateId = -1;
                        txtTemplateName.Text = "";
                    }
                    MessageBox.Show("تم حذف القالب بنجاح", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTemplateList();
                }
                else
                {
                    MessageBox.Show("حدث خطأ أثناء الحذف", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNewTemplate_Click(object sender, EventArgs e)
        {
            currentTemplateId = -1;
            txtTemplateName.Text = "";
            elements.Clear();
            selectedIndex = -1;
            pnlCanvas.Invalidate();
        }

        // =============================================================================
        //  دوال مساعدة
        // =============================================================================

        private void LoadTemplateList()
        {
            try
            {
                lstTemplates.Items.Clear();
                DataTable dt = helper.GetCertificateTemplates();
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string id = row["Id"].ToString();
                        string name = row["TemplateName"].ToString();
                        lstTemplates.Items.Add($"[{id}] {name}");
                    }
                }
            }
            catch { }
        }

        private int GetTemplateIdFromListItem(string text)
        {
            try
            {
                int start = text.IndexOf('[') + 1;
                int end = text.IndexOf(']');
                if (start > 0 && end > start)
                {
                    string idStr = text.Substring(start, end - start);
                    return int.Parse(idStr);
                }
            }
            catch { }
            return -1;
        }

        /// <summary>
        /// الحصول على عناصر التصميم (تُستخدم من FrmGenerateCertificate)
        /// </summary>
        public List<CertificateElement> GetElements()
        {
            return elements;
        }

        // Replace the GetDesignJson method with:
        /// <summary>
        /// الحصول على JSON للتصميم الحالي
        /// </summary>
        public string GetDesignJson()
        {
            return JsonSerializer.Serialize(elements);
        }
    }
}
