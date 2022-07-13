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

namespace Soy
{
    public partial class Account : UserControl
    {
        public Account()
        {
            InitializeComponent();
        }
        //定義
        public static int FirstLoadStatus = ConInstance.accountFirstLoad;
        public static int execCode = 0;
        public static string ID = "0";
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Panel pa3 = new Panel();
        public static TextBox tb1 = new TextBox();
        public static TextBox tb2 = new TextBox();
        public static Button btn1 = new Button();
        public static DataGridView dg = new DataGridView();
        class LocalSetup
        {
            private static void Common(UserControl uc)
            {
                FunCom.AddPanel(pa2, 0, uc, new int[] { 0, 0 });
                FunCom.AddPanel(pa1, 2, uc, new int[] { 350, 0 });
                pa2.BackColor = Color.LightCoral;
                pa2.Padding = new Padding(10, 10, 10, 10);
            }
            private static void LeftSide()
            {
                Label lb1 = new Label();
                FunCom.AddLabel(lb1, 5, pa1);
                lb1.Text = "口座";
                lb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                lb1.Location = new Point(15, 15);
                FunCom.AddTextbox(tb1, 5, 2, pa1, new int[] { 180, 10 });
                tb1.Location = new Point(100, 15);
                FunCom.AddButton(btn1, 5, 3, pa1, new int[] { 90, 50 });
                btn1.Location = new Point(100, 85);
                btn1.BackColor = Color.LightCoral;
                btn1.Click += (sender, e) =>
                {
                    if (tb1.Text == "")
                    {
                        FunMSG.ErrMsg(ConMSG.CheckMSG.message00001);
                    }
                    else
                    {
                        if (execCode == 0)
                        {
                            FunSQL.SQLDML("SQL0010", ConSQL.AccountSQL.SQL0010, new string[] { "@ACCOUNT_NAME" }, new string[] { tb1.Text });
                        }
                        else if (execCode == 1)
                        {
                            FunSQL.SQLDML("SQL0020", ConSQL.AccountSQL.SQL0020, new string[] { "@ACCOUNT_NAME", "@ACCOUNT_ID" }, new string[] { tb1.Text, ID });
                        }
                        LocalCleaning.LocalMain();
                        LocalLoad.LocalMain();
                    }
                };
            }
            private static void RightSide()
            {
                FunCom.AddDataGridView(dg, 0, pa2, new int[] { 0, 0 });
                dg.BackgroundColor = Color.LightCoral;
                FunCom.AddDataGridViewColumns(dg, new string[] { "ID", "口座" });
                FunCom.AddContextMenuStrip(dg, ConCom.defaultBtnNames, new EventHandler[]
                {
                    (sender, e) =>
                    {//新規
                execCode = 0;
                btn1.Text = ConCom.defaultBtnNames[execCode];
                    },
                    (sender, e) =>
                    {//修正
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.CheckMSG.message00007); return; }
                        execCode = 1;
                        btn1.Text = ConCom.defaultBtnNames[execCode];
                        ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                        SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0001", ConSQL.AccountSQL.SQL0001, new string[] { "@ACCOUNT_ID" }, new string[] { ID });
                        while (reader.Read())
                        {
                            tb1.Text = (string)reader["ACCOUNT_NAME"];
                        }
                    },
                    (sender, e) =>
                    {//削除
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.CheckMSG.message00007); return; }
                        ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQL0030", ConSQL.AccountSQL.SQL0030, new string[] { "@ACCOUNT_ID" }, new string[] { ID });
                        LocalCleaning.LocalMain();
                        LocalLoad.LocalMain();
                    }
                });
                FunCom.AddPanel(pa3, 1, pa2, new int[] { 0, 50 });
                Label lb1 = new Label();
                FunCom.AddLabel(lb1, 5, pa3);
                lb1.Text = "口座";
                lb1.Location = new Point(0, 0);
                lb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                FunCom.AddTextbox(tb2, 5, 1, pa3, new int[] { 180, 10 });
                tb2.Location = new Point(72, 0);
                tb2.TextChanged += (sender, e) =>
                {
                    LocalLoad.DataLocalLoad();
                };
            }
            public static void LocalMain(UserControl uc)
            {
                Common(uc);
                LeftSide();
                RightSide();
            }
        }
        class LocalStartup
        {
            public static void LocalMain()
            {
            }
        }
        class LocalCleaning
        {
            public static void LocalMain()
            {
                tb1.Text = "";
                ID = "0";
                execCode = 0;
                btn1.Text = ConCom.defaultBtnNames[execCode];
            }
        }
        class LocalLoad
        {
            public static void DataLocalLoad()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("%");
                sb.Append(tb1.Text);
                sb.Append("%");
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0000", ConSQL.AccountSQL.SQL0000, new string[] { "@ACCOUNT_NAME" }, new string[] { sb.ToString() });
                dg.Rows.Clear();
                while (reader.Read())
                {
                    dg.Rows.Add(
                        ((Int64)reader["ACCOUNT_ID"]).ToString(),
                        (string)reader["ACCOUNT_NAME"]
                        );
                }
            }
            public static void LocalMain()
            {
                DataLocalLoad();
            }
        }
        private void Account_VisibleChanged(object sender, EventArgs e)
        {
            if (ConInstance.account.Visible == true)
            {
                if (ConInstance.accountFirstLoad < 2)
                {
                    ConInstance.accountFirstLoad += 1;
                }
                int LoadStatus = ConInstance.accountFirstLoad;
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
            else if (ConInstance.account.Visible == false && ConInstance.accountFirstLoad == 1 && FirstLoadStatus == 1)
            {
                ConInstance.accountFirstLoad += 1;
                LocalSetup.LocalMain(this);
                //LocalStartup.LocalMain();
                LocalCleaning.LocalMain();
                LocalLoad.LocalMain();
            }
        }
    }
}
