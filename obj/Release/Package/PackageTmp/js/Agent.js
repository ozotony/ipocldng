function AppAgentModel() {


    var self = this;
    var url2 = '/Handlers/Getcountry.ashx';
    var url3 = '/Handlers/GetState.ashx';
    var url = '/Handlers/SaveAgent.ashx';
    var url4 = '/Handlers/EmailCount.ashx';
    





    self.selectedValue = ko.observable("");

    self.Firstname = ko.observable("").extend({ required: { message: 'FirstName Is Required.' } });;

    self.SurName = ko.observable("").extend({ required: { message: 'SurName Is Required.' } });;;
  // self.AccountType = ko.observable("").extend({ required: { message: 'Select Account Type.' } });;;;;
   self.AccountType = ko.observable("");


    self.xid = ko.observable("");

    self.dob = ko.observable("");

    self.CompName = ko.observable("").extend({ required: { message: 'Enter Company Name' } });;

    self.availablecontact = ko.observableArray(['Corporate']);

    self.availablecountry = ko.observableArray([]);

    self.availablestate = ko.observableArray([]);
    self.availablesponsor2 = ko.observableArray([]);

    self.errors = ko.validation.group(self);



    self.CompAddress = ko.observable("");
    self.country = ko.observable("").extend({ required: { message: 'Select Country.' } });;;;

    //  self.country = ko.observable("");

    self.country2 = ko.observable("");

    self.state = ko.observable("").extend({ required: { message: 'Select State.' } });;;;;

    //   self.state = ko.observable("");
    self.email = ko.observable("").extend({ email: true });;

    self.email2 = ko.observable("").extend({ email: true, required: { message: 'Enter Email.' } });;
    self.password = ko.observable("").extend({ required: { message: 'Enter Password' } });;

    self.ConfirmPassword = ko.observable("");

    self.CompPhone = ko.observable("");

    self.CompPerson = ko.observable("");

    self.ContactPhone = ko.observable("");

    self.DobIncorp = ko.observable("");

    self.cac = ko.observable("").extend({ required: { message: 'Select File To Upload.' } });
    self.loi = ko.observable("").extend({ required: { message: 'Select File To Upload.' } });;
    self.passport = ko.observable("").extend({ required: { message: 'Select File To Upload.' } });;


    self.AccountType.subscribe(function (newValue) {

        var propBooleanlValid = ko.validation.group(self.AccountType, { deep: false });
        propBooleanlValid.showAllMessages(false);

        var propBooleanlValid = ko.validation.group(self.country, { deep: false });
        propBooleanlValid.showAllMessages(false);

        var propBooleanlValid = ko.validation.group(self.state, { deep: false });
        propBooleanlValid.showAllMessages(false);


    });


    $("#page_1").hide("slow");
    Sammy(function () {
        this.get('#:page', function () {
            var pageId = this.params.page;

            navigate(pageId);
        });
    }).run('#Home');

    window.location.hash = "#Home"


    $(".target").change(function () {
        var dd = $("#password").val();

        var dd2 = $(this).val();

        if (dd === dd2) {

        }
        else {
            $(this).val("");
            alert("Confirm password must equal password")

        }

    });


    $(".target2").change(function () {
       

        var dd2 = $(this).val();


        var formData = new FormData();

       


        formData.append("vv", dd2);

        //Ajax call to Insert the Employee
        $.ajax({
            type: "POST",
            url: url4,
            data: formData,

            contentType: false,
            processData: false,
            //Convert the Observable Data into JSON
            dataType: 'json',
            success: function (data) {
                var dd2 = data;
              
                if (dd2 != "0") {
                   
                    alert("Email Already Exist");

                    $("#Email").val("");

                    self.email2("");

                }
               
                //self.EmpNo(data.EmpNo);
                //alert("The New Employee Id :" + self.EmpNo());
                //GetEmployees();
            },
            error: function (ee) {
                alert(ee);
            }
        });

       

    });



    self.save = function () {

        var SponsData = {


            Firstname: self.Firstname,
            SurName: self.SurName,
            Title: self.Title,
            dob: self.dob,
            mobile: self.mobile,
            email: self.email,
            sex: self.sex,
            country: self.country().xcode,
            state: self.state().xid,
            Sponsor: self.sponsor().xid


        };

        var formData = new FormData();

        var totalFiles = document.getElementById("pic").files.length;



        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementById("pic").files[i];

            formData.append("FileUpload", file);
        }


        formData.append("vv", ko.toJSON(SponsData));

        //Ajax call to Insert the Employee
        $.ajax({
            type: "POST",
            url: url,
            data: formData,

            contentType: false,
            processData: false,
            //Convert the Observable Data into JSON
            dataType: 'json',
            success: function (data) {
                alert("Record Added Successfully");

                self.availablesponsor(data);
                //self.EmpNo(data.EmpNo);
                //alert("The New Employee Id :" + self.EmpNo());
                //GetEmployees();
            },
            error: function (ee) {
                alert(ee);
            }
        });
        return false;
        //Ends Here
    };
    self.one = ko.observable(false);

    //$('.datepicker').datepicker({
    //    format: 'mm/dd/yyyy'
    //});
    //$("#commentForm").validate();
    $.ajax({
        type: "GET",
        url: url2,

        dataType: 'json',
        success: function (data) {
            //   alert(data)
            //   ko.mapping.fromJS(data,  self.Employees);
            self.availablecountry(data);

            var propBooleanlValid = ko.validation.group(self.AccountType, { deep: false });
            propBooleanlValid.showAllMessages(false);

            var propBooleanlValid = ko.validation.group(self.country, { deep: false });
            propBooleanlValid.showAllMessages(false);

            var propBooleanlValid = ko.validation.group(self.state, { deep: false });
            propBooleanlValid.showAllMessages(false);



            // self.country(data[2])
            //self.EmpNo(data.EmpNo);
            //alert("The New Employee Id :" + self.EmpNo());
            //GetEmployees();
        },
        error: function (ee) {
            alert("Failed");
        }
    });

    //ko.validation.init({
    //    decorateInputElement: true
    //});
    self.save = function () {



        var formData = new FormData();

        var totalFiles = document.getElementById("cac").files.length;
        if (totalFiles == 0) {
            Alert("Upload File")
            self.cac("");

            return;

        }
        var totalFiles2 = document.getElementById("Letter_Intro").files.length;

        if (totalFiles2 == 0) {
            Alert("Upload File")
            self.loi("");

            return;

        }

        var totalFiles3 = document.getElementById("passport").files.length;

        if (totalFiles3 == 0) {
            Alert("Upload File")
            self.passport("");

            return;

        }

        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementById("cac").files[i];

            

            formData.append("FileUpload", file);
        }

        for (var i = 0; i < totalFiles2; i++) {
            var file = document.getElementById("Letter_Intro").files[i];

            formData.append("FileUpload2", file);
        }

        for (var i = 0; i < totalFiles3; i++) {
            var file = document.getElementById("passport").files[i];

            formData.append("FileUpload3", file);
        }



        if (self.errors().length == 0) {

            var AgentsData = {


                AccountType: self.AccountType,
                FirstName: self.Firstname,
                Surname: self.SurName,
                Nationality: self.country().name,
                State: self.state().id,
                dob: self.dob,
                CompName: self.CompName,
                CompAddress: self.CompAddress,
                CompEmail: self.email,
                CompPhone: self.CompPhone,
                CompPerson: self.CompPerson,
                ContactPhone: self.ContactPhone,
                DobIncorp: self.DobIncorp,
                Email: self.email2,

                password: self.password



            };

            formData.append("vv", ko.toJSON(AgentsData));
            ajaxindicatorstart('Submitting Record.. please wait..');

            $.ajax({
                type: "POST",
                url: url,
                data: formData,

                contentType: false,
                processData: false,
                //Convert the Observable Data into JSON
                dataType: 'json',
                success: function (data) {
                    ajaxindicatorstop();
                    alert("Record Added Successfully");
                    window.location.hash = "#Sucess"
                 //   self.availablesponsor(data);
                    //self.EmpNo(data.EmpNo);
                    //alert("The New Employee Id :" + self.EmpNo());
                    //GetEmployees();
                },
                error: function (ee) {
                    ajaxindicatorstop();
                    alert(ee);
                }
            });

        }
        else {

            var result = ko.validation.group(self, { deep: true });
            alert("Please fix all errors before preceding");
            result.showAllMessages(true);
            alert(self.errors())
        }
    }


    self.country.subscribe(function (newValue) {

        var formData = new FormData();
        if (newValue != null) {

            var pp = ko.toJSON(newValue)
            var qq = newValue.code;



            formData.append("vid", newValue.code);
            $.ajax({
                type: "POST",
                url: url3,
                data: formData,
                contentType: false,
                processData: false,
                dataType: 'json',
                success: function (data) {
                    //   alert(data)
                    //   ko.mapping.fromJS(data,  self.Employees);
                    self.availablestate(data);

                    var propBooleanlValid = ko.validation.group(self.AccountType, { deep: false });
                    propBooleanlValid.showAllMessages(false);

                    var propBooleanlValid = ko.validation.group(self.country, { deep: false });
                    propBooleanlValid.showAllMessages(false);

                    var propBooleanlValid = ko.validation.group(self.state, { deep: false });
                    propBooleanlValid.showAllMessages(false);
                    //self.EmpNo(data.EmpNo);
                    //alert("The New Employee Id :" + self.EmpNo());
                    //GetEmployees();
                },
                error: function (ee) {
                    alert("Failed");
                }
            });
        }
    });




}

function navigate(viewId) {

    $(".page").hide();


    if (viewId == "Portfolio") {
        $("#page_1").hide("slow");

    }
    if (viewId == "Home") {
        $("#Home").show("slow");

    }
    else {
        $("#" + viewId).show("slow");
    }
}


function ajaxindicatorstart(text) {

    if (jQuery('body').find('#resultLoading').attr('id') != 'resultLoading') {

        jQuery('body').append('<div id="resultLoading" style="display:none"><div><img src="ajax-loader.jpg"><div>' + text + '</div></div><div class="bg"></div></div>');

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

function ajaxindicatorstop()
   
    {
       
        jQuery('#resultLoading .bg').height('100%');
       
        jQuery('#resultLoading').fadeOut(300);
       
        jQuery('body').css('cursor', 'default');
       
    }
