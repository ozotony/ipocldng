﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="profile2.aspx.cs" Inherits="Ipong.A.profile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>WELCOME TO IPO NIGERIA OFFICIAL WEB PORTAL</title>
    <link href="../css/a_structure.css" rel="stylesheet" type="text/css" />
    <script src="../js/timers.js" type="text/javascript"></script> 
    <script src="../js/aj.js" type="text/javascript"></script> 

     <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script  type="text/javascript" src="../js/knockout-2.2.0.js"></script>
    <script type="text/javascript" src="../js/Assign_Role2.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        .style2  { width: 120px;height: 40px;}
       
    </style>
    <script type="text/javascript">
        showClock();
        </script>
</head>
<body>
<div id="container">
<div id="hd_main_space">
<div id="hd_t">
<div id="hd_tl">
<a href="./profile.aspx"><img src="../images/LOGOCLD.jpg" width="458px" height="76px"  alt="CLD" /></a>
</div>
</div>

</div>

<div id="welcome">
    <div style="float:left; padding:5px 0 0 10px;">
    <img alt="Principal Photo" <%if( (Session["pic"].ToString()!="")&&(Session["pic"]!=null)){ %> src="../<%=Session["pic"].ToString() %>"<%} else { %>src="../images/shadow_male.jpg"<%} %> />
    </div>
    <h1>WELCOME <%if(Session["coy_name"]!=null){Response.Write(Session["coy_name"].ToString().ToUpper());}%> </h1>
    <img alt="Einao Solutions" <%if( (Session["logo"].ToString()!="")&&(Session["logo"]!=null)){ %> src="<%=Session["logo"].ToString() %>"<%} else { %>src="../images/einao_logo.png"<%} %>  style="width: 200px; height: 70px" /><br />
   <span id="sc" style="font-size:12px;padding-right:10px;" ></span>
   
   <a href="#" style=" font-size:12px;" onclick="doPrev('../a_login.aspx');">Log Out <img alt="" src="../images/LOGOUT.png" style="width: 20px; height: 20px" /></a>

</div>

<div id="section_header">ACCREDITED AGENT DASHBOARD  </div>
<div id="x_types">
<div id="side_menu">
<div id='flyout_menu'>
    <form action="#" runat="server"> 
<ul>
   <li><a data-bind="visible: Agent" href='./profile.aspx'><span>DASHBOARD</span></a></li>
   <li data-bind="visible: Agent"> <a href="#" onclick="postwith('<%=System.Configuration.ConfigurationManager.AppSettings["payx_home"] %>', { agentType: '<%=agentType %>', pwalletID: '<%=adminID %>' });"><span>MAKE PAYMENT</span></a></li>
   <li data-bind="visible: Agent" ><a href='./v_bask.aspx'><span>VIEW BASKET</span></a></li>

   <li data-bind="visible: Agent"><a href='#'><span>REPORTS</span></a>
    <ul> 
        <li><a href='./xreport_payments.aspx' data-bind="visible: Agent"><span>PAYMENTS</span></a> </li> 
        <li data-bind="visible: Agent"><a href='xreport_apps.aspx'><span>APPLICATIONS</span></a></li>   
        </ul> 
   </li>
   <li data-bind="visible: Agent"><a href='#'><span>STATUS</span></a>
    <ul>        
        <li data-bind="visible: Agent"><a href='xstatus_payments.aspx'><span>PAYMENTS</span></a></li>
       <li data-bind="visible: Agent"><a href='#'><span>APPLICATIONS</span></a>
            <ul>
             <li><a href="#"><span>DESIGN</span></a></li>
            <li><a href="./xstatus_pt_apps.aspx"><span>PATENT</span></a></li>
            <li><a href="./xstatus_tm_apps.aspx"><span>TRADEMARK</span></a></li>                
           </ul>    
      </li>   
      </ul> 
   </li>

   <li data-bind="visible: Agent"><a href='./xmail.aspx'><span>CONTACT IPO</span></a></li> <li data-bind="visible: Agent"><a href='./feelist.aspx'><span>FEE LIST ITEMS</span></a></li>

    <li data-bind="visible: Agent"> <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">BRANCH COLLECT <span> <br /><br /> PAYMENT</span> </asp:LinkButton> </li>
</ul>

      
   
</div>
</div>

<div id="x_main">
    

         <asp:HiddenField ID="hfImage1" runat="server" />

    </form>
    <div  class ="hidden2"> 
    <div  class="alert alert-info">
    <a href="#" class="close" data-dismiss="alert">&times;</a>
   <h1>  <strong> You need to be an Approved Agent to access platform dashboard content .</strong></h1>
</div>

        </div>
    
<table  style="width: 100%;" >
        <tr class="center-align">
            <td style="width: 30%;"  >
                <a href="#" onclick="postwith('<%=System.Configuration.ConfigurationManager.AppSettings["payx_home"] %>', { agentType: '<%=agentType %>', pwalletID: '<%=adminID %>' });">
                    <img alt="" data-bind="visible: Agent" src="../images/MAKE PAYMENT.png" style="width: 100px; height: 100px" /></a></td>
            <td style="width: 30%;" data-bind="visible: Agent">
                <a href="./v_bask.aspx">
                    <img alt="" data-bind="visible: Agent" src="../images/SHOPPING CART.png" style="width: 100px; height: 100px" /></a></td>
            <td style="width: 30%;" data-bind="visible: Agent">
                <a href="reports_p.aspx"> 
                    <img alt="" src="../images/REPORTS.png" style="width: 100px; height: 100px" /></a></td>
        </tr>
        
        <tr class="center-align">
            <td style="width: 30%;"  >
                 <a href="#" data-bind="visible: Agent" onclick="postwith('<%=System.Configuration.ConfigurationManager.AppSettings["payx_home"] %>', { agentType: '<%=agentType %>', pwalletID: '<%=adminID %>' });">MAKE PAYMENT</a></td>
            <td style="width: 30%;" data-bind="visible: Agent">
                <a href="./v_bask.aspx">VIEW BASKET</a></td>
            <td style="width: 30%;" data-bind="visible: Agent">
                <a href="reports_p.aspx">REPORTS</a></td>
        </tr>
        
        <tr class="center-align">
            <td colspan="3" >
                &nbsp;</td>
        </tr>
        
        <tr class="center-align" data-bind="visible: Agent">
            <td style="width: 30%;">
                <a href="status_p.aspx" >
                    <img alt="" src="../images/STATUS.png" style="width: 100px; height: 100px" /></a></td>
            <td style="width: 30%;">
                <a href="./u_application.aspx">
                    <img alt="" src="../images/SETTINGS.png" style="width: 100px; height: 100px" /></a></td>
            <td style="width: 30%;">
                <a href="./xmail.aspx">
                    <img alt="" src="../images/MAIL.png" style="width: 100px; height: 100px" /></a></td>
        </tr>
        <tr class="center-align" data-bind="visible: Agent">
            <td style="width: 30%;">
                <a href="status_p.aspx" >STATUS</a></td>
            <td style="width: 30%;">
                <a href="./u_application.aspx" >UPDATE PROFILE</a></td>
            <td style="width: 30%;">
                <a href="./xmail.aspx" >CONTACT IPO</a></td>
        </tr>    
    
    <tr class="center-align">
            <td colspan="3" >
                &nbsp;</td>
        </tr>  
    
     <tr class="center-align">
            <td style="width: 30%;" data-bind="visible: SuperAdmin">
                <a href="./Accred_Process.aspx" >
                    <img alt="" src="../images/accred.jpg" style="width: 100px; height: 100px" /></a></td>
            <td style="width: 30%;">
                </td>
            <td style="width: 30%;">
                </td>
        </tr> 

     <tr class="center-align">
            <td style="width: 30%;" data-bind="visible: SuperAdmin">
                <a href="./Accred_Process.aspx" >AGENT ACCREDITATION</a></td>
            <td style="width: 30%;">
                </td>
            <td style="width: 30%;">
                </td>
        </tr>   
        </table>
   
    </div>

<div id="ads" style="color:#fff;text-align:center;">ADVERTORIALS</div>
</div>

<div id="left_footer_menu">
<a href="#">RELATED LINKS</a> | <a href="#">NEWS</a> | <a href="#">IPO WEBSITE</a> | <a href="#">PUBLICATIONS</a> | <a href="../user_guide.pdf" target="_blank">USER GUIDE</a> </div>
<div id="right_footer_menu">EVENTS &amp; FEATURES</div>
<div id="bottom_footer">
   <b style="font-family:Cambria;font-size:13px;">POWERED BY<br />  <img alt="Einao Solutions" class="style2"  src="../images/einao_logo.png" /></b>
   </div>

</div>
<script type="text/javascript">
    new CountUp('<%=log_date %>', 'sc', " Since logged on");
    </script>
</body>
</html>
