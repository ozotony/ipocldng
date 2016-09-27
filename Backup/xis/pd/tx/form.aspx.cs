using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


namespace Ipong.xis.pd.tx
{
    public partial class form : System.Web.UI.Page
    {
        protected InterSwitch.PayDirect.Classes.Hasher hash_value = new InterSwitch.PayDirect.Classes.Hasher();
        public string inputString = ""; public string hash = ""; protected string adminID = "0";
        public string product_id = "0"; public string currency = ""; public string site_redirect_url = "";
        public string txn_ref = ""; public string pay_item_id = "0"; public string amount = "";
        public string einao_split_amt = "0"; public string cld_split_amt = "0"; public string coy_name = "";
        public string name = ""; public string pd_payment_page = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            { this.adminID = Session["pwalletID"].ToString(); }
            else
            { base.Response.Redirect("../../../a_login.aspx"); }

            product_id = ConfigurationManager.AppSettings["pd_product_id"];
            currency = ConfigurationManager.AppSettings["pd_currency"];
            site_redirect_url = ConfigurationManager.AppSettings["pd_site_redirect_url"];
            pay_item_id = ConfigurationManager.AppSettings["pd_pay_item_id"];
            pd_payment_page = ConfigurationManager.AppSettings["pd_payment_page"];

            if (Session["Refno"] != null) { txn_ref = Session["Refno"].ToString(); }
            if (Session["total_amt"] != null) { amount = Session["total_amt"].ToString(); }
            if (Session["hashString"] != null) { hash = Session["hashString"].ToString(); }
            if (Session["einao_split_amt"] != null) { einao_split_amt = Session["einao_split_amt"].ToString(); }
            if (Session["cld_split_amt"] != null) { cld_split_amt = Session["cld_split_amt"].ToString(); }
            if (Session["name"] != null) { name = Session["name"].ToString(); }
            if (Session["coy_name"] != null) { coy_name = Session["coy_name"].ToString(); } 
        }
    }
}