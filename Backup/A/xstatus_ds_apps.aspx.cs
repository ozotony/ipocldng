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
    public partial class xstatus_ds_apps : System.Web.UI.Page
    {
        protected string adminID = "0"; protected string agentType = ""; protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected bool show_search = true;  protected string fullname = ""; protected string email = ""; protected string mobile = "";
        protected string transID = ""; protected string agent_code = ""; protected string coy_name = ""; protected string cust_id = "";
        protected string log_date = ""; 
        protected Classes.Retriever ret = new Classes.Retriever();
        protected Classes.Registration reg = new Classes.Registration();

        protected Classes.XObjs.Registration c_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Registration c_sub_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Subagent c_sub = new Classes.XObjs.Subagent();

        ///////////////////////////////////////////////////For Tm
        protected List<tm.MarkInfo> lt_mi = new List<tm.MarkInfo>();
        protected List<tm.Stage> lt_pw = new List<tm.Stage>();
        protected tm.Representative lt_rep = new tm.Representative();
        protected tm.AddressService c_aos = new tm.AddressService();
        protected tm.Applicant c_app = new tm.Applicant();
        protected tm.Address c_app_addy = new tm.Address();
        protected tm.MarkInfo c_mark = new tm.MarkInfo();
        protected tm.Representative c_rep = new tm.Representative();
        protected tm.Address c_rep_addy = new tm.Address();
        protected tm.Stage c_stage = new tm.Stage();
        protected string pwalletID = "";
        protected int showtm;
        protected string status = "N/A";
        protected string data_status = "N/A";
        protected tm t = new tm();
        protected zues z = new zues();
         
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {          
            if (this.txt_status.Text != "")
            {
                if (this.txt_status.Text.Contains("OAI/TM/"))
                {
                    this.txt_status.Text = this.txt_status.Text.Replace("OAI/TM/", "");
                }
                this.transID = this.txt_status.Text.Trim();
                this.lt_pw = this.t.getStageByClientIDAcc(this.txt_status.Text);
                if (this.lt_pw.Count != 0)
                {
                    Session["xvid"] = this.txt_status.Text.Trim();
                    this.lt_mi = this.t.getMarkInfoByUserID(this.lt_pw[0].ID);
                    this.lt_rep = this.t.getRepByUserID(this.lt_pw[0].ID);
                    Session["agent_code"] = lt_rep.agent_code;
                    if (this.lt_pw[0].status == "1")
                    {
                        this.status = "Verification";
                        if (lt_pw[0].data_status == "Fresh") { data_status = "Untreated"; }
                    }
                    if (this.lt_pw[0].status == "2")
                    {
                        this.status = "Search";
                        if (lt_pw[0].data_status == "Re-conduct search") { data_status = "being re-conducted"; }
                    }
                    if (this.lt_pw[0].status == "22")
                    {
                        this.status = "Search 2";
                        if (lt_pw[0].data_status == "Re-conduct search 1") { data_status = "being re-conducted"; }
                    }
                    if (this.lt_pw[0].status == "3")
                    {
                        this.status = "Search 2";
                        if (lt_pw[0].data_status == "Search Conducted") { data_status = "being processed"; }
                    }
                    if (this.lt_pw[0].status == "33")
                    {
                        this.status = "Examiners";
                        if (lt_pw[0].data_status == "Search 2 Conducted") { data_status = "being processed"; }
                    }
                    if (this.lt_pw[0].status == "4")
                    {
                        this.status = "Acceptance";
                        if (lt_pw[0].data_status == "Registrable") { data_status = "Accepted"; }
                        else if (lt_pw[0].data_status == "Refused") { data_status = "Refused"; }
                        else if (lt_pw[0].data_status == "Non-registrable") { data_status = "not-registrable"; }
                    }
                    if (this.lt_pw[0].status == "5")
                    {
                        this.status = "Publication";
                        if (lt_pw[0].data_status == "Accepted") { data_status = "being published"; }
                    }
                    if (this.lt_pw[0].status == "6")
                    {
                        this.status = "Opposition";
                        if (lt_pw[0].data_status == "Published") { data_status = "being published"; } else { data_status = "been opposed"; }
                    }
                    if (this.lt_pw[0].status == "7")
                    {
                        this.status = "Certification";
                        if (lt_pw[0].data_status == "Not Opposed") { data_status = "being processed"; }
                    }
                    if (this.lt_pw[0].status == "8")
                    {
                        this.status = "Registrars";
                        if (lt_pw[0].data_status == "Certified") { data_status = "being processed"; }
                    }
                    if (this.lt_pw[0].status == "9")
                    {
                        this.status = "Registrars";
                        if (lt_pw[0].data_status == "Registered") { data_status = "being registered"; }
                    }
                    this.showtm = 1; show_search = false;
                }
                else   {  this.status = "N/A";  this.showtm = 1; }
            }
            else
            {
                base.Response.Write("<script language=JavaScript>alert('PLEASE ENTER A VALID REFERENCE NUMBER')</script>");
            }
        }

        protected void btnNewSearch_Click(object sender, EventArgs e)
        {
            txt_status.Text = ""; this.showtm = 0; show_search = true;
        }
        protected void btnNewSearch2_Click(object sender, EventArgs e)
        {
            txt_status.Text = ""; this.showtm = 0; show_search = true;
        }

        protected void BtnReprintTmAck_Click(object sender, EventArgs e)
        {
            if ((Session["xvid"] != null) && (Session["xvid"].ToString() != "")){ transID = Session["xvid"].ToString(); }
            if ((Session["agent_code"] != null) && (Session["agent_code"].ToString() != "")) { agent_code = Session["agent_code"].ToString(); }
            this.pwalletID = this.t.getCheckStatusDetails(transID, agent_code);
            if (this.pwalletID != "")
            {
                this.c_mark = this.t.getMarkInfoClassByUserID(this.pwalletID);
                this.c_rep = this.t.getRepClassByUserID(this.pwalletID);
                this.c_stage = this.t.getStageClassByUserID(this.pwalletID);
                this.c_app = this.t.getApplicantClassByID(this.pwalletID);
                this.c_app_addy = this.t.getAddressClassByID(this.c_app.addressID);
                this.showtm = 2; show_search = false;
            }
            else      {  this.showtm = 0; show_search = true; }

        }

        
              

    }
}