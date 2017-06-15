<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Assign_Role.aspx.cs" Inherits="Ipong.Agent_Accreditation.Assign_Role" %>

<!DOCTYPE html>

<html data-ng-app="myModule" >
<head >
    <title>WELCOME TO IPO NIGERIA OFFICIAL WEB PORTAL</title>
    <link href="../css/a_structure.css" rel="stylesheet" type="text/css" />
    <script src="../js/timers.js" type="text/javascript"></script> 
    <script src="../js/aj.js" type="text/javascript"></script> 
     <script src="../js/jquery-2.1.1.min.js"></script>
     
    <script src="../js/angular.min.js"></script>

    <script src="../js/AngularLogin.js"></script>

    <script src="../js/ng-modal.min.js"></script>
    <link href="../css/ng-modal.css" rel="stylesheet" />
    <script src="../js/sweet-alert.min.js"></script>
    <link href="../css/sweet-alert.css" rel="stylesheet" />

    <style type="text/css">
        .style2  { width: 120px;height: 40px;}
    </style>
    <script type="text/javascript">
        showClock();
        </script>


    
   <%-- <script src="../js/knockout-2.2.0.js"></script>
    <script src="../js/Assign_Role.js"></script>--%>
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


            Popup($('#tx').html());

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
<form id="form1" runat="server" ng-controller="myController">
    <div>
       <%-- <div data-bind="foreach: $root.Roles">
      
    <input type="checkbox" data-bind="value: Role_Name(), checked: $root.associatedItemIds, click: $root.toggleAssociation" />	
    <span data-bind="text: Role_Name"></span>
</div>--%>
      
        
        

           <%--<table id="c_b"  class="table " >

    <tbody  >

    <tr>
        
      
        <td>   Agent Code        </td>
    
        <td> <input  type="text" data-bind="value: Agents_Code" disabled /> <a type="button" 
   class="btn" 
   href="#article-editor" 
   data-toggle="modal">Click To Select Agent</a>   </td>

           <td>   First Name       </td>
    
        <td>  <input  data-bind="value: First_Name" type="text" disabled /> </td>

          
    </tr>   


         <tr>
        
      
        <td>  Surname       </td>
    
        <td>  <input data-bind="value: SurName"  type="text" disabled /> </td>

           <td>   Email       </td>
    
        <td>  <input  type="text" data-bind="value: Email" disabled /> </td>

          
    </tr>   

</tbody>

</table>

           <div class=" row-fluid"> 
            <div class="span8"> 
          <div class="modal fade" id="article-editor">
    <div class="modal-header">
       <a class="close" data-dismiss="modal">&times;</a>
       <h3>Select Agent</h3>
    </div>
    <div class="modal-body">
     <div id="tb">
                 <table class="table table-bordered table-hover table-responsive " >
<thead>
    <tr>
     
        <th>Agent Code</th>
         <th>First Name</th>
          <th>Surname</th>
        <th>Email</th>
      
    <th> </th>

    
    </tr>
    </thead>
<tbody data-bind="foreach: Agents">


  
    <tr>
   
        <td data-bind="text: Agent_Code"></td>

         <td data-bind="text: FirstName"></td>

          <td data-bind="text: SurName"></td>

        <td data-bind="text: Email"></td>
      
     

      <td> <button class="fa fa-trash-o" data-bind="click: $root.Updaterec">Select</button></td>
       

     
       
    </tr>   

</tbody>
</table>
            
            </div>
    </div>
    <div class="modal-footer">
       <a href="#" class="btn" data-dismiss="modal">Close</a>
      
    </div>
</div>

            </div>
            </div>--%>

        <%-- <button id="Button3" type="submit" data-bind="click: $root.save" class="btn-primary">Submit </button>--%>

         <div >
                                                <ul class="pagination">
                                                    <li ng-class="prevPageDisabled()">
                                                        <a href="" ng-click="prevPage(); $event.preventDefault(); $event.stopPropagation();">« Prev</a>
                                                    </li>
                                                    <li>
                                                        <a href="#">{{currentPage +1}} of {{pageCount2+ 1}}</a>
                                                        

                                                    </li>
                                                   
                                                    <li ng-class="nextPageDisabled()">
                                                        <a href="" ng-click="nextPage(); $event.preventDefault(); $event.stopPropagation();">Next »</a>
                                                    </li>
                                                </ul>
                                            </div>
        <table class="table table-bordered table-hover table-responsive ">
              
                                        <thead>
                                            <tr>
                                                <th>
                                                   Agent Code

                                                </th>
                                                <th>Firstname</th>

                                                <th>Surname</th>
                                                <th>Email</th>
                                                <th>DateOfBrith</th>
                                                <th>IncorporatedDate</th>
                                                <th>PhoneNumber</th>
                                                <th>CompanyName</th>
                                                <th>Website</th>

                                                 <th>Payment Status</th>
                                                <th>Agent  Status</th>
                                                <th> </th>
                                               

                                            </tr>
                                        </thead>
                                        <tbody>
                                           <tr ng-repeat="user in BranchCollect|offset: currentPage*itemsPerPage |limitTo: itemsPerPage">
                                                <td>{{user.Sys_ID}}</td>
                                                <td>{{user.Firstname}}</td>
                                                <td>{{user.Surname}}</td>
                                                <td>{{user.Email}}</td>

                                                <td>{{user.DateOfBrith}}</td>

                                                <td>{{user.IncorporatedDate}}</td>

                                                <td>{{user.PhoneNumber}}</td>

                                                <td>{{user.CompanyName}}</td>

                                                <td>{{user.CompanyWebsite}}</td>

                                              
                                                <td>{{user.xsync}} </td>

                                               <td>{{user.xstatus}} </td>
                                               <td><a ng-click="EditRow(user);$event.preventDefault(); $event.stopPropagation();" href="#">View</a> </td>

                                            </tr>

                                        </tbody>
                                        <tfoot>
                                       
                                        <td colspan="3">
                                            <div >
                                                
                                            </div>
                                        </td>

                                        </tfoot>

                                    </table>

         <div >
                                                <ul class="pagination">
                                                    <li ng-class="prevPageDisabled()">
                                                        <a href="" ng-click="prevPage(); $event.preventDefault(); $event.stopPropagation();">« Prev</a>
                                                    </li>
                                                    <li>
                                                        <a href="#">{{currentPage +1}} of {{pageCount2+ 1}}</a>
                                                        

                                                    </li>
                                                   
                                                    <li ng-class="nextPageDisabled()">
                                                        <a href="" ng-click="nextPage(); $event.preventDefault(); $event.stopPropagation();">Next »</a>
                                                    </li>
                                                </ul>
                                            </div>
                                   
        <modal-dialog show='dialogShown'  dialog-title='Agent Detail'>
    <form method="post" name="reset">
        <div id="tx"> 
        <table class="table"   style="border:1px solid black;border-collapse:collapse;">
    <tbody >
<tr>

<td style="border:1px solid black;" >
FIRSTNAME
</td>

<td style="border:1px solid black;">
    {{user.Firstname}}
    </td>



</tr>

        <tr>

<td style="border:1px solid black;" >
SURNAME
</td>

<td style="border:1px solid black;" >
      {{user.Surname}}
    </td>



</tr>

         <tr>

<td style="border:1px solid black;" >
EMAIL
</td>

<td style="border:1px solid black;"  >
     {{user.Email}}
    </td>


</tr>

         <tr>

<td style="border:1px solid black;" >
DATE OF BIRTH
</td>

<td style="border:1px solid black;" >
      {{user.DateOfBrith}}
    </td>



</tr>


        <tr>

<td style="border:1px solid black;" >
ACREDITATION TYPE
</td>

<td style="border:1px solid black;"  >
      {{user.AccrediationType}}
</td>

</tr>

          <tr>

<td style="border:1px solid black;" >
COMPANY NAME
</td>

<td style="border:1px solid black;"  >
      {{user.CompanyName}}
</td>

</tr>


          <tr>

<td style="border:1px solid black;" >
COMPANY ADDRESS
</td>

<td style="border:1px solid black;"  >

      {{user.CompanyAddress}}
    </td>



</tr>


          <tr>

<td style="border:1px solid black;" >
CONTACT PERSON
</td>

<td style="border:1px solid black;"  >
     {{user.ContactPerson}}
</td>

</tr>


        <tr>

<td style="border:1px solid black;" >
CONTACT PERSON PHONE
</td>

<td style="border:1px solid black;"  >
     {{user.ContactPersonPhone}}
    </td>



</tr>


         <tr>

<td style="border:1px solid black;" >
WEBSITE
</td>

<td style="border:1px solid black;"  >
     {{user.CompanyWebsite}}
    </td>



</tr>

          <tr>

<td style="border:1px solid black;" >
REGISTRATION DATE
</td>

<td style="border:1px solid black;"  >
     {{user.xreg_date}}
    </td>



</tr>




          <tr>

<td style="border:1px solid black;" >
CERTIFICATE
</td>

<td style="border:1px solid black;" />



<a ng-href="http://ipo.cldng.com/{{user.Certificate}}" target="_blank">Download</a>

</tr>

         <tr>

<td style="border:1px solid black;" >
LETTER OF INTRODUCTION
</td>

<td style="border:1px solid black;" />



<a ng-href="http://ipo.cldng.com/{{user.Introduction}}" target="_blank">Download</a>

</tr>

         <tr>

<td style="border:1px solid black;" >
PASSPORT
</td>

<td style="border:1px solid black;" />



<a ng-href="http://ipo.cldng.com/{{user.logo}}" target="_blank">Download</a>



</tr>



        </tbody>

            

</table>

            </div>

        <span><input id="Button1" type="button" value="Print" onclick="printDiv('printableArea')" />  <input id="Button2" type="button" ng-click="block(user)" value="Block Agent" /> </span>
    </form>

</modal-dialog>

        <%--  <table class="table table-bordered table-hover table-responsive " >
<thead>
    <tr>
     
        <th>Agent Code</th>
         <th>First Name</th>
          <th>Surname</th>
        <th>Role</th>
      
    <th> </th>

    
    </tr>
    </thead>
<tbody data-bind="foreach: Agents2">


  
    <tr>
   
        <td data-bind="text: Agent_Code"></td>

         <td data-bind="text: FirstName"></td>

          <td data-bind="text: SurName"></td>

        <td data-bind="text: Email"></td>
      
     

      <td> <button class=" btn-danger " data-bind="click: $root.Delrec">Remove</button></td>
       

     
       
    </tr>   

</tbody>
</table>--%>
    
    </div>
    </form>
    </div>

<div id="ads2" style="color:#fff;text-align:center;">ADVERTORIALS</div>
</div>

<%--<div id="left_footer_menu">
<a href="#">RELATED LINKS</a> | <a href="#">NEWS</a> | <a href="#">IPO WEBSITE</a> | <a href="#">PUBLICATIONS</a> | <a href="../user_guide.pdf" target="_blank">USER GUIDE</a> </div>
<div id="right_footer_menu">EVENTS &amp; FEATURES</div>
<div id="bottom_footer">
   <b style="font-family:Cambria;font-size:13px;">POWERED BY<br />  <img alt="Einao Solutions" class="style2"  src="../images/einao_logo.png" /></b>
   </div>--%>

</div>
<script type="text/javascript">
    new CountUp('<%=log_date %>', 'sc', " Since logged on");
    </script>
</body>
</html>
