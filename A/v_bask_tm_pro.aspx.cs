using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ipong.Classes;

namespace Ipong.A
{
    public partial class v_bask_tm_pro : Page
    {
        private Retriever ret = new Retriever();
        protected string adminID = "0";       
        protected string agentType = "";        
        protected string log_date = "";   
        protected int show_inv=0;        
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int xtotal_amt=0;

        protected int gtm_cnt = 0; protected int gtmu_cnt = 0; protected int tmu_cnt = 0; protected int tm_cnt = 0;

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
                this.tmu_cnt = this.ret.getAllPaidFee_detail_ItemsCntByCat(this.adminID, "tm", this.agentType);
                this.tm_cnt = this.ret.getAllPaidFee_detail_ItemsCntByCat(this.adminID, "ds", this.agentType);
                //this.gtm_cnt = this.ret.getAllPaidFee_detail_ItemsCntByCat(this.adminID, "tm", this.agentType);
                //this.gtmu_cnt = this.ret.getAllPaidFee_detail_ItemsCntByCat(this.adminID, "ag", this.agentType);
            }
        }
    }
}