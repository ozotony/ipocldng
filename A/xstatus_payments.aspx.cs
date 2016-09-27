using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Ipong.A
{
    using Ipong.Classes;
    public partial class xstatus_payments : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected string xsearch = "";
        protected string payment_status = "";
        protected int show_receipt;
        protected int show_search = 1;
        protected string xgt_type = "";
        protected int xtotal_amt;
        protected string log_date = "";
        public double amt;
        protected string fullname = "";
        protected string email = "";
        protected string mobile = "";
        protected string transID = "";
        protected string coy_name = "";
        protected string cust_id = "";
        public string total_amt = "0";
        protected Retriever ret = new Retriever();
        protected Registration reg = new Registration();
        protected XObjs.Applicant c_app = new XObjs.Applicant();
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Twallet c_twall = new XObjs.Twallet();
        protected List<XObjs.Fee_details> lt_fdets = new List<XObjs.Fee_details>();
        protected List<XObjs.Hwallet> lt_hwall = new List<XObjs.Hwallet>();
        protected List<XObjs.PaymentReciept> lt_pr = new List<XObjs.PaymentReciept>();
        protected XObjs.InterSwitchPostFields isw_fields = new XObjs.InterSwitchPostFields();
        protected int tm_cnt;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.adminID = this.Session["pwalletID"].ToString();
            this.log_date = this.Session["log_date"].ToString();

            if (this.Session["log_date"] != null && this.Session["log_date"].ToString() != "" && this.Session["exp_date"] != null && this.Session["exp_date"].ToString() != "")
            {               
                if (!this.ret.DoLogOutTimer(Convert.ToDateTime(this.Session["exp_date"].ToString())))
                {
                    this.Session["pwalletID"] = null;
                    base.Response.Redirect("../a_login.aspx");
                }
                else
                {
                    this.adminID = this.Session["pwalletID"].ToString();
                    this.log_date = this.Session["log_date"].ToString();
                }
            }
            this.xadminID.Value = this.adminID;
            if (this.Session["agentType"] != null && this.Session["agentType"].ToString() != "")
            {
                this.agentType = this.Session["agentType"].ToString();
                this.tm_cnt = this.ret.getPaidFee_detail_ItemsCntByCatBk(this.adminID, "ag", "2", this.agentType);
                if (!(this.agentType == "Agent"))
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
                    return;
                }
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
                    return;
                }
            }
            else
            {
                base.Response.Redirect("../a_login.aspx");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.xsearch = this.txt_status.Text.Trim();
            string txnref;
            if (this.xsearch.Contains("-"))
            {
                txnref = this.xsearch.Trim().Split(new char[]
				{
					'-'
				})[0];
            }
            else
            {
                txnref = this.xsearch;
            }
            this.c_twall = this.ret.getTwalletByTransIDAdminID(txnref, this.adminID, this.agentType);
            this.c_app = this.ret.getApplicantByID(this.c_twall.applicantID);
            if (this.c_twall.xid != null)
            {
                if (this.c_twall.xgt == "xpay_bk")
                {
                    this.xgt_type = "Bank";
                }
                else
                {
                    if (this.c_twall.xgt == "xpay_isw")
                    {
                        this.xgt_type = "Inter switch";
                    }
                    else
                    {
                        this.xgt_type = "None";
                    }
                }
                if (this.c_twall.xpay_status == "1")
                {
                    this.payment_status = "Paid";
                }
                else
                {
                    if (this.c_twall.xpay_status == "2")
                    {
                        this.payment_status = "Pending";
                    }
                    else
                    {
                        this.payment_status = "Failed";
                    }
                }
                this.Session["c_twall"] = this.c_twall;
                this.lt_fdets = this.ret.getFee_detailsByTwalletID(this.c_twall.xid);
                if (this.lt_fdets.Count > 0)
                {
                    this.Session["lt_fdets"] = this.lt_fdets;
                }
                this.lt_hwall = this.ret.getHwalletByTransID(txnref);
                this.isw_fields = this.ret.getISWtransactionByTransactionID(txnref);
                this.isw_fields.TransactionDate = this.isw_fields.TransactionDate.Substring(0, 11).Trim();
                int num = 1;
                int num2 = 0;
                foreach (XObjs.Hwallet current in this.lt_hwall)
                {
                    XObjs.PaymentReciept paymentReciept = new XObjs.PaymentReciept();
                    XObjs.Fee_list fee_list = new XObjs.Fee_list();
                    fee_list = this.ret.getFee_listByID(this.ret.getFee_detailsByID(current.fee_detailsID).fee_listID);
                    paymentReciept.sn = num.ToString();
                    paymentReciept.item_code = fee_list.item_code;
                    paymentReciept.item_desc = fee_list.xdesc;
                    int num3 = Convert.ToInt32(fee_list.init_amt) + Convert.ToInt32(fee_list.tech_amt);
                    paymentReciept.amount = string.Format("{0:n}", num3);
                    paymentReciept.transID = string.Concat(new string[]
					{
						current.transID,
						"-",
						current.fee_detailsID,
						"-",
						current.xid
					});
                    num2 += Convert.ToInt32(num3);
                    this.lt_pr.Add(paymentReciept);
                    num++;
                }
                this.total_amt = string.Format("{0:n}", (double)num2 + Math.Round(Convert.ToDouble(this.isw_fields.isw_conv_fee), 2));
                this.show_receipt = 1;
                this.show_search = 0;
                return;
            }
            this.show_receipt = 0;
            this.show_search = 1;
        }

        protected void btnNewSearch_Click(object sender, EventArgs e)
        {
            this.txt_status.Text = "";
            this.show_receipt = 0;
            this.show_search = 1;
        }
    }
}