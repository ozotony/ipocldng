using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Net;
using System.Net.Security;

namespace Ipong.InterSwitch.PayDirect.Classes
{
    public class Transactions
    {
        public JavaScriptSerializer js = new JavaScriptSerializer();
        protected InterSwitch.PayDirect.Classes.Hasher hash_value = new InterSwitch.PayDirect.Classes.Hasher();
        protected Ipong.Classes.XObjs.InterSwitchResponse isr = new Ipong.Classes.XObjs.InterSwitchResponse();
        protected string payment_page = ""; protected string check_trans_page = "";
        public string json = ""; public string resp_str = ""; protected string inputString = "";       

        public string DoPaymentX()
        {
            payment_page = ConfigurationManager.AppSettings["pd_payment_page"];
            
            Ipong.Classes.RemotePost rp = new Ipong.Classes.RemotePost();
            rp.Add("product_id", "4584");
            rp.Add("pay_item_id", "101");
            rp.Add("amount","3470000");
            rp.Add("currency", "566");
            rp.Add("site_redirect_url", "http://xpayng.com/xis/pd/xreturn/index.aspx");
            rp.Add("txnref", "D7B9123C8277");
            rp.Add("hash", "95A664FC0B0FE78C0A887B8CB88B6D3E3FC0196BB985B4DAAA07781CF8D8F74FF035E86730EB9DF655692E9F39FD598AAA15F88EF5180B250339FEA6EFAC4E84");
            string succ = rp.SendForm("https://stageserv.interswitchng.com/test_paydirect/pay", "POST");
            return succ.ToString();
        }

         public string DoPayment(Ipong.Classes.XObjs.InterSwitchPostFields ispf)
        {
            payment_page = ConfigurationManager.AppSettings["pd_payment_page"];

            Ipong.Classes.RemotePost rp = new Ipong.Classes.RemotePost();
            rp.Add("product_id", ispf.product_id);
            rp.Add("pay_item_id", ispf.pay_item_id);
            rp.Add("amount", ispf.amount);
            rp.Add("currency", ispf.currency);
            rp.Add("site_redirect_url", ispf.site_redirect_url);
            rp.Add("txn_ref", ispf.txn_ref);
            rp.Add("hash", ispf.hash);
            string succ = rp.SendForm(payment_page, "POST");
            return succ.ToString();
        }

         public Ipong.Classes.XObjs.InterSwitchResponse getJsonTrasactionsData(string product_id, string transactionreference, string amount, string get_trans_hash)
        {
            //inputString = product_id + transactionreference + hash;
            //Get the hash value
          //  string get_trans_hash = hash_value.GetGetSHA512String(inputString);            
            check_trans_page = ConfigurationManager.AppSettings["pd_get_trans_json_page"];
            Ipong.Classes.RemotePost rp = new Ipong.Classes.RemotePost();
            rp.Add("productid", product_id);
            rp.Add("transactionreference", transactionreference);
            rp.Add("amount", amount);
            rp.AddHeader("Hash", get_trans_hash);
            string succ = rp.GetFormResponse(check_trans_page, "GET");
            //0.985
            isr = js.Deserialize<Ipong.Classes.XObjs.InterSwitchResponse>(succ);
            return isr;
        }

         public Ipong.Classes.XObjs.InterSwitchResponse myRedirect(string url, string headerName, string headerValue)
         {
             try
             {
                 HttpWebRequest request = (HttpWebRequest )WebRequest.Create(url);
                // request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:8.0) Gecko/20100101 Firefox/8.0";
                // request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,**;q=0.8";
                // request.UnsafeAuthenticatedConnectionSharing = true;
                 request.Headers.Add(headerName, headerValue);
                 request.Method = "GET";
                 //request.KeepAlive = true;
                // request.AllowAutoRedirect = true;

                 ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

                 HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                 System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
                 string content = sr.ReadToEnd();
                 sr.Close();
                 isr = js.Deserialize<Ipong.Classes.XObjs.InterSwitchResponse>(content);
             }
             catch (Exception ex)
             {
                 string err = ex.ToString();
                 return isr;
             }
             return isr;
         }

         public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
         {
             return true;
         }

         public Ipong.Classes.XObjs.InterSwitchResponse myOldRedirect(string url, string headerName, string headerValue)
         {

             // Response.Clear();
             try
             {
                 System.Net.WebRequest request = System.Net.WebRequest.Create(url);

                 request.Headers.Add(headerName, headerValue);
                 request.Method = "GET";
                 // request.Headers.
                 System.Net.WebResponse response = request.GetResponse();
                 //if (request.Headers["Hash"] != null)
                 //{
                 //    Sess = request.Headers["Hash"].ToString();
                 //}
                 System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);


                 string content = sr.ReadToEnd();

                 sr.Close();

                 isr = js.Deserialize<Ipong.Classes.XObjs.InterSwitchResponse>(content);
             }
             catch (Exception ex)
             {
                 string err = ex.ToString();
             }

             return isr;

         }
    }
}