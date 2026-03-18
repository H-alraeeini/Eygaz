using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;

namespace Eygaz
{
    public partial class FrmTeacherAttendance : MetroFramework.Forms.MetroForm
    {
        Func f = new Func();
        AttendanceHelper helper = new AttendanceHelper();
        private DataTable dtTeacherAttendance;

        public FrmTeacherAttendance()
        {
            InitializeComponent();
        }

        // =============================================
        // تحميل الشاشة
        // =============================================
        private void FrmTeacherAttendance_Load(object sender, EventArgs e)
        {
            try
            {
                AttendDate.Value = DateTime.Today;

                GrdTeacherAttend.OptionsBehavior.Editable = true;
                GrdTeacherAttend.RowHeight = 28;

                BtnSave.Enabled = false;
                BtnMarkAllPresent.Enabled = false;
                BtnMarkAllAbsent.Enabled = false;
                BtnSendWhatsApp.Enabled = false;

                // تحميل تلقائي لتاريخ اليوم
                LoadTeachers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء تحميل البيانات: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // تحميل المدرسين
        // =============================================
        private void BtnLoadTeachers_Click(object sender, EventArgs e)
        {
            LoadTeachers();
        }

        private void LoadTeachers()
        {
            try
            {
                string date = AttendDate.Value.ToString("yyyy-MM-dd");
                dtTeacherAttendance = helper.PrepareTeacherAttendanceGrid(date);

                if (dtTeacherAttendance == null || dtTeacherAttendance.Rows.Count == 0)
                {
                    MessageBox.Show("لا يوجد مدرسين نشطين في النظام", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    BtnSave.Enabled = false;
                    BtnMarkAllPresent.Enabled = false;
                    BtnMarkAllAbsent.Enabled = false;
                    BtnSendWhatsApp.Enabled = false;
                    return;
                }

                GVTeacherAttend.DataSource = dtTeacherAttendance;
                SetupGridColumns();

                BtnSave.Enabled = true;
                BtnMarkAllPresent.Enabled = true;
                BtnMarkAllAbsent.Enabled = true;
                BtnSendWhatsApp.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء تحميل المدرسين: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // إعداد أعمدة الـ Grid
        // =============================================
        private void SetupGridColumns()
        {
            if (GrdTeacherAttend.Columns.Count == 0) return;

            for (int i = 0; i < GrdTeacherAttend.Columns.Count; i++)
                GrdTeacherAttend.Columns[i].Visible = false;

            // إخفاء TeacherId و Phone
            if (GrdTeacherAttend.Columns.ColumnByFieldName("TeacherId") != null)
                GrdTeacherAttend.Columns["TeacherId"].Visible = false;

            if (GrdTeacherAttend.Columns.ColumnByFieldName("Phone") != null)
                GrdTeacherAttend.Columns["Phone"].Visible = false;

            // اسم المدرس
            if (GrdTeacherAttend.Columns.ColumnByFieldName("TeacherName") != null)
            {
                var colName = GrdTeacherAttend.Columns["TeacherName"];
                colName.Visible = true;
                colName.VisibleIndex = 0;
                colName.Caption = "اسم المدرس";
                colName.OptionsColumn.AllowEdit = false;
                colName.Width = 200;
            }

            // حالة الحضور
            if (GrdTeacherAttend.Columns.ColumnByFieldName("StatusId") != null)
            {
                var colStatus = GrdTeacherAttend.Columns["StatusId"];
                colStatus.Visible = true;
                colStatus.VisibleIndex = 1;
                colStatus.Caption = "الحالة";
                colStatus.OptionsColumn.AllowEdit = true;
                colStatus.Width = 120;

                RepositoryItemLookUpEdit repoCombo = new RepositoryItemLookUpEdit();
                DataTable statusData = helper.GetAttendanceStatuses();
                repoCombo.DataSource = statusData;
                repoCombo.ValueMember = "Id";
                repoCombo.DisplayMember = "StatusName";
                repoCombo.NullText = "حاضر";
                repoCombo.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("StatusName", "الحالة"));
                repoCombo.ShowHeader = false;

                GVTeacherAttend.RepositoryItems.Add(repoCombo);
                colStatus.ColumnEdit = repoCombo;
            }

            // إخفاء StatusName
            if (GrdTeacherAttend.Columns.ColumnByFieldName("StatusName") != null)
                GrdTeacherAttend.Columns["StatusName"].Visible = false;

            // ملاحظات
            if (GrdTeacherAttend.Columns.ColumnByFieldName("Notes") != null)
            {
                var colNotes = GrdTeacherAttend.Columns["Notes"];
                colNotes.Visible = true;
                colNotes.VisibleIndex = 2;
                colNotes.Caption = "ملاحظات";
                colNotes.OptionsColumn.AllowEdit = true;
                colNotes.Width = 200;
            }

            // RTL support
            if (Func.vRtL)
            {
                int maxCol = GrdTeacherAttend.VisibleColumns.Count;
                for (int i = 0; i < GrdTeacherAttend.Columns.Count; i++)
                {
                    if (GrdTeacherAttend.Columns[i].Visible)
                        GrdTeacherAttend.Columns[i].VisibleIndex = maxCol - GrdTeacherAttend.Columns[i].VisibleIndex - 1;
                }
            }
        }

        // =============================================
        // حفظ الحضور
        // =============================================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtTeacherAttendance == null || dtTeacherAttendance.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات للحفظ", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                GrdTeacherAttend.CloseEditor();
                GrdTeacherAttend.UpdateCurrentRow();

                string date = AttendDate.Value.ToString("yyyy-MM-dd");
                bool success = helper.SaveBulkTeacherAttendance(date, dtTeacherAttendance);

                if (success)
                    MessageBox.Show("تم حفظ حضور المدرسين بنجاح", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("حدث خطأ أثناء حفظ بعض السجلات", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        // دالة مشتركة لتحديد حالة جميع المدرسين
        // =============================================
        private void MarkAll(int statusId)
        {
            if (dtTeacherAttendance == null || dtTeacherAttendance.Rows.Count == 0) return;

            GrdTeacherAttend.CloseEditor();

            string statusName = "";
            switch (statusId)
            {
                case 1: statusName = "حاضر"; break;
                case 2: statusName = "غائب"; break;
                case 3: statusName = "متأخر"; break;
                case 4: statusName = "غياب بعذر"; break;
            }

            foreach (DataRow row in dtTeacherAttendance.Rows)
            {
                row["StatusId"] = statusId;
                row["StatusName"] = statusName;
            }

            GrdTeacherAttend.RefreshData();
        }

        // =============================================
        // إرسال إشعارات واتساب للغائبين والمتأخرين
        // =============================================
        private void BtnSendWhatsApp_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtTeacherAttendance == null || dtTeacherAttendance.Rows.Count == 0) return;

                GrdTeacherAttend.CloseEditor();
                GrdTeacherAttend.UpdateCurrentRow();

                string date = AttendDate.Value.ToString("yyyy-MM-dd");
                int sentCount = 0;

                foreach (DataRow row in dtTeacherAttendance.Rows)
                {
                    int statusId = Convert.ToInt32(row["StatusId"]);
                    string phone = row["Phone"]?.ToString() ?? "";
                    string teacherName = row["TeacherName"].ToString();

                    if (string.IsNullOrEmpty(phone)) continue;

                    if (statusId == 2) // غائب
                    {
                        WhatsAppHelper.SendTeacherAbsenceNotification(teacherName, phone, date);
                        sentCount++;
                    }
                    else if (statusId == 3) // متأخر
                    {
                        WhatsAppHelper.SendTeacherLateNotification(teacherName, phone, date);
                        sentCount++;
                    }
                }

                if (sentCount > 0)
                    MessageBox.Show($"تم فتح واتساب لإرسال {sentCount} إشعار(ات)", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("لا يوجد مدرسين غائبين أو متأخرين لإرسال إشعارات لهم", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // اختصارات لوحة المفاتيح
        // =============================================
        private void FrmTeacherAttendance_KeyDown(object sender, KeyEventArgs e)
        {
            if (dtTeacherAttendance == null || dtTeacherAttendance.Rows.Count == 0) return;

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

            // اختصارات للمدرس الحالي
            int rowHandle = GrdTeacherAttend.FocusedRowHandle;
            if (rowHandle < 0 || rowHandle >= dtTeacherAttendance.Rows.Count) return;

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
                GrdTeacherAttend.CloseEditor();
                string statusName = "";
                switch (newStatusId)
                {
                    case 1: statusName = "حاضر"; break;
                    case 2: statusName = "غائب"; break;
                    case 3: statusName = "متأخر"; break;
                    case 4: statusName = "غياب بعذر"; break;
                }

                dtTeacherAttendance.Rows[rowHandle]["StatusId"] = newStatusId;
                dtTeacherAttendance.Rows[rowHandle]["StatusName"] = statusName;
                GrdTeacherAttend.RefreshData();

                // الانتقال للسطر التالي
                if (rowHandle < dtTeacherAttendance.Rows.Count - 1)
                    GrdTeacherAttend.FocusedRowHandle = rowHandle + 1;

                e.Handled = true;
            }
        }
    }
}
