using Ipong.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ipong.A
{
    public partial class profile4 : System.Web.UI.Page
    {
        public string adminID = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((this.Session["pwalletID"] != null) && (this.Session["pwalletID"].ToString() != ""))
            {
                this.adminID = this.Session["pwalletID"].ToString();
                Retriever ret = new  Retriever();
                Classes.XObjs.Registration c_reg = ret.getRegistrationByID(adminID);

                xname.Value =  c_reg.Sys_ID;
            }
        }
    }
}