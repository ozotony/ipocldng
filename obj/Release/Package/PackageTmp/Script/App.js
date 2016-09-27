angular.module('formApp', ['ngAnimate', 'ui.router', 'angular-loading-bar', 'ngSanitize', 'smart-table'])

// configuring our routes 
// =============================================================================
.config(function ($stateProvider, $urlRouterProvider) {

    $stateProvider

        // route to show our basic form (/form)
        .state('form', {
            url: '/form',
            templateUrl: 'form.html',
            controller: 'formController'
        })

        // nested states 
        // each of these sections will have their own view
        // url will be nested (/form/profile)
        .state('form.inbox', {
            url: '/inbox',
            templateUrl: 'Change_Name_Detail.html'
        })

        // url will be /form/interests
        .state('form.detail', {
            url: '/detail',
            templateUrl: 'Change_Name_Detail2.html'
        })

        // url will be /form/payment
       

    // catch all route
    // send users to the form page 
    $urlRouterProvider.otherwise('/form/inbox');
})

// our controller for the form
// =============================================================================
.controller('formController', function ($scope, $state, $rootScope, $http, $stateParams, $location) {

    var serviceBase = 'http://88.150.164.30/EinaoTestEnvironment.IPO/Handlers/GetEmail.ashx';
    $(document).ready(function () {

        var xname = $("input#xname").val();

      //  $scope.formData.vname = xname;


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
                var dd = [];

                dd = response;

                    $scope.itemsByPage = 50;
                    $scope.ListAgent = response;
                    $scope.displayedCollection = [].concat($scope.ListAgent);

              

                //  IpoTradeMarks2(response.Email, response.Firstname, response.CompanyAddress, response.xid, response.PhoneNumber)
                //  ajaxindicatorstop();

            })
            .error(function (response) {
              //  ajaxindicatorstop();
            });


     //   alert(xname)

    });

  // alert($location.search().name)
  
    $scope.Payment_Reference = "";
    // we will store all of our form data in this object
    $scope.formData = {};

  //  $scope.formData.vname = $location.search().name;



    $scope.add2 = function (vrow) {
        var serviceBase2 = 'http://88.150.164.30/EinaoTestEnvironment.IPO/Handlers/GetEmail2.ashx';

        var Encrypt = {
            vid: vrow.id
        }


        $http({
            method: 'POST',
            url: serviceBase2,
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

              $scope.itemsByPage = 10;
              $scope.ListAgent2 = response;
              $scope.displayedCollection2 = [].concat($scope.ListAgent2);



              //  IpoTradeMarks2(response.Email, response.Firstname, response.CompanyAddress, response.xid, response.PhoneNumber)
              //  ajaxindicatorstop();

          })
          .error(function (response) {
           //   ajaxindicatorstop();
          });

       // $rootScope.xid = vrow.xid
        $state.go('form.detail')
    }

    $scope.add3 = function (vrow) {

        window.history.back();
        location.reload();
    }


    $scope.add5 = function (vrow) {

        if (vrow.Status) {

            return false
        }
        else {

            return true;
        }

      
    }


  

 

});



