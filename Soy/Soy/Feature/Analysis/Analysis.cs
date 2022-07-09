using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace Soy
{
    public partial class Analysis : UserControl
    {
        public Analysis()
        {
            InitializeComponent();
        }
        //定義
        public static int FirstLoadStatus = ConInstance.analysisFirstLoad;
        public static Panel p1 = new Panel();
        public static ComboBox cb1 = new ComboBox();
        public static ComboBox cb2 = new ComboBox();
        public static ComboBox cb3 = new ComboBox();
        public static TextBox tb1 = new TextBox();
        public static TextBox tb2 = new TextBox();
        public static Chart ch = new Chart();
        public static int mode = 0;
        class LocalSetup
        {
            public static void LocalMain(UserControl uc)
            {
                string[] labelName = { "chart", "data1", "data2", "期間" };
                for (int i = 0; i < labelName.Length; i++)
                {
                    Label lb = new Label();
                    lb.Text = labelName[i];
                    lb.Location = new Point(250 * i, 10);
                    lb.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                    FunCom.AddLabel(lb, 5, p1);
                }
                FunCom.AddComboboxItem(cb1, new long[] { 0, 1 }, new string[] { "棒", "折れ線" });
                FunCom.AddComboboxItem(cb2, new long[] { 0, 1, 2 }, new string[] { "純資産額", "貯金額", "債務額" });
                FunCom.AddCombobox(cb1, 5, 1, p1, new int[] { 180, 10 });
                FunCom.AddCombobox(cb2, 5, 2, p1, new int[] { 180, 10 });
                FunCom.AddCombobox(cb3, 5, 3, p1, new int[] { 180, 10 });
                cb1.Location = new Point(57, 10);
                cb2.Location = new Point(311, 10);
                cb3.Location = new Point(561, 10);
                cb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb2.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb3.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb1.SelectedValueChanged += (e, sender) =>
                {
                    mode = 1;
                };
                cb2.SelectedValueChanged += (e, sender) =>
                {
                    LocalLoad.LocalMainCombo();
                    mode = 1;
                };
                cb3.SelectedValueChanged += (e, sender) =>
                {
                    mode = 1;
                };
                FunCom.AddTextbox(tb1, 5, 3, p1, new int[] { 180, 10 });
                FunCom.AddTextbox(tb2, 5, 4, p1, new int[] { 180, 10 });
                tb1.Location = new Point(802, 10);
                tb1.Text = FunDate.getToday(3, int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Analysis]", "from"))));
                tb2.Location = new Point(1002, 10);
                tb2.Text = FunDate.getToday(3, int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Analysis]", "to"))));
                tb1.TextChanged += (sender, e) =>
                {
                    mode = 1;
                };
                tb2.TextChanged += (sender, e) =>
                {
                    mode = 1;
                };
                ch.ChartAreas.Add(new ChartArea("Area"));
                ch.Click += (sender, e) =>
                {
                    LocalLoad.LocalMainDataLoad();
                };
                FunCom.AddChart(ch, 0, uc, new int[] { 0, 0 });
                FunCom.AddPanel(p1, 1, uc, new int[] { 0, 100 });
                cb1.SelectedValue = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Analysis]", "selected1")));
                cb2.SelectedValue = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Analysis]", "selected2")));
                LocalLoad.LocalMainCombo();
                mode = 1;
            }
        }
        class LocalStartup { }
        class LocalCleaning { }
        class LocalLoad
        {
            public static void LocalMainCombo()
            {
                long[] keys = new long[] { 0 };
                string[] values = new string[] { "ALL" };
                int selected = int.Parse(string.Format("{0}", cb2.SelectedValue.ToString()));
                if (selected == 0)
                {
                    keys = new long[] { 0 };
                    values = new string[] { "純資産額" };
                }
                else if (selected == 1)
                {
                    SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0000", ConSQL.AccountSQL.SQL0000, new string[] { }, new string[] { });
                    while (reader.Read())
                    {
                        Array.Resize(ref keys, keys.Length + 1);
                        keys[keys.Length - 1] = (Int64)reader["ACCOUNT_ID"];
                        Array.Resize(ref values, values.Length + 1);
                        values[values.Length - 1] = (string)reader["ACCOUNT_NAME"];
                    }
                }
                else if (selected == 2)
                {
                    SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0200", ConSQL.CardSQL.SQL0200, new string[] { }, new string[] { });
                    while (reader.Read())
                    {
                        Array.Resize(ref keys, keys.Length + 1);
                        keys[keys.Length - 1] = (Int64)reader["CARD_ID"];
                        Array.Resize(ref values, values.Length + 1);
                        values[values.Length - 1] = (string)reader["CARD_NAME"];
                    }
                }
                FunCom.AddComboboxItem(cb3, keys, values);
            }
            public static void LocalMainDataLoad()
            {
                int selected1 = int.Parse(string.Format("{0}", cb1.SelectedValue.ToString()));
                int selected2 = int.Parse(string.Format("{0}", cb2.SelectedValue.ToString()));
                int selected3 = int.Parse(string.Format("{0}", cb3.SelectedValue.ToString()));
                if (!Regex.IsMatch(tb1.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])$") || !Regex.IsMatch(tb2.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])$"))
                {
                    FunMSG.ErrMsg(ConMSG.CheckMSG.message00003);
                    return;
                }
                else if (mode == 0) { return; }
                ch.Series.Clear();
                ch.Legends.Clear();
                string[] legends = new string[] { };
                string[] x_label = new string[] { };
                SQLiteDataReader reader = null;
                if (selected2 == 0)
                {
                    /*純資産額 */
                    if (selected3 == 0)
                    {
                        legends = new string[] { cb3.Text };
                    }
                }
                else if (selected2 == 1)
                {
                    /* 貯金額 */
                    if (selected3 == 0)
                    {
                        reader = FunSQL.SQLSELECT("SQL0000", ConSQL.AccountSQL.SQL0000, new string[] { }, new string[] { });
                        while (reader.Read())
                        {
                            Array.Resize(ref legends, legends.Length + 1);
                            legends[legends.Length - 1] = (string)reader["ACCOUNT_NAME"];
                        }
                    }
                    else
                    {
                        legends = new string[] { cb3.Text };
                    }
                }
                else if (selected2 == 2)
                {
                    /* 債務額 */
                    if (selected3 == 0)
                    {
                        reader = FunSQL.SQLSELECT("SQL0200", ConSQL.CardSQL.SQL0200, new string[] { }, new string[] { });
                        while (reader.Read())
                        {
                            Array.Resize(ref legends, legends.Length + 1);
                            legends[legends.Length - 1] = (string)reader["CARD_NAME"];
                        }
                    }
                    else
                    {
                        legends = new string[] { cb3.Text };
                    }
                }
                reader = FunSQL.SQLSELECT("SQL0504", ConSQL.AnalysisSQL.SQL0504, new string[] { "@MONTH1", "@MONTH2" }, new string[] { tb1.Text, tb2.Text });
                while (reader.Read())
                {
                    Array.Resize(ref x_label, x_label.Length + 1);
                    x_label[x_label.Length - 1] = ((DateTime)reader["DATECOLUMNS"]).ToString("yyyy-MM");
                }
                if (legends.Length > 0) { ch.Legends.Add(legends[0]); };
                for (int i = 0; i < legends.Length; i++)
                {
                    ch.Series.Add(legends[i]);
                    long[] y_label = new long[] { };
                    if (selected2 == 0 && selected3 == 0)
                    {
                        reader = FunSQL.SQLSELECT("SQL0500", ConSQL.AnalysisSQL.SQL0500, new string[] { "@MONTH1", "@MONTH2" }, new string[] { tb1.Text, tb2.Text });
                    }
                    else if (selected2 == 1)
                    {
                        reader = FunSQL.SQLSELECT("SQLSQL0501", ConSQL.AnalysisSQL.SQL0501, new string[] { "@ACCOUNT_NAME","@MONTH1", "@MONTH2" }, new string[] { legends[i],tb1.Text, tb2.Text });
                    }
                    else if (selected2 == 2)
                    {
                        reader = FunSQL.SQLSELECT("SQLSQL0502", ConSQL.AnalysisSQL.SQL0502, new string[] { "@CARD_NAME", "@MONTH1", "@MONTH2" }, new string[] { legends[i], tb1.Text, tb2.Text });
                    }
                    while (reader.Read())
                    {
                        Array.Resize(ref y_label, y_label.Length + 1);
                        if (selected2 == 0)
                        {
                            y_label[y_label.Length - 1] = (Int64)reader["ASSET_AMOUNT"];
                        }
                        else if (selected2 == 1)
                        {
                            y_label[y_label.Length - 1] = (Int64)reader["SAVING_AMOUNT"];
                        }
                        else if (selected2 == 2)
                        {
                            y_label[y_label.Length - 1] = (Int64)reader["BILLING_AMOUNT"];
                        }
                    }
                    if (selected1 == 0)
                    {
                        ch.Series[legends[i]].ChartType = SeriesChartType.StackedColumn;
                    }
                    else if (selected1 == 1)
                    {
                        ch.Series[legends[i]].ChartType = SeriesChartType.Line;
                        ch.Series[legends[i]].MarkerStyle = MarkerStyle.Circle;
                    }
                    for (int j = 0; j < y_label.Length; j++)
                    {
                        ch.Series[legends[i]].Points.AddXY(x_label[j], y_label[j]);
                    }
                }
                mode = 0;
            }
        }
        private void Analysis_VisibleChanged(object sender, EventArgs e)
        {
            if (ConInstance.analysis.Visible == true)
            {
                if (ConInstance.analysisFirstLoad < 2)
                {
                    ConInstance.analysisFirstLoad += 1;
                }
                int LoadStatus = ConInstance.analysisFirstLoad;
                if (LoadStatus == 1)
                {
                    LocalSetup.LocalMain(this);
                    LocalLoad.LocalMainDataLoad();
                }
                else if (LoadStatus == 2)
                {
                    mode = 1;
                    LocalLoad.LocalMainDataLoad();
                }
            }
            else if (ConInstance.analysis.Visible == false && ConInstance.analysisFirstLoad == 1 && FirstLoadStatus == 1)
            {
                ConInstance.analysisFirstLoad += 1;
                LocalSetup.LocalMain(this);
                    LocalLoad.LocalMainDataLoad();
            }
        }
    }
}