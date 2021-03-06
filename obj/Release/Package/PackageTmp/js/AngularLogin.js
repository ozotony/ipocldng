﻿var app = angular.module('myModule', ['ngModal']);
app.filter('offset', function () {
    return function (input, start) {
        start = parseInt(start, 10);
        return input.slice(start);
    };
});


app.controller('myController', ['$scope', '$http', '$rootScope', function ($scope, $http, $rootScope) {

    $scope.BranchCollect = [];
    $scope.itemsPerPage = 10;
    $scope.currentPage = 0;
    $scope.items = [];
    var url3 = 'http://ipo.cldng.com/Handlers/GetRegistration4.ashx';

    // var url3 = ' http://localhost:21936/home/GetAgent';

    $scope.EditRow = function (user) {
      //  alert(user.DateOfBrith)
        $rootScope.user = user;
        $scope.VEmail = "";
        $scope.dialogShown = true;

    }

    $scope.block = function (user) {
        //  alert(user.DateOfBrith)
       
        swal({
            title: "Are you sure?",
            text: "This Action Will Block Agent",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55", confirmButtonText: "Yes, Block Agent!",
            cancelButtonText: "No, cancel plx!",
            closeOnConfirm: true,
            closeOnCancel: true
        },
 function (isConfirm) {
     if (isConfirm) {
         var url3 = "http://ipo.cldng.com/Handlers/BlockAgent.ashx";
         var formData = new FormData();
         formData.append("vid", user.xid);

         $.ajax({
             type: "POST",
             url: url3,
             data: formData,

             contentType: false,
             processData: false,
             //Convert the Observable Data into JSON
             dataType: 'json',
             success: function (data) {


                 swal("User Blocked Successfully", "success");


             },
             error: function (ee) {

                 swal("Error Occured", "error");
             }
         });


     } else {
         swal("Cancelled", "Action Canceled :)", "error");
     }
 });

    }

    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
    };

    $scope.prevPageDisabled = function () {
        return $scope.currentPage === 0 ? "disabled" : "";
    };

    $scope.pageCount = function () {
        return Math.ceil($scope.BranchCollect.length / $scope.itemsPerPage) - 1;
    };

    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pageCount()) {
            $scope.currentPage++;
        }
    };

    $scope.nextPageDisabled = function () {
        return $scope.currentPage === $scope.pageCount() ? "disabled" : "";
    };

   

    $scope.$on('$viewContentLoaded', function () {

       
     
    });
    ajaxindicatorstart('Loading Page.. please wait..');
    $.ajax({
        type: "POST",
        url: url3,

        contentType: false,
        processData: false,
        //Convert the Observable Data into JSON
        dataType: 'json',
        success: function (data) {
            //self.paymentData(data);
            $scope.BranchCollect = data;
            $scope.pageCount2 = Math.ceil($scope.BranchCollect.length / $scope.itemsPerPage) - 1;
            $scope.$apply();
            ajaxindicatorstop();

        },
        error: function (ee) {

            ajaxindicatorstop();
            //ajaxindicatorstop();
            //alert(ee);
        }
    });

 
    //When you have entire dataset
   


}]);

app.controller('myController2', ['$scope', '$http', '$rootScope', function ($scope, $http, $rootScope) {





    $scope.$on('$viewContentLoaded', function () {



    });
  



    $scope.add = function () {
       
        alert($scope.OnlineNumber)


        //var SponsData = {


        //    email: $scope.Email,
        //    xpass: $scope.Password,
        //    request: 'vlogin'


        //};

    }
    //When you have entire dataset



}]);









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
