﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.IO;
//using System.Globalization;
using System.Windows.Forms;

namespace Ipong.A
{
    using Ipong.ExcelClasses;
    public partial class xreport_payments : System.Web.UI.Page
    {
        protected string adminID = "0"; protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int show_inv = 0; protected int xtotal_amt = 0; protected string agentType = "";
        protected string fullname = ""; protected string email = ""; protected string mobile = "";
        protected string transID = ""; protected string ref_no = ""; protected string check_trans_page = "";
        protected int tot_amtx = 0; protected string coy_name = ""; protected string cust_id = "";
        public string status = "N/A"; public string data_status = "N/A";
        protected string from_dt = "0000-01-01"; protected string to_dt = DateTime.Now.ToString("yyyy-MM-dd"); protected string xpay_status = "";
        protected string log_date = ""; public string docpath = "";
        protected Classes.Retriever ret = new Classes.Retriever();
        protected Classes.Registration reg = new Classes.Registration();

        protected InterSwitch.PayDirect.Classes.ErrorHandler err = new InterSwitch.PayDirect.Classes.ErrorHandler();
        protected InterSwitch.PayDirect.Classes.Transactions tx = new InterSwitch.PayDirect.Classes.Transactions();
        protected InterSwitch.PayDirect.Classes.Hasher hash_value = new InterSwitch.PayDirect.Classes.Hasher();

        protected Classes.XObjs.InterSwitchPostFields isw_fields = new Classes.XObjs.InterSwitchPostFields();
        protected Ipong.Classes.XObjs.InterSwitchResponse isr = new Ipong.Classes.XObjs.InterSwitchResponse();
        protected Classes.XObjs.Registration c_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Registration c_sub_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Subagent c_sub = new Classes.XObjs.Subagent();

        protected Classes.XObjs.Fee_details c_fdets = new Classes.XObjs.Fee_details();
        protected List<Classes.XObjs.Twallet> lt_twall = new List<Classes.XObjs.Twallet>();
        protected List<Classes.XObjs.Fee_details> lt_fdets = new List<Classes.XObjs.Fee_details>();
        public Ipong.Classes.XObjs.InterSwitchPostFields xispf = new Classes.XObjs.InterSwitchPostFields();
        protected Classes.tm t = new Classes.tm();
        protected List<Classes.tm.Stage> lt_pw = new List<Classes.tm.Stage>();
        public List<Classes.XObjs.ReportItem> lt_ri = new List<Classes.XObjs.ReportItem>();
        public Classes.XWriters x = new Classes.XWriters();
        public StringBuilder xstring = new StringBuilder();
        public Classes.ExcelFuncs ef = new Classes.ExcelFuncs();
        protected int tm_cnt = 0; 

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
        }

        protected void BtnReport_Click(object sender, EventArgs e)
        {
            if (fromDate.Text == "") { fromDate.Text = from_dt; } if (toDate.Text == "") { toDate.Text = to_dt; }
            lt_ri = ret.getPaymentReportItem(ddl_cat.SelectedValue, adminID, agentType, ddl_status.SelectedValue, ddl_mode.SelectedValue, fromDate.Text.Trim(), toDate.Text.Trim());
            if (lt_ri.Count > 0)
            {
                tm_cnt = lt_ri.Count; Session["tm_cnt"] = tm_cnt;
                 foreach (Classes.XObjs.ReportItem r in lt_ri)
                {
                    lt_pw = this.t.getStageByUserIDAcc(r.newtransID, cust_id);
                    if (lt_pw.Count > 0)
                    {
                        if (ddl_cat.SelectedValue == "tm")
                        {
                            showTmStatus(lt_pw);
                            r.office_status = status;
                            r.data_status = data_status;
                            r.payment_mode = ddl_mode.SelectedItem.Text;
                            r.payment_status = ddl_status.SelectedItem.Text;
                            r.init_amt = string.Format("{0:n}", Convert.ToInt32(r.init_amt));
                            r.tech_amt = string.Format("{0:n}", Convert.ToInt32(r.tech_amt));
                        }
                        else if (ddl_cat.SelectedValue == "pt")
                        {
                            showPtStatus(lt_pw);
                            r.office_status = status;
                            r.data_status = data_status;
                            r.payment_mode = ddl_mode.SelectedItem.Text;
                            r.payment_status = ddl_status.SelectedItem.Text;
                            r.init_amt = string.Format("{0:n}", Convert.ToInt32(r.init_amt));
                            r.tech_amt = string.Format("{0:n}", Convert.ToInt32(r.tech_amt));
                        }
                        else 
                        {

                        }
                    }
                    else
                    {
                        r.office_status = status;
                        r.data_status = data_status;
                        r.payment_mode = ddl_mode.SelectedItem.Text;
                        r.payment_status = ddl_status.SelectedItem.Text;
                        r.init_amt = string.Format("{0:n}", Convert.ToInt32(r.init_amt));
                        r.tech_amt = string.Format("{0:n}", Convert.ToInt32(r.tech_amt));
                    }
                }
                Session["lt_ri"] = lt_ri;
                gvTm.DataSource = lt_ri;
                gvTm.DataBind();
                //6 is Delete 7 is Requery 8 is Payment
                if (ddl_mode.SelectedValue == "xpay_isw")
                {
                    if (ddl_status.SelectedValue == "1") { gvTm.Columns[6].Visible = false; gvTm.Columns[7].Visible = false; gvTm.Columns[8].Visible = false; }
                    if (ddl_status.SelectedValue == "2") { gvTm.Columns[6].Visible = true; gvTm.Columns[7].Visible = false; gvTm.Columns[8].Visible = true; }
                    if (ddl_status.SelectedValue == "3") { gvTm.Columns[6].Visible = false; gvTm.Columns[7].Visible = true; gvTm.Columns[8].Visible = false; }
                }
                else
                {
                    if (ddl_status.SelectedValue == "1") { gvTm.Columns[6].Visible = false; gvTm.Columns[7].Visible = false; gvTm.Columns[8].Visible = false; }
                    if (ddl_status.SelectedValue == "2") { gvTm.Columns[6].Visible = true; gvTm.Columns[7].Visible = false; gvTm.Columns[8].Visible = false; }
                }
            }
        }
        public void showTmStatus(List<Classes.tm.Stage> lt_p)
        {
            status = "N/A"; data_status = "N/A";
            if (lt_p[0].status == "1")
            {
                this.status = "Verification";
                if (lt_p[0].data_status == "Fresh") { data_status = "Untreated"; }
            }
            if (lt_p[0].status == "2")
            {
                this.status = "Search";
                if (lt_p[0].data_status == "Re-conduct search") { data_status = "being re-conducted"; }
            }
            if (lt_p[0].status == "2b")
            {
                this.status = "Search 2";
                if (lt_p[0].data_status == "Re-conduct search 1") { data_status = "being re-conducted"; }
            }
            if (lt_p[0].status == "3")
            {
                this.status = "Search 2";
                if (lt_p[0].data_status == "Search Conducted") { data_status = "being processed"; }
            }
            if (lt_p[0].status == "3b")
            {
                this.status = "Examiners";
                if (lt_p[0].data_status == "Search 2 Conducted") { data_status = "being processed"; }
            }
            if (lt_p[0].status == "4")
            {
                this.status = "Acceptance";
                if (lt_p[0].data_status == "Registrable") { data_status = "Accepted"; }
                else if (lt_p[0].data_status == "Refused") { data_status = "Refused"; }
                else if (lt_p[0].data_status == "Non-registrable") { data_status = "not-registrable"; }
            }
            if (lt_p[0].status == "5")
            {
                this.status = "Publication";
                if (lt_p[0].data_status == "Accepted") { data_status = "being published"; }
            }
            if (lt_p[0].status == "6")
            {
                this.status = "Opposition";
                if (lt_p[0].data_status == "Published") { data_status = "being published"; } else { data_status = "been opposed"; }
            }
            if (lt_p[0].status == "7")
            {
                this.status = "Certification";
                if (lt_p[0].data_status == "Not Opposed") { data_status = "being processed"; }
            }
            if (lt_p[0].status == "8")
            {
                this.status = "Registrars";
                if (lt_p[0].data_status == "Certified") { data_status = "being processed"; }
            }
            if (lt_p[0].status == "9")
            {
                this.status = "Registrars";
                if (lt_p[0].data_status == "Registered") { data_status = "being registered"; }
            }
        }
        public void showPtStatus(List<Classes.tm.Stage> lt_p)
        {
            status = "N/A"; data_status = "N/A";
            if (lt_p[0].status == "1")
            {
                status = "Payment Verification Office";
                if (lt_p[0].data_status == "Fresh") { data_status = "Untreated"; }
                else if (lt_p[0].data_status == "Invalid") { data_status = "Invalid"; }
                else if (lt_p[0].data_status == "V_Contact") { data_status = "Being processed"; }
            }

            if (lt_p[0].status == "2")
            {
                status = "Patent Search Office";
                if (lt_p[0].data_status == "Valid") { data_status = "Successfully reviewed"; }
                else if (lt_p[0].data_status == "S_Contact") { data_status = "Being processed"; }
            }
            if (lt_p[0].status == "3")
            {
                status = "Patent Examiner 1 Office";
                if (lt_p[0].data_status == "Further Search") { data_status = "Further search required"; status = "Patent Search Office"; }
                else if (lt_p[0].data_status == "E_Contact") { data_status = "Being processed"; }
                else if (lt_p[0].data_status == "Search Conducted") { data_status = "Successfully reviewed"; }
                else if (lt_p[0].data_status == "Refused") { data_status = "Refused"; }
            }

            if (lt_p[0].status == "4")
            {
                status = "Patent Approving Office";
                if (lt_p[0].data_status == "Not-Patentable") { data_status = "Not-patentable"; status = "Patent Examiner 1 Office"; }
                else if (lt_p[0].data_status == "A_Contact") { data_status = "Being processed"; }
                else if (lt_p[0].data_status == "Futher Review") { data_status = "Successfully reviewed"; }
            }
            if (lt_p[0].status == "5")
            {
                status = "Registrars Office";
                if (lt_p[0].data_status == "Review Patent") { data_status = "Being reviewed"; status = "Patent Approving Office"; }
                else if (lt_p[0].data_status == "R_Contact") { data_status = "Being processed"; }
                else if (lt_p[0].data_status == "Patentable") { data_status = "Successfully reviewed"; }
            }
            if (lt_p[0].status == "6")
            {
                status = "Registrars Office";
                if (lt_p[0].data_status == "Grant Patent") { data_status = "Patent granted"; }
            }
        }
        protected void gvTm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if ((Session["tm_cnt"] != null) && (Session["tm_cnt"].ToString() != "")) { tm_cnt = Convert.ToInt32(Session["tm_cnt"]); }
            if ((Session["lt_ri"] != null) && (Session["lt_ri"].ToString() != "")) { lt_ri = (List<Classes.XObjs.ReportItem>)Session["lt_ri"]; }
            gvTm.DataSource = lt_ri;
            gvTm.DataBind();

            if (e.CommandName == "TmPayClick")
            {
                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;
                string tnxref = e.CommandArgument.ToString();

                xispf = ret.getISWtransactionByTransactionID(tnxref);

                if ((xispf.xid != null) && (xispf.xid != null))
                {
                    Session["xispf"] = xispf;
                    Response.Redirect("../xis/pd/tx/re_payment_details.aspx");
                }
            }

            if (e.CommandName == "TmReqClick")
            {
                string new_local_date = DateTime.Now.ToString("dd-MMM-yy HH:MM:ss");
                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;
                string og_transID = e.CommandArgument.ToString(); int succ = 0;

                isw_fields = ret.getISWtransactionByTransactionID(og_transID);
                //Get the hash value
                string inputString = isw_fields.product_id + isw_fields.txn_ref + isw_fields.mackey;

                string get_trans_hash = hash_value.GetGetSHA512String(inputString);
                isr = tx.myRedirect(check_trans_page + "?productid=" + isw_fields.product_id + "&transactionreference=" + isw_fields.txn_ref + "&amount=" + isw_fields.amount, "Hash", get_trans_hash);
                //////////////////////////////////////////////////////////////////////////////////////////
                if ((isr.ResponseCode != "") && (isr.ResponseCode != null) &&
                    (isr.PaymentReference != "") && (isr.PaymentReference != null) &&
                    (isr.ResponseDescription != "") && (isr.ResponseDescription != null)
                    )
                {
                    if (isw_fields.txn_ref == null) { isw_fields.txn_ref = ""; };
                    if (isw_fields.pay_ref == null) { isw_fields.pay_ref = ""; };
                    if (isw_fields.ret_ref == null) { isw_fields.ret_ref = ""; };
                    if (isr.ResponseCode == null) { isr.ResponseCode = ""; };
                    if (isr.TransactionDate == null) { isr.TransactionDate = ""; };
                    if (isr.MerchantReference == null) { isr.MerchantReference = ""; };
                    if (isr.ResponseDescription == null) { isr.ResponseDescription = ""; };

                    xstring.AppendLine("Sent Amount: " + isw_fields.amount + "\r\n Product ID: " + isw_fields.product_id + "\r\n Hash: " + get_trans_hash + "\r\n Amount: " + isr.Amount + "\r\n CardNumber: " + isr.CardNumber + "\r\n MerchantReference: " + isr.MerchantReference
                          + "\r\n PaymentReference: " + isr.PaymentReference + "\r\n RetrievalReferenceNumber: " + isr.RetrievalReferenceNumber
                          + "\r\n LeadBankCbnCode: " + isr.LeadBankCbnCode + "\r\n TransactionDate: " + isr.TransactionDate
                          + "\r\n ResponseCode: " + isr.ResponseCode + "\r\n ResponseDescription: " + isr.ResponseDescription + "\r\n Json Page: " + check_trans_page);
                    //Log the info in a file
                    if (isw_fields.txn_ref != "")
                    {
                        docpath = base.Server.MapPath("~/") + "LiveInterLogs/Ag/" + isw_fields.txn_ref + ".txt";
                        succ = x.WriteToFile(xstring.ToString(), docpath);
                    }
                    else
                    {
                        docpath = base.Server.MapPath("~/") + "LiveInterLogs/Ag/xxx.txt";
                        succ = x.WriteToFile(xstring.ToString(), docpath);
                    }

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
                    succ = reg.updateInterSwitchRecords(isw_fields.txn_ref, isw_fields.pay_ref, isw_fields.ret_ref, isr.ResponseCode, isr.TransactionDate, isr.MerchantReference, isr.ResponseDescription);
                    reg.updateTwalletPaymentStatus(isw_fields.txn_ref, xpay_status);

                    reg.updateInterSwitchPostFieldsDate(isw_fields.txn_ref, new_local_date);
                    Response.Redirect("./xreport_payments.aspx");
                    if (succ != 0)
                    {
                        //sendAlert();
                    }
                }
                else
                {
                    Response.Redirect("./xreport_payments.aspx");
                }
            }

            if (e.CommandName == "TmDeleteClick")
            {
                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;
                string og_transID = e.CommandArgument.ToString(); int succ = 0;
                succ = reg.updateInterSwitchVisibleStatus(og_transID, "0");
                Response.Redirect("./xreport_payments.aspx");
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

        protected void gvTm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTm.PageIndex = e.NewPageIndex;
            if ((Session["tm_cnt"] != null) && (Session["tm_cnt"].ToString() != "")) { tm_cnt = Convert.ToInt32(Session["tm_cnt"]); }
            if ((Session["lt_ri"] != null) && (Session["lt_ri"].ToString() != "")) { lt_ri = (List<Classes.XObjs.ReportItem>)Session["lt_ri"]; }
            gvTm.DataSource = lt_ri;
            gvTm.DataBind();
            show_inv = 0; 
        }

        protected void BtnBackToList_Click(object sender, EventArgs e)
        {
            if ((Session["tm_cnt"] != null) && (Session["tm_cnt"].ToString() != "")) { tm_cnt = Convert.ToInt32(Session["tm_cnt"]); }
            if ((Session["lt_ri"] != null) && (Session["lt_ri"].ToString() != "")) { lt_ri = (List<Classes.XObjs.ReportItem>)Session["lt_ri"]; }
            gvTm.DataSource = lt_ri;
            gvTm.DataBind();
            show_inv = 0;
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            if ((Session["tm_cnt"] != null) && (Session["tm_cnt"].ToString() != "")) { tm_cnt = Convert.ToInt32(Session["tm_cnt"]); }
            if ((Session["lt_ri"] != null) && (Session["lt_ri"].ToString() != "")) { lt_ri = (List<Classes.XObjs.ReportItem>)Session["lt_ri"]; }
            gvTm.DataSource = lt_ri;
            gvTm.DataBind();
            show_inv = 0;
            docpath += agentType + "_" + adminID + "_" + DateTime.Now.ToLongTimeString().Replace(" ", "_").Replace(":", "_") + ".xls";

            ef.CreateReportExcel(this, lt_ri, docpath, ddl_mode.SelectedItem.Text+" Payment Report");              
        }

    }
}