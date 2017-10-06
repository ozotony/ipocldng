using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;
using Ipong.Classes;

namespace Ipong.A
{
    public partial class v_bask_pt : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        protected XObjs.Applicant c_app = new XObjs.Applicant();
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected NameValueCollection data = new NameValueCollection();
        protected string data_status = "";
        protected string log_date = "";
        protected List<tm.Stage> lt_pw = new List<tm.Stage>();
        protected string mobile = "";
        protected string ref_no = "";
        private Retriever ret = new Retriever();
        protected RemotePost rp = new RemotePost();
        protected int show_inv;
        protected string status = "";
        protected tm t = new tm();
        protected XObjs.Trademark_item ti = new XObjs.Trademark_item();
        protected string transID = "";
        protected int unused_cnt;
        protected int viz;
        protected SortedList<string, string> xheaders = new SortedList<string, string>();
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int xtotal_amt;
        protected string postwith = "";
        protected string agent = "";
        protected string agentemail = "";
        protected string agentpnumber = "";
        protected string agentname = "";
        public string pwalletid = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            Session["Trademark_item"] = null;
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            {
                adminID = Session["pwalletID"].ToString();
                Session["pwalletID2"] = adminID;
                xadminID.Value = adminID;
              //  xadminID.Value = "166";
                pwalletid = adminID;
                if (!IsPostBack)
                {

                    Session["fee_detailsID"] = null;


                    if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
                    {
                        agentType = Session["agentType"].ToString();
                        unused_cnt = ret.getPaidUsedCntByCat(adminID, "pt", "1", agentType, "Not Used"); Session["unused_cnt"] = unused_cnt;
                        if (agentType == "Agent")
                        {
                            c_reg = ret.getRegistrationByID(adminID);
                            Session["c_reg"] = c_reg;
                            Session["coy_name"] = c_reg.CompanyName;
                            Session["agentemail"] = c_reg.Email;
                            Session["agentpnumber"] = c_reg.PhoneNumber;
                            Session["Sys_ID"] = c_reg.Sys_ID;
                            Session["cname"] = c_reg.CompanyName;
                        }
                        else
                        {
                            c_sub = ret.getSubAgentByID(adminID);
                            Session["c_sub"] = c_sub;
                            c_sub_reg = ret.getRegistrationBySubagentRegistrationID(c_sub.RegistrationID);
                            Session["c_sub_reg"] = c_sub_reg;
                            Session["coy_name"] = c_sub_reg.CompanyName;
                            Session["agentemail"] = c_sub.Email;
                            Session["agentpnumber"] = c_sub.Telephone;
                            Session["Sys_ID"] = c_sub.Sys_ID;
                            Session["cname"] = c_sub_reg.CompanyName;
                        }
                    }
                    else { Response.Redirect("../a_login.aspx"); }
                }
                else
                {
                    if (Session["unused_cnt"] != null) { unused_cnt = Convert.ToInt32(Session["unused_cnt"]); }
                }


            }
            else
            { Response.Redirect("../a_login.aspx"); }
            if ((Session["log_date"] != null) && (Session["log_date"].ToString() != ""))
            { log_date = Session["log_date"].ToString(); }
        }

        protected void gvTm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "TmStatusClick")
            {
                GridViewRow namingContainer = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                e.CommandArgument.ToString();
                string item_code = gvTm.Rows[rowIndex].Cells[1].Text;
                string transID = gvTm.Rows[rowIndex].Cells[4].Text;
                string f_amt = gvTm.Rows[rowIndex].Cells[5].Text; f_amt = f_amt.Replace(",", "").Substring(0, f_amt.IndexOf(".") - 1);
                int amt = Convert.ToInt32(f_amt);
                TextBox product_title_tm = (TextBox)gvTm.Rows[rowIndex].Cells[3].FindControl("txt_product_title_tm");
                ImageButton btnMakePaymentTm = (ImageButton)gvTm.Rows[rowIndex].Cells[6].FindControl("btnMakePaymentTm");
                string[] strArray = transID.Split(new char[] { '-' });
                if (product_title_tm.Text != "")
                {
                    c_app = ret.getApplicantByID(ret.getTwalletByTransID(strArray[0].Trim()).applicantID);

                    ti.item_code = item_code; Session["item_code"] = ti.item_code; Session["pc"] = ti.item_code; ti.pc = ti.item_code;
                    ti.hwalletID = strArray[2].Trim();
                    ti.amt = amt.ToString();
                    ti.transID = transID.Trim();
                    ti.fee_detailsID = strArray[1].Trim();
                    ti.product_title = product_title_tm.Text;
                    ti.xgt = "xpay";
                    ti.xmemberID = adminID;
                    ti.applicant_name = c_app.xname;
                    ti.applicant_addy = c_app.address;
                    ti.applicant_email = c_app.xemail;
                    ti.applicant_no = c_app.xmobile;
                    ti.vid = transID;
                    if (Session["Sys_ID"] != null) { agent = Session["Sys_ID"].ToString(); ti.aid = agent; ti.agent = agent; }
                    if (Session["cname"] != null) { agentname = Session["cname"].ToString(); ti.agentname = agentname; }
                    if (Session["agentemail"] != null) { agentemail = Session["agentemail"].ToString(); ti.agentemail = agentemail; }
                    if (Session["agentpnumber"] != null) { agentpnumber = Session["agentpnumber"].ToString(); ti.agentpnumber = agentpnumber; }

                    Session["Trademark_item"] = ti;

                    Response.Redirect("confirm_basket_details.aspx");
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("v_bask_ptu.aspx");
        }

    }
}