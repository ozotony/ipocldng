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
    public partial class xstatus_pt_apps : System.Web.UI.Page
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
        public string agt; public string pwalletID = "";
        public List<pt.PtInfo> lt_mi = new List<pt.PtInfo>();
        public List<pt.Stage> lt_pw = new List<pt.Stage>();
        public pt.Representative lt_rep = new pt.Representative();
        public string r;
        public int showtm;
        public string status = "N/A";
        public string data_status = "N/A";
        public pt t = new pt();
        public zues z = new zues();
        public int refill = 0;

        public List<pt.Applicant> lt_appx = new List<pt.Applicant>();
        public List<pt.PtInfo> lt_mix = new List<pt.PtInfo>();
        public List<pt.Representative> lt_repx = new List<pt.Representative>();
        public List<pt.Stage> lt_stagex = new List<pt.Stage>();
        public List<pt.Assignment_info> lt_assinfox = new List<pt.Assignment_info>();
        public List<pt.Inventor> lt_invx = new List<pt.Inventor>();
        public List<pt.Priority_info> lt_xprix = new List<pt.Priority_info>();

        public List<pt.Applicant> lt_app = new List<pt.Applicant>();
        public List<pt.Stage> lt_stage = new List<pt.Stage>();

        public List<pt.Assignment_info> lt_assinfo = new List<pt.Assignment_info>();
        public List<pt.Inventor> lt_inv = new List<pt.Inventor>();
        public List<pt.Priority_info> lt_xpri = new List<pt.Priority_info>();

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
            if (this.txt_status.Text!= "")
            {
                if (this.txt_status.Text.Contains("OAI/PT/"))
                {
                    this.txt_status.Text= this.txt_status.Text.Replace("OAI/PT/", "");
                }
                this.r = this.txt_status.Text;
                this.lt_pw = this.t.getStageByUserIDAcc(this.txt_status.Text, cust_id);
                if (this.lt_pw.Count > 0)
                {
                    pt.Stage s = this.t.getStatusIDByvalidationID(this.txt_status.Text.Trim());

                    this.lt_mix = this.t.getPtInfoByPwalletID(this.lt_pw[0].ID);
                    this.lt_repx = this.t.getRepListByUserID(this.lt_pw[0].ID);
                    this.lt_stagex = this.t.getStageByUserID(this.lt_pw[0].ID);
                    this.lt_appx = this.t.getApplicantByvalidationID(this.lt_pw[0].ID);
                    this.lt_invx = this.t.getInventorByvalidationID(this.lt_pw[0].ID);
                    this.lt_assinfox = this.t.getAssignment_infoByvalidationID(this.lt_pw[0].ID);
                    this.lt_xprix = this.t.getPriority_infoByvalidationID(this.lt_pw[0].ID);

                    Session["xvid"] = this.txt_status.Text;
                    this.showtm = 1; show_search = false;
                    if ((Convert.ToInt32(s.status) == 1) &&
                                 (lt_appx.Count >= 1) && (lt_mix.Count == 1) &&
                                 (lt_stagex.Count == 1) && (lt_repx.Count == 1)
                                 )
                    {
                        if (
                            (lt_mix[0].pt_desc == "") || (lt_mix[0].spec_doc == "") || (lt_mix[0].loa_doc == "") || (lt_mix[0].claim_doc == "") ||
                         (lt_mix[0].pct_doc == "") || (lt_mix[0].doa_doc == "") || (lt_appx[0].address == "") || (lt_appx[0].xmobile == "") ||
                         (lt_repx[0].address == "") || (lt_repx[0].xmobile == "")
                        )
                        {
                            status = "Filing"; data_status = "Uncompleted";
                            refill = 1;
                        }
                        else
                        {
                            refill = 0;
                            showStatus(lt_pw);
                        }
                    }
                    else if ((Convert.ToInt32(s.status) == 1) &&
                        ((lt_appx.Count >= 1) || (lt_mix.Count == 1) ||
                                 (lt_stagex.Count == 1) || (lt_repx.Count == 1)
                        ))
                    {
                        status = "Filing"; data_status = "Uncompleted";
                        refill = 1;
                    }
                    else if (Convert.ToInt32(s.status) > 1)
                    {
                        refill = 0;
                        if (this.lt_pw.Count != 0)
                        {
                            Session["xvid"] = this.txt_status.Text;
                            this.lt_mi = this.t.getPtInfoByUserID(this.lt_pw[0].ID);
                            this.lt_rep = this.t.getRepByUserID(this.lt_pw[0].ID);
                            showStatus(lt_pw);
                        }
                        else   {  this.status = "N/A";}
                    }
                }
            }
            else
            {
                base.Response.Write("<script language=JavaScript>alert('PLEASE ENTER A VALID REFERENCE NUMBER')</script>");
            }   
        }
        public void showStatus(List<pt.Stage> lt_p)
        {
            if (lt_p[0].status == "1")
            {
                status = "Payment Verification Office";
                if (lt_p[0].data_status == "Fresh") { data_status = "Untreated"; }
                else if (lt_p[0].data_status == "Invalid") { data_status = "Invalid"; }
                else if (lt_p[0].data_status == "V_Contact") { data_status = "Being processed"; }
            }

            if (lt_p[0].status == "2")
            {
                status = "Patent Search Office";
                if (lt_p[0].data_status == "Valid") { data_status = "Successfully reviewed"; }
                else if (lt_p[0].data_status == "S_Contact") { data_status = "Being processed"; }
            }
            if (lt_p[0].status == "3")
            {
                status = "Patent Examiner 1 Office";
                if (lt_p[0].data_status == "Further Search") { data_status = "Further search required"; status = "Patent Search Office"; }
                else if (lt_p[0].data_status == "E_Contact") { data_status = "Being processed"; }
                else if (lt_p[0].data_status == "Search Conducted") { data_status = "Successfully reviewed"; }
                else if (lt_p[0].data_status == "Refused") { data_status = "Refused"; }
            }

            if (lt_p[0].status == "4")
            {
                status = "Patent Approving Office";
                if (lt_p[0].data_status == "Not-Patentable") { data_status = "Not-patentable"; status = "Patent Examiner 1 Office"; }
                else if (lt_p[0].data_status == "A_Contact") { data_status = "Being processed"; }
                else if (lt_p[0].data_status == "Futher Review") { data_status = "Successfully reviewed"; }
            }
            if (lt_p[0].status == "5")
            {
                status = "Registrars Office";
                if (lt_p[0].data_status == "Review Patent") { data_status = "Being reviewed"; status = "Patent Approving Office"; }
                else if (lt_p[0].data_status == "R_Contact") { data_status = "Being processed"; }
                else if (lt_p[0].data_status == "Patentable") { data_status = "Successfully reviewed"; }
            }
            if (lt_p[0].status == "6")
            {
                status = "Registrars Office";
                if (lt_p[0].data_status == "Grant Patent") { data_status = "Patent granted"; }
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
            if ((Session["xvid"] != null) && (Session["xvid"].ToString() != "")) { transID = Session["xvid"].ToString(); }            
            this.pwalletID = this.t.getPwalletID(this.transID);
            if (this.pwalletID != "")
            {
                this.lt_mi = this.t.getPtInfoByPwalletID(this.pwalletID);
                this.lt_repx = this.t.getRepListByUserID(this.pwalletID);
                this.lt_stage = this.t.getStageByUserID(this.pwalletID);
                this.lt_app = this.t.getApplicantByvalidationID(this.pwalletID);
                this.lt_inv = this.t.getInventorByvalidationID(this.pwalletID);
                this.lt_assinfo = this.t.getAssignment_infoByvalidationID(this.pwalletID);
                this.lt_xpri = this.t.getPriority_infoByvalidationID(this.pwalletID);

                Session["xserviceaddress"] = null;
                Session["xrepresentative"] = null;
                Session["xmarkinfo"] = null;
                Session["xapplication"] = null;
                Session["vid"] = null;
                Session["amt"] = null;
                Session["aid"] = null;
                Session["g"] = null;
                Session["pc"] = null;
                Session["new_ptID"] = null;
                Session["loa_newfilename"] = null;
                Session["claim_newfilename"] = null;
                Session["pct_newfilename"] = null;
                Session["doa_newfilename"] = null;
                Session["spec_newfilename"] = null;
                Session["txt_loa_no"] = null;
                Session["txt_claim_no"] = null;
                Session["txt_pct_no"] = null;
                Session["txt_doa_no"] = null;
                this.showtm = 2; show_search = false;
            }
            else
            { this.showtm = 0; show_search = true; }
       
        }

        
              

    }
}