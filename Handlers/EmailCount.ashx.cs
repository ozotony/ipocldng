using Ipong.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for EmailCount
    /// </summary>
    public class EmailCount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
              var pp2 = context.Request["vv"];

              JavaScriptSerializer ser = new JavaScriptSerializer();
              var pp = context.Request["vv"];

              Email2 dd = ser.Deserialize<Email2>(pp2);
            GetData pp3 = new GetData();
            Int32 kk =Convert.ToInt32( pp3.getACnt(dd.Email));


          //  JavaScriptSerializer ser = new JavaScriptSerializer();
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