﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="us_application_docs.aspx.cs" Inherits="Ipong.Forms.u_application_docs" MaintainScrollPositionOnPostback="true" %>

<%@ Register assembly="Brettle.Web.NeatUpload" namespace="Brettle.Web.NeatUpload" tagprefix="Upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>WELCOME TO IPO NIGERIA OFFICIAL WEB PORTAL</title>
    <link href="../css/ain_structure.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="../css/jquery.ui.all.css" type="text/css"/>
<link rel="stylesheet" href="../css/jquery.ui.theme.css" type="text/css"/>

     <script src="../js/timers.js" type="text/javascript"></script> 
    <script src="../js/aj.js" type="text/javascript"></script>
   <script src="../js/funk.js" type="text/javascript"></script>
   <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
<script src="../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
<script src="../js/jquery.js" type="text/javascript"></script>
<script src="../js/ui/jquery.cookie.js" type="text/javascript"></script>
<script src="../js/ui/jquery.ui.core.js" type="text/javascript"></script>
<script src="../js/ui/jquery.ui.widget.js" type="text/javascript"></script>

<script src="../js/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <style type="text/css">
        .style2  { width: 60px;height: 20px;}
    </style>
    <script type="text/javascript">
        showClock();
        </script>
<script  type="text/javascript">

    $(function () {

        $(".txt_date_pri").datepicker({
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            dateFormat: 'yy-mm-dd',
            yearRange: 'c-100:c+0'
        });

    });
</script>
</head>
<body>
<form id="form1" runat="server">
<div id="container">
<div id="hd_main_space">
<div id="hd_t">
<div id="hd_tl">
<a href=""><img src="../images/header_left.png" width="200px" height="40px"  alt="Coat" /></a>
</div>
</div>
<div id="hd_menu">
<div id="seal">
<img src="../images/seal_hd.png" alt="Seal" width="80px" height="80px"/>
</div>
</div>
</div>

<div id="main">
<div id="welcome">
    <div style="float:left; padding:5px 0 0 10px;">
    <img alt="Principal Photo"  src="../images/shadow_male.jpg" />
    </div>
    <h1>WELCOME <%if(Session["coy_name"]!=null){Response.Write(Session["coy_name"].ToString().ToUpper());}%> </h1>
    <img alt="Einao Solutions" src="../images/einao_logo.png" style="width: 110px; height: 50px" /><br />
   <span id="sc" style="font-size:12px;padding-right:10px;" ></span>
 
   <a href="#" style=" font-size:12px;" onclick="doPrev('../a_login.aspx');">Log Out <img alt="" src="../images/LOGOUT.png" style="width: 20px; height: 20px" /></a>

</div>

<div id="section_header">ACCREDITED AGENT DASHBOARD  </div>
<div id="x_types">
<div id="side_menu">
<div id='flyout_menu'>
<ul>
   <li><a href='../A/profile.aspx'><span>DASHBOARD</span></a></li>
   <li> <a href="#" onclick="postwith('<%=System.Configuration.ConfigurationManager.AppSettings["payx_home"] %>', { agentType: '<%=agentType %>', pwalletID: '<%=adminID %>' });"><span>MAKE PAYMENT</span></a></li>
   <li><a href='../A/v_bask.aspx'><span>VIEW BASKET</span></a></li>

   <li><a href='#'><span>REPORTS</span></a>
    <ul> 
        <li><a href='../A/xreport_payments.aspx'><span>PAYMENTS</span></a> </li> 
       <li><a href='../A/xreport_apps.aspx'><span>APPLICATIONS</span></a></li>   
        </ul> 
   </li>
   <li><a href='#'><span>STATUS</span></a>
    <ul>        
        <li><a href='../A/xstatus_payments.aspx'><span>PAYMENTS</span></a></li>
       <li><a href='#'><span>APPLICATIONS</span></a>
            <ul>
             <li><a href="#"><span>DESIGN</span></a></li>
            <li><a href="../A/xstatus_pt_apps.aspx"><span>PATENT</span></a></li>
            <li><a href="../A/xstatus_tm_apps.aspx"><span>TRADEMARK</span></a></li>                
           </ul>    
      </li>   
      </ul> 
   </li>

   <li><a href='../A/xmail.aspx'><span>CONTACT IPO</span></a> </li>
</ul>
</div>

<div class="side_hd">--- IPO WEBSITE--- </div>
<div id='home_menu'>
<ul>
   <li><a href='#'><span>HOME</span></a></li>
   <li> <a href="#" ><span>NEWS</span></a></li>
   <li><a href='#'><span>RELATED LINKS</span></a></li>
   <li> <a href="#" ><span>PUBLICATIONS</span></a></li>
   <li><a href='#'><span>USER GUIDE</span></a></li>
  
</ul>
</div>

</div>

<div id="x_main">
<table class="center-align" style="width:100%;">
            <tr >
                <td >
    <div id="searchform">                
        <table style="width:100%;font-family:Calibri;" id="applicantForm" class="center-align" >
            <tr class="center-align">
                <td colspan="2">
                    <img alt="Coat Of Arms" height="79" src="../images/coat_of_arms.png" 
                        width="85" />
               </td>
            </tr>
            <tr class="center-align" style=" font-size:11pt;" >
                <td colspan="2">
                    FEDERAL REPUBLIC OF NIGERIA<br />
                    FEDERAL MINISTRY OF INDUSTRY, TRADE AND INVESTMENT<br />
                    COMMERCIAL LAW DEPARTMENT<br />
                    TRADEMARKS, PATENTS AND DESIGNS REGISTRY<br />
                    PATENTS AND DESIGNS DECREE NO 60 OF 1970
</td>
            </tr>
           
           <tr>
           <td colspan="3">                       
            <table style="width:100%;font-family:Calibri;" id="doc_tbl" class="center-align">
            <tr>
                    <td class="tbbg" colspan="2">                       
                        PLEASE ENTER THE NUMBER OF PAGES OF EACH ITEM ACCOMPANYING THIS FORM AND ATTACH 
                        DOCUMENTS BELOW :</td>
                </tr>
              
                <tr>
                    <td class="center-align" colspan="2" style="color:Red; font-size:16px;">
                       <strong>
                        (NOTE: DOCUMENTS ATTACHED SHOULD BE OF JPEG/PNG FORMAT ONLY AND NOT MORE THAN 1MB EACH!!)</strong> </td>
                </tr>
                <tr style="background-color:#999999;">
                    <td align="left" class="tbbg_left2" style="width:25%;">
                        &nbsp;ITEMS</td>
                    <td align="left" class="tbbg_left2">
                        ATTACH DOCUMENT</td>
                </tr>
                <%if (pic_newfilename == "0")
                  { %>
                <tr>
                    <td align="left">
                        &nbsp;PROFILE IMAGE :
                    </td>
                    <td align="left">
                          <Upload:InputFile ID="fu_pic_doc"  CssClass="textbox" runat="server"/>
                         <asp:RegularExpressionValidator id="RegularExpressionLoa" 
				ControlToValidate="fu_pic_doc"
				ValidationExpression="(([^.;]*[.])+(JPEG|JPG|jpg|jpeg|PNG|png); *)*(([^.;]*[.])+(JPEG|JPG|jpg|jpeg|PNG|png))?$"
				Display="Static"
				ErrorMessage="DOCUMENTS ATTACHED SHOULD BE OF JPEG/PNG!!"
				EnableClientScript="True"  runat="server"/>
                        </td>
                </tr>               
                
                <tr>
                    <td colspan="2" class="center-align">
                     
                    </td>
                </tr>
                 <%} %>
                 <%if (logo_newfilename == "0")
                   { %>
                <tr>
                    <td align="left">
                        &nbsp;COMPANY LOGO :
                    </td>
                    <td align="left">
                        <Upload:InputFile ID="fu_logo_doc"  CssClass="textbox" runat="server" />
                         <asp:RegularExpressionValidator id="RegularExpressionClaim" 
				ControlToValidate="fu_logo_doc"
				ValidationExpression="(([^.;]*[.])+(JPEG|JPG|jpg|jpeg|PNG|png); *)*(([^.;]*[.])+(JPEG|JPG|jpg|jpeg|PNG|png))?$"
				Display="Static"
				ErrorMessage="DOCUMENTS ATTACHED SHOULD BE OF JPEG/PNG!!"
				EnableClientScript="True"  runat="server"/>
                        </td>
                </tr>
               <%} %>
                <tr>
                    <td colspan="2" class="center-align">
                          <Upload:ProgressBar id="pb_all_doc" runat="server" inline="true" Text="" /></td>
                </tr>

                <tr>
                    <td align="center" colspan="2">
                    <%if(sp==0){ %>
                          <asp:Button ID="btn_all_doc" runat="server" Text="Upload Documents"   CssClass="button"/> 
                          <% }
                      else if (sp > 0)
                      { %>                         
                          <asp:Button ID="btn_profile" runat="server" Text="Back to Dashboard"  CssClass="button" onclick="btn_profile_Click"/>                                <%} %>
                          </td>
                </tr>
                </table>
           
            </td>
            </tr>
            </table>
            
    
    </div>
                    <%=succ_msg %></td>
    </tr>
    </table>
    </div>

</div>


</div>

</div>
</form>
<script type="text/javascript">
    new CountUp('<%=log_date %>', 'sc', " Since logged on");
    </script>
</body>
</html>