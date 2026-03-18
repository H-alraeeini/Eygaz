-- =============================================
-- جدول الشهادات الطلابية
-- =============================================

CREATE TABLE IF NOT EXISTS StudentCertificates (
    Id               INTEGER PRIMARY KEY AUTOINCREMENT,
    StudentId        INTEGER NOT NULL,
    ClassId          INTEGER NOT NULL,
    CertificateType  TEXT NOT NULL DEFAULT 'شهادة حضور',
    IssueDate        DATE NOT NULL,
    AttendancePercentage REAL DEFAULT 0,
    TotalSessions    INTEGER DEFAULT 0,
    AbsentCount      INTEGER DEFAULT 0,
    PresentCount     INTEGER DEFAULT 0,
    TeacherName      TEXT,
    Notes            TEXT,
    CreatedAt        DATETIME DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (StudentId) REFERENCES Students(Id) ON DELETE RESTRICT,
    FOREIGN KEY (ClassId) REFERENCES Classes(Id) ON DELETE RESTRICT
);

-- فهرس للبحث السريع
CREATE INDEX IF NOT EXISTS IX_StudentCertificates_StudentId ON StudentCertificates(StudentId);
CREATE INDEX IF NOT EXISTS IX_StudentCertificates_ClassId ON StudentCertificates(ClassId);
CREATE INDEX IF NOT EXISTS IX_StudentCertificates_IssueDate ON StudentCertificates(IssueDate);
