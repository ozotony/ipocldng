using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace Ipong.Classes
{
    public class pt
    {

        public string Connect()
        {
            return ConfigurationManager.ConnectionStrings["ptConnectionString"].ConnectionString;
        }

        public string Connect2()
        {
            return ConfigurationManager.ConnectionStrings["dsConnectionString"].ConnectionString;
        }

        public string addSwallet(SWallet s)
        {
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO search_wallet (mark_infoID,search_str,search_cri,xclass,log_officer,reg_date,visible) VALUES (@mark_infoID,@search_str,@search_cri,@xclass,@log_officer,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@mark_infoID", SqlDbType.NVarChar);
            command.Parameters.Add("@search_str", SqlDbType.Text);
            command.Parameters.Add("@search_cri", SqlDbType.Text);
            command.Parameters.Add("@xclass", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@log_officer", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@reg_date", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@visible", SqlDbType.NVarChar, 1);
            command.Parameters["@mark_infoID"].Value = s.mark_infoID;
            command.Parameters["@search_str"].Value = s.search_str;
            command.Parameters["@search_cri"].Value = s.search_cri;
            command.Parameters["@xclass"].Value = s.xclass;
            command.Parameters["@log_officer"].Value = s.log_officer;
            command.Parameters["@reg_date"].Value = s.reg_date;
            command.Parameters["@visible"].Value = s.visible;
            str2 = command.ExecuteScalar().ToString();
            connection.Close();
            return str2;
        }
        public SWallet getSwalletByID(string ID)
        {
            SWallet wallet = new SWallet();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM search_wallet WHERE mark_infoID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                wallet.ID = Convert.ToInt64(reader["ID"]).ToString();
                wallet.mark_infoID = reader["mark_infoID"].ToString();
                wallet.search_cri = reader["search_cri"].ToString();
                wallet.search_str = reader["search_str"].ToString();
                wallet.xclass = reader["xclass"].ToString();
                wallet.log_officer = reader["log_officer"].ToString();
                wallet.reg_date = reader["reg_date"].ToString();
                wallet.visible = reader["visible"].ToString();
            }
            reader.Close();
            return wallet;
        }
        public List<PtInfo> getSearchPtInfoRS(string kword, List<string> fulltext, string cri)
        {
            List<PtInfo> list = new List<PtInfo>();
            new PtInfo();
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
                    cmdText = "select * from pt_info LEFT OUTER JOIN pwallet ON pt_info.log_staff=pwallet.ID WHERE (pwallet.status >'3') AND (title_of_invention like '%" + kword + "%') ORDER BY xID ASC";
                }
                else
                {
                    cmdText = "select * from pt_info LEFT OUTER JOIN pwallet ON pt_info.log_staff=pwallet.ID WHERE  (pwallet.status >'3') AND (title_of_invention like '%" + kword + "%') ORDER BY xID ASC";
                }
            }
            else
            {
                num = fulltext.Count - 1;
                if (cri == "0")
                {

                    str2 = "select * from pt_info LEFT OUTER JOIN pwallet ON pt_info.log_staff=pwallet.ID WHERE (pwallet.status >'3') AND ";
                }
                else
                {
                    str2 = "select * from pt_info LEFT OUTER JOIN pwallet ON pt_info.log_staff=pwallet.ID WHERE (pwallet.status >'3') AND ";
                }
                for (int i = 0; i < fulltext.Count; i++)
                {
                    if (fulltext.Count == 1)
                    {
                        str3 = str3 + " ( title_of_invention like '%" + fulltext[i] + "%' ) ";
                    }
                    else if (num == i)
                    {
                        str3 = str3 + " ( title_of_invention like '%" + fulltext[i] + "%' ) ";
                    }
                    else
                    {
                        str3 = str3 + " ( title_of_invention like '%" + fulltext[i] + "%' ) OR";
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
                PtInfo item = new PtInfo
                {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    xtype = reader["xtype"].ToString(),
                    title_of_invention = reader["title_of_invention"].ToString(),
                    pt_desc = reader["pt_desc"].ToString(),
                    spec_doc = reader["spec_doc"].ToString(),
                    loa_no = reader["loa_no"].ToString(),
                    loa_doc = reader["loa_doc"].ToString(),
                    claim_no = reader["claim_no"].ToString(),
                    claim_doc = reader["claim_doc"].ToString(),
                    pct_no = reader["pct_no"].ToString(),
                    pct_doc = reader["pct_doc"].ToString(),
                    doa_no = reader["doa_no"].ToString(),
                    doa_doc = reader["doa_doc"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
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

        public string doUploadDoc(string ID, string path, FileUpload fu)
        {
            string str = "";
            try
            {
                if (!Directory.Exists(path + ID + "/"))
                {
                    Directory.CreateDirectory(path + ID + "/");
                }
                string str2 = Path.GetFileName(fu.FileName).Replace(" ", "_");
                fu.SaveAs(path + ID + "/" + str2);
                return (str = path + ID + "/" + str2);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string doUploadDocNoLimit(string ID, string path, FileUpload fu)
        {
            string str = "";
            try
            {
                if (!Directory.Exists(path + ID + "/"))
                {
                    Directory.CreateDirectory(path + ID + "/");
                }
                string str2 = Path.GetFileName(fu.FileName).Replace(" ", "_");
                fu.SaveAs(path + ID + "/" + str2);
                return (str = path + ID + "/" + str2);
            }
            catch (Exception exception)
            {
                return ("The file could not be uploaded. The following error occured: " + exception.Message);
            }
        }

        public string doUploadPic(string ID, string newfilename, string path, FileUpload fu)
        {
            try
            {
                if (!Directory.Exists(path + ID + "/"))
                {
                    Directory.CreateDirectory(path + ID + "/");
                }
                newfilename = newfilename.Replace(" ", "_");
                fu.SaveAs(path + ID + "/" + newfilename);
                return (path + ID + "/" + newfilename);
            }
            catch (Exception exception)
            {
                return ("The file could not be uploaded. The following error occured: " + exception.Message);
            }
        }


        public List<Address> getAddressByID(string ID)
        {
            List<Address> list = new List<Address>();
            new Address();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM address WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Address item = new Address
                {
                    ID = reader["ID"].ToString(),
                    countryID = reader["countryID"].ToString(),
                    stateID = reader["stateID"].ToString(),
                    lgaID = reader["lgaID"].ToString(),
                    city = ConvertTab2Apos(reader["city"].ToString()),
                    street = ConvertTab2Apos(reader["street"].ToString()),
                    zip = reader["zip"].ToString(),
                    telephone1 = reader["telephone1"].ToString(),
                    telephone2 = reader["telephone2"].ToString(),
                    email1 = ConvertTab2Apos(reader["email1"].ToString()),
                    email2 = ConvertTab2Apos(reader["email2"].ToString()),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<Address> getAddressByLog_staffID(string validationID)
        {
            List<Address> list = new List<Address>();
            new Address();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM address WHERE ID='" + validationID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Address item = new Address
                {
                    ID = reader["ID"].ToString(),
                    countryID = reader["countryID"].ToString(),
                    stateID = reader["stateID"].ToString(),
                    lgaID = reader["lgaID"].ToString(),
                    city = ConvertTab2Apos(reader["city"].ToString()),
                    street = ConvertTab2Apos(reader["street"].ToString()),
                    zip = reader["zip"].ToString(),
                    telephone1 = reader["telephone1"].ToString(),
                    telephone2 = reader["telephone2"].ToString(),
                    email1 = ConvertTab2Apos(reader["email1"].ToString()),
                    email2 = ConvertTab2Apos(reader["email2"].ToString()),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<AddressService> getAddressServiceByID(string ID)
        {
            List<AddressService> list = new List<AddressService>();
            new AddressService();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM address_service WHERE log_staff='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                AddressService item = new AddressService
                {
                    ID = Convert.ToInt64(reader["ID"]).ToString(),
                    countryID = reader["countryID"].ToString(),
                    stateID = reader["stateID"].ToString(),
                    lgaID = reader["lgaID"].ToString(),
                    city = ConvertTab2Apos(reader["city"].ToString()),
                    street = ConvertTab2Apos(reader["street"].ToString()),
                    zip = reader["zip"].ToString(),
                    telephone1 = reader["telephone1"].ToString(),
                    telephone2 = reader["telephone2"].ToString(),
                    email1 = ConvertTab2Apos(reader["email1"].ToString()),
                    email2 = ConvertTab2Apos(reader["email2"].ToString()),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public bool getAdminExtension(string filename)
        {
            string str = "";
            bool flag = false;
            int num = filename.LastIndexOf(".");
            str = filename.Substring(num + 1);
            if (((!(str == "pdf") && !(str == "jpg")) && (!(str == "jpeg") && !(str == "PDF"))) && (!(str == "JPG") && !(str == "JPEG")))
            {
                return flag;
            }
            return true;
        }

        public string getAgentEmail1ByID(string ID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT email1 FROM address WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["email1"].ToString();
            }
            reader.Close();
            return str;
        }

        public string getAgentTelephone1ByID(string ID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT telephone1 FROM address WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["telephone1"].ToString();
            }
            reader.Close();
            return str;
        }

        public List<Applicant> getApplicantByUserID(string ID)
        {
            List<Applicant> list = new List<Applicant>();
            new Applicant();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM applicant WHERE log_staff='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Applicant item = new Applicant
                {
                    ID = reader["ID"].ToString(),
                    xname = ConvertTab2Apos(reader["xname"].ToString()),
                    address = ConvertTab2Apos(reader["address"].ToString()),
                    xemail = ConvertTab2Apos(reader["xemail"].ToString()),
                    xmobile = reader["xmobile"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<Applicant> getApplicantByvalidationID(string validationID)
        {
            List<Applicant> list = new List<Applicant>();
            new Applicant();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM applicant WHERE log_staff='" + validationID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Applicant item = new Applicant
                {
                    ID = reader["ID"].ToString(),
                    xname = ConvertTab2Apos(reader["xname"].ToString()),
                    address = ConvertTab2Apos(reader["address"].ToString()),
                    xemail = ConvertTab2Apos(reader["xemail"].ToString()),
                    xmobile = reader["xmobile"].ToString(),
                    nationality = reader["nationality"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }
        public List<Inventor> getInventorByvalidationID(string validationID)
        {
            List<Inventor> list = new List<Inventor>();
            new Inventor();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM inventor WHERE log_staff='" + validationID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Inventor item = new Inventor
                {
                    ID = reader["ID"].ToString(),
                    xname = ConvertTab2Apos(reader["xname"].ToString()),
                    address = ConvertTab2Apos(reader["address"].ToString()),
                    xemail = ConvertTab2Apos(reader["xemail"].ToString()),
                    xmobile = reader["xmobile"].ToString(),
                    nationality = reader["nationality"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<Assignment_info> getAssignment_infoByvalidationID(string validationID)
        {
            List<Assignment_info> list = new List<Assignment_info>();
            new Assignment_info();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM assignment_info WHERE log_staff='" + validationID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Assignment_info item = new Assignment_info
                {
                    ID = reader["xID"].ToString(),
                    date_of_assignment = reader["date_of_assignment"].ToString(),
                    assignee_name = ConvertTab2Apos(reader["assignee_name"].ToString()),
                    assignee_address = ConvertTab2Apos(reader["assignee_address"].ToString()),
                    assignee_nationality = reader["assignee_nationality"].ToString(),
                    assignor_name = ConvertTab2Apos(reader["assignor_name"].ToString()),
                    assignor_nationality = reader["assignor_nationality"].ToString(),
                    assignor_address = ConvertTab2Apos(reader["assignor_address"].ToString()),
                    log_staff = reader["log_staff"].ToString(),
                    visible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<Priority_info> getPriority_infoByvalidationID(string validationID)
        {
            List<Priority_info> list = new List<Priority_info>();
            new Priority_info();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM priority_info WHERE log_staff='" + validationID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Priority_info item = new Priority_info
                {
                    xID = reader["xID"].ToString(),
                    countryID = reader["countryID"].ToString(),
                    app_no = reader["app_no"].ToString(),
                    xdate = reader["xdate"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public Stage getStatusIDByvalidationID(string validationID)
        {
            Stage s = new Stage();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE validationID='" + validationID + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                s.status = reader["status"].ToString();
                s.stage = reader["stage"].ToString();
                s.applicantID = reader["applicantID"].ToString();
            }
            reader.Close();
            return s;
        }

        public string getClientNumber()
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            string cmdText = "SELECT TOP 1 ID,pin FROM xscard where usedstatus='0'";
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["ID"].ToString() + "_" + reader["pin"].ToString();
            }
            reader.Close();
            return str;
        }

        public List<Country> getCountry()
        {
            List<Country> list = new List<Country>();
            new Country();
            SqlConnection connection = new SqlConnection(this.Connect());
            string cmdText = "SELECT * FROM country";
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Country item = new Country
                {
                    ID = Convert.ToInt64(reader["ID"]).ToString(),
                    name = reader["name"].ToString(),
                    code = reader["code"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public string getCountryName(string ID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT name FROM country WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["name"].ToString();
            }
            reader.Close();
            return str;
        }

        public string getExtension(string filename)
        {
            int num = filename.LastIndexOf(".");
            return filename.Substring(num + 1);
        }

        public string getFormattedAddressByID(string ID)
        {
            string str = "";
            List<Address> list = new List<Address>();
            new Address();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM address WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Address item = new Address
                {
                    ID = reader["ID"].ToString(),
                    countryID = reader["countryID"].ToString(),
                    stateID = reader["stateID"].ToString(),
                    lgaID = reader["lgaID"].ToString(),
                    city = reader["city"].ToString(),
                    street = ConvertTab2Apos(reader["street"].ToString()),
                    zip = reader["zip"].ToString(),
                    telephone1 = reader["telephone1"].ToString(),
                    telephone2 = reader["telephone2"].ToString(),
                    email1 = ConvertTab2Apos(reader["email1"].ToString()),
                    email2 = ConvertTab2Apos(reader["email2"].ToString()),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            string str4 = str;
            return (str4 + list[0].street.ToUpper() + "," + list[0].city.ToUpper() + "," + this.getStateName(list[0].stateID).ToUpper());
        }

        public List<Lga> getLga()
        {
            List<Lga> list = new List<Lga>();
            new Lga();
            SqlConnection connection = new SqlConnection(this.Connect());
            string cmdText = "SELECT * FROM lga";
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Lga item = new Lga
                {
                    ID = Convert.ToInt64(reader["ID"]).ToString(),
                    name = reader["name"].ToString(),
                    stateID = reader["stateID"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public string getLogoDescriptionName(string id)
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

        public string getNationalClassDesc(string id)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT description from national_classes where xID='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["description"].ToString();
            }
            reader.Close();
            return str;
        }


        public List<PtInfo> getPtInfoByUserID(string ID)
        {
            List<PtInfo> list = new List<PtInfo>();
            new PtInfo();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pt_info WHERE xID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                PtInfo item = new PtInfo
                {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    xtype = reader["xtype"].ToString(),
                    title_of_invention = ConvertTab2Apos(reader["title_of_invention"].ToString()),
                    pt_desc = ConvertTab2Apos(reader["pt_desc"].ToString()),
                    spec_doc = reader["spec_doc"].ToString(),
                    loa_no = reader["loa_no"].ToString(),
                    loa_doc = reader["loa_doc"].ToString(),
                    claim_no = reader["claim_no"].ToString(),
                    claim_doc = reader["claim_doc"].ToString(),
                    pct_no = reader["pct_no"].ToString(),
                    pct_doc = reader["pct_doc"].ToString(),
                    doa_no = reader["doa_no"].ToString(),
                    doa_doc = reader["doa_doc"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<PtInfo> getPtInfoByPwalletID(string ID)
        {
            List<PtInfo> list = new List<PtInfo>();
            new PtInfo();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pt_info WHERE log_staff='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                PtInfo item = new PtInfo
                {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    xtype = reader["xtype"].ToString(),
                    title_of_invention = ConvertTab2Apos(reader["title_of_invention"].ToString()),
                    pt_desc = reader["pt_desc"].ToString(),
                    spec_doc = reader["spec_doc"].ToString(),
                    loa_no = reader["loa_no"].ToString(),
                    loa_doc = reader["loa_doc"].ToString(),
                    claim_no = reader["claim_no"].ToString(),
                    claim_doc = reader["claim_doc"].ToString(),
                    pct_no = reader["pct_no"].ToString(),
                    pct_doc = reader["pct_doc"].ToString(),
                    doa_no = reader["doa_no"].ToString(),
                    doa_doc = reader["doa_doc"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public string getPtTypeName(string id)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT type from pt_type where xID='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["type"].ToString();
            }
            reader.Close();
            return str;
        }

        public List<Stage> getPwalletDetailsByID(string ID)
        {
            List<Stage> list = new List<Stage>();
            new Stage();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Stage item = new Stage
                {
                    ID = reader["ID"].ToString(),
                    applicantID = reader["applicantID"].ToString(),
                    validationID = reader["validationID"].ToString(),
                    stage = reader["stage"].ToString(),
                    amt = reader["amt"].ToString(),
                    reg_date = reader["reg_date"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }
        public List<Stage> getPwalletDetailsByValidationID(string validationID)
        {
            List<Stage> list = new List<Stage>();
            new Stage();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE validationID='" + validationID + "'  ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Stage item = new Stage
                {
                    ID = reader["ID"].ToString(),
                    applicantID = reader["applicantID"].ToString(),
                    validationID = reader["validationID"].ToString(),
                    stage = reader["stage"].ToString(),
                    amt = reader["amt"].ToString(),
                    reg_date = reader["reg_date"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public string getPwalletID(string validationID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT ID FROM pwallet WHERE validationID='" + validationID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = Convert.ToInt64(reader["ID"]).ToString();
            }
            reader.Close();
            return str;
        }

        public string getPwalletIDByMID(string pt_infoID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT log_staff from pt_info where xID='" + pt_infoID + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["log_staff"].ToString();
            }
            reader.Close();
            return str;
        }

        public Representative getRepByUserID(string ID)
        {
            Representative representative = new Representative();
            representative.ID = "";
            representative.agent_code = "";
            representative.xname = "";
            representative.nationality = "";
            representative.country = "";
            representative.state = "";
            representative.address = "";
            representative.xemail = "";
            representative.xmobile = "";
            representative.log_staff = "";
            representative.visible = "";

            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM representative WHERE log_staff='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                representative.ID = reader["ID"].ToString();
                representative.agent_code = reader["agent_code"].ToString();
                representative.xname = ConvertTab2Apos(reader["xname"].ToString());
                representative.nationality = reader["nationality"].ToString();
                representative.country = reader["country"].ToString();
                representative.nationality = reader["nationality"].ToString();
                representative.state = reader["state"].ToString();
                representative.address = ConvertTab2Apos(reader["address"].ToString());
                representative.xemail = ConvertTab2Apos(reader["xemail"].ToString());
                representative.xmobile = reader["xmobile"].ToString();
                representative.log_staff = reader["log_staff"].ToString();
                representative.visible = reader["visible"].ToString();

            }
            reader.Close();
            return representative;
        }

        public List<Representative> getRepListByUserID(string validationID)
        {
            List<Representative> list = new List<Representative>();
            new Representative();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM representative WHERE log_staff='" + validationID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Representative item = new Representative
                {
                    ID = reader["ID"].ToString(),
                    agent_code = reader["agent_code"].ToString(),
                    xname = ConvertTab2Apos(reader["xname"].ToString()),
                    nationality = reader["nationality"].ToString(),
                    country = reader["country"].ToString(),
                    state = reader["state"].ToString(),
                    address = ConvertTab2Apos(reader["address"].ToString()),
                    xemail = ConvertTab2Apos(reader["xemail"].ToString()),
                    xmobile = reader["xmobile"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<Contact_form> getContact_formByOfficerID(string xofficerID, string sent_status)
        {
            List<Contact_form> list = new List<Contact_form>();
            new Contact_form();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM contact_form WHERE xofficerID='" + xofficerID + "' AND sent_status='" + sent_status + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Contact_form item = new Contact_form
                {
                    xID = reader["xID"].ToString(),
                    response_deadline = reader["response_deadline"].ToString(),
                    xofficerID = reader["xofficerID"].ToString(),
                    xmsg = ConvertTab2Apos(reader["xmsg"].ToString()),
                    sent_status = reader["sent_status"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<Stage> getStageByUserID(string ID)
        {
            List<Stage> list = new List<Stage>();
            new Stage();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Stage item = new Stage
                {
                    ID = reader["ID"].ToString(),
                    applicantID = reader["applicantID"].ToString(),
                    validationID = reader["validationID"].ToString(),
                    stage = reader["stage"].ToString(),
                    status = reader["status"].ToString(),
                    amt = reader["amt"].ToString(),
                    reg_date = reader["reg_date"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public Int32 getStageIDByvalidationID(string validationID)
        {
            Int32 ID = 0;
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT ID FROM pwallet WHERE validationID='" + validationID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {

                ID = Convert.ToInt32(reader["ID"]);
            }
            reader.Close();
            return ID;
        }

        public List<Stage> getStageByUserIDAcc(string validationID, string agentID)
        {
            List<Stage> list = new List<Stage>();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE validationID='" + validationID + "'  AND applicantID='" + agentID + "' AND stage='5' AND data_status <>'' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Stage item = new Stage
                {
                    ID = reader["ID"].ToString(),
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

        public List<Stage> getStageByUserIDAdmin(string validationID)
        {
            List<Stage> list = new List<Stage>();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE validationID='" + validationID + "'  AND data_status <>'' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Stage item = new Stage
                {
                    ID = reader["ID"].ToString(),
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
        public List<State> getState(string countryID)
        {
            List<State> list = new List<State>();
            new State();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM state WHERE countryID='" + countryID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                State item = new State
                {
                    ID = Convert.ToInt64(reader["ID"]).ToString(),
                    name = reader["name"].ToString(),
                    countryID = reader["countryID"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public string getStateName(string ID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT name FROM state WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["name"].ToString();
            }
            reader.Close();
            return str;
        }


        public string ValidationIDByPwalletID(string ID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT validationID FROM pwallet WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["validationID"].ToString();
            }
            reader.Close();
            return str;
        }

        public string addNewPatent(List<Applicant> lt_app, List<pt.Priority_info> lt_pri, List<pt.Inventor> lt_inv, PtInfo c_pt, pt.Assignment_info c_assinfo, Representative c_rep)
        {
            string xID = "";

            foreach (Applicant c_app in lt_app)
            {
                if ((c_app.xname != null) && (c_app.xname != ""))
                {
                    this.addApplicant(c_app);
                }
            }
            foreach (Priority_info c_pri in lt_pri)
            {
                if ((c_pri.app_no != null) && (c_pri.app_no != ""))
                {
                    this.addPriority_info(c_pri);
                }
            }
            foreach (Inventor c_inv in lt_inv)
            {
                if ((c_inv.xname != null) && (c_inv.xname != ""))
                {
                    this.addInventor(c_inv);
                }
            }
            if ((c_assinfo.assignee_name != null) && (c_assinfo.assignee_name != "") && (c_assinfo.date_of_assignment != null) && (c_assinfo.date_of_assignment != "") && (c_assinfo.ID != null) && (c_assinfo.ID != ""))
            {
                this.addAssignment_info(c_assinfo);
            }
            xID = this.addPt(c_pt);
            this.updatePtReg(xID, c_pt.xtype);
            this.addRepresentative(c_rep);
            this.updatePwalletStatus(c_pt.log_staff, "0");
            return xID;
        }

        public string updatePwalletStatus(string pwalletID, string log_officer)
        {
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE pwallet SET stage=5,log_officer=@log_officer WHERE ID=@ID ";
            connection.Open();
            command.Parameters.Add("@ID", SqlDbType.BigInt);
            command.Parameters.Add("@log_officer", SqlDbType.NVarChar, 50);
            command.Parameters["@ID"].Value = Convert.ToInt64(pwalletID);
            command.Parameters["@log_officer"].Value = log_officer;
            str2 = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str2;
        }


        public string updateApplicant(Applicant x)
        {
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE [dbo].[applicant] SET [xname] ='" + ConvertApos2Tab(x.xname) + "',[address] = '" + ConvertApos2Tab(x.address) + "',[xemail] = '" + ConvertApos2Tab(x.xemail) + "',[xmobile] = '" + x.xmobile + "',  ";
            command.CommandText += " [nationality] = '" + x.nationality + "',[log_staff] = '" + x.log_staff + "',[visible] = '" + x.visible + "' WHERE ID ='" + x.ID + "' ";

            connection.Open();
            str2 = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str2;
        }

        public string updateAssignment_info(Assignment_info x)
        {
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE [dbo].[assignment_info] SET [date_of_assignment] ='" + x.date_of_assignment + "',[assignee_name] = '" + ConvertApos2Tab(x.assignee_name) + "',[assignee_address] = '" + ConvertApos2Tab(x.assignee_address) + "', ";
            command.CommandText += "  [assignee_nationality] = '" + x.assignee_nationality + "',[assignor_name] = '" + ConvertApos2Tab(x.assignor_name) + "',[assignor_address] = '" + ConvertApos2Tab(x.assignor_address) + "' , ";
            command.CommandText += "  [assignor_nationality] = '" + x.assignor_nationality + "',[log_staff] = '" + x.log_staff + "',[xvisible] = '" + x.visible + "' WHERE xID ='" + x.ID + "' ";

            connection.Open();
            str2 = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str2;
        }


        public string updateInventor(Inventor x)
        {
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE [dbo].[inventor] SET [xname] ='" + ConvertApos2Tab(x.xname) + "',[address] = '" + ConvertApos2Tab(x.address) + "',[xemail] = '" + ConvertApos2Tab(x.xemail) + "', ";
            command.CommandText += "  [xmobile] = '" + x.xmobile + "',[nationality] = '" + x.nationality + "',[log_staff] = '" + x.log_staff + "' , ";
            command.CommandText += "  [visible] = '" + x.visible + "' WHERE ID ='" + x.ID + "' ";

            connection.Open();
            str2 = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str2;
        }

        public string updatePriority_info(Priority_info x)
        {
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE [dbo].[priority_info] SET [countryID] ='" + x.countryID + "',[app_no] = '" + x.app_no + "',[xdate] = '" + x.xdate + "', ";
            command.CommandText += "  [log_staff] = '" + x.log_staff + "',[xvisible] = '" + x.xvisible + "' WHERE xID ='" + x.xID + "' ";

            connection.Open();
            str2 = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str2;
        }

        public string updatePtInfo(PtInfo x)
        {
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE [dbo].[pt_info] SET [reg_number] ='" + x.reg_number + "',[xtype] = '" + x.xtype + "',[title_of_invention] = '" + ConvertApos2Tab(x.title_of_invention) + "', ";
            command.CommandText += "  [pt_desc] = '" + ConvertApos2Tab(x.pt_desc) + "',[reg_date] = '" + x.reg_date + "', ";
            command.CommandText += "  [log_staff] = '" + x.log_staff + "',[xvisible] = '" + x.xvisible + "' WHERE xID ='" + x.xID + "' ";

            connection.Open();
            str2 = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str2;
        }

        public string updateRepresentative(Representative x)
        {
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE [dbo].[representative] SET [agent_code] ='" + x.agent_code + "',[xname] = '" + ConvertApos2Tab(x.xname) + "',[nationality] = '" + x.nationality + "', ";
            command.CommandText += "  [country] = '" + x.country + "',[state] = '" + x.state + "',[address] = '" + ConvertApos2Tab(x.address) + "' , ";
            command.CommandText += "  [xemail] = '" + ConvertApos2Tab(x.xemail) + "',[xmobile] = '" + x.xmobile + "',[log_staff] = '" + x.log_staff + "' , ";
            command.CommandText += "  [visible] = '" + x.visible + "' WHERE ID ='" + x.ID + "' ";

            connection.Open();
            str2 = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str2;
        }

        public string updatePtDocz(string spec_doc, string loa_doc, string loa_no, string claim_doc, string claim_no, string pct_doc, string pct_no, string doa_doc, string doa_no, string pwalletID)
        {
            string connectionString = this.Connect();
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

        public string updateDoaDocz(string doa_doc, string doa_no, string pwalletID)
        {
            string connectionString = this.Connect();
            string succ = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE pt_info SET doa_doc=@doa_doc,doa_no=@doa_no WHERE xID=@pwalletID ";
            connection.Open();

            command.Parameters.Add("@doa_doc", SqlDbType.Text);
            command.Parameters.Add("@doa_no", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@pwalletID", SqlDbType.NVarChar);

            command.Parameters["@doa_doc"].Value = doa_doc;
            command.Parameters["@doa_no"].Value = doa_no;
            command.Parameters["@pwalletID"].Value = pwalletID;

            succ = command.ExecuteNonQuery().ToString();
            connection.Close();
            return succ;
        }
        public string updatePctDocz(string pct_doc, string pct_no, string pwalletID)
        {
            string connectionString = this.Connect();
            string succ = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE pt_info SET pct_doc=@pct_doc,pct_no=@pct_no WHERE xID=@pwalletID ";
            connection.Open();

            command.Parameters.Add("@pct_doc", SqlDbType.Text);
            command.Parameters.Add("@pct_no", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@pwalletID", SqlDbType.NVarChar);

            command.Parameters["@pct_doc"].Value = pct_doc;
            command.Parameters["@pct_no"].Value = pct_no;
            command.Parameters["@pwalletID"].Value = pwalletID;

            succ = command.ExecuteNonQuery().ToString();
            connection.Close();
            return succ;
        }
        public string updateClaimDocz(string claim_doc, string claim_no, string pwalletID)
        {
            string connectionString = this.Connect();
            string succ = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE pt_info SET claim_doc=@claim_doc,claim_no=@claim_no WHERE xID=@pwalletID ";
            connection.Open();

            command.Parameters.Add("@claim_doc", SqlDbType.Text);
            command.Parameters.Add("@claim_no", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@pwalletID", SqlDbType.NVarChar);

            command.Parameters["@claim_doc"].Value = claim_doc;
            command.Parameters["@claim_no"].Value = claim_no;
            command.Parameters["@pwalletID"].Value = pwalletID;

            succ = command.ExecuteNonQuery().ToString();
            connection.Close();
            return succ;
        }
        public string updateLoaDocz(string loa_doc, string loa_no, string pwalletID)
        {
            string connectionString = this.Connect();
            string succ = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE pt_info SET loa_doc=@loa_doc,loa_no=@loa_no WHERE xID=@pwalletID ";
            connection.Open();

            command.Parameters.Add("@loa_doc", SqlDbType.Text);
            command.Parameters.Add("@loa_no", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@pwalletID", SqlDbType.NVarChar);

            command.Parameters["@loa_doc"].Value = loa_doc;
            command.Parameters["@loa_no"].Value = loa_no;
            command.Parameters["@pwalletID"].Value = pwalletID;

            succ = command.ExecuteNonQuery().ToString();
            connection.Close();
            return succ;
        }
        public string updateSpecDocz(string spec_doc, string pwalletID)
        {
            string connectionString = this.Connect();
            string succ = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE pt_info SET spec_doc=@spec_doc WHERE xID=@pwalletID ";
            connection.Open();
            command.Parameters.Add("@spec_doc", SqlDbType.Text);
            command.Parameters.Add("@pwalletID", SqlDbType.NVarChar);

            command.Parameters["@spec_doc"].Value = spec_doc;
            command.Parameters["@pwalletID"].Value = pwalletID;

            succ = command.ExecuteNonQuery().ToString();
            connection.Close();
            return succ;
        }

        public string updatePtReg(string xID, string typ)
        {
            string str = "0";
            string str2 = "";
            if (typ.ToUpper() == "NON-CONVENTIONAL")
            {
                str2 = "NG/PT/NC/" + DateTime.Today.Date.ToString("yyyy") + "/" + xID;
            }
            else
            {
                str2 = "NG/PT/C/" + DateTime.Today.Date.ToString("yyyy") + "/" + xID;
            }
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE pt_info SET reg_number=@reg_number WHERE xID=@xID ";
            connection.Open();
            command.Parameters.Add("@xID", SqlDbType.BigInt);
            command.Parameters.Add("@reg_number", SqlDbType.NVarChar, 50);
            command.Parameters["@xID"].Value = Convert.ToInt64(xID);
            command.Parameters["@reg_number"].Value = str2;
            str = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str;
        }

        public string deleteApplicant(string id)
        {
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM applicant WHERE [log_staff] ='" + id + "'";
            connection.Open();
            str2 = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str2;
        }
        public string deleteInventor(string id)
        {
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM inventor WHERE [log_staff] ='" + id + "'";
            connection.Open();
            str2 = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str2;
        }

        public string addAddress(Address c_app_addy, string pwalletID)
        {
            string str = DateTime.Today.Date.ToString("yyyy-MM-dd");
            string str2 = "1";
            if (c_app_addy.countryID == null)
            {
                c_app_addy.countryID = "";
            }
            if (c_app_addy.stateID == null)
            {
                c_app_addy.stateID = "";
            }
            if (c_app_addy.city == null)
            {
                c_app_addy.city = "";
            }
            if (c_app_addy.street == null)
            {
                c_app_addy.street = "";
            }
            if (c_app_addy.telephone1 == null)
            {
                c_app_addy.telephone1 = "";
            }
            if (c_app_addy.email1 == null)
            {
                c_app_addy.email1 = "";
            }
            string connectionString = this.Connect();
            string str4 = "0";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO address (countryID,stateID,city,street,telephone1,email1,log_staff,reg_date,visible) VALUES (@countryID,@stateID,@city,@street,@telephone1,@email1,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@countryID", SqlDbType.VarChar, 10);
            command.Parameters.Add("@stateID", SqlDbType.NVarChar, 10);
            command.Parameters.Add("@city", SqlDbType.VarChar, 40);
            command.Parameters.Add("@street", SqlDbType.Text);
            command.Parameters.Add("@telephone1", SqlDbType.NVarChar, 40);
            command.Parameters.Add("@email1", SqlDbType.VarChar, 40);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 40);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 40);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@countryID"].Value = c_app_addy.countryID;
            command.Parameters["@stateID"].Value = c_app_addy.stateID;
            command.Parameters["@city"].Value = c_app_addy.city;
            command.Parameters["@street"].Value = ConvertApos2Tab(c_app_addy.street);
            command.Parameters["@telephone1"].Value = c_app_addy.telephone1;
            command.Parameters["@email1"].Value = ConvertApos2Tab(c_app_addy.email1);
            command.Parameters["@log_staff"].Value = pwalletID;
            command.Parameters["@reg_date"].Value = str;
            command.Parameters["@visible"].Value = str2;
            str4 = command.ExecuteScalar().ToString();
            connection.Close();
            return str4;
        }
        public string addAos(AddressService c_aos, string pwalletID)
        {
            string str = DateTime.Today.Date.ToString("yyyy-MM-dd");
            string str2 = "1";
            if (c_aos.countryID == null)
            {
                c_aos.countryID = "";
            }
            if (c_aos.stateID == null)
            {
                c_aos.stateID = "";
            }
            if (c_aos.city == null)
            {
                c_aos.city = "";
            }
            if (c_aos.street == null)
            {
                c_aos.street = "";
            }
            if (c_aos.telephone1 == null)
            {
                c_aos.telephone1 = "";
            }
            if (c_aos.email1 == null)
            {
                c_aos.email1 = "";
            }
            string connectionString = this.Connect();
            string str4 = "0";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO address_service (countryID,stateID,city,street,telephone1,email1,log_staff,reg_date,visible) VALUES (@countryID,@stateID,@city,@street,@telephone1,@email1,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@countryID", SqlDbType.VarChar, 10);
            command.Parameters.Add("@stateID", SqlDbType.NVarChar, 10);
            command.Parameters.Add("@city", SqlDbType.VarChar, 40);
            command.Parameters.Add("@street", SqlDbType.Text);
            command.Parameters.Add("@telephone1", SqlDbType.NVarChar, 40);
            command.Parameters.Add("@email1", SqlDbType.VarChar, 40);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 40);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 40);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@countryID"].Value = c_aos.countryID;
            command.Parameters["@stateID"].Value = c_aos.stateID;
            command.Parameters["@city"].Value = c_aos.city;
            command.Parameters["@street"].Value = ConvertApos2Tab(c_aos.street);
            command.Parameters["@telephone1"].Value = c_aos.telephone1;
            command.Parameters["@email1"].Value = ConvertApos2Tab(c_aos.email1);
            command.Parameters["@log_staff"].Value = pwalletID;
            command.Parameters["@reg_date"].Value = str;
            command.Parameters["@visible"].Value = str2;
            str4 = command.ExecuteScalar().ToString();
            connection.Close();
            return str4;
        }

        public string addApplicant(Applicant c_app)
        {
            string connectionString = this.Connect();
            string succ = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO applicant (xname,address,xemail,xmobile,nationality,log_staff,visible) VALUES (@xname,@address,@xemail,@xmobile,@nationality,@log_staff,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@xname", SqlDbType.VarChar);
            command.Parameters.Add("@address", SqlDbType.Text);
            command.Parameters.Add("@xemail", SqlDbType.VarChar);
            command.Parameters.Add("@xmobile", SqlDbType.VarChar);
            command.Parameters.Add("@nationality", SqlDbType.VarChar, 50);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 20);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 10);

            command.Parameters["@xname"].Value = ConvertApos2Tab(c_app.xname);
            command.Parameters["@address"].Value = ConvertApos2Tab(c_app.address);
            command.Parameters["@xemail"].Value = ConvertApos2Tab(c_app.xemail);
            command.Parameters["@xmobile"].Value = c_app.xmobile;
            command.Parameters["@nationality"].Value = c_app.nationality;
            command.Parameters["@log_staff"].Value = c_app.log_staff;
            command.Parameters["@visible"].Value = c_app.visible;
            succ = command.ExecuteScalar().ToString();
            connection.Close();
            return succ;
        }
        public string addInventor(Inventor c_inv)
        {
            string connectionString = this.Connect();
            string succ = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO inventor (xname,address,xemail,xmobile,nationality,log_staff,visible) VALUES (@xname,@address,@xemail,@xmobile,@nationality,@log_staff,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@xname", SqlDbType.VarChar);
            command.Parameters.Add("@address", SqlDbType.Text);
            command.Parameters.Add("@xemail", SqlDbType.VarChar);
            command.Parameters.Add("@xmobile", SqlDbType.VarChar);
            command.Parameters.Add("@nationality", SqlDbType.VarChar, 50);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 20);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 10);

            command.Parameters["@xname"].Value = ConvertApos2Tab(c_inv.xname);
            command.Parameters["@address"].Value = ConvertApos2Tab(c_inv.address);
            command.Parameters["@xemail"].Value = ConvertApos2Tab(c_inv.xemail);
            command.Parameters["@xmobile"].Value = c_inv.xmobile;
            command.Parameters["@nationality"].Value = c_inv.nationality;
            command.Parameters["@log_staff"].Value = c_inv.log_staff;
            command.Parameters["@visible"].Value = c_inv.visible;
            succ = command.ExecuteScalar().ToString();
            connection.Close();
            return succ;
        }
        public string addPriority_info(Priority_info c_pri)
        {
            string connectionString = this.Connect();
            string succ = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO priority_info (countryID,app_no,xdate,log_staff,xvisible) VALUES (@countryID,@app_no,@xdate,@log_staff,@xvisible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@countryID", SqlDbType.VarChar, 50);
            command.Parameters.Add("@app_no", SqlDbType.VarChar);
            command.Parameters.Add("@xdate", SqlDbType.VarChar, 50);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 20);
            command.Parameters.Add("@xvisible", SqlDbType.VarChar, 10);

            command.Parameters["@countryID"].Value = c_pri.countryID;
            command.Parameters["@app_no"].Value = c_pri.app_no;
            command.Parameters["@xdate"].Value = c_pri.xdate;
            command.Parameters["@log_staff"].Value = c_pri.log_staff;
            command.Parameters["@xvisible"].Value = c_pri.xvisible;
            succ = command.ExecuteScalar().ToString();
            connection.Close();
            return succ;
        }
        public string addAssignment_info(Assignment_info c_ass)
        {
            string connectionString = this.Connect();
            string succ = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO assignment_info (date_of_assignment,assignee_name,assignee_address,assignee_nationality,assignor_name,assignor_address,assignor_nationality,log_staff,xvisible) VALUES (@date_of_assignment,@assignee_name,@assignee_address,@assignee_nationality,@assignor_name,@assignor_address,@assignor_nationality,@log_staff,@xvisible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@date_of_assignment", SqlDbType.VarChar, 50);
            command.Parameters.Add("@assignee_name", SqlDbType.VarChar);
            command.Parameters.Add("@assignee_address", SqlDbType.Text);
            command.Parameters.Add("@assignee_nationality", SqlDbType.VarChar, 50);
            command.Parameters.Add("@assignor_name", SqlDbType.VarChar);
            command.Parameters.Add("@assignor_address", SqlDbType.Text);
            command.Parameters.Add("@assignor_nationality", SqlDbType.VarChar, 50);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 50);
            command.Parameters.Add("@xvisible", SqlDbType.VarChar, 10);

            command.Parameters["@date_of_assignment"].Value = c_ass.date_of_assignment;
            command.Parameters["@assignee_name"].Value = ConvertApos2Tab(c_ass.assignee_name);
            command.Parameters["@assignee_address"].Value = ConvertApos2Tab(c_ass.assignee_address);
            command.Parameters["@assignee_nationality"].Value = c_ass.assignee_nationality;
            command.Parameters["@assignor_name"].Value = ConvertApos2Tab(c_ass.assignor_name);
            command.Parameters["@assignor_address"].Value = ConvertApos2Tab(c_ass.assignor_address);
            command.Parameters["@assignor_nationality"].Value = c_ass.assignor_nationality;
            command.Parameters["@log_staff"].Value = c_ass.log_staff;
            command.Parameters["@xvisible"].Value = c_ass.visible;
            succ = command.ExecuteScalar().ToString();
            connection.Close();
            return succ;
        }
        public string addPt(PtInfo c_pt)
        {
            string succ = "";
            string connectionString = this.Connect();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO pt_Info (reg_number,xtype,title_of_invention,pt_desc,spec_doc,loa_no,loa_doc,claim_no,claim_doc,pct_no,pct_doc,doa_no,doa_doc,log_staff,reg_date,xvisible) VALUES (@reg_number,@xtype,@title_of_invention,@pt_desc,@spec_doc,@loa_no,@loa_doc,@claim_no,@claim_doc,@pct_no,@pct_doc,@doa_no,@doa_doc,@log_staff,@reg_date,@xvisible) SELECT SCOPE_IDENTITY()";
            connection.Open();

            command.Parameters.Add("@reg_number", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@xtype", SqlDbType.NVarChar);
            command.Parameters.Add("@title_of_invention", SqlDbType.NVarChar);
            command.Parameters.Add("@pt_desc", SqlDbType.Text);
            command.Parameters.Add("@spec_doc", SqlDbType.Text);
            command.Parameters.Add("@loa_no", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@loa_doc", SqlDbType.Text);
            command.Parameters.Add("@claim_no", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@claim_doc", SqlDbType.Text);
            command.Parameters.Add("@pct_no", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@pct_doc", SqlDbType.Text);
            command.Parameters.Add("@doa_no", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@doa_doc", SqlDbType.Text);
            command.Parameters.Add("@log_staff", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@reg_date", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@xvisible", SqlDbType.NVarChar, 10);

            command.Parameters["@reg_number"].Value = c_pt.reg_number;
            command.Parameters["@xtype"].Value = c_pt.xtype;
            command.Parameters["@title_of_invention"].Value = ConvertApos2Tab(c_pt.title_of_invention);
            command.Parameters["@pt_desc"].Value = ConvertApos2Tab(c_pt.pt_desc);
            command.Parameters["@spec_doc"].Value = "";
            command.Parameters["@loa_no"].Value = c_pt.loa_no;
            command.Parameters["@loa_doc"].Value = "";
            command.Parameters["@claim_no"].Value = c_pt.claim_no;
            command.Parameters["@claim_doc"].Value = "";
            command.Parameters["@pct_no"].Value = c_pt.pct_no;
            command.Parameters["@pct_doc"].Value = "";
            command.Parameters["@doa_no"].Value = c_pt.doa_no;
            command.Parameters["@doa_doc"].Value = "";
            command.Parameters["@log_staff"].Value = c_pt.log_staff;
            command.Parameters["@reg_date"].Value = c_pt.reg_date;
            command.Parameters["@xvisible"].Value = c_pt.xvisible;

            succ = command.ExecuteScalar().ToString();
            connection.Close();
            return succ;
        }
        public string addRepresentative(Representative c_rep)
        {
            string succ = "0";
            string connectionString = this.Connect();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO representative (agent_code,xname,nationality,country,state,address,xemail,xmobile,log_staff,visible) VALUES (@agent_code,@xname,@nationality,@country,@state,@address,@xemail,@xmobile,@log_staff,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@agent_code", SqlDbType.VarChar);
            command.Parameters.Add("@xname", SqlDbType.NVarChar);
            command.Parameters.Add("@nationality", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@country", SqlDbType.VarChar, 50);
            command.Parameters.Add("@state", SqlDbType.VarChar, 50);
            command.Parameters.Add("@address", SqlDbType.Text);
            command.Parameters.Add("@xemail", SqlDbType.NVarChar);
            command.Parameters.Add("@xmobile", SqlDbType.VarChar);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 50);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 10);

            command.Parameters["@agent_code"].Value = c_rep.agent_code;
            command.Parameters["@xname"].Value = ConvertApos2Tab(c_rep.xname);
            command.Parameters["@nationality"].Value = c_rep.nationality;
            command.Parameters["@country"].Value = c_rep.country;
            command.Parameters["@state"].Value = c_rep.state;
            command.Parameters["@address"].Value = ConvertApos2Tab(c_rep.address);
            command.Parameters["@xemail"].Value = ConvertApos2Tab(c_rep.xemail);
            command.Parameters["@xmobile"].Value = c_rep.xmobile;
            command.Parameters["@log_staff"].Value = c_rep.log_staff;
            command.Parameters["@visible"].Value = c_rep.visible;
            succ = command.ExecuteScalar().ToString();
            connection.Close();
            return succ;
        }

        public string addPwallet(string serverpath, string validationID, string agent_code, string amt, string log_officer)
        {
            string connectionString = this.Connect();
            //this.cleanseTmByValidation(serverpath, validationID);
            string str3 = DateTime.Today.Date.ToString("yyyy-MM-dd");
            string str4 = "1";
            string str5 = "1";
            string str6 = "1";
            string str7 = "Fresh";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO pwallet (validationID,applicantID,log_officer,amt,stage,status,data_status,reg_date,visible )  VALUES ( @validationID,@applicantID,@log_officer,@amt,@stage,@status,@data_status,@regdate,@visible ) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@validationID", SqlDbType.VarChar, 50);
            command.Parameters.Add("@applicantID", SqlDbType.VarChar, 50);
            command.Parameters.Add("@log_officer", SqlDbType.VarChar, 50);
            command.Parameters.Add("@amt", SqlDbType.VarChar, 50);
            command.Parameters.Add("@stage", SqlDbType.NChar, 10);
            command.Parameters.Add("@status", SqlDbType.VarChar, 20);
            command.Parameters.Add("@data_status", SqlDbType.VarChar, 50);
            command.Parameters.Add("@regdate", SqlDbType.VarChar, 50);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@validationID"].Value = validationID;
            command.Parameters["@applicantID"].Value = agent_code;
            command.Parameters["@log_officer"].Value = log_officer;
            command.Parameters["@amt"].Value = amt;
            command.Parameters["@stage"].Value = str6;
            command.Parameters["@status"].Value = str5;
            command.Parameters["@data_status"].Value = str7;
            command.Parameters["@regdate"].Value = str3;
            command.Parameters["@visible"].Value = str4;
            return command.ExecuteScalar().ToString();
        }

        public void cleanseTmByValidation(string serverpath, string vid)
        {
            string id = "0";
            string connectionString = this.Connect();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SELECT * from pwallet where validationID='" + vid + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                reader["stage"].ToString();
                id = reader["ID"].ToString();
            }
            reader.Close();
            if (id != "0")
            {
                SqlConnection connection2 = new SqlConnection(connectionString);
                SqlCommand command2 = new SqlCommand("DELETE from pwallet where validationID='" + vid + "'", connection2);
                connection2.Open();
                command2.ExecuteNonQuery();
                connection2.Close();
                this.flushApplicant(id);
                this.flushPt_info(serverpath, id);
                this.flushAddress_service(id);
                this.flushRepresentative(id);
                this.flushAddress(id);
            }
        }
        public void flushAddress(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from address where log_staff='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void flushAddress_service(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from address_service where log_staff='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void flushApplicant(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from applicant where log_staff='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void flushPt_info(string serverpath, string id)
        {
            long markID = 0L;
            string connectionString = this.Connect();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SELECT xID from pt_info where log_staff='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                markID = Convert.ToInt64(reader["xID"]);
            }
            reader.Close();
            SqlConnection connection2 = new SqlConnection(connectionString);
            SqlCommand command2 = new SqlCommand("DELETE from pt_info where log_staff='" + id + "'", connection2);
            connection2.Open();
            command2.ExecuteNonQuery();
            connection2.Close();
            if (markID > 0L)
            {
                this.doDeleteDir(serverpath, markID);
            }
        }

        public void flushRepresentative(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from representative where log_staff='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void flushPwalletByID(string id)
        {
            string connectionString = this.Connect();

            if ((id != "0") && (id != ""))
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand("DELETE from pwallet where ID='" + id + "'", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void flushAddress_serviceByID(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from address_service where ID='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void flushApplicantByID(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from applicant where ID='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void flushMark_infoByID(string serverpath, string id)
        {
            string connectionString = this.Connect();
            SqlConnection connection2 = new SqlConnection(connectionString);
            SqlCommand command2 = new SqlCommand("DELETE from pt_info where xID='" + id + "'", connection2);
            connection2.Open();
            command2.ExecuteNonQuery();
            connection2.Close();
            if (Convert.ToInt64(id) > 0L)
            {
                this.doDeleteDir(serverpath, Convert.ToInt64(id));
            }
        }

        public void flushRepresentativeByID(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from representative where ID='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void flushAddressByID(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from address where ID='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void doDeleteDir(string serverpath, long markID)
        {
            if (markID > 0L)
            {
                string path = serverpath + "admin/tm/docz/" + markID.ToString() + "/";
                string str2 = serverpath + "admin/tm/Picz/" + markID.ToString() + "/";
                try
                {
                    if (Directory.Exists(path))
                    {
                        foreach (string str3 in Directory.GetFiles(path))
                        {
                            File.Delete(str3);
                        }
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    if (Directory.Exists(str2))
                    {
                        foreach (string str4 in Directory.GetFiles(str2))
                        {
                            File.Delete(str4);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void doDeleteCurrentDir(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    foreach (string str3 in Directory.GetFiles(path))
                    {
                        File.Delete(str3);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path);
                }
            }

        }

        /// <summary>
        /// ////////////Classes start here//////////////////////////////////////////////
        /// </summary>
        /// 
        public class Contact_form
        {
            public string xID { get; set; }
            public string pwalletID { get; set; }
            public string response_deadline { get; set; }
            public string xofficerID { get; set; }
            public string xmsg { get; set; }
            public string sent_status { get; set; }
            public string reg_date { get; set; }
            public string xvisible { get; set; }
        }

        public class Address
        {
            public string city { get; set; }
            public string countryID { get; set; }
            public string email1 { get; set; }
            public string email2 { get; set; }
            public string ID { get; set; }
            public string lgaID { get; set; }
            public string log_staff { get; set; }
            public string reg_date { get; set; }
            public string stateID { get; set; }
            public string street { get; set; }
            public string telephone1 { get; set; }
            public string telephone2 { get; set; }
            public string visible { get; set; }
            public string zip { get; set; }
        }

        public class AddressService
        {
            public string city { get; set; }
            public string countryID { get; set; }
            public string email1 { get; set; }
            public string email2 { get; set; }
            public string ID { get; set; }
            public string lgaID { get; set; }
            public string log_staff { get; set; }
            public string reg_date { get; set; }
            public string stateID { get; set; }
            public string street { get; set; }
            public string telephone1 { get; set; }
            public string telephone2 { get; set; }
            public string visible { get; set; }
            public string zip { get; set; }
        }

        public class Applicant
        {
            public string ID { get; set; }
            public string xname { get; set; }
            public string address { get; set; }
            public string xemail { get; set; }
            public string xmobile { get; set; }
            public string nationality { get; set; }
            public string log_staff { get; set; }
            public string visible { get; set; }
        }

        public class Assignment_info
        {
            public string ID { get; set; }
            public string date_of_assignment { get; set; }
            public string assignee_name { get; set; }
            public string assignee_address { get; set; }
            public string assignee_nationality { get; set; }
            public string assignor_name { get; set; }
            public string assignor_address { get; set; }
            public string assignor_nationality { get; set; }
            public string log_staff { get; set; }
            public string visible { get; set; }
        }

        public class Inventor
        {
            public string ID { get; set; }
            public string xname { get; set; }
            public string address { get; set; }
            public string xemail { get; set; }
            public string xmobile { get; set; }
            public string nationality { get; set; }
            public string log_staff { get; set; }
            public string visible { get; set; }
        }
        public class Country
        {
            public string code { get; set; }
            public string ID { get; set; }
            public string name { get; set; }
        }

        public class Lga
        {
            public string ID { get; set; }
            public string name { get; set; }
            public string stateID { get; set; }
        }

        public class NClass
        {
            public string xdescription { get; set; }
            public string xID { get; set; }
            public string xtype { get; set; }
        }

        public class PtInfo
        {
            public string xID { get; set; }
            public string reg_number { get; set; }
            public string xtype { get; set; }
            public string title_of_invention { get; set; }
            public string pt_desc { get; set; }
            public string spec_doc { get; set; }
            public string loa_no { get; set; }
            public string loa_doc { get; set; }
            public string claim_no { get; set; }
            public string claim_doc { get; set; }
            public string pct_no { get; set; }
            public string pct_doc { get; set; }
            public string doa_no { get; set; }
            public string doa_doc { get; set; }
            public string log_staff { get; set; }
            public string reg_date { get; set; }
            public string xvisible { get; set; }
        }

        public class Priority_info
        {
            public string xID { get; set; }
            public string countryID { get; set; }
            public string app_no { get; set; }
            public string xdate { get; set; }
            public string log_staff { get; set; }
            public string xvisible { get; set; }
        }


        public class Representative
        {
            public string ID { get; set; }
            public string agent_code { get; set; }
            public string xname { get; set; }
            public string nationality { get; set; }
            public string country { get; set; }
            public string state { get; set; }
            public string address { get; set; }
            public string xemail { get; set; }
            public string xmobile { get; set; }
            public string log_staff { get; set; }
            public string reg_date { get; set; }
            public string visible { get; set; }
        }

        public class Stage
        {
            public string amt { get; set; }
            public string applicantID { get; set; }
            public string data_status { get; set; }
            public string ID { get; set; }
            public string reg_date { get; set; }
            public string stage { get; set; }
            public string status { get; set; }
            public string validationID { get; set; }
        }

        public class State
        {
            public string countryID { get; set; }
            public string ID { get; set; }
            public string name { get; set; }
        }

        public class SWallet
        {
            public string ID { get; set; }
            public string log_officer { get; set; }
            public string mark_infoID { get; set; }
            public string reg_date { get; set; }
            public string search_cri { get; set; }
            public string search_str { get; set; }
            public string xclass { get; set; }
            public string visible { get; set; }
        }

        public class PtOffice
        {
            public string admin_status { get; set; }
            public string data_status { get; set; }
            public string ID { get; set; }
            public string pwalletID { get; set; }
            public string reg_date { get; set; }
            public string xcomment { get; set; }
            public string xdoc1 { get; set; }
            public string xdoc2 { get; set; }
            public string xdoc3 { get; set; }
            public string xofficer { get; set; }
            public string xvisible { get; set; }
        }
    }
}