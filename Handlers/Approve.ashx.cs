

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
    /// Summary description for Approve
    /// </summary>
    public class Approve : IHttpHandler
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
            if (pp4.Sys_ID == null || pp4.Sys_ID == "")
            {

                int vmax = kp.getMaxSysId();
                vmax = vmax + 1;
                String vsys_id = "CLD/RA/0" + vmax;

              
                kp.updateRegistrationSysID(vid2, vsys_id);

                kp.addRoles(vsys_id, "Agent");
                kp.addRoles(vsys_id, "Payment");
               
                pp4.Sys_ID = vsys_id;
                sendemail(pp4);

                message = "success";

            }

            else
            {

                kp.addRoles(pp4.Sys_ID, "Agent");

                kp.addRoles(pp4.Sys_ID, "Payment");
                sendemail(pp4);
                message = "success";
            }


           

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

                mail.Subject = "Agent Accreditation Request Approved";

                mail.IsBodyHtml = true;
                String ss2 = "Dear " + px2.CompanyName + ",<br/> <br/>" + " This is to notify you that your application has been approved having satisfied the requirements for agent accreditation with the Trademarks, Patents and Designs Registry. <br/>";

              //  ss2 = ss2 + "To gain access to your account, you would need to click here <a href=\"http://88.150.164.30/IpoTest2/#/Register/" + vid + " \">click</a>   to validate your account and also make payment. " + "<br/><br/><br/>";
                ss2 = ss2 + "Your agent code and information provided are displayed below:. <br/><br/><br/>";
                ss2 = ss2 + " <table style=\"border:1px solid black;border-collapse:collapse; \"   >  <tr> <td style=\"border:1px solid black;\" > AGENT CODE </td> <td style=\"border:1px solid black;\" >" + px2.Sys_ID + "</td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" > FIRSTNAME </td> <td style=\"border:1px solid black;\" >" + px2.Firstname + "</td> </tr>";
                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" > SURNAME </td> <td style=\"border:1px solid black;\" >" + px2.Surname + "</td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" > EMAIL </td> <td style=\"border:1px solid black;\" >" + px2.Email + "</td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" > DATE OF BIRTH </td> <td style=\"border:1px solid black;\" >" + px2.DateOfBrith + "</td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" > COMPANY NAME </td> <td style=\"border:1px solid black;\" >" + px2.CompanyName + "</td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" > COMPANY ADDRESS </td> <td style=\"border:1px solid black;\" >" + px2.CompanyAddress + "</td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" > CERTIFICATE </td> <td style=\"border:1px solid black;\" ><a href=\"http://ipo.cldng.com/"+  px2.Certificate + " \">Download</a> </td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" >  LETTER OF INTRODUCTION </td> <td style=\"border:1px solid black;\" ><a href=\"http://ipo.cldng.com/" + px2.Introduction + " \">Download</a> </td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" >  PASSPORT </td> <td style=\"border:1px solid black;\" ><a href=\"http://ipo.cldng.com/" + px2.logo + " \">Download</a> </td> </tr>";

            

         

                ss2 = ss2 + "</table> <br/><br/>";

                ss2 = ss2 + "The online services are designed to make your Intellectual property filing experience worthwhile. <br/>";

                ss2 = ss2 + "The following online services are available for your use:";


                ss2 = ss2 + "<ol> <br/>";

                ss2 = ss2 + "<li>Electronic payment using Interswitch powered debit cards</li>";
                ss2 = ss2 + "<li>Electronic filing of Trademarks, Patents and Designs and other related services</li>";
                ss2 = ss2 + "<li>The track and trace module to enable you follow-up on your applications</li>";
                ss2 = ss2 + "<li>Reporting tools</li>";
                ss2 = ss2 + "<li>Customer support system</li>";
                ss2 = ss2 + "</ol> <br/>";

                ss2 = ss2 + "Please keep your password safe and do not share your log in details with anyone. You may change your password at your convenience. In the event that you cannot remember your password, kindly follow the instructions provided for password recovery.";

                ss2 = ss2 + "Please do not reply this mail <br/> <br/>";

                ss2 = ss2 + "Live 24/7 Support: (+234) 09038979681 <br/>";

                ss2 = ss2 + "info@iponigeria.com or go online to use our live feedback form <br/><br/> ";

                ss2 = ss2 + "This e-mail and any attachments are confidential; it must not be read, copied, disclosed or used by any person other than the above named addressees. Unauthorized use, disclosure or copying is strictly prohibited and may be unlawful. Iponigeria.com disclaims any liability for any action taken in reliance on the content of this e-mail. The comments or statements expressed in this e-mail could be personal opinions and are not necessarily those of iponigeria.com. If you have received this email in error or think you may have done so, you may not peruse, use, disseminate, distribute or copy this message. Please notify the sender immediately and delete the original e-mail from your system.";



                       






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