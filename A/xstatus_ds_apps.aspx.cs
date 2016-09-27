using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using Ipong.Classes;

namespace Ipong.A
{
    public partial class xstatus_ds_apps : Page
    {
        protected string adminID = "0";
        protected string agent_code = "";
        protected string agentType = "";
        protected AppStatus c_as = new AppStatus();
        protected tm.AddressService c_aos = new tm.AddressService();
        protected tm.Applicant c_app = new tm.Applicant();
        protected tm.Address c_app_addy = new tm.Address();
        protected tm.MarkInfo c_mark = new tm.MarkInfo();
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected tm.Representative c_rep = new tm.Representative();
        protected tm.Address c_rep_addy = new tm.Address();
        protected tm.Stage c_stage = new tm.Stage();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected string coy_name = "";
        protected string cust_id = "";
        protected string data_status = "N/A";
        protected string email = "";
        protected string fullname = "";
        protected string log_date = "";
        protected List<tm.MarkInfo> lt_mi = new List<tm.MarkInfo>();
        protected List<tm.Stage> lt_pw = new List<tm.Stage>();
        protected tm.Representative lt_rep = new tm.Representative();
        protected string mobile = "";
        protected string pwalletID = "";
        protected Ipong.Classes.Registration reg = new Ipong.Classes.Registration();
        protected Retriever ret = new Retriever();
        protected bool show_search = true;
        protected int showtm;
        protected string status = "N/A";
        protected tm t = new tm();
        protected string transID = "";
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected zues z = new zues();

        protected void btnNewSearch_Click(object sender, EventArgs e)
        {
            this.txt_status.Text = "";
            this.showtm = 0;
            this.show_search = true;
        }

        protected void btnNewSearch2_Click(object sender, EventArgs e)
        {
            this.txt_status.Text = "";
            this.showtm = 0;
            this.show_search = true;
        }

        protected void BtnReprintTmAck_Click(object sender, EventArgs e)
        {
            if ((this.Session["xvid"] != null) && (this.Session["xvid"].ToString() != ""))
            {
                this.transID = this.Session["xvid"].ToString();
            }
            if ((this.Session["agent_code"] != null) && (this.Session["agent_code"].ToString() != ""))
            {
                this.agent_code = this.Session["agent_code"].ToString();
            }
            this.pwalletID = this.t.getCheckStatusDetails2(this.transID, this.agent_code);
            if (this.pwalletID != "")
            {
                this.c_mark = this.t.getMarkInfoClassByUserID(this.pwalletID);
                this.c_rep = this.t.getRepClassByUserID(this.pwalletID);
                this.c_stage = this.t.getStageClassByUserID(this.pwalletID);
                this.c_app = this.t.getApplicantClassByID(this.pwalletID);
                this.c_app_addy = this.t.getAddressClassByID(this.c_app.addressID);
                this.showtm = 2;
                this.show_search = false;
            }
            else
            {
                this.showtm = 0;
                this.show_search = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.txt_status.Text != "")
            {
                if (this.txt_status.Text.Contains("OAI/TM/"))
                {
                    this.txt_status.Text = this.txt_status.Text.Replace("OAI/TM/", "");
                }
                this.transID = this.txt_status.Text.Trim();
                this.lt_pw = this.t.getStageByClientIDAcc2(this.txt_status.Text);
                if (this.lt_pw.Count > 0)
                {
                    this.Session["xvid"] = this.txt_status.Text.Trim();
                    this.lt_mi = this.t.getMarkInfoByUserID(this.lt_pw[0].ID);
                    this.lt_rep = this.t.getRepByUserID(this.lt_pw[0].ID);
                    this.Session["agent_code"] = this.lt_rep.agent_code;
                    SortedList<string, string> x = c_as.showDsStatus(lt_pw[0].status, lt_pw[0].data_status);
                    status = x["status"];
                    data_status = x["data_status"];
                    this.showtm = 1;
                    this.show_search = false;
                }
                else
                {
                    this.status = "N/A";
                    this.showtm = 1;
                }
            }
            else
            {
                base.Response.Write("<script language=JavaScript>alert('PLEASE ENTER A VALID REFERENCE NUMBER')</script>");
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
    }
}