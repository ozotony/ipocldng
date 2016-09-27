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
    public partial class v_bask_ptu : Page
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
        protected Registration reg = new Registration();
        private Retriever ret = new Retriever();
        protected RemotePost rp = new RemotePost();
        protected int show_inv;
        protected string status = "";
        protected tm t = new tm();
        protected XObjs.Trademark_item ti = new XObjs.Trademark_item();
        protected string transID = "";
        protected int used_cnt;
        protected int viz;
        protected SortedList<string, string> xheaders = new SortedList<string, string>();
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int xtotal_amt;
        protected string postwith = "";
        protected string agent = "";
        protected string agentemail = "";
        protected string agentpnumber = "";
        protected string agentname = "";
        protected List<XObjs.XDisplayItem> lt_xdi = new List<XObjs.XDisplayItem>();
        protected AppStatus app_status = new AppStatus();
        int tm_vcount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Trademark_item"] = null;
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            {
                adminID = Session["pwalletID"].ToString(); xadminID.Value = adminID;

                if (!IsPostBack)
                {
                    Session["fee_detailsID"] = null;

                    if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
                    {
                        agentType = Session["agentType"].ToString();
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

                        //   used_cnt = ret.getPaidUsedCntByCat(adminID, "tm", "1", agentType, "Used"); Session["used_cnt"] = used_cnt;
                        lt_xdi = ret.GetXDisplayItem("pt", adminID, "1", "Used");
                        if (lt_xdi.Count > 0)
                        {
                            Session["used_cnt"] = lt_xdi.Count;
                            foreach (Classes.XObjs.XDisplayItem x in lt_xdi)
                            {
                                lt_pw = t.getStageByUserIDAccPt(x.new_transID, Session["Sys_ID"].ToString());
                                if (lt_pw.Count > 0)
                                {

                                    SortedList<string, string> sl_app_status = new SortedList<string, string>();

                                    sl_app_status = app_status.showPtStatus(lt_pw[0].status, lt_pw[0].data_status);
                                    x.lbl_cur_office = sl_app_status["status"]; x.lbl_cur_status = sl_app_status["data_status"];
                                }
                                else
                                {
                                    string[] tm_trans_arr = x.new_transID.Split('-');
                                    //if(Convert.ToInt32(tm_trans_arr[2].Trim())>0)
                                    //{ 
                                    //int tm_hcount = reg.updateHwalletStatus(tm_trans_arr[2].Trim());
                                    //if (tm_hcount > 0) { tm_vcount++;
                                    //}
                                    //}
                                }

                            }
                        }

                        if (tm_vcount > 0)
                        {
                            Response.Redirect("v_bask_ptu.aspx");
                        }
                        else
                        {
                            Session["lt_xdi"] = lt_xdi; used_cnt = lt_xdi.Count; Session["used_cnt"] = used_cnt;
                            gvTmUsed.DataSource = lt_xdi;
                            gvTmUsed.DataBind();
                        }

                    }
                    else { Response.Redirect("../a_login.aspx"); }
                }
                else
                {
                    if (Session["used_cnt"] != null) { used_cnt = Convert.ToInt32(Session["used_cnt"]); }
                }

            }
            else
            { Response.Redirect("../a_login.aspx"); }
            if ((Session["log_date"] != null) && (Session["log_date"].ToString() != ""))
            { log_date = Session["log_date"].ToString(); }
        }



        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("v_bask_pt.aspx");
        }

        protected void gvTmUsed_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTmUsed.PageIndex = e.NewPageIndex;
            if (Session["used_cnt"] != null) { lt_xdi = (List<XObjs.XDisplayItem>)Session["lt_xdi"]; }
            gvTmUsed.DataSource = lt_xdi;
            gvTmUsed.DataBind();
            if (Session["used_cnt"] != null) { used_cnt = Convert.ToInt32(Session["used_cnt"]); }
            //show_inv = 0; show_details_grid = 0; 
        }



    }

}