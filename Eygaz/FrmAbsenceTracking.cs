using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class FrmAbsenceTracking : MetroFramework.Forms.MetroForm
    {
        Func f = new Func();
        AttendanceHelper helper = new AttendanceHelper();
        private Label lblHijriFrom;
        private Label lblHijriTo;

        public FrmAbsenceTracking()
        {
            InitializeComponent();
        }

        private void FrmAbsenceTracking_Load(object sender, EventArgs e)
        {
            try
            {
                f.DataComboWithNull(CmbClass, "Classes", "ClassName", "Id", " WHERE IsActive = 0 ORDER BY ClassName");

                DtFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DtTo.Value = DateTime.Today;
                NumMinAbsences.Value = 1;
                EnsureHijriDateLabels();
                DtFrom.ValueChanged += DateFilters_ValueChanged;
                DtTo.ValueChanged += DateFilters_ValueChanged;
                UpdateHijriDateLabels();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnsureHijriDateLabels()
        {
            if (lblHijriFrom == null)
            {
                lblHijriFrom = new Label();
                lblHijriFrom.AutoSize = true;
                lblHijriFrom.ForeColor = Color.DarkSlateBlue;
                lblHijriFrom.Font = new Font("Tahoma", 7.5F, FontStyle.Bold);
                lblHijriFrom.Location = new System.Drawing.Point(460, 35);
                lblHijriFrom.Name = "lblHijriFrom";
                lblHijriFrom.RightToLeft = RightToLeft.No;
                PnlFilter.Controls.Add(lblHijriFrom);
            }

            if (lblHijriTo == null)
            {
                lblHijriTo = new Label();
                lblHijriTo.AutoSize = true;
                lblHijriTo.ForeColor = Color.DarkSlateBlue;
                lblHijriTo.Font = new Font("Tahoma", 7.5F, FontStyle.Bold);
                lblHijriTo.Location = new System.Drawing.Point(325, 35);
                lblHijriTo.Name = "lblHijriTo";
                lblHijriTo.RightToLeft = RightToLeft.No;
                PnlFilter.Controls.Add(lblHijriTo);
            }
        }

        private void DateFilters_ValueChanged(object sender, EventArgs e)
        {
            UpdateHijriDateLabels();
        }

        private void UpdateHijriDateLabels()
        {
            if (lblHijriFrom != null)
                lblHijriFrom.Text = AttendanceHelper.ToHijriDateDisplayArabic(DtFrom.Value.Date);
            if (lblHijriTo != null)
                lblHijriTo.Text = AttendanceHelper.ToHijriDateDisplayArabic(DtTo.Value.Date);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateHijriDateLabels();
                int classId = CmbClass.SelectedValue != null ? Convert.ToInt32(CmbClass.SelectedValue) : 0;
                string dateFrom = DtFrom.Value.ToString("yyyy-MM-dd");
                string dateTo = DtTo.Value.ToString("yyyy-MM-dd");
                int minAbsences = (int)NumMinAbsences.Value;

                DataTable dt = helper.GetAbsenceTracking(classId, dateFrom, dateTo, minAbsences);

                if (dt != null && dt.Rows.Count > 0)
                {
                    GVAbsence.DataSource = dt;

                    // إخفاء عمود StudentId
                    if (GrdAbsence.Columns.ColumnByFieldName("StudentId") != null)
                        GrdAbsence.Columns["StudentId"].Visible = false;

                    GrdAbsence.BestFitColumns();

                    // تلوين الصفوف حسب نسبة الحضور
                    GrdAbsence.RowCellStyle += (s, ev) =>
                    {
                        if (ev.Column.FieldName == "نسبة الحضور %")
                        {
                            var val = GrdAbsence.GetRowCellValue(ev.RowHandle, ev.Column);
                            if (val != null && val != DBNull.Value)
                            {
                                double pct = Convert.ToDouble(val);
                                if (pct < 50)
                                    ev.Appearance.ForeColor = System.Drawing.Color.Red;
                                else if (pct < 75)
                                    ev.Appearance.ForeColor = System.Drawing.Color.Orange;
                                else
                                    ev.Appearance.ForeColor = System.Drawing.Color.Green;
                                ev.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
                            }
                        }
                    };

                    // RTL
                    if (Func.vRtL && GrdAbsence.Columns.Count > 0)
                    {
                        int maxCol = GrdAbsence.Columns.Count;
                        for (int i = 0; i < maxCol; i++)
                            GrdAbsence.Columns[i].VisibleIndex = maxCol - i - 1;
                    }
                }
                else
                {
                    GVAbsence.DataSource = null;
                    MessageBox.Show("لا توجد بيانات غياب تطابق الشروط المحددة", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
