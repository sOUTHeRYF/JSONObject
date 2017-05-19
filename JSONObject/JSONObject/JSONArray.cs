using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSON
{
    public class JSONArray : List<object>
    {
        public JSONArray()
        {
        }
        override public string ToString()
        {
            StringBuilder builer = new StringBuilder("[");
            foreach (Object index in this)
            {
                builer.Append(index.ToJsonString());
                builer.Append(",");
            }
            if (this.Count > 0)
                builer.DelTail();
            builer.Append("]");
            return builer.ToString();
        }
        public static JSONArray FromString(string str)
        {
            JSONArray result = new JSONArray();
            str = str.Trim();
            if (str.IsStrStartEndByChar('[', ']') == true)
            {
                str = str.CutOffFirstAndLast();
                string[] objs = str.Split(',');
                if (objs.Length > 0)
                {
                    foreach (string index in objs)
                    {
                        string indexStr = index.Trim();
                        Object obj = new object();
                        if (ClassFuncInjecter.TryParseObjFromStr(indexStr, out obj))
                        {
                            result.Add(obj);
                        }
                    }
                }
            }
            return result;
        }
    }
}
