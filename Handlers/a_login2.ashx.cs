using Ipong.Classes;
using Ipong.InterSwitch.PayDirect.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;


namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for a_login2
    /// </summary>
    public class a_login2 : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public string coy_code = "";
        public string xcode = "";
        public string new_hash = "";

        XObjs.Registration xagent = new XObjs.Registration();
        XObjs.Subagent xsub_agent = new XObjs.Subagent();
        Registration reg = new Registration();
        Retriever ret = new Retriever();
        Hasher hash = new Hasher();


        public string email = ""; public string xpass = "";
        public string isagent = "";
        public string json2 = "";
        public int succ = 0;
        public JavaScriptSerializer js = new JavaScriptSerializer();
        public string ccode = "";
        public string x_code = "";

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            ccode = ConfigurationManager.AppSettings["ccode"];
            x_code = ConfigurationManager.AppSettings["xcode"];

            JavaScriptSerializer ser = new JavaScriptSerializer();
            var pp = context.Request["vv"];

            Logins dd = ser.Deserialize<Logins>(pp);

            if (dd.request == "vlogin")
            {
                try
                {
                    if (
                        (dd.email != null) && (dd.xpass != "")

                        )
                    {
                        email = dd.email;
                        xpass = dd.xpass;


                        try
                        {
                            // context.Session["agentType"] = "isagent";



                            new_hash = hash.GetGetSHA512String(ccode + xpass + x_code);
                            // xagent = ret.getRegistrationByLogin(email, xpass);

                            xagent = ret.getRegistrationByLogin(email, new_hash);
                            if ((xagent.xid != null) && (xagent.xid != ""))
                            {
                                //context.Session["MemberInfo"] = mi;
                                UserSession.ID = "agentID";
                               
                                context.Session["agentID"] = xagent.xid;
                                HttpContext.Current.Session["agentID2"] = xagent.xid;
                               
                                context.Session["agentEmail"] = email;
                                context.Session["agentPin"] = xpass;
                               
                                 json2 = js.Serialize(xagent);
                               // json2 = js.Serialize("true");
                                
                                context.Response.ContentType = "application/json";
                                // json = "{\"msg\":" + json + "}";
                                context.Response.Write(json2);
                            }
                            else
                            {
                                json2 = js.Serialize("false");
                                //  json = "{\"msg\":" + json + "}";
                                context.Response.ContentType = "application/json";
                                context.Response.Write(json2);
                            }




                        }
                        catch (Exception ex)
                        {
                            string ex1 = ex.ToString();
                            json2 = js.Serialize("Could not complete the process at this time!");
                            context.Response.ContentType = "application/json";
                            // json = "{\"msg\":" + json + "}";
                            context.Response.Write(json2);
                        }
                    }


                }
                catch (Exception ex)
                {
                    string ex1 = ex.ToString();
                    json2 = js.Serialize("Could not complete the process at this time!");
                    context.Response.ContentType = "application/json";
                    //  json = "{\"msg\":" + json + "}";
                    context.Response.Write(json2);
                }

            }

            else if (dd.request == "vlogin3")
            {
                string newpass = "111111";
                new_hash = hash.GetGetSHA512String(ccode + newpass + x_code);
                string xid = ret.getAgentLogDetails2(dd.email);
                if (xid != null || xid != "")
                {

                
                int vnum = ret.updateRegistrationSysID3(xid, new_hash);
                if (vnum > 0)
                {



                    xagent = ret.getRegistrationByLogin(dd.email, new_hash);

                       
                      

                        sendemail(dd.email, xagent.Surname);
                    json2 = js.Serialize(vnum);
                    //  json = "{\"msg\":" + json + "}";
                    context.Response.ContentType = "application/json";
                    context.Response.Write(json2);

                }
                else
                {
                    json2 = js.Serialize("false");
                    //  json = "{\"msg\":" + json + "}";
                    context.Response.ContentType = "application/json";
                    context.Response.Write(json2);

                }
                }
                else
                {
                    json2 = js.Serialize("false");
                    //  json = "{\"msg\":" + json + "}";
                    context.Response.ContentType = "application/json";
                    context.Response.Write(json2);
                }
            }
            else
            {
                string vgent = UserSession.ID;
                if (HttpContext.Current.Session["agentID2"] != null)
                {
                    

                    string json2 = js.Serialize("true");
                    context.Response.ContentType = "application/json";
                    // json = "{\"msg\":" + json + "}";
                    context.Response.Write(json2);
                }
                else
                {


                    string json2 = js.Serialize("false");
                    context.Response.ContentType = "application/json";
                    // json = "{\"msg\":" + json + "}";
                    context.Response.Write(json2);
                }

            }
           
        }

        public void sendemail(string vemail, string vcompany)
        {
            try
            {
                int port = 0x24b;


                MailMessage mail = new MailMessage();
                mail.From =
           new MailAddress("noreply@iponigeria.com", "noreply@iponigeria.com");
                // new MailAddress("tradeservices@fsdhgroup.com");
                mail.Priority = MailPriority.High;

                mail.To.Add(
    new MailAddress(vemail));

                //    new MailAddress("ozotony@yahoo.com"));



                //mail.CC.Add(new MailAddress("Anthony.Ozoagu@firstcitygroup.com"));

                mail.Subject = "Account Reset";

                mail.IsBodyHtml = true;
                String ss2 = "Dear " + vcompany + ",<br/> <br/>" + " Your Password has been reset to 111111 <br/>";

                ss2 = ss2 + "You may log into your account now and change your password. <br/>";

                ss2 = ss2 + "Please keep your password safe and do not share your log in details with anyone. You may change your password at your convenience. In the event that you cannot remember your password, kindly follow the instructions provided for password recovery. <br/>";
              
                ss2 = ss2 + "Please do not reply this mail <br/>";
                ss2 = ss2 + " Live 24/7 Support: +234 (0)9038979681  <br/><br/>";

                ss2 = ss2 + "Email: info@iponigeria.com or go online to use our live feedback form <br/><br/>";

                ss2 = ss2 + "Disclaimer: This e-mail and any attachments are confidential; it must not be read, copied, disclosed or used by any person other than the above named addressees. Unauthorised use, disclosure or copying is strictly prohibited and may be unlawful. Iponigeria.com disclaims any liability for any action taken in reliance on the content of this e-mail. The comments or statements expressed in this e-mail could be personal opinions and are not necessarily those of iponigeria.com. If you have received this email in error or think you may have done so, you may not peruse, use, disseminate, distribute or copy this message. Please notify the sender immediately and delete the original e-mail from your system";






                //ss2 = ss2 + "Please keep your password safe and do not share your log in details with anyone. You may change your password at your convenience. In the event that you cannot remember your password, kindly follow the instructions provided for password recovery."  + "<br/>";
                //ss2 = ss2 + "Please do not reply this mail" +  "<br/><br/>";
                //ss2 = ss2 + "Email: info@iponigeria.com or go online to use our live feedback form .<br/><br/>";

                String ss = "<html> <head> </head> <body>" + ss2 + "</body> </html>";

                //  mail.Body = ss;

                mail.Body = ss;

                SmtpClient client = new SmtpClient("88.150.164.30");
                //  SmtpClient client = new SmtpClient("192.168.0.12");

                client.Port = port;

                //    client.Credentials = new System.Net.NetworkCredential("paymentsupport@einaosolutions.com", "Zues.4102.Hector");

                client.Credentials = new System.Net.NetworkCredential("noreply@iponigeria.com", "Einao2015@@$");



                //   new System.Net.NetworkCredential("ebusiness@firstcitygroup.com", "welcome@123");
                //   new System.Net.NetworkCredential(q60.smtp_user, q60.smtp_password);







                client.Send(mail);

            }
            catch (Exception ee)
            {


            }



        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}