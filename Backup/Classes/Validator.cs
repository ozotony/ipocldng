using System;
using System.Collections.Generic;
using System.Text;

namespace Ipong.Classes
{
     public  class Validator
    {
		public int IsPresent(string txt)
		{
            int cnt = 0;
            if (txt == ""){ cnt++; }
            return cnt;
		}
        public int IsDecimal(string txt)
		{
            int cnt = 0;
			try
			{
				Convert.ToDecimal(txt);
                return cnt;
			}
			catch (FormatException)
			{
                cnt++; return cnt;
			}
		}
		public int IsInt64(string txt)
		{
            int cnt = 0;
			try
			{
				Convert.ToInt64(txt);
                return cnt;
			}
			catch (FormatException)
			{
                cnt++;  return cnt;
			}
		}
        public int IsInt32(string txt)
        {
            int cnt = 0;
            try
            {
                Convert.ToInt32(txt);
                return cnt;
            }
            catch (FormatException)
            {
                cnt++; return cnt;
            }
        }
		public int IsWithinRange(string txt, decimal min, decimal max)
		{
            int cnt = 0;
			decimal number = Convert.ToDecimal(txt);
			if (number < min || number > max)
			{

                cnt++; return cnt;
			}
            return cnt;
		}
        public int IsValidEmail(string txt)
        {
            int cnt = 0;
            if (txt != "")
            {
                if (txt.IndexOf("@") == -1 ||
                     txt.IndexOf(".") == -1)
                {

                    cnt++; return cnt;
                }
                else
                {
                   return cnt;
                }

            } 
            return cnt;
        }
        public int IsValidMobile(string txt)
        {
          //  List<string> num = new List<string>();  for (int i = 0; i < 10; i++) { num.Add(i.ToString());  }
            int cnt = 0;
            if (txt != "")
            {
                if (txt.Length>=11)
                {
                   
                }
                else
                {
                    cnt++; 
                }
            }
            else
            {
                cnt++;
            }
            return cnt;
        }
    }

}