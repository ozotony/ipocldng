using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for BlockAgent
    /// </summary>
    public class BlockAgent : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var pp = context.Request["vid"];
            string message = "";
            String dd = "";
            string vid2 = Convert.ToString(pp);


            Ipong.Classes.Retriever kp = new Ipong.Classes.Retriever();

            Ipong.Classes.XObjs.Registration pp4 = kp.getRegistrationBySubagentRegistrationID(vid2);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            //  XObjs.Registration px = kp.getRegistrationBySubagentRegistrationID(vid2);




            kp.updateRegistrationSysID5(vid2, "0");


         

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