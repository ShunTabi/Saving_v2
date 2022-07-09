using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Soy
{
    class ConCom
    {
        public static string appName = "Soy";
        public static string[] defaultBtnNames = new string[] { "登録", "更新", "削除" };
    }
    class ConColor
    {
        public static Color mainButtonColor = Color.RoyalBlue;
        public static Color mainButtonColorPushed = Color.MediumBlue;
        public static Color subButtonColor = Color.LightSalmon;
        public static Color subButtonColorPushed = Color.OrangeRed;
    }
    class ConSaving
    {
        public static int execCode = 0;
        public static string ID = "0";
        public static string selectedDate = FunDate.getToday(0, 0);
        public static string[] SavingButtons = new string[] { "貯金", "口座" }; 
    }
    class ConSetting
    {
        public static string[] names = new string[]
        {
            "sqlLog","msgBox","msgLog","eventLog","未","未",
            "未","未","未","未","未","未",
            "未","未","未","未","未","debug",
        };
        public static long[] keys = new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static string[][] values = new string[][]
        {
            new string[]{"OFF","ON"},//1
            new string[]{"OFF","ON"},//2
            new string[]{"OFF","ON"},//3
            new string[]{"OFF","ON"},//4
            new string[]{"OFF","ON"},//5
            new string[]{"OFF","ON"},//6
            new string[]{"OFF","ON"},//7
            new string[]{"OFF","ON"},//8
            new string[]{"OFF","ON"},//9
            new string[]{"OFF","ON"},//10
            new string[]{"OFF","ON"},//11
            new string[]{"OFF","ON"},//12
            new string[]{"OFF","ON"},//13
            new string[]{"OFF","ON"},//14
            new string[]{"OFF","ON"},//15
            new string[]{"OFF","ON"},//16
            new string[]{"OFF","ON"},//17
            new string[]{"OFF","ON"},//18
        };
        public static string[] startupSettingCodes = FunINI.GetString(ConFILE.iniDefault, "[Setting]", "startupSettingCode");
    }
}
