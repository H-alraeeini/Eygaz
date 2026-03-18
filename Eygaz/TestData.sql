-- =============================================
-- بيانات تجريبية لاختبار نظام الحضور والغياب
-- =============================================

PRAGMA foreign_keys = ON;

-- =============================================
-- 1. إضافة فصول تجريبية
-- =============================================
INSERT OR IGNORE INTO Classes (Id, ClassName, ClassLocation, IsActive, Notes, CreatedAt)
VALUES
    (1, 'الفصل الأول',  'الطابق الأول - غرفة 101',  1, '', datetime('now','localtime')),
    (2, 'الفصل الثاني', 'الطابق الأول - غرفة 102',  1, '', datetime('now','localtime')),
    (3, 'الفصل الثالث', 'الطابق الثاني - غرفة 201', 1, '', datetime('now','localtime'));

-- =============================================
-- 2. إضافة مواد تجريبية
-- =============================================
INSERT OR IGNORE INTO Subjects (Id, SubjectName, Description, IsActive, CreatedAt)
VALUES
    (1, 'القرآن الكريم',    'حفظ وتلاوة القرآن الكريم',  1, datetime('now','localtime')),
    (2, 'التجويد',          'أحكام التجويد',              1, datetime('now','localtime')),
    (3, 'الفقه',            'الفقه الإسلامي',             1, datetime('now','localtime')),
    (4, 'الحديث الشريف',    'دراسة الأحاديث النبوية',     1, datetime('now','localtime'));

-- =============================================
-- 3. إضافة مدرسين تجريبيين
-- =============================================
INSERT OR IGNORE INTO Teachers (Id, FullName, Address, Phone, Gender, IsActive, Notes, CreatedAt)
VALUES
    (1, 'أحمد محمد علي',     'شارع المدينة',    '0501234567', 1, 1, '', datetime('now','localtime')),
    (2, 'خالد عبدالله سعيد', 'حي النور',        '0509876543', 1, 1, '', datetime('now','localtime')),
    (3, 'عمر حسن إبراهيم',  'شارع الملك فهد',  '0551112233', 1, 1, '', datetime('now','localtime'));

-- =============================================
-- 4. إضافة طلاب تجريبيين (مع ربطهم بالفصول)
-- =============================================
-- ملاحظة: تأكد من وجود عمود ClassId في جدول Students
-- ALTER TABLE Students ADD COLUMN ClassId INTEGER DEFAULT 0;

INSERT OR IGNORE INTO Students (Id, FullName, Age, BirthDate, Address, Phone, Gender, IsActive, Notes, CreatedAt, ClassId)
VALUES
    (1,  'محمد أحمد',      12, '2014-03-15', 'حي السلام',    '0501111111', 1, 1, '', datetime('now','localtime'), 1),
    (2,  'عبدالرحمن خالد', 11, '2015-06-20', 'حي النور',     '0502222222', 1, 1, '', datetime('now','localtime'), 1),
    (3,  'يوسف عمر',       13, '2013-01-10', 'شارع المدينة', '0503333333', 1, 1, '', datetime('now','localtime'), 1),
    (4,  'إبراهيم سعد',    12, '2014-09-05', 'حي الربيع',    '0504444444', 1, 1, '', datetime('now','localtime'), 1),
    (5,  'عبدالله ناصر',   11, '2015-11-22', 'حي المروج',    '0505555555', 1, 1, '', datetime('now','localtime'), 1),
    (6,  'سعد محمد',       12, '2014-04-18', 'حي الفيحاء',   '0506666666', 1, 1, '', datetime('now','localtime'), 2),
    (7,  'فيصل أحمد',      13, '2013-07-30', 'حي العزيزية',  '0507777777', 1, 1, '', datetime('now','localtime'), 2),
    (8,  'ماجد علي',        11, '2015-02-14', 'حي الروضة',    '0508888888', 1, 1, '', datetime('now','localtime'), 2),
    (9,  'نايف حسن',       12, '2014-12-01', 'حي الملك',     '0509999999', 1, 1, '', datetime('now','localtime'), 2),
    (10, 'تركي سلطان',     13, '2013-08-25', 'حي الورود',    '0501010101', 1, 1, '', datetime('now','localtime'), 3),
    (11, 'بندر خالد',      11, '2015-05-12', 'حي الياسمين',  '0502020202', 1, 1, '', datetime('now','localtime'), 3),
    (12, 'حمد عبدالعزيز',  12, '2014-10-08', 'حي التعاون',   '0503030303', 1, 1, '', datetime('now','localtime'), 3);

-- =============================================
-- 5. بيانات تجريبية للحضور
-- =============================================

-- جلسة 1: الفصل الأول - القرآن الكريم - 2026-03-10
INSERT OR IGNORE INTO AttendanceSessions (Id, ClassId, SubjectId, TeacherId, SessionDate, Notes)
VALUES (1, 1, 1, 1, '2026-03-10', 'حصة تجريبية');

INSERT OR IGNORE INTO StudentAttendance (SessionId, StudentId, StatusId, Notes) VALUES (1, 1, 1, '');  -- حاضر
INSERT OR IGNORE INTO StudentAttendance (SessionId, StudentId, StatusId, Notes) VALUES (1, 2, 1, '');  -- حاضر
INSERT OR IGNORE INTO StudentAttendance (SessionId, StudentId, StatusId, Notes) VALUES (1, 3, 2, '');  -- غائب
INSERT OR IGNORE INTO StudentAttendance (SessionId, StudentId, StatusId, Notes) VALUES (1, 4, 3, '');  -- متأخر
INSERT OR IGNORE INTO StudentAttendance (SessionId, StudentId, StatusId, Notes) VALUES (1, 5, 1, '');  -- حاضر

-- جلسة 2: الفصل الأول - القرآن الكريم - 2026-03-12
INSERT OR IGNORE INTO AttendanceSessions (Id, ClassId, SubjectId, TeacherId, SessionDate, Notes)
VALUES (2, 1, 1, 1, '2026-03-12', '');

INSERT OR IGNORE INTO StudentAttendance (SessionId, StudentId, StatusId, Notes) VALUES (2, 1, 1, '');
INSERT OR IGNORE INTO StudentAttendance (SessionId, StudentId, StatusId, Notes) VALUES (2, 2, 2, '');
INSERT OR IGNORE INTO StudentAttendance (SessionId, StudentId, StatusId, Notes) VALUES (2, 3, 1, '');
INSERT OR IGNORE INTO StudentAttendance (SessionId, StudentId, StatusId, Notes) VALUES (2, 4, 1, '');
INSERT OR IGNORE INTO StudentAttendance (SessionId, StudentId, StatusId, Notes) VALUES (2, 5, 4, 'مريض');

-- جلسة 3: الفصل الثاني - التجويد - 2026-03-10
INSERT OR IGNORE INTO AttendanceSessions (Id, ClassId, SubjectId, TeacherId, SessionDate, Notes)
VALUES (3, 2, 2, 2, '2026-03-10', '');

INSERT OR IGNORE INTO StudentAttendance (SessionId, StudentId, StatusId, Notes) VALUES (3, 6, 1, '');
INSERT OR IGNORE INTO StudentAttendance (SessionId, StudentId, StatusId, Notes) VALUES (3, 7, 1, '');
INSERT OR IGNORE INTO StudentAttendance (SessionId, StudentId, StatusId, Notes) VALUES (3, 8, 3, '');
INSERT OR IGNORE INTO StudentAttendance (SessionId, StudentId, StatusId, Notes) VALUES (3, 9, 2, '');
