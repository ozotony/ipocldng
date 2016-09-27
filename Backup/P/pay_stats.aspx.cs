using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Ipong.P
{
    public partial class pay_stats : System.Web.UI.Page
    {
        protected string adminID = "0"; protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int show_inv = 0; protected string pr = ""; protected string agentType = "";
        protected string old_date = ""; protected string new_date = ""; protected string transID = "";
     
        protected int grand_tot_cnt = 0;  protected int grand_tot_amt = 0; protected int tm_cnt = 0;
        protected string new_grand_tot_amt = ""; protected string search_msg = "";

        public int jan = 0; public int feb = 0; public int mar = 0; public int apr = 0;
        public int may = 0; public int jun = 0; public int jul = 0; public int aug = 0;
        public int sep = 0; public int oct = 0; public int nov = 0; public int dec = 0;

        protected Classes.XObjs.PRatio c_pr = new Classes.XObjs.PRatio();
        protected Classes.Retriever ret = new Classes.Retriever();
        protected Classes.Registration reg = new Classes.Registration();


        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            { this.adminID = Session["pwalletID"].ToString(); }
            else
            { base.Response.Redirect("../login.aspx"); }

            if (!IsPostBack)
            {
                old_date = ret.getOldestDate(); Session["old_date"] = old_date;
                new_date = ret.getLatestDate(); Session["new_date"] = new_date;
                popYear();
                Session["IpongMemberType"] = null;
                Session["grand_tot_cnt"] = null;
                Session["new_grand_tot_amt"] = null;
            }
            c_pr = ret.getPratioByMemberID(adminID);
            if (c_pr.xid != null)
            {
                Session["IpongMemberType"] = c_pr.p_type;
                if (Session["IpongMemberType"] != null) { pr = Session["IpongMemberType"].ToString(); }
            }

           
            if(Session["grand_tot_cnt"]==null){ Session["grand_tot_cnt"]="0";}
            if (Session["new_grand_tot_amt"] == null) { Session["new_grand_tot_amt"] = "0"; }
            
        }

        public void popYear()
        {
            if (Session["old_date"] != null) { old_date = Session["old_date"].ToString(); }
            if (Session["new_date"] != null) { new_date = Session["new_date"].ToString(); }            
            for (int i = Convert.ToInt32(old_date); i <= Convert.ToInt32(new_date); i++)
            {
                ListItem li = new ListItem();
                li.Text = i.ToString(); li.Value = i.ToString();
                ddl_year.Items.Add(li);
            }            
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grand_tot_cnt = ret.getCntTotalTransAdminGraph(ddl_year.SelectedValue); Session["grand_tot_cnt"] = grand_tot_cnt;

        if (Session["IpongMemberType"] != null)
        {
            if (Session["IpongMemberType"].ToString() == "merchant")
            {
                jan = ret.getSumTotalByMonthMerchant(ddl_year.SelectedValue, "01");
                feb = ret.getSumTotalByMonthMerchant(ddl_year.SelectedValue, "02");
                mar = ret.getSumTotalByMonthMerchant(ddl_year.SelectedValue, "03");
                apr = ret.getSumTotalByMonthMerchant(ddl_year.SelectedValue, "04");
                may = ret.getSumTotalByMonthMerchant(ddl_year.SelectedValue, "05");
                jun = ret.getSumTotalByMonthMerchant(ddl_year.SelectedValue, "06");
                jul = ret.getSumTotalByMonthMerchant(ddl_year.SelectedValue, "07");
                aug = ret.getSumTotalByMonthMerchant(ddl_year.SelectedValue, "08");
                sep = ret.getSumTotalByMonthMerchant(ddl_year.SelectedValue, "09");
                oct = ret.getSumTotalByMonthMerchant(ddl_year.SelectedValue, "10");
                nov = ret.getSumTotalByMonthMerchant(ddl_year.SelectedValue, "11");
                dec = ret.getSumTotalByMonthMerchant(ddl_year.SelectedValue, "12");
                grand_tot_amt = jan + feb + mar + apr + may + jun + jul + aug + sep + oct + nov + dec;
            }
            else if (Session["IpongMemberType"].ToString() == "admin")
            {
                jan = ret.getSumTotalByMonthAdmin(ddl_year.SelectedValue, "01");
                feb = ret.getSumTotalByMonthAdmin(ddl_year.SelectedValue, "02");
                mar = ret.getSumTotalByMonthAdmin(ddl_year.SelectedValue, "03");
                apr = ret.getSumTotalByMonthAdmin(ddl_year.SelectedValue, "04");
                may = ret.getSumTotalByMonthAdmin(ddl_year.SelectedValue, "05");
                jun = ret.getSumTotalByMonthAdmin(ddl_year.SelectedValue, "06");
                jul = ret.getSumTotalByMonthAdmin(ddl_year.SelectedValue, "07");
                aug = ret.getSumTotalByMonthAdmin(ddl_year.SelectedValue, "08");
                sep = ret.getSumTotalByMonthAdmin(ddl_year.SelectedValue, "09");
                oct = ret.getSumTotalByMonthAdmin(ddl_year.SelectedValue, "10");
                nov = ret.getSumTotalByMonthAdmin(ddl_year.SelectedValue, "11");
                dec = ret.getSumTotalByMonthAdmin(ddl_year.SelectedValue, "12");
                grand_tot_amt = jan + feb + mar + apr + may + jun + jul + aug + sep + oct + nov + dec;
            }
            else
            {
                jan = ret.getSumTotalByMonthWingman(ddl_year.SelectedValue, "01");
                feb = ret.getSumTotalByMonthWingman(ddl_year.SelectedValue, "02");
                mar = ret.getSumTotalByMonthWingman(ddl_year.SelectedValue, "03");
                apr = ret.getSumTotalByMonthWingman(ddl_year.SelectedValue, "04");
                may = ret.getSumTotalByMonthWingman(ddl_year.SelectedValue, "05");
                jun = ret.getSumTotalByMonthWingman(ddl_year.SelectedValue, "06");
                jul = ret.getSumTotalByMonthWingman(ddl_year.SelectedValue, "07");
                aug = ret.getSumTotalByMonthWingman(ddl_year.SelectedValue, "08");
                sep = ret.getSumTotalByMonthWingman(ddl_year.SelectedValue, "09");
                oct = ret.getSumTotalByMonthWingman(ddl_year.SelectedValue, "10");
                nov = ret.getSumTotalByMonthWingman(ddl_year.SelectedValue, "11");
                dec = ret.getSumTotalByMonthWingman(ddl_year.SelectedValue, "12");
                grand_tot_amt = jan + feb + mar + apr + may + jun + jul + aug + sep + oct + nov + dec;
            }
            new_grand_tot_amt = string.Format("{0:n}", grand_tot_amt); Session["new_grand_tot_amt"] = new_grand_tot_amt;
            show_inv = 1;
        }
        }
    
             
    }
}