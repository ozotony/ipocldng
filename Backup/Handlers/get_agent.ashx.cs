using System;
using System.Web;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Ipong.Classes;
using Ipong.InterSwitch.PayDirect.Classes;

namespace Ipong.Handlers
{
    public class get_agent : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public string coy_code = "";
        public string xcode = "";
        public string new_hash = "";

        XObjs.Registration xagent = new XObjs.Registration();
        XObjs.Subagent xsub_agent = new XObjs.Subagent();
        Registration reg = new Registration();
        Retriever ret = new Retriever();
        Hasher hash = new Hasher();


        public string email = ""; public string sys_id = "";
        public string isagent = "";
        public string json = "";
        public int succ = 0;
        public JavaScriptSerializer js = new JavaScriptSerializer();


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            try
            {
                if (
                    (context.Request.Form["email"] != null) && (context.Request.Form["email"] != "") &&
                   (context.Request.Form["sys_id"] != null) && (context.Request.Form["sys_id"] != "") &&
                    (context.Request.Form["isagent"] != null) && (context.Request.Form["isagent"] != "")
                    )
                {
                    email = context.Request.Form["email"].ToString();
                    sys_id = context.Request.Form["sys_id"].ToString();
                    isagent = context.Request.Form["isagent"].ToString();

                    try
                    {
                        context.Session["agentType"] = isagent;
                        if (isagent == "Agent")
                        {
                            xagent = ret.getRegistrationBySysID(email, sys_id);
                            if ((xagent.xid != null) && (xagent.xid != ""))
                            {   //context.Session["MemberInfo"] = mi;
                                context.Session["agentID"] = xagent.xid;
                                context.Session["agentEmail"] = email;
                                context.Session["sys_id"] = sys_id;
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
                            xsub_agent = ret.getSubAgentBySysID(email, sys_id);
                            if ((xsub_agent.xid != null) && (xsub_agent.xid != ""))
                            {
                                //context.Session["MemberInfo"] = mi;
                                context.Session["agentID"] = xsub_agent.xid;
                                context.Session["agentEmail"] = email;
                                context.Session["sys_id"] = sys_id;

                                json = js.Serialize(xsub_agent);
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
                        // json = js.Serialize(ex1);
                        json = "{\"msg\":" + json + "}";
                        context.Response.Write(json);
                    }
                }


            }
            catch (Exception ex)
            {
                string ex1 = ex.ToString();
                json = js.Serialize("Could not complete the process at this time!");
                // json = js.Serialize(ex1);
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