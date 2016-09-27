using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace Ipong
{
    public partial class upd_pro : System.Web.UI.Page
    {
        protected string adminID = "0"; protected string name = ""; protected string addressID = "0";
        private Classes.Validator val = new Classes.Validator();
        private Classes.Registration reg = new Classes.Registration();
        private Classes.Retriever ret = new Classes.Retriever();
        private Classes.XObjs.Pwallet xpwallet = new Classes.XObjs.Pwallet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            { this.adminID = Session["pwalletID"].ToString(); }
            else
            { base.Response.Redirect("./login.aspx"); }
            
                xpwallet = ret.getPwalletByID(adminID);
                if (xpwallet.xmembertype == "rc")
                {
                    name = ret.getMemberByID(xpwallet.xmemberID).xname; addressID = ret.getMemberByID(xpwallet.xmemberID).addressID;
                }
                else if (xpwallet.xmembertype == "rb")
                {
                    name = ret.getBankerByID(xpwallet.xmemberID).xname; addressID = ret.getBankerByID(xpwallet.xmemberID).addressID;
                }
                else if (xpwallet.xmembertype == "ra")
                {
                    name = ret.getAgentByID(xpwallet.xmemberID).xname; addressID = ret.getAgentByID(xpwallet.xmemberID).addressID;
                }
                xname.Text = name.ToUpper();
                if (!IsPostBack)
                {
                    this.ViewState["PreviousPage"] = base.Request.UrlReferrer;
                    xemail.Text = xpwallet.xemail;
                    xtelephone.Text = xpwallet.xmobile;
                    xpass.Text = xpwallet.xpass;
                }
            
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (this.ViewState["PreviousPage"] != null)
            {
                base.Response.Redirect(this.ViewState["PreviousPage"].ToString());
            }
        }
        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            if ((xemail.Text == "") || (xpass.Text == "") || (xtelephone.Text == ""))
            {
                if (xemail.Text == "") { xemail.BorderColor = System.Drawing.Color.Red; } else { xemail.BorderColor = System.Drawing.Color.Green; }
                if (xpass.Text == "") { xpass.BorderColor = System.Drawing.Color.Red; } else { xpass.BorderColor = System.Drawing.Color.Green; }
                if (xtelephone.Text == "") { xtelephone.BorderColor = System.Drawing.Color.Red; } else { xtelephone.BorderColor = System.Drawing.Color.Green; }
                btnAddMember.Text = "I CONFIRM THAT THE ABOVE ENTRIES ARE CORRECT";
                Response.Write("<script language=JavaScript  type='text/javascript'>alert('PLEASE BE SURE TO FILL IN ALL THE ENTRIES MARKED WITH A RED STAR!!'); </script>");
            }
            else if ((xemail.Text != "") && (xpass.Text != "") && (xtelephone.Text != ""))
            {
                int cnt = 0;
                if (btnAddMember.Text == "I CONFIRM THAT THE ABOVE ENTRIES ARE CORRECT")
                { btnAddMember.Text = "UPDATE"; }
                else
                {
                    cnt += val.IsValidMobile(xtelephone.Text); cnt += val.IsValidEmail(xemail.Text);
                    if (cnt > 0)
                    {
                        btnAddMember.Text = "I CONFIRM THAT THE ABOVE ENTRIES ARE CORRECT";
                        Response.Write("<script language=JavaScript  type='text/javascript'>alert('PLEASE BE SURE THAT THE E-MAIL ADDRESS AND MOBILE NUMBER ARE IN THE CORRECT FORMAT!!'); </script>");
                    }
                    else
                    {
                        reg.updatePwalletProfile(adminID, xemail.Text, xtelephone.Text, xpass.Text, addressID);
                        xtelephone.BorderColor = System.Drawing.Color.White;
                        xemail.BorderColor = System.Drawing.Color.White;
                        xpass.BorderColor = System.Drawing.Color.White;
                        Classes.Email em = new Classes.Email();
                        string msg = "Dear " + name + ",<br/>";
                        msg += "Your profile has been updated on the CLD Platform!<br/>Please notify our ADMIN if this was not intended.<br/>Regards";
                        string sub = "CLD PROFILE UPDATE";
                        string f_email = "admin@cldng.com";
                        string to_mail = xemail.Text;
                        em.sendMail("CLD PROFILE UPDATE", to_mail, f_email, sub, msg, "");  
                        Response.Redirect("profile_update_succ.aspx");

                    }
                }
            }

        }

        
    }
}