<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="v_bask_ds.aspx.cs" Inherits="Ipong.A.v_bask_ds"  MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>WELCOME TO IPO NIGERIA OFFICIAL WEB PORTAL</title>
    <link href="../css/ain_structure.css" rel="stylesheet" type="text/css" />

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
                PAID ITEMS SECTION</td>
        </tr>
        <tr >
            <td  class="center-align">
                <asp:HiddenField ID="xadminID" runat="server" />
                <asp:Label ID="lblNotice" runat="server" Text=""></asp:Label>
                <br />
            </td>
        </tr>
       
           <% if (tm_cnt > 0)
              { %>
               <% if (unused_cnt > 0)
                  { %>
        <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align">
                --- DESIGNS ["Un-used Items"] ---</td>
        </tr>
        <tr class="center-align" style="background-color:#efefef;">
            <td class="center-align">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="center-align">
                <asp:GridView ID="gvTm" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flTmUnused" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvTm_RowCommand" 
                    CellPadding="4" ForeColor="#333333" CaptionAlign="Left" 
                    HorizontalAlign="Left"  AllowSorting="True" 
                    AllowPaging="True" PageSize="50" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>   
                        <asp:TemplateField HeaderText="S/N">
                <ItemTemplate>
                     <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
             </asp:TemplateField>
                        <asp:BoundField DataField="item_code" HeaderText="CODE" 
                            SortExpression="item_code" >
                             <HeaderStyle HorizontalAlign="Left" Width="70px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="PRODUCT TITLE">
                            <ItemTemplate>                           
                            <asp:Label ID="Label1" runat="server">Product Title:</asp:Label><br />
                                <asp:TextBox ID="txt_product_title_tm" runat="server"  Width="600px"></asp:TextBox><br />
                            </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="new_transID" HeaderText="TRANSACTION ID" 
                            SortExpression="new_transID" >
                             <HeaderStyle HorizontalAlign="Left" Width="140px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="init_amt" HeaderText="AMT" ReadOnly="True" 
                            SortExpression="init_amt" >
                             <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                           <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                           <asp:BoundField DataField="used_status" HeaderText="STATUS" SortExpression="used_status">
                            <HeaderStyle HorizontalAlign="Left" Width="60px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:ImageButton ID="btnMakePaymentTm" ImageUrl="../images/r_arrow.gif" runat="server" Height="16px" CommandName="TmStatusClick"  CommandArgument='<%#Eval("xid") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Center" Width="30px"/>
                             <ItemStyle HorizontalAlign="Center" />
                             </asp:TemplateField>
                        
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"/>
                    <PagerSettings FirstPageImageUrl="~/images/begin.gif" 
                        LastPageImageUrl="~/images/end.gif" NextPageImageUrl="~/images/ffwd.gif" 
                        PageButtonCount="50" PreviousPageImageUrl="~/images/frev.gif" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB"/>
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                 
                 <asp:SqlDataSource ID="flTmUnused" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="select hwallet.*,hwallet.transID+'-'+CAST(hwallet.xid AS NVARCHAR) AS new_transID,fee_list.item_code,fee_list.xdesc,fee_details.init_amt from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid INNER JOIN twallet ON  twallet.xid=fee_details.twalletID INNER JOIN hwallet ON hwallet.fee_detailsID=fee_details.xid where fee_list.xcategory='ds' AND twallet.xmemberID=@getAdminID AND twallet.xpay_status='1' AND hwallet.used_status='Not used' ">
                    <SelectParameters>
                       <asp:ControlParameter ControlID="xadminID" Name="getAdminID" PropertyName="Value" Type="String" />
                   </SelectParameters>
                </asp:SqlDataSource>                  
                                 
            </td>
        </tr>     

        
         <tr>
            <td class="center-align">
                &nbsp;</td>
        </tr>
         <% 
                  }
                  if (used_cnt > 0)
                  { %>
        
         <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align">
                --- DESIGNS ["Used Items"] ---</td>
        </tr>

        
         <tr>
            <td class="center-align" style="background-color:#efefef;">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="center-align">
                <asp:GridView ID="gvTmUsed" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flTmUsed" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" 
                    CellPadding="4" ForeColor="#333333" CaptionAlign="Left" 
                    HorizontalAlign="Left" AllowSorting="True" 
                    AllowPaging="True" onrowcommand="gvTmUsed_RowCommand" 
                    onload="gvTmUsed_Load" PageSize="50">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>   
                        <asp:TemplateField HeaderText="S/N">
                <ItemTemplate>
                     <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
             </asp:TemplateField>
                        <asp:BoundField DataField="item_code" HeaderText="CODE" 
                            SortExpression="item_code" >
                             <HeaderStyle HorizontalAlign="Left" Width="70px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="PRODUCT TITLE">
                            <ItemTemplate>
                            <asp:Label ID="txt_product_title_tm" runat="server"  Width="470px" Text='<%#Eval("product_title") %>'></asp:Label>
                            </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="new_transID" HeaderText="TRANSACTION ID" 
                            SortExpression="new_transID" >
                             <HeaderStyle HorizontalAlign="Left" Width="140px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="init_amt" HeaderText="AMT" ReadOnly="True" 
                            SortExpression="init_amt" >
                             <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                           <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                          <asp:TemplateField HeaderText="CURRENT OFFICE">
                            <ItemTemplate>
                            <asp:Label ID="lbl_cur_office" runat="server"  ></asp:Label>
                            </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="130px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="STATUS">
                            <ItemTemplate>
                            <asp:Label ID="lbl_cur_status" runat="server"></asp:Label>
                            </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="110px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                                                
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"/>
                    <PagerSettings FirstPageImageUrl="~/images/begin.gif" 
                        LastPageImageUrl="~/images/end.gif" NextPageImageUrl="~/images/ffwd.gif" 
                        PageButtonCount="50" PreviousPageImageUrl="~/images/frev.gif" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB"/>
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>                
                  <asp:SqlDataSource ID="flTmUsed" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="select hwallet.*,hwallet.transID+'-'+CAST(hwallet.xid AS NVARCHAR) AS new_transID,fee_list.item_code,fee_list.xdesc,fee_details.init_amt from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid INNER JOIN twallet ON  twallet.xid=fee_details.twalletID INNER JOIN hwallet ON hwallet.fee_detailsID=fee_details.xid where fee_list.xcategory='ds' AND twallet.xmemberID=@ugetAdminID AND twallet.xpay_status='1' AND hwallet.used_status='Used' ">
                    <SelectParameters>
                       <asp:ControlParameter ControlID="xadminID" Name="ugetAdminID" PropertyName="Value" Type="String" />
                   </SelectParameters>
                </asp:SqlDataSource>                
            </td>
        </tr>     
           <% } %> 
        <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;"></td>
        </tr>
         <% }%>
        <tr>
            <td class="center-align">
                <input id="BtnBack" type="button" value="Back" class="button" onclick="doPrev('v_bask_ds_pro.aspx');return false;"/><br />
            </td>
        </tr>
        
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
