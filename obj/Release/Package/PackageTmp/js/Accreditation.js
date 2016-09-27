function vload() {

  
    var RoleViewModel = function () {
        //Make the self as 'this' reference
        var self = this;
        //Declare observable which will be bind with UI 
      
        self.Agents = ko.observableArray([]);

        self.Agents3 = ko.observableArray([]);

      //  self.selectedValue = ko.observable("");
       

     

       var url3 = "http://ipo.cldng.com/Handlers/Approve.ashx";

        //   var url3 = "http://localhost:4556/Handlers/Approve.ashx";

       var url6 = "http://ipo.cldng.com/Handlers/Reject.ashx";

        var url5 = "http://ipo.cldng.com/Handlers/GetRegistration.ashx";

       

        var url4 = "http://ipo.cldng.com/Handlers/Approve2.ashx";

        self.vvid = function (employee) {
           
            if ((employee != null) ) {
                return false;
            }
            else
            {
                return true;
            }

        }


        self.vvid2 = function (employee) {
           
            if ((employee != null)) {
                return true;
            }
            else {
                return false;
            }

        }


       self.Updaterecord2 = function (employee) {

         //  alert(employee.Xid)

           var formData = new FormData();
           formData.append("vid", employee.Xid);

           $.ajax({
               type: "POST",
               url: url3,
               data: formData,

               contentType: false,
               processData: false,
               //Convert the Observable Data into JSON
               dataType: 'json',
               success: function (data) {

               
                   location.reload();
                  
                
               },
               error: function (ee) {

                   alert(ee);
               }
           });
           //var favorite = [];
           //$.each($("input[name='sport']:checked"), function () {
           //    favorite.push($(this).val());
           //});
           //alert("My favourite sports are: " + favorite.join(", "));


       }

       self.Updaterecord5 = function (employee) {

           //  alert(employee.Xid)

           var formData = new FormData();
           formData.append("vid", employee.Xid);

           $.ajax({
               type: "POST",
               url: url6,
               data: formData,

               contentType: false,
               processData: false,
               //Convert the Observable Data into JSON
               dataType: 'json',
               success: function (data) {


                   location.reload();


               },
               error: function (ee) {

                   alert(ee);
               }
           });
           //var favorite = [];
           //$.each($("input[name='sport']:checked"), function () {
           //    favorite.push($(this).val());
           //});
           //alert("My favourite sports are: " + favorite.join(", "));


       }

       self.Updaterecord4 = function (employee) {

           var formData = new FormData();
           formData.append("vid", employee.Xid);
           ajaxindicatorstart('Loading Page.. please wait..');

           $.ajax({
               type: "POST",
               url: url5,
               data: formData,

               contentType: false,
               processData: false,
               //Convert the Observable Data into JSON
               dataType: 'json',
               success: function (data) {
                   data.Certificate = "http://ipo.cldng.com/" + data.Certificate;
                   data.Introduction = "http://ipo.cldng.com/" + data.Introduction;
                   data.logo = "http://ipo.cldng.com/" + data.logo;
                   self.Agents3(data)
                   ajaxindicatorstop();

                 //  location.reload();


               },
               error: function (ee) {
                   ajaxindicatorstop();
                   alert(ee);
               }
           });

       }


       self.Updaterecord3 = function (employee) {

           //  alert(employee.Xid)

           var formData = new FormData();
           formData.append("vid", employee.Xid);

           $.ajax({
               type: "POST",
               url: url4,
               data: formData,

               contentType: false,
               processData: false,
               //Convert the Observable Data into JSON
               dataType: 'json',
               success: function (data) {


                   location.reload();


               },
               error: function (ee) {

                   alert(ee);
               }
           });
           //var favorite = [];
           //$.each($("input[name='sport']:checked"), function () {
           //    favorite.push($(this).val());
           //});
           //alert("My favourite sports are: " + favorite.join(", "));


       }

       var url = 'http://88.150.164.30/CLDMVC/Home/GetAgentStatus';

     

        var url2 = 'http://88.150.164.30/CLDMVC/Home/GetAgent';




        ajaxindicatorstart('Loading Page.. please wait..');


        $.ajax({
            type: "GET",
            url: url,

            contentType: "application/json",
            success: function (data) {
                var dd = data;

              
                self.Agents(data);
                ajaxindicatorstop();
                 

            },
            error: function () {
                alert("Failed");

                ajaxindicatorstop();
            }
        });

     

        self.Updaterec = function (employee) {


        }







    }
    ko.applyBindings(new RoleViewModel());


}
$(document).ready(function () {




    vload();


});




function ajaxindicatorstart(text) {

    if (jQuery('body').find('#resultLoading').attr('id') != 'resultLoading') {

        jQuery('body').append('<div id="resultLoading" style="display:none"><div><img src="../ajax-loader.jpg"><div>' + text + '</div></div><div class="bg"></div></div>');

    }



    jQuery('#resultLoading').css({

        'width': '100%',

        'height': '100%',

        'position': 'fixed',

        'z-index': '10000000',

        'top': '0',

        'left': '0',

        'right': '0',

        'bottom': '0',

        'margin': 'auto'

    });



    jQuery('#resultLoading .bg').css({

        'background': '#000000',

        'opacity': '0.7',

        'width': '100%',

        'height': '100%',

        'position': 'absolute',

        'top': '0'

    });



    jQuery('#resultLoading>div:first').css({

        'width': '250px',

        'height': '75px',

        'text-align': 'center',

        'position': 'fixed',

        'top': '0',

        'left': '0',

        'right': '0',

        'bottom': '0',

        'margin': 'auto',

        'font-size': '16px',

        'z-index': '10',

        'color': '#ffffff'



    });



    jQuery('#resultLoading .bg').height('100%');

    jQuery('#resultLoading').fadeIn(300);

    jQuery('body').css('cursor', 'wait');

}

function ajaxindicatorstop() {

    jQuery('#resultLoading .bg').height('100%');

    jQuery('#resultLoading').fadeOut(300);

    jQuery('body').css('cursor', 'default');

}