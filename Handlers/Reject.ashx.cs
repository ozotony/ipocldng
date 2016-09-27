using Ipong.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;

namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for Reject
    /// </summary>
    public class Reject : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var pp = context.Request["vid"];
            string message = "";
            String dd = "";
            string vid2 = Convert.ToString(pp);


            Ipong.Classes.Retriever kp = new Ipong.Classes.Retriever();

            Ipong.Classes.XObjs.Registration pp4 = kp.getRegistrationBySubagentRegistrationID(vid2);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            //  XObjs.Registration px = kp.getRegistrationBySubagentRegistrationID(vid2);
           
               


                kp.updateRegistrationSysID4(vid2, "REFUSED");

             
                sendemail(pp4);

                message = "success";

           




            context.Response.ContentType = "application/json";
            context.Response.Write(ser.Serialize(message));
        }

        public void sendemail(XObjs.Registration px2)
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
    new MailAddress(px2.Email));

                //    new MailAddress("ozotony@yahoo.com"));



                //mail.CC.Add(new MailAddress("Anthony.Ozoagu@firstcitygroup.com"));

                mail.Subject = "Agent Accreditation Request Rejected";

                mail.IsBodyHtml = true;
                String ss2 = "Dear " + px2.CompanyName + ",<br/> <br/>" + " This is to inform you that your application has been refused!! Please contact our personnel for further details .<br/> <br/>";

                //  ss2 = ss2 + "To gain access to your account, you would need to click here <a href=\"http://88.150.164.30/IpoTest2/#/Register/" + vid + " \">click</a>   to validate your account and also make payment. " + "<br/><br/><br/>";
                ss2 = ss2 + "Please do not reply this mail. <br/> <br/>";

                

                ss2 = ss2 + "Email: info@iponigeria.com or go online to use our live feedback form <br/> <br/>";



                ss2 = ss2 + "<b> Disclaimer: </b>This e-mail and any attachments are confidential; it must not be read, copied, disclosed or used by any person other than the above named addressees. Unauthorized use, disclosure or copying is strictly prohibited and may be unlawful. Iponigeria.com disclaims any liability for any action taken in reliance on the content of this e-mail. The comments or statements expressed in this e-mail could be personal opinions and are not necessarily  those of iponigeria.com. If you have received this email in error or think you may have done so, you may not peruse, use, disseminate, distribute or copy this message. Please notify the sender immediately and delete the original e-mail from your system.";










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