using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ipong.xis.pd.tx
{
    public partial class temp : System.Web.UI.Page
    {
        protected InterSwitch.PayDirect.Classes.Hasher hash_value = new InterSwitch.PayDirect.Classes.Hasher();
        protected string adminID = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            { this.adminID = Session["pwalletID"].ToString(); }
            else
            { base.Response.Redirect("../../../a_login.aspx"); }

            string input = "D7B9123C827745841013470000http://xpayng.com/xis/pd/xreturn/index.aspxE092D3166B4E787C6B4B9EDFE8E7E7659D47321DDF4D2644B61B709D0A0A9B9098FB7F3342813FEFCD2F0198F380C6F28D56C3E42CFDE20F8CD472EF5202E312";

            string xhash = hash_value.GetGetSHA512String(input);
        }
    }
}