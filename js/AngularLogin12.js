var app = angular.module('myModule', ['smart-table', 'angular-loading-bar', 'ngMessages', '720kb.datepicker']);

var serviceBaseIpo = 'http://5.77.54.44/EinaoTestEnvironment.IPO';

//var serviceBaseIpo = 'http://localhost:4556';


var serviceBaseCld = 'http://5.77.54.44/EinaoTestEnvironment.CLD';

var serviceBasePayx = 'http://5.77.54.44/EinaoTestEnvironment.Payx';
//var serviceBasePayx = 'http://localhost:21327';


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

app.controller('myController3', ['$scope', '$http', '$rootScope', function ($scope, $http, $rootScope) {
    $scope.checked = false;
    $scope.checked2 = false;
    $scope.newValue = function (value) {
        if ($scope.Searchname == "rtm") {
            $("#sticky1").html('Enter a valid Transaction  No .');
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


        var serviceBase = serviceBaseIpo + '/Handlers/GetCertificate2.ashx';

        if ($scope.Searchname == "rtm") {
            serviceBase = serviceBaseIpo + '/Handlers/GetCertificate8.ashx';
        }

        else {

            serviceBase = serviceBaseIpo + '/Handlers/GetCertificate9.ashx';
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
                console.log("response2")
                console.log(response)

                //if (dd.length > 0 && dd[0].xstat == "New") {


                //    swal("", "This Record has been captured and is awaiting Verification", "warning");
                //    return;
                //}


               
                if (dd) {



                    $scope.itemsByPage = 50;
                    $scope.ListAgent = response;
                    console.log("Record Found")
                    console.log(response)
                    $scope.displayedCollection = [].concat($scope.ListAgent);

                }

                else {

                 alert("Record Not Found")

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

        var online_id = dd.kk.validationID

        //  IpoTradeMarks3(xemail, xname, xaddress, xpwalletID, xPhoneNumber, online_id)

        IpoTradeMarks3(xemail, xname, xaddress, xpwalletID, xPhoneNumber, online_id, dd.kk3.xname, dd.kk3.address, dd.kk3.xemail, dd.kk3.xmobile)


    }


    $scope.add4 = function (dd) {

        var xname = $("input#xname").val();
        var xaddress = $("input#xaddress").val()
        var xemail = $("input#xemail").val()

        var xPhoneNumber = $("input#xPhoneNumber").val()

        var xpwalletID = $("input#xpwalletID").val()

        var online_id = dd.oai_no

        IpoTradeMarks2(xemail, xname, xaddress, xpwalletID, xPhoneNumber, online_id)

    }

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

    postwith(serviceBasePayx + '/A/m_payx.aspx', {

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

function IpoTradeMarks3(email, name, address, vid, PhoneNumber, vonlineid, name2, address2, email2, PhoneNumber2) {

    postwith(serviceBasePayx + '/A/m_payx.aspx', {

        //postwith('http://localhost:21327/A/m_payx.aspx', {

        xname: name,
        agentType: 'Agent',
        address: address,
        email: email,
        PhoneNumber14: PhoneNumber,
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
