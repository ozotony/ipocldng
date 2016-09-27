using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Ipong.A
{
    public partial class xstatus_payments : System.Web.UI.Page
    {
        protected string adminID = "0"; protected string agentType = ""; protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected string xsearch = ""; protected string payment_status = "";protected int show_receipt = 0; protected int show_search = 1;
        protected string xgt_type = ""; protected int xtotal_amt = 0; protected string log_date = ""; 
        protected string fullname = ""; protected string email = ""; protected string mobile = "";

        protected string transID = "";protected string coy_name = "";protected string cust_id = "";  public string total_amt = "0";

        protected Classes.Retriever ret = new Classes.Retriever();
        protected Classes.Registration reg = new Classes.Registration();

        protected Classes.XObjs.Applicant c_app = new Classes.XObjs.Applicant();
        protected Classes.XObjs.Registration c_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Registration c_sub_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Subagent c_sub = new Classes.XObjs.Subagent();

        protected Classes.XObjs.Twallet c_twall = new Classes.XObjs.Twallet();
        protected List<Classes.XObjs.Fee_details> lt_fdets = new List<Classes.XObjs.Fee_details>();
        protected List<Classes.XObjs.Hwallet> lt_hwall = new List<Classes.XObjs.Hwallet>();
        protected List<Classes.XObjs.PaymentReciept> lt_pr = new List<Classes.XObjs.PaymentReciept>();

        protected int tm_cnt = 0; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            { this.adminID = Session["pwalletID"].ToString(); }
            else
            { base.Response.Redirect("../a_login.aspx"); }

            if ((Session["log_date"] != null) && (Session["log_date"].ToString() != "")) { this.log_date = Session["log_date"].ToString(); }   

            xadminID.Value = adminID;
            if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
            {
                agentType = Session["agentType"].ToString();
                tm_cnt = ret.getPaidFee_detail_ItemsCntByCatBk(adminID, "ag", "2", agentType);
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
            xsearch = txt_status.Text.Trim();
            c_twall = ret.getTwalletByTransIDAdminID(xsearch, adminID,agentType);
            c_app = ret.getApplicantByID(c_twall.applicantID);

            if (c_twall.xid != null)
            {
                if (c_twall.xgt == "xpay_bk") { xgt_type = "Bank"; }
                else if (c_twall.xgt == "xpay_isw") { xgt_type = "Inter switch"; }
                else  { xgt_type = "None"; }

                if (c_twall.xpay_status == "1") { payment_status = "Paid"; }
                else if (c_twall.xpay_status == "2") { payment_status = "Pending"; }
                else { payment_status = "Failed"; }

                Session["c_twall"] = c_twall;
                lt_fdets = ret.getFee_detailsByTwalletID(c_twall.xid);
                if (lt_fdets.Count > 0)
                {
                    Session["lt_fdets"] = lt_fdets;
                }
                lt_hwall = ret.getHwalletByTransID(xsearch);

                int i = 1; int amt = 0;
                foreach (Classes.XObjs.Hwallet h in lt_hwall)
                {
                    Classes.XObjs.PaymentReciept pr = new Classes.XObjs.PaymentReciept();
                    Classes.XObjs.Fee_list fl = new Classes.XObjs.Fee_list();
                    fl = ret.getFee_listByID(ret.getFee_detailsByID(h.fee_detailsID).fee_listID);

                    pr.sn = i.ToString();
                    pr.item_code = fl.item_code;
                    pr.item_desc = fl.xdesc;
                    int amount = Convert.ToInt32(fl.init_amt) + Convert.ToInt32(fl.tech_amt);
                    pr.amount = string.Format("{0:n}", amount);
                    pr.transID = h.transID + "-" + h.fee_detailsID + "-" + h.xid;
                    amt += Convert.ToInt32(amount);
                    total_amt = string.Format("{0:n}", amt);

                    lt_pr.Add(pr);
                    i++;
                }
                show_receipt = 1; show_search = 0;
            }

            else
            {
              show_receipt = 0; show_search = 1;
            }

        }

        protected void btnNewSearch_Click(object sender, EventArgs e)
        {
            txt_status.Text = "";
            show_receipt = 0; show_search = 1;
        }
              

    }
}