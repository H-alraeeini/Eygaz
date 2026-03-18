namespace Eygaz
{
    partial class FrmGenerateCertificate
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
            this.grpSelection = new System.Windows.Forms.GroupBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.CmbClass = new System.Windows.Forms.ComboBox();
            this.lblStudent = new System.Windows.Forms.Label();
            this.CmbStudent = new System.Windows.Forms.ComboBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.CmbTemplate = new System.Windows.Forms.ComboBox();
            this.BtnLoadData = new System.Windows.Forms.Button();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.lblStudentNameLbl = new System.Windows.Forms.Label();
            this.txtStudentName = new System.Windows.Forms.TextBox();
            this.lblClassNameLbl = new System.Windows.Forms.Label();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.lblTeacherLbl = new System.Windows.Forms.Label();
            this.txtTeacherName = new System.Windows.Forms.TextBox();
            this.lblPctLbl = new System.Windows.Forms.Label();
            this.txtAttendPct = new System.Windows.Forms.TextBox();
            this.grpActions = new System.Windows.Forms.GroupBox();
            this.BtnPreview = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.BtnSaveRecord = new System.Windows.Forms.Button();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.grpSelection.SuspendLayout();
            this.grpInfo.SuspendLayout();
            this.grpActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSelection
            // 
            this.grpSelection.Controls.Add(this.BtnLoadData);
            this.grpSelection.Controls.Add(this.CmbTemplate);
            this.grpSelection.Controls.Add(this.lblTemplate);
            this.grpSelection.Controls.Add(this.CmbStudent);
            this.grpSelection.Controls.Add(this.lblStudent);
            this.grpSelection.Controls.Add(this.CmbClass);
            this.grpSelection.Controls.Add(this.lblClass);
            this.grpSelection.Font = new System.Drawing.Font("Tahoma", 9F);
            this.grpSelection.Location = new System.Drawing.Point(15, 65);
            this.grpSelection.Name = "grpSelection";
            this.grpSelection.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grpSelection.Size = new System.Drawing.Size(770, 70);
            this.grpSelection.TabIndex = 0;
            this.grpSelection.TabStop = false;
            this.grpSelection.Text = "اختيار البيانات";
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Location = new System.Drawing.Point(720, 30);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(40, 14);
            this.lblClass.TabIndex = 0;
            this.lblClass.Text = "الفصل:";
            // 
            // CmbClass
            // 
            this.CmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbClass.Location = new System.Drawing.Point(575, 27);
            this.CmbClass.Name = "CmbClass";
            this.CmbClass.Size = new System.Drawing.Size(140, 22);
            this.CmbClass.TabIndex = 1;
            this.CmbClass.SelectedIndexChanged += new System.EventHandler(this.CmbClass_SelectedIndexChanged);
            // 
            // lblStudent
            // 
            this.lblStudent.AutoSize = true;
            this.lblStudent.Location = new System.Drawing.Point(520, 30);
            this.lblStudent.Name = "lblStudent";
            this.lblStudent.Size = new System.Drawing.Size(46, 14);
            this.lblStudent.TabIndex = 2;
            this.lblStudent.Text = "الطالب:";
            // 
            // CmbStudent
            // 
            this.CmbStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbStudent.Location = new System.Drawing.Point(370, 27);
            this.CmbStudent.Name = "CmbStudent";
            this.CmbStudent.Size = new System.Drawing.Size(145, 22);
            this.CmbStudent.TabIndex = 3;
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.Location = new System.Drawing.Point(318, 30);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(42, 14);
            this.lblTemplate.TabIndex = 4;
            this.lblTemplate.Text = "القالب:";
            // 
            // CmbTemplate
            // 
            this.CmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbTemplate.Location = new System.Drawing.Point(165, 27);
            this.CmbTemplate.Name = "CmbTemplate";
            this.CmbTemplate.Size = new System.Drawing.Size(148, 22);
            this.CmbTemplate.TabIndex = 5;
            // 
            // BtnLoadData
            // 
            this.BtnLoadData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.BtnLoadData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLoadData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnLoadData.ForeColor = System.Drawing.Color.White;
            this.BtnLoadData.Location = new System.Drawing.Point(15, 24);
            this.BtnLoadData.Name = "BtnLoadData";
            this.BtnLoadData.Size = new System.Drawing.Size(140, 28);
            this.BtnLoadData.TabIndex = 6;
            this.BtnLoadData.Text = "توليد الشهادة";
            this.BtnLoadData.UseVisualStyleBackColor = false;
            this.BtnLoadData.Click += new System.EventHandler(this.BtnLoadData_Click);
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.txtAttendPct);
            this.grpInfo.Controls.Add(this.lblPctLbl);
            this.grpInfo.Controls.Add(this.txtTeacherName);
            this.grpInfo.Controls.Add(this.lblTeacherLbl);
            this.grpInfo.Controls.Add(this.txtClassName);
            this.grpInfo.Controls.Add(this.lblClassNameLbl);
            this.grpInfo.Controls.Add(this.txtStudentName);
            this.grpInfo.Controls.Add(this.lblStudentNameLbl);
            this.grpInfo.Font = new System.Drawing.Font("Tahoma", 9F);
            this.grpInfo.Location = new System.Drawing.Point(15, 140);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grpInfo.Size = new System.Drawing.Size(770, 55);
            this.grpInfo.TabIndex = 1;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "بيانات الشهادة";
            // 
            // lblStudentNameLbl
            // 
            this.lblStudentNameLbl.AutoSize = true;
            this.lblStudentNameLbl.Location = new System.Drawing.Point(700, 25);
            this.lblStudentNameLbl.Name = "lblStudentNameLbl";
            this.lblStudentNameLbl.Size = new System.Drawing.Size(56, 14);
            this.lblStudentNameLbl.TabIndex = 0;
            this.lblStudentNameLbl.Text = "الطالب:";
            // 
            // txtStudentName
            // 
            this.txtStudentName.Location = new System.Drawing.Point(575, 22);
            this.txtStudentName.Name = "txtStudentName";
            this.txtStudentName.ReadOnly = true;
            this.txtStudentName.Size = new System.Drawing.Size(120, 22);
            this.txtStudentName.TabIndex = 1;
            // 
            // lblClassNameLbl
            // 
            this.lblClassNameLbl.AutoSize = true;
            this.lblClassNameLbl.Location = new System.Drawing.Point(520, 25);
            this.lblClassNameLbl.Name = "lblClassNameLbl";
            this.lblClassNameLbl.Size = new System.Drawing.Size(42, 14);
            this.lblClassNameLbl.TabIndex = 2;
            this.lblClassNameLbl.Text = "الفصل:";
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(400, 22);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.ReadOnly = true;
            this.txtClassName.Size = new System.Drawing.Size(115, 22);
            this.txtClassName.TabIndex = 3;
            // 
            // lblTeacherLbl
            // 
            this.lblTeacherLbl.AutoSize = true;
            this.lblTeacherLbl.Location = new System.Drawing.Point(345, 25);
            this.lblTeacherLbl.Name = "lblTeacherLbl";
            this.lblTeacherLbl.Size = new System.Drawing.Size(48, 14);
            this.lblTeacherLbl.TabIndex = 4;
            this.lblTeacherLbl.Text = "المدرس:";
            // 
            // txtTeacherName
            // 
            this.txtTeacherName.Location = new System.Drawing.Point(220, 22);
            this.txtTeacherName.Name = "txtTeacherName";
            this.txtTeacherName.ReadOnly = true;
            this.txtTeacherName.Size = new System.Drawing.Size(120, 22);
            this.txtTeacherName.TabIndex = 5;
            // 
            // lblPctLbl
            // 
            this.lblPctLbl.AutoSize = true;
            this.lblPctLbl.Location = new System.Drawing.Point(140, 25);
            this.lblPctLbl.Name = "lblPctLbl";
            this.lblPctLbl.Size = new System.Drawing.Size(67, 14);
            this.lblPctLbl.TabIndex = 6;
            this.lblPctLbl.Text = "الحضور %:";
            // 
            // txtAttendPct
            // 
            this.txtAttendPct.Location = new System.Drawing.Point(55, 22);
            this.txtAttendPct.Name = "txtAttendPct";
            this.txtAttendPct.ReadOnly = true;
            this.txtAttendPct.Size = new System.Drawing.Size(80, 22);
            this.txtAttendPct.TabIndex = 7;
            // 
            // grpActions
            // 
            this.grpActions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right));
            this.grpActions.Controls.Add(this.BtnSaveRecord);
            this.grpActions.Controls.Add(this.BtnPrint);
            this.grpActions.Controls.Add(this.BtnPreview);
            this.grpActions.Font = new System.Drawing.Font("Tahoma", 9F);
            this.grpActions.Location = new System.Drawing.Point(15, 505);
            this.grpActions.Name = "grpActions";
            this.grpActions.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grpActions.Size = new System.Drawing.Size(770, 60);
            this.grpActions.TabIndex = 3;
            this.grpActions.TabStop = false;
            this.grpActions.Text = "العمليات";
            // 
            // BtnPreview
            // 
            this.BtnPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.BtnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPreview.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnPreview.ForeColor = System.Drawing.Color.White;
            this.BtnPreview.Location = new System.Drawing.Point(550, 20);
            this.BtnPreview.Name = "BtnPreview";
            this.BtnPreview.Size = new System.Drawing.Size(150, 30);
            this.BtnPreview.TabIndex = 0;
            this.BtnPreview.Text = "👁 معاينة الشهادة";
            this.BtnPreview.UseVisualStyleBackColor = false;
            this.BtnPreview.Click += new System.EventHandler(this.BtnPreview_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnPrint.ForeColor = System.Drawing.Color.White;
            this.BtnPrint.Location = new System.Drawing.Point(370, 20);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(150, 30);
            this.BtnPrint.TabIndex = 1;
            this.BtnPrint.Text = "🖨 طباعة PDF";
            this.BtnPrint.UseVisualStyleBackColor = false;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnSaveRecord
            // 
            this.BtnSaveRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.BtnSaveRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSaveRecord.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnSaveRecord.ForeColor = System.Drawing.Color.Black;
            this.BtnSaveRecord.Location = new System.Drawing.Point(190, 20);
            this.BtnSaveRecord.Name = "BtnSaveRecord";
            this.BtnSaveRecord.Size = new System.Drawing.Size(150, 30);
            this.BtnSaveRecord.TabIndex = 2;
            this.BtnSaveRecord.Text = "💾 حفظ السجل";
            this.BtnSaveRecord.UseVisualStyleBackColor = false;
            this.BtnSaveRecord.Click += new System.EventHandler(this.BtnSaveRecord_Click);
            // 
            // pnlPreview
            // 
            this.pnlPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPreview.BackColor = System.Drawing.Color.White;
            this.pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreview.Location = new System.Drawing.Point(15, 200);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(770, 295);
            this.pnlPreview.TabIndex = 2;
            this.pnlPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPreview_Paint);
            // 
            // FrmGenerateCertificate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 580);
            this.Controls.Add(this.pnlPreview);
            this.Controls.Add(this.grpActions);
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.grpSelection);
            this.Name = "FrmGenerateCertificate";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "إصدار الشهادات";
            this.Load += new System.EventHandler(this.FrmGenerateCertificate_Load);
            this.grpSelection.ResumeLayout(false);
            this.grpSelection.PerformLayout();
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.grpActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpSelection;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.ComboBox CmbClass;
        private System.Windows.Forms.Label lblStudent;
        private System.Windows.Forms.ComboBox CmbStudent;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.ComboBox CmbTemplate;
        private System.Windows.Forms.Button BtnLoadData;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Label lblStudentNameLbl;
        private System.Windows.Forms.TextBox txtStudentName;
        private System.Windows.Forms.Label lblClassNameLbl;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label lblTeacherLbl;
        private System.Windows.Forms.TextBox txtTeacherName;
        private System.Windows.Forms.Label lblPctLbl;
        private System.Windows.Forms.TextBox txtAttendPct;
        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.Button BtnPreview;
        private System.Windows.Forms.Button BtnPrint;
        private System.Windows.Forms.Button BtnSaveRecord;
        private System.Windows.Forms.Panel pnlPreview;
    }
}
