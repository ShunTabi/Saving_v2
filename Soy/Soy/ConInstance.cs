using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data;


namespace Soy
{
    class ConInstance
    {
        public static Account account;
        public static SavingAmount savingAmount;
        public static Saving saving;
        public static Card card;
        public static BillingAmount billingAmount;
        public static Billing billing;
        public static Asset asset;
        public static Analysis analysis;
        public static Setting setting;
        //
        public static int accountFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[0]));
        public static int savingAmountFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[1]));
        public static int savingFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[2]));
        public static int cardFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[3]));
        public static int billingAmountFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[4]));
        public static int billingFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[5]));
        public static int assetFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[6]));
        public static int analysisFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[7]));
        public static int settingFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[8]));
    }
}
