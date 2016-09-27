using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


namespace Ipong.xis.pd.tx
{
    public partial class payment_details : System.Web.UI.Page
    {
        public Ipong.Classes.XObjs.InterSwitchPostFields xispf = new Classes.XObjs.InterSwitchPostFields();
        protected InterSwitch.PayDirect.Classes.Transactions xtrans = new InterSwitch.PayDirect.Classes.Transactions();       

        public string name = ""; public string coy_name = ""; public string refno = "";
        public string addy = ""; public string isw_conv_fee = "0"; public string amt = "0"; public string total_amt = "0";
        public int btnProceedShow = 1; protected string adminID = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            { this.adminID = Session["pwalletID"].ToString(); }
            else
            { base.Response.Redirect("../../../a_login.aspx"); }

            if (Session["amt"] != null) { amt = Session["amt"].ToString(); }
            if (Session["isw_conv_fee"] != null) { isw_conv_fee = Session["isw_conv_fee"].ToString(); }
            if (Session["total_amt"] != null) { total_amt = Session["total_amt"].ToString(); }
            if (Session["name"] != null) { name = Session["name"].ToString(); }
            if (Session["coy_name"] != null) { coy_name =Session["coy_name"].ToString(); }          
            if (Session["Refno"] != null) { refno =Session["Refno"].ToString(); }
            if (Session["Address"] != null) { addy = Session["Address"].ToString(); }

            amt = string.Format("{0:n}", amt);
            isw_conv_fee = string.Format("{0:n}", isw_conv_fee);
            if (addy.Contains(',')) { addy = addy.Replace(",", ", "); }
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            Response.Redirect("./form.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../../A/profile.aspx");
        }
    }
}