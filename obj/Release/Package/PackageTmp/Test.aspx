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
</head>
<body>
 <nav class="col-1">Nav</nav>
  <div class="col-2">
  	<header>Header</header>
    <main class="content">
    <article>Article</article>
    <aside>Aside</aside>
    </main>
  	<footer>Footer</footer>
  </div>
</body>
</html>
