using System;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class FrmDashboard : MetroFramework.Forms.MetroForm
    {
        AttendanceHelper helper = new AttendanceHelper();

        public FrmDashboard()
        {
            InitializeComponent();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            LoadDashboard();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            try
            {
                string today = DateTime.Today.ToString("yyyy-MM-dd");

                lblStudentsCount.Text = helper.GetTotalStudents().ToString();
                lblTeachersCount.Text = helper.GetTotalTeachers().ToString();
                lblSessionsCount.Text = helper.GetTodaySessions(today).ToString();
                lblPresentCount.Text = helper.GetTodayPresent(today).ToString();
                lblAbsentCount.Text = helper.GetTodayAbsent(today).ToString();
                lblLateCount.Text = helper.GetTodayLate(today).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء تحميل البيانات: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
