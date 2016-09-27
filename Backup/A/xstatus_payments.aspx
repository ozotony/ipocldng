<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xstatus_payments.aspx.cs" Inherits="Ipong.A.xstatus_payments"  MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>WELCOME TO IPO NIGERIA OFFICIAL WEB PORTAL</title>
    <link href="../css/ain_structure.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
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

</div>

<div id="main">
<div id="welcome">
    <div style="float:left; padding:5px 0 0 10px;">
    <img alt="Principal Photo" <%if( (Session["pic"].ToString()!="")&&(Session["pic"]!=null)){ %> src="<%=Session["pic"].ToString() %>"<%} else { %>src="../images/shadow_male.jpg"<%} %> />
    </div>
    <h1>WELCOME <%if(Session["coy_name"]!=null){Response.Write(Session["coy_name"].ToString().ToUpper());}%> </h1>
    <img alt="Einao Solutions" <%if( (Session["logo"].ToString()!="")&&(Session["logo"]!=null)){ %> src="<%=Session["logo"].ToString() %>"<%} else { %>src="../images/einao_logo.png"<%} %>  style="width: 110px; height: 50px" /><br />
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
<table class="center-align" style="width:100%;">
        <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;">
                PAYMENT STATUS SECTION</td>
        </tr>
      
           <% if (show_search == 1)
              { %>
              <tr >
            <td >
                <asp:HiddenField ID="xadminID" runat="server" />
                Enter a valid Transaction ID :<br />
                <asp:TextBox ID="txt_status" runat="server" Width="400px" CssClass="textbox"></asp:TextBox>
               
            </td>
        </tr>
               <tr>
           <td class="center-align" style="background-color:#1C5E55; color:#ffffff;">
           </td>
         </tr>
          <tr >
            <td >     
                <asp:Button ID="btnSearch" runat="server" class="button" Text="Search" onclick="btnSearch_Click" />          
            </td>
        </tr>
        
         <% } %>     
       

       <% if (show_receipt ==1)
            { %>
        <tr id="invoice">
        <td>
        <table  style="font-size:12px;width:100%;">
        <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td colspan="2">
                INVOICE/RECIEPT FOR TRANSACTION :&nbsp;"<%=c_twall.ref_no%>" </td>
        </tr>
        <tr >
            <td  style="width:50%;">
                <img alt="Coat of Arms" height="69" src="../images/coat_of_arms.png" 
                    width="88" /></td>
            <td class="right-align" >
               <img src="../images/einao_logo.png"  alt="XPay" width="40%" height="40%" /></td>
        </tr>
        <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td class="center-align" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="center-align">
               <strong> DATE:</strong> <%=xreg_date %></td>
            <td class="center-align">
                <strong> INVOICE DATE:</strong>  <%=c_twall.xreg_date%></td>
        </tr>
        
        <tr>
            <td class="center-align" colspan="2" style="background-color:#666; color:#ffffff;">
                ---
                APPLICANT INFORMATION ---</td>
        </tr>
        
        <tr>
            <td class="center-align" colspan="2">
                <strong> NAME:</strong><br /><% =c_app.xname%></td>
        </tr>
        
        <tr>
            <td class="center-align" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="center-align">
                <strong> E-MAIL ADDRESS:</strong><br /><%= c_app.xemail%></td>
            <td class="center-align">
               <strong>MOBILE NUMBER:</strong><br /><%= c_app.xmobile%> </td>
        </tr>
        <tr>
            <td class="center-align" colspan="2" style="background-color:#666; color:#ffffff;">
                ---
                AGENT INFORMATION ---</td>
        </tr>
        <tr>
            <td class="center-align" colspan="2">
                <strong> NAME:</strong><br /><% =fullname%></td>
        </tr>
        <tr>
            <td class="center-align" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="center-align">
                <strong> E-MAIL ADDRESS:</strong><br /><%= email%></td>
            <td class="center-align">
               <strong>MOBILE NUMBER:</strong><br /><%= mobile%> </td>
        </tr>
        <tr>
            <td class="center-align" colspan="2"  style="background-color:#666; color:#ffffff;">
                <strong>--- PAYMENT DETAILS ---</strong></td>
        </tr>
        <tr>
            <td class="center-align">
                PAYMENT STATUS :&nbsp; "<strong><%=payment_status.ToUpper() %></strong>"</td>
            <td class="center-align">
                PAYMENT MODE :&nbsp; "<strong><%=xgt_type.ToUpper() %></strong>"</td>
        </tr>
        <tr>
            <td class="center-align" colspan="2" style="font-size:12px;">
                <table style="width:100%;" id="Table1" class="tiger-stripe" >
                    <tr style="background-color:#1C5E55; color:#ffffff;">

                        <td style="width:5%;">
                            <strong>S/N</strong></td>
                        <td style="width:20%;">
                            <strong>TRANSACTION ID</strong></td>
                        <td style="width:10%;">
                            <strong>ITEM CODE</strong></td>
                        <td style="width:50%;">
                            <strong>ITEM DESCRIPTION</strong></td>
                        <td style="width:15%;">
                            <strong>AMOUNT (<em>NGN</em> )</strong></td>
                    </tr>
                    <% 
                        foreach (Ipong.Classes.XObjs.PaymentReciept pr in lt_pr)
                       { %>
                    <tr>
                        <td>
                            <%=pr.sn%></td>
                        <td>
                            <%=pr.transID%></td>
                        <td>
                            <%=pr.item_code%></td>
                        <td>
                             <%=pr.item_desc%></td>
                        <td>
                        <%=pr.amount%></td>
                    </tr>
                     <%  } %>
                   
                    
                </table>
            </td>
        </tr>
         <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td colspan="2">
               
            </td>
        </tr>
       <tr style="font-size:16px;text-decoration:underline; color:#1C5E55; font-weight:bolder; text-align:right;">
            <td colspan="2" >
               <em>TOTAL AMOUNT:</em>&nbsp;<%=total_amt%> NGN</td>
        </tr>
       
       <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td colspan="2">
               
            </td>
        </tr>
        </table>
        </td>
        </tr>
         <tr>
            <td colspan="2">
                 <input type="button" name="Printform" id="Printform" value="Print" onclick="printStatus('invoice');return false" class="button" />                 <asp:Button ID="btnNewSearch" runat="server" class="button" Text="New Search" onclick="btnNewSearch_Click" />  
            </td>
        </tr>
        <% }
           %> 
        
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
