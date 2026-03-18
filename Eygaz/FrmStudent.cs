using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eygaz
{
    public partial class FrmStudent : MetroFramework.Forms.MetroForm
    {
        Func f = new Func();
        private bool saveOrEdit;
        private DataTable dt;
        private string Tbl;
        private string Pky;
        private object Whr;
        private string Col;
        private string Whrstr;
        bool EditSuccess = false;
        // ========================
        // VARIABLES FOR GUARDIANS FORM
        // ========================
        private TextBox TxtId = new TextBox();
        private TextBox TxtGuardianId = new TextBox();
        private TextBox TxtFullName = new TextBox();
        private TextBox TxtPhone = new TextBox();
        private TextBox TxtAddress = new TextBox();
        private TextBox TxtNotes = new TextBox();
        private TextBox TxtAge = new TextBox();
        private DateTimePicker TxtBirthDate = new DateTimePicker();
        private ComboBox GenderDDL = new ComboBox();
        private DateTimePicker TxtCreatedAt = new DateTimePicker();
        private CheckBox ChkIsActive = new CheckBox();
        public FrmStudent()
        {
            InitializeComponent();
        }

        private void FrmStudent_Load(object sender, EventArgs e)
        {
            try
            {
                SetForm();
                inial();
            }
            catch (Exception ex)
            {

                // throw;
            }


        }
        private void inial()
        {
            clearCntrl();

            // تهيئة الحقول حسب رغبتك
            //CreatedAt.Value = DateTime.Now;

            FullName.Focus();
            f.DataCombo(Gender, "Flags", "FlagName", "FlagCode", " where FlagType=1 order by FlagCode desc");

        }
        private void clearCntrl()
        {
            // txtGuardianId.Text = "";
            TxtFullName.Text = "";
            // GenderDDL.SelectedIndex = 1;


            TxtPhone.Text = "";
            TxtBirthDate.Text = "";
            TxtAddress.Text = "";
            TxtNotes.Text = "";

        }
        private void SetForm()
        {
            Tbl = "Students";
            Pky = "Id";
            Whr = "";

            Col = "SELECT " +
                    "Id, " +
                    "FullName, " +
                    "Age," +
                    "BirthDate," +
                    "Address, " +
                    "Phone, " +
                    "Gender, " +
                    //  "EnrollmentDate"+
                    "IsActive, " +
                    "Notes, " +
                    "CreatedAt " +
                  "FROM " + Tbl + " ORDER BY " + Pky + "";

            // ربط الحقول
            TxtId = Id;
            TxtFullName = FullName;
            TxtAge = Age;
            TxtBirthDate = BirthDate;
            TxtAddress = Address;
            TxtPhone = Phone;
            GenderDDL = Gender;
            ChkIsActive = IsActive;

            TxtNotes = Notes;

            GrdDtl.OptionsBehavior.Editable = false;
            GrdDtl.RowHeight = 25;

            FillGrid();
        }
        private void FillGrid()
        {
            GVShow.DataSource = f.GetData(Col);
            if (GrdDtl.Columns.Count > 0)
            {
                int MaxFld = GrdDtl.Columns.Count;
                if (Func.vRtL)
                {
                    for (int i = 0; i < MaxFld; i++)
                        GrdDtl.Columns[i].VisibleIndex = MaxFld - i - 1;
                }
                TxtId = Id;
                TxtFullName = FullName;
                TxtAge = Age;
                TxtBirthDate = BirthDate;
                TxtAddress = Address;
                TxtPhone = Phone;
                GenderDDL = Gender;
                ChkIsActive = IsActive;
                GrdDtl.Columns[0].Caption = "الرقم";
                GrdDtl.Columns[1].Caption = "الاسم";
                GrdDtl.Columns[2].Caption = "العمر";
                GrdDtl.Columns[3].Caption = "تاريخ الميلاد";
                GrdDtl.Columns[4].Caption = "العنوان";
                GrdDtl.Columns[5].Caption = "التلفون";
                GrdDtl.Columns[6].Caption = "النوع";
                GrdDtl.Columns[7].Caption = "الحاله";
                GrdDtl.Columns[8].Caption = "الملاحظات";



                int ValAll = GVShow.Width - 20;
                for (int i = 0; i < MaxFld; i++)
                {
                    GrdDtl.Columns[i].Width = ValAll * 10 / 100;
                    if (i == 3 || i == 1 || i == 9 || i == 10)
                        GrdDtl.Columns[i].Width = ValAll * 20 / 100;
                    else if (i == 2)
                        GrdDtl.Columns[i].Width = ValAll * 15 / 100;
                }

            }

        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            try
            {
                f.ClearControl(PnlData);
                f.Dsbl_Enbl_Cntrl(PnlData, true);
                f.Dsbl_Enbl_But(PnlBut, false);
                string Sql = "SELECT MAX(" + Pky + ")  FROM " + Tbl + Whr;
                string maxid = f.GetScalar(Sql);
                if (!String.IsNullOrEmpty(maxid))
                {
                    TxtId.Text = (int.Parse(maxid) + 1).ToString();
                }
                else
                {
                    TxtId.Text = "1";

                }


                TxtId.Enabled = true;
                TxtId.ReadOnly = true;
                TxtFullName.Focus();


            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridSelect();
            }
            catch (Exception)
            {

                // throw;
            }
        }
        private void DataGridSelect()
        {
            try
            {
                f.Dsbl_Enbl_Cntrl(PnlData, false);
                clearCntrl();
                f.Dsbl_Enbl_But(PnlBut, true);
                f.Dsbl_Enbl_But(PnlSrh, true);
                GVShow.Enabled = true;

                if (GrdDtl.RowCount > 0)
                {
                    var row = GrdDtl.GetDataRow(GrdDtl.FocusedRowHandle);
                    if (row != null)
                    {
                        string id = row["Id"].ToString();

                        // 🔹 استخدم StringBuilder لكتابة استعلام جدول Students
                        var sb = new System.Text.StringBuilder();
                        sb.AppendLine("SELECT ");
                        sb.AppendLine("Id, FullName, BirthDate, Address, Phone, Gender, ");
                        sb.AppendLine("IsActive, Notes, CreatedAt ");
                        sb.AppendLine("FROM Students WHERE Id = " + id + ";");

                        DataTable tbl = f.GetData(sb.ToString());

                        if (tbl.Rows.Count > 0)
                        {
                            var dr = tbl.Rows[0];

                            // 🔹 تعبئة الحقول مع فحص القيم
                            if (!string.IsNullOrEmpty(dr["BirthDate"].ToString()))
                                BirthDate.Value = Convert.ToDateTime(dr["BirthDate"]);

                            TxtId.Text = dr["Id"].ToString();
                            FullName.Text = dr["FullName"].ToString();
                            Address.Text = dr["Address"].ToString();
                            Phone.Text = dr["Phone"].ToString();
                            Notes.Text = dr["Notes"].ToString();

                            // 🔹 Gender (ComboBox)
                            if (!string.IsNullOrEmpty(dr["Gender"].ToString()))
                                Gender.SelectedValue = dr["Gender"].ToString();
                            else
                                f.DataCombo(Gender, "GenderTypes", "Name", "Id", "");

                            // 🔹 IsActive (CheckBox)
                            IsActive.Checked = dr["IsActive"].ToString() == "1";
                        }
                    }
                }
                else
                {
                    BtnEdit.Enabled = false;
                    BtnDel.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                f.Dsbl_Enbl_Cntrl(PnlData, true);
                f.Dsbl_Enbl_But(PnlBut, false);
                Id.Enabled = false;
                Id.ReadOnly = true;
                saveOrEdit = true;



            }
            catch (Exception)
            {

                // throw;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // إعدادات عامة
                TxtId = Id;  // أصبح رقم الطالب
                Whr = "";           // لا يوجد شرط إضافي في البحث

                // التحقق من الحقول المطلوبة
                string msg = "يجب إدخال ";

                if (f.ChkField(Id, msg + lblId.Text)) return;
                if (f.ChkField(FullName, msg + lblFullName.Text)) return;
                if (f.ChkField(GenderDDL, msg + lblGender.Text)) return;

                string sql = "";

                // -------------------------------
                //  ✔ إضافة طالب جديد
                // -------------------------------
                if (Id.Enabled)
                {
                    // جلب أكبر رقم ID لعمل ID جديد
                    sql = $"SELECT MAX({Pky}) FROM {Tbl}";

                    string maxId = f.GetScalar(sql);

                    if (!string.IsNullOrEmpty(maxId))
                        Id.Text = (int.Parse(maxId) + 1).ToString();
                    else
                        Id.Text = "1";

                    // حفظ البيانات
                    if (!f.SaveRec(PnlData, Tbl))
                        return;
                    if (EditSuccess)
                        MessageBox.Show("تم الحفظ بنجاح ");
                }
                // -------------------------------
                //  ✔ تعديل بيانات طالب موجود
                // -------------------------------
                else
                {
                    sql = $"{Pky} = {Id.Text}";
                    sql = " WHERE " + sql;

                    EditSuccess = f.EditRec(PnlData, $"SELECT * FROM {Tbl} {sql}", Tbl);
                    if (EditSuccess)
                        MessageBox.Show("تم التعديل بنجاح ");


                }

                // -------------------------------
                //  ✔ تحديث الـ Grid وتحديد السطر السابق
                // -------------------------------
                int oldIndex = 0;
                if (GrdDtl.RowCount > 0)
                    oldIndex = GrdDtl.FocusedRowHandle;

                FillGrid();
                GrdDtl.FocusedRowHandle = oldIndex;

                DataGridSelect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BtnSrch_Click(object sender, EventArgs e)
        {
            f.Dsbl_Enbl_Cntrl(PnlData, true);
            f.Dsbl_Enbl_But(PnlBut, true);
            Whrstr = "";
            SetForm();
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "هل أنت متأكد أنك تريد الحذف ؟",
                    "حذف سجل",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    // 🔹 التحقق من وجود رقم الطالب قبل الحذف
                    if (string.IsNullOrEmpty(TxtId.Text))
                    {
                        MessageBox.Show("لا يوجد رقم لتحديد السجل المراد حذفه", "تنبيه");
                        return;
                    }

                    string tbl = "Students";
                    string pky = "Id";

                    // 🔹 بناء جملة الحذف بالشكل الصحيح
                    string sql = $"DELETE FROM {tbl} WHERE {pky} = {TxtId.Text}";

                    string res = f.Trans(sql);

                    if (res != "DONE...")
                    {
                        MessageBox.Show("حدثت مشكلة أثناء الحذف", "خطأ");
                    }
                    else
                    {
                        MessageBox.Show("تم الحذف بنجاح", "تنبيه");
                    }

                    // 🔹 تحديث العرض بعد الحذف
                    DataGridSelect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطأ");
            }

        }

        private void GrdDtl_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                DataGridSelect();
            }
            catch { }
        }

        private void Age_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Age.Text))
                return;

            if (!int.TryParse(Age.Text, out int age))
            {
                MessageBox.Show("الرجاء إدخال رقم صحيح", "تنبيه");
                Age.Clear();
                return;
            }

            // 🔹 فاليديشن العمر
            if (age < 0 || age > 100)
            {
                MessageBox.Show("العمر يجب أن يكون بين 0 و 100 سنة", "تنبيه");
                Age.Clear();
                return;
            }

            // 🔹 حساب تاريخ الميلاد
            DateTime birth = DateTime.Today.AddYears(-age);

            // 🔹 وضع التاريخ في الـ DateTimePicker
            BirthDate.Value = birth;
        }


        private void BirthDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime birth = BirthDate.Value;
            DateTime today = DateTime.Today;

            // حساب العمر
            int age = today.Year - birth.Year;

            if (birth.Date > today.AddYears(-age))
                age--;

            // 🔹 فاليديشن العمر (لا يزيد عن 100)
            if (age < 0 || age > 100)
            {
                MessageBox.Show("العمر يجب أن يكون بين 0 و 100 سنة", "تنبيه");

                // إعادة ضبط تاريخ الميلاد إلى تاريخ صحيح (100 سنة)
                BirthDate.Value = today.AddYears(-100);

                Age.Text = "100";
                return;
            }

            Age.Text = age.ToString();
        }

        private void Age_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // يمنع إدخال سطر جديد

                // هنا نفّذ ما تريد عند الضغط على Enter
                Age_TextChanged(sender, e);

                // أو أي دالة أخرى تريد تشغيلها
                // SaveData();
            }
        }

    }
}
