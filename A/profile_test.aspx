<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="profile_test.aspx.cs" Inherits="Ipong.A.profile_test" %>

<!DOCTYPE html>

<html >
<head runat="server">
    <title></title>
     <link href="../css/a_structure.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-2.1.1.js"></script>
    <script src="../js/metro.min.js"></script>
    <link href="../css/metro.css" rel="stylesheet" />
</head>
<body>
     <div class="grid">
                 <div class="app-bar cell " data-role="appbar">
       
        <div >
            <div class="app-bar-element"  >     ACCREDITED AGENT DASHBOARD  </div>

        </div>
            </div>
         </div>
    <form id="form1" runat="server">
      
            <div class="grid">
            <div class="row cells5"> 
        <div class="cell">
        <div id='flyout_menu'>
   
         <input id="xname" name="xname" type="hidden" runat="server" />
<ul>
   <li><a data-bind="visible: Agent" href='./profile.aspx'><span>DASHBOARD</span></a></li>
  <%-- <li data-bind="visible: Agent"> <a href="#" onclick="postwith('<%=System.Configuration.ConfigurationManager.AppSettings["payx_home"] %>', { agentType: '<%=agentType %>', pwalletID: '<%=adminID %>' });"><span>MAKE PAYMENT</span></a></li>--%>
   <li data-bind="visible: Agent" ><a href='./v_bask.aspx'><span>VIEW BASKET</span></a></li>

   <li data-bind="visible: Agent"><a href='#'><span>REPORTS</span></a>
    <ul> 
        <li><a href='./xreport_payments.aspx' data-bind="visible: Agent"><span>PAYMENTS</span></a> </li> 
        <li data-bind="visible: Agent"><a href='xreport_apps.aspx'><span>APPLICATIONS</span></a></li>   
        </ul> 
   </li>
   <li data-bind="visible: Agent"><a href='#'><span>STATUS</span></a>
    <ul>        
        <li data-bind="visible: Agent"><a href='xstatus_payments.aspx'><span>PAYMENTS</span></a></li>
       <li data-bind="visible: Agent"><a href='#'><span>APPLICATIONS</span></a>
            <ul>
             <li><a href="./xstatus_ds_apps.aspx"><span>DESIGN</span></a></li>
            <li><a href="./xstatus_pt_apps.aspx"><span>PATENT</span></a></li>
            <li><a href="./xstatus_tm_apps.aspx"><span>TRADEMARK</span></a></li>                
           </ul>    
      </li>   
      </ul> 
   </li>

   <li data-bind="visible: Agent"><a href='./xmail.aspx'><span>CONTACT IPO</span></a></li> <li data-bind="visible: Agent"><a href='./feelist.aspx'><span>FEE LIST ITEMS</span></a></li>

   <%-- <li data-bind="visible: Agent"> <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">BRANCH COLLECT <span> <br /><br /> PAYMENT</span> </asp:LinkButton> </li>--%>
</ul>
        </div>
            </div>
        <div class="cell colspan3">
            <div class="tile">
    <div class="tile-content">
        <div class="image-container">
            <div class="frame">
                <img src="../images/MAKE PAYMENT.png">
            </div>
            <div class="image-overlay">
               MAKE PAYMENT
            </div>
        </div>
    </div>

                 <div class="tile-content">
        <div class="image-container">
            <div class="frame">
                <img src="../images/MAKE PAYMENT.png">
            </div>
            <div class="image-overlay">
               MAKE PAYMENT2
            </div>
        </div>
    </div>
</div>


        </div>

         <div class="cell">
       
         </div>
    </div>
            </div>
         <div class="grid">
             <div class="row">
        <div class="cell">
           <div id="left_footer_menu">
<a href="#">RELATED LINKS</a> | <a href="#">NEWS</a> | <a href="#">IPO WEBSITE</a> | <a href="#">PUBLICATIONS</a> | <a href="../user_guide.pdf" target="_blank">USER GUIDE</a> </div>
<div id="right_footer_menu">EVENTS &amp; FEATURES</div>
<div id="bottom_footer">
   <b style="font-family:Cambria;font-size:13px;">POWERED BY<br />  <img alt="Einao Solutions" class="style2"  src="../images/einao_logo.png" /></b>
   </div>

        </div>
    </div>
</div>
       

   
    </form>
</body>
</html>
