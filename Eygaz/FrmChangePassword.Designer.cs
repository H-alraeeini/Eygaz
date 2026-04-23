namespace Eygaz
{
    partial class FrmChangePassword
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.TxtNew = new System.Windows.Forms.TextBox();
            this.TxtConfirm = new System.Windows.Forms.TextBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.LblError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TxtNew
            // 
            this.TxtNew.Location = new System.Drawing.Point(20, 25);
            this.TxtNew.Name = "TxtNew";
            this.TxtNew.PasswordChar = '*';
            this.TxtNew.Size = new System.Drawing.Size(260, 20);
            this.TxtNew.TabIndex = 0;
            // 
            // TxtConfirm
            // 
            this.TxtConfirm.Location = new System.Drawing.Point(20, 61);
            this.TxtConfirm.Name = "TxtConfirm";
            this.TxtConfirm.PasswordChar = '*';
            this.TxtConfirm.Size = new System.Drawing.Size(260, 20);
            this.TxtConfirm.TabIndex = 1;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(20, 94);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(260, 27);
            this.BtnSave.TabIndex = 2;
            this.BtnSave.Text = "حفظ كلمة المرور";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // LblError
            // 
            this.LblError.ForeColor = System.Drawing.Color.Maroon;
            this.LblError.Location = new System.Drawing.Point(20, 130);
            this.LblError.Name = "LblError";
            this.LblError.Size = new System.Drawing.Size(260, 35);
            this.LblError.TabIndex = 3;
            // 
            // FrmChangePassword
            // 
            this.ClientSize = new System.Drawing.Size(301, 176);
            this.Controls.Add(this.LblError);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.TxtConfirm);
            this.Controls.Add(this.TxtNew);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "تغيير كلمة المرور";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox TxtNew;
        private System.Windows.Forms.TextBox TxtConfirm;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Label LblError;
    }
}
