namespace Eygaz
{
    partial class FrmAttendanceStud
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.PnlData = new System.Windows.Forms.Panel();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnLoadStudents = new System.Windows.Forms.Button();
            this.BtnMarkAllPresent = new System.Windows.Forms.Button();
            this.BtnMarkAllAbsent = new System.Windows.Forms.Button();
            this.AttendDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.ClassId = new System.Windows.Forms.ComboBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.SubjectId = new System.Windows.Forms.ComboBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.TeacherId = new System.Windows.Forms.ComboBox();
            this.lblTeacher = new System.Windows.Forms.Label();
            this.lblShortcuts = new System.Windows.Forms.Label();
            this.GVAttendStud = new DevExpress.XtraGrid.GridControl();
            this.GrdAttendStud = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.PnlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVAttendStud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdAttendStud)).BeginInit();
            this.SuspendLayout();
            // 
            // PnlData
            // 
            this.PnlData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlData.Controls.Add(this.BtnSave);
            this.PnlData.Controls.Add(this.BtnLoadStudents);
            this.PnlData.Controls.Add(this.BtnMarkAllPresent);
            this.PnlData.Controls.Add(this.BtnMarkAllAbsent);
            this.PnlData.Controls.Add(this.AttendDate);
            this.PnlData.Controls.Add(this.label1);
            this.PnlData.Controls.Add(this.TeacherId);
            this.PnlData.Controls.Add(this.lblTeacher);
            this.PnlData.Controls.Add(this.SubjectId);
            this.PnlData.Controls.Add(this.lblSubject);
            this.PnlData.Controls.Add(this.ClassId);
            this.PnlData.Controls.Add(this.lblClass);
            this.PnlData.Controls.Add(this.lblShortcuts);
            this.PnlData.Location = new System.Drawing.Point(11, 63);
            this.PnlData.Name = "PnlData";
            this.PnlData.Size = new System.Drawing.Size(680, 130);
            this.PnlData.TabIndex = 73;
            // 
            // Row 1: الفصل - المادة - المدرس
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblClass.Location = new System.Drawing.Point(630, 12);
            this.lblClass.Name = "lblClass";
            this.lblClass.Text = "الفصل";

            this.ClassId.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
            this.ClassId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ClassId.Location = new System.Drawing.Point(500, 9);
            this.ClassId.Name = "ClassId";
            this.ClassId.Size = new System.Drawing.Size(125, 21);
            this.ClassId.TabIndex = 1;

            this.lblSubject.AutoSize = true;
            this.lblSubject.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblSubject.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblSubject.Location = new System.Drawing.Point(438, 12);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Text = "المادة";

            this.SubjectId.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
            this.SubjectId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SubjectId.Location = new System.Drawing.Point(308, 9);
            this.SubjectId.Name = "SubjectId";
            this.SubjectId.Size = new System.Drawing.Size(125, 21);
            this.SubjectId.TabIndex = 2;

            this.lblTeacher.AutoSize = true;
            this.lblTeacher.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblTeacher.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblTeacher.Location = new System.Drawing.Point(240, 12);
            this.lblTeacher.Name = "lblTeacher";
            this.lblTeacher.Text = "المدرس";

            this.TeacherId.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
            this.TeacherId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TeacherId.Location = new System.Drawing.Point(110, 9);
            this.TeacherId.Name = "TeacherId";
            this.TeacherId.Size = new System.Drawing.Size(125, 21);
            this.TeacherId.TabIndex = 3;
            // 
            // Row 2: التاريخ - تحميل - حفظ - تحديد الكل
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.label1.Location = new System.Drawing.Point(630, 42);
            this.label1.Name = "label1";
            this.label1.Text = "التاريخ";

            this.AttendDate.CustomFormat = "yyyy-MM-dd";
            this.AttendDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.AttendDate.Location = new System.Drawing.Point(500, 39);
            this.AttendDate.Name = "AttendDate";
            this.AttendDate.Size = new System.Drawing.Size(125, 20);
            this.AttendDate.TabIndex = 4;

            this.BtnLoadStudents.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.BtnLoadStudents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLoadStudents.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnLoadStudents.ForeColor = System.Drawing.Color.White;
            this.BtnLoadStudents.Location = new System.Drawing.Point(370, 37);
            this.BtnLoadStudents.Name = "BtnLoadStudents";
            this.BtnLoadStudents.Size = new System.Drawing.Size(100, 25);
            this.BtnLoadStudents.TabIndex = 5;
            this.BtnLoadStudents.Text = "تحميل الطلاب";
            this.BtnLoadStudents.UseVisualStyleBackColor = false;
            this.BtnLoadStudents.Click += new System.EventHandler(this.BtnLoadStudents_Click);

            this.BtnSave.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSave.ForeColor = System.Drawing.Color.White;
            this.BtnSave.Location = new System.Drawing.Point(260, 37);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(100, 25);
            this.BtnSave.TabIndex = 6;
            this.BtnSave.Text = "حفظ الحضور";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // Row 3: أزرار التحديد السريع + اختصارات
            // 
            this.BtnMarkAllPresent.BackColor = System.Drawing.Color.ForestGreen;
            this.BtnMarkAllPresent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMarkAllPresent.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.BtnMarkAllPresent.ForeColor = System.Drawing.Color.White;
            this.BtnMarkAllPresent.Location = new System.Drawing.Point(550, 70);
            this.BtnMarkAllPresent.Name = "BtnMarkAllPresent";
            this.BtnMarkAllPresent.Size = new System.Drawing.Size(110, 23);
            this.BtnMarkAllPresent.TabIndex = 7;
            this.BtnMarkAllPresent.Text = "الكل حاضر (F5)";
            this.BtnMarkAllPresent.UseVisualStyleBackColor = false;
            this.BtnMarkAllPresent.Click += new System.EventHandler(this.BtnMarkAllPresent_Click);

            this.BtnMarkAllAbsent.BackColor = System.Drawing.Color.Crimson;
            this.BtnMarkAllAbsent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMarkAllAbsent.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.BtnMarkAllAbsent.ForeColor = System.Drawing.Color.White;
            this.BtnMarkAllAbsent.Location = new System.Drawing.Point(430, 70);
            this.BtnMarkAllAbsent.Name = "BtnMarkAllAbsent";
            this.BtnMarkAllAbsent.Size = new System.Drawing.Size(110, 23);
            this.BtnMarkAllAbsent.TabIndex = 8;
            this.BtnMarkAllAbsent.Text = "الكل غائب (F6)";
            this.BtnMarkAllAbsent.UseVisualStyleBackColor = false;
            this.BtnMarkAllAbsent.Click += new System.EventHandler(this.BtnMarkAllAbsent_Click);

            this.lblShortcuts.AutoSize = true;
            this.lblShortcuts.Font = new System.Drawing.Font("Tahoma", 7F);
            this.lblShortcuts.ForeColor = System.Drawing.Color.Gray;
            this.lblShortcuts.Location = new System.Drawing.Point(50, 74);
            this.lblShortcuts.Name = "lblShortcuts";
            this.lblShortcuts.Text = "اختصارات: P=حاضر | A=غائب | L=متأخر | E=عذر | F5=الكل حاضر | F6=الكل غائب";
            // 
            // GVAttendStud
            // 
            this.GVAttendStud.Location = new System.Drawing.Point(2, 200);
            this.GVAttendStud.MainView = this.GrdAttendStud;
            this.GVAttendStud.Name = "GVAttendStud";
            this.GVAttendStud.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GVAttendStud.Size = new System.Drawing.Size(695, 510);
            this.GVAttendStud.TabIndex = 74;
            this.GVAttendStud.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.GrdAttendStud });

            this.GrdAttendStud.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.GrdAttendStud.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Blue;
            this.GrdAttendStud.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdAttendStud.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.GrdAttendStud.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdAttendStud.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdAttendStud.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 8F);
            this.GrdAttendStud.Appearance.Row.Options.UseFont = true;
            this.GrdAttendStud.Appearance.Row.Options.UseTextOptions = true;
            this.GrdAttendStud.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdAttendStud.ColumnPanelRowHeight = 30;
            this.GrdAttendStud.GridControl = this.GVAttendStud;
            this.GrdAttendStud.Name = "GrdAttendStud";
            this.GrdAttendStud.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GrdAttendStud.OptionsView.ColumnAutoWidth = true;
            this.GrdAttendStud.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.GrdAttendStud.OptionsView.ShowGroupPanel = false;
            // 
            // FrmAttendanceStud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 720);
            this.Controls.Add(this.GVAttendStud);
            this.Controls.Add(this.PnlData);
            this.KeyPreview = true;
            this.Name = "FrmAttendanceStud";
            this.Text = "سجل الحضور والغياب للطلاب";
            this.Load += new System.EventHandler(this.FrmAttendanceStud_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAttendanceStud_KeyDown);
            this.PnlData.ResumeLayout(false);
            this.PnlData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVAttendStud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdAttendStud)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel PnlData;
        private System.Windows.Forms.ComboBox ClassId;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.ComboBox SubjectId;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.ComboBox TeacherId;
        private System.Windows.Forms.Label lblTeacher;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker AttendDate;
        private System.Windows.Forms.Button BtnLoadStudents;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnMarkAllPresent;
        private System.Windows.Forms.Button BtnMarkAllAbsent;
        private System.Windows.Forms.Label lblShortcuts;
        private DevExpress.XtraGrid.GridControl GVAttendStud;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdAttendStud;
    }
}