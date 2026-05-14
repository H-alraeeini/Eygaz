-- =============================================
-- 008 - ربط الطلاب بالمواد الدراسية
-- =============================================

-- جدول ربط الطالب بالمواد (لكل طالب قائمة مواد خاصة به)
CREATE TABLE IF NOT EXISTS StudentSubjects (
    Id          INTEGER PRIMARY KEY AUTOINCREMENT,
    StudentId   INTEGER NOT NULL,
    SubjectId   INTEGER NOT NULL,
    FOREIGN KEY (StudentId) REFERENCES Students(Id) ON DELETE CASCADE,
    FOREIGN KEY (SubjectId) REFERENCES Subjects(Id) ON DELETE RESTRICT,
    UNIQUE(StudentId, SubjectId)
);

-- فهارس
CREATE INDEX IF NOT EXISTS IX_StudentSubjects_StudentId ON StudentSubjects(StudentId);
CREATE INDEX IF NOT EXISTS IX_StudentSubjects_SubjectId ON StudentSubjects(SubjectId);
