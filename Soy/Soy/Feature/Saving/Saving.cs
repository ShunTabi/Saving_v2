using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soy
{
    public partial class Saving : UserControl
    {
        public Saving()
        {
            InitializeComponent();
        }
        //定義
        public static int FirstLoadStatus = ConInstance.savingFirstLoad;
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Button[] btns = new Button[] { };
        class LocalSetup
        {
            private static void Common1(UserControl uc)
            {
                //uc.VisibleChanged += (sender, e) => { if (uc.Visible == false) { return; } else { cleaning.LocalMain(); load.LocalMain(); } };
                FunCom.AddPanel(pa2, 0, uc, new int[] { 0, 0 });
                FunCom.AddPanel(pa1, 1, uc, new int[] { 0, 100 });
                string[] btnNames = new string[] { "口座","貯金"};
                ConInstance.account = new Account();
                ConInstance.savingAmount = new SavingAmount();
                UserControl[] userControls = { ConInstance.account, ConInstance.savingAmount};
                for (int i = 0; i < userControls.Length; i++)
                {
                    Button btn = new Button();
                    Array.Resize(ref btns, btns.Length + 1);
                    btns[btns.Length - 1] = btn;
                    FunCom.AddButton(btn, 5, i, pa1, new int[] { 90, 50 });
                    btn.Text = btnNames[i];
                    btn.Location = new Point(10 + i * 95, 10);
                    btn.BackColor = ConColor.subButtonColor;
                    btn.Click += (sender, e) => {
                        for (int j = 0; j < userControls.Length; j++)
                        {
                            btns[j].BackColor = ConColor.subButtonColor;
                            uc = userControls[j];
                            uc.Visible = false;
                        }
                        btns[btn.TabIndex].BackColor = ConColor.subButtonColorPushed;
                        userControls[btn.TabIndex].Visible = true;
                    };
                }
            }
            private static void Common2()
            {
                UserControl[] userControls = { ConInstance.account, ConInstance.savingAmount };
                for (int i = 0; i < userControls.Length; i++)
                {
                    UserControl uc = userControls[i];
                    FunCom.AddUserControl(uc, 0, pa2);
                    uc.Visible = false;
                }
                btns[ConMain.savingStartupCode].BackColor = ConColor.subButtonColorPushed;
                userControls[ConMain.savingStartupCode].Visible = true;
            }
            public static void LocalMain(UserControl uc)
            {
                Common1(uc);
                Common2();
            }
        }
        private void Saving_VisibleChanged(object sender, EventArgs e)
        {
            if (ConInstance.saving.Visible == true)
            {
                if (ConInstance.savingFirstLoad < 2)
                {
                    ConInstance.savingFirstLoad += 1;
                }
                int LoadStatus = ConInstance.savingFirstLoad;
                if (LoadStatus == 1)
                {
                    LocalSetup.LocalMain(this);
                    //startup.LocalMain();
                }
            }
            else if (ConInstance.saving.Visible == false && ConInstance.savingFirstLoad == 1 && FirstLoadStatus == 1)
            {
                ConInstance.savingFirstLoad += 1;
                LocalSetup.LocalMain(this);
                //startup.LocalMain();
            }
        }
    }
}
