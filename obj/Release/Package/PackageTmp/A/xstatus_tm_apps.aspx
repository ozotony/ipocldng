<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xstatus_tm_apps.aspx.cs" Inherits="Ipong.A.xstatus_tm_apps"  MaintainScrollPositionOnPostback="true"%>

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
        .style2  { width: 120px;height: 40px;}
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
            <li><a href="./xstatus_ds_apps.aspx"><span>DESIGN</span></a></li>
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
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;">
                APPLICATION STATUS SECTION</td>
        </tr>
      
           <% if (show_search == true)
              { %>
              <tr >
                 
            <td >
               <%-- Enter a valid Transaction ID :<br />--%>
                 <asp:DropDownList ID="DropDownList1" runat="server">
                     <asp:ListItem Selected="True">Select Search Criteria</asp:ListItem>
                     <asp:ListItem Value="TransactionId">Transaction Id</asp:ListItem>
                     <asp:ListItem Value="TpNumber">Tp Number</asp:ListItem>
                     <asp:ListItem Value="TrademarkName">Trademark Name</asp:ListItem>
                </asp:DropDownList>
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
       

       <% if (showtm == 1)
            { %>
        <tr id="status" class="center-align">
        <td>
        <table align="center" class="inv"  id="table1">
                    <tr>
                        <td colspan="2" align="center" width="100%">
                            <strong>FEDERAL MINISTRY OF TRADE AND INVESTMENT<br />
                                COMMERCIAL LAW DEPARTMENT<br />
                                INDUSTRIAL PROPERTY OFFICE REGISTRY </strong>
                        </td>
                    </tr>
                     <tr>
            <td style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;" colspan="2"  >   </td>
        </tr>
                    <tr>
                        <td colspan="2" style=" text-align:center; width:100%;">
                            <img src="../images/coat_of_arms.png"  style="width: 80px; height: 80px" />
                        </td>
                    </tr>
                   
                    <tr>
                        <td colspan="2" style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;width:100%">
                            <strong>---STATUS INFORMATION---</strong>
                        </td>
                    </tr>
                      <%if (lt_pw.Count > 0)
                              { %>
                    <tr>
                        <td width="100%" align="justify" colspan="2">
                         Dear 
                            <% Response.Write(fullname); %>, 
                          
                            <br /> We will like to inform you that your application ("/OAI/TM/<% Response.Write(lt_pw[0].validationID); %>") is currently at the <strong>  "<% Response.Write(status); %>" Office</strong><br />
                            Regards,
                        </td>
                    </tr>
                     <%} %>
                      <%if (lt_pw.Count == 0)
                              { %>
                      <tr>
                        <td width="100%" align="justify" colspan="2">
                         Dear 
                            <% Response.Write(fullname); %>,                           
                            <br />  The Transaction ID DOES NOT Exist on the system<br />
                            Regards,
                        </td>
                    </tr>
                     <%} %>
                    <tr>
                        <td style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;width:100%" colspan="2">
                           
                        </td>
                    </tr>
                    <tr>
                        <td  style="width:50%;">
                            <strong>&nbsp;THE TRADEMARK REGISTRY,<br />
                                &nbsp;COMMERCIAL LAW DEPARTMENT NIGERIA,<br />
                                &nbsp;FEDERAL MINISTRY OF TRADE AND INVESTMENT,<br />
                                &nbsp;FEDERAL CAPITAL TERRITORY,<br />
                                &nbsp;ABUJA,NIGERIA </strong>
                        </td>
                        <td align="right">
                            <strong>REGISTRAR OF TRADEMARKS&nbsp;&nbsp; </strong>
                           
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;width:100%" colspan="2">
                            
                        </td>
                    </tr>
                </table>
        </td>
        </tr>
         <tr>
            <td colspan="2">
                 <input type="button" name="Printform" id="Printform" value="Print" onclick="printStatus('status'); return false" class="button" /> 
                 <%if (lt_pw.Count > 0)
                              { %>                
                 <asp:Button ID="BtnReprintTmAck" runat="server" CssClass="button" Text="Reprint Acknowledgement Slip" onclick="BtnReprintTmAck_Click" />
                 <%}%>
&nbsp;<asp:Button ID="btnNewSearch" runat="server" class="button" Text="New Search" onclick="btnNewSearch_Click" />  

                <asp:Button ID="Button2" runat="server" class="button" Visible="false" Text="Acceptance Letter" onclick="btnNewSearch3_Click" /> 

                 <asp:Button ID="Button3" runat="server" class="button" Visible="false" Text="Refusal Letter" onclick="btnNewSearch4_Click" />  
                  <asp:Button ID="Button4" runat="server" class="button" Visible="false" Text="Certificate" onclick="btnNewSearch5_Click" />  
                 <asp:Button ID="Button5" runat="server" class="button" Visible="false" Text="Index Card" onclick="btnNewSearch6_Click" />  
            </td>
        </tr>
        <% }
           %> 
        
          <% if (showtm == 2)
            { %>
        <tr id="ark" >
        <td align="center">
       <table   class="inv"  width="100%">
       <tr>
            <td colspan="2" style="text-align:center;">
                <img alt="Coat Of Arms" height="79" src="../images/coat_of_arms.png" 
                        width="85" /></td>
        </tr>
       
        
        <tr>
            <td colspan="2"  style=" font-size:11pt;text-align:center;">
                 FEDERAL REPUBLIC OF NIGERIA<br />
                    FEDERAL MINISTRY OF INDUSTRY, TRADE AND INVESTMENT<br />
                    COMMERCIAL LAW DEPARTMENT<br />
                    TRADEMARKS, PATENTS AND DESIGNS REGISTRY<br />
                     </td>
        </tr>
          <tr>
            <td style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;" colspan="2"  >   </td>
        </tr>
        <%if ((c_mark.xID != null) && (c_mark.xID != "") && (c_rep.ID != null) && (c_rep.ID != "") && (c_stage.ID != null) && (c_stage.ID != "") && (c_app.ID != null) && (c_app.ID != ""))
          { %>
       <tr>
            <td style="font-size:16pt;line-height:115%; text-align:center;" colspan="2">
              TRADEMARK REGISTRATION ACKNOWLEDGEMENT FORM</td>
        </tr>
        
        
        <tr>
            <td  style="width:50%; text-align:right;">
                &nbsp;FILLING/APPLICATION DATE :             </td>
            <td style="width:50%;">
               
                <asp:Label ID="Label1" runat="server" Text="Label"><% Response.Write(c_mark.reg_date); %></asp:Label>&nbsp;</td>
        </tr>
           <tr>
            <td  style="text-align:center;">
               REGISTRATION NUMBER : </td>
            <td style="text-align:center;">
              ONLINE APPLICATION ID :</td>
        </tr>       
       
           <tr>
            <td style="text-align:center;">
                <% Response.Write(c_mark.reg_number); %></td>
            <td style="text-align:center;">
               <% Response.Write("OAI/TM/"+(c_stage.validationID) ); %></td>
        </tr>       
       
        <tr>
            <td style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;" 
                colspan="2">
                --- APPLICANT INFORMATION ---</td>
        </tr>
        
        <tr>
            <td style="text-align:center;" colspan="2">
                APPLICANT NAME:</td>
        </tr>
        
        <tr>
            <td style="text-align:center;" colspan="2">
                  <% Response.Write(c_app.xname); %></td>
        </tr>
        
      
        
        <tr>
            <td style="text-align:center;" colspan="2">
                ADDRESS :</td>                                
        </tr>
        
        <tr>
            <td style="text-align:center;" colspan="2">
                 <% Response.Write(t.getFormattedAddressByID(c_app.addressID)); %></td>
        </tr>
        
        <tr>
            <td style="text-align:center;">
                PHONE NUMBER :
            </td>
                <td style="text-align:center;">
                E-MAILS :
                   </td>
       </tr>
        
        <tr>
            <td style="text-align:center;">
                <% Response.Write(t.getAgentTelephone1ByID(c_app.addressID)); %></td>
                <td style="text-align:center;">
                     <% Response.Write(t.getAgentEmail1ByID(c_app.addressID)); %></td>
       </tr>
         
        <tr>
            <td colspan="2" 
                style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;">
                --- TRADEMARK INFORMATION --- </td>
        </tr>
          
        <tr>
            <td style="text-align:center;" colspan="2">
                &nbsp;&nbsp;TRADEMARK :</td>
        </tr>
        
        <tr>
            <td style="text-align:center;" colspan="2">
                <% Response.Write(c_mark.product_title); %></td>
        </tr>
         <tr>
            <td style="text-align:center;" colspan="2">
                &nbsp;</td>                                
        </tr>
        <tr>
            <td style="text-align:right;">
                &nbsp;SPECIFICATION 
                OF GOODS/SERVICES :
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Label"><% Response.Write(c_mark.nice_class); %></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align:center;" colspan="2">
                &nbsp;</td>                                
        </tr>
        <tr>
            <td style="text-align:center;" colspan="2">
                                &nbsp; SPECIFICATION OF GOODS/SERVICES DESCRIPTION : </td>
            
        </tr>
        <tr>
            <td style="text-align:center;" colspan="2"> 
                                <asp:Label ID="Label5" runat="server" Text="Label"><% Response.Write(c_mark.nice_class_desc); %></asp:Label>
                                </td>
        </tr>
        
        <tr>
            <td style="text-align:center;" colspan="2"> 
                                <strong>DISCLAIMER</strong></td>
        </tr>
        <tr>
            <td style="text-align:center;" colspan="2"> 
                                <asp:Label ID="Label7" runat="server" Text="Label"><% Response.Write((c_mark.disclaimer) ); %></asp:Label></td>
        </tr>
         
        <tr>
            <td style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;" 
                colspan="2"> 
                --- TRADEMARK REPRESENTATION --- </td>
        </tr>
          
        <tr>
            <td style="text-align:center;" colspan="2">   
                
                 <% if(c_mark.logo_descriptionID!="2")
                   {%>
                <asp:Image ID="tm_img" runat="server" />
                <% } %>
                <%                    
                   else
                   {    Response.Write(c_mark.product_title);
                    }
                     %>

                </td>
        </tr>
           
        <tr>
            <td colspan="2" 
                style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;">
                --- ATTORNEY INFORMATION ---
            </td>
        </tr>
          
         <tr>
            <td style="text-align:center;">
                ATTORNEY CODE : </td>
            <td style="text-align:center;">
               ATTORNEY NAME :
               </td>
        </tr>
        
         <tr>
            <td style="text-align:center;">
               <% Response.Write(c_rep.agent_code); %></td>
            <td style="text-align:center;">
                <% Response.Write(c_rep.xname); %></td>
        </tr>
        
        <tr>
            <td style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;" colspan="2">
                &nbsp;ADDRESS :
                </td>
        </tr>
        
        <tr>
            <td style="text-align:center;" colspan="2">
            <% Response.Write(t.getFormattedAddressByID(c_rep.addressID)); %></td>
        </tr>               
        
        <tr>
            <td style="text-align:center;">
                PHONE NUMBER : </td>
            <td style="text-align:center;">
                E-MAIL : 
               </td>
        </tr>

        <tr>
            <td style="text-align:center;">
                <% Response.Write(t.getAgentTelephone1ByID(c_rep.addressID)); %></td>
            <td style="text-align:center;">
                  <% Response.Write(t.getAgentEmail1ByID(c_rep.addressID)); %></td>
        </tr>
           
        <tr>
            <td colspan="2" 
                style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;">
                --- PAYMENT INFORMATION ---
            </td>
        </tr>
           <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
         <tr>
            <td style="text-align:center;">
                TRANSACTION ID : </td>
            <td style="text-align:center;">
                 TRANSACTION AMOUNT : 
               </td>
        </tr>
        
         <tr>
            <td style="text-align:center;">
                 <% Response.Write(c_stage.validationID); %></td>
            <td style="text-align:center;">
                 <% Response.Write(c_stage.amt); %></td>
        </tr>
        
        <tr>
            <td style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;" 
                colspan="2"  >              
                &nbsp;</td>
        </tr>
        <tr>
            <td  style="text-align:center;" colspan="2">
             <strong>Your application has been received and is receiving due attention</strong><br />
             <strong>REGISTRAR 
                (COMMERCIAL LAW DEPARTMENT NIGERIA)</strong> <br /></td>
        </tr>
         <% }else { %>
       
         
        <tr>
            <td  style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;" 
                colspan="2" >
              
                --- ERROR PRINTING ACKNOWLEDGEMENT AT THIS TIME ---&nbsp;</td>
        </tr>       
         
        <tr>
            <td  style="text-align:center;" colspan="2">              
                <strong>PLEASE CONTACT AN ADMINSTRATOR AS SOON AS POSSIBLE</strong></td>
        </tr>
        
        <% } %>
        
    </table>
        </td>
        </tr>
         <tr>
            <td colspan="2">
                 <input type="button" name="Printform" id="Button1" value="Print" onclick="printStatus('ark'); return false" class="button" /> 
                 <asp:Button ID="btnNewSearch2" runat="server" class="button" Text="New Search" onclick="btnNewSearch2_Click" />  
                

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
