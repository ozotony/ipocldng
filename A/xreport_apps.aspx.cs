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

namespace Ipong.A
{
    using Ipong.ExcelClasses;
    public partial class xreport_apps : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        protected AppStatus c_as = new AppStatus();
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected string check_trans_page = "";
        protected string coy_name = "";
        protected string cust_id = "";
        public string data_status = "N/A";
        public string docpath = "";
        public ExcelFuncs ef = new ExcelFuncs();
        protected string email = "";
        protected string from_dt = "0000-01-01";
        protected string fullname = "";
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
        protected pt xpt = new pt();
        protected int tm_cnt;
        protected string to_dt = DateTime.Now.ToString("yyyy-MM-dd");
        protected int tot_amtx;
        protected string transID = "";
        public XWriters x = new XWriters();
        protected string xpay_status = "";
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        public StringBuilder xstring = new StringBuilder();
        protected tm.Stage c_stage = new tm.Stage();
        protected tm.Applicant c_app = new tm.Applicant();
        protected tm.AddressService c_aos = new tm.AddressService();
        protected tm.MarkInfo c_mark = new tm.MarkInfo();
        protected tm.Representative c_rep = new tm.Representative();
        protected tm.Address c_app_addy=new tm.Address();
        protected tm.Address c_rep_addy = new tm.Address();

        public List<pt.Applicant> lt_app = new List<pt.Applicant>();
        public List<pt.Assignment_info> lt_assinfo = new List<pt.Assignment_info>();
        public List<pt.Inventor> lt_inv = new List<pt.Inventor>();
        public List<pt.PtInfo> lt_mi = new List<pt.PtInfo>();
        public List<pt.PtInfo> lt_pt_mi = new List<pt.PtInfo>();
        public List<pt.Representative> lt_rep = new List<pt.Representative>();
        public List<pt.Representative> lt_repx = new List<pt.Representative>();
        public List<pt.Stage> lt_stage = new List<pt.Stage>();
        public List<pt.Priority_info> lt_xpri = new List<pt.Priority_info>();

        protected int xtotal_amt;

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
            ef.CreateReportExcel(this, lt_ri, docpath, ddl_mode.SelectedItem.Text + " Application Report");
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
            lt_ri = ret.getApplicationReportItem(ddl_cat.SelectedValue, adminID, agentType, ddl_status.SelectedValue, ddl_mode.SelectedValue, fromDate.Text.Trim(), toDate.Text.Trim());
            if (lt_ri.Count > 0)
            {
                tm_cnt = lt_ri.Count;
                Session["tm_cnt"] = tm_cnt;
                int tm_vcount = 0; int pt_vcount = 0; int ds_vcount = 0;
                foreach (XObjs.ReportItem item in lt_ri)
                {
                          if (ddl_status.SelectedValue=="Used")
                                        { 
                                        if (ddl_cat.SelectedValue == "tm")
                                        {
                            
                                                lt_pw = t.getStageByUserIDAccTm(item.newtransID, cust_id); //Check if valid
                                                if (lt_pw.Count > 0)
                                                {
                                                    //showTmStatus(lt_pw);
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
                                                else
                                                {
                                                //    string[] tm_trans_arr = item.newtransID.Split('-');
                                                //    int tm_hcount = reg.updateHwalletStatus(tm_trans_arr[2].Trim());
                                                //    if (tm_hcount > 0) { tm_vcount++; }
                                                }

                           

                                        }
                                        else if (ddl_cat.SelectedValue == "pt")
                                        {
                                            lt_pw = t.getStageByUserIDAccPt(item.newtransID, cust_id);
                                            if (lt_pw.Count > 0)
                                            {
                                               // showPtStatus(lt_init_stage);
                                                SortedList<string, string> x = c_as.showPtStatus(lt_pw[0].status, lt_pw[0].data_status);
                                                item.office_status = status;
                                                item.data_status = data_status;
                                                item.payment_mode = ddl_mode.SelectedItem.Text;
                                                item.payment_status = ddl_status.SelectedItem.Text;
                                                item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
                                                item.tech_amt = string.Format("{0:n}", Convert.ToInt32(item.tech_amt));
                                            }
                                            else
                                            {
                                                //string[] pt_trans_arr = item.newtransID.Split('-');
                                                //int pt_hcount = reg.updateHwalletStatus(pt_trans_arr[2].Trim());
                                                //if (pt_hcount > 0) { pt_vcount++; }
                                            }

                                        }
                                        else if (ddl_cat.SelectedValue == "ds")
                                        {
                                            lt_pw = t.getStageByUserIDAccDs(item.newtransID, cust_id);
                                            if (lt_pw.Count > 0)
                                            {
                                                //   showDsStatus(lt_init_stage);
                                                SortedList<string, string> x = c_as.showDsStatus(lt_pw[0].status, lt_pw[0].data_status);
                                                item.office_status = status;
                                                item.data_status = data_status;
                                                item.payment_mode = ddl_mode.SelectedItem.Text;
                                                item.payment_status = ddl_status.SelectedItem.Text;
                                                item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
                                                item.tech_amt = string.Format("{0:n}", Convert.ToInt32(item.tech_amt));
                                            }
                                            else
                                            {
                                                string[] ds_trans_arr = item.newtransID.Split('-');
                                                int ds_hcount = reg.updateHwalletStatus(ds_trans_arr[2].Trim());
                                                if (ds_hcount > 0) { ds_vcount++; }
                                            }
                                        }
                                   }
                                else
                                {
                                    item.office_status = "Not Used";
                                    item.data_status = "Not Used";
                                    item.payment_mode = ddl_mode.SelectedItem.Text;
                                    item.payment_status = ddl_status.SelectedItem.Text;
                                    item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
                                    item.tech_amt = string.Format("{0:n}", Convert.ToInt32(item.tech_amt));
                                    gvTm.Columns[6].Visible = false;
                                }                   
                  
                }
                if ((ds_vcount > 0) || (pt_vcount > 0) || (tm_vcount > 0))
                {
                   // ReReloadReportList();
                }
                else
                { 
                Session["lt_ri"] = lt_ri;
                gvTm.DataSource = lt_ri;
                gvTm.DataBind();
                }
            }
        }

        protected void ReReloadReportList()
        {
            if (fromDate.Text == "")
            {
                fromDate.Text = from_dt;
            }
            if (toDate.Text == "")
            {
                toDate.Text = to_dt;
            }
            lt_ri = ret.getApplicationReportItem(ddl_cat.SelectedValue, adminID, agentType, ddl_status.SelectedValue, ddl_mode.SelectedValue, fromDate.Text.Trim(), toDate.Text.Trim());
            if (lt_ri.Count > 0)
            {
                tm_cnt = lt_ri.Count;
                Session["tm_cnt"] = tm_cnt;
                int tm_vcount = 0; int pt_vcount = 0; int ds_vcount = 0;
                foreach (XObjs.ReportItem item in lt_ri)
                {

                    if (lt_ri.Count > 0)
                    {
                        if (ddl_cat.SelectedValue == "tm")
                        {

                            lt_pw = t.getStageByUserIDAccTm(item.newtransID, cust_id); //Check if valid
                            if (lt_pw.Count > 0)
                            {
                             //   showTmStatus(lt_pw);
                                item.office_status = status;
                                item.data_status = data_status;
                                item.payment_mode = ddl_mode.SelectedItem.Text;
                                item.payment_status = ddl_status.SelectedItem.Text;
                                item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
                                item.tech_amt = string.Format("{0:n}", Convert.ToInt32(item.tech_amt));
                            }
                            else
                            {
                                string[] tm_trans_arr = item.newtransID.Split('-');
                                int tm_hcount = reg.updateHwalletStatus(tm_trans_arr[2].Trim());
                                if (tm_hcount > 0) { tm_vcount++; }
                            }
                        }
                        else if (ddl_cat.SelectedValue == "pt")
                        {
                            lt_pw = t.getStageByUserIDAccPt(item.newtransID, cust_id);
                            if (lt_pw.Count > 0)
                            {
                               // showPtStatus(lt_init_stage);
                                item.office_status = status;
                                item.data_status = data_status;
                                item.payment_mode = ddl_mode.SelectedItem.Text;
                                item.payment_status = ddl_status.SelectedItem.Text;
                                item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
                                item.tech_amt = string.Format("{0:n}", Convert.ToInt32(item.tech_amt));
                            }
                            else
                            {
                                string[] pt_trans_arr = item.newtransID.Split('-');
                                int pt_hcount = reg.updateHwalletStatus(pt_trans_arr[2].Trim());
                                if (pt_hcount > 0) { pt_vcount++; }
                            }

                        }
                        else if (ddl_cat.SelectedValue == "ds")
                        {
                            lt_pw = t.getStageByUserIDAccDs(item.newtransID, cust_id);
                            if (lt_pw.Count > 0)
                            {
                               // showDsStatus(lt_init_stage);
                                item.office_status = status;
                                item.data_status = data_status;
                                item.payment_mode = ddl_mode.SelectedItem.Text;
                                item.payment_status = ddl_status.SelectedItem.Text;
                                item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
                                item.tech_amt = string.Format("{0:n}", Convert.ToInt32(item.tech_amt));
                            }
                            else
                            {
                                string[] ds_trans_arr = item.newtransID.Split('-');
                                int ds_hcount = reg.updateHwalletStatus(ds_trans_arr[2].Trim());
                                if (ds_hcount > 0) { ds_vcount++; }
                            }
                        }
                    }

                }
                if ((ds_vcount > 0) || (pt_vcount > 0) || (tm_vcount > 0))
                {

                }
                else
                {
                    Session["lt_ri"] = lt_ri;
                    gvTm.DataSource = lt_ri;
                    gvTm.DataBind();
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
            if (e.CommandName == "TmDetailsClick")
            {

                GridViewRow namingContainer = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string new_transID = gvTm.Rows[rowIndex].Cells[1].Text;
                string item_code = gvTm.Rows[rowIndex].Cells[2].Text;
                int vcount = 0;

                if (item_code.Contains("T"))
                {
                    c_stage = t.getStageByTransID(new_transID);
                    if (Convert.ToInt32(c_stage.ID) > 0)
                    {
                        c_app = t.getApplicantByUserID(c_stage.ID);
                        c_mark = t.getMarkInfoClassByUserID(c_stage.ID);
                        c_aos = t.getAddressServiceByID(c_stage.ID);
                        c_rep = t.getRepClassByUserID(c_stage.ID);
                        if (c_rep.addressID!=null) { c_rep_addy = t.getAddressClassByID(c_rep.addressID); }
                        if (c_app.addressID!=null ){ c_app_addy = t.getAddressClassByID(c_app.addressID); }                      
                    }
                    else
                    {
                        Registration reg = new Registration();
                        string[] trans_arr = new_transID.Split('-');
                        int hcount = reg.updateHwalletStatus(trans_arr[2].Trim());
                        if (hcount > 0)  { vcount ++; }
                    }
                    show_inv = 2;
                }
                else if (item_code.Contains("P"))
                {
                    lt_stage = xpt.getPwalletDetailsByValidationID(new_transID);                   

                    if (Convert.ToInt32(lt_stage[0].ID) > 0)
                    {
                        lt_mi = xpt.getPtInfoByPwalletID(lt_stage[0].ID);
                        lt_rep = xpt.getRepListByUserID(lt_stage[0].ID);
                        lt_stage = xpt.getStageByUserID(lt_stage[0].ID);
                        lt_app = xpt.getApplicantByvalidationID(lt_stage[0].ID);
                        lt_inv = xpt.getInventorByvalidationID(lt_stage[0].ID);
                        lt_assinfo = xpt.getAssignment_infoByvalidationID(lt_stage[0].ID);
                        lt_xpri = xpt.getPriority_infoByvalidationID(lt_stage[0].ID);
                    }
                    else
                    {
                        Registration reg = new Registration();
                        string[] trans_arr = new_transID.Split('-');
                        int hcount = reg.updateHwalletStatus(trans_arr[2].Trim());
                        if (hcount > 0) { vcount++; }
                    }
                    show_inv = 3;
                }
                else if (item_code.Contains("D"))
                {
                    //c_stage = t.getStageByTransID(new_transID);
                    //if (Convert.ToInt32(c_stage.ID) > 0)
                    //{
                    //    c_app = t.getApplicantByUserID(c_stage.ID);
                    //    c_mark = t.getMarkInfoClassByUserID(c_stage.ID);
                    //    c_aos = t.getAddressServiceByID(c_stage.ID);
                    //    c_rep = t.getRepClassByUserID(c_stage.ID);
                    //    if (Convert.ToInt32(c_rep.addressID) > 0) { c_rep_addy = t.getAddressClassByID(c_rep.addressID); }
                    //    if (Convert.ToInt32(c_app.addressID) > 0) { c_app_addy = t.getAddressClassByID(c_app.addressID); }
                    //}
                    //else
                    //{
                    //    Registration reg = new Registration();
                    //    string[] trans_arr = new_transID.Split('-');
                    //    int hcount = reg.updateHwalletStatus(trans_arr[2].Trim());
                    //    if (hcount > 0)  { vcount ++; }
                    //}
                    //show_inv = 2;
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