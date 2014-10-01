<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_WholePage_Params.aspx.cs" Inherits="TestJSAPIMappingArc93.Test_WholePage_Params" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>TDP Test Harness - Whole Page & Params</title>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="Pragma" content="no-cache" />
		<meta http-equiv="cache-control" content="no-cache" />
  	<style type="text/css">
		html,body{ padding: 0px; margin: 0px; width: 100%; height: 100%; overflow: hidden; }
		</style>
</head>
<body>

    <%-- START: Map control required tags --%>
    <div class="tundra" dojoType="ESRIUK.Dijits.Map" param_WholePage="true" 
      param_Level="1" param_Scale="2500" param_LocationX="532000" param_LocationY="180000"></div>
    <%-- END: Map control required tags --%>      
		
		<%-- START: Map control required tags --%>
    <script type="text/javascript" src="./esriMap/ESRIJSAPIv1.5/default.ashx"></script>
		<%-- END: Map control required tags --%>    
</body>
</html>
