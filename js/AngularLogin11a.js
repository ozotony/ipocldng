var app = angular.module('myModule', ['smart-table', 'angular-loading-bar', 'ngMessages', '720kb.datepicker', 'ngModal']);

var serviceBaseIpo = 'http://ipo.cldng.com';

var serviceBaseCld = 'http://tm.cldng.com/';

//var serviceBaseCld = 'http://localhost:49703/';


var serviceBasePayx = 'http://88.150.164.30/Payx';
app.factory('dataFactory', ['$http', '$q', function ($http, $q) {

    var urlBase = '/api/customers';
    var dataFactory = {};

    dataFactory.checkMac = function (vemail) {
        var deferred = $q.defer();
        var url7 = serviceBaseCld + '/Handlers/Mark_Count.ashx';
        var vresult = true;
        // var kkk = $('#VEMAIL').val();
        var kkk = vemail;

        var AgentsData = {
            Email: kkk

        }

        var formData = new FormData();

        formData.append("vv", kkk);

        $http.post(url7, formData, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })
        .success(function (response) {

            var dd = parseInt(response);

            if (dd > 0) {



                deferred.resolve(true);


            }
            else {


                deferred.resolve(false);

            }

        })
        .error(function (err, status) {

            deferred.reject(err);

        });

        return deferred.promise;
    };



    return dataFactory;
}]);



app.controller('myController2', ['$scope', '$http', '$rootScope', function ($scope, $http, $rootScope) {





    $scope.$on('$viewContentLoaded', function () {



    });

    $scope.myValue = false;

    $scope.add3 = function (dd, dd2) {
        $scope.payment = [];
        var Encrypt = {
            vid: dd
        }
        var kk = $("#vchk").attr("checked")

        if ($("#vchk").attr("checked")) {

            $http({
                method: 'POST',
                url: serviceBaseCld + 'Handlers/GetBranchCollectPayment2.ashx',
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
                if (response.TransId == null) {
                    swal("Record Not Found Please Complete T003 Payment")


                }

                $scope.payment = response;


            })
            .error(function (response) {


                alert("error " + response)
            });

        }
        else {

            $http({
                method: 'POST',
                url: serviceBaseCld + 'Handlers/GetPayment.ashx',
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

                    try {

                        response = response.split('"').join('');



                        if (response == "Full Transaction  Id Search Not Allowed ") {

                            swal("", "Full Transaction  Id Search Not Allowed", "error")
                            return;
                        }

                    }

                    catch (err) {


                    }

                    if (response.TransId == null) {
                        swal("Record Not Found Please Complete T003 Payment")


                    }

                    $scope.payment = response;


                })
                .error(function (response) {

                    swal("", "Please Enter Correct Payment ID", "error")
                    //  alert("error " + response)
                });

        }


    }

    $rootScope.BranchCollect = false;
    $scope.EditRow = function (dd) {

        $scope.VEmail = "";
        $rootScope.VEmail = "";
        $("input#emailaddress").val("")
        $scope.payment = "";
        $rootScope.oai_no = dd.oai_no;
        $scope.dialogShown = true;

    }

    $scope.EditRow2 = function (dd) {


        var Encrypt = {
            vid: dd.TransId,
            vid2: $rootScope.oai_no
        }

        $http({
            method: 'POST',
            url: serviceBaseCld + 'Handlers/GetCld.ashx',
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
                response = response.split('"').join('');



                if (response == "This Payment  Id Is not For This Transaction") {

                    swal("", "This Payment  Id Is not For This Transaction", "error")
                    return;
                }
                if (response == "Id Already Exist") {

                    swal("", "Id Already Used", "error")
                    return;
                }

                swal(response)
             //   $scope.dialogShown = false;
                //  swal("Record Updated Successfully")
                //  location.reload();

            })
            .error(function (error) {



                //document.open();
                //document.write(error);
                //document.close();
                // alert(  document.writeln(error) )
                swal("", error, "error")


            });

    }

    $scope.add2 = function (dd) {

        //  alert(dd.xid)
        $rootScope.vid = dd.id;
        $rootScope.vid2 = dd.xid;
        $rootScope.sysid = $("input#xname").val();
        $scope.myValue = true;
      //  $scope.detailFrame = "http://45.40.139.163/EinaoTestEnvironment.CLD/admin/tm/rejection_slip_details.aspx?x=" + dd.xid;

    }

    $scope.submitForm = function () {
        var formData = new FormData();

        if ($scope.Vcomment == null) {
            swal("","Comment   Cannot Be Null", "error");

            return

        }


        var Encrypt = {
            vv: $rootScope.vid,
            vv2: $rootScope.sysid,
            vv3: $scope.Vcomment,
            vv4: $rootScope.vid2,

        }
        formData.append("vv", JSON.stringify(Encrypt));


        var totalFiles = document.getElementById("File1").files.length;

        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementById("File1").files[i];



            formData.append("FileUpload", file);
        }

        $http.post(serviceBaseCld + 'Handlers/AppealRejection.ashx', formData, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {

            swal("", "Record Submitted Successfully", "success")
            var kx = JSON.parse(response);
           
            OpenWindowWithPost2(serviceBasecld + +"/admin/tm/AppealRejection4.aspx?x=" + $rootScope.vid2 + "&x2=" + kx, "width=1000, height=600, left=100, top=100, resizable=yes, scrollbars=yes", "NewFile");

            console.log(JSON.parse(response))
            // var content = JSON.parse(response);
           
        }).error(function (data, status, headers, config) {
            $scope.message = 'Unexpected Error';
            console.log(data)
            alert($scope.message);
        });

    }


    $scope.add22 = function (dd) {

        //  alert(dd.xid)
        OpenWindowWithPost2(serviceBasecld+"/admin/tm/rejection_slip_details.aspx?x=" + dd.xid, "width=1000, height=600, left=100, top=100, resizable=yes, scrollbars=yes", "NewFile");
        //  $scope.detailFrame = "http://45.40.139.163/EinaoTestEnvironment.CLD/admin/tm/rejection_slip_details.aspx?x=" + dd.xid;

    }


    $scope.add = function () {

        //  alert($scope.OnlineNumber)

        $scope.myValue = false;
        var vk = $scope.OnlineNumber;


        var serviceBase = serviceBaseIpo + '/Handlers/GetRefusedApplication.ashx';

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

                //if (dd.length > 0 && dd[0].xstat == "New") {


                //    swal("Oops...", "Transaction Exist But Been Verified", "error");
                //    return;
                //}

                if (dd.length > 0 && dd[0].TransactionId != "") {


                    swal("Oops...", "This is not a Refused Application", "error");
                    return;
                }

                if (dd.length > 0) {

                    $scope.itemsByPage = 50;
                    $scope.ListAgent = response;
                    $scope.displayedCollection = [].concat($scope.ListAgent);

                }

                else {

                    swal("Oops...", "This is not a Refused Application!", "error");

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












//function IpoTradeMarks2(email, name, address, vid, PhoneNumber, vonlineid) {

//    postwith('http://88.150.164.30/EinaoTestEnvironment.Payx/A/m_payx.aspx', {

//        //   postwith('http://localhost:21327/A/m_payx.aspx', {

//        xname: name,
//        agentType: 'Agent',
//        address: address,
//        email: email,
//        PhoneNumber2: PhoneNumber,
//        pwalletID: vid,
//        onlineid: vonlineid
//    });



//}

function IpoTradeMarks2(email, name, address, vid, PhoneNumber, vonlineid, name2, address2, email2, PhoneNumber2) {

    postwith(serviceBasePayx+ '/A/m_payx.aspx', {

        //postwith('http://localhost:21327/A/m_payx.aspx', {

        xname: name,
        agentType: 'Agent',
        address: address,
        email: email,
        PhoneNumber2: PhoneNumber,
        pwalletID: vid,
        onlineid: vonlineid,
        xname2: name2,
        address2: address2,
        email2: email2,
        PhoneNumber77: PhoneNumber2
    });



}


//function IpoTradeMarks3(email, name, address, vid, PhoneNumber, vonlineid) {

//    postwith('http://88.150.164.30/EinaoTestEnvironment.Payx/A/m_payx.aspx', {

//        //   postwith('http://localhost:21327/A/m_payx.aspx', {

//        xname: name,
//        agentType: 'Agent',
//        address: address,
//        email: email,
//        PhoneNumber3: PhoneNumber,
//        pwalletID: vid,
//        onlineid: vonlineid
//    });



//}


function IpoTradeMarks4(email, name, address, vid, PhoneNumber, vonlineid, name2, address2, email2, PhoneNumber2) {

    postwith(serviceBasePayx + '/A/m_payx.aspx', {

        //postwith('http://localhost:21327/A/m_payx.aspx', {

        xname: name,
        agentType: 'Agent',
        address: address,
        email: email,
        PhoneNumber11: PhoneNumber,
        pwalletID: vid,
        onlineid: vonlineid,
        xname2: name2,
        address2: address2,
        email2: email2,
        PhoneNumber77: PhoneNumber2
    });



}

function OpenWindowWithPost2(url, windowoption, name) {

    window.open(url, name, windowoption);
}
function IpoTradeMarks3(email, name, address, vid, PhoneNumber, vonlineid, name2, address2, email2, PhoneNumber2) {

    postwith(serviceBasePayx + '/A/m_payx.aspx', {

        //postwith('http://localhost:21327/A/m_payx.aspx', {

        xname: name,
        agentType: 'Agent',
        address: address,
        email: email,
        PhoneNumber3: PhoneNumber,
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
