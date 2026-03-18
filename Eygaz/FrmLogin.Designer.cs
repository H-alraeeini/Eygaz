
namespace Eygaz
{
    partial class FrmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.PnlData = new System.Windows.Forms.Panel();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.UserPwd = new System.Windows.Forms.TextBox();
            this.UserName = new System.Windows.Forms.TextBox();
            this.lblNo = new System.Windows.Forms.Label();
            this.lblCustNo = new System.Windows.Forms.Label();
            this.PnlData.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlData
            // 
            this.PnlData.Controls.Add(this.BtnExit);
            this.PnlData.Controls.Add(this.BtnOk);
            this.PnlData.Controls.Add(this.UserPwd);
            this.PnlData.Controls.Add(this.UserName);
            this.PnlData.Controls.Add(this.lblNo);
            this.PnlData.Controls.Add(this.lblCustNo);
            this.PnlData.Location = new System.Drawing.Point(44, 110);
            this.PnlData.Name = "PnlData";
            this.PnlData.Size = new System.Drawing.Size(299, 92);
            this.PnlData.TabIndex = 40;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExit.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Location = new System.Drawing.Point(71, 52);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 23);
            this.BtnExit.TabIndex = 66;
            this.BtnExit.Text = "خروج";
            this.BtnExit.UseVisualStyleBackColor = false;
            // 
            // BtnOk
            // 
            this.BtnOk.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOk.Location = new System.Drawing.Point(152, 52);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(75, 23);
            this.BtnOk.TabIndex = 65;
            this.BtnOk.Text = "دخول";
            this.BtnOk.UseVisualStyleBackColor = false;

            // 
            // UserPwd
            // 
            this.UserPwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.UserPwd.Location = new System.Drawing.Point(91, 29);
            this.UserPwd.Multiline = true;
            this.UserPwd.Name = "UserPwd";
            this.UserPwd.PasswordChar = '*';
            this.UserPwd.Size = new System.Drawing.Size(103, 17);
            this.UserPwd.TabIndex = 10;
            this.UserPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // UserName
            // 
            this.UserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.UserName.Location = new System.Drawing.Point(91, 9);
            this.UserName.Multiline = true;
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(103, 17);
            this.UserName.TabIndex = 9;
            this.UserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(79)))), ((int)(((byte)(88)))));
            this.lblNo.Location = new System.Drawing.Point(213, 9);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(31, 13);
            this.lblNo.TabIndex = 64;
            this.lblNo.Text = "الرقم";
            // 
            // lblCustNo
            // 
            this.lblCustNo.AutoSize = true;
            this.lblCustNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(79)))), ((int)(((byte)(88)))));
            this.lblCustNo.Location = new System.Drawing.Point(197, 30);
            this.lblCustNo.Name = "lblCustNo";
            this.lblCustNo.Size = new System.Drawing.Size(59, 13);
            this.lblCustNo.TabIndex = 63;
            this.lblCustNo.Text = "كلمة المرور";
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 280);
            this.Controls.Add(this.PnlData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLogin";
            this.Text = "دخول النظام";
            this.PnlData.ResumeLayout(false);
            this.PnlData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlData;
        private System.Windows.Forms.TextBox UserPwd;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label lblCustNo;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnOk;
    }
}