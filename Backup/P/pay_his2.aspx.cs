using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Ipong.P
{
    public partial class pay_his2 : System.Web.UI.Page
    {
        protected string adminID = "3"; protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int show_inv = 0; protected int xtotal_amt = 0; protected string agentType = "";
        protected string fullname = ""; protected string email = ""; protected string mobile = "";
        protected string transID = ""; protected string ref_no = ""; protected string check_trans_page = "";
        protected int tot_amtx = 0; protected string coy_name = ""; protected string cust_id = "";

        protected int show_details_grid = 0; protected int show_banker_grid = 0; protected int show_isw_grid = 0;
        protected int grand_tot_cnt = 0;  protected int grand_tot_amt = 0; protected int tm_cnt = 0;
        protected string new_grand_tot_amt = ""; protected string search_msg = "";


        protected InterSwitch.PayDirect.Classes.ErrorHandler err = new InterSwitch.PayDirect.Classes.ErrorHandler();
        protected InterSwitch.PayDirect.Classes.Transactions tx = new InterSwitch.PayDirect.Classes.Transactions();
        protected InterSwitch.PayDirect.Classes.Hasher hash_value = new InterSwitch.PayDirect.Classes.Hasher();
        protected Classes.XObjs.InterSwitchPostFields isw_fields = new Classes.XObjs.InterSwitchPostFields();
        protected Ipong.Classes.XObjs.InterSwitchResponse isr = new Ipong.Classes.XObjs.InterSwitchResponse();

        protected Classes.XObjs.Registration c_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Registration c_sub_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Subagent c_sub = new Classes.XObjs.Subagent();
        protected Classes.XObjs.XPartner c_partner = new Classes.XObjs.XPartner();
        protected Classes.XObjs.PRatio c_pr = new Classes.XObjs.PRatio();

        protected Classes.XObjs.Fee_details c_fdets = new Classes.XObjs.Fee_details();
        protected List<Classes.XObjs.Twallet> lt_twall = new List<Classes.XObjs.Twallet>();
        protected List<Classes.XObjs.Fee_details> lt_fdets = new List<Classes.XObjs.Fee_details>();

        protected Classes.Retriever ret = new Classes.Retriever();
        protected Classes.Registration reg = new Classes.Registration();



        protected void Page_Load(object sender, EventArgs e)
        {
            c_pr = ret.getPratioByMemberID(adminID);
            if (c_pr.xid != null)
            {
                if (c_pr.p_type == "merchant")
                {

                }
                else
                {

                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            xadminID.Value = adminID;
            if (fromDate.Text == "") { xfromDate.Value = "0000-01-01"; } else { xfromDate.Value = fromDate.Text; }
            
           
            if (toDate.Text != "")
            {
                xtoDate.Value = toDate.Text;
                grand_tot_cnt = ret.getCntTotalTransAdmin(fromDate.Text, toDate.Text);
                grand_tot_amt = ret.getSumTotalTransMerchant(fromDate.Text, toDate.Text);
                new_grand_tot_amt = string.Format("{0:n}", grand_tot_amt);
                //LoadAgent();
                show_inv = 1;
            }
            else
            {
                show_inv = 0;
                search_msg = "";
                search_msg = "THE \"TO\" DATE FIELD CANNOT BE EMPTY, PLEASE SELECT A DATE AND TRY AGAIN!!";
            }
           
           
        }

        protected void gvTm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "TmPayClick")
            {
                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;
                string og_transID = e.CommandArgument.ToString(); int succ = 0;

                isw_fields = ret.getISWtransactionByTransactionID(og_transID);
                //Get the hash value
                // string get_trans_hash = hash_value.GetGetSHA512String(inputString);

                isr = tx.myRedirect(check_trans_page + "?productid=" + isw_fields.product_id + "&transactionreference=" + isw_fields.txn_ref + "&amount=" + isw_fields.amount, "Hash", isw_fields.hash);

                string xpay_status = "";
                succ = reg.updateInterSwitchRecords(isw_fields.txn_ref, isw_fields.pay_ref, isw_fields.ret_ref, isr.ResponseCode, isr.TransactionDate, isr.MerchantReference, isr.ResponseDescription);
                if ((isr.ResponseCode == "") || (isr.ResponseCode == null))
                {
                    xpay_status = "2";//Pending
                }
                else if (isr.ResponseCode == "00")
                {
                    xpay_status = "1";//Successfull
                }
                else
                {
                    xpay_status = "3";//Error
                }
                string new_local_date = DateTime.Now.ToString("dd-MMM-yy HH:MM:ss");
                reg.updateInterSwitchPostFieldsDate(isw_fields.txn_ref, new_local_date);
                Response.Redirect("./v_bask_tmp.aspx");
                if (succ != 0)
                {
                    // sendAlert();                
                }

            }

            if (e.CommandName == "TmDeleteClick")
            {
                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;
                string og_transID = e.CommandArgument.ToString(); int succ = 0;
                succ = reg.updateInterSwitchVisibleStatus(og_transID, "0");

                Response.Redirect("./v_bask_tmp.aspx");
                //if (succ != 0){    sendAlert();    }
            }

            if (e.CommandName == "TmDetailsClick")
            {
                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;
                string tnxref = e.CommandArgument.ToString();
                lt_twall = ret.getTwalletByMemberID(adminID, tnxref, Session["agentType"].ToString());
                if (lt_twall.Count > 0)
                {
                    string pay_status = lt_twall[0].xpay_status;
                    lt_fdets = ret.getFee_detailsByTwalletID(lt_twall[0].xid);

                    Session["transID"] = tnxref; Session["memberID"] = adminID;
                    show_inv = 1;
                    if (pay_status == "0")
                    { pay_status = "PAYMENT UNCOMPLETED!!!"; }
                    else if (pay_status == "1") { pay_status = "PAYMENT SUCCESSFUL!!!"; }
                    else if (pay_status == "2") { pay_status = "PAYMENT PENDING!!!"; }
                    else if (pay_status == "3") { pay_status = "PAYMENT UNSUCCESSFUL!!!"; }

                }
            }

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void LoadAgent()
        {
            string trans = "";
            for (int rowindex = 0; rowindex < gvTm.Rows.Count; rowindex++)
            {
                trans = gvTm.Rows[rowindex].Cells[1].Text;
                trans = trans.Remove(0, trans.LastIndexOf('-') + 1);
            }
        }
      
    }
}