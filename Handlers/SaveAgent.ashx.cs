using Ipong.InterSwitch.PayDirect.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for SaveAgent
    /// </summary>
    public class SaveAgent : IHttpHandler
    {
        Hasher hash = new Hasher();

        public void ProcessRequest(HttpContext context)
        {
            string new_hash = "";
            string ccode = ConfigurationManager.AppSettings["ccode"]; string xcode = ConfigurationManager.AppSettings["xcode"];

            JavaScriptSerializer ser = new JavaScriptSerializer();
            var pp = context.Request["vv"];
            
            Register dd = ser.Deserialize<Register>(pp);
            
            dd.reg_date = DateTime.Now.ToShortDateString();

            dd.password = hash.GetGetSHA512String(ccode + dd.password + xcode);

            if (context.Request.Files.Count > 0)
            {
                var files = new List<string>();



                // interate the files and save on the server
                foreach (string file in context.Request.Files)
                {
                    if (file == "FileUpload")
                    {

                        var postedFile = context.Request.Files[file];
                        var vfile = postedFile.FileName.Replace("\"", string.Empty).Replace("'", string.Empty);
                        vfile = Stp(vfile);
                        string FileName = context.Server.MapPath("~/admin/ag_docz/" + vfile);
                     //   dd.cac_file = "/images/" + vfile;

                        dd.cac_file = "admin/ag_docz/" + vfile;

                        postedFile.SaveAs(FileName);



                    }

                    if (file == "FileUpload2")
                    {

                        var postedFile = context.Request.Files[file];
                        var vfile = postedFile.FileName.Replace("\"", string.Empty).Replace("'", string.Empty);
                        vfile = Stp(vfile);
                        string FileName = context.Server.MapPath("~/admin/ag_docz/" + vfile);
                        
                      //  dd.Letter_Intro_file = "/images/" + vfile;
                        dd.Letter_Intro_file = "admin/ag_docz/" + vfile;

                        postedFile.SaveAs(FileName);

                    }

                    if (file == "FileUpload3")
                    {

                        var postedFile = context.Request.Files[file];
                        var vfile = postedFile.FileName.Replace("\"", string.Empty).Replace("'", string.Empty);
                        vfile = Stp(vfile);
                        string FileName = context.Server.MapPath("~/admin/ag_docz/" + vfile);
                     //   dd.passport_file = "/images/" + vfile;
                        dd.passport_file = "admin/ag_docz/" + vfile;

                        postedFile.SaveAs(FileName);

                    }
                    //  dd.File_path = "/Images/Patient/" + vfile;

                     


                }
            }
            GetData gg = new GetData();
            string sp = gg.addAgent(dd);

            Ipong.Classes.Retriever kp = new Ipong.Classes.Retriever();

            
            
            sendemail(dd.Email, dd.CompName,sp);
            try
            {
                kp.updateRegistrationSysID4(sp, "PENDING");
            }
            catch (Exception ee)
            {

            }
            context.Response.ContentType = "application/json";
            context.Response.Write(ser.Serialize(sp));
        }

        public string Stp(string s)
        {
            var sb = new StringBuilder();
            foreach (char c in s)
            {
                if (!char.IsWhiteSpace(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }


        public void sendemail(string vemail, string vcompany,string vid)
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

                mail.Subject = "Validate your Account";

                mail.IsBodyHtml = true;
                String ss2 = "Dear " + vcompany + ",<br/> <br/>"  + " Thank you for completing the accreditation form for Agents of Trademarks, Patents and Designs Registry.<br/>";
                ss2 = ss2 + "To gain access to your account, you would need to click here <a href=\"http://88.150.164.30/IpoNigeria/#/Register/" + vid + " \">click</a>   to validate your account and also make payment. " + "<br/><br/><br/>";

                ss2 = ss2 + "Please keep your password safe and do not share your log in details with anyone. You may change your password at your convenience. In the event that you cannot remember your password, kindly follow the instructions provided for password recovery.<br/><br/>";

                ss2 = ss2 + "Please do not reply this mail. <br/><br/><br/>";
                ss2 = ss2 + " Live 24/7 Support: (+234) 09038979681  <br/><br/>";

                ss2 = ss2 + "Email: info@iponigeria.com or go online to use our live feedback form. <br/><br/>";

                ss2 = ss2 + "<b>Disclaimer: </b> This e-mail and any attachments are confidential; it must not be read, copied, disclosed or used by any person other than the above named addressees. Unauthorised use, disclosure or copying is strictly prohibited and may be unlawful. Iponigeria.com disclaims any liability for any action taken in reliance on the content of this e-mail. The comments or statements expressed in this e-mail could be personal opinions and are not necessarily those of iponigeria.com. If you have received this email in error or think you may have done so, you may not peruse, use, disseminate, distribute or copy this message. Please notify the sender immediately and delete the original e-mail from your system.";






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