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
  
  }


  #container {
      display:flex;
      flex-direction:column;
      width: 100%; 
  } 

   #content {
     flex-grow:1;
     width: 100%; 
  } 

    #footer {
     height:50px;
     
     width: 100%; 
     background-color:red;
      display:flex;
      justify-content:center;
  } 

    #footer2 {

    }
</style>

<script crossorigin src="https://unpkg.com/react@16/umd/react.development.js"></script>
<script crossorigin src="https://unpkg.com/react-dom@16/umd/react-dom.development.js"></script>
<script src="https://unpkg.com/babel-standalone@6/babel.min.js">
</script>

</head>
<body>
     <div id="container">
            <div id="content">
<p> Testin</p>
         

     </div>

          <div id="footer">

         <p id="footer2"> Testin2 </p>

     </div>
         

     </div>
  


     
 
</body>
</html>
