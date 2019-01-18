function vload() {

    function DemoItem(id, name) {
        var self = this;

        self.Role_Xid = ko.observable(id);
        self.Role_Name = ko.observable(name);
        self.Roles_Granted = ko.observable(false);
    }
    var RoleViewModel = function () {
        //Make the self as 'this' reference
        var self = this;
        //Declare observable which will be bind with UI 
        self.Role_ID = ko.observable("");
        self.Agents_Code = ko.observable("");
        self.First_Name = ko.observable("");
        self.Email = ko.observable("");

        self.SurName = ko.observable("");

        

        
        
        self.associatedItemIds = ko.observableArray([]);
       
        self.Roles = ko.observableArray([]);
        self.Agents = ko.observableArray([]);
        self.Agents2 = ko.observableArray([]);
        selectedPeople = ko.observableArray([]);
      
        self.toggleAssociation = function (item) {
            var pp = self.associatedItemIds();
          
          //  alert("associate item " + item.Role_Name);
        //    item.Selected(!(item.Selected()));
            return true;
        };

        self.selectedChoicesDelimited = ko.dependentObservable(function () {


           return self.Roles().join(",");
        });

        var mm = {
            State_Code: self.State_Code,
            State_Name: self.State_Name
        };

        
        self.Delrec = function (employee) {
            var url7 = 'http://5.77.54.44/CLDMVC/Home/DeleteAgent';
            // DeleteAgent(String kk2,String vRole)

            $.ajax({
                type: "GET",
                data: { kk2: employee.Agent_Code, vRole: employee.Email },
                url: url7,
                dataType: "json",
                success: function (resultdata) {


                    self.Agents2(resultdata);

                    // self.State(resultdata);
                    //  alert("Record Deleted Successfully")


                }
            });

           
        }
        self.Updaterec = function (employee) {
            
            self.Agents_Code(employee.Agent_Code);
            self.First_Name(employee.FirstName);
            self.SurName(employee.SurName);

            self.Email(employee.Email);

            var url4 = 'http://5.77.54.44/CLDMVC/Home/GetAgent2';
            $.ajax({
                type: "GET",
                data: { kk2: employee.Agent_Code },
                url: url4,
                dataType: "json",
                success: function (resultdata) {


                    self.Agents2(resultdata);

                   // self.State(resultdata);
                  //  alert("Record Deleted Successfully")


                }
            });


            $('#article-editor').modal('hide');
            //var favorite = [];
            //$.each($("input[name='sport']:checked"), function () {
            //    favorite.push($(this).val());
            //});
            //alert("My favourite sports are: " + favorite.join(", "));


        }

       

        self.save = function () {

            var url3 = 'http://5.77.54.44/CLDMVC/Home/SubmitAgent';

            var formData = new FormData();





                var AgentsData = {


                    Agent_Code: self.Agents_Code,
                    Vroles: self.associatedItemIds



                };

                formData.append("vv", ko.toJSON(AgentsData));
               // ajaxindicatorstart('Submitting Record.. please wait..');

                $.ajax({
                    type: "POST",
                    url: url3,
                    data: formData,

                    contentType: false,
                    processData: false,
                    //Convert the Observable Data into JSON
                    dataType: 'json',
                    success: function (data) {
                      
                      //  alert(data);

                          self.Agents2(data);
                      //  location.reload();
                     //   window.location.hash = "#Sucess"
                        //   self.availablesponsor(data);
                        //self.EmpNo(data.EmpNo);
                        //alert("The New Employee Id :" + self.EmpNo());
                        //GetEmployees();
                    },
                    error: function (ee) {
                       
                        alert(ee);
                    }
                });

           
        }

        var url = 'http://5.77.54.44/CLDMVC/Home/GetRole';

        var url2 = 'http://5.77.54.44/CLDMVC/Home/GetAgent';

       

        
        ajaxindicatorstart('Loading Page.. please wait..');
         

                $.ajax({
                    type: "GET",
                    url: url,
                   
                    contentType: "application/json",
                    success: function (data) {
                        var dd = data;

                        for (var i = 0, len = data.length; i < len; ++i) {
                            self.Roles.push(new DemoItem(data[i].Roles_Granted, data[i].Role_Name));

                        }

                        ajaxindicatorstop();
                      //  self.Roles(data);
                       
                    },
                    error: function () {
                        alert("Failed");

                        ajaxindicatorstop();
                    }
                });
          
                $.ajax({
                    type: "GET",
                    url: url2,

                    contentType: "application/json",
                    success: function (data) {
                        var dd = data;

                       
                          self.Agents(data);

                    },
                    error: function () {
                        alert("Failed");
                    }
                });


        

       



        

    }
    ko.applyBindings(new RoleViewModel());


}
$(document).ready(function () {




    vload();


});

function gothroughtheObservableArray(Addressarray) {
    alert("Got Address Array of length " + Addressarray().length);

    for (var i = 0, len = Addressarray().length; i < len; ++i) {
        var address = Addressarray()[i];
       // alert(address.street());
    }

}

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
