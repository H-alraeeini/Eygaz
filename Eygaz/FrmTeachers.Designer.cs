namespace Eygaz
{
    partial class FrmTeachers
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
            this.PnlBut = new System.Windows.Forms.Panel();
            this.BtnUndo = new System.Windows.Forms.Button();
            this.BtnDel = new System.Windows.Forms.Button();
            this.BtnNew = new System.Windows.Forms.Button();
            this.BtnSrch = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.PnlData = new System.Windows.Forms.Panel();
            this.FullName = new System.Windows.Forms.TextBox();
            this.IsActive = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Address = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Gender = new System.Windows.Forms.ComboBox();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.Notes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.Phone = new System.Windows.Forms.TextBox();
            this.LblAmt = new System.Windows.Forms.Label();
            this.Id = new System.Windows.Forms.TextBox();
            this.lblNo = new System.Windows.Forms.Label();
            this.PnlSrh = new System.Windows.Forms.Panel();
            this.GVShow = new DevExpress.XtraGrid.GridControl();
            this.GrdDtl = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.PnlBut.SuspendLayout();
            this.PnlData.SuspendLayout();
            this.PnlSrh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDtl)).BeginInit();
            this.SuspendLayout();
            // 
            // PnlBut
            // 
            this.PnlBut.Controls.Add(this.BtnUndo);
            this.PnlBut.Controls.Add(this.BtnDel);
            this.PnlBut.Controls.Add(this.BtnNew);
            this.PnlBut.Controls.Add(this.BtnSrch);
            this.PnlBut.Controls.Add(this.BtnSave);
            this.PnlBut.Controls.Add(this.BtnEdit);
            this.PnlBut.Location = new System.Drawing.Point(71, 82);
            this.PnlBut.Name = "PnlBut";
            this.PnlBut.Size = new System.Drawing.Size(581, 30);
            this.PnlBut.TabIndex = 72;
            // 
            // BtnUndo
            // 
            this.BtnUndo.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnUndo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnUndo.Location = new System.Drawing.Point(377, 4);
            this.BtnUndo.Name = "BtnUndo";
            this.BtnUndo.Size = new System.Drawing.Size(81, 23);
            this.BtnUndo.TabIndex = 6;
            this.BtnUndo.Text = "تراجع";
            this.BtnUndo.UseVisualStyleBackColor = false;
            this.BtnUndo.Click += new System.EventHandler(this.BtnUndo_Click);
            // 
            // BtnDel
            // 
            this.BtnDel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnDel.Location = new System.Drawing.Point(41, 4);
            this.BtnDel.Name = "BtnDel";
            this.BtnDel.Size = new System.Drawing.Size(81, 23);
            this.BtnDel.TabIndex = 5;
            this.BtnDel.Text = "حذف";
            this.BtnDel.UseVisualStyleBackColor = false;
            this.BtnDel.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNew.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnNew.Location = new System.Drawing.Point(461, 4);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(81, 23);
            this.BtnNew.TabIndex = 1;
            this.BtnNew.Text = "جديد";
            this.BtnNew.UseVisualStyleBackColor = false;
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // BtnSrch
            // 
            this.BtnSrch.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnSrch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSrch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSrch.Location = new System.Drawing.Point(125, 4);
            this.BtnSrch.Name = "BtnSrch";
            this.BtnSrch.Size = new System.Drawing.Size(81, 23);
            this.BtnSrch.TabIndex = 4;
            this.BtnSrch.Text = "بحث";
            this.BtnSrch.UseVisualStyleBackColor = false;
            this.BtnSrch.Click += new System.EventHandler(this.BtnSrch_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnSave.Location = new System.Drawing.Point(293, 4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(81, 23);
            this.BtnSave.TabIndex = 2;
            this.BtnSave.Text = "حفظ";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEdit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.BtnEdit.Location = new System.Drawing.Point(209, 4);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(81, 23);
            this.BtnEdit.TabIndex = 3;
            this.BtnEdit.Text = "تعديل ";
            this.BtnEdit.UseVisualStyleBackColor = false;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // PnlData
            // 
            this.PnlData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlData.Controls.Add(this.FullName);
            this.PnlData.Controls.Add(this.IsActive);
            this.PnlData.Controls.Add(this.label5);
            this.PnlData.Controls.Add(this.Address);
            this.PnlData.Controls.Add(this.label4);
            this.PnlData.Controls.Add(this.Gender);
            this.PnlData.Controls.Add(this.lblGender);
            this.PnlData.Controls.Add(this.lblFullName);
            this.PnlData.Controls.Add(this.lblId);
            this.PnlData.Controls.Add(this.Notes);
            this.PnlData.Controls.Add(this.lblNotes);
            this.PnlData.Controls.Add(this.Phone);
            this.PnlData.Controls.Add(this.LblAmt);
            this.PnlData.Controls.Add(this.Id);
            this.PnlData.Controls.Add(this.lblNo);
            this.PnlData.Location = new System.Drawing.Point(57, 118);
            this.PnlData.Name = "PnlData";
            this.PnlData.Size = new System.Drawing.Size(614, 175);
            this.PnlData.TabIndex = 73;
            // 
            // FullName
            // 
            this.FullName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.FullName.Location = new System.Drawing.Point(117, 1);
            this.FullName.Multiline = true;
            this.FullName.Name = "FullName";
            this.FullName.Size = new System.Drawing.Size(280, 23);
            this.FullName.TabIndex = 1;
            this.FullName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // IsActive
            // 
            this.IsActive.AutoSize = true;
            this.IsActive.Location = new System.Drawing.Point(187, 62);
            this.IsActive.Name = "IsActive";
            this.IsActive.Size = new System.Drawing.Size(15, 14);
            this.IsActive.TabIndex = 114;
            this.IsActive.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(79)))), ((int)(((byte)(88)))));
            this.label5.Location = new System.Drawing.Point(221, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 113;
            this.label5.Text = "ايقاف";
            // 
            // Address
            // 
            this.Address.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Address.Location = new System.Drawing.Point(275, 56);
            this.Address.Multiline = true;
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(280, 23);
            this.Address.TabIndex = 112;
            this.Address.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(79)))), ((int)(((byte)(88)))));
            this.label4.Location = new System.Drawing.Point(572, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 111;
            this.label4.Text = "العنوان";
            // 
            // Gender
            // 
            this.Gender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Gender.FormattingEnabled = true;
            this.Gender.Location = new System.Drawing.Point(466, 31);
            this.Gender.Name = "Gender";
            this.Gender.Size = new System.Drawing.Size(87, 21);
            this.Gender.TabIndex = 2;
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(79)))), ((int)(((byte)(88)))));
            this.lblGender.Location = new System.Drawing.Point(572, 39);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(29, 13);
            this.lblGender.TabIndex = 109;
            this.lblGender.Text = "النوع";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(79)))), ((int)(((byte)(88)))));
            this.lblFullName.Location = new System.Drawing.Point(423, 4);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(36, 13);
            this.lblFullName.TabIndex = 106;
            this.lblFullName.Text = "الاسم";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(79)))), ((int)(((byte)(88)))));
            this.lblId.Location = new System.Drawing.Point(572, 3);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(31, 13);
            this.lblId.TabIndex = 103;
            this.lblId.Text = "الرقم";
            // 
            // Notes
            // 
            this.Notes.Location = new System.Drawing.Point(134, 95);
            this.Notes.Multiline = true;
            this.Notes.Name = "Notes";
            this.Notes.Size = new System.Drawing.Size(419, 67);
            this.Notes.TabIndex = 87;
            this.Notes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(79)))), ((int)(((byte)(88)))));
            this.lblNotes.Location = new System.Drawing.Point(555, 125);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(48, 13);
            this.lblNotes.TabIndex = 86;
            this.lblNotes.Text = "ملاحظات";
            // 
            // Phone
            // 
            this.Phone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Phone.Location = new System.Drawing.Point(275, 29);
            this.Phone.Multiline = true;
            this.Phone.Name = "Phone";
            this.Phone.Size = new System.Drawing.Size(122, 23);
            this.Phone.TabIndex = 3;
            this.Phone.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LblAmt
            // 
            this.LblAmt.AutoSize = true;
            this.LblAmt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(79)))), ((int)(((byte)(88)))));
            this.LblAmt.Location = new System.Drawing.Point(420, 39);
            this.LblAmt.Name = "LblAmt";
            this.LblAmt.Size = new System.Drawing.Size(39, 13);
            this.LblAmt.TabIndex = 79;
            this.LblAmt.Text = "التلفون";
            // 
            // Id
            // 
            this.Id.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Id.Location = new System.Drawing.Point(466, 1);
            this.Id.Multiline = true;
            this.Id.Name = "Id";
            this.Id.Size = new System.Drawing.Size(86, 23);
            this.Id.TabIndex = 1;
            this.Id.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(79)))), ((int)(((byte)(88)))));
            this.lblNo.Location = new System.Drawing.Point(480, -19);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(31, 13);
            this.lblNo.TabIndex = 64;
            this.lblNo.Text = "الرقم";
            // 
            // PnlSrh
            // 
            this.PnlSrh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSrh.Controls.Add(this.GVShow);
            this.PnlSrh.Location = new System.Drawing.Point(57, 310);
            this.PnlSrh.Name = "PnlSrh";
            this.PnlSrh.Size = new System.Drawing.Size(614, 220);
            this.PnlSrh.TabIndex = 74;
            // 
            // GVShow
            // 
            this.GVShow.Location = new System.Drawing.Point(6, 3);
            this.GVShow.MainView = this.GrdDtl;
            this.GVShow.Name = "GVShow";
            this.GVShow.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GVShow.Size = new System.Drawing.Size(603, 207);
            this.GVShow.TabIndex = 1;
            this.GVShow.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdDtl});
            // 
            // GrdDtl
            // 
            this.GrdDtl.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 7F);
            this.GrdDtl.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Blue;
            this.GrdDtl.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdDtl.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.GrdDtl.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GrdDtl.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDtl.Appearance.Row.Options.UseFont = true;
            this.GrdDtl.Appearance.Row.Options.UseTextOptions = true;
            this.GrdDtl.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GrdDtl.ColumnPanelRowHeight = 30;
            this.GrdDtl.GridControl = this.GVShow;
            this.GrdDtl.Name = "GrdDtl";
            this.GrdDtl.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GrdDtl.OptionsView.ColumnAutoWidth = false;
            this.GrdDtl.OptionsView.ShowAutoFilterRow = true;
            this.GrdDtl.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.GrdDtl.OptionsView.ShowGroupPanel = false;
            this.GrdDtl.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GrdDtl_FocusedRowChanged);
            // 
            // FrmTeachers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 546);
            this.Controls.Add(this.PnlSrh);
            this.Controls.Add(this.PnlData);
            this.Controls.Add(this.PnlBut);
            this.Name = "FrmTeachers";
            this.Text = "المدرسين";
            this.Load += new System.EventHandler(this.FrmTeachers_Load);
            this.PnlBut.ResumeLayout(false);
            this.PnlData.ResumeLayout(false);
            this.PnlData.PerformLayout();
            this.PnlSrh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GVShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdDtl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlBut;
        private System.Windows.Forms.Button BtnUndo;
        private System.Windows.Forms.Button BtnDel;
        private System.Windows.Forms.Button BtnNew;
        private System.Windows.Forms.Button BtnSrch;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnEdit;
        private System.Windows.Forms.Panel PnlData;
        private System.Windows.Forms.TextBox FullName;
        private System.Windows.Forms.CheckBox IsActive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Address;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Gender;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox Notes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox Phone;
        private System.Windows.Forms.Label LblAmt;
        private System.Windows.Forms.TextBox Id;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Panel PnlSrh;
        private DevExpress.XtraGrid.GridControl GVShow;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdDtl;
    }
}