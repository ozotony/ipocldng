using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Ipong.Classes
{
    public class ScardManager
    {
        

        public string Connect()
        {
            return ConfigurationManager.ConnectionStrings["cldConnectionString"].ConnectionString;
        }

        public List<Classes.XObjs.Scard> GenerateGuidNum(int amt, int cnt)
        {
          List<Classes.XObjs.Scard> lt_scards = new List<Classes.XObjs.Scard>();
          

          string xvisible = "1"; string xsync = "0"; string xreg_date=DateTime.Now.ToString("yyyy-MM-dd");
          string xlogstaff = "1"; string xvalid = "1";
          for (int i = 0; i < cnt; i++)
          {
              Classes.XObjs.Scard sc = new Classes.XObjs.Scard();
              //string x= Guid.NewGuid().ToString("n").Substring(0, amt).ToUpper();
              sc.xnum = Guid.NewGuid().ToString("n").Substring(0, amt).ToUpper();
              sc.xlogstaff = xlogstaff;
              sc.xreg_date = xreg_date;
              sc.xsync = xsync;
              sc.xvisible = xvisible;
              sc.xvalid = xvalid;
              if (!lt_scards.Contains(sc))
              {
                  lt_scards.Add(sc);
              }
          }
            return lt_scards;
        }

        public string addScards(List<Classes.XObjs.Scard> lt_scards)
        {
            int succ_cnt = 0;
            foreach (Classes.XObjs.Scard s in lt_scards)
            {
                string connectionString = this.Connect();
                
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO scard (xnum,xvalid,xlogstaff,xreg_date,xvisible,xsync) VALUES (@xnum,@xvalid,@xlogstaff,@xreg_date,@xvisible,@xsync) SELECT SCOPE_IDENTITY()";
                connection.Open();
                command.Parameters.Add("@xnum", SqlDbType.NVarChar);
                command.Parameters.Add("@xvalid", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@xlogstaff", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@xreg_date", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@xvisible", SqlDbType.NVarChar, 10);
                command.Parameters.Add("@xsync", SqlDbType.VarChar, 10);
                command.Parameters["@xnum"].Value = s.xnum;
                command.Parameters["@xvalid"].Value = s.xvalid;
                command.Parameters["@xlogstaff"].Value = s.xlogstaff;
                command.Parameters["@xreg_date"].Value = s.xreg_date;
                command.Parameters["@xvisible"].Value = s.xvisible;
                command.Parameters["@xsync"].Value = s.xsync;
                succ_cnt +=Convert.ToInt32(command.ExecuteScalar());
                connection.Close();               
            }
            return succ_cnt.ToString();
        }

    }
}