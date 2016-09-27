using Ipong.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for GetEmailCount
    /// </summary>
    public class GetEmailCount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var pp = context.Request["vid"];
            String dd = "";
            JavaScriptSerializer ser = new JavaScriptSerializer();
            Retriever pp2 = new Retriever();
          //  pp2.updateEmail(pp);
            Int32 kk = pp2.getEmailCount2(pp);




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