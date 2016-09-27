using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ipong.A
{
    public partial class v_bask : System.Web.UI.Page
    {
        protected string adminID = "0"; protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int show_inv = 0; protected int xtotal_amt = 0; protected string agentType = ""; protected string log_date = ""; 
        private Classes.Retriever ret = new Classes.Retriever();
       

        protected int pt_cnt = 0; protected int ds_cnt = 0; protected int tm_cnt = 0; protected int ag_cnt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            { this.adminID = Session["pwalletID"].ToString(); }
            else
            { base.Response.Redirect("../a_login.aspx"); }
            if ((Session["log_date"] != null) && (Session["log_date"].ToString() != "")) { this.log_date = Session["log_date"].ToString(); }     

            if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
            {
                agentType = Session["agentType"].ToString();
                pt_cnt = ret.getAllPaidFee_detail_ItemsCntByCat(adminID, "pt", agentType);
                ds_cnt = ret.getAllPaidFee_detail_ItemsCntByCat(adminID, "ds", agentType);
                tm_cnt = ret.getAllPaidFee_detail_ItemsCntByCat(adminID, "tm", agentType); 
                ag_cnt = ret.getAllPaidFee_detail_ItemsCntByCat(adminID, "ag", agentType);
            }           
           
        }

    }
}