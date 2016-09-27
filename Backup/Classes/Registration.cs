using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace Ipong.Classes
{
    public class Registration
    {
        public string ConnectXhome()
        {
            return ConfigurationManager.ConnectionStrings["homeConnectionString"].ConnectionString;
        }   
        public string ConnectXpay()
        {
            return ConfigurationManager.ConnectionStrings["xpayConnectionString"].ConnectionString;
        }

        public string ConvertApos2Tab(string x)
        {
            string y = x;
            if ((x != null) || (x != ""))
            {
                if (x.Contains("'"))
                {
                    y = x.Replace("'", "|");
                }
            }
            return y;
        }
        public string ConvertTab2Apos(string x)
        {
            string y = x;
            if ((x != null) || (x != ""))
            {
                if (x.Contains("|"))
                {
                    y = x.Replace("|", "'");
                }
            }
            return y;
        }
        public string FormatDate(string x)
        {
            string y = x;
            if ((x != null) || (x != ""))
            {
                x = x.Trim();
                if (x.Contains("/"))
                {
                    string[] arr_side = x.Split('/');
                    for (int i = 0; i < arr_side.Length; i++)
                    {
                        if (arr_side[i].Length == 1) { arr_side[i] = arr_side[i].ToString().PadLeft(2, '0'); }
                    }
                    y = arr_side[2] + "-" + arr_side[1] + "-" + arr_side[0];
                }
                else if (x.Contains("-"))
                {
                    string[] arr_dash = x.Split('-');
                    for (int i = 0; i < arr_dash.Length; i++)
                    {
                        if (arr_dash[i].Length == 1) { arr_dash[i] = arr_dash[i].ToString().PadLeft(2, '0'); }
                    }
                    y = arr_dash[2] + "-" + arr_dash[1] + "-" + arr_dash[0];
                }
                else
                {
                    y = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            return y;
        }
        public int addRegistration(XObjs.Registration x)
        {
            string command_text = "";
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            command_text += "INSERT INTO InterSwitchPostFields (product_id,amount,isw_conv_fee,currency,site_redirect_url,txn_ref,hash,mackey,pay_item_id, ";
            command_text += " site_name,cust_id,cust_id_desc,cust_name,resp_desc,pay_item_name,local_date_time,MerchantReference,TransactionDate,trans_status,pay_ref, ";
            command_text += " ret_ref,xreg_date,xvisible,xsync) ";
            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open();
            int succ = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return succ;
        }
        public int updateRegistration(XObjs.Registration x)
        {
            string command_text = "";
            command_text += "UPDATE registrations SET Firstname='" + ConvertApos2Tab(x.Firstname) + "',Surname='" + ConvertApos2Tab(x.Surname) + "' ,Email='" + ConvertApos2Tab(x.Email) + "', ";
            command_text += " xpassword='" + ConvertApos2Tab(x.xpassword) + "',DateOfBrith='" + FormatDate(x.DateOfBrith) + "' ,IncorporatedDate='" + FormatDate(x.IncorporatedDate) + "',PhoneNumber='" + x.PhoneNumber + "', Sys_ID='" + x.Sys_ID + "', ";
            command_text += " CompanyName='" + ConvertApos2Tab(x.CompanyName) + "',CompanyAddress='" + ConvertApos2Tab(x.CompanyAddress) + "' ,ContactPerson='" + ConvertApos2Tab(x.ContactPerson) + "',ContactPersonPhone='" + x.ContactPersonPhone + "',  ";
            command_text += " CompanyWebsite='" + ConvertApos2Tab(x.CompanyWebsite) + "', xreg_date='" + x.xreg_date + "'  ";
            command_text += " WHERE xid='" + x.xid + "'";

            string connectionString = this.ConnectXhome();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }

        public int updateSubAgent(XObjs.Subagent x)
        {
            string command_text = "";
            command_text += "UPDATE subagents SET Firstname='" + ConvertApos2Tab(x.Firstname) + "',Surname='" + ConvertApos2Tab(x.Surname) + "' ,Email='" + ConvertApos2Tab(x.Email) + "', ";
            command_text += " xpassword='" + ConvertApos2Tab(x.xpassword) + "',DateOfBrith='" + FormatDate(x.DateOfBrith) + "' ,Sys_ID='" + x.Sys_ID + "',Telephone='" + x.Telephone + "', xreg_date='" + x.xreg_date + "' ";
            command_text += " WHERE xid='" + x.xid + "'";

            string connectionString = this.ConnectXhome();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }

        public int addXpayAddress(Ipong.Classes.XObjs.Address x)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("INSERT INTO address (countryID,stateID,lgaID,city,street,zip,telephone1,telephone2,email1,email2,log_staff,reg_date,visible,xsync) VALUES ('" + x.countryID + "','" + x.stateID + "','" + x.lgaID + "','" + x.city + "','" + x.street + "','" + x.zip + "','" + x.telephone1 + "','" + x.telephone2 + "','" + x.email1 + "','" + x.email2 + "','" + x.log_staff + "','" + x.reg_date + "','" + x.visible + "','" + x.xsync + "') SELECT SCOPE_IDENTITY()", connection);
            connection.Open();
            int succ = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            return succ;
        }

        public int addXpayAgent(Ipong.Classes.XObjs.XAgent x)
        {
            string sys_ID = "0";
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("INSERT INTO xagent (addressID,sys_ID,xname,cname,xpassword,nationality,xreg_date,xvisible,xsync) VALUES ('" + x.addressID + "','" + x.sys_ID + "','" + x.xname + "','" + x.cname + "','" + x.xpassword + "','" + x.nationality + "','" + x.xreg_date + "','" + x.xvisible + "','" + x.xsync + "') SELECT SCOPE_IDENTITY()", connection);
            connection.Open();
            int succ = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            if (succ > 0)
            {
                sys_ID = "CLD/RA/" + succ.ToString().PadLeft(5, '0');
                updateXpayAgent(succ.ToString(), sys_ID);
            }
            return succ;
        }

       
        public int addImpXpayAgent(Ipong.Classes.XObjs.XAgent x)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("INSERT INTO xagent (addressID,sys_ID,xname,cname,xpassword,nationality,xreg_date,xvisible,xsync) VALUES ('" + x.addressID + "','" + x.sys_ID + "','" + x.xname + "','" + x.cname + "','" + x.xpassword + "','" + x.nationality + "','" + x.xreg_date + "','" + x.xvisible + "','" + x.xsync + "') SELECT SCOPE_IDENTITY()", connection);
            connection.Open();
            int succ = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();            
            return succ;
        }
        public int addXpayMember(Ipong.Classes.XObjs.XMember x)
        {
            string sys_ID = "0";
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("INSERT INTO xmember (addressID,sys_ID,xname,cname,xpassword,nationality,xreg_date,xvisible,xsync) VALUES ('" + x.addressID + "','" + x.sys_ID + "','" + x.xname + "','" + x.cname + "','" + x.xpassword + "','" + x.nationality + "','" + x.xreg_date + "','" + x.xvisible + "','" + x.xsync + "') SELECT SCOPE_IDENTITY()", connection);
            connection.Open();
            int succ = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            if (succ > 0)
            {
                sys_ID = "CLD/RC/" + succ.ToString().PadLeft(5, '0');
                updateXpayMember(succ.ToString(), sys_ID);
            }
            return succ;
        }
        public int addXpayBanker(Ipong.Classes.XObjs.XBanker x)
        {
            string sys_ID = "0";
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("INSERT INTO xbanker (bankname,xposition,addressID,sys_ID,xname,xpassword,nationality,xreg_date,xvisible,xsync) VALUES ('" + x.bankname + "','" + x.xposition + "','" + x.addressID + "','" + x.sys_ID + "','" + x.xname + "','" + x.xpassword + "','" + x.nationality + "','" + x.xreg_date + "','" + x.xvisible + "','" + x.xsync + "') SELECT SCOPE_IDENTITY()", connection);
            connection.Open();
            int succ = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            if (succ > 0)
            {
                sys_ID = "CLD/RB/" + succ.ToString().PadLeft(5, '0');
                updateXpayBanker(succ.ToString(), sys_ID);
            }
            return succ;
        }

        public int addPwallet(Ipong.Classes.XObjs.Pwallet x)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("INSERT INTO pwallet (xemail,xmobile,xmemberID,xmembertype,xpass,reg_date) VALUES ('" + x.xemail + "','" + x.xmobile + "','" + x.xmemberID + "','" + x.xmembertype + "','" + x.xpass + "','" + x.reg_date + "') SELECT SCOPE_IDENTITY()", connection);
            connection.Open();
            int succ = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();           
            return succ;
        }
        public int updateRegistrationSysID(string xid, string Sys_ID)
        {
            string connectionString = this.ConnectXhome();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE registrations SET Sys_ID='" + Sys_ID + "' WHERE xid='" + xid + "'  ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }
        public int updateSubAgentSysID(string xid, string Sys_ID)
        {
            string connectionString = this.ConnectXhome();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE subagents SET Sys_ID='" + Sys_ID + "' WHERE xid='" + xid + "'  ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }

        public int addApplicant(Classes.XObjs.Applicant x)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("INSERT INTO applicant (xname,address,xemail,xmobile) VALUES ('" + x.xname + "','" + x.address + "','" + x.xemail + "','" + x.xmobile + "') SELECT SCOPE_IDENTITY()", connection);
            connection.Open();
            int succ = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            return succ;
        }

        public int addTwallet(Classes.XObjs.Twallet x)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("INSERT INTO twallet (transID,xmemberID,xmembertype,xpay_status,xgt,ref_no,xbankerID,applicantID,xreg_date,xvisible,xsync) VALUES ('" + x.transID + "','" + x.xmemberID + "','" + x.xmembertype + "','" + x.xpay_status + "','" + x.xgt + "','" + x.ref_no + "','" + x.xbankerID + "','" + x.applicantID + "','" + x.xreg_date + "','" + x.xvisible + "','" + x.xsync + "') SELECT SCOPE_IDENTITY()", connection);
            connection.Open();
            int succ = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            return succ;
        }
        public int updateTwalletBanker(string transID, string xbankerID, string xmemberID)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE twallet SET xbankerID='" + xbankerID + "',xpay_status='1'  WHERE transID='" + transID + "' AND xmemberID='" + xmemberID + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }

        public int updateTwalletXgt(string transID, string xtype, string xmemberID)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE twallet SET xgt='" + xtype + "'  WHERE transID='" + transID + "' AND xmemberID='" + xmemberID + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }
        public int updateTwalletXgtBanker(string transID, string xtype, string xmemberID)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE twallet SET xgt='" + xtype + "',xpay_status='2'  WHERE transID='" + transID + "' AND xmemberID='" + xmemberID + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }


        public int updateTwalletPaymentStatus(string transID, string xpay_status)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE twallet SET xpay_status='" + xpay_status + "'  WHERE transID='" + transID + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }

        public int updateTwalletReference(string transID, string new_ref)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE twallet SET transID='" + new_ref + "'  WHERE transID='" + transID + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }
        public int updateInterSwitchPostFieldsDate(string transID, string new_local_date)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE InterSwitchPostFields SET local_date_time='" + new_local_date + "'  WHERE txn_ref='" + transID + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }

        public int updateInterSwitchVisibleStatus(string transID, string status)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE InterSwitchPostFields SET xvisible='" + status + "'  WHERE txn_ref='" + transID + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }

        public int addInterSwitchRecords(Ipong.Classes.XObjs.InterSwitchPostFields x)
        {
            string command_text = "";
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            command_text += "INSERT INTO InterSwitchPostFields (product_id,amount,isw_conv_fee,currency,site_redirect_url,txn_ref,hash,mackey,pay_item_id, ";
            command_text += " site_name,cust_id,cust_id_desc,cust_name,resp_desc,pay_item_name,local_date_time,MerchantReference,TransactionDate,trans_status,pay_ref, ";
            command_text += " ret_ref,xreg_date,xvisible,xsync) ";

            command_text += " VALUES ('" + x.product_id + "','" + x.amount + "','" + x.isw_conv_fee + "','" + x.currency + "','" + x.site_redirect_url + "','" + x.txn_ref + "','" + x.hash + "','" + x.mackey + "', ";
            command_text += "'" + x.pay_item_id + "','" + x.site_name + "','" + x.cust_id + "','" + x.cust_id_desc + "','" + x.cust_name + "','" + x.resp_desc + "','" + x.pay_item_name + "', ";
            command_text += "'" + x.local_date_time + "','" + x.TransactionDate + "','" + x.MerchantReference + "','" + x.trans_status + "','" + x.pay_ref + "','" + x.ret_ref + "','" + x.xreg_date + "','" + x.xvisible + "','" + x.xsync + "') SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open();
            int succ = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
           
            return succ;
        }


        public int updateInterSwitchRecords(string txnref, string payRef, string retRef, string trans_status, string TransactionDate, string MerchantReference, string resp_desc)
        {
            TransactionDate = formatISWTrasactionDate(TransactionDate);
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE InterSwitchPostFields SET MerchantReference='" + MerchantReference + "',pay_ref='" + payRef + "',ret_ref='" + retRef + "',trans_status='" + trans_status + "',TransactionDate='" + formatISWTrasactionDate(TransactionDate) + "',resp_desc='" + resp_desc + "'  WHERE txn_ref='" + txnref + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
           // succ = updateAddressProfile(addressID, xemail, xmobile);
            return succ;
        }

        public int updateInterSwitchTransactionRef(string txnref, string new_tref, string new_hash)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE InterSwitchPostFields SET txn_ref='" + new_tref + "',hash='" + new_hash + "'  WHERE txn_ref='" + txnref + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            // succ = updateAddressProfile(addressID, xemail, xmobile);
            return succ;
        }

        public string formatISWTrasactionDate(string date)
        {
            //2013-08-07 11:08:04.000
            string new_date = "";
            if ((date != null) && (date != ""))
            {
                date = date.Trim();
                //string[] x = date.Split('T');
                //if (x.Length== 2) {new_date = x[0].Trim() + " " + x[1].Trim() + ".000";}
                new_date = date.Replace("T", " ") + ".000";
            }

            else
            {
                new_date = "";
            }
            return new_date;
        }
        public int updatePwalletProfile(string xid, string xemail, string xmobile, string xpass,string addressID)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE pwallet SET xemail='" + xemail + "',xmobile='" + xmobile + "',xpass='" + xpass + "'  WHERE xid='" + xid + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            succ = updateAddressProfile(addressID, xemail, xmobile);
            return succ;
        }

        public int updateAddressProfile(string xid, string xemail, string xmobile)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE address SET email1='" + xemail + "',telephone1='" + xmobile + "'  WHERE ID='" + xid + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }
       
        public int addHwallet(Ipong.Classes.XObjs.Hwallet x)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("INSERT INTO hwallet (transID,fee_detailsID,used_status,xreg_date,used_date,product_title) VALUES ('" + x.transID + "','" + x.fee_detailsID + "','" + x.used_status + "','" + x.xreg_date + "','" + x.used_date + "','" + x.product_title + "') SELECT SCOPE_IDENTITY()", connection);
            connection.Open();
            int succ = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            return succ;
        }
        public int updateHwallet(string xid, string used_status, string used_date, string product_title)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE hwallet SET used_status='" + used_status + "',product_title='" + product_title + "',used_date='" + used_date + "'  WHERE xid='" + xid + "'  ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }

        public int updateHwalletTransactionRef(string tref, string new_ref)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE hwallet SET transID='" + new_ref + "'  WHERE transID='" + tref + "'  ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }
        public int deleteHwallet(string xid)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("DELETE FROM hwallet  WHERE xid='" + xid + "'  ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }

        public int deleteFee_detailsByID(string xid)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("DELETE FROM Fee_details  WHERE xid='" + xid + "'  ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }

        public int updateFee_detailsQty(string xid, string xqty, string tot_amt)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE Fee_details SET xqty='" + xqty + "',tot_amt='" + tot_amt + "' WHERE xid='" + xid + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }

        public int updateFee_detailsTransactionRef(string twalletID, string new_ref)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE Fee_details SET twalletID='" + new_ref + "' WHERE twalletID='" + twalletID + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }

        public int addFee_details(Ipong.Classes.XObjs.Fee_details x)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("INSERT INTO fee_details (fee_listID,twalletID,xqty,xused,xlogstaff,tot_amt,init_amt,tech_amt,xreg_date,xvisible,xsync) VALUES ('" + x.fee_listID + "','" + x.twalletID + "','" + x.xqty + "','" + x.xused + "','" + x.xlogstaff + "','" + x.tot_amt + "','" + x.init_amt + "','" + x.tech_amt + "','" + x.xreg_date + "','" + x.xvisible + "','" + x.xsync + "') SELECT SCOPE_IDENTITY()", connection);
            connection.Open();
            int succ = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            return succ; 
        }
        public int updateUsedFee_details(string xid, string xused)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE xmember SET xused='" + xused + "' WHERE xid='" + xid + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }
        public int updateXpayAgent(string xid, string sys_ID)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE xagent SET sys_ID='" + sys_ID + "' WHERE xid='" + xid + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }
        public int updateXpayMember(string xid, string sys_ID)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE xmember SET sys_ID='" + sys_ID + "' WHERE xid='" + xid + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }
        public int updateXpayBanker(string xid, string sys_ID)
        {
            string connectionString = this.ConnectXpay();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE xbanker SET sys_ID='" + sys_ID + "' WHERE xid='" + xid + "' ", connection);
            connection.Open();
            int succ = command.ExecuteNonQuery();
            connection.Close();
            return succ;
        }
              

        public string updatePtDocz(string spec_doc, string loa_doc, string loa_no, string claim_doc, string claim_no, string pct_doc, string pct_no, string doa_doc, string doa_no, string pwalletID)
        {
            string connectionString = this.ConnectXhome();
            string succ = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE pt_info SET spec_doc=@spec_doc,loa_doc=@loa_doc,claim_doc=@claim_doc,pct_doc=@pct_doc,doa_doc=@doa_doc,loa_no=@loa_no,claim_no=@claim_no,pct_no=@pct_no,doa_no=@doa_no WHERE xID=@pwalletID ";
            connection.Open();
            command.Parameters.Add("@spec_doc", SqlDbType.Text);
            command.Parameters.Add("@loa_doc", SqlDbType.Text);
            command.Parameters.Add("@claim_doc", SqlDbType.Text);
            command.Parameters.Add("@pct_doc", SqlDbType.Text);
            command.Parameters.Add("@doa_doc", SqlDbType.Text);
            command.Parameters.Add("@loa_no", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@claim_no", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@pct_no", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@doa_no", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@pwalletID", SqlDbType.NVarChar);

            command.Parameters["@spec_doc"].Value = spec_doc;
            command.Parameters["@loa_doc"].Value = loa_doc;
            command.Parameters["@claim_doc"].Value = claim_doc;
            command.Parameters["@pct_doc"].Value = pct_doc;
            command.Parameters["@doa_doc"].Value = doa_doc;
            command.Parameters["@loa_no"].Value = loa_no;
            command.Parameters["@claim_no"].Value = claim_no;
            command.Parameters["@pct_no"].Value = pct_no;
            command.Parameters["@doa_no"].Value = doa_no;
            command.Parameters["@pwalletID"].Value = pwalletID;

            succ = command.ExecuteNonQuery().ToString();
            connection.Close();
            return succ;
        }

        public string updateAgProfileDocz(string pic_doc, string logo_doc, string pwalletID)
        {
            string connectionString = this.ConnectXhome();
            string succ = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE registrations SET Principal=@pic_doc,logo=@logo_doc WHERE xID=@pwalletID ";
            connection.Open();
            command.Parameters.Add("@pic_doc", SqlDbType.Text);
            command.Parameters.Add("@logo_doc", SqlDbType.Text);
            command.Parameters.Add("@pwalletID", SqlDbType.NVarChar);

            command.Parameters["@pic_doc"].Value = pic_doc;
            command.Parameters["@logo_doc"].Value = logo_doc;
            command.Parameters["@pwalletID"].Value = pwalletID;

            succ = command.ExecuteNonQuery().ToString();
            connection.Close();
            return succ;
        }
        public string updateSubAgProfileDocz(string pic_doc,string pwalletID)
        {
            string connectionString = this.ConnectXhome();
            string succ = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE subagents SET AgentPassport=@pic_doc WHERE xID=@pwalletID";
            connection.Open();
            command.Parameters.Add("@pic_doc", SqlDbType.Text);
            command.Parameters.Add("@pwalletID", SqlDbType.NVarChar);

            command.Parameters["@pic_doc"].Value = pic_doc;
            command.Parameters["@pwalletID"].Value = pwalletID;

            succ = command.ExecuteNonQuery().ToString();
            connection.Close();
            return succ;
        }

    }
}