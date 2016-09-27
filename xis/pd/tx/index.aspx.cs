using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ipong.xis.pd.tx
{
    public partial class index : System.Web.UI.Page
    {
        protected string adminID = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            { this.adminID = Session["pwalletID"].ToString(); }
            else
            { base.Response.Redirect("../../../a_login.aspx"); }
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            Response.Redirect("./payment_options.aspx");
        }
    }
}