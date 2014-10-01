<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_MapModeNone.aspx.cs" Inherits="TestJSAPIMappingArc93.Test_MapModeNone" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>TDP Test Harness - Travel News filter test</title>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="Pragma" content="no-cache" />
		<meta http-equiv="cache-control" content="no-cache" />
		<script type="text/javascript" src="script/TestHarness/APIHandler.js"></script>
		<style type="text/css">
		body,body { padding: 0px; }
    html,body,body *{ font-size: 12px; font-family: Corbel, "Lucida Grande", "Lucida Sans Unicode", "Lucida Sans", "DejaVu Sans", "Bitstream Vera Sans", "Liberation Sans", Verdana, "Verdana Ref", sans-serif; }
    #testHarnessCode *{ font-size: 11px; }
    #testHarnessCode .dijitButton, #testHarnessCode .dijitDropDownButton, #testHarnessCode .dijitComboButton{
      margin:0.1em;
    }
    #testHarnessCode .dijitButtonNode{
      padding:0.1em 0.1em 0.1em;
    }
		</style>
</head>
<body class="tundra">
    <a href="Links.aspx" title="Back to links page" style="display:block;font-size:8px">Back to links page</a>
    <div dojoType="dijit.layout.BorderContainer" style="height: 750px; width: 1172px; margin: auto; border: 1px solid #ccc; padding: 4px;">
        <div dojoType="dijit.layout.ContentPane" region="top"><strong>TDP ArcIMS JavaScript API solution</strong></div>
        <div dojoType="dijit.layout.ContentPane" region="center" style="width: 780px; overflow: hidden; font-size: 12px;">
          <%-- START: Map control required tags --%>
          <div id="esriukTDPMap" class="tundra" dojoType="ESRIUK.Dijits.Map"
              param_Tools="selectNearby,userDefined,zoom,pan"
              param_Mode="none"
              param_Width="776" param_Height="540" param_Scale="5000" 
              param_LocationX="528700" param_LocationY="180600">
          </div>
          <%-- END: Map control required tags --%>
        </div>
        <div dojoType="dijit.layout.ContentPane" region="trailing" style="width: 350px; font-size: 12px;" id="testHarnessCode">
            <div dojoType="dijit.layout.ContentPane" style="border: 1px solid #ccc; margin: 2px; padding: 2px">
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.getLayerList'>Get Layer List</button>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.setLayerList'>Update Map</button>
                <div id="layerListContent" style="border: 1px solid #ccc; margin: 2px; padding: 2px"></div>
            </div>                                  
        </div>
    </div>
		<%-- START: Map control required tags --%>
    <script type="text/javascript" src="./esriMap/ESRIJSAPIv1.5/default.ashx?language=en"></script>
		<%-- END: Map control required tags --%>        
		<script type="text/javascript" src="./script/TestHarness/ESRIUKTestHarness.js"></script>
</body>
</html>
