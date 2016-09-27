<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xmail.aspx.cs" Inherits="Ipong.A.xmail"  MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>WELCOME TO IPO NIGERIA OFFICIAL WEB PORTAL</title>
    <link href="../css/ain_structure.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="../css/jquery.ui.all.css" />
     <link rel="stylesheet" href="../css/jquery.ui.theme.css" />
     <link rel="stylesheet" href="../css/jquery.ui.tabs.css" />
     <link href="../css/jquery.ui.timepicker.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../ui/jquery.ui.datepicker.js"></script>
    <script src="../js/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../js/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../js/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../js/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
     <script src="../js/timers.js" type="text/javascript"></script> 
    <script src="../js/aj.js" type="text/javascript"></script>
   <script src="../js/funk.js" type="text/javascript"></script>
    <script type="text/javascript">
        showClock();
        </script>

<script type="text/javascript">
    $(function () {
        $(".dt").datepicker({
            changeMonth: true,
            changeYear: true, yearRange: 'c-10:c+10',
            showButtonPanel: true,
            dateFormat: 'yy-mm-dd'
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
<a href="./profile.aspx"><img src="../images/LOGOCLD.jpg" width="458px" height="76px"  alt="CLD" /></a>
</div>
</div>

</div>

<div id="main">
<div id="welcome">
    <div style="float:left; padding:5px 0 0 10px;">
   <img alt="Principal Photo" <%if( (Session["pic"].ToString()!="")&&(Session["pic"]!=null)){ %> src="<%=Session["pic"].ToString() %>"<%} else { %>src="../images/shadow_male.jpg"<%} %> />
    </div>
    <h1>WELCOME <%if(Session["coy_name"]!=null){Response.Write(Session["coy_name"].ToString().ToUpper());}%> </h1>
    <img alt="Einao Solutions" <%if( (Session["logo"].ToString()!="")&&(Session["logo"]!=null)){ %> src="<%=Session["logo"].ToString() %>"<%} else { %>src="../images/einao_logo.png"<%} %>  style="width: 200px; height: 70px"  /><br />
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

   <li><a href='./xmail.aspx'><span>CONTACT IPO</span></a></li> <li><a href='./feelist.aspx'><span>FEE LIST ITEMS</span></a></li>
</ul>
</div>

<div class="side_hd">--- IPO WEBSITE--- </div>
<div id='home_menu'>
<ul>
   <li><a href='#'><span>HOME</span></a></li>
   <li> <a href="#" ><span>NEWS</span></a></li>
   <li><a href='#'><span>RELATED LINKS</span></a></li>
   <li> <a href="#" ><span>PUBLICATIONS</span></a></li>
   <li><a href="../user_guide.pdf" target="_blank"><span>USER GUIDE</span></a></li>
  
</ul>
</div>

</div>

<div id="x_main">
<table class="center-align" style="width:100%;">
        <tr>
            <td class="center-align" style="background-color:#efefef;">
                CATEGORY</td>
        </tr>
        <tr >
            <td  align="center">
                <asp:RadioButtonList ID="rbl_mail_cat" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                <asp:ListItem Value="Complaints" Text="Complaints" Selected="True"></asp:ListItem>
                <asp:ListItem Value="Enquires" Text="Enquires"></asp:ListItem>
                <asp:ListItem Value="Feedback" Text="Feedback"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>

         <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align"></td>
        </tr>
        <tr >
            <td style="text-align:center;" >
                MESSAGE:<br />
                <br />
                <asp:TextBox ID="txt_msg" runat="server" Rows="10" TextMode="MultiLine" Width="80%" CssClass="textbox"></asp:TextBox>
                <br />
                </td>
        </tr>      
       
         <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align"></td>
        </tr>
         <tr >
            <td > <asp:Button ID="btnSubmit" runat="server" Text="Send" CssClass="button" onclick="btnSubmit_Click" /></td>    
            </tr>      
            <%if (xsucc == 1)
              { %>   
        
           <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align"></td>
        </tr>
         <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align">MESSAGE SENT SUCCESSFULLY!!</td>
        </tr>
        <% } %> 
        <%if (xsucc == 2)
              { %>  
       
           <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align"></td>
        </tr>
         <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align">PLEASE ENTER A VALID MESSAGE!!</td>
        </tr>
        <% } %> 
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
