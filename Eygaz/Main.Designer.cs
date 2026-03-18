
namespace Eygaz
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuInput = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTeachers = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuClasses = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSubject = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOpt = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAttendanceStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAttendanceSessions = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAbsenceTracking = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAttendanceTechers = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuCertificate = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuCertDesigner = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuGenerateCert = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuReport = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAttandRep = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDashboard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuInput,
            this.MenuOpt,
            this.MenuReport,
            this.MenuDashboard});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(830, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuInput
            // 
            this.MenuInput.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStudent,
            this.MenuTeachers,
            this.MenuClasses,
            this.MenuSubject});
            this.MenuInput.Name = "MenuInput";
            this.MenuInput.Size = new System.Drawing.Size(64, 20);
            this.MenuInput.Text = "المدخلات";
            // 
            // MenuStudent
            // 
            this.MenuStudent.Name = "MenuStudent";
            this.MenuStudent.Size = new System.Drawing.Size(160, 22);
            this.MenuStudent.Text = "الطلاب";
            this.MenuStudent.Click += new System.EventHandler(this.MenuStudent_Click);
            // 
            // MenuTeachers
            // 
            this.MenuTeachers.Name = "MenuTeachers";
            this.MenuTeachers.Size = new System.Drawing.Size(160, 22);
            this.MenuTeachers.Text = "المدرسين";
            this.MenuTeachers.Click += new System.EventHandler(this.MenuTeachers_Click);
            // 
            // MenuClasses
            // 
            this.MenuClasses.Name = "MenuClasses";
            this.MenuClasses.Size = new System.Drawing.Size(160, 22);
            this.MenuClasses.Text = "الفصول والحلقات";
            this.MenuClasses.Click += new System.EventHandler(this.MenuClasses_Click);
            // 
            // MenuSubject
            // 
            this.MenuSubject.Name = "MenuSubject";
            this.MenuSubject.Size = new System.Drawing.Size(160, 22);
            this.MenuSubject.Text = "المواد";
            this.MenuSubject.Click += new System.EventHandler(this.MenuSubject_Click);
            // 
            // MenuOpt
            // 
            this.MenuOpt.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAttendanceStudent,
            this.MenuAttendanceSessions,
            this.MenuAbsenceTracking,
            this.MenuAttendanceTechers,
            this.MenuCertificate,
            this.MenuCertDesigner,
            this.MenuGenerateCert});
            this.MenuOpt.Name = "MenuOpt";
            this.MenuOpt.Size = new System.Drawing.Size(62, 20);
            this.MenuOpt.Text = "العمليات";
            // 
            // MenuAttendanceStudent
            // 
            this.MenuAttendanceStudent.Name = "MenuAttendanceStudent";
            this.MenuAttendanceStudent.Size = new System.Drawing.Size(230, 22);
            this.MenuAttendanceStudent.Text = "سجل الحضور والغياب للطلاب";
            this.MenuAttendanceStudent.Click += new System.EventHandler(this.MenuAttendanceStudent_Click);
            // 
            // MenuAttendanceSessions
            // 
            this.MenuAttendanceSessions.Name = "MenuAttendanceSessions";
            this.MenuAttendanceSessions.Size = new System.Drawing.Size(230, 22);
            this.MenuAttendanceSessions.Text = "إدارة جلسات الحضور";
            this.MenuAttendanceSessions.Click += new System.EventHandler(this.MenuAttendanceSessions_Click);
            // 
            // MenuAbsenceTracking
            // 
            this.MenuAbsenceTracking.Name = "MenuAbsenceTracking";
            this.MenuAbsenceTracking.Size = new System.Drawing.Size(230, 22);
            this.MenuAbsenceTracking.Text = "متابعة الغياب";
            this.MenuAbsenceTracking.Click += new System.EventHandler(this.MenuAbsenceTracking_Click);
            // 
            // MenuAttendanceTechers
            // 
            this.MenuAttendanceTechers.Name = "MenuAttendanceTechers";
            this.MenuAttendanceTechers.Size = new System.Drawing.Size(230, 22);
            this.MenuAttendanceTechers.Text = "سجل الحضور والغياب للمدرسين";
            this.MenuAttendanceTechers.Click += new System.EventHandler(this.MenuAttendanceTechers_Click);
            // 
            // MenuCertificate
            // 
            this.MenuCertificate.Name = "MenuCertificate";
            this.MenuCertificate.Size = new System.Drawing.Size(230, 22);
            this.MenuCertificate.Text = "الشهادات";
            this.MenuCertificate.Click += new System.EventHandler(this.MenuCertificate_Click);
            // 
            // MenuCertDesigner
            // 
            this.MenuCertDesigner.Name = "MenuCertDesigner";
            this.MenuCertDesigner.Size = new System.Drawing.Size(230, 22);
            this.MenuCertDesigner.Text = "مصمم الشهادات";
            this.MenuCertDesigner.Click += new System.EventHandler(this.MenuCertDesigner_Click);
            // 
            // MenuGenerateCert
            // 
            this.MenuGenerateCert.Name = "MenuGenerateCert";
            this.MenuGenerateCert.Size = new System.Drawing.Size(230, 22);
            this.MenuGenerateCert.Text = "إصدار شهادة بقالب";
            this.MenuGenerateCert.Click += new System.EventHandler(this.MenuGenerateCert_Click);
            // 
            // MenuReport
            // 
            this.MenuReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAttandRep});
            this.MenuReport.Name = "MenuReport";
            this.MenuReport.Size = new System.Drawing.Size(57, 20);
            this.MenuReport.Text = "التقارير ";
            // 
            // MenuAttandRep
            // 
            this.MenuAttandRep.Name = "MenuAttandRep";
            this.MenuAttandRep.Size = new System.Drawing.Size(183, 22);
            this.MenuAttandRep.Text = "تقارير الحضور والغياب";
            this.MenuAttandRep.Click += new System.EventHandler(this.MenuAttandRep_Click);
            // 
            // MenuDashboard
            // 
            this.MenuDashboard.Name = "MenuDashboard";
            this.MenuDashboard.Size = new System.Drawing.Size(83, 20);
            this.MenuDashboard.Text = "لوحة المتابعة";
            this.MenuDashboard.Click += new System.EventHandler(this.MenuDashboard_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 458);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "الرئيسية";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuInput;
        private System.Windows.Forms.ToolStripMenuItem MenuStudent;
        private System.Windows.Forms.ToolStripMenuItem MenuTeachers;
        private System.Windows.Forms.ToolStripMenuItem MenuClasses;
        private System.Windows.Forms.ToolStripMenuItem MenuSubject;
        private System.Windows.Forms.ToolStripMenuItem MenuOpt;
        private System.Windows.Forms.ToolStripMenuItem MenuAttendanceStudent;
        private System.Windows.Forms.ToolStripMenuItem MenuReport;
        private System.Windows.Forms.ToolStripMenuItem MenuAttandRep;
        private System.Windows.Forms.ToolStripMenuItem MenuCertificate;
        private System.Windows.Forms.ToolStripMenuItem MenuCertDesigner;
        private System.Windows.Forms.ToolStripMenuItem MenuGenerateCert;
        private System.Windows.Forms.ToolStripMenuItem MenuAttendanceTechers;
        private System.Windows.Forms.ToolStripMenuItem MenuAttendanceSessions;
        private System.Windows.Forms.ToolStripMenuItem MenuAbsenceTracking;
        private System.Windows.Forms.ToolStripMenuItem MenuDashboard;
    }
}