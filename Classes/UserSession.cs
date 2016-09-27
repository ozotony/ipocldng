using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ipong.Classes
{
    public static class UserSession
    {
        public static String ID
        {
            set { HttpContext.Current.Session["Id"] = value; }
            get { return (String)HttpContext.Current.Session["Id"]; }
        }
    }
}