﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace Ipong.Classes
{
    public class Retriever
    {
       
        public string ConnectXpay()
        {
            return ConfigurationManager.ConnectionStrings["xpayConnectionString"].ConnectionString;
        }
        public string ConnectXhome()
        {
            return ConfigurationManager.ConnectionStrings["homeConnectionString"].ConnectionString;
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

       

        public string a_xadminz(string uname, string xpass, string serverpath)
        {
            List<Classes.XObjs.Pwallet> lt_adz = new List<Classes.XObjs.Pwallet>();
            string file_string = "Xavier";

            string keydir = serverpath + "\\Handlers\\bf.kez";
            if (File.Exists(keydir))
            {
                StreamReader streamReader = new StreamReader(keydir, true);
                file_string = streamReader.ReadToEnd();
                streamReader.Close();
                if (file_string != null)
                {
                    string bitStrengthString = file_string.Substring(0, file_string.IndexOf("</BitStrength>") + 14);
                    file_string = file_string.Replace(bitStrengthString, "");
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string connectionString = this.ConnectXpay();
            string xID = "";
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select xid,xemail,xpass from pwallet ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Classes.XObjs.Pwallet ad = new Classes.XObjs.Pwallet();
                ad.xid = reader["xID"].ToString();
                ad.xemail = reader["xemail"].ToString();
                ad.xpass = reader["xpass"].ToString();
                lt_adz.Add(ad);
            }
            reader.Close();
            string dpass = ""; string dmail = "";
            for (int i = 0; i < lt_adz.Count; i++)
            {
               // dmail = ody.DecryptString(lt_adz[i].xemail, file_len, file_string);
                //dpass = ody.DecryptString(lt_adz[i].xpass, file_len, file_string);
                if ((uname == lt_adz[i].xemail) && (xpass == lt_adz[i].xpass))
                {
                    xID = lt_adz[i].xid.ToString();
                }
            }
            return xID;
        }
        public string getAgentLogDetails(string uname, string xpass)
        {
            string xID = "0";
            SqlConnection connection = new SqlConnection(this.ConnectXhome());
            SqlCommand command = new SqlCommand("select xid from registrations WHERE  Email='" + uname + "' AND xpassword LIKE'%" + xpass + "%' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                xID =reader["xid"].ToString();
            }
            reader.Close();
            return xID;
        }

        public string getSubAgentLogDetails(string uname, string xpass)
        {
            string xID = "0";
            SqlConnection connection = new SqlConnection(this.ConnectXhome());
            SqlCommand command = new SqlCommand("select xid from subagents WHERE  Email='" + uname + "' AND xpassword LIKE'%" + xpass + "%' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                xID = reader["xid"].ToString();
            }
            reader.Close();
            return xID;
        }

        public string getMemberTypeByID(string id)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT xmembertype from pwallet where xid='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["xmembertype"].ToString();
            }
            reader.Close();
            return str;
        }
        public string addAdminLog(string adminID, string ip_addy, string remote_host, string remote_user, string server_name, string server_url)
        {
            string log_date = DateTime.Today.Date.ToString("yyyy-MM-dd");
            string log_time = DateTime.Now.ToLongTimeString();

            string connectionString = this.ConnectXpay();
            string succ = "0";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO admin_lg (adminID,ip_addy,remote_host,remote_user,server_name,server_url,log_date,log_time) VALUES (@adminID,@ip_addy,@remote_host,@remote_user,@server_name,@server_url,@log_date,@log_time) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@adminID", SqlDbType.VarChar, 200);
            command.Parameters.Add("@ip_addy", SqlDbType.Text);
            command.Parameters.Add("@remote_host", SqlDbType.Text);
            command.Parameters.Add("@remote_user", SqlDbType.Text);
            command.Parameters.Add("@server_name", SqlDbType.Text);
            command.Parameters.Add("@server_url", SqlDbType.Text);
            command.Parameters.Add("@log_date", SqlDbType.VarChar, 200);
            command.Parameters.Add("@log_time", SqlDbType.VarChar, 200);
            command.Parameters["@adminID"].Value = adminID;
            command.Parameters["@ip_addy"].Value = ip_addy;
            command.Parameters["@remote_host"].Value = remote_host;
            command.Parameters["@remote_user"].Value = remote_user;
            command.Parameters["@server_name"].Value = server_name;
            command.Parameters["@server_url"].Value = server_url;
            command.Parameters["@log_date"].Value = log_date;
            command.Parameters["@log_time"].Value = log_time;
            succ = command.ExecuteScalar().ToString();
            connection.Close();
            return succ;
        }


        public int getFee_detailCntByCat(string memberID, string cat, string xmembertype)
        {
            int succ = 0;
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(fee_details.xid) AS cnt from fee_list INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid INNER JOIN twallet ON  twallet.xid=fee_details.twalletID where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "'  AND twallet.xmembertype='" + xmembertype + "'  AND twallet.xgt<>'xpay' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    succ = Convert.ToInt32(reader["cnt"]);
                }
                catch (Exception ex)
                {
                    succ = 0;
                }
            }
            reader.Close();
            return succ;
        }
        public int getPaidFee_detailCntByCat(string memberID, string cat, string xpaystatus, string xmembertype)
         {
             int succ = 0;
             SqlConnection connection = new SqlConnection(this.ConnectXpay());
             SqlCommand command = new SqlCommand("select count(fee_details.xid) AS cnt from fee_list INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid INNER JOIN twallet ON  twallet.xid=fee_details.twalletID where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "' AND twallet.xpay_status='" + xpaystatus + "' AND twallet.xmembertype='" + xmembertype + "'   AND twallet.xgt<>'xpay' ", connection);
             connection.Open();
             SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
             while (reader.Read())
             {
                 try
                 {
                     succ = Convert.ToInt32(reader["cnt"]);
                 }
                 catch (Exception ex)
                 {
                     succ = 0;
                 }
             }
             reader.Close();
             return succ;
         }

        public int getPaidUsedCntByCat(string memberID, string cat, string xpaystatus, string xmembertype, string used_status)
         {
             int succ = 0;
             SqlConnection connection = new SqlConnection(this.ConnectXpay());
             SqlCommand command = new SqlCommand("select count(fee_details.xid) AS cnt from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid INNER JOIN twallet ON  twallet.xid=fee_details.twalletID INNER JOIN hwallet ON hwallet.fee_detailsID=fee_details.xid where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "' AND twallet.xpay_status='" + xpaystatus + "' AND twallet.xgt<>'xpay'  AND twallet.xmembertype='" + xmembertype + "' AND hwallet.used_status='" + used_status + "' ", connection);
             connection.Open();
             SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
             while (reader.Read())
             {
                 succ = Convert.ToInt32(reader["cnt"]);
             }
             reader.Close();
             return succ;
         }

        public int getCntTotalTransAdmin(string fromDate, string toDate)
        {
            int succ = 0;
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(twallet.xid) as cnt from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    succ = Convert.ToInt32(reader["cnt"]);
                }
                catch (Exception ex)
                {
                    succ = 0;
                }
            }
            reader.Close();
            return succ;
        }

        public int getCntTotalTransAdminGraph(string year)
        {
            int succ = 0;
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(twallet.xid) as cnt from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date LIKE '%" + year + "%' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    succ = Convert.ToInt32(reader["cnt"]);
                }
                catch (Exception ex)
                {
                    succ = 0;
                }
            }
            reader.Close();
            return succ;
        }

        public int getSumTotalTransAdmin(string fromDate, string toDate)
        {
            int succ = 0;
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select CAST(SUM(CAST(fee_details.tot_amt AS int) * CAST(fee_details.xqty AS int)) as money) as xsum from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    succ = Convert.ToInt32(reader["xsum"]);
                }
                catch (Exception ex)
                {
                    succ = 0;
                }
            }
            reader.Close();
            return succ;
        }


        public int getSumTotalTransMerchant(string fromDate, string toDate)
        {
            int succ = 0;
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select CAST(SUM(CAST(fee_details.init_amt AS int) * CAST(fee_details.xqty AS int)) as money) as xsum from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    succ = Convert.ToInt32(reader["xsum"]);
                }
                catch (Exception ex)
                {
                    succ = 0;
                }
            }
            reader.Close();
            return succ;
        }

        public int getSumTotalTransWingman(string fromDate, string toDate)
        {
            int succ = 0;
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select CAST(SUM(CAST(fee_details.tech_amt AS int)) as money) as xsum from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    succ = Convert.ToInt32(reader["xsum"]);
                }
                catch (Exception ex)
                {
                    succ = 0;
                }
            }
            reader.Close();
            return succ;
        }

        public int getSumTotalByMonthAdmin(string year, string month)
        {
            int succ = 0; string fromDate = year + "-" + month + "-01"; string toDate = year + "-" + month + "-31";

            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select CAST(SUM(CAST(fee_details.tot_amt AS int) * CAST(fee_details.xqty AS int)) as money) as xsum from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    succ = Convert.ToInt32(reader["xsum"]);
                }
                catch (Exception ex)
                {
                    succ = 0;
                }
            }
            reader.Close();
            return succ;
        }

        public int getSumTotalByMonthMerchant(string year, string month)
        {
            int succ = 0; string fromDate = year + "-" + month + "-01"; string toDate = year + "-" + month + "-31";

            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select CAST(SUM(CAST(fee_details.init_amt AS int) * CAST(fee_details.xqty AS int)) as money) as xsum from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    succ = Convert.ToInt32(reader["xsum"]);
                }
                catch (Exception ex)
                {
                    succ = 0;
                }
            }
            reader.Close();
            return succ;
        }

        public int getSumTotalByMonthWingman(string year, string month)
        {
            int succ = 0; string fromDate = year + "-" + month + "-01"; string toDate = year + "-" + month + "-31";

            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select CAST(SUM(CAST(fee_details.tech_amt AS int)) as money) as xsum from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    succ = Convert.ToInt32(reader["xsum"]);
                }
                catch (Exception ex)
                {
                    succ = 0;
                }
            }
            reader.Close();
            return succ;
        }

        public string getOldestDate()
        {
            string new_date = "";
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select top 1 twallet.xreg_date from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' order by twallet.xreg_date ASC ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                new_date = reader["xreg_date"].ToString();
            }
            reader.Close();
            new_date = new_date.Substring(0, 4);
            return new_date;
        }

        public string getLatestDate()
        {
            string new_date = "";
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select top 1 twallet.xreg_date from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' order by twallet.xreg_date DESC ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                new_date = reader["xreg_date"].ToString();
            }
            reader.Close();
            new_date = new_date.Substring(0, 4);
            return new_date;
        }

        public List<XObjs.ReportItem> getPaymentReportItem(string xcategory, string xmemberID, string xmembertype, string xpay_status, string xgt, string fromDate, string toDate)
        {
            List<XObjs.ReportItem> xlist = new List<XObjs.ReportItem>();
            int sn = 1; string command_text = "";
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            command_text += "select fee_list.item_code,fee_list.xdesc,fee_list.init_amt,fee_list.tech_amt, fee_details.xid AS fdID, fee_details.xqty,fee_details.tot_amt, ";
            command_text += " twallet.xid,twallet.transID,twallet.xpay_status,twallet.xgt,twallet.xreg_date ,hwallet.xid AS hID,hwallet.used_status,hwallet.product_title, ";
            command_text += " CAST(hwallet.transID AS nvarchar)+'-'+CAST(hwallet.fee_detailsID AS nvarchar)+'-'+CAST(hwallet.xid AS nvarchar) AS newtransID ";
            command_text += " FROM fee_list LEFT OUTER JOIN fee_details ON fee_list.xid=fee_details.fee_listID ";
            command_text += " LEFT OUTER JOIN twallet ON fee_details.twalletID=twallet.xid ";
            command_text += " LEFT OUTER JOIN hwallet ON twallet.transID=hwallet.transID ";

            command_text += " WHERE (twallet.xpay_status='" + xpay_status + "') AND (twallet.xgt='" + xgt + "') AND (twallet.xmemberID='" + xmemberID + "')  ";
            command_text += " AND (twallet.xmembertype='" + xmembertype + "') AND (fee_list.xcategory='" + xcategory + "') ";
            command_text += " AND (twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "') ";

            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.ReportItem x = new XObjs.ReportItem();
                x.sn = sn.ToString();
                x.hID = reader["hID"].ToString();
                x.fdID = reader["fdID"].ToString();
                x.transID = reader["transID"].ToString();
                x.newtransID = reader["newtransID"].ToString();
                x.item_code = reader["item_code"].ToString();
                x.item_desc = reader["xdesc"].ToString();
                x.payment_date = reader["xreg_date"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.total_amt = reader["tot_amt"].ToString();
                x.qty = reader["xqty"].ToString();
                x.used_status = reader["used_status"].ToString();
                x.product_title = reader["product_title"].ToString();
                xlist.Add(x);
                sn++;
            }
            reader.Close();
            return xlist;
        }


        public List<XObjs.ReportItem> getApplicationReportItem(string xcategory, string xmemberID, string xmembertype, string used_status, string xgt, string fromDate, string toDate)
        {
            List<XObjs.ReportItem> xlist = new List<XObjs.ReportItem>();
            int sn = 1; string command_text = "";
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            command_text += "select fee_list.item_code,fee_list.xdesc,fee_list.init_amt,fee_list.tech_amt, fee_details.xid AS fdID, fee_details.xqty,fee_details.tot_amt, ";
            command_text += " twallet.xid,twallet.transID,twallet.xpay_status,twallet.xgt,twallet.xreg_date ,hwallet.xid AS hID,hwallet.used_status,hwallet.product_title, ";
            command_text += " CAST(hwallet.transID AS nvarchar)+'-'+CAST(hwallet.fee_detailsID AS nvarchar)+'-'+CAST(hwallet.xid AS nvarchar) AS newtransID ";
            command_text += " FROM fee_list LEFT OUTER JOIN fee_details ON fee_list.xid=fee_details.fee_listID ";
            command_text += " LEFT OUTER JOIN twallet ON fee_details.twalletID=twallet.xid ";
            command_text += " LEFT OUTER JOIN hwallet ON twallet.transID=hwallet.transID ";
            command_text += " WHERE (twallet.xpay_status='1') AND (twallet.xgt='" + xgt + "') AND (twallet.xmemberID='" + xmemberID + "')  ";
            command_text += " AND (twallet.xmembertype='" + xmembertype + "') AND (fee_list.xcategory='" + xcategory + "') AND (hwallet.used_status='" + used_status + "')  ";
            command_text += " AND (twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "') ";

            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.ReportItem x = new XObjs.ReportItem();
                x.sn = sn.ToString();
                x.hID = reader["hID"].ToString();
                x.fdID = reader["fdID"].ToString();
                x.transID = reader["transID"].ToString();
                x.newtransID = reader["newtransID"].ToString();
                x.item_code = reader["item_code"].ToString();
                x.item_desc = reader["xdesc"].ToString();
                x.payment_date = reader["xreg_date"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.total_amt = reader["tot_amt"].ToString();
                x.qty = reader["xqty"].ToString();
                x.used_status = reader["used_status"].ToString();
                x.product_title = reader["product_title"].ToString();
                xlist.Add(x);
                sn++;
            }
            reader.Close();
            return xlist;
        }

        public List<XObjs.PartnerGrid> getPartnerGridMerchantList(string fromDate, string toDate)
        {
            List<XObjs.PartnerGrid> xlist = new List<XObjs.PartnerGrid>();
            int sn = 1;
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select twallet.xid,twallet.transID,twallet.xmemberID,twallet.xmembertype,twallet.xgt,twallet.ref_no,twallet.xbankerID,fee_details.fee_listID,fee_details.init_amt,fee_details.tech_amt,fee_details.tot_amt,fee_details.xqty,twallet.xreg_date from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "' AND twallet.xgt<>'xpay' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.PartnerGrid x = new XObjs.PartnerGrid();
                x.sn = sn.ToString();
                x.xid = reader["xid"].ToString();
                x.transID = reader["transID"].ToString();
                x.xmemberID = reader["xmemberID"].ToString();
                x.xmembertype = reader["xmembertype"].ToString();
                x.xgt = reader["xgt"].ToString();
                x.ref_no = reader["ref_no"].ToString();
                x.xbankerID = reader["xbankerID"].ToString();
                x.fee_listID = reader["fee_listID"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.tot_amt = reader["tot_amt"].ToString();
                x.xqty = reader["xqty"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                xlist.Add(x);
                sn++;
            }
            reader.Close();
            return xlist;
        }

        public int getAllPaidFee_detail_ItemsCntByCat(string memberID, string cat, string xmembertype)
         {
             int succ = 0;
             SqlConnection connection = new SqlConnection(this.ConnectXpay());
             SqlCommand command = new SqlCommand("select count(hwallet.xid) as cnt from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid  INNER JOIN twallet ON  twallet.xid=fee_details.twalletID  INNER JOIN hwallet ON hwallet.fee_detailsID=fee_details.xid where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "' AND twallet.xmembertype='" + xmembertype + "' AND twallet.xpay_status='1' AND twallet.xgt<>'xpay' ", connection);
             connection.Open();
             SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
             while (reader.Read())
             {
                 succ = Convert.ToInt32(reader["cnt"]);
             }
             reader.Close();
             return succ;
         }

        public int getCombinedPaidFee_detail_ItemsCntByCat(string memberID, string cat, string xpaystatus, string xmembertype)
        {
            int succ = 0;
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(DISTINCT twallet.xid) as cnt from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid INNER JOIN twallet ON  twallet.xid=fee_details.twalletID INNER JOIN hwallet ON hwallet.fee_detailsID=fee_details.xid  where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "' AND twallet.xpay_status='" + xpaystatus + "' AND twallet.xmembertype='" + xmembertype + "'  AND twallet.xgt<>'xpay' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                succ = Convert.ToInt32(reader["cnt"]);
            }
            reader.Close();
            return succ;
        }

        public int getPaidFee_detail_ItemsCntByCatISW(string memberID, string cat, string xpaystatus, string xmembertype)
        {
            int succ = 0;
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(DISTINCT twallet.xid) as cnt from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid INNER JOIN twallet ON  twallet.xid=fee_details.twalletID INNER JOIN hwallet ON hwallet.fee_detailsID=fee_details.xid INNER JOIN InterSwitchPostFields ON twallet.transID=InterSwitchPostFields.txn_ref  where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "' AND twallet.xpay_status='" + xpaystatus + "' AND twallet.xmembertype='" + xmembertype + "' AND InterSwitchPostFields.xvisible='1' AND twallet.xgt<>'xpay' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                succ = Convert.ToInt32(reader["cnt"]);
            }
            reader.Close();
            return succ;
        }
        public int getPaidFee_detail_ItemsCntByCatBk(string memberID, string cat, string xpaystatus, string xmembertype)
        {
            int succ = 0;
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(DISTINCT twallet.xid) as cnt from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid INNER JOIN twallet ON  twallet.xid=fee_details.twalletID INNER JOIN hwallet ON hwallet.fee_detailsID=fee_details.xid where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "' AND twallet.xpay_status='" + xpaystatus + "' AND twallet.xmembertype='" + xmembertype + "' AND twallet.xgt='xpay_bk' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                succ = Convert.ToInt32(reader["cnt"]);
            }
            reader.Close();
            return succ;
        }

        public int getPaidFee_detail_ItemsCntByCatOld(string memberID, string cat, string xpaystatus, string xmembertype)
        {
            int succ = 0;
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(hwallet.xid) as cnt from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid  INNER JOIN twallet ON  twallet.xid=fee_details.twalletID  INNER JOIN hwallet ON hwallet.fee_detailsID=fee_details.xid where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "' AND twallet.xpay_status='" + xpaystatus + "' AND twallet.xmembertype='" + xmembertype + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                succ = Convert.ToInt32(reader["cnt"]);
            }
            reader.Close();
            return succ;
        }
        public List<string> getAllMobileNumbers()
        {
            List<string> xlist = new List<string>();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT telephone1 FROM address", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                string x = reader["telephone1"].ToString();
                xlist.Add(x);
            }
            reader.Close();
            return xlist;
        }

        public List<string> getAllEmails()
        {
            List<string> xlist = new List<string>();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT email1 FROM address", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                string x = reader["email1"].ToString();
                xlist.Add(x);
            }
            reader.Close();
            return xlist;
        }

        public List<Classes.XObjs.Fee_list> getFee_listByCat(string cat)
        {
            List<Classes.XObjs.Fee_list> xlist = new List<Classes.XObjs.Fee_list>();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM fee_list WHERE xcategory='"+cat+"' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Classes.XObjs.Fee_list x = new XObjs.Fee_list();
                x.xid = reader["xid"].ToString();
                 x.init_amt = reader["init_amt"].ToString();
                 x.item = reader["item"].ToString();
                 x.item_code = reader["item_code"].ToString();
                 x.tech_amt = reader["tech_amt"].ToString();
                 x.xcategory = reader["xcategory"].ToString();
                 x.xdesc = reader["xdesc"].ToString();
                 x.xlogstaff = reader["xlogstaff"].ToString();
                 x.xreg_date = reader["xreg_date"].ToString();
                 x.xsync = reader["xsync"].ToString();
                 x.xvisible = reader["xvisible"].ToString();

                 xlist.Add(x);
            }
            reader.Close();
            return xlist;
        }
        public Classes.XObjs.Fee_list getFee_listByID(string xid)
        {
            Classes.XObjs.Fee_list x = new XObjs.Fee_list();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM fee_list WHERE xid='" + xid + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {                
                x.xid = reader["xid"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.item = reader["item"].ToString();
                x.item_code = reader["item_code"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.xcategory = reader["xcategory"].ToString();
                x.xdesc = reader["xdesc"].ToString();
                x.xlogstaff = reader["xlogstaff"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xsync = reader["xsync"].ToString();
                x.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return x;
        }
        public Classes.XObjs.Fee_list getFee_listByItemCode(string item_code)
        {
            Classes.XObjs.Fee_list x = new XObjs.Fee_list();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM fee_list WHERE item_code='" + item_code + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.item = reader["item"].ToString();
                x.item_code = reader["item_code"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.xcategory = reader["xcategory"].ToString();
                x.xdesc = reader["xdesc"].ToString();
                x.xlogstaff = reader["xlogstaff"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xsync = reader["xsync"].ToString();
                x.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return x;
        }    
        public Classes.XObjs.Hwallet getHwalletByID(string xid)
        {
            Classes.XObjs.Hwallet x = new XObjs.Hwallet();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM hwallet WHERE xid='" + xid + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.transID = reader["transID"].ToString();
                x.fee_detailsID = reader["fee_detailsID"].ToString();
                x.used_status = reader["used_status"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
            }
            reader.Close();
            return x;
        }

        public Classes.XObjs.InterSwitchPostFields getISWtransactionByTransactionID(string txnref)
        {
            Classes.XObjs.InterSwitchPostFields x = new XObjs.InterSwitchPostFields();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM InterSwitchPostFields WHERE txn_ref='" + txnref + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.product_id = reader["product_id"].ToString();
                x.amount = reader["amount"].ToString();
                x.isw_conv_fee = reader["isw_conv_fee"].ToString();
                x.currency = reader["currency"].ToString();
                x.site_redirect_url = reader["site_redirect_url"].ToString();
                x.txn_ref = reader["txn_ref"].ToString();
                x.hash = reader["hash"].ToString();
                x.mackey = reader["mackey"].ToString();
                x.pay_item_id = reader["pay_item_id"].ToString();
                x.site_name = reader["site_name"].ToString();
                x.cust_id = reader["cust_id"].ToString();
                x.cust_id_desc = reader["cust_id_desc"].ToString();
                x.cust_name = reader["cust_name"].ToString();
                x.resp_desc = reader["resp_desc"].ToString();
                x.pay_item_name = reader["pay_item_name"].ToString();
                x.local_date_time = reader["local_date_time"].ToString();
                x.TransactionDate = reader["TransactionDate"].ToString();
                x.MerchantReference = reader["MerchantReference"].ToString();
                x.trans_status = reader["trans_status"].ToString();
                x.pay_ref = reader["pay_ref"].ToString();
                x.ret_ref = reader["ret_ref"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return x;
        }
       
        public List<Classes.XObjs.Hwallet> getHwalletByFee_detailsID(string fee_detailsID)
        {
            List<Classes.XObjs.Hwallet> xlist = new List<Classes.XObjs.Hwallet>();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM hwallet WHERE fee_detailsID='" + fee_detailsID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Classes.XObjs.Hwallet x = new XObjs.Hwallet();
                x.xid = reader["xid"].ToString();
                x.transID = reader["transID"].ToString();
                x.fee_detailsID = reader["fee_detailsID"].ToString();
                x.used_status = reader["used_status"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                xlist.Add(x);
            }
            reader.Close();
            return xlist;
        }

        public List<Classes.XObjs.Hwallet> getHwalletByTransID(string transID)
        {
            List<Classes.XObjs.Hwallet> xlist = new List<Classes.XObjs.Hwallet>();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM hwallet WHERE transID='" + transID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Classes.XObjs.Hwallet x = new XObjs.Hwallet();
                x.xid = reader["xid"].ToString();
                x.transID = reader["transID"].ToString();
                x.fee_detailsID = reader["fee_detailsID"].ToString();
                x.used_status = reader["used_status"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                xlist.Add(x);
            }
            reader.Close();
            return xlist;
        }

        public Classes.XObjs.Pwallet getPwalletByID(string xid)
        {
            Classes.XObjs.Pwallet x = new XObjs.Pwallet();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE xid='" + xid + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.xmembertype = reader["xmembertype"].ToString();
                x.xmemberID = reader["xmemberID"].ToString();
                x.xemail = reader["xemail"].ToString();
                x.xmobile = reader["xmobile"].ToString();
                x.xpass = reader["xpass"].ToString();
                x.reg_date = reader["reg_date"].ToString();
            }
            reader.Close();
            return x;
        }
        public Classes.XObjs.Pwallet getPwalletByMobile(string xmobile)
        {
            Classes.XObjs.Pwallet x = new XObjs.Pwallet();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE xmobile='" + xmobile + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.xmembertype = reader["xmembertype"].ToString();
                x.xmemberID = reader["xmemberID"].ToString();
                x.xemail = reader["xemail"].ToString();
                x.xmobile = reader["xmobile"].ToString();
                x.xpass = reader["xpass"].ToString();
                x.reg_date = reader["reg_date"].ToString();
            }
            reader.Close();
            return x;
        }
        public Classes.XObjs.Pwallet getPwalletByMemberID(string xmemberID)
        {
            Classes.XObjs.Pwallet x = new XObjs.Pwallet();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE xmemberID='" + xmemberID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.xmembertype = reader["xmembertype"].ToString();
                x.xmemberID = reader["xmemberID"].ToString();
                x.xemail = reader["xemail"].ToString();
                x.xmobile = reader["xmobile"].ToString();
                x.xpass = reader["xpass"].ToString();
                x.reg_date = reader["reg_date"].ToString();
            }
            reader.Close();
            return x;
        }
        public Classes.XObjs.XMember getMemberByID(string xid)
        {
            Classes.XObjs.XMember x = new XObjs.XMember();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM xmember WHERE xid='" + xid + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.xname = reader["xname"].ToString();
                x.cname = reader["cname"].ToString();
                x.xpassword = reader["xpassword"].ToString();
                x.nationality = reader["nationality"].ToString();
                x.addressID = reader["addressID"].ToString();
                x.sys_ID = reader["sys_ID"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return x;
        }
        public Classes.XObjs.XBanker getBankerByID(string xid)
        {
            Classes.XObjs.XBanker x = new XObjs.XBanker();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM xbanker WHERE xid='" + xid + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.xname = reader["xname"].ToString();
                x.bankname = reader["bankname"].ToString();
                x.xpassword = reader["xpassword"].ToString();
                x.nationality = reader["nationality"].ToString();
                x.addressID = reader["addressID"].ToString();
                x.xposition = reader["xposition"].ToString();
                x.sys_ID = reader["sys_ID"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return x;
        }
        public Classes.XObjs.XAgent getAgentByID(string xid)
        {
            Classes.XObjs.XAgent x = new XObjs.XAgent();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM xagent WHERE xid='" + xid + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.xname = reader["xname"].ToString();
                x.cname = reader["cname"].ToString();
                x.xpassword = reader["xpassword"].ToString();
                x.nationality = reader["nationality"].ToString();
                x.addressID = reader["addressID"].ToString();
                x.sys_ID = reader["sys_ID"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return x;
        }

        public List<Classes.XObjs.Registration> getAllRegistrations()
        {          
            List<Classes.XObjs.Registration> x_lt = new List<XObjs.Registration>();
            SqlConnection connection = new SqlConnection(this.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM registrations  ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Classes.XObjs.Registration x = new XObjs.Registration();
                x.xid = reader["xid"].ToString();
                x.AccrediationType = reader["AccrediationType"].ToString();
                x.Sys_ID = reader["Sys_ID"].ToString();
                x.Firstname = reader["Firstname"].ToString();
                x.Surname = reader["Surname"].ToString();
                x.Email = reader["Email"].ToString();
                x.xpassword = reader["xpassword"].ToString();
                x.DateOfBrith = reader["DateOfBrith"].ToString();
                x.IncorporatedDate = reader["IncorporatedDate"].ToString();
                x.Nationality = reader["Nationality"].ToString();
                x.PhoneNumber = reader["PhoneNumber"].ToString();
                x.CompanyName = reader["CompanyName"].ToString();
                x.CompanyAddress = reader["CompanyAddress"].ToString();
                x.ContactPerson = reader["ContactPerson"].ToString();
                x.ContactPersonPhone = reader["ContactPersonPhone"].ToString();
                x.CompanyWebsite = reader["CompanyWebsite"].ToString();
                x.Certificate = reader["Certificate"].ToString();
                x.Introduction = reader["Introduction"].ToString();
                x.Principal = reader["Principal"].ToString();
                x.logo = reader["logo"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xstatus = reader["xstatus"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
                x_lt.Add(x);
            }
            reader.Close();
            return x_lt;
        }
        public Classes.XObjs.Registration getRegistrationBySubagentRegistrationID(string RegistrationID)
        {
            Classes.XObjs.Registration x = new XObjs.Registration();
            SqlConnection connection = new SqlConnection(this.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM registrations WHERE (xid='" + RegistrationID + "')  ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.AccrediationType = reader["AccrediationType"].ToString();
                x.Sys_ID = reader["Sys_ID"].ToString();
                x.Firstname = ConvertTab2Apos(reader["Firstname"].ToString());
                x.Surname = ConvertTab2Apos(reader["Surname"].ToString());
                x.Email = ConvertTab2Apos(reader["Email"].ToString());
                x.xpassword = ConvertTab2Apos(reader["xpassword"].ToString());
                x.DateOfBrith = reader["DateOfBrith"].ToString();
                x.IncorporatedDate = reader["IncorporatedDate"].ToString();
                x.Nationality = reader["Nationality"].ToString();
                x.PhoneNumber = reader["PhoneNumber"].ToString();
                x.CompanyName = ConvertTab2Apos(reader["CompanyName"].ToString());
                x.CompanyAddress = ConvertTab2Apos(reader["CompanyAddress"].ToString());
                x.ContactPerson = ConvertTab2Apos(reader["ContactPerson"].ToString());
                x.ContactPersonPhone = reader["ContactPersonPhone"].ToString();
                x.CompanyWebsite = ConvertTab2Apos(reader["CompanyWebsite"].ToString());
                x.Certificate = reader["Certificate"].ToString();
                x.Introduction = reader["Introduction"].ToString();
                x.Principal = reader["Principal"].ToString();
                x.logo = reader["logo"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xstatus = reader["xstatus"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return x;
        }     
        public List<Classes.XObjs.Subagent> getAllSubAgents()
        {
            List<Classes.XObjs.Subagent> x_lt = new List<Classes.XObjs.Subagent>();
            SqlConnection connection = new SqlConnection(this.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM subagents", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Classes.XObjs.Subagent x = new XObjs.Subagent();
                x.xid = reader["xid"].ToString();
                x.RegistrationID = reader["RegistrationID"].ToString();
                x.Surname = reader["Surname"].ToString();
                x.Firstname = reader["Firstname"].ToString();
                x.Email = reader["Email"].ToString();
                x.xpassword = reader["xpassword"].ToString();
                x.Telephone = reader["Telephone"].ToString();
                x.AssignID = reader["AssignID"].ToString();
                x.Sys_ID = reader["Sys_ID"].ToString();
                x.Address = reader["Address"].ToString();
                x.AgentPassport = reader["AgentPassport"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xstatus = reader["xstatus"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
                x_lt.Add(x);
            }
            reader.Close();
            return x_lt;
        }
        public Classes.XObjs.Registration getRegistrationByPhoneNumber(string PhoneNumber)
        {
            Classes.XObjs.Registration x = new XObjs.Registration();
            SqlConnection connection = new SqlConnection(this.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM registrations WHERE PhoneNumber='" + PhoneNumber + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.AccrediationType = reader["AccrediationType"].ToString();
                x.Sys_ID = reader["Sys_ID"].ToString();
                x.Firstname = ConvertTab2Apos(reader["Firstname"].ToString());
                x.Surname = ConvertTab2Apos(reader["Surname"].ToString());
                x.Email = ConvertTab2Apos(reader["Email"].ToString());
                x.xpassword = ConvertTab2Apos(reader["xpassword"].ToString());
                x.DateOfBrith = reader["DateOfBrith"].ToString();
                x.IncorporatedDate = reader["IncorporatedDate"].ToString();
                x.Nationality = reader["Nationality"].ToString();
                x.PhoneNumber = reader["PhoneNumber"].ToString();
                x.CompanyName = ConvertTab2Apos(reader["CompanyName"].ToString());
                x.CompanyAddress = ConvertTab2Apos(reader["CompanyAddress"].ToString());
                x.ContactPerson = ConvertTab2Apos(reader["ContactPerson"].ToString());
                x.ContactPersonPhone = reader["ContactPersonPhone"].ToString();
                x.CompanyWebsite = ConvertTab2Apos(reader["CompanyWebsite"].ToString());
                x.Certificate = reader["Certificate"].ToString();
                x.Introduction = reader["Introduction"].ToString();
                x.Principal = reader["Principal"].ToString();
                x.logo = reader["logo"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xstatus = reader["xstatus"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return x;
        }
        public Classes.XObjs.Subagent getSubAgentByPhoneNumber(string PhoneNumber)
        {
            Classes.XObjs.Subagent x = new XObjs.Subagent();
            SqlConnection connection = new SqlConnection(this.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM subagents WHERE Telephone='" + PhoneNumber + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.RegistrationID = reader["RegistrationID"].ToString();
                x.Surname = ConvertTab2Apos(reader["Surname"].ToString());
                x.Firstname = ConvertTab2Apos(reader["Firstname"].ToString());
                x.Email = ConvertTab2Apos(reader["Email"].ToString());
                x.xpassword = ConvertTab2Apos(reader["xpassword"].ToString());
                x.Telephone = reader["Telephone"].ToString();
                x.DateOfBrith = reader["DateOfBrith"].ToString();
                x.AssignID = reader["AssignID"].ToString();
                x.Sys_ID = reader["Sys_ID"].ToString();
                x.Address = ConvertTab2Apos(reader["Address"].ToString());
                x.AgentPassport = reader["AgentPassport"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xstatus = reader["xstatus"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return x;
        }
        public XObjs.Registration getRegistrationByLogin(string email, string xpass)
        {
            XObjs.Registration x = new XObjs.Registration();
            SqlConnection connection = new SqlConnection(this.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM registrations WHERE Email='" + email + "'  AND xpassword='" + xpass + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.AccrediationType = reader["AccrediationType"].ToString();
                x.Sys_ID = reader["Sys_ID"].ToString();
                x.Firstname = ConvertTab2Apos(reader["Firstname"].ToString());
                x.Surname = ConvertTab2Apos(reader["Surname"].ToString());
                x.Email = ConvertTab2Apos(reader["Email"].ToString());
                x.xpassword = ConvertTab2Apos(reader["xpassword"].ToString());
                x.DateOfBrith = reader["DateOfBrith"].ToString();
                x.IncorporatedDate = reader["IncorporatedDate"].ToString();
                x.Nationality = reader["Nationality"].ToString();
                x.PhoneNumber = reader["PhoneNumber"].ToString();
                x.CompanyName = ConvertTab2Apos(reader["CompanyName"].ToString());
                x.CompanyAddress = ConvertTab2Apos(reader["CompanyAddress"].ToString());
                x.ContactPerson = ConvertTab2Apos(reader["ContactPerson"].ToString());
                x.ContactPersonPhone = reader["ContactPersonPhone"].ToString();
                x.CompanyWebsite = ConvertTab2Apos(reader["CompanyWebsite"].ToString());
                x.Certificate = reader["Certificate"].ToString();
                x.Introduction = reader["Introduction"].ToString();
                x.Principal = reader["Principal"].ToString();
                x.logo = reader["logo"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xstatus = reader["xstatus"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return x;
        }
        public XObjs.Registration getRegistrationBySysID(string email, string sys_id)
        {
            XObjs.Registration x = new XObjs.Registration();
            SqlConnection connection = new SqlConnection(this.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM registrations WHERE Email='" + email + "'  AND Sys_ID='" + sys_id + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.AccrediationType = reader["AccrediationType"].ToString();
                x.Sys_ID = reader["Sys_ID"].ToString();
                x.Firstname = ConvertTab2Apos(reader["Firstname"].ToString());
                x.Surname = ConvertTab2Apos(reader["Surname"].ToString());
                x.Email = ConvertTab2Apos(reader["Email"].ToString());
                x.xpassword = ConvertTab2Apos(reader["xpassword"].ToString());
                x.DateOfBrith = reader["DateOfBrith"].ToString();
                x.IncorporatedDate = reader["IncorporatedDate"].ToString();
                x.Nationality = reader["Nationality"].ToString();
                x.PhoneNumber = reader["PhoneNumber"].ToString();
                x.CompanyName = ConvertTab2Apos(reader["CompanyName"].ToString());
                x.CompanyAddress = ConvertTab2Apos(reader["CompanyAddress"].ToString());
                x.ContactPerson = ConvertTab2Apos(reader["ContactPerson"].ToString());
                x.ContactPersonPhone = reader["ContactPersonPhone"].ToString();
                x.CompanyWebsite = ConvertTab2Apos(reader["CompanyWebsite"].ToString());
                x.Certificate = reader["Certificate"].ToString();
                x.Introduction = reader["Introduction"].ToString();
                x.Principal = reader["Principal"].ToString();
                x.logo = reader["logo"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xstatus = reader["xstatus"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return x;
        }
        public XObjs.Registration getRegistrationByID(string xid)
        {
            XObjs.Registration x = new XObjs.Registration();
            SqlConnection connection = new SqlConnection(this.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM registrations WHERE xid='" + xid + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.AccrediationType = reader["AccrediationType"].ToString();
                x.Sys_ID = reader["Sys_ID"].ToString();
                x.Firstname = ConvertTab2Apos(reader["Firstname"].ToString());
                x.Surname = ConvertTab2Apos(reader["Surname"].ToString());
                x.Email = ConvertTab2Apos(reader["Email"].ToString());
                x.xpassword = ConvertTab2Apos(reader["xpassword"].ToString());
                x.DateOfBrith = reader["DateOfBrith"].ToString();
                x.IncorporatedDate = reader["IncorporatedDate"].ToString();
                x.Nationality = reader["Nationality"].ToString();
                x.PhoneNumber = reader["PhoneNumber"].ToString();
                x.CompanyName = ConvertTab2Apos(reader["CompanyName"].ToString());
                x.CompanyAddress = ConvertTab2Apos(reader["CompanyAddress"].ToString());
                x.ContactPerson = ConvertTab2Apos(reader["ContactPerson"].ToString());
                x.ContactPersonPhone = reader["ContactPersonPhone"].ToString();
                x.CompanyWebsite = ConvertTab2Apos(reader["CompanyWebsite"].ToString());
                x.Certificate = reader["Certificate"].ToString();
                x.Introduction = reader["Introduction"].ToString();
                x.Principal = reader["Principal"].ToString();
                x.logo = reader["logo"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xstatus = reader["xstatus"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return x;
        }
        public XObjs.Registration getRegistration()
        {
            XObjs.Registration x = new XObjs.Registration();
            SqlConnection connection = new SqlConnection(this.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM registrations ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.AccrediationType = reader["AccrediationType"].ToString();
                x.Sys_ID = reader["Sys_ID"].ToString();
                x.Firstname = reader["Firstname"].ToString();
                x.Surname = reader["Surname"].ToString();
                x.Email = reader["Email"].ToString();
                x.xpassword = reader["xpassword"].ToString();
                x.DateOfBrith = reader["DateOfBrith"].ToString();
                x.IncorporatedDate = reader["IncorporatedDate"].ToString();
                x.Nationality = reader["Nationality"].ToString();
                x.PhoneNumber = reader["PhoneNumber"].ToString();
                x.CompanyName = reader["CompanyName"].ToString();
                x.CompanyAddress = reader["CompanyAddress"].ToString();
                x.ContactPerson = reader["ContactPerson"].ToString();
                x.ContactPersonPhone = reader["ContactPersonPhone"].ToString();
                x.CompanyWebsite = reader["CompanyWebsite"].ToString();
                x.Certificate = reader["Certificate"].ToString();
                x.Introduction = reader["Introduction"].ToString();
                x.Principal = reader["Principal"].ToString();
                x.logo = reader["logo"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xstatus = reader["xstatus"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return x;
        }
        public XObjs.Subagent getSubAgentBySysID(string email, string sys_id)
        {
            XObjs.Subagent x = new XObjs.Subagent();
            SqlConnection connection = new SqlConnection(this.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM subagents WHERE Email='" + email + "'   AND Sys_ID='" + sys_id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.RegistrationID = reader["RegistrationID"].ToString();
                x.Surname = ConvertTab2Apos(reader["Surname"].ToString());
                x.Firstname = ConvertTab2Apos(reader["Firstname"].ToString());
                x.Email = ConvertTab2Apos(reader["Email"].ToString());
                x.xpassword = ConvertTab2Apos(reader["xpassword"].ToString());
                x.Telephone = reader["Telephone"].ToString();
                x.DateOfBrith = reader["DateOfBrith"].ToString();
                x.AssignID = reader["AssignID"].ToString();
                x.Sys_ID = reader["Sys_ID"].ToString();
                x.Address = ConvertTab2Apos(reader["Address"].ToString());
                x.AgentPassport = reader["AgentPassport"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xstatus = reader["xstatus"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return x;
        }
        public XObjs.Subagent getSubAgentByLogin(string email, string xpass)
        {
            XObjs.Subagent x = new XObjs.Subagent();
            SqlConnection connection = new SqlConnection(this.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM subagents WHERE Email='" + email + "'  AND xpassword='" + xpass + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.RegistrationID = reader["RegistrationID"].ToString();
                x.Surname = ConvertTab2Apos(reader["Surname"].ToString());
                x.Firstname = ConvertTab2Apos(reader["Firstname"].ToString());
                x.Email = ConvertTab2Apos(reader["Email"].ToString());
                x.xpassword = ConvertTab2Apos(reader["xpassword"].ToString());
                x.Telephone = reader["Telephone"].ToString();
                x.DateOfBrith = reader["DateOfBrith"].ToString();
                x.AssignID = reader["AssignID"].ToString();
                x.Sys_ID = reader["Sys_ID"].ToString();
                x.Address = ConvertTab2Apos(reader["Address"].ToString());
                x.AgentPassport = reader["AgentPassport"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xstatus = reader["xstatus"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return x;
        }
        public XObjs.Subagent getSubAgentByID(string xid)
        {
            XObjs.Subagent x = new XObjs.Subagent();
            SqlConnection connection = new SqlConnection(this.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM subagents WHERE xid='" + xid + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.RegistrationID = reader["RegistrationID"].ToString();
                x.Surname = ConvertTab2Apos(reader["Surname"].ToString());
                x.Firstname = ConvertTab2Apos(reader["Firstname"].ToString());
                x.Email = ConvertTab2Apos(reader["Email"].ToString());
                x.xpassword = ConvertTab2Apos(reader["xpassword"].ToString());
                x.Telephone = reader["Telephone"].ToString();
                x.DateOfBrith = reader["DateOfBrith"].ToString();
                x.AssignID = reader["AssignID"].ToString();
                x.Sys_ID = reader["Sys_ID"].ToString();
                x.Address = ConvertTab2Apos(reader["Address"].ToString());
                x.AgentPassport = reader["AgentPassport"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xstatus = reader["xstatus"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return x;
        }
        public XObjs.Subagent getSubAgent()
        {
            XObjs.Subagent x = new XObjs.Subagent();
            SqlConnection connection = new SqlConnection(this.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM subagents", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.RegistrationID = reader["RegistrationID"].ToString();
                x.Surname = ConvertTab2Apos(reader["Surname"].ToString());
                x.Firstname = ConvertTab2Apos(reader["Firstname"].ToString());
                x.Email = ConvertTab2Apos(reader["Email"].ToString());
                x.xpassword = ConvertTab2Apos(reader["xpassword"].ToString());
                x.Telephone = reader["Telephone"].ToString();
                x.DateOfBrith = reader["DateOfBrith"].ToString();
                x.AssignID = reader["AssignID"].ToString();
                x.Sys_ID = reader["Sys_ID"].ToString();
                x.Address = ConvertTab2Apos(reader["Address"].ToString());
                x.AgentPassport = reader["AgentPassport"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xstatus = reader["xstatus"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return x;
        }


        public Classes.XObjs.Applicant getApplicantByID(string xid)
        {
            Classes.XObjs.Applicant x = new XObjs.Applicant();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM applicant WHERE xid='" + xid + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.xname = reader["xname"].ToString();
                x.address = reader["address"].ToString();
                x.xemail = reader["xemail"].ToString();
                x.xmobile = reader["xmobile"].ToString();
            }
            reader.Close();
            return x;
        }

        public Classes.XObjs.Address getAddressByID(string xid)
        {
            Classes.XObjs.Address x = new XObjs.Address();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM address WHERE ID='" + xid + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.ID = reader["ID"].ToString();
                x.city = reader["city"].ToString();
                x.countryID = reader["countryID"].ToString();
                x.email1 = reader["email1"].ToString();
                x.email2 = reader["email2"].ToString();
                x.lgaID = reader["lgaID"].ToString();
                x.log_staff = reader["log_staff"].ToString();
                x.reg_date = reader["reg_date"].ToString();
                x.stateID = reader["stateID"].ToString();
                x.street = reader["street"].ToString();
                x.telephone1 = reader["telephone1"].ToString();
                x.telephone2 = reader["telephone2"].ToString();
                x.visible = reader["visible"].ToString();
                x.xsync = reader["xsync"].ToString();
                x.zip = reader["zip"].ToString();
            }
            reader.Close();
            return x;
        }
        public List<Classes.XObjs.Fee_list> getAllFee_list()
        {
            List<Classes.XObjs.Fee_list> xlist = new List<Classes.XObjs.Fee_list>();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM fee_list", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Classes.XObjs.Fee_list x = new XObjs.Fee_list();
                x.xid = reader["xid"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.item = reader["item"].ToString();
                x.item_code = reader["item_code"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.xcategory = reader["xcategory"].ToString();
                x.xdesc = reader["xdesc"].ToString();
                x.xlogstaff = reader["xlogstaff"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xsync = reader["xsync"].ToString();
                x.xvisible = reader["xvisible"].ToString();

                xlist.Add(x);
            }
            reader.Close();
            return xlist;
        }

        public Classes.XObjs.Twallet getTwalletByTransID(string transID)
        {
            Classes.XObjs.Twallet x = new XObjs.Twallet();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * from twallet where transID='" + transID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.transID = reader["transID"].ToString();
                x.xmemberID = reader["xmemberID"].ToString();
                x.xmembertype = reader["xmembertype"].ToString();
                x.xpay_status = reader["xpay_status"].ToString();
                x.xgt = reader["xgt"].ToString();
                x.ref_no = reader["ref_no"].ToString();
                x.xbankerID = reader["xbankerID"].ToString();
                x.applicantID = reader["applicantID"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xsync = reader["xsync"].ToString();
                x.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return x;
        }
        public Classes.XObjs.Twallet getTwalletByTransIDAdminID(string transID, string xmemberID, string xmembertype)
        {
            Classes.XObjs.Twallet x = new XObjs.Twallet();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * from twallet where (transID='" + transID + "') AND  (xmemberID='" + xmemberID + "') AND  (xmembertype='" + xmembertype + "') ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.transID = reader["transID"].ToString();
                x.xmemberID = reader["xmemberID"].ToString();
                x.xmembertype = reader["xmembertype"].ToString();
                x.xpay_status = reader["xpay_status"].ToString();
                x.xgt = reader["xgt"].ToString();
                x.ref_no = reader["ref_no"].ToString();
                x.xbankerID = reader["xbankerID"].ToString();
                x.applicantID = reader["applicantID"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xsync = reader["xsync"].ToString();
                x.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return x;
        }
        public List<Classes.XObjs.Twallet> getTwalletByMemberID(string xmemberID, string transID, string xmembertype)
        {
            List<Classes.XObjs.Twallet> xlist = new List<Classes.XObjs.Twallet>();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * from twallet where (xmemberID='" + xmemberID + "') AND (transID='" + transID + "') AND (xmembertype='" + xmembertype + "') ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Classes.XObjs.Twallet x = new XObjs.Twallet();
                x.xid = reader["xid"].ToString();
                x.transID = reader["transID"].ToString();
                x.xmemberID = reader["xmemberID"].ToString();
                x.xmembertype = reader["xmembertype"].ToString();
                x.xpay_status = reader["xpay_status"].ToString();
                x.xgt = reader["xgt"].ToString();
                x.ref_no = reader["ref_no"].ToString();
                x.xbankerID = reader["xbankerID"].ToString();
                x.applicantID = reader["applicantID"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xsync = reader["xsync"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                xlist.Add(x);
            }
            reader.Close();
            return xlist;
        }
        public List<Classes.XObjs.Twallet> getValidatedTwalletByMemberID(string xmemberID, string transID)
        {
            List<Classes.XObjs.Twallet> xlist = new List<Classes.XObjs.Twallet>();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * from twallet where xmemberID='" + xmemberID + "'  AND transID='" + transID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Classes.XObjs.Twallet x = new XObjs.Twallet();
                x.xid = reader["xid"].ToString();
                x.transID = reader["transID"].ToString();
                x.xmemberID = reader["xmemberID"].ToString();
                x.xpay_status = reader["xpay_status"].ToString();
                x.xgt = reader["xgt"].ToString();
                x.ref_no = reader["ref_no"].ToString();
                x.xbankerID = reader["xbankerID"].ToString();
                x.applicantID = reader["applicantID"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xsync = reader["xsync"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                xlist.Add(x);
            }
            reader.Close();
            return xlist;
        }

        public Classes.XObjs.PRatio getPratioByMemberID(string xmemberID)
        {
            Classes.XObjs.PRatio x = new XObjs.PRatio();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * from p_ratio where xpartnerID='" + xmemberID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {               
                x.xid = reader["xid"].ToString();
                x.xpartnerID = reader["xpartnerID"].ToString();
                x.p_type = reader["p_type"].ToString();
                x.xratio = reader["xratio"].ToString();
                x.r_type = reader["r_type"].ToString();              
                x.xreg_date = reader["xreg_date"].ToString();
                x.xsync = reader["xsync"].ToString();
                x.xvisible = reader["xvisible"].ToString();
              
            }
            reader.Close();
            return x;
        }

        public List<Classes.XObjs.Fee_details> getFee_detailsByTwalletID(string twalletID)
        {
            List<Classes.XObjs.Fee_details> xlist = new List<Classes.XObjs.Fee_details>();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * from fee_details where twalletID='" + twalletID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Classes.XObjs.Fee_details x = new XObjs.Fee_details();
                x.xid = reader["xid"].ToString();
                x.fee_listID = reader["fee_listID"].ToString();
                x.twalletID = reader["twalletID"].ToString();
                x.xqty = reader["xqty"].ToString();
                x.xused = reader["xused"].ToString();
                x.tot_amt = reader["tot_amt"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.xreg_date = reader["xlogstaff"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xsync = reader["xsync"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                xlist.Add(x);
            }
            reader.Close();
            return xlist;

        }
        public Classes.XObjs.Fee_details getFee_detailsByID(string xid)
        {
            Classes.XObjs.Fee_details x = new XObjs.Fee_details();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select * from  fee_details where xid='" + xid + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.fee_listID = reader["fee_listID"].ToString();
                x.twalletID = reader["twalletID"].ToString();
                x.xqty = reader["xqty"].ToString();
                x.xused = reader["xused"].ToString();
                x.tot_amt = reader["tot_amt"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.xreg_date = reader["xlogstaff"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xsync = reader["xsync"].ToString();
                x.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return x;
        }

        public Classes.XObjs.Fee_details getFee_detailsByID(string xid, string cat, string xmemberID)
        {
            Classes.XObjs.Fee_details x = new XObjs.Fee_details();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select fee_details.*,fee_list.* from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid  INNER JOIN twallet ON  twallet.xid=fee_details.twalletID where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + xmemberID + "' AND twallet.xpay_status='1' AND xid='" + xid + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.fee_listID = reader["fee_listID"].ToString();
                x.twalletID = reader["twalletID"].ToString();
                x.xqty = reader["xqty"].ToString();
                x.xused = reader["xused"].ToString();
                x.tot_amt = reader["tot_amt"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.xreg_date = reader["xlogstaff"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xsync = reader["xsync"].ToString();
                x.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return x;
        }
        public Classes.XObjs.Fee_details getFee_detailsByHwalletID(string hID)
        {
            Classes.XObjs.Fee_details x = new XObjs.Fee_details();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("select fee_details.* from fee_details where xid in (select hwallet.fee_detailsID from hwallet where hwallet.xid='"+hID+"') ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.fee_listID = reader["fee_listID"].ToString();
                x.twalletID = reader["twalletID"].ToString();
                x.xqty = reader["xqty"].ToString();
                x.xused = reader["xused"].ToString();
                x.tot_amt = reader["tot_amt"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.xreg_date = reader["xlogstaff"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xsync = reader["xsync"].ToString();
                x.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return x;
        }
        public Classes.XObjs.Scard getRandomScard()
        {
            Classes.XObjs.Scard x = new XObjs.Scard();
            SqlConnection connection = new SqlConnection(this.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM scard ORDER BY NEWID()   ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {                
                x.xid = reader["xid"].ToString();
                x.xnum = reader["xnum"].ToString();
                x.xvalid = reader["xvalid"].ToString();
                x.xlogstaff = reader["xlogstaff"].ToString();             
                x.xvalid = reader["xvalid"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xsync = reader["xsync"].ToString();
                x.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return x;
        }
       
    }
}