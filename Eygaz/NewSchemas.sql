-- =============================================
-- نظام الشهادات الديناميكي + حضور المدرسين
-- قاعدة بيانات: SQLite (EygazDb.db)
-- =============================================

PRAGMA foreign_keys = ON;

-- =============================================
-- 1. جدول قوالب الشهادات CertificateTemplates
-- =============================================
CREATE TABLE IF NOT EXISTS CertificateTemplates (
    Id               INTEGER PRIMARY KEY AUTOINCREMENT,
    TemplateName     TEXT    NOT NULL,
    DesignJson       TEXT    NOT NULL DEFAULT '[]',
    PageOrientation  TEXT    NOT NULL DEFAULT 'Landscape',
    IsDefault        INTEGER NOT NULL DEFAULT 0,
    CreatedAt        TEXT    DEFAULT (datetime('now','localtime')),
    UpdatedAt        TEXT    DEFAULT (datetime('now','localtime'))
);

-- فهرس على اسم القالب
CREATE INDEX IF NOT EXISTS IX_CertificateTemplates_Name ON CertificateTemplates(TemplateName);

-- =============================================
-- 2. جدول حضور وغياب المدرسين TeacherAttendance
-- =============================================
CREATE TABLE IF NOT EXISTS TeacherAttendance (
    Id              INTEGER PRIMARY KEY AUTOINCREMENT,
    TeacherId       INTEGER NOT NULL,
    AttendanceDate  TEXT    NOT NULL,
    StatusId        INTEGER NOT NULL DEFAULT 1,
    Notes           TEXT    DEFAULT '',
    CreatedAt       TEXT    DEFAULT (datetime('now','localtime')),

    -- المفاتيح الخارجية
    FOREIGN KEY (TeacherId) REFERENCES Teachers (Id) ON DELETE RESTRICT ON UPDATE CASCADE,
    FOREIGN KEY (StatusId)  REFERENCES AttendanceStatus (Id) ON DELETE RESTRICT ON UPDATE CASCADE
);

-- منع تسجيل المدرس مرتين في نفس التاريخ
CREATE UNIQUE INDEX IF NOT EXISTS UQ_TeacherAttendance_Teacher_Date
    ON TeacherAttendance (TeacherId, AttendanceDate);

-- فهارس لتحسين الأداء
CREATE INDEX IF NOT EXISTS IX_TeacherAttendance_TeacherId ON TeacherAttendance (TeacherId);
CREATE INDEX IF NOT EXISTS IX_TeacherAttendance_Date ON TeacherAttendance (AttendanceDate);
CREATE INDEX IF NOT EXISTS IX_TeacherAttendance_StatusId ON TeacherAttendance (StatusId);
