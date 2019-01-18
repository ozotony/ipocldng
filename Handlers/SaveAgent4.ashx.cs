using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Mail;
using System.Text;
using System.Web.Script.Serialization;
using Ipong.InterSwitch.PayDirect.Classes;
using System.Configuration;
using Ipong.Classes;

namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for SaveAgent4
    /// </summary>
    public class SaveAgent4 : IHttpHandler
    {

        Hasher hash = new Hasher();
        public void ProcessRequest(HttpContext context)
        {
            string new_hash = "";
            //  string ccode = ConfigurationManager.AppSettings["ccode"]; string xcode = ConfigurationManager.AppSettings["xcode"];

            JavaScriptSerializer ser = new JavaScriptSerializer();
            var pp = context.Request["vv"];

            Register dd = ser.Deserialize<Register>(pp);

            //   dd.reg_date = DateTime.Now.ToShortDateString();

            //  dd.password = hash.GetGetSHA512String(ccode + dd.password + xcode);

            if (context.Request.Files.Count > 0)
            {
                var files = new List<string>();



                // interate the files and save on the server
                foreach (string file in context.Request.Files)
                {
                    if (file == "FileUpload")
                    {

                        var postedFile = context.Request.Files[file];
                        var vfile = postedFile.FileName.Replace("\"", string.Empty).Replace("'", string.Empty);
                        vfile = Stp(vfile);
                        string FileName = context.Server.MapPath("~/admin/ag_docz/" + vfile);
                        //   dd.cac_file = "/images/" + vfile;

                        dd.cac_file = "admin/ag_docz/" + vfile;

                        postedFile.SaveAs(FileName);



                    }

                    if (file == "FileUpload2")
                    {

                        var postedFile = context.Request.Files[file];
                        var vfile = postedFile.FileName.Replace("\"", string.Empty).Replace("'", string.Empty);
                        vfile = Stp(vfile);
                        string FileName = context.Server.MapPath("~/admin/ag_docz/" + vfile);

                        //  dd.Letter_Intro_file = "/images/" + vfile;
                        dd.Letter_Intro_file = "admin/ag_docz/" + vfile;

                        postedFile.SaveAs(FileName);

                    }

                    if (file == "FileUpload3")
                    {

                        var postedFile = context.Request.Files[file];
                        var vfile = postedFile.FileName.Replace("\"", string.Empty).Replace("'", string.Empty);
                        vfile = Stp(vfile);
                        string FileName = context.Server.MapPath("~/admin/ag_docz/" + vfile);
                        //   dd.passport_file = "/images/" + vfile;
                        dd.passport_file = "admin/ag_docz/" + vfile;

                        postedFile.SaveAs(FileName);

                    }
                    //  dd.File_path = "/Images/Patient/" + vfile;




                }
            }
            GetData gg = new GetData();
            Registration ss = new Registration();

            string sp = "";
            sp = ss.updateAgProfileDocz3( dd.xid);

            Ipong.Classes.Retriever kp = new Ipong.Classes.Retriever();




            context.Response.ContentType = "application/json";
            context.Response.Write(ser.Serialize(sp));
        }

        public string Stp(string s)
        {
            var sb = new StringBuilder();
            foreach (char c in s)
            {
                if (!char.IsWhiteSpace(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}