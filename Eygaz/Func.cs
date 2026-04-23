using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eygaz
{
    public class Func
    {
        public static string dbname;
        public static string Dbpwd;
        //public SQLiteConnection cn = new SQLiteConnection(@"Password='" + Dbpwd + "';Initial Catalog='" + dbname + "'");
        public SQLiteConnection cn = new SQLiteConnection(@"Data Source=" + dbname + "");
        public SQLiteCommand cmd;
        private int RowEff;
        private int alterdep;
        private string retdep;
        private string Reslt10;
        public static bool vRtL;

        public string Trans(String sql)
        {
            string str = "";
            cn = new SQLiteConnection(@"Data Source=" + dbname + "");
            cmd = new SQLiteCommand(sql, cn);
            try
            {
                cmd.Connection.Open();
                RowEff = cmd.ExecuteNonQuery();

                alterdep = 0;
                retdep = "DONE...";
            }
            catch (SQLiteException ex)
            {
                ex.Message.ToString();
                retdep = ex.Message.ToString();
                alterdep = 1;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return retdep;
        }
        public string GetScalar(String sqlget)
        {
            string str = "";
            Reslt10 = "";
            cn = new SQLiteConnection(@"Data Source=" + dbname + "");
            string ret = "true";
            cmd = new SQLiteCommand(sqlget, cn);
            try
            {

                cn.Open();
                ret = cmd.ExecuteScalar().ToString();

            }
            catch (Exception ex)
            {
                Reslt10 = ex.Message;
                ret = "";
            }
            finally
            {
                cn.Close();
            }
            return ret;
        }
        public DataTable GetData(string sql)
        {

            cn = new SQLiteConnection(@"Data Source=" + dbname + "");
            string ret = "true";
            SQLiteDataAdapter da3 = new SQLiteDataAdapter(sql, cn);
            DataTable ds3 = new DataTable();
            try
            {
                cn.Open();
                da3.Fill(ds3);
                return ds3;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return null;
        }

        public DataTable GetData(string sql, Dictionary<string, object> parameters)
        {
            cn = new SQLiteConnection(@"Data Source=" + dbname + "");
            DataTable table = new DataTable();
            try
            {
                cn.Open();
                using (SQLiteCommand localCmd = new SQLiteCommand(sql, cn))
                {
                    AddParameters(localCmd, parameters);
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(localCmd))
                    {
                        da.Fill(table);
                    }
                }
                return table;
            }
            catch
            {
                return null;
            }
            finally
            {
                cn.Close();
            }
        }

        public string GetScalar(string sql, Dictionary<string, object> parameters)
        {
            cn = new SQLiteConnection(@"Data Source=" + dbname + "");
            try
            {
                cn.Open();
                using (SQLiteCommand localCmd = new SQLiteCommand(sql, cn))
                {
                    AddParameters(localCmd, parameters);
                    object value = localCmd.ExecuteScalar();
                    return value == null || value == DBNull.Value ? "" : value.ToString();
                }
            }
            catch
            {
                return "";
            }
            finally
            {
                cn.Close();
            }
        }

        public int ExecuteNonQuery(string sql, Dictionary<string, object> parameters)
        {
            cn = new SQLiteConnection(@"Data Source=" + dbname + "");
            try
            {
                cn.Open();
                using (SQLiteCommand localCmd = new SQLiteCommand(sql, cn))
                {
                    AddParameters(localCmd, parameters);
                    return localCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                cn.Close();
            }
        }

        private static void AddParameters(SQLiteCommand command, Dictionary<string, object> parameters)
        {
            if (parameters == null) return;
            foreach (var kv in parameters)
            {
                command.Parameters.AddWithValue(kv.Key, kv.Value ?? DBNull.Value);
            }
        }
        public void DataCombo(ComboBox DDL, string TABLE_NAME, string Col_Name, string Col_Value, string WHR)
        {
            try
            {
                cn = new SQLiteConnection(@"Data Source=" + dbname + "");
                string SQLSTR = "select " + Col_Name + " , " + Col_Value + " from " + TABLE_NAME + " " + WHR;
                SQLiteDataAdapter sda = new SQLiteDataAdapter(SQLSTR, cn);
                DataSet ds2 = new DataSet();
                sda.Fill(ds2);
                DDL.DataSource = ds2.Tables[0];
                DDL.DisplayMember = Col_Name;
                DDL.ValueMember = Col_Value;


                DDL.SelectedValue = Col_Name;
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }

        }
        public void DataComboWithNull(ComboBox DDL, string TABLE_NAME, string Col_Name, string Col_Value, string WHR)
        {
            try
            {
                cn = new SQLiteConnection(@"Data Source=" + dbname + "");
                string SQLSTR = "select " + Col_Name + " , " + Col_Value + " from " + TABLE_NAME + " " + WHR;
                SQLiteDataAdapter sda = new SQLiteDataAdapter(SQLSTR, cn);
                DataSet ds2 = new DataSet();
                sda.Fill(ds2);
                // DDL.Items.Clear();
                DDL.DataSource = ds2.Tables[0];
                DDL.DisplayMember = Col_Name;
                DDL.ValueMember = Col_Value;
                DDL.SelectedIndex = -1;
                //DDL.Items.Add("");
                //DDL.Items[0] = "";
                //for (int i = 1; i <= ds2.Tables[0].Rows.Count; i++)
                //{
                //    DDL.Items.Add(ds2.Tables[0].Rows[i - 1][Col_Name].ToString());
                //    DDL.Items[i] = ds2.Tables[0].Rows[i - 1][Col_Name].ToString();
                //    DDL.DisplayMember = ds2.Tables[0].Rows[i - 1][Col_Name].ToString();
                //    DDL.ValueMember = ds2.Tables[0].Rows[i - 1][Col_Name].ToString();
                //}
            }
            catch
            {
            }
        }
        public void Dsbl_Enbl_Cntrl(System.Windows.Forms.Control parent, bool Opr_Type)
        {
            foreach (Control C in parent.Controls)
                if (C is TextBox)
                    ((TextBox)C).ReadOnly = !Opr_Type;
                else if (C is ComboBox)
                {
                    ComboBox CB = (ComboBox)C;
                    CB.Enabled = Opr_Type;
                    if (Opr_Type)
                        CB.DropDownStyle = ComboBoxStyle.DropDownList;
                    else
                        CB.DropDownStyle = ComboBoxStyle.DropDown;
                }
                else if (C is GroupBox)
                    Dsbl_Enbl_Cntrl(C, Opr_Type);
                else if (!(C is Label))
                    C.Enabled = Opr_Type;
        }
        public void Dsbl_Enbl_But(System.Windows.Forms.Control parent, bool Opr_Type)
        {
            bool vAdd = Opr_Type;
            bool vEdt = Opr_Type;
            bool vDel = Opr_Type;

            //DataTable DSec = GetData("Select * From dbo.ScrScurty Where UsrID = " + vUser + " AND ScrID = " + vScrn);
            //if (DSec.Rows.Count > 0)
            //{
            //	vAdd = Convert.ToBoolean((DSec.Rows[0]["ButAdd"]));
            //	vEdt = Convert.ToBoolean((DSec.Rows[0]["ButEdt"]));
            //	vDel = Convert.ToBoolean((DSec.Rows[0]["ButDel"]));
            //}

            foreach (Control C in parent.Controls)
            {
                if (C is Button)
                {
                    switch (C.Name)
                    {
                        case "ButNew":
                            if (vAdd)
                                C.Enabled = Opr_Type;
                            else
                                C.Enabled = vAdd;
                            break;
                        case "ButEdit":
                        case "ButPrint":
                            if (vEdt)
                                C.Enabled = Opr_Type;
                            else
                                C.Enabled = vEdt;
                            break;
                        case "ButDel":
                            if (vDel)
                                C.Enabled = Opr_Type;
                            else
                                C.Enabled = vDel;
                            break;
                        case "ButExit":
                        case "DG_Data":
                        case "ButSch":
                            C.Enabled = Opr_Type;
                            break;
                        case "ButSave":
                        case "ButUndo":
                            C.Enabled = !Opr_Type;
                            break;
                    }
                }
            }
        }
        public void ClearControl(System.Windows.Forms.Control parent)
        {
            foreach (System.Windows.Forms.Control C in parent.Controls)
            {
                if (!(C is Label) && !(C is GroupBox))
                {
                    if (C is CheckBox)
                        ((CheckBox)C).Checked = false;
                    else if (C is ComboBox)
                    {
                        ComboBox Cmb = (ComboBox)C;
                        if (Cmb.Name != "CntryNo")
                            if (Cmb.Items.Count > 0) Cmb.SelectedIndex = 0;
                        //if (Cmb.Name != "OrderNo")
                        //{
                        //    if (Cmb.Name != "Doc_Type")
                        //    {
                        //        if (Cmb.Name != "A_Type")
                        //        {
                        //            if (Cmb.Name != "DDLGetType")
                        //            {
                        //                if (Cmb.Items.Count > 0) Cmb.SelectedIndex = 0;
                        //            }
                        //        }
                        //    }
                        //}
                    }
                    else if (C is PictureBox)
                    {
                        ((PictureBox)C).Image = null;
                    }
                    //else if (C is Tailor.TextAcc)
                    //	((Tailor.TextAcc)C).TxtAccNo.Text = "";
                    else
                        if (C.Name != "Id" || C.Name != "CustNo" || C.Name != "ItemCode" || C.Name != "DistNo" || C.Name != "CityNo" || C.Name != "CntryNo" || C.Name != "AdvID")
                    {
                        if (!(C is Button))
                            C.Text = "";
                    }
                }
            }
        }
        public int GetmaxNO(string feiled, string table)
        {
            int MaxNo = 0;
            int tempNo;
            String VAL = GetScalar("select max(" + feiled + ") from " + table + "");
            if (String.IsNullOrEmpty(VAL)) VAL = "0";
            tempNo = int.Parse(VAL);
            if (tempNo == 0)
                MaxNo = 1;
            else
                MaxNo = tempNo + 1;
            return MaxNo;

        }
        public int GetmaxNO(string feiled, string table, string whr)
        {
            int MaxNo = 0;
            int tempNo;
            String VAL = GetScalar("select max(" + feiled + ") from " + table + " " + whr + "");
            if (String.IsNullOrEmpty(VAL)) VAL = "0";
            tempNo = int.Parse(VAL);
            if (tempNo == 0)
                MaxNo = 1;
            else
                MaxNo = tempNo + 1;
            return MaxNo;

        }
        public bool ViewData(Control parent, string Sql, string tbl)
        {
            try
            {
                //string Sql = "SELECT * FROM " + Tbl_Name + " WHERE " + sWhr;
                //if (Tbl_Name == "Flages") Sql = "SELECT FlgNo,(SELECT MsgTxt FROM dbo.Messages WHERE MsgID = F.MsgID AND ScrID = 99 AND LngID = " + vLang + ") FlgName FROM Flages F WHERE " + sWhr;

                DataTable VData = GetData(Sql);
                foreach (Control C in parent.Controls)
                {
                    if (!(C is Label))
                    {
                        if (C is ComboBox)
                        {
                            ComboBox CB = (ComboBox)C;
                            if (!String.IsNullOrEmpty(VData.Rows[0][C.Name].ToString()))
                                CB.SelectedValue = VData.Rows[0][C.Name].ToString();


                        }
                        else if (C is CheckBox)
                        {
                            ((CheckBox)C).Checked = Convert.ToBoolean(VData.Rows[0][C.Name].ToString());
                        }

                        else if (C is PictureBox)
                        {
                            C.Text = Application.StartupPath.ToString() + VData.Rows[0][C.Name].ToString();
                        }
                        else
                        {
                            if (C.Name != "txtAmt_Write")
                            {
                                C.Text = VData.Rows[0][C.Name].ToString();
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SaveRec(Control parent, string Tbl_Name)
        {

            cn = new SQLiteConnection(@"Data Source=" + dbname + "");
            SQLiteDataAdapter DAdptr = new SQLiteDataAdapter("SELECT * FROM " + Tbl_Name, cn);
            DataSet DSet = new DataSet();
            try
            {
                cn.Open();
                DAdptr.Fill(DSet, Tbl_Name);
                DataTable DTbl = DSet.Tables[Tbl_Name];
                DataRow DRow = DTbl.NewRow();
                foreach (Control C in parent.Controls)
                {
                    if (!(C is Label))
                    {
                        if (C is ComboBox)
                        {
                            ComboBox CB = ((ComboBox)(C));
                            if (CB.SelectedValue != null && CB.SelectedValue.ToString() != "")
                            {
                                try { DRow[C.Name] = CB.SelectedValue.ToString(); }
                                catch { DRow[C.Name] = Convert.ToBoolean(CB.SelectedValue); }
                            }
                        }
                        else if (C is CheckBox)
                        {
                            DRow[C.Name] = ((CheckBox)C).Checked;
                        }
                        else if (C is GroupBox)
                        {
                            if (C.Tag.ToString().Trim() == "Rido")
                            {
                                foreach (Control RCtr in C.Controls)
                                {
                                    if (RCtr is RadioButton)
                                    {
                                        RadioButton RB = ((RadioButton)(RCtr));
                                        if (RB.Checked)
                                        {
                                            DRow[C.Name] = RB.Tag.ToString();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        //else if (C is DateTimePicker)
                        //{
                        //   DateTimePicker DT = (DateTimePicker)C;
                        //   if (DT.Format == DateTimePickerFormat.Time)
                        //      DRow[C.Name] = DT.Text; // Convert.ToDateTime(DT.Text).ToShortTimeString();
                        //   else
                        //      DRow[C.Name] = C.Text.Trim();
                        //}
                        else
                        {
                            //if (C.Name != "UsrPWDR")

                            if (C.Text == "") // && DSet.Tables[0].Rows[0][C.Name].GetType().ToString() == "System.Int32")
                            {

                                try { DRow[C.Name] = ""; }
                                catch { DRow[C.Name] = "0"; }
                            }
                            else
                                DRow[C.Name] = C.Text.Trim();
                            //}
                        }
                    }
                }
                DTbl.Rows.Add(DRow);
                SQLiteCommandBuilder CmndBldr = new SQLiteCommandBuilder(DAdptr);
                DAdptr.Update(DSet, Tbl_Name);

                cn.Close();
                return true;
            }
            catch (Exception ex)
            {
                cn.Close();
                MsgShow("حصل خطأ اثناء الحفظ" + "\n" + ex.Message, parent.Parent.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool EditRec(Control parent, string Sql, string Tbl)
        {
            cn = new SQLiteConnection(@"Data Source=" + dbname + "");
            SQLiteDataAdapter DAdptr = new SQLiteDataAdapter(Sql, cn);
            SQLiteCommandBuilder CmndBldr = new SQLiteCommandBuilder(DAdptr);
            DataSet DSet = new DataSet();
            try
            {
                DAdptr.Fill(DSet, Tbl);
                foreach (Control C in parent.Controls)
                {
                    if (!(C is Label))
                    {
                        if (C is ComboBox)
                        {
                            ComboBox CB = ((ComboBox)(C));
                            if (CB.SelectedValue != null && CB.SelectedValue.ToString() != "")
                            {
                                if (DSet.Tables[0].Rows[0][C.Name].GetType().ToString() == "System.Boolean")
                                    DSet.Tables[0].Rows[0][C.Name] = Convert.ToBoolean(CB.SelectedValue);
                                else
                                    DSet.Tables[0].Rows[0][C.Name] = CB.SelectedValue.ToString();
                            }
                        }
                        else if (C is CheckBox)
                        {
                            DSet.Tables[0].Rows[0][C.Name] = ((CheckBox)C).Checked;
                        }
                        else if (C is GroupBox)
                        {
                            if (C.Tag.ToString().Trim() == "Rido")
                            {
                                foreach (Control RCtr in C.Controls)
                                {
                                    if (RCtr is RadioButton)
                                    {
                                        RadioButton RB = ((RadioButton)(RCtr));
                                        if (RB.Checked)
                                        {
                                            DSet.Tables[0].Rows[0][C.Name] = RB.Tag.ToString();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (C.Name != "TxtAge")
                                DSet.Tables[0].Rows[0][C.Name] = C.Text.Trim();
                        }
                    }
                }
                DAdptr.Update(DSet, Tbl);
                return true;
            }
            catch (Exception ex)
            {
                MsgShow("مشكله في التعديل", parent.Parent.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool DelRec(string Sql)
        {
            cn = new SQLiteConnection(@"Data Source=" + dbname + "");
            MessageBoxOptions MsgOpt = MessageBoxOptions.DefaultDesktopOnly;
            if (vRtL) MsgOpt = MessageBoxOptions.RtlReading;
            if (MessageBox.Show("هل تريد  حذف السجل الحالي", "حذف مستخدم", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MsgOpt) == DialogResult.Yes)
            {
                string Str = DoSQL(Sql);
                if (Str.Trim() == "")
                    return true;
                else
                {
                    string MsgErr = "لا يمكن الحذف" + "\n" + Str;
                    if (Str.Contains("FK_")) MsgErr = "لا يمكن الحذف. يوجد بيانات مرتبطة";
                    MsgShow(MsgErr, "حصل خطأ اثناء الحذف" + " - ButDel_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
                return false;
        }

        public void MsgShow(string MsgTxt, string MsgTit, MessageBoxButtons MsgBut, MessageBoxIcon MsgIcon) //, MessageBoxDefaultButton MsgDuf)
        {
            //if (MsgIcon == null) MsgIcon = MessageBoxIcon.None;
            //if (MsgBut == null) MsgBut = MessageBoxButtons.OK;
            //if (MsgDuf == null) MsgDuf = MessageBoxDefaultButton.Button1;

            MessageBoxOptions MsgOpt = MessageBoxOptions.DefaultDesktopOnly;
            if (vRtL) MsgOpt = MessageBoxOptions.RtlReading;
            MessageBox.Show(MsgTxt, MsgTit, MsgBut, MsgIcon, MessageBoxDefaultButton.Button1, MsgOpt);
        }
        public string DoSQL(String sql)
        {
            string str = "";
            cn = new SQLiteConnection(@"Data Source=" + dbname + "");

            cmd = new SQLiteCommand(sql, cn);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                return "";

            }
            catch (SQLiteException ex)
            {
                str = ex.Message.ToString();
                retdep = ex.Message.ToString();
                alterdep = 1;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return retdep;
        }
        internal bool ChkField(System.Windows.Forms.Control C, string sMsg)
        {
            if (C.Text == "" || C.Text == "0" || C.Text == "   /   /")
            {
                MessageBox.Show(sMsg, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                C.Focus();
                return true;
            }
            else
                return false;
        }
    }
}
