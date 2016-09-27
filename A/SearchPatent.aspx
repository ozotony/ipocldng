<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchPatent.aspx.cs" Inherits="Ipong.A.SearchPatent" %>

<!DOCTYPE html>

<html data-ng-app="formApp" >
<head >
    <title>WELCOME TO IPO NIGERIA OFFICIAL WEB PORTAL</title>
     <link href="../css/a_structure.css" rel="stylesheet" type="text/css" />
    <script src="../js/timers.js" type="text/javascript"></script> 
    <script src="../js/aj.js" type="text/javascript"></script> 

     <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
 
    <script src="../Script/angular.js"></script>
    <script src="../Script/angular-animate.js"></script>
    <script src="../Script/angular-ui-router.min.js"></script>
    <link href="../css/sweet-alert.css" rel="stylesheet" />
    <link href="../css/loading-bar.css" rel="stylesheet" />
    <link href="../css/animate.css" rel="stylesheet" />
    <script src="../Script/App5.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <script src="../js/loading-bar.js"></script>
    <script src="../Script/angular-sanitize.min.js"></script>
    <script src="../js/stickytooltip.js"></script>
     <link href="../css/sweet-alert.css" rel="stylesheet" />
    <script src="../js/sweet-alert.min.js"></script>
  
    <link href="../css/stickytooltip.css" rel="stylesheet" />
    <style type="text/css">
        .style2  { width: 120px;height: 40px;}

        .pp3 {margin-left:5px;}
       
    </style>
    <script type="text/javascript">
        //  showClock();
        </script>

    <style type="text/css">
 .pending-delete {
       /*color:red;
     background-color: lightgreen;*/

     text-decoration: line-through;
    
 }
    </style>

       <style>
        .pageheader {
            width: 100%;
            height: 40px;
            padding: 10px;
            color: #fff;
            margin-top: 30px;
            margin-bottom: 10px;
            background: #006699;
            text-align: center;
        }

        .btn-default {
            background-color: #006699;
            color: #fff;
            padding: 10px;
        }

        body {
            font-family: Calibri;
        }
    </style>
</head>
<body >
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
   
   <a href="http://www.iponigeria.com/#/logout" style=" font-size:12px;" >Log Out <img alt="" src="../images/LOGOUT.png" style="width: 20px; height: 20px" /></a>

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
        <li ><a href='./xreport_payments.aspx'><span>PAYMENTS</span></a> </li> 
        <li   ><a href='xreport_apps.aspx'><span>APPLICATIONS</span></a></li>   
        </ul> 
   </li>
   <li ><a href='#'><span>STATUS</span></a>
    <ul>        
        <li ><a href='xstatus_payments.aspx'><span>PAYMENTS</span></a></li>
       <li><a href='#'><span>APPLICATIONS</span></a>
            <ul>
             <li><a href="#"><span>DESIGN</span></a></li>
            <li><a href="./xstatus_pt_apps.aspx"><span>PATENT</span></a></li>
            <li><a href="./xstatus_tm_apps.aspx"><span>TRADEMARK</span></a></li>                
           </ul>    
      </li>   
      </ul> 
   </li>

   <li><a href='./xmail.aspx'><span>CONTACT IPO</span></a></li> <li><a href='./feelist.aspx'><span>FEE LIST ITEMS</span></a></li>
</ul>
</div>
</div>

<div id="x_main">
    <form class="form-inline"> 
      <div ui-view></div>
         <input id="xname" name="xname" type="hidden" runat="server" />
        </form>
    </div>

<div id="ads2" style="color:#fff;text-align:center;">ADVERTORIALS</div>
</div>

<%--<div id="left_footer_menu">
<a href="#">RELATED LINKS</a> | <a href="#">NEWS</a> | <a href="#">IPO WEBSITE</a> | <a href="#">PUBLICATIONS</a> | <a href="../user_guide.pdf" target="_blank">USER GUIDE</a> </div>
<div id="right_footer_menu">EVENTS &amp; FEATURES</div>
<div id="bottom_footer">
   <b style="font-family:Cambria;font-size:13px;">POWERED BY<br />  <img alt="Einao Solutions" class="style2"  src="../images/einao_logo.png" /></b>
   </div>--%>

</div>
<script type="text/javascript">
    new CountUp('<%=log_date %>', 'sc', " Since logged on");
    </script>
</body>
</html>