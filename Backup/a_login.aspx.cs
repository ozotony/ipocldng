﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

namespace Ipong
{
    using Classes;
    public partial class a_login : System.Web.UI.Page
    {
        public string adminID = "0";
        public string code_text = "";
        public string email_text = "";
        public string enable_Captcha = "0";
        public string enable_Confirm = "0";
        public string enable_Save = "1";
        public string newp = "0";
        public string newState = "0";
        public string agentState = "0";
        public string password_text = "";
        public static string server_url = "";
        public static string remote_host = "";
        public static string remote_user = "";
        public static string server_name = "";
        public static string OriginalIP = "";
        public static string RemoteIP = "";
        public string ccode = "";
        public string x_code = "";
        public string new_hash = "";

        public zues z = new zues();
        public Classes.Retriever ret = new Classes.Retriever();
        protected Classes.XObjs.Registration c_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Registration c_sub_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Subagent c_sub = new Classes.XObjs.Subagent();
        protected InterSwitch.PayDirect.Classes.Hasher hash = new InterSwitch.PayDirect.Classes.Hasher();

        protected void ConfirmDetails_Click(object sender, EventArgs e)
        {
            int num = 0;
            if (this.xemail.Text == "")
            {
                this.email_text = "1";
                num++;
            }
            if (this.xcode.Text == "")
            {
                this.code_text = "1";
                num++;
            }
            if (num != 0)
            {
                base.Response.Write("<script language=JavaScript>alert('Please fill in the marked fileds')</script>");
            }
            else
            {
                this.doCaptcha();
            }
        }

        protected void doCaptcha()
        {
            string str = "";
            if (Session["Captcha"] != null)
            {
                str = Session["Captcha"].ToString();
            }
            if (str == this.xcode.Text.ToUpper())
            {
                this.newState = "0";
            }
            else
            {
                this.newState = "1";
                this.xcode.Text = "";
            }

            if (rblagentType.SelectedValue != "")
            {
                this.agentState = "0";   
            }
            else
            {
                this.agentState = "1";
            }

            if ((rblagentType.SelectedValue != "") && (str == this.xcode.Text.ToUpper()))
            {
                xpassword.Focus();
                this.enable_Save = "0";
                this.enable_Confirm = "1";
                this.enable_Captcha = "1";
                this.newp = "1";
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["log_date"] = null;
            string ClientIP = Ipong.login.GetClientIPAddress(Context.Request);
            if (!IsPostBack)
            {
                Session["agentType"] = null;
            }
            else
            {
                if (rblagentType.SelectedValue != "")
                {
                    Session["agentType"] = rblagentType.SelectedValue; enable_Captcha = "1"; xcode.Focus();
                }
                else
                {
                    if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
                    {
                        enable_Captcha = "1";
                        if (Session["agentType"].ToString() == "Agent") { rblagentType.SelectedIndex = 0; } else { rblagentType.SelectedIndex = 1; }
                    }
                }
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            ccode = ConfigurationManager.AppSettings["ccode"]; x_code = ConfigurationManager.AppSettings["xcode"]; 
            if (this.xpassword.Text != "")
            {
                this.password_text = this.xpassword.Text;
                new_hash = hash.GetGetSHA512String(ccode + this.password_text + x_code);
            }
            string agentType = "";
            if ((Session["agentType"] != null) && (Session["agentType"].ToString() != "")) { agentType = Session["agentType"].ToString(); }
            if (agentType == "Agent")
            {
                this.adminID = this.ret.getAgentLogDetails(this.xemail.Text, new_hash);
                c_reg = ret.getRegistrationByID(adminID); Session["c_reg"] = c_reg;
                Session["logo"] = "../"+c_reg.logo;
                Session["pic"] = "../" + c_reg.Principal;
            }
            else
            {
                this.adminID = this.ret.getSubAgentLogDetails(this.xemail.Text, new_hash);
                c_sub = ret.getSubAgentByID(adminID); Session["c_sub"] = c_sub;                
                c_sub_reg = ret.getRegistrationBySubagentRegistrationID(c_sub.RegistrationID); Session["c_sub_reg"] = c_sub_reg;
                Session["logo"] = "../" + c_sub_reg.logo;
                Session["pic"] = "../" + c_sub.AgentPassport;
            }
           
            if (this.adminID != "0")
            {
                string succ = ret.addAdminLog(adminID, RemoteIP, remote_host, remote_user, server_name, server_url);
                if (succ != "")
                {
                    TimeSpan tspan=new TimeSpan(9,0,0);

                    DateTime prev_dt = DateTime.Now.Add(tspan);
                    Session["pwalletID"] = this.adminID; Session["log_date"] = prev_dt.ToString("F");
                    Response.Redirect("./A/profile.aspx");
                }
                else
                {
                    base.Response.Redirect("./login.aspx");
                }
            }

        }

        protected void xpassword_Unload(object sender, EventArgs e)
        {
            this.password_text = this.xpassword.Text;
        }

        public static string GetClientIPAddress(System.Web.HttpRequest httpRequest)
        {
            OriginalIP = "Proxy IP: " + httpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"]; //original IP will be updated by Proxy/Load Balancer.
            RemoteIP = "Remote IP: " + httpRequest.ServerVariables["LOCAL_ADDR"]; //Proxy/Load Balancer IP or original IP if no proxy was used
            remote_host = "Remote Host: " + httpRequest.ServerVariables["REMOTE_HOST"];
            remote_user = "User Agent: " + httpRequest.UserAgent;
            server_name = "UserHostName: " + httpRequest.UserHostName;
            server_url = "UserHostAddress: " + httpRequest.UserHostAddress;

            if (OriginalIP != null && OriginalIP.Trim().Length > 0)
            {
                return OriginalIP + "(" + RemoteIP + ")"; //Lets return both the IPs.
            }

            return RemoteIP;
        }
    }
}