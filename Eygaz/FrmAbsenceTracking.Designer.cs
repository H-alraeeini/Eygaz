namespace Eygaz
{
    partial class FrmAbsenceTracking
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
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.DtFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.DtTo = new System.Windows.Forms.DateTimePicker();
            this.lblMinAbsences = new System.Windows.Forms.Label();
            this.NumMinAbsences = new System.Windows.Forms.NumericUpDown();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.GVAbsence = new DevExpress.XtraGrid.GridControl();
            this.GrdAbsence = new DevExpress.XtraGrid.Views.Grid.GridView();

            this.PnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumMinAbsences)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVAbsence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdAbsence)).BeginInit();
            this.SuspendLayout();

            // PnlFilter
            this.PnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlFilter.Controls.Add(this.BtnSearch);
            this.PnlFilter.Controls.Add(this.NumMinAbsences);
            this.PnlFilter.Controls.Add(this.lblMinAbsences);
            this.PnlFilter.Controls.Add(this.DtTo);
            this.PnlFilter.Controls.Add(this.lblDateTo);
            this.PnlFilter.Controls.Add(this.DtFrom);
            this.PnlFilter.Controls.Add(this.lblDateFrom);
            this.PnlFilter.Controls.Add(this.CmbClass);
            this.PnlFilter.Controls.Add(this.lblClass);
            this.PnlFilter.Location = new System.Drawing.Point(11, 63);
            this.PnlFilter.Name = "PnlFilter";
            this.PnlFilter.Size = new System.Drawing.Size(780, 50);

            this.lblClass.AutoSize = true;
            this.lblClass.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblClass.Location = new System.Drawing.Point(730, 15);
            this.lblClass.Text = "الفصل";

            this.CmbClass.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
            this.CmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbClass.Location = new System.Drawing.Point(600, 12);
            this.CmbClass.Name = "CmbClass";
            this.CmbClass.Size = new System.Drawing.Size(125, 21);

            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblDateFrom.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblDateFrom.Location = new System.Drawing.Point(560, 15);
            this.lblDateFrom.Text = "من";

            this.DtFrom.CustomFormat = "yyyy-MM-dd";
            this.DtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtFrom.Location = new System.Drawing.Point(460, 12);
            this.DtFrom.Name = "DtFrom";
            this.DtFrom.Size = new System.Drawing.Size(95, 20);

            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblDateTo.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblDateTo.Location = new System.Drawing.Point(425, 15);
            this.lblDateTo.Text = "إلى";

            this.DtTo.CustomFormat = "yyyy-MM-dd";
            this.DtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtTo.Location = new System.Drawing.Point(325, 12);
            this.DtTo.Name = "DtTo";
            this.DtTo.Size = new System.Drawing.Size(95, 20);

            this.lblMinAbsences.AutoSize = true;
            this.lblMinAbsences.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblMinAbsences.ForeColor = System.Drawing.Color.FromArgb(43, 79, 88);
            this.lblMinAbsences.Location = new System.Drawing.Point(250, 15);
            this.lblMinAbsences.Text = "الحد الأدنى";

            this.NumMinAbsences.Location = new System.Drawing.Point(190, 12);
            this.NumMinAbsences.Name = "NumMinAbsences";
            this.NumMinAbsences.Size = new System.Drawing.Size(55, 20);
            this.NumMinAbsences.Minimum = 1;
            this.NumMinAbsences.Maximum = 100;
            this.NumMinAbsences.Value = 1;

            this.BtnSearch.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.BtnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSearch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSearch.ForeColor = System.Drawing.Color.White;
            this.BtnSearch.Location = new System.Drawing.Point(80, 10);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(90, 25);
            this.BtnSearch.Text = "بحث";
            this.BtnSearch.UseVisualStyleBackColor = false;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);

            // GVAbsence
            this.GVAbsence.Location = new System.Drawing.Point(2, 120);
            this.GVAbsence.MainView = this.GrdAbsence;
            this.GVAbsence.Name = "GVAbsence";
            this.GVAbsence.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GVAbsence.Size = new System.Drawing.Size(795, 590);
            this.GVAbsence.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.GrdAbsence });

            this.GrdAbsence.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.GrdAbsence.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Blue;
            this.GrdAbsence.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdAbsence.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.GrdAbsence.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdAbsence.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdAbsence.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 8F);
            this.GrdAbsence.Appearance.Row.Options.UseFont = true;
            this.GrdAbsence.Appearance.Row.Options.UseTextOptions = true;
            this.GrdAbsence.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdAbsence.ColumnPanelRowHeight = 30;
            this.GrdAbsence.GridControl = this.GVAbsence;
            this.GrdAbsence.Name = "GrdAbsence";
            this.GrdAbsence.OptionsBehavior.Editable = false;
            this.GrdAbsence.OptionsView.ColumnAutoWidth = true;
            this.GrdAbsence.OptionsView.ShowGroupPanel = false;
            this.GrdAbsence.RowHeight = 28;

            // FrmAbsenceTracking
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 720);
            this.Controls.Add(this.GVAbsence);
            this.Controls.Add(this.PnlFilter);
            this.Name = "FrmAbsenceTracking";
            this.Text = "متابعة الغياب";
            this.Load += new System.EventHandler(this.FrmAbsenceTracking_Load);
            this.PnlFilter.ResumeLayout(false);
            this.PnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumMinAbsences)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVAbsence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdAbsence)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel PnlFilter;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.ComboBox CmbClass;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker DtFrom;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker DtTo;
        private System.Windows.Forms.Label lblMinAbsences;
        private System.Windows.Forms.NumericUpDown NumMinAbsences;
        private System.Windows.Forms.Button BtnSearch;
        private DevExpress.XtraGrid.GridControl GVAbsence;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdAbsence;
    }
}
