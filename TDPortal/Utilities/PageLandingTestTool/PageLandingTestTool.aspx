<%@ Page language="c#" Codefile="PageLandingTestTool.aspx.cs" validaterequest="false" AutoEventWireup="false" Inherits="PageLandingTestTool" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en-GB" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>PageLandingTestTool</title>
		<meta content="JavaScript" name="vs_defaultClientScript" />
		
		<style type="text/css">
		    
		    body 
		    {
	        	font-family: verdana, arial, helvetica, sans-serif;
	        	font-size: 0.9em;
        	}
        	
        	.tableBody
        	{
        	    border: blue thin solid;
        	}
        	        	       	
        	.tableHeading
        	{
        	    font-size: 1.3em;
        	    font-weight: bold;
        	}
        	
        	.txtSeven
        	{
        	    font-size: 0.7em;
        	}
        	
        	.txtOne
        	{
        	    font-size: 1.0em;
        	}
        	
		</style>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table class="tableBody" width="1024" border="1">
				<tr>
					<td class="txtSeven" valign="top"><asp:textbox id="textServerName" runat="server"></asp:textbox><asp:label id="labelServerName" runat="server">Server Name</asp:label><br />
						<asp:textbox id="textPartnerId" runat="server"></asp:textbox><asp:label id="labelPartnerID" runat="server">Partner ID</asp:label></td>
					<td class="txtSeven" align="right"><textarea id="textURLResult" name="textURLResult" rows="6" cols="95" runat="server"></textarea>
					</td>
				</tr>
			</table>
			<div align="center">
			    <a href="#JP">Journey Planner</a>&nbsp;&nbsp;<a href="#FN">Find Nearest</a>&nbsp;&nbsp;
			    <a href="#CO2">CO2</a>&nbsp;&nbsp;<a href="#TN">Travel News</a>&nbsp;&nbsp;
			    <a href="#IFJP">IFrame Journey Planner</a>
			    <a href="#SI">StopInformation</a>
			</div>
			<br />
			
			<!-- JOURNEY PLANNER -->
			<a name="JP"></a>
			<table class="tableBody" width="1024" border="1">
				<thead>
					<tr>
						<td class="txtSeven" colspan="2"><asp:label id="labelJPHeader" runat="server" CssClass="tableHeading">Journey Planning</asp:label></td>
					</tr>
				</thead>
				<tr>
					<td class="txtSeven"><b><asp:label id="labelOrigin" runat="server">Origin</asp:label></b><br />
						<asp:radiobutton id="radioOriginNull" runat="server" groupname="radioOrigin" AutoPostBack="True"></asp:radiobutton><asp:label id="labelOriginNull" runat="server">Not Specified</asp:label><asp:radiobutton id="radioOriginNaptan" runat="server" groupname="radioOrigin" AutoPostBack="True"></asp:radiobutton><asp:label id="labelOriginNaptan" runat="server">NaPTAN</asp:label><asp:radiobutton id="radioOriginPCodeAddr" runat="server" groupname="radioOrigin" AutoPostBack="True"></asp:radiobutton><asp:label id="labelOriginPCodeAddr" runat="server">Postcode/Address</asp:label><br />
						<asp:radiobutton id="radioOriginCoord" runat="server" groupname="radioOrigin" AutoPostBack="True"></asp:radiobutton><asp:label id="labelOriginCoords" runat="server">OSGR Coordinates</asp:label><asp:radiobutton id="radioOriginLongLat" runat="server" groupname="radioOrigin" AutoPostBack="True"></asp:radiobutton><asp:label id="labelOriginLongLat" runat="server">Longitude/Latitude</asp:label>
						<asp:radiobutton id="radioOriginCRS" runat="server" groupname="radioOrigin" AutoPostBack="True"></asp:radiobutton><asp:label id="labelOriginCRS" runat="server">CRS</asp:label><br />
						<asp:textbox id="textOriginText" runat="server"></asp:textbox><asp:label id="labelOriginText" runat="server">Origin Text e.g. Leeds</asp:label><br />
						<asp:textbox id="textOriginData" runat="server"></asp:textbox><asp:label id="labelOriginData" runat="server">Origin Data e.g. 9100Leeds</asp:label></td>
					<td class="txtSeven"><b><asp:label id="labelDestination" runat="server">Destination</asp:label></b><br />
						<asp:radiobutton id="radioDestinationNull" runat="server" groupname="radioDestination" AutoPostBack="True"></asp:radiobutton><asp:label id="labelDestinationNull" runat="server">Not Specified</asp:label><asp:radiobutton id="radioDestinationNaptan" runat="server" groupname="radioDestination" AutoPostBack="True"></asp:radiobutton><asp:label id="labelDestNaPTAN" runat="server">NaPTAN</asp:label><asp:radiobutton id="radioDestinationPCodeAddr" runat="server" groupname="radioDestination" AutoPostBack="True"></asp:radiobutton><asp:label id="labelDestPCodeAddress" runat="server">Postcode/Address</asp:label><br />
						<asp:radiobutton id="radioDestinationCoord" runat="server" groupname="radioDestination" AutoPostBack="True"></asp:radiobutton><asp:label id="labelDestCoords" runat="server">OSGR Coordinates</asp:label><asp:radiobutton id="radioDestinationLongLat" runat="server" groupname="radioDestination" AutoPostBack="True"></asp:radiobutton><asp:label id="labelDestLongLat" runat="server">Longitude/Latitude</asp:label>
						<asp:radiobutton id="radioDestinationCRS" runat="server" groupname="radioDestination" AutoPostBack="True"></asp:radiobutton><asp:label id="labelDestCRS" runat="server">CRS</asp:label><br />
						<asp:textbox id="textDestinationText" runat="server"></asp:textbox><asp:label id="labelDestinationText" runat="server">Destination Text e.g. Wembley Stadium</asp:label><br />
						<asp:textbox id="textDestinationData" runat="server"></asp:textbox><asp:label id="labelDestinationData" runat="server">Destination Data e.g. 913587, 825663</asp:label></td>
				</tr>
				<tr>
					<td class="txtSeven" colspan="2"><asp:checkbox id="encryptOrigin" runat="server" checked="True"></asp:checkbox><asp:label id="labelEncrypt" runat="server">Encrypt origin? (or destination)</asp:label></td>
				</tr>
				<tr>
					<td class="txtSeven"><br />
						<br />
						<asp:textbox id="textOutwardDate" runat="server"></asp:textbox><asp:label id="labelOutwardDate" runat="server">Outward Date e.g. 10112005</asp:label><br />
						<asp:textbox id="textOutwardTime" runat="server"></asp:textbox><asp:label id="labelOutwardTime" runat="server">Outward Time e.g. 1015</asp:label><br />
						<b>
							<asp:label id="labelOutwardDepartArrive" runat="server">Depart/Arrive</asp:label></b><br />
						<asp:radiobutton id="radioOutwardDepArrNull" runat="server" groupname="radioOutwardDepArr"></asp:radiobutton><asp:label id="labelOutwardDepArrNull" runat="server">Not Specified</asp:label><asp:radiobutton id="radioOutwardDepart" runat="server" groupname="radioOutwardDepArr"></asp:radiobutton><asp:label id="labelOutwardDepart" runat="server">Depart after</asp:label><asp:radiobutton id="radioOutwardArrive" runat="server" groupname="radioOutwardDepArr"></asp:radiobutton><asp:label id="labelOutwardArrive" runat="server">Arrive by</asp:label></td>
					<td class="txtSeven"><asp:label id="labelReturnRequired" runat="server">Is return required?</asp:label><asp:radiobutton id="radioReturnNull" runat="server" groupname="radioReturn"></asp:radiobutton>Not 
						specified
						<asp:radiobutton id="radioReturnYes" runat="server" groupname="radioReturn"></asp:radiobutton>Yes
						<asp:radiobutton id="radioReturnNo" runat="server" groupname="radioReturn"></asp:radiobutton>No
						<br />
						<br />
						<asp:textbox id="textReturnDate" runat="server"></asp:textbox><asp:label id="labelReturnDate" runat="server">Return Date</asp:label><br />
						<asp:textbox id="textReturnTime" runat="server"></asp:textbox><asp:label id="labelReturnTime" runat="server">Return Time</asp:label><br />
						<b>
							<asp:label id="labelReturnDepartArrive" runat="server">Depart/Arrive</asp:label></b><br />
						<asp:radiobutton id="radioReturnDepArrNull" runat="server" groupname="radioReturnDepArr"></asp:radiobutton><asp:label id="labelReturnDepArrNull" runat="server">Not Specified</asp:label><asp:radiobutton id="radioReturnDepart" runat="server" groupname="radioReturnDepArr"></asp:radiobutton><asp:label id="labelReturnDepart" runat="server">Depart after</asp:label><asp:radiobutton id="radioReturnArrive" runat="server" groupname="radioReturnDepArr"></asp:radiobutton><asp:label id="labelReturnArrive" runat="server">Arrive by</asp:label></td>
				</tr>
				<tr>
					<td class="txtSeven">
					    <b><asp:label id="labelMode" runat="server">Mode</asp:label></b>
					        <asp:radiobutton id="radioModeNull" runat="server" groupname="radioMode"></asp:radiobutton><asp:label id="labelModeNull" runat="server">Not specified</asp:label>
					        <asp:radiobutton id="radioModeMulti" runat="server" groupname="radioMode"></asp:radiobutton><asp:label id="labelMulti" runat="server">Multi Modal</asp:label>
					        <asp:radiobutton id="radioModeCar" runat="server" groupname="radioMode"></asp:radiobutton><asp:label id="labelModeCar" runat="server">Car</asp:label>
					        <asp:radiobutton id="radioModeTrain" runat="server" groupname="radioMode"></asp:radiobutton><asp:label id="labelModeTrain" runat="server">Train</asp:label>
					        <asp:radiobutton id="radioModeCoach" runat="server" groupname="radioMode"></asp:radiobutton><asp:label id="labelModeCoach" runat="server">Coach</asp:label>
					        <asp:radiobutton id="radioModeFlight" runat="server" groupname="radioMode"></asp:radiobutton><asp:label id="labelModeFlight" runat="server">Flight</asp:label>
					        <asp:radiobutton id="radioModeCycle" runat="server" groupname="radioMode"></asp:radiobutton><asp:label id="labelModeCycle" runat="server">Cycle</asp:label>
					        <br />
						<b><asp:label id="labelIncludeCar" runat="server">Include Car?</asp:label></b>
                            <asp:radiobutton id="radioCarNull" runat="server" groupname="radioCar"></asp:radiobutton>Not Specified
						    <asp:radiobutton id="radioCarYes" runat="server" groupname="radioCar"></asp:radiobutton>Yes
						    <asp:radiobutton id="radioCarNo" runat="server" groupname="radioCar"></asp:radiobutton>No
						    <br />
						<b><asp:label id="labelAutoPlan" runat="server">Auto Plan?</asp:label></b>
						    <asp:radiobutton id="radioAutoNull" runat="server" groupname="radioAuto"></asp:radiobutton>Not Specified
						    <asp:radiobutton id="radioAutoYes" runat="server" groupname="radioAuto"></asp:radiobutton>Yes
						    <asp:radiobutton id="radioAutoNo" runat="server" groupname="radioAuto"></asp:radiobutton>No
						    <br />
						<b><asp:Label ID="labelAccessibleOption" runat="server">Accessibility</asp:Label></b>
						    <asp:RadioButton ID="radioAccessibleNull" runat="server" GroupName="radioAccessible" />Not Specified
						    <asp:RadioButton ID="radioAccessibleWheelchairAssistance" runat="server" GroupName="radioAccessible" />Wheelchair & assistance
						    <asp:RadioButton ID="radioAccessibleAssistance" runat="server" GroupName="radioAccessible" />Assistance
						    <asp:RadioButton ID="radioAccessibleWheelchair" runat="server" GroupName="radioAccessible" />Wheelchair
						    <asp:CheckBox ID="chkAccessibleFewerChanges" runat="server" Text="Fewer changes" />
						    <br />
					</td>
					<td>
						<p class="txtSeven"><b><label>Modes to exclude</label></b><br /></p>
							<table>
								<tr>
									<td class="txtSeven"><asp:CheckBox id="chkExcludeRail" runat="server" Text="Rail"></asp:CheckBox></td>
									<td class="txtSeven"><asp:CheckBox id="chkExcludeBusCoach" runat="server" Text="Bus/Coach"></asp:CheckBox></td>
									<td class="txtSeven"><asp:CheckBox id="chkExcludeUnderground" runat="server" Text="Underground/Metro"></asp:CheckBox></td>
								</tr>
								<tr>
									<td class="txtSeven"><asp:CheckBox id="chkExcludeTram" runat="server" Text="Tram/Light Rail"></asp:CheckBox></td>
									<td class="txtSeven"><asp:CheckBox id="chkExcludeFerry" runat="server" Text="Ferry"></asp:CheckBox></td>
									<td class="txtSeven"><asp:CheckBox id="chkExcludePlane" runat="server" Text="Plane"></asp:CheckBox></td>
								</tr>
								<tr>
								    <td class="txtSeven"><asp:CheckBox id="chkExcludeTelecabine" runat="server" Text="Telecabine"></asp:CheckBox></td>
								</tr>
							</table>
						<p class="txtSeven"><asp:checkbox id="chkEncryptJP" runat="server" Text="Encrypt Journey Planning Parameters"></asp:checkbox></p>
						<p class="txtSeven"><asp:button id="buttonGenerateAll" runat="server" 
                                text="Generate URL"></asp:button>&nbsp;<asp:button id="buttonGeneratePost" runat="server" text="Generate POST Request"></asp:button></p>
					</td>
				</tr>
			</table>
			<br />
			
			<!-- FIND NEAREST -->
			<a name="FN"></a>
			<table class="tableBody" width="1024" border="1">
				<thead>
					<tr>
						<td class="txtSeven" colspan="2"><asp:label id="Label1" runat="server" CssClass="tableHeading">Find nearest</asp:label></td>
					</tr>
				</thead>
				<tr>
					<td class="txtSeven">
						<b><asp:label id="Label2" runat="server">Find nearest Type</asp:label></b>
						<asp:radiobutton id="radioFNTypeCarPark" runat="server" groupname="rdoFNType"></asp:radiobutton>Car Park
						<br />
						<b><asp:label ID="lblFNLocationType" runat="server">Location Type</asp:label></b>
						<asp:radiobutton id="radioLocTypePlaceName" runat="server" groupname="rdoLocType"></asp:radiobutton>Placename
						<asp:radiobutton ID="radioLocTypeOSGR" runat="server" groupname="rdoLocType"></asp:radiobutton>OSGR Coordinates
						<asp:radiobutton ID="radioLocTypeLatLong" runat="server" groupname="rdoLocType"></asp:radiobutton>Longitude/Latitude
						<br />
						<b>
							<asp:label id="lblFNPlace" runat="server">Place Text e.g. Leeds</asp:label></b>
						<asp:textbox id="txtFNPlace" runat="server"></asp:textbox><br />
						<br />
						<b><asp:label id="lblFNLocData" runat="server">Location Data</asp:label></b>
						<asp:textbox ID="txtFNLocData" runat="server"></asp:textbox><br />
						<b>
							<asp:label id="lblFNLocGaz" runat="server">Gazetteer</asp:label></b>
						<asp:radiobutton id="radioLocGazAddress" runat="server" groupname="rdoLocGazAuto"></asp:radiobutton>Address/Postcode
						<asp:radiobutton id="radioLocGazTown" runat="server" groupname="rdoLocGazAuto"></asp:radiobutton>Town/district/village
						<asp:radiobutton id="radioLocGazStation" runat="server" groupname="rdoLocGazAuto"></asp:radiobutton>Station/airport
						<asp:radiobutton id="radioLocGazFacility" runat="server" groupname="rdoLocGazAuto"></asp:radiobutton>Facility/attraction
						<br />
						<b>
							<asp:label id="lblFNNumberOfResults" runat="server">Number of results</asp:label></b>
						<asp:textbox id="txtFNNumberOfResults" runat="server"></asp:textbox><br />
						<b>
							<asp:label id="lblFNAutoPlan" runat="server">Auto Plan</asp:label></b>
						<asp:radiobutton id="radioFNAutoPlanNull" runat="server" groupname="radioAuto"></asp:radiobutton>Not 
						Specified
						<asp:radiobutton id="radioFNAutoPlanYes" runat="server" groupname="radioAuto"></asp:radiobutton>Yes
						<asp:radiobutton id="radioFNAutoPlanNo" runat="server" groupname="radioAuto"></asp:radiobutton>No
					</td>
					<td class="txtSeven">
						<p><asp:checkbox id="chkEncryptFN" runat="server" Text="Encrypt Find Nearest Parameters"></asp:checkbox></p>
						<p><asp:button id="btnGenerateFindNearest" runat="server" text="Generate URL"></asp:button>&nbsp;<asp:button id="btnGenerateFindNearestPost" runat="server" text="Generate POST Request"></asp:button></p>
					</td>
				</tr>
			</table>
			<br />
			
            <!-- CO2 Landing -->
            <a name="CO2"></a>
			<table class="tableBody" width="1024" border="1">
				<thead>
					<tr>
						<td class="txtSeven" colspan="2"><asp:label id="Label3" runat="server" CssClass="tableHeading">CO2 Calculator</asp:label></td>
					</tr>
				</thead>
				<tr>
					<td class="txtSeven"><b><asp:label id="lblCO2Distance" runat="server">Enter Distance</asp:label></b><asp:textbox id="txtCO2Distance" runat="server"></asp:textbox><br />
						<b><asp:label id="lblCO2Units" runat="server">Units</asp:label></b>
						<asp:radiobutton id="RadioCO2UnitMiles" runat="server" groupname="rdoCO2Units"></asp:radiobutton>Miles
						<asp:radiobutton id="RadioCO2UnitKm" runat="server" groupname="rdoCO2Units"></asp:radiobutton>Km
						<br />
						
						<b><asp:label id="Label8" runat="server">Auto Plan</asp:label></b>
						<asp:radiobutton id="radioCO2AutoPlanYesNull" runat="server" groupname="radioCO2Auto"></asp:radiobutton>Not Specified
						<asp:radiobutton id="radioCO2AutoPlanYes" runat="server" groupname="radioCO2Auto"></asp:radiobutton>Yes
						<asp:radiobutton id="radioCO2AutoPlanNo" runat="server" groupname="radioCO2Auto"></asp:radiobutton>No
						<br />
						
					</td>
					<td class="txtSeven" rowspan="2">
						<p><asp:checkbox id="CheckCO2Encrypt" runat="server" Text="Encrypt CO2 Parameters"></asp:checkbox></p>
						<p><asp:button id="btnGenerateCO2" runat="server" text="Generate URL"></asp:button>&nbsp;<asp:button id="btnPostCO2" runat="server" text="Generate POST Request"></asp:button></p>
					</td>
				</tr>
				<tr>
				    <td>
				        <span class="txtSeven" ><b><asp:label id="CO2ExcludeLabel" runat="server">Exclude Modes</asp:label></b></span>
				        <table>
							<tr>
								<td class="txtSeven"><asp:checkbox id="CheckModeSCar" runat="server" Text="SmallCar"></asp:checkbox></td>
								<td class="txtSeven"><asp:checkbox id="CheckModeLCar" runat="server" Text="LargeCar"></asp:checkbox></td>
								<td class="txtSeven"><asp:checkbox id="CheckModeBus" runat="server" Text="Bus/Coach"></asp:checkbox></td>
								<td class="txtSeven"><asp:checkbox id="CheckModeRail" runat="server" Text="Rail"></asp:checkbox></td>
								<td class="txtSeven"><asp:checkbox id="CheckModeAir" runat="server" Text="Plane"></asp:checkbox></td>
							</tr>
						</table>
				    </td>
				</tr>
			</table>
			<br />

			<!-- LIVE TRAVEL NEWS -->
			<a name="TN"></a>
			<table class="tableBody" width="1024" border="1">
				<thead>
					<tr>
						<td class="txtSeven" colspan="2"><asp:label id="labelTNHeader" runat="server" CssClass="tableHeading">Travel News</asp:label></td>
					</tr>
				</thead>
				<tr>
					<td class="txtSeven"><b><asp:label id="lblRegion" runat="server">Region</asp:label></b>&nbsp;&nbsp;<asp:textbox id="txtRegion" runat="server"></asp:textbox><br />
						<b>
							<asp:label id="labelNewsType" runat="server">NewsType</asp:label></b><asp:radiobutton id="radioNewsTypeNotSpecified" runat="server" groupname="radioNewsType"></asp:radiobutton><asp:label id="labelNewsTypeNotSpecified" runat="server">Not specified</asp:label><asp:radiobutton id="radioNewsTypeRoad" runat="server" groupname="radioNewsType"></asp:radiobutton><asp:label id="labelNewsTypeRoad" runat="server">Road</asp:label><asp:radiobutton id="radioNewsTypePT" runat="server" groupname="radioNewsType"></asp:radiobutton><asp:label id="labelNewsTypePT" runat="server">Public Trasnport</asp:label><asp:radiobutton id="radioNewsTypeBoth" runat="server" groupname="radioNewsType"></asp:radiobutton><asp:label id="labelNewsTypeBoth" runat="server">Both</asp:label><br />
						<b>
							<asp:label id="labelSeverity" runat="server">Severity</asp:label></b><asp:radiobutton id="radioSeverityNotSpecified" runat="server" groupname="radioSeverity"></asp:radiobutton>Not 
						Specified
						<asp:radiobutton id="radioSeverityMajor" runat="server" groupname="radioSeverity"></asp:radiobutton>Major
						<asp:radiobutton id="radioSeverityAll" runat="server" groupname="radioSeverity"></asp:radiobutton>All<br />
						<b>
							<asp:label id="labelTM" runat="server">Table or Map?</asp:label></b><asp:radiobutton id="radioTMNotSpecified" runat="server" groupname="radioTableMap"></asp:radiobutton>Not 
						Specified
						<asp:radiobutton id="radioTMTable" runat="server" groupname="radioTableMap"></asp:radiobutton>Table
						<asp:radiobutton id="radioTMMap" runat="server" groupname="radioTableMap"></asp:radiobutton>Map
					</td>
					<td class="txtSeven">
						<p><asp:checkbox id="chkEncryptTN" runat="server" Text="Encrypt Travel News Parameters"></asp:checkbox></p>
						<p><asp:button id="btnGenerateTNGet" runat="server" text="Generate URL"></asp:button>&nbsp;<asp:button id="btnGenerateTNPost" runat="server" text="Generate POST Request"></asp:button></p>
					</td>
				</tr>
			</table>
			<br />
			
			<!-- IFRAME JOURNEY PLANNER -->
			<a name="IFJP"></a>			
			<table class="tableBody" width="1024" border="1">
				<thead>
					<tr>
						<td class="txtSeven" colspan="2"><asp:label id="labelIFJPHeader" runat="server" CssClass="tableHeading">IFrame Journey Planner</asp:label></td>
					</tr>
				</thead>
				<tr>
					<td class="txtSeven">
					    <b><asp:label id="labelIFJPFrom" runat="server">From</asp:label></b>&nbsp;&nbsp;
					    <asp:radiobutton id="radioIFJPLocGazFromAddress" runat="server" groupname="rdoIFJPLocGazAutoFrom"></asp:radiobutton>Address/Postcode
						<asp:radiobutton id="radioIFJPLocGazFromTown" runat="server" groupname="rdoIFJPLocGazAutoFrom"></asp:radiobutton>Town/district/village
						<asp:radiobutton id="radioIFJPLocGazFromStation" runat="server" groupname="rdoIFJPLocGazAutoFrom"></asp:radiobutton>Station/airport
						<asp:radiobutton id="radioIFJPLocGazFromFacility" runat="server" groupname="rdoIFJPLocGazAutoFrom"></asp:radiobutton>Facility/attraction
						<asp:radiobutton id="radioIFJPLocGazFromNotSpecified" runat="server" groupname="rdoIFJPLocGazAutoFrom"></asp:radiobutton>Not specified
						<br />
						<asp:textbox id="txtIFJPFrom" runat="server"></asp:textbox>&nbsp;&nbsp;&nbsp;
						<br />
						
						<b><asp:label id="labelIFJPTo" runat="server">To</asp:label></b>&nbsp;&nbsp;
						<asp:radiobutton id="radioIFJPLocGazToAddress" runat="server" groupname="rdoIFJPLocGazAutoTo"></asp:radiobutton>Address/Postcode
						<asp:radiobutton id="radioIFJPLocGazToTown" runat="server" groupname="rdoIFJPLocGazAutoTo"></asp:radiobutton>Town/district/village
						<asp:radiobutton id="radioIFJPLocGazToStation" runat="server" groupname="rdoIFJPLocGazAutoTo"></asp:radiobutton>Station/airport
						<asp:radiobutton id="radioIFJPLocGazToFacility" runat="server" groupname="rdoIFJPLocGazAutoTo"></asp:radiobutton>Facility/attraction
						<asp:radiobutton id="radioIFJPLocGazToNotSpecified" runat="server" groupname="rdoIFJPLocGazAutoTo"></asp:radiobutton>Not specified
						<br />
						<asp:textbox id="txtIFJPTo" runat="server"></asp:textbox>&nbsp;&nbsp;&nbsp;
						<br />
						<br />
						
						<asp:label id="labelIFJPOutwardDate" runat="server">Outward Date e.g. 10112005</asp:label><br />&nbsp;&nbsp;<asp:textbox id="txtIFJPOutwardDate" runat="server"></asp:textbox><br />
						<asp:label id="labelIFJPOutwardTime" runat="server">Outward Time e.g. 1015</asp:label><br />&nbsp;&nbsp;<asp:textbox id="txtIFJPOutwardTime" runat="server"></asp:textbox><br />											
						
						<b><asp:label id="labelIFJPPublicTransport" runat="server">Public transport</asp:label></b>&nbsp;&nbsp;
						<asp:radiobutton id="radioIFJPPTYes" runat="server" groupname="rdoIFJPPublicTransport"></asp:radiobutton>Yes
						<asp:radiobutton id="radioIFJPPTNo" runat="server" groupname="rdoIFJPPublicTransport"></asp:radiobutton>No
						<asp:radiobutton id="radioIFJPPTNotSpecified" runat="server" groupname="rdoIFJPPublicTransport"></asp:radiobutton>Not specified
						<br />
						
						<b><asp:label id="labelIFJPCar" runat="server">Car</asp:label></b>&nbsp;&nbsp;
						<asp:radiobutton id="radioIFJPCarYes" runat="server" groupname="rdoIFJPCar"></asp:radiobutton>Yes
						<asp:radiobutton id="radioIFJPCarNo" runat="server" groupname="rdoIFJPCar"></asp:radiobutton>No
						<asp:radiobutton id="radioIFJPCarNotSpecified" runat="server" groupname="rdoIFJPCar"></asp:radiobutton>Not specified
						<br />
						
						<b><asp:label id="labelIFJPShowAdvanced" runat="server">Show advanced</asp:label></b>&nbsp;&nbsp;
						<asp:radiobutton id="radioIFJPAdvancedYes" runat="server" groupname="rdoIFJPAdvanced"></asp:radiobutton>Yes
						<asp:radiobutton id="radioIFJPAdvancedNo" runat="server" groupname="rdoIFJPAdvanced"></asp:radiobutton>No
						<asp:radiobutton id="radioIFJPAdvancedNotSpecified" runat="server" groupname="rdoIFJPAdvanced"></asp:radiobutton>Not specified
						<br />
						
						<b><asp:label id="labelIFJPAutoPlan" runat="server">Auto Plan</asp:label></b>&nbsp;&nbsp;
						<asp:radiobutton id="radioIFJPAutoplanYes" runat="server" groupname="rdoIFJPAuto"></asp:radiobutton>Yes
						<asp:radiobutton id="radioIFJPAutoplanNo" runat="server" groupname="rdoIFJPAuto"></asp:radiobutton>No
						<asp:radiobutton id="radioIFJPAutoplanNotSpecified" runat="server" groupname="rdoIFJPAuto"></asp:radiobutton>Not Specified
						<br />
						
					</td>
					<td class="txtSeven">
						<p><asp:checkbox id="chkIFJPIncludeSpecifiedOptions" runat="server" Text="Only include populated values"></asp:checkbox></p>
						<p><asp:button id="btnIFJPGenerateGet" runat="server" text="Generate URL"></asp:button>&nbsp;<asp:button id="btnIFJPGeneratePost" runat="server" text="Generate POST Request"></asp:button></p>
					</td>
				</tr>
			</table>
			
			<!-- Stop Information -->
            <a name="SI"></a>
			<table class="tableBody" width="1024" border="1">
				<thead>
					<tr>
						<td class="txtSeven" colspan="2"><asp:label id="Label4" runat="server" CssClass="tableHeading">Stop Information</asp:label></td>
					</tr>
				</thead>
				<tr>
					<td class="txtSeven"><b><asp:label id="Label5" runat="server">Enter Stop Data</asp:label></b><asp:textbox id="txtStopData" runat="server"></asp:textbox><br />
						<b><asp:label id="Label6" runat="server">Stop Data Type</asp:label></b>
						<asp:radiobutton id="RadioStopDataNaptan" runat="server" groupname="rdoStopDataType" Checked="true"></asp:radiobutton>NaPTAN
						<asp:radiobutton id="RadioStopDataCRS" runat="server" groupname="rdoStopDataType"></asp:radiobutton>CRS
						<asp:radiobutton id="RadioStopDataIATA" runat="server" groupname="rdoStopDataType"></asp:radiobutton>IATA
						<asp:radiobutton id="RadioStopDataSMS" runat="server" groupname="rdoStopDataType"></asp:radiobutton>SMS
						<br />						
					</td>
					<td class="txtSeven" rowspan="2">
						<p><asp:checkbox id="CheckStopInformationEncrypt" runat="server" Text="Encrypt Stop Information Parameters"></asp:checkbox></p>
						<p><asp:button id="btnGenerateStopInformation" runat="server" text="Generate URL"></asp:button>&nbsp;<asp:button id="btnPostStopInformation" runat="server" text="Generate POST Request"></asp:button></p>
					</td>
				</tr>
				<tr>
				    <td>
				        <span class="txtSeven" ><b><asp:label id="Label9" runat="server">Exclude functions</asp:label></b></span>
				        <table>
							<tr>
								<td class="txtSeven"><asp:checkbox id="CheckSIMap" runat="server" Text="Map"></asp:checkbox></td>
								<td class="txtSeven"><asp:checkbox id="CheckSIJourneyPlanning" runat="server" Text="Journey Planning"></asp:checkbox></td>
								<td class="txtSeven"><asp:checkbox id="CheckSITaxiInformation" runat="server" Text="Taxi Information"></asp:checkbox></td>
								<td class="txtSeven"><asp:checkbox id="CheckSIOperators" runat="server" Text="Operators"></asp:checkbox></td>
								
							</tr>
							<tr>
							    <td class="txtSeven"><asp:checkbox id="CheckSINextServices" runat="server" Text="Next Services"></asp:checkbox></td>
								<td class="txtSeven"><asp:checkbox id="CheckSIRealtimeLinks" runat="server" Text="Realtime Links"></asp:checkbox></td>
								<td class="txtSeven"><asp:checkbox id="CheckSILocationInformation" runat="server" Text="Location Information"></asp:checkbox></td>
								<td class="txtSeven"><asp:checkbox id="CheckSIFacilityInformation" runat="server" Text="Facility Information"></asp:checkbox></td>
							</tr>
						</table>
				    </td>
				</tr>
			</table>
			<br />

			
			<div align="center">
			    <a href="#">Back to top</a>
			</div>
		</form>
		
	</body>
</html>
