<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xreport_apps.aspx.cs" Inherits="Ipong.A.xreport_apps"  MaintainScrollPositionOnPostback="true"%>

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
<style type="text/css">
        .grid_print tr td {border-top: 1px solid red;border-bottom: 1px solid red;}
    </style>

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
   <li><a href='#'><span>USER GUIDE</span></a></li>
  
</ul>
</div>

</div>

<div id="x_main">
<table class="center-align" style="width:100%;">
        <tr>
            <td class="center-align" style="background-color:#efefef;" colspan="5">
               PLEASE SELECT THE SEARCH CRITERIA BELOW</td>
        </tr>
        <tr >
            <td colspan="5" >
                <asp:HiddenField ID="xadminID" runat="server" />
            </td>
        </tr>

        <tr >
            <td colspan="5" >
                &nbsp;</td>
        </tr>
        <tr >
            <td style="width:20%; text-align:center;" >
                Category:<br />
                <asp:DropDownList ID="ddl_cat" runat="server" >
                <asp:ListItem Value="ag" Text="Accreditation"></asp:ListItem>
                <asp:ListItem Value="ds" Text="Design"></asp:ListItem>
                <asp:ListItem Value="pt" Text="Patent"></asp:ListItem>
                <asp:ListItem Value="tm" Text="Trademark" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width:20%; text-align:center;" >
                Status<br />
                 <asp:DropDownList ID="ddl_status" runat="server" >
                <asp:ListItem Value="Used" Text="Used" Selected="True"></asp:ListItem>                
                </asp:DropDownList>
            </td>
            <td style="width:20%; text-align:center;" >
                Mode<br />
                 <asp:DropDownList ID="ddl_mode" runat="server" >
                <asp:ListItem Value="xpay_bk" Text="Bank"></asp:ListItem>
                <asp:ListItem Value="xpay_isw" Text="Interswitch" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width:20%; text-align:center;" >
                From<br />
                <asp:TextBox ID="fromDate" runat="server" CssClass="textbox dt"></asp:TextBox>
            </td>
            <td style="width:20%; text-align:center;" >
                To<br />
                <asp:TextBox ID="toDate" runat="server" CssClass="textbox dt"></asp:TextBox>
            </td>
        </tr>
        <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align" colspan="5"></td>
        </tr>
        <tr >
            <td colspan="5" >            
                <asp:Button ID="BtnReport" runat="server" class="button" Text="Get Report" onclick="BtnReport_Click"  />            
                        </td>
        </tr>
         <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align" colspan="5"></td>
        </tr>


        <% if (tm_cnt > 0)
           { %>

           <% if (show_inv == 0)
              { %>
        
        <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align" colspan="5">
                --- <%=tm_cnt %> RECORDS FOUND FOR THE FOLLOWING CRITERIA ---<br /><strong></strong>
                Category&nbsp;[ <strong>"<%=ddl_cat.SelectedItem.Text %>"</strong> ]&nbsp;&nbsp;Status&nbsp;[ <strong>"<%=ddl_status.SelectedItem.Text%>"</strong> ]&nbsp;&nbsp;Mode&nbsp;[ <strong>"<%=ddl_mode.SelectedItem.Text%>"</strong> ]&nbsp;&nbsp;Initial Date&nbsp;[ <strong>"<%=fromDate.Text %>"</strong> ]&nbsp;&nbsp;Final Date&nbsp;[ <strong>"<%=toDate.Text%>"</strong> ]                
                </td>
        </tr>
        <tr class="center-align" style="background-color:#efefef;">
            <td class="center-align" colspan="5">
                &nbsp;</td>
        </tr>
        <tr id="ReportGrid" style="text-align:center;">
            <td colspan="5">
                <asp:GridView ID="gvTm" runat="server" AutoGenerateColumns="False"  EnableModelValidation="True" 
                style="margin-top: 0px; width:100%;" onrowcommand="gvTm_RowCommand" 
                    CellPadding="4" ForeColor="#333333" GridLines="Both" CaptionAlign="Left"  HorizontalAlign="Left"
                    AllowPaging="True"  onpageindexchanging="gvTm_PageIndexChanging" PageSize="50" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>   
                       <asp:TemplateField HeaderText="S/N">
                            <ItemTemplate>
                                 <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="newtransID" HeaderText="TRANSACTION ID"  >
                             <HeaderStyle HorizontalAlign="Left" Width="80px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="item_code" HeaderText="ITEM CODE" ReadOnly="True">
                             <HeaderStyle HorizontalAlign="Left" Width="45px"/>
                           <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                          <asp:BoundField DataField="item_desc" HeaderText="ITEM DESCRIPTION" ReadOnly="True">
                             <HeaderStyle HorizontalAlign="Left" Width="300px"/>
                           <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                          <asp:BoundField DataField="payment_date" HeaderText="PAYMENT DATE" >
                            <HeaderStyle HorizontalAlign="Left" Width="60px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         
                           <asp:BoundField DataField="office_status" HeaderText="CURRENT OFFICE" >
                            <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                          <asp:BoundField DataField="data_status" HeaderText="STATUS" >
                            <HeaderStyle HorizontalAlign="Left" Width="80px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="DETAILS" >
                        <ItemTemplate>
                        <asp:ImageButton ID="lbDetTm" ImageUrl="../images/search.gif" runat="server" Height="16px" CommandName="TmDetailsClick"  CommandArgument='<%#Eval("transID") %>'  />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="20px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                          
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"/>
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB"/>
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>                 
                        
            </td>
        </tr>

        <tr style="width:100%;text-align:center;">
        <td colspan="5">
        <input type="button" name="Printform" id="PrintReport" value="Print" onclick="PrintPartner('ReportGrid'); return false" class="button" />&nbsp;
            <asp:Button ID="btnExportExcel" runat="server" class="button" onclick="btnExportExcel_Click" Text="Export Excel" />
        </td>
        </tr>
         <% } %>
      
         <% if (show_inv ==2)
            { %>
        <tr id="TmArk" >
        <td colspan="5" align="center" width="100%">
       <table   class="form"  width="100%">
       <tr>
            <td colspan="2" style="text-align:center;">
                <img alt="Coat Of Arms" height="79" src="../images/coat_of_arms.png"  width="85" /></td>
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
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;" 
                colspan="2">
                --- APPLICANT INFORMATION ---</td>
        </tr>
            <tr>
            <td colspan="2">&nbsp;</td>
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
                &nbsp;</td>                                
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
            <td style="text-align:center;" colspan="2">
                &nbsp;</td>                                
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
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" 
                style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;">
                --- TRADEMARK INFORMATION --- </td>
        </tr>
           <tr>
            <td colspan="2">&nbsp;</td>
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
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;" 
                colspan="2"> 
                --- TRADEMARK REPRESENTATION --- </td>
        </tr>
           <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:center;" colspan="2">   
                
                 <% if (c_mark.logo_pic != "")
                    {%>
                <asp:Image ID="tm_img" runat="server" />
                <% }
                    else
                    {   
                        Response.Write(c_mark.product_title); } %>
                </td>
        </tr>
           <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" 
                style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;">
                --- ATTORNEY INFORMATION ---
            </td>
        </tr>
           <tr>
            <td colspan="2">&nbsp;</td>
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
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td style="background-color:#1C5E55; color:#ffffff; text-align:center; font-weight:bold;" colspan="2">
                &nbsp;ADDRESS :
                </td>
        </tr>
         <tr>
            <td colspan="2">&nbsp;</td>
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
            <td colspan="2">&nbsp;</td>
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
        
        <tr style="width:100%;text-align:center;">
        <td colspan="5">
            
                <asp:Button ID="BtnBackToList" runat="server" class="button" 
                                Text="Back to Transactions List" onclick="BtnBackToList_Click" />            
                        &nbsp;<input type="button" name="Printform" id="Button1" value="Print" onclick="PrintPartner('TmArk'); return false" class="button" />
        </td>
        </tr>
        <% }%>
               
               <% if (show_inv ==3)
            { %>
        <tr id="PtArk" >
        <td colspan="5" align="center" width="100%">
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
        
        <%if (lt_pt_mi.Count > 0)
          { %>
        <tr>
            <td width="100%" align="right" colspan="2" class="sub_header">
                </td>
        </tr>
        
        <tr>
            <td width="50%" align="right">
                &nbsp;FILLING/APPLICATION DATE :             </td>
            <td>
               
                <asp:Label ID="Label2" runat="server" Text="Label"><% Response.Write(lt_pt_mi[0].reg_date); %></asp:Label>&nbsp;</td>
        </tr>
        
        <tr>
            <td align="right">
                REGISTRATION NUMBER :
            </td>
                <td>
                  <asp:Label ID="Label3" runat="server" Text="Label"><% Response.Write(lt_pt_mi[0].reg_number); %></asp:Label>
                    </td>
        </tr>
         <tr>
            <td align="right"> 
                                &nbsp;
                                ONLINE APPLICATION ID : </td>
            <td>
                 
                <asp:Label ID="Label16" runat="server" Text="Label"><% Response.Write("OAI/PT/"+t.ValidationIDByPwalletID(lt_pt_mi[0].log_staff) ); %></asp:Label></td>
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
                 
                  <asp:Label ID="Label6" runat="server" Text="Label"><% Response.Write(lt_pt_mi[0].xtype); %></asp:Label></td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;TITLE OF INVENTION :
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Label"><% Response.Write(lt_pt_mi[0].title_of_invention); %></asp:Label></td>
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
                <asp:Label ID="Label19" runat="server" Text="Label"><% Response.Write(lt_repx[0].address); %></asp:Label></td>
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
            <%if ((lt_pt_mi[0].loa_doc == "")||(lt_pt_mi[0].loa_doc == "0"))
              { %> NOT ATTACHED<%}
              else
              { %> ATTACHED<%} %></td>
        </tr>
        
        <tr>
            <td align="right">
                CLAIM(S) :</td>
            <td >
                <%if ((lt_pt_mi[0].claim_doc == "") || (lt_pt_mi[0].claim_doc == "0"))
                  { %> NOT ATTACHED<%}
                  else
                  { %> ATTACHED<%} %></td>
        </tr>
        
        <tr>
            <td align="right">
                PCT DOCUMENT:</td>
            <td >
                  <%if ((lt_pt_mi[0].pct_doc == "") || (lt_pt_mi[0].pct_doc == "0"))
                    { %> NOT ATTACHED<%}
                    else
                    { %> ATTACHED<%} %></td>
        </tr>
        
        <tr>
            <td align="right">
                DEED OF ASSIGNMENT:</td>
            <td >
                  <%if ((lt_pt_mi[0].doa_doc == "") || (lt_pt_mi[0].doa_doc == "0"))
                    { %> NOT ATTACHED<%}
                    else
                    { %> ATTACHED<%} %></td>
        </tr>
        
        <tr>
            <td align="right">
                COMPLETE SPECIFICATION (FORM 3):</td>
            <td >
                  <%if ((lt_pt_mi[0].spec_doc == "") || (lt_pt_mi[0].spec_doc == "0"))
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
        
        <tr style="width:100%;text-align:center;">
        <td colspan="5">
            
                <asp:Button ID="BtnBackToListPt" runat="server" class="button" 
                                Text="Back to Transactions List" onclick="BtnBackToList_Click" />            
                        &nbsp;<input type="button" name="Printform" id="BtnPrintArkPt" value="Print" onclick="PrintPartner('PtArk'); return false" class="button" />
        </td>
        </tr>
        <% }
               
           }
           else
           {%>  
               
        
           <tr>
            <td class="center-align" colspan="5">
         <strong>THERE ARE CURRENTLY NO TRANSACTIONS AVAILABLE!!!</strong>
            </td>
        </tr>
         <tr>
           <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" colspan="5">&nbsp;</td>
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
