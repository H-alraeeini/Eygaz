namespace Eygaz
{
    partial class FrmUserManagement
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.TxtUsername = new System.Windows.Forms.TextBox();
            this.TxtDisplayName = new System.Windows.Forms.TextBox();
            this.TxtEmail = new System.Windows.Forms.TextBox();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.TxtConfirm = new System.Windows.Forms.TextBox();
            this.CmbRole = new System.Windows.Forms.ComboBox();
            this.BtnCreate = new System.Windows.Forms.Button();
            this.LblError = new System.Windows.Forms.Label();
            this.GVUsers = new DevExpress.XtraGrid.GridControl();
            this.GrdUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.LblUser = new System.Windows.Forms.Label();
            this.LblName = new System.Windows.Forms.Label();
            this.LblMail = new System.Windows.Forms.Label();
            this.LblPass = new System.Windows.Forms.Label();
            this.LblConfirm = new System.Windows.Forms.Label();
            this.LblRole = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GVUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtUsername
            // 
            this.TxtUsername.Location = new System.Drawing.Point(23, 89);
            this.TxtUsername.Name = "TxtUsername";
            this.TxtUsername.Size = new System.Drawing.Size(140, 20);
            this.TxtUsername.TabIndex = 0;
            // 
            // TxtDisplayName
            // 
            this.TxtDisplayName.Location = new System.Drawing.Point(169, 89);
            this.TxtDisplayName.Name = "TxtDisplayName";
            this.TxtDisplayName.Size = new System.Drawing.Size(140, 20);
            this.TxtDisplayName.TabIndex = 1;
            // 
            // TxtEmail
            // 
            this.TxtEmail.Location = new System.Drawing.Point(315, 89);
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(160, 20);
            this.TxtEmail.TabIndex = 2;
            // 
            // TxtPassword
            // 
            this.TxtPassword.Location = new System.Drawing.Point(481, 89);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '*';
            this.TxtPassword.Size = new System.Drawing.Size(120, 20);
            this.TxtPassword.TabIndex = 3;
            // 
            // TxtConfirm
            // 
            this.TxtConfirm.Location = new System.Drawing.Point(607, 89);
            this.TxtConfirm.Name = "TxtConfirm";
            this.TxtConfirm.PasswordChar = '*';
            this.TxtConfirm.Size = new System.Drawing.Size(120, 20);
            this.TxtConfirm.TabIndex = 4;
            // 
            // CmbRole
            // 
            this.CmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbRole.Location = new System.Drawing.Point(733, 89);
            this.CmbRole.Name = "CmbRole";
            this.CmbRole.Size = new System.Drawing.Size(120, 21);
            this.CmbRole.TabIndex = 5;
            // 
            // BtnCreate
            // 
            this.BtnCreate.Location = new System.Drawing.Point(23, 115);
            this.BtnCreate.Name = "BtnCreate";
            this.BtnCreate.Size = new System.Drawing.Size(830, 27);
            this.BtnCreate.TabIndex = 6;
            this.BtnCreate.Text = "إضافة مستخدم";
            this.BtnCreate.UseVisualStyleBackColor = true;
            this.BtnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // LblError
            // 
            this.LblError.ForeColor = System.Drawing.Color.Maroon;
            this.LblError.Location = new System.Drawing.Point(23, 145);
            this.LblError.Name = "LblError";
            this.LblError.Size = new System.Drawing.Size(830, 23);
            this.LblError.TabIndex = 7;
            // 
            // GVUsers
            // 
            this.GVUsers.Location = new System.Drawing.Point(23, 171);
            this.GVUsers.MainView = this.GrdUsers;
            this.GVUsers.Name = "GVUsers";
            this.GVUsers.Size = new System.Drawing.Size(830, 324);
            this.GVUsers.TabIndex = 8;
            this.GVUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdUsers});
            // 
            // GrdUsers
            // 
            this.GrdUsers.GridControl = this.GVUsers;
            this.GrdUsers.Name = "GrdUsers";
            this.GrdUsers.OptionsBehavior.Editable = false;
            this.GrdUsers.OptionsView.ShowGroupPanel = false;
            // 
            // LblUser
            // 
            this.LblUser.AutoSize = true;
            this.LblUser.Location = new System.Drawing.Point(23, 73);
            this.LblUser.Name = "LblUser";
            this.LblUser.Size = new System.Drawing.Size(76, 13);
            this.LblUser.TabIndex = 9;
            this.LblUser.Text = "اسم المستخدم";
            // 
            // LblName
            // 
            this.LblName.AutoSize = true;
            this.LblName.Location = new System.Drawing.Point(169, 73);
            this.LblName.Name = "LblName";
            this.LblName.Size = new System.Drawing.Size(71, 13);
            this.LblName.TabIndex = 10;
            this.LblName.Text = "الاسم الكامل";
            // 
            // LblMail
            // 
            this.LblMail.AutoSize = true;
            this.LblMail.Location = new System.Drawing.Point(315, 73);
            this.LblMail.Name = "LblMail";
            this.LblMail.Size = new System.Drawing.Size(84, 13);
            this.LblMail.TabIndex = 11;
            this.LblMail.Text = "البريد الإلكتروني";
            // 
            // LblPass
            // 
            this.LblPass.AutoSize = true;
            this.LblPass.Location = new System.Drawing.Point(481, 73);
            this.LblPass.Name = "LblPass";
            this.LblPass.Size = new System.Drawing.Size(63, 13);
            this.LblPass.TabIndex = 12;
            this.LblPass.Text = "كلمة المرور";
            // 
            // LblConfirm
            // 
            this.LblConfirm.AutoSize = true;
            this.LblConfirm.Location = new System.Drawing.Point(607, 73);
            this.LblConfirm.Name = "LblConfirm";
            this.LblConfirm.Size = new System.Drawing.Size(87, 13);
            this.LblConfirm.TabIndex = 13;
            this.LblConfirm.Text = "تأكيد كلمة المرور";
            // 
            // LblRole
            // 
            this.LblRole.AutoSize = true;
            this.LblRole.Location = new System.Drawing.Point(733, 73);
            this.LblRole.Name = "LblRole";
            this.LblRole.Size = new System.Drawing.Size(74, 13);
            this.LblRole.TabIndex = 14;
            this.LblRole.Text = "الدور/الصلاحية";
            // 
            // FrmUserManagement
            // 
            this.ClientSize = new System.Drawing.Size(876, 518);
            this.Controls.Add(this.LblRole);
            this.Controls.Add(this.LblConfirm);
            this.Controls.Add(this.LblPass);
            this.Controls.Add(this.LblMail);
            this.Controls.Add(this.LblName);
            this.Controls.Add(this.LblUser);
            this.Controls.Add(this.GVUsers);
            this.Controls.Add(this.LblError);
            this.Controls.Add(this.BtnCreate);
            this.Controls.Add(this.CmbRole);
            this.Controls.Add(this.TxtConfirm);
            this.Controls.Add(this.TxtPassword);
            this.Controls.Add(this.TxtEmail);
            this.Controls.Add(this.TxtDisplayName);
            this.Controls.Add(this.TxtUsername);
            this.Name = "FrmUserManagement";
            this.Text = "إدارة المستخدمين";
            this.Load += new System.EventHandler(this.FrmUserManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GVUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox TxtUsername;
        private System.Windows.Forms.TextBox TxtDisplayName;
        private System.Windows.Forms.TextBox TxtEmail;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.TextBox TxtConfirm;
        private System.Windows.Forms.ComboBox CmbRole;
        private System.Windows.Forms.Button BtnCreate;
        private System.Windows.Forms.Label LblError;
        private DevExpress.XtraGrid.GridControl GVUsers;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdUsers;
        private System.Windows.Forms.Label LblUser;
        private System.Windows.Forms.Label LblName;
        private System.Windows.Forms.Label LblMail;
        private System.Windows.Forms.Label LblPass;
        private System.Windows.Forms.Label LblConfirm;
        private System.Windows.Forms.Label LblRole;
    }
}
