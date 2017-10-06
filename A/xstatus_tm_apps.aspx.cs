using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using Ipong.Classes;
using System.Net;

namespace Ipong.A
{
    public partial class xstatus_tm_apps : Page
    {
        protected string adminID = "0";
        protected string agent_code = "";
        protected string agentType = "";
        protected AppStatus c_as = new AppStatus();
        protected tm.AddressService c_aos = new tm.AddressService();
        protected tm.Applicant c_app = new tm.Applicant();
        protected tm.Address c_app_addy = new tm.Address();
        protected tm.MarkInfo c_mark = new tm.MarkInfo();
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected tm.Representative c_rep = new tm.Representative();
        protected tm.Address c_rep_addy = new tm.Address();
        protected tm.Stage c_stage = new tm.Stage();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected string coy_name = "";
        protected string cust_id = "";
        protected string data_status = "N/A";
        protected string email = "";
        protected string fullname = "";
        protected string log_date = "";
        protected List<tm.MarkInfo> lt_mi = new List<tm.MarkInfo>();
        protected List<tm.Stage> lt_pw = new List<tm.Stage>();
        protected tm.Representative lt_rep = new tm.Representative();
        protected string mobile = "";
        protected string pwalletID = "";
        protected Ipong.Classes.Registration reg = new Ipong.Classes.Registration();
        protected Retriever ret = new Retriever();
        protected bool show_search = true;
        protected int showtm;
        protected string status = "N/A";
        protected tm t = new tm();
        protected string transID = "";
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected zues z = new zues();
        public String acceptance_letter = "";

        public String Certificate = "";
        public String refusal_letter = "";

        public string indexcard = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            {
                adminID = Session["pwalletID"].ToString();
            }
            else
            {
                base.Response.Redirect("../a_login.aspx");
            }
            if ((Session["log_date"] != null) && (Session["log_date"].ToString() != ""))
            {
                log_date = Session["log_date"].ToString();
            }
            if ((Session["agentType"] != null) && (Session["agentType"].ToString() != ""))
            {
                agentType = Session["agentType"].ToString();
                if (agentType == "Agent")
                {
                    if (Session["c_reg"] != null)
                    {
                        c_reg = (XObjs.Registration)Session["c_reg"];
                        fullname = c_reg.Firstname + " " + c_reg.Surname;
                        coy_name = c_reg.CompanyName;
                        cust_id = c_reg.Sys_ID;
                        email = c_reg.Email;
                        mobile = c_reg.PhoneNumber;
                        Session["coy_name"] = coy_name;
                        Session["fullname"] = fullname;
                        Session["email"] = email;
                        Session["mobile"] = mobile;
                        Session["c_addy"] = c_reg.CompanyAddress;
                    }
                }
                else
                {
                    XObjs.Registration registration = new XObjs.Registration();
                    if (Session["c_sub"] != null)
                    {
                        c_sub = (XObjs.Subagent)Session["c_sub"];
                        fullname = c_sub.Firstname + " " + c_sub.Surname;
                        email = c_sub.Email;
                        mobile = c_sub.Telephone;
                    }
                    if (Session["c_sub_reg"] != null)
                    {
                        registration = (XObjs.Registration)Session["c_sub_reg"];
                        coy_name = registration.CompanyName;
                        cust_id = registration.Sys_ID + "_" + c_sub.AssignID;
                    }
                    Session["coy_name"] = coy_name;
                    Session["fullname"] = fullname;
                    Session["email"] = email;
                    Session["mobile"] = mobile;
                    Session["c_addy"] = registration.CompanyAddress;
                }
            }
        }

        protected void btnNewSearch_Click(object sender, EventArgs e)
        {
            txt_status.Text = "";
            showtm = 0;
            show_search = true;
        }

        protected void btnNewSearch2_Click(object sender, EventArgs e)
        {
            txt_status.Text = "";
            showtm = 0;
            show_search = true;
        }

        protected void btnNewSearch3_Click(object sender, EventArgs e)
        {
            if (Session["acceptance_letter"] != null)
            {
                acceptance_letter = Convert.ToString(Session["acceptance_letter"]);
            }
            Response.Redirect(acceptance_letter);
        }

        protected void btnNewSearch5_Click(object sender, EventArgs e)
        {
            if (Session["Certificate"] != null)
            {
                Certificate = Convert.ToString(Session["Certificate"]);
            }
            Response.Redirect(Certificate);
        }


        protected void btnNewSearch6_Click(object sender, EventArgs e)
        {
            if (Session["indexcard"] != null)
            {
                indexcard = Convert.ToString(Session["indexcard"]);
            }
            Response.Redirect(indexcard);
        }

        protected void btnNewSearch4_Click(object sender, EventArgs e)
        {
            if (Session["refusal_letter"] != null)
            {
                refusal_letter = Convert.ToString(Session["refusal_letter"]);
            }
            Response.Redirect(refusal_letter);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedItem.Value == "TransactionId")
            {

                if (txt_status.Text != "")
                {
                    if (txt_status.Text.Contains("OAI/TM/"))
                    {
                        txt_status.Text = txt_status.Text.Replace("OAI/TM/", "");
                    }
                    transID = txt_status.Text.Trim();
                    lt_pw = t.getStageByClientIDAcc(txt_status.Text);
                    if (lt_pw.Count > 0)
                    {
                        Session["xvid"] = txt_status.Text.Trim();
                        lt_mi = t.getMarkInfoByUserID(lt_pw[0].ID);
                        lt_rep = t.getRepByUserID(lt_pw[0].ID);
                        Session["agent_code"] = lt_rep.agent_code;
                        SortedList<string, string> x = c_as.showTmStatus(lt_pw[0].status, lt_pw[0].data_status);
                        


                        //  if (Convert.ToInt32(lt_pw[0].status) >= 5 && lt_pw[0].data_status == "Accepted")
                        if (lt_pw[0].data_status != "Fresh" && lt_pw[0].data_status != "Valid" && lt_pw[0].data_status != "Withdraw" && lt_pw[0].data_status != "Re-conduct search" && lt_pw[0].data_status != "Search Conducted" && lt_pw[0].data_status != "Search 2 Conducted" &&  lt_pw[0].data_status != "Invalid" && lt_pw[0].data_status != "Re-examine" && lt_pw[0].data_status != "Registrable" && lt_pw[0].data_status != "Refused" && lt_pw[0].data_status != "Non-registrable" && lt_pw[0].data_status.ToUpper() != "KIV")
                        {
                            Session["d_status"] = lt_pw[0].status;
                            string ssd = System.Configuration.ConfigurationManager.AppSettings["cld_root"];

                            //  acceptance_letter = "http://tm.cldng.com/admin/tm/acceptance_slip_details.aspx?x=" + lt_mi[0].xID + " &x2=tt"; ;

                            acceptance_letter = ssd + "admin/tm/acceptance_slip_details.aspx?x=" + lt_mi[0].xID + " &x2=tt"; ;



                            Session["acceptance_letter"] = acceptance_letter;
                            Button2.Visible = true;
                        }
                        else
                        {

                            Button2.Visible = false;
                            string sss = "";
                        }

                        if (Convert.ToInt32(lt_pw[0].status) == 4 && lt_pw[0].data_status == "Refused")
                        {
                            Button3.Visible = true;

                            string ssd = System.Configuration.ConfigurationManager.AppSettings["cld_root"];

                            refusal_letter = ssd + "admin/tm/rejection_slip_details.aspx?x=" + lt_mi[0].xID + " &x2=tt";

                            Session["refusal_letter"] = refusal_letter;

                        }

                        else
                        {
                            Button3.Visible = false;



                        }


                        if (Convert.ToInt32(lt_pw[0].status) >= 5)
                        {
                            try
                            {

                                //Button5.Visible = true;

                                //string ssd = System.Configuration.ConfigurationManager.AppSettings["cld_root"];

                                //indexcard = ssd + "admin/tm/acceptance_unit/index_cards2.aspx?param=" + lt_mi[0].log_staff;

                                //Session["indexcard"] = indexcard;

                            }

                            catch(Exception ee )
                            {


                            }

                        }

                        else
                        {
                            Button5.Visible = false;



                        }

                        if (lt_pw[0].data_status == "Registered")
                        {
                            try
                            {
                                //Button4.Visible = true;

                                //string ssd = System.Configuration.ConfigurationManager.AppSettings["cld_root"];

                                //Certificate = ssd + "admin/tm/registrar_c_data_detailsx2.aspx?x=" + lt_mi[0].xID;

                                //Session["Certificate"] = Certificate;



                            }

                            catch(Exception ee )
                            {

                            }

                        }

                        else
                        {
                            Button4.Visible = false;



                        }
                        Session["d_status"] = lt_pw[0].status;
                        status = x["status"];
                        data_status = x["data_status"];
                        showtm = 1;
                        show_search = false;
                    }
                    else
                    {
                        status = "N/A";
                        showtm = 1;
                    }
                }
                else
                {
                    base.Response.Write("<script language=JavaScript>alert('PLEASE ENTER A VALID REFERENCE NUMBER')</script>");
                }

            }


           else  if (DropDownList1.SelectedItem.Value == "TpNumber")
            {

                if (txt_status.Text != "")
                {
                   
                    transID = txt_status.Text.Trim();
                 //  t.getStageClassByUserID( t.getMarkInfoByRegno(transID)[0].log_staff);
                    lt_pw = t.getStageByClientIDAcc(t.getStageClassByUserID(t.getMarkInfoByRegno(transID)[0].log_staff).validationID);
                    if (lt_pw.Count > 0)
                    {
                        Session["xvid"] = txt_status.Text.Trim();
                        lt_mi = t.getMarkInfoByUserID(lt_pw[0].ID);
                        lt_rep = t.getRepByUserID(lt_pw[0].ID);
                        Session["agent_code"] = lt_rep.agent_code;
                        SortedList<string, string> x = c_as.showTmStatus(lt_pw[0].status, lt_pw[0].data_status);


                        //  if (Convert.ToInt32(lt_pw[0].status) >= 5 && lt_pw[0].data_status == "Accepted")
                        if (lt_pw[0].data_status != "Fresh" && lt_pw[0].data_status != "Valid" && lt_pw[0].data_status != "Withdraw" && lt_pw[0].data_status != "Re-conduct search" && lt_pw[0].data_status != "Search Conducted" && lt_pw[0].data_status != "Search 2 Conducted" && lt_pw[0].data_status != "Re-examine" && lt_pw[0].data_status != "Invalid" && lt_pw[0].data_status != "Registrable" && lt_pw[0].data_status != "Refused" && lt_pw[0].data_status != "Non-registrable" && lt_pw[0].data_status.ToUpper() != "KIV")
                        
                        {
                            Session["d_status"] = lt_pw[0].status;
                            string ssd = System.Configuration.ConfigurationManager.AppSettings["cld_root"];

                            //  acceptance_letter = "http://tm.cldng.com/admin/tm/acceptance_slip_details.aspx?x=" + lt_mi[0].xID + " &x2=tt"; ;

                            acceptance_letter = ssd + "admin/tm/acceptance_slip_details.aspx?x=" + lt_mi[0].xID + " &x2=tt"; ;



                            Session["acceptance_letter"] = acceptance_letter;
                            Button2.Visible = true;
                        }
                        else
                        {

                            Button2.Visible = false;
                            string sss = "";
                        }

                        if (Convert.ToInt32(lt_pw[0].status) == 4 && lt_pw[0].data_status == "Refused")
                        {
                            Button3.Visible = true;

                            string ssd = System.Configuration.ConfigurationManager.AppSettings["cld_root"];

                            refusal_letter = ssd + "admin/tm/rejection_slip_details.aspx?x=" + lt_mi[0].xID + " &x2=tt";

                            Session["refusal_letter"] = refusal_letter;

                        }

                        else
                        {
                            Button3.Visible = false;



                        }


                        if (Convert.ToInt32(lt_pw[0].status) >= 5 )
                        {
                           
                            //Button5.Visible = true;

                            //string ssd = System.Configuration.ConfigurationManager.AppSettings["cld_root"];

                            //indexcard = ssd + "admin/tm/acceptance_unit/index_cards2.aspx?param=" + lt_mi[0].log_staff ;

                            //Session["indexcard"] = indexcard;



                        }

                        else
                        {
                            Button5.Visible = false;



                        }


                        if (lt_pw[0].data_status == "Registered")
                        {
                            //Button4.Visible = true;

                            //string ssd = System.Configuration.ConfigurationManager.AppSettings["cld_root"];

                            //Certificate = ssd + "admin/tm/registrar_c_data_detailsx2.aspx?x=" + lt_mi[0].xID;

                            //Session["Certificate"] = Certificate;

                        }

                        else
                        {
                            Button4.Visible = false;



                        }
                        Session["d_status"] = lt_pw[0].status;
                        status = x["status"];
                        data_status = x["data_status"];
                        showtm = 1;
                        show_search = false;
                    }
                    else
                    {
                        status = "N/A";
                        showtm = 1;
                    }
                }
                else
                {
                    base.Response.Write("<script language=JavaScript>alert('PLEASE ENTER A VALID REFERENCE NUMBER')</script>");
                }

            }


            else if (DropDownList1.SelectedItem.Value == "TrademarkName")
            {

                if (txt_status.Text != "")
                {
                    if (txt_status.Text.Contains("OAI/TM/"))
                    {
                        txt_status.Text = txt_status.Text.Replace("OAI/TM/", "");
                    }
                    transID = txt_status.Text.Trim();
                    //  lt_pw = t.getStageByClientIDAcc(txt_status.Text);
                    lt_pw = t.getStageByClientIDAcc(t.getStageClassByUserID(t.getMarkInfoByProductTitle(transID)[0].log_staff).validationID);
                    if (lt_pw.Count > 0)
                    {
                        Session["xvid"] = txt_status.Text.Trim();
                        lt_mi = t.getMarkInfoByUserID(lt_pw[0].ID);
                        lt_rep = t.getRepByUserID(lt_pw[0].ID);
                        Session["agent_code"] = lt_rep.agent_code;
                        SortedList<string, string> x = c_as.showTmStatus(lt_pw[0].status, lt_pw[0].data_status);


                        //  if (Convert.ToInt32(lt_pw[0].status) >= 5 && lt_pw[0].data_status == "Accepted")
                        if (lt_pw[0].data_status != "Fresh" && lt_pw[0].data_status != "Valid" && lt_pw[0].data_status != "Withdraw" && lt_pw[0].data_status != "Re-conduct search" && lt_pw[0].data_status != "Search Conducted" && lt_pw[0].data_status != "Search 2 Conducted" && lt_pw[0].data_status != "Re-examine" && lt_pw[0].data_status != "Invalid" && lt_pw[0].data_status != "Registrable" && lt_pw[0].data_status != "Refused" && lt_pw[0].data_status != "Non-registrable" && lt_pw[0].data_status.ToUpper() != "KIV")
                        {
                            Session["d_status"] = lt_pw[0].status;
                            string ssd = System.Configuration.ConfigurationManager.AppSettings["cld_root"];

                            //  acceptance_letter = "http://tm.cldng.com/admin/tm/acceptance_slip_details.aspx?x=" + lt_mi[0].xID + " &x2=tt"; ;

                            acceptance_letter = ssd + "admin/tm/acceptance_slip_details.aspx?x=" + lt_mi[0].xID + " &x2=tt"; ;



                            Session["acceptance_letter"] = acceptance_letter;
                            Button2.Visible = true;
                        }
                        else
                        {

                            Button2.Visible = false;
                            string sss = "";
                        }

                        if (Convert.ToInt32(lt_pw[0].status) == 4 && lt_pw[0].data_status == "Refused")
                        {
                            Button3.Visible = true;

                            string ssd = System.Configuration.ConfigurationManager.AppSettings["cld_root"];

                            refusal_letter = ssd + "admin/tm/rejection_slip_details.aspx?x=" + lt_mi[0].xID + " &x2=tt";

                            Session["refusal_letter"] = refusal_letter;

                        }

                        else
                        {
                            Button3.Visible = false;



                        }


                        if (Convert.ToInt32(lt_pw[0].status) >= 5)
                        {
                            //Button5.Visible = true;

                            //string ssd = System.Configuration.ConfigurationManager.AppSettings["cld_root"];

                            //indexcard = ssd + "admin/tm/acceptance_unit/index_cards2.aspx?param=" + lt_mi[0].log_staff;

                            //Session["indexcard"] = indexcard;

                        }

                        else
                        {
                            Button5.Visible = false;



                        }

                        if (lt_pw[0].data_status == "Registered")
                        {
                            //Button4.Visible = true;

                            //string ssd = System.Configuration.ConfigurationManager.AppSettings["cld_root"];

                            //Certificate = ssd + "admin/tm/registrar_c_data_detailsx2.aspx?x=" + lt_mi[0].xID;

                            //Session["Certificate"] = Certificate;

                        }

                        else
                        {
                            Button4.Visible = false;



                        }
                        Session["d_status"] = lt_pw[0].status;
                        status = x["status"];
                        data_status = x["data_status"];
                        showtm = 1;
                        show_search = false;
                    }
                    else
                    {
                        status = "N/A";
                        showtm = 1;
                    }
                }
                else
                {
                    base.Response.Write("<script language=JavaScript>alert('PLEASE ENTER A VALID REFERENCE NUMBER')</script>");
                }

            }
        }
        protected void BtnReprintTmAck_Click(object sender, EventArgs e)
        {
            if ((Session["xvid"] != null) && (Session["xvid"].ToString() != ""))
            {
                transID = Session["xvid"].ToString();
            }
            if ((Session["agent_code"] != null) && (Session["agent_code"].ToString() != ""))
            {
                agent_code = Session["agent_code"].ToString();
            }
            pwalletID = t.getCheckStatusDetails(transID, agent_code);
            if ((Convert.ToInt32(pwalletID) > 0) && (pwalletID != ""))
            {
                c_mark = t.getMarkInfoClassByUserID(pwalletID);
                c_rep = t.getRepClassByUserID(pwalletID);
                c_stage = t.getStageClassByUserID(pwalletID);
                c_app = t.getApplicantClassByID(pwalletID);
                c_app_addy = t.getAddressClassByID(c_app.addressID);

                if ((c_mark.xID != null) && (c_mark.logo_pic != ""))
                {
                    tm_img.ImageUrl = "http://88.150.164.30/CLD/admin/tm/" + c_mark.logo_pic;

                    Stream str = null;
                    string imageUrl = "http://88.150.164.30/CLD/admin/tm/" + c_mark.logo_pic;
                    HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(imageUrl);
                    HttpWebResponse wRes = (HttpWebResponse)(wReq).GetResponse();
                    str = wRes.GetResponseStream();

                    try
                    {
                        var imageOrig = System.Drawing.Image.FromStream(str);
                        int height = imageOrig.Height;
                        int width = imageOrig.Width;

                        if ((height > 0) && (width > 0))
                        {
                            if ((height > 240) && (width > 240))
                            {
                                if (height > width) { tm_img.Height = new Unit(320, UnitType.Pixel); tm_img.Width = new Unit(240, UnitType.Pixel); }
                                else if (height < width) { tm_img.Height = new Unit(240, UnitType.Pixel); tm_img.Width = new Unit(320, UnitType.Pixel); }
                                else { tm_img.Height = new Unit(320, UnitType.Pixel); tm_img.Width = new Unit(320, UnitType.Pixel); }
                            }
                            else
                            {
                                tm_img.Height = new Unit(height, UnitType.Pixel); tm_img.Width = new Unit(width, UnitType.Pixel);
                            }
                        }
                        else
                        {
                            tm_img.ImageUrl = "http://88.150.164.30/IpoCldng/images/na.png";
                            tm_img.Height = new Unit(240, UnitType.Pixel); tm_img.Width = new Unit(240, UnitType.Pixel);
                        }

                    }
                    catch (Exception ex)
                    {
                        tm_img.ImageUrl = "http://88.150.164.30/IpoCldng/images/na.png";
                        tm_img.Height = new Unit(240, UnitType.Pixel); tm_img.Width = new Unit(240, UnitType.Pixel);
                    }
                }
                else
                {
                    // tm_img.ImageUrl = "http://88.150.164.30/IpoCldng/images/na.png";
                    //  tm_img.Height = new Unit(240, UnitType.Pixel); tm_img.Width = new Unit(240, UnitType.Pixel);
                }
                showtm = 2;
                show_search = false;
            }
            else
            {
                showtm = 0;
                show_search = true;
            }
        }

    }
}