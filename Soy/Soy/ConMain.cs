using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soy
{
    class ConMain
    {
        public static string[] mainButton = new string[]
        {
            "Saving",
            "Billing",
            "Asset",
            "Analysis",
            "Setting",
        };
        public static int startupCode = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault,"[Main]","startupCode")[0]));
        public static int savingStartupCode = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Saving]", "savingStartupCode")[0]));
        public static int billingStartupCode = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Billing]", "billingStartupCode")[0]));
    }
}
