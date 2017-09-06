<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="a_login.aspx.cs" Inherits="Ipong.a_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>XPAY: ADMIN LOGIN</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/a_structure.css" rel="stylesheet" type="text/css" />
<script src="js/jquery.js" type="text/javascript"></script>
<script src="js/funk.js" type="text/javascript"></script>
    <link href="https://fonts.googleapis.com/css?family=Lato" rel="stylesheet"/>
    <style type="text/css">
        html {

            font-family: 'Lato', sans-serif;
            
        }
       
    </style>

    </head>
<body>
    <form id="form1" runat="server">
    <div>  
          <table  style="width:775px;border: 1px dashed #ccc; border-radius:5px;" align="center">
        <tr>
        <td>
           
           <%-- <div class="alert alert-info fade in">
                                            <img src="images/einao_logo.png" /> <br/>
                                            Dear User, <br />

                                            This site is currently unavailable due to some operational maintenance. We apologize for any inconveniences caused.<br />

                                            For any queries, please send us an email: info@einaosolutions.com <br />

                                            Thanks.

                                        </div>--%>
            
            <table class="center-align" style="width:100%; "   >
             <tr class="center-align">
                <td colspan="3">
                   <img src="./images/LOGOCLD.jpg" width="458px" height="76px"  alt="CLD" />
               </td>
            </tr>

             <tr>
            <td colspan="3" class="center-align" style="background-color:#1C5E55;">
            </td>
        </tr>
       
         <tr>
            <td colspan="3" class="tbbg_solid" style="font-size:14px;">
                PLEASE LOGIN IN USING YOUR E-MAIL ADDRESS AND PASSWORD
                </td>
        </tr>
          <tr>
            <td colspan="3" class="center-align" style="background-color:#1C5E55;">
            </td>
        </tr>
      
        
        <tr>
            <td class="right-align-align" align="right">
                &nbsp;E-MAIL: &nbsp;<br />
                <asp:TextBox ID="xemail" runat="server" Width="200px" CssClass="textbox" ></asp:TextBox>
                <% if (email_text == "1")
                   { %>
                <img src="images/arrow-left.gif" alt="" width="16px" height="16px" />
                <%  } if (enable_Save == "0")
                   { %><img src="images/checkmark.gif" alt="" width="16px" height="16px" />
                <% }%>
                 <asp:RadioButtonList ID="rblagentType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                <asp:ListItem Text="Agent" Value="Agent"></asp:ListItem>
                 <asp:ListItem Text="Sub-Agent" Value="SubAgent"></asp:ListItem>
                </asp:RadioButtonList>

                </td>
                
            <td rowspan="3" style="width: 20%;" class="left-align">
                <img alt="login" src="images/login_big.png" style="width: 128px; height: 128px" /></td>
                
            <td rowspan="3" style="width: 30%;" class="left-align">
                   <div id='flyout_menu_index'>
<ul>
   <li><a href='./index.html'><span> <img alt="Login" src="images/home_b.png" height="35px" width="35px" />&nbsp;&nbsp;&nbsp;HOME</span></a></li>
   <li><a href='./terms.pdf' target="_blank"><span> <img alt="Login" src="images/terms-and-conditions.png" height="35px" width="35px" />&nbsp;&nbsp;&nbsp;TERMS OF USE</span></a></li>
    <li><a href='./privacy.pdf' target="_blank"><span> <img alt="Login" src="images/privacy_policy.png" height="35px" width="35px" />&nbsp;&nbsp;&nbsp;PRIVACY POLICY</span></a></li>
   <li><a href='./disclaimer.pdf' target="_blank"><span> <img alt="Login" src="images/disclaimer.png" height="35px" width="35px" />&nbsp;&nbsp;&nbsp;DISCLAIMER</span></a></li>
    <li><a href='./user_guide.pdf' target="_blank"><span style="border-bottom:0px;">
      <img alt="User Guide" src="images/userguide.png" height="30px" width="30px" />&nbsp;&nbsp;&nbsp;USER GUIDE</span></a></li>
      <li><a href='./faq.pdf' target="_blank"><span style="border-bottom:0px;">
     <img alt="FAQ" src="images/faq_mini.png" height="30px" width="30px" />&nbsp;&nbsp;&nbsp;FAQ</span></a></li>
</ul>
</div>
</td>
        </tr>
        
      
        <%if (enable_Captcha != "0")
          { %>
          <tr>
            <td  align="left" style="background-color:#1C5E55;color:#fff; font-size:14px;">
           
                Please note that the letters below are not case sensitive!!!
                           
                </td>
        </tr>

        <tr>
            <td class="right-align"> <img alt="captcha" src="./xcaptcha.ashx" /><br /><br />
                ENTER CODE : 
            <asp:TextBox ID="xcode" runat="server" Width="90px" CssClass="textbox"></asp:TextBox>
            <% if (code_text == "1")
               { %>
                <img src="images/arrow-left.gif" alt="" width="16px" height="16px" />
                <% } if (enable_Save == "0")
               { %><img src="images/checkmark.gif" alt="" width="16px" height="16px" />
                <% }%>     
                </td>
        </tr>
        



        <% if (newState != "0")
           { %>
        <tr>
            <td colspan="3" class="center-align">
                <strong>SORRY BUT THE CODE YOU ENTERED IS INVALID.</strong>
            </td>
        </tr>
        <% } %>
       
        <% if (agentState != "0")
           { %>
        <tr>
            <td colspan="3" class="center-align">
                <strong>SORRY BUT YOU MUST SELECT AN OPTION ABOVE (AGENT OR SUB-AGENT).</strong>
            </td>
        </tr>
        <% } %>
        <% if (newp != "0")
           { %>
        <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" 
                colspan="3"></td>
        </tr>
        
        
        <tr>
            <td class="left-align">
                PASSWORD: 
            <asp:TextBox ID="xpassword" runat="server" Width="200px" CssClass="textbox" TextMode="Password" onunload="xpassword_Unload" ></asp:TextBox>
            </td>
            <td align="left" colspan="2">
                &nbsp;</td>
        </tr>        
        <% } %>
        <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" 
                colspan="3">
            </td>
        </tr>
        <tr>
            <td colspan="3" class="center-align">
                <% if (enable_Confirm == "0")
                   { %>
                <asp:Button ID="ConfirmDetails" runat="server" Text="Confirm Details" 
                    OnClick="ConfirmDetails_Click" class="button" />
               
                <% } if (enable_Save == "0")
                   { %>
                <asp:Button ID="Save" runat="server" Text="Login" OnClick="Save_Click" 
                    class="button" />
                <% }%>
            </td>
        </tr>
        
        
         <%} %>
         
    </table>
     <table style="width:100%;" class="center-align" >
         <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" 
                colspan="2">
            </td>
        </tr>
         <tr>
            <td colspan="2">&nbsp;
            </td>
        </tr>
         <tr style="font-size:12px;">
            <td class="center-align" align="center" colspan="2">            
                Home: <a href="http://www.iponigeria.com">www.iponigeria.com</a>&nbsp;&nbsp;Helpline:+2349038979681&nbsp;&nbsp;   
                 Support E-mail(s): <a href="mailto:customersupport@iponigeria.com">customersupport@iponigeria.com</a>,&nbsp;
                <a href="mailto:paymentsupport@einaosolutions.com">paymentsupport@einaosolutions.com</a>
            </td>
        </tr>
         <tr>
            <td class="center-align" align="center" colspan="2">           
             <div id="index_bottom_footer">
   <b style="font-family:Cambria;font-size:12px;">POWERED BY</b><br />
    <a href="http://www.einaosolutions.com" target="_blank"><img alt="Einao Solutions" src="./images/einao_logo.png" width="150px" height="60px"/>
    </a>
   </div>
             
             </td>
        </tr>
    </table>
      </td>
        </tr>
        </table>
       </div> 
    </form>
</body>
</html>

