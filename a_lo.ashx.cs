using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Ipong
{
    /// <summary>
    /// Summary description for lo
    /// </summary>
    public class a_lo : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Session["pwalletID"] = null;
            context.Response.Redirect("./a_login.aspx");
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