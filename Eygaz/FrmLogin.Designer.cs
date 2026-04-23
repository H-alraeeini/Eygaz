namespace Eygaz
{
    partial class FrmLogin
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

        private void InitializeComponent()
        {
            this.TxtUsername = new System.Windows.Forms.TextBox();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.ChkRememberMe = new System.Windows.Forms.CheckBox();
            this.LblError = new System.Windows.Forms.Label();
            this.LblUsername = new System.Windows.Forms.Label();
            this.LblPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TxtUsername
            // 
            this.TxtUsername.Location = new System.Drawing.Point(34, 88);
            this.TxtUsername.Name = "TxtUsername";
            this.TxtUsername.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtUsername.Size = new System.Drawing.Size(300, 20);
            this.TxtUsername.TabIndex = 0;
            // 
            // TxtPassword
            // 
            this.TxtPassword.Location = new System.Drawing.Point(34, 136);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '*';
            this.TxtPassword.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtPassword.Size = new System.Drawing.Size(300, 20);
            this.TxtPassword.TabIndex = 1;
            this.TxtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPassword_KeyPress);
            // 
            // BtnLogin
            // 
            this.BtnLogin.Location = new System.Drawing.Point(179, 204);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(155, 28);
            this.BtnLogin.TabIndex = 3;
            this.BtnLogin.Text = "تسجيل الدخول";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(34, 204);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(139, 28);
            this.BtnExit.TabIndex = 4;
            this.BtnExit.Text = "خروج";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // ChkRememberMe
            // 
            this.ChkRememberMe.AutoSize = true;
            this.ChkRememberMe.Location = new System.Drawing.Point(251, 171);
            this.ChkRememberMe.Name = "ChkRememberMe";
            this.ChkRememberMe.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRememberMe.Size = new System.Drawing.Size(59, 17);
            this.ChkRememberMe.TabIndex = 2;
            this.ChkRememberMe.Text = "تذكرني";
            this.ChkRememberMe.UseVisualStyleBackColor = true;
            // 
            // LblError
            // 
            this.LblError.ForeColor = System.Drawing.Color.Maroon;
            this.LblError.Location = new System.Drawing.Point(34, 239);
            this.LblError.Name = "LblError";
            this.LblError.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblError.Size = new System.Drawing.Size(300, 42);
            this.LblError.TabIndex = 7;
            // 
            // LblUsername
            // 
            this.LblUsername.AutoSize = true;
            this.LblUsername.Location = new System.Drawing.Point(234, 72);
            this.LblUsername.Name = "LblUsername";
            this.LblUsername.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblUsername.Size = new System.Drawing.Size(109, 13);
            this.LblUsername.TabIndex = 8;
            this.LblUsername.Text = "اسم المستخدم/البريد";
            // 
            // LblPassword
            // 
            this.LblPassword.AutoSize = true;
            this.LblPassword.Location = new System.Drawing.Point(271, 120);
            this.LblPassword.Name = "LblPassword";
            this.LblPassword.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblPassword.Size = new System.Drawing.Size(59, 13);
            this.LblPassword.TabIndex = 9;
            this.LblPassword.Text = "كلمة المرور";
            // 
            // FrmLogin
            // 
            this.ClientSize = new System.Drawing.Size(368, 300);
            this.Controls.Add(this.LblPassword);
            this.Controls.Add(this.LblUsername);
            this.Controls.Add(this.LblError);
            this.Controls.Add(this.ChkRememberMe);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.TxtPassword);
            this.Controls.Add(this.TxtUsername);
            this.Name = "FrmLogin";
            this.Text = "تسجيل الدخول";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox TxtUsername;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.Button BtnLogin;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.CheckBox ChkRememberMe;
        private System.Windows.Forms.Label LblError;
        private System.Windows.Forms.Label LblUsername;
        private System.Windows.Forms.Label LblPassword;
    }
}
