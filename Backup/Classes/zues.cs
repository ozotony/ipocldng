﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Ipong.Classes
{   

    public class zues
    {
        public string a_regadmin(string xname, string xrole, string xemail, string telephone1, string telephone2, string xsection, string pwalletID,string pass)
        {
            string connectionString = this.Connect();
            string str2 = DateTime.Today.Date.ToString("yyyy-MM-dd");
            string str3 = "";
            new Random();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("sp_a_TmRegAdmin", connection) {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@xname", xname));
            command.Parameters.Add(new SqlParameter("@xroleID", xrole));
            command.Parameters.Add(new SqlParameter("@xemail", xemail));
            command.Parameters.Add(new SqlParameter("@xpass", pass));
            command.Parameters.Add(new SqlParameter("@xtelephone1", telephone1));
            command.Parameters.Add(new SqlParameter("@xtelephone2", telephone2));
            command.Parameters.Add(new SqlParameter("@xsection", xsection));
            command.Parameters.Add(new SqlParameter("@xlog_officer", pwalletID));
            command.Parameters.Add(new SqlParameter("@xreg_date", str2));
            command.Parameters.Add(new SqlParameter("@xvisible", "1"));
            SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            str3 = parameter.Value.ToString();
            connection.Close();
            return str3;
        }

        public string a_tm_office(string pwalletID, string admin_status, string data_status, string xcomment, string xdoc1, string xdoc2, string xdoc3, string xofficer)
        {
            string connectionString = this.Connect();
            string str2 = DateTime.Today.Date.ToString("yyyy-MM-dd");
            string str3 = "";
            xdoc1 = xdoc1.Replace(" ", "_");
            xdoc2 = xdoc2.Replace(" ", "_");
            xdoc3 = xdoc3.Replace(" ", "_");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("sp_a_tm_office", connection) {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@pwalletID", pwalletID));
            command.Parameters.Add(new SqlParameter("@admin_status", admin_status));
            command.Parameters.Add(new SqlParameter("@data_status", data_status));
            command.Parameters.Add(new SqlParameter("@xcomment", xcomment));
            command.Parameters.Add(new SqlParameter("@xdoc1", xdoc1));
            command.Parameters.Add(new SqlParameter("@xdoc2", xdoc2));
            command.Parameters.Add(new SqlParameter("@xdoc3", xdoc3));
            command.Parameters.Add(new SqlParameter("@xofficer", xofficer));
            command.Parameters.Add(new SqlParameter("@reg_date", str2));
            command.Parameters.Add(new SqlParameter("@xvisible", "1"));
            SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            str3 = parameter.Value.ToString();
            connection.Close();
            if (str3 == "0")
            {
                return "0";
            }
            if (!(Convert.ToInt32(this.e_PwalletStatus(pwalletID, admin_status, data_status)).ToString() != "0"))
            {
                str3 = "0";
            }
            return str3;
        }

        public string a_xadminz(string uname, string xpass, string serverpath)
        {
            List<Adminz> lt_adz = new List<Adminz>();
            string file_string = "Xavier";
            int file_len = 1024;

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
            string connectionString = this.Connect();
            string xID = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("select xID,xemail,xpass from xadminz_tm where xvisible='1' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Adminz ad = new Adminz();
                ad.xID = reader["xID"].ToString();
                ad.xemail = reader["xemail"].ToString();
                ad.xpass = reader["xpass"].ToString();
                lt_adz.Add(ad);
            }
            reader.Close();
            string dpass = ""; string dmail = "";
            for (int i = 0; i < lt_adz.Count; i++)
            {
               // dmail = ody.DecryptString(lt_adz[i].xemail, file_len, file_string);
               // dpass = ody.DecryptString(lt_adz[i].xpass, file_len, file_string);
                if ((uname == dmail) && (xpass == dpass))
                {
                    xID = lt_adz[i].xID.ToString();
                }
            }
            return xID;
        }

        public string addAdminLog(string adminID,string ip_addy,string remote_host,string remote_user,string server_name,string server_url)
        {
            string log_date = DateTime.Today.Date.ToString("yyyy-MM-dd");
            string log_time = DateTime.Now.ToLongTimeString();
           
            string connectionString = this.Connect();
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
        public string Connect()
        {
            return ConfigurationManager.ConnectionStrings["cldConnectionString"].ConnectionString;
        }

        public int e_PwalletStatus(string xID, string status, string data_status)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            connection.Open();
            SqlCommand command = new SqlCommand("sp_u_PwalletStatus", connection) {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@ID", xID));
            command.Parameters.Add(new SqlParameter("@status", status));
            command.Parameters.Add(new SqlParameter("@data_status", data_status));
            SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            int num = (int) parameter.Value;
            connection.Close();
            if (num > 0)
            {
                num = Convert.ToInt32(xID);
            }
            return num;
        }

        public int e_regadmin(string xname, string xpass, string xrole, string xemail, string telephone1, string telephone2, string xsection, string pwalletID, string xID, string visible)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE xadminz_tm SET xname=@xname,xpass=@xpass,xroleID=@xroleID,xemail=@xemail,xtelephone1=@xtelephone1,xtelephone2=@xtelephone2,xsection=@xsection,xlog_officer=@pwalletID,xvisible=@xvisible WHERE xID=@xID ";
            connection.Open();
            command.Parameters.Add("@xID", SqlDbType.BigInt);
            command.Parameters.Add("@xname", SqlDbType.NVarChar);
            command.Parameters.Add("@xpass", SqlDbType.Text);
            command.Parameters.Add("@xroleID", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@xemail", SqlDbType.Text);
            command.Parameters.Add("@xtelephone1", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@xtelephone2", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@xsection", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@pwalletID", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@xvisible", SqlDbType.NVarChar, 1);
            command.Parameters["@xID"].Value = xID;
            command.Parameters["@xname"].Value = xname;
            command.Parameters["@xpass"].Value = xpass;
            command.Parameters["@xroleID"].Value = xrole;
            command.Parameters["@xemail"].Value = xemail;
            command.Parameters["@xtelephone1"].Value = telephone1;
            command.Parameters["@xtelephone2"].Value = telephone2;
            command.Parameters["@xsection"].Value = xsection;
            command.Parameters["@pwalletID"].Value = pwalletID;
            command.Parameters["@xvisible"].Value = visible;
            int num = command.ExecuteNonQuery();
            connection.Close();
            if (num > 0)
            {
                num = Convert.ToInt32(xID);
            }
            return num;
        }

        public string e_xadminz(string adminID, string xpass)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            connection.Open();
            SqlCommand command = new SqlCommand("sp_u_xadminz_pass", connection) {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@pwalletID", adminID));
            command.Parameters.Add(new SqlParameter("@xpass", xpass));
            SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            adminID = parameter.Value.ToString();
            connection.Close();
            return adminID;
        }

        public string GetAddressTags(string select_search)
        {
            return "";
        }

        public Adminz getAdminDetails(string ID)
        {
            Adminz adminz = new Adminz();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * from xadminz_tm where xID='" + ID + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                adminz.xID = reader["xID"].ToString();
                adminz.xroleID = reader["xroleID"].ToString();
                adminz.xname = reader["xname"].ToString();
                adminz.xemail = reader["xemail"].ToString();
                adminz.xpass = reader["xpass"].ToString();
                adminz.xtelephone1 = reader["xtelephone1"].ToString();
                adminz.xtelephone2 = reader["xtelephone2"].ToString();
                adminz.xsection = reader["xsection"].ToString();
                adminz.xlog_officer = reader["xlog_officer"].ToString();
                adminz.xreg_date = reader["xreg_date"].ToString();
                adminz.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return adminz;
        }

        public string getApplicant(string log_staff)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT xname from applicant where log_staff='" + log_staff + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["xname"].ToString();
            }
            reader.Close();
            return str;
        }

        public List<MarkInfo> getCriAccpetanceMarkInfoRS(string stage, string data_status)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            string cmdText = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            if (data_status == "Refused")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status='" + stage + "' AND ( pwallet.data_status = 'Refused')  ORDER BY xID ASC";
            }
            else if (data_status == "Registrable")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status='" + stage + "' AND ( pwallet.data_status = 'Registrable')  ORDER BY xID ASC";
            }
            else if (data_status == "Non-registrable")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status='" + stage + "' AND ( pwallet.data_status = 'Non-registrable')  ORDER BY xID ASC";
            }
            else if (data_status == "XRegistrable")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status='" + stage + "' AND (( pwallet.data_status = 'Registrable') OR ( pwallet.data_status = 'Non-registrable'))  ORDER BY xID ASC";
            }
            else if (data_status == "Accepted")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status >'" + stage + "' AND pwallet.data_status='" + data_status + "' ORDER BY xID ASC";
            }
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<MarkInfo> getCriCertifyMarkInfoRS(string stage, string data_status)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            string cmdText = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            if (data_status == "Not Opposed")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status='" + stage + "' AND ( pwallet.data_status = 'Not Opposed') (pwallet.stage='5') ORDER BY xID ASC";
            }
            else if (data_status == "Deferred")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status='" + stage + "' AND ( pwallet.data_status = 'Deferred')  AND  (pwallet.stage='5') ORDER BY xID ASC";
            }
            else if (data_status == "Certified")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status >'" + stage + "' AND pwallet.data_status='" + data_status + "' AND  pwallet.stage='5' ORDER BY xID ASC";
            }
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<MarkInfo> getCriMarkInfoRS(string stage, string data_status)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            string cmdText = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            if (data_status != "Re-conduct search")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status='" + stage + "' AND ( pwallet.data_status <> 'Re-conduct search')  ORDER BY xID ASC";
            }
            else
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status='" + stage + "' AND ( pwallet.data_status = '" + data_status + "')  ORDER BY xID ASC";
            }

            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<MarkInfo> getCriOppesedMarkInfoRS(string stage, string data_status)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            string cmdText = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            if (data_status == "Opposed")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status='" + stage + "' AND ( pwallet.data_status = 'Opposed')  ORDER BY xID ASC";
            }
            else if (data_status == "Published")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status='" + stage + "' AND ( pwallet.data_status = 'Published')  ORDER BY xID ASC";
            }
            else if (data_status == "")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status > '" + stage + "'   ORDER BY xID ASC";
            }
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<MarkInfo> getCriPublishMarkInfoRS(string stage, string data_status)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            string cmdText = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            if (data_status == "Accepted")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status='" + stage + "' AND ( pwallet.data_status = 'Accepted') AND ( pwallet.applicantID <> 'CLD/SA/22')  ORDER BY CAST(national_classID AS INT), xID ASC";
            }
            else if (data_status == "Deferred")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status='" + stage + "' AND ( pwallet.data_status = 'Deferred') AND ( pwallet.applicantID <> 'CLD/SA/22') ORDER BY CAST(national_classID AS INT), xID ASC";
            }
            else if (data_status == "Published")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status >'" + stage + "' AND ( pwallet.applicantID <> 'CLD/SA/22') ORDER BY CAST(national_classID AS INT), xID ASC ";
            }
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<MarkInfo> getCriRegisteredMarkInfoRS(string stage, string data_status)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            string cmdText = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            if (data_status == "Certified")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status='" + stage + "' AND ( pwallet.data_status = 'Certified')  ORDER BY xID ASC";
            }
            else if (data_status == "Deferred")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status='" + stage + "' AND ( pwallet.data_status = 'Deferred')  ORDER BY xID ASC";
            }
            else if (data_status == "Registered")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.status >'" + stage + "' AND pwallet.data_status='" + data_status + "' ORDER BY xID ASC";
            }
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<TmOffice> getCurrentTmOfficeDetailsByID(string pwalletID, string admin_status, string data_status)
        {
            List<TmOffice> list = new List<TmOffice>();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT top 1 * FROM tm_office where pwalletID='" + pwalletID + "' AND admin_status='" + admin_status + "' AND data_status='" + data_status + "' ORDER BY ID ASC", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                TmOffice item = new TmOffice {
                    ID = reader["ID"].ToString(),
                    pwalletID = reader["pwalletID"].ToString(),
                    admin_status = reader["admin_status"].ToString(),
                    data_status = reader["data_status"].ToString(),
                    xcomment = reader["xcomment"].ToString(),
                    xdoc1 = reader["xdoc1"].ToString(),
                    xdoc2 = reader["xdoc2"].ToString(),
                    xdoc3 = reader["xdoc3"].ToString(),
                    xofficer = reader["xofficer"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public string getLogoDescriptionNameByID(string id)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT type from logo_description where xID='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["type"].ToString();
            }
            reader.Close();
            return str;
        }

        public List<MarkInfo> getMarkInfoByDataStatusRS(string stage, string data_status)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("select * from mark_info LEFT OUTER JOIN tm_office ON mark_info.log_staff=tm_office.pwalletID WHERE tm_office.admin_status='" + stage + "' AND tm_office.data_status='" + data_status + "' ORDER BY xID ASC", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public MarkInfo getMarkInfoByUserID(string ID)
        {
            MarkInfo info = new MarkInfo();
            info.xID = "";
            info.reg_number = "";
            info.tm_typeID = "";
            info.logo_descriptionID = "";
            info.national_classID = "";
            info.product_title = "";
            info.nice_class = "";
            info.nice_class_desc = "";
            info.sign_type = "";
            info.vienna_class = "";
            info.disclaimer = "";
            info.logo_pic = "";
            info.auth_doc = "";
            info.sup_doc1 = "";
            info.sup_doc2 = "";
            info.log_staff = "";
            info.reg_date = "";
            info.xvisible = "";

            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM mark_info WHERE xID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                info.xID = reader["xID"].ToString();
                info.reg_number = reader["reg_number"].ToString();
                info.tm_typeID = reader["tm_typeID"].ToString();
                info.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info.national_classID = reader["national_classID"].ToString();
                info.product_title = reader["product_title"].ToString();
                info.nice_class = reader["nice_class"].ToString();
                info.nice_class_desc = reader["nice_class_desc"].ToString();
                info.sign_type = reader["sign_type"].ToString();
                info.vienna_class = reader["vienna_class"].ToString();
                info.disclaimer = reader["disclaimer"].ToString();
                info.logo_pic = reader["logo_pic"].ToString();
                info.auth_doc = reader["auth_doc"].ToString();
                info.sup_doc1 = reader["sup_doc1"].ToString();
                info.sup_doc2 = reader["sup_doc2"].ToString();
                info.log_staff = reader["log_staff"].ToString();
                info.reg_date = reader["reg_date"].ToString();
                info.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return info;
        }

        public List<MarkInfo> getMarkInfoRS(string status, string data_status)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = null;
            //
            if ((status == "4") && ((data_status == "Refused") || (data_status == "Non-registrable")))
            {
                command = new SqlCommand("select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status='" + status + "' AND ((pwallet.data_status='Non-registrable') OR (pwallet.data_status='Refused')) ORDER BY xID ASC", connection);
            }
            else
            {
                command = new SqlCommand("select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status='" + status + "' AND pwallet.data_status='" + data_status + "' ORDER BY xID ASC", connection);
            }
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<MarkInfo> getMarkInfoRSX(string status, string data_status,string start,string limit)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            SqlConnection connection = new SqlConnection(this.Connect());

            if ((status == "4") && ((data_status == "Refused") || (data_status == "Non-registrable")))
            {
                SqlCommand command = new SqlCommand("WITH RSTbl AS (select mark_info.xID,mark_info.reg_number,mark_info.product_title,mark_info.tm_typeID,mark_info.reg_date,mark_info.log_staff, ROW_NUMBER() OVER (ORDER BY mark_info.xID) AS 'RowRank' from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status='" + status + "' AND ((pwallet.data_status='Non-registrable') OR (pwallet.data_status='Refused')) )SELECT * FROM RSTbl  WHERE RowRank BETWEEN '" + start + "' AND '" + limit + "' ", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    MarkInfo item = new MarkInfo
                    {
                        xID = reader["xID"].ToString(),
                        reg_number = reader["reg_number"].ToString(),
                        tm_typeID = reader["tm_typeID"].ToString(),
                        product_title = reader["product_title"].ToString(),
                        log_staff = reader["log_staff"].ToString(),
                        reg_date = reader["reg_date"].ToString(),
                    };
                    list.Add(item);
                }
                reader.Close();
            }
            else
            {
                SqlCommand command = new SqlCommand("WITH RSTbl AS (select mark_info.xID,mark_info.reg_number,mark_info.product_title,mark_info.tm_typeID,mark_info.reg_date,mark_info.log_staff, ROW_NUMBER() OVER (ORDER BY mark_info.xID) AS 'RowRank' from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status='" + status + "' AND pwallet.data_status='" + data_status + "' )SELECT * FROM RSTbl  WHERE RowRank BETWEEN '" + start + "' AND '" + limit + "' ", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    MarkInfo item = new MarkInfo
                    {
                        xID = reader["xID"].ToString(),
                        reg_number = reader["reg_number"].ToString(),
                        tm_typeID = reader["tm_typeID"].ToString(),
                        product_title = reader["product_title"].ToString(),
                        log_staff = reader["log_staff"].ToString(),
                        reg_date = reader["reg_date"].ToString(),
                    };
                    list.Add(item);
                }
                reader.Close();
            }
            
            return list;
        }
      
          public List<MarkInfo> getAcceptanceMarkInfoRSX(string status, string start, string limit)
        {           
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("WITH RSTbl AS (select mark_info.xID,mark_info.reg_number,mark_info.product_title,mark_info.tm_typeID,mark_info.reg_date,mark_info.log_staff, ROW_NUMBER() OVER (ORDER BY mark_info.xID) AS 'RowRank' from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE   pwallet.stage='5' AND pwallet.status<>'33' AND pwallet.status<>'22' AND CAST(pwallet.status AS INT)>=5  )SELECT * FROM RSTbl  WHERE RowRank BETWEEN '" + start + "' AND '" + limit + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo
                {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<MarkInfo> getPublishMarkInfoRSX(string status, string data_status, string start, string limit)
        {           
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("WITH RSTbl AS (select mark_info.xID,mark_info.reg_number,mark_info.product_title,mark_info.tm_typeID,mark_info.reg_date,mark_info.log_staff, ROW_NUMBER() OVER (ORDER BY mark_info.xID) AS 'RowRank' from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status<>'33' AND pwallet.status<>'22' AND CAST(pwallet.status AS INT)>=5 AND pwallet.data_status='" + data_status + "') SELECT * FROM RSTbl  WHERE RowRank BETWEEN '" + start + "' AND '" + limit + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo
                {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }
        public Int64 getMarkInfoRSCnt(string status, string data_status)
        {
            Int64 cnt = 0;
            SqlConnection connection = new SqlConnection(this.Connect());

            if ((status == "4") && ((data_status == "Refused") || (data_status == "Non-registrable")))
            {                
                SqlCommand command = new SqlCommand("select Count(*) AS cnt from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status='" + status + "' AND ((pwallet.data_status='Non-registrable') OR (pwallet.data_status='Refused')) ", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {

                    cnt = Convert.ToInt64(reader["cnt"]);
                }
                reader.Close();
            }
            else
            {
                SqlCommand command = new SqlCommand("select Count(*) AS cnt from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status='" + status + "' AND pwallet.data_status='" + data_status + "' ", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {

                    cnt = Convert.ToInt64(reader["cnt"]);
                }
                reader.Close();
            }            
           
            return cnt;
        }
        
         public Int64 getAcceptanceMarkInfoRSCnt(string status)
        {
            Int64 cnt = 0;
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("select Count(*) AS cnt from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status<>'33' AND pwallet.status<>'22' AND CAST(pwallet.status AS INT)>=5  ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {

                cnt = Convert.ToInt64(reader["cnt"]);
            }
            reader.Close();
            return cnt;
        }

        public Int64 getPublishMarkInfoRSCnt(string status, string data_status)
        {
            Int64 cnt = 0;
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("select Count(*) AS cnt from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status<>'33' AND pwallet.status<>'22' AND CAST(pwallet.status AS INT)>=5 AND pwallet.data_status = '" + data_status + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {

                cnt = Convert.ToInt64(reader["cnt"]);
            }
            reader.Close();
            return cnt;
        }
        public List<MarkInfo> getAcceptanceAdminSearchMarkInfoRS(string status, string criteria, List<string> fulltext, string dateFrom, string dateTo)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            string cmdText = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            int num = 0;
            SqlConnection connection = new SqlConnection(this.Connect());
            if (criteria == "product_title")
            {
                num = fulltext.Count - 1;
                str2 = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status<>'33' AND pwallet.status<>'22' AND CAST(pwallet.status AS INT)>=5 AND ";
                for (int i = 0; i < fulltext.Count; i++)
                {
                    if (fulltext.Count == 1)
                    {
                        str3 = str3 + " ( product_title like '%" + fulltext[i] + "%' ) ";
                    }
                    else if (num == i)
                    {
                        str3 = str3 + " ( product_title like '%" + fulltext[i] + "%' ) ";
                    }
                    else
                    {
                        str3 = str3 + " ( product_title like '%" + fulltext[i] + "%' ) OR";
                    }
                }
                str4 = "AND pwallet.reg_date between '" + dateFrom + "' AND '" + dateTo + "' ORDER BY xID ASC";
                cmdText = str2 + str3 + str4;
            }

            else if (criteria == "app_number")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status<>'33' AND pwallet.status<>'22' AND CAST(pwallet.status AS INT)>=5  AND pwallet.validationID like  '%" + fulltext[0] + "%' AND pwallet.reg_date between '" + dateFrom + "' AND '" + dateTo + "'  ORDER BY xID ASC ";


            }

            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo
                {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }


        public List<MarkInfo> getPublishAdminSearchMarkInfoRS(string status, string data_status, string criteria, List<string> fulltext, string dateFrom, string dateTo)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            string cmdText = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            int num = 0;
            SqlConnection connection = new SqlConnection(this.Connect());
            if (criteria == "product_title")
            {
                num = fulltext.Count - 1;
                str2 = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status<>'33' AND pwallet.status<>'22' AND CAST(pwallet.status AS INT)>=5 AND pwallet.data_status='" + data_status + "' AND ";
                for (int i = 0; i < fulltext.Count; i++)
                {
                    if (fulltext.Count == 1)
                    {
                        str3 = str3 + " ( product_title like '%" + fulltext[i] + "%' ) ";
                    }
                    else if (num == i)
                    {
                        str3 = str3 + " ( product_title like '%" + fulltext[i] + "%' ) ";
                    }
                    else
                    {
                        str3 = str3 + " ( product_title like '%" + fulltext[i] + "%' ) OR";
                    }
                }
                str4 = "AND pwallet.reg_date between '" + dateFrom + "' AND '" + dateTo + "' ORDER BY xID ASC";
                cmdText = str2 + str3 + str4;
            }

            else if (criteria == "app_number")
            {
                cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status<>'33' AND pwallet.status<>'22' AND CAST(pwallet.status AS INT)>=5  AND pwallet.data_status='" + data_status + "' AND pwallet.validationID like  '%" + fulltext[0] + "%' AND pwallet.reg_date between '" + dateFrom + "' AND '" + dateTo + "'  ORDER BY xID ASC ";

            }

            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo
                {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<MarkInfo> getMarkInfoSlipPlusRS(string stage)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT mark_info.*  FROM pwallet LEFT OUTER JOIN mark_info ON pwallet.ID=mark_info.log_staff WHERE pwallet.status >= '" + stage + "' AND mark_info.log_staff IN (Select ID  FROM pwallet) ORDER BY ID ASC", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<MarkInfo> getMarkInfoSlipRS(string stage, string status)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT mark_info.*  FROM mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID LEFT OUTER JOIN tm_office ON pwallet.ID=tm_office.pwalletID  WHERE tm_office.admin_status = '" + stage + "' AND tm_office.data_status='" + status + "' AND mark_info.log_staff IN (Select pwallet.ID  FROM pwallet) ORDER BY pwallet.ID DESC", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<MarkInfo> getMarkInfoXRS(string stage, string status)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT mark_info.*  FROM pwallet LEFT OUTER JOIN mark_info ON pwallet.ID=mark_info.log_staff LEFT OUTER JOIN tm_office ON tm_office.pwalletID=mark_info.log_staff WHERE pwallet.status = '" + stage + "'  AND tm_office.data_status='" + status + "' AND mark_info.log_staff IN (Select pwallet.ID  FROM pwallet) ORDER BY pwallet.ID DESC", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public Pwallet getPwalletDetailsByID(string ID)
        {
            Pwallet pwallet = new Pwallet();
            pwallet.ID = "";
            pwallet.applicantID = "";
            pwallet.validationID = "";
            pwallet.stage = "";
            pwallet.status = "";
            pwallet.data_status = "";
            pwallet.amt = "";
            pwallet.reg_date = "";

            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                pwallet.ID = reader["ID"].ToString();
                pwallet.applicantID = reader["applicantID"].ToString();
                pwallet.validationID = reader["validationID"].ToString();
                pwallet.stage = reader["stage"].ToString();
                pwallet.status = reader["status"].ToString();
                pwallet.data_status = reader["data_status"].ToString();
                pwallet.amt = reader["amt"].ToString();
                pwallet.reg_date = reader["reg_date"].ToString();
            }
            reader.Close();
            return pwallet;
        }

        public List<Pwallet> getPwalletListDetailsByID(string ID)
        {
            List<Pwallet> list = new List<Pwallet>();

            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Pwallet item = new Pwallet
                {
                ID = Convert.ToInt64(reader["ID"]).ToString(),
                applicantID = reader["applicantID"].ToString(),
                validationID = reader["validationID"].ToString(),
                stage = reader["stage"].ToString(),
                status = reader["status"].ToString(),
                data_status = reader["data_status"].ToString(),
                amt = reader["amt"].ToString(),
                reg_date = reader["reg_date"].ToString()
                };
                list.Add(item);

               
            }
            reader.Close();
            return list;
        }

        public string getPwalletIDByMID(string mark_infoID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT log_staff from mark_info where xID='" + mark_infoID + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["log_staff"].ToString();
            }
            reader.Close();
            return str;
        }

        public string getRoleByID(string id)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT xroleID from xadminz_tm where xID='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["xroleID"].ToString();
            }
            reader.Close();
            return str;
        }

        public string getRoleNameByID(string ID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT name FROM roles WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["name"].ToString();
            }
            reader.Close();
            return str;
        }

        public List<MarkInfo> getSearchMarkInfoRS(string kword, List<string> fulltext,string cri)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            string cmdText = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            int num = 0;
            SqlConnection connection = new SqlConnection(this.Connect());
            if (fulltext == null)
            {
                if (cri == "0")
                {
                    cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE (pwallet.status <>'22') AND (pwallet.status <>'33') AND (pwallet.status >'5') AND (product_title like '%" + kword + "%') ORDER BY xID ASC";
                }
                else
                {
                    cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE (pwallet.status <>'22') AND (pwallet.status <>'33') AND (pwallet.status >'5') AND (product_title like '%" + kword + "%') AND national_classID = '" + cri + "' ORDER BY xID ASC";
                }
            }
            else
            {
                num = fulltext.Count - 1;
                if (cri == "0")
                {

                    str2 = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE (pwallet.status <>'22') AND (pwallet.status <>'33') AND (pwallet.status >'5') AND ";
                }
                else
                {
                    str2 = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE (pwallet.status <>'22') AND (pwallet.status <>'33') AND (pwallet.status >'5') AND (national_classID = '" + cri + "') AND ";
                }
                for (int i = 0; i < fulltext.Count; i++)
                {
                    if (fulltext.Count == 1)
                    {
                        str3 = str3 + " ( product_title like '%" + fulltext[i] + "%' ) ";
                    }
                    else if (num == i)
                    {
                        str3 = str3 + " ( product_title like '%" + fulltext[i] + "%' ) ";
                    }
                    else
                    {
                        str3 = str3 + " ( product_title like '%" + fulltext[i] + "%' ) OR";
                    }
                }
                str4 = " ORDER BY xID ASC";
                cmdText = str2 + str3 + str4;
            }
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<MarkInfo> getAdminSearchMarkInfoRS(string status, string data_status, string criteria, List<string> fulltext, string dateFrom, string dateTo)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            string cmdText = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            int num = 0;
            SqlConnection connection = new SqlConnection(this.Connect());
            if (criteria == "product_title")
            {
                num = fulltext.Count - 1;
                if ((status == "4") && ((data_status == "Refused") || (data_status == "Non-registrable")))
                {
                    str2 = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status='" + status + "' AND ((pwallet.data_status='Non-registrable') OR (pwallet.data_status='Refused'))  AND ";
                }
                else
                {
                    str2 = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status='" + status + "' AND pwallet.data_status='" + data_status + "' AND ";
                }
                for (int i = 0; i < fulltext.Count; i++)
                {
                    if (fulltext.Count == 1)
                    {
                        str3 = str3 + " ( product_title like '%" + fulltext[i] + "%' ) ";
                    }
                    else if (num == i)
                    {
                        str3 = str3 + " ( product_title like '%" + fulltext[i] + "%' ) ";
                    }
                    else
                    {
                        str3 = str3 + " ( product_title like '%" + fulltext[i] + "%' ) OR";
                    }
                }
                str4 = "AND pwallet.reg_date between '"+dateFrom+"' AND '"+dateTo+"'  ORDER BY xID ASC";
                cmdText = str2 + str3 + str4;
            }

            else if (criteria == "app_number")
            {
                if ((status == "4") && ((data_status == "Refused") || (data_status == "Non-registrable")))
                {
                    cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status='" + status + "' AND ((pwallet.data_status='Non-registrable') OR (pwallet.data_status='Refused')) AND pwallet.validationID like  '%" + fulltext[0] + "%' AND pwallet.reg_date between '" + dateFrom + "' AND '" + dateTo + "'  ORDER BY xID ASC ";
                }
                else
                {
                    cmdText = "select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status='" + status + "' AND pwallet.data_status='" + data_status + "' AND pwallet.validationID like  '%" + fulltext[0] + "%' AND pwallet.reg_date between '" + dateFrom + "' AND '" + dateTo + "'  ORDER BY xID ASC ";
                }
               
            }

            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo
                {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public Adminz getTmAdminDetailsByID(string ID)
        {
            Adminz adminz = new Adminz();
            adminz.xID = "";
            adminz.xroleID = "";
            adminz.xname = "";
            adminz.xemail = "";
            adminz.xpass = "";
            adminz.xtelephone1 = "";
            adminz.xtelephone2 = "";
            adminz.xsection = "";
            adminz.xlog_officer = "";
            adminz.xreg_date = "";
            adminz.xvisible = "";

            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM xadminz_tm WHERE xID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                adminz.xID = reader["xID"].ToString();
                adminz.xroleID = reader["xroleID"].ToString();
                adminz.xname = reader["xname"].ToString();
                adminz.xemail = reader["xemail"].ToString();
                adminz.xpass = reader["xpass"].ToString();
                adminz.xtelephone1 = reader["xtelephone1"].ToString();
                adminz.xtelephone2 = reader["xtelephone2"].ToString();
                adminz.xsection = reader["xsection"].ToString();
                adminz.xlog_officer = reader["xlog_officer"].ToString();
                adminz.xreg_date = reader["xreg_date"].ToString();
                adminz.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return adminz;
        }

        public string getTmOfficeByMID(string pwalletID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT data_status from tm_office where pwalletID='" + pwalletID + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["data_status"].ToString();
            }
            reader.Close();
            return str;
        }

        public List<TmOffice> getTmOfficeDetailsByID(string ID)
        {
            List<TmOffice> list = new List<TmOffice>();
           
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM tm_office WHERE pwalletID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                TmOffice item = new TmOffice
                {
                    ID = "",
                    pwalletID = "",
                    admin_status = "",
                    data_status = "",
                    xcomment = "",
                    xdoc1 = "",
                    xdoc2 = "",
                    xdoc3 = "",
                    xofficer = "",
                    reg_date = "",
                    xvisible = ""
                };

                item.ID = reader["ID"].ToString();
                item.pwalletID = reader["pwalletID"].ToString();
                item.admin_status = reader["admin_status"].ToString();
                item.data_status = reader["data_status"].ToString();
                item.xcomment = reader["xcomment"].ToString();
                item.xdoc1 = reader["xdoc1"].ToString();
                item.xdoc2 = reader["xdoc2"].ToString();
                item.xdoc3 = reader["xdoc3"].ToString();
                item.xofficer = reader["xofficer"].ToString();
                item.reg_date = reader["reg_date"].ToString();
                item.xvisible = reader["xvisible"].ToString();
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public string getTmTypeByID(string id)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT type from tm_type where xID='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["type"].ToString();
            }
            reader.Close();
            return str;
        }

        public Adminz getTopAdminDetails()
        {
            Adminz adminz = new Adminz();
            SqlConnection connection = new SqlConnection(this.Connect());
            string cmdText = "SELECT top 1 * from xadminz_tm";
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                adminz.xID = reader["xID"].ToString();
                adminz.xroleID = reader["xroleID"].ToString();
                adminz.xname = reader["xname"].ToString();
                adminz.xemail = reader["xemail"].ToString();
                adminz.xpass = reader["xpass"].ToString();
                adminz.xtelephone1 = reader["xtelephone1"].ToString();
                adminz.xtelephone2 = reader["xtelephone2"].ToString();
                adminz.xsection = reader["xsection"].ToString();
                adminz.xlog_officer = reader["xlog_officer"].ToString();
                adminz.xreg_date = reader["xreg_date"].ToString();
                adminz.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return adminz;
        }

        public string getTotalEntries(string unit)
        {
            string str = "0";
            string cmdText = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            if (unit != "")
            {
                cmdText = "SELECT count(*) as count FROM pwallet  where status='" + unit + "'";
            }
            else
            {
                cmdText = "SELECT count(*) as count FROM pwallet ";
            }
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["count"].ToString();
            }
            reader.Close();
            return str;
        }

        public string getTotalEntriesByDate(string unit, string xfrom, string xto)
        {
            string str = "0";
            string cmdText = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            if (unit != "")
            {
                cmdText = "SELECT count(*) as count FROM pwallet  where (status='" + unit + "') AND (reg_date BETWEEN '" + xfrom + "' AND '" + xto + "') ";
            }
            else
            {
                cmdText = "SELECT count(*) as count FROM pwallet WHERE reg_date BETWEEN '" + xfrom + "' AND '" + xto + "' ";
            }
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["count"].ToString();
            }
            reader.Close();
            return str;
        }

        public string getTotalEntryCountByStage(string stage, string status)
        {
            string str = "0";
            string cmdText = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            if (status == "")
            {
                cmdText = "SELECT count(*) as count FROM pwallet  where status > '" + stage + "' ";
            }
            else
            {
                cmdText = "SELECT count(*) as count FROM pwallet  where status='" + stage + "' AND data_status='" + status + "' ";
            }
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["count"].ToString();
            }
            reader.Close();
            return str;
        }

        public string getTotalEntryCountStageByDate(string stage, string status, string xfrom, string xto)
        {
            string str = "0";
            string cmdText = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            if (status == "")
            {
                cmdText = "SELECT count(*) as count FROM pwallet  where (status >'" + stage + "')  AND (reg_date BETWEEN '" + xfrom + "' AND '" + xto + "' ) ";
            }
            else
            {
                cmdText = "SELECT count(*) as count FROM pwallet  where (status='" + stage + "') AND (data_status='" + status + "') AND (reg_date BETWEEN '" + xfrom + "' AND '" + xto + "' ) ";
            }
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["count"].ToString();
            }
            reader.Close();
            return str;
        }

        public string getValidationIDFromMarkId(string ID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("select validationID from pwallet where ID IN ( select log_staff from mark_info where xID='" + ID + "' ) ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["validationID"].ToString();
            }
            reader.Close();
            connection.Close();
            return str;
        }

        public string formatDate(string date)
        {
            if ((date == "") || (date == null))
            {
                date = DateTime.Today.Date.ToString("MM/dd/yyyy");
            }
            string str3 = "";
            string str2 = date.Substring(0, 2);
            string str = date.Substring(3, 2);
            str3 = date.Substring(6, 4);
            return (str3 + "-" + str2 + "-" + str);
        }

        public string formatSearchDate(string date)
        {
            string str3 = "";
            string str2 = "";
            string str = "";
            string xstr = "";
            if (date != "")
            {
                str3 = "";
                str2 = date.Substring(0, 2);
                str = date.Substring(3, 2);
                str3 = date.Substring(6, 4);
                xstr = str3 + "-" + str2 + "-" + str;
                }
                return (xstr);
            
        }


        public class Adminz
        {
            public string xemail;
            public string xID;
            public string xlog_officer;
            public string xname;
            public string xpass;
            public string xreg_date;
            public string xroleID;
            public string xsection;
            public string xtelephone1;
            public string xtelephone2;
            public string xvisible;
        }

        public class MarkInfo
        {
            public string auth_doc;
            public string disclaimer;
            public string log_staff;
            public string logo_descriptionID;
            public string logo_pic;
            public string national_classID;
            public string nice_class;
            public string nice_class_desc;
            public string product_title;
            public string reg_date;
            public string reg_number;
            public string sign_type;
            public string sup_doc1;
            public string sup_doc2;
            public string tm_typeID;
            public string vienna_class;
            public string xID;
            public string xvisible;
        }

        public class Pwallet
        {
            public string amt;
            public string applicantID;
            public string data_status;
            public string ID;
            public string reg_date;
            public string stage;
            public string status;
            public string validationID;
            public string visible;
        }

        public class TmOffice
        {
            public string admin_status;
            public string data_status;
            public string ID;
            public string pwalletID;
            public string reg_date;
            public string xcomment;
            public string xdoc1;
            public string xdoc2;
            public string xdoc3;
            public string xofficer;
            public string xvisible;
        }
    }
}

