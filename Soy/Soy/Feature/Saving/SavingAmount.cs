using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace Soy
{
    public partial class SavingAmount : UserControl
    {
        public SavingAmount()
        {
            InitializeComponent();
        }
        //定義
        public static int FirstLoadStatus = ConInstance.savingAmountFirstLoad;
        public static int execCode = 0;
        public static string ID = "0";
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Panel pa3 = new Panel();
        public static Panel pa4 = new Panel();
        public static Panel pa5 = new Panel();
        public static TextBox tb1 = new TextBox();
        public static TextBox tb2 = new TextBox();
        public static TextBox tb3 = new TextBox();
        public static TextBox tb4 = new TextBox();
        public static TextBox tb5 = new TextBox();
        public static Button btn1 = new Button();
        public static ComboBox cb1 = new ComboBox();
        public static DataGridView dg = new DataGridView();
        class LocalSetup
        {
            private static void Common(UserControl uc)
            {
                FunCom.AddPanel(pa2, 0, uc, new int[] { 0, 0 });
                FunCom.AddPanel(pa1, 2, uc, new int[] { 350, 0 });
                pa2.BackColor = Color.Coral;
                pa2.Padding = new Padding(10, 10, 10, 10);
            }
            private static void LeftSide()
            {
                string[] lbs = new string[] { "日付", "口座", "貯金額" };
                for (int i = 0; i < lbs.Length; i++)
                {
                    Label lb = new Label();
                    FunCom.AddLabel(lb, 5, pa1);
                    lb.Text = lbs[i];
                    lb.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                    lb.Location = new Point(15, 15 + i * 70);
                }
                FunCom.AddTextbox(tb1, 5, 5, pa1, new int[] { 180, 10 });
                tb1.Location = new Point(100, 15);
                tb1.Text = FunDate.getToday(0, 0);
                FunCom.AddCombobox(cb1, 5, 4, pa1, new int[] { 180, 10 });
                cb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb1.Location = new Point(100, 85);
                FunCom.AddTextbox(tb2, 5, 6, pa1, new int[] { 180, 10 });
                tb2.Location = new Point(100, 155);
                tb2.Text = FunDate.getToday(0, 0);
                FunCom.AddButton(btn1, 5, 7, pa1, new int[] { 90, 50 });
                btn1.Location = new Point(100, 225);
                btn1.BackColor = Color.Coral;
                btn1.Click += (sender, e) =>
                {
                    if (cb1.Text == "" || tb1.Text == "" || tb2.Text == "")
                    {
                        FunMSG.ErrMsg(ConMSG.CheckMSG.message00001);
                    }
                    else if (!Regex.IsMatch(tb1.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$"))
                    {
                        FunMSG.ErrMsg(ConMSG.CheckMSG.message00003);
                    }
                    else
                    {
                        if (execCode == 0)
                        {
                            FunSQL.SQLDML("SQL0110", ConSQL.SavingSQL.SQL0110, new string[] { "@SAVING_DATE", "@ACCOUNT_ID", "@SAVING_AMOUNT" }, new string[] { tb1.Text, cb1.SelectedValue.ToString(), tb2.Text });
                        }
                        else if (execCode == 1)
                        {
                            FunSQL.SQLDML("SQL0120", ConSQL.SavingSQL.SQL0120, new string[] { "@SAVING_DATE", "@ACCOUNT_ID", "@SAVING_AMOUNT", "@SAVING_ID" }, new string[] { tb1.Text, cb1.SelectedValue.ToString(), tb2.Text, ID });
                        }
                        LocalCleaning.LocalMain();
                        LocalLoad.LocalMain();
                    }
                };
            }
            public static void RightSide()
            {
                FunCom.AddDataGridView(dg, 0, pa2, new int[] { 0, 0 });
                dg.BackgroundColor = Color.Coral;
                FunCom.AddDataGridViewColumns(dg, new string[] { "ID", "月", "口座", "貯金実績" });
                dg.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                FunCom.AddContextMenuStrip(dg, ConCom.defaultBtnNames, new EventHandler[]
                {
                    (sender, e) =>
                    {//新規
                        ID = "0";
                        execCode = 0;
                        btn1.Text = ConCom.defaultBtnNames[execCode];
                    },
                    (sender, e) =>
                    {//更新
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.CheckMSG.message00007); return; }
                        ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                        SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0101", ConSQL.SavingSQL.SQL0101, new string[] { "@SAVING_ID" }, new string[] { ID });
                        while (reader.Read())
                        {
                            tb1.Text = ((DateTime)reader["SAVING_DATE"]).ToString("yyyy-MM-01");
                            cb1.Text = (string)reader["ACCOUNT_NAME"];
                            tb2.Text = ((Int64)reader["SAVING_AMOUNT"]).ToString();
                            }
                        execCode = 1;
                        btn1.Text = ConCom.defaultBtnNames[execCode];
                    },
                    (sender, e) =>
                    {//削除
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.CheckMSG.message00007); return; }
                        ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQL0130", ConSQL.SavingSQL.SQL0130, new string[] { "@SAVING_ID" }, new string[] { ID });
                        LocalCleaning.LocalMain();
                        LocalLoad.LocalMain();
                    }
                }
                );
                FunCom.AddPanel(pa3, 1, pa2, new int[] { 0, 50 });
                Label lb2 = new Label();
                FunCom.AddLabel(lb2, 5, pa3);
                lb2.Text = "口座";
                lb2.Location = new Point(0, 0);
                FunCom.AddTextbox(tb5, 5, 1, pa3, new int[] { 180, 10 });
                tb5.Location = new Point(72, 0);
                tb5.TextChanged += (sender, e) => { LocalLoad.DataLocalLoad(); };
                Label lb1 = new Label();
                FunCom.AddLabel(lb1, 5, pa3);
                lb1.Text = "期間";
                lb1.Location = new Point(272, 0);
                FunCom.AddTextbox(tb3, 5, 1, pa3, new int[] { 180, 10 });
                tb3.Location = new Point(324, 0);
                tb3.Text = FunDate.getToday(3, int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Saving]", "from"))));
                tb3.TextChanged += (sender, e) => { if (Regex.IsMatch(tb3.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])$")) { LocalLoad.DataLocalLoad(); } };
                FunCom.AddTextbox(tb4, 5, 1, pa3, new int[] { 180, 10 });
                tb4.Location = new Point(514, 0);
                tb4.Text = FunDate.getToday(3, int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Saving]", "to"))));
                tb4.TextChanged += (sender, e) => { if (Regex.IsMatch(tb4.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])$")) { LocalLoad.DataLocalLoad(); } };
            }
            public static void LocalMain(UserControl uc)
            {
                Common(uc);
                LeftSide();
                RightSide();
            }
        }
        //class LocalStartup
        //{
        //    public static void LocalMain()
        //    {

        //    }
        //}
        class LocalCleaning
        {
            public static void LocalMain()
            {
                tb1.Text = FunDate.getToday(0, 0);
                tb2.Text = "";
                ID = "0";
                execCode = 0;
                btn1.Text = ConCom.defaultBtnNames[execCode];
            }
        }
        class LocalLoad
        {
            public static void DataLocalLoad()
            {
                StringBuilder sb = new StringBuilder("%");
                sb.Append(tb5.Text);
                sb.Append("%");
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0100", ConSQL.SavingSQL.SQL0100, new string[] { "@MONTH1", "@MONTH2","@KEYWORD" }, new string[] { tb3.Text, tb4.Text,sb.ToString() }); 
                dg.Rows.Clear();
                while (reader.Read())
                {
                    dg.Rows.Add(
                        ((Int64)reader["SAVING_ID"]).ToString(),
                        ((DateTime)reader["SAVING_DATE"]).ToString("yyyy-MM"),
                        (string)reader["ACCOUNT_NAME"],
                        string.Format("{0:#,#}\r\n", (Int64)reader["SAVING_AMOUNT"])
                        );
                }
            }
            private static void CmdLocalLoad1()
            {
                long[] keys = new long[] { };
                string[] values = new string[] { };
                FunCom.AddComboboxItem(cb1, keys, values);
                keys = new long[] { };
                values = new string[] { };
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0000", ConSQL.AccountSQL.SQL0000, new string[] { }, new string[] { });
                while (reader.Read())
                {
                    Array.Resize(ref keys, keys.Length + 1);
                    keys[keys.Length - 1] = (Int64)reader["ACCOUNT_ID"];
                    Array.Resize(ref values, values.Length + 1);
                    values[values.Length - 1] = (string)reader["ACCOUNT_NAME"];
                }
                FunCom.AddComboboxItem(cb1, keys, values);
            }
            public static void CmdLocalLoad()
            {
                CmdLocalLoad1();
            }
            public static void LocalMain()
            {
                DataLocalLoad();
                CmdLocalLoad();
            }
        }
        private void SavingAmount_VisibleChanged(object sender, EventArgs e)
        {
            if (ConInstance.savingAmount.Visible == true)
            {
                if (ConInstance.savingAmountFirstLoad < 2)
                {
                    ConInstance.savingAmountFirstLoad += 1;
                }
                int LoadStatus = ConInstance.savingAmountFirstLoad;
                if (LoadStatus == 1)
                {
                    LocalSetup.LocalMain(this);
                    //LocalStartup.LocalMain();
                    LocalCleaning.LocalMain();
                    LocalLoad.LocalMain();
                }
                else if (LoadStatus == 2)
                {
                    LocalCleaning.LocalMain();
                    LocalLoad.LocalMain();
                }
            }
            else if (ConInstance.savingAmount.Visible == false && ConInstance.savingAmountFirstLoad == 1 && FirstLoadStatus == 1)
            {
                ConInstance.savingAmountFirstLoad += 1;
                LocalSetup.LocalMain(this);
                //LocalStartup.LocalMain();
                LocalCleaning.LocalMain();
                LocalLoad.LocalMain();
            }
        }
    }
}
