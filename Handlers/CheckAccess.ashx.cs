using Ipong.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for CheckAccess
    /// </summary>
    public class CheckAccess : IHttpHandler, System.Web.SessionState.IRequiresSessionState, System.Web.SessionState.IReadOnlySessionState
    {
        public JavaScriptSerializer js = new JavaScriptSerializer();
        public void ProcessRequest(HttpContext context)
        {

            if (UserSession.ID != null)
            {


               string  json2 = js.Serialize("true");
                context.Response.ContentType = "application/json";
                // json = "{\"msg\":" + json + "}";
                context.Response.Write(json2);
            }
            else
            {


                string json2 = js.Serialize("false");
                context.Response.ContentType = "application/json";
                // json = "{\"msg\":" + json + "}";
                context.Response.Write(json2);
            }
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