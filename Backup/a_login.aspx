<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="a_login.aspx.cs" Inherits="Ipong.a_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>XPAY: ADMIN LOGIN</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
<script src="js/jquery.js" type="text/javascript"></script>
<script src="js/funk.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 501px;
            height: 549px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <div class="container">
    <div class="sidebar"></div>
       
        <div class="content">        
 
            <table class="center-align" style="width:100%;">
             <tr class="center-align" >
                <td colspan="2">
                    <img alt="Coat Of Arms"  src="./images/headerpayxcld.jpg" 
                        />
               </td>
            </tr>
        <tr>
            <td colspan="2" class="center-align" style="background-color:#1C5E55; color:#ffffff; font-size:18px;">
        </tr>
       
        
        <tr align="right">
            <td style="width:50%;">
                <table style="width:400px;" class="login_tbl">
                <tr style="width:100%;">
                 <td  class="center-align">
                     USERNAME (email address)</td>
                </tr>
                <tr style="width:100%;">
                 <td  class="center-align">
            <asp:TextBox ID="xemail" runat="server" Width="200px" CssClass="textbox"></asp:TextBox>
                <% if (email_text == "1")
                   { %>
                <img src="images/arrow-left.gif" alt="" width="16px" height="16px" />
                <%  } if (enable_Save == "0")
                   { %><img src="images/checkmark.gif" alt="" width="16px" height="16px" />
                <% }%></td>
                </tr>
                <tr style="width:100%;">
                 <td  style="background-color:#1C5E55; color:#ffffff;">
                     </td>
                </tr>
                <tr style="width:100%;">
                 <td align="center">
            <asp:RadioButtonList ID="rblagentType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                <asp:ListItem Text="Agent" Value="Agent"></asp:ListItem>
                 <asp:ListItem Text="Sub-Agent" Value="SubAgent"></asp:ListItem>
                </asp:RadioButtonList>
                    </td>
                </tr>
               <% if (agentState != "0")
           { %>
            <tr style="width:100%;">
                 <td  style="background-color:#1C5E55; color:#ffffff;">
                     </td>
                </tr>
        <tr>
            <td  class="center-align">
                <strong>SORRY BUT YOU MUST SELECT AN OPTION ABOVE (AGENT OR SUB-AGENT).</strong>
            </td>
        </tr>
        <% } %>
          <% else
           { %>
            <tr style="width:100%;">
                 <td >&nbsp;
                     </td>
                </tr>
        <tr>
            <td >
              &nbsp;
            </td>
        </tr>
        <% } %>
                 <%if (enable_Captcha != "0")
          { %>
             <tr class="center-align" style="background-color:#1C5E55; color:#ffffff; font-size:14px;">
            <td class="center-align" > Please note that the letters below are not case sensitive!!!
                </td>
        </tr>
                <tr style="width:100%;">
                 <td  class="center-align">
                     ENTER CODE : 
            <asp:TextBox ID="xcode" runat="server" Width="90px" CssClass="textbox"></asp:TextBox>
            <% if (code_text == "1")
               { %>
                <img src="images/arrow-left.gif" alt="" width="16px" height="16px" />
                <% } if (enable_Save == "0")
               { %><img src="images/checkmark.gif" alt="" width="16px" height="16px" />
                <% }%>  </td>
                </tr>
        
        <tr>
            <td class="center-align"> <img alt="captcha" src="./xcaptcha.ashx" /><br />
                </td>
        </tr>
          <% if (newState != "0")
           { %>
            <tr style="width:100%;">
                 <td  style="background-color:#1C5E55; color:#ffffff;">
                     </td>
                </tr>
        <tr>
            <td  class="center-align">
                <strong>SORRY BUT THE CODE YOU ENTERED IS INVALID.</strong>
            </td>
            
        </tr>
        <% } %>
         <% else
           { %>
            <tr style="width:100%;">
                 <td >
                    &nbsp;  </td>
                </tr>
        <tr>
            <td  class="center-align">
                &nbsp;
            </td>
            
        </tr>
        <% } %>
                <% if (newp != "0")
           { %>
            <tr style="width:100%;">
                 <td  style="background-color:#1C5E55; color:#ffffff;">
                     </td>
                </tr>
           <tr style="width:100%;">
                 <td  class="center-align">
                     PASSWORD</td>
                </tr>
        <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" ></td>
        </tr>        
        
        <tr>          
            <td class="center-align">
            <asp:TextBox ID="xpassword" runat="server" Width="200px" CssClass="textbox" TextMode="Password" onunload="xpassword_Unload" ></asp:TextBox>
                 </td>
        </tr>   
         <tr style="width:100%;">
                 <td  class="center-align">
                     FORGOT PASSWORD?</td>
                </tr>     
        <% } %>
           <% else
           { %>
            <tr style="width:100%;">
                 <td >
                    &nbsp;  </td>
                </tr>
           <tr style="width:100%;">
                 <td  >
                     &nbsp;</td>
                </tr>
        <tr>
            <td  > &nbsp;</td>
        </tr>        
        
        <tr>          
            <td >
           &nbsp;
                 </td>
        </tr>   
         <tr style="width:100%;">
                 <td>
                      &nbsp;</td>
                </tr>     
        <% } %>
               
               <tr>
            <td  class="center-align">
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
                <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>
               
                <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>
                <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>
                <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>
                <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>
                
        <%} %>

         <% else
          { %>              
        
             <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>
          <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>    
        
          <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>
           <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>   
       
               
                 <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>
               
                <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>
                <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>
                <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>
                <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>
                <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>
                  <tr style="width:100%;">
                 <td  class="center-align">
                     &nbsp;</td>
                </tr>
        <%} %>



                </table></td>
            <td class="left-align" style="width:50%;" >
                <img alt="Notification Box" class="style1" src="images/notification_box.jpg" /></td>
        </tr>
       
        
      
        
        <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" colspan="2">
            </td>
        </tr>
       
        
        
         
         
          <tr>
            <td class="center-align" colspan="2">
            <b style="font-family:Cambria;font-size:13px;">POWERED BY<br />  <img alt="Einao Solutions"  src="../images/einao_logo.png" /></b>
            </td>
        </tr>
    </table>
        </div>
     </div>   
    
    
        </div>
    </form>
</body>
</html>
