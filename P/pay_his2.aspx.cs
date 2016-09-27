using Ipong.Classes;
using Ipong.InterSwitch.PayDirect.Classes;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Ipong.P
{
    public partial class pay_his2 : Page
    {
        protected string adminID = "3";
        protected string agentType = "";
        protected XObjs.Fee_details c_fdets = new XObjs.Fee_details();
        protected XObjs.XPartner c_partner = new XObjs.XPartner();
        protected XObjs.PRatio c_pr = new XObjs.PRatio();
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected string check_trans_page = "";
        protected string coy_name = "";
        protected string cust_id = "";
        protected string email = "";
        protected ErrorHandler err = new ErrorHandler();
        protected string fullname = "";
        protected int grand_tot_amt;
        protected int grand_tot_cnt;
        protected Hasher hash_value = new Hasher();
        protected XObjs.InterSwitchResponse isr = new XObjs.InterSwitchResponse();
        protected XObjs.InterSwitchPostFields isw_fields = new XObjs.InterSwitchPostFields();
        protected List<XObjs.Fee_details> lt_fdets = new List<XObjs.Fee_details>();
        protected List<XObjs.Twallet> lt_twall = new List<XObjs.Twallet>();
        protected string mobile = "";
        protected string new_grand_tot_amt = "";
        protected string ref_no = "";
        protected Ipong.Classes.Registration reg = new Ipong.Classes.Registration();
        protected Retriever ret = new Retriever();
        protected string search_msg = "";
        protected int show_banker_grid;
        protected int show_details_grid;
        protected int show_inv;
        protected int show_isw_grid;
        protected int tm_cnt;
        protected int tot_amtx;
        protected string transID = "";
        protected Transactions tx = new Transactions();
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int xtotal_amt;

        protected void btnBack_Click(object sender, EventArgs e)
        {
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.xadminID.Value = this.adminID;
            if (this.fromDate.Text == "")
            {
                this.xfromDate.Value = "0000-01-01";
            }
            else
            {
                this.xfromDate.Value = this.fromDate.Text;
            }
            if (this.toDate.Text != "")
            {
                this.xtoDate.Value = this.toDate.Text;
                this.grand_tot_cnt = this.ret.getCntTotalTransAdmin(this.fromDate.Text, this.toDate.Text);
                this.grand_tot_amt = this.ret.getSumTotalTransMerchant(this.fromDate.Text, this.toDate.Text);
                this.new_grand_tot_amt = string.Format("{0:n}", this.grand_tot_amt);
                this.show_inv = 1;
            }
            else
            {
                this.show_inv = 0;
                this.search_msg = "";
                this.search_msg = "THE \"TO\" DATE FIELD CANNOT BE EMPTY, PLEASE SELECT A DATE AND TRY AGAIN!!";
            }
        }

        protected void gvTm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "TmPayClick")
            {
                GridViewRow namingContainer = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string txnref = e.CommandArgument.ToString();
                int num = 0;
                this.isw_fields = this.ret.getISWtransactionByTransactionID(txnref);
                this.isr = this.tx.myRedirect(this.check_trans_page + "?productid=" + this.isw_fields.product_id + "&transactionreference=" + this.isw_fields.txn_ref + "&amount=" + this.isw_fields.amount, "Hash", this.isw_fields.hash);
                num = this.reg.updateInterSwitchRecords(this.isw_fields.txn_ref, this.isw_fields.pay_ref, this.isw_fields.ret_ref, this.isr.ResponseCode, this.isr.TransactionDate, this.isr.MerchantReference, this.isr.ResponseDescription);
                if (!(this.isr.ResponseCode == "") && (this.isr.ResponseCode != null))
                {
                    bool flag1 = this.isr.ResponseCode == "00";
                }
                string str2 = DateTime.Now.ToString("dd-MMM-yy HH:MM:ss");
                this.reg.updateInterSwitchPostFieldsDate(this.isw_fields.txn_ref, str2);
                base.Response.Redirect("./v_bask_tmp.aspx");
            }
            if (e.CommandName == "TmDeleteClick")
            {
                GridViewRow row2 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int num2 = row2.RowIndex;
                string transID = e.CommandArgument.ToString();
                this.reg.updateInterSwitchVisibleStatus(transID, "0");
                base.Response.Redirect("./v_bask_tmp.aspx");
            }
            if (e.CommandName == "TmDetailsClick")
            {
                GridViewRow row3 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int num3 = row3.RowIndex;
                string str4 = e.CommandArgument.ToString();
                this.lt_twall = this.ret.getTwalletByMemberID(this.adminID, str4, this.Session["agentType"].ToString());
                if (this.lt_twall.Count > 0)
                {
                    string str5 = this.lt_twall[0].xpay_status;
                    this.lt_fdets = this.ret.getFee_detailsByTwalletID(this.lt_twall[0].xid);
                    this.Session["transID"] = str4;
                    this.Session["memberID"] = this.adminID;
                    this.show_inv = 1;
                    switch (str5)
                    {
                        case "0":
                            str5 = "PAYMENT UNCOMPLETED!!!";
                            return;

                        case "1":
                            str5 = "PAYMENT SUCCESSFUL!!!";
                            return;

                        case "2":
                            str5 = "PAYMENT PENDING!!!";
                            return;

                        case "3":
                            str5 = "PAYMENT UNSUCCESSFUL!!!";
                            break;
                    }
                }
            }
        }

        protected void LoadAgent()
        {
            string text = "";
            for (int i = 0; i < this.gvTm.Rows.Count; i++)
            {
                text = this.gvTm.Rows[i].Cells[1].Text;
                text = text.Remove(0, text.LastIndexOf('-') + 1);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.c_pr = this.ret.getPratioByMemberID(this.adminID);
            if (this.c_pr.xid != null)
            {
                bool flag1 = this.c_pr.p_type == "merchant";
            }
        }
    }
}