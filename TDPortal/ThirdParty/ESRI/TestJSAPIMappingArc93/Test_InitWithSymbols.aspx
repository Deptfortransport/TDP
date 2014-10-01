<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_InitWithSymbols.aspx.cs" Inherits="TestJSAPIMappingArc93.Test_InitWithSymbols" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>TDP Test Harness - Dojo</title>
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
              param_XMin="405000" param_YMin="190000" param_XMax="460000" param_YMax="260000"
              param_Symbols="[{x:450000,y:200000,label:'point 1',type:'start',infoWindowRequired:false},
              {x:400000,y:250000,label:'',type:'symbol',symbolKey:'DIAMOND',infoWindowRequired:true,content:'esriukTDPMap_symbolContent1'},
              {x:415000,y:210000,label:'point 3',type:'end',infoWindowRequired:true,content:'this is text content'}]" 
              param_Tools="selectNearby,userDefined,zoom,pan"
              param_Width="776" param_Height="540">
          </div>
          <%-- Content for map symbols --%>
          <div id="esriukTDPMap_symbolContent1" style="display:none"><a href='javascript:showCarParkInfo(1234);'>Car Park Info</a></div>
          <%-- END: Map control required tags --%>
        </div>
        <div dojoType="dijit.layout.ContentPane" region="trailing" style="width: 350px; font-size: 12px;" id="testHarnessCode">             
            <div dojoType="dijit.layout.ContentPane" style="border: 1px solid #ccc; margin: 2px; padding: 2px">
                <label for="setMinXBox">Minx: </label>
                <input id="setMinXBox" type="text" style="width: 60px;" value="300000" dojoType="dijit.form.TextBox"/>
                <label for="setMinYBox">Miny: </label>
                <input id="setMinYBox" type="text" style="width: 60px;" value="150000" dojoType="dijit.form.TextBox"/>
                <br />
                <label for="setMaxXBox">Maxx: </label>
                <input id="setMaxXBox" type="text" style="width: 60px;" value="400000" dojoType="dijit.form.TextBox"/>
                <label for="setMaxYBox">Maxy: </label>
                <input id="setMaxYBox" type="text" style="width: 60px;" value="250000" dojoType="dijit.form.TextBox"/>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.zoomToEnvelope' 
                  title='Zooms map to envelope defined in the above x & y inputs.'>Zoom to Envelope</button>
            </div>
            <div dojoType="dijit.layout.ContentPane" style="border: 1px solid #ccc; margin: 2px; padding: 2px">
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.clearPoints'>Clear Symbols</button>
            </div>
            Initial symbols shown:<br />
            <code style="color:Blue;font-family:Courier New">
            param_Symbols="[{x:450000,y:200000,label:'point 1',type:'start',infoWindowRequired:'false'},
              {x:400000,y:250000,label:'',type:'symbol',symbolKey:'PUSHPIN',infoWindowRequired:'true',content:'esriukTDPMap_symbolContent1'},
              {x:415000,y:210000,label:'point 3',type:'end',infoWindowRequired:'true',content:'this is text content'}]" 
            </code>               
        </div>
    </div>
		<%-- START: Map control required tags --%>
    <script type="text/javascript" src="./esriMap/ESRIJSAPIv1.5/default.ashx?language=en"></script>
		<%-- END: Map control required tags --%>        
		<script type="text/javascript" src="./script/TestHarness/ESRIUKTestHarness.js"></script>
</body>
</html>
