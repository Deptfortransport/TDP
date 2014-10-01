<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_InitWithSymbolsProgrammatically.aspx.cs" Inherits="TestJSAPIMappingArc93.InitWithSymbolsProgrammatically" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="Content-type" content="text/html;charset=UTF-8" />
		<meta http-equiv="Pragma" content="no-cache" />
		<meta http-equiv="cache-control" content="no-cache" />
</head>

<body style="padding: 0px; margin: 0px;">
    <%-- START: Map control required tags --%>
    <div class="tundra"></div>
    <%-- END: Map control required tags --%>
		<%-- START: Map control required tags --%>
    <script type="text/javascript" src="./esriMap/ESRIJSAPIv1.5/default.ashx"></script>
		<%-- END: Map control required tags --%>   
		
		<script type="text/javascript" language="javascript">
		  dojo.addOnLoad(function() {
		    var map = new ESRIUK.Dijits.Map({
		      'class': 'tundra',
          param_XMin:542500,param_YMin:255000,param_XMax:549000,param_YMax:258000,
          param_Symbols:[
              { x: 543000, y: 256000, label: 'point 1',type:'start',infoWindowRequired:true},
              { x: 548000, y: 258000, label: '',type:'symbol',symbolKey:'PUSHPIN',infoWindowRequired:true,content:'<a href="javascript:showCarParkInfo(1234);">Car Park Info</a>'},
              { x: 545000, y: 257000, label: 'point 3', type: 'end', infoWindowRequired: true, content: 'this is text content' },
              { x: 546200, y: 257300, label: 'point 4', type: 'symbol', symbolKey: 'PUSHPIN', infoWindowRequired: true, content: 'this is text content' }
          ], 
          param_Tools:"selectNearby,userDefined,zoom,pan",
          param_Width:776,param_Height:540  		      
		    },
		     document.body.getElementsByTagName('div')[0]);
		    map.startup();
		  }
      )
		</script>     
    </body>
</html>
