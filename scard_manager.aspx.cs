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
        public Classes.Retriever ret = new Classes.Retriever();
        public Classes.Registration reg = new Classes.Registration();
        public List<Classes.XObjs.UpdateHwallet> x = new List<Classes.XObjs.UpdateHwallet>();
        public string xvisible = "1"; public string xsync = "0"; public string xreg_date=DateTime.Now.ToString("yyyy-MM-dd");
        public string xlogstaff = "1"; public string xvalid = "1"; protected string adminID = "0";
        int cnt = 0; int cnt_succ = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //string b = "baby's"; string c = ""; string d = "";
            //if (b.Contains("'")) { c = b.Replace("'", "''"); d = c; }

           // x = ret.getHwalletUsedStatusTm("CLD/RA/0011"); ///Tm
           // x = ret.getHwalletUsedStatusTmGen(""); ///Tm
             //x= ret.getHwalletUsedStatusPt("");   //Pt
            //x = ret.getHwalletUsedStatusPtRen("");   //Pt Ren

            //if (x.Count > 0)
            //{
                
            //    foreach(Classes.XObjs.UpdateHwallet a in x)
            //    {
            //        string[] hwallet = a.validationID.Trim().Split('-');
            //       // string b = hwallet[2];
            //        if (hwallet.Length == 3)
            //        {
            //          //  if (a.product_title.Contains("'")) { a.product_title.Replace("'", "''");  }
            //            cnt += reg.updateHwalletToUsedStatus(a.product_title, a.reg_date, hwallet[2]);
            //        }
                    
            //    }
            //    cnt_succ = cnt;
            //}
            //if (Session["pwalletID"] != null)
            //{
            //    if (Session["pwalletID"].ToString() != "")
            //    {
            //        this.adminID = Session["pwalletID"].ToString();
            //    }
            //    else
            //    {
            //        base.Response.Redirect("./login.aspx");
            //    }
            //}
            //else
            //{
            //    base.Response.Redirect("./login.aspx");
            //}

            //lt_scards = scm.GenerateGuidNum(12, 200000);
            //if (lt_scards.Count > 0)
            //{
            //    scm.addScards(lt_scards);
            //}         
        }
    }
}