<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_struc.aspx.cs" Inherits="Ipong.A._struc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>WELCOME TO IPO NIGERIA OFFICIAL WEB PORTAL</title>
    <link href="../css/ain_structure.css" rel="stylesheet" type="text/css" />

     <script src="../js/timers.js" type="text/javascript"></script> 
    <script src="../js/aj.js" type="text/javascript"></script>
   <script src="../js/funk.js" type="text/javascript"></script>
  <style type="text/css">
        .style2  { width: 60px;height: 20px;}
    </style>
    <script type="text/javascript">
        showClock();
        </script>
</head>
<body>
<form id="form1" runat="server">
<div id="container">
<div id="hd_main_space">
<div id="hd_t">
<div id="hd_tl">
<a href=""><img src="../images/headerpayxcld.jpg" width="450px" height="60px"  alt="Coat" /></a>
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
   <br />
   <a href="#" style=" font-size:12px;" onclick="doPrev('../a_login.aspx');">Log Out <img alt="" src="../images/LOGOUT.png" style="width: 20px; height: 20px" /></a>

</div>

<div id="section_header">ACCREDITED AGENT DASHBOARD  </div>
<div id="x_types">
<div id="side_menu">
<div id='flyout_menu'>
<ul>
   <li><a href='./profile.aspx'><span>DASHBOARD</span></a></li>
   <li> <a href="#" onclick="postwith('<%=System.Configuration.ConfigurationManager.AppSettings["payx_home"] %>', { agentType: '<%=agentType %>', pwalletID: '<%=adminID %>' });"><span>MAKE PAYMENT</span></a></li>
   <li><a href='./v_bask.aspx'><span>VIEW BASKET</span></a></li>

   <li><a href='#'><span>REPORTS</span></a>
    <ul> 
        <li><a href='./xreport_payments.aspx'><span>PAYMENTS</span></a> </li> 
       <li><a href='xreport_apps.aspx'><span>APPLICATIONS</span></a></li>   
        </ul> 
   </li>
   <li><a href='#'><span>STATUS</span></a>
    <ul>        
        <li><a href='xstatus_payments.aspx'><span>PAYMENTS</span></a></li>
        <li><a href='#'><span>APPLICATIONS</span></a>
            <ul>
             <li><a href="#"><span>DESIGN</span></a></li>
            <li><a href="./xstatus_pt_apps.aspx"><span>PATENT</span></a></li>
            <li><a href="./xstatus_tm_apps.aspx"><span>TRADEMARK</span></a></li>                
           </ul>    
      </li>   
      </ul> 
   </li>

    <li><a href='./xmail.aspx'><span>CONTACT IPO</span></a></li>
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
<table  align="center"  style="width:1200px;">
            <tr id="searchform">
                <td >                 
        <table style="width:1200px;font-family:Calibri;" id="applicantForm" class="center-align" >
             <tr>
                <td>
                 <hr />
                    </td>
            </tr>
            <tr>
                <td style="font-size:18pt;line-height:115%;" class="center-align">
                        ACCREDITATION UPDATE FORM
                        </td>
            </tr>
            

            <tr id="reg_tbl" style="display:none;">
            <td>
             <table style="width:1200px;">
            <tr>
                <td colspan="2" class="tbbg_left">
                    &nbsp;APPLICANT INFORMATION >>
                 </td>
             </tr>            
          
             <tr>
                <td colspan="2" >
                    <input id="xid" runat="server" class=" textbox" name="xid" size="50" type="hidden" />
                    </td>
            </tr>
            <tr class="left-align">
                <td style="width:400px;" >
                    FIRST NAME:</td>
                <td style="width:800px;">
                    <input id="txt_firstname" runat="server" class=" textbox" name="txt_firstname" size="50" type="text" /></td>
            </tr>
            
            <tr>
                <td class="left-align" >
                    SURNAME:</td>
                <td class="left-align">
                    <input id="txt_surname" runat="server" class=" textbox" name="txt_surname" 
                        size="50" type="text" />
                        </td>
            </tr>
            
            <tr>
                <td class="left-align">
                    E-MAIL:</td>
                <td class="left-align">
                    <input id="txt_mail" runat="server" class=" textbox" name="txt_mail" 
                        size="50" type="text" /></td>
            </tr>
            
            <tr>
                <td class="left-align">
                    MOBILE NUMBER:</td>
                <td class="left-align">
                    <input id="txt_mobile" runat="server" class=" textbox" name="txt_mobile" 
                        size="50" type="text" /></td>
            </tr>
            
            <tr>
                <td class="left-align">
                    DATE OF BIRTH:</td>
                <td class="left-align">
                    <asp:TextBox ID="txt_dob" runat="server" Width="70px" class="txt_date_pri" ></asp:TextBox></td>
            </tr>
            
            <tr>
                <td class="left-align">
                    NATIONALITY:</td>
                <td class="left-align">
                    NIGERIA</td>
            </tr>
            
            <tr>
                <td colspan="2" >
            <table id="agt" style="display:inline;width:100%;">
             <tr>
                <td colspan="2" class="sub_header left-align">
                    &nbsp;COMPANY INFORMATION >>
                    </td>
            </tr>
            <tr>
                <td class="left-align">
                    NAME:</td>
                <td class="left-align">
                    <input id="txt_coy_name" runat="server" class=" textbox" name="txt_coy_name" 
                        size="87" type="text" /></td>
            </tr>            
            <tr>
                <td class="left-align">
                    ADDRESS:</td>
                <td class="left-align">
                    <textarea id="txt_coy_addy" class="textbox" cols="60" name="txt_coy_addy" 
                        rows="3"></textarea></td>
            </tr>            
            <tr>
                <td class="left-align">
                    WEBSITE:</td>
                <td class="left-align">
                    <input id="txt_coy_web" runat="server" class=" textbox" name="txt_coy_web" 
                        size="87" type="text" /></td>
            </tr>            
             <tr>
                <td class="left-align">
                    DATE OF INCORPORATION:</td>
                <td class="left-align">
                    <asp:TextBox ID="txt_doi" runat="server" Width="70px"   class="txt_date_pri" ></asp:TextBox></td>
            </tr>
           <tr>
                <td colspan="2" class="sub_header left-align" >
                    &nbsp;CONTACT INFORMATION >></td>
            </tr>
            
            <tr>
                <td class="left-align">
                    FULLNAME:</td>
                <td class="left-align">
                    <input id="txt_cont_fullname" runat="server" class=" textbox" name="txt_cont_fullname" 
                        size="87" type="text" /></td>
            </tr>           
            
            <tr>
                <td class="left-align">
                    MOBILE:</td>
                <td class="left-align">
                <input id="txt_cont_mobile" runat="server" class=" textbox"  name="txt_cont_mobile" size="50" type="text" />                    
                    </td>
            </tr>
            </table>
              </td>
            </tr>
            <tr>
                <td colspan="2" class="sub_header left-align">
                    &nbsp;SECURITY >>
                    </td>
            </tr>
            
            <tr>
                <td class="left-align">
                    PASSWORD:</td>
                <td class="left-align">
                  <input id="txt_xpass" runat="server" class=" textbox" name="txt_xpass" size="50" type="password" />
                    </td>
            </tr>
            
            <tr>
                <td class="left-align">
                    CONFIRM PASSWORD:</td>
                <td class="left-align">
                  <input id="txt_conxpass" runat="server" class=" textbox" name="txt_conxpass" size="50" type="password" />
                    </td>
            </tr>
           
             <tr>
                <td colspan="2">
                 <hr />
                    </td>
            </tr>
             <tr>
	        <td  colspan="2" class="center-align">
             <div id="acc_error_img" style="display:none;"><img src="../images/delete.png"  /></div>
              <div id="acc_error_msg" style="display:none;"></div> 
             <div id="acc_success_img" style="display:none;"><img src="../images/check.png"  /></div>
            <div id="acc_success_msg" style="display:none;"></div>   
            
             </td>
            </tr>
            <tr class="center-align" >
                <td colspan="2">            
                 <input id="btnUpdateApplication" class="button" onclick="updateAccreditation('accreditaion.ashx');" type="button" value="Update Application" />
                &nbsp;
                 <input id="btnContinue" class="button"  onclick="ContinueAccreditationDocz('u_application_docs.aspx');" type="button" 
                        value="Continue"  style="display:none;"/></td>
            </tr>           
           </table>
            </td>
            </tr>
          
            </table>
     
    </td>
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
