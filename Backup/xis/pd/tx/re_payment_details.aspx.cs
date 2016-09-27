using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


namespace Ipong.xis.pd.tx
{
    public partial class re_payment_details : System.Web.UI.Page
    {
        public Ipong.Classes.XObjs.InterSwitchPostFields xispf = new Classes.XObjs.InterSwitchPostFields();
        protected InterSwitch.PayDirect.Classes.Transactions xtrans = new InterSwitch.PayDirect.Classes.Transactions();       

        public string name = ""; public string coy_name = ""; public string refno = "";
        public string addy = ""; public string isw_conv_fee = "0"; public string amt = "0"; public string total_amt = "0";
        public int btnProceedShow = 1; protected string adminID = "0";protected string agentType = ""; protected string c_addy = "";

        protected Classes.Retriever ret = new Classes.Retriever();
        protected Classes.XObjs.Registration c_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Registration c_sub_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Subagent c_sub = new Classes.XObjs.Subagent();
        protected List<Classes.XObjs.Twallet> twall = new List<Classes.XObjs.Twallet>();

        protected List<Classes.XObjs.Fee_details> lt_fdets = new List<Classes.XObjs.Fee_details>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            { this.adminID = Session["pwalletID"].ToString(); }
            else
            { base.Response.Redirect("../../../a_login.aspx"); }
             if (Session["xispf"] != null) { xispf = (Ipong.Classes.XObjs.InterSwitchPostFields)Session["xispf"]; }

             if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
             {
                 agentType = Session["agentType"].ToString();
                 if (agentType == "Agent")
                 {
                     if (Session["c_reg"] != null)
                     {
                         c_reg = (Classes.XObjs.Registration)Session["c_reg"];
                         Session["name"] = c_reg.Firstname + " " + c_reg.Surname;
                         Session["coy_name"] = c_reg.CompanyName;
                         Session["Address"] = c_reg.CompanyAddress;
                     }

                 }
                 else
                 {
                     Classes.XObjs.Registration c_sub_reg = new Classes.XObjs.Registration();

                     if (Session["c_sub"] != null)
                     {
                         c_sub = (Classes.XObjs.Subagent)Session["c_sub"];
                         Session["name"] = c_sub.Firstname + " " + c_sub.Surname;
                     }
                     if (Session["c_sub_reg"] != null)
                     {
                         c_sub_reg = (Classes.XObjs.Registration)Session["c_sub_reg"];
                         Session["coy_name"] = c_sub_reg.CompanyName;
                         Session["Address"] = c_sub_reg.CompanyAddress;
                     }

                 }
             }

             twall = ret.getTwalletByMemberID(adminID, xispf.txn_ref, agentType);
            lt_fdets = ret.getFee_detailsByTwalletID(twall[0].xid);

            int cld_amt = 0; int einao_amt = 0;
            if (lt_fdets.Count > 0)
            {
                foreach (Classes.XObjs.Fee_details f in lt_fdets)
                {
                    cld_amt += Convert.ToInt32(f.init_amt) * Convert.ToInt32(f.xqty) * 100;
                    einao_amt += Convert.ToInt32(f.tech_amt) * Convert.ToInt32(f.xqty) * 100;
                }
            }
            refno = xispf.txn_ref; 
            isw_conv_fee = Math.Round(Convert.ToDecimal(xispf.isw_conv_fee), 2).ToString(); 
            total_amt =Convert.ToString( xispf.amount);
            amt = Convert.ToString((Convert.ToDecimal(total_amt)/100) - Convert.ToDecimal(isw_conv_fee));
            
            name = Session["name"].ToString(); 
            coy_name =Session["coy_name"].ToString();           
            addy = Session["Address"].ToString(); 

            amt = string.Format("{0:n}", amt);
            isw_conv_fee = string.Format("{0:n}", isw_conv_fee);

            Session["amt"] = amt;
            Session["isw_conv_fee"] = isw_conv_fee;
            Session["total_amt"] = total_amt;
            Session["Refno"] = refno;
            Session["einao_split_amt"] = einao_amt.ToString(); ;
            Session["cld_split_amt"] = cld_amt.ToString();
            Session["hashString"] = xispf.hash;
            if (addy.Contains(',')) { addy = addy.Replace(",", ", "); }
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
           // Session["total_amt"] = Convert.ToString(Convert.ToInt32(total_amt)*100);
            Response.Redirect("./form.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../../A/v_bask_tmu.aspx");
        }
    }
}