using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace Ipong.B
{
    public partial class m_status : System.Web.UI.Page
    {
        protected string state_row = "0"; protected string status_msg = ""; protected string xvisible = "1";
        protected string adminID = "0"; protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int show_inv = 0; protected double tot_amtx = 0; protected string paid_status_msg = "";
        protected string fullname = ""; protected string email = ""; protected string mobile = "";
        protected string transID = ""; protected int succ, update_twallxgt_succ = 0;
        public string cust_id = ""; protected string agentType = ""; public string coy_name = "";
        protected Classes.Validator val = new Classes.Validator();
        protected Classes.Registration reg = new Classes.Registration();
        protected Classes.Retriever ret = new Classes.Retriever();

        protected List<Classes.XObjs.Twallet> lt_twall = new List<Classes.XObjs.Twallet>();
        protected List<Classes.XObjs.Fee_details> lt_fdets = new List<Classes.XObjs.Fee_details>();

        protected Classes.XObjs.Pwallet c_pwall = new Classes.XObjs.Pwallet();
        protected Classes.XObjs.XMember c_xmem = new Classes.XObjs.XMember();
        protected Classes.XObjs.XBanker c_xbank = new Classes.XObjs.XBanker();
        protected Classes.XObjs.XAgent c_xagt = new Classes.XObjs.XAgent();
        protected Classes.XObjs.Address c_addy = new Classes.XObjs.Address();

        protected Classes.XObjs.Registration c_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Registration c_sub_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Subagent c_sub = new Classes.XObjs.Subagent();

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            { this.adminID = Session["pwalletID"].ToString(); }
            else
            { base.Response.Redirect("../login.aspx"); }

            if(!IsPostBack)
            {
                Session["transID"] = null; Session["memberID"] = null; Session["agentType"] = null;
            }
            if (rblagentType.SelectedValue != "")
            {
                Session["agentType"] = rblagentType.SelectedValue; 
            }
            else
            {
                if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
                {
                    if (Session["agentType"].ToString() == "Agent") { rblagentType.SelectedIndex = 0; } else { rblagentType.SelectedIndex = 1; }
                }
            }
        }
     
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if ((xtrans.Text == "") || (xmob.Text == "")|| (val.IsValidMobile(xmob.Text)>0) )
            {
                if (xtrans.Text == "") { xtrans.BorderColor = System.Drawing.Color.Red; } else { xtrans.BorderColor = System.Drawing.Color.Green; }
                if (xmob.Text == "") { xmob.BorderColor = System.Drawing.Color.Red; } else { xmob.BorderColor = System.Drawing.Color.Green; }
                Response.Write("<script language=JavaScript  type='text/javascript'>alert('PLEASE BE SURE TO FILL IN ALL THE ENTRIES MARKED WITH A RED STAR!!'); </script>");
            }
            else if ((xtrans.Text != "") && (xmob.Text != "") && (val.IsValidMobile(xmob.Text) == 0))
            {
                if ((Session["agentType"] != null) && (Session["agentType"].ToString() != "")) { agentType = Session["agentType"].ToString(); }
                if (agentType == "Agent")
                {
                    c_reg = ret.getRegistrationByPhoneNumber(xmob.Text); Session["c_reg"] = c_reg; Session["RegID"] = c_reg.xid;
                    fullname = c_reg.Firstname + " " + c_reg.Surname; coy_name = c_reg.CompanyName; cust_id = c_reg.Sys_ID;
                    email = c_reg.Email; mobile = c_reg.PhoneNumber;
                    Session["fullname"] = fullname;
                    Session["email"] = email;
                    Session["mobile"] = mobile;
                    Session["c_addy"] = c_reg.CompanyAddress;
                }
                else
                {
                    if (Session["c_sub"] != null)
                    {
                        c_sub = ret.getSubAgentByPhoneNumber(xmob.Text); Session["c_sub"] = c_sub; Session["RegID"] = c_sub.xid;
                        fullname = c_sub.Firstname + " " + c_sub.Surname; Session["fullname"] = fullname;
                        email = c_sub.Email; Session["email"] = email;
                        mobile = c_sub.Telephone; Session["mobile"] = mobile;
                    }
                    if (Session["c_sub_reg"] != null)
                    {
                        c_sub_reg = ret.getRegistrationBySubagentRegistrationID(c_sub.RegistrationID); Session["c_sub_reg"] = c_sub_reg;
                        coy_name = c_sub_reg.CompanyName;
                        cust_id = c_sub_reg.Sys_ID + "_" + c_sub.AssignID;
                        Session["fullname"] = fullname;
                        Session["email"] = email;
                        Session["mobile"] = mobile;
                        Session["c_addy"] = c_sub_reg.CompanyAddress;
                    }
                }
                string RegID = "";
                if ((Session["RegID"] != null) && (Session["RegID"].ToString() != "")) { RegID = Session["RegID"].ToString(); }
                if ((RegID != null)&&(RegID!=""))
                {
                    lt_twall = ret.getValidatedTwalletByMemberID(RegID, xtrans.Text);
                    if (lt_twall.Count > 0)
                    {
                        paid_status_msg = lt_twall[0].xpay_status;
                        if (paid_status_msg == "1") { paid_status_msg = "PAID"; btnValidate.Visible = false; } else { paid_status_msg = "NOT PAID"; btnValidate.Visible = true; }
                        lt_fdets = ret.getFee_detailsByTwalletID(lt_twall[0].xid);
                       
                        Session["transID"] = xtrans.Text; Session["memberID"] = RegID;
                        show_inv = 1;
                    }
                    else { status_msg="COULD NOT FIND THE TRANSACTION ON THE SYSTEM!!"; }
                }
                
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            show_inv = 0;
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {           
            if( (Session["transID"]!=null)&&(Session["memberID"]!=null))
            {               
                    succ = reg.updateTwalletBanker(Session["transID"].ToString(), adminID, Session["memberID"].ToString());
                    if (succ > 0)
                    {
                        sendAlert(); show_inv = 0;
                        status_msg = "TRANSACTION :" + Session["transID"].ToString().ToUpper() + "HAS BEEN SUCCESSFULLY UPDATED!!";
                    }
            }
        }

        protected void sendAlert()
        {
            fullname = Session["fullname"].ToString(); email=Session["email"].ToString();
            mobile = Session["mobile"].ToString();
            Classes.Email em = new Classes.Email(); Classes.Messenger mess = new Classes.Messenger();
            string msg = "Dear " + fullname + ",<br/>";
            msg += "Transaction : " + Session["transID"].ToString().ToUpper() + " has been successfully validated!<br/>";
            msg += "You may now use you items from your profile page.<br/>Regards";

            string xmsg = "Dear " + fullname + ",\r\n";
            xmsg += "Transaction : " + Session["transID"].ToString().ToUpper() + " has been successfully validated!\r\nYou may now use you items from your profile page.\r\nRegards";

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