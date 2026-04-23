using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
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
                WHERE ClassId = {classId} AND IsActive = 0
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

            string idStr = f.GetScalar($@"SELECT max(id) from AttendanceSessions where ClassId = {classId} and  SubjectId = {subjectId} and SessionDate = '{sessionDate}' and TeacherId={teacherId}");
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
            string val = f.GetScalar("SELECT COUNT(*) FROM Students WHERE IsActive = 0");
            return string.IsNullOrEmpty(val) ? 0 : int.Parse(val);
        }

        public int GetTotalTeachers()
        {
            string val = f.GetScalar("SELECT COUNT(*) FROM Teachers WHERE IsActive = 0");
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
            string val = f.GetScalar("SELECT COUNT(*) FROM Classes WHERE IsActive = 0");
            return string.IsNullOrEmpty(val) ? 0 : int.Parse(val);
        }

        public int GetTotalSubjects()
        {
            string val = f.GetScalar("SELECT COUNT(*) FROM Subjects WHERE IsActive = 0");
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
            string sql = "SELECT Id, FullName, Phone FROM Teachers WHERE IsActive = 0 ORDER BY FullName";
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
                WHERE t.IsActive = 0
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
                WHERE s.IsActive = 0
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
                WHERE ClassId = {classId} AND IsActive = 0
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

        // =============================================
        // 45. إضافة اختبار جديد
        // =============================================
        public int CreateExam(int subjectId, int classId, string term, string examDate, double maxScore, string description)
        {
            string sql = @"
                INSERT INTO Exams (SubjectId, ClassId, Term, ExamDate, MaxScore, Description)
                VALUES (@subjectId, @classId, @term, @examDate, @maxScore, @description);";

            var parameters = new Dictionary<string, object>
            {
                { "@subjectId", subjectId },
                { "@classId", classId },
                { "@term", term },
                { "@examDate", examDate },
                { "@maxScore", maxScore },
                { "@description", description ?? "" }
            };

            int rows = f.ExecuteNonQuery(sql, parameters);
            if (rows <= 0) return -1;

            string idStr = f.GetScalar($@"SELECT max(id) from Exams where ClassId = {classId} and  SubjectId = {subjectId}  and term='{term}'");

            return string.IsNullOrEmpty(idStr) ? -1 : int.Parse(idStr);
        }

        // =============================================
        // 46. إدخال/تحديث درجة طالب في اختبار
        // =============================================
        public bool SaveExamResult(int examId, int studentId, double score, string notes = "")
        {
            string sql = @"
                INSERT INTO ExamResults (ExamId, StudentId, Score, Notes)
                VALUES (@examId, @studentId, @score, @notes)
                ON CONFLICT(ExamId, StudentId)
                DO UPDATE SET Score = excluded.Score, Notes = excluded.Notes;";

            var parameters = new Dictionary<string, object>
            {
                { "@examId", examId },
                { "@studentId", studentId },
                { "@score", score },
                { "@notes", notes ?? "" }
            };

            return f.ExecuteNonQuery(sql, parameters) >= 0;
        }

        // =============================================
        // 47. حذف درجة
        // =============================================
        public bool DeleteExamResult(int examId, int studentId)
        {
            string sql = "DELETE FROM ExamResults WHERE ExamId = @examId AND StudentId = @studentId;";
            var parameters = new Dictionary<string, object>
            {
                { "@examId", examId },
                { "@studentId", studentId }
            };
            return f.ExecuteNonQuery(sql, parameters) >= 0;
        }

        // =============================================
        // 48. تقرير كشف الدرجات
        // =============================================
        public DataTable GetGradeReport(int classId, string term, int studentId = 0, int subjectId = 0)
        {
            string sql = @"
                SELECT
                    s.Id AS StudentId,
                    s.FullName AS 'اسم الطالب',
                    sub.SubjectName AS 'المادة',
                    e.Term AS 'الترم',
                    e.ExamDate AS 'تاريخ الاختبار',
                    er.Score AS 'الدرجة',
                    e.MaxScore AS 'الدرجة العظمى',
                    ROUND((er.Score * 100.0) / e.MaxScore, 1) AS 'النسبة',
                    CASE
                        WHEN (er.Score * 100.0 / e.MaxScore) >= 90 THEN 'ممتاز (A)'
                        WHEN (er.Score * 100.0 / e.MaxScore) >= 80 THEN 'جيد جداً (B)'
                        WHEN (er.Score * 100.0 / e.MaxScore) >= 70 THEN 'جيد (C)'
                        WHEN (er.Score * 100.0 / e.MaxScore) >= 60 THEN 'مقبول (D)'
                        ELSE 'راسب (F)'
                    END AS 'التقدير'
                FROM ExamResults er
                INNER JOIN Exams e ON e.Id = er.ExamId
                INNER JOIN Students s ON s.Id = er.StudentId
                INNER JOIN Subjects sub ON sub.Id = e.SubjectId
                WHERE e.ClassId = @classId
                  AND e.Term = @term
                  AND (@studentId = 0 OR s.Id = @studentId)
                  AND (@subjectId = 0 OR sub.Id = @subjectId)
                ORDER BY s.FullName, e.ExamDate;";

            var parameters = new Dictionary<string, object>
            {
                { "@classId", classId },
                { "@term", term ?? "" },
                { "@studentId", studentId },
                { "@subjectId", subjectId }
            };
            return f.GetData(sql, parameters);
        }

        // =============================================
        // 49. ملخص كشف الدرجات
        // =============================================
        public DataTable GetGradeSummary(int classId, string term, int studentId = 0, int subjectId = 0)
        {
            string sql = @"
                SELECT
                    ROUND(AVG((er.Score * 100.0) / e.MaxScore), 1) AS AveragePercent,
                    ROUND(MAX((er.Score * 100.0) / e.MaxScore), 1) AS TopPercent,
                    ROUND(MIN((er.Score * 100.0) / e.MaxScore), 1) AS LowestPercent,
                    SUM(CASE WHEN ((er.Score * 100.0) / e.MaxScore) >= 60 THEN 1 ELSE 0 END) AS PassedCount,
                    SUM(CASE WHEN ((er.Score * 100.0) / e.MaxScore) < 60 THEN 1 ELSE 0 END) AS FailedCount
                FROM ExamResults er
                INNER JOIN Exams e ON e.Id = er.ExamId
                INNER JOIN Students s ON s.Id = er.StudentId
                WHERE e.ClassId = @classId
                  AND e.Term = @term
                  AND (@studentId = 0 OR s.Id = @studentId)
                  AND (@subjectId = 0 OR e.SubjectId = @subjectId);";

            var parameters = new Dictionary<string, object>
            {
                { "@classId", classId },
                { "@term", term ?? "" },
                { "@studentId", studentId },
                { "@subjectId", subjectId }
            };
            return f.GetData(sql, parameters);
        }

        // =============================================
        // 50. كشف درجات شامل (أعمدة ثابتة + آخر سورة)
        // =============================================
        public DataTable GetComprehensiveGradeSheetRaw(int classId, string term, string studentName)
        {
            string sql = @"
                SELECT
                    s.Id AS StudentId,
                    s.FullName AS StudentName,
                    sub.SubjectName AS SubjectName,
                    e.MaxScore AS MaxScore,
                    er.Score AS Score,
                    er.LastSurah AS LastSurah
                FROM Students s
                LEFT JOIN Exams e ON e.ClassId = @classId AND e.Term = @term
                LEFT JOIN ExamResults er ON er.ExamId = e.Id AND er.StudentId = s.Id
                LEFT JOIN Subjects sub ON sub.Id = e.SubjectId
                WHERE s.ClassId = @classId
                  AND s.IsActive = 0
                  AND (@studentName = '' OR s.FullName LIKE '%' || @studentName || '%')
                ORDER BY s.FullName, sub.SubjectName;";

            var parameters = new Dictionary<string, object>
            {
                { "@classId", classId },
                { "@term", term ?? "" },
                { "@studentName", studentName ?? "" }
            };

            return f.GetData(sql, parameters);
        }

        public bool SaveExamResultWithLastSurah(int examId, int studentId, double score, string notes, string lastSurah)
        {
            string sql = @"
                INSERT INTO ExamResults (ExamId, StudentId, Score, Notes, LastSurah)
                VALUES (@examId, @studentId, @score, @notes, @lastSurah)
                ON CONFLICT(ExamId, StudentId)
                DO UPDATE SET
                    Score = excluded.Score,
                    Notes = excluded.Notes,
                    LastSurah = excluded.LastSurah;";

            var parameters = new Dictionary<string, object>
            {
                { "@examId", examId },
                { "@studentId", studentId },
                { "@score", score },
                { "@notes", notes ?? "" },
                { "@lastSurah", lastSurah ?? "" }
            };

            return f.ExecuteNonQuery(sql, parameters) >= 0;
        }

        public int GetExamId(int classId, int subjectId, string term)
        {
            string sql = @"
                SELECT Id
                FROM Exams
                WHERE ClassId = @classId AND SubjectId = @subjectId AND Term = @term
                ORDER BY ExamDate DESC, Id DESC
                LIMIT 1;";

            string id = f.GetScalar(sql, new Dictionary<string, object>
            {
                { "@classId", classId },
                { "@subjectId", subjectId },
                { "@term", term ?? "" }
            });

            return string.IsNullOrWhiteSpace(id) ? 0 : Convert.ToInt32(id);
        }

        public double GetExamMaxScore(int examId)
        {
            string score = f.GetScalar("SELECT MaxScore FROM Exams WHERE Id = @id;", new Dictionary<string, object> { { "@id", examId } });
            return string.IsNullOrWhiteSpace(score) ? 100 : Convert.ToDouble(score);
        }

        public DataTable GetGradeEntryRows(int classId, int subjectId, string term)
        {
            string sql = @"
                SELECT
                    s.Id AS StudentId,
                    s.FullName AS StudentName,
                    er.Id AS ResultId,
                    e.Id AS ExamId,
                    er.Score AS Score,
                    er.LastSurah AS LastSurah,
                    er.Notes AS Notes
                FROM Students s
                LEFT JOIN Exams e
                    ON e.Id = (
                        SELECT Id
                        FROM Exams
                        WHERE ClassId = @classId
                          AND SubjectId = @subjectId
                          AND Term = @term
                        ORDER BY ExamDate DESC, Id DESC
                        LIMIT 1
                    )
                LEFT JOIN ExamResults er
                    ON er.StudentId = s.Id
                   AND er.ExamId = e.Id
                WHERE s.ClassId = @classId
                  AND s.IsActive = 0
                ORDER BY s.FullName;";

            return f.GetData(sql, new Dictionary<string, object>
            {
                { "@classId", classId },
                { "@subjectId", subjectId },
                { "@term", term ?? "" }
            });
        }

        public DataTable GetGradeEntryMatrix(int classId, string term)
        {
            DataTable students = f.GetData(@"
                SELECT Id AS StudentId, FullName AS StudentName
                FROM Students
                WHERE ClassId = @classId AND IsActive = 0
                ORDER BY FullName;",
                new Dictionary<string, object> { { "@classId", classId } });

            DataTable subjects = f.GetData(@"
                SELECT Id AS SubjectId, SubjectName
                FROM Subjects
                WHERE IsActive = 0
                ORDER BY SubjectName;");

            DataTable results = f.GetData(@"
                SELECT er.StudentId, e.SubjectId, er.Score, er.LastSurah
                FROM ExamResults er
                INNER JOIN Exams e ON e.Id = er.ExamId
                WHERE e.ClassId = @classId
                  AND e.Term = @term;",
                new Dictionary<string, object>
                {
                    { "@classId", classId },
                    { "@term", term ?? "" }
                });

            DataTable matrix = new DataTable();
            matrix.Columns.Add("StudentId", typeof(int));
            matrix.Columns.Add("StudentName", typeof(string));
            matrix.Columns.Add("LastSurah", typeof(string));

            var subjectIds = new List<int>();
            if (subjects != null)
            {
                foreach (DataRow subject in subjects.Rows)
                {
                    int subjectId = Convert.ToInt32(subject["SubjectId"]);
                    string subjectName = subject["SubjectName"] == DBNull.Value ? "" : subject["SubjectName"].ToString();
                    string colName = GetSubjectScoreColumnName(subjectId);

                    DataColumn col = matrix.Columns.Add(colName, typeof(double));
                    col.Caption = subjectName;
                    subjectIds.Add(subjectId);
                }
            }

            var resultMap = new Dictionary<string, DataRow>();
            if (results != null)
            {
                foreach (DataRow row in results.Rows)
                {
                    int studentId = Convert.ToInt32(row["StudentId"]);
                    int subjectId = Convert.ToInt32(row["SubjectId"]);
                    resultMap[$"{studentId}_{subjectId}"] = row;
                }
            }

            int preferredLastSurahSubjectId = GetPreferredLastSurahSubjectId(classId, term);

            if (students != null)
            {
                foreach (DataRow student in students.Rows)
                {
                    int studentId = Convert.ToInt32(student["StudentId"]);
                    DataRow newRow = matrix.NewRow();
                    newRow["StudentId"] = studentId;
                    newRow["StudentName"] = student["StudentName"] == DBNull.Value ? "" : student["StudentName"].ToString();
                    newRow["LastSurah"] = "";

                    string fallbackLastSurah = "";
                    foreach (int subjectId in subjectIds)
                    {
                        string key = $"{studentId}_{subjectId}";
                        string colName = GetSubjectScoreColumnName(subjectId);
                        if (!resultMap.ContainsKey(key))
                        {
                            newRow[colName] = DBNull.Value;
                            continue;
                        }

                        DataRow result = resultMap[key];
                        newRow[colName] = result["Score"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDouble(result["Score"]);

                        if (result["LastSurah"] != DBNull.Value)
                        {
                            string surah = result["LastSurah"].ToString();
                            if (!string.IsNullOrWhiteSpace(surah))
                            {
                                if (subjectId == preferredLastSurahSubjectId)
                                    newRow["LastSurah"] = surah;
                                else if (string.IsNullOrWhiteSpace(fallbackLastSurah))
                                    fallbackLastSurah = surah;
                            }
                        }
                    }

                    if (string.IsNullOrWhiteSpace(newRow["LastSurah"] == DBNull.Value ? "" : newRow["LastSurah"].ToString()) &&
                        !string.IsNullOrWhiteSpace(fallbackLastSurah))
                    {
                        newRow["LastSurah"] = fallbackLastSurah;
                    }

                    matrix.Rows.Add(newRow);
                }
            }

            return matrix;
        }

        public int GetPreferredLastSurahSubjectId(int classId, string term)
        {
            string fromSaved = f.GetScalar(@"
                SELECT e.SubjectId
                FROM ExamResults er
                INNER JOIN Exams e ON e.Id = er.ExamId
                WHERE e.ClassId = @classId
                  AND e.Term = @term
                  AND er.LastSurah IS NOT NULL
                  AND TRIM(er.LastSurah) <> ''
                ORDER BY e.ExamDate DESC, e.Id DESC
                LIMIT 1;",
                new Dictionary<string, object>
                {
                    { "@classId", classId },
                    { "@term", term ?? "" }
                });

            if (!string.IsNullOrWhiteSpace(fromSaved))
                return Convert.ToInt32(fromSaved);

            string byName = f.GetScalar(@"
                SELECT Id
                FROM Subjects
                WHERE IsActive = 0
                  AND SubjectName LIKE @namePattern
                ORDER BY Id
                LIMIT 1;",
                new Dictionary<string, object> { { "@namePattern", "%حفظ%" } });

            if (!string.IsNullOrWhiteSpace(byName))
                return Convert.ToInt32(byName);

            string firstSubject = f.GetScalar(@"
                SELECT Id
                FROM Subjects
                WHERE IsActive = 0
                ORDER BY SubjectName, Id
                LIMIT 1;");

            return string.IsNullOrWhiteSpace(firstSubject) ? 0 : Convert.ToInt32(firstSubject);
        }

        public bool SaveGradeEntryMatrix(
            int classId,
            string term,
            DataTable matrixRows,
            Dictionary<string, int> subjectColumns,
            int? lastSurahSubjectId,
            string examDate,
            double maxScore,
            string description,
            out string errorMessage)
        {
            errorMessage = "";
            if (matrixRows == null)
            {
                errorMessage = "لا توجد بيانات للحفظ.";
                return false;
            }

            if (subjectColumns == null || subjectColumns.Count == 0)
            {
                errorMessage = "لا توجد مواد للحفظ.";
                return false;
            }

            using (var cn = new SQLiteConnection(@"Data Source=" + Func.dbname))
            {
                cn.Open();
                using (var timeoutCmd = new SQLiteCommand("PRAGMA busy_timeout = 5000;", cn))
                {
                    timeoutCmd.ExecuteNonQuery();
                }
                using (var tx = cn.BeginTransaction())
                {
                    try
                    {
                        var examIdsBySubject = new Dictionary<int, int>();
                        foreach (int subjectId in subjectColumns.Values.Distinct())
                        {
                            int examId = GetExamId(classId, subjectId, term, cn, tx);
                            if (examId <= 0)
                            {
                                examId = CreateExam(subjectId, classId, term, examDate, maxScore, description, cn, tx);
                            }

                            if (examId <= 0)
                            {
                                errorMessage = "تعذر إنشاء اختبار لإحدى المواد.";
                                tx.Rollback();
                                return false;
                            }

                            examIdsBySubject[subjectId] = examId;
                        }

                        foreach (DataRow row in matrixRows.Rows)
                        {
                            int studentId = Convert.ToInt32(row["StudentId"]);
                            string lastSurah = row["LastSurah"] == DBNull.Value ? "" : row["LastSurah"].ToString().Trim();

                            foreach (var entry in subjectColumns)
                            {
                                string colName = entry.Key;
                                int subjectId = entry.Value;
                                int examId = examIdsBySubject[subjectId];
                                object scoreObj = row[colName];
                                bool scoreEmpty = scoreObj == DBNull.Value || string.IsNullOrWhiteSpace(scoreObj.ToString());

                                bool assignLastSurah = lastSurahSubjectId.HasValue &&
                                                       lastSurahSubjectId.Value > 0 &&
                                                       subjectId == lastSurahSubjectId.Value;
                                bool lastSurahEmpty = string.IsNullOrWhiteSpace(lastSurah);

                                if (scoreEmpty && (!assignLastSurah || lastSurahEmpty))
                                    continue;

                                using (var cmd = new SQLiteCommand(@"
                                    INSERT INTO ExamResults (ExamId, StudentId, Score, LastSurah, Notes)
                                    VALUES (@examId, @studentId, @score, @lastSurah, @notes)
                                    ON CONFLICT(ExamId, StudentId)
                                    DO UPDATE SET
                                        Score = excluded.Score,
                                        LastSurah = excluded.LastSurah,
                                        Notes = excluded.Notes;", cn, tx))
                                {
                                    cmd.Parameters.AddWithValue("@examId", examId);
                                    cmd.Parameters.AddWithValue("@studentId", studentId);
                                    cmd.Parameters.AddWithValue("@score", scoreEmpty ? (object)DBNull.Value : Convert.ToDouble(scoreObj));
                                    cmd.Parameters.AddWithValue("@lastSurah",
                                        assignLastSurah && !lastSurahEmpty ? (object)lastSurah : DBNull.Value);
                                    cmd.Parameters.AddWithValue("@notes", DBNull.Value);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        tx.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        errorMessage = ex.Message;
                        return false;
                    }
                }
            }
        }

        private int GetExamId(int classId, int subjectId, string term, SQLiteConnection cn, SQLiteTransaction tx)
        {
            using (var cmd = new SQLiteCommand(@"
                SELECT Id
                FROM Exams
                WHERE ClassId = @classId AND SubjectId = @subjectId AND Term = @term
                ORDER BY ExamDate DESC, Id DESC
                LIMIT 1;", cn, tx))
            {
                cmd.Parameters.AddWithValue("@classId", classId);
                cmd.Parameters.AddWithValue("@subjectId", subjectId);
                cmd.Parameters.AddWithValue("@term", term ?? "");

                object value = cmd.ExecuteScalar();
                if (value == null || value == DBNull.Value) return 0;
                return Convert.ToInt32(value);
            }
        }

        private int CreateExam(
            int subjectId,
            int classId,
            string term,
            string examDate,
            double maxScore,
            string description,
            SQLiteConnection cn,
            SQLiteTransaction tx)
        {
            using (var insertCmd = new SQLiteCommand(@"
                INSERT INTO Exams (SubjectId, ClassId, Term, ExamDate, MaxScore, Description)
                VALUES (@subjectId, @classId, @term, @examDate, @maxScore, @description);", cn, tx))
            {
                insertCmd.Parameters.AddWithValue("@subjectId", subjectId);
                insertCmd.Parameters.AddWithValue("@classId", classId);
                insertCmd.Parameters.AddWithValue("@term", term ?? "");
                insertCmd.Parameters.AddWithValue("@examDate", examDate ?? "");
                insertCmd.Parameters.AddWithValue("@maxScore", maxScore);
                insertCmd.Parameters.AddWithValue("@description", description ?? "");

                int rows = insertCmd.ExecuteNonQuery();
                if (rows <= 0) return -1;
            }

            // last_insert_rowid is connection-scoped and safe inside this transaction.
            using (var idCmd = new SQLiteCommand("SELECT last_insert_rowid();", cn, tx))
            {
                object id = idCmd.ExecuteScalar();
                if (id == null || id == DBNull.Value) return -1;
                return Convert.ToInt32(id);
            }
        }

        public string GetSubjectScoreColumnName(int subjectId)
        {
            return "Sub_" + subjectId;
        }

        public bool SaveGradeEntryUpsert(int examId, DataTable gradeRows, out string errorMessage)
        {
            errorMessage = "";
            if (examId <= 0)
            {
                errorMessage = "ExamId غير صالح.";
                return false;
            }

            using (var cn = new SQLiteConnection(@"Data Source=" + Func.dbname))
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    try
                    {
                        foreach (DataRow row in gradeRows.Rows)
                        {
                            int studentId = Convert.ToInt32(row["StudentId"]);
                            object resultIdObj = row["ResultId"];
                            object scoreObj = row["Score"];
                            string lastSurah = row["LastSurah"] == DBNull.Value ? "" : row["LastSurah"].ToString();
                            string notes = row["Notes"] == DBNull.Value ? "" : row["Notes"].ToString();

                            bool scoreEmpty = scoreObj == DBNull.Value || string.IsNullOrWhiteSpace(scoreObj.ToString());
                            bool lastSurahEmpty = string.IsNullOrWhiteSpace(lastSurah);
                            bool notesEmpty = string.IsNullOrWhiteSpace(notes);
                            if (scoreEmpty && lastSurahEmpty && notesEmpty)
                                continue;

                            if (resultIdObj != DBNull.Value && !string.IsNullOrWhiteSpace(resultIdObj.ToString()))
                            {
                                using (var cmd = new SQLiteCommand(@"
                                    UPDATE ExamResults
                                    SET Score = @score,
                                        LastSurah = @lastSurah,
                                        Notes = @notes
                                    WHERE Id = @resultId;", cn, tx))
                                {
                                    cmd.Parameters.AddWithValue("@score", scoreEmpty ? (object)DBNull.Value : Convert.ToDouble(scoreObj));
                                    cmd.Parameters.AddWithValue("@lastSurah", string.IsNullOrWhiteSpace(lastSurah) ? (object)DBNull.Value : lastSurah);
                                    cmd.Parameters.AddWithValue("@notes", string.IsNullOrWhiteSpace(notes) ? (object)DBNull.Value : notes);
                                    cmd.Parameters.AddWithValue("@resultId", Convert.ToInt32(resultIdObj));
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                using (var cmd = new SQLiteCommand(@"
                                    INSERT INTO ExamResults (ExamId, StudentId, Score, LastSurah, Notes)
                                    VALUES (@examId, @studentId, @score, @lastSurah, @notes)
                                    ON CONFLICT(ExamId, StudentId)
                                    DO UPDATE SET
                                        Score = excluded.Score,
                                        LastSurah = excluded.LastSurah,
                                        Notes = excluded.Notes;", cn, tx))
                                {
                                    cmd.Parameters.AddWithValue("@examId", examId);
                                    cmd.Parameters.AddWithValue("@studentId", studentId);
                                    cmd.Parameters.AddWithValue("@score", scoreEmpty ? (object)DBNull.Value : Convert.ToDouble(scoreObj));
                                    cmd.Parameters.AddWithValue("@lastSurah", string.IsNullOrWhiteSpace(lastSurah) ? (object)DBNull.Value : lastSurah);
                                    cmd.Parameters.AddWithValue("@notes", string.IsNullOrWhiteSpace(notes) ? (object)DBNull.Value : notes);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        tx.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        errorMessage = ex.Message;
                        return false;
                    }
                }
            }
        }
    }
}
