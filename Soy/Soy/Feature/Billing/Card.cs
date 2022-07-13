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
    public partial class Card : UserControl
    {
        public Card()
        {
            InitializeComponent();
        }
        //定義
        public static int FirstLoadStatus = ConInstance.cardFirstLoad;
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
                lb1.Text = "ｶｰﾄﾞ名";
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
                            FunSQL.SQLDML("SQL0210", ConSQL.CardSQL.SQL0210, new string[] { "@CARD_NAME" }, new string[] { tb1.Text });
                        }
                        else if (execCode == 1)
                        {
                            FunSQL.SQLDML("SQL0220", ConSQL.CardSQL.SQL0220, new string[] { "@CARD_NAME", "@CARD_ID" }, new string[] { tb1.Text, ID });
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
                FunCom.AddDataGridViewColumns(dg, new string[] { "ID", "カード名" });
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
                        SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0201", ConSQL.CardSQL.SQL0201, new string[] { "@CARD_ID" }, new string[] { ID });
                        while (reader.Read())
                        {
                            tb1.Text = (string)reader["Card_NAME"];
                        }
                    },
                    (sender, e) =>
                    {//削除
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.CheckMSG.message00007); return; }
                        ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQL0230", ConSQL.CardSQL.SQL0230, new string[] { "@CARD_ID" }, new string[] { ID });
                        LocalCleaning.LocalMain();
                        LocalLoad.LocalMain();
                    }
                });
                FunCom.AddPanel(pa3, 1, pa2, new int[] { 0, 50 });
                Label lb1 = new Label();
                FunCom.AddLabel(lb1, 5, pa3);
                lb1.Text = "カード";
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
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0200", ConSQL.CardSQL.SQL0200, new string[] { "@CARD_NAME" }, new string[] { sb.ToString() });
                dg.Rows.Clear();
                while (reader.Read())
                {
                    dg.Rows.Add(
                        ((Int64)reader["Card_ID"]).ToString(),
                        (string)reader["Card_NAME"]
                        );
                }
            }
            public static void LocalMain()
            {
                DataLocalLoad();
            }
        }
        private void Card_VisibleChanged(object sender, EventArgs e)
        {
            if (ConInstance.card.Visible == true)
            {
                if (ConInstance.cardFirstLoad < 2)
                {
                    ConInstance.cardFirstLoad += 1;
                }
                int LoadStatus = ConInstance.cardFirstLoad;
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
            else if (ConInstance.card.Visible == false && ConInstance.cardFirstLoad == 1 && FirstLoadStatus == 1)
            {
                ConInstance.cardFirstLoad += 1;
                LocalSetup.LocalMain(this);
                //LocalStartup.LocalMain();
                LocalCleaning.LocalMain();
                LocalLoad.LocalMain();
            }
        }
    }
}
