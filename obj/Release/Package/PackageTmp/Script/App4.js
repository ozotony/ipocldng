var app = angular.module('myModule', [])



// configuring our routes 
// =============================================================================


// our controller for the form
// =============================================================================
app.controller('myController', function ($scope, $rootScope, $http) {

    $scope.add = function () {
        var xname = $("input#xname").val();
        if (xname != null || xname != "") {

            window.open(
      "http://88.150.164.30/EinaoTestEnvironment.Design/admin/pt/Design_Akwnolgment.aspx?x=" + xname,
      '_blank' // <- This is what makes it open in a new window.
    );

            //    window.location.href = "http://88.150.164.30/EinaoTestEnvironment.Design/admin/pt/Design_Akwnolgment.aspx?x=" + xname ;

        }

    }

   

    // alert($location.search().name)



    //  $scope.formData.vname = $location.search().name;









});