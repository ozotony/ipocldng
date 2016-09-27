<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="profileaa.aspx.cs" Inherits="Ipong.A.profileaa" %>

<!DOCTYPE html>

<html ng-app="formApp">
<head>
    <title>WELCOME TO IPO NIGERIA OFFICIAL WEB PORTAL</title>
    <link href="../css/a_structure2.css" rel="stylesheet" type="text/css" />
    <script src="../js/timers.js" type="text/javascript"></script> 
    <script src="../js/aj.js" type="text/javascript"></script> 

     <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script  type="text/javascript" src="../js/knockout-2.2.0.js"></script>
    <script type="text/javascript" src="../js/Assign_Role2.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Script/angular.js"></script>
    <script src="../Script/App2.js"></script>
    <style type="text/css">
        .style2  { width: 120px;height: 40px;}
       
    </style>
    <script type="text/javascript">
        showClock();
        </script>


   
</head>
<body ng-controller="formController">
     <script type="text/javascript">
<!--
    spe = 500;
    var na = document.getElementsByTagName("blink")
    // var   na = document.all.tags("blink");
    swi = 1;
    bringBackBlinky();

    function bringBackBlinky() {

        if (swi == 1) {
            sho = "visible";
            swi = 0;
        }
        else {
            sho = "hidden";
            swi = 1;
        }

        for (i = 0; i < na.length; i++) {
            na[i].style.visibility = sho;
        }

        setTimeout("bringBackBlinky()", spe);
    }

</script>

<div id="container" class="container">
<div id="hd_main_space" class="row">
<div id="hd_t">
<div id="hd_tl" class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
<a href="./profile.aspx"><img src="../images/LOGOCLD.jpg" width="458px" height="76px"  alt="CLD" /></a>
</div>
</div>

</div>

<div id="welcome" class="row">
    <div  class="col-xs-2 col-sm-2 col-md-2 col-lg-2">
    <img style=" float:left"   alt="Principal Photo" <%if( (Session["pic"].ToString()!="")&&(Session["pic"]!=null)){ %> src="../<%=Session["pic"].ToString() %>"<%} else { %>src="../images/shadow_male.jpg"<%} %> />
    </div>

    

   
    <div class="col-xs-offset-7 col-sm-offset-7 col-md-offset-7 col-lg-offset-7 col-xs-3 col-sm-3 col-md-3 col-lg-3"> 
         <div class="row">
    <h1 class="col-xs-12 col-sm-12 col-md-12 col-lg-12">WELCOME <%if(Session["coy_name"]!=null){Response.Write(Session["coy_name"].ToString().ToUpper());}%> </h1>

             </div>
        <div class="row">
    <img class="col-xs-12 col-sm-12 col-md-12 col-lg-12" alt="Einao Solutions" <%if( (Session["logo"].ToString()!="")&&(Session["logo"]!=null)){ %> src="<%=Session["logo"].ToString() %>"<%} else { %>src="../images/einao_logo.png"<%} %>  style="width: 200px; height: 70px; float:right" />
</div>
         <div class="row">
    <div > 
   <span id="sc" style="font-size:12px;padding-right:10px;" ></span>
            
   <a  href="http://www.iponigeria.com/#/logout" style=" font-size:12px;" >Log Out <img alt="" src="../images/LOGOUT.png" style="width: 20px; height: 20px" /></a>
 </div>
</div>
</div>
</div>
    <div id="Div1" class="row">
<div id="section_header" class="col-xs-12 col-sm-12 col-md-12 col-lg-12">     ACCREDITED AGENT DASHBOARD  </div>
        </div>
    <div id="Div2" class="row">
<div id="x_types" class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

<div id="side_menu" class="col-xs-2 col-sm-2 col-md-2 col-lg-2" >
<div id='flyout_menu'>
    <form id="Form1" action="#" runat="server"> 
         <input id="xname" name="xname" type="hidden" runat="server" />
<ul>
   <li><a data-bind="visible: Agent" href='./profile.aspx'><span>DASHBOARD</span></a></li>
   <li data-bind="visible: Agent">
     <%--   <a href="#" onclick="postwith('<%=System.Configuration.ConfigurationManager.AppSettings["payx_home"] %>', { agentType: '<%=agentType %>', pwalletID: '<%=adminID %>' });"><span>MAKE PAYMENT</span></a>--%>
       <a href="#" ><span>MAKE PAYMENT</span></a>

   </li>
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
             <li><a href="./xstatus_ds_apps.aspx"><span>DESIGN</span></a></li>
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

 

<div id="x_main" class="col-xs-8 col-sm-8 col-md-8 col-lg-8">
    

         <asp:HiddenField ID="hfImage1" runat="server" />
     <asp:HiddenField ID="hfImage2" runat="server" />

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
                <%-- <a href="#" >--%>
                    <img alt="" data-bind="visible: Agent" src="../images/MAKE PAYMENT.png" style=" height: 100px" /></a></td>
            <td style="width: 30%;" data-bind="visible: Agent">
                <a href="./v_bask.aspx">
                    <img alt="" data-bind="visible: Agent" src="../images/SHOPPING CART.png" style=" height: 100px" /></a></td>
            <td style="width: 30%;" data-bind="visible: Agent">
                <a href="reports_p.aspx"> 
                    <img alt="" src="../images/REPORTS.png" style=" height: 100px" /></a></td>
        </tr>
        
        <tr class="center-align">
            <td style="width: 30%;"  >
                 <a href="#" data-bind="visible: Agent" onclick="postwith('<%=System.Configuration.ConfigurationManager.AppSettings["payx_home"] %>', { agentType: '<%=agentType %>', pwalletID: '<%=adminID %>' });">MAKE PAYMENT</a>

                <%-- <a href="#" data-bind="visible: Agent" >MAKE PAYMENT</a>--%>


            </td>
            <td style="width: 30%;" data-bind="visible: Agent">
                <a href="./v_bask.aspx">VIEW BASKET</a></td>
            <td style="width: 30%;" data-bind="visible: Agent">
                <a href="reports_p.aspx">REPORTS</a></td>
        </tr>
        
        <tr class="center-align">
            <td colspan="3" >
                &nbsp;</td>
        </tr>
        
        <tr class="center-align" >
            <td style="width: 30%;" data-bind="visible: Agent">
                <a href="status_p.aspx" >
                    <img alt="" src="../images/STATUS.png" style=" height: 100px" /></a></td>
            <td style="width: 30%;" data-bind="visible: Agent || Payment">
                <a href="./u_application.aspx">
                    <img alt="" src="../images/SETTINGS.png" style=" height: 100px" /></a></td>
            <td style="width: 30%;" data-bind="visible: Agent">
                <a href="./xmail.aspx">
                    <img alt="" src="../images/MAIL.png" style=" height: 100px" /></a></td>
        </tr>
        <tr class="center-align" >
            <td style="width: 30%;" data-bind="visible: Agent">
                <a href="status_p.aspx" >STATUS</a></td>
            <td style="width: 30%;" data-bind="visible: Agent">
                <a href="./u_application.aspx" >UPDATE PROFILE</a></td>
            <td style="width: 30%;">
                <a href="./xmail.aspx" data-bind="visible: Agent || Payment" >CONTACT IPO</a></td>
        </tr>    
    
    <tr class="center-align">
            <td colspan="3" >
                &nbsp;</td>
        </tr>  
    
     <tr class="center-align">
            <td style="" data-bind="visible: Agent">
               <a href="./profile3.aspx" >
                    <img src="../images/recordals.png" />
                   <%-- <img alt="" src="../images/accred.jpg" style="width: 100px; height: 100px" />--%></a></td>
            <td style="" data-bind="visible: Agent">
                <img src="../images/trademark9.png" />
                <%--<a href="./Accred_Process.aspx" >
                    <img alt="" src="../images/online.jpg" style="width: 100px; height: 100px" /></a>--%>

                </td>
            <td style="" data-bind="visible: Agent">
                 <img src="../images/trademark10.png" />
                <%--<a href="./Accred_Process.aspx" >
                    <img alt="" src="../images/manual.jpg" style="width: 100px; height: 100px" /></a>--%>
                </td>
        </tr> 

     <tr class="center-align">
            <td style="width: 30%;" data-bind="visible: Agent">
               <a href="./profile3.aspx" >RECORDALS</a></td>
            <td style="width: 30%;" data-bind="visible: Agent">
                 <a href="./Online_Trademark.aspx" >APPLY FOR ONLINE TRADEMARKS CERTIFICATES</a>
                </td>
            <td style="width: 30%;" data-bind="visible: Agent">
                <a href="./Manual_Trademark.aspx" >APPLY FOR MANUAL TRADEMARKS CERTIFICATES</a>
                <%-- <a href="./Accred_Process.aspx" >APPLY FOR MANUAL TRADEMARKS CERTIFICATES</a>--%>
                </td>
        </tr>
    
     <tr class="center-align">
            <td colspan="3" >
                &nbsp;</td>
        </tr>  
     <tr class="center-align">
           
          <td style="" data-bind="visible: Agent">
                  <a href="./SearchDesign.aspx" >
                   <img alt="" src="../images/DESIGNS.png" style=" height: 100px" />
                   <%-- <img alt="" src="../images/accred.jpg" style="width: 100px; height: 100px" />--%></a></td>
            <td style="" data-bind="visible: Agent">
               
                 <a href="./SearchRecordal.aspx" >
                    <img src="../images/recordals.png" />
                   <%-- <img alt="" src="../images/accred.jpg" style="width: 100px; height: 100px" />--%></a>
                <%--<a href="./Accred_Process.aspx" >
                    <img alt="" src="../images/online.jpg" style="width: 100px; height: 100px" /></a>--%>

                </td>
            <td style="width: 30%;" data-bind="visible: Agent">
                 <a href="./SearchPatent.aspx">
                    <img alt="" src="../images/patents.png" style=" height: 100px" /></a>

                <%-- <img alt="" src="../images/patents.png" style="width: 100px; height: 100px" />--%>
               
              
                </td>
        </tr> 

     <tr class="center-align">
           <td style="width: 30%;" data-bind="visible: Agent">
                <a href="./SearchDesign.aspx" >UPDATE DESIGN DOCUMENTS</a></td>
            <td style="width: 30%;" data-bind="visible: Agent">
                 <a href="./SearchRecordal.aspx" >UPDATE RECORDAL DOCUMENTS</a>
                </td>
            <td style="width: 30%;" data-bind="visible: Agent">
                <a href="./SearchPatent.aspx" >UPDATE PATENT DOCUMENTS</a>
               
                </td>
        </tr>   

     <tr class="center-align">
            <td colspan="3" >
                &nbsp;</td>
        </tr>  

     <tr class="center-align">
          <td style="" data-bind="visible: SuperAdmin">
                  <a href="./Accred_Process.aspx" >
                    <img src="../images/accreditation.png" />
                   <%-- <img alt="" src="../images/accred.jpg" style="width: 100px; height: 100px" />--%></a></td>
           
            <td style="width: 30%;" data-bind="visible: Agent">
               
             

                </td>
            <td style="width: 30%;" data-bind="visible: Agent">
                
                </td>
        </tr> 

     <tr class="center-align">

          
          <td style="width: 30%;" data-bind="visible: SuperAdmin">
                <a href="./Accred_Process.aspx" >AGENT ACCREDITATION</a></td>
           
            <td style="width: 30%;" data-bind="visible: Agent">
               
                </td>
            <td style="width: 30%;" data-bind="visible: Agent">
               
                </td>
        </tr>  

        </table>
   
    </div>

<div id="ads" style="color:#fff;text-align:center;font-family: Arial, Helvetica, sans-serif;font-size: 12px;font-weight: 600;" class="col-xs-2 col-sm-2 col-md-2 col-lg-2">
    
    <div id="flyout_menu2"> 
<div class="row">

    <div ng-show="xvv" style="margin-top:20px" >
  <span class="col-xs-3 col-sm-3 col-md-3 col-lg-3">UNREAD MESSAGE  </span>   <a href="./profile4.aspx" class="blinkytext col-xs-2 col-sm-2 col-md-2 col-lg-2 " > <blink > <span id="Span1" class="label label-danger" ng-bind="vcount" style="float:left"></span> </blink> </a>

      
    </div>

    </div>

        <div class="row">
            <span class="glyphicon glyphicon-envelope" style="text-align:left"><a href="./profile4.aspx"  style="color:white;"  > INBOX</a></span>
            </div>
   
    </div>
        </div>
   
<div class="row">
<div id="left_footer_menu" class="col-xs-5 col-sm-5 col-md-5 col-lg-5">
<a href="#">RELATED LINKS</a> | <a href="#">NEWS</a> | <a href="#">IPO WEBSITE</a> | <a href="#">PUBLICATIONS</a> | <a href="../user_guide.pdf" target="_blank">USER GUIDE</a> 

</div>
<div id="right_footer_menu" class="col-xs-offset-2 col-sm-offset-2 col-md-offset-2 col-lg-offset-2 col-xs-5 col-sm-5 col-md-5 col-lg-5">EVENTS &amp; FEATURES</div>

    </div>
    <div class="row" id="bottom_footer">
<div  class="col-xs-offset-5 col-sm-offset-5 col-md-offset-5 col-lg-offset-5 col-xs-7 col-sm-7 col-md-7 col-lg-7">
   <b style="font-family:Cambria;font-size:13px; float:left">POWERED BY<br />  <img alt="Einao Solutions" class="style2"   src="../images/einao_logo.png" /></b>
   </div>
        </div>

</div>
        </div>
    </div>
<script type="text/javascript">
    new CountUp('<%=log_date %>', 'sc', " Since logged on");
    </script>
</body>
</html>