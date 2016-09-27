<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xindex_manual2.aspx.cs" Inherits="Ipong.A.xindex_manual2" %>

<!DOCTYPE html>

<html  data-ng-app="myModule" >

<head id="Head1" runat="server">

    <title>
TRADEMARK APPLICATION NOTICE
</title>
  <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" href="../css/jquery.ui.all.css" type="text/css"/>
<link rel="stylesheet" href="../css/jquery.ui.theme.css" type="text/css"/>
    <script src="../js/jquery-2.1.1.js"></script>


<script src="../js/funk.js" type="text/javascript"></script>

  

<script src="../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
<script src="../js/jquery.js" type="text/javascript"></script>
<script src="../ui/jquery.cookie.js" type="text/javascript"></script>
<script src="../ui/jquery.ui.core.js" type="text/javascript"></script>
<script src="../ui/jquery.ui.widget.js" type="text/javascript"></script>
  
<script src="../ui/jquery.ui.datepicker.js" type="text/javascript"></script>

 <script src="../js/angular.min.js"></script>

    <script src="../js/AngularLogin3.js"></script>

    <script src="../js/smart-table.min.js"></script>
    <script src="../js/loading-bar.js"></script>
    <link href="../css/loading-bar.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/sweet-alert.css" rel="stylesheet" />
    <script src="../js/sweet-alert.min.js"></script>

    <script src="../js/angular-messages.min.js"></script>
    <script src="../js/angular-datepicker.min.js"></script>
    <link href="../css/angular-datepicker.min.css" rel="stylesheet" />
<script language="javascript" type="text/javascript">

    $(function () {

        //$("#txt_application_date").datepicker({
        //    changeMonth: true,
        //    changeYear: true,
        //    showButtonPanel: true,
        //    dateFormat: 'yy-mm-dd',
        //    yearRange: 'c-100:c+0'
        //});
        //$("#txt_merger_date").datepicker({
        //    changeMonth: true,
        //    changeYear: true,
        //    showButtonPanel: true,
        //    dateFormat: 'yy-mm-dd',
        //    yearRange: 'c-100:c+0'
        //});
        //$("#txt_cert_publicationdate").datepicker({
        //    changeMonth: true,
        //    changeYear: true,
        //    showButtonPanel: true,
        //    dateFormat: 'yy-mm-dd',
        //    yearRange: 'c-100:c+0'
        //});
        //$("#txt_assignment_date").datepicker({
        //    changeMonth: true,
        //    changeYear: true,
        //    showButtonPanel: true,
        //    dateFormat: 'yy-mm-dd',
        //    yearRange: 'c-100:c+0'
        //});
        //$("#txt_renewal_date").datepicker({
        //    changeMonth: true,
        //    changeYear: true,
        //    showButtonPanel: true,
        //    dateFormat: 'yy-mm-dd',
        //    yearRange: 'c-100:c+0'
        //});

    });
</script>
 
  <script language="javascript" type="text/javascript">
      // <![CDATA[
      function Proceed_onclick() {
          window.location.href = "./g_napplication.aspx";
      }

      // ]]>
    </script>

    <style type="text/css">

 p.MsoNormal
	{margin-top:0in;
	margin-right:0in;
	margin-bottom:10.0pt;
	margin-left:0in;
	line-height:115%;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	}
p.MsoListParagraphCxSpFirst
	{margin-top:0in;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:.5in;
	margin-bottom:.0001pt;
	line-height:115%;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	}
p.MsoListParagraphCxSpMiddle
	{margin-top:0in;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:.5in;
	margin-bottom:.0001pt;
	line-height:115%;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	}
p.MsoListParagraphCxSpLast
	{margin-top:0in;
	margin-right:0in;
	margin-bottom:10.0pt;
	margin-left:.5in;
	line-height:115%;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	}
a:link
	{color:blue;
	text-decoration:underline;
        }
        .style1
        {
            color: black;
        }
    </style>

    <style>


    .has-error .help-block, .has-error .control-label, .has-error .radio, .has-error .checkbox, .has-error .radio-inline, .has-error .checkbox-inline {
        color: #a94442;
    }

    .help-block {
        display: block;
        margin-top: 5px;
        margin-bottom: 10px;
        color: red;
    }
   span.ng-scope {
        color:red;
    }
    /*input.ng-invalid {
  border: 1px solid red;
 
}
input.ng-valid {
  border: 1px solid green;
 
}
  
   
:focus ~ .error {
    display:none;
}*/
   form.ng-submitted .ng-invalid {
  border-color: red;
  border-width: 2px;
} 
</style>
</head>
<body ng-controller="myController5">
    <form name="userForm"  class="form-horizontal" ng-submit="submitForm(userForm.$valid)"  >
  <div>
    
      <asp:Panel ID="tt2"  runat="server">
            <table align="center" width="1200px">
            <tr >
                <td >
    <div id="searchform">                
        <table style="width:100%;font-family:Calibri;" id="applicantForm" align="center" class=" table-bordered">
            <tr align="center">
                <td colspan="2">
                    <img alt="Coat Of Arms" height="79" src="../images/coat_of_arms.png" 
                        width="85" />
               </td>
            </tr>
            <tr align="center" style=" font-size:11pt;" >
                <td colspan="2">
                    FEDERAL REPUBLIC OF NIGERIA<br />
                    FEDERAL MINISTRY OF INDUSTRY, TRADE AND INVESTMENT<br />
                    COMMERCIAL LAW DEPARTMENT<br />
                    TRADEMARKS, PATENTS AND DESIGNS REGISTRY
</td>
            </tr>
            
            <tr>
                <td colspan="2" style="font-size:18pt;line-height:115%;" align="center">
                      ADD  NEW RECORD
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                
                    </td>
            </tr>
            
           <%-- <tr>
                <td width="20%">
                    &nbsp;REQUEST FORM TYPE:</td>
                <td>    
                     <select ng-model="Application_Type" name="Application_Type" class="form-control" data-ng-options="c.id as c.name for c in vaplication"  required>
                                                                                                                                <option value="">-- Select Application Type --</option>
                                                                                                                            </select>
                             
                    <%--<b><asp:Label ID="lbl_type" runat="server" Text="" Width="1000px"></asp:Label></b>--%>
                </td>
            </tr>
                <caption>
                   
                    <tr>
                        <td class="tbbg_left" colspan="2">&nbsp;APPLICANT INFORMATION &gt;&gt;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;APPLICANT NAME:&nbsp; </td>
                        <td>
                            <div>
                                <input name="applicant_name" class="form-control"   ng-model="ng_applicant_name" required   type="text" />
                                <div class="errors" ng-messages="userForm.applicant_name.$error" role="alert">
                                    <span ng-message="required">Required!</span>
                                </div>
                                <%-- <span class="help-block col-lg-4" data-ng-show="userForm.txt_applicant_name.$error.required">This is required.</span>--%>
                            </div>
                            <%--  <asp:TextBox  ID="txt_applicant_name"   runat="server" class="form-control" 
                        Width="400px" ></asp:TextBox>--%></td>
                    </tr>
                    <tr>
                        <td>&nbsp;APPLICANT ADDRESS:&nbsp;</td>
                        <td>
                            <textarea class="form-control" cols="25" name="applicant_address" ng-model="ng_applicant_address" required="" rows="4"></textarea>
                            <div class="errors" ng-messages="userForm.applicant_address.$error" role="alert">
                                <span ng-message="required">Required!</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;APPLICANT E-MAIL:&nbsp;</td>
                        <td>
                            <input name="applicant_email"  class="form-control" ng-model="ng_applicant_email" required   type="text" />
                            <div class="errors" ng-messages="userForm.applicant_email.$error" role="alert">
                                <span ng-message="required">Required!</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;APPLICANT PHONE NUMBER:</td>
                        <td>
                            <input name="applicant_mobile" class="form-control"  ng-model="ng_applicant_mobile" required   type="text" />
                            <div class="errors" ng-messages="userForm.applicant_mobile.$error" role="alert">
                                <span ng-message="required">Required!</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;TRADING AS:&nbsp;</td>
                        <td>
                            <input name="trading_as" class="form-control"  ng-model="ng_trading_as"   type="text" />
                        </td>
                    </tr>
                    <%--<tr>
                        <td>&nbsp;AMOUNT:&nbsp;</td>
                        <td>
                            <input name="vamount" class="form-control"  ng-model="ng_amount" required   type="text" />
                            <div class="errors" ng-messages="userForm.vamount.$error" role="alert">
                                <span ng-message="required">Required!</span>
                            </div>
                        </td>
                    </tr>--%>
                    <tr>
                        <td align="left">&nbsp;NATIONALITY : </td>
                        <td align="left">
                            <select class="form-control" data-ng-change="GetStates2()" data-ng-options="c.code as c.name for c in countries" name="country" ng-model="country" required="">
                                <option value="">-- Select Country --</option>
                            </select>
                            <div class="errors" ng-messages="userForm.country.$error" role="alert">
                                <span ng-message="required">Required!</span>
                            </div>
                             <%--<asp:DropDownList ID="select_applicant_nationality" runat="server"  CssClass="textbox"  DataTextField="name" 
                    DataValueField="name" >
                </asp:DropDownList>--%><%-- <asp:SqlDataSource ID="ds_Nationality" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:cldConnectionString %>" 
                    SelectCommand="SELECT * FROM [country]">                  
                    </asp:SqlDataSource>--%></td>
                    </tr>
                    <tr>
                        <td class="tbbg_left" colspan="2">&nbsp;TRADEMARK INFORMATON &gt;&gt;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;TRADEMARK TYPE : </td>
                        <td><%-- <asp:SqlDataSource ID="ds_TmType" runat="server" ConnectionString="<%$ ConnectionStrings:cldConnectionString %>" SelectCommand="SELECT DISTINCT [xID], [type] FROM [tm_type]"></asp:SqlDataSource>--%>
                            <select class="form-control"  data-ng-options="c.id as c.name for c in varray" name="Trademark_Type" ng-model="Trademark_Type" required="">
                                <option value="">-- Select Trademark Type --</option>
                            </select> 
                            <div class="errors" ng-messages="userForm.Trademark_Type.$error" role="alert">
                                <span ng-message="required">Required!</span>
                            </div>

                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;TITLE OF TRADEMARK:</td>
                        <td>
                            <input name="title_of_trademark"  class="form-control"  ng-model="title_of_trademark" required   type="text" />
                            <div class="errors" ng-messages="userForm.title_of_trademark.$error" role="alert">
                                <span ng-message="required">Required!</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">&nbsp;RTM NUMBER(If any):</td>
                        <td>
                            <input name="rtm_no"  class="form-control"  ng-model="ng_rtm_no"    type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">&nbsp;FILE/TP NUMBER:</td>
                        <td>
                            <input name="application_no" id="application_no" class="form-control" ng-blur="change()"  ng-model="ng_application_no" required   type="text" />

                             <div class="errors" ng-messages="userForm.application_no.$error" role="alert">
                                <span ng-message="required">Required!</span>
                            </div>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="20%">&nbsp;FILING DATE:</td>
                        <td>
                            <datepicker button-next="&lt;i class='fa fa-arrow-right'&gt;&lt;/i&gt;" button-prev="&lt;i class='fa fa-arrow-left'&gt;&lt;/i&gt;" date-format="yyyy-M-d">
                                <input name="application_date" id="txt_application_date"  class="form-control" required ng-model="application_date"    type="text" />
                            </datepicker>
                            <div class="errors" ng-messages="userForm.application_date.$error" role="alert">
                                <span ng-message="required">Required!</span>
                            </div>
                            <%-- <asp:TextBox ID="txt_application_date" runat="server" class="form-control"
                            Width="100px"></asp:TextBox>--%></td>
                    </tr>
                    <tr>
                        <td>&nbsp;CLASS OF TRADEMARK:</td>
                        <td>
                            <select class="form-control" data-ng-options="c.xtype as c.xtype for c in vclass" name="vclass2" ng-model="vclass2" required="">
                                <option value="">-- Select CLASS OF TRADEMARK --</option>
                            </select>
                            <div class="errors" ng-messages="userForm.vclass2.$error" role="alert">
                                <span ng-message="required">Required!</span>
                            </div>
                             <%--<asp:DropDownList ID="select_class_of_trademark" runat="server" CssClass="textbox" 
                     DataTextField="type" DataValueField="xID" >
                </asp:DropDownList>--%><%--  <asp:SqlDataSource ID="ds_Class" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:cldConnectionString %>" 
                    
                    SelectCommand="SELECT * FROM national_classes">
                    
                </asp:SqlDataSource>  --%></td>
                    </tr>
                    <tr>
                        <td>&nbsp;DESCRIPTION OF GOODS AND SERVICES:&nbsp;</td>
                        <td>
                            <textarea class="form-control" cols="25" name="txt_goods_desc" ng-model="txt_goods_desc" rows="4"></textarea> <%--<asp:TextBox ID="txt_goods_desc" runat="server" Width="400px" class="form-control"
                    Height="80px"></asp:TextBox>--%></td>
                    </tr>
                    <tr>
                        <td>&nbsp;LOGO DESCRIPTION : </td>
                        <td>
                            <select class="form-control" data-ng-change="GetStates12()" data-ng-options="c.id as c.name for c in classtrademark" name="Logo" ng-model="Logo" required="">
                                <option value="">-- Select LOGO DESCRIPTION --</option>
                            </select>
                            <div class="errors" ng-messages="userForm.Logo.$error" role="alert">
                                <span ng-message="required">Required!</span>
                            </div>
                             <%--<asp:DropDownList ID="logo_description" runat="server"   DataTextField="type" DataValueField="type" CssClass="textbox"  >
                            
                        </asp:DropDownList>--%></td>
                    </tr>
                    <tr>
                        <td>&nbsp;DISCLAIMER(If any):</td>
                        <td>
                            <input name="txt_discalimer"  class="form-control"  ng-model="txt_discalimer"    type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tbbg_left" colspan="2">&nbsp;AGENT INFORMATION (all communications will be done using the information provided below) &gt;&gt;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;CODE: </td>
                        <td>
                            <select class="form-control" data-ng-change="GetAgent2()" data-ng-options="c.Sys_ID as c.Sys_ID for c in Agent" name="rep_code" ng-model="rep_code" required="">
                                <option value="">-- Select Agent Code --</option>
                            </select> 
                           <%-- <input name="rep_code" class="form-control"  ng-model="rep_code" required    type="text" />--%>
                            <%-- <asp:TextBox ID="rep_code" runat="server" Width="400px" 
                   class="form-control" ReadOnly="True" ></asp:TextBox>--%></td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;NAME : </td>
                        <td align="left">
                            <input name="rep_xname" class="form-control"   ng-model="rep_xname" required    type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;NATIONALITY : </td>
                        <td align="left">
                            <select class="form-control" data-ng-change="GetStates()" data-ng-options="c.code as c.name for c in countries" name="country2" ng-model="country2" required="">
                                <option value="">-- Select Country --</option>
                            </select> <%-- <asp:TextBox ID="txt_rep_nationality" runat="server" Width="400px" CssClass="textbox" 
                   ReadOnly="true" Text="Nigeria"></asp:TextBox> --%></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="background-color:#999999;">&nbsp;ADDRESS INFORMATION &gt;&gt;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;COUNTRY : </td>
                        <td>
                            <select class="form-control" data-ng-change="GetStates()" data-ng-options="c.code as c.name for c in countries" name="country3" ng-model="country3" required="">
                                <option value="">-- Select Country --</option>
                            </select> <%--<asp:TextBox ID="txt_rep_country" runat="server" Width="400px" CssClass="textbox" 
                   ReadOnly="true" Text="Nigeria"></asp:TextBox> --%></td>
                    </tr>
                    <tr>
                        <td><%--&nbsp;STATE:--%> </td>
                        <td><%-- <asp:SqlDataSource ID="ds_State" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:cldConnectionString %>" 
                    SelectCommand="SELECT DISTINCT * FROM [state] ">
                    
                </asp:SqlDataSource>--%>
                            <%--<select class="form-control" data-ng-disabled="!states" data-ng-options="s.name as s.name for s in states" name="state" ng-model="state" required="" style="display:inline">
                                <option value="">-- Select State --</option>
                            </select>--%> <%-- <asp:DropDownList ID="selectRepState" runat="server" CssClass="textbox" 
                    DataTextField="name" DataValueField="name" >
                </asp:DropDownList>--%></td>
                    </tr>
                    <tr>
                        <td>&nbsp;ADDRESS : </td>
                        <td>
                            <input name="txt_rep_address" class="form-control"   ng-model="txt_rep_address"    type="text" />
                            <%-- <asp:TextBox ID="txt_rep_address" runat="server" Width="400px" class="form-control" 
                    Height="80px" TextMode="MultiLine"></asp:TextBox> --%></td>
                    </tr>
                    <tr>
                        <td>&nbsp;TELEPHONE: &nbsp;</td>
                        <td>
                            <input name="txt_rep_telephone"  class="form-control"  ng-model="txt_rep_telephone"     type="text" />
                            <%-- <asp:TextBox ID="txt_rep_telephone" runat="server" Width="400px" class="form-control" ReadOnly="false"></asp:TextBox>--%></td>
                    </tr>
                    <tr>
                        <td>&nbsp;E-MAIL: </td>
                        <td>
                            <input name="txt_rep_email" class="form-control"  ng-model="txt_rep_email" required    type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tbbg_left" colspan="2">&nbsp;&nbsp;PUBLICATION INFORMATION&gt;&gt;</td>
                    </tr>
                  <tr>
                      <td>

                          <label>Has This Mark Been Published ? <input type="checkbox" ng-model="master"></label>

                      </td>
                      </tr>
                    <tr ng-show="master">
                        <td>&nbsp;DATE OF PUBLICATION:</td>
                        <td>
                            <datepicker button-next="&lt;i class='fa fa-arrow-right'&gt;&lt;/i&gt;" button-prev="&lt;i class='fa fa-arrow-left'&gt;&lt;/i&gt;" date-format="yyyy-M-d">
                                <input name="txt_cert_publicationdate" class="form-control" id="txt_cert_publicationdate"  ng-model="txt_cert_publicationdate"     type="text" />
                            </datepicker>
                            <%-- <asp:TextBox ID="txt_cert_publicationdate" runat="server" class="form-control"
                        Width="100px"></asp:TextBox>--%></td>
                    </tr>
                    <tr ng-show="master">
                        <td>&nbsp;DETAILS OF PUBLICATION:</td>
                        <td><b>(kindly state the volume and page number of journal)</b><br />
                            <textarea class="form-control" cols="25" name="txt_cert_details" ng-model="txt_cert_details" ng-model="txt_cert_details" rows="4"></textarea> <%-- <asp:TextBox ID="txt_cert_details" runat="server" Width="400px" class="form-control"
                    Height="80px" TextMode="MultiLine"></asp:TextBox> --%></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" style="background-color:#999999;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" style="background-color:#999999;">&nbsp;</td>
                    </tr>
                    <table id="doc_tbl" align="center" style="width:100%;font-family:Calibri;">
                        <tr>
                            <td class="tbbg" colspan="2"></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color:Red; font-size:16px;"><strong>(NOTE: DOCUMENTS ATTACHED SHOULD BE OF PDF FORMAT ONLY AND NOT MORE THAN 3MB EACH!!)</strong> </td>
                        </tr>
                        <tr style="background-color:#999999;">
                            <td align="left" class="tbbg_left2" style="width:25%;">&nbsp;ITEMS</td>
                            <td align="left" class="tbbg_left2">ATTACH DOCUMENT</td>
                        </tr>
                        <tr ng-show="trademark10">
                            <td align="left">&nbsp;TRADEMARK REPRESENTATION : </td>
                            <td align="left">
                                <input id="File1" type="file" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2"></td>
                        </tr>
                        <tr>
                            <td align="left">&nbsp;ACCEPTANCE LETTER : </td>
                            <td align="left">
                                <input id="File2" type="file" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                        </tr>
                      <%--  <tr>
                            <td align="left">&nbsp;COPY OF CERTIFICATE OF REGISTRATION: </td>
                            <td align="left">
                                <input id="File3" type="file" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <td align="left">&nbsp;TRADEMARK CERTIFICATE (IF ANY):</td>
                            <td align="left">
                                <input id="File4" type="file" />
                            </td>
                        </tr>
                    </table>
                    <tr>
                        <td align="center" colspan="2"><%-- <asp:Button ID="SaveAll" runat="server" Text="Submit Application"  class="button" 
                    />--%>
                            <button id="Button3" ng-disabled="processing" class="btn-primary" type="submit">
                               Add Record
                            </button>
                        </td>
                    </tr>
                </caption>
            </table>
            
    
    </div>
    </td>
    </tr>
    </table>

      </asp:Panel>
    
</div>
        <input id="vagent_code" name="vagent_code" type="hidden" runat="server" />

         <input id="vagent_name" name="vagent_name" type="hidden" runat="server" />
      <%--  <asp:Literal runat="server"   ID="litInputAmount"></asp:Literal>--%>
    </form>
</body>
</html>
