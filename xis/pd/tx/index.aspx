<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Ipong.xis.pd.tx.index" %>

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
                WELCOME TO THE PAYMENT SECTION, PLEASE FOLLOW THE INSTRUCTIONS BELOW AND THEN CLICK ON PROCEED</td>
        </tr>
        <tr >
            <td ><strong>PLEASE FOLLOW THE INSTRUCTIONS BELOW:</strong><br /><br />
                (1) To &quot;Add&quot; an item, put in the quantity (QTY) next to the 
                item to purchase and the click on
                <img alt="Add" height="16" src="../../../images/checkmark.gif" width="16" /><br />
                (2) To &quot;Remove an item, click on
                <img alt="Remove" height="16" src="../../../images/x.gif" width="16" /><br />
                (3) After you have finished selecting items, click on the &quot;CONFIRM&quot; button below<br />
                (4) You can &quot;CHANGE ITEMS&quot; after viewing your basket<br />
                (5) Click on &quot;CHECK OUT&quot; to process your invoice and recieve you transaction 
                PIN.</td>
        </tr>
        <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="center-align">
            
                <asp:Button ID="btnProceed" runat="server" class="button" Text="Proceed" onclick="btnProceed_Click" />
                </td>
        </tr>
       <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;">
                &nbsp;</td>
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
