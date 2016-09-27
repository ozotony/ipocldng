<%@ Page Language="C#" AutoEventWireup="true" CodeFile="application.aspx.cs" Inherits="application" MaintainScrollPositionOnPostback="true" %>

<%@ Register assembly="Brettle.Web.NeatUpload" namespace="Brettle.Web.NeatUpload" tagprefix="Upload" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>IPO PORTAL</title>

<link href="../css/style.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="../css/jquery.ui.all.css" type="text/css"/>
<link rel="stylesheet" href="../css/jquery.ui.theme.css" type="text/css"/>

<script src="../js/funk.js" type="text/javascript"></script>
<script src="../js/jquery.js" type="text/javascript"></script>
<script src="../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
<script src="../js/ui/jquery.cookie.js" type="text/javascript"></script>
<script src="../js/ui/jquery.ui.core.js" type="text/javascript"></script>
<script src="../js/ui/jquery.ui.widget.js" type="text/javascript"></script>

<script src="../js/ui/jquery.ui.datepicker.js" type="text/javascript"></script>

<script  type="text/javascript">

    $(function () {

        $(".txt_date_pri").datepicker({
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            dateFormat: 'yy-mm-dd',
            yearRange: 'c-100:c+0'
        });
        
    });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="center-align" width="1200px">
            <tr >
                <td >
    <div id="searchform">                
        <table style="width:100%;font-family:Calibri;" id="applicantForm" class="center-align" >
            <tr class="center-align">
                <td colspan="2">
                    <img alt="Coat Of Arms" height="79" src="../images/coat_of_arms.png" 
                        width="85" />
               </td>
            </tr>
            <tr class="center-align" style=" font-size:11pt;" >
                <td colspan="2">
                    FEDERAL REPUBLIC OF NIGERIA<br />
                    FEDERAL MINISTRY OF INDUSTRY, TRADE AND INVESTMENT<br />
                    COMMERCIAL LAW DEPARTMENT<br />
                    TRADEMARKS, PATENTS AND DESIGNS REGISTRY<br />
                    PATENTS AND DESIGNS ACT CAP 344 LFN 1990 
</td>
            </tr>
            
            <tr>
                <td colspan="2" style="font-size:18pt;line-height:115%;" class="center-align">
                        APPLICATION FOR ACCREDITATION
                </td>
            </tr>
            <tr>
                <td colspan="2">
                      &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" class="tbbg_left">
                    &nbsp;APPLICANT INFORMATION >></td>
            </tr>            
           
            <tr>
                <td >
                    ACCREDITATION TYPE:</td>
                <td>
                    <select id="selectAccreditationType" name="selectAccreditationType">
                        <option value="CORPORATE">CORPORATE</option>
                        <option value="INDIVIDUAL">INDIVIDUAL</option>
                    </select>
                    </td>
            </tr>
            
            <tr>
                <td >
                    TITLE:</td>
                <td>
                    <select id="selectTitle" name="selectTitle">
                        <option value="ALHAJA">ALHAJA</option>
                        <option value="ALHAJI">ALHAJI</option>
                        <option value="CHIEF">CHIEF</option>
                        <option value="DR">DR</option>
                        <option value="ENGR">ENGR</option>
                        <option value="MISS">MISS</option>
                        <option value="MR">MR</option>
                        <option value="MRS">MRS</option>
                        <option value="PASTOR">PASTOR</option>
                        <option value="PROF">PROF</option>
                        <option value="REV">REV</option>
                        <option value="SIR">SIR</option>
                        <option value="NOT APPLICABLE">NOT APPLICABLE</option>
                    </select></td>
            </tr>
            
            <tr>
                <td >
                    FIRST NAME:</td>
                <td>
                    <input id="txt_username" runat="server" class=" textbox" name="txt_username" size="50" type="text" /></td>
            </tr>
            
            <tr>
                <td >
                    SURNAME:</td>
                <td>
                    <input id="txt_username0" runat="server" class=" textbox" name="txt_username0" 
                        size="50" type="text" /></td>
            </tr>
            
            <tr>
                <td>
                    E-MAIL:</td>
                <td>
                    <input id="txt_username1" runat="server" class=" textbox" name="txt_username1" 
                        size="50" type="text" /></td>
            </tr>
            
            <tr>
                <td class="style1">
                    MOBILE NUMBER:</td>
                <td class="style1">
                    <input id="txt_username2" runat="server" class=" textbox" name="txt_username2" 
                        size="50" type="text" /></td>
            </tr>
            
            <tr>
                <td>
                    DATE OF BIRTH:</td>
                <td>
                    <asp:TextBox ID="txt_assignment_date" runat="server" Width="70px" CssClass="txt_date_pri" ></asp:TextBox></td>
            </tr>
            
           
            
            <tr>
                <td>
                    NATIONALITY:</td>
                <td>
                    NIGERIA</td>
            </tr>
            
           
            
            <tr>
                <td>
                    PASSPORT UPLOAD:&nbsp;</td>
                <td align="left">
                   <label for="uploadFile">
                   <div id="image" style="background-image:url('../images/shadow_male.jpg'); width:190px; height:155px; background-repeat:no-repeat;">
                   </div>
                    </label>
                <input type="file" id="uploadFile" style="display:none" />

</td>
            </tr>
             <tr>
                <td colspan="2" class="sub_header" align="left">
                    &nbsp;COMPANY INFORMATION >></td>
            </tr>
            <tr>
                <td>
                    NAME:</td>
                <td>
                    <input id="txt_username3" runat="server" class=" textbox" name="txt_username3" 
                        size="87" type="text" /></td>
            </tr>
            
            <tr>
                <td>
                    ADDRESS:</td>
                <td>
                    <textarea id="txt_postal_addy" class="textbox" cols="60" name="txt_postal_addy" 
                        rows="3"></textarea></td>
            </tr>
            
            <tr>
                <td>
                    WEBSITE:</td>
                <td>
                    <input id="txt_username4" runat="server" class=" textbox" name="txt_username4" 
                        size="87" type="text" /></td>
            </tr>
            
            <tr>
                <td>
                    INTRODUCTION:</td>
                <td>
                    <textarea id="txt_postal_addy1" class="textbox" cols="60" name="txt_postal_addy1" 
                        rows="3"></textarea></td>
            </tr>
             <tr>
                <td>
                    DATE OF INCORPORATION:</td>
                <td>
                    <asp:TextBox ID="txt_assignment_date0" runat="server" Width="70px" 
                        CssClass="txt_date_pri" ></asp:TextBox></td>
            </tr>
           <tr>
                <td colspan="2" class="sub_header" align="left">
                    &nbsp;CONTACT INFORMATION >></td>
            </tr>
            
            <tr>
                <td>
                    FULLNAME:</td>
                <td>
                    <input id="txt_username5" runat="server" class=" textbox" name="txt_username5" 
                        size="87" type="text" /></td>
            </tr>
            
            <tr>
                <td>
                    ADDRESS:</td>
                <td>
                    <textarea id="txt_postal_addy0" class="textbox" cols="60" 
                        name="txt_postal_addy0" rows="3"></textarea></td>
            </tr>
            
            <tr>
                <td>
                    MOBILE:</td>
                <td>
                    <input id="txt_username6" runat="server" class=" textbox" name="txt_username6" 
                        size="50" type="text" /></td>
            </tr>
            
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td colspan="2" class="center-align">            
                 <input id="btnChangePin" class="button" onclick="doChangePin('accreditaion.ashx');" type="button" value="Submit Application" />
                &nbsp;<asp:Button ID="SaveExit" runat="server" class="button" Text="Save and Exit" />
                </td>
            </tr>           
            </table>
            
    
    </div>
    </td>
    </tr>
    </table>
    </form>
</body>
</html>
