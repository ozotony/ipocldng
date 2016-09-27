using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for Save_GenericApplication
    /// </summary>
    public class Save_GenericApplication : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string new_hash = "";
          //  string ccode = ConfigurationManager.AppSettings["ccode"]; string xcode = ConfigurationManager.AppSettings["xcode"];

            JavaScriptSerializer ser = new JavaScriptSerializer();
            var pp = context.Request["vv"];

            Register dd = ser.Deserialize<Register>(pp);

         

            if (context.Request.Files.Count > 0)
            {
                var files = new List<string>();



                // interate the files and save on the server
                foreach (string file in context.Request.Files)
                {
                    if (file == "FileUpload")
                    {

                        var postedFile = context.Request.Files[file];
                      //  var vfile = postedFile.FileName.Replace("\"", string.Empty).Replace("'", string.Empty);
                     //   vfile = Stp(vfile);
                      //  string FileName = context.Server.MapPath("~/admin/ag_docz/" + vfile);
                        //   dd.cac_file = "/images/" + vfile;

                      //  dd.cac_file = "admin/ag_docz/" + vfile;

                      //  postedFile.SaveAs(FileName);



                    }

                    if (file == "FileUpload2")
                    {

                        var postedFile = context.Request.Files[file];
                       

                    }

                    if (file == "FileUpload3")
                    {

                        var postedFile = context.Request.Files[file];
                      

                    }
                    //  dd.File_path = "/Images/Patient/" + vfile;




                }
            }
            GetData gg = new GetData();
            string sp = gg.addAgent(dd);

            Ipong.Classes.Retriever kp = new Ipong.Classes.Retriever();



          //  sendemail(dd.Email, dd.CompName, sp);
            try
            {
              //  kp.updateRegistrationSysID4(sp, "PENDING");
            }
            catch (Exception ee)
            {

            }
            context.Response.ContentType = "application/json";
            context.Response.Write(ser.Serialize(sp));
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