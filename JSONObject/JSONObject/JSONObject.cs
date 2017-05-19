using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSON
{
    public class JSONObject : Dictionary<string,Object>
    {
        public JSONObject()
        { 
        }
        override public string ToString()
        {
           // Object obj = "AAA";
        
         //   return ((string)obj).ToJsonString();
            
            StringBuilder builder = new StringBuilder("{");
            foreach (KeyValuePair<string, object> pair in this)
            {
                StringBuilder pairBuilder = new StringBuilder();
                pairBuilder.Append(pair.Key.ToJsonString());
                pairBuilder.Append(":");
                pairBuilder.Append(pair.Value.ToJsonString());
                builder.Append(pairBuilder);
                builder.Append(",");
            }
            builder.DelTail();
            builder.Append("}");
            return builder.ToString();
            
        }
        public static JSONObject FromString(string str)
        {
            JSONObject result = new JSONObject();
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
                        if (ClassFuncInjecter.TryParsePairFromStr(indexStr, out key, out value))
                        {
                            result.Add(key, value);
                        }
                    }
                }
            }
            return result;
        }

    }
}
