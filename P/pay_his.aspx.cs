using Ipong.Classes;
using Ipong.InterSwitch.PayDirect.Classes;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Ipong.P
{
    public partial class pay_his : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        protected XObjs.XBanker c_banker = new XObjs.XBanker();
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
        protected string fullname = "";
        protected int grand_tot_amt;
        protected int grand_tot_cnt;
        protected XObjs.InterSwitchResponse isr = new XObjs.InterSwitchResponse();
        protected XObjs.InterSwitchPostFields isw_fields = new XObjs.InterSwitchPostFields();
        protected List<XObjs.Fee_details> lt_fdets = new List<XObjs.Fee_details>();
        protected List<XObjs.PartnerGrid> lt_pg = new List<XObjs.PartnerGrid>();
        protected List<XObjs.Twallet> lt_twall = new List<XObjs.Twallet>();
        protected string mobile = "";
        protected string new_grand_tot_amt = "";
        protected string pr = "";
        protected string ref_no = "";
        protected Ipong.Classes.Registration reg = new Ipong.Classes.Registration();
        protected Retriever ret = new Retriever();
        protected string search_msg = "";
        protected int show_banker_grid;
        protected int show_details_grid;
        protected int show_details_grid_wingman;
        protected int show_inv;
        protected int tm_cnt;
        protected int tot_amtx;
        protected string transID = "";
        protected Transactions tx = new Transactions();
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int xtotal_amt;

        protected void btnBack_Click(object sender, EventArgs e)
        {
            this.show_inv = 1;
            this.show_details_grid = 0;
            this.show_details_grid_wingman = 0;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.fromDate.Text == "")
            {
                this.fromDate.Text = "0000-01-01";
            }
            if (this.toDate.Text != "")
            {
                this.grand_tot_cnt = this.ret.getCntTotalTransAdmin(this.fromDate.Text, this.toDate.Text);
                this.Session["grand_tot_cnt"] = this.grand_tot_cnt;
                this.lt_pg = this.ret.getPartnerGridMerchantList(this.fromDate.Text, this.toDate.Text);
                foreach (XObjs.PartnerGrid grid in this.lt_pg)
                {
                    if (this.Session["IpongMemberType"] != null)
                    {
                        if (this.Session["IpongMemberType"].ToString() == "merchant")
                        {
                            grid.tot_amt = string.Format("{0:n}", Convert.ToInt32(grid.init_amt) * Convert.ToInt32(grid.xqty));
                            this.grand_tot_amt = this.ret.getSumTotalTransMerchant(this.fromDate.Text, this.toDate.Text);
                        }
                        else if (this.Session["IpongMemberType"].ToString() == "admin")
                        {
                            grid.tot_amt = string.Format("{0:n}", (Convert.ToInt32(grid.init_amt) + Convert.ToInt32(grid.tech_amt)) * Convert.ToInt32(grid.xqty));
                            this.grand_tot_amt = this.ret.getSumTotalTransAdmin(this.fromDate.Text, this.toDate.Text);
                        }
                        else
                        {
                            grid.tot_amt = string.Format("{0:n}", Convert.ToInt32(grid.tech_amt) * Convert.ToInt32(grid.xqty));
                            this.grand_tot_amt = this.ret.getSumTotalTransWingman(this.fromDate.Text, this.toDate.Text);
                        }
                    }
                    if (grid.xmembertype == "Agent")
                    {
                        grid.xmemberID = this.ret.getRegistrationByID(grid.xmemberID).Firstname + " " + this.ret.getRegistrationByID(grid.xmemberID).Surname;
                    }
                    else
                    {
                        grid.xmemberID = this.ret.getSubAgentByID(grid.xmemberID).Firstname + " " + this.ret.getSubAgentByID(grid.xmemberID).Surname;
                    }
                }
                this.Session["lt_pg"] = this.lt_pg;
                this.gvTm.DataSource = this.lt_pg;
                this.gvTm.DataBind();
                this.new_grand_tot_amt = string.Format("{0:n}", this.grand_tot_amt);
                this.Session["new_grand_tot_amt"] = this.new_grand_tot_amt;
                this.show_inv = 1;
                this.show_details_grid = 0;
                this.show_details_grid_wingman = 0;
            }
            else
            {
                this.show_inv = 0;
                this.search_msg = "";
                this.search_msg = "THE \"TO\" DATE FIELD CANNOT BE EMPTY, PLEASE SELECT A DATE AND TRY AGAIN!!";
            }
        }

        protected void gvTm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvTm.PageIndex = e.NewPageIndex;
            if (this.Session["lt_pg"] != null)
            {
                this.lt_pg.Clear();
                this.lt_pg = (List<XObjs.PartnerGrid>)this.Session["lt_pg"];
            }
            this.gvTm.DataSource = this.lt_pg;
            this.gvTm.DataBind();
            this.show_inv = 1;
            this.show_details_grid = 0;
            this.show_details_grid_wingman = 0;
        }

        protected void gvTm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "TmDetailsClick")
            {
                GridViewRow namingContainer = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string transID = e.CommandArgument.ToString();
                this.Session["transID"] = transID;
                XObjs.Twallet twallet = this.ret.getTwalletByTransID(transID);
                if (twallet.xid != null)
                {
                    string xgt = twallet.xgt;
                    this.lt_fdets = this.ret.getFee_detailsByTwalletID(twallet.xid);
                    this.Session["agentType"] = twallet.xmembertype;
                    this.Session["transID"] = transID;
                    this.Session["memberID"] = this.adminID;
                    this.Session["transDate"] = twallet.xreg_date;
                    if (xgt == "xpay_bk")
                    {
                        this.Session["payment_type"] = "Bank";
                        this.c_banker = this.ret.getBankerByID(this.ret.getPwalletByID(twallet.xbankerID).xmemberID);
                        this.Session["bank_xname"] = this.c_banker.xname;
                        this.Session["bank_bankname"] = this.c_banker.bankname;
                        this.Session["bank_xposition"] = this.c_banker.xposition;
                        this.Session["bank_street"] = this.ret.getAddressByID(this.c_banker.addressID).street;
                        this.Session["bank_telephone"] = this.ret.getAddressByID(this.c_banker.addressID).telephone1;
                        this.Session["bank_email"] = this.ret.getAddressByID(this.c_banker.addressID).email1;
                    }
                    else if (xgt == "xpay_isw")
                    {
                        this.Session["payment_type"] = "Online (Inter Switch)";
                    }
                    else
                    {
                        this.Session["payment_type"] = "Online";
                    }
                    this.show_inv = 0;
                    if (this.Session["IpongMemberType"].ToString() == "merchant")
                    {
                        this.show_details_grid = 1;
                    }
                    else
                    {
                        this.show_details_grid_wingman = 1;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((this.Session["pwalletID"] != null) && (this.Session["pwalletID"].ToString() != ""))
            {
                this.adminID = this.Session["pwalletID"].ToString();
            }
            else
            {
                base.Response.Redirect("../login.aspx");
            }
            if (!base.IsPostBack)
            {
                this.Session["IpongMemberType"] = null;
                this.Session["grand_tot_cnt"] = null;
                this.Session["new_grand_tot_amt"] = null;
                this.Session["transID"] = null;
                this.Session["payment_type"] = null;
                this.Session["bank_xname"] = null;
                this.Session["bank_bankname"] = null;
                this.Session["bank_xposition"] = null;
                this.Session["bank_street"] = null;
                this.Session["bank_telephone"] = null;
                this.Session["bank_email"] = null;
                this.Session["transDate"] = null;
            }
            this.c_pr = this.ret.getPratioByMemberID(this.adminID);
            if (this.c_pr.xid != null)
            {
                this.Session["IpongMemberType"] = this.c_pr.p_type;
                if (this.Session["IpongMemberType"] != null)
                {
                    this.pr = this.Session["IpongMemberType"].ToString();
                }
            }
            if (this.Session["grand_tot_cnt"] == null)
            {
                this.Session["grand_tot_cnt"] = "0";
            }
            if (this.Session["new_grand_tot_amt"] == null)
            {
                this.Session["new_grand_tot_amt"] = "0";
            }
            if (this.Session["transID"] == null)
            {
                this.Session["transID"] = "0";
            }
            if (this.Session["payment_type"] == null)
            {
                this.Session["payment_type"] = "";
            }
            if (this.Session["bank_xname"] == null)
            {
                this.Session["bank_xname"] = "";
            }
            if (this.Session["bank_bankname"] == null)
            {
                this.Session["bank_bankname"] = "";
            }
            if (this.Session["bank_xposition"] == null)
            {
                this.Session["bank_xposition"] = "";
            }
            if (this.Session["bank_street"] == null)
            {
                this.Session["bank_street"] = "";
            }
            if (this.Session["bank_telephone"] == null)
            {
                this.Session["bank_telephone"] = "";
            }
            if (this.Session["bank_email"] == null)
            {
                this.Session["bank_email"] = "";
            }
            if (this.Session["transDate"] == null)
            {
                this.Session["bank_email"] = "";
            }
        }
    }
}