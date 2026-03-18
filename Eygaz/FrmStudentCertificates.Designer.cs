namespace Eygaz
{
    partial class FrmStudentCertificates
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
            // === القسم 1: اختيار البيانات ===
            this.PnlSelect = new System.Windows.Forms.Panel();
            this.lblClass = new System.Windows.Forms.Label();
            this.CmbClass = new System.Windows.Forms.ComboBox();
            this.lblStudent = new System.Windows.Forms.Label();
            this.CmbStudent = new System.Windows.Forms.ComboBox();
            this.lblCertType = new System.Windows.Forms.Label();
            this.CmbCertType = new System.Windows.Forms.ComboBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.DtIssueDate = new System.Windows.Forms.DateTimePicker();
            this.BtnLoadData = new System.Windows.Forms.Button();

            // === القسم 2: بيانات الشهادة ===
            this.PnlInfo = new System.Windows.Forms.Panel();
            this.lblInfoTitle = new System.Windows.Forms.Label();
            this.lblStudName = new System.Windows.Forms.Label();
            this.txtStudentName = new System.Windows.Forms.TextBox();
            this.lblClsName = new System.Windows.Forms.Label();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.lblTchName = new System.Windows.Forms.Label();
            this.txtTeacherName = new System.Windows.Forms.TextBox();
            this.lblPct = new System.Windows.Forms.Label();
            this.txtAttendancePct = new System.Windows.Forms.TextBox();
            this.lblSessions = new System.Windows.Forms.Label();
            this.txtTotalSessions = new System.Windows.Forms.TextBox();
            this.lblAbsent = new System.Windows.Forms.Label();
            this.txtAbsentCount = new System.Windows.Forms.TextBox();
            this.lblPresent = new System.Windows.Forms.Label();
            this.txtPresentCount = new System.Windows.Forms.TextBox();

            // === القسم 3: الأزرار ===
            this.PnlButtons = new System.Windows.Forms.Panel();
            this.BtnPreview = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.BtnSaveRecord = new System.Windows.Forms.Button();

            // === القسم 4: سجل الشهادات ===
            this.GVCertificates = new DevExpress.XtraGrid.GridControl();
            this.GrdCertificates = new DevExpress.XtraGrid.Views.Grid.GridView();

            this.PnlSelect.SuspendLayout();
            this.PnlInfo.SuspendLayout();
            this.PnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVCertificates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdCertificates)).BeginInit();
            this.SuspendLayout();

            // ==========================================
            // PnlSelect: اختيار البيانات
            // ==========================================
            this.PnlSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSelect.Controls.Add(this.BtnLoadData);
            this.PnlSelect.Controls.Add(this.DtIssueDate);
            this.PnlSelect.Controls.Add(this.lblDate);
            this.PnlSelect.Controls.Add(this.CmbCertType);
            this.PnlSelect.Controls.Add(this.lblCertType);
            this.PnlSelect.Controls.Add(this.CmbStudent);
            this.PnlSelect.Controls.Add(this.lblStudent);
            this.PnlSelect.Controls.Add(this.CmbClass);
            this.PnlSelect.Controls.Add(this.lblClass);
            this.PnlSelect.Location = new System.Drawing.Point(11, 63);
            this.PnlSelect.Name = "PnlSelect";
            this.PnlSelect.Size = new System.Drawing.Size(780, 55);

            this.lblClass.AutoSize = true;
            this.lblClass.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblClass.Location = new System.Drawing.Point(733, 17);
            this.lblClass.Text = "الفصل";

            this.CmbClass.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
            this.CmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbClass.Location = new System.Drawing.Point(613, 14);
            this.CmbClass.Name = "CmbClass";
            this.CmbClass.Size = new System.Drawing.Size(115, 21);
            this.CmbClass.TabIndex = 1;
            this.CmbClass.SelectedIndexChanged += new System.EventHandler(this.CmbClass_SelectedIndexChanged);

            this.lblStudent.AutoSize = true;
            this.lblStudent.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblStudent.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblStudent.Location = new System.Drawing.Point(572, 17);
            this.lblStudent.Text = "الطالب";

            this.CmbStudent.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
            this.CmbStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbStudent.Location = new System.Drawing.Point(440, 14);
            this.CmbStudent.Name = "CmbStudent";
            this.CmbStudent.Size = new System.Drawing.Size(125, 21);
            this.CmbStudent.TabIndex = 2;

            this.lblCertType.AutoSize = true;
            this.lblCertType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblCertType.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblCertType.Location = new System.Drawing.Point(395, 17);
            this.lblCertType.Text = "النوع";

            this.CmbCertType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCertType.Location = new System.Drawing.Point(265, 14);
            this.CmbCertType.Name = "CmbCertType";
            this.CmbCertType.Size = new System.Drawing.Size(125, 21);
            this.CmbCertType.TabIndex = 3;

            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblDate.Location = new System.Drawing.Point(225, 17);
            this.lblDate.Text = "التاريخ";

            this.DtIssueDate.CustomFormat = "yyyy-MM-dd";
            this.DtIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtIssueDate.Location = new System.Drawing.Point(120, 14);
            this.DtIssueDate.Name = "DtIssueDate";
            this.DtIssueDate.Size = new System.Drawing.Size(100, 20);
            this.DtIssueDate.TabIndex = 4;

            this.BtnLoadData.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.BtnLoadData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLoadData.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.BtnLoadData.ForeColor = System.Drawing.Color.White;
            this.BtnLoadData.Location = new System.Drawing.Point(20, 12);
            this.BtnLoadData.Name = "BtnLoadData";
            this.BtnLoadData.Size = new System.Drawing.Size(85, 25);
            this.BtnLoadData.Text = "تحميل البيانات";
            this.BtnLoadData.UseVisualStyleBackColor = false;
            this.BtnLoadData.Click += new System.EventHandler(this.BtnLoadData_Click);

            // ==========================================
            // PnlInfo: بيانات الشهادة
            // ==========================================
            this.PnlInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlInfo.Controls.Add(this.lblInfoTitle);
            this.PnlInfo.Controls.Add(this.txtStudentName);
            this.PnlInfo.Controls.Add(this.lblStudName);
            this.PnlInfo.Controls.Add(this.txtClassName);
            this.PnlInfo.Controls.Add(this.lblClsName);
            this.PnlInfo.Controls.Add(this.txtTeacherName);
            this.PnlInfo.Controls.Add(this.lblTchName);
            this.PnlInfo.Controls.Add(this.txtAttendancePct);
            this.PnlInfo.Controls.Add(this.lblPct);
            this.PnlInfo.Controls.Add(this.txtTotalSessions);
            this.PnlInfo.Controls.Add(this.lblSessions);
            this.PnlInfo.Controls.Add(this.txtAbsentCount);
            this.PnlInfo.Controls.Add(this.lblAbsent);
            this.PnlInfo.Controls.Add(this.txtPresentCount);
            this.PnlInfo.Controls.Add(this.lblPresent);
            this.PnlInfo.Location = new System.Drawing.Point(11, 125);
            this.PnlInfo.Name = "PnlInfo";
            this.PnlInfo.Size = new System.Drawing.Size(780, 130);

            this.lblInfoTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblInfoTitle.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblInfoTitle.Location = new System.Drawing.Point(600, 5);
            this.lblInfoTitle.AutoSize = true;
            this.lblInfoTitle.Text = "◆  بيانات الشهادة";

            // Row 1
            int r1y = 28;
            this.lblStudName.AutoSize = true;
            this.lblStudName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblStudName.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblStudName.Location = new System.Drawing.Point(710, r1y);
            this.lblStudName.Text = "اسم الطالب";

            this.txtStudentName.ReadOnly = true;
            this.txtStudentName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtStudentName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtStudentName.Location = new System.Drawing.Point(530, r1y - 3);
            this.txtStudentName.Size = new System.Drawing.Size(175, 22);

            this.lblClsName.AutoSize = true;
            this.lblClsName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblClsName.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblClsName.Location = new System.Drawing.Point(470, r1y);
            this.lblClsName.Text = "الفصل";

            this.txtClassName.ReadOnly = true;
            this.txtClassName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtClassName.Location = new System.Drawing.Point(340, r1y - 3);
            this.txtClassName.Size = new System.Drawing.Size(125, 20);

            this.lblTchName.AutoSize = true;
            this.lblTchName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblTchName.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblTchName.Location = new System.Drawing.Point(290, r1y);
            this.lblTchName.Text = "المدرس";

            this.txtTeacherName.ReadOnly = true;
            this.txtTeacherName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTeacherName.Location = new System.Drawing.Point(150, r1y - 3);
            this.txtTeacherName.Size = new System.Drawing.Size(135, 20);

            // Row 2
            int r2y = 65;
            this.lblPct.AutoSize = true;
            this.lblPct.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblPct.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblPct.Location = new System.Drawing.Point(693, r2y);
            this.lblPct.Text = "نسبة الحضور %";

            this.txtAttendancePct.ReadOnly = true;
            this.txtAttendancePct.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtAttendancePct.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.txtAttendancePct.ForeColor = System.Drawing.Color.ForestGreen;
            this.txtAttendancePct.Location = new System.Drawing.Point(620, r2y - 3);
            this.txtAttendancePct.Size = new System.Drawing.Size(70, 24);
            this.txtAttendancePct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            this.lblSessions.AutoSize = true;
            this.lblSessions.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblSessions.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblSessions.Location = new System.Drawing.Point(555, r2y);
            this.lblSessions.Text = "عدد الجلسات";

            this.txtTotalSessions.ReadOnly = true;
            this.txtTotalSessions.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalSessions.Location = new System.Drawing.Point(495, r2y - 3);
            this.txtTotalSessions.Size = new System.Drawing.Size(55, 20);
            this.txtTotalSessions.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            this.lblPresent.AutoSize = true;
            this.lblPresent.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblPresent.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblPresent.Location = new System.Drawing.Point(435, r2y);
            this.lblPresent.Text = "الحضور";

            this.txtPresentCount.ReadOnly = true;
            this.txtPresentCount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPresentCount.Location = new System.Drawing.Point(375, r2y - 3);
            this.txtPresentCount.Size = new System.Drawing.Size(55, 20);
            this.txtPresentCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            this.lblAbsent.AutoSize = true;
            this.lblAbsent.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblAbsent.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblAbsent.Location = new System.Drawing.Point(315, r2y);
            this.lblAbsent.Text = "الغياب";

            this.txtAbsentCount.ReadOnly = true;
            this.txtAbsentCount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtAbsentCount.Location = new System.Drawing.Point(255, r2y - 3);
            this.txtAbsentCount.Size = new System.Drawing.Size(55, 20);
            this.txtAbsentCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // ==========================================
            // PnlButtons: الأزرار
            // ==========================================
            this.PnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlButtons.Controls.Add(this.BtnPreview);
            this.PnlButtons.Controls.Add(this.BtnPrint);
            this.PnlButtons.Controls.Add(this.BtnSaveRecord);
            this.PnlButtons.Location = new System.Drawing.Point(11, 262);
            this.PnlButtons.Name = "PnlButtons";
            this.PnlButtons.Size = new System.Drawing.Size(780, 42);

            this.BtnPreview.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPreview.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnPreview.ForeColor = System.Drawing.Color.White;
            this.BtnPreview.Location = new System.Drawing.Point(580, 7);
            this.BtnPreview.Name = "BtnPreview";
            this.BtnPreview.Size = new System.Drawing.Size(110, 28);
            this.BtnPreview.Text = "معاينة الشهادة";
            this.BtnPreview.UseVisualStyleBackColor = false;
            this.BtnPreview.Click += new System.EventHandler(this.BtnPreview_Click);

            this.BtnPrint.BackColor = System.Drawing.Color.MediumPurple;
            this.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrint.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnPrint.ForeColor = System.Drawing.Color.White;
            this.BtnPrint.Location = new System.Drawing.Point(460, 7);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(110, 28);
            this.BtnPrint.Text = "طباعة مباشرة";
            this.BtnPrint.UseVisualStyleBackColor = false;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);

            this.BtnSaveRecord.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.BtnSaveRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSaveRecord.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSaveRecord.ForeColor = System.Drawing.Color.White;
            this.BtnSaveRecord.Location = new System.Drawing.Point(340, 7);
            this.BtnSaveRecord.Name = "BtnSaveRecord";
            this.BtnSaveRecord.Size = new System.Drawing.Size(110, 28);
            this.BtnSaveRecord.Text = "حفظ السجل";
            this.BtnSaveRecord.UseVisualStyleBackColor = false;
            this.BtnSaveRecord.Click += new System.EventHandler(this.BtnSaveRecord_Click);

            // ==========================================
            // GVCertificates: سجل الشهادات
            // ==========================================
            this.GVCertificates.Location = new System.Drawing.Point(2, 312);
            this.GVCertificates.MainView = this.GrdCertificates;
            this.GVCertificates.Name = "GVCertificates";
            this.GVCertificates.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GVCertificates.Size = new System.Drawing.Size(795, 400);
            this.GVCertificates.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.GrdCertificates });

            this.GrdCertificates.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.GrdCertificates.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Blue;
            this.GrdCertificates.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdCertificates.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.GrdCertificates.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdCertificates.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdCertificates.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 8F);
            this.GrdCertificates.Appearance.Row.Options.UseFont = true;
            this.GrdCertificates.Appearance.Row.Options.UseTextOptions = true;
            this.GrdCertificates.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdCertificates.ColumnPanelRowHeight = 30;
            this.GrdCertificates.GridControl = this.GVCertificates;
            this.GrdCertificates.Name = "GrdCertificates";
            this.GrdCertificates.OptionsBehavior.Editable = false;
            this.GrdCertificates.OptionsView.ColumnAutoWidth = true;
            this.GrdCertificates.OptionsView.ShowGroupPanel = false;
            this.GrdCertificates.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.GrdCertificates.RowHeight = 28;

            // ==========================================
            // FrmStudentCertificates
            // ==========================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 720);
            this.Controls.Add(this.GVCertificates);
            this.Controls.Add(this.PnlButtons);
            this.Controls.Add(this.PnlInfo);
            this.Controls.Add(this.PnlSelect);
            this.Name = "FrmStudentCertificates";
            this.Text = "الشهادات الطلابية";
            this.Load += new System.EventHandler(this.FrmStudentCertificates_Load);
            this.PnlSelect.ResumeLayout(false);
            this.PnlSelect.PerformLayout();
            this.PnlInfo.ResumeLayout(false);
            this.PnlInfo.PerformLayout();
            this.PnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GVCertificates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdCertificates)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel PnlSelect;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.ComboBox CmbClass;
        private System.Windows.Forms.Label lblStudent;
        private System.Windows.Forms.ComboBox CmbStudent;
        private System.Windows.Forms.Label lblCertType;
        private System.Windows.Forms.ComboBox CmbCertType;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker DtIssueDate;
        private System.Windows.Forms.Button BtnLoadData;

        private System.Windows.Forms.Panel PnlInfo;
        private System.Windows.Forms.Label lblInfoTitle;
        private System.Windows.Forms.Label lblStudName;
        private System.Windows.Forms.TextBox txtStudentName;
        private System.Windows.Forms.Label lblClsName;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label lblTchName;
        private System.Windows.Forms.TextBox txtTeacherName;
        private System.Windows.Forms.Label lblPct;
        private System.Windows.Forms.TextBox txtAttendancePct;
        private System.Windows.Forms.Label lblSessions;
        private System.Windows.Forms.TextBox txtTotalSessions;
        private System.Windows.Forms.Label lblAbsent;
        private System.Windows.Forms.TextBox txtAbsentCount;
        private System.Windows.Forms.Label lblPresent;
        private System.Windows.Forms.TextBox txtPresentCount;

        private System.Windows.Forms.Panel PnlButtons;
        private System.Windows.Forms.Button BtnPreview;
        private System.Windows.Forms.Button BtnPrint;
        private System.Windows.Forms.Button BtnSaveRecord;

        private DevExpress.XtraGrid.GridControl GVCertificates;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdCertificates;
    }
}
