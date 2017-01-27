var app = angular.module('myModule', ['smart-table', 'angular-loading-bar', 'ngMessages', '720kb.datepicker']);



app.controller('myController3', ['$scope', '$http', '$rootScope', function ($scope, $http, $rootScope) {
    $scope.checked = false;
    $scope.checked2 = false;
    $scope.newValue = function (value) {
        if ($scope.Searchname == "rtm") {
            $("#sticky1").html('Enter a valid Rtm No as displayed on your Certificate Of Registration.');
            $scope.checked = true;
            $scope.checked2 = false;
        }

        if ($scope.Searchname == "file") {
            $("#sticky1").html('Enter a valid File/Tp No as displayed on your Acknowledgement Form.');
            $scope.checked = false;
            $scope.checked2 = true;

        }

    }

    $scope.add = function () {

        //  alert($scope.OnlineNumber)
        if ($scope.Searchname == null) {

            swal("Pls Select Search Criteria")
            return;
        }

        else {

            //  alert($scope.Searchname)
        }

        var vk = $scope.OnlineNumber;


        var serviceBase = 'http://88.150.164.30/EinaoTestEnvironment.IPO/Handlers/GetCertificate2.ashx';

        if ($scope.Searchname == "rtm") {
            serviceBase = 'http://88.150.164.30/EinaoTestEnvironment.IPO/Handlers/GetCertificate6.ashx';
        }

        else {

          //  serviceBase = 'http://88.150.164.30/EinaoTestEnvironment.IPO/Handlers/GetCertificate7.ashx';
            serviceBase = 'http://localhost:4556/Handlers/GetCertificate7.ashx';
        }

        //  var serviceBase = 'http://localhost:4556/Handlers/GetCertificate2.ashx';


        var Encrypt = {
            vid: vk
        }

        $http({
            method: 'POST',
            url: serviceBase,
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            },
            data: Encrypt,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8;' }
        })
            .success(function (response) {
                var dd = [];

                dd = response;


                //if (dd.length > 0 && dd[0].TransactionId != "") {


                //    swal("Oops...", "This Certificate  Has Been Paid For", "error");
                //    return;
                //}

                if (dd.length > 0) {

                    $scope.itemsByPage = 50;
                    $scope.ListAgent = response;
                    $scope.displayedCollection = [].concat($scope.ListAgent);

                }

                else {

                    swal({
                        title: "Record Not Found",
                        text: "Add New Entry?",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55", confirmButtonText: "ADD!",
                        cancelButtonText: "No!",
                        closeOnConfirm: true,
                        closeOnCancel: true
                    },
function (isConfirm) {
    if (isConfirm) {

        var xname = $("input#xname").val();
        var xaddress = $("input#xaddress").val()
        var xemail = $("input#xemail").val()

        var xPhoneNumber = $("input#xPhoneNumber").val()

        var xpwalletID = $("input#xpwalletID").val()

        var vsys_id = $("input#vsys_id").val()


        doUrlPost("xindex_manual.aspx", "", "0", vsys_id, xname, "", "", "", xaddress, "", xname, xemail, xPhoneNumber, "", "", "", "", "")



    } else {
        swal("Cancelled", "Action Canceled :)", "error");
    }
});

                    $scope.displayedCollection = [];
                    $scope.ListAgent = [];
                }
                //  IpoTradeMarks2(response.Email, response.Firstname, response.CompanyAddress, response.xid, response.PhoneNumber)
                //  ajaxindicatorstop();

            })
            .error(function (response) {
                ajaxindicatorstop();
            });



        //var SponsData = {


        //    email: $scope.Email,
        //    xpass: $scope.Password,
        //    request: 'vlogin'


        //};

    }

    $scope.add2 = function (dd) {

        var xname = $("input#xname").val();
        var xaddress = $("input#xaddress").val()
        var xemail = $("input#xemail").val()

        var xPhoneNumber = $("input#xPhoneNumber").val()

        var xpwalletID = $("input#xpwalletID").val()

        var online_id = dd.id


        IpoTradeMarks7(dd.log_staff)

    }

}]);

app.controller('myController2', ['$scope', '$http', '$rootScope', function ($scope, $http, $rootScope) {





    $scope.$on('$viewContentLoaded', function () {



    });

    $scope.add2 = function (dd) {

        var xname = $("input#xname").val();
        var xaddress = $("input#xaddress").val()
        var xemail = $("input#xemail").val()

        var xPhoneNumber = $("input#xPhoneNumber").val()

        var xpwalletID = $("input#xpwalletID").val()

        var online_id = dd.oai_no

        IpoTradeMarks2(xemail, xname, xaddress, xpwalletID, xPhoneNumber, online_id)

    }


    $scope.add = function () {

        //  alert($scope.OnlineNumber)


        var vk = $scope.OnlineNumber;


        var serviceBase = 'http://88.150.164.30/EinaoTestEnvironment.IPO/Handlers/GetCertificate.ashx';

        // var serviceBase = 'http://localhost:4556/Handlers/GetRegistration.ashx';


        var Encrypt = {
            vid: vk
        }

        $http({
            method: 'POST',
            url: serviceBase,
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            },
            data: Encrypt,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8;' }
        })
            .success(function (response) {
                var dd = [];

                dd = response;



                if (dd.length > 0 && dd[0].TransactionId != "") {


                    swal("Oops...", "This Certificate  Has Been Paid For", "error");
                    return;
                }

                if (dd.length > 0) {

                    $scope.itemsByPage = 50;
                    $scope.ListAgent = response;
                    $scope.displayedCollection = [].concat($scope.ListAgent);

                }

                else {

                    swal("Oops...", "Invalid Online Number!", "error");

                    $scope.displayedCollection = [];
                    $scope.ListAgent = [];
                }
                //  IpoTradeMarks2(response.Email, response.Firstname, response.CompanyAddress, response.xid, response.PhoneNumber)
                //  ajaxindicatorstop();

            })
            .error(function (response) {
                ajaxindicatorstop();
            });



        //var SponsData = {


        //    email: $scope.Email,
        //    xpass: $scope.Password,
        //    request: 'vlogin'


        //};

    }


    //When you have entire dataset



}]);




app.controller('myController4', ['$scope', '$http', '$rootScope', function ($scope, $http, $rootScope) {
    $scope.trademark10 = false;
    $scope.GetStates12 = function () {
        if ($scope.Logo == "WORD MARK") {

            $scope.trademark10 = false;
        }
        else {
            $scope.trademark10 = true;
        }
    }

    $(document).ready(function () {


        var xname = $("input#vagent_code").val();


        var xname2 = $("input#vagent_name").val();

        $scope.rep_code = xname;

        $scope.rep_xname = xname2;
        $scope.country2 = "Nigeria";
        $scope.country3 = "Nigeria";
        var serviceBase = 'http://88.150.164.30/EinaoTestEnvironment.IPO/Handlers/GetRegistration3.ashx';
        var Encrypt = {
            vid: xname
        }

        $http({
            method: 'POST',
            url: serviceBase,
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            },
            data: Encrypt,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8;' }
        })
            .success(function (response) {
                $scope.txt_rep_address = response.CompanyAddress;
                $scope.txt_rep_telephone = response.PhoneNumber;
                $scope.txt_rep_email = response.Email;
                //   IpoTradeMarks2(response.Email, response.Firstname, response.CompanyAddress, response.xid, response.PhoneNumber)
                //   ajaxindicatorstop();

            })
            .error(function (response) {
                //  ajaxindicatorstop();
            });


    });


    $scope.add = function () {

        //  alert($scope.OnlineNumber)

        //alert($scope.Searchname)


        var vk = $scope.OnlineNumber;


        var serviceBase = 'http://88.150.164.30/EinaoTestEnvironment.IPO/Handlers/GetCertificate2.ashx';

        //  var serviceBase = 'http://localhost:4556/Handlers/GetCertificate2.ashx';


        var Encrypt = {
            vid: vk
        }

        $http({
            method: 'POST',
            url: serviceBase,
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            },
            data: Encrypt,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8;' }
        })
            .success(function (response) {
                var dd = [];

                dd = response;


                if (dd.length > 0 && dd[0].TransactionId != "") {


                    swal("Oops...", "This Certificate  Has Been Paid For", "error");
                    return;
                }

                if (dd.length > 0) {

                    $scope.itemsByPage = 50;
                    $scope.ListAgent = response;
                    $scope.displayedCollection = [].concat($scope.ListAgent);

                }

                else {

                    swal({
                        title: "Add New Entry ?",
                        text: "Record Not Found ,Add New Entry",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55", confirmButtonText: "ADD!",
                        cancelButtonText: "No, cancel please!",
                        closeOnConfirm: true,
                        closeOnCancel: true
                    },
function (isConfirm) {
    if (isConfirm) {

        var xname = $("input#xname").val();
        var xaddress = $("input#xaddress").val()
        var xemail = $("input#xemail").val()

        var xPhoneNumber = $("input#xPhoneNumber").val()

        var xpwalletID = $("input#xpwalletID").val()

        var vsys_id = $("input#vsys_id").val()

        //  doUrlPost("xindex_manual.aspx", xtransid, xamt, xagent, xgt, xapplicant_name, xapplicant_email, xapplicant_no, xapplicant_addy, xagent2, xagentname, xagentemail, xagentpnumber, xproduct_title, xitem_code, xpc, xhwalletID, xfee_detailsID)
        doUrlPost("xindex_manual.aspx", "", "0", vsys_id, xname, "", "", "", "", "", "", "", "", "", "", "", "", "")



    } else {
        swal("Cancelled", "Action Canceled :)", "error");
    }
});

                    $scope.displayedCollection = [];
                    $scope.ListAgent = [];
                }
                //  IpoTradeMarks2(response.Email, response.Firstname, response.CompanyAddress, response.xid, response.PhoneNumber)
                //  ajaxindicatorstop();

            })
            .error(function (response) {
                ajaxindicatorstop();
            });



        //var SponsData = {


        //    email: $scope.Email,
        //    xpass: $scope.Password,
        //    request: 'vlogin'


        //};

    }

    $scope.varray = [{ name: 'LOCAL', id: 'LOCAL' }, { name: 'FOREIGN', id: 'FOREIGN' }]

    $scope.vaplication = [{ name: 'T001', id: 'T001' }, { name: 'T003', id: 'T003' }, { name: 'T004', id: 'T004' }]

    $scope.classtrademark = [{ name: 'DEVICE', id: 'DEVICE' }, { name: 'WORD MARK', id: 'WORD MARK' }, { name: 'WORD AND DEVICE', id: 'WORD AND DEVICE' }]
    GetCountries();
    GetClass();

    function GetCountries() {
        $http({
            method: 'GET',
            url: 'http://88.150.164.30/IpoCldng/Handlers/Getcountry.ashx'
        }).success(function (data, status, headers, config) {
            var dd = data;
            $scope.countries = data;
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


    $scope.GetStates2 = function () {
        var countryId = $scope.country;

        if (countryId == 'Nigeria') {
            $scope.Trademark_Type = "LOCAL";

        }

        else {

            $scope.Trademark_Type = "FOREIGN";
        }

    }

    $scope.GetStates = function () {
        var countryId = $scope.country3;
        if (countryId == 'Nigeria') {

            countryId = "160"
        }
        var Encrypt = {
            vid: countryId
        }
        var formData = new FormData();
        formData.append("vid", countryId);
        if (countryId) {

            $http({
                method: 'POST',
                url: 'http://88.150.164.30/IpoCldng/Handlers/GetState.ashx',
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
                data: Encrypt,

                //Convert the Observable Data into JSON

                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8;' }
                //JSON.stringify({ vid: countryId })
            }).success(function (data, status, headers, config) {
                $scope.states = data;
            }).error(function (data, status, headers, config) {
                $scope.message = 'Unexpected Error';
            });
        }
    }


    $scope.submitForm = function (isValid) {
        if (isValid) {
            var AgentsData = {
                Applicant_name: $scope.ng_applicant_name,
                Applicant_Address: $scope.ng_applicant_address,
                Applicant_Email: $scope.ng_applicant_email,
                Applicant_Phone: $scope.ng_applicant_mobile,
                Trading_As: $scope.ng_trading_as,
                Nationality: $scope.country,
                Trademark_Type: $scope.Trademark_Type,
                Title_Of_Trademark: $scope.title_of_trademark,
                Rtm_No: $scope.ng_rtm_no,
                Application_No: $scope.ng_application_no,
                Application_Date: $scope.application_date,
                Trademark_Class: $scope.vclass2,
                Goods_Desc: $scope.txt_goods_desc,
                Logo_Desc: $scope.Logo,
                Txt_Discalimer: $scope.txt_discalimer,
                Agent_Code: $scope.rep_code,
                Rep_Xname: $scope.rep_xname,
                Agent_Nationality: $scope.country2,
                Agent_Rep_Nationality: $scope.country3,
                Agent_State: $scope.state,
                rep_address: $scope.txt_rep_address,
                Rep_telephone: $scope.txt_rep_telephone,
                Rep_email: $scope.txt_rep_email,
                Cert_publicationdate: $scope.txt_cert_publicationdate,
                cert_details: $scope.txt_cert_details,
                amount: '0',
                Application_Type: 'T003'




            };

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
            if (totalFiles2 == 0) {
                alert("Upload File")
                //  self.cac("");

                return;

            }

            else {

                //var ext = $('#cac').val().split('.').pop().toLowerCase();

                //if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) > -1) {
                //    alert('invalid extension!');
                //    return;
                //}

            }

            for (var i = 0; i < totalFiles; i++) {
                var file = document.getElementById("File1").files[i];



                formData.append("FileUpload", file);
            }

            for (var i = 0; i < totalFiles2; i++) {
                var file = document.getElementById("File2").files[i];



                formData.append("FileUpload2", file);
            }

            for (var i = 0; i < totalFiles3; i++) {
                var file = document.getElementById("File3").files[i];



                formData.append("FileUpload3", file);
            }

            for (var i = 0; i < totalFiles4; i++) {
                var file = document.getElementById("File4").files[i];



                formData.append("FileUpload4", file);
            }


            formData.append("vv", JSON.stringify(AgentsData));

            var url9 = "http://88.150.164.30/EinaoTestEnvironment.CLD/Handlers/Save_GenericApplication.ashx";


            $http.post(url9, formData, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
        .success(function (response) {

            //   ajaxindicatorstop();

            swal({
                title: "Record Successfully Added, You will receive an email notification once Record is verified by the Registry",
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
        .error(function () {
            //  ajaxindicatorstop();
            swal("error")
        });


        }


    }


}]);






function IpoTradeMarks2(email, name, address, vid, PhoneNumber, vonlineid) {

    postwith('http://88.150.164.30/EinaoTestEnvironment.Payx/A/m_payx.aspx', {

        //   postwith('http://localhost:21327/A/m_payx.aspx', {

        xname: name,
        agentType: 'Agent',
        address: address,
        email: email,
        PhoneNumber2: PhoneNumber,
        pwalletID: vid,
        onlineid: vonlineid
    });



}


function IpoTradeMarks3(email, name, address, vid, PhoneNumber, vonlineid, name2, address2, email2, PhoneNumber2) {

    postwith('http://88.150.164.30/EinaoTestEnvironment.Payx/A/m_payx.aspx', {

        //postwith('http://localhost:21327/A/m_payx.aspx', {

        xname: name,
        agentType: 'Agent',
        address: address,
        email: email,
        PhoneNumber6: PhoneNumber,
        pwalletID: vid,
        onlineid: vonlineid,
        xname2: name2,
        address2: address2,
        email2: email2,
        PhoneNumber77: PhoneNumber2
    });



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

function IpoTradeMarks7(log_staff) {

    postwith('http://45.40.139.163/EinaoTestEnvironment.CLD/admin/tm/Change_ApplicantAgent.aspx', {

        //postwith('http://localhost:21327/A/m_payx.aspx', {

        transID: log_staff,
        vamount: "0",
        vtranid: "00",
    });



}

function ajaxindicatorstop() {

    jQuery('#resultLoading .bg').height('100%');

    jQuery('#resultLoading').fadeOut(300);

    jQuery('body').css('cursor', 'default');

}



//   alert(aa)



function doUrlPost(x_url, transID, amt, agt, xgt, applicantname, applicantemail, applicantpnumber, applicant_addy, agent, agentname, agentemail, agentpnumber, product_title, item_code, pc, hwalletID, fee_detailsID) {


    postwith(x_url, {
        transID: transID, amt: amt, agt: agt, xgt: xgt, applicantname: applicantname, applicantemail: applicantemail, applicantpnumber: applicantpnumber, applicant_addy: applicant_addy, agent: agent,
        agentname: agentname, agentemail: agentemail, agentpnumber: agentpnumber, product_title: product_title, item_code: item_code, pc: pc, hwalletID: hwalletID, fee_detailsID: fee_detailsID
    });
}
