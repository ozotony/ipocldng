using Ipong.Classes;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Ipong.P
{
    public partial class pay_stats : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        public int apr;
        public int aug;
        protected XObjs.PRatio c_pr = new XObjs.PRatio();
        public int dec;
        public int feb;
        protected int grand_tot_amt;
        protected int grand_tot_cnt;
        public int jan;
        public int jul;
        public int jun;
        public int mar;
        public int may;
        protected string new_date = "";
        protected string new_grand_tot_amt = "";
        public int nov;
        public int oct;
        protected string old_date = "";
        protected string pr = "";
        protected Ipong.Classes.Registration reg = new Ipong.Classes.Registration();
        protected Retriever ret = new Retriever();
        protected string search_msg = "";
        public int sep;
        protected int show_inv;
        protected int tm_cnt;
        protected string transID = "";
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.grand_tot_cnt = this.ret.getCntTotalTransAdminGraph(this.ddl_year.SelectedValue);
            this.Session["grand_tot_cnt"] = this.grand_tot_cnt;
            if (this.Session["IpongMemberType"] != null)
            {
                if (this.Session["IpongMemberType"].ToString() == "merchant")
                {
                    this.jan = this.ret.getSumTotalByMonthMerchant(this.ddl_year.SelectedValue, "01");
                    this.feb = this.ret.getSumTotalByMonthMerchant(this.ddl_year.SelectedValue, "02");
                    this.mar = this.ret.getSumTotalByMonthMerchant(this.ddl_year.SelectedValue, "03");
                    this.apr = this.ret.getSumTotalByMonthMerchant(this.ddl_year.SelectedValue, "04");
                    this.may = this.ret.getSumTotalByMonthMerchant(this.ddl_year.SelectedValue, "05");
                    this.jun = this.ret.getSumTotalByMonthMerchant(this.ddl_year.SelectedValue, "06");
                    this.jul = this.ret.getSumTotalByMonthMerchant(this.ddl_year.SelectedValue, "07");
                    this.aug = this.ret.getSumTotalByMonthMerchant(this.ddl_year.SelectedValue, "08");
                    this.sep = this.ret.getSumTotalByMonthMerchant(this.ddl_year.SelectedValue, "09");
                    this.oct = this.ret.getSumTotalByMonthMerchant(this.ddl_year.SelectedValue, "10");
                    this.nov = this.ret.getSumTotalByMonthMerchant(this.ddl_year.SelectedValue, "11");
                    this.dec = this.ret.getSumTotalByMonthMerchant(this.ddl_year.SelectedValue, "12");
                    this.grand_tot_amt = ((((((((((this.jan + this.feb) + this.mar) + this.apr) + this.may) + this.jun) + this.jul) + this.aug) + this.sep) + this.oct) + this.nov) + this.dec;
                }
                else if (this.Session["IpongMemberType"].ToString() == "admin")
                {
                    this.jan = this.ret.getSumTotalByMonthAdmin(this.ddl_year.SelectedValue, "01");
                    this.feb = this.ret.getSumTotalByMonthAdmin(this.ddl_year.SelectedValue, "02");
                    this.mar = this.ret.getSumTotalByMonthAdmin(this.ddl_year.SelectedValue, "03");
                    this.apr = this.ret.getSumTotalByMonthAdmin(this.ddl_year.SelectedValue, "04");
                    this.may = this.ret.getSumTotalByMonthAdmin(this.ddl_year.SelectedValue, "05");
                    this.jun = this.ret.getSumTotalByMonthAdmin(this.ddl_year.SelectedValue, "06");
                    this.jul = this.ret.getSumTotalByMonthAdmin(this.ddl_year.SelectedValue, "07");
                    this.aug = this.ret.getSumTotalByMonthAdmin(this.ddl_year.SelectedValue, "08");
                    this.sep = this.ret.getSumTotalByMonthAdmin(this.ddl_year.SelectedValue, "09");
                    this.oct = this.ret.getSumTotalByMonthAdmin(this.ddl_year.SelectedValue, "10");
                    this.nov = this.ret.getSumTotalByMonthAdmin(this.ddl_year.SelectedValue, "11");
                    this.dec = this.ret.getSumTotalByMonthAdmin(this.ddl_year.SelectedValue, "12");
                    this.grand_tot_amt = ((((((((((this.jan + this.feb) + this.mar) + this.apr) + this.may) + this.jun) + this.jul) + this.aug) + this.sep) + this.oct) + this.nov) + this.dec;
                }
                else
                {
                    this.jan = this.ret.getSumTotalByMonthWingman(this.ddl_year.SelectedValue, "01");
                    this.feb = this.ret.getSumTotalByMonthWingman(this.ddl_year.SelectedValue, "02");
                    this.mar = this.ret.getSumTotalByMonthWingman(this.ddl_year.SelectedValue, "03");
                    this.apr = this.ret.getSumTotalByMonthWingman(this.ddl_year.SelectedValue, "04");
                    this.may = this.ret.getSumTotalByMonthWingman(this.ddl_year.SelectedValue, "05");
                    this.jun = this.ret.getSumTotalByMonthWingman(this.ddl_year.SelectedValue, "06");
                    this.jul = this.ret.getSumTotalByMonthWingman(this.ddl_year.SelectedValue, "07");
                    this.aug = this.ret.getSumTotalByMonthWingman(this.ddl_year.SelectedValue, "08");
                    this.sep = this.ret.getSumTotalByMonthWingman(this.ddl_year.SelectedValue, "09");
                    this.oct = this.ret.getSumTotalByMonthWingman(this.ddl_year.SelectedValue, "10");
                    this.nov = this.ret.getSumTotalByMonthWingman(this.ddl_year.SelectedValue, "11");
                    this.dec = this.ret.getSumTotalByMonthWingman(this.ddl_year.SelectedValue, "12");
                    this.grand_tot_amt = ((((((((((this.jan + this.feb) + this.mar) + this.apr) + this.may) + this.jun) + this.jul) + this.aug) + this.sep) + this.oct) + this.nov) + this.dec;
                }
                this.new_grand_tot_amt = string.Format("{0:n}", this.grand_tot_amt);
                this.Session["new_grand_tot_amt"] = this.new_grand_tot_amt;
                this.show_inv = 1;
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
                this.old_date = this.ret.getOldestDate();
                this.Session["old_date"] = this.old_date;
                this.new_date = this.ret.getLatestDate();
                this.Session["new_date"] = this.new_date;
                this.popYear();
                this.Session["IpongMemberType"] = null;
                this.Session["grand_tot_cnt"] = null;
                this.Session["new_grand_tot_amt"] = null;
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
        }

        public void popYear()
        {
            if (this.Session["old_date"] != null)
            {
                this.old_date = this.Session["old_date"].ToString();
            }
            if (this.Session["new_date"] != null)
            {
                this.new_date = this.Session["new_date"].ToString();
            }
            for (int i = Convert.ToInt32(this.old_date); i <= Convert.ToInt32(this.new_date); i++)
            {
                ListItem item = new ListItem();
                item.Text = i.ToString();
                item.Value = i.ToString();
                this.ddl_year.Items.Add(item);
            }
        }
    }
}