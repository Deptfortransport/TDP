<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_ValidHTML4.aspx.cs" Inherits="TestJSAPIMappingArc93.Test_ValidHTML4" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
        "http://www.w3.org/TR/html4/loose.dtd">
 <%-- VALID HTML 4.01 Transitional Page --%>
<html>
<head>
    <title>TDP Test Harness - Valid HTML 4.01 Transitional</title>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <meta http-equiv="Content-type" content="text/html;charset=UTF-8">
		<meta http-equiv="Pragma" content="no-cache">
		<meta http-equiv="cache-control" content="no-cache">
</head>
<body style="padding: 0px; margin: 0px;">
    <%-- START: Map control required tags --%>
    <div class="tundra"></div>
    <%-- END: Map control required tags --%>
		<%-- START: Map control required tags --%>
    <script type="text/javascript" src="./esriMap/ESRIJSAPIv1.5/default.ashx"></script>
		<%-- END: Map control required tags --%>   
		
		<script type="text/javascript" language="javascript">
		  dojo.addOnLoad(function() {
		        var map = new ESRIUK.Dijits.Map({
		          'class': 'tundra'
		        },
		        document.body.getElementsByTagName('div')[0]);
		        map.startup(); 
		     }
      )
		</script>     

    <a href="http://validator.w3.org/check?uri=referer" style="float:right;border-width:0px;">
      <img src="http://www.w3.org/Icons/valid-html401"
        alt="Valid HTML 4.01 Transitional" height="31" width="88" style="border-width:0px;">
    </a>
    </body>
</html>