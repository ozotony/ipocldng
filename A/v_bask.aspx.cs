using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ipong.Classes;

namespace Ipong.A
{
    public partial class v_bask : Page
    {
        protected string adminID = "0";
        protected int ag_cnt;
        protected string agentType = "";
        protected int ds_cnt;
        protected string log_date = "";
        protected int pt_cnt;
        private Retriever ret = new Retriever();
        protected int show_inv;
        protected int tm_cnt;
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int xtotal_amt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((this.Session["pwalletID"] != null) && (this.Session["pwalletID"].ToString() != ""))
            {
                this.adminID = this.Session["pwalletID"].ToString();
            }
            else
            {
                base.Response.Redirect("../a_login.aspx");
            }
            if ((this.Session["log_date"] != null) && (this.Session["log_date"].ToString() != ""))
            {
                this.log_date = this.Session["log_date"].ToString();
            }
            if ((this.Session["agentType"] != null) && (this.Session["agentType"].ToString() != ""))
            {
                this.agentType = this.Session["agentType"].ToString();
                this.pt_cnt = this.ret.getAllPaidFee_detail_ItemsCntByCat(this.adminID, "pt", this.agentType);
                this.ds_cnt = this.ret.getAllPaidFee_detail_ItemsCntByCat(this.adminID, "ds", this.agentType);
                this.tm_cnt = this.ret.getAllPaidFee_detail_ItemsCntByCat(this.adminID, "tm", this.agentType);
                this.ag_cnt = this.ret.getAllPaidFee_detail_ItemsCntByCat(this.adminID, "ag", this.agentType);
            }
        }
    }
}