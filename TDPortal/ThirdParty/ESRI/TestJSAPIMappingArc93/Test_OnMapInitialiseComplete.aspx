<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_OnMapInitialiseComplete.aspx.cs" Inherits="TestJSAPIMappingArc93.Test_OnMapInitialiseComplete" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml"  style="padding: 0px; margin: 0px;">
<head id="Head1" runat="server">
    <title>TDP Test Harness - Basic Map</title>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="Pragma" content="no-cache" />
		<meta http-equiv="cache-control" content="no-cache" />
</head>
<body>
    <a href="Links.aspx" title="Back to links page" style="display:block;font-size:8px">Back to links page</a>
    <div class="tundra" dojoType="ESRIUK.Dijits.Map" param_Tools="userDefined,selectNearby,zoom,pan"></div>
    <script type="text/javascript">
        var ESRIUKTDPAPI = {
            onMapInitialiseComplete:function(map){
                alert('onMapInitialiseComplete called\nmap.id = '+map.id+'\nmap.loaded = '+map.loaded);
            }
        };
    </script>    
    <script type="text/javascript" src="./esriMap/ESRIJSAPIv1.5/default.ashx"></script>
</body>
</html>
