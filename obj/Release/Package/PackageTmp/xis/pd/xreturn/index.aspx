<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Ipong.xis.pd.xreturn.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FRESH PAYMENT SECTION</title>
    <link href="../../../css/style.css" rel="stylesheet" type="text/css" /> 
     <script src="../../../js/jquery.js" type="text/javascript"></script>
    <script src="../../../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

     <style type="text/css">
.item_alt {background-color:#E3EAEB; color:#000000;  }
.tiger-stripe{ font-size:12px;}
</style>
  <script type="text/javascript">
      $(function () {
          //  $("#tbl_loader").load("./payment_options.aspx");
      });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="container">
            <div class="sidebar">                 
               
            </div>
            <div class="content">
               
                <div id="searchform">
        
    <table class="center-align" width="100%" class="form">
     <tr class="center-align">
                <td colspan="4">
                    <img alt="Coat Of Arms" height="79" src="../../../images/coat_of_arms.png" 
                        width="85" />
               </td>
            </tr>
            <tr class="center-align" style=" font-size:11pt;" >
                <td colspan="4">
                    FEDERAL REPUBLIC OF NIGERIA<br />
                    FEDERAL MINISTRY OF INDUSTRY, TRADE AND INVESTMENT<br />
                    COMMERCIAL LAW DEPARTMENT<br />
                    TRADEMARKS, PATENTS AND DESIGNS REGISTRY
</td>
            </tr>
        <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;">
               PAYMENT CONFIRMATION SECTION</td>
        </tr>
        <% if (isr.ResponseCode == "00")
           { %>
        <tr class="center-align">
            <td  style="font-size:20px;"><strong>PAYMENT COMPLETED SUCCESSFULLY</strong><br />
               <div class="payment_success">
                 <div class="x_succ_img">
                An e-mail has been sent to "paymentsupport@einaosolutions.com":<br />
                Transaction Reference:&nbsp;<%=txnref%><br />
                Payment Reference:&nbsp;<%=payRef%><br /><br />
                Please check your "Payment Status" or "History Log" to view more details!!
                </div>
                </div>
            </td>
        </tr>
         <% } %>
          <% if ((isr.ResponseCode != "00")&&(isr.ResponseCode != "XXXX"))
             { %>
          <tr class="center-align">
            <td  style="font-size:20px;"><strong>PAYMENT NOT COMPLETED SUCCESSFULLY</strong><br />                
                <div class="payment_failure">
                 <div class="x_fail_img">                
                An e-mail has been sent to "paymentsupport@einaosolutions.com":<br />
                Reason:&nbsp;<%=isr.ResponseDescription%><br />
                Transaction Reference:&nbsp;<%=txnref%><br />
                Payment Reference:&nbsp;<%=payRef%><br /><br />
                Please check your "Payment Status" or "History Log" to view more details!!
                 </div>
                </div>
            </td>
        </tr>
         <% } %>
          <% if ((isr.ResponseCode != "00")&&(isr.ResponseCode == "XXXX"))
             { %>
          <tr class="center-align">
            <td  style="font-size:20px;"><strong>PAYMENT PENDING</strong><br />                
                <div class="payment_failure">
                 <div class="x_fail_img"> 
                Reason:&nbsp; <%=isr.ResponseDescription%><br />
                Please check your "Payment Status" or "History Log" to view more details!!
                 </div>
                </div>
            </td>
        </tr>
         <% } %>
        <tr>
            <td class="center-align">
                Click on <strong>&quot;Proceed&quot;</strong> to return your <strong>Dashboard</strong></td>
        </tr>
        <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;">            
                
                </td>
        </tr>

        <tr>
            <td class="center-align">            
                <asp:Button ID="btnProceed" runat="server" class="button" Text="Proceed" 
                    onclick="btnProceed_Click" />
                </td>
        </tr>
        <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            
               </td>
        </tr>
      <tr>
            <td class="center-align">
             <img alt="Einao Solutions" src="../../../images/einao_logo.png"   width="180px" height="39px"/><br />
                1.Adekola Balogun street, Off Yemi Olowude street, Lekki<br />
                <a href="http://www.einaosolutions.com|">www.einaosolutions.com</a><br />
                Support E-mail(s): <a href="mailto:paymentsupport@einaosolutions.com">paymentsupport@einaosolutions.com</a><br />
                Customer Contact Support Line(s): +234-8098367527, +234-11111   
            </td>
        </tr>
       
          </table>
                </div>
                


            </div>
        </div>
       
    </div>
    </form>
</body>
</html>
