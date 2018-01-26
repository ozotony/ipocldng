<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Ipong.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
<style>
  * {
    box-sizing: border-box; 
  }
  body {
    display: flex;
    min-height: 100vh;
    flex-direction: row;
    margin: 0;
  }
  .col-1 {
    background: #D7E8D4;
    flex: 1;
  }
  .col-2 {
    display: flex;
    flex-direction: column;
    flex: 5;
  }
  .content {
    display: flex;
    flex-direction: row;
  }
  .content > article {
    flex: 3;
    min-height: 60vh;
  }
  .content > aside {
    background: beige;
    flex: 1;
  }
  header, footer {
    background: yellowgreen;
    height: 20vh;
  }
  header, footer, article, nav, aside {
    padding: 1em;
  }
</style>

<script crossorigin src="https://unpkg.com/react@16/umd/react.development.js"></script>
<script crossorigin src="https://unpkg.com/react-dom@16/umd/react-dom.development.js"></script>
<script src="https://unpkg.com/babel-standalone@6/babel.min.js">
</script>

</head>
<body>
     <div id="app"></div>
        <script type="text/babel">
class App extends React.Component {


render() {

var rows = this.props.data.map(function(row){
return <tr>
<td>{row.when}</td>
<td>{row.who}</td>
<td>{row.description}</td>
</tr>
            });
return <div>
<table>
<thead>
<th>When</th>
<th>Who</th>
<th>Description</th>
</thead>
{rows}
</table>

    </div>
}
}
var mount = document.querySelector('#app');
var data = [{ "when": "2 minutes ago",
"who": "Jill Dupre",
"description": "Created new account"
},
{
"when": "1 hour ago",
"who": "Lose White",
"description": "Added fist chapter"
}];
ReactDOM.render(<App  data = {data} />, mount);
</script>


     
 
</body>
</html>
