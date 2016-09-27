using Ipong.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for GetRegistration4
    /// </summary>
    public class GetRegistration4 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();

            Retriever pp2 = new Retriever();

            List<XObjs.Registration> kk = pp2.getAllRegistrations44();



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