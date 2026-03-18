using System;
using System.Drawing;
using System.Drawing.Printing;

namespace Eygaz
{
    /// <summary>
    /// طباعة الشهادة الطلابية
    /// </summary>
    public class CertificatePrintDocument : PrintDocument
    {
        // بيانات الشهادة
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
        public string CertificateType { get; set; }
        public string IssueDate { get; set; }
        public double AttendancePercentage { get; set; }
        public int TotalSessions { get; set; }
        public int AbsentCount { get; set; }
        public int PresentCount { get; set; }
        public string InstitutionName { get; set; } = "إدارة مركز إيجاز لتحفيظ القرآن الكريم";
        public string DirectorName { get; set; } = "مدير المركز";

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            Graphics g = e.Graphics;
            float pageWidth = e.PageBounds.Width;
            float pageHeight = e.PageBounds.Height;
            float margin = 60;
            float contentWidth = pageWidth - (margin * 2);
            float y = margin;

            // خطوط
            Font fontTitle = new Font("Tahoma", 26, FontStyle.Bold);
            Font fontSubTitle = new Font("Tahoma", 18, FontStyle.Bold);
            Font fontName = new Font("Tahoma", 28, FontStyle.Bold);
            Font fontBody = new Font("Tahoma", 14);
            Font fontBodyBold = new Font("Tahoma", 14, FontStyle.Bold);
            Font fontSmall = new Font("Tahoma", 11);
            Font fontSignature = new Font("Tahoma", 12, FontStyle.Bold);

            // ألوان
            Brush brushDark = new SolidBrush(Color.FromArgb(43, 79, 88));
            Brush brushGold = new SolidBrush(Color.FromArgb(178, 134, 47));
            Brush brushBlack = Brushes.Black;
            Brush brushGray = Brushes.DimGray;
            Pen penGold = new Pen(Color.FromArgb(178, 134, 47), 3);
            Pen penDark = new Pen(Color.FromArgb(43, 79, 88), 2);

            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;
            centerFormat.LineAlignment = StringAlignment.Center;

            // =============================================
            // إطار خارجي مزدوج
            // =============================================
            g.DrawRectangle(penGold, margin - 20, margin - 20,
                contentWidth + 40, pageHeight - margin * 2 + 40);
            g.DrawRectangle(penDark, margin - 10, margin - 10,
                contentWidth + 20, pageHeight - margin * 2 + 20);

            // =============================================
            // خط زخرفي علوي
            // =============================================
            y = margin + 10;
            g.DrawLine(penGold, margin + 30, y, pageWidth - margin - 30, y);
            y += 5;
            g.DrawLine(new Pen(Color.FromArgb(178, 134, 47), 1), margin + 50, y, pageWidth - margin - 50, y);

            // =============================================
            // اسم المؤسسة
            // =============================================
            y += 20;
            g.DrawString(InstitutionName, fontSubTitle, brushDark,
                new RectangleF(margin, y, contentWidth, 40), centerFormat);

            // =============================================
            // خط فاصل
            // =============================================
            y += 50;
            g.DrawLine(penGold, margin + 80, y, pageWidth - margin - 80, y);

            // =============================================
            // عنوان الشهادة
            // =============================================
            y += 20;
            g.DrawString(CertificateType, fontTitle, brushGold,
                new RectangleF(margin, y, contentWidth, 60), centerFormat);

            // =============================================
            // زخرفة تحت العنوان
            // =============================================
            y += 70;
            float starY = y;
            string stars = "✦  ✦  ✦";
            g.DrawString(stars, fontBody, brushGold,
                new RectangleF(margin, starY, contentWidth, 30), centerFormat);

            // =============================================
            // نص الشهادة
            // =============================================
            y += 45;
            g.DrawString("تشهد إدارة المؤسسة بأن الطالب", fontBody, brushBlack,
                new RectangleF(margin, y, contentWidth, 30), centerFormat);

            // =============================================
            // اسم الطالب (بارز)
            // =============================================
            y += 45;
            // خلفية مميزة لاسم الطالب
            SizeF nameSize = g.MeasureString(StudentName, fontName);
            float nameX = (pageWidth - nameSize.Width) / 2;
            g.FillRectangle(new SolidBrush(Color.FromArgb(248, 243, 228)),
                nameX - 15, y - 5, nameSize.Width + 30, nameSize.Height + 10);
            g.DrawRectangle(penGold, nameX - 15, y - 5, nameSize.Width + 30, nameSize.Height + 10);
            g.DrawString(StudentName, fontName, brushDark,
                new RectangleF(margin, y, contentWidth, 50), centerFormat);

            // =============================================
            // باقي النص
            // =============================================
            y += 65;
            g.DrawString("قد التزم بحضور جلسات البرنامج في فصل", fontBody, brushBlack,
                new RectangleF(margin, y, contentWidth, 30), centerFormat);

            y += 40;
            g.DrawString(ClassName, fontSubTitle, brushDark,
                new RectangleF(margin, y, contentWidth, 35), centerFormat);

            y += 50;
            g.DrawString("وحقق نسبة حضور قدرها", fontBody, brushBlack,
                new RectangleF(margin, y, contentWidth, 30), centerFormat);

            y += 40;
            // نسبة الحضور بلون مميز
            string pctText = $"% {AttendancePercentage}";
            Color pctColor = AttendancePercentage >= 90 ? Color.FromArgb(39, 174, 96) :
                             AttendancePercentage >= 75 ? Color.FromArgb(243, 156, 18) :
                             Color.FromArgb(192, 57, 43);
            g.DrawString(pctText, fontTitle, new SolidBrush(pctColor),
                new RectangleF(margin, y, contentWidth, 50), centerFormat);

            // =============================================
            // إحصائيات الحضور
            // =============================================
            y += 60;
            string statsLine = $"إجمالي الجلسات: {TotalSessions}  |  الحضور: {PresentCount}  |  الغياب: {AbsentCount}";
            g.DrawString(statsLine, fontSmall, brushGray,
                new RectangleF(margin, y, contentWidth, 25), centerFormat);

            // =============================================
            // خاتمة
            // =============================================
            y += 40;
            g.DrawString("ونتمنى له دوام التوفيق والنجاح", fontBodyBold, brushDark,
                new RectangleF(margin, y, contentWidth, 30), centerFormat);

            // =============================================
            // زخرفة
            // =============================================
            y += 45;
            g.DrawString("✦  ✦  ✦", fontBody, brushGold,
                new RectangleF(margin, y, contentWidth, 30), centerFormat);

            // =============================================
            // التاريخ
            // =============================================
            y += 45;
            g.DrawString($"تاريخ الإصدار: {IssueDate}", fontSmall, brushBlack,
                new RectangleF(margin, y, contentWidth, 25), centerFormat);

            // =============================================
            // التوقيعات
            // =============================================
            y += 55;
            float signWidth = contentWidth / 2;

            // توقيع المدرس (يمين)
            g.DrawLine(penDark, margin + 40, y + 25, margin + signWidth - 40, y + 25);
            g.DrawString("توقيع المدرس", fontSignature, brushDark,
                new RectangleF(margin, y + 30, signWidth, 25), centerFormat);
            g.DrawString(TeacherName, fontSmall, brushGray,
                new RectangleF(margin, y + 55, signWidth, 20), centerFormat);

            // توقيع الإدارة (يسار)
            g.DrawLine(penDark, margin + signWidth + 40, y + 25, pageWidth - margin - 40, y + 25);
            g.DrawString("توقيع الإدارة", fontSignature, brushDark,
                new RectangleF(margin + signWidth, y + 30, signWidth, 25), centerFormat);
            g.DrawString(DirectorName, fontSmall, brushGray,
                new RectangleF(margin + signWidth, y + 55, signWidth, 20), centerFormat);

            // =============================================
            // خط زخرفي سفلي
            // =============================================
            float bottomY = pageHeight - margin - 25;
            g.DrawLine(new Pen(Color.FromArgb(178, 134, 47), 1), margin + 50, bottomY, pageWidth - margin - 50, bottomY);
            bottomY += 5;
            g.DrawLine(penGold, margin + 30, bottomY, pageWidth - margin - 30, bottomY);

            // تنظيف
            fontTitle.Dispose();
            fontSubTitle.Dispose();
            fontName.Dispose();
            fontBody.Dispose();
            fontBodyBold.Dispose();
            fontSmall.Dispose();
            fontSignature.Dispose();
            brushDark.Dispose();
            brushGold.Dispose();
            penGold.Dispose();
            penDark.Dispose();
            centerFormat.Dispose();

            e.HasMorePages = false;
        }
    }
}
