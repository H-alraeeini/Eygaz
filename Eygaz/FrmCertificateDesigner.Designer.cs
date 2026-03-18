namespace Eygaz
{
    partial class FrmCertificateDesigner
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlCanvas = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabElements = new System.Windows.Forms.TabPage();
            this.grpDynamicFields = new System.Windows.Forms.GroupBox();
            this.BtnAddIssueDate = new System.Windows.Forms.Button();
            this.BtnAddAttendPct = new System.Windows.Forms.Button();
            this.BtnAddTeacherName = new System.Windows.Forms.Button();
            this.BtnAddClassName = new System.Windows.Forms.Button();
            this.BtnAddStudentName = new System.Windows.Forms.Button();
            this.grpElements = new System.Windows.Forms.GroupBox();
            this.BtnAddShape = new System.Windows.Forms.Button();
            this.BtnAddImage = new System.Windows.Forms.Button();
            this.BtnAddText = new System.Windows.Forms.Button();
            this.tabProperties = new System.Windows.Forms.TabPage();
            this.grpProperties = new System.Windows.Forms.GroupBox();
            this.lblAlignment = new System.Windows.Forms.Label();
            this.cmbAlignment = new System.Windows.Forms.ComboBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.BtnPickColor = new System.Windows.Forms.Button();
            this.pnlColorPreview = new System.Windows.Forms.Panel();
            this.chkBold = new System.Windows.Forms.CheckBox();
            this.lblFontSize = new System.Windows.Forms.Label();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.lblPosY = new System.Windows.Forms.Label();
            this.numPosY = new System.Windows.Forms.NumericUpDown();
            this.lblPosX = new System.Windows.Forms.Label();
            this.numPosX = new System.Windows.Forms.NumericUpDown();
            this.lblWidth = new System.Windows.Forms.Label();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.lblText = new System.Windows.Forms.Label();
            this.txtElementText = new System.Windows.Forms.TextBox();
            this.BtnDeleteElement = new System.Windows.Forms.Button();
            this.tabTemplates = new System.Windows.Forms.TabPage();
            this.grpTemplateActions = new System.Windows.Forms.GroupBox();
            this.txtTemplateName = new System.Windows.Forms.TextBox();
            this.lblTemplateName = new System.Windows.Forms.Label();
            this.BtnSaveTemplate = new System.Windows.Forms.Button();
            this.BtnLoadTemplate = new System.Windows.Forms.Button();
            this.BtnDeleteTemplate = new System.Windows.Forms.Button();
            this.BtnNewTemplate = new System.Windows.Forms.Button();
            this.lstTemplates = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabElements.SuspendLayout();
            this.grpDynamicFields.SuspendLayout();
            this.grpElements.SuspendLayout();
            this.tabProperties.SuspendLayout();
            this.grpProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            this.tabTemplates.SuspendLayout();
            this.grpTemplateActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(20, 60);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.pnlCanvas);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(960, 530);
            this.splitContainer1.SplitterDistance = 680;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnlCanvas
            // 
            this.pnlCanvas.AutoScroll = true;
            this.pnlCanvas.BackColor = System.Drawing.Color.White;
            this.pnlCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCanvas.Location = new System.Drawing.Point(0, 0);
            this.pnlCanvas.Name = "pnlCanvas";
            this.pnlCanvas.Size = new System.Drawing.Size(680, 530);
            this.pnlCanvas.TabIndex = 0;
            this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabElements);
            this.tabControl1.Controls.Add(this.tabProperties);
            this.tabControl1.Controls.Add(this.tabTemplates);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(276, 530);
            this.tabControl1.TabIndex = 0;
            // 
            // tabElements
            // 
            this.tabElements.Controls.Add(this.grpDynamicFields);
            this.tabElements.Controls.Add(this.grpElements);
            this.tabElements.Location = new System.Drawing.Point(4, 22);
            this.tabElements.Name = "tabElements";
            this.tabElements.Size = new System.Drawing.Size(268, 504);
            this.tabElements.TabIndex = 0;
            this.tabElements.Text = "العناصر";
            // 
            // grpElements
            // 
            this.grpElements.Controls.Add(this.BtnAddShape);
            this.grpElements.Controls.Add(this.BtnAddImage);
            this.grpElements.Controls.Add(this.BtnAddText);
            this.grpElements.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpElements.Font = new System.Drawing.Font("Tahoma", 9F);
            this.grpElements.Location = new System.Drawing.Point(0, 0);
            this.grpElements.Name = "grpElements";
            this.grpElements.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grpElements.Size = new System.Drawing.Size(268, 130);
            this.grpElements.TabIndex = 0;
            this.grpElements.TabStop = false;
            this.grpElements.Text = "إضافة عناصر";
            // 
            // BtnAddText
            // 
            this.BtnAddText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.BtnAddText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddText.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnAddText.ForeColor = System.Drawing.Color.White;
            this.BtnAddText.Location = new System.Drawing.Point(10, 25);
            this.BtnAddText.Name = "BtnAddText";
            this.BtnAddText.Size = new System.Drawing.Size(248, 28);
            this.BtnAddText.TabIndex = 0;
            this.BtnAddText.Text = "📝 إضافة نص";
            this.BtnAddText.UseVisualStyleBackColor = false;
            this.BtnAddText.Click += new System.EventHandler(this.BtnAddText_Click);
            // 
            // BtnAddImage
            // 
            this.BtnAddImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.BtnAddImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddImage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnAddImage.ForeColor = System.Drawing.Color.White;
            this.BtnAddImage.Location = new System.Drawing.Point(10, 59);
            this.BtnAddImage.Name = "BtnAddImage";
            this.BtnAddImage.Size = new System.Drawing.Size(248, 28);
            this.BtnAddImage.TabIndex = 1;
            this.BtnAddImage.Text = "🖼 إضافة صورة";
            this.BtnAddImage.UseVisualStyleBackColor = false;
            this.BtnAddImage.Click += new System.EventHandler(this.BtnAddImage_Click);
            // 
            // BtnAddShape
            // 
            this.BtnAddShape.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(16)))), ((int)(((byte)(242)))));
            this.BtnAddShape.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddShape.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnAddShape.ForeColor = System.Drawing.Color.White;
            this.BtnAddShape.Location = new System.Drawing.Point(10, 93);
            this.BtnAddShape.Name = "BtnAddShape";
            this.BtnAddShape.Size = new System.Drawing.Size(248, 28);
            this.BtnAddShape.TabIndex = 2;
            this.BtnAddShape.Text = "⬛ إضافة شكل";
            this.BtnAddShape.UseVisualStyleBackColor = false;
            this.BtnAddShape.Click += new System.EventHandler(this.BtnAddShape_Click);
            // 
            // grpDynamicFields
            // 
            this.grpDynamicFields.Controls.Add(this.BtnAddIssueDate);
            this.grpDynamicFields.Controls.Add(this.BtnAddAttendPct);
            this.grpDynamicFields.Controls.Add(this.BtnAddTeacherName);
            this.grpDynamicFields.Controls.Add(this.BtnAddClassName);
            this.grpDynamicFields.Controls.Add(this.BtnAddStudentName);
            this.grpDynamicFields.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpDynamicFields.Font = new System.Drawing.Font("Tahoma", 9F);
            this.grpDynamicFields.Location = new System.Drawing.Point(0, 300);
            this.grpDynamicFields.Name = "grpDynamicFields";
            this.grpDynamicFields.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grpDynamicFields.Size = new System.Drawing.Size(268, 204);
            this.grpDynamicFields.TabIndex = 1;
            this.grpDynamicFields.TabStop = false;
            this.grpDynamicFields.Text = "الحقول الديناميكية";
            // 
            // BtnAddStudentName
            // 
            this.BtnAddStudentName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.BtnAddStudentName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddStudentName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnAddStudentName.ForeColor = System.Drawing.Color.White;
            this.BtnAddStudentName.Location = new System.Drawing.Point(10, 25);
            this.BtnAddStudentName.Name = "BtnAddStudentName";
            this.BtnAddStudentName.Size = new System.Drawing.Size(248, 28);
            this.BtnAddStudentName.TabIndex = 0;
            this.BtnAddStudentName.Text = "[StudentName] اسم الطالب";
            this.BtnAddStudentName.UseVisualStyleBackColor = false;
            this.BtnAddStudentName.Click += new System.EventHandler(this.BtnAddStudentName_Click);
            // 
            // BtnAddClassName
            // 
            this.BtnAddClassName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.BtnAddClassName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddClassName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnAddClassName.ForeColor = System.Drawing.Color.White;
            this.BtnAddClassName.Location = new System.Drawing.Point(10, 59);
            this.BtnAddClassName.Name = "BtnAddClassName";
            this.BtnAddClassName.Size = new System.Drawing.Size(248, 28);
            this.BtnAddClassName.TabIndex = 1;
            this.BtnAddClassName.Text = "[ClassName] اسم الفصل";
            this.BtnAddClassName.UseVisualStyleBackColor = false;
            this.BtnAddClassName.Click += new System.EventHandler(this.BtnAddClassName_Click);
            // 
            // BtnAddTeacherName
            // 
            this.BtnAddTeacherName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.BtnAddTeacherName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddTeacherName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnAddTeacherName.ForeColor = System.Drawing.Color.White;
            this.BtnAddTeacherName.Location = new System.Drawing.Point(10, 93);
            this.BtnAddTeacherName.Name = "BtnAddTeacherName";
            this.BtnAddTeacherName.Size = new System.Drawing.Size(248, 28);
            this.BtnAddTeacherName.TabIndex = 2;
            this.BtnAddTeacherName.Text = "[TeacherName] اسم المدرس";
            this.BtnAddTeacherName.UseVisualStyleBackColor = false;
            this.BtnAddTeacherName.Click += new System.EventHandler(this.BtnAddTeacherName_Click);
            // 
            // BtnAddAttendPct
            // 
            this.BtnAddAttendPct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.BtnAddAttendPct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddAttendPct.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnAddAttendPct.ForeColor = System.Drawing.Color.White;
            this.BtnAddAttendPct.Location = new System.Drawing.Point(10, 127);
            this.BtnAddAttendPct.Name = "BtnAddAttendPct";
            this.BtnAddAttendPct.Size = new System.Drawing.Size(248, 28);
            this.BtnAddAttendPct.TabIndex = 3;
            this.BtnAddAttendPct.Text = "[AttendancePercentage] نسبة الحضور";
            this.BtnAddAttendPct.UseVisualStyleBackColor = false;
            this.BtnAddAttendPct.Click += new System.EventHandler(this.BtnAddAttendPct_Click);
            // 
            // BtnAddIssueDate
            // 
            this.BtnAddIssueDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.BtnAddIssueDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddIssueDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnAddIssueDate.ForeColor = System.Drawing.Color.White;
            this.BtnAddIssueDate.Location = new System.Drawing.Point(10, 161);
            this.BtnAddIssueDate.Name = "BtnAddIssueDate";
            this.BtnAddIssueDate.Size = new System.Drawing.Size(248, 28);
            this.BtnAddIssueDate.TabIndex = 4;
            this.BtnAddIssueDate.Text = "[IssueDate] تاريخ الإصدار";
            this.BtnAddIssueDate.UseVisualStyleBackColor = false;
            this.BtnAddIssueDate.Click += new System.EventHandler(this.BtnAddIssueDate_Click);
            // 
            // tabProperties
            // 
            this.tabProperties.Controls.Add(this.grpProperties);
            this.tabProperties.Location = new System.Drawing.Point(4, 22);
            this.tabProperties.Name = "tabProperties";
            this.tabProperties.Size = new System.Drawing.Size(268, 504);
            this.tabProperties.TabIndex = 1;
            this.tabProperties.Text = "الخصائص";
            // 
            // grpProperties
            // 
            this.grpProperties.Controls.Add(this.BtnDeleteElement);
            this.grpProperties.Controls.Add(this.txtElementText);
            this.grpProperties.Controls.Add(this.lblText);
            this.grpProperties.Controls.Add(this.numHeight);
            this.grpProperties.Controls.Add(this.lblHeight);
            this.grpProperties.Controls.Add(this.numWidth);
            this.grpProperties.Controls.Add(this.lblWidth);
            this.grpProperties.Controls.Add(this.numPosX);
            this.grpProperties.Controls.Add(this.lblPosX);
            this.grpProperties.Controls.Add(this.numPosY);
            this.grpProperties.Controls.Add(this.lblPosY);
            this.grpProperties.Controls.Add(this.numFontSize);
            this.grpProperties.Controls.Add(this.lblFontSize);
            this.grpProperties.Controls.Add(this.chkBold);
            this.grpProperties.Controls.Add(this.pnlColorPreview);
            this.grpProperties.Controls.Add(this.BtnPickColor);
            this.grpProperties.Controls.Add(this.lblColor);
            this.grpProperties.Controls.Add(this.cmbAlignment);
            this.grpProperties.Controls.Add(this.lblAlignment);
            this.grpProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpProperties.Font = new System.Drawing.Font("Tahoma", 9F);
            this.grpProperties.Location = new System.Drawing.Point(0, 0);
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grpProperties.Size = new System.Drawing.Size(268, 504);
            this.grpProperties.TabIndex = 0;
            this.grpProperties.TabStop = false;
            this.grpProperties.Text = "خصائص العنصر المحدد";
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(220, 28);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(36, 14);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "النص:";
            // 
            // txtElementText
            // 
            this.txtElementText.Location = new System.Drawing.Point(10, 45);
            this.txtElementText.Multiline = true;
            this.txtElementText.Name = "txtElementText";
            this.txtElementText.Size = new System.Drawing.Size(248, 40);
            this.txtElementText.TabIndex = 1;
            this.txtElementText.TextChanged += new System.EventHandler(this.txtElementText_TextChanged);
            // 
            // lblPosX
            // 
            this.lblPosX.AutoSize = true;
            this.lblPosX.Location = new System.Drawing.Point(228, 95);
            this.lblPosX.Name = "lblPosX";
            this.lblPosX.Size = new System.Drawing.Size(18, 14);
            this.lblPosX.TabIndex = 2;
            this.lblPosX.Text = "X:";
            // 
            // numPosX
            // 
            this.numPosX.Location = new System.Drawing.Point(140, 92);
            this.numPosX.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            this.numPosX.Name = "numPosX";
            this.numPosX.Size = new System.Drawing.Size(80, 22);
            this.numPosX.TabIndex = 3;
            this.numPosX.ValueChanged += new System.EventHandler(this.numPosX_ValueChanged);
            // 
            // lblPosY
            // 
            this.lblPosY.AutoSize = true;
            this.lblPosY.Location = new System.Drawing.Point(112, 95);
            this.lblPosY.Name = "lblPosY";
            this.lblPosY.Size = new System.Drawing.Size(18, 14);
            this.lblPosY.TabIndex = 4;
            this.lblPosY.Text = "Y:";
            // 
            // numPosY
            // 
            this.numPosY.Location = new System.Drawing.Point(10, 92);
            this.numPosY.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            this.numPosY.Name = "numPosY";
            this.numPosY.Size = new System.Drawing.Size(80, 22);
            this.numPosY.TabIndex = 5;
            this.numPosY.ValueChanged += new System.EventHandler(this.numPosY_ValueChanged);
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(210, 125);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(42, 14);
            this.lblWidth.TabIndex = 6;
            this.lblWidth.Text = "العرض:";
            // 
            // numWidth
            // 
            this.numWidth.Location = new System.Drawing.Point(140, 122);
            this.numWidth.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            this.numWidth.Minimum = new decimal(new int[] { 20, 0, 0, 0 });
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(65, 22);
            this.numWidth.TabIndex = 7;
            this.numWidth.Value = new decimal(new int[] { 200, 0, 0, 0 });
            this.numWidth.ValueChanged += new System.EventHandler(this.numWidth_ValueChanged);
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(85, 125);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(48, 14);
            this.lblHeight.TabIndex = 8;
            this.lblHeight.Text = "الارتفاع:";
            // 
            // numHeight
            // 
            this.numHeight.Location = new System.Drawing.Point(10, 122);
            this.numHeight.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            this.numHeight.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(65, 22);
            this.numHeight.TabIndex = 9;
            this.numHeight.Value = new decimal(new int[] { 30, 0, 0, 0 });
            this.numHeight.ValueChanged += new System.EventHandler(this.numHeight_ValueChanged);
            // 
            // lblFontSize
            // 
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.Location = new System.Drawing.Point(186, 158);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(63, 14);
            this.lblFontSize.TabIndex = 10;
            this.lblFontSize.Text = "حجم الخط:";
            // 
            // numFontSize
            // 
            this.numFontSize.Location = new System.Drawing.Point(140, 155);
            this.numFontSize.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numFontSize.Minimum = new decimal(new int[] { 6, 0, 0, 0 });
            this.numFontSize.Name = "numFontSize";
            this.numFontSize.Size = new System.Drawing.Size(42, 22);
            this.numFontSize.TabIndex = 11;
            this.numFontSize.Value = new decimal(new int[] { 14, 0, 0, 0 });
            this.numFontSize.ValueChanged += new System.EventHandler(this.numFontSize_ValueChanged);
            // 
            // chkBold
            // 
            this.chkBold.AutoSize = true;
            this.chkBold.Location = new System.Drawing.Point(60, 157);
            this.chkBold.Name = "chkBold";
            this.chkBold.Size = new System.Drawing.Size(56, 18);
            this.chkBold.TabIndex = 12;
            this.chkBold.Text = "عريض";
            this.chkBold.CheckedChanged += new System.EventHandler(this.chkBold_CheckedChanged);
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(210, 192);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(38, 14);
            this.lblColor.TabIndex = 13;
            this.lblColor.Text = "اللون:";
            // 
            // BtnPickColor
            // 
            this.BtnPickColor.Location = new System.Drawing.Point(140, 188);
            this.BtnPickColor.Name = "BtnPickColor";
            this.BtnPickColor.Size = new System.Drawing.Size(60, 23);
            this.BtnPickColor.TabIndex = 14;
            this.BtnPickColor.Text = "اختيار";
            this.BtnPickColor.Click += new System.EventHandler(this.BtnPickColor_Click);
            // 
            // pnlColorPreview
            // 
            this.pnlColorPreview.BackColor = System.Drawing.Color.Black;
            this.pnlColorPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColorPreview.Location = new System.Drawing.Point(100, 188);
            this.pnlColorPreview.Name = "pnlColorPreview";
            this.pnlColorPreview.Size = new System.Drawing.Size(30, 23);
            this.pnlColorPreview.TabIndex = 15;
            // 
            // lblAlignment
            // 
            this.lblAlignment.AutoSize = true;
            this.lblAlignment.Location = new System.Drawing.Point(202, 224);
            this.lblAlignment.Name = "lblAlignment";
            this.lblAlignment.Size = new System.Drawing.Size(50, 14);
            this.lblAlignment.TabIndex = 16;
            this.lblAlignment.Text = "المحاذاة:";
            // 
            // cmbAlignment
            // 
            this.cmbAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlignment.Items.AddRange(new object[] { "Left", "Center", "Right" });
            this.cmbAlignment.Location = new System.Drawing.Point(90, 220);
            this.cmbAlignment.Name = "cmbAlignment";
            this.cmbAlignment.Size = new System.Drawing.Size(110, 22);
            this.cmbAlignment.TabIndex = 17;
            this.cmbAlignment.SelectedIndexChanged += new System.EventHandler(this.cmbAlignment_SelectedIndexChanged);
            // 
            // BtnDeleteElement
            // 
            this.BtnDeleteElement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.BtnDeleteElement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeleteElement.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnDeleteElement.ForeColor = System.Drawing.Color.White;
            this.BtnDeleteElement.Location = new System.Drawing.Point(10, 260);
            this.BtnDeleteElement.Name = "BtnDeleteElement";
            this.BtnDeleteElement.Size = new System.Drawing.Size(248, 30);
            this.BtnDeleteElement.TabIndex = 18;
            this.BtnDeleteElement.Text = "🗑 حذف العنصر المحدد";
            this.BtnDeleteElement.UseVisualStyleBackColor = false;
            this.BtnDeleteElement.Click += new System.EventHandler(this.BtnDeleteElement_Click);
            // 
            // tabTemplates
            // 
            this.tabTemplates.Controls.Add(this.lstTemplates);
            this.tabTemplates.Controls.Add(this.grpTemplateActions);
            this.tabTemplates.Location = new System.Drawing.Point(4, 22);
            this.tabTemplates.Name = "tabTemplates";
            this.tabTemplates.Size = new System.Drawing.Size(268, 504);
            this.tabTemplates.TabIndex = 2;
            this.tabTemplates.Text = "القوالب";
            // 
            // grpTemplateActions
            // 
            this.grpTemplateActions.Controls.Add(this.BtnNewTemplate);
            this.grpTemplateActions.Controls.Add(this.BtnDeleteTemplate);
            this.grpTemplateActions.Controls.Add(this.BtnLoadTemplate);
            this.grpTemplateActions.Controls.Add(this.BtnSaveTemplate);
            this.grpTemplateActions.Controls.Add(this.lblTemplateName);
            this.grpTemplateActions.Controls.Add(this.txtTemplateName);
            this.grpTemplateActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpTemplateActions.Font = new System.Drawing.Font("Tahoma", 9F);
            this.grpTemplateActions.Location = new System.Drawing.Point(0, 0);
            this.grpTemplateActions.Name = "grpTemplateActions";
            this.grpTemplateActions.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grpTemplateActions.Size = new System.Drawing.Size(268, 200);
            this.grpTemplateActions.TabIndex = 0;
            this.grpTemplateActions.TabStop = false;
            this.grpTemplateActions.Text = "إدارة القوالب";
            // 
            // lblTemplateName
            // 
            this.lblTemplateName.AutoSize = true;
            this.lblTemplateName.Location = new System.Drawing.Point(195, 28);
            this.lblTemplateName.Name = "lblTemplateName";
            this.lblTemplateName.Size = new System.Drawing.Size(62, 14);
            this.lblTemplateName.TabIndex = 0;
            this.lblTemplateName.Text = "اسم القالب:";
            // 
            // txtTemplateName
            // 
            this.txtTemplateName.Location = new System.Drawing.Point(10, 45);
            this.txtTemplateName.Name = "txtTemplateName";
            this.txtTemplateName.Size = new System.Drawing.Size(248, 22);
            this.txtTemplateName.TabIndex = 1;
            // 
            // BtnSaveTemplate
            // 
            this.BtnSaveTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.BtnSaveTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSaveTemplate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnSaveTemplate.ForeColor = System.Drawing.Color.White;
            this.BtnSaveTemplate.Location = new System.Drawing.Point(135, 75);
            this.BtnSaveTemplate.Name = "BtnSaveTemplate";
            this.BtnSaveTemplate.Size = new System.Drawing.Size(123, 30);
            this.BtnSaveTemplate.TabIndex = 2;
            this.BtnSaveTemplate.Text = "💾 حفظ القالب";
            this.BtnSaveTemplate.UseVisualStyleBackColor = false;
            this.BtnSaveTemplate.Click += new System.EventHandler(this.BtnSaveTemplate_Click);
            // 
            // BtnLoadTemplate
            // 
            this.BtnLoadTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.BtnLoadTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLoadTemplate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnLoadTemplate.ForeColor = System.Drawing.Color.White;
            this.BtnLoadTemplate.Location = new System.Drawing.Point(10, 75);
            this.BtnLoadTemplate.Name = "BtnLoadTemplate";
            this.BtnLoadTemplate.Size = new System.Drawing.Size(120, 30);
            this.BtnLoadTemplate.TabIndex = 3;
            this.BtnLoadTemplate.Text = "📂 تحميل القالب";
            this.BtnLoadTemplate.UseVisualStyleBackColor = false;
            this.BtnLoadTemplate.Click += new System.EventHandler(this.BtnLoadTemplate_Click);
            // 
            // BtnDeleteTemplate
            // 
            this.BtnDeleteTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.BtnDeleteTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeleteTemplate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnDeleteTemplate.ForeColor = System.Drawing.Color.White;
            this.BtnDeleteTemplate.Location = new System.Drawing.Point(135, 115);
            this.BtnDeleteTemplate.Name = "BtnDeleteTemplate";
            this.BtnDeleteTemplate.Size = new System.Drawing.Size(123, 30);
            this.BtnDeleteTemplate.TabIndex = 4;
            this.BtnDeleteTemplate.Text = "🗑 حذف القالب";
            this.BtnDeleteTemplate.UseVisualStyleBackColor = false;
            this.BtnDeleteTemplate.Click += new System.EventHandler(this.BtnDeleteTemplate_Click);
            // 
            // BtnNewTemplate
            // 
            this.BtnNewTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.BtnNewTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNewTemplate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnNewTemplate.ForeColor = System.Drawing.Color.Black;
            this.BtnNewTemplate.Location = new System.Drawing.Point(10, 115);
            this.BtnNewTemplate.Name = "BtnNewTemplate";
            this.BtnNewTemplate.Size = new System.Drawing.Size(120, 30);
            this.BtnNewTemplate.TabIndex = 5;
            this.BtnNewTemplate.Text = "✨ قالب جديد";
            this.BtnNewTemplate.UseVisualStyleBackColor = false;
            this.BtnNewTemplate.Click += new System.EventHandler(this.BtnNewTemplate_Click);
            // 
            // lstTemplates
            // 
            this.lstTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTemplates.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lstTemplates.FormattingEnabled = true;
            this.lstTemplates.ItemHeight = 16;
            this.lstTemplates.Location = new System.Drawing.Point(0, 200);
            this.lstTemplates.Name = "lstTemplates";
            this.lstTemplates.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lstTemplates.Size = new System.Drawing.Size(268, 304);
            this.lstTemplates.TabIndex = 1;
            // 
            // FrmCertificateDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 610);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmCertificateDesigner";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "مصمم الشهادات";
            this.Load += new System.EventHandler(this.FrmCertificateDesigner_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabElements.ResumeLayout(false);
            this.grpDynamicFields.ResumeLayout(false);
            this.grpElements.ResumeLayout(false);
            this.tabProperties.ResumeLayout(false);
            this.grpProperties.ResumeLayout(false);
            this.grpProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            this.tabTemplates.ResumeLayout(false);
            this.grpTemplateActions.ResumeLayout(false);
            this.grpTemplateActions.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlCanvas;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabElements;
        private System.Windows.Forms.TabPage tabProperties;
        private System.Windows.Forms.TabPage tabTemplates;
        private System.Windows.Forms.GroupBox grpElements;
        private System.Windows.Forms.Button BtnAddText;
        private System.Windows.Forms.Button BtnAddImage;
        private System.Windows.Forms.Button BtnAddShape;
        private System.Windows.Forms.GroupBox grpDynamicFields;
        private System.Windows.Forms.Button BtnAddStudentName;
        private System.Windows.Forms.Button BtnAddClassName;
        private System.Windows.Forms.Button BtnAddTeacherName;
        private System.Windows.Forms.Button BtnAddAttendPct;
        private System.Windows.Forms.Button BtnAddIssueDate;
        private System.Windows.Forms.GroupBox grpProperties;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.TextBox txtElementText;
        private System.Windows.Forms.Label lblPosX;
        private System.Windows.Forms.NumericUpDown numPosX;
        private System.Windows.Forms.Label lblPosY;
        private System.Windows.Forms.NumericUpDown numPosY;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.Label lblFontSize;
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.CheckBox chkBold;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button BtnPickColor;
        private System.Windows.Forms.Panel pnlColorPreview;
        private System.Windows.Forms.Label lblAlignment;
        private System.Windows.Forms.ComboBox cmbAlignment;
        private System.Windows.Forms.Button BtnDeleteElement;
        private System.Windows.Forms.GroupBox grpTemplateActions;
        private System.Windows.Forms.Label lblTemplateName;
        private System.Windows.Forms.TextBox txtTemplateName;
        private System.Windows.Forms.Button BtnSaveTemplate;
        private System.Windows.Forms.Button BtnLoadTemplate;
        private System.Windows.Forms.Button BtnDeleteTemplate;
        private System.Windows.Forms.Button BtnNewTemplate;
        private System.Windows.Forms.ListBox lstTemplates;
    }
}
