<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bank_reg_succ.aspx.cs" Inherits="Ipong.bank_reg_succ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CLIENT REGISTRATION</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
<script src="js/funk.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div class="container">
        <div class="sidebar">
            </div>
        <div class="content">
          
    <table  id="registration_form" class="center-align" width="100%"  class="form" >           
     <tr class="center-align">
                <td colspan="4">
                    <img alt="Coat Of Arms" height="79" src="./images/coat_of_arms.png" 
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
                BANKER REGISTRATION SUCCESSFULL!! 
            </td>
        </tr>
        
        <tr>
            <td width="20%" class="center-align" style="font-size:14px;">
                <img alt="success" height="50" src="images/check.png" width="50" /><br />
                BANKER : <strong>" <%= x.ToUpper() %>  "</strong> WITH REGISTRATION NUMBER : <strong>"<%= m.ToUpper() %> "</strong> HAS BEEN REGISTERED SUCCESSFULLY!! <br /> &nbsp;PLEASE CHECK YOUR E-MAIL FOR FURTHER DETAILS</td>
        </tr>
       
        <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;"></td>
        </tr>
        
        <tr>
            <td class="center-align">
            
                <asp:Button ID="btnSignIn" runat="server" class="button" Text="Sign In" 
                    onclick="btnSignIn_Click" />
            </td>
        </tr>
         <tr>
            <td class="center-align">
            <b style="font-family:Cambria;font-size:13px;">POWERED BY EINAO SOLUTIONS</b>
            </td>
        </tr>
    </table> 
       

        </div>
    </div>
    </div>
    </form>
</body>
</html>
