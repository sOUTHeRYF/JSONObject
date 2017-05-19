using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSON
{
    public static class ClassFuncInjecter
    {
       
        internal static void DelTail(this string str)
        {
            int length = str.Length;
            if (length > 0)
            {
                str = str.Substring(0, length - 1);
            }
        }
        internal static void DelTail(this StringBuilder str)
        {
            int length = str.Length;
            if (length > 0)
            {
                str = str.Remove( length - 1,1);
            }
        }

        #region Override ToString
        internal static string ToJsonString(this string str)
        {
            return "\"" + str + "\"";
        }
        internal static string ToJsonString(this bool booler)
        {
            return booler == true ? "true" : "false";
        }
        internal static string ToJsonString(this Object obj)
        {
            Type objType = obj.GetType();
            if (objType == typeof(String))
            {
                return ((string)obj).ToJsonString();
            }
            else if (objType == typeof(Boolean))
            {
                return ((Boolean)obj).ToJsonString();
            }
            else if (objType == typeof(List<Object>))
            {
                return ((List<Object>)obj).ToJsonString();
            }
            else if (objType == typeof(Dictionary<string, Object>))
            {
                return ((Dictionary<string, Object>)obj).ToJsonString();
            }
            else
                return obj.ToString();
        }
        internal static string ToJsonString<T>(this T[] array)
        {
            StringBuilder builer = new StringBuilder("[");
            foreach (T index in array)
            {
                builer.Append(index.ToString());
                builer.Append(",");
            }
            if (array.Length > 0)
                builer.DelTail();
            builer.Append("]");
            return builer.ToString();
        }
        internal static string ToJsonString(this List<Object> array)
        {
            StringBuilder builer = new StringBuilder("[");
            foreach (Object index in array)
            {
                builer.Append(index.ToString());
                builer.Append(",");
            }
            if (array.Count > 0)
                builer.DelTail();
            builer.Append("]");
            return builer.ToString();
        }
        internal static string ToJsonString(this Dictionary<string,Object> dic)
        {
            StringBuilder builder = new StringBuilder("{");
            foreach (KeyValuePair<string,Object> pair in dic)
            {
                StringBuilder pairBuilder = new StringBuilder();
                pairBuilder.Append(pair.Key.ToString());
                pairBuilder.Append(":");
                pairBuilder.Append(pair.Value.ToString());
                builder.Append(pairBuilder);
                builder.Append(",");
            }
            builder.DelTail();
            builder.Append("}");
            return builder.ToString();
        }
        #endregion
        #region FromString
        internal static bool TryParseObjFromStr(string str, out Object result)
        {
            result = null;
            if (null != str)
            {
                string tempStr = "";
                if (TryParseStringFromStr(str,out tempStr))
                {
                    result = tempStr;
                    return true;
                }
                Dictionary<string,Object> tempDic = new Dictionary<string, object>();
                if (TryParseDictionaryFromStr(str, out tempDic))
                {
                    result = tempDic;
                    return true;
                }
                List<Object> tempList = new List<object>();
                if (TryParseListFromStr(str, out tempList))
                {
                    result = tempList;
                    return true;
                }
                int tempInt = 0;
                if (TryParseIntFromStr(str, out tempInt))
                {
                    result = tempInt;
                    return true;
                }
                double tempDouble = 0;
                if (TryParseDoubleFromStr(str, out tempDouble))
                {
                    result = tempDouble;
                    return true;
                }
                bool tempBool = false;
                if (TryParseBoolFromStr(str, out tempBool))
                {
                    result = tempBool;
                    return true;
                }
                return false;
            }
            else
                return false; 
        }
        internal static bool TryParseStringFromStr(string str, out String result)
        {
            result = null;
            str = str.Trim();
            if (str.IsStrStartEndByChar('\"', '\"') == true)
            {
                str = str.CutOffFirstAndLast();
                result = str;
                return true;
            }
            else
                return false;
        }
        internal static bool TryParseDoubleFromStr(string str, out Double result)
        {
            result = 0;
            str = str.Trim();
            if (str.IndexOf('.') > 0)
            {
                return Double.TryParse(str, out result);           
            } 
            return false;
        }
        internal static bool TryParseBoolFromStr(string str, out Boolean result)
        {
            str = str.Trim();
            return Boolean.TryParse(str, out result);
        }
        public static bool TryParsePairFromStr(string str, out string resultKey, out Object resultValue)
        {
            str = str.Trim();
            resultKey = "";
            resultValue = null;
            string[] pairStr = str.Split(',');
            if (pairStr.Length == 2)
            {
                if (TryParseStringFromStr(pairStr[0], out resultKey) && TryParseObjFromStr(pairStr[1], out resultValue))
                {
                    return true;
                }
            }
            return false;
        }
        internal static bool TryParseIntFromStr(string str, out Int32 result)
        {
            str = str.Trim();
            return Int32.TryParse(str, out result);
        }
        internal static bool TryParseListFromStr(string str, out List<Object> result)
        {
            result = new List<object>();
            str = str.Trim();
            if (str.IsStrStartEndByChar('[',']') == true)
            {
                str = str.CutOffFirstAndLast();
                string[] objs = str.Split(',');
                if (objs.Length > 0)
                {
                    foreach (string index in objs)
                    {
                        string indexStr = index.Trim();
                        Object obj = new object();
                        if (TryParseObjFromStr(indexStr, out obj))
                        {
                            result.Add(obj);
                        }
                    }
                }

                return true;
            }
            else
                return false;
        }
        internal static bool TryParseDictionaryFromStr(string str, out Dictionary<string, Object> result)
        {
            result = new Dictionary<string, object>();
            str = str.Trim();
            if (str.IsStrStartEndByChar('{', '}') == true)
            {
                str = str.CutOffFirstAndLast();
                string[] objs = str.Split(',');
                if (objs.Length > 0)
                {
                    foreach (string index in objs)
                    {
                        string indexStr = index.Trim();
                        string key = "";
                        Object value = new object();
                        if (TryParsePairFromStr(indexStr, out key, out value))
                        {
                            result.Add(key,value);
                        }
                    }
                }
                return true;
            }
            else
                return false;
        }
        #endregion
        #region Str Utils
        public static bool? IsStrStartEndByChar(this string str, char chrStart,char chrEnd)
        {
            bool? result = null;
            if (!string.IsNullOrWhiteSpace(str))
            {
                result = str.First().Equals(chrStart) && str.Last().Equals(chrEnd);
            }
            return result;
        }
        public static string CutOffFirstAndLast(this string str)
        {
            int length = str.Length;
            if (length > 1)
            {
                return str.Substring(1, str.Length - 1 -1);
            }
            else
                return str;
        }
        #endregion
    }
}
