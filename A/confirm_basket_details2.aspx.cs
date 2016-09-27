using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Ipong.Classes;

namespace Ipong.A
{
    using Ipong.ExcelClasses;
    public partial class confirm_basket_details2 : Page
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
        protected XObjs.Trademark_item ti = new XObjs.Trademark_item();
        protected string transID = "";
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        public int xsucc;
        public Retriever ret = new Retriever();
       
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
                        Session["mobile"] = mobile;
                        Session["c_addy"] = c_reg.CompanyAddress;
                    }
                }
                else
                {
                    XObjs.Registration registration = new XObjs.Registration();
                    if (Session["c_sub"] != null)
                    {
                        c_sub = (XObjs.Subagent)Session["c_sub"];
                        fullname = c_sub.Firstname + " " + c_sub.Surname;
                        email = c_sub.Email;
                        mobile = c_sub.Telephone;
                    }
                    if (Session["c_sub_reg"] != null)
                    {
                        registration = (XObjs.Registration)Session["c_sub_reg"];
                        coy_name = registration.CompanyName;
                        cust_id = registration.Sys_ID + "_" + c_sub.AssignID;
                    }
                    Session["coy_name"] = coy_name;
                    Session["fullname"] = fullname;
                    Session["email"] = email;
                    Session["mobile"] = mobile;
                    Session["c_addy"] = registration.CompanyAddress;
                }

                ///
                if ((Session["Trademark_item"] != null) && (Session["Trademark_item"].ToString() != ""))
                {
                    ti = (XObjs.Trademark_item)Session["Trademark_item"];
                    if (ti.item_code.ToUpper().Contains("T") && ((ti.item_code.ToUpper() == "T002") || (ti.item_code.ToUpper() == "T102")))
                    {
                        ti.xurl = ConfigurationManager.AppSettings["new2_t002"];
                    }
                    else if ((ti.item_code.ToUpper().Contains("T") && (ti.item_code.ToUpper() != "T002")) && (ti.item_code.ToUpper() != "T102"))
                    {
                        ti.xurl = ConfigurationManager.AppSettings["new2_t003"];
                    }

                    else if (this.ti.item_code.ToUpper().Contains("P") && ((this.ti.item_code.ToUpper() == "P001") || (this.ti.item_code.ToUpper() == "P002") || (this.ti.item_code.ToUpper() == "P102")))
                    {
                        ti.xurl = ConfigurationManager.AppSettings["new2_p001"];
                    }
                    else if (this.ti.item_code.ToUpper().Contains("P") && ((this.ti.item_code.ToUpper() != "P001") && (this.ti.item_code.ToUpper() != "P002") && (this.ti.item_code.ToUpper() != "P102")))
                    {
                        ti.xurl = ConfigurationManager.AppSettings["new2_p003"];
                    }

                    else if (this.ti.item_code.ToUpper().Contains("D") && ((this.ti.item_code.ToUpper() == "D002") || (this.ti.item_code.ToUpper() == "D003") || (this.ti.item_code.ToUpper() == "D102")))
                    {
                        ti.xurl = ConfigurationManager.AppSettings["new2_d002"];
                    }
                    else if (this.ti.item_code.ToUpper().Contains("D") && ((this.ti.item_code.ToUpper() != "D002") && (this.ti.item_code.ToUpper() != "D003") && (this.ti.item_code.ToUpper() != "D102")))
                    {
                        ti.xurl = ConfigurationManager.AppSettings["new2_d004"];
                    }
                }
            }
        }
     
    }
}