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
    public partial class xstatus_pt_apps : Page
    {
        protected string adminID = "0";
        protected string agent_code = "";
        protected string agentType = "";
        public string agt;
        protected AppStatus c_as = new AppStatus();
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected string coy_name = "";
        protected string cust_id = "";
        public string data_status = "N/A";
        protected string email = "";
        protected string fullname = "";
        protected string log_date = "";
        public List<pt.Applicant> lt_app = new List<pt.Applicant>();
        public List<pt.Applicant> lt_appx = new List<pt.Applicant>();
        public List<pt.Assignment_info> lt_assinfo = new List<pt.Assignment_info>();
        public List<pt.Assignment_info> lt_assinfox = new List<pt.Assignment_info>();
        public List<pt.Inventor> lt_inv = new List<pt.Inventor>();
        public List<pt.Inventor> lt_invx = new List<pt.Inventor>();
        public List<pt.PtInfo> lt_mi = new List<pt.PtInfo>();
        public List<pt.PtInfo> lt_mix = new List<pt.PtInfo>();
        public List<pt.Stage> lt_pw = new List<pt.Stage>();
        public pt.Representative lt_rep = new pt.Representative();
        public List<pt.Representative> lt_repx = new List<pt.Representative>();
        public List<pt.Stage> lt_stage = new List<pt.Stage>();
        public List<pt.Stage> lt_stagex = new List<pt.Stage>();
        public List<pt.Priority_info> lt_xpri = new List<pt.Priority_info>();
        public List<pt.Priority_info> lt_xprix = new List<pt.Priority_info>();
        protected string mobile = "";
        public string pwalletID = "";
        public string r;
        public int refill;
        protected Ipong.Classes.Registration reg = new Ipong.Classes.Registration();
        protected Retriever ret = new Retriever();
        protected bool show_search = true;
        public int showtm;
        public string status = "N/A";
        public pt t = new pt();
        protected string transID = "";
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        public zues z = new zues();

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
                this.Session["xserviceaddress"] = null;
                this.Session["xrepresentative"] = null;
                this.Session["xmarkinfo"] = null;
                this.Session["xapplication"] = null;
                this.Session["vid"] = null;
                this.Session["amt"] = null;
                this.Session["aid"] = null;
                this.Session["g"] = null;
                this.Session["pc"] = null;
                this.Session["new_ptID"] = null;
                this.Session["loa_newfilename"] = null;
                this.Session["claim_newfilename"] = null;
                this.Session["pct_newfilename"] = null;
                this.Session["doa_newfilename"] = null;
                this.Session["spec_newfilename"] = null;
                this.Session["txt_loa_no"] = null;
                this.Session["txt_claim_no"] = null;
                this.Session["txt_pct_no"] = null;
                this.Session["txt_doa_no"] = null;
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
                if (this.txt_status.Text.Contains("OAI/PT/"))
                {
                    this.txt_status.Text = this.txt_status.Text.Replace("OAI/PT/", "");
                }
                this.r = this.txt_status.Text;
                this.lt_pw = this.t.getStageByUserIDAcc(this.txt_status.Text, this.cust_id);
                if (this.lt_pw.Count > 0)
                {
                    pt.Stage stage = this.t.getStatusIDByvalidationID(this.txt_status.Text.Trim());
                    this.lt_mix = this.t.getPtInfoByPwalletID(this.lt_pw[0].ID);
                    this.lt_repx = this.t.getRepListByUserID(this.lt_pw[0].ID);
                    this.lt_stagex = this.t.getStageByUserID(this.lt_pw[0].ID);
                    this.lt_appx = this.t.getApplicantByvalidationID(this.lt_pw[0].ID);
                    this.lt_invx = this.t.getInventorByvalidationID(this.lt_pw[0].ID);
                    this.lt_assinfox = this.t.getAssignment_infoByvalidationID(this.lt_pw[0].ID);
                    this.lt_xprix = this.t.getPriority_infoByvalidationID(this.lt_pw[0].ID);
                    this.Session["xvid"] = this.txt_status.Text;
                    this.showtm = 1;
                    this.show_search = false;
                    if ((((Convert.ToInt32(stage.status) == 1) && (this.lt_appx.Count >= 1)) && ((this.lt_mix.Count == 1) && (this.lt_stagex.Count == 1))) && (this.lt_repx.Count == 1))
                    {
                        if ((((this.lt_mix[0].pt_desc == "") || (this.lt_mix[0].spec_doc == "")) || ((this.lt_mix[0].loa_doc == "") || (this.lt_mix[0].claim_doc == ""))) || ((((this.lt_mix[0].pct_doc == "") || (this.lt_mix[0].doa_doc == "")) || ((this.lt_appx[0].address == "") || (this.lt_appx[0].xmobile == ""))) || ((this.lt_repx[0].address == "") || (this.lt_repx[0].xmobile == ""))))
                        {
                            this.status = "Filing";
                            this.data_status = "Uncompleted";
                            this.refill = 1;
                        }
                        else
                        {
                            this.refill = 0;
                            SortedList<string, string> x = c_as.showPtStatus(lt_pw[0].status, lt_pw[0].data_status);
                            status = x["status"];
                            data_status = x["data_status"];
                        }
                    }
                    else if ((Convert.ToInt32(stage.status) == 1) && (((this.lt_appx.Count >= 1) || (this.lt_mix.Count == 1)) || ((this.lt_stagex.Count == 1) || (this.lt_repx.Count == 1))))
                    {
                        this.status = "Filing";
                        this.data_status = "Uncompleted";
                        this.refill = 1;
                    }
                    else if (Convert.ToInt32(stage.status) > 1)
                    {
                        this.refill = 0;
                        if (this.lt_pw.Count != 0)
                        {
                            this.Session["xvid"] = this.txt_status.Text;
                            this.lt_mi = this.t.getPtInfoByUserID(this.lt_pw[0].ID);
                            this.lt_rep = this.t.getRepByUserID(this.lt_pw[0].ID);
                            SortedList<string, string> x = c_as.showPtStatus(lt_pw[0].status, lt_pw[0].data_status);
                            status = x["status"];
                            data_status = x["data_status"];
                        }
                        else
                        {
                            this.status = "N/A";
                        }
                    }
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