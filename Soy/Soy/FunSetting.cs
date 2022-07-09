using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Soy
{
    class FunSetting
    {
        public static void AppBootup(string Appname, string arg)
        {
            if (Appname == "")
            {
                StringBuilder sb = new StringBuilder(Appname);
                sb.Append("の起動に失敗しました。");
                FunMSG.ErrMsg(sb.ToString());
            }
            else
            {
                Process Proc = new Process();
                Proc.StartInfo.UseShellExecute = true;
                Proc.StartInfo.FileName = Appname;
                Proc.StartInfo.Arguments = arg;
                Proc.StartInfo.ErrorDialog = true;
                Proc.Start();
            }
        }
        public static void CopyFile(string filename, string key, int backupGeneration)
        {
            StringBuilder sb = new StringBuilder(filename);
            sb.Append(key);
            string f = sb.Append("01").ToString();
            DateTime filetime1 = File.GetLastWriteTime(filename);
            DateTime filetime2 = File.GetLastWriteTime(f);
            if(filetime1 != filetime2 || !File.Exists(f))
            {
                for (int i = 0; i < backupGeneration + 1; i++)
                {
                    StringBuilder sb1 = new StringBuilder(filename);
                    sb1.Append(key);
                    StringBuilder sb2 = new StringBuilder(filename);
                    sb2.Append(key);
                    string f1 = sb1.Append((backupGeneration - i).ToString("00")).ToString();
                    string f2 = sb2.Append((backupGeneration - i + 1).ToString("00")).ToString();
                    if (i == backupGeneration)
                    {
                        File.Copy(filename, f2);
                        continue;
                    }
                    if (File.Exists(f1))
                    {
                        if (i == 0)
                        {
                            File.Delete(f1);
                        }
                        else
                        {
                            File.Copy(f1, f2);
                            File.Delete(f1);
                        }
                    }
                }
            }
        }
    }
}
