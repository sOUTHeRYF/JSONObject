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
    }
}
