using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soy
{
    class ConTask
    {
        public static void AppStartup()
        {
            Task.Run(() =>
            {
                if (int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "backupGeneration"))) > 0)
                {
                    FunSetting.CopyFile(FunSQL.dataSource,"_bak",int.Parse(string.Format("{0}",FunINI.GetString(ConFILE.iniDefault,"[Setting]", "backupGeneration"))));
                }
                if (int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "vaccumStatus"))) == 1)
                {
                    FunSQL.SQLDML("SQL9003", ConSQL.VacuumSQL.SQL9003, new string[] { }, new string[] { });
                }
            });
        }
        public static async Task LOGTask(string msg,string fileName,int status)
        {
            Task RunTask = Task.Run(() =>
            {
                if (status == 0)
                {
                    status = 1;
                    FunMSG.WrtMsg(fileName, msg);
                }
            });
            await RunTask;
            status = 0;
        }
    }

}
