using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ipong
{
    public partial class profile_update_succ : System.Web.UI.Page
    {
         protected string adminID = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            {
                Session["adminID"] = null; 
                Response.Redirect("./login.aspx");
            }
            else
            { base.Response.Redirect("./login.aspx"); }
            
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("./login.aspx");
        }

    }
}