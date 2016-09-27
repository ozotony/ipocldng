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
	public partial class index : System.Web.UI.Page
	{
        int succ,write_succ = 0;

        protected string txnref = ""; protected string payRef = ""; protected string retRef = "";
        //protected string txnref = "21D777A9D233"; protected string payRef = "FBN|WEB|Einao|12-09-2013|029995"; protected string retRef = "000011105149";
        protected string check_trans_page = ""; protected string xpay_status = "0"; public String[] pending_code_list;
        protected string cardNum = "0"; protected string apprAmt = "0";  public StringBuilder xstring; public string docpath = "";

        public string mackey = ""; public string product_id = ""; public string amt = "0"; public string inputString = ""; 
        public string hash = ""; public string response = ""; public string resp = ""; public string desc = "";
        protected string adminID = "0"; public string fullname = ""; public string email = ""; public string mobile = ""; public string err_desc = "";

        public Classes.XWriters x = new Classes.XWriters(); 
        protected InterSwitch.PayDirect.Classes.Transactions tx = new InterSwitch.PayDirect.Classes.Transactions();
        protected InterSwitch.PayDirect.Classes.Hasher hash_value = new InterSwitch.PayDirect.Classes.Hasher();
        protected InterSwitch.PayDirect.Classes.ErrorHandler eh = new InterSwitch.PayDirect.Classes.ErrorHandler();
        protected Classes.XObjs.InterSwitchPostFields isw_fields=new Classes.XObjs.InterSwitchPostFields();
        protected Ipong.Classes.XObjs.InterSwitchResponse isr = new Ipong.Classes.XObjs.InterSwitchResponse();
        protected Classes.Registration reg = new Classes.Registration();
        protected Classes.Retriever ret = new Classes.Retriever();

        
		protected void Page_Load(object sender, EventArgs e)
		{
            //if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            //{ this.adminID = Session["pwalletID"].ToString(); }
            //else
            //{ base.Response.Redirect("../../../a_login.aspx"); }
          
            xstring = new StringBuilder();
            product_id = ConfigurationManager.AppSettings["pd_product_id"];
            mackey = ConfigurationManager.AppSettings["pd_mackey"];
            check_trans_page = ConfigurationManager.AppSettings["pd_get_trans_json_page"];           

            if ((Request.Form["txnRef"] != null) && (Request.Form["txnRef"] != "")) { txnref = Request.Form["txnRef"].ToString(); Session["transID"] = txnref; }
            if ((Request.Form["payRef"] != null) && (Request.Form["payRef"] != "")) { payRef = Request.Form["payRef"].ToString(); }
            if ((Request.Form["retRef"] != null) && (Request.Form["retRef"] != "")) { retRef = Request.Form["retRef"].ToString(); }
            if ((Request.Form["cardNum"] != null) && (Request.Form["cardNum"] != "")) { cardNum = Request.Form["cardNum"].ToString(); }
            if ((Request.Form["apprAmt"] != null) && (Request.Form["apprAmt"] != "")) { apprAmt = Request.Form["apprAmt"].ToString(); }
            if ((Request.Form["resp"] != null) && (Request.Form["resp"] != "")) { resp = Request.Form["resp"].ToString(); }
            if ((Request.Form["desc"] != null) && (Request.Form["desc"] != "")) { desc = Request.Form["desc"].ToString(); }

            if (!IsPostBack)
            {
                isw_fields = ret.getISWtransactionByTransactionID(txnref);
                xstring.AppendLine("Transaction reference= " + txnref + " Payment reference= " + payRef + " Switching Bank Reference number= " + retRef + " card No= " + cardNum + " apprAmt= " + apprAmt);

                inputString = product_id + txnref + mackey;
                //Get the hash value
                string get_trans_hash = hash_value.GetGetSHA512String(inputString);

                isr = tx.myRedirect(check_trans_page + "?productid=" + product_id + "&transactionreference=" + txnref + "&amount=" + isw_fields.amount, "Hash", get_trans_hash);
                  
                    
                 if ((isr.ResponseCode != "") && (isr.ResponseCode != null))
                {
                    err_desc = eh.getErrorDesc(isr.ResponseCode);
                    if ((err_desc != "") && (err_desc != null) && (err_desc != "NA"))
                    {
                        isr.ResponseDescription = err_desc;
                    }
                    xstring.AppendLine("Sent Amount: " + isw_fields.amount + "\r\n Product ID: " + product_id + "\r\n Hash: " + get_trans_hash + "\r\n Amount: " + isr.Amount + "\r\n CardNumber: " + isr.CardNumber + "\r\n MerchantReference: " + isr.MerchantReference
                     + "\r\n PaymentReference: " + isr.PaymentReference + "\r\n RetrievalReferenceNumber: " + isr.RetrievalReferenceNumber
                     + "\r\n LeadBankCbnCode: " + isr.LeadBankCbnCode + "\r\n TransactionDate: " + isr.TransactionDate
                     + "\r\n ResponseCode: " + isr.ResponseCode + "\r\n ResponseDescription: " + isr.ResponseDescription + "\r\n Json Page: " + check_trans_page
                     + "\r\n Form Response: " + resp + "\r\n Form Description: " + desc
                     );                   
                      
                    //Log the info in a file
                    if (txnref != "")
                    {
                        docpath = base.Server.MapPath("~/") + "InterLogs/" + txnref + ".txt";
                        succ = x.WriteToFile(xstring.ToString(), docpath);
                    }
                    else
                    {
                        docpath = base.Server.MapPath("~/") + "InterLogs/xxx.txt";
                        succ = x.WriteToFile(xstring.ToString(), docpath);
                    }
                    //Update the DB Record
                    
                    succ = reg.updateInterSwitchRecords(txnref, payRef, retRef, isr.ResponseCode, isr.TransactionDate, isr.MerchantReference,isr.ResponseDescription);                    
                     if (isr.ResponseCode == "00")
                    {
                        xpay_status = "1";//Successfull
                    }
                    else
                    {
                        xpay_status = "3";//Error
                    }
                    reg.updateTwalletPaymentStatus(txnref, xpay_status);
                    if (succ != 0)
                    {
                         sendAlert();                
                    }
                }
                else
                {
                    string rc = "None"; string rd = "None";                   

                    xstring.AppendLine("Sent Amount: " + isw_fields.amount + "\r\n Product ID: " + product_id + "\r\n Hash: " + get_trans_hash + "\r\n Amount: None\r\n CardNumber: None\r\n MerchantReference: None\r\n PaymentReference: None\r\n RetrievalReferenceNumber: None\r\n LeadBankCbnCode: None\r\n TransactionDate: None\r\n ResponseCode: " + rc + "\r\n ResponseDescription: " + rd + "\r\n Json Page: " + check_trans_page
                   + "\r\n Form Response: " + resp + "\r\n Form Description: " + desc
                   );

                    //Log the info in a file
                    if (txnref != "")
                    {
                        docpath = base.Server.MapPath("~/") + "InterLogs/" + txnref + ".txt";
                        succ = x.WriteToFile(xstring.ToString(), docpath);
                    }
                    else
                    {
                        docpath = base.Server.MapPath("~/") + "InterLogs/xxx.txt";
                        succ = x.WriteToFile(xstring.ToString(), docpath);
                    }
                    xpay_status = "3";//Error
                    reg.updateTwalletPaymentStatus(txnref, xpay_status);   
                    //No internet Connection at this time                    
                    if (desc == ""){isr.ResponseDescription = "Transaction Pending"; } else { isr.ResponseDescription = desc;}
                    if (resp == "") { isr.ResponseCode = "XXXX"; } else { isr.ResponseCode = resp; }
                    sendAlert();
                }
            }
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
                    msg += "Reason: "+isr.ResponseDescription+"<br />";
                    msg += "Transaction Reference: " + Session["Refno"].ToString().ToUpper() + "<br/>";
                    msg += "Payment Reference :"+payRef+"<br/>";
                    msg += "Please check your \"Payment Status\" or \"History Log\" to view more details!!<br/><br/>Regards";

                    xmsg += "Your payment transaction has been successfully completed\r\nReason: " + isr.ResponseDescription + "\r\nTransaction Reference: " + Session["Refno"].ToString().ToUpper() + " \r\nPayment Reference :" + payRef + "\r\nPlease check your 'Payment Status' or 'History Log' to view more details\r\nRegards";
                }
                else
                {
                    msg += "Your payment transaction was not successfull!<br/>";
                    msg += "Reason: " + isr.ResponseDescription + "<br />";
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