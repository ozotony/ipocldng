using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class application : System.Web.UI.Page
{
    public string agentemail;
    public string agentpnumber;
    public string aid = "";
    public string amt = "";
    public string cname="";
    public string gt = "";
    public string transID = "";
    public string vid = "";
    public string xapplication = "";
    public string pc = "";
    public string status = "0";

    public SortedList<string, string> sl_xx = new SortedList<string, string>();
    public List<SortedList<string, string>> lt_app = new List<SortedList<string, string>>();
    public List<SortedList<string, string>> lt_inv = new List<SortedList<string, string>>();
    public List<SortedList<string, string>> lt_pri = new List<SortedList<string, string>>();
    //public pt t = new pt();

    protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
    protected string xvisible = "1"; protected string agentType = "";
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
        //{ agentType = Session["agentType"].ToString(); }
        //else
        //{ Response.Redirect("../a_login.aspx"); }

        if (this.Session["pc"] != null)
        {
            //this.pc = this.Session["pc"].ToString();
            //if (this.pc == "P001"){  lbl_type.Text="NON-CONVENTIONAL"; }
            //else {  lbl_type.Text = "CONVENTION"; }
        }
       
        //if (this.Session["aid"] != null) { rep_code.Text = Session["aid"].ToString();  }
        //if (!Page.IsPostBack)
        //{
        //    if (this.Session["vid"] != null)  {this.transID = this.Session["vid"].ToString();}
        //    this.Session["xref"] = Convert.ToInt32(this.Session["xref"]) + 1;
        //    if (this.Session["xref"].ToString() != "1")
        //    {
        //      //  base.Response.Redirect("./violation.aspx");
        //    }
        //    if (((this.Session["xapplication"] != null) && (this.Session["xapplication"].ToString() != "")) && (this.transID != this.Session["xapplication"].ToString()))
        //    {
        //       // base.Response.Redirect("./violation.aspx");
        //    }
        //if (this.Session["cname"] != null){  this.rep_xname.Text = Session["cname"].ToString(); }
        //if (this.Session["agentemail"] != null) {this.txt_rep_email.Text = Session["agentemail"].ToString(); }
        //if (this.Session["agentpnumber"] != null) { this.txt_rep_telephone.Text = Session["agentpnumber"].ToString();}
        //if (this.Session["product_title"] != null){this.txt_title_of_invention.Text = Session["product_title"].ToString();}      
       // }
    }


    // protected void SaveAll_Click(object sender, EventArgs e)
    //{
    //    //try
    //    //{
    //        //Log.Info(@"Application Error");
    //        pt.PtInfo c_ptinfo = new pt.PtInfo();
    //        pt.Assignment_info c_assinfo = new pt.Assignment_info();
    //        pt.Representative c_rep = new pt.Representative();

    //        List<pt.Applicant> lt_xapp = new List<pt.Applicant>();
    //        List<pt.Inventor> lt_xinv = new List<pt.Inventor>();
    //        List<pt.Priority_info> lt_xpri = new List<pt.Priority_info>();

    //        c_ptinfo.reg_number = "";
    //        c_ptinfo.xtype = lbl_type.Text;
    //        c_ptinfo.title_of_invention = txt_title_of_invention.Text;
    //        c_ptinfo.pt_desc = txt_pt_desc.Text;
    //        if (Session["pwalletID"] != null)
    //        {
    //            c_ptinfo.log_staff = Session["pwalletID"].ToString();
    //        }
    //        else
    //        {// base.Response.Redirect("./violation.aspx");
    //        }
    //        c_ptinfo.reg_date = xreg_date;
    //        c_ptinfo.xvisible = xvisible;
    //        c_ptinfo.claim_no = "0";
    //        c_ptinfo.loa_no = "0";
    //        c_ptinfo.pct_no = "0";
    //        c_ptinfo.doa_no = "0";

    //        c_assinfo.date_of_assignment = txt_assignment_date.Text;
    //        c_assinfo.assignee_name = txt_assignee_name.Text;
    //        c_assinfo.assignee_address = txt_assignee_address.Text;
    //        c_assinfo.assignee_nationality = select_assignee_nationality.SelectedValue;
    //        c_assinfo.assignor_name = txt_assignor_name.Text;
    //        c_assinfo.assignor_address = txt_assignor_address.Text;
    //        c_assinfo.assignor_nationality = select_assignor_nationality.SelectedValue;
    //        if (Session["pwalletID"] != null)
    //        {
    //            c_assinfo.log_staff = Session["pwalletID"].ToString();
    //        }
    //        else
    //        { //base.Response.Redirect("./violation.aspx");
    //        }
    //        c_assinfo.visible = xvisible;

    //        c_rep.agent_code = rep_code.Text;
    //        c_rep.xname = rep_xname.Text;
    //        c_rep.nationality = "160";
    //        c_rep.country = "160";
    //        c_rep.state = select_rep_state.SelectedValue;
    //        c_rep.address = txt_rep_address.Text;
    //        c_rep.xmobile = txt_rep_telephone.Text;
    //        c_rep.xemail = txt_rep_email.Text;
    //        c_rep.reg_date = xreg_date;
    //        c_rep.visible = xvisible;
    //        if (Session["pwalletID"] != null)
    //        {
    //            c_rep.log_staff = Session["pwalletID"].ToString();
    //        }
    //        else
    //        { //base.Response.Redirect("./violation.aspx"); 
    //        }

    //        lt_app = (List<SortedList<string, string>>)Session["lt_app"];
    //        lt_inv = (List<SortedList<string, string>>)Session["lt_inv"];
    //        lt_pri = (List<SortedList<string, string>>)Session["lt_pri"];

    //        if (lt_app.Count > 0)
    //        {
    //            for (int i = 0; i < lt_app.Count; i++)
    //            {
    //                pt.Applicant c_app = new pt.Applicant();
    //                c_app.xname = lt_app[i]["txt_name_app"];
    //                c_app.address = lt_app[i]["txt_address_app"];
    //                c_app.xemail = lt_app[i]["txt_email_app"];
    //                c_app.xmobile = lt_app[i]["txt_mobile_app"];
    //                c_app.nationality = lt_app[i]["select_app_nationality"];
    //                if (Session["pwalletID"] != null)
    //                {
    //                    c_app.log_staff = Session["pwalletID"].ToString();
    //                }
    //                else
    //                {// base.Response.Redirect("./violation.aspx");
    //                }
    //                c_app.visible = xvisible;
    //                lt_xapp.Add(c_app);
    //            }
    //        }

    //        if (lt_inv.Count > 0)
    //        {
    //            for (int i = 0; i < lt_inv.Count; i++)
    //            {
    //                pt.Inventor c_inv = new pt.Inventor();
    //                c_inv.xname = lt_inv[i]["txt_name_inv"];
    //                c_inv.address = lt_inv[i]["txt_address_inv"];
    //                c_inv.xemail = lt_inv[i]["txt_email_inv"];
    //                c_inv.xmobile = lt_inv[i]["txt_mobile_inv"];
    //                c_inv.nationality = lt_inv[i]["select_inv_nationality"];
    //                if (Session["pwalletID"] != null)
    //                {
    //                    c_inv.log_staff = Session["pwalletID"].ToString();
    //                }
    //                else
    //                { //base.Response.Redirect("./violation.aspx"); 
    //                }
    //                c_inv.visible = xvisible;
    //                lt_xinv.Add(c_inv);
    //            }
    //        }

    //        if (lt_pri.Count > 0)
    //        {
    //            for (int i = 0; i < lt_pri.Count; i++)
    //            {
    //                pt.Priority_info c_pri = new pt.Priority_info();
    //                c_pri.countryID = lt_pri[i]["select_country_pri"];
    //                c_pri.app_no = lt_pri[i]["txt_application_no_pri"];
    //                c_pri.xdate = lt_pri[i]["txt_date_pri"];
    //                if (Session["pwalletID"] != null)
    //                {
    //                    c_pri.log_staff = Session["pwalletID"].ToString();
    //                }
    //                else
    //                { //base.Response.Redirect("./violation.aspx");
    //                }
    //                c_pri.xvisible = xvisible;
    //                lt_xpri.Add(c_pri);
    //            }
    //        }
    //        string pt_succ = t.addNewPatent(lt_xapp, lt_xpri, lt_xinv, c_ptinfo, c_assinfo, c_rep);
    //        if ((pt_succ != "") && (pt_succ != null))
    //        {
    //            Session["new_ptID"] = pt_succ;
    //            Response.Redirect("./application_docs.aspx");
    //        }
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    Log.Error(ex.Message, ex);
    //    //}
    //}
    // protected void SaveExit_Click(object sender, EventArgs e)
    // {
    //     //try
    //     //{
    //     //Log.Info(@"Application Error");

    //     pt.PtInfo c_ptinfo = new pt.PtInfo();
    //     pt.Assignment_info c_assinfo = new pt.Assignment_info();
    //     pt.Representative c_rep = new pt.Representative();

    //     List<pt.Applicant> lt_xapp = new List<pt.Applicant>();
    //     List<pt.Inventor> lt_xinv = new List<pt.Inventor>();
    //     List<pt.Priority_info> lt_xpri = new List<pt.Priority_info>();

    //     c_ptinfo.reg_number = "";
    //     c_ptinfo.xtype = lbl_type.Text;
    //     c_ptinfo.title_of_invention = txt_title_of_invention.Text;
    //     c_ptinfo.pt_desc = txt_pt_desc.Text;
    //     if (Session["pwalletID"] != null)
    //     {
    //         c_ptinfo.log_staff = Session["pwalletID"].ToString();
    //     }
    //     else
    //     {// base.Response.Redirect("./violation.aspx");
    //     }
    //     c_ptinfo.reg_date = xreg_date;
    //     c_ptinfo.xvisible = xvisible;
    //     c_ptinfo.claim_no = "0";
    //     c_ptinfo.loa_no = "0";
    //     c_ptinfo.pct_no = "0";
    //     c_ptinfo.doa_no = "0";

    //     c_assinfo.date_of_assignment = txt_assignment_date.Text;
    //     c_assinfo.assignee_name = txt_assignee_name.Text;
    //     c_assinfo.assignee_address = txt_assignee_address.Text;
    //     c_assinfo.assignee_nationality = select_assignee_nationality.SelectedValue;
    //     c_assinfo.assignor_name = txt_assignor_name.Text;
    //     c_assinfo.assignor_address = txt_assignor_address.Text;
    //     c_assinfo.assignor_nationality = select_assignor_nationality.SelectedValue;
    //     if (Session["pwalletID"] != null)
    //     {
    //         c_assinfo.log_staff = Session["pwalletID"].ToString();
    //     }
    //     else
    //     { //base.Response.Redirect("./violation.aspx");
    //     }
    //     c_assinfo.visible = xvisible;

    //     c_rep.agent_code = rep_code.Text;
    //     c_rep.xname = rep_xname.Text;
    //     c_rep.nationality = "160";
    //     c_rep.country = "160";
    //     c_rep.state = select_rep_state.SelectedValue;
    //     c_rep.address = txt_rep_address.Text;
    //     c_rep.xmobile = txt_rep_telephone.Text;
    //     c_rep.xemail = txt_rep_email.Text;
    //     c_rep.reg_date = xreg_date;
    //     c_rep.visible = xvisible;
    //     if (Session["pwalletID"] != null)
    //     {
    //         c_rep.log_staff = Session["pwalletID"].ToString();
    //     }
    //     else
    //     { //base.Response.Redirect("./violation.aspx"); 
    //     }

    //     lt_app = (List<SortedList<string, string>>)Session["lt_app"];
    //     lt_inv = (List<SortedList<string, string>>)Session["lt_inv"];
    //     lt_pri = (List<SortedList<string, string>>)Session["lt_pri"];

    //     if (lt_app.Count > 0)
    //     {
    //         for (int i = 0; i < lt_app.Count; i++)
    //         {
    //             pt.Applicant c_app = new pt.Applicant();
    //             c_app.xname = lt_app[i]["txt_name_app"];
    //             c_app.address = lt_app[i]["txt_address_app"];
    //             c_app.xemail = lt_app[i]["txt_email_app"];
    //             c_app.xmobile = lt_app[i]["txt_mobile_app"];
    //             c_app.nationality = lt_app[i]["select_app_nationality"];
    //             if (Session["pwalletID"] != null)
    //             {
    //                 c_app.log_staff = Session["pwalletID"].ToString();
    //             }
    //             else
    //             {// base.Response.Redirect("./violation.aspx");
    //             }
    //             c_app.visible = xvisible;
    //             lt_xapp.Add(c_app);
    //         }
    //     }

    //     if (lt_inv.Count > 0)
    //     {
    //         for (int i = 0; i < lt_inv.Count; i++)
    //         {
    //             pt.Inventor c_inv = new pt.Inventor();
    //             c_inv.xname = lt_inv[i]["txt_name_inv"];
    //             c_inv.address = lt_inv[i]["txt_address_inv"];
    //             c_inv.xemail = lt_inv[i]["txt_email_inv"];
    //             c_inv.xmobile = lt_inv[i]["txt_mobile_inv"];
    //             c_inv.nationality = lt_inv[i]["select_inv_nationality"];
    //             if (Session["pwalletID"] != null)
    //             {
    //                 c_inv.log_staff = Session["pwalletID"].ToString();
    //             }
    //             else
    //             { //base.Response.Redirect("./violation.aspx"); 
    //             }
    //             c_inv.visible = xvisible;
    //             lt_xinv.Add(c_inv);
    //         }
    //     }

    //     if (lt_pri.Count > 0)
    //     {
    //         for (int i = 0; i < lt_pri.Count; i++)
    //         {
    //             pt.Priority_info c_pri = new pt.Priority_info();
    //             c_pri.countryID = lt_pri[i]["select_country_pri"];
    //             c_pri.app_no = lt_pri[i]["txt_application_no_pri"];
    //             c_pri.xdate = lt_pri[i]["txt_date_pri"];
    //             if (Session["pwalletID"] != null)
    //             {
    //                 c_pri.log_staff = Session["pwalletID"].ToString();
    //             }
    //             else
    //             { //base.Response.Redirect("./violation.aspx");
    //             }
    //             c_pri.xvisible = xvisible;
    //             lt_xpri.Add(c_pri);
    //         }
    //     }
    //     string pt_succ = t.addNewPatent(lt_xapp, lt_xpri, lt_xinv, c_ptinfo, c_assinfo, c_rep);
    //     if ((pt_succ != "") && (pt_succ != null))
    //     {
    //         Session["new_ptID"] = pt_succ;
    //         //Response.Redirect("./application_docs.aspx");

    //         if (this.Session["aid"] != null)
    //         {
    //             this.aid = this.Session["aid"].ToString();
    //         }
    //         Response.Redirect("appstatus.aspx?agt=" + aid);
    //     }
    //     //}
    //     //catch (Exception ex)
    //     //{
    //     //    Log.Error(ex.Message, ex);
    //     //}
    // }
}