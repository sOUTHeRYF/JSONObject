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

    }
}
