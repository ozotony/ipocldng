<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay_his.aspx.cs" Inherits="Ipong.P.pay_his" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PAYMENT HISTORY</title>
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
    <script src="../js/funk.js" type="text/javascript"></script>

 
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
                 <a href="./pay_stats.aspx">PAYMENT CHARTS</a>
                <a href="../lo.ashx">SIGN OUT</a>  
            </div>
            <div class="content">
                

                 <table class="center-align" width="100%" class="form" >
                  <tr class="center-align">
                <td colspan="4">
                    <img alt="Coat of Arms" height="84" src="../images/LOGOCLD.png" width="509" />
               </td>
            </tr>
        <tr>
            <td align="right" style="width:25%;">
              <strong>GRAND TOTAL ITEM(S):</strong></td>
            <td align="left" style="width:25%; font-weight:bold; color:Green;">
                &nbsp;
                <%=Session["grand_tot_cnt"].ToString()%></td>
            <td align="right" style="width:25%;">
             <strong>GRAND TOTAL AMOUNT:</strong></td>
            <td align="left" style="width:25%;font-weight:bold; color:Green;">
                &nbsp;
                <%=Session["new_grand_tot_amt"].ToString()%> NGN</td>
        </tr>
         <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" colspan="4">&nbsp;</td>
        </tr>
         <tr>
            <td class="center-align" colspan="4" >
             FROM:
                <asp:TextBox ID="fromDate" runat="server" CssClass="textbox"></asp:TextBox>
&nbsp;TO:
                <asp:TextBox ID="toDate" runat="server" CssClass="textbox"></asp:TextBox>
             &nbsp;<asp:Button ID="btnSearch" runat="server" class="button" Text="SEARCH" 
                    onclick="btnSearch_Click"   />
                    </td>
        </tr>
         <tr>
            <td class="center-align" colspan="4" >
                <strong><%=search_msg %></strong>  
                </td>
        </tr>
        
        </table>
        <% if (show_inv == 1)
           {
               if (tm_cnt == 0)
               { %>   
               <table  width="100%" >
    <tr id="GridView">
            <td class="center-align">
                <asp:GridView ID="gvTm" runat="server" AutoGenerateColumns="False" 
                     EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvTm_RowCommand" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" CaptionAlign="Left" 
                    HorizontalAlign="Left" AllowPaging="True" 
                    onpageindexchanging="gvTm_PageIndexChanging" PageSize="50" >
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

                         <asp:BoundField DataField="tot_amt" HeaderText="AMT(NGN)" >
                            <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="xmemberID" HeaderText="AGENT" >
                            <HeaderStyle HorizontalAlign="Left" Width="200px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

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
                             
            </td>
        </tr>
        <tr>
        <td class="center-align">
        <input type="button" name="Printform" id="Printform" value="Print" onclick="PrintPartner('GridView'); return false" class="button" />
        </td>
        </tr>
</table>
          <% }
               else
               {%> 
            <table class="center-align" width="100%" class="gridtable form">
       
        <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;">&nbsp;</td>
        </tr>
        <tr>
        <td class="center-align" style="background-color:#000000; color:#ffffff;  font-weight:bold;">
               <strong> NO RECORDS FOUND FOR THE SEARCH QUERY!!</strong></td>
        </tr>
        <tr>
        <td class="center-align" style="background-color:#1C5E55; color:#ffffff;">&nbsp;</td>
        </tr>
        
        </table>   
 <% }
           }%>

        <% if (show_details_grid > 0)
             { %>  
             <table width="100%">
             <tr>
             <td>
<table id="MerchantDetails" class="center-align" width="100%" class="gridtable form">
        <tr  >
            <td >
              <div style="background-color:#1C5E55; color:#ffffff;text-align:center;">  DETAILS FOR TRANSACTION: "<%=Session["transID"].ToString()%>"</div>
                </td>
        </tr>
        <tr >
            <td  >               
              <div style="color:#000;text-align:center;" > 
               <strong>TOTAL ITEM(S):</strong> <%=lt_fdets.Count%><br />
              DATE: <%=Session["transDate"].ToString()%>
              </div>
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
                               new_amt = string.Format("{0:n}", (Convert.ToInt32(f.init_amt)));                                 
                                  %>
                           <%=new_amt%>
                        </td>
                        <td>
                        <% string new_qty = "";
                               new_qty = string.Format("{0:n}", (Convert.ToInt32(f.init_amt) * Convert.ToInt32(f.xqty)));
                            %>
                             <%=new_qty%>
                        </td>
                    </tr>
                     <% i++;
                            tot_amtx += Convert.ToInt32(Convert.ToInt32(f.init_amt) * Convert.ToInt32(f.xqty));
                       } %>
                   
                    <tr style="font-size:13px;text-decoration:underline; color:#1C5E55;">
                        <td align="right" colspan="5">
                            <strong><em>TOTAL AMOUNT:</em></strong>&nbsp;<strong><%=string.Format("{0:n}", Convert.ToDouble(tot_amtx))%> NGN</strong>
                            </td>
                    </tr>
                      <tr >
                        <td class="center-align" colspan="5">
                            &nbsp;</td>
                    </tr>
                      <tr >
                        <td align="left" colspan="5">
                            <strong>Payment Type:</strong> <%=Session["payment_type"].ToString() %>
                            </td>
                    </tr>
                    <%if ((Session["payment_type"] != null) && (Session["payment_type"].ToString() == "Bank"))
                      {%>
                      <tr >
                        <td align="left" colspan="5">
                        <br />
                        <strong>Bank:</strong> <%=Session["bank_bankname"].ToString()%><br />
                        <strong>Bank Officer:</strong> <%=Session["bank_xname"].ToString()%><br />
                        <strong>Position:</strong> <%=Session["bank_xposition"].ToString()%><br />
                        <strong>Telephone:</strong> <%=Session["bank_telephone"].ToString()%><br />
                        <strong>Email:</strong> <%=Session["bank_email"].ToString()%><br />                          
                          
                          </td>
                    </tr>
                     <%}%>
                     
                </table>
            </td>
        </tr>  
        
          </table>
             </td>
             </tr>

             <tr>
			<td class="center-align">
            <asp:Button ID="btnBack" runat="server" class="button" Text="Back" onclick="btnBack_Click"   />
			<input type="button" name="Printform" id="PrintMerchantDetails" value="Print" onclick="PrintPartner('MerchantDetails'); return false" class="button" />
			</td>
            </tr>
             </table> 
         
 <% } %> 
  <% if (show_details_grid_wingman > 0)
             { %> 
             <table width="100%">
             <tr>
             <td>
<table id="WingmanDetails" class="center-align" width="100%" class="gridtable_wingman form">
       
         <tr  >
            <td >
              <div style="background-color:#1C5E55; color:#ffffff;text-align:center;">  DETAILS FOR TRANSACTION: "<%=Session["transID"].ToString()%>"</div>
                </td>
        </tr>
        <tr >
            <td  >               
              <div style="color:#000;text-align:center;" > 
               <strong>TOTAL ITEM(S):</strong> <%=lt_fdets.Count%><br />
              DATE: <%=Session["transDate"].ToString()%>
              </div>
            </td>
        </tr>
        <tr>
            <td class="center-align">
                <table id="Table1" class="tiger-stripe" style="width:100%;">
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
                            <strong>TECH FEE</strong></td>
                        <td>
                            <strong>GRAND TOTAL (<em>NGN</em> )</strong></td>
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
                        <% string new_init = "";
                           new_init = string.Format("{0:n}", (Convert.ToDouble(f.init_amt))); 
                            %>
                             <%=new_init%>
                        </td>

                        <td>
                        <% string new_tech = "";
                           new_tech = string.Format("{0:n}", (Convert.ToDouble(f.tech_amt))); 
                            %>
                             <%=new_tech%>
                        </td>

                        <td>
                       
                              <% string new_amt = "";                          
                               new_amt = string.Format("{0:n}", (Convert.ToDouble(f.tot_amt)));
                                 
                                  %>
                           <%=new_amt%>
                        </td>


                    </tr>
                     <% i++;                      
                            tot_amtx += Convert.ToInt32(Convert.ToDouble(f.tot_amt) );
                       } %>
                   
                    <tr style="font-size:13px;text-decoration:underline; color:#1C5E55;">
                        <td align="right" colspan="6">
                            <strong><em>TOTAL AMOUNT:</em></strong>&nbsp;<strong><%=string.Format("{0:n}", Convert.ToDouble(tot_amtx))%> NGN</strong>
                            </td>
                    </tr>
                      <tr >
                        <td class="center-align" colspan="6">
                            &nbsp;</td>
                    </tr>
                      <tr >
                        <td align="left" colspan="6">
                            <strong>Payment Type:</strong> <%=Session["payment_type"].ToString() %>
                            </td>
                    </tr>
                    <%if ((Session["payment_type"] != null) && (Session["payment_type"].ToString() == "Bank"))
                      {%>
                      <tr >
                        <td align="left" colspan="6">
                        <br />
                        <strong>Bank:</strong> <%=Session["bank_bankname"].ToString()%><br />
                        <strong>Bank Officer:</strong> <%=Session["bank_xname"].ToString()%><br />
                        <strong>Position:</strong> <%=Session["bank_xposition"].ToString()%><br />
                        <strong>Telephone:</strong> <%=Session["bank_telephone"].ToString()%><br />
                        <strong>Email:</strong> <%=Session["bank_email"].ToString()%><br />                          
                          
                          </td>
                    </tr>
                     <%}%>
                     
                </table>
            </td>
        </tr>  
        
          </table>
             </td>
             </tr>

            <tr>
			<td class="center-align">
             <asp:Button ID="Button1" runat="server" class="button" Text="Back" onclick="btnBack_Click"   />
			<input type="button" name="Printform" id="PrintWingmanDetails" value="Print" onclick="PrintPartner('WingmanDetails'); return false" class="button" />
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
