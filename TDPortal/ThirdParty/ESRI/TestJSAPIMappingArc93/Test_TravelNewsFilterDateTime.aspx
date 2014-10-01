<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_TravelNewsFilterDateTime.aspx.cs" Inherits="TestJSAPIMappingArc93.Test_TravelNewsFilterDateTime" %>

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
              param_TravelNews="transportType:all,incidentType:all,severity:all,timePeriod:datetime,datetime:08/10/09 12/00"
              param_Tools="selectNearby,userDefined,zoom,pan"
              param_Width="776" param_Height="540" param_Scale="250000" 
              param_LocationX="520000" param_LocationY="180000">
          </div>
          <%-- END: Map control required tags --%>
        </div>
        <div dojoType="dijit.layout.ContentPane" region="trailing" style="width: 350px; font-size: 12px;" id="testHarnessCode">
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
              
              <div style="margin-top: 15px; font-size: 13px;">
                Default travel news set for page load:<br />
                <code style="color:Blue">
                  param_TravelNews= "transportType:all, incidentType:all, severity:all, timePeriod:datetime, datetime:08/10/09 12/00"
                </code>
              </div>
            </div>                                    
        </div>
    </div>
		<%-- START: Map control required tags --%>
    <script type="text/javascript" src="./esriMap/ESRIJSAPIv1.5/default.ashx?language=en"></script>
		<%-- END: Map control required tags --%>        
		<script type="text/javascript" src="./script/TestHarness/ESRIUKTestHarness.js"></script>
</body>
</html>
