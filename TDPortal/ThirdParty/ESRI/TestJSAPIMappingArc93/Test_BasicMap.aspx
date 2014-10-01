<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_BasicMap.aspx.cs" Inherits="TestJSAPIMappingArc93._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml"  style="padding: 0px; margin: 0px;">
<head runat="server">
    <title>TDP Test Harness - Basic Map</title>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="Pragma" content="no-cache" />
		<meta http-equiv="cache-control" content="no-cache" />
		<script type="text/javascript" src="script/TestHarness/APIHandler.js"></script>		
</head>
<body>
    <a href="Links.aspx" title="Back to links page" style="display:block;font-size:8px">Back to links page</a>
    <%-- START: Map control required tags --%>
    <div class="tundra" dojoType="ESRIUK.Dijits.Map" param_Tools="userDefined,selectNearby,zoom,pan"></div>
    <%-- END: Map control required tags --%>
		<%-- START: Map control required tags --%>
    <script type="text/javascript" src="./esriMap/ESRIJSAPIv1.5/default.ashx"></script>
		<%-- END: Map control required tags --%>        
</body>
</html>
