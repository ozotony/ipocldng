using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Ipong.Classes
{

    public class zues
    {
        public string a_regadmin(string xname, string xrole, string xemail, string telephone1, string telephone2, string xsection, string pwalletID, string pass)
        {
            string connectionString = this.Connect();
            string str2 = DateTime.Today.Date.ToString("yyyy-MM-dd");
            string str3 = "";
            new Random();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = new SqlCommand("sp_a_TmRegAdmin", connection);
            command2.CommandType = CommandType.StoredProcedure;
            SqlCommand command = command2;
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
            SqlCommand command2 = new SqlCommand("sp_a_tm_office", connection);
            command2.CommandType = CommandType.StoredProcedure;
            SqlCommand command = command2;
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
            List<Adminz> list = new List<Adminz>();
            string str = "Xavier";
            string path = serverpath + @"\Handlers\bf.kez";
            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path, true);
                str = reader.ReadToEnd();
                reader.Close();
                if (str != null)
                {
                    string oldValue = str.Substring(0, str.IndexOf("</BitStrength>") + 14);
                    str = str.Replace(oldValue, "");
                }
            }
            this.Connect();
            string str4 = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("select xID,xemail,xpass from xadminz_tm where xvisible='1' ", connection);
            connection.Open();
            SqlDataReader reader2 = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader2.Read())
            {
                Adminz item = new Adminz();
                item.xID = reader2["xID"].ToString();
                item.xemail = reader2["xemail"].ToString();
                item.xpass = reader2["xpass"].ToString();
                list.Add(item);
            }
            reader2.Close();
            string str5 = "";
            string str6 = "";
            for (int i = 0; i < list.Count; i++)
            {
                if ((uname == str6) && (xpass == str5))
                {
                    str4 = list[i].xID.ToString();
                }
            }
            return str4;
        }

        public string addAdminLog(string adminID, string ip_addy, string remote_host, string remote_user, string server_name, string server_url)
        {
            string str = DateTime.Today.Date.ToString("yyyy-MM-dd");
            string str2 = DateTime.Now.ToLongTimeString();
            string connectionString = this.Connect();
            string str4 = "0";
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
            command.Parameters["@log_date"].Value = str;
            command.Parameters["@log_time"].Value = str2;
            str4 = command.ExecuteScalar().ToString();
            connection.Close();
            return str4;
        }

        public List<NClass> getJNationalClasses()
        {
            List<NClass> list = new List<NClass>();
            new NClass();
            SqlConnection connection = new SqlConnection(Connect2());
            string cmdText = "SELECT xID,type,description FROM national_classes";
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                NClass item = new NClass
                {
                    xID = Convert.ToInt64(reader["xID"]).ToString(),
                    xtype = reader["type"].ToString(),
                    xdescription = reader["description"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }


        public string Connect()
        {
            return ConfigurationManager.ConnectionStrings["homeConnectionString"].ConnectionString;
        }


       
        public string Connect2()
        {
            return ConfigurationManager.ConnectionStrings["tmConnectionString"].ConnectionString;
        }

        public int e_PwalletStatus(string xID, string status, string data_status)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            connection.Open();
            SqlCommand command2 = new SqlCommand("sp_u_PwalletStatus", connection);
            command2.CommandType = CommandType.StoredProcedure;
            SqlCommand command = command2;
            command.Parameters.Add(new SqlParameter("@ID", xID));
            command.Parameters.Add(new SqlParameter("@status", status));
            command.Parameters.Add(new SqlParameter("@data_status", data_status));
            SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            int num = (int)parameter.Value;
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
            SqlCommand command2 = new SqlCommand("sp_u_xadminz_pass", connection);
            command2.CommandType = CommandType.StoredProcedure;
            SqlCommand command = command2;
            command.Parameters.Add(new SqlParameter("@pwalletID", adminID));
            command.Parameters.Add(new SqlParameter("@xpass", xpass));
            SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            adminID = parameter.Value.ToString();
            connection.Close();
            return adminID;
        }

        public string formatDate(string date)
        {
            if ((date == "") || (date == null))
            {
                date = DateTime.Today.Date.ToString("MM/dd/yyyy");
            }
            string str = "";
            string str2 = date.Substring(0, 2);
            string str3 = date.Substring(3, 2);
            str = date.Substring(6, 4);
            return (str + "-" + str2 + "-" + str3);
        }

        public string formatSearchDate(string date)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            if (date != "")
            {
                str = "";
                str2 = date.Substring(0, 2);
                str3 = date.Substring(3, 2);
                str = date.Substring(6, 4);
                str4 = str + "-" + str2 + "-" + str3;
            }
            return str4;
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info2.national_classID = reader["national_classID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.nice_class = reader["nice_class"].ToString();
                info2.nice_class_desc = reader["nice_class_desc"].ToString();
                info2.sign_type = reader["sign_type"].ToString();
                info2.vienna_class = reader["vienna_class"].ToString();
                info2.disclaimer = reader["disclaimer"].ToString();
                info2.logo_pic = reader["logo_pic"].ToString();
                info2.auth_doc = reader["auth_doc"].ToString();
                info2.sup_doc1 = reader["sup_doc1"].ToString();
                info2.sup_doc2 = reader["sup_doc2"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                info2.xvisible = reader["xvisible"].ToString();
                MarkInfo item = info2;
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public long getAcceptanceMarkInfoRSCnt(string status)
        {
            long num = 0L;
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("select Count(*) AS cnt from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status<>'33' AND pwallet.status<>'22' AND CAST(pwallet.status AS INT)>=5  ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                num = Convert.ToInt64(reader["cnt"]);
            }
            reader.Close();
            return num;
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                MarkInfo item = info2;
                list.Add(item);
            }
            reader.Close();
            return list;
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
                str4 = "AND pwallet.reg_date between '" + dateFrom + "' AND '" + dateTo + "'  ORDER BY xID ASC";
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info2.national_classID = reader["national_classID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.nice_class = reader["nice_class"].ToString();
                info2.nice_class_desc = reader["nice_class_desc"].ToString();
                info2.sign_type = reader["sign_type"].ToString();
                info2.vienna_class = reader["vienna_class"].ToString();
                info2.disclaimer = reader["disclaimer"].ToString();
                info2.logo_pic = reader["logo_pic"].ToString();
                info2.auth_doc = reader["auth_doc"].ToString();
                info2.sup_doc1 = reader["sup_doc1"].ToString();
                info2.sup_doc2 = reader["sup_doc2"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                info2.xvisible = reader["xvisible"].ToString();
                MarkInfo item = info2;
                list.Add(item);
            }
            reader.Close();
            return list;
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info2.national_classID = reader["national_classID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.nice_class = reader["nice_class"].ToString();
                info2.nice_class_desc = reader["nice_class_desc"].ToString();
                info2.sign_type = reader["sign_type"].ToString();
                info2.vienna_class = reader["vienna_class"].ToString();
                info2.disclaimer = reader["disclaimer"].ToString();
                info2.logo_pic = reader["logo_pic"].ToString();
                info2.auth_doc = reader["auth_doc"].ToString();
                info2.sup_doc1 = reader["sup_doc1"].ToString();
                info2.sup_doc2 = reader["sup_doc2"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                info2.xvisible = reader["xvisible"].ToString();
                MarkInfo item = info2;
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info2.national_classID = reader["national_classID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.nice_class = reader["nice_class"].ToString();
                info2.nice_class_desc = reader["nice_class_desc"].ToString();
                info2.sign_type = reader["sign_type"].ToString();
                info2.vienna_class = reader["vienna_class"].ToString();
                info2.disclaimer = reader["disclaimer"].ToString();
                info2.logo_pic = reader["logo_pic"].ToString();
                info2.auth_doc = reader["auth_doc"].ToString();
                info2.sup_doc1 = reader["sup_doc1"].ToString();
                info2.sup_doc2 = reader["sup_doc2"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                info2.xvisible = reader["xvisible"].ToString();
                MarkInfo item = info2;
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info2.national_classID = reader["national_classID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.nice_class = reader["nice_class"].ToString();
                info2.nice_class_desc = reader["nice_class_desc"].ToString();
                info2.sign_type = reader["sign_type"].ToString();
                info2.vienna_class = reader["vienna_class"].ToString();
                info2.disclaimer = reader["disclaimer"].ToString();
                info2.logo_pic = reader["logo_pic"].ToString();
                info2.auth_doc = reader["auth_doc"].ToString();
                info2.sup_doc1 = reader["sup_doc1"].ToString();
                info2.sup_doc2 = reader["sup_doc2"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                info2.xvisible = reader["xvisible"].ToString();
                MarkInfo item = info2;
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info2.national_classID = reader["national_classID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.nice_class = reader["nice_class"].ToString();
                info2.nice_class_desc = reader["nice_class_desc"].ToString();
                info2.sign_type = reader["sign_type"].ToString();
                info2.vienna_class = reader["vienna_class"].ToString();
                info2.disclaimer = reader["disclaimer"].ToString();
                info2.logo_pic = reader["logo_pic"].ToString();
                info2.auth_doc = reader["auth_doc"].ToString();
                info2.sup_doc1 = reader["sup_doc1"].ToString();
                info2.sup_doc2 = reader["sup_doc2"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                info2.xvisible = reader["xvisible"].ToString();
                MarkInfo item = info2;
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info2.national_classID = reader["national_classID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.nice_class = reader["nice_class"].ToString();
                info2.nice_class_desc = reader["nice_class_desc"].ToString();
                info2.sign_type = reader["sign_type"].ToString();
                info2.vienna_class = reader["vienna_class"].ToString();
                info2.disclaimer = reader["disclaimer"].ToString();
                info2.logo_pic = reader["logo_pic"].ToString();
                info2.auth_doc = reader["auth_doc"].ToString();
                info2.sup_doc1 = reader["sup_doc1"].ToString();
                info2.sup_doc2 = reader["sup_doc2"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                info2.xvisible = reader["xvisible"].ToString();
                MarkInfo item = info2;
                list.Add(item);
            }
            reader.Close();
            return list;
        }
        public XObjs.Registration getRegistrationBySubagentRegistrationID(string RegistrationID)
        {
            XObjs.Registration x = new XObjs.Registration();

            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM registrations WHERE (Sys_ID='" + RegistrationID + "')  ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                x.xid = reader["xid"].ToString();
                x.AccrediationType = reader["AccrediationType"].ToString();
                x.Sys_ID = reader["Sys_ID"].ToString();
                x.Firstname = DecodeChar(reader["Firstname"].ToString());
                x.Surname = DecodeChar(reader["Surname"].ToString());
                x.Email = DecodeChar(reader["Email"].ToString());
                x.xpassword = DecodeChar(reader["xpassword"].ToString());
                x.DateOfBrith = reader["DateOfBrith"].ToString();
                x.IncorporatedDate = reader["IncorporatedDate"].ToString();
                x.Nationality = reader["Nationality"].ToString();
                x.PhoneNumber = reader["PhoneNumber"].ToString();
                x.CompanyName = DecodeChar(reader["CompanyName"].ToString());
                x.CompanyAddress = DecodeChar(reader["CompanyAddress"].ToString());
                x.ContactPerson = DecodeChar(reader["ContactPerson"].ToString());
                x.ContactPersonPhone = reader["ContactPersonPhone"].ToString();
                x.CompanyWebsite = DecodeChar(reader["CompanyWebsite"].ToString());
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
        public List<XObjs.Office_view> getNew_MarkInfoRSX3(string status, string data_status,string pvalidation, int start, int limit)
        {
            SqlCommand command;
            List<XObjs.Office_view> list = new List<XObjs.Office_view>();
            new XObjs.Office_view();
            SqlConnection connection = new SqlConnection(this.Connect2());
      
       
          //  command = new SqlCommand(string.Concat(new object[] { "select   pwallet.rtm,pwallet.ID,pwallet.TransactionId,pwallet.visible,pwallet.applicantID,pwallet.applicantID,applicant.xname,address.street ,address.telephone1,address.email1,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID    LEFT OUTER JOIN address ON address.ID=applicant.addressID   WHERE pwallet.stage='5' AND pwallet.status>='", status, "'   and pwallet.validationID ='", pvalidation, "' order by pwallet.rtm DESC     " }), connection);

            command = new SqlCommand(string.Concat(new object[] { "select   pwallet.rtm,pwallet.ID,pwallet.TransactionId,pwallet.visible,pwallet.applicantID,pwallet.applicantID,applicant.xname,address.street ,address.telephone1,address.email1,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID    LEFT OUTER JOIN address ON address.ID=applicant.addressID   WHERE pwallet.stage='5' AND pwallet.data_status in ('Certified','Accepted','Opposed','Deferred','Published','kiv','Migrated','New','Not Opposed','Registered')   and pwallet.validationID ='", pvalidation, "' order by pwallet.rtm DESC     " }), connection);
            
            //  }
            // command.CommandTimeout = 0;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            string pp2 = "";
            int vcount = 0;
            int vsn = 0;
            string voffice = "";
            while (reader.Read())
            {
                vsn = vsn + 1;
                vcount = vcount + 1;

                XObjs.Registration pdd = getRegistrationBySubagentRegistrationID(reader["applicantID"].ToString());
                //if (getTmOfficeByMID(reader["log_staff"].ToString()) != "")
                //{
                //    voffice = (getTmOfficeByMID(reader["log_staff"].ToString()));
                //}
                //else
                //{
                //    voffice = "None";
                //}
                XObjs.Office_view item = new XObjs.Office_view
                {
                    xid = reader["xID"].ToString(),
                    id = reader["ID"].ToString(),
                    rtm = reader["rtm"].ToString(),
                    applicant_name = reader["xname"].ToString(),
                    xclass = reader["class"].ToString(),
                    reg_no = reader["reg_no"].ToString(),
                    tm_type = reader["tm_type"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    oai_no = reader["oai_no"].ToString(),
                    xstat = reader["xstat"].ToString(),
                    reg_dt = reader["reg_dt"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    //   batches = reader["visible"].ToString(),
                    applicantID = reader["applicantID"].ToString(),

                    //Office = reader["data_status"].ToString(),
                    Sn = Convert.ToString(vsn),
                    Agent_Code=pdd.Sys_ID,
                    Agent_Name=pdd.Surname,
                    TransactionId = reader["TransactionId"].ToString(),
                    Xaddress = reader["street"].ToString(),
                    Xemail = reader["email1"].ToString(),
                    Xmobile = reader["telephone1"].ToString()
                };

                try
                {
                    int dw = Convert.ToInt32(reader["visible"]);
                    if (dw > 1)
                    {
                        pp2 = (Convert.ToInt32(reader["visible"]) - 1).ToString();

                    }

                    else
                    {
                        pp2 = (Convert.ToInt32(reader["visible"])).ToString();

                    }
                }
                catch (Exception ee)
                {

                }

                item.batches = pp2;
                list.Add(item);


            }
            reader.Close();
            connection.Close();
            return list;
        }

        public List<XObjs.Office_view> getNew_MarkInfoRSX33(string status, string data_status, string pvalidation, int start, int limit)
        {
            SqlCommand command;
            List<XObjs.Office_view> list = new List<XObjs.Office_view>();
            new XObjs.Office_view();
            SqlConnection connection = new SqlConnection(this.Connect2());


            //  command = new SqlCommand(string.Concat(new object[] { "select   pwallet.rtm,pwallet.ID,pwallet.TransactionId,pwallet.visible,pwallet.applicantID,pwallet.applicantID,applicant.xname,address.street ,address.telephone1,address.email1,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID    LEFT OUTER JOIN address ON address.ID=applicant.addressID   WHERE pwallet.stage='5' AND pwallet.status>='", status, "'   and pwallet.validationID ='", pvalidation, "' order by pwallet.rtm DESC     " }), connection);

            command = new SqlCommand(string.Concat(new object[] { "select    pwallet.rtm,pwallet.ID,pwallet.TransactionId,pwallet.visible,pwallet.applicantID,pwallet.applicantID,applicant.xname,address.street ,address.telephone1,address.email1,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID    LEFT OUTER JOIN address ON address.ID=applicant.addressID   WHERE pwallet.stage='5' AND pwallet.data_status  ='", data_status, "'   and mark_info.reg_number ='", pvalidation, "'    " }), connection);

            //  }
            // command.CommandTimeout = 0;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            string pp2 = "";
            int vcount = 0;
            int vsn = 0;
            string voffice = "";
            while (reader.Read())
            {
                vsn = vsn + 1;
                vcount = vcount + 1;

                XObjs.Registration pdd = getRegistrationBySubagentRegistrationID(reader["applicantID"].ToString());
                //if (getTmOfficeByMID(reader["log_staff"].ToString()) != "")
                //{
                //    voffice = (getTmOfficeByMID(reader["log_staff"].ToString()));
                //}
                //else
                //{
                //    voffice = "None";
                //}
                XObjs.Office_view item = new XObjs.Office_view
                {
                    xid = reader["xID"].ToString(),
                    id = reader["ID"].ToString(),
                    rtm = reader["rtm"].ToString(),
                    applicant_name = reader["xname"].ToString(),
                    xclass = reader["class"].ToString(),
                    reg_no = reader["reg_no"].ToString(),
                    tm_type = reader["tm_type"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    oai_no = reader["oai_no"].ToString(),
                    xstat = reader["xstat"].ToString(),
                    reg_dt = reader["reg_dt"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    //   batches = reader["visible"].ToString(),
                    applicantID = reader["applicantID"].ToString(),

                    //Office = reader["data_status"].ToString(),
                    Sn = Convert.ToString(vsn),
                    Agent_Code = pdd.Sys_ID,
                    Agent_Name = pdd.Surname,
                    TransactionId = reader["TransactionId"].ToString(),
                    Xaddress = reader["street"].ToString(),
                    Xemail = reader["email1"].ToString(),
                    Xmobile = reader["telephone1"].ToString()
                };

                try
                {
                    int dw = Convert.ToInt32(reader["visible"]);
                    if (dw > 1)
                    {
                        pp2 = (Convert.ToInt32(reader["visible"]) - 1).ToString();

                    }

                    else
                    {
                        pp2 = (Convert.ToInt32(reader["visible"])).ToString();

                    }
                }
                catch (Exception ee)
                {

                }

                item.batches = pp2;
                list.Add(item);


            }
            reader.Close();
            connection.Close();
            return list;
        }

        public List<XObjs.Office_view> getNew_MarkInfoRSX6(string status, string data_status, string pvalidation, int start, int limit)
        {
            SqlCommand command;
            List<XObjs.Office_view> list = new List<XObjs.Office_view>();
            new XObjs.Office_view();
            SqlConnection connection = new SqlConnection(this.Connect2());

            //   command = new SqlCommand(string.Concat(new object[] { "select   pwallet.rtm,pwallet.ID,pwallet.TransactionId,pwallet.visible,pwallet.applicantID,pwallet.applicantID,applicant.xname,address_service.street ,address_service.telephone1,address_service.email1,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID    LEFT OUTER JOIN address_service ON address_service.log_staff=pwallet.ID   WHERE pwallet.stage='5' AND pwallet.status>='", status, "' AND pwallet.data_status='", data_status, "'  and pwallet.rtm ='", pvalidation, "' order by pwallet.rtm DESC     " }), connection);

           // command = new SqlCommand(string.Concat(new object[] { "select   pwallet.rtm,pwallet.ID,pwallet.TransactionId,pwallet.visible,pwallet.applicantID,pwallet.applicantID,applicant.xname,address.street ,address.telephone1,address.email1,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID    LEFT OUTER JOIN address ON address.ID=applicant.addressID   WHERE pwallet.stage='5' AND pwallet.data_status in ('Certified','Accepted','Opposed','Deferred','Published','kiv','Migrated','New','Registered','Not Opposed', 'Search 2 Conducted', 'Search Conducted')   and pwallet.rtm ='", pvalidation, "' order by pwallet.rtm DESC     " }), connection);

            command = new SqlCommand(string.Concat(new object[] { "select   pwallet.rtm,pwallet.ID,pwallet.TransactionId,pwallet.visible,pwallet.applicantID,pwallet.applicantID,applicant.xname,address.street ,address.telephone1,address.email1,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID    LEFT OUTER JOIN address ON address.ID=applicant.addressID   WHERE pwallet.stage='5'   and pwallet.rtm ='", pvalidation, "' order by pwallet.rtm DESC     " }), connection);
            //  }
            // command.CommandTimeout = 0;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            string pp2 = "";
            int vcount = 0;
            int vsn = 0;
            string voffice = "";
            while (reader.Read())
            {
                vsn = vsn + 1;
                vcount = vcount + 1;

                XObjs.Registration pdd = getRegistrationBySubagentRegistrationID(reader["applicantID"].ToString());
                //if (getTmOfficeByMID(reader["log_staff"].ToString()) != "")
                //{
                //    voffice = (getTmOfficeByMID(reader["log_staff"].ToString()));
                //}
                //else
                //{
                //    voffice = "None";
                //}
                XObjs.Office_view item = new XObjs.Office_view
                {
                    xid = reader["xID"].ToString(),
                    id = reader["ID"].ToString(),
                    rtm = reader["rtm"].ToString(),
                    applicant_name = reader["xname"].ToString(),
                    xclass = reader["class"].ToString(),
                    reg_no = reader["reg_no"].ToString(),
                    tm_type = reader["tm_type"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    oai_no = reader["oai_no"].ToString(),
                    xstat = reader["xstat"].ToString(),
                    reg_dt = reader["reg_dt"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    //   batches = reader["visible"].ToString(),
                    applicantID = reader["applicantID"].ToString(),

                    //Office = reader["data_status"].ToString(),
                    Sn = Convert.ToString(vsn),
                    Agent_Code = pdd.Sys_ID,
                    Agent_Name = pdd.Surname,
                    TransactionId = reader["TransactionId"].ToString(),
                    Xaddress = reader["street"].ToString(),
                    Xemail = reader["email1"].ToString(),
                    Xmobile = reader["telephone1"].ToString()
                };

                try
                {
                    int dw = Convert.ToInt32(reader["visible"]);
                    if (dw > 1)
                    {
                        pp2 = (Convert.ToInt32(reader["visible"]) - 1).ToString();

                    }

                    else
                    {
                        pp2 = (Convert.ToInt32(reader["visible"])).ToString();

                    }
                }
                catch (Exception ee)
                {

                }

                item.batches = pp2;
                list.Add(item);


            }
            reader.Close();
            connection.Close();
            return list;
        }

        public List<XObjs.Office_view> getNew_MarkInfoRSX7(string status, string data_status, string pvalidation, int start, int limit)
        {
            SqlCommand command;
            List<XObjs.Office_view> list = new List<XObjs.Office_view>();
            new XObjs.Office_view();
            SqlConnection connection = new SqlConnection(this.Connect2());

            //  command = new SqlCommand(string.Concat(new object[] { "select   pwallet.rtm,pwallet.ID,pwallet.TransactionId,pwallet.visible,pwallet.applicantID,pwallet.applicantID,applicant.xname,address_service.street ,address_service.telephone1,address_service.email1,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID LEFT OUTER JOIN address_service ON address_service.log_staff=pwallet.ID    WHERE pwallet.stage='5' AND pwallet.status>='", status, "' AND pwallet.data_status='", data_status, "'  and mark_info.reg_number ='", pvalidation, "' order by pwallet.rtm DESC     " }), connection);

            command = new SqlCommand(string.Concat(new object[] { "select   pwallet.rtm,pwallet.ID,pwallet.TransactionId,pwallet.visible,pwallet.applicantID,pwallet.applicantID,applicant.xname,address.street ,address.telephone1,address.email1,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID LEFT OUTER JOIN address ON address.ID=applicant.addressID    WHERE pwallet.stage='5'   and mark_info.reg_number ='", pvalidation, "'    " }), connection);
            //  }
            // command.CommandTimeout = 0;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            string pp2 = "";
            int vcount = 0;
            int vsn = 0;
            string voffice = "";
            while (reader.Read())
            {
                vsn = vsn + 1;
                vcount = vcount + 1;

                XObjs.Registration pdd = getRegistrationBySubagentRegistrationID(reader["applicantID"].ToString());
                //if (getTmOfficeByMID(reader["log_staff"].ToString()) != "")
                //{
                //    voffice = (getTmOfficeByMID(reader["log_staff"].ToString()));
                //}
                //else
                //{
                //    voffice = "None";
                //}
                XObjs.Office_view item = new XObjs.Office_view
                {
                    xid = reader["xID"].ToString(),
                    id = reader["ID"].ToString(),
                    rtm = reader["rtm"].ToString(),
                    applicant_name = reader["xname"].ToString(),
                    xclass = reader["class"].ToString(),
                    reg_no = reader["reg_no"].ToString(),
                    tm_type = reader["tm_type"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    oai_no = reader["oai_no"].ToString(),
                    xstat = reader["xstat"].ToString(),
                    reg_dt = reader["reg_dt"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    //   batches = reader["visible"].ToString(),
                    applicantID = reader["applicantID"].ToString(),

                    //Office = reader["data_status"].ToString(),
                    Sn = Convert.ToString(vsn),
                    Agent_Code = pdd.Sys_ID,
                    Agent_Name = pdd.Surname,
                    TransactionId = reader["TransactionId"].ToString(),
                    Xaddress = reader["street"].ToString(),
                    Xemail = reader["email1"].ToString(),
                    Xmobile = reader["telephone1"].ToString()
                };

                try
                {
                    int dw = Convert.ToInt32(reader["visible"]);
                    if (dw > 1)
                    {
                        pp2 = (Convert.ToInt32(reader["visible"]) - 1).ToString();

                    }

                    else
                    {
                        pp2 = (Convert.ToInt32(reader["visible"])).ToString();

                    }
                }
                catch (Exception ee)
                {

                }

                item.batches = pp2;
                list.Add(item);


            }
            reader.Close();
            connection.Close();
            return list;
        }


        public XObjs.Office_view getNew_MarkInfoRSX8(string status,string pvalidation, int start, int limit)
        {
            SqlCommand command;
            List<XObjs.Office_view> list = new List<XObjs.Office_view>();
            new XObjs.Office_view();
            SqlConnection connection = new SqlConnection(this.Connect2());

            XObjs.Office_view item2 = new XObjs.Office_view() ;

            //  command = new SqlCommand(string.Concat(new object[] { "select   pwallet.rtm,pwallet.ID,pwallet.TransactionId,pwallet.visible,pwallet.applicantID,pwallet.applicantID,applicant.xname,address_service.street ,address_service.telephone1,address_service.email1,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID LEFT OUTER JOIN address_service ON address_service.log_staff=pwallet.ID    WHERE pwallet.stage='5' AND pwallet.status>='", status, "' AND pwallet.data_status='", data_status, "'  and mark_info.reg_number ='", pvalidation, "' order by pwallet.rtm DESC     " }), connection);

            command = new SqlCommand(string.Concat(new object[] { "select   pwallet.rtm,pwallet.ID,pwallet.TransactionId,pwallet.visible,pwallet.applicantID,pwallet.applicantID,applicant.xname,address.street ,address.telephone1,address.email1,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff ,mark_info.logo_pic ,mark_info.auth_doc ,mark_info.sup_doc1 ,mark_info.sup_doc2 from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID LEFT OUTER JOIN address ON address.ID=applicant.addressID    WHERE pwallet.stage='5'   and mark_info.reg_number ='", pvalidation, "'  and pwallet.applicantid ='", status, "'    " }), connection);
            //  }
            // command.CommandTimeout = 0;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            string pp2 = "";
            int vcount = 0;
            int vsn = 0;
            string voffice = "";
            while (reader.Read())
            {
                vsn = vsn + 1;
                vcount = vcount + 1;

                XObjs.Registration pdd = getRegistrationBySubagentRegistrationID(reader["applicantID"].ToString());
                //if (getTmOfficeByMID(reader["log_staff"].ToString()) != "")
                //{
                //    voffice = (getTmOfficeByMID(reader["log_staff"].ToString()));
                //}
                //else
                //{
                //    voffice = "None";
                //}
                XObjs.Office_view item = new XObjs.Office_view
                {
                    xid = reader["xID"].ToString(),
                    id = reader["ID"].ToString(),
                    rtm = reader["rtm"].ToString(),
                    applicant_name = reader["xname"].ToString(),
                    xclass = reader["class"].ToString(),
                    reg_no = reader["reg_no"].ToString(),
                    tm_type = reader["tm_type"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    oai_no = reader["oai_no"].ToString(),
                    xstat = reader["xstat"].ToString(),
                    reg_dt = reader["reg_dt"].ToString(),
                    log_staff = reader["log_staff"].ToString(),

                    logo_pic = reader["logo_pic"].ToString(),

                    auth_doc = reader["auth_doc"].ToString(),

                    sup_doc1= reader["sup_doc1"].ToString(),

                    sup_doc2 = reader["sup_doc2"].ToString(),
                    //   batches = reader["visible"].ToString(),
                    applicantID = reader["applicantID"].ToString(),

                    //Office = reader["data_status"].ToString(),
                    Sn = Convert.ToString(vsn),
                    Agent_Code = pdd.Sys_ID,
                    Agent_Name = pdd.Surname,
                    TransactionId = reader["TransactionId"].ToString(),
                    Xaddress = reader["street"].ToString(),
                    Xemail = reader["email1"].ToString(),
                    Xmobile = reader["telephone1"].ToString()
                };

                try
                {
                    int dw = Convert.ToInt32(reader["visible"]);
                    if (dw > 1)
                    {
                        pp2 = (Convert.ToInt32(reader["visible"]) - 1).ToString();

                    }

                    else
                    {
                        pp2 = (Convert.ToInt32(reader["visible"])).ToString();

                    }
                }
                catch (Exception ee)
                {

                }

                item.batches = pp2;

                item2 = item;
               // list.Add(item);


            }
            reader.Close();
            connection.Close();
            return item2;
        }


        public XObjs.Office_view getNew_MarkInfoRSX9(string status, string pvalidation, int start, int limit)
        {
            SqlCommand command;
            List<XObjs.Office_view> list = new List<XObjs.Office_view>();
            new XObjs.Office_view();
            SqlConnection connection = new SqlConnection(this.Connect2());

            XObjs.Office_view item2 = new XObjs.Office_view();

            //  command = new SqlCommand(string.Concat(new object[] { "select   pwallet.rtm,pwallet.ID,pwallet.TransactionId,pwallet.visible,pwallet.applicantID,pwallet.applicantID,applicant.xname,address_service.street ,address_service.telephone1,address_service.email1,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID LEFT OUTER JOIN address_service ON address_service.log_staff=pwallet.ID    WHERE pwallet.stage='5' AND pwallet.status>='", status, "' AND pwallet.data_status='", data_status, "'  and mark_info.reg_number ='", pvalidation, "' order by pwallet.rtm DESC     " }), connection);

            command = new SqlCommand(string.Concat(new object[] { "select   pwallet.rtm,pwallet.ID,pwallet.TransactionId,pwallet.visible,pwallet.applicantID,pwallet.applicantID,applicant.xname,address.street ,address.telephone1,address.email1,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff ,mark_info.logo_pic ,mark_info.auth_doc ,mark_info.sup_doc1 ,mark_info.sup_doc2 from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID LEFT OUTER JOIN address ON address.ID=applicant.addressID    WHERE pwallet.stage='5'   and pwallet.validationid ='", pvalidation, "'  and pwallet.applicantid ='", status, "'    " }), connection);
            //  }
            // command.CommandTimeout = 0;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            string pp2 = "";
            int vcount = 0;
            int vsn = 0;
            string voffice = "";
            while (reader.Read())
            {
                vsn = vsn + 1;
                vcount = vcount + 1;

                XObjs.Registration pdd = getRegistrationBySubagentRegistrationID(reader["applicantID"].ToString());
                //if (getTmOfficeByMID(reader["log_staff"].ToString()) != "")
                //{
                //    voffice = (getTmOfficeByMID(reader["log_staff"].ToString()));
                //}
                //else
                //{
                //    voffice = "None";
                //}
                XObjs.Office_view item = new XObjs.Office_view
                {
                    xid = reader["xID"].ToString(),
                    id = reader["ID"].ToString(),
                    rtm = reader["rtm"].ToString(),
                    applicant_name = reader["xname"].ToString(),
                    xclass = reader["class"].ToString(),
                    reg_no = reader["reg_no"].ToString(),
                    tm_type = reader["tm_type"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    oai_no = reader["oai_no"].ToString(),
                    xstat = reader["xstat"].ToString(),
                    reg_dt = reader["reg_dt"].ToString(),
                    log_staff = reader["log_staff"].ToString(),

                    logo_pic = reader["logo_pic"].ToString(),

                    auth_doc = reader["auth_doc"].ToString(),

                    sup_doc1 = reader["sup_doc1"].ToString(),

                    sup_doc2 = reader["sup_doc2"].ToString(),
                    //   batches = reader["visible"].ToString(),
                    applicantID = reader["applicantID"].ToString(),

                    //Office = reader["data_status"].ToString(),
                    Sn = Convert.ToString(vsn),
                    Agent_Code = pdd.Sys_ID,
                    Agent_Name = pdd.Surname,
                    TransactionId = reader["TransactionId"].ToString(),
                    Xaddress = reader["street"].ToString(),
                    Xemail = reader["email1"].ToString(),
                    Xmobile = reader["telephone1"].ToString()
                };

                try
                {
                    int dw = Convert.ToInt32(reader["visible"]);
                    if (dw > 1)
                    {
                        pp2 = (Convert.ToInt32(reader["visible"]) - 1).ToString();

                    }

                    else
                    {
                        pp2 = (Convert.ToInt32(reader["visible"])).ToString();

                    }
                }
                catch (Exception ee)
                {

                }

                item.batches = pp2;

                item2 = item;
                // list.Add(item);


            }
            reader.Close();
            connection.Close();
            return item2;
        }



        public List<XObjs.Office_view> getNew_MarkInfoRSX4(string pvalidation)
        {
            SqlCommand command;
            List<XObjs.Office_view> list = new List<XObjs.Office_view>();
            new XObjs.Office_view();
            SqlConnection connection = new SqlConnection(this.Connect2());
     
            command = new SqlCommand(string.Concat(new object[] { "select g_tm_info.tm_title 'tm_title',g_pwallet.ID,g_tm_info.tm_class 'xclass',g_applicant_info.xname,g_applicant_info.address,g_applicant_info.xemail,g_applicant_info.xmobile, g_pwallet.log_officer,g_pwallet.validationID ,g_pwallet.applicantID,g_pwallet.TransactionId,g_tm_info.reg_number,g_tm_info.xID,g_app_info.rtm_number,g_app_info.application_no,g_app_info.item_code,g_app_info.filing_date,g_app_info.reg_no, g_app_info.reg_date,g_app_info.log_staff,g_app_info.visible,g_applicant_info.xname from g_app_info   LEFT OUTER JOIN g_tm_info ON g_app_info.log_staff=g_tm_info.log_staff   LEFT OUTER JOIN g_pwallet ON g_app_info.log_staff=g_pwallet.ID LEFT OUTER JOIN g_applicant_info on  g_pwallet.id=g_applicant_info.log_staff  WHERE  g_app_info.rtm_number ='", pvalidation, "'   " }), connection); ; 
            // g_app_info.reg_date,g_app_info.log_staff,g_app_info.visible from g_app_info   LEFT OUTER JOIN g_tm_info ON g_app_info.log_staff=g_tm_info.log_staff 
           
            //  }
 
            // command.CommandTimeout = 0;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            string pp2 = "";
            int vcount = 0;
            int vsn = 0;
            string voffice = "";
            while (reader.Read())
            {
                vsn = vsn + 1;
                vcount = vcount + 1;

                XObjs.Registration pdd = getRegistrationBySubagentRegistrationID(reader["applicantID"].ToString());
                //if (getTmOfficeByMID(reader["log_staff"].ToString()) != "")
                //{
                //    voffice = (getTmOfficeByMID(reader["log_staff"].ToString()));
                //}
                //else
                //{
                //    voffice = "None";
                //}
                XObjs.Office_view item = new XObjs.Office_view
                {
                    xid = reader["xID"].ToString(),
                    rtm = reader["rtm_number"].ToString(),
                    applicant_name = reader["xname"].ToString(),
                    Xaddress = reader["address"].ToString(),
                    Xemail = reader["xemail"].ToString(),
                    Xmobile = reader["xmobile"].ToString(),
                    xclass = reader["xclass"].ToString(),
                    reg_no = reader["application_no"].ToString(),
                   // tm_type = reader["tm_type"].ToString(),
                    product_title = reader["tm_title"].ToString(),
                    oai_no = reader["validationID"].ToString(),
                  //  xstat = reader["xstat"].ToString(),
                    reg_dt = reader["reg_date"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    //   batches = reader["visible"].ToString(),
                    applicantID = reader["applicantID"].ToString(),

                    //Office = reader["data_status"].ToString(),
                    Sn = Convert.ToString(vsn),
                    Agent_Code = pdd.Sys_ID,
                    Agent_Name = pdd.Surname,
                   TransactionId = reader["TransactionId"].ToString(),
                    id = reader["ID"].ToString()
                };

                try
                {
                    int dw = Convert.ToInt32(reader["visible"]);
                    if (dw > 1)
                    {
                        pp2 = (Convert.ToInt32(reader["visible"]) - 1).ToString();

                    }

                    else
                    {
                        pp2 = (Convert.ToInt32(reader["visible"])).ToString();

                    }
                }
                catch (Exception ee)
                {

                }

                item.batches = pp2;
                list.Add(item);


            }
            reader.Close();
            connection.Close();
            return list;
        }

        public List<XObjs.Office_view> getNew_MarkInfoRSX5(string pvalidation)
        {
            SqlCommand command;
            List<XObjs.Office_view> list = new List<XObjs.Office_view>();
            new XObjs.Office_view();
            SqlConnection connection = new SqlConnection(this.Connect2());
            //if ((status == "4") && (data_status == "Refused"))
            //{
            //    command = new SqlCommand(string.Concat(new object[] { "WITH RSTbl AS (select  pwallet.rtm,pwallet.visible,pwallet.applicantID,applicant.xname,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff, ROW_NUMBER() OVER (ORDER BY mark_info.xID) AS 'RowRank'  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID    WHERE pwallet.stage='5' AND pwallet.status='", status, "' AND pwallet.data_status='", data_status, "')SELECT * FROM RSTbl  WHERE RowRank BETWEEN '", start, "' AND '", limit, "' " }), connection);
            //}
            //else if ((status == "4") && ((data_status == "Registrable") || (data_status == "Non-registrable")))
            //{
            //    command = new SqlCommand(string.Concat(new object[] { "WITH RSTbl AS (select   pwallet.rtm,pwallet.visible,pwallet.applicantID,applicant.xname,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff, ROW_NUMBER() OVER (ORDER BY mark_info.xID) AS 'RowRank'  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID    WHERE pwallet.stage='5' AND pwallet.status='", status, "' AND ((pwallet.data_status='Non-registrable') OR (pwallet.data_status='Registrable'))  )SELECT * FROM RSTbl  WHERE RowRank BETWEEN '", start, "' AND '", limit, "' " }), connection);
            //}
            //else if ((status == "1") && ((data_status == "Fresh") || (data_status == "Invalid")))
            //{
            //    command = new SqlCommand(string.Concat(new object[] { "WITH RSTbl AS (select   pwallet.rtm,pwallet.visible,pwallet.applicantID,applicant.xname,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff, ROW_NUMBER() OVER (ORDER BY mark_info.xID) AS 'RowRank'  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID     WHERE pwallet.stage='5' AND pwallet.status='", status, "' AND (pwallet.data_status='Fresh' OR pwallet.data_status='Invalid') )SELECT * FROM RSTbl  WHERE RowRank BETWEEN '", start, "' AND '", limit, "' " }), connection);
            //}
            //else if ((status == "8") && (data_status == "Registered"))
            //{
            //    command = new SqlCommand(string.Concat(new object[] { "WITH RSTbl AS (select   pwallet.rtm,pwallet.visible,pwallet.applicantID,applicant.xname,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff, ROW_NUMBER() OVER (ORDER BY mark_info.xID) AS 'RowRank'  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID     WHERE pwallet.stage='5' AND pwallet.status>'", status, "' AND pwallet.data_status='", data_status, "' )SELECT * FROM RSTbl  WHERE RowRank BETWEEN '", start, "' AND '", limit, "' " }), connection);
            //}
            //else if ((status == "5") && (data_status == "acc_printed"))
            //{
            //    command = new SqlCommand(string.Concat(new object[] { "WITH RSTbl AS (select   pwallet.rtm,pwallet.visible,pwallet.applicantID,applicant.xname,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff, ROW_NUMBER() OVER (ORDER BY mark_info.xID) AS 'RowRank'  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID     WHERE pwallet.stage='5' AND pwallet.acc_p='1' )SELECT * FROM RSTbl  WHERE RowRank BETWEEN '", start, "' AND '", limit, "' " }), connection);
            //}
            //else
            //{

            //command = new SqlCommand(string.Concat(new object[] { "select   pwallet.rtm,pwallet.TransactionId,pwallet.visible,pwallet.applicantID,pwallet.applicantID,applicant.xname,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID     WHERE pwallet.stage='5' AND pwallet.status>='", status, "' AND pwallet.data_status='", data_status, "'  and pwallet.validationID ='", pvalidation, "' order by pwallet.rtm DESC     " }), connection);
            command = new SqlCommand(string.Concat(new object[] { "select g_tm_info.tm_title 'tm_title',g_pwallet.ID,g_tm_info.tm_class 'xclass',g_applicant_info.xname,g_applicant_info.address,g_applicant_info.xemail,g_applicant_info.xmobile, g_pwallet.log_officer,g_pwallet.validationID ,g_pwallet.applicantID,g_pwallet.TransactionId,g_tm_info.reg_number,g_tm_info.xID,g_app_info.rtm_number,g_app_info.application_no,g_app_info.item_code,g_app_info.filing_date,g_app_info.reg_no, g_app_info.reg_date,g_app_info.log_staff,g_app_info.visible,g_applicant_info.xname from g_app_info   LEFT OUTER JOIN g_tm_info ON g_app_info.log_staff=g_tm_info.log_staff   LEFT OUTER JOIN g_pwallet ON g_app_info.log_staff=g_pwallet.ID LEFT OUTER JOIN g_applicant_info on  g_pwallet.id=g_applicant_info.log_staff  WHERE  g_app_info.application_no ='", pvalidation, "'   " }), connection); ;
            // g_app_info.reg_date,g_app_info.log_staff,g_app_info.visible from g_app_info   LEFT OUTER JOIN g_tm_info ON g_app_info.log_staff=g_tm_info.log_staff 

            //  }

            // command.CommandTimeout = 0;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            string pp2 = "";
            int vcount = 0;
            int vsn = 0;
            string voffice = "";
            while (reader.Read())
            {
                vsn = vsn + 1;
                vcount = vcount + 1;

                XObjs.Registration pdd = getRegistrationBySubagentRegistrationID(reader["applicantID"].ToString());
                //if (getTmOfficeByMID(reader["log_staff"].ToString()) != "")
                //{
                //    voffice = (getTmOfficeByMID(reader["log_staff"].ToString()));
                //}
                //else
                //{
                //    voffice = "None";
                //}
                XObjs.Office_view item = new XObjs.Office_view
                {
                    xid = reader["xID"].ToString(),
                    rtm = reader["rtm_number"].ToString(),
                    applicant_name = reader["xname"].ToString(),
                    xclass = reader["xclass"].ToString(),
                    reg_no = reader["application_no"].ToString(),
                    Xaddress = reader["address"].ToString(),
                    Xemail = reader["xemail"].ToString(),
                    Xmobile = reader["xmobile"].ToString(),
                    // tm_type = reader["tm_type"].ToString(),
                    product_title = reader["tm_title"].ToString(),
                    oai_no = reader["validationID"].ToString(),
                    //  xstat = reader["xstat"].ToString(),
                    reg_dt = reader["reg_date"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    //   batches = reader["visible"].ToString(),
                    applicantID = reader["applicantID"].ToString(),

                    //Office = reader["data_status"].ToString(),
                    Sn = Convert.ToString(vsn),
                    Agent_Code = pdd.Sys_ID,
                    Agent_Name = pdd.Surname,
                    TransactionId = reader["TransactionId"].ToString(),
                    id = reader["ID"].ToString()
                };

                try
                {
                    int dw = Convert.ToInt32(reader["visible"]);
                    if (dw > 1)
                    {
                        pp2 = (Convert.ToInt32(reader["visible"]) - 1).ToString();

                    }

                    else
                    {
                        pp2 = (Convert.ToInt32(reader["visible"])).ToString();

                    }
                }
                catch (Exception ee)
                {

                }

                item.batches = pp2;
                list.Add(item);


            }
            reader.Close();
            connection.Close();
            return list;
        }

        public string DecodeChar(string x)
        {
            string y = x;
            if ((x != null) || (x != ""))
            {
                if (x.Contains("|"))
                {
                    y = x.Replace("|", "'"); x = y;
                }
                if (x.Contains("~~"))
                {
                    y = x.Replace("~~", "("); x = y;
                }

                if (x.Contains("**"))
                {
                    y = x.Replace("**", ")"); x = y;
                }
                if (x.Contains("%26"))
                {
                    y = x.Replace("%26", "&"); x = y;
                }
            }
            return y;
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info2.national_classID = reader["national_classID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.nice_class = reader["nice_class"].ToString();
                info2.nice_class_desc = reader["nice_class_desc"].ToString();
                info2.sign_type = reader["sign_type"].ToString();
                info2.vienna_class = reader["vienna_class"].ToString();
                info2.disclaimer = reader["disclaimer"].ToString();
                info2.logo_pic = reader["logo_pic"].ToString();
                info2.auth_doc = reader["auth_doc"].ToString();
                info2.sup_doc1 = reader["sup_doc1"].ToString();
                info2.sup_doc2 = reader["sup_doc2"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                info2.xvisible = reader["xvisible"].ToString();
                MarkInfo item = info2;
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
                TmOffice office2 = new TmOffice();
                office2.ID = reader["ID"].ToString();
                office2.pwalletID = reader["pwalletID"].ToString();
                office2.admin_status = reader["admin_status"].ToString();
                office2.data_status = reader["data_status"].ToString();
                office2.xcomment = reader["xcomment"].ToString();
                office2.xdoc1 = reader["xdoc1"].ToString();
                office2.xdoc2 = reader["xdoc2"].ToString();
                office2.xdoc3 = reader["xdoc3"].ToString();
                office2.xofficer = reader["xofficer"].ToString();
                office2.reg_date = reader["reg_date"].ToString();
                office2.xvisible = reader["xvisible"].ToString();
                TmOffice item = office2;
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info2.national_classID = reader["national_classID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.nice_class = reader["nice_class"].ToString();
                info2.nice_class_desc = reader["nice_class_desc"].ToString();
                info2.sign_type = reader["sign_type"].ToString();
                info2.vienna_class = reader["vienna_class"].ToString();
                info2.disclaimer = reader["disclaimer"].ToString();
                info2.logo_pic = reader["logo_pic"].ToString();
                info2.auth_doc = reader["auth_doc"].ToString();
                info2.sup_doc1 = reader["sup_doc1"].ToString();
                info2.sup_doc2 = reader["sup_doc2"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                info2.xvisible = reader["xvisible"].ToString();
                MarkInfo item = info2;
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info2.national_classID = reader["national_classID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.nice_class = reader["nice_class"].ToString();
                info2.nice_class_desc = reader["nice_class_desc"].ToString();
                info2.sign_type = reader["sign_type"].ToString();
                info2.vienna_class = reader["vienna_class"].ToString();
                info2.disclaimer = reader["disclaimer"].ToString();
                info2.logo_pic = reader["logo_pic"].ToString();
                info2.auth_doc = reader["auth_doc"].ToString();
                info2.sup_doc1 = reader["sup_doc1"].ToString();
                info2.sup_doc2 = reader["sup_doc2"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                info2.xvisible = reader["xvisible"].ToString();
                MarkInfo item = info2;
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public long getMarkInfoRSCnt(string status, string data_status)
        {
            long num = 0L;
            SqlConnection connection = new SqlConnection(this.Connect());
            if ((status == "4") && ((data_status == "Refused") || (data_status == "Non-registrable")))
            {
                SqlCommand command = new SqlCommand("select Count(*) AS cnt from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status='" + status + "' AND ((pwallet.data_status='Non-registrable') OR (pwallet.data_status='Refused')) ", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    num = Convert.ToInt64(reader["cnt"]);
                }
                reader.Close();
                return num;
            }
            SqlCommand command2 = new SqlCommand("select Count(*) AS cnt from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status='" + status + "' AND pwallet.data_status='" + data_status + "' ", connection);
            connection.Open();
            SqlDataReader reader2 = command2.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader2.Read())
            {
                num = Convert.ToInt64(reader2["cnt"]);
            }
            reader2.Close();
            return num;
        }

        public List<MarkInfo> getMarkInfoRSX(string status, string data_status, string start, string limit)
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
                    MarkInfo info2 = new MarkInfo();
                    info2.xID = reader["xID"].ToString();
                    info2.reg_number = reader["reg_number"].ToString();
                    info2.tm_typeID = reader["tm_typeID"].ToString();
                    info2.product_title = reader["product_title"].ToString();
                    info2.log_staff = reader["log_staff"].ToString();
                    info2.reg_date = reader["reg_date"].ToString();
                    MarkInfo item = info2;
                    list.Add(item);
                }
                reader.Close();
                return list;
            }
            SqlCommand command2 = new SqlCommand("WITH RSTbl AS (select mark_info.xID,mark_info.reg_number,mark_info.product_title,mark_info.tm_typeID,mark_info.reg_date,mark_info.log_staff, ROW_NUMBER() OVER (ORDER BY mark_info.xID) AS 'RowRank' from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status='" + status + "' AND pwallet.data_status='" + data_status + "' )SELECT * FROM RSTbl  WHERE RowRank BETWEEN '" + start + "' AND '" + limit + "' ", connection);
            connection.Open();
            SqlDataReader reader2 = command2.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader2.Read())
            {
                MarkInfo info4 = new MarkInfo();
                info4.xID = reader2["xID"].ToString();
                info4.reg_number = reader2["reg_number"].ToString();
                info4.tm_typeID = reader2["tm_typeID"].ToString();
                info4.product_title = reader2["product_title"].ToString();
                info4.log_staff = reader2["log_staff"].ToString();
                info4.reg_date = reader2["reg_date"].ToString();
                MarkInfo info3 = info4;
                list.Add(info3);
            }
            reader2.Close();
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info2.national_classID = reader["national_classID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.nice_class = reader["nice_class"].ToString();
                info2.nice_class_desc = reader["nice_class_desc"].ToString();
                info2.sign_type = reader["sign_type"].ToString();
                info2.vienna_class = reader["vienna_class"].ToString();
                info2.disclaimer = reader["disclaimer"].ToString();
                info2.logo_pic = reader["logo_pic"].ToString();
                info2.auth_doc = reader["auth_doc"].ToString();
                info2.sup_doc1 = reader["sup_doc1"].ToString();
                info2.sup_doc2 = reader["sup_doc2"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                info2.xvisible = reader["xvisible"].ToString();
                MarkInfo item = info2;
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info2.national_classID = reader["national_classID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.nice_class = reader["nice_class"].ToString();
                info2.nice_class_desc = reader["nice_class_desc"].ToString();
                info2.sign_type = reader["sign_type"].ToString();
                info2.vienna_class = reader["vienna_class"].ToString();
                info2.disclaimer = reader["disclaimer"].ToString();
                info2.logo_pic = reader["logo_pic"].ToString();
                info2.auth_doc = reader["auth_doc"].ToString();
                info2.sup_doc1 = reader["sup_doc1"].ToString();
                info2.sup_doc2 = reader["sup_doc2"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                info2.xvisible = reader["xvisible"].ToString();
                MarkInfo item = info2;
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info2.national_classID = reader["national_classID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.nice_class = reader["nice_class"].ToString();
                info2.nice_class_desc = reader["nice_class_desc"].ToString();
                info2.sign_type = reader["sign_type"].ToString();
                info2.vienna_class = reader["vienna_class"].ToString();
                info2.disclaimer = reader["disclaimer"].ToString();
                info2.logo_pic = reader["logo_pic"].ToString();
                info2.auth_doc = reader["auth_doc"].ToString();
                info2.sup_doc1 = reader["sup_doc1"].ToString();
                info2.sup_doc2 = reader["sup_doc2"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                info2.xvisible = reader["xvisible"].ToString();
                MarkInfo item = info2;
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<XObjs.Office_view> getNew_MarkInfoRSX2(string status, string data_status, int start, int limit)
        {
            SqlCommand command;
            List<XObjs.Office_view> list = new List<XObjs.Office_view>();
            new XObjs.Office_view();
            SqlConnection connection = new SqlConnection(this.Connect());
            //if ((status == "4") && (data_status == "Refused"))
            //{
            //    command = new SqlCommand(string.Concat(new object[] { "WITH RSTbl AS (select  pwallet.rtm,pwallet.visible,pwallet.applicantID,applicant.xname,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff, ROW_NUMBER() OVER (ORDER BY mark_info.xID) AS 'RowRank'  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID    WHERE pwallet.stage='5' AND pwallet.status='", status, "' AND pwallet.data_status='", data_status, "')SELECT * FROM RSTbl  WHERE RowRank BETWEEN '", start, "' AND '", limit, "' " }), connection);
            //}
            //else if ((status == "4") && ((data_status == "Registrable") || (data_status == "Non-registrable")))
            //{
            //    command = new SqlCommand(string.Concat(new object[] { "WITH RSTbl AS (select   pwallet.rtm,pwallet.visible,pwallet.applicantID,applicant.xname,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff, ROW_NUMBER() OVER (ORDER BY mark_info.xID) AS 'RowRank'  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID    WHERE pwallet.stage='5' AND pwallet.status='", status, "' AND ((pwallet.data_status='Non-registrable') OR (pwallet.data_status='Registrable'))  )SELECT * FROM RSTbl  WHERE RowRank BETWEEN '", start, "' AND '", limit, "' " }), connection);
            //}
            //else if ((status == "1") && ((data_status == "Fresh") || (data_status == "Invalid")))
            //{
            //    command = new SqlCommand(string.Concat(new object[] { "WITH RSTbl AS (select   pwallet.rtm,pwallet.visible,pwallet.applicantID,applicant.xname,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff, ROW_NUMBER() OVER (ORDER BY mark_info.xID) AS 'RowRank'  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID     WHERE pwallet.stage='5' AND pwallet.status='", status, "' AND (pwallet.data_status='Fresh' OR pwallet.data_status='Invalid') )SELECT * FROM RSTbl  WHERE RowRank BETWEEN '", start, "' AND '", limit, "' " }), connection);
            //}
            //else if ((status == "8") && (data_status == "Registered"))
            //{
            //    command = new SqlCommand(string.Concat(new object[] { "WITH RSTbl AS (select   pwallet.rtm,pwallet.visible,pwallet.applicantID,applicant.xname,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff, ROW_NUMBER() OVER (ORDER BY mark_info.xID) AS 'RowRank'  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID     WHERE pwallet.stage='5' AND pwallet.status>'", status, "' AND pwallet.data_status='", data_status, "' )SELECT * FROM RSTbl  WHERE RowRank BETWEEN '", start, "' AND '", limit, "' " }), connection);
            //}
            //else if ((status == "5") && (data_status == "acc_printed"))
            //{
            //    command = new SqlCommand(string.Concat(new object[] { "WITH RSTbl AS (select   pwallet.rtm,pwallet.visible,pwallet.applicantID,applicant.xname,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff, ROW_NUMBER() OVER (ORDER BY mark_info.xID) AS 'RowRank'  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID     WHERE pwallet.stage='5' AND pwallet.acc_p='1' )SELECT * FROM RSTbl  WHERE RowRank BETWEEN '", start, "' AND '", limit, "' " }), connection);
            //}
            //else
            //{
            command = new SqlCommand(string.Concat(new object[] { "select   pwallet.rtm,pwallet.visible,pwallet.applicantID,applicant.xname,mark_info.national_classID 'class',mark_info.xID,mark_info.reg_number 'reg_no',mark_info.product_title,tm_type.type 'tm_type',pwallet.validationID 'oai_no', ISNULL(pwallet.data_status,'None') 'xstat', mark_info.reg_date 'reg_dt',mark_info.log_staff  from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID   LEFT OUTER JOIN tm_type ON tm_type.xID=mark_info.tm_typeID  LEFT OUTER JOIN applicant ON applicant.log_staff=pwallet.ID     WHERE pwallet.stage='5' AND pwallet.status='", status, "' AND pwallet.data_status='", data_status, "'      " }), connection);
            //  }
            // command.CommandTimeout = 0;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            string pp2 = "";
            int vcount = 0;
            int vsn = 0;
            string voffice = "";
            while (reader.Read())
            {
                vsn = vsn + 1;
                vcount = vcount + 1;

                //if (getTmOfficeByMID(reader["log_staff"].ToString()) != "")
                //{
                //    voffice = (getTmOfficeByMID(reader["log_staff"].ToString()));
                //}
                //else
                //{
                //    voffice = "None";
                //}
                XObjs.Office_view item = new XObjs.Office_view
                {
                    xid = reader["xID"].ToString(),
                    rtm = reader["rtm"].ToString(),
                    applicant_name = reader["xname"].ToString(),
                    xclass = reader["class"].ToString(),
                    reg_no = reader["reg_no"].ToString(),
                    tm_type = reader["tm_type"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    oai_no = reader["oai_no"].ToString(),
                    xstat = reader["xstat"].ToString(),
                    reg_dt = reader["reg_dt"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    //   batches = reader["visible"].ToString(),
                    applicantID = reader["visible"].ToString(),

                    //Office = reader["data_status"].ToString(),
                    Sn = Convert.ToString(vsn)
                };

                try
                {
                    int dw = Convert.ToInt32(reader["visible"]);
                    if (dw > 1)
                    {
                        pp2 = (Convert.ToInt32(reader["visible"]) - 1).ToString();

                    }

                    else
                    {
                        pp2 = (Convert.ToInt32(reader["visible"])).ToString();

                    }
                }
                catch (Exception ee)
                {

                }

                item.batches = pp2;
                list.Add(item);


            }
            reader.Close();
            connection.Close();
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info2.national_classID = reader["national_classID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.nice_class = reader["nice_class"].ToString();
                info2.nice_class_desc = reader["nice_class_desc"].ToString();
                info2.sign_type = reader["sign_type"].ToString();
                info2.vienna_class = reader["vienna_class"].ToString();
                info2.disclaimer = reader["disclaimer"].ToString();
                info2.logo_pic = reader["logo_pic"].ToString();
                info2.auth_doc = reader["auth_doc"].ToString();
                info2.sup_doc1 = reader["sup_doc1"].ToString();
                info2.sup_doc2 = reader["sup_doc2"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                info2.xvisible = reader["xvisible"].ToString();
                MarkInfo item = info2;
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public long getPublishMarkInfoRSCnt(string status, string data_status)
        {
            long num = 0L;
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("select Count(*) AS cnt from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.stage='5' AND pwallet.status<>'33' AND pwallet.status<>'22' AND CAST(pwallet.status AS INT)>=5 AND pwallet.data_status = '" + data_status + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                num = Convert.ToInt64(reader["cnt"]);
            }
            reader.Close();
            return num;
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                MarkInfo item = info2;
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

        public List<Pwallet> getPwalletListDetailsByID(string ID)
        {
            List<Pwallet> list = new List<Pwallet>();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Pwallet pwallet2 = new Pwallet();
                pwallet2.ID = Convert.ToInt64(reader["ID"]).ToString();
                pwallet2.applicantID = reader["applicantID"].ToString();
                pwallet2.validationID = reader["validationID"].ToString();
                pwallet2.stage = reader["stage"].ToString();
                pwallet2.status = reader["status"].ToString();
                pwallet2.data_status = reader["data_status"].ToString();
                pwallet2.amt = reader["amt"].ToString();
                pwallet2.reg_date = reader["reg_date"].ToString();
                Pwallet item = pwallet2;
                list.Add(item);
            }
            reader.Close();
            return list;
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

        public List<MarkInfo> getSearchMarkInfoRS(string kword, List<string> fulltext, string cri)
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
                MarkInfo info2 = new MarkInfo();
                info2.xID = reader["xID"].ToString();
                info2.reg_number = reader["reg_number"].ToString();
                info2.tm_typeID = reader["tm_typeID"].ToString();
                info2.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info2.national_classID = reader["national_classID"].ToString();
                info2.product_title = reader["product_title"].ToString();
                info2.nice_class = reader["nice_class"].ToString();
                info2.nice_class_desc = reader["nice_class_desc"].ToString();
                info2.sign_type = reader["sign_type"].ToString();
                info2.vienna_class = reader["vienna_class"].ToString();
                info2.disclaimer = reader["disclaimer"].ToString();
                info2.logo_pic = reader["logo_pic"].ToString();
                info2.auth_doc = reader["auth_doc"].ToString();
                info2.sup_doc1 = reader["sup_doc1"].ToString();
                info2.sup_doc2 = reader["sup_doc2"].ToString();
                info2.log_staff = reader["log_staff"].ToString();
                info2.reg_date = reader["reg_date"].ToString();
                info2.xvisible = reader["xvisible"].ToString();
                MarkInfo item = info2;
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
                TmOffice office2 = new TmOffice();
                office2.ID = "";
                office2.pwalletID = "";
                office2.admin_status = "";
                office2.data_status = "";
                office2.xcomment = "";
                office2.xdoc1 = "";
                office2.xdoc2 = "";
                office2.xdoc3 = "";
                office2.xofficer = "";
                office2.reg_date = "";
                office2.xvisible = "";
                TmOffice item = office2;
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

