<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manual_Trademark.aspx.cs" Inherits="Ipong.A.Manual_Trademark" %>

<!DOCTYPE html>

<html data-ng-app="myModule" >
<head >
    <title>WELCOME TO IPO NIGERIA OFFICIAL WEB PORTAL</title>
    <link href="../css/a_structure.css" rel="stylesheet" type="text/css" />
    <script src="../js/timers.js" type="text/javascript"></script> 
    <script src="../js/aj.js" type="text/javascript"></script> 
     <script src="../js/jquery-2.1.1.min.js"></script>
     
    <script src="../js/angular.min.js"></script>

    <script src="../js/AngularLogin3.js"></script>

    <script src="../js/ng-modal.min.js"></script>
    <link href="../css/ng-modal.css" rel="stylesheet" />
    <script src="../js/sweet-alert.min.js"></script>
    <link href="../css/sweet-alert.css" rel="stylesheet" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" />

    <script src="../js/smart-table.min.js"></script>

    <script src="../js/loading-bar.js"></script>
    <link href="../css/loading-bar.css" rel="stylesheet" />
    <script src="../js/stickytooltip.js"></script>
    <link href="../css/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="../css/stickytooltip.css" rel="stylesheet" />
    <script src="../js/angular-datepicker.min.js"></script>
  
      <script src="../js/angular-messages.min.js"></script>

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
    .icon-zoom-in
{
  position:relative;
  left:23px;
  top:-8px;
  z-index:999;
}
.input-append input
{
  height:30px;
  padding-left:25px;
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
<body ng-controller="myController3">
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
<form id="form1" runat="server" class="form-inline" >
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
<div class="container">

     
           
    <div id="mystickytooltip" class="stickytooltip">
<div style="padding:5px">

<div id="sticky1">
Enter a valid Rtm No as displayed on your Certificate Of Registration.
</div>

         



   

    </div>

<div class="stickystatus"></div>
</div>

    
  <table class="table table-responsive table-bordered ">
      <tr>
          <td style="align-content:center">
              
              <div class="col-sm-9">
            <div  >
                <label>
    <input type="radio" ng-model="Searchname" ng-change='newValue(value)' value="rtm">
    Search Using Rtm No
  </label>
                <label>
    <input type="radio" ng-model="Searchname" ng-change='newValue(value)' value="file">
    Search Using File/Tp No
  </label>
               
            </div>
        </div>

          </td>

          <td>
         <input type="text" ng-model="OnlineNumber" data-tooltip="sticky1" ng-show="checked" class="form-control" id="OnlineNumber" placeholder="Rtm Number">
         <input type="text" ng-model="OnlineNumber" data-tooltip="mystickytooltip2" ng-show="checked2" class="form-control" id="OnlineNumber2" placeholder="File/Tp Number">


          </td>

          <td>
             

               <button type="button" ng-click="add()" class="btn   btn-info "><i class="fa fa-search"></i>Search</button>

          </td>

      </tr>


  </table>
   <div class="row col-lg-11 col-md-11"> 
                   <table st-table="displayedCollection" st-safe-src="ListAgent" class="table form table-responsive  col-lg-3 col-md-3  table-striped">
        <thead>
            <tr>
                 <%--<th  class="tbbg2">S/N</th>--%>
                
               
                <th st-sort="oai_no" class="tbbg2">OAI NUMBER</th>
                <th st-sort="reg_dt" class="tbbg2">FILING DATE</th>
                 <th st-sort="reg_no" class="tbbg2"> FILE No</th>
                 <th st-sort="applicant_name" class="tbbg2">APPLICANT NAME</th>
                 <th  st-sort="product_title" class="tbbg2">PRODUCT TITLE</th>
                 <th  st-sort="product_title" class="tbbg2">AGENT CODE</th>
                <th  st-sort="product_title" class="tbbg2">AGENT NAME</th>
                   <th  st-sort="product_title" class="tbbg2">CLASS</th>
                   <th  st-sort="product_title" class="tbbg2">STATUS</th>
            
          
           
               
                <th  class="tbbg2">PAY</th>
                

            </tr>
           
        </thead>
        <tbody>
            <tr ng-repeat="row in displayedCollection">
               
              <%--  <td align="center">{{row.Sn}}</td>--%>
                
                
                <td >{{row.oai_no}}</td>
                <td >{{row.reg_dt}}</td>
                 <td >{{row.reg_no}}</td>
                <td>{{row.applicant_name}}</td>
                <td >{{row.product_title}}</td>
                 <td >{{row.Agent_Code}}</td>
                    <td >{{row.Agent_Name}}</td>
                 <td >{{row.xclass}}</td>
                 <td >{{row.xstat}}</td>
                
                 

              

                 <td align="center">
               
               <a target="_blank" ng-click="add2(row); $event.preventDefault(); $event.stopPropagation();" class="icon-bar" href="#"> <i class="fa  fa-paypal "></i></a>
            </td>

             
                
                


              
            </tr>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="10" class="text-center">
                    <div st-pagination="" st-items-by-page="itemsByPage" st-displayed-pages="7"></div>
                </td>
            </tr>
        </tfoot>
    </table>
       </div>
   
      <asp:HiddenField ID="xname" runat="server" />
    
     <asp:HiddenField ID="xaddress" runat="server" />

     <asp:HiddenField ID="xemail" runat="server" />

      <asp:HiddenField ID="xPhoneNumber" runat="server" />

     <asp:HiddenField ID="xpwalletID" runat="server" />

     <asp:HiddenField ID="vsys_id" runat="server" />
    


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