using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;


namespace Soy
{
    class FunCom
    {
        private static void jgDock(Control ctl, int code0)
        {
            if (code0 == 0)
            {
                ctl.Dock = DockStyle.Fill;
            }
            else if (code0 == 1)
            {
                ctl.Dock = DockStyle.Top;
            }
            else if (code0 == 2)
            {
                ctl.Dock = DockStyle.Left;
            }
            else if (code0 == 3)
            {
                ctl.Dock = DockStyle.Right;
            }
            else if (code0 == 4)
            {
                ctl.Dock = DockStyle.Bottom;
            }
            else if (code0 == 5)
            {
                ctl.Dock = DockStyle.None;
            }
        }
        public static void AddPanel(Panel pl, int code0, Control ctl, int[] size)
        {
            jgDock(pl, code0);
            pl.Margin = Padding.Empty;
            pl.Padding = Padding.Empty;
            ctl.Controls.Add(pl);
            pl.TabIndex = 200;
            pl.Width = size[0];
            pl.Height = size[1];
        }
        public static void AddChart(Chart ch, int code0, Control ctl, int[] size)
        {
            jgDock(ch, code0);
            ctl.Controls.Add(ch);
            ch.TabIndex = 200;
            ch.Width = size[0];
            ch.Height = size[1];
        }
        public static void AddButton(Button b, int code0, int code1, Control ctl, int[] size)
        {
            jgDock(b, code0);
            ctl.Controls.Add(b);
            b.TabIndex = code1;
            b.Width = size[0];
            b.Height = size[1];
            b.Font = new Font("Yu mincho", 10, FontStyle.Regular);
        }
        public static void AddTextbox(TextBox tb, int code0, int code1, Control ctl, int[] size)
        {
            jgDock(tb, code0);
            ctl.Controls.Add(tb);
            tb.TabIndex = code1;
            tb.Font = new Font("Yu mincho", 10, FontStyle.Regular);
            tb.Width = size[0];
            tb.Height = size[1];
        }
        public static void AddLabel(Label lbl, int code0, Control ctl)
        {
            jgDock(lbl, code0);
            ctl.Controls.Add(lbl);
            lbl.Font = new Font("Yu mincho", 10, FontStyle.Regular);
            lbl.AutoSize = true;
        }
        public static void AddUserControl(UserControl uc, int code0, Control ctl)
        {
            jgDock(uc, code0);
            ctl.Controls.Add(uc);
        }
        public static void AddCombobox(ComboBox cmb, int code0, int code1, Control ctl, int[] xy)
        {
            jgDock(cmb, code0);
            ctl.Controls.Add(cmb);
            cmb.TabIndex = code1;
            cmb.Font = new Font("Yu mincho", 15, FontStyle.Regular);
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb.Width = xy[0];
            cmb.Height = xy[1];
        }
        public static void AddComboboxItem(ComboBox cmb, long[] keys, string[] values)
        {
            //comboBox.Items.Clear();
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("ValueMember");
            dt.Columns.Add("DisplayMember");
            for (int i = 0; i < values.Length; i++)
            {
                dr = dt.NewRow();
                dr["ValueMember"] = (keys[i]).ToString();
                dr["DisplayMember"] = values[i];
                dt.Rows.Add(dr);
            }
            cmb.DataSource = dt;
            cmb.ValueMember = "ValueMember";
            cmb.DisplayMember = "DisplayMember";
            cmb.AutoSize = true;
        }
        public static void AddDataGridView(DataGridView dg, int code0, Control ctl, int[] xy)
        {
            jgDock(dg, code0);
            ctl.Controls.Add(dg);
            dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dg.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg.ColumnHeadersHeight = 30;
            dg.Font = new Font("Yu mincho", 9, FontStyle.Regular);
            dg.BorderStyle = BorderStyle.None;
            dg.RowHeadersVisible = false;
        }
        public static void AddDataGridViewColumns(DataGridView dg, string[] columnNames)
        {
            dg.Columns.Clear();
            dg.AllowUserToAddRows = false;
            for (int i = 0; i < columnNames.Length; i++)
            {
                dg.Columns.Add(i.ToString(), columnNames[i]);
            }
            dg.Columns[0].Visible = false;
            foreach (DataGridViewColumn c in dg.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
            dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }
        public static void AddContextMenuStrip(Control control, string[] btnNames, EventHandler[] e)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            for (int i = 0; i < btnNames.Length; i++)
            {
                contextMenuStrip.Items.Add(btnNames[i], null, e[i]);
            }
            control.ContextMenuStrip = contextMenuStrip;
        }
        public static void neverClose(object sender, System.ComponentModel.CancelEventArgs e, bool trueFalse)
        {
            e.Cancel = trueFalse;
        }
    }
}
