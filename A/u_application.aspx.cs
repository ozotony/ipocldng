using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Ipong.Classes;

namespace Ipong.A
{
    public partial class u_application : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected string coy_name = "";
        protected string cust_id = "";
        protected string email = "";
        protected string fullname = "";
        public string log_date = "";
        protected string mobile = "";
        private Retriever ret = new Retriever();
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            {
                adminID = Session["pwalletID"].ToString();
            }
            else
            {
                base.Response.Redirect("../a_login.aspx");
            }
            if ((Session["log_date"] != null) && (Session["log_date"].ToString() != ""))
            {
                log_date = Session["log_date"].ToString();
            }
            if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
            {
                agentType = Session["agentType"].ToString();
                if (agentType == "Agent")
                {
                    if (Session["c_reg"] != null)
                    {
                        c_reg = (XObjs.Registration)Session["c_reg"];
                        fullname = c_reg.Firstname + " " + c_reg.Surname;
                        coy_name = c_reg.CompanyName;
                        cust_id = c_reg.Sys_ID;
                        email = c_reg.Email;
                        mobile = c_reg.PhoneNumber;
                        Session["coy_name"] = coy_name;
                        Session["fullname"] = fullname;
                        Session["email"] = email;
                        Session["agentemail"] = email;
                        Session["mobile"] = mobile;
                        Session["c_addy"] = c_reg.CompanyAddress;
                        Session["Sys_ID"] = cust_id;
                        Session["xid"] = c_reg.xid;
                    }
                }
                else
                {
                    if (Session["c_sub"] != null)
                    {
                        c_sub = (XObjs.Subagent)Session["c_sub"];
                        fullname = c_sub.Firstname + " " + c_sub.Surname;
                        email = c_sub.Email;
                        mobile = c_sub.Telephone;
                    }
                    if (Session["c_sub_reg"] != null)
                    {
                        c_sub_reg = (XObjs.Registration)Session["c_sub_reg"];
                        coy_name = c_sub_reg.CompanyName;
                        cust_id = c_sub_reg.Sys_ID + "_" + c_sub.AssignID;
                    }
                    Session["coy_name"] = coy_name;
                    Session["fullname"] = fullname;
                    Session["email"] = email;
                    Session["agentemail"] = email;
                    Session["mobile"] = mobile;
                    Session["c_addy"] = c_sub_reg.CompanyAddress;
                    Session["Sys_ID"] = cust_id;
                    Session["xid"] = c_sub.xid;
                }
            }
        }
    }
}