using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Ipong.P
{
    public partial class pay_hisx : System.Web.UI.Page
    {
        protected string adminID = "0"; protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int show_inv = 0; protected int xtotal_amt = 0; protected string agentType = "";
        protected string fullname = ""; protected string email = ""; protected string mobile = "";
        protected string transID = ""; protected string ref_no = ""; protected string check_trans_page = "";
        protected int tot_amtx = 0; protected string coy_name = ""; protected string cust_id = "";

        protected int nume = 0; protected int grand_tot = 0; protected int tm_cnt = 0;

        protected int eu = 0; protected int pages = 0; 


        protected InterSwitch.PayDirect.Classes.ErrorHandler err = new InterSwitch.PayDirect.Classes.ErrorHandler();
        protected InterSwitch.PayDirect.Classes.Transactions tx = new InterSwitch.PayDirect.Classes.Transactions();
        protected InterSwitch.PayDirect.Classes.Hasher hash_value = new InterSwitch.PayDirect.Classes.Hasher();
        protected Classes.XObjs.InterSwitchPostFields isw_fields = new Classes.XObjs.InterSwitchPostFields();
        protected Ipong.Classes.XObjs.InterSwitchResponse isr = new Ipong.Classes.XObjs.InterSwitchResponse();
        protected Classes.XObjs.Registration c_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Registration c_sub_reg = new Classes.XObjs.Registration();
        protected Classes.XObjs.Subagent c_sub = new Classes.XObjs.Subagent();
        protected Classes.XObjs.XPartner c_partner = new Classes.XObjs.XPartner();
        protected Classes.XObjs.PRatio c_pr = new Classes.XObjs.PRatio();

        protected Classes.XObjs.Fee_details c_fdets = new Classes.XObjs.Fee_details();
        protected List<Classes.XObjs.Twallet> lt_twall = new List<Classes.XObjs.Twallet>();
        protected List<Classes.XObjs.Fee_details> lt_fdets = new List<Classes.XObjs.Fee_details>();

        protected Classes.Retriever ret = new Classes.Retriever();
        protected Classes.Registration reg = new Classes.Registration();



        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}