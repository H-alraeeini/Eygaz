namespace Eygaz
{
    partial class FrmDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.PnlCards = new System.Windows.Forms.FlowLayoutPanel();

            this.CardStudents = new System.Windows.Forms.Panel();
            this.lblStudentsTitle = new System.Windows.Forms.Label();
            this.lblStudentsCount = new System.Windows.Forms.Label();
            this.lblStudentsIcon = new System.Windows.Forms.Label();

            this.CardTeachers = new System.Windows.Forms.Panel();
            this.lblTeachersTitle = new System.Windows.Forms.Label();
            this.lblTeachersCount = new System.Windows.Forms.Label();
            this.lblTeachersIcon = new System.Windows.Forms.Label();

            this.CardSessions = new System.Windows.Forms.Panel();
            this.lblSessionsTitle = new System.Windows.Forms.Label();
            this.lblSessionsCount = new System.Windows.Forms.Label();
            this.lblSessionsIcon = new System.Windows.Forms.Label();

            this.CardPresent = new System.Windows.Forms.Panel();
            this.lblPresentTitle = new System.Windows.Forms.Label();
            this.lblPresentCount = new System.Windows.Forms.Label();
            this.lblPresentIcon = new System.Windows.Forms.Label();

            this.CardAbsent = new System.Windows.Forms.Panel();
            this.lblAbsentTitle = new System.Windows.Forms.Label();
            this.lblAbsentCount = new System.Windows.Forms.Label();
            this.lblAbsentIcon = new System.Windows.Forms.Label();

            this.CardLate = new System.Windows.Forms.Panel();
            this.lblLateTitle = new System.Windows.Forms.Label();
            this.lblLateCount = new System.Windows.Forms.Label();
            this.lblLateIcon = new System.Windows.Forms.Label();

            this.BtnRefresh = new System.Windows.Forms.Button();

            this.PnlCards.SuspendLayout();
            this.SuspendLayout();

            // ---- PnlCards (FlowLayout) ----
            this.PnlCards.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.PnlCards.Location = new System.Drawing.Point(10, 63);
            this.PnlCards.Name = "PnlCards";
            this.PnlCards.Size = new System.Drawing.Size(780, 360);
            this.PnlCards.Padding = new System.Windows.Forms.Padding(5);

            // ---- Helper: Card dimensions ----
            int cardW = 240, cardH = 110;

            // ===== Card: Students =====
            SetupCard(this.CardStudents, this.lblStudentsIcon, this.lblStudentsTitle,
                this.lblStudentsCount, "👨‍🎓", "عدد الطلاب", "0",
                System.Drawing.Color.FromArgb(41, 128, 185), cardW, cardH);

            // ===== Card: Teachers =====
            SetupCard(this.CardTeachers, this.lblTeachersIcon, this.lblTeachersTitle,
                this.lblTeachersCount, "👨‍🏫", "عدد المدرسين", "0",
                System.Drawing.Color.FromArgb(142, 68, 173), cardW, cardH);

            // ===== Card: Today Sessions =====
            SetupCard(this.CardSessions, this.lblSessionsIcon, this.lblSessionsTitle,
                this.lblSessionsCount, "📋", "جلسات اليوم", "0",
                System.Drawing.Color.FromArgb(39, 174, 96), cardW, cardH);

            // ===== Card: Today Present =====
            SetupCard(this.CardPresent, this.lblPresentIcon, this.lblPresentTitle,
                this.lblPresentCount, "✅", "الحاضرون اليوم", "0",
                System.Drawing.Color.FromArgb(22, 160, 133), cardW, cardH);

            // ===== Card: Today Absent =====
            SetupCard(this.CardAbsent, this.lblAbsentIcon, this.lblAbsentTitle,
                this.lblAbsentCount, "❌", "الغائبون اليوم", "0",
                System.Drawing.Color.FromArgb(192, 57, 43), cardW, cardH);

            // ===== Card: Today Late =====
            SetupCard(this.CardLate, this.lblLateIcon, this.lblLateTitle,
                this.lblLateCount, "⏰", "المتأخرون اليوم", "0",
                System.Drawing.Color.FromArgb(243, 156, 18), cardW, cardH);

            // Add cards to FlowLayout
            this.PnlCards.Controls.Add(this.CardStudents);
            this.PnlCards.Controls.Add(this.CardTeachers);
            this.PnlCards.Controls.Add(this.CardSessions);
            this.PnlCards.Controls.Add(this.CardPresent);
            this.PnlCards.Controls.Add(this.CardAbsent);
            this.PnlCards.Controls.Add(this.CardLate);

            // ---- BtnRefresh ----
            this.BtnRefresh.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.BtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.BtnRefresh.ForeColor = System.Drawing.Color.White;
            this.BtnRefresh.Location = new System.Drawing.Point(340, 440);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(120, 35);
            this.BtnRefresh.Text = "تحديث البيانات";
            this.BtnRefresh.UseVisualStyleBackColor = false;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);

            // ---- FrmDashboard ----
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.BtnRefresh);
            this.Controls.Add(this.PnlCards);
            this.Name = "FrmDashboard";
            this.Text = "لوحة المتابعة";
            this.Load += new System.EventHandler(this.FrmDashboard_Load);
            this.PnlCards.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// إعداد بطاقة إحصائية
        /// </summary>
        private void SetupCard(System.Windows.Forms.Panel card,
            System.Windows.Forms.Label icon, System.Windows.Forms.Label title,
            System.Windows.Forms.Label count, string iconText, string titleText,
            string countText, System.Drawing.Color bgColor, int w, int h)
        {
            card.BackColor = bgColor;
            card.Size = new System.Drawing.Size(w, h);
            card.Margin = new System.Windows.Forms.Padding(8);
            card.Padding = new System.Windows.Forms.Padding(10);

            icon.Text = iconText;
            icon.Font = new System.Drawing.Font("Segoe UI Emoji", 24F);
            icon.ForeColor = System.Drawing.Color.White;
            icon.Location = new System.Drawing.Point(w - 65, 10);
            icon.AutoSize = true;

            title.Text = titleText;
            title.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            title.ForeColor = System.Drawing.Color.White;
            title.Location = new System.Drawing.Point(10, 15);
            title.AutoSize = true;

            count.Text = countText;
            count.Font = new System.Drawing.Font("Tahoma", 28F, System.Drawing.FontStyle.Bold);
            count.ForeColor = System.Drawing.Color.White;
            count.Location = new System.Drawing.Point(10, 50);
            count.AutoSize = true;

            card.Controls.Add(icon);
            card.Controls.Add(title);
            card.Controls.Add(count);
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel PnlCards;

        private System.Windows.Forms.Panel CardStudents;
        private System.Windows.Forms.Label lblStudentsTitle;
        private System.Windows.Forms.Label lblStudentsCount;
        private System.Windows.Forms.Label lblStudentsIcon;

        private System.Windows.Forms.Panel CardTeachers;
        private System.Windows.Forms.Label lblTeachersTitle;
        private System.Windows.Forms.Label lblTeachersCount;
        private System.Windows.Forms.Label lblTeachersIcon;

        private System.Windows.Forms.Panel CardSessions;
        private System.Windows.Forms.Label lblSessionsTitle;
        private System.Windows.Forms.Label lblSessionsCount;
        private System.Windows.Forms.Label lblSessionsIcon;

        private System.Windows.Forms.Panel CardPresent;
        private System.Windows.Forms.Label lblPresentTitle;
        private System.Windows.Forms.Label lblPresentCount;
        private System.Windows.Forms.Label lblPresentIcon;

        private System.Windows.Forms.Panel CardAbsent;
        private System.Windows.Forms.Label lblAbsentTitle;
        private System.Windows.Forms.Label lblAbsentCount;
        private System.Windows.Forms.Label lblAbsentIcon;

        private System.Windows.Forms.Panel CardLate;
        private System.Windows.Forms.Label lblLateTitle;
        private System.Windows.Forms.Label lblLateCount;
        private System.Windows.Forms.Label lblLateIcon;

        private System.Windows.Forms.Button BtnRefresh;
    }
}
