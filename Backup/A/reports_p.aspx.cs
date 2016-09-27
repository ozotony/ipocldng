using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ipong.A
{
    public partial class reports_p : System.Web.UI.Page
    {
        protected string adminID = "0"; protected string agentType = ""; protected string log_date = "";        

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            { this.adminID = Session["pwalletID"].ToString(); }
            else
            { base.Response.Redirect("../a_login.aspx"); }
            if ((Session["log_date"] != null) && (Session["log_date"].ToString() != "")) { this.log_date = Session["log_date"].ToString(); }     

            if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
            {
              
            }           
           
        }

    }
}