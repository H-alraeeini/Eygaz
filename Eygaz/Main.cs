using System;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class Main : Form
    {
        FrmStudent frmStudent;
        FrmTeachers frmteachers;
        FrmClassess frmclassess;
        FrmSubject frmsubject;
        FrmAttendanceStud frmattendanceStud;
        FrmAttendanceSessions frmattendanceSessions;
        FrmAttendanceReports frmattendanceReports;
        FrmAbsenceTracking frmabsenceTracking;
        FrmDashboard frmdashboard;
        FrmStudentCertificates frmcertificates;
        FrmTeacherAttendance frmteacherAttendance;
        FrmCertificateDesigner frmcertDesigner;
        FrmGenerateCertificate frmgenerateCert;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Func.dbname = Application.StartupPath + @"\EygazDb.db";
        }

        private void MenuStudent_Click(object sender, EventArgs e)
        {
            if (frmStudent == null || frmStudent.IsDisposed)
            {
                frmStudent = new FrmStudent();
                frmStudent.MdiParent = this;
                frmStudent.Show();
            }
            else
            {
                frmStudent.BringToFront();
            }
        }

        private void MenuTeachers_Click(object sender, EventArgs e)
        {
            if (frmteachers == null || frmteachers.IsDisposed)
            {
                frmteachers = new FrmTeachers();
                frmteachers.MdiParent = this;
                frmteachers.Show();
            }
            else
            {
                frmteachers.BringToFront();
            }
        }

        private void MenuClasses_Click(object sender, EventArgs e)
        {
            if (frmclassess == null || frmclassess.IsDisposed)
            {
                frmclassess = new FrmClassess();
                frmclassess.MdiParent = this;
                frmclassess.Show();
            }
            else
            {
                frmclassess.BringToFront();
            }
        }

        private void MenuSubject_Click(object sender, EventArgs e)
        {
            if (frmsubject == null || frmsubject.IsDisposed)
            {
                frmsubject = new FrmSubject();
                frmsubject.MdiParent = this;
                frmsubject.Show();
            }
            else
            {
                frmsubject.BringToFront();
            }
        }

        private void MenuAttendanceStudent_Click(object sender, EventArgs e)
        {
            if (frmattendanceStud == null || frmattendanceStud.IsDisposed)
            {
                frmattendanceStud = new FrmAttendanceStud();
                frmattendanceStud.MdiParent = this;
                frmattendanceStud.Show();
            }
            else
            {
                frmattendanceStud.BringToFront();
            }
        }

        private void MenuAttendanceSessions_Click(object sender, EventArgs e)
        {
            if (frmattendanceSessions == null || frmattendanceSessions.IsDisposed)
            {
                frmattendanceSessions = new FrmAttendanceSessions();
                frmattendanceSessions.MdiParent = this;
                frmattendanceSessions.Show();
            }
            else
            {
                frmattendanceSessions.BringToFront();
            }
        }

        private void MenuAttandRep_Click(object sender, EventArgs e)
        {
            if (frmattendanceReports == null || frmattendanceReports.IsDisposed)
            {
                frmattendanceReports = new FrmAttendanceReports();
                frmattendanceReports.MdiParent = this;
                frmattendanceReports.Show();
            }
            else
            {
                frmattendanceReports.BringToFront();
            }
        }

        private void MenuAbsenceTracking_Click(object sender, EventArgs e)
        {
            if (frmabsenceTracking == null || frmabsenceTracking.IsDisposed)
            {
                frmabsenceTracking = new FrmAbsenceTracking();
                frmabsenceTracking.MdiParent = this;
                frmabsenceTracking.Show();
            }
            else
            {
                frmabsenceTracking.BringToFront();
            }
        }

        private void MenuDashboard_Click(object sender, EventArgs e)
        {
            if (frmdashboard == null || frmdashboard.IsDisposed)
            {
                frmdashboard = new FrmDashboard();
                frmdashboard.MdiParent = this;
                frmdashboard.Show();
            }
            else
            {
                frmdashboard.BringToFront();
            }
        }

        private void MenuCertificate_Click(object sender, EventArgs e)
        {
            if (frmcertificates == null || frmcertificates.IsDisposed)
            {
                frmcertificates = new FrmStudentCertificates();
                frmcertificates.MdiParent = this;
                frmcertificates.Show();
            }
            else
            {
                frmcertificates.BringToFront();
            }
        }

        private void MenuAttendanceTechers_Click(object sender, EventArgs e)
        {
            if (frmteacherAttendance == null || frmteacherAttendance.IsDisposed)
            {
                frmteacherAttendance = new FrmTeacherAttendance();
                //frmteacherAttendance.MdiParent = this;
                frmteacherAttendance.Show();
            }
            else
            {
                frmteacherAttendance.BringToFront();
            }
        }

        private void MenuCertDesigner_Click(object sender, EventArgs e)
        {
            if (frmcertDesigner == null || frmcertDesigner.IsDisposed)
            {
                frmcertDesigner = new FrmCertificateDesigner();
                frmcertDesigner.MdiParent = this;
                frmcertDesigner.Show();
            }
            else
            {
                frmcertDesigner.BringToFront();
            }
        }

        private void MenuGenerateCert_Click(object sender, EventArgs e)
        {
            if (frmgenerateCert == null || frmgenerateCert.IsDisposed)
            {
                frmgenerateCert = new FrmGenerateCertificate();
                frmgenerateCert.MdiParent = this;
                frmgenerateCert.Show();
            }
            else
            {
                frmgenerateCert.BringToFront();
            }
        }
    }
}

