using Ipong.Classes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Ipong.Handlers
{
    /// <summary>
    /// Summary description for PortalA
    /// </summary>
    public class PortalA : IHttpHandler
    {
        string sp = "";
        public void ProcessRequest(HttpContext context)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();

            PortalAA pk = null;

            if (context.Request.Files.Count > 0)
            {
                var files = new List<string>();



                // interate the files and save on the server
                foreach (string file in context.Request.Files)
                {
                    if (file == "FileUpload")
                    {

                        var postedFile = context.Request.Files[file];
                        var vfile = postedFile.FileName.Replace("\"", string.Empty).Replace("'", string.Empty);
                        vfile = Stp(vfile);
                        string FileName = context.Server.MapPath("~/admin/ag_docz/" + vfile);
                        //   dd.cac_file = "/images/" + vfile;

                      //  dd.cac_file = "admin/ag_docz/" + vfile;

                        postedFile.SaveAs(FileName);

                          

                        string fileExtension =System.IO.Path.GetExtension(context.Request.Files[file].FileName);

                        if (fileExtension == ".xlsx")
                        {
                         // pk=  readExcel5(FileName);

                            pk = ImportToDataTable(FileName, "Sheet1");


                

                        }

                        else
                        {
                          pk=  readExcel4(FileName);

                        }


                    }

                  
                    
                   
                    //  dd.File_path = "/Images/Patient/" + vfile;




                }
            }
            context.Response.ContentType = "application/json";
            context.Response.Write(ser.Serialize(pk));
        }

        public string Stp(string s)
        {
            var sb = new StringBuilder();
            foreach (char c in s)
            {
                if (!char.IsWhiteSpace(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        public static PortalAA ImportToDataTable(string FilePath, string SheetName)
        {
            DataTable dt = new DataTable();
            FileInfo fi = new FileInfo(FilePath);
            PortalAA dd = null;

            // Check if the file exists
            if (!fi.Exists)
                throw new Exception("File " + FilePath + " Does Not Exists");

            using (ExcelPackage xlPackage = new ExcelPackage(fi))
            {
                // get the first worksheet in the workbook
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[SheetName];

                // Fetch the WorkSheet size
                ExcelCellAddress startCell = worksheet.Dimension.Start;
                ExcelCellAddress endCell = worksheet.Dimension.End;
                 dd = new PortalAA();
                // create all the needed DataColumn
                for (int col = startCell.Column; col <= endCell.Column; col++)
                    dt.Columns.Add(col.ToString());
                int rol = 1;
                // place all the data into DataTable
                for (int row = startCell.Row; row <= endCell.Row; row++)
                {
                    DataRow dr = dt.NewRow();
                    int x = 0;
                    for (int col = startCell.Column; col <= endCell.Column; col++)
                    {
                      //  sp =Convert.ToString( worksheet.Cells[row, col].Value); ;
                        if (row != 1)
                        {
                            dr[x++] = worksheet.Cells[row, col].Value;

                            if (col == 1)
                            {
                                dd.transID =Convert.ToString( worksheet.Cells[row, col].Value); ;

                            }

                            if (col == 2)
                            {
                                dd.amt = Convert.ToString(worksheet.Cells[row, col].Value); ;

                            }

                            if (col == 3)
                            {
                                dd.agt = Convert.ToString(worksheet.Cells[row, col].Value); ;

                            }

                            if (col == 4)
                            {
                                dd.applicantname = Convert.ToString(worksheet.Cells[row, col].Value); ;

                            }

                            if (col == 5)
                            {
                                dd.applicantemail = Convert.ToString(worksheet.Cells[row, col].Value); ;

                            }


                            if (col == 6)
                            {
                                dd.applicantpnumber = Convert.ToString(worksheet.Cells[row, col].Value); ;

                            }


                            if (col == 7)
                            {
                                dd.applicant_addy = Convert.ToString(worksheet.Cells[row, col].Value); ;

                            }

                            if (col == 8)
                            {
                                dd.product_title = Convert.ToString(worksheet.Cells[row, col].Value); ;

                            }

                            if (col == 9)
                            {
                                dd.item_code = Convert.ToString(worksheet.Cells[row, col].Value); ;

                            }


                            if (col == 10)
                            {
                                dd.pc= Convert.ToString(worksheet.Cells[row, col].Value); ;

                                dd.xgt = "xpay";
                                XObjs.Registration ap = null;
                                Retriever pp = new Retriever();
                                ap = pp.getRegistrationBySysID2(dd.agt);
                                if (ap != null)
                                {
                                    dd.agentname = ap.Surname;
                                    dd.agentemail = ap.Email;
                                    dd.agentpnumber = ap.PhoneNumber;
                                }



                            }
                        }
                    }
                   // dt.Rows.Add(dr);
                    rol = rol + 1;
                }
            }
            return dd;
        }
        public PortalAA readExcel4(string Filename)
        {
            XObjs.Registration ap = null;
            string path = Filename;
            string szConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
         //   string szConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\""; 
            OleDbConnection conn = new OleDbConnection(szConn);
            conn.Open();




            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", conn);
            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            string email = "";
            string user_name = "";
            string password = "";

            PortalAA dd2 = new PortalAA();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                // Result pp = new Result();
                //  string data = string.Format("F1:{0}, F2:{1}, F3:{2}", dr[0], dr[1], dr[2]);
                dd2.transID = Convert.ToString(dr[0]);
                dd2.amt = Convert.ToString(dr[1]);
                dd2.agt = Convert.ToString(dr[2]);
                dd2.xgt ="xpay";
                dd2.applicantname = Convert.ToString(dr[3]);
                dd2.applicantemail = Convert.ToString(dr[4]);

                dd2.applicantpnumber = Convert.ToString(dr[5]);

                dd2.applicant_addy = Convert.ToString(dr[6]);

           
                Retriever pp = new Retriever();
                ap = pp.getRegistrationBySysID2(dd2.agt);
                if (ap != null)
                {
                    dd2.agentname = ap.Surname;
                    dd2.agentemail = ap.Email;
                    dd2.agentpnumber = ap.PhoneNumber;
                }
                dd2.product_title = Convert.ToString(dr[7]);
                dd2.item_code = Convert.ToString(dr[8]);
                dd2.pc = Convert.ToString(dr[8]);
                //  pp.subject_code = Convert.ToString(dr[1]);
                //  pp.score = Convert.ToInt32(dr[2]);
                // pp.score = null;
              


            }

            conn.Close();
            return dd2;
        }

        public PortalAA readExcel5(string Filename)
        {
            XObjs.Registration ap = null;
            string path = Filename;
            string szConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
            //   string szConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\""; 
            OleDbConnection conn = new OleDbConnection(szConn);
            conn.Open();




            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", conn);
            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            string email = "";
            string user_name = "";
            string password = "";

            PortalAA dd2 = new PortalAA();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                // Result pp = new Result();
                //  string data = string.Format("F1:{0}, F2:{1}, F3:{2}", dr[0], dr[1], dr[2]);
                dd2.transID = Convert.ToString(dr[0]);
                dd2.amt = Convert.ToString(dr[1]);
                dd2.agt = Convert.ToString(dr[2]);
                dd2.xgt = "xpay";
                dd2.applicantname = Convert.ToString(dr[3]);
                dd2.applicantemail = Convert.ToString(dr[4]);

                dd2.applicantpnumber = Convert.ToString(dr[5]);

                dd2.applicant_addy = Convert.ToString(dr[6]);


                Retriever pp = new Retriever();
                ap = pp.getRegistrationBySysID2(dd2.agt);
                if (ap != null)
                {
                    dd2.agentname = ap.Surname;
                    dd2.agentemail = ap.Email;
                    dd2.agentpnumber = ap.PhoneNumber;
                }
                dd2.product_title = Convert.ToString(dr[7]);
                dd2.item_code = Convert.ToString(dr[8]);
                dd2.pc = Convert.ToString(dr[8]);
                //  pp.subject_code = Convert.ToString(dr[1]);
                //  pp.score = Convert.ToInt32(dr[2]);
                // pp.score = null;



            }

            conn.Close();
            return dd2;
        }

     


    }
}