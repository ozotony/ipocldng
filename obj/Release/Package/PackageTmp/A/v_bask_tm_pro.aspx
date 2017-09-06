<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="v_bask_tm_pro.aspx.cs" Inherits="Ipong.A.v_bask_tm_pro"  MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>WELCOME TO IPO NIGERIA OFFICIAL WEB PORTAL</title>
    <link href="../css/a_structure.css" rel="stylesheet" type="text/css" />
    <script src="../js/timers.js" type="text/javascript"></script> 
    <script src="../js/aj.js" type="text/javascript"></script> 
    <style type="text/css">
        .style2  { width: 120px;height: 40px;}
    </style>
    <script type="text/javascript">
        showClock();
        </script>
     <link href="https://fonts.googleapis.com/css?family=Lato" rel="stylesheet"/>
    <style type="text/css">
        html {

            font-family: 'Lato', sans-serif;
          
        }
       
    </style>
</head>
<body>
<div id="container">
<div id="hd_main_space">
<div id="hd_t">
<div id="hd_tl">
<a href="./profile.aspx"><img src="../images/LOGOCLD.jpg"   alt="CLD" /></a>
</div>
</div>

</div>

<div id="welcome">
    <div style="float:left; padding:5px 0 0 10px;">
     <img alt="Principal Photo" <%if( (Session["pic"].ToString()!="")&&(Session["pic"]!=null)){ %> src="<%=Session["pic"].ToString() %>"<%} else { %>src="../images/shadow_male.jpg"<%} %> />
    </div>
    <h1>WELCOME <%if(Session["coy_name"]!=null){Response.Write(Session["coy_name"].ToString().ToUpper());}%> </h1>
    <img alt="Einao Solutions" <%if( (Session["logo"].ToString()!="")&&(Session["logo"]!=null)){ %> src="<%=Session["logo"].ToString() %>"<%} else { %>src="../images/einao_logo.png"<%} %>  style="width: 200px; height: 70px"  /><br />
   <span id="sc" style="font-size:12px;padding-right:10px;" ></span>
   
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
       <li><a href='./xreport_apps.aspx'><span>APPLICATIONS</span></a></li>   
        </ul> 
   </li>
   <li><a href='#'><span>STATUS</span></a>
    <ul>        
        <li><a href='./xstatus_payments.aspx'><span>PAYMENTS</span></a></li>
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
<table  style="width: 100%; text-align:center;">
       <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" colspan="3">
                ITEM SECTION
                </td>
        </tr>
        <tr >
            <td class="center-align" colspan="3">
                    <img alt="" src="../images/TRADEMARKS.png" style="width: 100px; height: 100px" />
            </td>
        </tr>
        <tr class="center-align" style="background-color:#1C5E55; color:#ffffff; font-weight:bold; font-size:14px;">
            <td class="center-align" colspan="3">T002</td>
        </tr>  
        <tr >
            <td class="center-align" style="width:33%;">
                 <a href="./v_bask_tm.aspx">UN-USED</a><br />
            <strong><%=tm_cnt%> Item(s)</strong>
            </td>

            <td class="center-align" style="width:33%;">          
                 &nbsp;
            </td>

           <td class="center-align" style="width:33%;">
                <a href="./v_bask_tmu.aspx">USED</a> <br />
            <strong><%=tmu_cnt%> Item(s)</strong>
           </td>
        </tr>
       
        <tr class="center-align" style="background-color:#1C5E55; color:#ffffff; font-weight:bold; font-size:14px;">
            <td class="center-align" colspan="3">OTHERS</td>
        </tr>   
        <tr >
            <td class="center-align" style="width:33%;">
                 <a href="./v_bask_tm.aspx">UN-USED</a> <br />
            <strong><%=gtm_cnt%> Item(s)</strong>      
            </td>

            <td class="center-align" style="width:33%;">          
                 &nbsp;
            </td>

           <td class="center-align" style="width:33%;">
               <a href="./v_bask_tmu.aspx">USED</a><br />
            <strong><%=gtmu_cnt%> Item(s)</strong>
             
           </td>
        </tr>
          </table>
    </div>

<div id="ads" style="color:#fff;text-align:center;">ADVERTORIALS</div>
</div>

<div id="left_footer_menu">
<a href="">RELATED LINKS</a> | <a href="">NEWS</a> | <a href="">IPO WEBSITE</a> | <a href="">PUBLICATIONS</a> | <a href="../user_guide.pdf" target="_blank">USER GUIDE</a> </div>
<div id="right_footer_menu">EVENTS &amp; FEATURES</div>
<div id="bottom_footer">
      <b style="font-family:Cambria;font-size:13px;">POWERED BY<br />  <img alt="Einao Solutions" class="style2"  src="../images/einao_logo.png" /></b>
</div>
<script type="text/javascript">
    new CountUp('<%=log_date %>', 'sc', " Since logged on");
    </script>
</body>
</html>
