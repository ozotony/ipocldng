using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace Ipong
{
    public partial class bank_reg : System.Web.UI.Page
    {
        protected string state_row = "0"; protected string xsync = "0"; protected string xvisible = "1";
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd"); protected string newState = "0";
        private Classes.Validator val = new Classes.Validator();
        
        private Classes.Registration reg = new Classes.Registration();
        private Classes.Retriever ret = new Classes.Retriever();
        private Classes.XObjs.XBanker xmem = new Classes.XObjs.XBanker();
        private Classes.XObjs.Address xaddy = new Classes.XObjs.Address();
        private Classes.XObjs.Pwallet xpwallet = new Classes.XObjs.Pwallet();
        private List<string> AllEmails = new List<string>();
        private List<string> AllMobiles = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                nationality.SelectedIndex = 159; residence.SelectedIndex = 159;
            }
            AllEmails = ret.getAllEmails();
            AllMobiles = ret.getAllMobileNumbers();
        }

        protected void residence_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (residence.SelectedIndex != 159)
            {
                state_row = "1";
            }
            else { state_row = "0"; }
        }
        protected bool doCaptcha()
        {
            bool xx = false;
            string str = "";
            if (Session["Captcha"] != null)
            {
                str = Session["Captcha"].ToString();
            }
            if (str == this.xcode.Text.ToUpper())
            {
                xx = true;
                this.newState = "0";
            }
            else
            {
                xx = false;
                this.newState = "1";
                this.xcode.Text = "";
            }
            return xx;
        }
        protected void btnAddMember_Click(object sender, EventArgs e)
        {

            if ((xname.Text == "") || (xcity.Text == "") || (xaddress.Text == "") || (xtelephone.Text == "") || (xemail.Text == "") || (xpass.Text == "") || (xposition.Text == "") || (xcode.Text == ""))
            {
                if (xname.Text == "") { xname.BorderColor = System.Drawing.Color.Red; } else { xname.BorderColor = System.Drawing.Color.Green; }
                if (xcity.Text == "") { xcity.BorderColor = System.Drawing.Color.Red; } else { xcity.BorderColor = System.Drawing.Color.Green; }
                if (xaddress.Text == "") { xaddress.BorderColor = System.Drawing.Color.Red; } else { xaddress.BorderColor = System.Drawing.Color.Green; }
                if (xtelephone.Text == "") { xtelephone.BorderColor = System.Drawing.Color.Red; } else { xtelephone.BorderColor = System.Drawing.Color.Green; }
                if (xemail.Text == "") { xemail.BorderColor = System.Drawing.Color.Red; } else { xemail.BorderColor = System.Drawing.Color.Green; }
                if (xpass.Text == "") { xpass.BorderColor = System.Drawing.Color.Red; } else { xpass.BorderColor = System.Drawing.Color.Green; }
                if (xposition.Text == "") { xposition.BorderColor = System.Drawing.Color.Red; } else { xposition.BorderColor = System.Drawing.Color.Green; }
                if (xcode.Text == "") { xcode.BorderColor = System.Drawing.Color.Red; } else { xcode.BorderColor = System.Drawing.Color.Green; }

                btnAddMember.Text = "I CONFIRM THAT THE ABOVE ENTRIES ARE CORRECT";
                Response.Write("<script language=JavaScript  type='text/javascript'>alert('PLEASE BE SURE TO FILL IN ALL THE ENTRIES MARKED WITH A RED STAR!!'); </script>"); 
            }
            else if ((xname.Text != "") && (xcity.Text != "") && (xaddress.Text != "") && (xtelephone.Text != "") && (xemail.Text != "") && (xpass.Text != "") && (xposition.Text != "") && (xcode.Text != ""))
            {
                 
                int cnt = 0;
                if (btnAddMember.Text == "I CONFIRM THAT THE ABOVE ENTRIES ARE CORRECT")
                {
                    if (doCaptcha() == false)
                    {
                        btnAddMember.Text = "I CONFIRM THAT THE ABOVE ENTRIES ARE CORRECT";
                        return;
                    }
                    else
                    {
                        btnAddMember.Text = "REGISTER";
                    }
                }
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
                        bool contains_mobile = AllMobiles.Contains(xtelephone.Text); bool contains_email = AllEmails.Contains(xemail.Text);
                        if ((!contains_mobile) && (!contains_email))
                        {
                            int memberID = 0; int addyID = 0;

                            xaddy.city = xcity.Text;
                            xaddy.countryID = residence.SelectedValue;
                            xaddy.email1 = xemail.Text;
                            xaddy.lgaID = "0";
                            xaddy.log_staff = "0";
                            xaddy.reg_date = xreg_date;
                            xaddy.stateID = xselectState.SelectedValue;
                            xaddy.street = xaddress.Text;
                            xaddy.telephone1 = xtelephone.Text;
                            xaddy.visible = xvisible;
                            xaddy.zip = "";
                            xaddy.xsync = xsync;
                            addyID = reg.addXpayAddress(xaddy);

                            if (addyID > 0)
                            {
                                xmem.xname = xname.Text;
                                xmem.xreg_date = xreg_date;
                                xmem.xposition = xposition.Text;
                                xmem.bankname = selectBank.SelectedValue;
                                xmem.xvisible = xvisible;
                                xmem.xsync = xsync;
                                xmem.xpassword = xpass.Text;
                                xmem.nationality = nationality.SelectedValue;
                                xmem.addressID = addyID.ToString();
                                xmem.sys_ID = "";
                                memberID = reg.addXpayBanker(xmem);

                                if (memberID > 0)
                                {
                                    xpwallet.xemail = xemail.Text;
                                    xpwallet.xmobile = xtelephone.Text;
                                    xpwallet.xmemberID = memberID.ToString();
                                    xpwallet.xmembertype = "rb";
                                    xpwallet.xpass = xpass.Text;
                                    xpwallet.reg_date = xreg_date;
                                    reg.addPwallet(xpwallet);
                                    /////////////////////////////////////////////////////////////////////////////////////////////////////////  
                                    xname.BorderColor = System.Drawing.Color.White;
                                    xcity.BorderColor = System.Drawing.Color.White;
                                    xaddress.BorderColor = System.Drawing.Color.White;
                                    xtelephone.BorderColor = System.Drawing.Color.White;
                                    xemail.BorderColor = System.Drawing.Color.White;
                                    xpass.BorderColor = System.Drawing.Color.White;
                                    Classes.Email em = new Classes.Email(); Classes.Messenger mess = new Classes.Messenger();

                                    string msg = "Dear " + xname.Text + ",<br/>";
                                    msg += "You have been registered on the CLD Platform!Please store the details below <br/>";
                                    msg += " Username: " + xemail.Text + "<br/>"; msg += "Password: " + xpass.Text + "<br/>System ID :CLD/RC/" + memberID.ToString().PadLeft(5, '0') + ",<br/>Regards";
                                    string xmsg = "Dear " + xname.Text.ToUpper() + ",\r\n";
                                    xmsg += "You have been registered on the CLD Platform!Please store the details below\r\n";
                                    xmsg += "Username: " + xemail.Text + ";\r\n"; xmsg += "Password:\r\n" + xpass.Text + ";\r\nSystem ID :CLD/RC/" + memberID.ToString().PadLeft(5, '0') + ",\r\nRegards";

                                    string sub = "CLD REGISTRATION";
                                    string f_email = "admin@cldng.com";
                                    string to_mail = xemail.Text;
                                    string to_mobile = xtelephone.Text;

                                    xmsg = Server.UrlEncode(xmsg);
                                    if (to_mobile.StartsWith("0")) { to_mobile = "234" + to_mobile.Remove(0, 1); }

                                    em.sendMail("CLD REGISTRATION", to_mail, f_email, sub, msg, "");
                                    string stat = mess.send_sms(xmsg, "CLD REG.", to_mobile);
                                    if (stat == "The remote name could not be resolved: 'www.smslive247.com'")
                                    {
                                        Response.Write("<script language=JavaScript  type='text/javascript'>alert('PLEASE BE SURE THAT THERE IS AN INTERNET CONNECTION!!'); </script>");
                                    }
                                    Response.Redirect("bank_reg_succ.aspx?x=" + xname.Text.ToUpper() + "&m=CLD/RB/" + memberID.ToString().PadLeft(5, '0'));
                                }
                            }
                            
                        }
                        else
                        {
                            btnAddMember.Text = "I CONFIRM THAT THE ABOVE ENTRIES ARE CORRECT";
                            if (contains_email) { Response.Write("<script language=JavaScript  type='text/javascript'>alert('THE E-MAIL ADDRESS ALREADY EXISTS ON THE SYSTEM'); </script>"); }

                            if (contains_mobile) { Response.Write("<script language=JavaScript  type='text/javascript'>alert('THE MOBILE NUMBER ALREADY EXISTS ON THE SYSTEM'); </script>"); }
                        }

                    }                
                }
            }

        }
    }
}