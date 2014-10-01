<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Links.aspx.cs" Inherits="TestJSAPIMappingArc93.Links" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TDP Test Harness - Links Page</title>
    <style type="text/css">
      *{
        font-family: Arial Sans-Serif;
        font-size: 12px;
      }
      h1{
        font-size: 1.6em;
      }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding-left:15px;">
      <h1>Links to test harness pages</h1>
      <ol>
        <li><a href="Frames.html" title="Main Init Methods & API Page">Main Test Page</a> - Includes Init Methods & API calls</li>
        <li><a href="Test_WholePage.aspx" title="Whole Page Map">Whole Page Map</a> - A whole page map with no parameters set</li>
        <li><a href="Test_WholePage_Params.aspx" title="Whole Page Map with location parameters">Whole Page Map with location parameters</a> - A whole page map with location parameters set</li>
        <li><a href="Test_BasicMap.aspx" title="Basic Map">Basic Map</a> - A basic map using default dimensions & no parameters set</li>
        <li><a href="Test_BasicMap_Params.aspx" title="Basic Map with location & style parameters">Basic Map with location & style</a> - A basic map with location & style parameters set</li>
        <li><a href="Test_DojoHarness.aspx" title="Main Dojo Test Harness Map">Main Dojo Test Harness Map</a></li> 
        <li><a href="Test_ValidHTML4.aspx" title="Valid HTML4 Map">Valid HTML4 Map</a></li> 
        <li><a href="Test_ValidXHTML.aspx" title="Valid XHTML Map">Valid XHTML Map</a></li>
        <li><a href="Maps/Default.aspx" title="Sub-directory test">Sub-directory test</a></li>
        <li><a href="Maps/Maps/Default.aspx" title="Sub-sub-directory test">Sub-Sub-directory test</a></li>
        <li><a href="Test_InfoWindowX.aspx" title="InfoWindow &apos;x&apos; CSS test">InfoWindow &apos;x&apos; CSS test</a></li>
        <li><a href="Test_TravelNewsFilterDateTime.aspx" title="TravelNews filter datetime test">TravelNews filter datetime test - declarative</a></li>
        <li><a href="Test_TravelNewsFilterDateTime1.aspx" title="TravelNews filter datetime test">TravelNews filter datetime test - programmatic</a></li>
        <li><a href="Test_MapModeNone.aspx" title="None Map Mode">None option for map mode</a></li>
        <li><a href="Test_InitialExtent.aspx" title="Initial Extent">Testing initial extent in 'globe' tool on navigation bar</a></li>
        <li><a href="Test_HidePan_Zoom.aspx" title="Show pan/zoom panels">Show pan/zoom panels</a></li>
        <li><a href="Test_Tools_NoNavPanel.aspx" title="Hide pan/zoom panels">Hide pan/zoom panels</a></li>
        <li><a href="Test_InitWithRoutes.aspx" title="Initialise with roues">Initialise with routes</a></li>
        <li><a href="Test_InitWithRoutesAndTravel.aspx" title="Initialise with roues and travelNews">Initialise with routes and travelNews</a></li>
        <li><a href="Test_OnMapInitialiseComplete.aspx" title="OnMapInitialiseComplete">OnMapInitialiseComplete</a></li>
        <li><a href="Test_InitWithCycleAndTravel.aspx" title="OnMapInitialiseComplete">Initialise with cycle route and travel news</a></li>
        <li><a href="Test_InitWithSymbols.aspx" title="Initialise with symbols">Initialise with symbols & infowindow</a></li>
        <li><a href="Test_InitWithSymbols1.aspx" title="Initialise with symbols">Initialise with symbols & infowindow with main pt and no content</a></li>
        <li><a href="Test_InitWithSymbolsProgrammatically.aspx" title="Initialise with symbols programmatically">Initialise with symbols & infowindow programmatically</a></li>
      </ol>
    </div>
    </form>
</body>
</html>
