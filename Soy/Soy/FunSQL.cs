using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Soy
{
    class FunSQL
    {
        //定義
        public static string dataSource = FunINI.GetString(ConFILE.iniDefault, "[db]", "dataSource")[0];
        private static void SQLLOG(string msg)
        {
            if (int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[0])) == 1)
            {
                Task ActiveTask = ConTask.LOGTask(msg, ConFILE.sqlLog, ConFILE.sqlLogStatus);
            }
        }
        public static SQLiteDataReader SQLSELECT(string sqlcode,string sql,string[] parameters,string[] values)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            sb.Append(sqlcode);
            sb.Append("：");
            SQLiteDataReader output = null;
            SQLiteConnection Conn = new SQLiteConnection($"DataSource={dataSource}");
            Conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, Conn);
            if (int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[17])) == 1)
            {
                sb1 = new StringBuilder(sqlcode);
                sb1.Append(":");
                sb1.Append(sql);
            }
            if (parameters.Length != 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if(int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[17])) == 1)
                    {
                        sb2 = new StringBuilder("\n");
                        sb2.Append(sqlcode);
                        sb2.Append(":");
                        sb2.Append(parameters[i]);
                        sb3 = new StringBuilder("\n");
                        sb3.Append(sqlcode);
                        sb3.Append(":");
                        sb3.Append(values[i]);
                        sb1.Append(sb2.ToString());
                        sb1.Append(sb3.ToString());
                    }
                    cmd.Parameters.Add(new SQLiteParameter(parameters[i], values[i]));
                }
            }
            try
            {
                output = cmd.ExecuteReader();
                sb.Append("[Success]");
                SQLLOG(sb.ToString());
                if (int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[17])) == 1)
                {
                    Task ActiveTask = ConTask.LOGTask(sb1.ToString(), ConFILE.debugLog, ConFILE.debugLogStatus);
                }
            }
            catch(Exception ex)
            {
                sb.Append(ex.Message);
                FunMSG.ErrMsg(sb.ToString());
            }
            return output;
        }
        public static void SQLDML(string sqlcode,string sql,string[] parameters,string[] values)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            sb.Append(sqlcode);
            sb.Append("：");
            SQLiteConnection Conn = new SQLiteConnection($"DataSource={dataSource}");
            Conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, Conn);
            if (int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[17])) == 1)
            {
                sb1 = new StringBuilder(sqlcode);
                sb1.Append(":");
                sb1.Append(sql);
            }
            if (parameters.Length != 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[17])) == 1)
                    {
                        sb2 = new StringBuilder("\n");
                        sb2.Append(sqlcode);
                        sb2.Append(":");
                        sb2.Append(parameters[i]);
                        sb3 = new StringBuilder("\n");
                        sb3.Append(sqlcode);
                        sb3.Append(":");
                        sb3.Append(values[i]);
                        sb1.Append(sb2.ToString());
                        sb1.Append(sb3.ToString());
                    }
                    cmd.Parameters.Add(new SQLiteParameter(parameters[i], values[i]));
                }
            }
            try
            {
                cmd.ExecuteNonQuery();
                if (int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[17])) == 1)
                {
                    Task ActiveTask = ConTask.LOGTask(sb1.ToString(), ConFILE.debugLog, ConFILE.debugLogStatus);
                }
                sb.Append("[Success]");
                SQLLOG(sb.ToString());
            }
            catch (Exception ex)
            {
                sb.Append(ex.Message);
                FunMSG.ErrMsg(sb.ToString());
            }
        }
    }
}
