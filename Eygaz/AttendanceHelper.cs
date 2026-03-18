using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Eygaz
{
    /// <summary>
    /// طبقة الوصول للبيانات الخاصة بنظام الحضور والغياب
    /// </summary>
    public class AttendanceHelper
    {
        private Func f = new Func();

        // =============================================
        // 1. جلب طلاب فصل معين
        // =============================================
        public DataTable GetStudentsByClass(int classId)
        {
            string sql = $@"
                SELECT Id, FullName, ClassId
                FROM Students
                WHERE ClassId = {classId} AND IsActive = 1
                ORDER BY FullName";
            return f.GetData(sql);
        }

        // =============================================
        // 2. البحث عن جلسة موجودة
        // =============================================
        public string FindExistingSession(int classId, int subjectId, string sessionDate)
        {
            string sql = $@"
                SELECT Id FROM AttendanceSessions
                WHERE ClassId = {classId}
                  AND SubjectId = {subjectId}
                  AND SessionDate = '{sessionDate}'";
            return f.GetScalar(sql);
        }

        // =============================================
        // 3. إنشاء جلسة حضور جديدة
        // =============================================
        public int CreateSession(int classId, int subjectId, int teacherId, string sessionDate, string notes = "")
        {
            string sql = $@"
                INSERT INTO AttendanceSessions (ClassId, SubjectId, TeacherId, SessionDate, Notes)
                VALUES ({classId}, {subjectId}, {teacherId}, '{sessionDate}', '{notes}')";

            string result = f.Trans(sql);
            if (result != "DONE...")
                return -1;

            string idStr = f.GetScalar("SELECT last_insert_rowid()");
            return string.IsNullOrEmpty(idStr) ? -1 : int.Parse(idStr);
        }

        // =============================================
        // 4. حفظ حضور طالب واحد (Upsert)
        // =============================================
        public bool SaveAttendance(int sessionId, int studentId, int statusId, string notes = "")
        {
            string existingId = f.GetScalar($@"
                SELECT Id FROM StudentAttendance
                WHERE SessionId = {sessionId} AND StudentId = {studentId}");

            string sql;
            if (!string.IsNullOrEmpty(existingId))
            {
                sql = $@"UPDATE StudentAttendance
                    SET StatusId = {statusId}, Notes = '{notes}'
                    WHERE Id = {existingId}";
            }
            else
            {
                sql = $@"INSERT INTO StudentAttendance (SessionId, StudentId, StatusId, Notes)
                    VALUES ({sessionId}, {studentId}, {statusId}, '{notes}')";
            }

            string result = f.Trans(sql);
            return result == "DONE...";
        }

        // =============================================
        // 5. حفظ حضور مجموعة طلاب (Bulk Save)
        // =============================================
        public bool SaveBulkAttendance(int sessionId, DataTable attendanceData)
        {
            try
            {
                for (int i = 0; i < attendanceData.Rows.Count; i++)
                {
                    int studentId = Convert.ToInt32(attendanceData.Rows[i]["StudentId"]);
                    int statusId = Convert.ToInt32(attendanceData.Rows[i]["StatusId"]);
                    string notes = attendanceData.Rows[i]["Notes"]?.ToString() ?? "";

                    if (!SaveAttendance(sessionId, studentId, statusId, notes))
                        return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // =============================================
        // 6. تحديث حالة حضور طالب
        // =============================================
        public bool UpdateAttendance(int attendanceId, int statusId, string notes = "")
        {
            string sql = $@"UPDATE StudentAttendance
                SET StatusId = {statusId}, Notes = '{notes}'
                WHERE Id = {attendanceId}";

            string result = f.Trans(sql);
            return result == "DONE...";
        }

        // =============================================
        // 7. جلب حضور جلسة معينة
        // =============================================
        public DataTable GetSessionAttendance(int sessionId)
        {
            string sql = $@"
                SELECT
                    sa.Id, sa.SessionId, sa.StudentId,
                    s.FullName AS StudentName,
                    sa.StatusId, ast.StatusName, sa.Notes
                FROM StudentAttendance sa
                INNER JOIN Students s ON sa.StudentId = s.Id
                INNER JOIN AttendanceStatus ast ON sa.StatusId = ast.Id
                WHERE sa.SessionId = {sessionId}
                ORDER BY s.FullName";
            return f.GetData(sql);
        }

        // =============================================
        // 8. تقرير حضور طالب شهري
        // =============================================
        public DataTable GetStudentMonthlyReport(int studentId, int month, int year)
        {
            string monthStr = month.ToString("D2");
            string yearStr = year.ToString();

            string sql = $@"
                SELECT
                    s.FullName AS 'اسم الطالب',
                    sess.SessionDate AS 'التاريخ',
                    sub.SubjectName AS 'المادة',
                    c.ClassName AS 'الفصل',
                    ast.StatusName AS 'الحالة',
                    sa.Notes AS 'ملاحظات'
                FROM StudentAttendance sa
                INNER JOIN AttendanceSessions sess ON sa.SessionId = sess.Id
                INNER JOIN Students s ON sa.StudentId = s.Id
                INNER JOIN Subjects sub ON sess.SubjectId = sub.Id
                INNER JOIN Classes c ON sess.ClassId = c.Id
                INNER JOIN AttendanceStatus ast ON sa.StatusId = ast.Id
                WHERE sa.StudentId = {studentId}
                  AND substr(sess.SessionDate, 1, 4) = '{yearStr}'
                  AND substr(sess.SessionDate, 6, 2) = '{monthStr}'
                ORDER BY sess.SessionDate";
            return f.GetData(sql);
        }

        // =============================================
        // 9. تقرير حضور فصل معين
        // =============================================
        public DataTable GetClassMonthlyReport(int classId, int month, int year)
        {
            string monthStr = month.ToString("D2");
            string yearStr = year.ToString();

            string sql = $@"
                SELECT
                    s.FullName AS 'اسم الطالب',
                    sess.SessionDate AS 'التاريخ',
                    sub.SubjectName AS 'المادة',
                    ast.StatusName AS 'الحالة'
                FROM StudentAttendance sa
                INNER JOIN AttendanceSessions sess ON sa.SessionId = sess.Id
                INNER JOIN Students s ON sa.StudentId = s.Id
                INNER JOIN Subjects sub ON sess.SubjectId = sub.Id
                INNER JOIN AttendanceStatus ast ON sa.StatusId = ast.Id
                WHERE sess.ClassId = {classId}
                  AND substr(sess.SessionDate, 1, 4) = '{yearStr}'
                  AND substr(sess.SessionDate, 6, 2) = '{monthStr}'
                ORDER BY s.FullName, sess.SessionDate";
            return f.GetData(sql);
        }

        // =============================================
        // 10. تقرير الغياب
        // =============================================
        public DataTable GetAbsentReport(int classId, string dateFrom, string dateTo)
        {
            string classFilter = classId > 0 ? $"AND sess.ClassId = {classId}" : "";
            string sql = $@"
                SELECT
                    s.FullName AS 'اسم الطالب',
                    c.ClassName AS 'الفصل',
                    COUNT(*) AS 'عدد مرات الغياب',
                    GROUP_CONCAT(sess.SessionDate, ', ') AS 'تواريخ الغياب'
                FROM StudentAttendance sa
                INNER JOIN AttendanceSessions sess ON sa.SessionId = sess.Id
                INNER JOIN Students s ON sa.StudentId = s.Id
                INNER JOIN Classes c ON sess.ClassId = c.Id
                WHERE sa.StatusId = 2
                  {classFilter}
                  AND sess.SessionDate BETWEEN '{dateFrom}' AND '{dateTo}'
                GROUP BY sa.StudentId
                ORDER BY COUNT(*) DESC";
            return f.GetData(sql);
        }

        // =============================================
        // 11. تقرير المتأخرين
        // =============================================
        public DataTable GetLateReport(int classId, string dateFrom, string dateTo)
        {
            string classFilter = classId > 0 ? $"AND sess.ClassId = {classId}" : "";
            string sql = $@"
                SELECT
                    s.FullName AS 'اسم الطالب',
                    c.ClassName AS 'الفصل',
                    COUNT(*) AS 'عدد مرات التأخير',
                    GROUP_CONCAT(sess.SessionDate, ', ') AS 'تواريخ التأخير'
                FROM StudentAttendance sa
                INNER JOIN AttendanceSessions sess ON sa.SessionId = sess.Id
                INNER JOIN Students s ON sa.StudentId = s.Id
                INNER JOIN Classes c ON sess.ClassId = c.Id
                WHERE sa.StatusId = 3
                  {classFilter}
                  AND sess.SessionDate BETWEEN '{dateFrom}' AND '{dateTo}'
                GROUP BY sa.StudentId
                ORDER BY COUNT(*) DESC";
            return f.GetData(sql);
        }

        // =============================================
        // 12. ملخص حضور طالب (إحصائيات)
        // =============================================
        public DataTable GetStudentAttendanceSummary(int studentId, int month, int year)
        {
            string monthStr = month.ToString("D2");
            string yearStr = year.ToString();

            string sql = $@"
                SELECT
                    s.FullName AS 'اسم الطالب',
                    SUM(CASE WHEN sa.StatusId = 1 THEN 1 ELSE 0 END) AS 'حاضر',
                    SUM(CASE WHEN sa.StatusId = 2 THEN 1 ELSE 0 END) AS 'غائب',
                    SUM(CASE WHEN sa.StatusId = 3 THEN 1 ELSE 0 END) AS 'متأخر',
                    SUM(CASE WHEN sa.StatusId = 4 THEN 1 ELSE 0 END) AS 'غياب بعذر',
                    COUNT(*) AS 'إجمالي الحصص'
                FROM StudentAttendance sa
                INNER JOIN AttendanceSessions sess ON sa.SessionId = sess.Id
                INNER JOIN Students s ON sa.StudentId = s.Id
                WHERE sa.StudentId = {studentId}
                  AND substr(sess.SessionDate, 1, 4) = '{yearStr}'
                  AND substr(sess.SessionDate, 6, 2) = '{monthStr}'
                GROUP BY sa.StudentId";
            return f.GetData(sql);
        }

        // =============================================
        // 13. تحضير Grid الحضور
        // =============================================
        public DataTable PrepareAttendanceGrid(int classId, int sessionId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentId", typeof(int));
            dt.Columns.Add("StudentName", typeof(string));
            dt.Columns.Add("StatusId", typeof(int));
            dt.Columns.Add("StatusName", typeof(string));
            dt.Columns.Add("Notes", typeof(string));

            if (sessionId > 0)
            {
                DataTable existing = GetSessionAttendance(sessionId);
                if (existing != null)
                {
                    foreach (DataRow row in existing.Rows)
                    {
                        dt.Rows.Add(
                            Convert.ToInt32(row["StudentId"]),
                            row["StudentName"].ToString(),
                            Convert.ToInt32(row["StatusId"]),
                            row["StatusName"].ToString(),
                            row["Notes"]?.ToString() ?? ""
                        );
                    }
                }
            }
            else
            {
                DataTable students = GetStudentsByClass(classId);
                if (students != null)
                {
                    foreach (DataRow row in students.Rows)
                    {
                        dt.Rows.Add(
                            Convert.ToInt32(row["Id"]),
                            row["FullName"].ToString(),
                            1, "حاضر", ""
                        );
                    }
                }
            }
            return dt;
        }

        // =============================================
        // 14. جلب حالات الحضور
        // =============================================
        public DataTable GetAttendanceStatuses()
        {
            string sql = "SELECT Id, StatusName FROM AttendanceStatus ORDER BY Id";
            return f.GetData(sql);
        }

        // =============================================
        // 15. جلب جميع الجلسات مع الفلترة (شاشة إدارة الجلسات)
        // =============================================
        public DataTable GetSessions(int classId, int subjectId, int teacherId, string dateFrom, string dateTo)
        {
            string where = "WHERE 1=1";
            if (classId > 0) where += $" AND sess.ClassId = {classId}";
            if (subjectId > 0) where += $" AND sess.SubjectId = {subjectId}";
            if (teacherId > 0) where += $" AND sess.TeacherId = {teacherId}";
            if (!string.IsNullOrEmpty(dateFrom)) where += $" AND sess.SessionDate >= '{dateFrom}'";
            if (!string.IsNullOrEmpty(dateTo)) where += $" AND sess.SessionDate <= '{dateTo}'";

            string sql = $@"
                SELECT
                    sess.Id AS SessionId,
                    sess.SessionDate,
                    c.ClassName,
                    sub.SubjectName,
                    t.FullName AS TeacherName,
                    (SELECT COUNT(*) FROM StudentAttendance sa WHERE sa.SessionId = sess.Id) AS StudentsCount,
                    sess.Notes
                FROM AttendanceSessions sess
                INNER JOIN Classes c ON sess.ClassId = c.Id
                INNER JOIN Subjects sub ON sess.SubjectId = sub.Id
                INNER JOIN Teachers t ON sess.TeacherId = t.Id
                {where}
                ORDER BY sess.SessionDate DESC, c.ClassName";
            return f.GetData(sql);
        }

        // =============================================
        // 16. حذف جلسة مع جميع سجلاتها
        // =============================================
        public bool DeleteSession(int sessionId)
        {
            // حذف سجلات الحضور أولاً
            string sql1 = $"DELETE FROM StudentAttendance WHERE SessionId = {sessionId}";
            f.Trans(sql1);

            // حذف الجلسة
            string sql2 = $"DELETE FROM AttendanceSessions WHERE Id = {sessionId}";
            string result = f.Trans(sql2);
            return result == "DONE...";
        }

        // =============================================
        // 17. جلب تفاصيل جلسة واحدة
        // =============================================
        public DataTable GetSessionDetails(int sessionId)
        {
            string sql = $@"
                SELECT
                    sess.Id, sess.ClassId, sess.SubjectId, sess.TeacherId,
                    sess.SessionDate, sess.Notes,
                    c.ClassName, sub.SubjectName, t.FullName AS TeacherName
                FROM AttendanceSessions sess
                INNER JOIN Classes c ON sess.ClassId = c.Id
                INNER JOIN Subjects sub ON sess.SubjectId = sub.Id
                INNER JOIN Teachers t ON sess.TeacherId = t.Id
                WHERE sess.Id = {sessionId}";
            return f.GetData(sql);
        }

        // =============================================
        // 18. تحديث جلسة حضور
        // =============================================
        public bool UpdateSession(int sessionId, int classId, int subjectId, int teacherId, string sessionDate, string notes)
        {
            string sql = $@"UPDATE AttendanceSessions
                SET ClassId = {classId}, SubjectId = {subjectId},
                    TeacherId = {teacherId}, SessionDate = '{sessionDate}', Notes = '{notes}'
                WHERE Id = {sessionId}";
            string result = f.Trans(sql);
            return result == "DONE...";
        }

        // =============================================
        // 19. إحصائيات لوحة التحكم (Dashboard)
        // =============================================
        public int GetTotalStudents()
        {
            string val = f.GetScalar("SELECT COUNT(*) FROM Students WHERE IsActive = 1");
            return string.IsNullOrEmpty(val) ? 0 : int.Parse(val);
        }

        public int GetTotalTeachers()
        {
            string val = f.GetScalar("SELECT COUNT(*) FROM Teachers WHERE IsActive = 1");
            return string.IsNullOrEmpty(val) ? 0 : int.Parse(val);
        }

        public int GetTodaySessions(string today)
        {
            string val = f.GetScalar($"SELECT COUNT(*) FROM AttendanceSessions WHERE SessionDate = '{today}'");
            return string.IsNullOrEmpty(val) ? 0 : int.Parse(val);
        }

        public int GetTodayAbsent(string today)
        {
            string val = f.GetScalar($@"
                SELECT COUNT(*) FROM StudentAttendance sa
                INNER JOIN AttendanceSessions sess ON sa.SessionId = sess.Id
                WHERE sa.StatusId = 2 AND sess.SessionDate = '{today}'");
            return string.IsNullOrEmpty(val) ? 0 : int.Parse(val);
        }

        public int GetTodayLate(string today)
        {
            string val = f.GetScalar($@"
                SELECT COUNT(*) FROM StudentAttendance sa
                INNER JOIN AttendanceSessions sess ON sa.SessionId = sess.Id
                WHERE sa.StatusId = 3 AND sess.SessionDate = '{today}'");
            return string.IsNullOrEmpty(val) ? 0 : int.Parse(val);
        }

        public int GetTodayPresent(string today)
        {
            string val = f.GetScalar($@"
                SELECT COUNT(*) FROM StudentAttendance sa
                INNER JOIN AttendanceSessions sess ON sa.SessionId = sess.Id
                WHERE sa.StatusId = 1 AND sess.SessionDate = '{today}'");
            return string.IsNullOrEmpty(val) ? 0 : int.Parse(val);
        }

        public int GetTotalClasses()
        {
            string val = f.GetScalar("SELECT COUNT(*) FROM Classes WHERE IsActive = 1");
            return string.IsNullOrEmpty(val) ? 0 : int.Parse(val);
        }

        public int GetTotalSubjects()
        {
            string val = f.GetScalar("SELECT COUNT(*) FROM Subjects WHERE IsActive = 1");
            return string.IsNullOrEmpty(val) ? 0 : int.Parse(val);
        }

        // =============================================
        // 20. متابعة الغياب - الطلاب الأكثر غياباً
        // =============================================
        public DataTable GetAbsenceTracking(int classId, string dateFrom, string dateTo, int minAbsences)
        {
            string classFilter = classId > 0 ? $"AND sess.ClassId = {classId}" : "";
            string sql = $@"
                SELECT
                    s.Id AS StudentId,
                    s.FullName AS 'اسم الطالب',
                    c.ClassName AS 'الفصل',
                    SUM(CASE WHEN sa.StatusId = 2 THEN 1 ELSE 0 END) AS 'غياب بدون عذر',
                    SUM(CASE WHEN sa.StatusId = 4 THEN 1 ELSE 0 END) AS 'غياب بعذر',
                    SUM(CASE WHEN sa.StatusId = 3 THEN 1 ELSE 0 END) AS 'تأخير',
                    SUM(CASE WHEN sa.StatusId = 2 OR sa.StatusId = 4 THEN 1 ELSE 0 END) AS 'إجمالي الغياب',
                    COUNT(*) AS 'إجمالي الحصص',
                    ROUND(CAST(SUM(CASE WHEN sa.StatusId = 1 THEN 1 ELSE 0 END) AS FLOAT) / COUNT(*) * 100, 1) AS 'نسبة الحضور %'
                FROM StudentAttendance sa
                INNER JOIN AttendanceSessions sess ON sa.SessionId = sess.Id
                INNER JOIN Students s ON sa.StudentId = s.Id
                INNER JOIN Classes c ON sess.ClassId = c.Id
                WHERE sess.SessionDate BETWEEN '{dateFrom}' AND '{dateTo}'
                  {classFilter}
                GROUP BY sa.StudentId
                HAVING SUM(CASE WHEN sa.StatusId = 2 OR sa.StatusId = 4 THEN 1 ELSE 0 END) >= {minAbsences}
                ORDER BY SUM(CASE WHEN sa.StatusId = 2 OR sa.StatusId = 4 THEN 1 ELSE 0 END) DESC";
            return f.GetData(sql);
        }

        // =============================================
        // 21. تقرير حضور طالب بالفترة
        // =============================================
        public DataTable GetStudentReportByDate(int studentId, string dateFrom, string dateTo)
        {
            string sql = $@"
                SELECT
                    s.FullName AS 'اسم الطالب',
                    sess.SessionDate AS 'التاريخ',
                    sub.SubjectName AS 'المادة',
                    c.ClassName AS 'الفصل',
                    t.FullName AS 'المدرس',
                    ast.StatusName AS 'الحالة',
                    sa.Notes AS 'ملاحظات'
                FROM StudentAttendance sa
                INNER JOIN AttendanceSessions sess ON sa.SessionId = sess.Id
                INNER JOIN Students s ON sa.StudentId = s.Id
                INNER JOIN Subjects sub ON sess.SubjectId = sub.Id
                INNER JOIN Classes c ON sess.ClassId = c.Id
                INNER JOIN Teachers t ON sess.TeacherId = t.Id
                INNER JOIN AttendanceStatus ast ON sa.StatusId = ast.Id
                WHERE sa.StudentId = {studentId}
                  AND sess.SessionDate BETWEEN '{dateFrom}' AND '{dateTo}'
                ORDER BY sess.SessionDate";
            return f.GetData(sql);
        }

        // =============================================
        // 22. التقرير الشهري الشامل
        // =============================================
        public DataTable GetMonthlyOverviewReport(int month, int year)
        {
            string monthStr = month.ToString("D2");
            string yearStr = year.ToString();

            string sql = $@"
                SELECT
                    c.ClassName AS 'الفصل',
                    s.FullName AS 'اسم الطالب',
                    SUM(CASE WHEN sa.StatusId = 1 THEN 1 ELSE 0 END) AS 'حاضر',
                    SUM(CASE WHEN sa.StatusId = 2 THEN 1 ELSE 0 END) AS 'غائب',
                    SUM(CASE WHEN sa.StatusId = 3 THEN 1 ELSE 0 END) AS 'متأخر',
                    SUM(CASE WHEN sa.StatusId = 4 THEN 1 ELSE 0 END) AS 'غياب بعذر',
                    COUNT(*) AS 'إجمالي',
                    ROUND(CAST(SUM(CASE WHEN sa.StatusId = 1 THEN 1 ELSE 0 END) AS FLOAT) / COUNT(*) * 100, 1) AS 'نسبة الحضور %'
                FROM StudentAttendance sa
                INNER JOIN AttendanceSessions sess ON sa.SessionId = sess.Id
                INNER JOIN Students s ON sa.StudentId = s.Id
                INNER JOIN Classes c ON sess.ClassId = c.Id
                WHERE substr(sess.SessionDate, 1, 4) = '{yearStr}'
                  AND substr(sess.SessionDate, 6, 2) = '{monthStr}'
                GROUP BY sa.StudentId
                ORDER BY c.ClassName, s.FullName";
            return f.GetData(sql);
        }

        // =============================================
        // 23. إحصائيات حضور طالب (للشهادات)
        // =============================================
        public DataTable GetStudentCertificateStats(int studentId, int classId)
        {
            string classFilter = classId > 0 ? $"AND sess.ClassId = {classId}" : "";
            string sql = $@"
                SELECT
                    s.FullName AS StudentName,
                    c.ClassName,
                    COUNT(*) AS TotalSessions,
                    SUM(CASE WHEN sa.StatusId = 1 THEN 1 ELSE 0 END) AS PresentCount,
                    SUM(CASE WHEN sa.StatusId = 2 THEN 1 ELSE 0 END) AS AbsentCount,
                    SUM(CASE WHEN sa.StatusId = 3 THEN 1 ELSE 0 END) AS LateCount,
                    SUM(CASE WHEN sa.StatusId = 4 THEN 1 ELSE 0 END) AS ExcusedCount,
                    ROUND(CAST(SUM(CASE WHEN sa.StatusId = 1 THEN 1 ELSE 0 END) AS FLOAT) / COUNT(*) * 100, 1) AS AttendancePercentage
                FROM StudentAttendance sa
                INNER JOIN AttendanceSessions sess ON sa.SessionId = sess.Id
                INNER JOIN Students s ON sa.StudentId = s.Id
                INNER JOIN Classes c ON sess.ClassId = c.Id
                WHERE sa.StudentId = {studentId}
                  {classFilter}
                GROUP BY sa.StudentId";
            return f.GetData(sql);
        }

        // =============================================
        // 24. جلب اسم المدرس الأكثر تدريساً للطالب
        // =============================================
        public string GetMainTeacher(int studentId, int classId)
        {
            string sql = $@"
                SELECT t.FullName
                FROM AttendanceSessions sess
                INNER JOIN Teachers t ON sess.TeacherId = t.Id
                INNER JOIN StudentAttendance sa ON sa.SessionId = sess.Id
                WHERE sa.StudentId = {studentId} AND sess.ClassId = {classId}
                GROUP BY sess.TeacherId
                ORDER BY COUNT(*) DESC
                LIMIT 1";
            return f.GetScalar(sql);
        }

        // =============================================
        // 25. حفظ سجل شهادة
        // =============================================
        public bool SaveCertificate(int studentId, int classId, string certType,
            string issueDate, double attendancePct, int totalSessions,
            int absentCount, int presentCount, string teacherName, string notes)
        {
            string sql = $@"
                INSERT INTO StudentCertificates
                (StudentId, ClassId, CertificateType, IssueDate, AttendancePercentage,
                 TotalSessions, AbsentCount, PresentCount, TeacherName, Notes)
                VALUES ({studentId}, {classId}, '{certType}', '{issueDate}', {attendancePct},
                        {totalSessions}, {absentCount}, {presentCount}, '{teacherName}', '{notes}')";
            string result = f.Trans(sql);
            return result == "DONE...";
        }

        // =============================================
        // 26. جلب سجل الشهادات
        // =============================================
        public DataTable GetCertificateHistory(int studentId)
        {
            string where = studentId > 0 ? $"WHERE sc.StudentId = {studentId}" : "";
            string sql = $@"
                SELECT
                    sc.Id,
                    s.FullName AS 'اسم الطالب',
                    c.ClassName AS 'الفصل',
                    sc.CertificateType AS 'نوع الشهادة',
                    sc.IssueDate AS 'تاريخ الإصدار',
                    sc.AttendancePercentage AS 'نسبة الحضور',
                    sc.TotalSessions AS 'الجلسات',
                    sc.TeacherName AS 'المدرس'
                FROM StudentCertificates sc
                INNER JOIN Students s ON sc.StudentId = s.Id
                INNER JOIN Classes c ON sc.ClassId = c.Id
                {where}
                ORDER BY sc.IssueDate DESC";
            return f.GetData(sql);
        }

        // =============================================
        // 27. حذف شهادة
        // =============================================
        public bool DeleteCertificate(int certId)
        {
            string sql = $"DELETE FROM StudentCertificates WHERE Id = {certId}";
            string result = f.Trans(sql);
            return result == "DONE...";
        }

        // =============================================================================
        //  حضور وغياب المدرسين (28-34)
        // =============================================================================

        // =============================================
        // 28. جلب جميع المدرسين النشطين
        // =============================================
        public DataTable GetAllTeachers()
        {
            string sql = "SELECT Id, FullName, Phone FROM Teachers WHERE IsActive = 1 ORDER BY FullName";
            return f.GetData(sql);
        }

        // =============================================
        // 29. حفظ/تحديث حضور مدرس واحد (Upsert)
        // =============================================
        public bool SaveTeacherAttendance(int teacherId, string date, int statusId, string notes = "")
        {
            string existingId = f.GetScalar($@"
                SELECT Id FROM TeacherAttendance
                WHERE TeacherId = {teacherId} AND AttendanceDate = '{date}'");

            string sql;
            if (!string.IsNullOrEmpty(existingId))
            {
                sql = $@"UPDATE TeacherAttendance
                    SET StatusId = {statusId}, Notes = '{notes}'
                    WHERE Id = {existingId}";
            }
            else
            {
                sql = $@"INSERT INTO TeacherAttendance (TeacherId, AttendanceDate, StatusId, Notes)
                    VALUES ({teacherId}, '{date}', {statusId}, '{notes}')";
            }

            string result = f.Trans(sql);
            return result == "DONE...";
        }

        // =============================================
        // 30. حفظ حضور مجموعة مدرسين (Bulk Save)
        // =============================================
        public bool SaveBulkTeacherAttendance(string date, DataTable attendanceData)
        {
            try
            {
                for (int i = 0; i < attendanceData.Rows.Count; i++)
                {
                    int teacherId = Convert.ToInt32(attendanceData.Rows[i]["TeacherId"]);
                    int statusId = Convert.ToInt32(attendanceData.Rows[i]["StatusId"]);
                    string notes = attendanceData.Rows[i]["Notes"]?.ToString() ?? "";

                    if (!SaveTeacherAttendance(teacherId, date, statusId, notes))
                        return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // =============================================
        // 31. جلب حضور المدرسين لتاريخ معين
        // =============================================
        public DataTable GetTeacherAttendanceByDate(string date)
        {
            string sql = $@"
                SELECT
                    t.Id AS TeacherId,
                    t.FullName AS TeacherName,
                    t.Phone,
                    COALESCE(ta.StatusId, 1) AS StatusId,
                    COALESCE(ast.StatusName, 'حاضر') AS StatusName,
                    COALESCE(ta.Notes, '') AS Notes
                FROM Teachers t
                LEFT JOIN TeacherAttendance ta ON t.Id = ta.TeacherId AND ta.AttendanceDate = '{date}'
                LEFT JOIN AttendanceStatus ast ON ta.StatusId = ast.Id
                WHERE t.IsActive = 1
                ORDER BY t.FullName";
            return f.GetData(sql);
        }

        // =============================================
        // 32. تقرير حضور مدرس بالفترة
        // =============================================
        public DataTable GetTeacherAttendanceReport(int teacherId, string dateFrom, string dateTo)
        {
            string sql = $@"
                SELECT
                    t.FullName AS 'اسم المدرس',
                    ta.AttendanceDate AS 'التاريخ',
                    ast.StatusName AS 'الحالة',
                    ta.Notes AS 'ملاحظات'
                FROM TeacherAttendance ta
                INNER JOIN Teachers t ON ta.TeacherId = t.Id
                INNER JOIN AttendanceStatus ast ON ta.StatusId = ast.Id
                WHERE ta.TeacherId = {teacherId}
                  AND ta.AttendanceDate BETWEEN '{dateFrom}' AND '{dateTo}'
                ORDER BY ta.AttendanceDate";
            return f.GetData(sql);
        }

        // =============================================
        // 33. تقرير غياب المدرسين
        // =============================================
        public DataTable GetTeacherAbsentReport(string dateFrom, string dateTo)
        {
            string sql = $@"
                SELECT
                    t.FullName AS 'اسم المدرس',
                    t.Phone AS 'رقم الهاتف',
                    COUNT(*) AS 'عدد مرات الغياب',
                    GROUP_CONCAT(ta.AttendanceDate, ', ') AS 'تواريخ الغياب'
                FROM TeacherAttendance ta
                INNER JOIN Teachers t ON ta.TeacherId = t.Id
                WHERE ta.StatusId = 2
                  AND ta.AttendanceDate BETWEEN '{dateFrom}' AND '{dateTo}'
                GROUP BY ta.TeacherId
                ORDER BY COUNT(*) DESC";
            return f.GetData(sql);
        }

        // =============================================
        // 34. التحقق من حضور المدرس في تاريخ معين
        // =============================================
        public bool IsTeacherPresent(int teacherId, string date)
        {
            string statusId = f.GetScalar($@"
                SELECT StatusId FROM TeacherAttendance
                WHERE TeacherId = {teacherId} AND AttendanceDate = '{date}'");

            // إذا لا يوجد سجل أو الحالة = حاضر (1)
            if (string.IsNullOrEmpty(statusId) || statusId == "1")
                return true;

            return false;
        }

        // =============================================================================
        //  قوالب الشهادات (35-39)
        // =============================================================================

        // =============================================
        // 35. حفظ قالب شهادة جديد
        // =============================================
        public int SaveCertificateTemplate(string name, string json, string orientation = "Landscape")
        {
            string sql = $@"
                INSERT INTO CertificateTemplates (TemplateName, DesignJson, PageOrientation)
                VALUES ('{name}', '{json.Replace("'", "''")}', '{orientation}')";

            string result = f.Trans(sql);
            if (result != "DONE...")
                return -1;

            string idStr = f.GetScalar("SELECT last_insert_rowid()");
            return string.IsNullOrEmpty(idStr) ? -1 : int.Parse(idStr);
        }

        // =============================================
        // 36. تحديث قالب شهادة
        // =============================================
        public bool UpdateCertificateTemplate(int id, string name, string json, string orientation = "Landscape")
        {
            string sql = $@"
                UPDATE CertificateTemplates
                SET TemplateName = '{name}',
                    DesignJson = '{json.Replace("'", "''")}',
                    PageOrientation = '{orientation}',
                    UpdatedAt = datetime('now','localtime')
                WHERE Id = {id}";

            string result = f.Trans(sql);
            return result == "DONE...";
        }

        // =============================================
        // 37. جلب جميع القوالب
        // =============================================
        public DataTable GetCertificateTemplates()
        {
            string sql = @"
                SELECT Id, TemplateName, PageOrientation, IsDefault, CreatedAt, UpdatedAt
                FROM CertificateTemplates
                ORDER BY TemplateName";
            return f.GetData(sql);
        }

        // =============================================
        // 38. جلب قالب بالـ Id (مع JSON)
        // =============================================
        public DataTable GetCertificateTemplateById(int id)
        {
            string sql = $@"
                SELECT Id, TemplateName, DesignJson, PageOrientation, IsDefault
                FROM CertificateTemplates
                WHERE Id = {id}";
            return f.GetData(sql);
        }

        // =============================================
        // 39. حذف قالب شهادة
        // =============================================
        public bool DeleteCertificateTemplate(int id)
        {
            string sql = $"DELETE FROM CertificateTemplates WHERE Id = {id}";
            string result = f.Trans(sql);
            return result == "DONE...";
        }

        // =============================================================================
        //  دوال إضافية (40-42)
        // =============================================================================

        // =============================================
        // 40. جلب جميع الطلاب مع الفصول
        // =============================================
        public DataTable GetAllStudentsForCertificate()
        {
            string sql = @"
                SELECT s.Id, s.FullName, c.ClassName, s.ClassId
                FROM Students s
                INNER JOIN Classes c ON s.ClassId = c.Id
                WHERE s.IsActive = 1
                ORDER BY c.ClassName, s.FullName";
            return f.GetData(sql);
        }

        // =============================================
        // 41. جلب طلاب فصل معين (للشهادات)
        // =============================================
        public DataTable GetStudentsByClassForCert(int classId)
        {
            string sql = $@"
                SELECT Id, FullName
                FROM Students
                WHERE ClassId = {classId} AND IsActive = 1
                ORDER BY FullName";
            return f.GetData(sql);
        }

        // =============================================
        // 42. تقرير تأخير المدرسين
        // =============================================
        public DataTable GetTeacherLateReport(string dateFrom, string dateTo)
        {
            string sql = $@"
                SELECT
                    t.FullName AS 'اسم المدرس',
                    t.Phone AS 'رقم الهاتف',
                    COUNT(*) AS 'عدد مرات التأخير',
                    GROUP_CONCAT(ta.AttendanceDate, ', ') AS 'تواريخ التأخير'
                FROM TeacherAttendance ta
                INNER JOIN Teachers t ON ta.TeacherId = t.Id
                WHERE ta.StatusId = 3
                  AND ta.AttendanceDate BETWEEN '{dateFrom}' AND '{dateTo}'
                GROUP BY ta.TeacherId
                ORDER BY COUNT(*) DESC";
            return f.GetData(sql);
        }

        // =============================================
        // 43. ملخص حضور المدرسين (إحصائيات)
        // =============================================
        public DataTable GetTeacherAttendanceSummary(string dateFrom, string dateTo)
        {
            string sql = $@"
                SELECT
                    t.FullName AS 'اسم المدرس',
                    SUM(CASE WHEN ta.StatusId = 1 THEN 1 ELSE 0 END) AS 'حاضر',
                    SUM(CASE WHEN ta.StatusId = 2 THEN 1 ELSE 0 END) AS 'غائب',
                    SUM(CASE WHEN ta.StatusId = 3 THEN 1 ELSE 0 END) AS 'متأخر',
                    SUM(CASE WHEN ta.StatusId = 4 THEN 1 ELSE 0 END) AS 'غياب بعذر',
                    COUNT(*) AS 'إجمالي',
                    ROUND(CAST(SUM(CASE WHEN ta.StatusId = 1 THEN 1 ELSE 0 END) AS FLOAT) / COUNT(*) * 100, 1) AS 'نسبة الحضور %'
                FROM TeacherAttendance ta
                INNER JOIN Teachers t ON ta.TeacherId = t.Id
                WHERE ta.AttendanceDate BETWEEN '{dateFrom}' AND '{dateTo}'
                GROUP BY ta.TeacherId
                ORDER BY t.FullName";
            return f.GetData(sql);
        }

        // =============================================
        // 44. تحضير Grid حضور المدرسين
        // =============================================
        public DataTable PrepareTeacherAttendanceGrid(string date)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TeacherId", typeof(int));
            dt.Columns.Add("TeacherName", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("StatusId", typeof(int));
            dt.Columns.Add("StatusName", typeof(string));
            dt.Columns.Add("Notes", typeof(string));

            // جلب الحضور المحفوظ أو القائمة الافتراضية
            DataTable existing = GetTeacherAttendanceByDate(date);
            if (existing != null)
            {
                foreach (DataRow row in existing.Rows)
                {
                    dt.Rows.Add(
                        Convert.ToInt32(row["TeacherId"]),
                        row["TeacherName"].ToString(),
                        row["Phone"]?.ToString() ?? "",
                        Convert.ToInt32(row["StatusId"]),
                        row["StatusName"].ToString(),
                        row["Notes"]?.ToString() ?? ""
                    );
                }
            }
            return dt;
        }
    }
}
