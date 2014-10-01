<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_Tools_NoNavPanel.aspx.cs" Inherits="TestJSAPIMappingArc93.Test_Tools_NoNavPanel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml"  style="padding: 0px; margin: 0px;">
<head id="Head1" runat="server">
    <title>TDP Test Harness - Basic Map & Params</title>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="Pragma" content="no-cache" />
		<meta http-equiv="cache-control" content="no-cache" />
</head>
<body>
    <a href="Links.aspx" title="Back to links page" style="display:block;font-size:8px">Back to links page</a>
    <%-- START: Map control required tags --%>
    <div class="tundra" dojoType="ESRIUK.Dijits.Map" 
      param_Width="750" param_Height="400"
      param_Scale="10000" param_LocationX="384792" param_LocationY="397854" param_Tools="selectNearby"></div>
    <%-- END: Map control required tags --%>
		<%-- START: Map control required tags --%>
    <script type="text/javascript" src="./esriMap/ESRIJSAPIv1.5/default.ashx"></script>
		<%-- END: Map control required tags --%>        
</body>
</html>
