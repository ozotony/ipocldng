using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Text;
using System.Drawing;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using NPOI.POIFS.FileSystem;
using NPOI.HPSF;


namespace Ipong.Classes
{
    public class ExcelFuncs
    {
        public void CreateReportExcel(System.Web.UI.Page pg, List<Classes.XObjs.ReportItem> ri, string filename,string sheetname)
        {
           // Create a new workbook
           var workbook = new HSSFWorkbook();
           var sheet = workbook.CreateSheet(sheetname);
           //Create Header Style
           var headerLabelCellStyle = workbook.CreateCellStyle();
           headerLabelCellStyle.Alignment = HorizontalAlignment.Center; 
           headerLabelCellStyle.BorderBottom = BorderStyle.Thick;
           headerLabelCellStyle.FillForegroundColor = HSSFColor.SeaGreen.Index;  
           headerLabelCellStyle.FillPattern = FillPattern.SolidForeground; 

           var headerLabelFont = workbook.CreateFont();
           headerLabelFont.Boldweight = (short)FontBoldWeight.Normal;
           headerLabelFont.FontName = "Calibri";
           headerLabelFont.Color = HSSFColor.White.Index; 
           headerLabelFont.FontHeightInPoints = Convert.ToInt16(8);   
           headerLabelCellStyle.SetFont(headerLabelFont);

           //Create Data Style
           var dataCellStyle = workbook.CreateCellStyle();
           dataCellStyle.Alignment = HorizontalAlignment.Center;

           var dataFont = workbook.CreateFont();
           dataFont.Boldweight = (short)FontBoldWeight.Normal;
           dataFont.FontName = "Calibri";
           dataFont.FontHeightInPoints = Convert.ToInt16(8);
           dataCellStyle.SetFont(dataFont);
           // Add header labels
           var rowIndex = 0; 
           var row = sheet.CreateRow(rowIndex);
           var cell = row.CreateCell(0);   cell.SetCellValue("S/N"); cell.CellStyle = headerLabelCellStyle;
           cell = row.CreateCell(1); cell.SetCellValue("TRANSACTION ID"); cell.CellStyle = headerLabelCellStyle;
           cell = row.CreateCell(2); cell.SetCellValue("CODE"); cell.CellStyle = headerLabelCellStyle;
           cell = row.CreateCell(3); cell.SetCellValue("DESCRIPTION"); cell.CellStyle = headerLabelCellStyle;
           cell = row.CreateCell(4); cell.SetCellValue("PAYMENT DATE"); cell.CellStyle = headerLabelCellStyle;
           cell = row.CreateCell(5); cell.SetCellValue("PAYMENT MODE"); cell.CellStyle = headerLabelCellStyle;
           cell = row.CreateCell(6); cell.SetCellValue("PAYMENT STATUS"); cell.CellStyle = headerLabelCellStyle;
           cell = row.CreateCell(7); cell.SetCellValue("INITIAL AMOUNT (NGN)"); cell.CellStyle = headerLabelCellStyle;
           cell = row.CreateCell(8); cell.SetCellValue("TECH FEES (NGN)"); cell.CellStyle = headerLabelCellStyle;
           cell = row.CreateCell(9); cell.SetCellValue("CURRENT OFFICE"); cell.CellStyle = headerLabelCellStyle;
           cell = row.CreateCell(10); cell.SetCellValue("STATUS"); cell.CellStyle = headerLabelCellStyle;
           cell = row.CreateCell(11); cell.SetCellValue("DATE"); cell.CellStyle = headerLabelCellStyle;
           rowIndex++;
           int sn = 1; 
           foreach (Ipong.Classes.XObjs.ReportItem r in ri)
           {
               row = sheet.CreateRow(rowIndex);  //row.CreateCell(0).
               cell = row.CreateCell(0); cell.SetCellValue(sn.ToString()); cell.CellStyle = dataCellStyle;
               cell = row.CreateCell(1); cell.SetCellValue(r.newtransID); cell.CellStyle = dataCellStyle;
               cell = row.CreateCell(2); cell.SetCellValue(r.item_code); cell.CellStyle = dataCellStyle;
               cell = row.CreateCell(3); cell.SetCellValue(r.item_desc); cell.CellStyle = dataCellStyle;
               cell = row.CreateCell(4); cell.SetCellValue(r.payment_date); cell.CellStyle = dataCellStyle;
               cell = row.CreateCell(5); cell.SetCellValue(r.payment_mode); cell.CellStyle = dataCellStyle;
               cell = row.CreateCell(6); cell.SetCellValue(r.payment_status); cell.CellStyle = dataCellStyle;
               cell = row.CreateCell(7); cell.SetCellValue(r.init_amt); cell.CellStyle = dataCellStyle;
               cell = row.CreateCell(8); cell.SetCellValue(r.tech_amt); cell.CellStyle = dataCellStyle;
               cell = row.CreateCell(9); cell.SetCellValue(r.office_status); cell.CellStyle = dataCellStyle;
               cell = row.CreateCell(10); cell.SetCellValue(r.data_status); cell.CellStyle = dataCellStyle;
               cell = row.CreateCell(11); cell.SetCellValue(DateTime.Now.ToString("yyyy-MM-dd")); cell.CellStyle = dataCellStyle;              
               sn++; rowIndex++;
           }
           // Auto-size each column
           for (var i = 0; i <=11; i++)
           {
               sheet.AutoSizeColumn(i);

               // Bump up with auto-sized column width to account for bold headers
               sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + 1024);
           }
           // Add row indicating date/time report was generated...
           sheet.CreateRow(rowIndex + 1).CreateCell(0).SetCellValue("Report generated on " + DateTime.Now.ToString());

           // NPOI Save the Excel spreadsheet to a file on the web server's file system
           //using (var fileData = new FileStream(filename, FileMode.Create))
           //{
           //    workbook.Write(fileData);
           //}
           // NPOI Save the Excel spreadsheet to a MemoryStream and return it to the client
           using (var exportData = new MemoryStream())
           {
               workbook.Write(exportData);
               pg.Response.ContentType = "application/vnd.ms-excel";
               pg.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
               pg.Response.Clear();
               pg.Response.BinaryWrite(exportData.GetBuffer());
               pg.Response.End();
           }
        }

    }
}