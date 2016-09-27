using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ipong
{
    public partial class scard_manager : System.Web.UI.Page
    {
        public Classes.ScardManager scm = new Classes.ScardManager();
        public Classes.XObjs.Scard sc = new Classes.XObjs.Scard();
        public List<Classes.XObjs.Scard> lt_scards = new List<Classes.XObjs.Scard>();

        public string xvisible = "1"; public string xsync = "0"; public string xreg_date=DateTime.Now.ToString("yyyy-MM-dd");
        public string xlogstaff = "1"; public string xvalid = "1"; protected string adminID = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["pwalletID"] != null)
            {
                if (Session["pwalletID"].ToString() != "")
                {
                    this.adminID = Session["pwalletID"].ToString();
                }
                else
                {
                    base.Response.Redirect("./login.aspx");
                }
            }
            else
            {
                base.Response.Redirect("./login.aspx");
            }

            lt_scards = scm.GenerateGuidNum(12, 200000);
            if (lt_scards.Count > 0)
            {
                scm.addScards(lt_scards);
            }         
        }
    }
}