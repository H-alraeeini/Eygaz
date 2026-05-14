namespace Eygaz
{
    partial class FrmStudentReport
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PnlFilter = new System.Windows.Forms.Panel();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.BtnGenerate = new System.Windows.Forms.Button();
            this.TxtStudentName = new System.Windows.Forms.TextBox();
            this.lblStudentName = new System.Windows.Forms.Label();
            this.CmbClass = new System.Windows.Forms.ComboBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.GVReport = new DevExpress.XtraGrid.GridControl();
            this.GrdReport = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.PnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdReport)).BeginInit();
            this.SuspendLayout();
            // 
            // PnlFilter
            // 
            this.PnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlFilter.Controls.Add(this.BtnPrint);
            this.PnlFilter.Controls.Add(this.BtnExport);
            this.PnlFilter.Controls.Add(this.BtnGenerate);
            this.PnlFilter.Controls.Add(this.TxtStudentName);
            this.PnlFilter.Controls.Add(this.lblStudentName);
            this.PnlFilter.Controls.Add(this.CmbClass);
            this.PnlFilter.Controls.Add(this.lblClass);
            this.PnlFilter.Location = new System.Drawing.Point(11, 63);
            this.PnlFilter.Name = "PnlFilter";
            this.PnlFilter.Size = new System.Drawing.Size(780, 65);
            this.PnlFilter.TabIndex = 0;
            // 
            // BtnPrint
            // 
            this.BtnPrint.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrint.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnPrint.ForeColor = System.Drawing.Color.White;
            this.BtnPrint.Location = new System.Drawing.Point(19, 18);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(90, 25);
            this.BtnPrint.TabIndex = 6;
            this.BtnPrint.Text = "طباعة";
            this.BtnPrint.UseVisualStyleBackColor = false;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.BackColor = System.Drawing.Color.DarkOrange;
            this.BtnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExport.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnExport.ForeColor = System.Drawing.Color.White;
            this.BtnExport.Location = new System.Drawing.Point(115, 18);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(110, 25);
            this.BtnExport.TabIndex = 5;
            this.BtnExport.Text = "تصدير Excel";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnGenerate
            // 
            this.BtnGenerate.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.BtnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGenerate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnGenerate.ForeColor = System.Drawing.Color.White;
            this.BtnGenerate.Location = new System.Drawing.Point(231, 18);
            this.BtnGenerate.Name = "BtnGenerate";
            this.BtnGenerate.Size = new System.Drawing.Size(100, 25);
            this.BtnGenerate.TabIndex = 4;
            this.BtnGenerate.Text = "عرض التقرير";
            this.BtnGenerate.UseVisualStyleBackColor = false;
            this.BtnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // TxtStudentName
            // 
            this.TxtStudentName.Location = new System.Drawing.Point(379, 21);
            this.TxtStudentName.Name = "TxtStudentName";
            this.TxtStudentName.Size = new System.Drawing.Size(145, 20);
            this.TxtStudentName.TabIndex = 3;
            // 
            // lblStudentName
            // 
            this.lblStudentName.AutoSize = true;
            this.lblStudentName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblStudentName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(79)))), ((int)(((byte)(88)))));
            this.lblStudentName.Location = new System.Drawing.Point(530, 24);
            this.lblStudentName.Name = "lblStudentName";
            this.lblStudentName.Size = new System.Drawing.Size(73, 13);
            this.lblStudentName.TabIndex = 2;
            this.lblStudentName.Text = "اسم الطالب:";
            // 
            // CmbClass
            // 
            this.CmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbClass.FormattingEnabled = true;
            this.CmbClass.Location = new System.Drawing.Point(623, 21);
            this.CmbClass.Name = "CmbClass";
            this.CmbClass.Size = new System.Drawing.Size(100, 21);
            this.CmbClass.TabIndex = 1;
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(79)))), ((int)(((byte)(88)))));
            this.lblClass.Location = new System.Drawing.Point(729, 24);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(43, 13);
            this.lblClass.TabIndex = 0;
            this.lblClass.Text = "الفصل:";
            // 
            // GVReport
            // 
            this.GVReport.Location = new System.Drawing.Point(11, 134);
            this.GVReport.MainView = this.GrdReport;
            this.GVReport.Name = "GVReport";
            this.GVReport.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GVReport.Size = new System.Drawing.Size(780, 526);
            this.GVReport.TabIndex = 1;
            this.GVReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdReport});
            // 
            // GrdReport
            // 
            this.GrdReport.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdReport.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Blue;
            this.GrdReport.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdReport.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.GrdReport.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdReport.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdReport.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 8F);
            this.GrdReport.Appearance.Row.Options.UseFont = true;
            this.GrdReport.Appearance.Row.Options.UseTextOptions = true;
            this.GrdReport.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdReport.GridControl = this.GVReport;
            this.GrdReport.Name = "GrdReport";
            this.GrdReport.OptionsBehavior.Editable = false;
            this.GrdReport.OptionsView.ShowGroupPanel = false;
            this.GrdReport.AppearancePrint.Row.Font = new System.Drawing.Font("Tahoma", 8F);
            this.GrdReport.AppearancePrint.Row.Options.UseFont = true;
            this.GrdReport.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.GrdReport.AppearancePrint.HeaderPanel.Options.UseFont = true;
            // 
            // FrmStudentReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 672);
            this.Controls.Add(this.GVReport);
            this.Controls.Add(this.PnlFilter);
            this.Name = "FrmStudentReport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "تقرير الطلاب والفصول";
            this.Load += new System.EventHandler(this.FrmStudentReport_Load);
            this.PnlFilter.ResumeLayout(false);
            this.PnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlFilter;
        private System.Windows.Forms.Button BtnPrint;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.Button BtnGenerate;
        private System.Windows.Forms.TextBox TxtStudentName;
        private System.Windows.Forms.Label lblStudentName;
        private System.Windows.Forms.ComboBox CmbClass;
        private System.Windows.Forms.Label lblClass;
        private DevExpress.XtraGrid.GridControl GVReport;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdReport;
    }
}