function vload() {

  
    var RoleViewModel = function () {
        //Make the self as 'this' reference
        var self = this;
        //Declare observable which will be bind with UI 
        self.SuperAdmin = ko.observable(false);
        self.Agent = ko.observable("");
        self.Agent2 = ko.observable(false);
        self.Payment = ko.observable("");
      
     
       var url4 = 'http://88.150.164.30/CLDMVC/Home/AgentAccess';

       // var url4 = 'http://localhost:21936/Home/AgentAccess';

        $('.hidden2').css('display', 'none');

        
        var aa = $("input#hfImage1").val()
        var aa2 = $("input#hfImage2").val()
        $.ajax({
            type: "GET",
            data: { kk2: aa, kk3: aa2 },
            url: url4,
            dataType: "json",
            success: function (resultdata) {


                self.SuperAdmin(resultdata.SuperAdmin);

                self.Agent(resultdata.Agent);

                self.Payment(resultdata.Payment);

                if (self.Agent() == "") {
                   
                   $('.hidden2').css('display', 'inline');
                 
                }

                // self.State(resultdata);
                //  alert("Record Deleted Successfully")


            }
        });

    


    }
    ko.applyBindings(new RoleViewModel());


}
$(document).ready(function () {




    vload();


});