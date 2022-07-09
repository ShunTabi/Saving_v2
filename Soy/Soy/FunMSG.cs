using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Soy
{
    class FunMSG
    {
        public static void WrtMsg(string fileName, string msg)
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            StreamWriter writer = new StreamWriter(fileName, true, sjisEnc);
            StringBuilder sb = new StringBuilder("[Thread");
            sb.Append(Thread.CurrentThread.ManagedThreadId.ToString());
            sb.Append("]- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
            sb.Append(FunDate.getToday(4, 0));
            sb.Append(" > \n");
            sb.Append(msg);
            writer.WriteLine(sb.ToString());
            writer.Close();
        }
        public static void ErrMsg(string msg)
        {
            if (int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[1])) == 1)
            {
                MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            if (int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[2])) == 1)
            {
                Task ActiveTask = ConTask.LOGTask(msg, ConFILE.msgLog, ConFILE.msgLogStatus);
            }
        }
    }
}
