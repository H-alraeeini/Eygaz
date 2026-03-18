namespace Eygaz
{
    partial class FrmAttendanceSessions
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
            this.PnlFilter = new System.Windows.Forms.Panel();
            this.lblClass = new System.Windows.Forms.Label();
            this.CmbClass = new System.Windows.Forms.ComboBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.CmbSubject = new System.Windows.Forms.ComboBox();
            this.lblTeacher = new System.Windows.Forms.Label();
            this.CmbTeacher = new System.Windows.Forms.ComboBox();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.DtFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.DtTo = new System.Windows.Forms.DateTimePicker();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.BtnShowAll = new System.Windows.Forms.Button();

            this.PnlButtons = new System.Windows.Forms.Panel();
            this.BtnOpen = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();

            this.GVSessions = new DevExpress.XtraGrid.GridControl();
            this.GrdSessions = new DevExpress.XtraGrid.Views.Grid.GridView();

            this.PnlFilter.SuspendLayout();
            this.PnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVSessions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdSessions)).BeginInit();
            this.SuspendLayout();

            // ---- PnlFilter ----
            this.PnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlFilter.Controls.Add(this.BtnShowAll);
            this.PnlFilter.Controls.Add(this.BtnSearch);
            this.PnlFilter.Controls.Add(this.DtTo);
            this.PnlFilter.Controls.Add(this.lblDateTo);
            this.PnlFilter.Controls.Add(this.DtFrom);
            this.PnlFilter.Controls.Add(this.lblDateFrom);
            this.PnlFilter.Controls.Add(this.CmbTeacher);
            this.PnlFilter.Controls.Add(this.lblTeacher);
            this.PnlFilter.Controls.Add(this.CmbSubject);
            this.PnlFilter.Controls.Add(this.lblSubject);
            this.PnlFilter.Controls.Add(this.CmbClass);
            this.PnlFilter.Controls.Add(this.lblClass);
            this.PnlFilter.Location = new System.Drawing.Point(11, 63);
            this.PnlFilter.Name = "PnlFilter";
            this.PnlFilter.Size = new System.Drawing.Size(780, 100);
            this.PnlFilter.TabIndex = 0;

            // Row 1: الفصل - المادة - المدرس
            this.lblClass.AutoSize = true;
            this.lblClass.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblClass.Location = new System.Drawing.Point(720, 12);
            this.lblClass.Name = "lblClass";
            this.lblClass.Text = "الفصل";

            this.CmbClass.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
            this.CmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbClass.Location = new System.Drawing.Point(580, 9);
            this.CmbClass.Name = "CmbClass";
            this.CmbClass.Size = new System.Drawing.Size(135, 21);
            this.CmbClass.TabIndex = 1;

            this.lblSubject.AutoSize = true;
            this.lblSubject.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblSubject.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblSubject.Location = new System.Drawing.Point(530, 12);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Text = "المادة";

            this.CmbSubject.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
            this.CmbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSubject.Location = new System.Drawing.Point(390, 9);
            this.CmbSubject.Name = "CmbSubject";
            this.CmbSubject.Size = new System.Drawing.Size(135, 21);
            this.CmbSubject.TabIndex = 2;

            this.lblTeacher.AutoSize = true;
            this.lblTeacher.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblTeacher.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblTeacher.Location = new System.Drawing.Point(330, 12);
            this.lblTeacher.Name = "lblTeacher";
            this.lblTeacher.Text = "المدرس";

            this.CmbTeacher.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
            this.CmbTeacher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbTeacher.Location = new System.Drawing.Point(190, 9);
            this.CmbTeacher.Name = "CmbTeacher";
            this.CmbTeacher.Size = new System.Drawing.Size(135, 21);
            this.CmbTeacher.TabIndex = 3;

            // Row 2: من - إلى - بحث
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblDateFrom.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblDateFrom.Location = new System.Drawing.Point(720, 48);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Text = "من تاريخ";

            this.DtFrom.CustomFormat = "yyyy-MM-dd";
            this.DtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtFrom.Location = new System.Drawing.Point(580, 45);
            this.DtFrom.Name = "DtFrom";
            this.DtFrom.Size = new System.Drawing.Size(135, 20);
            this.DtFrom.TabIndex = 4;

            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblDateTo.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblDateTo.Location = new System.Drawing.Point(530, 48);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Text = "إلى تاريخ";

            this.DtTo.CustomFormat = "yyyy-MM-dd";
            this.DtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtTo.Location = new System.Drawing.Point(390, 45);
            this.DtTo.Name = "DtTo";
            this.DtTo.Size = new System.Drawing.Size(135, 20);
            this.DtTo.TabIndex = 5;

            this.BtnSearch.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.BtnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSearch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSearch.ForeColor = System.Drawing.Color.White;
            this.BtnSearch.Location = new System.Drawing.Point(250, 43);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(80, 25);
            this.BtnSearch.TabIndex = 6;
            this.BtnSearch.Text = "بحث";
            this.BtnSearch.UseVisualStyleBackColor = false;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);

            this.BtnShowAll.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnShowAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnShowAll.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnShowAll.ForeColor = System.Drawing.Color.White;
            this.BtnShowAll.Location = new System.Drawing.Point(160, 43);
            this.BtnShowAll.Name = "BtnShowAll";
            this.BtnShowAll.Size = new System.Drawing.Size(80, 25);
            this.BtnShowAll.TabIndex = 7;
            this.BtnShowAll.Text = "عرض الكل";
            this.BtnShowAll.UseVisualStyleBackColor = false;
            this.BtnShowAll.Click += new System.EventHandler(this.BtnShowAll_Click);

            // ---- PnlButtons ----
            this.PnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlButtons.Controls.Add(this.BtnOpen);
            this.PnlButtons.Controls.Add(this.BtnEdit);
            this.PnlButtons.Controls.Add(this.BtnDelete);
            this.PnlButtons.Controls.Add(this.BtnPrint);
            this.PnlButtons.Location = new System.Drawing.Point(11, 170);
            this.PnlButtons.Name = "PnlButtons";
            this.PnlButtons.Size = new System.Drawing.Size(780, 40);
            this.PnlButtons.TabIndex = 1;

            this.BtnOpen.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOpen.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnOpen.ForeColor = System.Drawing.Color.White;
            this.BtnOpen.Location = new System.Drawing.Point(600, 6);
            this.BtnOpen.Name = "BtnOpen";
            this.BtnOpen.Size = new System.Drawing.Size(90, 27);
            this.BtnOpen.Text = "فتح الجلسة";
            this.BtnOpen.UseVisualStyleBackColor = false;
            this.BtnOpen.Click += new System.EventHandler(this.BtnOpen_Click);

            this.BtnEdit.BackColor = System.Drawing.Color.Orange;
            this.BtnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEdit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnEdit.ForeColor = System.Drawing.Color.White;
            this.BtnEdit.Location = new System.Drawing.Point(500, 6);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(90, 27);
            this.BtnEdit.Text = "تعديل";
            this.BtnEdit.UseVisualStyleBackColor = false;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);

            this.BtnDelete.BackColor = System.Drawing.Color.Crimson;
            this.BtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDelete.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnDelete.ForeColor = System.Drawing.Color.White;
            this.BtnDelete.Location = new System.Drawing.Point(400, 6);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(90, 27);
            this.BtnDelete.Text = "حذف";
            this.BtnDelete.UseVisualStyleBackColor = false;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);

            this.BtnPrint.BackColor = System.Drawing.Color.MediumPurple;
            this.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrint.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnPrint.ForeColor = System.Drawing.Color.White;
            this.BtnPrint.Location = new System.Drawing.Point(300, 6);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(90, 27);
            this.BtnPrint.Text = "طباعة";
            this.BtnPrint.UseVisualStyleBackColor = false;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);

            // ---- GVSessions ----
            this.GVSessions.Location = new System.Drawing.Point(2, 217);
            this.GVSessions.MainView = this.GrdSessions;
            this.GVSessions.Name = "GVSessions";
            this.GVSessions.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GVSessions.Size = new System.Drawing.Size(795, 490);
            this.GVSessions.TabIndex = 2;
            this.GVSessions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.GrdSessions });

            this.GrdSessions.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.GrdSessions.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Blue;
            this.GrdSessions.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdSessions.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.GrdSessions.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdSessions.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdSessions.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 8F);
            this.GrdSessions.Appearance.Row.Options.UseFont = true;
            this.GrdSessions.Appearance.Row.Options.UseTextOptions = true;
            this.GrdSessions.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdSessions.ColumnPanelRowHeight = 30;
            this.GrdSessions.GridControl = this.GVSessions;
            this.GrdSessions.Name = "GrdSessions";
            this.GrdSessions.OptionsBehavior.Editable = false;
            this.GrdSessions.OptionsView.ColumnAutoWidth = true;
            this.GrdSessions.OptionsView.ShowGroupPanel = false;
            this.GrdSessions.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.GrdSessions.RowHeight = 28;

            // ---- FrmAttendanceSessions ----
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 720);
            this.Controls.Add(this.GVSessions);
            this.Controls.Add(this.PnlButtons);
            this.Controls.Add(this.PnlFilter);
            this.Name = "FrmAttendanceSessions";
            this.Text = "إدارة جلسات الحضور";
            this.Load += new System.EventHandler(this.FrmAttendanceSessions_Load);
            this.PnlFilter.ResumeLayout(false);
            this.PnlFilter.PerformLayout();
            this.PnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GVSessions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdSessions)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel PnlFilter;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.ComboBox CmbClass;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.ComboBox CmbSubject;
        private System.Windows.Forms.Label lblTeacher;
        private System.Windows.Forms.ComboBox CmbTeacher;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker DtFrom;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker DtTo;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.Button BtnShowAll;

        private System.Windows.Forms.Panel PnlButtons;
        private System.Windows.Forms.Button BtnOpen;
        private System.Windows.Forms.Button BtnEdit;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnPrint;

        private DevExpress.XtraGrid.GridControl GVSessions;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdSessions;
    }
}
