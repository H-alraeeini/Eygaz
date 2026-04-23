using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class FrmPermissionsManagement : MetroFramework.Forms.MetroForm
    {
        private readonly AuthService authService = new AuthService();

        public FrmPermissionsManagement()
        {
            InitializeComponent();
        }

        private void FrmPermissionsManagement_Load(object sender, EventArgs e)
        {
            if (!AuthSession.HasPermission("permissions.manage"))
            {
                MessageBox.Show("ليس لديك صلاحية إدارة الصلاحيات.");
                Close();
                return;
            }

            LoadRoles();
            LoadUsers();
        }

        private void LoadRoles()
        {

            CmbRoles.DataSource = authService.GetRoles();
            CmbRoles.DisplayMember = "RoleName";
            CmbRoles.ValueMember = "Id";
            CmbRoles.SelectedIndex = -1;
        }

        private void CmbRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbRoles.SelectedValue == null || CmbRoles.SelectedValue is DataRowView)
                return;

            int roleId = Convert.ToInt32(CmbRoles.SelectedValue);
            LoadPermissions(roleId);
        }

        private void LoadPermissions(int roleId)
        {
            DataTable dt = authService.GetRolePermissions(roleId);
            ChkPermissions.Items.Clear();
            if (dt == null) return;

            foreach (DataRow row in dt.Rows)
            {
                var item = new PermissionItem
                {
                    PermissionId = Convert.ToInt32(row["Id"]),
                    PermissionCode = row["PermissionCode"].ToString(),
                    PermissionName = row["PermissionName"].ToString()
                };
                bool granted = Convert.ToInt32(row["IsGranted"]) == 1;
                ChkPermissions.Items.Add(item, granted);
            }
        }

        private void LoadUsers()
        {
            CmbUsers.DataSource = authService.GetUsersBasic();
            CmbUsers.DisplayMember = "Username";
            CmbUsers.ValueMember = "Id";
            CmbUsers.SelectedIndex = -1;
        }

        private void BtnSaveRolePermissions_Click(object sender, EventArgs e)
        {
            if (CmbRoles.SelectedValue == null) return;
            int roleId = Convert.ToInt32(CmbRoles.SelectedValue);

            for (int i = 0; i < ChkPermissions.Items.Count; i++)
            {
                PermissionItem item = (PermissionItem)ChkPermissions.Items[i];
                bool granted = ChkPermissions.GetItemChecked(i);
                authService.SetRolePermission(roleId, item.PermissionId, granted);
            }

            MessageBox.Show("تم حفظ الصلاحيات بنجاح.");
        }

        private void CmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbUsers.SelectedValue == null || CmbUsers.SelectedValue is DataRowView) return;
            DataTable dt = authService.GetUserPermissionOverrides(Convert.ToInt32(CmbUsers.SelectedValue));
            ChkUserPermissions.Items.Clear();
            if (dt == null) return;
            foreach (DataRow row in dt.Rows)
            {
                var item = new PermissionItem
                {
                    PermissionId = Convert.ToInt32(row["Id"]),
                    PermissionCode = row["PermissionCode"].ToString(),
                    PermissionName = row["PermissionName"].ToString()
                };
                bool granted = Convert.ToInt32(row["IsGranted"]) == 1;
                ChkUserPermissions.Items.Add(item, granted);
            }
        }

        private void BtnSaveUserPermissions_Click(object sender, EventArgs e)
        {
            if (CmbUsers.SelectedValue == null) return;
            int userId = Convert.ToInt32(CmbUsers.SelectedValue);
            for (int i = 0; i < ChkUserPermissions.Items.Count; i++)
            {
                PermissionItem item = (PermissionItem)ChkUserPermissions.Items[i];
                bool granted = ChkUserPermissions.GetItemChecked(i);
                authService.SetUserPermissionOverride(userId, item.PermissionId, granted);
            }
            MessageBox.Show("تم حفظ صلاحيات المستخدم.");
        }

        private void BtnCreateRole_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNewRole.Text))
            {
                LblError.Text = "يرجى إدخال اسم الدور.";
                return;
            }

            if (!authService.CreateRole(TxtNewRole.Text.Trim()))
            {
                LblError.Text = "تعذر إنشاء الدور.";
                return;
            }

            TxtNewRole.Text = "";
            LblError.Text = "";
            LoadRoles();
            MessageBox.Show("تم إنشاء الدور.");
        }

        private class PermissionItem
        {
            public int PermissionId { get; set; }
            public string PermissionCode { get; set; }
            public string PermissionName { get; set; }
            public override string ToString()
            {
                return PermissionName + " (" + PermissionCode + ")";
            }
        }
    }
}
