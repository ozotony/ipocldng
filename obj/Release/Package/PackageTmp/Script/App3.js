angular.module('formApp', ['ngAnimate', 'ui.router', 'angular-loading-bar', 'ngModal', 'smart-table'])

// configuring our routes 
// =============================================================================
.config(function ($stateProvider, $urlRouterProvider) {

    $stateProvider

        // route to show our basic form (/form)
        .state('form', {
            url: '/form',
            templateUrl: 'form2.html',
            controller: 'formController'
        })


         .state('form.search2', {
             url: '/search?MessageID',
             templateUrl: 'Change_Name_Detaila.html'
         })
        // nested states 
        // each of these sections will have their own view
        // url will be nested (/form/profile)
        .state('form.search', {
            url: '/search',
            templateUrl: 'Change_Name_Detaila.html'
        })

        // url will be /form/interests
        .state('form.detail', {
            url: '/detail',
            templateUrl: 'Change_Name_Detail2a.html'
        })

    // url will be /form/payment


    // catch all route
    // send users to the form page 
    $urlRouterProvider.otherwise('/form/search');
})

// our controller for the form
// =============================================================================
.controller('formController', function ($scope, $state, $rootScope, $http, $stateParams, $location) {
    var xname = "";
    $rootScope.Recordal_Id = "";
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
    //$scope.formData.vname = $location.search().name;
    $scope.checked = false;
    $scope.checked2 = false;
    $scope.Payment_Reference = "";
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

            var kk = $state.params.MessageID;
            $scope.formData.OnlineNumber = $state.params.MessageID
         

          
          
           // alert($state.params.MessageID)
         //  $scope.formData.OnlineNumber2 = $state.params.MessageID

        }
    }
    $scope.newValue2 = function (value) {
        var kl = "";
        if ($scope.formData.Searchname == "rtm") {
            $("#sticky1").html('Enter a valid Rtm No as displayed on your Certificate Of Registration.');
            $scope.checked = true;
            $scope.checked2 = false;
        }

        if ($scope.formData.Searchname == "file") {
            $("#sticky1").html('Enter a valid File/Tp No as displayed on your Acknowledgement Form.');
            $scope.checked = false;
            $scope.checked2 = true;

        }

    }
    // alert($location.search().name)

    //$scope.Payment_Reference = "";
   
    //$scope.formData = {};

    //  $scope.formData.vname = $location.search().name;

    $scope.add = function () {
      //  alert($scope.formData.Searchname + "=" + $scope.formData.OnlineNumber)
        var serviceBase = "http://88.150.164.30/MigrateGETest/";
        if ($scope.formData.Searchname == "rtm") {
            serviceBase = 'http://88.150.164.30/MigrateGETest/api/values/GetData2';
         // serviceBase = 'http://localhost:40768/api/values/GetData2';
        }

        else {
           serviceBase = 'http://88.150.164.30/MigrateGETest/api/values/GetData';
         //  serviceBase = 'http://localhost:40768/api/values/GetData';


        }

       
        var data = {
            property1: $scope.formData.OnlineNumber,
            property2: xname
        };


        $http.get(serviceBase , { params: data }, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            //  alert("tony response ="+response);
            //  localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName });
            var kk = response;


            if (response != "null") {



                if (response.pwallet.validationID != "") {
                    $state.go('form.detail')

                }



                $scope.formData.ng_applicant_name = response.applicant.xname;
                $scope.formData.ng_applicant_address = response.Applicant_Address.street;

                $scope.formData.applicant_email = response.Applicant_Address.email1;
                $scope.formData.applicant_mobile = response.Applicant_Address.telephone1;

                $scope.formData.country = response.Applicant_Address.countryID;

                $scope.formData.Trademark_Type = response.mark_info.tm_typeID;
                $scope.formData.title_of_trademark = response.mark_info.product_title;
                $scope.formData.rtm_no = response.pwallet.rtm;
                $scope.formData.application_no = response.mark_info.reg_number;
                $scope.formData.application_date = response.applicant.reg_date;
                $scope.formData.vclass2 = response.mark_info.nice_class;
                $scope.formData.Logo = response.mark_info.logo_descriptionID;

                $rootScope.markid = response.mark_info.xID;

                $scope.formData.rep_code = response.representative.agent_code;

                $scope.formData.rep_xname = response.representative.xname;

                $scope.formData.txt_rep_address = response.Representative_Address.street;
                $scope.formData.txt_rep_telephone = response.Representative_Address.telephone1;
                $scope.formData.txt_rep_email = response.Representative_Address.email1;
                if (response.mark_info.logo_pic != "") {

                    $scope.formData.logo_pic = "http://88.150.164.30/EinaoTestEnvironment.CLD/admin/tm/" + response.mark_info.logo_pic;
                    //  $scope.formData.logo_pic = "http://localhost:4556/admin/tm/" + response.mark_info.logo_pic;
                    $scope.formData.show = true;
                }

                else {

                    $scope.formData.show = false;
                }

                if (response.mark_info.auth_doc != "") {

                    $scope.formData.auth_doc = "http://88.150.164.30/EinaoTestEnvironment.CLD/admin/tm/" + response.mark_info.auth_doc;
                    //  $scope.formData.auth_doc = "http://localhost:4556/admin/tm/" + response.mark_info.auth_doc;
                    $scope.formData.show2 = true;
                }

                else {

                    $scope.formData.show2 = false;
                }


                if (response.mark_info.sup_doc2 != "") {

                    $scope.formData.sup_doc2 = "http://88.150.164.30/EinaoTestEnvironment.CLD/admin/tm/" + response.mark_info.sup_doc2;

                    // $scope.formData.sup_doc2 = "http://localhost:4556/admin/tm/" + response.mark_info.sup_doc2;
                    $scope.formData.show3 = true;
                }

                else {

                    $scope.formData.show3 = false;
                }


               var  serviceBase2 = 'http://88.150.164.30/EinaoTestEnvironment.CLD/Handlers/GetRecordal.ashx';

                var Encrypt = {
                    vid: response.mark_info.log_staff


                }

                $http({
                    method: 'POST',
                    // url: 'http://88.150.164.30/CLD/Handlers/GetData.ashx'
                    url: serviceBase2,
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

                    $scope.itemsByPage = 50;
                    $scope.ListAgent = response;
                    $scope.displayedCollection = [].concat($scope.ListAgent);

                    $scope.dialogShown = true;


                }).error(function (data, status, headers, config) {

                    $scope.message = 'Unexpected Error';
                });



            }

            else {

                swal("", "Record Not Found", 'error');
            }
         //   var kk2 = JSON.parse(kk);
           
        }).error(function (err, status) {

            var dd = err;
          //  swal("Cancelled", err.Message, "error");

        });



    }

    $scope.EditRow2 = function (dd) {

        $rootScope.Recordal_Id = dd.ID;
     //   swal("", $rootScope.Recordal_Id, 'success');
        $scope.dialogShown = false;

       
    }

    $scope.submitForm = function (isValid) {

        //var Encrypt = {
        //    vid: $rootScope.Recordal_Id


        //}

        if ($rootScope.Recordal_Id == "") {

            swal("", "You Must Select Recordal Type To Continue", 'error');
            return;
        }
       
            $scope.processing = true;
           

            var formData = new FormData();

           // formData.append("vid", $rootScope.Recordal_Id);

            formData.append("vid", $rootScope.Recordal_Id);

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
           

            //else {

            //    //var ext = $('#cac').val().split('.').pop().toLowerCase();

            //    //if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) > -1) {
            //    //    alert('invalid extension!');
            //    //    return;
            //    //}

            //}

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

            formData.append("vv", JSON.stringify($rootScope.markid));

            var url9 = "http://88.150.164.30/EinaoTestEnvironment.CLD/Handlers/Save_GenericApplication3.ashx";

         //var url9 = "http://localhost:49703/Handlers/Save_GenericApplication3.ashx";

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
            .error(function (e) {
                //  ajaxindicatorstop();
                var dd = e;
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


