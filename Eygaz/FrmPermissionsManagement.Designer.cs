namespace Eygaz
{
    partial class FrmPermissionsManagement
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.CmbRoles = new System.Windows.Forms.ComboBox();
            this.ChkPermissions = new System.Windows.Forms.CheckedListBox();
            this.BtnSaveRolePermissions = new System.Windows.Forms.Button();
            this.TxtNewRole = new System.Windows.Forms.TextBox();
            this.BtnCreateRole = new System.Windows.Forms.Button();
            this.LblError = new System.Windows.Forms.Label();
            this.CmbUsers = new System.Windows.Forms.ComboBox();
            this.ChkUserPermissions = new System.Windows.Forms.CheckedListBox();
            this.BtnSaveUserPermissions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CmbRoles
            // 
            this.CmbRoles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbRoles.Location = new System.Drawing.Point(23, 63);
            this.CmbRoles.Name = "CmbRoles";
            this.CmbRoles.Size = new System.Drawing.Size(300, 21);
            this.CmbRoles.TabIndex = 0;
            this.CmbRoles.SelectedIndexChanged += new System.EventHandler(this.CmbRoles_SelectedIndexChanged);
            // 
            // ChkPermissions
            // 
            this.ChkPermissions.CheckOnClick = true;
            this.ChkPermissions.FormattingEnabled = true;
            this.ChkPermissions.Location = new System.Drawing.Point(23, 90);
            this.ChkPermissions.Name = "ChkPermissions";
            this.ChkPermissions.Size = new System.Drawing.Size(520, 304);
            this.ChkPermissions.TabIndex = 1;
            // 
            // BtnSaveRolePermissions
            // 
            this.BtnSaveRolePermissions.Location = new System.Drawing.Point(23, 400);
            this.BtnSaveRolePermissions.Name = "BtnSaveRolePermissions";
            this.BtnSaveRolePermissions.Size = new System.Drawing.Size(520, 30);
            this.BtnSaveRolePermissions.TabIndex = 2;
            this.BtnSaveRolePermissions.Text = "حفظ صلاحيات الدور";
            this.BtnSaveRolePermissions.UseVisualStyleBackColor = true;
            this.BtnSaveRolePermissions.Click += new System.EventHandler(this.BtnSaveRolePermissions_Click);
            // 
            // TxtNewRole
            // 
            this.TxtNewRole.Location = new System.Drawing.Point(329, 63);
            this.TxtNewRole.Name = "TxtNewRole";
            this.TxtNewRole.Size = new System.Drawing.Size(214, 20);
            this.TxtNewRole.TabIndex = 3;
            // 
            // BtnCreateRole
            // 
            this.BtnCreateRole.Location = new System.Drawing.Point(549, 62);
            this.BtnCreateRole.Name = "BtnCreateRole";
            this.BtnCreateRole.Size = new System.Drawing.Size(120, 23);
            this.BtnCreateRole.TabIndex = 4;
            this.BtnCreateRole.Text = "إضافة دور جديد";
            this.BtnCreateRole.UseVisualStyleBackColor = true;
            this.BtnCreateRole.Click += new System.EventHandler(this.BtnCreateRole_Click);
            // 
            // LblError
            // 
            this.LblError.ForeColor = System.Drawing.Color.Maroon;
            this.LblError.Location = new System.Drawing.Point(549, 90);
            this.LblError.Name = "LblError";
            this.LblError.Size = new System.Drawing.Size(120, 80);
            this.LblError.TabIndex = 5;
            // 
            // CmbUsers
            // 
            this.CmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUsers.Location = new System.Drawing.Point(549, 180);
            this.CmbUsers.Name = "CmbUsers";
            this.CmbUsers.Size = new System.Drawing.Size(120, 21);
            this.CmbUsers.TabIndex = 6;
            this.CmbUsers.SelectedIndexChanged += new System.EventHandler(this.CmbUsers_SelectedIndexChanged);
            // 
            // ChkUserPermissions
            // 
            this.ChkUserPermissions.CheckOnClick = true;
            this.ChkUserPermissions.FormattingEnabled = true;
            this.ChkUserPermissions.Location = new System.Drawing.Point(549, 207);
            this.ChkUserPermissions.Name = "ChkUserPermissions";
            this.ChkUserPermissions.Size = new System.Drawing.Size(120, 184);
            this.ChkUserPermissions.TabIndex = 7;
            // 
            // BtnSaveUserPermissions
            // 
            this.BtnSaveUserPermissions.Location = new System.Drawing.Point(549, 400);
            this.BtnSaveUserPermissions.Name = "BtnSaveUserPermissions";
            this.BtnSaveUserPermissions.Size = new System.Drawing.Size(120, 30);
            this.BtnSaveUserPermissions.TabIndex = 8;
            this.BtnSaveUserPermissions.Text = "حفظ صلاحيات مستخدم";
            this.BtnSaveUserPermissions.UseVisualStyleBackColor = true;
            this.BtnSaveUserPermissions.Click += new System.EventHandler(this.BtnSaveUserPermissions_Click);
            // 
            // FrmPermissionsManagement
            // 
            this.ClientSize = new System.Drawing.Size(694, 443);
            this.Controls.Add(this.BtnSaveUserPermissions);
            this.Controls.Add(this.ChkUserPermissions);
            this.Controls.Add(this.CmbUsers);
            this.Controls.Add(this.LblError);
            this.Controls.Add(this.BtnCreateRole);
            this.Controls.Add(this.TxtNewRole);
            this.Controls.Add(this.BtnSaveRolePermissions);
            this.Controls.Add(this.ChkPermissions);
            this.Controls.Add(this.CmbRoles);
            this.Name = "FrmPermissionsManagement";
            this.Text = "إدارة الصلاحيات";
            this.Load += new System.EventHandler(this.FrmPermissionsManagement_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox CmbRoles;
        private System.Windows.Forms.CheckedListBox ChkPermissions;
        private System.Windows.Forms.Button BtnSaveRolePermissions;
        private System.Windows.Forms.TextBox TxtNewRole;
        private System.Windows.Forms.Button BtnCreateRole;
        private System.Windows.Forms.Label LblError;
        private System.Windows.Forms.ComboBox CmbUsers;
        private System.Windows.Forms.CheckedListBox ChkUserPermissions;
        private System.Windows.Forms.Button BtnSaveUserPermissions;
    }
}
