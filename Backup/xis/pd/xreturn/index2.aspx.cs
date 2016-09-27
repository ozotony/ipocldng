using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Configuration;


namespace Ipong.xis.pd.xreturn
{
    public partial class index2 : System.Web.UI.Page
    {
        int succ, write_succ = 0;

        protected string txnref = ""; protected string payRef = ""; protected string retRef = "";
        protected string check_trans_page = "";
        protected string cardNum = "0"; protected string apprAmt = "0"; public StringBuilder xstring; public string docpath = "";

        public string mackey = ""; public string product_id = ""; public string amt = "0"; public string inputString = "";
        public string hash = ""; public string response = ""; public string resp = ""; public string desc = "";
        protected string adminID = "0"; public string fullname = ""; public string email = ""; public string mobile = ""; public string err_desc = "";

        public Classes.XWriters x = new Classes.XWriters();
        protected InterSwitch.PayDirect.Classes.Transactions tx = new InterSwitch.PayDirect.Classes.Transactions();
        protected InterSwitch.PayDirect.Classes.Hasher hash_value = new InterSwitch.PayDirect.Classes.Hasher();
        protected InterSwitch.PayDirect.Classes.ErrorHandler eh = new InterSwitch.PayDirect.Classes.ErrorHandler();
        protected Classes.XObjs.InterSwitchPostFields isw_fields = new Classes.XObjs.InterSwitchPostFields();
        protected Ipong.Classes.XObjs.InterSwitchResponse isr = new Ipong.Classes.XObjs.InterSwitchResponse();
        protected Classes.Registration reg = new Classes.Registration();
        protected Classes.Retriever ret = new Classes.Retriever();


        protected void Page_Load(object sender, EventArgs e)
        {
             xstring = new StringBuilder();
            if(Request.Headers["Hash"]!=null)
            {
           hash=Request.Headers["Hash"].ToString();
           }
            xstring.AppendLine("Hash: " + hash);



            docpath = base.Server.MapPath("~/") + "InterLogs/GetHash/GetHash.txt";
                succ = x.WriteToFile(xstring.ToString(), docpath);
           
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../../A/profile.aspx");
        }

        protected void sendAlert()
        {
            if (Session["fullname"] != null) { fullname = Session["fullname"].ToString(); }
            if (Session["email"] != null) { email = Session["email"].ToString(); }
            if (Session["mobile"] != null) { mobile = Session["mobile"].ToString(); }
            Classes.Email em = new Classes.Email(); Classes.Messenger mess = new Classes.Messenger();
            string msg = "Dear " + fullname + ",<br/>";
            string xmsg = "Dear " + fullname + ",\r\n";

            if (Session["Refno"] != null)
            {
                if (isr.ResponseCode == "00")
                {
                    msg += "Your payment transaction has been successfully completed!<br/>";
                    msg += "Reason: " + isr.ResponseDescription + "<br />";
                    msg += "Transaction Reference: " + Session["Refno"].ToString().ToUpper() + "<br/>";
                    msg += "Payment Reference :" + payRef + "<br/>";
                    msg += "Please check your \"Payment Status\" or \"History Log\" to view more details!!<br/><br/>Regards";

                    xmsg += "Your payment transaction has been successfully completed\r\nReason: " + isr.ResponseDescription + "\r\nTransaction Reference: " + Session["Refno"].ToString().ToUpper() + " \r\nPayment Reference :" + payRef + "\r\nPlease check your 'Payment Status' or 'History Log' to view more details\r\nRegards";
                }
                else
                {
                    msg += "Your payment transaction was not successfull!<br/>";
                    msg += "Transaction Reference: " + Session["Refno"].ToString().ToUpper() + "<br/>";
                    msg += "Payment Reference :" + payRef + "<br/>";
                    msg += "Please check your \"Payment Status\" or \"History Log\" to view more details!!<br/><br/>Regards";

                    xmsg += "Your payment transaction was not successfull\r\nTransaction Reference: " + Session["Refno"].ToString().ToUpper() + " \r\nPayment Reference :" + payRef + "\r\nPlease check your 'Payment Status' or 'History Log' to view more details\r\nRegards";
                }
            }

            string sub = "XPAY ALERT";
            string f_email = "admin@xpay.com";
            string to_mail = email;
            string to_mobile = mobile;

            xmsg = Server.UrlEncode(xmsg);
            if (to_mobile.StartsWith("0")) { to_mobile = "234" + to_mobile.Remove(0, 1); }

            em.sendMail("XPAY ALERT", to_mail, f_email, sub, msg, "");
            string stat = mess.send_sms(xmsg, "XPAY ALERT", to_mobile);

        }
    }
}