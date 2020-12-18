using System;

namespace TGJ.NetworkFreight.Commons.Utils
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Util
    {
        /// <summary>
        /// 获取短信Code
        /// </summary>
        /// <returns></returns>
        public static string GetSmsCode()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }
        public static string ReplaceWithSpecialChar(string value, int startLen = 4, int endLen = 4, char specialChar = '*')
        {
            try
            {
                int lenth = value.Length - startLen - endLen;

                string replaceStr = value.Substring(startLen, lenth);

                string specialStr = string.Empty;

                for (int i = 0; i < replaceStr.Length; i++)
                {
                    specialStr += specialChar;
                }

                value = value.Replace(replaceStr, specialStr);
            }
            catch (Exception)
            {
                throw;
            }

            return value;
        }

        public static string GetColorValue(string color)
        {
            switch (color)
            {
                case "黄色":
                    return "#FFFF00";
                case "绿色":
                    return "#008000";
                case "红色":
                    return "#FF0000";
                case "蓝色":
                    return "#0000FF";
                default:
                    return "#0000FF";
            }
        }
    }
}
