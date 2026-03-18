namespace Eygaz
{
    partial class FrmTeacherAttendance
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
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.AttendDate = new System.Windows.Forms.DateTimePicker();
            this.BtnLoadTeachers = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnMarkAllPresent = new System.Windows.Forms.Button();
            this.BtnMarkAllAbsent = new System.Windows.Forms.Button();
            this.GVTeacherAttend = new DevExpress.XtraGrid.GridControl();
            this.GrdTeacherAttend = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnSendWhatsApp = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.GVTeacherAttend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdTeacherAttend)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(617, 33);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(51, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "التاريخ:";
            // 
            // AttendDate
            // 
            this.AttendDate.CustomFormat = "yyyy-MM-dd";
            this.AttendDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.AttendDate.Location = new System.Drawing.Point(427, 30);
            this.AttendDate.Name = "AttendDate";
            this.AttendDate.RightToLeftLayout = true;
            this.AttendDate.Size = new System.Drawing.Size(180, 22);
            this.AttendDate.TabIndex = 1;
            // 
            // BtnLoadTeachers
            // 
            this.BtnLoadTeachers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.BtnLoadTeachers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLoadTeachers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnLoadTeachers.ForeColor = System.Drawing.Color.White;
            this.BtnLoadTeachers.Location = new System.Drawing.Point(275, 26);
            this.BtnLoadTeachers.Name = "BtnLoadTeachers";
            this.BtnLoadTeachers.Size = new System.Drawing.Size(140, 30);
            this.BtnLoadTeachers.TabIndex = 2;
            this.BtnLoadTeachers.Text = "تحميل المدرسين";
            this.BtnLoadTeachers.UseVisualStyleBackColor = false;
            this.BtnLoadTeachers.Click += new System.EventHandler(this.BtnLoadTeachers_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnSave.ForeColor = System.Drawing.Color.White;
            this.BtnSave.Location = new System.Drawing.Point(15, 20);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(130, 35);
            this.BtnSave.TabIndex = 3;
            this.BtnSave.Text = "💾 حفظ الحضور";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnMarkAllPresent
            // 
            this.BtnMarkAllPresent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.BtnMarkAllPresent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMarkAllPresent.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnMarkAllPresent.ForeColor = System.Drawing.Color.White;
            this.BtnMarkAllPresent.Location = new System.Drawing.Point(155, 20);
            this.BtnMarkAllPresent.Name = "BtnMarkAllPresent";
            this.BtnMarkAllPresent.Size = new System.Drawing.Size(140, 35);
            this.BtnMarkAllPresent.TabIndex = 4;
            this.BtnMarkAllPresent.Text = "✅ الكل حاضر";
            this.BtnMarkAllPresent.UseVisualStyleBackColor = false;
            this.BtnMarkAllPresent.Click += new System.EventHandler(this.BtnMarkAllPresent_Click);
            // 
            // BtnMarkAllAbsent
            // 
            this.BtnMarkAllAbsent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.BtnMarkAllAbsent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMarkAllAbsent.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnMarkAllAbsent.ForeColor = System.Drawing.Color.White;
            this.BtnMarkAllAbsent.Location = new System.Drawing.Point(305, 20);
            this.BtnMarkAllAbsent.Name = "BtnMarkAllAbsent";
            this.BtnMarkAllAbsent.Size = new System.Drawing.Size(140, 35);
            this.BtnMarkAllAbsent.TabIndex = 5;
            this.BtnMarkAllAbsent.Text = "❌ الكل غائب";
            this.BtnMarkAllAbsent.UseVisualStyleBackColor = false;
            this.BtnMarkAllAbsent.Click += new System.EventHandler(this.BtnMarkAllAbsent_Click);
            // 
            // GVTeacherAttend
            // 
            this.GVTeacherAttend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GVTeacherAttend.Location = new System.Drawing.Point(15, 70);
            this.GVTeacherAttend.MainView = this.GrdTeacherAttend;
            this.GVTeacherAttend.Name = "GVTeacherAttend";
            this.GVTeacherAttend.Size = new System.Drawing.Size(655, 305);
            this.GVTeacherAttend.TabIndex = 6;
            this.GVTeacherAttend.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdTeacherAttend});
            // 
            // GrdTeacherAttend
            // 
            this.GrdTeacherAttend.GridControl = this.GVTeacherAttend;
            this.GrdTeacherAttend.Name = "GrdTeacherAttend";
            this.GrdTeacherAttend.OptionsView.ShowGroupPanel = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BtnSendWhatsApp);
            this.groupBox1.Controls.Add(this.BtnSave);
            this.groupBox1.Controls.Add(this.BtnMarkAllPresent);
            this.groupBox1.Controls.Add(this.BtnMarkAllAbsent);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.groupBox1.Location = new System.Drawing.Point(15, 385);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(655, 65);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "العمليات";
            // 
            // BtnSendWhatsApp
            // 
            this.BtnSendWhatsApp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(211)))), ((int)(((byte)(102)))));
            this.BtnSendWhatsApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSendWhatsApp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnSendWhatsApp.ForeColor = System.Drawing.Color.White;
            this.BtnSendWhatsApp.Location = new System.Drawing.Point(455, 20);
            this.BtnSendWhatsApp.Name = "BtnSendWhatsApp";
            this.BtnSendWhatsApp.Size = new System.Drawing.Size(185, 35);
            this.BtnSendWhatsApp.TabIndex = 6;
            this.BtnSendWhatsApp.Text = "📱 إرسال إشعار واتساب";
            this.BtnSendWhatsApp.UseVisualStyleBackColor = false;
            this.BtnSendWhatsApp.Click += new System.EventHandler(this.BtnSendWhatsApp_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(685, 380);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // FrmTeacherAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 465);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.AttendDate);
            this.Controls.Add(this.BtnLoadTeachers);
            this.Controls.Add(this.GVTeacherAttend);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmTeacherAttendance";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "سجل حضور وغياب المدرسين";
            this.Load += new System.EventHandler(this.FrmTeacherAttendance_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmTeacherAttendance_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.GVTeacherAttend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdTeacherAttend)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.DateTimePicker AttendDate;
        private System.Windows.Forms.Button BtnLoadTeachers;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnMarkAllPresent;
        private System.Windows.Forms.Button BtnMarkAllAbsent;
        private DevExpress.XtraGrid.GridControl GVTeacherAttend;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdTeacherAttend;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnSendWhatsApp;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}
