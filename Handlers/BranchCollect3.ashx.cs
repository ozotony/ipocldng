using Ipong.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for BranchCollect3
    /// </summary>
    public class BranchCollect3 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var pp = context.Request["vv"];

            Branch_Collect dd7 = ser.Deserialize<Branch_Collect>(pp);
            Retriever dd2 = new Retriever();

           ser.MaxJsonLength = 2147483644;

            List<BranchCollect> pp2 = dd2.getBranchCollect(dd7.Agent_Code, dd7.TransactionId);

            context.Response.ContentType = "application/json";
            context.Response.Write(ser.Serialize(pp2));
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