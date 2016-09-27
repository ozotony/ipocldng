using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for GetCountry
    /// </summary>
    public class GetCountry : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            GetData pp = new GetData();
            List<Country> kk = pp.GetCountry();


            JavaScriptSerializer ser = new JavaScriptSerializer();
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