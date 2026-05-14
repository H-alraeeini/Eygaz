namespace Eygaz
{
    partial class FrmComprehensiveGradeReport
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
            this.CmbClass = new System.Windows.Forms.ComboBox();
            this.CmbTerm = new System.Windows.Forms.ComboBox();
            this.CmbGradeYear = new System.Windows.Forms.ComboBox();
            this.CmbGradeMonth = new System.Windows.Forms.ComboBox();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.BtnShow = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.BtnExportExcel = new System.Windows.Forms.Button();
            this.BtnExportPdf = new System.Windows.Forms.Button();
            this.LblSummary = new System.Windows.Forms.Label();
            this.GVSheet = new DevExpress.XtraGrid.GridControl();
            this.GrdSheet = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.GVSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdSheet)).BeginInit();
            this.SuspendLayout();
            // 
            // CmbClass
            // 
            this.CmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbClass.Location = new System.Drawing.Point(730, 65);
            this.CmbClass.Name = "CmbClass";
            this.CmbClass.Size = new System.Drawing.Size(150, 21);
            this.CmbClass.TabIndex = 0;
            // 
            // CmbTerm
            // 
            this.CmbTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbTerm.Location = new System.Drawing.Point(577, 65);
            this.CmbTerm.Name = "CmbTerm";
            this.CmbTerm.Size = new System.Drawing.Size(150, 21);
            this.CmbTerm.TabIndex = 1;
            // 
            // CmbGradeYear
            // 
            this.CmbGradeYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGradeYear.Location = new System.Drawing.Point(510, 65);
            this.CmbGradeYear.Name = "CmbGradeYear";
            this.CmbGradeYear.Size = new System.Drawing.Size(65, 21);
            this.CmbGradeYear.TabIndex = 9;
            // 
            // CmbGradeMonth
            // 
            this.CmbGradeMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGradeMonth.Location = new System.Drawing.Point(376, 65);
            this.CmbGradeMonth.Name = "CmbGradeMonth";
            this.CmbGradeMonth.Size = new System.Drawing.Size(126, 21);
            this.CmbGradeMonth.TabIndex = 10;
            // 
            // TxtSearch
            // 
            this.TxtSearch.Location = new System.Drawing.Point(272, 65);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(97, 20);
            this.TxtSearch.TabIndex = 2;
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(190, 63);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(80, 24);
            this.BtnShow.TabIndex = 3;
            this.BtnShow.Text = "عرض الكشف";
            this.BtnShow.UseVisualStyleBackColor = true;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Location = new System.Drawing.Point(105, 63);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(80, 24);
            this.BtnPrint.TabIndex = 4;
            this.BtnPrint.Text = "طباعة";
            this.BtnPrint.UseVisualStyleBackColor = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnExportExcel
            // 
            this.BtnExportExcel.Location = new System.Drawing.Point(20, 63);
            this.BtnExportExcel.Name = "BtnExportExcel";
            this.BtnExportExcel.Size = new System.Drawing.Size(80, 24);
            this.BtnExportExcel.TabIndex = 5;
            this.BtnExportExcel.Text = "Excel";
            this.BtnExportExcel.UseVisualStyleBackColor = true;
            this.BtnExportExcel.Click += new System.EventHandler(this.BtnExportExcel_Click);
            // 
            // BtnExportPdf
            // 
            this.BtnExportPdf.Location = new System.Drawing.Point(20, 92);
            this.BtnExportPdf.Name = "BtnExportPdf";
            this.BtnExportPdf.Size = new System.Drawing.Size(80, 24);
            this.BtnExportPdf.TabIndex = 6;
            this.BtnExportPdf.Text = "PDF";
            this.BtnExportPdf.UseVisualStyleBackColor = true;
            this.BtnExportPdf.Click += new System.EventHandler(this.BtnExportPdf_Click);
            // 
            // LblSummary
            // 
            this.LblSummary.Location = new System.Drawing.Point(23, 120);
            this.LblSummary.Name = "LblSummary";
            this.LblSummary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblSummary.Size = new System.Drawing.Size(857, 23);
            this.LblSummary.TabIndex = 7;
            this.LblSummary.Text = "ملخص";
            // 
            // GVSheet
            // 
            this.GVSheet.Location = new System.Drawing.Point(23, 146);
            this.GVSheet.MainView = this.GrdSheet;
            this.GVSheet.Name = "GVSheet";
            this.GVSheet.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GVSheet.Size = new System.Drawing.Size(857, 390);
            this.GVSheet.TabIndex = 8;
            this.GVSheet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdSheet});
            // 
            // GrdSheet
            // 
            this.GrdSheet.GridControl = this.GVSheet;
            this.GrdSheet.Name = "GrdSheet";
            this.GrdSheet.OptionsView.ColumnAutoWidth = false;
            // 
            // FrmComprehensiveGradeReport
            // 
            this.ClientSize = new System.Drawing.Size(903, 560);
            this.Controls.Add(this.GVSheet);
            this.Controls.Add(this.LblSummary);
            this.Controls.Add(this.BtnExportPdf);
            this.Controls.Add(this.BtnExportExcel);
            this.Controls.Add(this.BtnPrint);
            this.Controls.Add(this.BtnShow);
            this.Controls.Add(this.TxtSearch);
            this.Controls.Add(this.CmbGradeMonth);
            this.Controls.Add(this.CmbGradeYear);
            this.Controls.Add(this.CmbTerm);
            this.Controls.Add(this.CmbClass);
            this.Name = "FrmComprehensiveGradeReport";
            this.Text = "كشف درجات الطلاب (شامل)";
            this.Load += new System.EventHandler(this.FrmComprehensiveGradeReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GVSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdSheet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox CmbClass;
        private System.Windows.Forms.ComboBox CmbTerm;
        private System.Windows.Forms.ComboBox CmbGradeYear;
        private System.Windows.Forms.ComboBox CmbGradeMonth;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Button BtnShow;
        private System.Windows.Forms.Button BtnPrint;
        private System.Windows.Forms.Button BtnExportExcel;
        private System.Windows.Forms.Button BtnExportPdf;
        private System.Windows.Forms.Label LblSummary;
        private DevExpress.XtraGrid.GridControl GVSheet;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdSheet;
    }
}
