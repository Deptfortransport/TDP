<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_ValidXHTML.aspx.cs" Inherits="TestJSAPIMappingArc93.Test_ValidXHTML" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<%-- VALID XHTML 1.0 Transitional Page --%>
<head>
    <title>TDP Test Harness - Valid XHTML 1.0 Transitional</title>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="Content-type" content="text/html;charset=UTF-8" />
		<meta http-equiv="Pragma" content="no-cache" />
		<meta http-equiv="cache-control" content="no-cache" />
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
      <img src="http://www.w3.org/Icons/valid-xhtml10"
        alt="Valid XHTML 1.0 Transitional" height="31" width="88" style="border-width:0px;" />
    </a>
    </body>
</html>
