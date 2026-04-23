namespace Eygaz
{
    partial class FrmGradeEntry
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
            this.CmbSubject = new System.Windows.Forms.ComboBox();
            this.CmbTerm = new System.Windows.Forms.ComboBox();
            this.DtExamDate = new System.Windows.Forms.DateTimePicker();
            this.TxtMaxScore = new System.Windows.Forms.TextBox();
            this.TxtDescription = new System.Windows.Forms.TextBox();
            this.BtnLoadStudents = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.LblStatus = new System.Windows.Forms.Label();
            this.GVStudents = new DevExpress.XtraGrid.GridControl();
            this.GrdStudents = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.GVStudents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // CmbClass
            // 
            this.CmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbClass.Location = new System.Drawing.Point(620, 63);
            this.CmbClass.Name = "CmbClass";
            this.CmbClass.Size = new System.Drawing.Size(140, 21);
            this.CmbClass.TabIndex = 0;
            // 
            // CmbSubject
            // 
            this.CmbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSubject.Location = new System.Drawing.Point(474, 63);
            this.CmbSubject.Name = "CmbSubject";
            this.CmbSubject.Size = new System.Drawing.Size(140, 21);
            this.CmbSubject.TabIndex = 1;
            // 
            // CmbTerm
            // 
            this.CmbTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbTerm.Location = new System.Drawing.Point(328, 63);
            this.CmbTerm.Name = "CmbTerm";
            this.CmbTerm.Size = new System.Drawing.Size(140, 21);
            this.CmbTerm.TabIndex = 2;
            // 
            // DtExamDate
            // 
            this.DtExamDate.CustomFormat = "yyyy-MM-dd";
            this.DtExamDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtExamDate.Location = new System.Drawing.Point(222, 63);
            this.DtExamDate.Name = "DtExamDate";
            this.DtExamDate.Size = new System.Drawing.Size(100, 20);
            this.DtExamDate.TabIndex = 3;
            // 
            // TxtMaxScore
            // 
            this.TxtMaxScore.Location = new System.Drawing.Point(146, 63);
            this.TxtMaxScore.Name = "TxtMaxScore";
            this.TxtMaxScore.Size = new System.Drawing.Size(70, 20);
            this.TxtMaxScore.TabIndex = 4;
            // 
            // TxtDescription
            // 
            this.TxtDescription.Location = new System.Drawing.Point(23, 63);
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(117, 20);
            this.TxtDescription.TabIndex = 5;
            // 
            // BtnLoadStudents
            // 
            this.BtnLoadStudents.Location = new System.Drawing.Point(620, 90);
            this.BtnLoadStudents.Name = "BtnLoadStudents";
            this.BtnLoadStudents.Size = new System.Drawing.Size(140, 24);
            this.BtnLoadStudents.TabIndex = 6;
            this.BtnLoadStudents.Text = "تحميل الطلاب";
            this.BtnLoadStudents.UseVisualStyleBackColor = true;
            this.BtnLoadStudents.Click += new System.EventHandler(this.BtnLoadStudents_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(474, 90);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(140, 24);
            this.BtnSave.TabIndex = 7;
            this.BtnSave.Text = "حفظ الدرجات";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // LblStatus
            // 
            this.LblStatus.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.LblStatus.Location = new System.Drawing.Point(23, 90);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblStatus.Size = new System.Drawing.Size(445, 24);
            this.LblStatus.TabIndex = 9;
            // 
            // GVStudents
            // 
            this.GVStudents.Location = new System.Drawing.Point(23, 120);
            this.GVStudents.MainView = this.GrdStudents;
            this.GVStudents.Name = "GVStudents";
            this.GVStudents.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GVStudents.Size = new System.Drawing.Size(737, 360);
            this.GVStudents.TabIndex = 8;
            this.GVStudents.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdStudents});
            // 
            // GrdStudents
            // 
            this.GrdStudents.GridControl = this.GVStudents;
            this.GrdStudents.Name = "GrdStudents";
            this.GrdStudents.OptionsView.ShowGroupPanel = false;
            // 
            // FrmGradeEntry
            // 
            this.ClientSize = new System.Drawing.Size(785, 503);
            this.Controls.Add(this.GVStudents);
            this.Controls.Add(this.LblStatus);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnLoadStudents);
            this.Controls.Add(this.TxtDescription);
            this.Controls.Add(this.TxtMaxScore);
            this.Controls.Add(this.DtExamDate);
            this.Controls.Add(this.CmbTerm);
            this.Controls.Add(this.CmbSubject);
            this.Controls.Add(this.CmbClass);
            this.Name = "FrmGradeEntry";
            this.Text = "إدخال الدرجات";
            this.Load += new System.EventHandler(this.FrmGradeEntry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GVStudents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdStudents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox CmbClass;
        private System.Windows.Forms.ComboBox CmbSubject;
        private System.Windows.Forms.ComboBox CmbTerm;
        private System.Windows.Forms.DateTimePicker DtExamDate;
        private System.Windows.Forms.TextBox TxtMaxScore;
        private System.Windows.Forms.TextBox TxtDescription;
        private System.Windows.Forms.Button BtnLoadStudents;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Label LblStatus;
        private DevExpress.XtraGrid.GridControl GVStudents;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdStudents;
    }
}
