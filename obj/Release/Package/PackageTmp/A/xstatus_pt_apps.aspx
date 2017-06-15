<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xstatus_pt_apps.aspx.cs" Inherits="Ipong.A.xstatus_pt_apps"  MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html >
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
<form id="form1" runat="server" >
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

   <li><a href='./xmail.aspx'><span>CONTACT IPO</span></a> </li>
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
       

       <% if (showtm == 1)
            { %>
        <tr id="status" class="center-align">
        <td>
        <table align="center" width="100%" class="form" 
                     id="table1">
                    <tr align="center">
                <td colspan="2">
                    <img alt="Coat Of Arms" height="79" src="../images/coat_of_arms.png" 
                        width="85" />
               </td>
            </tr>
            <tr align="center" style=" font-size:11pt;" >
                <td colspan="2">
                    FEDERAL REPUBLIC OF NIGERIA<br />
                    FEDERAL MINISTRY OF INDUSTRY, TRADE AND INVESTMENT<br />
                    COMMERCIAL LAW DEPARTMENT<br />
                    TRADEMARKS, PATENTS AND DESIGNS REGISTRY<br />
                    PATENTS AND DESIGNS DECREE NO 60 OF 1970
</td>
            </tr>
                   
                    <tr>
                        <td width="100%" colspan="2" class="tbbg">
                            <strong>---STATUS INFORMATION---</strong>
                        </td>
                    </tr>
                     <% if (refill == 0)
                       { %>
                    <tr>
                        <td width="100%" align="justify" colspan="2">
                         Dear 
                            <% if (fullname != null) { Response.Write(fullname); } else { Response.Write(lt_pw[0].validationID); }%>, 
                            <br /> We will like to inform you of the current position of your application ("/OAI/TM/<% Response.Write(lt_pw[0].validationID); %>") below: <br /><br />
                            Current Office: <strong>  "<% Response.Write(status); %>"</strong><br />
                            Current Status: <strong>"<% Response.Write(data_status); %>"</strong><br />
                            Regards,
                        </td>
                    </tr>
                     <% }
                       else if(refill==1){ %>
                        <tr>
                        <td width="100%" align="justify" colspan="2">
                         Dear 
                            <% if (fullname != null) { Response.Write(fullname); } else { Response.Write(lt_pw[0].validationID); }%>, 
                            <br /> We will like to inform you that your application ("OAI/TM/<% Response.Write(r); %>&quot;) 
                            has not been completed successfully!!!<br /> Please click 
                            on the &quot;COMPLETE PATENT APPLICATION&quot; button below to update the patent 
                            details<br />
                            Regards,
                        </td>
                    </tr>
                    <% } %>
                    <tr>
                        <td class="tbbg" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width:50%">
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
                        <td class="tbbg" colspan="2">
                            &nbsp;
                        </td>
                    </tr>                  
                </table>
        </td>
        </tr>
         <tr>
                        <td align="center" colspan="2">
                           
                <input type="button" name="Printform" id="Printform" value="Print" onclick="PrintHomePt('status'); return false" class="button" />
                <% if (refill == 0)
                   { %>
                        &nbsp;<asp:Button ID="BtnReprintTmAck" runat="server" class="button" Text="Reprint Acknowledgment Slip" onclick="BtnReprintTmAck_Click" />  
                       
                        <% } else if (refill == 1)
                   { %>
                            <asp:Button ID="btnCompletePatent" runat="server" class="button" Text="Complete Patent Application2" onclick="BtnReprintTmAck_Click" /> 
                            <asp:Button ID="Button2" runat="server" onclick="BtnReprintTmAck_Click" Text="Button" />
                           <%-- <input id="btnCompletePatent" class="button" name="btnCompletePatent" 
                                onclick="BtnReprintTmAck_Click"  type="button" value="Complete Patent Application" />--%>
                             <% }%>
                            <asp:Button ID="btnNewSearch" runat="server" class="button" Text="New Search" onclick="btnNewSearch_Click" />  
                        </td>
                    </tr>
        <% }
           %> 
        
          <% if (showtm == 2)
            { %>
        <tr id="ark" >
        <td align="center">
       <table align="center" width="1000px" class="form" >    
        <tr>
            <td colspan="2" align="center" >
             <img alt="Coat Of Arms" height="79" src="../images/coat_of_arms.png" 
                        width="85" /><br />
              FEDERAL REPUBLIC OF NIGERIA<br />
                    FEDERAL MINISTRY OF INDUSTRY, TRADE AND INVESTMENT<br />
                    COMMERCIAL LAW DEPARTMENT<br />
                    TRADEMARKS, PATENTS AND DESIGNS REGISTRY<br />
                    PATENTS AND DESIGNS DECREE NO 60 OF 1970<br />
                   <div style="font-size:20px;"><strong>ACKNOWLEDGMENT FORM</strong></div> 
            </td>
        </tr>       
        
        <%if (lt_mi.Count > 0)
          { %>
        <tr>
            <td width="100%" align="right" colspan="2" class="sub_header">
                </td>
        </tr>
        
        <tr>
            <td width="50%" align="right">
                &nbsp;FILLING/APPLICATION DATE :             </td>
            <td>
               
                <asp:Label ID="Label1" runat="server" Text="Label"><% Response.Write(lt_mi[0].reg_date); %></asp:Label>&nbsp;</td>
        </tr>
        
        <tr>
            <td align="right">
                REGISTRATION NUMBER :
            </td>
                <td>
                  <asp:Label ID="Label2" runat="server" Text="Label"><% Response.Write(lt_mi[0].reg_number); %></asp:Label>
                    </td>
        </tr>
         <tr>
            <td align="right"> 
                                &nbsp;
                                ONLINE APPLICATION ID : </td>
            <td>
                 
                <asp:Label ID="Label6" runat="server" Text="Label"><% Response.Write("OAI/PT/"+t.ValidationIDByPwalletID(lt_mi[0].log_staff) ); %></asp:Label></td>
        </tr>
         <tr>
            <td colspan="2" class="tbbg">
                --- 
                PATENT INFORMATION --- </td>
        </tr>
        
        <tr>
            <td align="right">
                &nbsp;PATENT TYPE :</td>
                <td>
                 
                  <asp:Label ID="Label3" runat="server" Text="Label"><% Response.Write(lt_mi[0].xtype); %></asp:Label></td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;TITLE OF INVENTION :
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Label"><% Response.Write(lt_mi[0].title_of_invention); %></asp:Label></td>
        </tr>
        <tr>
            <td align="right">
                TRANSACTION ID :
                </td>
            <td>
                <% Response.Write(lt_stage[0].validationID); %></td>
        </tr>
        
        <tr>
            <td align="right">
                TRANSACTION AMOUNT :
                </td>
            <td>
                <% Response.Write(lt_stage[0].amt); %>  NGN
                </td>
        </tr>  
        <%}%>      
       <%if (lt_app.Count > 0)
         {
            %>
        <tr>
            <td class="tbbg" colspan="2">
                --- APPLICANT INFORMATION ---</td>
        </tr>
         <%             for (int app = 0; app < lt_app.Count; app++)
             {%>
        <tr>
            <td align="left" colspan="2" style="background-color:#999999;">
                <strong>APPLICANT <%=app + 1%>>></strong></td>
        </tr>
        
        <tr>
            <td align="right">
                NAME :</td>
                <td>
                    <% Response.Write(lt_app[app].xname); %></td>
        </tr>
        
        <tr>
            <td align="right">
                ADDRESS :</td>
                <td>
                    <% Response.Write(lt_app[app].address); %></td>
        </tr>
        
        <tr>
            <td align="right">
                PHONE NUMBER :
            </td>
                <td>
                    <% Response.Write(lt_app[app].xmobile); %></td>
        </tr>
        
        <tr>
            <td align="right">
                E-MAILS :</td>
                <td>
                    <% Response.Write(lt_app[app].xemail); %></td>
        </tr>
        <tr>
            <td align="right">
                NATIONALITY :</td>
                <td>
                   <% Response.Write(t.getCountryName(lt_app[app].nationality)); %></td>
        </tr>
       
         <%
             }
         }%>
         <%if (lt_inv.Count > 0)
           {
              %>  
        
       
        <tr>
            <td class="tbbg" colspan="2">
                --- TRUE INVENTOR INFORMATION ---</td>
        </tr>
        <%   for (int inv = 0; inv < lt_inv.Count; inv++)
               {%>
        <tr>
            <td align="left" colspan="2" style="background-color:#999999;">
                <strong>INVENTOR <%=inv+1%>>></strong></td>
        </tr>
        
        <tr>
            <td align="right">
                NAME :</td>
                <td>
                    <% Response.Write(lt_inv[inv].xname); %></td>
        </tr>
        
        <tr>
            <td align="right">
                ADDRESS :</td>
                <td>
                    <% Response.Write(lt_inv[inv].address); %></td>
        </tr>
        
        <tr>
            <td align="right">
                PHONE NUMBER :
            </td>
                <td>
                    <% Response.Write(lt_inv[inv].xmobile); %></td>
        </tr>
        
        <tr>
            <td align="right">
                E-MAILS :</td>
                <td>
                    <% Response.Write(lt_inv[inv].xemail); %></td>
        </tr>
        <tr>
            <td align="right">
                NATIONALITY :</td>
                <td>
                   <% Response.Write(t.getCountryName(lt_inv[inv].nationality)); %></td>
        </tr>
        <%
               }
           }%>
        <%if(lt_assinfo.Count>0){ %>
        <tr>
            <td class="tbbg" colspan="2">
                --- ASSIGNMENT INFORMATION ---</td>
        </tr>
          <tr>
            <td align="right">
                DATE OF ASSIGNMENT :</td>
                <td>
                    <% Response.Write(lt_assinfo[0].date_of_assignment); %></td>
        </tr>
        <tr>
            <td align="left" colspan="2" style="background-color:#999999;">
                <strong>ASSIGNEE >></strong></td>
        </tr>
       
        <tr>
            <td align="right">
                NAME :</td>
                <td>
                    <% Response.Write(lt_assinfo[0].assignee_name); %></td>
        </tr>
        
        <tr>
            <td align="right">
                ADDRESS :</td>
                <td>
                    <% Response.Write(lt_assinfo[0].assignee_address); %></td>
        </tr>
         <tr>
            <td align="right">
                NATIONALITY :</td>
                <td>
                   <% Response.Write(t.getCountryName(lt_assinfo[0].assignee_nationality)); %></td>
        </tr>
       <tr>
            <td align="left" colspan="2" style="background-color:#999999;">
                <strong>ASSIGNOR >></strong></td>
        </tr>
        
        <tr>
            <td align="right">
                NAME :</td>
                <td>
                    <% Response.Write(lt_assinfo[0].assignor_name); %></td>
        </tr>
        
       
       
        <tr>
            <td align="right">
                ADDRESS&nbsp; :</td>
                <td>
                     <% Response.Write(lt_assinfo[0].assignor_address); %></td>
        </tr>
        <tr>
            <td align="right">
                NATIONALITY :</td>
                <td>
                   <% Response.Write(t.getCountryName(lt_assinfo[0].assignor_nationality)); %></td>
        </tr>
        <%} %>
        <%if(lt_xpri.Count>0){%>
        <tr>
            <td colspan="2" class="tbbg">
                --- PRIORITY INFORMATION ---</td>
        </tr>
        <tr>
            <td colspan="2" style="border:0px outset silver; padding: 0px;">
                <table width="100%">
                <tr style="background-color:#999999;">
                <td style="width:10%;">
                    <strong>S/N</strong></td>
                <td style="width:30%;">
                    <strong>COUNTRY</strong></td>
                <td style="width:30%;">
                    <strong>APPLICATION NUMBER</strong></td>
                <td style="width:30%;">
                    <strong>DATE</strong></td>
                </tr>

                 <%
                     for (int pri = 0; pri <lt_xpri.Count; pri++)
              {%>
                <tr >
                <td>
                <%=pri + 1%>
                </td>
                <td>
                    <% Response.Write(t.getCountryName(lt_xpri[pri].countryID)); %></td>
                <td>
                    <% Response.Write(lt_xpri[pri].app_no); %></td>
                <td>
                    <% Response.Write(lt_xpri[pri].xdate); %></td>
                </tr>
                 <%
              }
          %>
                </table></td>
        </tr>
        <%
          }%>
        <%if (lt_repx.Count > 0)
          { %>
        <tr>
            <td colspan="2" class="tbbg">
                --- ADDRESS OF SERVICE IN NIGERIA ---
            </td>
        </tr>
        <tr>
            <td align="right">
                                ATTORNEY CODE :
                </td>
            <td>
                 <asp:Label ID="Label9" runat="server" Text="Label"><% Response.Write(lt_repx[0].agent_code); %></asp:Label>
                     </td>
        </tr>        
        
        
        <tr>
            <td align="right">
                                ATTORNEY NAME :</td>
            <td>
                <% Response.Write(lt_repx[0].xname); %></td>
        </tr>
        
        
        <tr>
            <td align="right">
                NATIONALITY :</td>
            <td>
                <% Response.Write(t.getCountryName(lt_repx[0].nationality)); %></td>
        </tr>
        
        
        <tr>
            <td align="right">
                COUNTRY :</td>
            <td>
               <% Response.Write(t.getCountryName(lt_repx[0].country)); %></td>
        </tr>
        
        
        <tr>
            <td align="right">
                STATE :</td>
            <td>
               <% Response.Write(t.getStateName(lt_repx[0].state)); %></td>
        </tr>
        
        
        <tr>
            <td align="right">
                &nbsp;ADDRESS :
                </td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Label"><% Response.Write(lt_repx[0].address); %></asp:Label></td>
        </tr>
        
        
        <tr>
            <td align="right">
                PHONE NUMBER : </td>
            <td>
                <% Response.Write(lt_repx[0].xmobile); %></td>
        </tr>
        
        
        <tr>
            <td align="right">
                E-MAIL : </td>
            <td>
                <% Response.Write(lt_repx[0].xemail); %></td>
        </tr>
        <tr>
            <td colspan="2" class="tbbg">
                --- DOCUMENTS ATTACHED ---
            </td>
        </tr>
       <tr>
            <td align="right">
               LETTER OF AUTHORIZATION OF AGENT(FORM 2) :
            </td>
            <td >
            <%if ((lt_mi[0].loa_doc == "")||(lt_mi[0].loa_doc == "0"))
              { %> NOT ATTACHED<%}
              else
              { %> ATTACHED<%} %></td>
        </tr>
        
        <tr>
            <td align="right">
                CLAIM(S) :</td>
            <td >
                <%if ((lt_mi[0].claim_doc == "") || (lt_mi[0].claim_doc == "0"))
                  { %> NOT ATTACHED<%}
                  else
                  { %> ATTACHED<%} %></td>
        </tr>
        
        <tr>
            <td align="right">
                PCT DOCUMENT:</td>
            <td >
                  <%if ((lt_mi[0].pct_doc == "") || (lt_mi[0].pct_doc == "0"))
                    { %> NOT ATTACHED<%}
                    else
                    { %> ATTACHED<%} %></td>
        </tr>
        
        <tr>
            <td align="right">
                DEED OF ASSIGNMENT:</td>
            <td >
                  <%if ((lt_mi[0].doa_doc == "") || (lt_mi[0].doa_doc == "0"))
                    { %> NOT ATTACHED<%}
                    else
                    { %> ATTACHED<%} %></td>
        </tr>
        
        <tr>
            <td align="right">
                COMPLETE SPECIFICATION (FORM 3):</td>
            <td >
                  <%if ((lt_mi[0].spec_doc == "") || (lt_mi[0].spec_doc == "0"))
                    { %> NOT ATTACHED<%}
                    else
                    { %> ATTACHED<%} %></td>
        </tr>
        <%}%>
        <tr>
            <td colspan="2" style="color: #fff; background-color: #006699; text-align: center; font-weight: bold;">
              </td>
        </tr>
        <tr>
            <td  align="center" colspan="2">
              
             <strong>YOUR APPLICATION HAS BEEN RECEIVED AND IS RECEIVING DUE ATTENTION</strong><br />
             <strong>TRADEMARKS, PATENTS AND DESIGNS REGISTRY 
                <br />
                COMMERCIAL LAW DEPARTMENT
                <br />
                FEDERAL MINISTRY OF INDUSTRY, TRADE AND INVESTMENT
                </strong></td>
        </tr>
        
         
    </table>
        </td>
        </tr>
         <tr>
            <td colspan="2">
                 <input type="button" name="Printform" id="Button1" value="Print" onclick="PrintHomePt('ark'); return false" class="button" />  
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
