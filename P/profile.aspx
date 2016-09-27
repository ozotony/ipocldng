<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="Ipong.P.profile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MERCHANT PROFILE SECTION</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" /> 

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="container">
            <div class="sidebar">                   
                <a href="../upd_pro.aspx">UPDATE PROFILE</a> 
                <a href="./pay_his.aspx">PAYMENT HISTORY</a> 
                <a href="./pay_his.aspx">VIEW STATISTICS</a> 
                <a href="../lo.ashx">SIGN OUT</a>  
            </div>
            <div class="content">
                
                <div id="searchform">
        
    <table class="center-align" width="100%">
     <tr class="center-align">
                <td colspan="3">
                   <img alt="Coat of Arms" height="84" src="../images/LOGOCLD.png" width="509" />
               </td>
            </tr>
        <tr>
            <td colspan="3" class="center-align" style="background-color:#1C5E55; color:#ffffff;">
                WELCOME TO THE MERCHANT ADMINSTRATION UNIT</td>
        </tr>
       
        
                
        <tr>
            <td class="center-align" colspan="3">&nbsp;</td>
        </tr>
        
        <tr>
            <td style="width: 30%;" class="center-align">
                <a href="./pay_stats.aspx">
                    <img alt="" src="../images/REPORTS.png" style="width: 100px; height: 100px" /></a></td>
            <td style="width: 30%;" class="center-align">
                <a href="../upd_pro.aspx">
                    <img alt="" src="../images/PROFILE.png" style="width: 100px; height: 100px" /></a></td>
            <td style="width: 30%;" class="center-align">
                <a href="./pay_his.aspx">
                    <img alt="" src="../images/status.png" style="width: 100px; height: 100px" /></a></td>
        </tr>
        
        <tr>
            <td style="width: 30%;" class="center-align">
                <a href="./pay_stats.aspx">VIEW STATISTICS</a></td>
            <td style="width: 30%;" class="center-align">
                <a href="../upd_pro.aspx">UPDATE PROFILE</a></td>
            <td style="width: 30%;" class="center-align">
                <a href="./pay_his.aspx" >PAYMENT HISTORY</a></td>
        </tr>
        
        <tr>
            <td style="width: 30%;" class="center-align">
                &nbsp;</td>
            <td style="width: 30%;" class="center-align">
                &nbsp;</td>
            <td style="width: 30%;" class="center-align">
                &nbsp;</td>
        </tr>
        
       
          <tr>
            <td class="center-align" colspan="3">
            <b style="font-family:Cambria;font-size:13px;">POWERED BY EINAO SOLUTIONS</b>
            </td>
        </tr>
        
        <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" colspan="3">
              
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
