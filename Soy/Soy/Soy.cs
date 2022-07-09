using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Soy
{
    public partial class Soy : Form
    {
        public Soy()
        {
            InitializeComponent();
        }
        //定義
        public static bool trueFalse = true;
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Panel pa3 = new Panel();
        public static Panel pa4 = new Panel();
        public static Panel pa5 = new Panel();
        public static Panel pa6 = new Panel();
        public static Button[] btns = new Button[] { };
        class LocalSetup
        {
            public static void LocalMain(Form frm)
            {
                frm.FormClosing += (e, sender) => FunCom.neverClose(e, sender, trueFalse);
                frm.Size = new Size(
                    int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Main]", "XY")[0])),
                    int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Main]", "XY")[1]))
                    );
                frm.Text = ConCom.appName;
                frm.Location = new Point(
                    int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Main]", "Location")[0])),
                    int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Main]", "Location")[1]))
                    );
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Padding = new Padding(0, 0, 0, 0);
            }
        }
        class LocalStartup
        {
            private static void LocalMain1(Form frm)
            {
                FunCom.AddPanel(pa3, 0, frm, new int[] { 0, 0 });
                ContextMenuStrip ContextMenuStrip = new ContextMenuStrip();
                FunCom.AddContextMenuStrip(pa1, new string[] { "アプリケーションを終了", "最小化" }, new EventHandler[] {
                    LocalClose.LocalMain,
                    (sender, e) => {
                        frm.WindowState = FormWindowState.Minimized;
                    }
                });
                FunCom.AddPanel(pa2, 1, frm, new int[] { 0, 100 });
                Label lbl1 = new Label();
                FunCom.AddLabel(lbl1, 5, pa2);
                lbl1.Text = ConCom.appName;
                lbl1.Font = new Font("Segoe Print", 30, FontStyle.Bold);
                lbl1.Location = new Point(10, 0);
                FunCom.AddPanel(pa1, 1, frm, new int[] { 0, 20 });
                FunCom.AddPanel(pa4, 4, frm, new int[] { 0, 20 });
                pa1.BackColor = Color.RoyalBlue;
                pa4.BackColor = Color.RoyalBlue;
                FunCom.AddPanel(pa6, 0, pa3, new int[] { 0, 0 });
                FunCom.AddPanel(pa5, 2, pa3, new int[] { 130, 0 });
                pa5.AutoScroll = true;
                ConInstance.saving = new Saving();
                ConInstance.billing = new Billing();
                ConInstance.asset = new Asset();
                ConInstance.analysis = new Analysis();
                ConInstance.setting = new Setting();
                UserControl[] userControls = { ConInstance.saving, ConInstance.billing, ConInstance.asset, ConInstance.analysis, ConInstance.setting};
                for (int i = 0; i < userControls.Length; i++)
                {
                    Button btn = new Button();
                    Array.Resize(ref btns, btns.Length + 1);
                    btns[btns.Length - 1] = btn;
                    FunCom.AddButton(btn, 5, i, pa5, new int[] { 100, 70 });
                    btn.Location = new Point(15, 85 * i + 80);
                    btn.Text = ConMain.mainButton[i];
                    btn.Font = new Font("Segoe Print", 8, FontStyle.Regular);
                    btn.BackColor = ConColor.mainButtonColor;
                    btn.Click += (sender, e) =>
                    {
                        for (int j = 0; j < userControls.Length; j++)
                        {
                            btns[j].BackColor = ConColor.mainButtonColor;
                            UserControl uc = userControls[j];
                            uc.Visible = false;
                        }
                        btns[btn.TabIndex].BackColor = ConColor.mainButtonColorPushed;
                        userControls[btn.TabIndex].Visible = true;
                    };
                }
            }
            public static void LocalMain(Form frm) { LocalMain1(frm); LocalMain2(); }
        }
        private static void LocalMain2()
        {
            UserControl[] userControls = { ConInstance.saving, ConInstance.billing, ConInstance.asset, ConInstance.analysis, ConInstance.setting };
            for (int i = 0; i < userControls.Length; i++)
            {
                UserControl uc = userControls[i];
                FunCom.AddUserControl(uc, 0, pa6);
                uc.Visible = false;
            }
            btns[ConMain.startupCode].BackColor = ConColor.mainButtonColorPushed;
            userControls[ConMain.startupCode].Visible = true;
        }
        class LocalCleaning
        {
            public static void LocalMain()
            {
            }
        }
        class LocalLoad
        {
            public static void LocalMain()
            {
            }
        }
        private void Soy_Load(object sender, EventArgs e)
        {
            ConTask.AppStartup();
            LocalSetup.LocalMain(this);
            LocalStartup.LocalMain(this);
            LocalCleaning.LocalMain();
            LocalLoad.LocalMain();
        }
        class LocalClose
        {
            public static void LocalMain(object sender, EventArgs e)
            {
                trueFalse = false;
                Process myProcess = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcessesByName(myProcess.ProcessName);
                // WindowsFormsApp1をすべて終了させる
                foreach (Process process in processes)
                {
                    // クローズメッセージを送信する
                    process.CloseMainWindow();
                }
            }
        }
    }
}
