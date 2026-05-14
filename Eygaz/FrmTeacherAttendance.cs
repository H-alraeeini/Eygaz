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
        private Label lblHijriDate;

        public FrmTeacherAttendance()
        {
            InitializeComponent();
        }

        // =============================================
        //  Õ„Ì· «·‘«‘…
        // =============================================
        private void FrmTeacherAttendance_Load(object sender, EventArgs e)
        {
            try
            {
                AttendDate.Value = DateTime.Today;
                EnsureHijriDateLabel();
                AttendDate.ValueChanged += AttendDate_ValueChanged;
                UpdateHijriDateLabel();

                GrdTeacherAttend.OptionsBehavior.Editable = true;
                GrdTeacherAttend.RowHeight = 28;

                BtnSave.Enabled = false;
                BtnMarkAllPresent.Enabled = false;
                BtnMarkAllAbsent.Enabled = false;
                BtnSendWhatsApp.Enabled = false;

                //  Õ„Ì·  ·ﬁ«∆Ì · «—ÌŒ «·ÌÊ„
                LoadTeachers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Œÿ√ √À‰«¡  Õ„Ì· «·»Ì«‰« : " + ex.Message, "Œÿ√",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        //  Õ„Ì· «·„œ—”Ì‰
        // =============================================
        private void BtnLoadTeachers_Click(object sender, EventArgs e)
        {
            LoadTeachers();
        }

        private void EnsureHijriDateLabel()
        {
            if (lblHijriDate != null) return;

            lblHijriDate = new Label();
            lblHijriDate.AutoSize = true;
            lblHijriDate.ForeColor = Color.DarkSlateBlue;
            lblHijriDate.Font = new Font("Tahoma", 8F, FontStyle.Bold);
            lblHijriDate.Location = new Point(427, 55);
            lblHijriDate.Name = "lblHijriDate";
            Controls.Add(lblHijriDate);
            lblHijriDate.RightToLeft = RightToLeft.No;
            lblHijriDate.BringToFront();
        }

        private void AttendDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateHijriDateLabel();
        }

        private void UpdateHijriDateLabel()
        {
            if (lblHijriDate == null) return;
            string hijri = AttendanceHelper.ToHijriDateDisplayArabic(AttendDate.Value.Date);
            lblHijriDate.Text = hijri;
        }

        private void LoadTeachers()
        {
            try
            {
                UpdateHijriDateLabel();
                string date = AttendDate.Value.ToString("yyyy-MM-dd");
                dtTeacherAttendance = helper.PrepareTeacherAttendanceGrid(date);

                if (dtTeacherAttendance == null || dtTeacherAttendance.Rows.Count == 0)
                {
                    MessageBox.Show("·« ÌÊÃœ „œ—”Ì‰ ‰‘ÿÌ‰ ›Ì «·‰Ÿ«„", " ‰»ÌÂ",
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
                MessageBox.Show("Œÿ√ √À‰«¡  Õ„Ì· «·„œ—”Ì‰: " + ex.Message, "Œÿ√",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // ≈⁄œ«œ √⁄„œ… «·‹ Grid
        // =============================================
        private void SetupGridColumns()
        {
            if (GrdTeacherAttend.Columns.Count == 0) return;

            for (int i = 0; i < GrdTeacherAttend.Columns.Count; i++)
                GrdTeacherAttend.Columns[i].Visible = false;

            // ≈Œ›«¡ TeacherId Ê Phone
            if (GrdTeacherAttend.Columns.ColumnByFieldName("TeacherId") != null)
                GrdTeacherAttend.Columns["TeacherId"].Visible = false;

            if (GrdTeacherAttend.Columns.ColumnByFieldName("Phone") != null)
                GrdTeacherAttend.Columns["Phone"].Visible = false;

            // «”„ «·„œ—”
            if (GrdTeacherAttend.Columns.ColumnByFieldName("TeacherName") != null)
            {
                var colName = GrdTeacherAttend.Columns["TeacherName"];
                colName.Visible = true;
                colName.VisibleIndex = 0;
                colName.Caption = "«”„ «·„œ—”";
                colName.OptionsColumn.AllowEdit = false;
                colName.Width = 200;
            }

            // Õ«·… «·Õ÷Ê—
            if (GrdTeacherAttend.Columns.ColumnByFieldName("StatusId") != null)
            {
                var colStatus = GrdTeacherAttend.Columns["StatusId"];
                colStatus.Visible = true;
                colStatus.VisibleIndex = 1;
                colStatus.Caption = "«·Õ«·…";
                colStatus.OptionsColumn.AllowEdit = true;
                colStatus.Width = 120;

                RepositoryItemLookUpEdit repoCombo = new RepositoryItemLookUpEdit();
                DataTable statusData = helper.GetAttendanceStatuses();
                repoCombo.DataSource = statusData;
                repoCombo.ValueMember = "Id";
                repoCombo.DisplayMember = "StatusName";
                repoCombo.NullText = "Õ«÷—";
                repoCombo.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("StatusName", "«·Õ«·…"));
                repoCombo.ShowHeader = false;

                GVTeacherAttend.RepositoryItems.Add(repoCombo);
                colStatus.ColumnEdit = repoCombo;
            }

            // ≈Œ›«¡ StatusName
            if (GrdTeacherAttend.Columns.ColumnByFieldName("StatusName") != null)
                GrdTeacherAttend.Columns["StatusName"].Visible = false;

            // „·«ÕŸ« 
            if (GrdTeacherAttend.Columns.ColumnByFieldName("Notes") != null)
            {
                var colNotes = GrdTeacherAttend.Columns["Notes"];
                colNotes.Visible = true;
                colNotes.VisibleIndex = 2;
                colNotes.Caption = "„·«ÕŸ« ";
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
        // Õ›Ÿ «·Õ÷Ê—
        // =============================================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtTeacherAttendance == null || dtTeacherAttendance.Rows.Count == 0)
                {
                    MessageBox.Show("·«  ÊÃœ »Ì«‰«  ··Õ›Ÿ", " ‰»ÌÂ",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                GrdTeacherAttend.CloseEditor();
                GrdTeacherAttend.UpdateCurrentRow();

                string date = AttendDate.Value.ToString("yyyy-MM-dd");
                bool success = helper.SaveBulkTeacherAttendance(date, dtTeacherAttendance);

                if (success)
                    MessageBox.Show(" „ Õ›Ÿ Õ÷Ê— «·„œ—”Ì‰ »‰Ã«Õ", " ‰»ÌÂ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("ÕœÀ Œÿ√ √À‰«¡ Õ›Ÿ »⁄÷ «·”Ã·« ", "Œÿ√",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Œÿ√ √À‰«¡ «·Õ›Ÿ: " + ex.Message, "Œÿ√",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        //  ÕœÌœ «·ﬂ· Õ«÷—
        // =============================================
        private void BtnMarkAllPresent_Click(object sender, EventArgs e)
        {
            MarkAll(1); // 1 = Õ«÷—
        }

        // =============================================
        //  ÕœÌœ «·ﬂ· €«∆»
        // =============================================
        private void BtnMarkAllAbsent_Click(object sender, EventArgs e)
        {
            MarkAll(2); // 2 = €«∆»
        }

        // =============================================
        // œ«·… „‘ —ﬂ… · ÕœÌœ Õ«·… Ã„Ì⁄ «·„œ—”Ì‰
        // =============================================
        private void MarkAll(int statusId)
        {
            if (dtTeacherAttendance == null || dtTeacherAttendance.Rows.Count == 0) return;

            GrdTeacherAttend.CloseEditor();

            string statusName = "";
            switch (statusId)
            {
                case 1: statusName = "Õ«÷—"; break;
                case 2: statusName = "€«∆»"; break;
                case 3: statusName = "„ √Œ—"; break;
                case 4: statusName = "€Ì«» »⁄–—"; break;
            }

            foreach (DataRow row in dtTeacherAttendance.Rows)
            {
                row["StatusId"] = statusId;
                row["StatusName"] = statusName;
            }

            GrdTeacherAttend.RefreshData();
        }

        // =============================================
        // ≈—”«· ≈‘⁄«—«  Ê« ”«» ··€«∆»Ì‰ Ê«·„ √Œ—Ì‰
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

                    if (statusId == 2) // €«∆»
                    {
                        WhatsAppHelper.SendTeacherAbsenceNotification(teacherName, phone, date);
                        sentCount++;
                    }
                    else if (statusId == 3) // „ √Œ—
                    {
                        WhatsAppHelper.SendTeacherLateNotification(teacherName, phone, date);
                        sentCount++;
                    }
                }

                if (sentCount > 0)
                    MessageBox.Show($" „ › Õ Ê« ”«» ·≈—”«· {sentCount} ≈‘⁄«—(« )", " ‰»ÌÂ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("·« ÌÊÃœ „œ—”Ì‰ €«∆»Ì‰ √Ê „ √Œ—Ì‰ ·≈—”«· ≈‘⁄«—«  ·Â„", " ‰»ÌÂ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Œÿ√: " + ex.Message, "Œÿ√",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // «Œ ’«—«  ·ÊÕ… «·„›« ÌÕ
        // =============================================
        private void FrmTeacherAttendance_KeyDown(object sender, KeyEventArgs e)
        {
            if (dtTeacherAttendance == null || dtTeacherAttendance.Rows.Count == 0) return;

            // F5 = «·ﬂ· Õ«÷—
            if (e.KeyCode == Keys.F5)
            {
                MarkAll(1);
                e.Handled = true;
                return;
            }

            // F6 = «·ﬂ· €«∆»
            if (e.KeyCode == Keys.F6)
            {
                MarkAll(2);
                e.Handled = true;
                return;
            }

            // «Œ ’«—«  ··„œ—” «·Õ«·Ì
            int rowHandle = GrdTeacherAttend.FocusedRowHandle;
            if (rowHandle < 0 || rowHandle >= dtTeacherAttendance.Rows.Count) return;

            int newStatusId = -1;
            switch (e.KeyCode)
            {
                case Keys.P: newStatusId = 1; break; // Õ«÷—
                case Keys.A: newStatusId = 2; break; // €«∆»
                case Keys.L: newStatusId = 3; break; // „ √Œ—
                case Keys.E: newStatusId = 4; break; // €Ì«» »⁄–—
            }

            if (newStatusId > 0)
            {
                GrdTeacherAttend.CloseEditor();
                string statusName = "";
                switch (newStatusId)
                {
                    case 1: statusName = "Õ«÷—"; break;
                    case 2: statusName = "€«∆»"; break;
                    case 3: statusName = "„ √Œ—"; break;
                    case 4: statusName = "€Ì«» »⁄–—"; break;
                }

                dtTeacherAttendance.Rows[rowHandle]["StatusId"] = newStatusId;
                dtTeacherAttendance.Rows[rowHandle]["StatusName"] = statusName;
                GrdTeacherAttend.RefreshData();

                // «·«‰ ﬁ«· ··”ÿ— «· «·Ì
                if (rowHandle < dtTeacherAttendance.Rows.Count - 1)
                    GrdTeacherAttend.FocusedRowHandle = rowHandle + 1;

                e.Handled = true;
            }
        }
    }
}


