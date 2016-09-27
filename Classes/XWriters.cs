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
        public string errmsg = "";
        public int succ;

        public int ReadFromFile(string filepath)
        {
            try
            {
                TextReader reader = new StreamReader(filepath);
                reader.ReadLine();
                reader.Close();
                this.succ = 1;
            }
            catch (Exception exception)
            {
                this.errmsg = exception.Message;
                this.succ = 0;
            }
            return this.succ;
        }

        public int WriteToFile(string text, string filepath)
        {
            try
            {
                File.WriteAllText(filepath, text);
                this.succ = 1;
            }
            catch (Exception exception)
            {
                this.errmsg = exception.Message;
                this.succ = 0;
            }
            return this.succ;
        }
    }
}