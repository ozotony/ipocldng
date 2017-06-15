<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xreport_payments.aspx.cs" Inherits="Ipong.A.xreport_payments"  MaintainScrollPositionOnPostback="true"%>

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
  <style type="text/css">
.tiger-stripe{text-align:left;font-weight:normal;font-size:11px;}
.tiger-stripe tr:nth-child(odd) {background: #E3EAEB;color:#000000;text-align:left;font-weight:normal;font-size:11px;}
</style>


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
                <asp:ListItem Value="all" Text="All"></asp:ListItem>
                <asp:ListItem Value="ag" Text="Accreditation"></asp:ListItem>
                <asp:ListItem Value="ds" Text="Design"></asp:ListItem>
                <asp:ListItem Value="pt" Text="Patent"></asp:ListItem>
                <asp:ListItem Value="tm" Text="Trademark" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width:20%; text-align:center;" >
                Payment Status<br />
                 <asp:DropDownList ID="ddl_status" runat="server" >    
                <asp:ListItem Value="4" Text="All"></asp:ListItem>             
                <asp:ListItem Value="1" Text="Paid" Selected="True"></asp:ListItem>
                <asp:ListItem Value="3" Text="Failed"></asp:ListItem>
                 
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
                Category&nbsp;[ <strong>"<%=ddl_cat.SelectedItem.Text %>"</strong> ]&nbsp;&nbsp;Payment Status&nbsp;[ <strong>"<%=ddl_status.SelectedItem.Text%>"</strong> ]&nbsp;&nbsp;Mode&nbsp;[ <strong>"<%=ddl_mode.SelectedItem.Text%>"</strong> ]&nbsp;&nbsp;Initial Date&nbsp;[ <strong>"<%=fromDate.Text %>"</strong> ]&nbsp;&nbsp;Final Date&nbsp;[ <strong>"<%=toDate.Text%>"</strong> ]                
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
                         
                          <asp:TemplateField HeaderText="DETAILS" >
                             <ItemTemplate>
                              <asp:ImageButton ID="lbDetTm" ImageUrl="../images/search.gif" runat="server" Height="16px" CommandName="TmDetailsClick"  CommandArgument='<%#Eval("transID") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="20px"/>
                             <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>

                         <asp:TemplateField HeaderText="DELETE" >
                             <ItemTemplate>
                              <asp:ImageButton ID="lbDelTm" ImageUrl="../images/x.gif" runat="server" Height="16px" CommandName="TmDeleteClick"  CommandArgument='<%#Eval("transID") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="20px"/>
                             <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>

                               <asp:TemplateField HeaderText="REQUERY">
                             <ItemTemplate>
                              <asp:ImageButton ID="lbReqTm" ImageUrl="../images/reload.png" runat="server" Height="16px" CommandName="TmReqClick"  CommandArgument='<%#Eval("transID") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="20px"/>
                             <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>

                                <asp:TemplateField HeaderText="COMPLETE PAYMENT">
                             <ItemTemplate>
                              <asp:ImageButton ID="lbPayTm" ImageUrl="../images/reload.png" runat="server" Height="16px" CommandName="TmPayClick"  CommandArgument='<%#Eval("transID") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="center" Width="20px"/>
                             <ItemStyle HorizontalAlign="center" />
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
      
         <% if (show_inv > 0)
            { %>
        <tr id="ReportDetails">
            <td class="center-align" colspan="5">
               <table  id="registration_form" class="center-align inv" width="100%" >           
            
         
               <tr >
            <td align="center" colspan="4">
                 <img alt="Coat of Arms"  src="../images/LOGOCLD.jpg" width="458" height="76" /></td>
        </tr>
         <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;border:1px solid #000000;">
            <td colspan="4">
                PAYMENT <%=pay_type %> FOR TRANSACTION :&nbsp;"<%if (lt_twall.Count > 0) { Response.Write(lt_twall[0].ref_no.ToUpper()); }%>" </td>
        </tr>
        

        <tr>
            <td align="center" style="width:50%;" colspan="2">
               <strong> TRANSACTION ID:</strong><%if (lt_twall.Count > 0) { Response.Write(lt_twall[0].transID.ToUpper()); }%></td>
            <td align="center" style="width:50%;" colspan="2">
                <strong> DATE:</strong>  <%=isw_fields.TransactionDate%></td>
        </tr>
        
          <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;">
            <td align="center" colspan="4">
               &nbsp;
                </td>
        </tr>
        <tr style=" text-align:center;font-weight:bold;">
            <td align="center" colspan="4">
               &nbsp;
                PAYMENT REFERENCE:&nbsp;"<%=isw_fields.pay_ref %>"
                </td>
        </tr>
        <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;">
            <td align="center" colspan="4">
               &nbsp;
                </td>
        </tr>
          <tr>
            <td align="center" colspan="2" style="background-color:#666; color:#ffffff;font-weight:bold;">
                ---
                APPLICANT INFORMATION ---</td>
            <td align="center" colspan="2" style="background-color:#666; color:#ffffff;font-weight:bold;">
                ---
                AGENT INFORMATION ---</td>
        </tr>
        
        <tr>
            <td align="left" style="width:7%;"> NAME:</td>
            <td align="left" style="width:43%;"> <% =c_app.xname%></td>
            <td align="left" style="width:7%;">NAME:  </td>
            <td align="left" style="width:43%;"> <% =fullname%></td>
        </tr>
        
        <tr style="background-color:#E3EAEB;">
            <td align="left">ADDRESS:</td>
            <td align="left"> <% =c_app.address%></td>
            <td align="left"> CODE:</td>
            <td align="left"> <%=cust_id%></td>
        </tr>
        <tr>
            <td align="left"> E-MAIL:   </td>
            <td align="left"> <%= c_app.xemail%></td>
            <td align="left"> E-MAIL:</td>
            <td align="left"> <%= email%></td>
        </tr>
        <tr style="background-color:#E3EAEB;">
            <td align="left">MOBILE: </td>
            <td align="left"><%= c_app.xmobile%></td>
            <td align="left"> MOBILE: </td>
            <td align="left"><%= mobile%></td>
        </tr>
        <tr>
            <td align="center" colspan="4"  
                style="background-color:#666; color:#ffffff;font-weight:bold;">
                <strong>--- PAYMENT DETAILS ---</strong></td>
        </tr>

        <tr>
            <td colspan="4" align="left">            
                <table style="width:100%;" id="mitems" class="tiger-stripe" >
                    <tr style="background-color:#1C5E55; color:#ffffff;">
                        <td>
                            S/N</td>
                        <td> ITEM CODE</td>
                        
                          <td>ITEM DESCRIPTION</td>
                        <td>
                            QTY</td>
                        <td style="text-align:center;"> APPLICATION FEE(NGN)</td>
                              <td style="text-align:center;"> TECH. FEE(NGN)</td>
                        <td style="text-align:center;">
                            TOTAL (NGN)</td>
                    </tr>
                    <% int i = 1; 
                        foreach (Ipong.Classes.XObjs.Fee_details f in lt_fdets)
                       { %>
                    <tr>
                        <td>
                            <%=i%></td>
                        <td>
                           <%=ret.getFee_listByID(f.fee_listID).item_code%></td>
                       
                         <td>
                           <%=ret.getFee_listByID(f.fee_listID).item%></td>
                        <td>
                           <%=f.xqty%></td>

                        <td align="right">
                         <% string new_init_amt = string.Format("{0:n}", Convert.ToInt32(f.init_amt)); %>
                           <%=new_init_amt%></td>

                            <td align="right">
                         <% string new_tech_amt = string.Format("{0:n}", Convert.ToInt32(f.tech_amt)); %>
                           <%=new_tech_amt%></td>

                        <td align="right">
                         <% string new_tot_amt1 = string.Format("{0:n}", Convert.ToDouble(f.tot_amt)); %>
                             <%=new_tot_amt1 %></td>
                    </tr>
                     <% i++; amt += Convert.ToInt32(f.tot_amt); Session["tot_amtx"] = amt;
                       } %>
                   
                    
                    <tr>
                        <td colspan="6" style="text-align:right;font-weight:bold;">
                            PayX Convenience Fee:&nbsp;</td>

                        <td align="right">
                            &nbsp;<%=Math.Round(Convert.ToDouble(isw_fields.isw_conv_fee),2) %></td>
                    </tr>
                                        
                    
                </table>
            </td>
        </tr>
          
            <tr >
            <td colspan="4" class="right-align">&nbsp;</td>
        </tr>
       
       <tr style="font-size:13px;text-decoration:underline; color:#1C5E55;font-weight:bold;" align="right">
            <td colspan="4" class="right-align">
            <% string new_tot_amtx = string.Format("{0:n}", (amt + Math.Round(Convert.ToDouble(isw_fields.isw_conv_fee), 2))); %>
               TOTAL AMOUNT:&nbsp; NGN&nbsp;<%=new_tot_amtx%></td>
        </tr>
            
         <tr>
                        <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" 
                            colspan="4">&nbsp;</td>
                    </tr>
                  
                    <tr>
                        <td class="center-align" colspan="4" align="center">
            POWERED BY<br/>
           <img src="../images/payxlogo.jpg"  alt="XPay" width="90px" height="40px" /> <br />
                Plot 4. Oluwakayode Jacobs Street Ikate,Lekki Phase 1<br />
                <a href="http://www.einaosolutions.com">www.einaosolutions.com</a><br />
                Support E-mail(s): <a href="mailto:paymentsupport@einaosolutions.com">paymentsupport@einaosolutions.com</a><br />
                Customer Contact Support Line(s): +2349038979681 </td>
                    </tr> 
           
          
    </table>
               </td>
        </tr>
        
        <tr style="width:100%;text-align:center;">
        <td colspan="5">
            
                <asp:Button ID="BtnBackToList" runat="server" class="button" 
                                Text="Back to Transactions List" onclick="BtnBackToList_Click" />            
                        &nbsp;<input type="button" name="Printform" id="Button1" value="Print" onclick="PrintPartner('ReportDetails'); return false" class="button" />
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
