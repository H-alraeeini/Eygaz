using System;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class FrmLogin : MetroFramework.Forms.MetroForm
    {
        private readonly AuthService authService = new AuthService();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            string remembered = authService.GetRememberedUsername();
            if (!string.IsNullOrWhiteSpace(remembered))
            {
                TxtUsername.Text = remembered;
                ChkRememberMe.Checked = true;
                TxtPassword.Focus();
            }
            else
            {
                TxtUsername.Focus();
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            LblError.Text = "";
            bool ok = authService.Login(TxtUsername.Text, TxtPassword.Text, ChkRememberMe.Checked, out string error);
            if (!ok)
            {
                LblError.Text = error;
                return;
            }

            if (AuthSession.MustChangePassword)
            {
                using (var dlg = new FrmChangePassword())
                {
                    if (dlg.ShowDialog(this) != DialogResult.OK)
                    {
                        AuthSession.Clear();
                        LblError.Text = "يجب تغيير كلمة المرور في أول تسجيل دخول.";
                        return;
                    }
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(e.KeyChar) == 13)
                {
                    BtnLogin_Click(null, null);
                }
            }
            catch
            {
            }
        }
    }
}
