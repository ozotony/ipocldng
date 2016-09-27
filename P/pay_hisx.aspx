<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay_hisx.aspx.cs" Inherits="Ipong.P.pay_hisx" %>

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
            showButtonPanel: true
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
                <%=nume%></td>
            <td align="right" style="width:25%;">
             <strong>GRAND TOTAL AMOUNT:</strong></td>
            <td align="left" style="width:25%;font-weight:bold; color:Green;">
                &nbsp;
                <%=grand_tot%> NGN</td>
        </tr>
        </table>
        <% if (show_inv == 0)
           {
               if (tm_cnt > 0)
               { %>   
    <table class="center-align" width="100%" class="tiger-stripe">
        <tr>
            <td align="left" class="tbbg" colspan="8">
                PAYMENT ITEM SECTION</td>
        </tr> 
         <tr>
            <td class="center-align" colspan="8" style="background-color:#ffffff;">
                FROM:
                <asp:TextBox ID="fromDate" runat="server"></asp:TextBox>
&nbsp;TO:
                <asp:TextBox ID="toDate" runat="server"></asp:TextBox>
             &nbsp;<asp:Button ID="btnSearch" runat="server" class="button" Text="SEARCH" 
                    onclick="btnSearch_Click"   />
             </td>
        </tr>
          
        <tr >
            <td colspan="8" class="center-align" 
                style="background-color:#000000; color:#ffffff;  font-weight:bold;">Pages <% Response.Write(eu + 1);%> of <%if (pages.Count < 1) { Response.Write("1"); } else { Response.Write(pages.Count); }%> . Total records = <%=nume%> </td>
            </tr>
           
          <tr >
            <td colspan="8" class="center-align" style="background-color:#ffffff;"><strong><% foreach (string s in pages) { Response.Write(s + " "); }%></strong></td>
            </tr>
             <tr >
            <td colspan="8" class="center-align" class="tbbg">&nbsp;</td>
            </tr>
         <tr>
            <td class="center-align" colspan="8" style="background-color:#ffffff;">
                &nbsp;</td>
        </tr>  

        <tr >
            <td  align="left" style="background-color:#1C5E55; color:#ffffff; width:50px; font-weight:bold;">              
                &nbsp;&nbsp;S/N
            </td>

            <td  align="left" style="background-color:#1C5E55; color:#ffffff;width:150px;font-weight:bold;">              
                &nbsp;&nbsp;BANK</td>

            <td  align="left" style="background-color:#1C5E55; color:#ffffff;width:150px;font-weight:bold;">              
                BANK STAFF</td>

            <td  align="left" style="background-color:#1C5E55; color:#ffffff;width:200px;font-weight:bold;">              
                &nbsp;&nbsp;COMPANY</td>

            <td  align="left" style="background-color:#1C5E55; color:#ffffff;width:250px;font-weight:bold;">              
                &nbsp;&nbsp;CLIENT</td>

            <td  align="left" style="background-color:#1C5E55; color:#ffffff;width:150px;font-weight:bold;">              
                &nbsp;&nbsp;TOTAL AMOUNT (NGN)</td>

            <td  align="left" style="background-color:#1C5E55; color:#ffffff;width:100px;font-weight:bold;">              
                DATE</td>

            <td  align="left" style="background-color:#1C5E55; color:#ffffff;width:100px;font-weight:bold;">              
                &nbsp;&nbsp;DETAILS</td>
        </tr>  
        <% int sn = 1; foreach (Ipong.Classes.XObjs.Twallet t in lt_twall)
           { %>
        <tr >
            <td  align="left" >
                &nbsp;&nbsp;<%=sn%></td>

            <td  align="left" >              
                &nbsp;&nbsp;<%=getBname(t.xbankerID)%></td>

            <td  align="left" ><%= getBankername(t.xbankerID)%></td>

            <td  align="left" >              
                &nbsp;&nbsp;<%if (ret.getTwalletMembertypeByPwalletID(t.xmemberID) == "ra") { Response.Write(getCname("ra", t.xmemberID)); } else if (ret.getTwalletMembertypeByPwalletID(t.xmemberID) == "rc") {Response.Write(getCname("rc", t.xmemberID)); }%></td>

            <td  align="left" >              
                &nbsp;&nbsp;<%if (ret.getTwalletMembertypeByPwalletID(t.xmemberID) == "ra") { Response.Write(getXname("ra", t.xmemberID)); } else if (ret.getTwalletMembertypeByPwalletID(t.xmemberID) == "rc") { Response.Write(getXname("rc", t.xmemberID)); }%></td>

            <td  align="left" >              
                &nbsp;&nbsp;<strong> <%=doCalcInitTotAmt(Convert.ToInt64(t.xid))%></strong></td>

            <td  align="left" >              
                &nbsp;&nbsp;<%=t.xreg_date%></td>

            <td  align="left">              
                &nbsp;&nbsp;<a href="pay_his.aspx?0093283223=<%=t.xid %>&0093283F56=<%=fromDate.Text %>&0093283T5999=<%=toDate.Text  %>"><img src="../images/r_arrow.gif" alt="View Details" height="16px" width="16px"  /></a> </td>

        </tr>  
        <% sn++;
           } %>
          <tr >
            <td colspan="8" class="center-align" class="tbbg">&nbsp;</td>
            </tr>
             <tr >
            <td colspan="8" class="center-align" style="background-color:#ffffff;"><strong><% foreach (string s in pages) { Response.Write(s + " "); }%></strong></td>
            </tr>
             <tr >
            <td colspan="8" class="center-align" 
                     style="background-color:#000000; color:#ffffff;font-weight:bold;">Pages <% Response.Write(eu + 1);%> of <%if (pages.Count < 1) { Response.Write("1"); } else { Response.Write(pages.Count); }%> . Total records = <%=nume%> </td>
            </tr>     
        
          </table>
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

        <% if (show_inv > 0)
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
