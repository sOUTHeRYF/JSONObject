using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JSON;
namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            JSONObject obj = new JSONObject();
            obj.Add("name", "Yodo1");
            obj.Add("years", 2017);
            obj.Add("ifDead", true);
            JSONArray array = new JSONArray();
            array.Add("Dev");
            array.Add("Prod");
            array.Add(obj);

      //      obj.Add("Array", array);
            System.Diagnostics.Debug.WriteLine("result:" + array.ToString());
        }
    }
}
