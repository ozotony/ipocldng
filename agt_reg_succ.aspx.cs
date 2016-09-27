using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ipong
{
    public partial class agt_reg_succ : System.Web.UI.Page
    {
        public string x = ""; public string m = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["x"] != null) && (Request.QueryString["x"] != ""))
            {
                x = Request.QueryString["x"].ToString().ToUpper();
            }
            else { Response.Redirect("mem_reg.aspx"); }
            if ((Request.QueryString["m"] != null) && (Request.QueryString["m"] != ""))
            {
                m = Request.QueryString["m"].ToString().ToUpper();
            }
            else { Response.Redirect("mem_reg.aspx"); }
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("./login.aspx");
        }

    }
}