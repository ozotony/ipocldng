<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment_options.aspx.cs" Inherits="Ipong.xis.pd.tx.payment_options" %>

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
        .style1
        {
            width: 50px;
            height: 30px;
        }
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
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;">
              INTER SWITCH PAYMENT GUIDE</td>
        </tr>
        <tr>
            <td align="left">
               <span class="notice_header"> Step One:</span><br />
                <span class="notice_text">You should have either an Interswitch powered Debit card or Verve card</span><br />
                <br />
                <span class="notice_header">Step Two:</span><br />
                <span class="notice_text">Please ensure that the card provided is still valid (Check the expiry date!)</span><br />
                <br />
                <span class="notice_header">Step Three:</span><br />
                <span class="notice_text">Please ensure that you have the required amount (As shown on the details page!) 
                on the selected card type to pay for the required items</span><br />
                <br />
                <span class="notice_header">Step Four:</span><br />
                <span class="notice_text">On the payment gateway, select the card type and provide the card details,PIN 
                and any other required information</span><br />
                <br />
                <span class="notice_header">Step Five:</span><br />
                <span class="notice_text">Always cross-check the details before you proceed to the payment gateway</span><br />
                <br />
                <span class="notice_footer">Thank you for adhering to the above instructions!!</span>
                
                </td>
        </tr>
          <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;">
              BANK DEPOSIT PAYMENT GUIDE</td>
        </tr>
         
          <tr>
            <td align="left">
                <span class="notice_header">Step One:</span><br />
                <span class="notice_text">Make sure you have either recieved an alert (SMS/E-mail) or have printed a copy 
                of your invoice</span><br />
                <br />
                <span class="notice_header">Step Two:</span><br />
                <span class="notice_text">Kindly go to any of the designated banks for the service to make payment</span><br />
                <br />
                <span class="notice_header">Step Three:</span><br />
               <span class="notice_text"> Always cross-check the details before you proceed to generate the payment 
                invoice</span><br />
                <br />
                <span class="notice_footer">Thank you for adhering to the above instructions!!</span>
                
                </td>
        </tr>
        
        <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;">
              PLEASE SELECT AN OPTION BELOW</td>
        </tr>

       <tr class="center-align" >
                <td >
                    <asp:RadioButtonList ID="rblOptions" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"  CssClass="textbox">
                        <asp:ListItem Value="isw">
                        <img alt="interswitch"  width="92px" height="30px" src="../../../images/isw_logo_small.gif" />
                        <img alt="mastercard" class="style1" src="../../../images/mastercard-logo.png" />
                        <img alt="verve" class="style1" src="../../../images/verve.png" />
                        </asp:ListItem>
                        <asp:ListItem Value="bank">Bank Deposit&nbsp;<img alt="recharge_pin" class="style1" src="../../../images/recharge_pin.png" /></asp:ListItem>
                    </asp:RadioButtonList>
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
         <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;">
               </td>
        </tr>
         <tr>
            <td class="center-align">
             <asp:Button ID="btnBack" runat="server" class="button" Text="Back" 
                        onclick="btnBack_Click" />
               
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
