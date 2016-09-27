using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.IO;


namespace Ipong.Classes
{
    public class RemotePost
    {
            private NameValueCollection Inputs = new NameValueCollection();
            private NameValueCollection Extra_headers = new NameValueCollection();
            protected byte[] responseArray = null;

            public void Add(string name, string value)
            {
                Inputs.Add(name, value);
            }

            public void AddHeader(string name, string value)
            {
                Extra_headers.Add(name, value);
            } 

            public int Post(string uriString)
            {
                int succ = 0;
                // Create a new WebClient instance.
                WebClient myWebClient = new WebClient();
                if (Extra_headers.Count > 0) {myWebClient.Headers.Add(Extra_headers);}
                responseArray = myWebClient.UploadValues(uriString, "POST", Inputs);

                if (responseArray != null) { succ = 1; }
                return succ;
            }

            public string mySender(string url, SortedList<string,string> xheaders,string meth)
            {
                string succ = "0";
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    for(int i=0;i<xheaders.Count;i++)
                    {
                        request.Headers.Add(xheaders.Keys[i], xheaders.Values[i]);
                    }
                    request.Method = meth;

                    ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
                    succ = sr.ReadToEnd();
                    sr.Close();
                }
                catch (Exception ex)
                {
                    string err = ex.ToString();
                    return succ;
                }
                return succ;
            }

            public void JustPost(string url, SortedList<string, string> xheaders)
            {
                string postData = "";
                ASCIIEncoding encoding = new ASCIIEncoding();
                for (int i = 0; i < xheaders.Count; i++)
                {
                    if (i == 0)
                    {
                        postData += xheaders.Keys[i] + "=" + xheaders.Values[i];
                    }
                    else
                    {
                        postData += "&" + xheaders.Keys[i] + "=" + xheaders.Values[i];
                    }
                }
                byte[] data = encoding.GetBytes(postData);                

                // Prepare web request...
                HttpWebRequest myRequest =
                  (HttpWebRequest)WebRequest.Create(url);
                
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = data.Length;
                Stream newStream = myRequest.GetRequestStream();
                // Send the data.
                newStream.Write(data, 0, data.Length);
                newStream.Close();
            }

            public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            }

            public string SendForm(string uriString,string send_meth)
            {
                string response = "";
                // Create a new WebClient instance.
                WebClient myWebClient = new WebClient();
                if (Extra_headers.Count > 0) { myWebClient.Headers.Add(Extra_headers); }

                responseArray = myWebClient.UploadValues(uriString, send_meth, Inputs);

                if (responseArray != null) 
                {
                    response = Encoding.UTF8.GetString(responseArray);
                   // succ = 1;
                }
                return response;
            }

            public string GetFormResponse(string uriString, string send_meth)
            {
                string response = "";
                WebRequest request = WebRequest.Create(uriString);
                request.Method = send_meth;
                if (Extra_headers.Count > 0) { request.Headers.Add(Extra_headers); }
                using (WebResponse resp = request.GetResponse())
                    {
                        using (Stream stream = resp.GetResponseStream())
                        {
                            TextReader reader = new StreamReader(stream);
                            response = reader.ReadToEnd();
                        }
                    }

              
                return response;
            }

    }
}