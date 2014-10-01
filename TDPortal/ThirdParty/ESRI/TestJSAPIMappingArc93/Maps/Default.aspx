<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TestJSAPIMappingArc93.Maps.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Sub-directory text</title>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="Pragma" content="no-cache" />
		<meta http-equiv="cache-control" content="no-cache" />	
</head>
<body>
    <a href="../Links.aspx">Back to root directory</a> | <a href="./Maps/Default.aspx">Next sub-direcory level</a><br />
    <%-- START: Map control required tags --%>
    <div class="tundra" dojoType="ESRIUK.Dijits.Map" param_Tools="userDefined,selectNearby"></div>
    <%-- END: Map control required tags --%>
		<%-- START: Map control required tags --%>
    <script type="text/javascript" src="../esriMap/ESRIJSAPIv1.5/default.ashx"></script>
		<%-- END: Map control required tags --%>        
</body>
</html>
