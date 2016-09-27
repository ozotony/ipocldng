using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.IO;
using Ipong.Classes;

namespace Ipong.A
{
    using Ipong.ExcelClasses;
    public partial class xmail : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected string check_trans_page = "";
        protected string coy_name = "";
        protected string cust_id = "";
        protected string email = "";
        protected string fullname = "";
        protected string log_date = "";
        protected string mobile = "";
        protected string ref_no = "";
        protected Ipong.Classes.Registration reg = new Ipong.Classes.Registration();
        protected string transID = "";
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        public int xsucc;

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.xsucc = 0;
            if (this.txt_msg.Text != "")
            {
                this.Session["msg"] = this.txt_msg.Text;
                this.sendMsg();
            }
            else
            {
                this.xsucc = 2;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((this.Session["pwalletID"] != null) && (this.Session["pwalletID"].ToString() != ""))
            {
                this.adminID = this.Session["pwalletID"].ToString();
            }
            else
            {
                base.Response.Redirect("../a_login.aspx");
            }
            if ((this.Session["log_date"] != null) && (this.Session["log_date"].ToString() != ""))
            {
                this.log_date = this.Session["log_date"].ToString();
            }
            if ((this.Session["agentType"] != null) && (this.Session["agentType"].ToString() != ""))
            {
                this.agentType = this.Session["agentType"].ToString();
                if (this.agentType == "Agent")
                {
                    if (this.Session["c_reg"] != null)
                    {
                        this.c_reg = (XObjs.Registration)this.Session["c_reg"];
                        this.fullname = this.c_reg.Firstname + " " + this.c_reg.Surname;
                        this.coy_name = this.c_reg.CompanyName;
                        this.cust_id = this.c_reg.Sys_ID;
                        this.email = this.c_reg.Email;
                        this.mobile = this.c_reg.PhoneNumber;
                        this.Session["coy_name"] = this.coy_name;
                        this.Session["fullname"] = this.fullname;
                        this.Session["email"] = this.email;
                        this.Session["mobile"] = this.mobile;
                        this.Session["c_addy"] = this.c_reg.CompanyAddress;
                    }
                }
                else
                {
                    XObjs.Registration registration = new XObjs.Registration();
                    if (this.Session["c_sub"] != null)
                    {
                        this.c_sub = (XObjs.Subagent)this.Session["c_sub"];
                        this.fullname = this.c_sub.Firstname + " " + this.c_sub.Surname;
                        this.email = this.c_sub.Email;
                        this.mobile = this.c_sub.Telephone;
                    }
                    if (this.Session["c_sub_reg"] != null)
                    {
                        registration = (XObjs.Registration)this.Session["c_sub_reg"];
                        this.coy_name = registration.CompanyName;
                        this.cust_id = registration.Sys_ID + "_" + this.c_sub.AssignID;
                    }
                    this.Session["coy_name"] = this.coy_name;
                    this.Session["fullname"] = this.fullname;
                    this.Session["email"] = this.email;
                    this.Session["mobile"] = this.mobile;
                    this.Session["c_addy"] = registration.CompanyAddress;
                }
            }
        }

        protected void sendMsg()
        {
            if (this.Session["fullname"] != null)
            {
                this.fullname = this.Session["fullname"].ToString();
            }
            if (this.Session["email"] != null)
            {
                this.email = this.Session["email"].ToString();
            }
            Email email = new Email();
            new Messenger();
            if (this.Session["msg"] != null)
            {
                string subject = this.rbl_mail_cat.SelectedValue + " From: " + this.fullname;
                string from = this.email;
                string to = "paymentsupport@einaosolutions.com";
                if (email.sendMail(this.rbl_mail_cat.SelectedValue, to, from, subject, this.Session["msg"].ToString(), "") != "bad")
                {
                    this.txt_msg.Text = "";
                    this.xsucc = 1;
                }
            }
        }
    }
}