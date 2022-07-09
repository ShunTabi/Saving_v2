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
    public partial class Asset : UserControl
    {
        public Asset()
        {
            InitializeComponent();
        }
        //定義
        public static int FirstLoadStatus = ConInstance.assetFirstLoad;
        public static string ID = "0";
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Panel pa3 = new Panel();
        public static TextBox tb1 = new TextBox();
        public static TextBox tb2 = new TextBox();
        public static DataGridView dg = new DataGridView();
        class LocalSetup
        {
            public static void Common(UserControl uc)
            {
                FunCom.AddPanel(pa3, 0, uc, new int[] { 0, 0 });
                FunCom.AddPanel(pa2, 1, uc, new int[] { 0, 70 });
                FunCom.AddPanel(pa1, 1, uc, new int[] { 0, 50 });
                pa2.BackColor = Color.Coral;
                pa2.Padding = new Padding(10, 10, 10, 0);
                pa3.BackColor = Color.Coral;
                pa3.Padding = new Padding(10, 0, 10, 10);
            }
            private static void Bottom()
            {
                Label lb1 = new Label();
                FunCom.AddLabel(lb1, 5, pa2);
                lb1.Text = "期間";
                lb1.Location = new Point(10, 15);
                FunCom.AddTextbox(tb1, 5, 1, pa2, new int[] { 180, 10 });
                tb1.Location = new Point(62, 15);
                tb1.Text = FunDate.getToday(3, int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Asset]", "from"))));
                tb1.TextChanged += (sender, e) => { if (Regex.IsMatch(tb1.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])$")) { LocalLoad.LocalMain(); } };
                FunCom.AddTextbox(tb2, 5, 1, pa2, new int[] { 180, 10 });
                tb2.Location = new Point(254,15);
                tb2.Text = FunDate.getToday(3, int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Asset]", "to"))));
                tb2.TextChanged += (sender, e) => { if (Regex.IsMatch(tb2.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])$")) { LocalLoad.LocalMain(); } };
                FunCom.AddDataGridView(dg, 0, pa3, new int[] { 0, 0 });
                FunCom.AddDataGridViewColumns(dg, new string[] { "Dummy", "月", "貯金実績", "債務実績", "純資産実績" });
                dg.BackgroundColor = Color.Coral;
                dg.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            public static void LocalMain(UserControl uc)
            {
                Common(uc);
                Bottom();
            }
        }
        class LocalStartup
        {
        }
        class LocalCleaning
        {
        }
        class LocalLoad
        {
            public static void LocalMain()
            {
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0400", ConSQL.AssetSQL.SQL0400, new string[] { "@MONTH1", "@MONTH2" }, new string[] { tb1.Text, tb2.Text });
                dg.Rows.Clear();
                while (reader.Read())
                {
                    dg.Rows.Add(
                        "Dummy",
                        ((DateTime)reader["ASSET_DATE"]).ToString("yyyy-MM"),
                        string.Format("{0:#,#}\r\n", (Int64)reader["ASSET_SUMSAVINGAMOUNT"]),
                        string.Format("{0:#,#}\r\n", (Int64)reader["ASSET_SUMBILLINGAMOUNT"]),
                        string.Format("{0:#,#}\r\n", (Int64)reader["ASSET_AMOUNT"])
                        );
                }
            }
        }
        private void Asset_VisibleChanged(object sender, EventArgs e)
        {
            if (ConInstance.asset.Visible == true)
            {
                if (ConInstance.assetFirstLoad < 2)
                {
                    ConInstance.assetFirstLoad += 1;
                }
                int LoadStatus = ConInstance.assetFirstLoad;
                if (LoadStatus == 1)
                {
                    LocalSetup.LocalMain(this);
                    LocalLoad.LocalMain();
                }
                else if (LoadStatus == 2)
                {
                    LocalLoad.LocalMain();
                }
            }
            else if (ConInstance.asset.Visible == false && ConInstance.assetFirstLoad == 1 && FirstLoadStatus == 1)
            {
                ConInstance.assetFirstLoad += 1;
                LocalSetup.LocalMain(this);
                LocalLoad.LocalMain();
            }
        }
    }
}
