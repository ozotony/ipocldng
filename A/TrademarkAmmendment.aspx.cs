using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ipong.A
{
    public partial class TrademarkAmmendment : System.Web.UI.Page
    {
        public string log_date = "";
        protected string agentType = "";
        public string adminID = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((this.Session["agentType"] != null) && (this.Session["agentType"].ToString() != ""))
            {
                this.agentType = this.Session["agentType"].ToString();
            }
            else
            {
                // base.Response.Redirect("../a_login.aspx");
            }

            if ((this.Session["pwalletID"] != null) && (this.Session["pwalletID"].ToString() != ""))
            {
                this.adminID = this.Session["pwalletID"].ToString();
            }
            else
            {
                //  base.Response.Redirect("../a_login.aspx");
            }

            if (Session["c_reg"] != null)
            {
                Classes.XObjs.Registration c_reg2 = (Classes.XObjs.Registration)Session["c_reg"];

                xname.Value = c_reg2.Surname;
                xaddress.Value = c_reg2.CompanyAddress;
                xemail.Value = c_reg2.Email;
                xPhoneNumber.Value = c_reg2.PhoneNumber;
                xpwalletID.Value = c_reg2.xid;
            }

            if ((this.Session["log_date"] != null) && (this.Session["log_date"].ToString() != ""))
            {
                this.log_date = this.Session["log_date"].ToString();
            }
        }
    }
}