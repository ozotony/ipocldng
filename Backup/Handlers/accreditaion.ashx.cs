using System;
using System.Web;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Configuration;
using Ipong.Classes;
using Ipong.InterSwitch.PayDirect.Classes;

namespace Ipong.Handlers
{
    public class accreditaion : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        XObjs.Registration xagent = new XObjs.Registration();
        XObjs.Subagent xsub_agent = new XObjs.Subagent();
        Registration reg = new Registration();
        Retriever ret = new Retriever();
        Hasher hash = new Hasher();

        public string isagent = "";
        public string ccode = "";
        public string xcode = "";
        public string new_hash = "";

        public string json = "";
        public int succ = 0;
        public JavaScriptSerializer js = new JavaScriptSerializer();


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            ccode = ConfigurationManager.AppSettings["ccode"]; xcode = ConfigurationManager.AppSettings["xcode"];
            try
            {                
                    new_hash = hash.GetGetSHA512String(ccode + context.Request.Form["xpass"] + xcode);
                    try
                    {
                        context.Session["agentType"] = context.Request.Form["isagent"];
                        if (context.Request.Form["isagent"] == "Agent")
                        {
                            xagent = ret.getRegistrationByID(context.Request.Form["xid"].ToString());
                        if ((context.Request.Form["email"] != null) && (context.Request.Form["email"] != ""))
                            {
                            xagent.Email = context.Request.Form["email"].ToString();
                           }
                        if ((context.Request.Form["sys_id"] != null) && (context.Request.Form["sys_id"] != ""))
                        {
                            xagent.Sys_ID = context.Request.Form["sys_id"].ToString();
                        }
                        if ((context.Request.Form["isagent"] != null) && (context.Request.Form["isagent"] != ""))
                            {
                            isagent = context.Request.Form["isagent"].ToString();
                             }
                             if ((context.Request.Form["firstname"] != null) && (context.Request.Form["firstname"] != ""))
                            {
                            xagent.Firstname = context.Request.Form["firstname"].ToString();
                              }
                                  if ((context.Request.Form["surname"] != null) && (context.Request.Form["surname"] != ""))
                            {
                            xagent.Surname = context.Request.Form["surname"].ToString();
                             }
                                  if ((context.Request.Form["mobile"] != null) && (context.Request.Form["mobile"] != ""))
                            {
                            xagent.PhoneNumber = context.Request.Form["mobile"].ToString();
                            }
                                  if ((context.Request.Form["dob"] != null) && (context.Request.Form["dob"] != ""))
                            {
                            xagent.DateOfBrith = context.Request.Form["dob"].ToString();
                               }
                                  if ((context.Request.Form["coy_name"] != null) && (context.Request.Form["coy_name"] != ""))
                            {
                            xagent.CompanyName = context.Request.Form["coy_name"].ToString();
                            }
                                  if ((context.Request.Form["coy_addy"] != null) && (context.Request.Form["coy_addy"] != ""))
                            {
                            xagent.CompanyAddress = context.Request.Form["coy_addy"].ToString();
                              }
                                  if ((context.Request.Form["coy_web"] != null) && (context.Request.Form["coy_web"] != ""))
                            {
                            xagent.CompanyWebsite = context.Request.Form["coy_web"].ToString();
                             }
                                  if ((context.Request.Form["doi"] != null) && (context.Request.Form["doi"] != ""))
                            {
                            xagent.IncorporatedDate = context.Request.Form["doi"].ToString();
                            }
                                  if ((context.Request.Form["cont_fullname"] != null) && (context.Request.Form["cont_fullname"] != ""))
                            {
                            xagent.ContactPerson = context.Request.Form["cont_fullname"].ToString();
                             }
                                  if ((context.Request.Form["cont_mobile"] != null) && (context.Request.Form["cont_mobile"] != ""))
                            {
                            xagent.ContactPersonPhone = context.Request.Form["cont_mobile"].ToString();
                             }
                                  if ((context.Request.Form["xid"] != null) && (context.Request.Form["xid"] != ""))
                            {
                                xagent.xid = context.Request.Form["xid"].ToString();
                             }
                                 
                            xagent.xpassword = new_hash;
                            
                            xagent.xreg_date = DateTime.Now.ToString("yyyy-MM-dd");

                            succ = reg.updateRegistration(xagent);
                            if (succ > 0)
                            {
                                context.Session["c_reg"] = xagent;
                                context.Session["agentID"] = xagent.xid;
                                context.Session["agentEmail"] = xagent.Email;
                                context.Session["sys_id"] = xagent.Sys_ID;
                                context.Session["coy_name"] = xagent.CompanyName;
                                json = js.Serialize("updated");
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
                           
                            xsub_agent = ret.getSubAgentByID(context.Request.Form["xid"].ToString());
                            if ((context.Request.Form["email"] != null) && (context.Request.Form["email"] != ""))
                            {
                            xsub_agent.Email = context.Request.Form["email"].ToString();
                            }
                              if ((context.Request.Form["sys_id"] != null) && (context.Request.Form["sys_id"] != ""))
                            {
                            xsub_agent.Sys_ID = context.Request.Form["sys_id"].ToString();
                                 }
                              if ((context.Request.Form["isagent"] != null) && (context.Request.Form["isagent"] != ""))
                            {
                            isagent = context.Request.Form["isagent"].ToString();
                                   }
                              if ((context.Request.Form["firstname"] != null) && (context.Request.Form["firstname"] != ""))
                            {
                            xsub_agent.Firstname = context.Request.Form["firstname"].ToString();
                             }
                              if ((context.Request.Form["surname"] != null) && (context.Request.Form["surname"] != ""))
                            {
                                  xsub_agent.Surname = context.Request.Form["surname"].ToString();
                             }
                              if ((context.Request.Form["mobile"] != null) && (context.Request.Form["mobile"] != ""))
                            {
                                  xsub_agent.Telephone = context.Request.Form["mobile"].ToString();
                             }
                              if ((context.Request.Form["dob"] != null) && (context.Request.Form["dob"] != ""))
                            {
                                  xsub_agent.DateOfBrith = context.Request.Form["dob"].ToString();
                             }
                              if ((context.Request.Form["xid"] != null) && (context.Request.Form["xid"] != ""))
                              {
                                  xsub_agent.xid = context.Request.Form["xid"].ToString();
                              } 
                            xsub_agent.xpassword = new_hash;
                            
                            xsub_agent.xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
                            succ = reg.updateSubAgent(xsub_agent);

                            if (succ > 0)
                            {
                                context.Session["c_sub"] = xsub_agent;
                                context.Session["agentID"] = xsub_agent.xid;
                                context.Session["agentEmail"] = xsub_agent.Email;
                                context.Session["sys_id"] = xsub_agent.Sys_ID;

                                context.Session["c_sub_reg"] = ret.getRegistrationBySubagentRegistrationID(xsub_agent.RegistrationID);

                                json = js.Serialize("updated");
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