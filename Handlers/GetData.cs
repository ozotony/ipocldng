using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Ipong.Handlers
{
    public class GetData
    {

        public string Connect()
        {
            return ConfigurationManager.ConnectionStrings["homeConnectionString"].ConnectionString;
        }

        public List<Country> GetCountry()
        {
            List<Country> list = new List<Country>();
            string str = "Xavier";
            // string path = serverpath + @"\Handlers\bf.kez";


            string str4 = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("select id,name from country", connection);
            connection.Open();
            SqlDataReader reader2 = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader2.Read())
            {
                Country item = new Country
                {
                    code = reader2["id"].ToString(),
                    name = reader2["name"].ToString()

                };
                list.Add(item);
            }
            reader2.Close();

            return list;
        }

        public List<State> GetState(string id)
        {

            List<State> list = new List<State>();
            string str = "Xavier";
            // string path = serverpath + @"\Handlers\bf.kez";


            string str4 = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("select ID,name from state WHERE countryID='" + id + "' ", connection);
            connection.Open();
            SqlDataReader reader2 = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader2.Read())
            {
                State item = new State
                {
                    id = reader2["ID"].ToString(),
                    name = reader2["name"].ToString()

                };
                list.Add(item);
            }
            reader2.Close();

            return list;
        }

      


        public String  getACnt(string vemail)
        {
            int num = 0;
           // SqlConnection connection = new SqlConnection(this.ConnectXpay());
            
            string connectionString = this.Connect();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("select count(Email) as cnt from registrations where Email='" + vemail + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                num = Convert.ToInt32(reader["cnt"]);
            }
            reader.Close();
            return Convert.ToString(num);
        }

        public string addAgent(Register ss)
        {
           string str = DateTime.Today.Date.ToString("yyyy-MM-dd");
          //  string str = DateTime.Today.Date.ToString("MM/dd/yyyy");
            string str2 = DateTime.Now.ToLongTimeString();
            string connectionString = this.Connect();
            string str4 = "0";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO registrations (AccrediationType,Firstname,Surname,Email,xpassword,DateOfBrith,IncorporatedDate,Nationality,PhoneNumber,CompanyName,CompanyAddress,ContactPerson,ContactPersonPhone,Certificate,Introduction,xreg_date,xstatus,logo,Principal,xvisible) VALUES (@AccrediationType,@Firstname,@Surname,@Email,@xpassword,@DateOfBrith,@IncorporatedDate,@Nationality,@PhoneNumber,@CompanyName,@CompanyAddress,@ContactPerson,@ContactPersonPhone,@Certificate,@Introduction,@xreg_date,@xstatus,@logo,@Principal,@xvisible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@AccrediationType", SqlDbType.NVarChar);
            command.Parameters.Add("@Firstname", SqlDbType.NVarChar);
            command.Parameters.Add("@Surname", SqlDbType.NVarChar);
            command.Parameters.Add("@Email", SqlDbType.NVarChar);
            command.Parameters.Add("@xpassword", SqlDbType.NVarChar);
            command.Parameters.Add("@DateOfBrith", SqlDbType.NVarChar);
            command.Parameters.Add("@IncorporatedDate", SqlDbType.NVarChar);
            command.Parameters.Add("@Nationality", SqlDbType.NVarChar);
            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar);
            command.Parameters.Add("@CompanyName", SqlDbType.NVarChar);
            command.Parameters.Add("@CompanyAddress", SqlDbType.NVarChar);
            command.Parameters.Add("@ContactPerson", SqlDbType.NVarChar);
            command.Parameters.Add("@ContactPersonPhone", SqlDbType.NVarChar);
            command.Parameters.Add("@Certificate", SqlDbType.Text);
            command.Parameters.Add("@Introduction", SqlDbType.Text);
            command.Parameters.Add("@Principal", SqlDbType.Text);

            
            command.Parameters.Add("@xreg_date", SqlDbType.NVarChar);

            command.Parameters.Add("@xstatus", SqlDbType.NVarChar);
            command.Parameters.Add("@logo", SqlDbType.NVarChar);
             command.Parameters.Add("@xvisible", SqlDbType.NVarChar);

      

            command.Parameters["@AccrediationType"].Value = ss.AccountType;
            command.Parameters["@Firstname"].Value = ss.FirstName;
            command.Parameters["@Surname"].Value = ss.Surname;
            command.Parameters["@Email"].Value = ss.Email;
            command.Parameters["@xpassword"].Value = ss.password;
            command.Parameters["@DateOfBrith"].Value = ss.dob;
            command.Parameters["@IncorporatedDate"].Value = ss.DobIncorp;
            command.Parameters["@Nationality"].Value = ss.Nationality;
            command.Parameters["@PhoneNumber"].Value = ss.CompPhone;
            command.Parameters["@CompanyName"].Value = ss.CompName;
            command.Parameters["@CompanyAddress"].Value = ss.CompAddress;
            command.Parameters["@ContactPerson"].Value = ss.CompPerson;
            command.Parameters["@ContactPersonPhone"].Value = ss.ContactPhone;
            command.Parameters["@Certificate"].Value = ss.cac_file;
            command.Parameters["@Introduction"].Value = ss.Letter_Intro_file;

            command.Parameters["@xreg_date"].Value = ss.reg_date;
            command.Parameters["@xstatus"].Value = "0";
            command.Parameters["@logo"].Value = ss.passport_file;

            command.Parameters["@xvisible"].Value = "1";

            command.Parameters["@Principal"].Value = ss.passport_file;


            foreach (SqlParameter Parameter in command.Parameters)
            {
                if (Parameter.Value == null)
                {
                    Parameter.Value = DBNull.Value;
                }
            }


            str4 = command.ExecuteScalar().ToString();
            connection.Close();

          //  sendmail(ss.CompName, ss.Email);
            return str4;
        }


        public string addAgent2(Register ss)
        {
            string str = DateTime.Today.Date.ToString("yyyy-MM-dd");
            
            string str2 = DateTime.Now.ToLongTimeString();
            string connectionString = this.Connect();
            string str4 = "0";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO registrations (AccrediationType,Firstname,Surname,Email,xpassword,DateOfBrith,IncorporatedDate,Nationality,PhoneNumber,CompanyName,CompanyAddress,ContactPerson,ContactPersonPhone,Certificate,Introduction,xreg_date,xstatus,logo,Principal,xvisible) VALUES (@AccrediationType,@Firstname,@Surname,@Email,@xpassword,@DateOfBrith,@IncorporatedDate,@Nationality,@PhoneNumber,@CompanyName,@CompanyAddress,@ContactPerson,@ContactPersonPhone,@Certificate,@Introduction,@xreg_date,@xstatus,@logo,@Principal,@xvisible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@AccrediationType", SqlDbType.NVarChar);
            command.Parameters.Add("@Firstname", SqlDbType.NVarChar);
            command.Parameters.Add("@Surname", SqlDbType.NVarChar);
            command.Parameters.Add("@Email", SqlDbType.NVarChar);
            command.Parameters.Add("@xpassword", SqlDbType.NVarChar);
            command.Parameters.Add("@DateOfBrith", SqlDbType.NVarChar);
            command.Parameters.Add("@IncorporatedDate", SqlDbType.NVarChar);
            command.Parameters.Add("@Nationality", SqlDbType.NVarChar);
            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar);
            command.Parameters.Add("@CompanyName", SqlDbType.NVarChar);
            command.Parameters.Add("@CompanyAddress", SqlDbType.NVarChar);
            command.Parameters.Add("@ContactPerson", SqlDbType.NVarChar);
            command.Parameters.Add("@ContactPersonPhone", SqlDbType.NVarChar);
            command.Parameters.Add("@Certificate", SqlDbType.Text);
            command.Parameters.Add("@Introduction", SqlDbType.Text);
            command.Parameters.Add("@Principal", SqlDbType.Text);


            command.Parameters.Add("@xreg_date", SqlDbType.NVarChar);

            command.Parameters.Add("@xstatus", SqlDbType.NVarChar);
            command.Parameters.Add("@logo", SqlDbType.NVarChar);
            command.Parameters.Add("@xvisible", SqlDbType.NVarChar);



            command.Parameters["@AccrediationType"].Value = ss.AccountType;
            command.Parameters["@Firstname"].Value = ss.FirstName;
            command.Parameters["@Surname"].Value = ss.Surname;
            command.Parameters["@Email"].Value = ss.Email;
            command.Parameters["@xpassword"].Value = ss.password;
            command.Parameters["@DateOfBrith"].Value = ss.dob;
            command.Parameters["@IncorporatedDate"].Value = ss.DobIncorp;
            command.Parameters["@Nationality"].Value = ss.Nationality;
            command.Parameters["@PhoneNumber"].Value = ss.CompPhone;
            command.Parameters["@CompanyName"].Value = ss.CompName;
            command.Parameters["@CompanyAddress"].Value = ss.CompAddress;
            command.Parameters["@ContactPerson"].Value = ss.CompPerson;
            command.Parameters["@ContactPersonPhone"].Value = ss.ContactPhone;
            command.Parameters["@Certificate"].Value = ss.cac_file;
            command.Parameters["@Introduction"].Value = ss.Letter_Intro_file;

            command.Parameters["@xreg_date"].Value = ss.reg_date;
            command.Parameters["@xstatus"].Value = "0";
            command.Parameters["@logo"].Value = ss.passport_file;

            command.Parameters["@xvisible"].Value = "1";

            command.Parameters["@Principal"].Value = ss.passport_file;


            foreach (SqlParameter Parameter in command.Parameters)
            {
                if (Parameter.Value == null)
                {
                    Parameter.Value = DBNull.Value;
                }
            }


            str4 = command.ExecuteScalar().ToString();
            connection.Close();

            //  sendmail(ss.CompName, ss.Email);
            return str4;
        }


        public void sendmail(string vcompany,string vemail)
        {

            //var qq = gg2.tbl_inv_parameters.SingleOrDefault();
            //string path = qq.ImagePath;
            string email = vemail;
            //path = filename;

            string from = "anthony.ozoagu@gmail.com";

           // string to = "ozotony@yahoo.com";

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            mail.To.Add(email);
            mail.From = new MailAddress(from, "EINAO", System.Text.Encoding.UTF8);
            mail.Subject = "Agent Accreditation ";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "Dear " + vcompany + " <br/><br/><br/> <p> </p> <p/><p/> To complete the application process and gain unlimited access on online services, please click here <a href=http://localhost:4556/a_login.aspx>Log In </a>  to make payment for your accreditation  <br/>";

         //   mail.Body = "Dear " + vcompany + " <br/><br/><br/> <p> </p> <p/><p/> To complete the application process and gain unlimited access on online services, please click here   to make payment for your accreditation  <br/>";
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            //Attachment att = new Attachment(path);
            //mail.Attachments.Add(att);

           // SmtpClient client = new SmtpClient("smtp.gmail.com");
            SmtpClient client = new SmtpClient
            {
                Credentials = new NetworkCredential("anthony.ozoagu@gmail.com", "ozoTONY3"),
                Port =587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                 
                Timeout = 0x4e20
            };

            //Add the Creddentials- use your own email id and password

            //   client.Credentials = new System.Net.NetworkCredential(from, "ozoTONY3");
               client.Port = 587;
            //client.Port = 0x24b;


             //  client.EnableSsl = true;
               client.UseDefaultCredentials = false;
               client.Credentials = new NetworkCredential("anthony.ozoagu@gmail.com", "ozoTONY3");

             //  client.Credentials = new NetworkCredential("paymentsupport@einaosolutions.com", "Zues.4102.Hector");

             
            
            //   client.Host = "smtp.gmail.com";

            

            client.Send(mail);



        }
    }
}