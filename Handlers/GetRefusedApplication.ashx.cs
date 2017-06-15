using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Ipong.Classes;

namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for GetRefusedApplication
    /// </summary>
    public class GetRefusedApplication : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var pp = context.Request["vid"];
            String dd = "";
            JavaScriptSerializer ser = new JavaScriptSerializer();

            Ipong.Classes.zues pp2 = new Ipong.Classes.zues();
            List<XObjs.Office_view> kk = pp2.getNew_MarkInfoRSX33("4", "Refused", pp, 1, 1);

        

            // XObjs.Registration kk = pp2.getRegistrationBySubagentRegistrationID(pp);



            context.Response.ContentType = "application/json";
            context.Response.Write(ser.Serialize(kk));
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