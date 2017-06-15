<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment_details.aspx.cs" Inherits="Ipong.xis.pd.tx.payment_details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
 
</head>
<body>
    <form id="form1" runat="server"> 

    <div>
        <div class="container">
            <div class="sidebar">                 
               
            </div>
            <div class="content">
               
                <div id="searchform">
        
    <table class="center-align" width="100%">
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
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" colspan="2">
               PLEASE VERIFY YOUR DETAILS</td>
        </tr>
        
       <tr  >
                <td  style="width:50%;" align="right">
                    Name:&nbsp; </td>
                <td style="width:50%;" align="left">
                    &nbsp;<b><%=name%></b></td>
            </tr>
        
       <tr>
                <td  style="width:50%;" align="right">
                    Company: </td>
                <td style="width:50%;" align="left">
                    &nbsp;<b><%=coy_name%></b></td>
            </tr>
        
       <tr>
                <td  style="width:50%;" align="right">
                    Address: </td>
                <td style="width:50%;" align="left">
                    &nbsp;<b><%=addy%></b></td>
            </tr>
        
       <tr>
                <td  style="width:50%;" align="right">
                    Reference Number: </td>
                <td style="width:50%;" align="left">
                    &nbsp;<b><%=refno.ToUpper()%></b></td>
            </tr>
         <% string amtx = string.Format("{0:n}", Convert.ToDouble(Convert.ToInt32(amt) / 100)); %>
       <tr>
                <td  style="width:50%;" align="right">
                    Amount: </td>
                <td style="width:50%;" align="left">
                    &nbsp;<b><%=amtx%> NGN</b></td>
            </tr>
         <% string isw_conv_feex = string.Format("{0:n}", Convert.ToDouble(isw_conv_fee)); %>
       <tr  >
                <td  style="width:50%;" align="right">
                    Einao Convenience Fee: </td>
                <td style="width:50%;" align="left">
                    &nbsp;<b><%=isw_conv_feex%> NGN</b></td>
            </tr>
             <% string total_amtx = string.Format("{0:n}", Convert.ToDouble(total_amt) / 100); %>
              <tr  >
                <td  style="width:50%;" align="right">
                    Total Amount to be charged: </td>
                <td style="width:50%;" align="left">
                    &nbsp;<b><%=total_amtx %> NGN</b></td>
            </tr>
        
       <tr>
                <td  style="width:50%;" align="right">
                <asp:Button ID="btnBack" runat="server" class="button" Text="Back" 
                        onclick="btnBack_Click" />
               
                </td>
                <td style="width:50%;" align="left">
                
                <asp:Button ID="btnPay" runat="server" class="button" Text="Confirm Details" 
                        onclick="btnPay_Click" />
                         
                </td>
            </tr>
     <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" colspan="2" >
               </td>
        </tr>
       
     <tr>
            <td class="center-align" colspan="2">
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
