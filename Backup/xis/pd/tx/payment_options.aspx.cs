using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ipong.xis.pd.tx
{
    public partial class payment_options : System.Web.UI.Page
    {
        protected string adminID = "0"; protected string agentType = "";
        public int addIsw_succ,update_twallxgt_succ = 0; 

        protected Classes.Registration reg = new Classes.Registration();
        protected Classes.XObjs.Registration c_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Registration c_sub_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Subagent c_sub = new Classes.XObjs.Subagent();

        public Ipong.Classes.XObjs.InterSwitchPostFields xispf = new Classes.XObjs.InterSwitchPostFields();


        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            { this.adminID = Session["pwalletID"].ToString(); }
            else
            { base.Response.Redirect("../../../a_login.aspx"); }

           
            if (IsPostBack)
            {
                if (rblOptions.SelectedValue == "isw")
                {
                    if (Session["xispf"] != null)
                    {
                        xispf = (Ipong.Classes.XObjs.InterSwitchPostFields)Session["xispf"];
                        if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
                        {
                            agentType = Session["agentType"].ToString();
                            if (agentType == "Agent") 
                            { 
                                c_reg = (Classes.XObjs.Registration)Session["c_reg"];
                                xispf.cust_id = c_reg.Sys_ID;
                                xispf.cust_id_desc = "Portal Agent";
                            }
                            else 
                            { 
                                c_sub = (Classes.XObjs.Subagent)Session["c_sub"];
                                xispf.cust_id = c_sub.Sys_ID;
                                xispf.cust_id_desc = "Portal Sub-Agent";
                            }
                        }
                        addIsw_succ = reg.addInterSwitchRecords(xispf);
                        if (addIsw_succ > 0)
                        {
                            update_twallxgt_succ = reg.updateTwalletXgt(xispf.txn_ref, "xpay_isw", adminID);
                            if (update_twallxgt_succ > 0)
                            {
                                Response.Redirect("./payment_details.aspx");
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("../../../A/m_pay.aspx");
                    }                   
                }
                else if (rblOptions.SelectedValue == "bank")
                {
                    if (Session["xispf"] != null)
                    {
                        xispf = (Ipong.Classes.XObjs.InterSwitchPostFields)Session["xispf"];
                        update_twallxgt_succ = reg.updateTwalletXgtBanker(xispf.txn_ref, "xpay_bk", adminID);
                        if (update_twallxgt_succ > 0)
                        {
                            Response.Redirect("../../../A/m_invoice_bank.aspx?tx=" + xispf.txn_ref);
                        }
                    }
                } 
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../../A/profile.aspx");
        }

    }
}