using System;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace Eygaz
{
    /// <summary>
    /// نظام إرسال رسائل واتساب
    /// يدعم: رابط مباشر (wa.me) + WhatsApp Business API
    /// </summary>
    public class WhatsAppHelper
    {
        // =============================================
        // إعدادات WhatsApp Business API
        // =============================================
        public static string ApiToken = "";        // ضع API Token هنا
        public static string PhoneNumberId = "";   // معرّف رقم الهاتف من Meta

        // =============================================
        // 1. إرسال رسالة عبر رابط wa.me (الطريقة الأسهل)
        // =============================================
        /// <summary>
        /// فتح واتساب عبر الرابط في المتصفح الافتراضي
        /// </summary>
        public static bool SendWhatsAppMessage(string phone, string message)
        {
            try
            {
                string formattedPhone = FormatPhoneNumber(phone);
                string encodedMessage = Uri.EscapeDataString(message);
                string url = $"https://wa.me/{formattedPhone}?text={encodedMessage}";

                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // =============================================
        // 2. إرسال رسالة عبر WhatsApp Business API
        // =============================================
        /// <summary>
        /// إرسال رسالة عبر WhatsApp Business API (يحتاج إعداد حساب Meta Business)
        /// </summary>
        public static bool SendWhatsAppAPI(string phone, string message, string apiToken = "")
        {
            try
            {
                if (string.IsNullOrEmpty(apiToken))
                    apiToken = ApiToken;

                if (string.IsNullOrEmpty(apiToken) || string.IsNullOrEmpty(PhoneNumberId))
                    return false;

                string formattedPhone = FormatPhoneNumber(phone);
                string url = $"https://graph.facebook.com/v17.0/{PhoneNumberId}/messages";

                string jsonBody = "{" +
                    "\"messaging_product\": \"whatsapp\"," +
                    "\"to\": \"" + formattedPhone + "\"," +
                    "\"type\": \"text\"," +
                    "\"text\": {\"body\": \"" + EscapeJson(message) + "\"}" +
                    "}";

                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + apiToken;
                    client.Encoding = Encoding.UTF8;
                    client.UploadString(url, "POST", jsonBody);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // =============================================
        // 3. إشعار غياب طالب
        // =============================================
        public static bool SendAbsenceNotification(string studentName, string phone, string date)
        {
            string message = $"السلام عليكم ورحمة الله وبركاته\n\n" +
                $"نود إبلاغكم بأن الطالب/ة: {studentName}\n" +
                $"لم يحضر جلسة اليوم: {date}\n\n" +
                $"نرجو المتابعة والتأكد من الحضور.\n\n" +
                $"إدارة مركز إيجاز لتحفيظ القرآن الكريم";

            return SendWhatsAppMessage(phone, message);
        }

        // =============================================
        // 4. إشعار غياب مدرس
        // =============================================
        public static bool SendTeacherAbsenceNotification(string teacherName, string phone, string date)
        {
            string message = $"السلام عليكم ورحمة الله وبركاته\n\n" +
                $"نود إبلاغكم بتسجيل غياب المدرس/ة: {teacherName}\n" +
                $"بتاريخ: {date}\n\n" +
                $"نرجو التواصل مع الإدارة في حال وجود عذر.\n\n" +
                $"إدارة مركز إيجاز لتحفيظ القرآن الكريم";

            return SendWhatsAppMessage(phone, message);
        }

        // =============================================
        // 5. إشعار تأخير مدرس
        // =============================================
        public static bool SendTeacherLateNotification(string teacherName, string phone, string date)
        {
            string message = $"السلام عليكم ورحمة الله وبركاته\n\n" +
                $"نود إبلاغكم بتسجيل تأخر المدرس/ة: {teacherName}\n" +
                $"بتاريخ: {date}\n\n" +
                $"إدارة مركز إيجاز لتحفيظ القرآن الكريم";

            return SendWhatsAppMessage(phone, message);
        }

        // =============================================
        // 6. إشعار حضور (اختياري)
        // =============================================
        public static bool SendAttendanceConfirmation(string studentName, string phone, string date)
        {
            string message = $"السلام عليكم ورحمة الله وبركاته\n\n" +
                $"نشكركم على حضور الطالب/ة: {studentName}\n" +
                $"بتاريخ: {date}\n\n" +
                $"بارك الله فيكم.\n\n" +
                $"إدارة مركز إيجاز لتحفيظ القرآن الكريم";

            return SendWhatsAppMessage(phone, message);
        }

        // =============================================
        // 7. إرسال تقرير
        // =============================================
        public static bool SendReport(string phone, string reportTitle, string reportContent)
        {
            string message = $"السلام عليكم ورحمة الله وبركاته\n\n" +
                $"📋 {reportTitle}\n\n" +
                $"{reportContent}\n\n" +
                $"إدارة مركز إيجاز لتحفيظ القرآن الكريم";

            return SendWhatsAppMessage(phone, message);
        }

        // =============================================
        // تنسيق رقم الهاتف
        // =============================================
        /// <summary>
        /// تنسيق الرقم: إزالة المسافات، إضافة رمز الدولة (967) إذا لم يكن موجوداً
        /// </summary>
        public static string FormatPhoneNumber(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return "";

            // إزالة المسافات والأحرف غير الرقمية
            string cleaned = "";
            foreach (char c in phone)
            {
                if (char.IsDigit(c) || c == '+')
                    cleaned += c;
            }

            // إزالة + من البداية
            cleaned = cleaned.TrimStart('+');

            // إذا يبدأ بـ 0 → استبدل بـ 967
            if (cleaned.StartsWith("0"))
                cleaned = "967" + cleaned.Substring(1);

            // إذا لا يبدأ بـ 967 → أضفها
            if (!cleaned.StartsWith("967"))
                cleaned = "967" + cleaned;

            return cleaned;
        }

        // =============================================
        // تنظيف نص JSON
        // =============================================
        private static string EscapeJson(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return text
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r")
                .Replace("\t", "\\t");
        }
    }
}
