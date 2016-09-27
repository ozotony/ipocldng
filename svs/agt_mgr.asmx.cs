using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Ipong.svs
{
    /// <summary>
    /// Summary description for agt_imp
    /// </summary>
    [WebService(Namespace = "http://xpay.cldng.com/xpay/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class agt_mgr : System.Web.Services.WebService
    {
        private Classes.XObjs.XAgent xmem = new Classes.XObjs.XAgent();
        private Classes.XObjs.Address xaddy = new Classes.XObjs.Address();
        private Classes.XObjs.Pwallet xpwallet = new Classes.XObjs.Pwallet();
        private Classes.Registration reg = new Classes.Registration();

        protected string xsync = "0"; protected string xvisible = "1";
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");

        [WebMethod(EnableSession = true)]
        public string Agt_imp(string name, string cname, string code, string nationality, string a_country, string a_state, string a_city, string a_street, string a_tel, string a_mail, string pw)
        {
            string status = "0"; int addyID = 0; int memberID = 0;
            if ((name == "") || (cname == "") || (code == "") || (nationality == "") || (a_country == "") || (a_state == "") ||
                (a_city == "") || (a_street == "") || (a_tel == "") || (a_mail == "") || (pw == ""))
            {
                status = "0";
            }
            else if ((name != "") || (cname != "") || (code != "") || (nationality != "") || (a_country != "") || (a_state != "") ||
               (a_city != "") || (a_street != "") || (a_tel != "") || (a_mail != "") || (pw != ""))
            {
                xaddy.city = a_city;
                xaddy.countryID = a_country;
                xaddy.email1 = a_mail;
                xaddy.lgaID = "0";
                xaddy.log_staff = "0";
                xaddy.reg_date = xreg_date;
                xaddy.stateID = a_state;
                xaddy.street = a_street;
                xaddy.telephone1 = a_tel;
                xaddy.visible = xvisible;
                xaddy.zip = "";
                xaddy.xsync = xsync;
                addyID = reg.addXpayAddress(xaddy);

                if (addyID > 0)
                {
                    xmem.xname = name;
                    xmem.cname = cname;
                    xmem.xreg_date = xreg_date;
                    xmem.xvisible = xvisible;
                    xmem.xsync = xsync;
                    xmem.xpassword = pw;
                    xmem.nationality = nationality;
                    xmem.addressID = addyID.ToString();
                    xmem.sys_ID = code;
                    memberID = reg.addImpXpayAgent(xmem);

                    if (memberID > 0)
                    {
                        xpwallet.xemail = a_mail;
                        xpwallet.xmobile = a_tel;
                        xpwallet.xmemberID = memberID.ToString();
                        xpwallet.xmembertype = "ra";
                        xpwallet.xpass = pw;
                        xpwallet.reg_date = xreg_date;
                        reg.addPwallet(xpwallet);
                        status="1";
                    }
                }
            }

            return status;
        }
    }
}
