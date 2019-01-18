using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Ipong.Classes;
namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for GetCertificate9
    /// </summary>
    public class GetCertificate9 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var pp = context.Request["vid"];
            String dd = "";
            JavaScriptSerializer ser = new JavaScriptSerializer();

            Ipong.Classes.Retriever pp2 = new Ipong.Classes.Retriever();
            List<PtInfo> kk2 = pp2.getPtInfoByPwalletID2(pp);
            List<Stage> kk = pp2.getStageByUserIDAdmin2(kk2[0].log_staff);
          
            List<Applicant> kk3 = pp2.getApplicantByvalidationID(kk[0].ID);

            Reports mm = new Reports();
            mm.kk = kk[0];
            mm.kk2 = kk2[0];
            mm.kk3 = kk3[0];


            // XObjs.Registration kk = pp2.getRegistrationBySubagentRegistrationID(pp);



            context.Response.ContentType = "application/json";
            context.Response.Write(ser.Serialize(mm));
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