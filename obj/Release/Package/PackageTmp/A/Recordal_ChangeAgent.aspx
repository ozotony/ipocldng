<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recordal_ChangeAgent.aspx.cs" Inherits="Ipong.A.Recordal_ChangeAgent" %>

<!DOCTYPE html>

<html data-ng-app="myModule" >
<head >
    <title>WELCOME TO IPO NIGERIA OFFICIAL WEB PORTAL</title>
    <link href="../css/a_structure.css" rel="stylesheet" type="text/css" />
    <script src="../js/timers.js" type="text/javascript"></script> 
    <script src="../js/aj.js" type="text/javascript"></script> 
     <script src="../js/jquery-2.1.1.min.js"></script>
     
    <script src="../js/angular.min.js"></script>

    <script src="../js/AngularLogin6.js"></script>

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
    <img alt="Principal Photo"  src="../../admin/ag_docz/ag/1/logo_new.png" />
    </div>
    <h1>WELCOME ONLINE REGISTRY  OFFICE </h1>
    <img alt="Einao Solutions"  src="../admin/ag_docz/ag/1/logo_new.png"  style="width: 200px; height: 70px" /><br />
   <span id="sc" style="font-size:12px;padding-right:10px;" ></span>
   
   <a href="http://www.iponigeria.com/#/logout" style=" font-size:12px;" >Log Out <img alt="" src="../images/LOGOUT.png" style="width: 20px; height: 20px" /></a>

</div>

<div id="section_header">ACCREDITED AGENT DASHBOARD  </div>
<div id="x_types">
<div id="side_menu">
<div id='flyout_menu'>
<ul>
   <li><a href='./profile.aspx'><span>DASHBOARD</span></a></li>
   <li> <a href="#" onclick="postwith('http://88.150.164.30/EinaoTestEnvironment.Payx/A/m_payx.aspx', { agentType: 'Agent', pwalletID: '1' });"><span>MAKE PAYMENT</span></a></li>
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
<form name="form1" method="post" action="Recordal_ChangeAgent.aspx" id="form1" class="form-inline">
<div>
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="/wEPDwUKMTIwMTE5MjE2OWRk30Oc6pppbvALaN97X3zZM3nDKGrlWfRg0MGHdRWrRC0=" />
</div>

<div>

	<input type="hidden" name="__EVENTVALIDATION" id="__EVENTVALIDATION" value="/wEdAAfVfdozC8hNFgerDZKLjXIsvwF7M5WT4u6MMVfa0bGHEIkmlpRJoRvMwd1hcP8FaPx13eTm3AG4ZMiLcYdQXR3KFGDZDSAVjoAb+dBgl75NNI2KXfd977Hsiz/9IMyLh6ZLZmxMUPc1GQ4R6JI8Vg9ikYqdhe+vKonjDrPPK+Jj5Be14p//mvcq3dnKzhjtGKs=" />
</div>
    <div>
       
      
        
        

           

        
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
                 
                
               
                <th st-sort="oai_no" class="tbbg2">OAI NUMBER</th>
                <th st-sort="reg_dt" class="tbbg2">FILING DATE</th>
                 <th st-sort="reg_no" class="tbbg2"> FILE No</th>
                 <th st-sort="applicant_name" class="tbbg2">APPLICANT NAME</th>
                 <th  st-sort="product_title" class="tbbg2">PRODUCT TITLE</th>
                 <th  st-sort="product_title" class="tbbg2">AGENT CODE</th>
                <th  st-sort="product_title" class="tbbg2">AGENT NAME</th>
                   <th  st-sort="product_title" class="tbbg2">CLASS</th>
                   <th  st-sort="product_title" class="tbbg2">STATUS</th>
            
          
           
               
                <th  class="tbbg2">CHANGE</th>
                

            </tr>
           
        </thead>
        <tbody>
            <tr ng-repeat="row in displayedCollection">
               
              
                
                
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
               
               <a target="_blank" ng-click="add2(row); $event.preventDefault(); $event.stopPropagation();" class="icon-bar" href="#"> <i class="fa  fa-pencil-square-o "></i></a>
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
   
      <input type="hidden" name="xname" id="xname" runat="server" value=" OFFICE" />
    
     <input type="hidden" name="xaddress" id="xaddress" runat="server" value="AREA 1" />

     <input type="hidden" name="xemail" id="xemail" runat="server" value="einaosolutionsnigeria@gmail.com" />

      <input type="hidden" name="xPhoneNumber" id="xPhoneNumber" runat="server" value="09038979681" />

     <input type="hidden" name="xpwalletID" id="xpwalletID" runat="server" value="1" />

     <input type="hidden" name="vsys_id" id="vsys_id"  runat="server" value="CLD/RA/0003" />
    


        
    
    </div>
    </form>
    </div>

<div id="ads2" style="color:#fff;text-align:center;">ADVERTORIALS</div>
</div>



</div>
<script type="text/javascript">
    new CountUp('Thursday, January 26, 2017 8:16:59 PM', 'sc', " Since logged on");
    </script>
</body>
</html>

