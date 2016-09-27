<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay_his2.aspx.cs" Inherits="Ipong.P.pay_his2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PAYMENT HISTORY</title>
     <link href="../../css/style.css" rel="stylesheet" type="text/css" /> 
     <link rel="stylesheet" href="../../css/jquery.ui.all.css" />
     <link rel="stylesheet" href="../../css/jquery.ui.theme.css" />
     <link rel="stylesheet" href="../../css/jquery.ui.tabs.css" />
     <link href="../../css/jquery.ui.timepicker.css" rel="stylesheet" type="text/css" />

    <script src="../../js/jquery.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../ui/jquery.ui.datepicker.js"></script>
    <script src="../../css/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../css/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../css/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../../css/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../../js/funk.js" type="text/javascript"></script>

    <script  type="text/javascript">
        function doPostResults(eu) {
            postwith('./pay_his.aspx', { eu: eu });
        }

	</script>
     <style type="text/css">
.item_alt {background-color:#E3EAEB; color:#000000;  }
.tiger-stripe{ font-size:12px;}
</style>
  <script type="text/javascript">
      $(function () {
          $("table.tiger-stripe tr:odd").addClass("item_alt");
      });
    </script>
<script type="text/javascript">
    $(function () {
        $("#toDate").datepicker({
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            dateFormat: 'yy-mm-dd'
        });
    });
    $(function () {
        $("#fromDate").datepicker({
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true
        });
    });

</script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="container">
            <div class="sidebar">                 
              <a href="./profile.aspx">PROFILE</a>                 
                <a href="../upd_pro.aspx">UPDATE PROFILE</a> 
                <a href="./pay_his.aspx">PAYMENT HISTORY</a>
                <a href="../lo.ashx">SIGN OUT</a>  
            </div>
            <div class="content">
                <div class="header">
                    <div class="xmenu">
                        <div class="menu">
                            <ul>
                                <li><a href="../lo.ashx">
                                    <img alt="" src="../images/logoff.png" width="16" height="16" />Log off</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="xlogo">
                        <div class="xlogo_l">
                        </div>
                        <div class="xlogo_r">
                        </div>
                    </div>
                </div>

                 <table class="center-align" width="100%" class="gridtable">
        <tr>
            <td align="right" style="width:25%;">
              <strong>GRAND TOTAL TRANSACTIONS:</strong></td>
            <td align="left" style="width:25%; font-weight:bold; color:Green;">
                &nbsp;
                <%=grand_tot_cnt%></td>
            <td align="right" style="width:25%;">
             <strong>GRAND TOTAL AMOUNT:</strong></td>
            <td align="left" style="width:25%;font-weight:bold; color:Green;">
                &nbsp;
                <%=new_grand_tot_amt%> NGN</td>
        </tr>
         <tr>
            <td class="tbbg" colspan="4">&nbsp;</td>
        </tr>
         <tr>
            <td class="center-align" colspan="4" >
             FROM:
                <asp:TextBox ID="fromDate" runat="server"></asp:TextBox>
&nbsp;TO:
                <asp:TextBox ID="toDate" runat="server"></asp:TextBox>
             &nbsp;<asp:Button ID="btnSearch" runat="server" class="button" Text="SEARCH" 
                    onclick="btnSearch_Click"   />
                    </td>
        </tr>
         <tr>
            <td class="center-align" colspan="4" >
                <strong><%=search_msg %></strong>  
                </td>
        </tr>
         <tr>
            <td class="center-align" colspan="4" >
                <asp:HiddenField ID="xadminID" runat="server" />
                 <asp:HiddenField ID="xtoDate" runat="server" />
                 <asp:HiddenField ID="xfromDate" runat="server" />
                 </td>
        </tr>
        </table>
        <% if (show_inv == 1)
           {
               if (tm_cnt == 0)
               { %>   
    <tr>
            <td class="center-align">
                <asp:GridView ID="gvTm" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flTm" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvTm_RowCommand" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" CaptionAlign="Left" 
                    HorizontalAlign="Left">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>   
                        <asp:TemplateField HeaderText="S/N">
                <ItemTemplate>
                     <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
             </asp:TemplateField>
                        <asp:BoundField DataField="ref_no" HeaderText="TRANSACTION REF"  >
                             <HeaderStyle HorizontalAlign="Left" Width="170px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="xreg_date" HeaderText="TRANSACTION DATE" >
                            <HeaderStyle HorizontalAlign="Left" Width="120px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="AMT(NGN)">
                            <ItemTemplate>
                            <asp:Label ID="lbl_cur_amt" runat="server"></asp:Label>
                            </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="AGENT">
                            <ItemTemplate>
                            <asp:Label ID="lbl_cur_agent" runat="server"></asp:Label>
                            </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="200px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="DETAILS">
                             <ItemTemplate>
                              <asp:ImageButton ID="lbDetTm" ImageUrl="../images/search.gif" runat="server" Height="16px" CommandName="TmDetailsClick"  CommandArgument='<%#Eval("transID") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Right" Width="20px"/>
                             <ItemStyle HorizontalAlign="Right" />
                             </asp:TemplateField>
                      
                        
                        
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"/>
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB"/>
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                 
               
                <asp:SqlDataSource ID="flTm" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="select 
twallet.xid,twallet.transID,twallet.xmemberID,twallet.xmembertype,twallet.xgt,twallet.ref_no,twallet.xbankerID,
fee_details.fee_listID,fee_details.init_amt,fee_details.tech_amt,fee_details.tot_amt,fee_details.xqty,
twallet.xreg_date
 from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID
 WHERE twallet.xpay_status='1' AND twallet.xreg_date BETWEEN @get_xfromDate AND @get_xtoDate">
                    <SelectParameters>
                       <asp:ControlParameter ControlID="xtoDate" Name="get_xtoDate" PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="xfromDate" Name="get_xfromDate" PropertyName="Value" Type="String" />
                   </SelectParameters>
                </asp:SqlDataSource>               
            </td>
        </tr>
          <% }
               else
               {%> 
            <table class="center-align" width="100%" class="gridtable">
       
        <tr>
            <td align="left" class="tbbg">&nbsp;</td>
        </tr>
        <tr>
        <td class="center-align" style="background-color:#000000; color:#ffffff;  font-weight:bold;">
               <strong> NO RECORDS FOUND FOR THE SEARCH QUERY!!</strong></td>
        </tr>
        <tr>
        <td align="left" class="tbbg">&nbsp;</td>
        </tr>
        <tr>
        <td class="center-align">
                           <asp:Button ID="btnRefresh" runat="server" class="button" Text="BACK" 
                               onclick="btnRefresh_Click"/>
                            </td>
        </tr>
        </table>   
 <% }
           }%>

        <% if (show_details_grid > 0)
             { %>   
    <table class="center-align" width="100%" class="gridtable">
        <tr>
            <td align="left" class="tbbg">
                DETAILS FOR TRANSACTION: "<%=transID%>"</td>
        </tr>
        <tr >
            <td  class="center-align">               
               <strong>TOTAL ITEM(S): <%=lt_fdets.Count%></strong>
            </td>
        </tr>
        
        <tr>
            <td class="center-align">
                <table id="mitems" class="tiger-stripe" style="width:100%;">
                    <tr style="background-color:#1C5E55; color:#ffffff;">
                        <td>
                            <strong>S/N</strong></td>
                        <td>
                            <strong>ITEM CODE</strong></td>
                        <td>
                            <strong>QTY</strong></td>
                        <td>
                            <strong>AMOUNT</strong></td>
                        <td>
                            <strong>TOTAL (<em>NGN</em> )</strong></td>
                    </tr>
                    <% int i = 1;
                       foreach (Ipong.Classes.XObjs.Fee_details f in lt_fdets)
                       { %>
                    <tr>
                        <td>
                            <%=i%>
                        </td>
                        <td>
                           <%= ret.getFee_listByID(f.fee_listID).item_code%>
                        </td>
                        <td>
                           <%=f.xqty%>
                        </td>
                        <td>
                        <% string new_amt = "";
                           if (c_pr.p_type == "merchant")
                           {
                               new_amt = string.Format("{0:n}", (Convert.ToInt32(f.init_amt)));
                           }
                           else if (c_pr.p_type == "wingman")
                           {
                               new_amt = string.Format("{0:n}", (Convert.ToDouble(f.tech_amt) * (Convert.ToDouble(c_pr.xratio) * 0.01)));
                           }
                                 
                                  %>
                           <%=new_amt%>
                        </td>
                        <td>
                        <% string new_qty = "";
                           if (c_pr.p_type == "merchant")
                           {
                               new_qty = string.Format("{0:n}", (Convert.ToInt32(f.init_amt) * Convert.ToInt32(f.xqty)));
                           }
                           else if (c_pr.p_type == "wingman")
                           {
                               new_qty = string.Format("{0:n}", (Convert.ToDouble(f.tech_amt) * Convert.ToDouble(f.xqty) * (Convert.ToDouble(c_pr.xratio) * 0.01))); 
                           }
                            %>
                             <%=new_qty%>
                        </td>
                    </tr>
                     <% i++;
                        if (c_pr.p_type == "merchant")
                        {
                            tot_amtx += Convert.ToInt32(Convert.ToInt32(f.init_amt) * Convert.ToInt32(f.xqty));
                        }
                        else if (c_pr.p_type == "wingman")
                        {
                            tot_amtx += Convert.ToInt32(Convert.ToDouble(f.tech_amt) * Convert.ToDouble(f.xqty) * (Convert.ToDouble(c_pr.xratio) * 0.01));
                        }
                       } %>
                   
                    <tr style="font-size:13px;text-decoration:underline; color:#1C5E55;">
                        <td align="right" colspan="5">
                            <strong><em>TOTAL AMOUNT:</em></strong>&nbsp;<strong><%=string.Format("{0:n}", Convert.ToDouble(tot_amtx))%> NGN</strong>
                            </td>
                    </tr>
                      <tr >
                        <td class="center-align" colspan="5">
                           <asp:Button ID="btnBack" runat="server" class="button" Text="BACK" 
                                onclick="btnBack_Click"   />
                            </td>
                    </tr>
                </table>
            </td>
        </tr>  
        
          </table>     
 <% } %> 
            </div>
        </div>
    </div>
    </form>
</body>
</html>
