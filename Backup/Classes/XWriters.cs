using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace Ipong.Classes
{
    public class XWriters
    {
        public int succ = 0; public string errmsg = "";

        public int WriteToFile(string text,string filepath)
        {
            try
            {
                //TextWriter tw = new StreamWriter(filepath);
                //tw.WriteLine(text);
                //tw.Close();
                File.WriteAllText(filepath, text);
                succ = 1;
            }
            catch (Exception e)
            {
                errmsg = e.Message; succ = 0;
            }
            return succ;
        }


        public int ReadFromFile(string filepath)
        {
            try
            {
                TextReader tr = new StreamReader(filepath);
                tr.ReadLine();
                tr.Close();
                succ = 1;
            }
            catch (Exception e)
            {
                errmsg = e.Message; succ = 0;
            }
            return succ;
        }
    }
}