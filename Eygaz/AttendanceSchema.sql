-- =============================================
-- نظام الحضور والغياب - Attendance System Schema
-- قاعدة بيانات: SQLite (EygazDb.db)
-- =============================================

-- تفعيل المفاتيح الخارجية
PRAGMA foreign_keys = ON;

-- =============================================
-- 1. جدول حالات الحضور AttendanceStatus
-- =============================================
CREATE TABLE IF NOT EXISTS AttendanceStatus (
    Id          INTEGER PRIMARY KEY,
    StatusName  TEXT    NOT NULL,
    StatusCode  TEXT    NOT NULL UNIQUE
);

-- إدراج حالات الحضور الأساسية
INSERT OR IGNORE INTO AttendanceStatus (Id, StatusName, StatusCode) VALUES (1, 'حاضر',      'P');
INSERT OR IGNORE INTO AttendanceStatus (Id, StatusName, StatusCode) VALUES (2, 'غائب',      'A');
INSERT OR IGNORE INTO AttendanceStatus (Id, StatusName, StatusCode) VALUES (3, 'متأخر',     'L');
INSERT OR IGNORE INTO AttendanceStatus (Id, StatusName, StatusCode) VALUES (4, 'غياب بعذر', 'E');

-- =============================================
-- 2. جدول جلسات الحضور AttendanceSessions
-- =============================================
CREATE TABLE IF NOT EXISTS AttendanceSessions (
    Id          INTEGER PRIMARY KEY AUTOINCREMENT,
    ClassId     INTEGER NOT NULL,
    SubjectId   INTEGER NOT NULL,
    TeacherId   INTEGER NOT NULL,
    SessionDate TEXT    NOT NULL,
    Notes       TEXT    DEFAULT '',
    CreatedAt   TEXT    DEFAULT (datetime('now','localtime')),

    -- المفاتيح الخارجية
    FOREIGN KEY (ClassId)   REFERENCES Classes  (Id) ON DELETE RESTRICT ON UPDATE CASCADE,
    FOREIGN KEY (SubjectId) REFERENCES Subjects (Id) ON DELETE RESTRICT ON UPDATE CASCADE,
    FOREIGN KEY (TeacherId) REFERENCES Teachers (Id) ON DELETE RESTRICT ON UPDATE CASCADE
);

-- منع تكرار الجلسة لنفس الفصل والمادة في نفس التاريخ
CREATE UNIQUE INDEX IF NOT EXISTS UQ_Session_Class_Subject_Date
    ON AttendanceSessions (ClassId, SubjectId, SessionDate);

-- فهارس لتحسين الأداء
CREATE INDEX IF NOT EXISTS IX_AttendanceSessions_ClassId    ON AttendanceSessions (ClassId);
CREATE INDEX IF NOT EXISTS IX_AttendanceSessions_SubjectId  ON AttendanceSessions (SubjectId);
CREATE INDEX IF NOT EXISTS IX_AttendanceSessions_TeacherId  ON AttendanceSessions (TeacherId);
CREATE INDEX IF NOT EXISTS IX_AttendanceSessions_Date       ON AttendanceSessions (SessionDate);

-- =============================================
-- 3. جدول حضور الطلاب StudentAttendance
-- =============================================
CREATE TABLE IF NOT EXISTS StudentAttendance (
    Id          INTEGER PRIMARY KEY AUTOINCREMENT,
    SessionId   INTEGER NOT NULL,
    StudentId   INTEGER NOT NULL,
    StatusId    INTEGER NOT NULL DEFAULT 1,
    Notes       TEXT    DEFAULT '',

    -- المفاتيح الخارجية
    FOREIGN KEY (SessionId) REFERENCES AttendanceSessions (Id) ON DELETE CASCADE  ON UPDATE CASCADE,
    FOREIGN KEY (StudentId) REFERENCES Students            (Id) ON DELETE RESTRICT ON UPDATE CASCADE,
    FOREIGN KEY (StatusId)  REFERENCES AttendanceStatus     (Id) ON DELETE RESTRICT ON UPDATE CASCADE
);

-- منع تسجيل الطالب مرتين في نفس الجلسة
CREATE UNIQUE INDEX IF NOT EXISTS UQ_StudentAttendance_Session_Student
    ON StudentAttendance (SessionId, StudentId);

-- فهارس لتحسين الأداء
CREATE INDEX IF NOT EXISTS IX_StudentAttendance_SessionId ON StudentAttendance (SessionId);
CREATE INDEX IF NOT EXISTS IX_StudentAttendance_StudentId ON StudentAttendance (StudentId);
CREATE INDEX IF NOT EXISTS IX_StudentAttendance_StatusId  ON StudentAttendance (StatusId);

-- =============================================
-- 4. إضافة عمود ClassId لجدول الطلاب (إن لم يكن موجوداً)
-- لربط الطالب بالفصل
-- =============================================
-- ملاحظة: SQLite لا تدعم IF NOT EXISTS مع ALTER TABLE
-- لذلك قم بتنفيذ هذا الأمر يدوياً إذا لم يكن العمود موجوداً:
-- ALTER TABLE Students ADD COLUMN ClassId INTEGER DEFAULT 0;
