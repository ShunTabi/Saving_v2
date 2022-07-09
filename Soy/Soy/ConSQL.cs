using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soy
{
    class ConSQL
    {
        public class ComSQL
        {
            public static string now = "datetime(strftime('%s', 'now'), 'unixepoch', 'localtime')";
            public static string SQLLimit = FunINI.GetString(ConFILE.iniDefault,"[db]", "SQLLimit")[0];
        }
        public class AccountSQL
        {
            public static string SQL0000 = $"SELECT * FROM T_ACCOUNT ORDER BY ACCOUNT_DATETIME DESC,ACCOUNT_ID LIMIT {ComSQL.SQLLimit}";
            public static string SQL0001 = "SELECT * FROM T_ACCOUNT WHERE ACCOUNT_ID = @ACCOUNT_ID";
            public static string SQL0010 = $"INSERT INTO T_ACCOUNT(ACCOUNT_NAME,ACCOUNT_DATETIME) VALUES(@ACCOUNT_NAME,{ComSQL.now})";
            public static string SQL0020 = $"UPDATE T_ACCOUNT SET ACCOUNT_NAME = @ACCOUNT_NAME,ACCOUNT_DATETIME = {ComSQL.now} WHERE ACCOUNT_ID = @ACCOUNT_ID";
            public static string SQL0030 = "DELETE FROM T_ACCOUNT WHERE ACCOUNT_ID = @ACCOUNT_ID";
        }
        public class SavingSQL
        {
            public static string SQL0100 = $"SELECT * FROM V_SAVING WHERE (SAVING_DATE BETWEEN @MONTH1 AND @MONTH2) AND ACCOUNT_NAME LIKE @KEYWORD  ORDER BY SAVING_DATE DESC,ACCOUNT_ID LIMIT {ComSQL.SQLLimit}";
            public static string SQL0101 = "SELECT * FROM V_SAVING WHERE SAVING_ID = @SAVING_ID";
            public static string SQL0110 = $"INSERT INTO T_SAVING(ACCOUNT_ID,SAVING_DATE,SAVING_AMOUNT,SAVING_DATETIME) VALUES(@ACCOUNT_ID,strftime('%Y-%m-01 00:00:00',@SAVING_DATE),@SAVING_AMOUNT,{ComSQL.now})";
            public static string SQL0120 = $"UPDATE T_SAVING SET ACCOUNT_ID = @ACCOUNT_ID,SAVING_DATE = strftime('%Y-%m-01 00:00:00',@SAVING_DATE),SAVING_AMOUNT = @SAVING_AMOUNT,SAVING_DATETIME = {ComSQL.now} WHERE SAVING_ID = @SAVING_ID";
            public static string SQL0130 = "DELETE FROM T_SAVING WHERE SAVING_ID = @SAVING_ID";
        }
        public class CardSQL
        {
            public static string SQL0200 = $"SELECT * FROM T_CARD ORDER BY CARD_DATETIME DESC,CARD_ID LIMIT {ComSQL.SQLLimit}";
            public static string SQL0201 = "SELECT * FROM T_CARD WHERE CARD_ID = @CARD_ID";
            public static string SQL0210 = $"INSERT INTO T_CARD(CARD_NAME,CARD_DATETIME) VALUES(@CARD_NAME,{ComSQL.now})";
            public static string SQL0220 = $"UPDATE T_CARD SET CARD_NAME = @CARD_NAME,CARD_DATETIME = {ComSQL.now} WHERE CARD_ID = @CARD_ID";
            public static string SQL0230 = "DELETE FROM T_CARD WHERE CARD_ID = @CARD_ID";
        }
        public class BillingSQL
        {
            public static string SQL0300 = $"SELECT * FROM V_BILLING WHERE (BILLING_DATE BETWEEN @MONTH1 AND @MONTH2) AND CARD_NAME LIKE @KEYWORD ORDER BY BILLING_DATE DESC,CARD_ID LIMIT {ComSQL.SQLLimit}";
            public static string SQL0301 = "SELECT * FROM V_BILLING WHERE BILLING_ID = @BILLING_ID";
            public static string SQL0310 = $"INSERT INTO T_BILLING(CARD_ID,BILLING_DATE,BILLING_AMOUNT,BILLING_DATETIME) VALUES(@CARD_ID,strftime('%Y-%m-01 00:00:00',@BILLING_DATE),@BILLING_AMOUNT,{ComSQL.now})";
            public static string SQL0320 = $"UPDATE T_BILLING SET CARD_ID = @CARD_ID,BILLING_DATE = strftime('%Y-%m-01 00:00:00',@BILLING_DATE),BILLING_AMOUNT = @BILLING_AMOUNT,BILLING_DATETIME = {ComSQL.now} WHERE BILLING_ID = @BILLING_ID";
            public static string SQL0330 = "DELETE FROM T_BILLING WHERE BILLING_ID = @BILLING_ID";
        }
        public class AssetSQL
        {
            public static string SQL0400 = $"SELECT * FROM V_ASSET WHERE ASSET_DATE BETWEEN @MONTH1 AND @MONTH2 ORDER BY ASSET_DATE DESC LIMIT {ComSQL.SQLLimit}";
            }
        public class AnalysisSQL
        {
            public static string SQL0500 = $"SELECT * FROM V_ASSET WHERE ASSET_DATE BETWEEN @MONTH1 AND @MONTH2 ORDER BY ASSET_DATE";
            public static string SQL0501 = $"SELECT * FROM V_SAVING2 WHERE ACCOUNT_NAME = @ACCOUNT_NAME  AND (DATECOLUMNS BETWEEN @MONTH1 AND @MONTH2)  ORDER BY DATECOLUMNS";
            public static string SQL0502 = $"SELECT * FROM V_BILLING2 WHERE CARD_NAME = @CARD_NAME  AND (DATECOLUMNS BETWEEN @MONTH1 AND @MONTH2) ORDER BY DATECOLUMNS";
            public static string SQL0504 = $"SELECT * FROM V_DATE WHERE DATECOLUMNS BETWEEN @MONTH1 AND @MONTH2 ORDER BY DATECOLUMNS";

        }
        public class VacuumSQL
        {
            public static string SQL9003 = "VACUUM";
        }
    }
}
