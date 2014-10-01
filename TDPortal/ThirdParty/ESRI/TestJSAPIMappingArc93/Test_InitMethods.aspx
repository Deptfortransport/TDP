<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_InitMethods.aspx.cs" Inherits="TestJSAPIMappingArc93.Test_InitMethods" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
  Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TDP Test Harness - Init Methods, ASP.NET AJAX</title>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="Pragma" content="no-cache" />
		<meta http-equiv="cache-control" content="no-cache" />	
		<script type="text/javascript" src="script/TestHarness/APIHandler.js"></script>		
		<%-- START: Test harness css classes --%>
	  <style type="text/css">
	  .ajaxAccordian{
	      width: 450px;
	      height: 700px;
	      border: solid 1px #CCCCCC;
	      font-family: Arial,Sans-Serif;
	      position:absolute;
	      left:0px;
	      top:5px;
	  }
	  .ajaxAccordian *{ 
	    font-family: Arial,Sans-Serif;
	  }
		.ajaxAccordianHeader{
		  background: #7375FC url(./images/bg-menu-main.png) repeat-x top left;
		  height: 20px;
		  padding: 5px;
		  border-bottom: 1px solid #CCCCCC;
		  color: White;
		  font-weight: bold;
		  cursor: pointer;
		}
		.ajaxAccordianBody{
		  padding: 5px;
		}
	  .ajaxAccordianBody *{
		  font-size: 11px;
		}
		.ajaxInline{
		  display: inline;
		}
		.ajaxInputText{
		  font-size: 11px;
		}
		.ajaxInputBox{
		  border: solid 1px #CCCCCC;
		}
		.ajaxInnerPanel{
		  margin: 2px;
		  padding: 2px;
		  border: solid 1px #CCCCCC;
		}
		.ajaxCheckBox{
		  margin-bottom: -4px;
		}
		.ajaxCheckBox div{
		  top: 4px;
		}
		.link{
		  font-size: 10px; 
		  cursor: pointer;
		  color: Blue;
		}
		.link:hover{
		  text-decoration: underline;
		}
		
		</style>
		<%-- END: Test harness css classes --%>
</head>
<body>
    <a href="Links.aspx" title="Back to links page" style="display:block;font-size:8px">Back to links page</a>
  <form runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
    <%-- Test harness JavaScript functions --%>    
		<script type="text/javascript">

		  //  test harness global div id
		  var mapId = '';
		  var mapFrame = parent.frames['mapFrame'].window;

		  var getMap = function() {
		    var map = null;
	      if (mapId != '') {
	          map = mapFrame.document.getElementById(mapId);
	      }
	      if (map == null) {
	        var holder = mapFrame.dojo.byId('placeHolder');
	        map = holder.appendChild(mapFrame.document.createElement('div'));
	      }
		    return map;
		  };
		  var destroyMap = function() {
	      if (mapId != '') {
	        mapFrame.dijit.byId(mapId).destroyRecursive();
	      }
		  };
		  var setMapId = function(id) {
		    mapFrame.ESRIUKTestHarness.mapDijit = id;
		    mapId = id;
		  }

		  //  this method initiates a new map at a given location and scale (or slider zoom level)
		  var openMapAtLocationAndScale = function() {
		    //  attempt to destroy current map on the page - if any
		    destroyMap();
		    //  attempt to set new map with new properties
		    try {
		      //  gather relevant inputs
		      var useScale = document.getElementById("<%= LocationPointAndScale_scale.ClientID %>"),
        scale = document.getElementById("<%= scaleSliderValue.ClientID %>"),
        level = document.getElementById("<%= levelSliderValue.ClientID %>"),
        easting = document.getElementById("<%= eastingTextBox.ClientID %>"),
        northing = document.getElementById("<%= northingTextBox.ClientID %>"),
        mapPlaceHolder = getMap();
		      //  init new map with appropriate new settings
		      var esriMap = new mapFrame.ESRIUK.Dijits.Map({
		        //  css class name
		        'class': 'tundra',
		        //  easting
		        param_LocationX: easting.value,
		        //  northing
		        param_LocationY: northing.value,
		        //  use scale - if specified, or pass in null for this argument
		        param_Scale: mapFrame.dojo.attr(useScale, 'checked') ? scale.value : null,
		        //  slider zoom level
		        param_Level: level.value
		      },
        mapPlaceHolder  //  div to use as map 
      );
		      //  start map
		      esriMap.startup();
		      //  store global id of map - only for test harness
		      setMapId(esriMap.id);
		    } catch (err) {
		    alert('Method failed: ' + err.message);
		    }
		  }
		  //  this method initiates a new map at a given extent
		  var openMapAtLocationExtent = function() {
		    //  attempt to destroy current map on the page - if any
		  destroyMap();
		  //  attempt to set new map with new properties
		  try {
		    //  gather relevant inputs
		      var xmin = document.getElementById("<%= minXTextBox.ClientID %>"),
		        ymin = document.getElementById("<%= minYTextBox.ClientID %>"),
		        xmax = document.getElementById("<%= maxXTextBox.ClientID %>"),
		        ymax = document.getElementById("<%= maxYTextBox.ClientID %>"),
		        mapPlaceHolder = getMap();
		        //  init new map with appropriate new settings
		      var esriMap = new mapFrame.ESRIUK.Dijits.Map({ 
		            //  css class
		            'class': 'tundra',
		            //  xmin
		            param_XMin: xmin.value,
		            //  ymin
		            param_YMin: ymin.value,
		            //  xmax
		            param_XMax: xmax.value,
		            //  ymax
		            param_YMax: ymax.value
		          },
		          mapPlaceHolder  //  div to use as map 
		        );
		        //  start map
		        esriMap.startup();
		        //  store global id of map - only for test harness
		        setMapId(esriMap.id);
		      } catch (err) {
		      alert('Method failed: ' + err.message);
		      }
		    };
		  //  this method initiates a new map with a set plan a journey text stored in client memory
		  var openMapWithText = function(){
		    //  attempt to destroy current map on the page - if any
		  destroyMap();
		    //  attempt to set new map with new properties
		    try {
		      //  gather relevant inputs
		      var text = document.getElementById("<%= planAJourneyText.ClientID %>"),
		        mapPlaceHolder = getMap();
		      //  init new map with appropriate new settings
		      var esriMap = new mapFrame.ESRIUK.Dijits.Map({
		          //  css class
		          'class': 'tundra',
		          //  plan a journey text
		          param_Text: text.value,
		          //  available tools  - for testing purposes in test harness only
		          param_Tools: 'userDefined'
		        },
	          mapPlaceHolder  //  div to use as map 
	        );
	        //  start map
	        esriMap.startup();
	        //  store global id of map - only for test harness
	        setMapId(esriMap.id);
	      } catch (err) {
	      alert('Method failed: ' + err.message);
	      }
	    };
		  //  this method initiates the map with a set of specified tools available to the user in a toolbar
		  var openMapTools = function() {
		    //  attempt to destroy current map on the page - if any
		  destroyMap();
		    //  attempt to set new map with new properties
		    try {
		      //  gather relevant inputs
		      var toolSelection = '',
		        userDefined = document.getElementById("<%= toolUserDefinedPoint.ClientID %>"),
		        selectNearby = document.getElementById("<%= toolSelectNearby.ClientID %>"),
		        mapPlaceHolder = getMap();
		      //  construct tool selection input string (comma seperated array of tool names)
		      if (mapFrame.dojo.attr(userDefined, 'checked')) {
		        toolSelection += 'userDefined';
		        if (mapFrame.dojo.attr(selectNearby, 'checked')) {
		          toolSelection += ',selectNearby';
		        }
		      } else if (mapFrame.dojo.attr(selectNearby, 'checked')) {
		        toolSelection += 'selectNearby';
		      }
		      //  init new map with appropriate new settings
		      var esriMap = new mapFrame.ESRIUK.Dijits.Map({
		          //  css class
		          'class': 'tundra',
		          //  comma seperated list of tools available
		          param_Tools: toolSelection
		        },
		        mapPlaceHolder  //  div to use as map 
		      );
	        //  start map
	        esriMap.startup();
	        //  store global id of map - only for test harness
	        setMapId(esriMap.id);
	      } catch (err) {
	      alert('Method failed: ' + err.message);
	      }
	    };
	    //  this method initiates a new map at a given extent and available list of tools
	    var openMapAtLocationExtentAndToolsCombo = function() {
	      //  attempt to destroy current map on the page - if any
	    destroyMap();
	      //  attempt to set new map with new properties
	      try {
	        //  gather relevant inputs
	        var toolSelection = '',
		        userDefined = document.getElementById("<%= toolUserDefinedPoint_Combo.ClientID %>"),
		        selectNearby = document.getElementById("<%= toolSelectNearby_Combo.ClientID %>"),
		        xmin = document.getElementById("<%= minXTextBox_Combo.ClientID %>"),
		        ymin = document.getElementById("<%= minYTextBox_Combo.ClientID %>"),
		        xmax = document.getElementById("<%= maxXTextBox_Combo.ClientID %>"),
		        ymax = document.getElementById("<%= maxYTextBox_Combo.ClientID %>"),
		        mapPlaceHolder = getMap();
	        //  construct tool selection input string (comma seperated array of tool names)
	        if (mapFrame.dojo.attr(userDefined, 'checked')) {
	          toolSelection += 'userDefined';
	          if (mapFrame.dojo.attr(selectNearby, 'checked')) {
	            toolSelection += ',selectNearby';
	          }
	        } else if (mapFrame.dojo.attr(selectNearby, 'checked')) {
	          toolSelection += 'selectNearby';
	        }		        
	        //  init new map with appropriate new settings
	        var esriMap = new mapFrame.ESRIUK.Dijits.Map({
	            //  css class
	            'class': 'tundra',
	            //  xmin
	            param_XMin: xmin.value,
	            //  ymin
	            param_YMin: ymin.value,
	            //  xmax
	            param_XMax: xmax.value,
	            //  ymax
	            param_YMax: ymax.value,
	            //  comma seperated list of tools available
	            param_Tools: toolSelection	          
	          },
		          mapPlaceHolder  //  div to use as map 
		        );
	        //  start map
	        esriMap.startup();
	        //  store global id of map - only for test harness
	        setMapId(esriMap.id);
	      } catch (err) {
	      alert('Method failed: ' + err.message);
	      }
	    };
	    //  this method initiates a new map at a given location and scale (or slider zoom level)
	    //  and sets plan a journey text stored in client memory
	    var openMapAtLocationAndScaleAndTextCombo = function() {
	      //  attempt to destroy current map on the page - if any
	    destroyMap();
	      //  attempt to set new map with new properties
	      try {
	        //  gather relevant inputs
	        var useScale = document.getElementById("<%= LocationPointAndScale_scale_Combo.ClientID %>"),
            scale = document.getElementById("<%= scaleSliderValue_Combo.ClientID %>"),
            level = document.getElementById("<%= levelSliderValue_Combo.ClientID %>"),
            easting = document.getElementById("<%= eastingTextBox_Combo.ClientID %>"),
            northing = document.getElementById("<%= northingTextBox_Combo.ClientID %>"),
            text = document.getElementById("<%= planAJourneyText_Combo.ClientID %>"),
            mapPlaceHolder = getMap();
	        //  init new map with appropriate new settings
	        var esriMap = new mapFrame.ESRIUK.Dijits.Map({
	          //  css class name
	          'class': 'tundra',
	          //  easting
	          param_LocationX: easting.value,
	          //  northing
	          param_LocationY: northing.value,
	          //  use scale - if specified, or pass in null for this argument
	          param_Scale: mapFrame.dojo.attr(useScale, 'checked') ? scale.value : null,
	          //  slider zoom level
	          param_Level: level.value,
	          //  plan a journey text
	          param_Text: text.value,
	          //  available tools  - for testing purposes in test harness only
	          param_Tools: 'userDefined'
	        },
            mapPlaceHolder  //  div to use as map 
          );
	        //  start map
	        esriMap.startup();
	        //  store global id of map - only for test harness
	        setMapId(esriMap.id);
	      } catch (err) {
	      alert('Method failed: ' + err.message);
	      }
	    };
	    var openAllOptions = function() {
	      //  attempt to destroy current map on the page - if any
	      destroyMap();
	      //  attempt to set new map with new properties
	      try {
	        //  gather relevant inputs
	        var toolSelection = '',
            useExtent = document.getElementById("<%= useMapExtent_all.ClientID %>"),
		        userDefined = document.getElementById("<%= toolUserDefinedPoint_All.ClientID %>"),
		        selectNearby = document.getElementById("<%= toolSelectNearby_All.ClientID %>"),
		        xmin = document.getElementById("<%= minXTextBox_All.ClientID %>"),
		        ymin = document.getElementById("<%= minYTextBox_All.ClientID %>"),
		        xmax = document.getElementById("<%= maxXTextBox_All.ClientID %>"),
		        ymax = document.getElementById("<%= maxYTextBox_All.ClientID %>"),
            useScale = document.getElementById("<%= LocationPointAndScale_scale_All.ClientID %>"),
            scale = document.getElementById("<%= scaleSliderValue_All.ClientID %>"),
            level = document.getElementById("<%= levelSliderValue_All.ClientID %>"),
            easting = document.getElementById("<%= eastingTextBox_All.ClientID %>"),
            northing = document.getElementById("<%= northingTextBox_All.ClientID %>"),
            text = document.getElementById("<%= planAJourneyText_All.ClientID %>"),
            width = document.getElementById("<%= width_All.ClientID %>"),
            height = document.getElementById("<%= height_All.ClientID %>"),
            mode = document.getElementById("<%= setMapModeText.ClientID %>"),
            tNews = document.getElementById("<%= setTravelNewsFilter.ClientID %>"),
            symbols = document.getElementById("<%= setMapSymbolsText.ClientID %>"),
            mapPlaceHolder = getMap(),
            obj = {};
	        //  check location start params
	        if (mapFrame.dojo.attr(useExtent, 'checked')) {
	          obj = {
	            //  css class name
	            'class': 'tundra',
	            //  xmin
	            param_XMin: xmin.value,
	            //  ymin
	            param_YMin: ymin.value,
	            //  xmax
	            param_XMax: xmax.value,
	            //  ymax
	            param_YMax: ymax.value
	          };
	        } else {
	          obj = {
	            //  css class name
	            'class': 'tundra',
	            param_LocationX: easting.value,
	            //  northing
	            param_LocationY: northing.value,
	            //  use scale - if specified, or pass in null for this argument
	            param_Scale: mapFrame.dojo.attr(useScale, 'checked') ? scale.value : null,
	            //  slider zoom level
	            param_Level: level.value
	          };
	        }
	        //  construct tool selection input string (comma seperated array of tool names)
	        if (mapFrame.dojo.attr(userDefined, 'checked')) {
	          toolSelection += 'userDefined';
	          if (mapFrame.dojo.attr(selectNearby, 'checked')) {
	            toolSelection += ',selectNearby';
	          }
	        } else if (mapFrame.dojo.attr(selectNearby, 'checked')) {
	          toolSelection += 'selectNearby';
	        }
	        //  add symbols text
	        obj.param_Symbols = symbols.value;
	        //  add travel news json
	        obj.param_TravelNews = tNews.value;
	        //  add map mode text
	        obj.param_Mode = mode.value;
	        //  comma seperated list of tools available
	        obj.param_Tools = toolSelection;
	        //  plan a journey text
	        obj.param_Text = text.value;
	        //  map width
	        obj.param_Width = width.value;
	        //  map height
	        obj.param_Height = height.value;
	        //  init new map with appropriate new settings
	        var esriMap = new mapFrame.ESRIUK.Dijits.Map(obj, mapPlaceHolder);
	        //  start map
	        esriMap.startup();
	        //  store global id of map - only for test harness
	        setMapId(esriMap.id);
	      } catch (err) {
	        alert('Method failed: ' + err.message);
	      }
	    }
		</script>
    <%-- Floating input accordian control --%>
    <cc1:Accordion ID="userInputs" runat="server" Enabled="true"
    HeaderCssClass="ajaxAccordianHeader" ContentCssClass="ajaxAccordianBody" CssClass="ajaxAccordian"
    AutoSize="Fill" FadeTransitions="true" TransitionDuration="250" SelectedIndex="6"
    SuppressHeaderPostbacks="true" FramesPerSecond="40">
      <Panes>
        <%-- UC 1.6 - Location point and scale --%>
        <cc1:AccordionPane ID="AccordionPane1" runat="server">
          <Header>UC 1.6 - Location point and scale</Header>
          <Content>
            <%-- Panel Description --%>
            <p>
              This panel initiates a new map at a given location and scale (or slider zoom level)
            </p>
            <%-- Scale Slider Input --%>
            <asp:Panel ID="Panel3" runat="server" CssClass="ajaxInnerPanel">
              <asp:CheckBox ID="LocationPointAndScale_scale" runat="server" Checked="true" />
              <cc1:MutuallyExclusiveCheckBoxExtender ID="MutuallyExclusiveCheckBoxExtender1" TargetControlID="LocationPointAndScale_scale" 
                Key="LocationPointAndScale" runat="server" />
              <asp:Label ID="Label9" runat="server" Text="Use Scale Value" CssClass="ajaxInputText"></asp:Label>
              <br />
              <asp:Label ID="Label5" runat="server" Text="Choose Scale : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="scaleSliderValue" runat="server" Text="200000" CssClass="ajaxInline ajaxInputBox" Width="75px"></asp:TextBox>
            </asp:Panel>            
            <%-- Level Slider Input --%>
            <asp:Panel ID="Panel2" runat="server" CssClass="ajaxInnerPanel">
              <asp:CheckBox ID="LocationPointAndScale_label" runat="server" />
              <cc1:MutuallyExclusiveCheckBoxExtender ID="MutuallyExclusiveCheckBoxExtender2" TargetControlID="LocationPointAndScale_label" 
                Key="LocationPointAndScale" runat="server" />
              <asp:Label ID="Label10" runat="server" Text=" Use Slider Level" CssClass="ajaxInputText"></asp:Label>            
              <br />
              <asp:Label runat="server" Text="Choose Slider Level : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="levelSliderValue" runat="server" Text="5" CssClass="ajaxInline ajaxInputBox" Width="25px"></asp:TextBox>
            </asp:Panel>
            <%-- BNG Coords Number input --%>
            <asp:Panel ID="Panel4" runat="server" CssClass="ajaxInnerPanel">
              <asp:Label ID="Label6" runat="server" Text="BNG Easting : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="eastingTextBox" runat="server" Text="400000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>
              <asp:Literal ID="Literal1" runat="server">&nbsp;</asp:Literal>
              <asp:Label ID="Label7" runat="server" Text="  BNG Northing : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="northingTextBox" runat="server" Text="400000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>          
            </asp:Panel>
            <%-- Generate Map Submit Button --%>
            <div>
              <input id="Button2" type="button" value="Generate Map" onclick="openMapAtLocationAndScale();return false;" /> 
            </div>
          </Content>
        </cc1:AccordionPane>
        <%-- UC 1.6 - Location extent --%>
        <cc1:AccordionPane ID="AccordionPane2" runat="server">
          <Header>UC 1.6 - Location extent</Header>
          <Content>
            <%-- Panel Description --%>
            <p>
              This panel initiates a new map at a given extent
            </p>
            <%-- BNG Coords Number input --%>
            <asp:Panel ID="Panel6" runat="server" CssClass="ajaxInnerPanel">
              <asp:Label ID="Label3" runat="server" Text="MinX : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="minXTextBox" runat="server" Text="300000" CssClass="ajaxInline ajaxInputBox" Width="100px" ></asp:TextBox>
              <asp:Literal ID="Literal2" runat="server">&nbsp;</asp:Literal>
              <asp:Label ID="Label4" runat="server" Text="MinY : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="minYTextBox" runat="server" Text="200000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>
              <br />  
              <asp:Label ID="Label1" runat="server" Text="MaxX : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="maxXTextBox" runat="server" Text="500000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>
              <asp:Literal ID="Literal3" runat="server">&nbsp;</asp:Literal>
              <asp:Label ID="Label2" runat="server" Text="MaxY : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="maxYTextBox" runat="server" Text="400000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>                        
            </asp:Panel>
            <%-- Generate Map Submit Button --%>
            <div>
              <input id="Button1" type="button" value="Generate Map" onclick="openMapAtLocationExtent();return false;" /> 
            </div>
          </Content>
        </cc1:AccordionPane>
        <%-- UC 1.6 - Plan a journey text --%>       
        <cc1:AccordionPane ID="AccordionPane3" runat="server">
          <Header>UC 1.6 - Plan a journey text</Header>
          <Content>
            <%-- Panel Description --%>
            <p>
              This panel initiates a new map with a pre-defined option (link )to show the user when they have added a new point to the map
            </p>          
            <%-- Plan a journey text --%>
            <asp:Panel ID="Panel1" runat="server" CssClass="ajaxInnerPanel">
              <asp:Label ID="Label8" runat="server" Text="Text : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="planAJourneyText" runat="server" Text="Plan a cycle journey" CssClass="ajaxInline ajaxInputBox" Width="200px"></asp:TextBox>                          
            </asp:Panel>
            <%-- Generate Map Submit Button --%>
            <div>            
              <input id="Button3" type="button" value="Generate Map" onclick="openMapWithText();return false;" /> 
            </div>
          </Content>
        </cc1:AccordionPane>
        <%-- UC 1.6 - Tools List --%>         
        <cc1:AccordionPane ID="AccordionPane4" runat="server">
          <Header>UC 1.6 - Tools List</Header>
          <Content>
            <%-- Panel Description --%>
            <p>
              This panel initiates the map with a set of tools made available to the user in the top left toolbar panel
            </p>              
            <%-- Tools List --%>
            <asp:Panel ID="Panel5" runat="server" CssClass="ajaxInnerPanel">
              <asp:CheckBox ID="toolUserDefinedPoint" runat="server" CssClass="ajaxInline ajaxCheckBox" Text="User Defined Point" />
              <cc1:ToggleButtonExtender ID="ToggleButtonExtender1" runat="server" 
                TargetControlID="toolUserDefinedPoint" ImageHeight="16" ImageWidth="16" 
                CheckedImageAlternateText="Enabled" UncheckedImageAlternateText="Disabled"
                CheckedImageUrl="~/images/stock_calc-accept.png" UncheckedImageUrl="~/images/stock_calc-cancel.png" />
              <br />
              <asp:CheckBox ID="toolSelectNearby" runat="server" CssClass="ajaxInline ajaxCheckBox" Text="Select Nearby Point" />
              <cc1:ToggleButtonExtender ID="ToggleButtonExtender2" runat="server" 
                TargetControlID="toolSelectNearby" ImageHeight="16" ImageWidth="16" 
                CheckedImageAlternateText="Enabled" UncheckedImageAlternateText="Disabled"
                CheckedImageUrl="~/images/stock_calc-accept.png" UncheckedImageUrl="~/images/stock_calc-cancel.png" />                  
            </asp:Panel>
            <%-- Generate Map Submit Button --%>
            <div>                
              <input id="Button4" type="button" value="Generate Map" onclick="openMapTools();return false;" /> 
            </div>
          </Content>
        </cc1:AccordionPane>           
        <%-- UC 1.6 - Combo: Tools List & Location extent --%>         
        <cc1:AccordionPane ID="AccordionPane5" runat="server">
          <Header>UC 1.6 - Combo: Tools List & Location extent</Header>
          <Content>
            <%-- Panel Description --%>
            <p>
              This panel combines the functions of Location Extent & Tools list to use as inputs for initiating a map
            </p>
            <%-- BNG Coords Number input --%>
            <asp:Panel ID="Panel13" runat="server" CssClass="ajaxInnerPanel">
              <asp:Label ID="Label15" runat="server" Text="MinX : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="minXTextBox_Combo" runat="server" Text="300000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>
              <asp:Literal ID="Literal4" runat="server">&nbsp;</asp:Literal>
              <asp:Label ID="Label16" runat="server" Text="MinY : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="minYTextBox_Combo" runat="server" Text="200000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>
              <br />  
              <asp:Label ID="Label17" runat="server" Text="MaxX : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="maxXTextBox_Combo" runat="server" Text="500000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>
              <asp:Literal ID="Literal5" runat="server">&nbsp;</asp:Literal>
              <asp:Label ID="Label18" runat="server" Text="MaxY : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="maxYTextBox_Combo" runat="server" Text="400000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>
            </asp:Panel>                          
            <%-- Tools List --%>
            <asp:Panel ID="Panel11" runat="server" CssClass="ajaxInnerPanel">
              <asp:CheckBox ID="toolUserDefinedPoint_Combo" runat="server" CssClass="ajaxInline ajaxCheckBox" Text="User Defined Point" />
              <cc1:ToggleButtonExtender ID="ToggleButtonExtender3" runat="server" 
                TargetControlID="toolUserDefinedPoint_Combo" ImageHeight="16" ImageWidth="16" 
                CheckedImageAlternateText="Enabled" UncheckedImageAlternateText="Disabled"
                CheckedImageUrl="~/images/stock_calc-accept.png" UncheckedImageUrl="~/images/stock_calc-cancel.png" />
              <br />
              <asp:CheckBox ID="toolSelectNearby_Combo" runat="server" CssClass="ajaxInline ajaxCheckBox" Text="Select Nearby Point" />
              <cc1:ToggleButtonExtender ID="ToggleButtonExtender4" runat="server" 
                TargetControlID="toolSelectNearby_Combo" ImageHeight="16" ImageWidth="16" 
                CheckedImageAlternateText="Enabled" UncheckedImageAlternateText="Disabled"
                CheckedImageUrl="~/images/stock_calc-accept.png" UncheckedImageUrl="~/images/stock_calc-cancel.png" />                  
            </asp:Panel>
            <%-- Generate Map Submit Button --%>
            <div>                
              <input id="Button5" type="button" value="Generate Map" onclick="openMapAtLocationExtentAndToolsCombo();return false;" /> 
            </div>
          </Content>
        </cc1:AccordionPane>           
        <%-- UC 1.6 - Combo: Location Point and scale & Plan a journey text --%>         
        <cc1:AccordionPane ID="AccordionPane6" runat="server">
          <Header>UC 1.6 - Combo: Point and Text</Header>
          <Content>
            <%-- Panel Description --%>
            <p>
              This panel combines Location Point and scale & Plan a journey text as inputs for map initialisation
            </p>
            <%-- Scale Slider Input --%>
            <asp:Panel ID="Panel14" runat="server" CssClass="ajaxInnerPanel">
              <asp:CheckBox ID="LocationPointAndScale_scale_Combo" runat="server" Checked="true" />
              <cc1:MutuallyExclusiveCheckBoxExtender ID="MutuallyExclusiveCheckBoxExtender3" TargetControlID="LocationPointAndScale_scale_Combo" 
                Key="LocationPointAndScale_Combo" runat="server" />
              <asp:Label ID="Label19" runat="server" Text="Use Scale Value" CssClass="ajaxInputText"></asp:Label>
              <br />
              <asp:Label ID="Label20" runat="server" Text="Choose Scale : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="scaleSliderValue_Combo" runat="server" Text="200000" CssClass="ajaxInline ajaxInputBox" Width="75px"></asp:TextBox>
            </asp:Panel>            
            <%-- Level Slider Input --%>
            <asp:Panel ID="Panel15" runat="server" CssClass="ajaxInnerPanel">
              <asp:CheckBox ID="LocationPointAndScale_label_Combo" runat="server" />
              <cc1:MutuallyExclusiveCheckBoxExtender ID="MutuallyExclusiveCheckBoxExtender4" TargetControlID="LocationPointAndScale_label_Combo" 
                Key="LocationPointAndScale_Combo" runat="server" />
              <asp:Label ID="Label21" runat="server" Text=" Use Slider Level" CssClass="ajaxInputText"></asp:Label>            
              <br />
              <asp:Label ID="Label22" runat="server" Text="Choose Slider Level : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="levelSliderValue_Combo" runat="server" Text="5" CssClass="ajaxInline ajaxInputBox" Width="25px"></asp:TextBox>
            </asp:Panel>
            <%-- BNG Coords Number input --%>
            <asp:Panel ID="Panel17" runat="server" CssClass="ajaxInnerPanel">
              <asp:Label ID="Label24" runat="server" Text="BNG Easting : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="eastingTextBox_Combo" runat="server" Text="400000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>
              <asp:Literal ID="Literal6" runat="server">&nbsp;</asp:Literal>
              <asp:Label ID="Label25" runat="server" Text="  BNG Northing : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="northingTextBox_Combo" runat="server" Text="400000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>
            </asp:Panel>
            <%-- Plan a journey text --%>
            <asp:Panel ID="Panel18" runat="server" CssClass="ajaxInnerPanel">
              <asp:Label ID="Label26" runat="server" Text="Text : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="planAJourneyText_Combo" runat="server" Text="Plan a cycle journey" CssClass="ajaxInline ajaxInputBox" Width="200px"></asp:TextBox>                          
            </asp:Panel>            
            <%-- Generate Map Submit Button --%>
            <div>                
              <input id="Button6" type="button" value="Generate Map" onclick="openMapAtLocationAndScaleAndTextCombo();return false;" /> 
            </div>
          </Content>
        </cc1:AccordionPane>   
        <%-- UC 1.6 - Combo: Location Point and scale & Plan a journey text --%>         
        <cc1:AccordionPane ID="AccordionPane7" runat="server">
          <Header>UC 1.6 - All</Header>
          <Content>
            <%-- Panel Description --%>
            <p>
              All init combinations.
              Start map with an initial extent - OR - start with a location and scale, then add text, map dimensions and tools list options
            </p>
            <%-- Start map with extent --%>
            <asp:Panel ID="Panel24" runat="server" CssClass="ajaxInnerPanel">
              <%-- CheckBox --%>              
              <asp:CheckBox ID="useMapExtent_all" runat="server" Checked="true" />
              <cc1:MutuallyExclusiveCheckBoxExtender ID="MutuallyExclusiveCheckBoxExtender7" TargetControlID="useMapExtent_all" 
                Key="useMapExtent" runat="server" />
              <asp:Label ID="Label35" runat="server" Text="Start map with extent" CssClass="ajaxInputText"></asp:Label> 
              <br />      
              <asp:Label ID="Label37" runat="server" Text="MinX : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="minXTextBox_All" runat="server" Text="300000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>
              <asp:Literal ID="Literal8" runat="server">&nbsp;</asp:Literal>
              <asp:Label ID="Label38" runat="server" Text="MinY : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="minYTextBox_All" runat="server" Text="200000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>
              <br />  
              <asp:Label ID="Label39" runat="server" Text="MaxX : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="maxXTextBox_All" runat="server" Text="500000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>
              <asp:Literal ID="Literal9" runat="server">&nbsp;</asp:Literal>
              <asp:Label ID="Label40" runat="server" Text="MaxY : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="maxYTextBox_All" runat="server" Text="400000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>
            </asp:Panel>
            <%-- Start map with point & scale --%>
            <asp:Panel runat="server" CssClass="ajaxInnerPanel">
              <%-- CheckBox --%>
              <asp:CheckBox ID="useMapPoint_all" runat="server"/>
              <cc1:MutuallyExclusiveCheckBoxExtender ID="MutuallyExclusiveCheckBoxExtender8" TargetControlID="useMapPoint_all" 
                Key="useMapExtent" runat="server" />
              <asp:Label ID="Label36" runat="server" Text="Start map with point & scale" CssClass="ajaxInputText"></asp:Label>               
              <%-- Scale Slider Input --%>
              <asp:Panel ID="Panel19" runat="server" CssClass="ajaxInnerPanel">
                <asp:CheckBox ID="LocationPointAndScale_scale_All" runat="server" Checked="true" />
                <cc1:MutuallyExclusiveCheckBoxExtender ID="MutuallyExclusiveCheckBoxExtender5" TargetControlID="LocationPointAndScale_scale_All" 
                  Key="LocationPointAndScale_All" runat="server" />
                <asp:Label ID="Label27" runat="server" Text="Use Scale Value" CssClass="ajaxInputText"></asp:Label>
                <br />
                <asp:Label ID="Label28" runat="server" Text="Choose Scale : " CssClass="ajaxInline ajaxInputText"></asp:Label>
                <asp:TextBox ID="scaleSliderValue_All" runat="server" Text="200000" CssClass="ajaxInline ajaxInputBox" Width="75px"></asp:TextBox>
              </asp:Panel>            
              <%-- Level Slider Input --%>
              <asp:Panel ID="Panel20" runat="server" CssClass="ajaxInnerPanel">
                <asp:CheckBox ID="LocationPointAndScale_level_All" runat="server" />
                <cc1:MutuallyExclusiveCheckBoxExtender ID="MutuallyExclusiveCheckBoxExtender6" TargetControlID="LocationPointAndScale_level_All" 
                  Key="LocationPointAndScale_All" runat="server" />
                <asp:Label ID="Label29" runat="server" Text=" Use Slider Level" CssClass="ajaxInputText"></asp:Label>            
                <br />
                <asp:Label ID="Label30" runat="server" Text="Choose Slider Level : " CssClass="ajaxInline ajaxInputText"></asp:Label>
                <asp:TextBox ID="levelSliderValue_All" runat="server" Text="5" CssClass="ajaxInline ajaxInputBox" Width="25px"></asp:TextBox>
              </asp:Panel>
              <%-- BNG Coords Number input --%>
              <asp:Panel ID="Panel21" runat="server" CssClass="ajaxInnerPanel">
                <asp:Label ID="Label31" runat="server" Text="BNG Easting : " CssClass="ajaxInline ajaxInputText"></asp:Label>
                <asp:TextBox ID="eastingTextBox_All" runat="server" Text="400000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>
                <asp:Literal ID="Literal7" runat="server">&nbsp;</asp:Literal>
                <asp:Label ID="Label32" runat="server" Text="  BNG Northing : " CssClass="ajaxInline ajaxInputText"></asp:Label>
                <asp:TextBox ID="northingTextBox_All" runat="server" Text="400000" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>
              </asp:Panel>
            </asp:Panel>
            <%-- Misc --%>
            <asp:Panel ID="Panel22" runat="server" CssClass="ajaxInnerPanel">
              <%-- Map Mode --%>
              <asp:Label ID="Label43" runat="server" Text="Map Mode : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="setMapModeText" runat="server" Text="start,via,end" CssClass="ajaxInline ajaxInputBox" Width="100px"></asp:TextBox>                          
              <br />   
              <%-- Travel News --%>
              <asp:Label ID="Label44" runat="server" Text="Travel News Filter : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="setTravelNewsFilter" runat="server" Text="transportType:all,incidentType:all,severity:all,timePeriod:current,datetime:10/07/1981 10/43/22" CssClass="ajaxInline ajaxInputBox" Width="300px"></asp:TextBox>                          
              <br />   
              <%-- Map Symbols --%>
              <asp:Label ID="Label45" runat="server" Text="Map Symbols : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="setMapSymbolsText" runat="server" Text="[{x:450000,y:200000,label:'hello world',type:'start'},{x:451000,y:195000,label:'hello world 2',type:'via'},{x:451000,y:175000,label:'MY SYMBOL',type:'symbol',symbolKey:'TRIANGLE'},{x:443000,y:187000,label:'hello world 3',type:'end',main:true}]" CssClass="ajaxInline ajaxInputBox" Width="300px"></asp:TextBox>                          
              <br />                                      
              <%-- Plan a journey text --%>
              <asp:Label ID="Label33" runat="server" Text="Text : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="planAJourneyText_All" runat="server" Text="Plan a cycle journey" CssClass="ajaxInline ajaxInputBox" Width="200px"></asp:TextBox>                          
              <br />
              <%-- Map Dimensions --%>
              <asp:Label ID="Label41" runat="server" Text="Map Width : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="width_All" runat="server" Text="754" CssClass="ajaxInline ajaxInputBox" Width="70px"></asp:TextBox>
              <asp:Literal ID="Literal10" runat="server">&nbsp;</asp:Literal>
              <asp:Label ID="Label42" runat="server" Text="Map Height : " CssClass="ajaxInline ajaxInputText"></asp:Label>
              <asp:TextBox ID="height_All" runat="server" Text="500" CssClass="ajaxInline ajaxInputBox" Width="70px"></asp:TextBox>
              <br />
              <%-- Tools List --%>
              <asp:CheckBox ID="toolUserDefinedPoint_All" runat="server" CssClass="ajaxInline ajaxCheckBox" Text="User Defined Point" />
              <cc1:ToggleButtonExtender ID="ToggleButtonExtender5" runat="server" 
                TargetControlID="toolUserDefinedPoint_All" ImageHeight="16" ImageWidth="16" 
                CheckedImageAlternateText="Enabled" UncheckedImageAlternateText="Disabled"
                CheckedImageUrl="~/images/stock_calc-accept.png" UncheckedImageUrl="~/images/stock_calc-cancel.png" />
              <asp:CheckBox ID="toolSelectNearby_All" runat="server" CssClass="ajaxInline ajaxCheckBox" Text="Select Nearby Point" />
              <cc1:ToggleButtonExtender ID="ToggleButtonExtender6" runat="server" 
                TargetControlID="toolSelectNearby_All" ImageHeight="16" ImageWidth="16" 
                CheckedImageAlternateText="Enabled" UncheckedImageAlternateText="Disabled"
                CheckedImageUrl="~/images/stock_calc-accept.png" UncheckedImageUrl="~/images/stock_calc-cancel.png" />                                
            </asp:Panel>        
            <%-- Generate Map Submit Button --%>
            <div>                
              <input id="Button7" type="button" value="Generate Map" onclick="openAllOptions();return false;" /> 
            </div>
          </Content>
        </cc1:AccordionPane>   
      </Panes>
    </cc1:Accordion>
  </form>
</body>
</html>
