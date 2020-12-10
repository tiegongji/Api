using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices
{
    public static class ObjectExtensions
    {
        #region 类型转换
        /// <summary>
        /// 转换Long类型
        /// </summary>
        public static long? ToLong(this object o, long? nullVal = null)
        {
            if (null == o)
                return nullVal;
            long oInt;
            if (long.TryParse(o.ToString(), out oInt))
                return oInt;
            return nullVal;
        }


        /// <summary>
        ///  转换Int类型
        /// </summary>
        public static int? ToInt(this object o, int? nullVal = null)
        {
            if (null == o)
                return nullVal;
            int oInt;
            if (int.TryParse(o.ToString(), out oInt))
                return oInt;
            return nullVal;
        }


        /// <summary>
        ///  转换Decimal类型
        /// </summary>
        public static decimal? ToDecimal(this object o, decimal? nullVal = null)
        {
            if (null == o)
                return nullVal;
            decimal oInt;
            if (decimal.TryParse(o.ToString(), out oInt))
                return oInt;
            return nullVal;
        }

        /// <summary>
        ///  转换Bool类型
        /// </summary>
        public static bool? ToBoolean(this object o, string trueVal = "1", string falseVal = "0", bool? nullVal = null)
        {
            if (null == o)
                return nullVal;
            bool oBool;
            var oStr = o.ToString();
            if (bool.TryParse(oStr, out oBool))
                return oBool;
            else if (trueVal == oStr)
                return true;
            else if (falseVal == oStr)
                return false;

            return nullVal;
        }

        /// <summary>
        /// 转换成Guid类型
        /// </summary>
        public static Guid? ToGuid(this object o, Guid? nullVal = null)
        {
            if (null == o)
                return nullVal;
            Guid oGuid;
            if (Guid.TryParse(o.ToString(), out oGuid))
                return oGuid;
            return nullVal;
        }

        /// <summary>
        /// 转换DateTime类型
        /// </summary>
        public static DateTime? ToDateTime(this object o, DateTime? nullValue = null)
        {
            if (null == o)
                return nullValue;
            DateTime oDateTime;
            if (DateTime.TryParse(o.ToString(), out oDateTime))
                return oDateTime;
            return nullValue;
        }

        /// <summary>
        /// 尝试获取字符串值
        /// </summary>
        public static string ToString(this object o, string nullValue = "")
        {
            if (null == o)
                return nullValue;

            return o.ToString();
        }

        /// <summary>
        /// 尝试获取字符串值
        /// </summary>
        public static string ToString(this object o, Func<string> fn, string nullValue = "")
        {
            if (null == o)
                return nullValue;
            return fn();
        }

        /// <summary>
        /// 尝试获取字符串值
        /// </summary>
        public static string ToTrimString(this object o, string nullValue = "")
        {
            if (null == o)
                return nullValue;
            return o.ToString().Trim();
        }

        /// <summary>
        /// 是否为null
        /// </summary>
        public static bool IsNull<T>(this T obj)
        {
            return obj == null;
        }


        public static string ToBase64(this string str)
        {
            Encoding encode = System.Text.Encoding.UTF8;
            byte[] bytedata = encode.GetBytes(str);
            return Convert.ToBase64String(bytedata, 0, bytedata.Length);
        }

        public static string ToStringByBase64(this string str)
        {

            byte[] bpath = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(bpath);
        }

        /// <summary>
        /// 扩展方法，获得枚举的Description
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <param name="nameInstead">当枚举值没有定义DescriptionAttribute，是否使用枚举名代替，默认是使用</param>
        /// <returns>枚举的Description</returns>
        public static string GetDescription(this Enum value, Boolean nameInstead = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }

            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (attribute == null && nameInstead == true)
            {
                return name;
            }
            return attribute == null ? null : attribute.Description;
        }

        #endregion

        #region 
        public static string GetDescriptionOriginal(this Enum @this)
        {
            var name = @this.ToString();
            var field = @this.GetType().GetField(name);
            if (field == null) return name;
            var att = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false);
            return att == null ? field.Name : ((DescriptionAttribute)att).Description;
        }
        #endregion


        /// <summary>
        /// 转换DateTime类型
        /// </summary>
        public static string ToDate(this object o)
        {
            if (null == o)
                return "";
            DateTime oDateTime;
            if (DateTime.TryParse(o.ToString(), out oDateTime))
            {
                if (DateTime.Now.ToString("d") == oDateTime.ToString("d"))
                {
                    return "今天 " + oDateTime.ToString("t");
                }
                else if (DateTime.Now.AddDays(1).ToString("d") == oDateTime.ToString("d"))
                {
                    return "明天 " + oDateTime.ToString("t");
                }
                return oDateTime.ToString("MM月dd日 HH:mm");
            }
            return "";
        }
    }
}
