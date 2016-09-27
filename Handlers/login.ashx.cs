using System;
using System.Web;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Ipong.Classes;
using Ipong.InterSwitch.PayDirect.Classes;
using System.Configuration;

namespace Ipong.Handlers
{
    public class login : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public string coy_code = "";
        public string xcode = "";
        public string new_hash = "";

        XObjs.Registration xagent = new XObjs.Registration();
        XObjs.Subagent xsub_agent = new XObjs.Subagent();
        Registration reg = new Registration();
        Retriever ret = new Retriever();
        Hasher hash = new Hasher();


        public string email = ""; public string xpass = "";
        public string isagent = "";
        public string json = "";
        public int succ = 0;
        public JavaScriptSerializer js = new JavaScriptSerializer();
        public string ccode = "";
        public string x_code = "";
      //  protected InterSwitch.PayDirect.Classes.Hasher hash = new InterSwitch.PayDirect.Classes.Hasher();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            ccode = ConfigurationManager.AppSettings["ccode"];
            x_code = ConfigurationManager.AppSettings["xcode"]; 
            try
            {
                if (
                    (context.Request.Form["email"] != null) && (context.Request.Form["email"] != "") &&
                   (context.Request.Form["xpass"] != null) && (context.Request.Form["xpass"] != "") &&
                    (context.Request.Form["isagent"] != null) && (context.Request.Form["isagent"] != "")
                    )
                {
                    email = context.Request.Form["email"].ToString();
                    xpass = context.Request.Form["xpass"].ToString();
                    isagent = context.Request.Form["isagent"].ToString();

                    try
                    {
                        context.Session["agentType"] = isagent;
                        if (isagent == "Agent")
                        {
                            new_hash = hash.GetGetSHA512String(ccode + xpass + x_code);
                           // xagent = ret.getRegistrationByLogin(email, xpass);

                            xagent = ret.getRegistrationByLogin(email, new_hash);
                            if ((xagent.xid != null) && (xagent.xid != ""))
                            {
                                //context.Session["MemberInfo"] = mi;
                                context.Session["agentID"] = xagent.xid;
                                context.Session["agentEmail"] = email;
                                context.Session["agentPin"] = xpass;
                                json = js.Serialize(xagent);
                                json = "{\"msg\":" + json + "}";
                                context.Response.Write(json);
                            }
                            else
                            {
                                json = js.Serialize("Could not complete the process at this time!");
                                json = "{\"msg\":" + json + "}";
                                context.Response.Write(json);
                            }
                        }
                        else
                        {
                            xsub_agent = ret.getSubAgentByLogin(email, xpass);
                            if ((xsub_agent.xid != null) && (xsub_agent.xid != ""))
                            {
                                //context.Session["MemberInfo"] = mi;
                                context.Session["agentID"] = xsub_agent.xid;
                                context.Session["agentEmail"] = email;
                                context.Session["agentPin"] = xpass;

                                json = js.Serialize(xagent);
                                json = "{\"msg\":" + json + "}";
                                context.Response.Write(json);
                            }
                            else
                            {
                                json = js.Serialize("Could not complete the process at this time!");
                                json = "{\"msg\":" + json + "}";
                                context.Response.Write(json);
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        string ex1 = ex.ToString();
                        json = js.Serialize("Could not complete the process at this time!");
                        json = "{\"msg\":" + json + "}";
                        context.Response.Write(json);
                    }
                }


            }
            catch (Exception ex)
            {
                string ex1 = ex.ToString();
                json = js.Serialize("Could not complete the process at this time!");
                json = "{\"msg\":" + json + "}";
                context.Response.Write(json);
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