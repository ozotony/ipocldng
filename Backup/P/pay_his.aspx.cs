using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Ipong.P
{
    public partial class pay_his : System.Web.UI.Page
    {
        protected string adminID = "0"; protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int show_inv = 0; protected int xtotal_amt = 0; protected string agentType = "";
        protected string fullname = ""; protected string email = ""; protected string mobile = "";
        protected string transID = ""; protected string ref_no = ""; protected string check_trans_page = "";
        protected int tot_amtx = 0; protected string coy_name = ""; protected string cust_id = ""; protected string pr = "";

        protected int show_details_grid = 0; protected int show_banker_grid = 0; protected int show_details_grid_wingman = 0;
        protected int grand_tot_cnt = 0;  protected int grand_tot_amt = 0; protected int tm_cnt = 0;
        protected string new_grand_tot_amt = ""; protected string search_msg = "";

        protected InterSwitch.PayDirect.Classes.Transactions tx = new InterSwitch.PayDirect.Classes.Transactions();
        protected Classes.XObjs.InterSwitchPostFields isw_fields = new Classes.XObjs.InterSwitchPostFields();
        protected Ipong.Classes.XObjs.InterSwitchResponse isr = new Ipong.Classes.XObjs.InterSwitchResponse();

        protected Classes.XObjs.Registration c_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Registration c_sub_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Subagent c_sub = new Classes.XObjs.Subagent();
        protected Classes.XObjs.XPartner c_partner = new Classes.XObjs.XPartner();
        protected Classes.XObjs.PRatio c_pr = new Classes.XObjs.PRatio();

        protected Classes.XObjs.Fee_details c_fdets = new Classes.XObjs.Fee_details();
        protected Classes.XObjs.XBanker c_banker= new Classes.XObjs.XBanker();
        protected List<Classes.XObjs.Twallet> lt_twall = new List<Classes.XObjs.Twallet>();
        protected List<Classes.XObjs.Fee_details> lt_fdets = new List<Classes.XObjs.Fee_details>();

        protected Classes.Retriever ret = new Classes.Retriever();
        protected Classes.Registration reg = new Classes.Registration();
        protected List<Classes.XObjs.PartnerGrid> lt_pg = new List<Classes.XObjs.PartnerGrid>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            { this.adminID = Session["pwalletID"].ToString(); }
            else
            { base.Response.Redirect("../login.aspx"); }

            if (!IsPostBack)
            {
                Session["IpongMemberType"] = null;
                Session["grand_tot_cnt"] = null;
                Session["new_grand_tot_amt"] = null;
                Session["transID"] = null;
                Session["payment_type"] = null;
                Session["bank_xname"] = null;
                Session["bank_bankname"] = null;
                Session["bank_xposition"] = null;
                Session["bank_street"] = null;
                Session["bank_telephone"] = null;
                Session["bank_email"] = null;
                Session["transDate"] = null;
            }
            c_pr = ret.getPratioByMemberID(adminID);
            if (c_pr.xid != null)
            {
                Session["IpongMemberType"] = c_pr.p_type;
                if (Session["IpongMemberType"] != null) { pr = Session["IpongMemberType"].ToString(); }
            }
            if(Session["grand_tot_cnt"]==null){ Session["grand_tot_cnt"]="0";}
            if (Session["new_grand_tot_amt"] == null) { Session["new_grand_tot_amt"] = "0"; }
            if (Session["transID"] == null) { Session["transID"] = "0"; }

            if (Session["payment_type"] == null) { Session["payment_type"] = ""; }
            if (Session["bank_xname"] == null) { Session["bank_xname"] = ""; }
            if (Session["bank_bankname"] == null) { Session["bank_bankname"] = ""; }
            if (Session["bank_xposition"] == null) { Session["bank_xposition"] = ""; }
            if (Session["bank_street"] == null) { Session["bank_street"] = ""; }
            if (Session["bank_telephone"] == null) { Session["bank_telephone"] = ""; }
            if (Session["bank_email"] == null) { Session["bank_email"] = ""; }
            if (Session["transDate"] == null) { Session["bank_email"] = ""; }
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (fromDate.Text == "") { fromDate.Text = "0000-01-01"; }             
           
            if (toDate.Text != "")
            {
                grand_tot_cnt = ret.getCntTotalTransAdmin(fromDate.Text, toDate.Text); Session["grand_tot_cnt"] = grand_tot_cnt;
                lt_pg = ret.getPartnerGridMerchantList(fromDate.Text, toDate.Text);
                foreach (Classes.XObjs.PartnerGrid p in lt_pg)
                {
                    if (Session["IpongMemberType"] != null)
                    {
                        if (Session["IpongMemberType"].ToString() == "merchant")
                        {
                            p.tot_amt = string.Format("{0:n}", (Convert.ToInt32(p.init_amt) *Convert.ToInt32(p.xqty)));                         
                            grand_tot_amt = ret.getSumTotalTransMerchant(fromDate.Text, toDate.Text);
                        }
                        else if (Session["IpongMemberType"].ToString() == "admin")
                        {
                            p.tot_amt = string.Format("{0:n}", ((Convert.ToInt32(p.init_amt) + Convert.ToInt32(p.tech_amt)) * Convert.ToInt32(p.xqty)));
                            grand_tot_amt = ret.getSumTotalTransAdmin(fromDate.Text, toDate.Text);
                        }
                        else
                        {
                            p.tot_amt = string.Format("{0:n}", (Convert.ToInt32(p.tech_amt) * Convert.ToInt32(p.xqty)));
                            grand_tot_amt = ret.getSumTotalTransWingman(fromDate.Text, toDate.Text);
                        }
                    }
                    if (p.xmembertype == "Agent") 
                    { 
                      p.xmemberID = ret.getRegistrationByID(p.xmemberID).Firstname + " " + ret.getRegistrationByID(p.xmemberID).Surname;
                    }
                    else 
                    { 
                     p.xmemberID = ret.getSubAgentByID(p.xmemberID).Firstname + " " + ret.getSubAgentByID(p.xmemberID).Surname; 
                    }
                }
                Session["lt_pg"] = lt_pg;
                gvTm.DataSource = lt_pg;
                gvTm.DataBind();
                new_grand_tot_amt = string.Format("{0:n}", grand_tot_amt); Session["new_grand_tot_amt"] = new_grand_tot_amt;
                show_inv = 1; show_details_grid = 0; show_details_grid_wingman = 0;               
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

            if (e.CommandName == "TmDetailsClick")
            {
                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;
                string tnxref = e.CommandArgument.ToString(); Session["transID"] = tnxref;
                Classes.XObjs.Twallet twall = ret.getTwalletByTransID(tnxref);
                if (twall.xid !=null)
                {
                    string xgt = twall.xgt;
                    lt_fdets = ret.getFee_detailsByTwalletID(twall.xid);
                    Session["agentType"] = twall.xmembertype;
                    Session["transID"] = tnxref; Session["memberID"] = adminID; Session["transDate"] = twall.xreg_date;

                    if (xgt == "xpay_bk")
                    {
                        Session["payment_type"] = "Bank";
                        c_banker = ret.getBankerByID(ret.getPwalletByID(twall.xbankerID).xmemberID);
                        Session["bank_xname"] = c_banker.xname;
                        Session["bank_bankname"] = c_banker.bankname;
                        Session["bank_xposition"] = c_banker.xposition;
                        Session["bank_street"] = ret.getAddressByID(c_banker.addressID).street;
                        Session["bank_telephone"] = ret.getAddressByID(c_banker.addressID).telephone1;
                        Session["bank_email"] = ret.getAddressByID(c_banker.addressID).email1;
                        
                    }
                    else if (xgt == "xpay_isw")
                    {
                        Session["payment_type"] = "Online (Inter Switch)";
                    }
                    else
                    {
                        Session["payment_type"] = "Online";
                    }
                    show_inv = 0; 
                    if (Session["IpongMemberType"].ToString() == "merchant") { show_details_grid = 1; }
                    else { show_details_grid_wingman = 1; }
                }
            }

        }

       
        protected void btnBack_Click(object sender, EventArgs e)
        {
            show_inv = 1; show_details_grid = 0;  show_details_grid_wingman = 0;
        }

        protected void gvTm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTm.PageIndex = e.NewPageIndex;
            if (Session["lt_pg"] != null) { lt_pg.Clear(); lt_pg = (List<Classes.XObjs.PartnerGrid>)Session["lt_pg"]; }
            gvTm.DataSource = lt_pg;
            gvTm.DataBind();
            show_inv = 1; show_details_grid = 0; show_details_grid_wingman = 0;
        }

          
    }
}