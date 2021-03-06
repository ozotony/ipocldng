﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay_stats.aspx.cs" Inherits="Ipong.P.pay_stats" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PAYMENT HISTORY</title>
     <link href="../../css/style.css" rel="stylesheet" type="text/css" /> 
     <link rel="stylesheet" href="../../css/jquery.ui.all.css" />
     <link rel="stylesheet" href="../../css/jquery.ui.theme.css" />
     <link rel="stylesheet" href="../../css/jquery.ui.tabs.css" />

    <script src="../../js/jquery.js" type="text/javascript"></script>
   <script src="../amcharts_2.6.6/amcharts/amcharts.js" type="text/javascript"></script> 
   <script src="../../js/funk.js" type="text/javascript"></script>  
   <%if (show_inv > 0)
     { %>     
        <script type="text/javascript">
            var chart;

            var chartData = [{
                country: "Jan",
                litres: <%=jan%>,
                short: ""
            }, {
                country: "Feb",
                litres: <%=feb%>,
                short: ""
            }, {
                country: "Mar",
                litres: <%=mar %>,
                short: ""
            }, {
                country: "Apr",
                litres: <%=apr %>,
                short: ""
            }, {
                country: "May",
                litres: <%=may%>,
                short: ""
            }, {
                country: "Jun",
                litres: <%=jun %>,
                short: ""
            },
            {
                country: "Jul",
                litres: <%=jul %>,
                short: ""
            }, {
                country: "Aug",
                litres: <%=aug %>,
                short: ""
            }, {
                country: "Sep",
                litres: <%=sep%>,
                short: ""
            }, {
                country: "Oct",
                litres: <%=oct%>,
                short: ""
            }, {
                country: "Nov",
                litres: <%=nov %>,
                short: ""
            }, {
                country: "Dec",
                litres: <%=dec %>,
                short: ""
            }];

            AmCharts.ready(function () {
                // SERIAL CHART
                var chart = new AmCharts.AmSerialChart();
                chart.dataProvider = chartData;
                chart.categoryField = "country";
                chart.startDuration = 2;
                // change balloon text color                
                chart.balloon.color = "#000000";

                // AXES
                // category
                var categoryAxis = chart.categoryAxis;
                categoryAxis.gridAlpha = 0;
                categoryAxis.axisAlpha = 0;
                categoryAxis.labelsEnabled = true;

                // value
                var valueAxis = new AmCharts.ValueAxis();
                valueAxis.gridAlpha = 0;
                valueAxis.axisAlpha = 0;
                valueAxis.labelsEnabled = true;
                valueAxis.minimum = 0;
                chart.addValueAxis(valueAxis);

                // GRAPH
                var graph = new AmCharts.AmGraph();
                graph.balloonText = "[[category]]: [[value]]";
                graph.valueField = "litres";
                graph.descriptionField = "short";
                graph.type = "column";
                graph.lineAlpha = 0;
                graph.fillAlphas = 1;
                graph.fillColors = ["#ffe78e", "#bf1c25"];
                graph.labelText = "[[description]]";
                graph.balloonText = "[[category]]: [[value]] NGN";
                chart.addGraph(graph);

                // WRITE
                chart.write("chartdiv");
            });
        </script>
 <%} %>  

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="container">
            <div class="sidebar">                 
              <a href="./profile.aspx">PROFILE</a>                 
                <a href="../upd_pro.aspx">UPDATE PROFILE</a> 
                <a href="./pay_his.aspx">PAYMENT HISTORY</a>
                <a href="./pay_stats.aspx">PAYMENT CHARTS</a>
                <a href="../lo.ashx">SIGN OUT</a>  
            </div>
            <div class="content">
               

                 <table class="center-align" width="100%" class="form">
                  <tr class="center-align">
                <td colspan="4">
                  <img alt="Coat of Arms" height="84" src="../images/LOGOCLD.png" width="509" />
               </td>
            </tr>
        <tr>
            <td align="right" style="width:25%;">
              <strong>GRAND TOTAL ITEM(S):</strong></td>
            <td align="left" style="width:25%; font-weight:bold; color:Green;">
                &nbsp;
                <%=Session["grand_tot_cnt"].ToString()%></td>
            <td align="right" style="width:25%;">
             <strong>GRAND TOTAL AMOUNT:</strong></td>
            <td align="left" style="width:25%;font-weight:bold; color:Green;">
                &nbsp;
                <%=Session["new_grand_tot_amt"].ToString()%> NGN</td>
        </tr>
         <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" colspan="4">&nbsp;</td>
        </tr>
         <tr>
            <td class="center-align" colspan="4" >
             Select a year:
                <asp:DropDownList ID="ddl_year" runat="server" CssClass="textbox">
                </asp:DropDownList>
                &nbsp;<asp:Button ID="btnSearch" runat="server" class="button" Text="View Graph" 
                    onclick="btnSearch_Click"   />
                    </td>
        </tr>
         <tr>
            <td class="center-align" colspan="4" >
                <strong><%=search_msg %></strong>  
                </td>
        </tr>
        
        </table>
        <% if (show_inv == 1)
           {
               %>  
              
    <tr>
            <td class="center-align">
             <div id="chartdiv" style="width: 100%; height: 600px;"></div>                
            </td>
        </tr>

          <tr >
			<td >
		<div style="padding-left:50%;">	<input type="button" name="Printform" id="PrintWingmanDetails" value="Print" onclick="PrintPartner('chartdiv');return false" class="button" /></div>
			</td>
            </tr>
               
 <%    }%>
       
            </div>
        </div>
    </div>
    </form>
</body>
</html>
