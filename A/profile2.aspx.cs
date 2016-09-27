using Ipong.Classes;
using System;
using System.Web.UI;


namespace Ipong.A
{
    public partial class profile : Page
    {
        public string adminID = "0";
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
        protected Ipong.Classes.Registration reg = new Ipong.Classes.Registration();
        protected Retriever ret = new Retriever();
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");

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
            }
            else
            {
                base.Response.Redirect("../a_login.aspx");
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
                        hfImage1.Value = this.cust_id;
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
                    if (this.Session["c_sub"] != null)
                    {
                        this.c_sub = (XObjs.Subagent)this.Session["c_sub"];
                        this.fullname = this.c_sub.Firstname + " " + this.c_sub.Surname;
                        this.email = this.c_sub.Email;
                        this.mobile = this.c_sub.Telephone;
                    }
                    if (this.Session["c_sub_reg"] != null)
                    {
                        this.c_sub_reg = (XObjs.Registration)this.Session["c_sub_reg"];
                        this.coy_name = this.c_sub_reg.CompanyName;
                        this.cust_id = this.c_sub_reg.Sys_ID + "_" + this.c_sub.AssignID;
                    }
                    this.Session["coy_name"] = this.coy_name;
                    this.Session["fullname"] = this.fullname;
                    this.Session["email"] = this.email;
                    this.Session["mobile"] = this.mobile;
                    this.Session["c_addy"] = this.c_sub_reg.CompanyAddress;
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            this.c_reg = (XObjs.Registration)this.Session["c_reg"];

            string[] words = this.c_reg.Sys_ID.Split('/');

            Response.Redirect("http://88.150.164.30/IpoTest2/#/approvedbranchcollect/" + words[2]);


        }
    }
}