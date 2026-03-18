using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class FrmStudentCertificates : MetroFramework.Forms.MetroForm
    {
        Func f = new Func();
        AttendanceHelper helper = new AttendanceHelper();

        // بيانات الشهادة الحالية
        private double currentPct = 0;
        private int currentTotal = 0;
        private int currentAbsent = 0;
        private int currentPresent = 0;
        private string currentTeacher = "";
        private bool dataLoaded = false;

        public FrmStudentCertificates()
        {
            InitializeComponent();
        }

        // =============================================
        // تحميل الشاشة
        // =============================================
        private void FrmStudentCertificates_Load(object sender, EventArgs e)
        {
            try
            {
                // تعبئة الفصول
                f.DataCombo(CmbClass, "Classes", "ClassName", "Id", " WHERE IsActive = 1 ORDER BY ClassName");

                // أنواع الشهادات
                CmbCertType.Items.Clear();
                CmbCertType.Items.Add("شهادة حضور");
                CmbCertType.Items.Add("شهادة إتمام برنامج");
                CmbCertType.Items.Add("شهادة تقدير");
                CmbCertType.Items.Add("شهادة تفوق");
                CmbCertType.SelectedIndex = 0;

                DtIssueDate.Value = DateTime.Today;

                // تعطيل الأزرار حتى يتم تحميل البيانات
                BtnPreview.Enabled = false;
                BtnPrint.Enabled = false;
                BtnSaveRecord.Enabled = false;

                // تحميل سجل الشهادات
                LoadCertificateHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // عند اختيار الفصل → تحميل الطلاب
        // =============================================
        private void CmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CmbClass.SelectedValue != null && CmbClass.SelectedValue.ToString() != "")
                {
                    int classId = Convert.ToInt32(CmbClass.SelectedValue);
                    f.DataCombo(CmbStudent, "Students", "FullName", "Id",
                        $" WHERE ClassId = {classId} AND IsActive = 1 ORDER BY FullName");
                }
            }
            catch { }
        }

        // =============================================
        // تحميل بيانات الشهادة
        // =============================================
        private void BtnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbClass.SelectedValue == null)
                {
                    MessageBox.Show("يرجى اختيار الفصل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (CmbStudent.SelectedValue == null)
                {
                    MessageBox.Show("يرجى اختيار الطالب", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int studentId = Convert.ToInt32(CmbStudent.SelectedValue);
                int classId = Convert.ToInt32(CmbClass.SelectedValue);

                DataTable stats = helper.GetStudentCertificateStats(studentId, classId);

                if (stats == null || stats.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات حضور لهذا الطالب في هذا الفصل", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearInfo();
                    return;
                }

                var dr = stats.Rows[0];
                txtStudentName.Text = dr["StudentName"].ToString();
                txtClassName.Text = dr["ClassName"].ToString();

                currentTotal = Convert.ToInt32(dr["TotalSessions"]);
                currentPresent = Convert.ToInt32(dr["PresentCount"]);
                currentAbsent = Convert.ToInt32(dr["AbsentCount"]);
                currentPct = Convert.ToDouble(dr["AttendancePercentage"]);

                txtTotalSessions.Text = currentTotal.ToString();
                txtPresentCount.Text = currentPresent.ToString();
                txtAbsentCount.Text = currentAbsent.ToString();
                txtAttendancePct.Text = currentPct.ToString("F1");

                // تلوين النسبة
                if (currentPct >= 90)
                    txtAttendancePct.ForeColor = Color.ForestGreen;
                else if (currentPct >= 75)
                    txtAttendancePct.ForeColor = Color.DarkOrange;
                else
                    txtAttendancePct.ForeColor = Color.Red;

                // جلب المدرس الرئيسي
                currentTeacher = helper.GetMainTeacher(studentId, classId) ?? "";
                txtTeacherName.Text = currentTeacher;

                dataLoaded = true;
                BtnPreview.Enabled = true;
                BtnPrint.Enabled = true;
                BtnSaveRecord.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInfo()
        {
            txtStudentName.Text = "";
            txtClassName.Text = "";
            txtTeacherName.Text = "";
            txtAttendancePct.Text = "";
            txtTotalSessions.Text = "";
            txtAbsentCount.Text = "";
            txtPresentCount.Text = "";
            dataLoaded = false;
            BtnPreview.Enabled = false;
            BtnPrint.Enabled = false;
            BtnSaveRecord.Enabled = false;
        }

        // =============================================
        // إنشاء مستند الطباعة
        // =============================================
        private CertificatePrintDocument CreateCertificateDoc()
        {
            var doc = new CertificatePrintDocument();
            doc.StudentName = txtStudentName.Text;
            doc.ClassName = txtClassName.Text;
            doc.TeacherName = txtTeacherName.Text;
            doc.CertificateType = CmbCertType.SelectedItem?.ToString() ?? "شهادة حضور";
            doc.IssueDate = DtIssueDate.Value.ToString("yyyy/MM/dd");
            doc.AttendancePercentage = currentPct;
            doc.TotalSessions = currentTotal;
            doc.AbsentCount = currentAbsent;
            doc.PresentCount = currentPresent;
            doc.DefaultPageSettings.Landscape = true;
            return doc;
        }

        // =============================================
        // معاينة الشهادة
        // =============================================
        private void BtnPreview_Click(object sender, EventArgs e)
        {
            if (!dataLoaded)
            {
                MessageBox.Show("يرجى تحميل البيانات أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var doc = CreateCertificateDoc();
                PrintPreviewDialog preview = new PrintPreviewDialog();
                preview.Document = doc;
                preview.Width = 900;
                preview.Height = 700;
                preview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء معاينة الشهادة: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // طباعة مباشرة
        // =============================================
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (!dataLoaded)
            {
                MessageBox.Show("يرجى تحميل البيانات أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var doc = CreateCertificateDoc();
                PrintDialog printDlg = new PrintDialog();
                printDlg.Document = doc;

                if (printDlg.ShowDialog() == DialogResult.OK)
                {
                    doc.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء الطباعة: " + ex.Message, "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // حفظ سجل الشهادة
        // =============================================
        private void BtnSaveRecord_Click(object sender, EventArgs e)
        {
            if (!dataLoaded)
            {
                MessageBox.Show("يرجى تحميل البيانات أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int studentId = Convert.ToInt32(CmbStudent.SelectedValue);
                int classId = Convert.ToInt32(CmbClass.SelectedValue);
                string certType = CmbCertType.SelectedItem?.ToString() ?? "شهادة حضور";
                string issueDate = DtIssueDate.Value.ToString("yyyy-MM-dd");

                bool saved = helper.SaveCertificate(studentId, classId, certType,
                    issueDate, currentPct, currentTotal, currentAbsent, currentPresent,
                    currentTeacher, "");

                if (saved)
                {
                    MessageBox.Show("تم حفظ سجل الشهادة بنجاح", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCertificateHistory();
                }
                else
                {
                    MessageBox.Show("حدث خطأ أثناء الحفظ", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // تحميل سجل الشهادات
        // =============================================
        private void LoadCertificateHistory()
        {
            try
            {
                DataTable dt = helper.GetCertificateHistory(0); // جميع الشهادات
                GVCertificates.DataSource = dt;

                if (GrdCertificates.Columns.Count > 0)
                {
                    if (GrdCertificates.Columns.ColumnByFieldName("Id") != null)
                        GrdCertificates.Columns["Id"].Visible = false;

                    GrdCertificates.BestFitColumns();

                    if (Func.vRtL)
                    {
                        int maxCol = GrdCertificates.Columns.Count;
                        for (int i = 0; i < maxCol; i++)
                            GrdCertificates.Columns[i].VisibleIndex = maxCol - i - 1;
                    }
                }
            }
            catch { }
        }
    }
}
