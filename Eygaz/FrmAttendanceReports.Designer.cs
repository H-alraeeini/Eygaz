namespace Eygaz
{
    partial class FrmAttendanceReports
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
            this.lblReportType = new System.Windows.Forms.Label();
            this.CmbReportType = new System.Windows.Forms.ComboBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.CmbClass = new System.Windows.Forms.ComboBox();
            this.lblStudent = new System.Windows.Forms.Label();
            this.CmbStudent = new System.Windows.Forms.ComboBox();
            this.lblMonth = new System.Windows.Forms.Label();
            this.CmbMonth = new System.Windows.Forms.ComboBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.CmbYear = new System.Windows.Forms.ComboBox();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.DtFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.DtTo = new System.Windows.Forms.DateTimePicker();
            this.BtnGenerate = new System.Windows.Forms.Button();
            this.GVReport = new DevExpress.XtraGrid.GridControl();
            this.GrdReport = new DevExpress.XtraGrid.Views.Grid.GridView();

            this.PnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdReport)).BeginInit();
            this.SuspendLayout();

            // PnlFilter
            this.PnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlFilter.Controls.Add(this.BtnGenerate);
            this.PnlFilter.Controls.Add(this.DtTo);
            this.PnlFilter.Controls.Add(this.lblDateTo);
            this.PnlFilter.Controls.Add(this.DtFrom);
            this.PnlFilter.Controls.Add(this.lblDateFrom);
            this.PnlFilter.Controls.Add(this.CmbYear);
            this.PnlFilter.Controls.Add(this.lblYear);
            this.PnlFilter.Controls.Add(this.CmbMonth);
            this.PnlFilter.Controls.Add(this.lblMonth);
            this.PnlFilter.Controls.Add(this.CmbStudent);
            this.PnlFilter.Controls.Add(this.lblStudent);
            this.PnlFilter.Controls.Add(this.CmbClass);
            this.PnlFilter.Controls.Add(this.lblClass);
            this.PnlFilter.Controls.Add(this.CmbReportType);
            this.PnlFilter.Controls.Add(this.lblReportType);
            this.PnlFilter.Location = new System.Drawing.Point(11, 63);
            this.PnlFilter.Name = "PnlFilter";
            this.PnlFilter.Size = new System.Drawing.Size(780, 105);

            // Row 1
            this.lblReportType.AutoSize = true;
            this.lblReportType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblReportType.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblReportType.Location = new System.Drawing.Point(720, 12);
            this.lblReportType.Text = "نوع التقرير";

            this.CmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbReportType.Location = new System.Drawing.Point(560, 9);
            this.CmbReportType.Name = "CmbReportType";
            this.CmbReportType.Size = new System.Drawing.Size(155, 21);
            this.CmbReportType.TabIndex = 1;
            this.CmbReportType.SelectedIndexChanged += new System.EventHandler(this.CmbReportType_SelectedIndexChanged);

            this.lblClass.AutoSize = true;
            this.lblClass.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblClass.Location = new System.Drawing.Point(510, 12);
            this.lblClass.Text = "الفصل";

            this.CmbClass.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
            this.CmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbClass.Location = new System.Drawing.Point(370, 9);
            this.CmbClass.Name = "CmbClass";
            this.CmbClass.Size = new System.Drawing.Size(135, 21);
            this.CmbClass.TabIndex = 2;
            this.CmbClass.SelectedIndexChanged += new System.EventHandler(this.CmbClass_SelectedIndexChanged);

            this.lblStudent.AutoSize = true;
            this.lblStudent.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblStudent.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblStudent.Location = new System.Drawing.Point(320, 12);
            this.lblStudent.Text = "الطالب";

            this.CmbStudent.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
            this.CmbStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbStudent.Location = new System.Drawing.Point(180, 9);
            this.CmbStudent.Name = "CmbStudent";
            this.CmbStudent.Size = new System.Drawing.Size(135, 21);
            this.CmbStudent.TabIndex = 3;

            // Row 2
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblMonth.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblMonth.Location = new System.Drawing.Point(720, 45);
            this.lblMonth.Text = "الشهر";

            this.CmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbMonth.Location = new System.Drawing.Point(640, 42);
            this.CmbMonth.Name = "CmbMonth";
            this.CmbMonth.Size = new System.Drawing.Size(75, 21);
            this.CmbMonth.TabIndex = 4;

            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblYear.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblYear.Location = new System.Drawing.Point(600, 45);
            this.lblYear.Text = "السنة";

            this.CmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbYear.Location = new System.Drawing.Point(520, 42);
            this.CmbYear.Name = "CmbYear";
            this.CmbYear.Size = new System.Drawing.Size(75, 21);
            this.CmbYear.TabIndex = 5;

            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblDateFrom.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblDateFrom.Location = new System.Drawing.Point(470, 45);
            this.lblDateFrom.Text = "من";

            this.DtFrom.CustomFormat = "yyyy-MM-dd";
            this.DtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtFrom.Location = new System.Drawing.Point(360, 42);
            this.DtFrom.Name = "DtFrom";
            this.DtFrom.Size = new System.Drawing.Size(105, 20);
            this.DtFrom.TabIndex = 6;

            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblDateTo.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblDateTo.Location = new System.Drawing.Point(320, 45);
            this.lblDateTo.Text = "إلى";

            this.DtTo.CustomFormat = "yyyy-MM-dd";
            this.DtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtTo.Location = new System.Drawing.Point(210, 42);
            this.DtTo.Name = "DtTo";
            this.DtTo.Size = new System.Drawing.Size(105, 20);
            this.DtTo.TabIndex = 7;

            this.BtnGenerate.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.BtnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGenerate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnGenerate.ForeColor = System.Drawing.Color.White;
            this.BtnGenerate.Location = new System.Drawing.Point(80, 40);
            this.BtnGenerate.Name = "BtnGenerate";
            this.BtnGenerate.Size = new System.Drawing.Size(110, 25);
            this.BtnGenerate.Text = "عرض التقرير";
            this.BtnGenerate.UseVisualStyleBackColor = false;
            this.BtnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);

            // GVReport
            this.GVReport.Location = new System.Drawing.Point(2, 175);
            this.GVReport.MainView = this.GrdReport;
            this.GVReport.Name = "GVReport";
            this.GVReport.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GVReport.Size = new System.Drawing.Size(795, 535);
            this.GVReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.GrdReport });

            this.GrdReport.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.GrdReport.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Blue;
            this.GrdReport.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdReport.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.GrdReport.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdReport.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdReport.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 8F);
            this.GrdReport.Appearance.Row.Options.UseFont = true;
            this.GrdReport.Appearance.Row.Options.UseTextOptions = true;
            this.GrdReport.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdReport.ColumnPanelRowHeight = 30;
            this.GrdReport.GridControl = this.GVReport;
            this.GrdReport.Name = "GrdReport";
            this.GrdReport.OptionsBehavior.Editable = false;
            this.GrdReport.OptionsView.ColumnAutoWidth = true;
            this.GrdReport.OptionsView.ShowGroupPanel = false;
            this.GrdReport.RowHeight = 28;

            // FrmAttendanceReports
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 720);
            this.Controls.Add(this.GVReport);
            this.Controls.Add(this.PnlFilter);
            this.Name = "FrmAttendanceReports";
            this.Text = "تقارير الحضور والغياب";
            this.Load += new System.EventHandler(this.FrmAttendanceReports_Load);
            this.PnlFilter.ResumeLayout(false);
            this.PnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdReport)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel PnlFilter;
        private System.Windows.Forms.Label lblReportType;
        private System.Windows.Forms.ComboBox CmbReportType;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.ComboBox CmbClass;
        private System.Windows.Forms.Label lblStudent;
        private System.Windows.Forms.ComboBox CmbStudent;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.ComboBox CmbMonth;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.ComboBox CmbYear;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker DtFrom;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker DtTo;
        private System.Windows.Forms.Button BtnGenerate;
        private DevExpress.XtraGrid.GridControl GVReport;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdReport;
    }
}
