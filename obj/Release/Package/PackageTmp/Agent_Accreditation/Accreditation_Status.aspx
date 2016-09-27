<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Accreditation_Status.aspx.cs" Inherits="Ipong.Agent_Accreditation.Accreditation_Status" %>

<!DOCTYPE html>

<html>
<head>
    <title>WELCOME TO IPO NIGERIA OFFICIAL WEB PORTAL</title>
    <link href="../css/a_structure.css" rel="stylesheet" type="text/css" />
    <script src="../js/timers.js" type="text/javascript"></script> 
    <script src="../js/aj.js" type="text/javascript"></script> 
    <style type="text/css">
        .style2  { width: 120px;height: 40px;}
    </style>
    <script type="text/javascript">
        showClock();
        </script>


     <script src="../js/jquery-2.1.1.min.js"></script>
    <script src="../js/knockout-2.2.0.js"></script>
    <script src="../js/Accreditation.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />



     <style type="text/css">
    #container .modal.fade {
         left: -25%;
          -webkit-transition: opacity 0.3s linear, left 0.3s ease-out;
             -moz-transition: opacity 0.3s linear, left 0.3s ease-out;
               -o-transition: opacity 0.3s linear, left 0.3s ease-out;
                  transition: opacity 0.3s linear, left 0.3s ease-out;
    }
    #container .modal.fade.in {
        left: 10%;top:150px;
    }
     #container .modal-body {
        min-height: 50px;
    }
  #article-editor { background:white; }
    #article-editor {
    width: 800px;
  
}
    
    </style>

    <script type="text/javascript">
        function printDiv(divName) {
          
          
            Popup($('#tb2').html());
           
        }


        function Popup(data) {
            var mywindow = window.open('', 'my div', 'height=400,width=600');
            mywindow.document.write('<html><head><title>Application</title>');
            mywindow.document.write("<link rel=\"stylesheet\" href=\"http://ipo.cldng.com/css/bootstrap.min.css\" type=\"text/css\" media=\"print\" />");
           
            mywindow.document.write('</head><body >');
            mywindow.document.write(data);
            mywindow.document.write('</body></html>');

            mywindow.print();
            //mywindow.close();

            return true;
        }

    </script>
</head>
<body>
<div id="container">
<div id="hd_main_space">
<div id="hd_t">
<div id="hd_tl">
<a href="./profile.aspx"><img src="../images/LOGOCLD.jpg" width="458px" height="76px"  alt="CLD" /></a>
</div>
</div>

</div>

<div id="welcome">
    <div style="float:left; padding:5px 0 0 10px;">
    <img alt="Principal Photo" <%if( (Session["pic"].ToString()!="")&&(Session["pic"]!=null)){ %> src="../<%=Session["pic"].ToString() %>"<%} else { %>src="../images/shadow_male.jpg"<%} %> />
    </div>
    <h1>WELCOME <%if(Session["coy_name"]!=null){Response.Write(Session["coy_name"].ToString().ToUpper());}%> </h1>
    <img alt="Einao Solutions" <%if( (Session["logo"].ToString()!="")&&(Session["logo"]!=null)){ %> src="<%=Session["logo"].ToString() %>"<%} else { %>src="../images/einao_logo.png"<%} %>  style="width: 200px; height: 70px" /><br />
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
        <li ><a href='./xreport_payments.aspx'><span>PAYMENTS</span></a> </li> 
        <li   ><a href='xreport_apps.aspx'><span>APPLICATIONS</span></a></li>   
        </ul> 
   </li>
   <li ><a href='#'><span>STATUS</span></a>
    <ul>        
        <li ><a href='xstatus_payments.aspx'><span>PAYMENTS</span></a></li>
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
</div>

<div id="x_main">
<form id="form1" runat="server">
    <div>
        
      
        
          <%--<div data-bind="text: selectedChoicesDelimited"></div>
              <h3>Select your favorite sports:</h3>
        <label><input type="checkbox" value="football" name="sport"/> Football</label>
        <label><input type="checkbox" value="baseball" name="sport"/> Baseball</label>
        <label><input type="checkbox" value="cricket" name="sport"/> Cricket</label>
        <label><input type="checkbox" value="boxing" name="sport"/> Boxing</label>
        <label><input type="checkbox" value="racing" name="sport"/> Racing</label>
        <label><input type="checkbox" value="swimming" name="sport"/> Swimming</label>
        <br/>--%>

        
    <div >
     <div id="tb">
                 <table class="table table-bordered table-hover table-responsive " >
<thead>
    <tr>
        <th>Id</th>
        <th>Agent Code</th>
         <th>First Name</th>
          <th>Surname</th>
        <th>Payment Status</th>
       
      
    <th> </th>

    
    </tr>
    </thead>
<tbody data-bind="foreach: Agents">


  
    <tr>
    <td data-bind="text: Xid"></td>
        <td data-bind="text: Agent_Code"></td>

         <td data-bind="text: FirstName"></td>

          <td data-bind="text: SurName"></td>

        <td data-bind="text: Paid_Status"></td>

       
      
     

      <td>  <button class="fa fa-trash-o" data-bind="click: $root.Updaterecord2">Approve</button> 

          <button class="fa fa-trash-o" data-bind="click: $root.Updaterecord5">Reject</button> 

          <a type="button" 
   class="btn" 
data-bind="click: $root.Updaterecord4"
   href="#article-editor" 
   data-toggle="modal">View</a>
        
          
            <%--<button class="fa fa-trash-o" data-bind="    click: $root.Updaterecord3, visible: $root.vvid2(Agent_Code) ">Remove Id</button> --%>


      </td>
       

     
       
    </tr>   

</tbody>
</table>
            
            </div>
    </div>


          <div class="modal fade"  id="article-editor">
    <div class="modal-header">
       <a class="close" data-dismiss="modal">&times;</a>
       <h3>APPLICATION</h3>
    </div>
    <div class="modal-body" id="tb2">
     <div id="tb">

<table class="table"   style="border:1px solid black;border-collapse:collapse;">
    <tbody data-bind="foreach: Agents3">
<tr>

<td style="border:1px solid black;" >
FIRSTNAME
</td>

<td style="border:1px solid black;" data-bind="text:Firstname" />



</tr>

        <tr>

<td style="border:1px solid black;" >
SURNAME
</td>

<td style="border:1px solid black;" data-bind="text: Surname" />



</tr>

         <tr>

<td style="border:1px solid black;" >
EMAIL
</td>

<td style="border:1px solid black;" data-bind="text: Email" />


</tr>

         <tr>

<td style="border:1px solid black;" >
DATE OF BIRTH
</td>

<td style="border:1px solid black;" data-bind="text: DateOfBrith" />



</tr>

          <tr>

<td style="border:1px solid black;" >
COMPANY NAME
</td>

<td style="border:1px solid black;" data-bind="text: CompanyName" />



</tr>


          <tr>

<td style="border:1px solid black;" >
COMPANY ADDRESS
</td>

<td style="border:1px solid black;" data-bind="text: CompanyAddress" />



</tr>


          <tr>

<td style="border:1px solid black;" >
CONTACT PERSON
</td>

<td style="border:1px solid black;" data-bind="text: ContactPerson" />



</tr>


        <tr>

<td style="border:1px solid black;" >
CONTACT PERSON PHONE
</td>

<td style="border:1px solid black;" data-bind="text: ContactPersonPhone" />



</tr>


         <tr>

<td style="border:1px solid black;" >
WEBSITE
</td>

<td style="border:1px solid black;" data-bind="text: CompanyWebsite" />



</tr>


          <tr>

<td style="border:1px solid black;" >
ACCREDITATION TYPE
</td>

<td style="border:1px solid black;" data-bind="text: AccrediationType" />



</tr>

          <tr>

<td style="border:1px solid black;" >
REGISTRATION DATE
</td>

<td style="border:1px solid black;" data-bind="text: xreg_date" />



</tr>




          <tr>

<td style="border:1px solid black;" >
CERTIFICATE
</td>

<td style="border:1px solid black;" />

<a data-bind="attr: { href:Certificate }" target="_blank">Download</a>



</tr>

         <tr>

<td style="border:1px solid black;" >
LETTER OF INTRODUCTION
</td>

<td style="border:1px solid black;" />

<a data-bind="attr: { href: Introduction }" target="_blank">Download</a>



</tr>

         <tr>

<td style="border:1px solid black;" >
PASSPORT
</td>

<td style="border:1px solid black;" />

<a data-bind="attr: { href: logo }" target="_blank">Download</a>



</tr>



        </tbody>

</table>


                 
        
            </div>
    </div>
    <div class="modal-footer">
         <input id="Button1" onclick="printDiv('printableArea')" type="button" value="PRINT" />
       <a href="#" class="btn" data-dismiss="modal">Close</a>
      
    </div>
</div>
  
</div>

           

       


     
    
  
    </form>
    </div>

<div id="ads" style="color:#fff;text-align:center;">ADVERTORIALS</div>
</div>

<div id="left_footer_menu">
<a href="#">RELATED LINKS</a> | <a href="#">NEWS</a> | <a href="#">IPO WEBSITE</a> | <a href="#">PUBLICATIONS</a> | <a href="../user_guide.pdf" target="_blank">USER GUIDE</a> </div>
<div id="right_footer_menu">EVENTS &amp; FEATURES</div>
<div id="bottom_footer">
   <b style="font-family:Cambria;font-size:13px;">POWERED BY<br />  <img alt="Einao Solutions" class="style2"  src="../images/einao_logo.png" /></b>
   </div>

</div>
<script type="text/javascript">
    new CountUp('<%=log_date %>', 'sc', " Since logged on");
    </script>
</body>
</html>