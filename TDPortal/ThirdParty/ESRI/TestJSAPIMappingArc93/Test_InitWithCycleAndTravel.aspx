<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_InitWithCycleAndTravel.aspx.cs" Inherits="TestJSAPIMappingArc93.Test_InitWithRoutesAndTravel" %>

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
              param_TravelNews="transportType:all,incidentType:all,severity:all,timePeriod:date,datetime:10/07/1981 10/43"          
              param_Routes="[{sessionId:'1',routeNumber:1,type:'Cycle'}]"
              param_Tools="selectNearby,userDefined,zoom,pan"
              param_Width="776" param_Height="540">
          </div>
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
                <strong>PT Route&nbsp;</strong>
                <label for="ptRouteSessionID">Session: </label>
                <input id="ptRouteSessionID" type="text" style="width: 25px;" value="1" dojoType="dijit.form.TextBox"/>
                <label for="ptRouteRouteNumber">Num: </label>
                <input id="ptRouteRouteNumber" type="text" style="width: 25px;" value="1" dojoType="dijit.form.TextBox"/>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.zoomToPTRoute' 
                  title='Zooms map to extent of a PT route.'>Zoom</button>   
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.addPTRoute' 
                  title='Add a PT route to the map.'>Add</button>  
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.clearPTRoutes' 
                  title='Clear all PT routes from map.'>Clear</button>                        
                <hr style="border-color:#ccc; border-width: 1px 0px 0px 0px; height: 1px; width: 80%; color: #ccc; margin-top: 1px; margin-bottom: 1px;" />                      
                <strong>Road Route&nbsp;</strong>
                <label for="roadRouteSessionID">Session: </label>
                <input id="roadRouteSessionID" type="text" style="width: 25px;" value="1" dojoType="dijit.form.TextBox"/>
                <label for="roadRouteRouteNumber">Num: </label>
                <input id="roadRouteRouteNumber" type="text" style="width: 25px;" value="1" dojoType="dijit.form.TextBox"/>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.zoomToRoadRoute' 
                  title='Zooms map to extent of a Road route.'>Zoom</button>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.addRoadRoute' 
                  title='Add a Road route to the map.'>Add</button>  
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.clearRoadRoutes' 
                  title='Clear all road routes from map.'>Clear</button>                                             
                <hr style="border-color:#ccc; border-width: 1px 0px 0px 0px; height: 1px; width: 80%; color: #ccc; margin-top: 1px; margin-bottom: 1px;" />                      
                <strong>Cycle Route&nbsp;</strong>
                <label for="cycleRouteSessionID">Session: </label>
                <input id="cycleRouteSessionID" type="text" style="width: 25px;" value="1" dojoType="dijit.form.TextBox"/>
                <label for="cycleRouteRouteNumber">Num: </label>
                <input id="cycleRouteRouteNumber" type="text" style="width: 25px;" value="1" dojoType="dijit.form.TextBox"/>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.zoomToCycleRoute' 
                  title='Zooms map to extent of a Cycle route.'>Zoom</button>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.addCycleRoute' 
                  title='Add a Road Cycle to the map.'>Add</button>  
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.clearCycleRoutes' 
                  title='Clear all Cycle routes from map.'>Clear</button>    
                <hr style="border-color:#ccc; border-width: 1px 0px 0px 0px; height: 1px; width: 80%; color: #ccc; margin-top: 1px; margin-bottom: 1px;" />                                                                       
                <button dojoType="dijit.form.Button" onClick="dijit.byId('esriukTDPMap').zoomtoAllAddedRoutes();"
                  title='ZoomtoAllAddedRoutes.'>ZoomtoAllAddedRoutes</button>  
            </div>
            Initial routes shown:<br />
            <code style="color:Blue;font-family:Courier New">
            param_Routes="[ {sessionId:'1',routeNumber:1,type:'Cycle'} ]"
            </code>   
            <br />Init TravelNews
            <br />
            <code style="color:Blue;font-family:Courier New">
             param_TravelNews=" transportType:all ,incidentType:all, severity:all, timePeriod:date, datetime:10/07/1981 10/43          
            </code>
        </div>
    </div>
		<%-- START: Map control required tags --%>
    <script type="text/javascript" src="./esriMap/ESRIJSAPIv1.5/default.ashx?language=en"></script>
		<%-- END: Map control required tags --%>        
		<script type="text/javascript" src="./script/TestHarness/ESRIUKTestHarness.js"></script>
</body>
</html>
