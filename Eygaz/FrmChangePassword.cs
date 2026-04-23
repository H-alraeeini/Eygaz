using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class FrmChangePassword : Form
    {
        private readonly AuthService authService = new AuthService();

        public FrmChangePassword()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            LblError.Text = "";
            if (TxtNew.Text != TxtConfirm.Text)
            {
                LblError.Text = "تأكيد كلمة المرور غير مطابق.";
                return;
            }
            if (TxtNew.Text.Length < 6)
            {
                LblError.Text = "كلمة المرور يجب أن تكون 6 أحرف على الأقل.";
                return;
            }

            if (!authService.ChangePassword(AuthSession.UserId, TxtNew.Text))
            {
                LblError.Text = "فشل تحديث كلمة المرور.";
                return;
            }

            AuthSession.MarkPasswordChanged();

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
