<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="confirm_basket_details.aspx.cs" Inherits="Ipong.A.confirm_basket_details"  MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>WELCOME TO IPO NIGERIA OFFICIAL WEB PORTAL</title>
    <link href="../css/ain_structure.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" href="../css/jquery.ui.all.css" />
     <link rel="stylesheet" href="../css/jquery.ui.theme.css" />
     <link rel="stylesheet" href="../css/jquery.ui.tabs.css" />
     <link href="../css/jquery.ui.timepicker.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../ui/jquery.ui.datepicker.js"></script>
    <script src="../js/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../js/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../js/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../js/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
     <script src="../js/timers.js" type="text/javascript"></script> 
    <script src="../js/aj.js" type="text/javascript"></script>
   <script src="../js/funk.js" type="text/javascript"></script>
    <script type="text/javascript">
        showClock();
        </script>

<script  type="text/javascript">

    function doUrlPost2() {
        
        var xapplicant_addy = $("input#xapplicant_addy").val()
        var xurl = $("input#xurl").val()
        var xtransid = $("input#xtransid").val()
        var xamt = $("input#xamt").val()
        var xagent = $("input#xagent").val()
        var xgt = $("input#xgt").val()

        var xapplicant_name = $("input#xapplicant_name").val()

        var xapplicant_email = $("input#xapplicant_email").val()

        var xapplicant_no = $("input#xapplicant_no").val()

        var xapplicant_addy = $("input#xapplicant_addy").val()

        var xagent2 = $("input#xagent2").val()

        var xagentname = $("input#xagentname").val()

        var xagentemail = $("input#xagentemail").val()

        var xagentpnumber = $("input#xagentpnumber").val()

        var xproduct_title = $("input#xproduct_title").val()

        var xitem_code = $("input#xitem_code").val()

        var xpc = $("input#xpc").val()

        var xhwalletID = $("input#xhwalletID").val()

        var xfee_detailsID = $("input#xfee_detailsID").val()

        doUrlPost(xurl, xtransid, xamt, xagent, xgt, xapplicant_name, xapplicant_email, xapplicant_no, xapplicant_addy, xagent2, xagentname, xagentemail, xagentpnumber, xproduct_title, xitem_code, xpc, xhwalletID, xfee_detailsID)
     //   alert(aa)

    }


    function doUrlPost(x_url, transID, amt, agt, xgt, applicantname, applicantemail, applicantpnumber, applicant_addy, agent,agentname, agentemail, agentpnumber, product_title, item_code, pc, hwalletID, fee_detailsID) {
       
      
        postwith(x_url, {
            transID: transID, amt: amt, agt: agt, xgt: xgt, applicantname: applicantname, applicantemail: applicantemail, applicantpnumber: applicantpnumber, applicant_addy: applicant_addy, agent: agent, 
            agentname: agentname, agentemail: agentemail, agentpnumber: agentpnumber, product_title: product_title, item_code: item_code, pc: pc, hwalletID: hwalletID,fee_detailsID:fee_detailsID
        });
    }
	</script>
  

</head>
<body>
<form id="form1" runat="server">
<div id="container">
<div id="hd_main_space">
<div id="hd_t">
<div id="hd_tl">
<a href="./profile.aspx"><img src="../images/LOGOCLD.jpg" width="458px" height="76px"  alt="CLD" /></a>
</div>
</div>

</div>

<div id="main">
<div id="welcome">
    <div style="float:left; padding:5px 0 0 10px;">
   <img alt="Principal Photo" <%if( (Session["pic"].ToString()!="")&&(Session["pic"]!=null)){ %> src="<%=Session["pic"].ToString() %>"<%} else { %>src="../images/shadow_male.jpg"<%} %> />
    </div>
    <h1>WELCOME <%if(Session["coy_name"]!=null){Response.Write(Session["coy_name"].ToString().ToUpper());}%> </h1>
    <img alt="Einao Solutions" <%if( (Session["logo"].ToString()!="")&&(Session["logo"]!=null)){ %> src="<%=Session["logo"].ToString() %>"<%} else { %>src="../images/einao_logo.png"<%} %>  style="width: 200px; height: 70px"  /><br />
   <span id="sc" style="font-size:12px;padding-right:10px;" ></span>
  
   <a href="http://www.iponigeria.com/#/logout" style=" font-size:12px;" >Log Out <img alt="" src="../images/LOGOUT.png" style="width: 20px; height: 20px" /></a>

</div>

<div id="section_header">ACCREDITED AGENT DASHBOARD  </div>
<div id="x_types">
<div id="side_menu">
<div id='flyout_menu'>
<ul>
   <li><a href='./profile.aspx'><span>DASHBOARD</span></a></li>
   <li> <a href="#" onclick="postwith('<%=System.Configuration.ConfigurationManager.AppSettings["payx_home"] %>', { agentType: '<%=agentType %>', pwalletID: '<%=adminID %>' });"><span>MAKE PAYMENT</span></a></li>
   <li><a href='./v_bask.aspx'><span>VIEW BASKET</span></a></li>

   <li><a href='#'><span>REPORTS</span></a>
    <ul> 
        <li><a href='./xreport_payments.aspx'><span>PAYMENTS</span></a> </li> 
       <li><a href='xreport_apps.aspx'><span>APPLICATIONS</span></a></li>   
        </ul> 
   </li>
   <li><a href='#'><span>STATUS</span></a>
    <ul>        
        <li><a href='xstatus_payments.aspx'><span>PAYMENTS</span></a></li>
        <li><a href='#'><span>APPLICATIONS</span></a>
            <ul>
             <li><a href="#"><span>DESIGN</span></a></li>
            <li><a href="./xstatus_pt_apps.aspx"><span>PATENT</span></a></li>
            <li><a href="./xstatus_tm_apps.aspx"><span>TRADEMARK</span></a></li>                
           </ul>    
      </li>   
      </ul> 
   </li>

   <li><a href='./xmail.aspx'><span>CONTACT IPO</span></a></li> <li><a href='./feelist.aspx'><span>FEE LIST ITEMS</span></a></li>
</ul>
</div>

<div class="side_hd">--- IPO WEBSITE--- </div>
<div id='home_menu'>
<ul>
   <li><a href='#'><span>HOME</span></a></li>
   <li> <a href="#" ><span>NEWS</span></a></li>
   <li><a href='#'><span>RELATED LINKS</span></a></li>
   <li> <a href="#" ><span>PUBLICATIONS</span></a></li>
   <li><a href="../user_guide.pdf" target="_blank"><span>USER GUIDE</span></a></li>
  
</ul>
</div>

</div>

<div id="x_main">
<table  style="width:100%;">      

         <tr class="center-align" style="background-color:#1C5E55; color:#ffffff; font-size:18px;">
            <td class="center-align" colspan="2">PLEASE CONFIRM THE DETAILS BELOW</td>
        </tr>
        <tr >
            <td style="text-align:center;" colspan="2" >
                <br />
                </td>
        </tr>      
       
         <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align" colspan="2">ITEM DETAILS</td>
        </tr>
         <tr >
            <td  style="width:100px; background-color:#808080;"> Product Title:</td>    
            <td > <%=ret.DecodeChar(ti.product_title) %></td>    
            </tr>      
          <tr >
            <td  style="width:100px; background-color:#808080;"> Item Code:</td>    
            <td >  <%=ret.DecodeChar(ti.item_code) %></td>    
            </tr>  
        <tr >
            <td  style="width:100px; background-color:#808080;"> Transaction ID: </td>    
            <td >  <%=ret.DecodeChar(ti.transID) %></td>    
            </tr> 
     <% string new_amt = string.Format("{0:n}", Convert.ToInt32(ti.amt)); %> 
        <tr >
            <td  style="width:100px; background-color:#808080;"> Amount: </td>    
            <td >  <%=ret.DecodeChar(new_amt)%></td>    
            </tr> 
        
           <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align" colspan="2">APPLICANT DETAILS</td>
        </tr>
         
        <tr >
            <td  style="width:100px; background-color:#808080;">Name: </td>    
            <td > <%=ret.DecodeChar(ti.applicant_name) %></td>    
            </tr>  
        <tr >
            <td  style="width:100px; background-color:#808080;">Address: </td>    
            <td > <%=ret.DecodeChar(ti.applicant_addy) %></td>    
            </tr>  
        <tr >
            <td  style="width:100px; background-color:#808080;">E-mail :</td>    
            <td > <%=ret.DecodeChar(ti.applicant_email) %></td>    
            </tr>
         <tr >
            <td  style="width:100px; background-color:#808080;">Phone Number:</td>    
            <td > <%=ret.DecodeChar(ti.applicant_no) %></td>    
          </tr> 
         
        
           <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align" colspan="2">AGENT DETAILS</td>
        </tr>
         
        
          <tr >
            <td  style="width:100px; background-color:#808080;">Name: </td>    
            <td > <%=ret.DecodeChar(ti.agentname) %></td>    
            </tr>  
        
        <tr >
            <td  style="width:100px; background-color:#808080;">E-mail :</td>    
            <td >  <%=ret.DecodeChar(ti.agentemail) %></td>    
            </tr>
         <tr >
            <td  style="width:100px; background-color:#808080;">Phone Number:</td>    
            <td > <%=ret.DecodeChar(ti.agentpnumber) %></td>    
          </tr> 
        
           <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align" colspan="2"></td>
        </tr>
               
       <tr class="center-align" >
           
           
           
           
           
            <td class="center-align" colspan="2"><input id="btnProceed" type="button" value="File Application"  class="button"  
 <%--   onclick="doUrlPost('<%=ti.xurl.Trim().Replace("\n","").Replace("\t","").Replace("\r","").Trim()%>','<%=ti.transID%>','<%=ti.amt%>','<%=ti.agent%>','<%=ti.xgt%>','<%=ti.applicant_name.Trim().Replace("\n","").Replace("\t","").Replace("\r","").Replace(" ''","").Trim()%>','<%=ti.applicant_email.Trim().Replace("\n","").Replace("\t","").Replace("\r","").Trim()%>','<%=ti.applicant_no.Trim().Replace("\n","").Replace("\t","").Replace("\r","").Trim()%>','<%=ti.applicant_addy.Trim().Replace("\n","").Replace("\t","").Replace("\r","").Trim()%>','<%=ti.agent%>','<%=ti.agentname.Trim().Replace("\n","").Replace("\t","").Replace("\r","").Trim()%>','<%=ti.agentemail.Trim().Replace("\n","").Replace("\t","").Replace("\r","").Trim()%>','<%=ti.agentpnumber.Trim().Replace("\n","").Replace("\t","").Replace("\r","").Trim()%>','<%=ti.product_title.Trim().Replace("\n","").Replace("\t","").Replace("\r","").Trim()%>','<%=ti.item_code%>','<%=ti.pc%>','<%=ti.hwalletID%>','<%=ti.fee_detailsID%>');return false;" />--%>
 onclick="doUrlPost2();return false;" />
                <input id="btnBack" type="button" value="Back"  class="button"   onclick="doHistory('1');return false;" />
            </td>
        </tr>


          </table>

     <asp:HiddenField ID="xurl" runat="server" />
            <asp:HiddenField ID="xtransid" runat="server" />
           <asp:HiddenField ID="xamt" runat="server" />
            <asp:HiddenField ID="xagent" runat="server" />
           <asp:HiddenField ID="xgt" runat="server" />
            <asp:HiddenField ID="xapplicant_name" runat="server" />
           <asp:HiddenField ID="xapplicant_email" runat="server" />
            <asp:HiddenField ID="xapplicant_no" runat="server" />
            <asp:HiddenField ID="xapplicant_addy" runat="server" />
            <asp:HiddenField ID="xagent2" runat="server" />
             <asp:HiddenField ID="xagentname" runat="server" />
           <asp:HiddenField ID="xagentemail" runat="server" />

           <asp:HiddenField ID="xagentpnumber" runat="server" />

            <asp:HiddenField ID="xproduct_title" runat="server" />
            <asp:HiddenField ID="xitem_code" runat="server" />

           <asp:HiddenField ID="xpc" runat="server" />

           <asp:HiddenField ID="xhwalletID" runat="server" />

           <asp:HiddenField ID="xfee_detailsID" runat="server" />
           

</div>

</div>


</div>

</div>
</form>
<script type="text/javascript">
    new CountUp('<%=log_date %>', 'sc', " Since logged on");
    </script>
</body>
</html>
