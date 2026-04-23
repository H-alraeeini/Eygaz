using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;

namespace Eygaz
{
    public partial class FrmAttendanceStud : MetroFramework.Forms.MetroForm
    {
        Func f = new Func();
        AttendanceHelper attendanceHelper = new AttendanceHelper();
        private int currentSessionId = -1;
        private DataTable dtAttendance;
        private int openSessionId = -1; // لفتح جلسة محددة

        // =============================================
        // Constructors
        // =============================================
        public FrmAttendanceStud()
        {
            InitializeComponent();
        }

        /// <summary>
        /// فتح الشاشة مع تحميل جلسة محددة
        /// </summary>
        public FrmAttendanceStud(int sessionId)
        {
            InitializeComponent();
            openSessionId = sessionId;
        }

        // =============================================
        // تحميل الشاشة
        // =============================================
        private void FrmAttendanceStud_Load(object sender, EventArgs e)
        {
            try
            {
                f.DataCombo(ClassId, "Classes", "ClassName", "Id", " WHERE IsActive = 0 ORDER BY ClassName");
                f.DataCombo(SubjectId, "Subjects", "SubjectName", "Id", " WHERE IsActive = 0 ORDER BY SubjectName");
                f.DataCombo(TeacherId, "Teachers", "FullName", "Id", " WHERE IsActive = 0 ORDER BY FullName");

                AttendDate.Value = DateTime.Today;

                GrdAttendStud.OptionsBehavior.Editable = true;
                GrdAttendStud.RowHeight = 28;

                BtnSave.Enabled = false;
                BtnMarkAllPresent.Enabled = false;
                BtnMarkAllAbsent.Enabled = false;

                // إذا تم فتح الشاشة بجلسة محددة
                if (openSessionId > 0)
                {
                    LoadExistingSession(openSessionId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء تحميل البيانات: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // تحميل جلسة موجودة (من شاشة إدارة الجلسات)
        // =============================================
        private void LoadExistingSession(int sessionId)
        {
            try
            {
                DataTable details = attendanceHelper.GetSessionDetails(sessionId);
                if (details == null || details.Rows.Count == 0)
                {
                    MessageBox.Show("لم يتم العثور على الجلسة", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var dr = details.Rows[0];
                ClassId.SelectedValue = dr["ClassId"].ToString();
                SubjectId.SelectedValue = dr["SubjectId"].ToString();
                TeacherId.SelectedValue = dr["TeacherId"].ToString();
                AttendDate.Value = Convert.ToDateTime(dr["SessionDate"]);

                currentSessionId = sessionId;
                int classId = Convert.ToInt32(dr["ClassId"]);
                dtAttendance = attendanceHelper.PrepareAttendanceGrid(classId, currentSessionId);

                GVAttendStud.DataSource = dtAttendance;
                SetupGridColumns();

                BtnSave.Enabled = true;
                BtnMarkAllPresent.Enabled = true;
                BtnMarkAllAbsent.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء تحميل الجلسة: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // زر تحميل الطلاب
        // =============================================
        private void BtnLoadStudents_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClassId.SelectedValue == null || ClassId.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("يجب اختيار الفصل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClassId.Focus();
                    return;
                }
                if (SubjectId.SelectedValue == null || SubjectId.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("يجب اختيار المادة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SubjectId.Focus();
                    return;
                }
                if (TeacherId.SelectedValue == null || TeacherId.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("يجب اختيار المدرس", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TeacherId.Focus();
                    return;
                }

                int classId = Convert.ToInt32(ClassId.SelectedValue);
                int subjectId = Convert.ToInt32(SubjectId.SelectedValue);
                string sessionDate = AttendDate.Value.ToString("yyyy-MM-dd");

                string existingSessionId = attendanceHelper.FindExistingSession(classId, subjectId, sessionDate);

                if (!string.IsNullOrEmpty(existingSessionId))
                {
                    currentSessionId = int.Parse(existingSessionId);
                    dtAttendance = attendanceHelper.PrepareAttendanceGrid(classId, currentSessionId);
                    MessageBox.Show("تم العثور على جلسة حضور محفوظة مسبقاً. يمكنك تعديل البيانات وحفظها.",
                        "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    currentSessionId = -1;
                    dtAttendance = attendanceHelper.PrepareAttendanceGrid(classId, -1);

                    if (dtAttendance == null || dtAttendance.Rows.Count == 0)
                    {
                        MessageBox.Show("لا يوجد طلاب في هذا الفصل", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        BtnSave.Enabled = false;
                        BtnMarkAllPresent.Enabled = false;
                        BtnMarkAllAbsent.Enabled = false;
                        return;
                    }
                }

                GVAttendStud.DataSource = dtAttendance;
                SetupGridColumns();

                BtnSave.Enabled = true;
                BtnMarkAllPresent.Enabled = true;
                BtnMarkAllAbsent.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء تحميل الطلاب: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // إعداد أعمدة الـ Grid
        // =============================================
        private void SetupGridColumns()
        {
            if (GrdAttendStud.Columns.Count == 0) return;

            for (int i = 0; i < GrdAttendStud.Columns.Count; i++)
                GrdAttendStud.Columns[i].Visible = false;

            if (GrdAttendStud.Columns.ColumnByFieldName("StudentId") != null)
                GrdAttendStud.Columns["StudentId"].Visible = false;

            if (GrdAttendStud.Columns.ColumnByFieldName("StudentName") != null)
            {
                var colName = GrdAttendStud.Columns["StudentName"];
                colName.Visible = true;
                colName.VisibleIndex = 0;
                colName.Caption = "اسم الطالب";
                colName.OptionsColumn.AllowEdit = false;
                colName.Width = 200;
            }

            if (GrdAttendStud.Columns.ColumnByFieldName("StatusId") != null)
            {
                var colStatus = GrdAttendStud.Columns["StatusId"];
                colStatus.Visible = true;
                colStatus.VisibleIndex = 1;
                colStatus.Caption = "الحالة";
                colStatus.OptionsColumn.AllowEdit = true;
                colStatus.Width = 120;

                RepositoryItemLookUpEdit repoCombo = new RepositoryItemLookUpEdit();
                DataTable statusData = attendanceHelper.GetAttendanceStatuses();
                repoCombo.DataSource = statusData;
                repoCombo.ValueMember = "Id";
                repoCombo.DisplayMember = "StatusName";
                repoCombo.NullText = "حاضر";
                repoCombo.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("StatusName", "الحالة"));
                repoCombo.ShowHeader = false;

                GVAttendStud.RepositoryItems.Add(repoCombo);
                colStatus.ColumnEdit = repoCombo;
            }

            if (GrdAttendStud.Columns.ColumnByFieldName("StatusName") != null)
                GrdAttendStud.Columns["StatusName"].Visible = false;

            if (GrdAttendStud.Columns.ColumnByFieldName("Notes") != null)
            {
                var colNotes = GrdAttendStud.Columns["Notes"];
                colNotes.Visible = true;
                colNotes.VisibleIndex = 2;
                colNotes.Caption = "ملاحظات";
                colNotes.OptionsColumn.AllowEdit = true;
                colNotes.Width = 200;
            }

            if (Func.vRtL)
            {
                int maxCol = GrdAttendStud.VisibleColumns.Count;
                for (int i = 0; i < GrdAttendStud.Columns.Count; i++)
                {
                    if (GrdAttendStud.Columns[i].Visible)
                        GrdAttendStud.Columns[i].VisibleIndex = maxCol - GrdAttendStud.Columns[i].VisibleIndex - 1;
                }
            }
        }

        // =============================================
        // زر حفظ الحضور
        // =============================================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtAttendance == null || dtAttendance.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات للحفظ", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                GrdAttendStud.CloseEditor();
                GrdAttendStud.UpdateCurrentRow();

                int classId = Convert.ToInt32(ClassId.SelectedValue);
                int subjectId = Convert.ToInt32(SubjectId.SelectedValue);
                int teacherId = Convert.ToInt32(TeacherId.SelectedValue);
                string sessionDate = AttendDate.Value.ToString("yyyy-MM-dd");

                if (currentSessionId <= 0)
                {
                    currentSessionId = attendanceHelper.CreateSession(classId, subjectId, teacherId, sessionDate);
                    if (currentSessionId <= 0)
                    {
                        //MessageBox.Show("حدث خطأ أثناء إنشاء جلسة الحضور", "خطأ",
                        //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //return;
                    }
                }

                bool success = attendanceHelper.SaveBulkAttendance(currentSessionId, dtAttendance);

                if (success)
                    MessageBox.Show("تم حفظ الحضور بنجاح", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("حدث خطأ أثناء حفظ بعض سجلات الحضور", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء الحفظ: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // تحديد الكل حاضر
        // =============================================
        private void BtnMarkAllPresent_Click(object sender, EventArgs e)
        {
            MarkAll(1); // 1 = حاضر
        }

        // =============================================
        // تحديد الكل غائب
        // =============================================
        private void BtnMarkAllAbsent_Click(object sender, EventArgs e)
        {
            MarkAll(2); // 2 = غائب
        }

        // =============================================
        // دالة مشتركة لتحديد حالة جميع الطلاب
        // =============================================
        private void MarkAll(int statusId)
        {
            if (dtAttendance == null || dtAttendance.Rows.Count == 0) return;

            GrdAttendStud.CloseEditor();

            string statusName = "";
            switch (statusId)
            {
                case 1: statusName = "حاضر"; break;
                case 2: statusName = "غائب"; break;
                case 3: statusName = "متأخر"; break;
                case 4: statusName = "غياب بعذر"; break;
            }

            foreach (DataRow row in dtAttendance.Rows)
            {
                row["StatusId"] = statusId;
                row["StatusName"] = statusName;
            }

            GrdAttendStud.RefreshData();
        }

        // =============================================
        // اختصارات لوحة المفاتيح
        // =============================================
        private void FrmAttendanceStud_KeyDown(object sender, KeyEventArgs e)
        {
            if (dtAttendance == null || dtAttendance.Rows.Count == 0) return;

            // F5 = الكل حاضر
            if (e.KeyCode == Keys.F5)
            {
                MarkAll(1);
                e.Handled = true;
                return;
            }

            // F6 = الكل غائب
            if (e.KeyCode == Keys.F6)
            {
                MarkAll(2);
                e.Handled = true;
                return;
            }

            // اختصارات للطالب الحالي
            int rowHandle = GrdAttendStud.FocusedRowHandle;
            if (rowHandle < 0 || rowHandle >= dtAttendance.Rows.Count) return;

            int newStatusId = -1;
            switch (e.KeyCode)
            {
                case Keys.P: newStatusId = 1; break; // حاضر
                case Keys.A: newStatusId = 2; break; // غائب
                case Keys.L: newStatusId = 3; break; // متأخر
                case Keys.E: newStatusId = 4; break; // غياب بعذر
            }

            if (newStatusId > 0)
            {
                GrdAttendStud.CloseEditor();
                string statusName = "";
                switch (newStatusId)
                {
                    case 1: statusName = "حاضر"; break;
                    case 2: statusName = "غائب"; break;
                    case 3: statusName = "متأخر"; break;
                    case 4: statusName = "غياب بعذر"; break;
                }

                dtAttendance.Rows[rowHandle]["StatusId"] = newStatusId;
                dtAttendance.Rows[rowHandle]["StatusName"] = statusName;
                GrdAttendStud.RefreshData();

                // الانتقال للسطر التالي
                if (rowHandle < dtAttendance.Rows.Count - 1)
                    GrdAttendStud.FocusedRowHandle = rowHandle + 1;

                e.Handled = true;
            }
        }
    }
}
