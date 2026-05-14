using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraPrinting.Export.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Eygaz
{
    public partial class FrmSubject : MetroFramework.Forms.MetroForm
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
        private TextBox TxtSubjectName = new TextBox();
        private TextBox TxtDescription = new TextBox();
        private TextBox TxtNotes = new TextBox();
        private CheckBox ChkIsActive = new CheckBox();
        public FrmSubject()
        {
            InitializeComponent();
        }

        private void FrmSubject_Load(object sender, EventArgs e)
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

            SubjectName.Focus();

        }
        private void clearCntrl()
        {
            TxtSubjectName.Text = "";
            TxtDescription.Text = "";
            DisplayOrder.Text = "0";
        }
        private void SetForm()
        {
            Tbl = "Subjects";
            Pky = "Id";
            Whr = "";

            Col = "SELECT " +
                    "Id, " +
                    "SubjectName, " +
                    "Description, " +
                    "DisplayOrder, " +
                    "IsActive, " +
                    "CreatedAt " +
                  "FROM " + Tbl + " ORDER BY DisplayOrder, SubjectName, " + Pky + "";


            // ربط الحقول
            TxtId = Id;
            TxtSubjectName = SubjectName;
            ChkIsActive = IsActive;
            TxtDescription = Description;

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

                GrdDtl.Columns[0].Caption = "الرقم";
                GrdDtl.Columns[1].Caption = "الاسم";
                GrdDtl.Columns[2].Caption = "الوصف";
                GrdDtl.Columns[3].Caption = "ترتيب العرض";
                GrdDtl.Columns[4].Caption = "الحاله";
                if (GrdDtl.Columns.Count > 5)
                    GrdDtl.Columns[5].Caption = "تاريخ الإنشاء";

                int ValAll = GVShow.Width - 20;
                for (int i = 0; i < MaxFld; i++)
                {
                    GrdDtl.Columns[i].Width = ValAll * 9 / 100;
                    if (i == 1 || i == 2)
                        GrdDtl.Columns[i].Width = ValAll * 18 / 100;
                    else if (i == 3 || i == 4)
                        GrdDtl.Columns[i].Width = ValAll * 10 / 100;
                }

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

                        // 🔹 استخدم StringBuilder لكتابة استعلام جدول Subjects
                        var sb = new System.Text.StringBuilder();
                        sb.AppendLine("SELECT ");
                        sb.AppendLine("Id, SubjectName, Description, ");
                        sb.AppendLine("DisplayOrder, IsActive, CreatedAt ");
                        sb.AppendLine("FROM Subjects WHERE Id = " + id + ";");

                        DataTable tbl = f.GetData(sb.ToString());

                        if (tbl.Rows.Count > 0)
                        {
                            var dr = tbl.Rows[0];

                            // 🔹 تعبئة الحقول مع فحص القيم


                            TxtId.Text = dr["Id"].ToString();
                            SubjectName.Text = dr["SubjectName"].ToString();
                            Description.Text = dr["Description"].ToString();
                            DisplayOrder.Text = dr.Table.Columns.Contains("DisplayOrder") && dr["DisplayOrder"] != DBNull.Value
                                ? dr["DisplayOrder"].ToString()
                                : "0";

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
        private void BtnNew_Click(object sender, EventArgs e)
        {
            try
            {
                f.ClearControl(PnlData);
                f.Dsbl_Enbl_Cntrl(PnlData, true);
                ApplySubjectEditingRestrictions();
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
                TxtSubjectName.Focus();


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

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                f.Dsbl_Enbl_Cntrl(PnlData, true);
                ApplySubjectEditingRestrictions();
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
                if (f.ChkField(SubjectName, msg + lblName.Text)) return;

                if (!int.TryParse(DisplayOrder.Text.Trim(), out int displayOrderVal) || displayOrderVal < 0)
                {
                    MessageBox.Show("ترتيب العرض يجب أن يكون رقماً صحيحاً (0 أو أكبر).", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DisplayOrder.Focus();
                    return;
                }

                DisplayOrder.Text = displayOrderVal.ToString();

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

                    string tbl = "Subjects";
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

        /// <summary>
        /// مادة المواظبة / الدرجات المستمدة من الحضور تُعرَف بالوصف أو الاسم؛ الاسم يجب أن يبقى كما يتوقعه AttendanceHelper.
        /// يُسمح بتعديل ترتيب العرض والوصف والحالة دون تغيير الاسم.
        /// </summary>
        private static bool IsAttendanceDerivedSubject(string subjectName, string description)
        {
            string name = (subjectName ?? string.Empty).Trim();
            string desc = (description ?? string.Empty).Trim();

            if (name.Length > 0 && name.StartsWith("\u0627\u0644\u0645\u0648\u0627\u0638\u0628\u0629", StringComparison.Ordinal))
                return true;

            if (desc.IndexOf("\u0645\u062d\u0633\u0648\u0628\u0629", StringComparison.Ordinal) >= 0
                && (desc.IndexOf("\u0627\u0644\u062d\u0636\u0648\u0631", StringComparison.Ordinal) >= 0
                    || desc.IndexOf("\u063a\u064a\u0627\u0628", StringComparison.Ordinal) >= 0))
                return true;

            return false;
        }

        /// <summary>
        /// بعد <see cref="Func.Dsbl_Enbl_Cntrl"/> في وضع التعديل: يضمن إمكانية تعديل ترتيب العرض للمواد المحسوبة من الحضور.
        /// </summary>
        private void ApplySubjectEditingRestrictions()
        {
            bool derived = IsAttendanceDerivedSubject(SubjectName.Text, Description.Text);

            if (derived)
            {
                SubjectName.ReadOnly = true;
                Description.ReadOnly = false;
                DisplayOrder.ReadOnly = false;
                DisplayOrder.Enabled = true;
                IsActive.Enabled = true;
            }
            else
            {
                SubjectName.ReadOnly = false;
                Description.ReadOnly = false;
                DisplayOrder.ReadOnly = false;
                DisplayOrder.Enabled = true;
                IsActive.Enabled = true;
            }

            Id.ReadOnly = true;
        }
    }
}
