using Ipong.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for GetCldClass
    /// </summary>
    public class GetCldClass : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
          
            JavaScriptSerializer ser = new JavaScriptSerializer();

            zues pp2 = new zues();
            List<NClass>  kk = pp2.getJNationalClasses();



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