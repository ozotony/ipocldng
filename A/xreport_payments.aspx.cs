using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.IO;
using Ipong.Classes;
using Ipong.InterSwitch.PayDirect.Classes;

namespace Ipong.A
{
    using Ipong.ExcelClasses;
    public partial class xreport_payments : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        protected AppStatus c_as = new AppStatus();
        protected XObjs.Fee_details c_fdets = new XObjs.Fee_details();
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected XObjs.Applicant c_app = new XObjs.Applicant();
        protected string check_trans_page = "";
        protected string coy_name = "";
        protected string cust_id = "";
        public string data_status = "N/A";
        public string docpath = "";
        public ExcelFuncs ef = new ExcelFuncs();
        protected string email = "";
        protected ErrorHandler err = new ErrorHandler();
        protected string from_dt = "0000-01-01";
        protected string fullname = "";
        //protected GridView gvTm;
        protected Hasher hash_value = new Hasher();
        protected XObjs.InterSwitchResponse isr = new XObjs.InterSwitchResponse();
        protected XObjs.InterSwitchPostFields isw_fields = new XObjs.InterSwitchPostFields();
        protected string log_date = "";
        protected List<XObjs.Fee_details> lt_fdets = new List<XObjs.Fee_details>();
        protected List<tm.Stage> lt_pw = new List<tm.Stage>();
        public List<XObjs.ReportItem> lt_ri = new List<XObjs.ReportItem>();
        protected List<XObjs.Twallet> lt_twall = new List<XObjs.Twallet>();
        protected string mobile = "";
        protected string ref_no = "";
        protected Ipong.Classes.Registration reg = new Ipong.Classes.Registration();
        protected Retriever ret = new Retriever();
        protected int show_inv;
        public string status = "N/A";
        protected tm t = new tm();
        protected int tm_cnt;
        protected string to_dt = DateTime.Now.ToString("yyyy-MM-dd");
        protected int tot_amtx;
        protected string transID = "";
        protected Transactions tx = new Transactions();
        public XWriters x = new XWriters();
        public XObjs.InterSwitchPostFields xispf = new XObjs.InterSwitchPostFields();
        protected string xpay_status = "";
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        public StringBuilder xstring = new StringBuilder();
        protected int xtotal_amt;
        protected int amt;
        protected string pay_type = "";

        protected void BtnBackToList_Click(object sender, EventArgs e)
        {
            if ((Session["tm_cnt"] != null) && (Session["tm_cnt"].ToString() != ""))
            {
                tm_cnt = Convert.ToInt32(Session["tm_cnt"]);
            }
            if ((Session["lt_ri"] != null) && (Session["lt_ri"].ToString() != ""))
            {
                lt_ri = (List<XObjs.ReportItem>)Session["lt_ri"];
            }
            gvTm.DataSource = lt_ri;
            gvTm.DataBind();
            show_inv = 0;
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            if ((Session["tm_cnt"] != null) && (Session["tm_cnt"].ToString() != ""))
            {
                tm_cnt = Convert.ToInt32(Session["tm_cnt"]);
            }
            if ((Session["lt_ri"] != null) && (Session["lt_ri"].ToString() != ""))
            {
                lt_ri = (List<XObjs.ReportItem>)Session["lt_ri"];
            }
            gvTm.DataSource = lt_ri;
            gvTm.DataBind();
            show_inv = 0;
         

            docpath = docpath + agentType + "_" + adminID + "_" + DateTime.Now.ToLongTimeString().Replace(" ", "_").Replace(":", "_") + ".xls";
            ef.CreateReportExcel(this, lt_ri, docpath, ddl_mode.SelectedItem.Text + " Payment Report");
        }

        protected void BtnReport_Click(object sender, EventArgs e)
        {
            if (fromDate.Text == "")
            {
                fromDate.Text = from_dt;
            }
            if (toDate.Text == "")
            {
                toDate.Text = to_dt;
            }
            lt_ri = ret.getPaymentReportItem(ddl_cat.SelectedValue, adminID, agentType, ddl_status.SelectedValue, ddl_mode.SelectedValue, fromDate.Text.Trim(), toDate.Text.Trim());
            if (lt_ri.Count > 0)
            {
                tm_cnt = lt_ri.Count;
                Session["tm_cnt"] = tm_cnt;
                foreach (XObjs.ReportItem item in lt_ri)
                {
                  
                    if (lt_pw.Count > 0)
                    {
                        if (ddl_cat.SelectedValue == "tm")
                        {
                            lt_pw = t.getStageByUserIDAccTm(item.newtransID, cust_id);
                           // showTmStatus(lt_pw);
                            SortedList<string, string> x = c_as.showTmStatus(lt_pw[0].status, lt_pw[0].data_status);
                            status = x["status"];
                            data_status = x["data_status"];
                            item.office_status = status;
                            item.data_status = data_status;
                            item.payment_mode = ddl_mode.SelectedItem.Text;
                            item.payment_status = ddl_status.SelectedItem.Text;
                            item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
                            item.tech_amt = string.Format("{0:n}", Convert.ToInt32(item.tech_amt));
                        }
                        else if (ddl_cat.SelectedValue == "pt")
                        {
                            lt_pw = t.getStageByUserIDAccPt(item.newtransID, cust_id);
                           // showPtStatus(lt_pw);
                            SortedList<string, string> x = c_as.showPtStatus(lt_pw[0].status, lt_pw[0].data_status);
                            status = x["status"];
                            data_status = x["data_status"];
                            item.office_status = status;
                            item.data_status = data_status;
                            item.payment_mode = ddl_mode.SelectedItem.Text;
                            item.payment_status = ddl_status.SelectedItem.Text;
                            item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
                            item.tech_amt = string.Format("{0:n}", Convert.ToInt32(item.tech_amt));
                        }
                        else if (ddl_cat.SelectedValue == "ds")
                        {
                            lt_pw = t.getStageByUserIDAccDs(item.newtransID, cust_id);
                            // showPtStatus(lt_pw);
                            SortedList<string, string> x = c_as.showDsStatus(lt_pw[0].status, lt_pw[0].data_status);
                            status = x["status"];
                            data_status = x["data_status"];
                            item.office_status = status;
                            item.data_status = data_status;
                            item.payment_mode = ddl_mode.SelectedItem.Text;
                            item.payment_status = ddl_status.SelectedItem.Text;
                            item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
                            item.tech_amt = string.Format("{0:n}", Convert.ToInt32(item.tech_amt));
                        }
                    }
                    else
                    {
                        item.office_status = status;
                        item.data_status = data_status;
                        item.payment_mode = ddl_mode.SelectedItem.Text;
                        item.payment_status = ddl_status.SelectedItem.Text;
                        item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
                        item.tech_amt = string.Format("{0:n}", Convert.ToInt32(item.tech_amt));
                    }
                }
                Session["lt_ri"] = lt_ri;
                gvTm.DataSource = lt_ri;
                gvTm.DataBind();
                if (ddl_mode.SelectedValue == "xpay_isw")
                {
                    if (ddl_status.SelectedValue == "1")
                    {
                        gvTm.Columns[6].Visible = false;
                        gvTm.Columns[7].Visible = false;
                        gvTm.Columns[8].Visible = false;
                    }
                    if (ddl_status.SelectedValue == "2")
                    {
                        gvTm.Columns[6].Visible = true;
                        gvTm.Columns[7].Visible = false;
                        gvTm.Columns[8].Visible = true;
                    }
                    if (ddl_status.SelectedValue == "3")
                    {
                        gvTm.Columns[6].Visible = false;
                        gvTm.Columns[7].Visible = true;
                        gvTm.Columns[8].Visible = false;
                    }
                }
                else
                {
                    if (ddl_status.SelectedValue == "1")
                    {
                        gvTm.Columns[6].Visible = false;
                        gvTm.Columns[7].Visible = false;
                        gvTm.Columns[8].Visible = false;
                    }
                    if (ddl_status.SelectedValue == "2")
                    {
                        gvTm.Columns[6].Visible = true;
                        gvTm.Columns[7].Visible = false;
                        gvTm.Columns[8].Visible = false;
                    }
                }
            }
        }

        protected void gvTm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTm.PageIndex = e.NewPageIndex;
            if ((Session["tm_cnt"] != null) && (Session["tm_cnt"].ToString() != ""))
            {
                tm_cnt = Convert.ToInt32(Session["tm_cnt"]);
            }
            if ((Session["lt_ri"] != null) && (Session["lt_ri"].ToString() != ""))
            {
                lt_ri = (List<XObjs.ReportItem>)Session["lt_ri"];
            }
            gvTm.DataSource = lt_ri;
            gvTm.DataBind();
            show_inv = 0;
        }

        protected void gvTm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if ((Session["tm_cnt"] != null) && (Session["tm_cnt"].ToString() != ""))
            {
                tm_cnt = Convert.ToInt32(Session["tm_cnt"]);
            }
            if ((Session["lt_ri"] != null) && (Session["lt_ri"].ToString() != ""))
            {
                lt_ri = (List<XObjs.ReportItem>)Session["lt_ri"];
            }
            gvTm.DataSource = lt_ri;
            gvTm.DataBind();
            if (e.CommandName == "TmPayClick")
            {
                GridViewRow namingContainer = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string txnref = e.CommandArgument.ToString();
                xispf = ret.getISWtransactionByTransactionID(txnref);
                if ((xispf.xid != null) && (xispf.xid != null))
                {
                    Session["xispf"] = xispf;
                    base.Response.Redirect("../xis/pd/tx/re_payment_details.aspx");
                }
            }
            if (e.CommandName == "TmReqClick")
            {
                string str2 = DateTime.Now.ToString("dd-MMM-yy HH:MM:ss");
                GridViewRow row2 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int num2 = row2.RowIndex;
                string str3 = e.CommandArgument.ToString();
                int num = 0;
                isw_fields = ret.getISWtransactionByTransactionID(str3);
                string inputString = isw_fields.product_id + isw_fields.txn_ref + isw_fields.mackey;
                string headerValue = hash_value.GetGetSHA512String(inputString);
                isr = tx.myRedirect(check_trans_page + "?productid=" + isw_fields.product_id + "&transactionreference=" + isw_fields.txn_ref + "&amount=" + isw_fields.amount, "Hash", headerValue);
                if ((((isr.ResponseCode != "") && (isr.ResponseCode != null)) && ((isr.PaymentReference != "") && (isr.PaymentReference != null))) && ((isr.ResponseDescription != "") && (isr.ResponseDescription != null)))
                {
                    if (isw_fields.txn_ref == null)
                    {
                        isw_fields.txn_ref = "";
                    }
                    if (isw_fields.pay_ref == null)
                    {
                        isw_fields.pay_ref = "";
                    }
                    if (isw_fields.ret_ref == null)
                    {
                        isw_fields.ret_ref = "";
                    }
                    if (isr.ResponseCode == null)
                    {
                        isr.ResponseCode = "";
                    }
                    if (isr.TransactionDate == null)
                    {
                        isr.TransactionDate = "";
                    }
                    if (isr.MerchantReference == null)
                    {
                        isr.MerchantReference = "";
                    }
                    if (isr.ResponseDescription == null)
                    {
                        isr.ResponseDescription = "";
                    }
                    xstring.AppendLine("Sent Amount: " + isw_fields.amount + "\r\n Product ID: " + isw_fields.product_id + "\r\n Hash: " + headerValue + "\r\n Amount: " + isr.Amount + "\r\n CardNumber: " + isr.CardNumber + "\r\n MerchantReference: " + isr.MerchantReference + "\r\n PaymentReference: " + isr.PaymentReference + "\r\n RetrievalReferenceNumber: " + isr.RetrievalReferenceNumber + "\r\n LeadBankCbnCode: " + isr.LeadBankCbnCode + "\r\n TransactionDate: " + isr.TransactionDate + "\r\n ResponseCode: " + isr.ResponseCode + "\r\n ResponseDescription: " + isr.ResponseDescription + "\r\n Json Page: " + check_trans_page);
                    if (isw_fields.txn_ref != "")
                    {
                        docpath = base.Server.MapPath("~/") + "LiveInterLogs/Ag/" + isw_fields.txn_ref + ".txt";
                        num = x.WriteToFile(xstring.ToString(), docpath);
                    }
                    else
                    {
                        docpath = base.Server.MapPath("~/") + "LiveInterLogs/Ag/xxx.txt";
                        num = x.WriteToFile(xstring.ToString(), docpath);
                    }
                    if ((isr.ResponseCode == "") || (isr.ResponseCode == null))
                    {
                        xpay_status = "2";
                    }
                    else if (isr.ResponseCode == "00")
                    {
                        xpay_status = "1";
                    }
                    else
                    {
                        xpay_status = "3";
                    }
                    num = reg.updateInterSwitchRecords(isw_fields.txn_ref, isw_fields.pay_ref, isw_fields.ret_ref, isr.ResponseCode, isr.TransactionDate, isr.MerchantReference, isr.ResponseDescription);
                    reg.updateTwalletPaymentStatus(isw_fields.txn_ref, xpay_status);
                    reg.updateInterSwitchPostFieldsDate(isw_fields.txn_ref, str2);
                    base.Response.Redirect("./xreport_payments.aspx");
                    if (num == 0)
                    {
                    }
                }
                else
                {
                    base.Response.Redirect("./xreport_payments.aspx");
                }
            }
            if (e.CommandName == "TmDeleteClick")
            {
                GridViewRow row3 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int num3 = row3.RowIndex;
                string transID = e.CommandArgument.ToString();
                reg.updateInterSwitchVisibleStatus(transID, "0");
                base.Response.Redirect("./xreport_payments.aspx");
            }
            if (e.CommandName == "TmDetailsClick")
            {
                GridViewRow row4 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int num4 = row4.RowIndex;
                string str7 = e.CommandArgument.ToString();
                lt_twall = ret.getTwalletByMemberID(adminID, str7, Session["agentType"].ToString());
                //if (lt_twall.Count > 0) {  }

                if (lt_twall.Count > 0)
                {
                    isw_fields = ret.getISWtransactionByTransactionID(lt_twall[0].transID);
                    isw_fields.TransactionDate = isw_fields.TransactionDate.Substring(0, 11).Trim();

                    c_app = ret.getApplicantByID(lt_twall[0].applicantID);
                    string str8 = lt_twall[0].xpay_status;
                    lt_fdets = ret.getFee_detailsByTwalletID(lt_twall[0].xid);
                    Session["transID"] = str7;
                    Session["memberID"] = adminID;
                    show_inv = 1;
                    switch (str8)
                    {
                        case "0":
                            str8 = "PAYMENT UNCOMPLETED!!!";
                            return;

                        case "1":
                            str8 = "PAYMENT SUCCESSFUL!!!";
                            return;

                        case "2":
                            str8 = "PAYMENT PENDING!!!";
                            return;

                        case "3":
                            str8 = "PAYMENT UNSUCCESSFUL!!!";
                            break;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            {
                adminID = Session["pwalletID"].ToString();
            }
            else
            {
                base.Response.Redirect("../a_login.aspx");
            }
            if ((Session["log_date"] != null) && (Session["log_date"].ToString() != ""))
            {
                log_date = Session["log_date"].ToString();
            }
            if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
            {
                agentType = Session["agentType"].ToString();
                if (agentType == "Agent")
                {
                    if (Session["c_reg"] != null)
                    {
                        c_reg = (XObjs.Registration)Session["c_reg"];
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
                    XObjs.Registration registration = new XObjs.Registration();
                    if (Session["c_sub"] != null)
                    {
                        c_sub = (XObjs.Subagent)Session["c_sub"];
                        fullname = c_sub.Firstname + " " + c_sub.Surname;
                        email = c_sub.Email;
                        mobile = c_sub.Telephone;
                    }
                    if (Session["c_sub_reg"] != null)
                    {
                        registration = (XObjs.Registration)Session["c_sub_reg"];
                        coy_name = registration.CompanyName;
                        cust_id = registration.Sys_ID + "_" + c_sub.AssignID;
                    }
                    Session["coy_name"] = coy_name;
                    Session["fullname"] = fullname;
                    Session["email"] = email;
                    Session["mobile"] = mobile;
                    Session["c_addy"] = registration.CompanyAddress;
                }
            }
        }

      
    }
}