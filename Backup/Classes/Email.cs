namespace Ipong.Classes
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    public class Email
    {
        private MailMessage mail = new MailMessage();
        private string username = "templar.idigbe@gmail.com";
        private string passwd = "Doc2ore1976";
        private string hostname = "smtp.gmail.com";
        private int port = 587;

        public string sendMail(string userdisplayname, string to, string from, string subject, string msg, string path)
        {
            string str = "";
            SmtpClient client = new SmtpClient
            {
                Credentials = new NetworkCredential(username, passwd),
                Port = port,
                Host = hostname,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 20000
            };
            this.mail = new MailMessage();
            string[] strArray = to.Split(new char[] { ',' });
            try
            {
                this.mail.From = new MailAddress(from, userdisplayname, Encoding.UTF8);
                for (byte i = 0; i < strArray.Length; i = (byte)(i + 1))
                {
                    this.mail.To.Add(strArray[i]);
                }
                this.mail.Priority = MailPriority.High;
                this.mail.Subject = subject;
                this.mail.Body = msg;
                if (path != "")
                {
                    LinkedResource item = new LinkedResource(path)
                    {
                        ContentId = "Logo"
                    };
                    AlternateView view = AlternateView.CreateAlternateViewFromString("<html><body><table border=2><tr width=100%><td><img src=cid:Logo alt=companyname /></td><td>FROM BLUEFROST</td></tr></table><hr/></body></html>" + msg, null, "text/html");
                    view.LinkedResources.Add(item);
                    this.mail.AlternateViews.Add(view);
                    this.mail.IsBodyHtml = true;
                    this.mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    this.mail.ReplyTo = new MailAddress(to);
                    client.Send(this.mail);
                    return str;
                }
                if (path == "")
                {
                    this.mail.IsBodyHtml = true;
                    this.mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    this.mail.ReplyTo = new MailAddress(to);
                    client.Send(this.mail);
                    str = "good";
                }
            }
            catch (Exception exception)
            {
                if (exception.ToString() == "The operation has timed out")
                {
                    client.Send(this.mail);
                    str = "bad";
                }
            }
            return str;
        }
    }
}

