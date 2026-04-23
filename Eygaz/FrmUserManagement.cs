using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class FrmUserManagement : MetroFramework.Forms.MetroForm
    {
        private readonly AuthService authService = new AuthService();

        public FrmUserManagement()
        {
            InitializeComponent();
        }

        private void FrmUserManagement_Load(object sender, EventArgs e)
        {
            if (!AuthSession.HasPermission("users.view"))
            {
                MessageBox.Show("ليس لديك صلاحية عرض المستخدمين.");
                Close();
                return;
            }

            LoadRoles();
            LoadUsers();

            bool canCreate = AuthSession.HasPermission("users.create");
            BtnCreate.Enabled = canCreate;
            if (!canCreate)
                LblError.Text = "إنشاء المستخدمين مسموح فقط للمستخدم Admin/مشرف.";
        }

        private void LoadRoles()
        {
            DataTable roles = authService.GetRoles();
            CmbRole.DataSource = roles;
            CmbRole.DisplayMember = "RoleName";
            CmbRole.ValueMember = "Id";
            CmbRole.SelectedIndex = -1;
        }

        private void LoadUsers()
        {
            DataTable users = authService.GetAllUsers();
            GVUsers.DataSource = users;
            GrdUsers.BestFitColumns();
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            LblError.Text = "";

            if (!AuthSession.HasPermission("users.create"))
            {
                LblError.Text = "ليس لديك صلاحية إنشاء المستخدمين.";
                return;
            }

            if (TxtPassword.Text != TxtConfirm.Text)
            {
                LblError.Text = "تأكيد كلمة المرور غير مطابق.";
                return;
            }

            if (CmbRole.SelectedValue == null)
            {
                LblError.Text = "يرجى اختيار الدور/الصلاحية.";
                return;
            }

            bool created = authService.CreateUser(
                TxtUsername.Text.Trim(),
                TxtDisplayName.Text.Trim(),
                TxtEmail.Text.Trim(),
                TxtPassword.Text,
                Convert.ToInt32(CmbRole.SelectedValue),
                out string error);

            if (!created)
            {
                LblError.Text = error;
                return;
            }

            MessageBox.Show("تم إنشاء المستخدم بنجاح.");
            TxtUsername.Text = "";
            TxtDisplayName.Text = "";
            TxtEmail.Text = "";
            TxtPassword.Text = "";
            TxtConfirm.Text = "";
            CmbRole.SelectedIndex = -1;
            LoadUsers();
        }
    }
}

