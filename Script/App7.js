var app = angular.module('formApp2', ['ngAnimate', 'ui.router', 'angular-loading-bar']);
var serviceBaseIpo = 'http://88.150.164.30/EinaoTestEnvironment.IPO/';

var serviceBaseDs = 'http://88.150.164.30/EinaoTestEnvironment.Design/'
// configuring our routes 
// =============================================================================
app.config(function ($stateProvider, $urlRouterProvider) {

    $stateProvider

        // route to show our basic form (/form)
        .state('form', {
            url: '/form',
            templateUrl: 'form4.html',
            controller: 'formController'
        })

        // nested states 
        // each of these sections will have their own view
        // url will be nested (/form/profile)
        .state('form.search2', {
            url: '/search?MessageID',
            templateUrl: 'Change_Name_Detailb2.html'
        })
        .state('form.search', {
            url: '/search',
            templateUrl: 'Change_Name_Detailb22.html'
        })

        // url will be /form/interests
        .state('form.detail', {
            url: '/detail',
            templateUrl: 'Change_Name_Detail2b.html'
        })

    // url will be /form/payment


    // catch all route
    // send users to the form page 
    $urlRouterProvider.otherwise('/form/search');
});

// our controller for the form
// =============================================================================
app.controller('formController', function ($scope, $state, $rootScope, $http, $stateParams, $location) {
    var xname = "";
    $(document).ready(function () {
        xname = $("input#xname").val();

    });

    GetCountries();
    GetClass();

    GetAgent();

    function GetAgent() {
        $http({
            method: 'GET',
            url: 'http://88.150.164.30/EinaoTestEnvironment.IPO/Handlers/GetRegistration2.ashx'

            // url: 'http://localhost:4556/Handlers/GetRegistration2.ashx'
        }).success(function (data, status, headers, config) {
            var dd = data;
            $scope.Agent = data;

        }).error(function (data, status, headers, config) {
            $scope.message = 'Unexpected Error';
        });
    }
    $scope.varray = [{ name: 'LOCAL', id: '1' }, { name: 'FOREIGN', id: '2' }]

    $scope.vaplication = [{ name: 'T001', id: 'T001' }, { name: 'T003', id: 'T003' }, { name: 'T004', id: 'T004' }]

    $scope.classtrademark = [{ name: 'DEVICE', id: '1' }, { name: 'WORD MARK', id: '2' }, { name: 'WORD AND DEVICE', id: '3' }]

    function GetCountries() {
        $http({
            method: 'GET',
            url: 'http://88.150.164.30/EinaoTestEnvironment.IPO/Handlers/Getcountry.ashx'
        }).success(function (data, status, headers, config) {
            var dd = data;
            $scope.countries = data;
            $scope.country2 = "160";
            $scope.country3 = "160";
        }).error(function (data, status, headers, config) {
            $scope.message = 'Unexpected Error';
        });
    }

    function GetClass() {
        $http({
            method: 'GET',
            url: 'http://88.150.164.30/EinaoTestEnvironment.IPO/Handlers/GetCldClass.ashx'
        }).success(function (data, status, headers, config) {
            var dd = data;
            $scope.vclass = data;
        }).error(function (data, status, headers, config) {
            $scope.message = 'Unexpected Error';
        });
    }

    var serviceBase = 'http://88.150.164.30/EinaoTestEnvironment.IPO/Handlers/GetEmail.ashx';
    $(document).ready(function () {




        //   alert(xname)

    });
    $scope.formData = {};
    $scope.checked = false;
    $scope.checked2 = false;

    $scope.Payment_Reference = "";
    // we will store all of our form data in this object
    $scope.formData = {};
    if ($state.params.MessageID != null) {
        $scope.formData.Searchname = "file"
        if ($scope.formData.Searchname == "rtm") {
            $("#sticky1").html('Enter a valid Rtm No as displayed on your Certificate Of Registration.');
            $scope.checked = true;
            $scope.checked2 = false;
        }

        if ($scope.formData.Searchname == "file") {
            $("#sticky1").html('Enter a valid Registration No as displayed on your Acknowledgement Form.');
            $scope.checked = false;
            $scope.checked2 = true;
            //  $('#OnlineNumber2').val("233444");
            $scope.formData.OnlineNumber = $state.params.MessageID

        }
    }

    //$scope.formData.vname = $location.search().name;

    $scope.newValue2 = function (value) {
        var kl = "";
        if ($scope.formData.Searchname == "rtm") {
            $("#sticky1").html('Enter a valid Rtm No as displayed on your Certificate Of Registration.');
            $scope.checked = true;
            $scope.checked2 = false;
        }

        if ($scope.formData.Searchname == "file") {
            $("#sticky1").html('Enter a valid Registration No as displayed on your Acknowledgement Form.');
            $scope.checked = false;
            $scope.checked2 = true;

        }

    }
    // alert($location.search().name)


    //  $scope.formData.vname = $location.search().name;

    $scope.add = function () {
        //  alert($scope.formData.Searchname + "=" + $scope.formData.OnlineNumber)
        var serviceBase = "http://88.150.164.30/MigrateGETest/";
        if ($scope.formData.Searchname == "rtm") {
            serviceBase = 'http://88.150.164.30/EinaoTestEnvironment.Patent/Handlers/GetData4a.ashx';
            //  serviceBase = 'http://localhost:40768/api/values/GetData2';
        }

        else {
            serviceBase = 'http://88.150.164.30/EinaoTestEnvironment.Patent/Handlers/GetData5a.ashx';
            // serviceBase = 'http://localhost:55482/Handlers/GetData4.ashx';


        }


        var Encrypt = {
            vid: $scope.formData.OnlineNumber,

            vid2: xname
        }


        $http({
            method: 'POST',
            // url: 'http://88.150.164.30/CLD/Handlers/GetData.ashx'
            url: serviceBase,
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            },
            data: Encrypt,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8;' }
        }).success(function (response) {
            var dd = response;
            if (response != "null") {


                $scope.formData.reg_number = response.reg_number;
                $scope.formData.title_of_invention = response.title_of_invention;
                $scope.formData.xtype = response.xtype;
                if (response.loa_doc != "") {
                    $scope.formData.logo_pic = "http://88.150.164.30/EinaoTestEnvironment.Patent/admin/pt/" + response.loa_doc;
                    $scope.formData.show = true;

                }
                if (response.ns_doc != "") {
                    $scope.formData.ns_doc = "http://88.150.164.30/EinaoTestEnvironment.Patent/admin/pt/" + response.ns_doc;
                    $scope.formData.show2 = true;
                }
                if (response.pd_doc != "") {
                    $scope.formData.pd_doc = "http://88.150.164.30/EinaoTestEnvironment.Patent/admin/pt/" + response.pd_doc;
                    $scope.formData.show3 = true;
                }
                if (response.doa_doc != "") {
                    $scope.formData.sup_doc3 = "http://88.150.164.30/EinaoTestEnvironment.Patent/admin/pt/" + response.doa_doc;
                    $scope.formData.show4 = true;
                }
                if (response.rep_pic != "") {
                    $scope.formData.rep_pic = "http://88.150.164.30/EinaoTestEnvironment.Patent/admin/pt/" + response.rep_pic;
                    $scope.formData.show5 = true;
                }

                if (response.rep2_pic != "") {
                    $scope.formData.rep2_pic = "http://88.150.164.30/EinaoTestEnvironment.Patent/admin/pt/" + response.rep2_pic;
                    $scope.formData.show6 = true;
                }

                if (response.rep3_pic != "") {
                    $scope.formData.rep3_pic = "http://88.150.164.30/EinaoTestEnvironment.Patent/admin/pt/" + response.rep3_pic;
                    $scope.formData.show7 = true;
                }


                if (response.rep4_pic != "") {
                    $scope.formData.rep4_pic = "http://88.150.164.30/EinaoTestEnvironment.Patent/admin/pt/" + response.rep4_pic;
                    $scope.formData.show8 = true;
                }

                if (response.reg_number != "") {
                    $state.go('form.detail')

                }

            }

            else {

                swal("", "Record Not Found", 'error');
            }

        }).error(function (data, status, headers, config) {
            alert("error " + data)
            $scope.message = 'Unexpected Error';
        });



    }



    $scope.submitForm = function (isValid) {

        $scope.processing = true;


        var formData = new FormData();

        var totalFiles;
        try {
            totalFiles = document.getElementById("File1").files.length;
        }
        catch (Ex) {

        }
        var totalFiles2;
        try {
            totalFiles2 = document.getElementById("File2").files.length;
        }
        catch (Ex) {

        }
        var totalFiles3;
        try {
            totalFiles3 = document.getElementById("File3").files.length;
        }
        catch (Ex) {

        }
        var totalFiles4;
        try {
            totalFiles4 = document.getElementById("File4").files.length;
        }


        catch (Ex) {

        }

        var totalFiles5;
        try {
            totalFiles5 = document.getElementById("File5").files.length;
        }


        catch (Ex) {

        }


        var totalFiles6;
        try {
            totalFiles6 = document.getElementById("File6").files.length;
        }


        catch (Ex) {

        }

        var totalFiles7;
        try {
            totalFiles7 = document.getElementById("File7").files.length;
        }


        catch (Ex) {

        }

        var totalFiles8;
        try {
            totalFiles8 = document.getElementById("File8").files.length;
        }


        catch (Ex) {

        }


        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementById("File1").files[i];



            formData.append("FileUpload", file);
        }

        if (totalFiles == 0) {
            //if (formData.logo_pic != null) {

            //    var blob = dataURLtoBlob(formData.logo_pic);
            //}

            //formData.append("FileUpload", file);


        }

        for (var i = 0; i < totalFiles2; i++) {
            var file = document.getElementById("File2").files[i];



            formData.append("FileUpload2", file);
        }

        if (totalFiles2 == 0) {
            //if (formData.auth_doc != null) {

            //    var blob = dataURLtoBlob(formData.auth_doc);
            //}

            //formData.append("FileUpload", file);


        }
        for (var i = 0; i < totalFiles3; i++) {
            var file = document.getElementById("File3").files[i];



            formData.append("FileUpload3", file);
        }



        for (var i = 0; i < totalFiles4; i++) {
            var file = document.getElementById("File4").files[i];



            formData.append("FileUpload4", file);
        }

        if (totalFiles4 == 0) {
            //if (formData.sup_doc2 != null) {

            //    var blob = dataURLtoBlob(formData.sup_doc2);
            //}

            //formData.append("FileUpload", file);


        }


        for (var i = 0; i < totalFiles5; i++) {
            var file = document.getElementById("File5").files[i];



            formData.append("FileUpload5", file);
        }

        for (var i = 0; i < totalFiles6; i++) {
            var file = document.getElementById("File6").files[i];



            formData.append("FileUpload6", file);
        }

        for (var i = 0; i < totalFiles7; i++) {
            var file = document.getElementById("File7").files[i];



            formData.append("FileUpload7", file);
        }

        for (var i = 0; i < totalFiles8; i++) {
            var file = document.getElementById("File8").files[i];



            formData.append("FileUpload8", file);
        }

        //  formData.append("vv", JSON.stringify($scope.formData.OnlineNumber));

        formData.append("vv", $scope.formData.OnlineNumber);

        // var url9 = "http://88.150.164.30/EinaoTestEnvironment.Patent/Handlers/Save_GenericApplication3.ashx";
        var url9 = "";
        if ($scope.formData.Searchname == "rtm") {

            url9 = "http://88.150.164.30/EinaoTestEnvironment.Patent/Handlers/Save_GenericApplication3.ashx";
        }

        else {

            url9 = "http://88.150.164.30/EinaoTestEnvironment.Patent/Handlers/Save_GenericApplication4.ashx";
        }

        $http.post(url9, formData, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })
        .success(function (response) {

            //   ajaxindicatorstop();

            swal({
                title: "Record Updated Successfully",
                text: "",
                type: "success",
                showCancelButton: false,
                confirmButtonColor: "#DD6B55", confirmButtonText: "OK!",
                cancelButtonText: "No, cancel please!",
                closeOnConfirm: true,
                closeOnCancel: true
            },
function (isConfirm) {
    if (isConfirm) {

        window.location.assign("profile.aspx");


    }

});

            //swal("Record Successfully Added, You will receive an email notification once Record is verified by the Registry");

            //window.location.assign("profile.aspx");


        })
        .error(function (d) {
            var pp = d;
            //  ajaxindicatorstop();
            swal("error")
        });

    }








});

function dataURLtoBlob(dataURI) {
    // convert base64/URLEncoded data component to raw binary data held in a string
    var byteString;
    if (dataURI.split(',')[0].indexOf('base64') >= 0)
        byteString = atob(dataURI.split(',')[1]);
    else
        byteString = unescape(dataURI.split(',')[1]);

    // separate out the mime component
    var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];

    // write the bytes of the string to a typed array
    var ia = new Uint8Array(byteString.length);
    for (var i = 0; i < byteString.length; i++) {
        ia[i] = byteString.charCodeAt(i);
    }

    return new Blob([ia], { type: mimeString });
}


