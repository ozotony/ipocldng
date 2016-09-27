using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;

namespace Ipong.A
{
    public partial class v_bask_pt : System.Web.UI.Page
    {
        protected string adminID = "0"; protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int show_inv = 0; protected int xtotal_amt = 0; protected int tm_cnt = 0; protected int used_cnt = 0; protected int unused_cnt = 0;
        protected string data_status = ""; protected string status = ""; protected string mobile = "";
        protected string transID = ""; protected string ref_no = ""; protected int viz = 0;
        protected string log_date = ""; protected string agentType = "";

        private Classes.Retriever ret = new Classes.Retriever();
        protected Classes.tm t = new Classes.tm();

        protected NameValueCollection data = new NameValueCollection();
        protected Classes.XObjs.Registration c_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Registration c_sub_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Subagent c_sub = new Classes.XObjs.Subagent();
        protected Classes.XObjs.Applicant c_app = new Classes.XObjs.Applicant();
        protected Classes.XObjs.Trademark_item ti = new Classes.XObjs.Trademark_item();
        protected Classes.RemotePost rp = new Classes.RemotePost();
        protected List<Classes.tm.Stage> lt_pw = new List<Classes.tm.Stage>();
        protected SortedList<string, string> xheaders = new SortedList<string, string>();


        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Trademark_item"] = null;
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            {
                this.adminID = Session["pwalletID"].ToString();
                xadminID.Value = adminID;
                if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
                {
                    agentType = Session["agentType"].ToString();
                    tm_cnt = ret.getCombinedPaidFee_detail_ItemsCntByCat(adminID, "pt", "1", agentType);
                    used_cnt = ret.getPaidUsedCntByCat(adminID, "pt", "1", agentType, "Used");
                    unused_cnt = ret.getPaidUsedCntByCat(adminID, "pt", "1", agentType, "Not Used");

                    if (agentType == "Agent")
                    {
                        c_reg = ret.getRegistrationByID(adminID); Session["c_reg"] = c_reg;
                        Session["coy_name"] = c_reg.CompanyName;
                        Session["agentemail"] = c_reg.Email;
                        Session["agentpnumber"] = c_reg.PhoneNumber;
                        Session["Sys_ID"] = c_reg.Sys_ID;
                        Session["cname"] = c_reg.CompanyName;
                    }
                    else
                    {
                        c_sub = ret.getSubAgentByID(adminID); Session["c_sub"] = c_sub;
                        c_sub_reg = ret.getRegistrationBySubagentRegistrationID(c_sub.RegistrationID); Session["c_sub_reg"] = c_sub_reg;
                        Session["coy_name"] = c_sub_reg.CompanyName;
                        Session["agentemail"] = c_sub.Email;
                        Session["agentpnumber"] = c_sub.Telephone;
                        Session["Sys_ID"] = c_sub.Sys_ID;
                        Session["cname"] = c_sub_reg.CompanyName;
                    }
                }
            }
            else
            { base.Response.Redirect("../a_login.aspx"); }

            if ((Session["log_date"] != null) && (Session["log_date"].ToString() != "")) { this.log_date = Session["log_date"].ToString(); }   
        }

        protected void gvTm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "TmStatusClick")
            {
                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;
                string xid = e.CommandArgument.ToString();
                string item_code = gvTm.Rows[rowindex].Cells[1].Text;
                string new_transID = gvTm.Rows[rowindex].Cells[3].Text;
                int amt = Convert.ToInt32(gvTm.Rows[rowindex].Cells[4].Text);
                TextBox txt_ptitle = (TextBox)gvTm.Rows[rowindex].Cells[2].FindControl("txt_product_title_tm");
                ImageButton lb = (ImageButton)gvTm.Rows[rowindex].Cells[5].FindControl("btnMakePaymentTm");

                string[] transID = new_transID.Split('-');

                if (txt_ptitle.Text != "")
                {
                    c_app = ret.getApplicantByID(ret.getTwalletByTransID(transID[0].Trim()).applicantID);
                    ti.item_code = item_code; Session["item_code"] = ti.item_code;
                    ti.hwalletID = transID[2].Trim();
                    ti.amt = amt.ToString();
                    ti.transID = new_transID.Trim();
                    ti.fee_detailsID = transID[1].Trim();
                    ti.product_title = txt_ptitle.Text;
                    ti.xgt = "xpay";
                    ti.xmemberID = adminID;
                    ti.applicant_name = c_app.xname;
                    ti.applicant_addy = c_app.address;
                    ti.applicant_email = c_app.xemail;
                    ti.applicant_no = c_app.xmobile;

                    if ((ti.item_code.ToUpper().Contains("P")) && ((ti.item_code.ToUpper() == "P002") || (ti.item_code.ToUpper() == "P102")))
                    {
                        data.Add("item_code", ti.item_code);
                        data.Add("applicantname", ti.applicant_name);
                        data.Add("applicant_addy", ti.applicant_addy);
                        data.Add("applicant_email", ti.applicant_email);
                        data.Add("applicant_no", ti.applicant_no);
                        data.Add("product_title", ti.product_title);
                        if (Session["Sys_ID"] != null) { data.Add("agent", Session["Sys_ID"].ToString()); }
                        data.Add("transID", ti.transID);
                        data.Add("fee_detailsID", ti.fee_detailsID);
                        data.Add("hwalletID", ti.hwalletID);
                        data.Add("xgt", "xpay");
                        data.Add("amt", amt.ToString());
                        if (Session["cname"] != null) { data.Add("cname", Session["cname"].ToString()); }
                        if (Session["agentemail"] != null) { data.Add("agentemail", Session["agentemail"].ToString()); }
                        if (Session["agentpnumber"] != null) { data.Add("agentpnumber", Session["agentpnumber"].ToString()); }

                        HttpHelper.RedirectAndPOST(this.Page, ConfigurationManager.AppSettings["new_tm_page"], data);

                    }
                    else if ((ti.item_code.ToUpper().Contains("P")) && ((ti.item_code.ToUpper() != "P002") && (ti.item_code.ToUpper() != "P102")))
                    {
                        data.Add("item_code", ti.item_code);
                        data.Add("applicantname", ti.applicant_name);
                        data.Add("applicant_addy", ti.applicant_addy);
                        data.Add("applicant_email", ti.applicant_email);
                        data.Add("applicant_no", ti.applicant_no);
                        data.Add("product_title", ti.product_title);
                        if (Session["Sys_ID"] != null) { data.Add("agent", Session["Sys_ID"].ToString()); }
                        data.Add("transID", ti.transID);
                        data.Add("fee_detailsID", ti.fee_detailsID);
                        data.Add("hwalletID", ti.hwalletID);
                        data.Add("xgt", "xpay");
                        data.Add("amt", amt.ToString());
                        if (Session["cname"] != null) { data.Add("cname", Session["cname"].ToString()); }
                        if (Session["agentemail"] != null) { data.Add("agentemail", Session["agentemail"].ToString()); }
                        if (Session["agentpnumber"] != null) { data.Add("agentpnumber", Session["agentpnumber"].ToString()); }

                        HttpHelper.RedirectAndPOST(this.Page, ConfigurationManager.AppSettings["new_tm_gen_page"], data);
                    }
                }

            }
        }
        protected void gvTmUsed_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            string xid = e.CommandArgument.ToString();
            string transID = gvTmUsed.Rows[rowindex].Cells[3].Text;
            int amt = Convert.ToInt32(gvTm.Rows[rowindex].Cells[4].Text);
            TextBox txt_ptitle = (TextBox)gvTmUsed.Rows[rowindex].Cells[2].FindControl("txt_product_title_tm");
            ImageButton lb = (ImageButton)gvTmUsed.Rows[rowindex].Cells[5].FindControl("btnMakePaymentTmUsed");
        }
        protected void gvTmUsed_Load(object sender, EventArgs e)
        {
            for (int rowindex = 0; rowindex < gvTmUsed.Rows.Count; rowindex++)
            {
                transID = gvTmUsed.Rows[rowindex].Cells[3].Text;
                if (Session["Sys_ID"] != null) { rp.Add("agent", Session["Sys_ID"].ToString()); }
                if (Session["Sys_ID"] != null) { this.lt_pw = this.t.getStageByUserIDAcc(transID, Session["Sys_ID"].ToString()); }
                if (this.lt_pw.Count != 0)
                {
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
                    if (this.lt_pw[0].status == "2b")
                    {
                        this.status = "Search 2";
                        if (lt_pw[0].data_status == "Re-conduct search 1") { data_status = "being re-conducted"; }
                    }
                    if (this.lt_pw[0].status == "3")
                    {
                        this.status = "Search 2";
                        if (lt_pw[0].data_status == "Search Conducted") { data_status = "being processed"; }
                    }
                    if (this.lt_pw[0].status == "3b")
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
                    Label lbl_cur_office = (Label)gvTmUsed.Rows[rowindex].Cells[5].FindControl("lbl_cur_office");
                    Label lbl_cur_status = (Label)gvTmUsed.Rows[rowindex].Cells[6].FindControl("lbl_cur_status");
                    lbl_cur_office.Text = status; lbl_cur_status.Text = data_status;
                }
                else
                {
                    this.status = "N/A";
                }
            }
        }

    }
}