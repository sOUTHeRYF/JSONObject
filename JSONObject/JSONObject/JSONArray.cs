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
    }
}
