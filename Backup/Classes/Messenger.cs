namespace Ipong.Classes
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    public class Messenger
    {

        public string send_sms(string message, string sender, string sendto)
        {
            string username = "aidigbe@mynovasys.com";
            string pass = "Doc2ore";
            string str5 = "";
            string URI = "http://smsdam.com/http/?action=bulksms&message=" + message + "&sender=" + sender + "&mobile=" + sendto + "&username=" + username + "&password=" + pass;
            try
            {
                System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
                //req.Proxy = new System.Net.WebProxy(ProxyString, true); //true means no proxy
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                str5 = sr.ReadToEnd().Trim();

                sr.Close();
                resp.Close();
            }
            catch (WebException exception)
            {
                str5 = exception.ToString();
                return "No net";
            }
            return str5.Replace("1201|", "");
        }

    }
}

