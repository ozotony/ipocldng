﻿using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;


namespace Ipong.B
{
    public partial class profile : System.Web.UI.Page
    {
        protected string adminID = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            { this.adminID = Session["pwalletID"].ToString(); }
            else
            { base.Response.Redirect("../login.aspx"); }
        }
    }
}