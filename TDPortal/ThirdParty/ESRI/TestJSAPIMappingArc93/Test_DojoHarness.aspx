<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_DojoHarness.aspx.cs" Inherits="TestJSAPIMappingArc93._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
              param_TravelNews="transportType:all,incidentType:all,severity:all,timePeriod:datetime,datetime:10/07/1981 10/43"
              param_Symbols="[{x:450000,y:200000,label:'point 1',type:'start',infoWindowRequired:false},
              {x:400000,y:250000,label:'',type:'symbol',symbolKey:'DIAMOND',infoWindowRequired:true,content:'esriukTDPMap_symbolContent1'},
              {x:415000,y:210000,label:'point 3',type:'end',infoWindowRequired:true,content:'this is text content'}]"
              param_Tools="selectNearby,userDefined,zoom,pan"
              param_Mode="start,via,end"
              param_Text="Plan a plane journey"
              param_Width="776" param_Height="540" param_Scale="750000" 
              param_LocationX="450000" param_LocationY="200000">
          </div>
          <%-- Content for map symbols --%>
          <div id="esriukTDPMap_symbolContent1" style="display:none"><a href='javascript:showCarParkInfo(1234);'>Car Park Info</a></div>          
          <%-- END: Map control required tags --%>
        </div>
        <div dojoType="dijit.layout.ContentPane" region="trailing" style="width: 350px; font-size: 12px;" id="testHarnessCode">
            <div dojoType="dijit.layout.ContentPane" style="border: 1px solid #ccc; margin: 2px; padding: 2px">
            <%--
                <button dojoType="dijit.form.Button"
                  onClick='ESRIUKTestHarness.identify'>Identify</button>   
                <button dojoType="dijit.form.Button"
                  onClick='ESRIUKTestHarness.zoomPrevious'>Previous Extent</button>                              
                <button dojoType="dijit.form.Button"
                  onClick='ESRIUKTestHarness.zoomFullExtent' title='Zooms map to full extent'>Full Extent</button>
            --%>                  
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.destroyMap' title='Completely destroys current map object.'>Destroy Map</button>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.addNewMap' title='Adds a new map object - only if destroy map has been called'>Add New Map</button>
                <div id="activeToolDropDown" dojoType="dijit.form.DropDownButton" title='Changes current map navigation state (current active tool).'>
                  <span>&nbsp;Set Active Tool&nbsp;</span>
                  <div dojoType="dijit.Menu">
                    <div dojoType="dijit.MenuItem" onClick="ESRIUKTestHarness.setActiveTool('default')">default</div>
                    <div dojoType="dijit.MenuItem" onClick="ESRIUKTestHarness.setActiveTool('userDefined')">userDefined</div>
                    <div dojoType="dijit.MenuItem" onClick="ESRIUKTestHarness.setActiveTool('selectNearby')">selectNearby</div>
                  </div>
                </div>
            </div>
            <div dojoType="dijit.layout.ContentPane" style="border: 1px solid #ccc; margin: 2px; padding: 2px">
              <div>
                <label for="setScaleBox">Scale:&nbsp;</label>
                <input id="setScaleBox" type="text" style="width: 60px;" value="50000" dojoType="dijit.form.TextBox"/>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.setScale' 
                  title='Sets map scale to closest scale matching value input here'>Set Scale</button>
                  &nbsp;|&nbsp;
                <label for="setZoomBox">Level:&nbsp;</label>
                <input id="setZoomBox" type="text" style="width: 20px;" value="5" dojoType="dijit.form.TextBox"/>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.setZoom' 
                  title='Sets map scale slider to level set here'>Set Level</button>
              </div>
              <hr style="border-color:#ccc; border-width: 1px 0px 0px 0px; height: 1px; width: 80%; color: #ccc; margin-top: 1px; margin-bottom: 1px;" />
              <div>
                <label for="setXBox">Easting:&nbsp;</label>
                <input id="setXBox" type="text" style="width: 50px;" value="400000" dojoType="dijit.form.TextBox"/>
                <label for="setYBox">Northing:&nbsp;</label>
                <input id="setYBox" type="text" style="width: 50px;" value="250000" dojoType="dijit.form.TextBox"/>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.zoomToLocation' 
                  title='Zooms map to location defined in the above x & y inputs.'>Zoom To Location</button>
              </div>
              <hr style="border-color:#ccc; border-width: 1px 0px 0px 0px; height: 1px; width: 80%; color: #ccc; margin-top: 1px; margin-bottom: 1px;" />
              <div>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.zoomToLocationAndScale' 
                  title='Zooms map to location defined in the above x, y & scale inputs.'>Zoom To Location & Scale</button>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.zoomToLocationAndZoom' 
                  title='Zooms map to location defined in the above x, y & zoom level inputs.'>Zoom To Location & Level</button>                      
              </div>                  
              <hr style="border-color:#ccc; border-width: 1px 0px 0px 0px; height: 1px; width: 80%; color: #ccc; margin-top: 1px; margin-bottom: 1px;" />
              <div>
                <label for="setLocationTextBox">Text: </label>
                <input id="setLocationTextBox" type="text" style="width: 100px;" value="My house"
                    dojoType="dijit.form.TextBox" />
                <br />
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.zoomToLocationAndScaleText' 
                  title='Zooms map to location defined in the above x, y & scale inputs and adds the text in an infoWindow at the specified point.'>Zoom To Location, Scale &amp; Text</button>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.zoomToLocationAndLevelText' 
                  title='Zooms map to location defined in the above x, y & zoom level inputs and adds the text in an infoWindow at the specified point.'>Zoom To Location, Level &amp; Text</button>                      
              </div>
            </div>                                
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
                <label for="junctionToidA">TOID A: </label>
                <input id="junctionToidA" type="text" style="width: 100px;" value="4000000018035448" dojoType="dijit.form.TextBox"/>
                <label for="junctionToidB">TOID B: </label>
                <input id="junctionToidB" type="text" style="width: 100px;" value="4000000018035447" dojoType="dijit.form.TextBox"/>
                <br />
                <label for="junctionToidLevel">Level:&nbsp;</label>
                <input id="junctionToidLevel" type="text" style="width: 25px;" value="10" dojoType="dijit.form.TextBox"/>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.findJunctionPoint' 
                  title='Zooms map to Junction between two defined roads.'>Zoom to Junction</button>
                <hr style="border-color:#ccc; border-width: 1px 0px 0px 0px; height: 1px; width: 80%; color: #ccc; margin-top: 1px; margin-bottom: 1px;" />                      
                <label for="itnToid">TOID: </label>
                <input id="itnToid" type="text" style="width: 100px;" value="4000000027830824" dojoType="dijit.form.TextBox"/>
                <label for="itnLevel">Level: </label>
                <input id="itnLevel" type="text" style="width: 25px;" value="10" dojoType="dijit.form.TextBox"/>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.findITNNodePoint' 
                  title='Zooms map to ITN Node point.'>Zoom to ITN Node</button>
                <hr style="border-color:#ccc; border-width: 1px 0px 0px 0px; height: 1px; width: 80%; color: #ccc; margin-top: 1px; margin-bottom: 1px;" />                     
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
                <label for="printImageWidth">Width: </label>
                <input id="printImageWidth" type="text" style="width: 25px;" value="750" dojoType="dijit.form.TextBox"/>
                <label for="printImageHeight">Height: </label>
                <input id="printImageHeight" type="text" style="width: 25px;" value="500" dojoType="dijit.form.TextBox"/>
                <label for="printImageDPI">DPI: </label>
                <input id="printImageDPI" type="text" style="width: 25px;" value="196" dojoType="dijit.form.TextBox"/>                    
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.getPrintImage' 
                  title='Gets a print image.'>Print Image</button>
                <hr style="border-color:#ccc; border-width: 1px 0px 0px 0px; height: 1px; width: 80%; color: #ccc; margin-top: 1px; margin-bottom: 1px;" />                      
                <label for="cyclePrintScale">Scale: </label>
                <input id="cyclePrintScale" type="text" style="width: 60px;" value="20000" dojoType="dijit.form.TextBox"/>    
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.getNumberOfCycleImages' 
                  title='Gets number of cycle images at given scale.'>Number of Cycle Images</button>    
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.getCyclePrintDetails' 
                  title='Gets Cycle Print Details.'>Get Cycle Print Details</button>                                   
            </div>                
            <div dojoType="dijit.layout.ContentPane" style="border: 1px solid #ccc; margin: 2px; padding: 2px">
                <form dojoType="dijit.form.Form" id="myForm" onSubmit="ESRIUKTestHarness.setTravelNewsFilter">
                <span style="font-weight:bold;">Transport Type:&nbsp;</span>
                <input dojoType="dijit.form.RadioButton" type="radio" name="tType" id="tTypeAll" value="all"  checked="checked" />
                <label for="tTypeAll">All</label>
                <input dojoType="dijit.form.RadioButton" type="radio" name="tType" id="tTypeRoad" value="road"/>
                <label for="tTypeRoad">Road</label>
                <input dojoType="dijit.form.RadioButton" type="radio" name="tType" id="tTypePublic" value="public"/>
                <label for="tTypePublic">Public Transport</label>
                <input dojoType="dijit.form.RadioButton" type="radio" name="tType" id="tTypeNone" value="none"/>
                <label for="tTypeNone">None</label>
                <br />
                <span style="font-weight:bold;">Severity:&nbsp;</span>
                <input dojoType="dijit.form.RadioButton" type="radio" name="severity" id="delaysAll" value="all"  checked="checked" />
                <label for="delaysAll">All</label>
                <input dojoType="dijit.form.RadioButton" type="radio" name="severity" id="delaysMajor" value="major"/>
                <label for="delaysMajor">Major</label>
                <br />
                <span style="font-weight:bold;">Incident Type:&nbsp;</span>
                <input dojoType="dijit.form.RadioButton" type="radio" name="incidentType" id="incidentTypeAll" value="all"  checked="checked" />
                <label for="delaysAll">All</label>
                <input dojoType="dijit.form.RadioButton" type="radio" name="incidentType" id="incidentTypePlanned" value="planned"/>
                <label for="delaysMajor">Planned</label>
                <input dojoType="dijit.form.RadioButton" type="radio" name="incidentType" id="incidentTypeUnplanned" value="unplanned"/>
                <label for="delaysRecent">Unplanned</label>
                <br />    
                <span style="font-weight:bold;">Time Period:&nbsp;</span>
                <input dojoType="dijit.form.RadioButton" type="radio" name="timePeriod" id="timePeriodCurrent" value="current" checked="checked" />
                <label for="delaysRecent">Current</label>                    
                <input dojoType="dijit.form.RadioButton" type="radio" name="timePeriod" id="timePeriodRecent" value="recent"/>
                <label for="delaysRecent">Recent</label>
                <input dojoType="dijit.form.RadioButton" type="radio" name="timePeriod" id="timePeriodDate" value="date"/>
                <label for="delaysRecent">Date</label>
                <input dojoType="dijit.form.RadioButton" type="radio" name="timePeriod" id="timePeriodDateTime" value="datetime"/>
                <label for="delaysRecent">Date Time</label>                                        
                <br />
                <span style="font-weight:bold;">Date Time:&nbsp;</span>
                <input dojoType="dijit.form.TextBox" type="text" name="travelNewsDate" id="travelNewsDate" value="" />
                <script type="text/javascript">
                  //  Self-calling function - adds current date to the input box above
                  setTimeout(function() {

                    //var str = d.getDay() + '/' + d.getMonth() + '/' + d.getFullYear();
                    var d = new Date(),
                      hours = d.getHours(),
                      min = d.getMinutes(),
                      str = '8/10/09';  //  for testing
                    str += ' ' + (hours.legth == 1 ? '0' : '') + hours + ':' + (min.legth == 1 ? '0' : '') + min;
                    var elm = document.getElementById('travelNewsDate');
                    elm.value = str;
                  }, 3500);
                </script>                
                <span dojoType="dijit.Tooltip" connectId="travelNewsDate" toggle="explode">Only valid when <strong>Time Period</strong> is set to <strong>date -or- date time</strong></span>
                <button dojoType="dijit.form.Button" type="submit"
                  title='Gathers travel news inputs and applies to map.'>Update Map</button>
              </form>
            </div>                          
            <div dojoType="dijit.layout.ContentPane" style="border: 1px solid #ccc; margin: 2px; padding: 2px">
                <input dojoType="dijit.form.RadioButton" type="radio" name="marker" id="markerStart" value="start" checked="checked">
                <label for="markerStart">Start</label>
                <input dojoType="dijit.form.RadioButton" type="radio" name="marker" id="markerVia" value="via"/>
                <label for="markerVia">Via</label>
                <input dojoType="dijit.form.RadioButton" type="radio" name="marker" id="markerEnd" value="end"/>
                <label for="markerEnd">End</label>
                <input dojoType="dijit.form.RadioButton" type="radio" name="marker" id="markerSymbol" value="symbol"/>
                <label for="markerSymbol">Symbol</label>&nbsp;                    
                <label for="setMarkerSymbolType">- Type: </label>
                <input id="setMarkerSymbolType" type="text" style="width: 75px;" value="TRIANGLE"
                    dojoType="dijit.form.TextBox" />  
                <span dojoType="dijit.Tooltip" connectId="setMarkerSymbolType" toggle="explode">Symbol Types = CIRCLE | SQUARE | TRIANGLE | DIAMOND | PUSHPIN | INVALID</span>                      
                <br />
                <label for="setMarkerXBox">Easting: </label>
                <input id="setMarkerXBox" type="text" style="width: 60px;" value="400000"
                    dojoType="dijit.form.TextBox"/>
                <label for="setMarkerYBox">Northing: </label>
                <input id="setMarkerYBox" type="text" style="width: 60px;" value="250000"
                    dojoType="dijit.form.TextBox"/>
                <br />
                <label for="setMarkerTextBox">Text: </label>
                <input id="setMarkerTextBox" type="text" style="width: 100px;" value="Start Here!"
                    dojoType="dijit.form.TextBox" />
                <label for="setMarkerAddInfoWindow">AddInfoWindow: </label>
                <input id="setMarkerAddInfoWindow" type="checkbox" checked="checked" dojoType="dijit.form.CheckBox" /> 
                &nbsp;
                <br />
                <label for="setMarkerMain">Main: </label>
                <input id="setMarkerMain" type="checkbox" checked="checked" dojoType="dijit.form.CheckBox" /> 
                &nbsp;                
                <label for="setMarkerContent">Content: </label>
                <input id="setMarkerContent" type="text" style="width: 100px;" value="this is content" dojoType="dijit.form.TextBox" />                                                     
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.addMarker'>Add</button>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.clearPoints'>Clear</button>
            </div>
            <div dojoType="dijit.layout.ContentPane" style="border: 1px solid #ccc; margin: 2px; padding: 2px">
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.getLayerList'>Get Layer List</button>
                <button dojoType="dijit.form.Button" onClick='ESRIUKTestHarness.setLayerList'>Update Map</button>
                <div id="layerListContent" style="border: 1px solid #ccc; margin: 2px; padding: 2px"></div>
            </div>
            <%--               
            <div dojoType="dijit.layout.ContentPane" style="border: 1px solid #ccc; margin: 2px; padding: 2px">
                <strong>Results</strong>
                <div dojoType="dijit.layout.ContentPane" style="border: 1px solid #ccc; margin: 2px; padding: 2px">
                    <div id="contentPlacer"></div>
                </div>
            </div> 
            --%>                
        </div>
    </div>
		<%-- START: Map control required tags --%>
    <script type="text/javascript" src="./esriMap/ESRIJSAPIv1.5/default.ashx?language=en"></script>
		<%-- END: Map control required tags --%>        
		<script type="text/javascript" src="./script/TestHarness/ESRIUKTestHarness.js"></script>
</body>
</html>
