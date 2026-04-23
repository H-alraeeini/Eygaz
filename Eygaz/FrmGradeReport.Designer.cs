namespace Eygaz
{
    partial class FrmGradeReport
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.CmbClass = new System.Windows.Forms.ComboBox();
            this.CmbStudent = new System.Windows.Forms.ComboBox();
            this.CmbSubject = new System.Windows.Forms.ComboBox();
            this.CmbTerm = new System.Windows.Forms.ComboBox();
            this.BtnGenerate = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.GVReport = new DevExpress.XtraGrid.GridControl();
            this.GrdReport = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.GVReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdReport)).BeginInit();
            this.SuspendLayout();
            // 
            // CmbClass
            // 
            this.CmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbClass.Location = new System.Drawing.Point(613, 63);
            this.CmbClass.Name = "CmbClass";
            this.CmbClass.Size = new System.Drawing.Size(160, 21);
            this.CmbClass.TabIndex = 0;
            this.CmbClass.SelectedIndexChanged += new System.EventHandler(this.CmbClass_SelectedIndexChanged);
            // 
            // CmbStudent
            // 
            this.CmbStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbStudent.Location = new System.Drawing.Point(447, 63);
            this.CmbStudent.Name = "CmbStudent";
            this.CmbStudent.Size = new System.Drawing.Size(160, 21);
            this.CmbStudent.TabIndex = 1;
            // 
            // CmbSubject
            // 
            this.CmbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSubject.Location = new System.Drawing.Point(281, 63);
            this.CmbSubject.Name = "CmbSubject";
            this.CmbSubject.Size = new System.Drawing.Size(160, 21);
            this.CmbSubject.TabIndex = 2;
            // 
            // CmbTerm
            // 
            this.CmbTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbTerm.Location = new System.Drawing.Point(115, 63);
            this.CmbTerm.Name = "CmbTerm";
            this.CmbTerm.Size = new System.Drawing.Size(160, 21);
            this.CmbTerm.TabIndex = 3;
            // 
            // BtnGenerate
            // 
            this.BtnGenerate.Location = new System.Drawing.Point(23, 62);
            this.BtnGenerate.Name = "BtnGenerate";
            this.BtnGenerate.Size = new System.Drawing.Size(86, 24);
            this.BtnGenerate.TabIndex = 4;
            this.BtnGenerate.Text = "عرض";
            this.BtnGenerate.UseVisualStyleBackColor = true;
            this.BtnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Location = new System.Drawing.Point(23, 90);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(86, 24);
            this.BtnPrint.TabIndex = 5;
            this.BtnPrint.Text = "طباعة";
            this.BtnPrint.UseVisualStyleBackColor = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // GVReport
            // 
            this.GVReport.Location = new System.Drawing.Point(23, 120);
            this.GVReport.MainView = this.GrdReport;
            this.GVReport.Name = "GVReport";
            this.GVReport.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GVReport.Size = new System.Drawing.Size(750, 370);
            this.GVReport.TabIndex = 6;
            this.GVReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdReport});
            // 
            // GrdReport
            // 
            this.GrdReport.GridControl = this.GVReport;
            this.GrdReport.Name = "GrdReport";
            this.GrdReport.OptionsBehavior.Editable = false;
            this.GrdReport.OptionsView.ShowGroupPanel = false;
            // 
            // FrmGradeReport
            // 
            this.ClientSize = new System.Drawing.Size(799, 513);
            this.Controls.Add(this.GVReport);
            this.Controls.Add(this.BtnPrint);
            this.Controls.Add(this.BtnGenerate);
            this.Controls.Add(this.CmbTerm);
            this.Controls.Add(this.CmbSubject);
            this.Controls.Add(this.CmbStudent);
            this.Controls.Add(this.CmbClass);
            this.Name = "FrmGradeReport";
            this.Text = "كشف الدرجات";
            this.Load += new System.EventHandler(this.FrmGradeReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GVReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdReport)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ComboBox CmbClass;
        private System.Windows.Forms.ComboBox CmbStudent;
        private System.Windows.Forms.ComboBox CmbSubject;
        private System.Windows.Forms.ComboBox CmbTerm;
        private System.Windows.Forms.Button BtnGenerate;
        private System.Windows.Forms.Button BtnPrint;
        private DevExpress.XtraGrid.GridControl GVReport;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdReport;
    }
}
