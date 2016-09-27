using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ipong.A
{
    public partial class feelist : System.Web.UI.Page
    {
        public string agentType = "";
        public string adminID = "";
        public string log_date = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Page.IsPostBack))
            {
                if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
                {
                    agentType = Convert.ToString(Session["agentType"]);

                    adminID = Convert.ToString(Session["pwalletID"]);




                }

            }

        }
    }
}