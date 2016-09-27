using Ipong.Classes;
using Ipong.InterSwitch.PayDirect.Classes;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Ipong.P
{
    public partial class pay_hisx : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        protected XObjs.Fee_details c_fdets = new XObjs.Fee_details();
        protected XObjs.XPartner c_partner = new XObjs.XPartner();
        protected XObjs.PRatio c_pr = new XObjs.PRatio();
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected string check_trans_page = "";
        protected string coy_name = "";
        protected string cust_id = "";
        protected string email = "";
        protected ErrorHandler err = new ErrorHandler();
        protected int eu;
        protected string fullname = "";
        protected int grand_tot;
        protected Hasher hash_value = new Hasher();
        protected XObjs.InterSwitchResponse isr = new XObjs.InterSwitchResponse();
        protected XObjs.InterSwitchPostFields isw_fields = new XObjs.InterSwitchPostFields();
        protected List<XObjs.Fee_details> lt_fdets = new List<XObjs.Fee_details>();
        protected List<XObjs.Twallet> lt_twall = new List<XObjs.Twallet>();
        protected string mobile = "";
        protected int nume;
        protected int pages;
        protected string ref_no = "";
        protected Ipong.Classes.Registration reg = new Ipong.Classes.Registration();
        protected Retriever ret = new Retriever();
        protected int show_inv;
        protected int tm_cnt;
        protected int tot_amtx;
        protected string transID = "";
        protected Transactions tx = new Transactions();
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int xtotal_amt;

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}