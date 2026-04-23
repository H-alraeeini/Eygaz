using System;
using System.Data;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class FrmAttendanceSessions : MetroFramework.Forms.MetroForm
    {
        Func f = new Func();
        AttendanceHelper helper = new AttendanceHelper();

        public FrmAttendanceSessions()
        {
            InitializeComponent();
        }

        // =============================================
        // تحميل الشاشة
        // =============================================
        private void FrmAttendanceSessions_Load(object sender, EventArgs e)
        {
            try
            {
                // تعبئة الفلاتر
                f.DataComboWithNull(CmbClass, "Classes", "ClassName", "Id", " WHERE IsActive <> 1 ORDER BY ClassName");
                f.DataComboWithNull(CmbSubject, "Subjects", "SubjectName", "Id", " WHERE IsActive <> 1 ORDER BY SubjectName");
                f.DataComboWithNull(CmbTeacher, "Teachers", "FullName", "Id", " WHERE IsActive = <> ORDER BY FullName");

                // تعيين فترة افتراضية (الشهر الحالي)
                DtFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DtTo.Value = DateTime.Today;

                // تحميل جميع الجلسات
                LoadSessions();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء تحميل البيانات: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // تحميل الجلسات
        // =============================================
        private void LoadSessions()
        {
            int classId = CmbClass.SelectedValue != null ? Convert.ToInt32(CmbClass.SelectedValue) : 0;
            int subjectId = CmbSubject.SelectedValue != null ? Convert.ToInt32(CmbSubject.SelectedValue) : 0;
            int teacherId = CmbTeacher.SelectedValue != null ? Convert.ToInt32(CmbTeacher.SelectedValue) : 0;
            string dateFrom = DtFrom.Value.ToString("yyyy-MM-dd");
            string dateTo = DtTo.Value.ToString("yyyy-MM-dd");

            DataTable dt = helper.GetSessions(classId, subjectId, teacherId, dateFrom, dateTo);
            GVSessions.DataSource = dt;
            SetupGridColumns();
        }

        private void SetupGridColumns()
        {
            if (GrdSessions.Columns.Count == 0) return;

            if (GrdSessions.Columns.ColumnByFieldName("SessionId") != null)
            {
                GrdSessions.Columns["SessionId"].Caption = "رقم الجلسة";
                GrdSessions.Columns["SessionId"].Width = 80;
            }
            if (GrdSessions.Columns.ColumnByFieldName("SessionDate") != null)
            {
                GrdSessions.Columns["SessionDate"].Caption = "التاريخ";
                GrdSessions.Columns["SessionDate"].Width = 100;
            }
            if (GrdSessions.Columns.ColumnByFieldName("ClassName") != null)
            {
                GrdSessions.Columns["ClassName"].Caption = "الفصل";
                GrdSessions.Columns["ClassName"].Width = 120;
            }
            if (GrdSessions.Columns.ColumnByFieldName("SubjectName") != null)
            {
                GrdSessions.Columns["SubjectName"].Caption = "المادة";
                GrdSessions.Columns["SubjectName"].Width = 120;
            }
            if (GrdSessions.Columns.ColumnByFieldName("TeacherName") != null)
            {
                GrdSessions.Columns["TeacherName"].Caption = "المدرس";
                GrdSessions.Columns["TeacherName"].Width = 130;
            }
            if (GrdSessions.Columns.ColumnByFieldName("StudentsCount") != null)
            {
                GrdSessions.Columns["StudentsCount"].Caption = "عدد الطلاب";
                GrdSessions.Columns["StudentsCount"].Width = 80;
            }
            if (GrdSessions.Columns.ColumnByFieldName("Notes") != null)
            {
                GrdSessions.Columns["Notes"].Caption = "ملاحظات";
            }

            // RTL
            if (Func.vRtL)
            {
                int maxCol = GrdSessions.Columns.Count;
                for (int i = 0; i < maxCol; i++)
                    GrdSessions.Columns[i].VisibleIndex = maxCol - i - 1;
            }
        }

        // =============================================
        // بحث
        // =============================================
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            LoadSessions();
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            CmbClass.SelectedIndex = -1;
            CmbSubject.SelectedIndex = -1;
            CmbTeacher.SelectedIndex = -1;
            DtFrom.Value = new DateTime(2020, 1, 1);
            DtTo.Value = DateTime.Today;
            LoadSessions();
        }

        // =============================================
        // جلب رقم الجلسة المحددة
        // =============================================
        private int GetSelectedSessionId()
        {
            if (GrdSessions.RowCount == 0 || GrdSessions.FocusedRowHandle < 0) return -1;
            var row = GrdSessions.GetDataRow(GrdSessions.FocusedRowHandle);
            if (row == null) return -1;
            return Convert.ToInt32(row["SessionId"]);
        }

        // =============================================
        // فتح الجلسة في شاشة الحضور
        // =============================================
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                int sessionId = GetSelectedSessionId();
                if (sessionId <= 0)
                {
                    MessageBox.Show("يرجى اختيار جلسة من القائمة", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                FrmAttendanceStud frm = new FrmAttendanceStud(sessionId);
                frm.MdiParent = this.MdiParent;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // تعديل الجلسة
        // =============================================
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int sessionId = GetSelectedSessionId();
                if (sessionId <= 0)
                {
                    MessageBox.Show("يرجى اختيار جلسة من القائمة", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // فتح شاشة الحضور مع الجلسة المحددة
                FrmAttendanceStud frm = new FrmAttendanceStud(sessionId);
                frm.MdiParent = this.MdiParent;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // حذف الجلسة
        // =============================================
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int sessionId = GetSelectedSessionId();
                if (sessionId <= 0)
                {
                    MessageBox.Show("يرجى اختيار جلسة من القائمة", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "هل أنت متأكد من حذف هذه الجلسة وجميع سجلات الحضور المرتبطة بها؟",
                    "تأكيد الحذف",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    if (helper.DeleteSession(sessionId))
                    {
                        MessageBox.Show("تم حذف الجلسة بنجاح", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadSessions();
                    }
                    else
                    {
                        MessageBox.Show("حدث خطأ أثناء الحذف", "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // طباعة تقرير الجلسة
        // =============================================
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int sessionId = GetSelectedSessionId();
                if (sessionId <= 0)
                {
                    MessageBox.Show("يرجى اختيار جلسة من القائمة", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // جلب بيانات الجلسة وعرضها في رسالة (يمكن تطويرها لاحقاً)
                DataTable details = helper.GetSessionDetails(sessionId);
                DataTable attendance = helper.GetSessionAttendance(sessionId);

                if (details != null && details.Rows.Count > 0 && attendance != null)
                {
                    var dr = details.Rows[0];
                    string report = $"تقرير جلسة الحضور رقم: {sessionId}\n";
                    report += $"━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
                    report += $"الفصل: {dr["ClassName"]}\n";
                    report += $"المادة: {dr["SubjectName"]}\n";
                    report += $"المدرس: {dr["TeacherName"]}\n";
                    report += $"التاريخ: {dr["SessionDate"]}\n";
                    report += $"━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
                    report += $"عدد الطلاب: {attendance.Rows.Count}\n\n";

                    int present = 0, absent = 0, late = 0, excused = 0;
                    foreach (DataRow row in attendance.Rows)
                    {
                        int statusId = Convert.ToInt32(row["StatusId"]);
                        if (statusId == 1) present++;
                        else if (statusId == 2) absent++;
                        else if (statusId == 3) late++;
                        else if (statusId == 4) excused++;
                    }

                    report += $"حاضر: {present} | غائب: {absent} | متأخر: {late} | غياب بعذر: {excused}\n";
                    report += $"━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n";

                    foreach (DataRow row in attendance.Rows)
                    {
                        report += $"  {row["StudentName"]}  →  {row["StatusName"]}\n";
                    }

                    MessageBox.Show(report, "تقرير الجلسة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
