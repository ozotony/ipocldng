using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Brettle.Web.NeatUpload;
using System.Configuration;

namespace Ipong.Forms
{
    public partial class us_application_docs : System.Web.UI.Page
    {
        public string serverpath; public string doc_path = ""; public string succ_msg = ""; protected string agentType = "";
        public string pic_newfilename = "0"; public string logo_newfilename = "0"; public string xid = ""; public int sp = 0;
        protected string adminID = "0"; protected string fullname = ""; protected string email = ""; protected string mobile = "";
        protected string transID = ""; protected string agent_code = ""; protected string coy_name = ""; protected string cust_id = "";
        protected string log_date = ""; 
        public Classes.Registration t = new Classes.Registration();
        
         protected Classes.XObjs.Registration c_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Registration c_sub_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Subagent c_sub = new Classes.XObjs.Subagent();


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
            if (!IsPostBack)
            {
                if ((Request.Form["x"] != null) && (Request.Form["x"].ToString() != ""))
                {
                    xid = Request.Form["x"].ToString(); Session["new_ptID"] = Request.Form["x"].ToString();
                }
                else
                {
                    Response.Redirect("../A/profile.aspx");
                }
            }
            btn_all_doc.Click += new System.EventHandler(this.upload_Clicked);
            this.serverpath = base.Server.MapPath("~/");             
        }

        private void upload_Clicked(object sender, EventArgs e)
        {
            try
            {
                if ((Session["agentType"] != null) && (Session["agentType"].ToString() != "") && (Session["new_ptID"] != null) && (Session["new_ptID"].ToString() != ""))
                {
                    agentType = Session["agentType"].ToString();

                    if (agentType == "Sub-Agent")
                    {
                        doc_path = base.Server.MapPath("~/") + "admin/ag_docz/agsub/" + Session["new_ptID"].ToString() + "/";

                        if (!Directory.Exists(doc_path))
                        {
                            Directory.CreateDirectory(doc_path);
                        }
                        if (IsValid && fu_pic_doc.HasFile)
                        {
                            pic_newfilename = Path.Combine(doc_path, fu_pic_doc.FileName.Replace(" ", "_"));
                            fu_pic_doc.MoveTo(Path.Combine(doc_path, fu_pic_doc.FileName.Replace(" ", "_")), MoveToOptions.Overwrite);
                        }

                        pic_newfilename = pic_newfilename.Replace(base.Server.MapPath("~/"), "");

                        if (pic_newfilename != "0")
                        {
                            if (t.updateSubAgProfileDocz(pic_newfilename, Session["new_ptID"].ToString()) != "0")
                            {
                                succ_msg = "DOCUMENTS UPLOADED SUCCESSFULLY!!"; sp = 1;
                            }
                            else
                            {
                                succ_msg = "DOCUMENTS NOT UPLOADED. PLEASE TRY AGAIN!!"; sp = 0;
                            }
                        }
                    }
                }
                else
                { Response.Redirect("../a_login.aspx"); }

            }
            catch (Exception ex)
            {
                succ_msg = "DOCUMENTS NOT UPLOADED. PLEASE TRY AGAIN!!";
            }
        }
        protected void btn_profile_Click(object sender, EventArgs e)
        {
            Response.Redirect("../A/profile.aspx");
        }



       
    }
}