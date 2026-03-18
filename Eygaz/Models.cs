using System;

namespace Eygaz
{
    // =============================================
    // نماذج C# لجميع جداول قاعدة البيانات
    // =============================================

    /// <summary>
    /// نموذج الطالب - جدول Students
    /// </summary>
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public string CreatedAt { get; set; }
        public int ClassId { get; set; }
    }

    /// <summary>
    /// نموذج المدرس - جدول Teachers
    /// </summary>
    public class Teacher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public string CreatedAt { get; set; }
    }

    /// <summary>
    /// نموذج المادة الدراسية - جدول Subjects
    /// </summary>
    public class Subject
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string CreatedAt { get; set; }
    }

    /// <summary>
    /// نموذج الفصل - جدول Classes
    /// </summary>
    public class ClassRoom
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string ClassLocation { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public string CreatedAt { get; set; }
    }

    /// <summary>
    /// نموذج حالة الحضور - جدول AttendanceStatus
    /// </summary>
    public class AttendanceStatusModel
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
        public string StatusCode { get; set; }
    }

    /// <summary>
    /// نموذج جلسة الحضور - جدول AttendanceSessions
    /// </summary>
    public class AttendanceSession
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public string SessionDate { get; set; }
        public string Notes { get; set; }
        public string CreatedAt { get; set; }
    }

    /// <summary>
    /// نموذج سجل حضور الطالب - جدول StudentAttendance
    /// </summary>
    public class StudentAttendanceRecord
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int StudentId { get; set; }
        public int StatusId { get; set; }
        public string Notes { get; set; }

        // حقول إضافية للعرض
        public string StudentName { get; set; }
        public string StatusName { get; set; }
    }

    /// <summary>
    /// نموذج قالب الشهادة - جدول CertificateTemplates
    /// </summary>
    public class CertificateTemplate
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public string DesignJson { get; set; }
        public string PageOrientation { get; set; }
        public bool IsDefault { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }

    /// <summary>
    /// نموذج عنصر داخل قالب الشهادة (يُسلسل كـ JSON)
    /// </summary>
    public class CertificateElement
    {
        public string ElementType { get; set; }  // Text, DynamicField, Image, Shape
        public string Text { get; set; }
        public string FieldName { get; set; }     // [StudentName], [ClassName], etc.
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float FontSize { get; set; }
        public string FontFamily { get; set; }
        public bool FontBold { get; set; }
        public string FontColor { get; set; }     // Hex color
        public string Alignment { get; set; }     // Left, Center, Right
        public string ImagePath { get; set; }
        public string ShapeType { get; set; }     // Rectangle, Line, Ellipse
        public string ShapeColor { get; set; }

        public CertificateElement()
        {
            ElementType = "Text";
            Text = "";
            FieldName = "";
            X = 0;
            Y = 0;
            Width = 200;
            Height = 30;
            FontSize = 14;
            FontFamily = "Arial";
            FontBold = false;
            FontColor = "#000000";
            Alignment = "Center";
            ImagePath = "";
            ShapeType = "";
            ShapeColor = "#000000";
        }
    }

    /// <summary>
    /// نموذج حضور المدرس - جدول TeacherAttendance
    /// </summary>
    public class TeacherAttendanceRecord
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string AttendanceDate { get; set; }
        public int StatusId { get; set; }
        public string Notes { get; set; }

        // حقول إضافية للعرض
        public string TeacherName { get; set; }
        public string StatusName { get; set; }
    }
}
