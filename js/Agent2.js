function AppAgentModel2() {


    var self = this;
   
   var url2 = 'http://localhost:60693/xindex.aspx';
  //  var url2 = 'http://ds.cldng.com/xindex.aspx';
    var url3 = '/Handlers/GetState.ashx';
    var url = '/Handlers/PortalA.ashx';
    var url4 = '/Handlers/EmailCount.ashx';

    self.transID = ko.observable("");

    self.xvisible = ko.observable(true);

    self.xvisible2 = ko.observable(false);

    self.amt = ko.observable("");

    self.agt = ko.observable("");

    self.xgt = ko.observable("");

    self.applicantname = ko.observable("");

    self.applicantemail = ko.observable("");

    self.applicantpnumber = ko.observable("");

    self.applicant_addy = ko.observable("");

    self.agentname = ko.observable("");

    self.agentemail = ko.observable("");

    self.agentpnumber = ko.observable("");

    self.product_title = ko.observable("");

    self.item_code = ko.observable("");

    self.pc = ko.observable("");



    self.cac = ko.observable("");
    self.loi = ko.observable("");;
    self.passport = ko.observable("");

    


  


   
    //ko.validation.init({
    //    decorateInputElement: true
    //});
    self.save = function () {



        var formData = new FormData();

        var totalFiles = document.getElementById("cac").files.length;
        if (totalFiles == 0) {
          
            self.cac("");

            return;

        }
      

       

        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementById("cac").files[i];



            formData.append("FileUpload", file);
        }

      

        ajaxindicatorstart('Uploading Record.. please wait..');

    

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
                    alert("Record Uploaded Successfully " + data.transID);

                    self.xvisible(false);
                    self.xvisible2(true);

                    self.transID(data.transID); self.amt(data.amt);
                    self.agt(data.agt); self.xgt(data.xgt);
                    self.applicantname(data.applicantname); self.applicantemail(data.applicantemail);
                    self.applicantpnumber(data.applicantpnumber); self.applicant_addy(data.applicant_addy);
                    self.agentname(data.agentname); self.agentemail(data.agentemail);
                    self.agentpnumber(data.agentpnumber); self.product_title(data.product_title);
                    self.item_code(data.item_code); self.pc(data.pc);

                  
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


    self.save2 = function () {

        doUrlPost(url2, self.transID(), self.amt(), self.agt(), self.xgt(), self.applicantname(), self.applicantemail(), self.applicantpnumber(), self.applicant_addy(), self.agt(), self.agentname(), self.agentemail(), self.agentpnumber(), self.product_title(), self.item_code(), self.pc(), '', '');

       


    }

   




}

function doUrlPost(x_url, transID, amt, agt, xgt, applicantname, applicantemail, applicantpnumber, applicant_addy, agent, agentname, agentemail, agentpnumber, product_title, item_code, pc, hwalletID, fee_detailsID) {
    postwith(x_url, {
        transID: transID, amt: amt, agt: agt, xgt: xgt, applicantname: applicantname, applicantemail: applicantemail, applicantpnumber: applicantpnumber, applicant_addy: applicant_addy, agent: agent,
        agentname: agentname, agentemail: agentemail, agentpnumber: agentpnumber, product_title: product_title, item_code: item_code, pc: pc, hwalletID: hwalletID, fee_detailsID: fee_detailsID
    });
}

function postwith(to, p) {
    var myForm = document.createElement("form");
    myForm.method = "post";
    myForm.action = to;
    for (var k in p) {
        var myInput = document.createElement("input");
        myInput.setAttribute("name", k);
        myInput.setAttribute("value", p[k]);
        myForm.appendChild(myInput);
    }
    document.body.appendChild(myForm);
    myForm.submit();
    document.body.removeChild(myForm);
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

function ajaxindicatorstop() {

    jQuery('#resultLoading .bg').height('100%');

    jQuery('#resultLoading').fadeOut(300);

    jQuery('body').css('cursor', 'default');

}
