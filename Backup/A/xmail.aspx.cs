using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Ipong.A
{
    using Ipong.ExcelClasses;
    public partial class xmail : System.Web.UI.Page
    {
        protected string adminID = "0"; protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd"); protected string agentType = "";
        protected string fullname = ""; protected string email = ""; protected string mobile = "";public int xsucc=0;
        protected string transID = ""; protected string ref_no = ""; protected string check_trans_page = "";
        protected string coy_name = ""; protected string cust_id = "";  protected string log_date = ""; 
      
        //protected Classes.Retriever ret = new Classes.Retriever();
        protected Classes.Registration reg = new Classes.Registration();

        protected Classes.XObjs.Registration c_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Registration c_sub_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Subagent c_sub = new Classes.XObjs.Subagent();
        
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            { this.adminID = Session["pwalletID"].ToString(); }
            else
            { base.Response.Redirect("../a_login.aspx"); }

            if ((Session["log_date"] != null) && (Session["log_date"].ToString() != "")) { this.log_date = Session["log_date"].ToString(); }   

            if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
            {
                agentType = Session["agentType"].ToString();

                if (agentType == "Agent")
                {
                    if (Session["c_reg"] != null)
                    {
                        c_reg = (Classes.XObjs.Registration)Session["c_reg"];
                        fullname = c_reg.Firstname + " " + c_reg.Surname;
                        coy_name = c_reg.CompanyName;
                        cust_id = c_reg.Sys_ID;
                        email = c_reg.Email;
                        mobile = c_reg.PhoneNumber;
                        Session["coy_name"] = coy_name;
                        Session["fullname"] = fullname;
                        Session["email"] = email;
                        Session["mobile"] = mobile;
                        Session["c_addy"] = c_reg.CompanyAddress;
                    }
                }
                else
                {
                    Classes.XObjs.Registration c_sub_reg = new Classes.XObjs.Registration();

                    if (Session["c_sub"] != null)
                    {
                        c_sub = (Classes.XObjs.Subagent)Session["c_sub"];
                        fullname = c_sub.Firstname + " " + c_sub.Surname;
                        email = c_sub.Email;
                        mobile = c_sub.Telephone;
                    }
                    if (Session["c_sub_reg"] != null)
                    {
                        c_sub_reg = (Classes.XObjs.Registration)Session["c_sub_reg"];
                        coy_name = c_sub_reg.CompanyName;
                        cust_id = c_sub_reg.Sys_ID + "_" + c_sub.AssignID;
                    }
                    Session["coy_name"] = coy_name;
                    Session["fullname"] = fullname; Session["email"] = email;
                    Session["mobile"] = mobile;
                    Session["c_addy"] = c_sub_reg.CompanyAddress;
                }
            }
          
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            xsucc = 0;
            if (txt_msg.Text != "")
            {
                Session["msg"] = txt_msg.Text;
                sendMsg();
            }
            else { xsucc = 2; }
          
        }

        protected void sendMsg()
        {
            if (Session["fullname"] != null) { fullname = Session["fullname"].ToString(); }
            if (Session["email"] != null) { email = Session["email"].ToString(); }
            Classes.Email em = new Classes.Email(); Classes.Messenger mess = new Classes.Messenger();

            if (Session["msg"] != null)
            {
            string sub = rbl_mail_cat.SelectedValue+" From: "+fullname;
            string f_email = email;
            string to_mail = "anthony@3nitix.com";
           // string to_mail = rbl_mail_cat.SelectedValue.ToLower() + "@cldng.com";
            string str=em.sendMail(rbl_mail_cat.SelectedValue, to_mail, f_email, sub, Session["msg"].ToString(), "");
            if (str != "bad")
            {
                txt_msg.Text = ""; xsucc = 1; 
            }

            }
        }

    }
}