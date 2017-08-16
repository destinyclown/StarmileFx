using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace StarmileFx.Common
{
    /// <summary>
    /// 工具集合类
    /// </summary>
    public class Tool
    {
        #region 生成16位的随机GUID函数
        /// <summary>
        /// 生成16位的随机GUID函数
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string GenerateGuid16String(Guid guid)
        {
            long i = 1;
            foreach (byte b in guid.ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        #endregion

        #region 非法关键字检查
        /// <summary>
        /// 非法关键字检查
        /// </summary>
        /// <param name="Emtxt">要检查的字符串</param>
        /// <returns>如果字符串里没有非法关键字，返回true，否则，返回false</returns>
        public static bool CheckWord(string Emtxt)
        {
            string keyword = @"^18岁以下勿|AV电影|AV女|A片|黑社会|虐待|安眠药|办理文凭|办证|保钓|本拉登|冰毒|波波|博彩|不良少女|藏独|操你|草你|成人|成人电影|成人卡通|成人漫画|成人片|成人图片|成人文学|成人小说|赤裸|假币|枪支|手枪|处女|春宵|春药|催情药|达赖|独裁者|大陆官员|骚乱|代开发票|裸聊|调情|东突|毒品|赌博|赌球|二奶|法轮功|法輪功|反动|反革命|反共|反华|反政府|反中游行|假钞|仿真枪|风骚|干你|肛交|高潮|暴乱|共产党|共产主义|迷香|国民党|嗨妹|嗨药|换妻|黄片|黄色电影|鸡巴|鸡吧|妓女|假币|奸淫|监听器|疆独|叫床|禁区|禁书|精液|巨波|嗑药|口交|拉登|老虎机|六合采|六合彩|乱伦|裸体|卖淫|毛片|蒙汗药|迷魂药|嫩穴|女优|色情|兽交|退党|我操|性爱|阴唇|阴蒂|阴户|阴茎|阴毛|淫乱|淫靡|淫水|中共|自慰|做爱|做鸡";
            Regex regex = new Regex(keyword, RegexOptions.IgnoreCase);
            if (regex.IsMatch(Emtxt))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// SQL注入关键字过滤
        /// </summary>
        private const string StrKeyWord = @"^\s*select | select |^\s*insert | insert |^\s*delete | delete |^\s*from | from |^\s*declare | declare |^\s*exec | exec | count\(|drop table|^\s*update | update |^\s*truncate | truncate |asc\(|mid\(|char\(|xp_cmdshell|^\s*master| master |exec master|netlocalgroup administrators|net user|""|^\s*or | or |^\s*and | and |^\s*null | null ";

        /// <summary>
        /// 关键字过滤
        /// </summary>
        /// <param name="_sWord"></param>
        /// <returns></returns>
        public static string ResplaceSql(string _sWord)
        {
            if (!string.IsNullOrEmpty(_sWord))
            {
                Regex regex = new Regex(StrKeyWord, RegexOptions.IgnoreCase);
                _sWord = regex.Replace(_sWord, "");
                _sWord = _sWord.Replace("'", "''");
                return _sWord;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region html输出转义
        /// <summary>
        /// html输出转义
        /// </summary>
        /// <param name="str">要输出的字符串</param>
        /// <returns></returns>
        public static string HtmlEncode(string str)
        {
            if (str == null || str == "")
                return "";
            str = str.Replace(">", "&gt;");
            str = str.Replace(" <", "&lt;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("  ", " &nbsp;");
            str = str.Replace("\"", "&quot;");
            str = str.Replace("\'", "&#39;");
            str = str.Replace("\n", " <br/> ");
            return str;
        }
        #endregion

        #region 获取小数位（四舍五入 ，保留小数）

        /// <summary>
        /// 四舍五入，保留2位小数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal Rounding(decimal obj)
        {
            return Rounding(obj, 2);
        }
        /// <summary>
        /// 四舍五入，保留n位小数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="len">保留几位小数</param>
        /// <returns></returns>
        public static decimal Rounding(decimal obj, int len)
        {
            return Math.Round(obj, len, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 只舍不入，保留2位小数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal RoundingMin(decimal obj)
        {
            return RoundingMin(obj, 2);
        }

        /// <summary>
        /// 只舍不入，保留n位小数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static decimal RoundingMin(decimal obj, int len)
        {
            var str = "0." + "".PadLeft(len, '0') + "5";
            decimal dec = Convert.ToDecimal(str);
            return Rounding(obj - dec, len);
        }


        /// <summary>
        /// 只舍不入，保留2位小数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal RoundingMax(decimal obj)
        {
            return RoundingMax(obj, 2);
        }

        /// <summary>
        /// 只舍不入，保留n位小数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static decimal RoundingMax(decimal obj, int len)
        {
            var str = "0." + "".PadLeft(len, '0') + "4";
            decimal dec = Convert.ToDecimal(str);
            return Rounding(obj + dec, len);
        }

        #endregion

        #region 根据月份找出是哪个季度
        /// <summary>
        /// 根据月份找出是哪个季度
        /// </summary>
        /// <param name="month">月份</param>
        /// <returns>季度</returns>
        public static int GetQuarter(int month)
        {
            int Quarter = 0;
            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    Quarter = 1;
                    break;
                case 4:
                case 5:
                case 6:
                    Quarter = 2;
                    break;
                case 7:
                case 8:
                case 9:
                    Quarter = 3;
                    break;
                case 10:
                case 11:
                case 12:
                    Quarter = 4;
                    break;
            }
            return Quarter;
        }
        #endregion

        #region 日期处理函数
        /// <summary>
        /// 将DateTime日期格式化为带年月日的日期字符串。
        /// </summary>
        /// <param name="datetime">DateTime日期</param>
        /// <returns>带年月日的日期字符串</returns>
        public static string GetChinaDate(DateTime datetime)
        {
            if (datetime != null && datetime != DateTime.MinValue)
            {
                return datetime.ToString("yyyy年MM月dd日");
            }
            return "";
        } 

        /// <summary>
        /// 将DateTime日期格式化为带年月日的日期字符串。
        /// </summary>
        /// <param name="datetime">DateTime日期</param>
        /// <returns>yyyy-MM-dd</returns>
        public static string GetShortDate(DateTime datetime)
        {
            if (datetime != null && datetime != DateTime.MinValue)
            {
                return datetime.ToString("yyyy-MM-dd");
            }
            return "";
        }
        #endregion

        #region 信息屏蔽
        /// <summary>
        /// 手机号码屏蔽
        /// </summary>
        /// <param name="mob"></param>
        /// <returns></returns>
        public static string PhoneShield(string Phone)
        {
            Phone = Phone.Substring(0, 3) + "****" + Phone.Substring(7);
            return Phone;
        }

        /// <summary>
        /// 邮箱屏蔽
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public static string EmailShield(string Email)
        {
            if (Email.Length > 7)
            {
                Email = Email.Substring(0, 3) + "****" + Email.Substring(Email.Length - 5);
            }
            return Email;
        }

        /// <summary>
        /// 名字屏蔽
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public static string NameShield(string Name)
        {
            if (Name.Length > 4)
            {
                Name = Name.Substring(0, 2) + "**";
            }
            else
            {
                Name = Name.Substring(0, 1) + "**";
            }
            return Name;
        }

        /// <summary>
        /// 字符串省略操作
        /// </summary>
        /// <param name="str"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string Shield(string str, int i)
        {
            if (str.Length > i)
            {
                str = str.Substring(0, i) + "...";
            }
            return str;
        }
        #endregion

        #region 得到百分比函数
        /// <summary>
        /// 得到百分比函数(a/b)
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static string Percent(object A, object B)
        {
            double rate = new double();
            try
            {
                rate = Convert.ToDouble(A) / Convert.ToDouble(B);
            }
            catch (Exception)
            {
                return null;
            }
            return rate.ToString("p"); //格式为12.23%
        }
        #endregion 得到百分比函数

        #region 判断实体中是否有属性为Null
        /// <summary>
        /// 判断实体中是否有属性为Null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool GetProperties<T>(T t, ref string MsgStr)
        {
            try
            {
                PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                if (properties.Length <= 0)
                {
                    return false;
                }
                foreach (PropertyInfo item in properties)
                {
                    if (item == null) continue;
                    object value = item.GetValue(t, null);
                    if (value == null)
                    {
                        MsgStr += item.Name + ",";
                    }
                }
                if (!string.IsNullOrEmpty(MsgStr))
                {
                    MsgStr = MsgStr.Substring(0, MsgStr.Length - 1);
                    return true;
                }
            }
            catch { }
            return false;
        }
        #endregion
    }
}
