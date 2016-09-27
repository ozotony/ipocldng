using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for Approve2
    /// </summary>
    public class Approve2 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var pp = context.Request["vid"];
            string message = "";
            String dd = "";
            string vid2 = Convert.ToString(pp);
            Ipong.Classes.Retriever kp = new Ipong.Classes.Retriever();

            int vmax = kp.getMaxSysId();
            vmax = vmax + 1;
            String vsys_id = "CLD/RA/0" + vmax;


            kp.updateRegistrationSysID2(vid2, vsys_id);

            kp.updateRegistrationSysID4(vid2, "APPROVED");

            JavaScriptSerializer ser = new JavaScriptSerializer();


            message = "success";



            context.Response.ContentType = "application/json";
            context.Response.Write(ser.Serialize(message));
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