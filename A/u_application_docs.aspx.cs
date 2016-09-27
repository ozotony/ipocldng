using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Brettle.Web.NeatUpload;
using Ipong.Classes;

namespace Ipong.A
{
    public partial class u_application_docs : Page
    {
        protected string adminID = "0";
        protected string agent_code = "";
        protected string agentType = "";
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected string coy_name = "";
        protected string cust_id = "";
        public string doc_path = "";
        protected string email = "";
        protected string fullname = "";
        protected string log_date = "";
        public string logo_newfilename = "0";
        protected string mobile = "";
        public string pic_newfilename = "0";
        public string serverpath;
        public int sp;
        public string succ_msg = "";
        public Ipong.Classes.Registration t = new Ipong.Classes.Registration();
        protected string transID = "";
        public string xid = "";

        protected void btn_profile_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("../A/profile.aspx");
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
            if (!base.IsPostBack)
            {
                if ((base.Request.Form["x"] != null) && (base.Request.Form["x"].ToString() != ""))
                {
                    this.xid = base.Request.Form["x"].ToString();
                    this.Session["new_ptID"] = base.Request.Form["x"].ToString();
                }
                else
                {
                    base.Response.Redirect("../A/profile.aspx");
                }
            }
            this.btn_all_doc.Click += new EventHandler(this.upload_Clicked);
            this.serverpath = base.Server.MapPath("~/");
        }

        private void upload_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (((this.Session["agentType"] != null) && (this.Session["agentType"].ToString() != "")) && ((this.Session["new_ptID"] != null) && (this.Session["new_ptID"].ToString() != "")))
                {
                    this.agentType = this.Session["agentType"].ToString();
                    if (this.agentType == "Agent")
                    {
                        this.doc_path = base.Server.MapPath("~/") + "admin/ag_docz/ag/" + this.Session["new_ptID"].ToString() + "/";
                        if (!Directory.Exists(this.doc_path))
                        {
                            Directory.CreateDirectory(this.doc_path);
                        }
                        if (base.IsValid && this.fu_pic_doc.HasFile)
                        {
                            this.pic_newfilename = Path.Combine(this.doc_path, this.fu_pic_doc.FileName.Replace(" ", "_"));
                            this.fu_pic_doc.MoveTo(Path.Combine(this.doc_path, this.fu_pic_doc.FileName.Replace(" ", "_")), MoveToOptions.Overwrite);
                            Session["pic"] = "../admin/ag_docz/ag/" + this.Session["new_ptID"].ToString() + "/" + fu_pic_doc.FileName.Replace(" ", "_");
                        }
                        if (base.IsValid && this.fu_logo_doc.HasFile)
                        {
                            this.logo_newfilename = Path.Combine(this.doc_path, this.fu_logo_doc.FileName.Replace(" ", "_"));
                            this.fu_logo_doc.MoveTo(this.logo_newfilename, MoveToOptions.Overwrite);
                            Session["logo"] = "../admin/ag_docz/ag/" + this.Session["new_ptID"].ToString() + "/" + fu_logo_doc.FileName.Replace(" ", "_");
                        }
                        this.pic_newfilename = this.pic_newfilename.Replace(base.Server.MapPath("~/"), "");
                        this.logo_newfilename = this.logo_newfilename.Replace(base.Server.MapPath("~/"), "");
                        if ((this.pic_newfilename != "0") && (this.logo_newfilename != "0"))
                        {
                            if (this.t.updateAgProfileDocz(this.pic_newfilename, this.logo_newfilename, this.Session["new_ptID"].ToString()) != "0")
                            {
                                this.succ_msg = "DOCUMENTS UPLOADED SUCCESSFULLY!!";                               
                               
                                this.sp = 1;
                            }
                            else
                            {
                                this.succ_msg = "DOCUMENTS NOT UPLOADED. PLEASE TRY AGAIN!!";
                                this.sp = 0;
                            }
                        }
                    }
                }
                else
                {
                    base.Response.Redirect("../a_login.aspx");
                }
            }
            catch (Exception)
            {
                this.succ_msg = "DOCUMENTS NOT UPLOADED. PLEASE TRY AGAIN!!";
            }
        }
    }
}