-- ************************************************************
-- NAME 		: SC10009_TransportDirect_Content_9_NetworkMaps.sql
-- DESCRIPTION 	: Updates content to the Networks Map page
-- AUTHOR		: XXXX
-- DATE         :  
-- ************************************************************

USE [Content]
GO

DECLARE @MaxGroupId INT

SELECT @MaxGroupId = MAX(GroupId)+1 FROM tblGroup

IF NOT EXISTS(Select * from tblGroup WHERE [Name] = 'mapsdefault')
	INSERT INTO [dbo].[tblGroup] ([GroupId], [Name])
			VALUES (@MaxGroupId, 'mapsdefault')
GO


DECLARE @GroupId INT
SELECT @GroupId = GroupId FROM tblGroup WHERE [Name] = 'mapsdefault'


EXEC AddtblContent
1, @GroupId,'Body Text','/Channels/TransportDirect/Maps/NetworkMaps'
,'
<div id="primcontent">  <div id="contentarea">  <div class="hdtypetwo">
    <h2><a id="Rail" name="Rail">Rail</a></h2></div>  
		<p><a title="Open a new window displaying the maps of rail stations with access for people with reduced mobililty on the National Rail website" href="http://www.nationalrail.co.uk/passenger_services/disabled_passengers/" target="_blank">Maps of rail stations with access for people with reduced mobility <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the National Rail website" href="http://www.nationalrail.co.uk/passenger_services/maps/" target="_blank">National&nbsp;Rail <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the ScotRail Website" href="http://www.scotrail.co.uk/timetables-routes" target="_blank" name="Rail">ScotRail <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
<div align="right"><a class="jpt" href="/Web2/Maps/NetworkMaps.aspx#logoTop">Return to top ^</a></div>  
<div class="hdtypetwo">
    <h2><a id="Coach" name="Coach">Coach</a></h2></div>  
		<p><a title="Open a new window displaying the Megabus website" href="http://uk.megabus.com/BusStops.aspx" target="_blank">Megabus <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the National Express website" href="http://coach.nationalexpress.com/nxbooking/stop-finder" target="_blank">National Express <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the Scottish City Link website" href="http://www.citylink.co.uk/routes.php" target="_blank">Scottish Citylink <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
<div align="right"><a class="jpt" href="/Web2/Maps/NetworkMaps.aspx#logoTop">Return to top ^</a></div>  
<div class="hdtypetwo">
    <h2><a id="UndergroundMetro" name="UndergroundMetro">Underground/Metro</a></h2></div>  
		<p><a title="Open a new window displaying the Blackpool Trams website" href="http://www.blackpooltransport.com/services/maps" target="_blank">Blackpool Trams <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the Transport for London website (Croydon Tramlink)" href="http://www.tfl.gov.uk/gettingaround/9443.aspx" target="_blank">Croydon Tramlink <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the Transport for London website (Docklands Light Railway)" href="http://www.tfl.gov.uk/gettingaround/9441.aspx" target="_blank">Docklands Light Railway <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the Edinburgh Trams website" href="http://www.edinburghtrams.com" target="_blank">Edinburgh Trams <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the Glasgow Underground website" href="http://www.spt.co.uk/subway/about_the_subway.aspx" target="_blank">Glasgow Underground <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the London Underground website" href="http://www.tfl.gov.uk/gettingaround/1106.aspx" target="_blank">London Underground <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the Manchester Metrolink website (Route Map)" href="http://www.tfgm.com/journey_planning/Documents/PDFMaps/Metrolink-system-map.pdf" target="_blank">Manchester Metrolink <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the Nottingham Express Transit website" href="http://www.thetram.net/where-does-the-tram-run/" target="_blank">Nottingham Express Transit <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the Sheffield Supertram website (Route Map)" href="http://www.supertram.net/uploads/RouteMap.pdf" target="_blank">Sheffield Supertram <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the Tyne &amp; Wear Metro website" href="http://www.nexus.org.uk/travel-information/regional-maps" target="_blank">Tyne &amp; Wear Metro <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the West Midlands Metro website" href="http://www.networkwestmidlands.com/metro/trammap.aspx" target="_blank">West Midlands Metro <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
<div align="right"><a class="jpt" href="/Web2/Maps/NetworkMaps.aspx#logoTop">Return to top ^</a></div>  
<div class="hdtypetwo">
    <h2><a id="Bus" name="Bus">Bus</a></h2></div>  
		<p><a title="Open a new window displaying the Aberdeenshire Council website" href="http://www.aberdeenshire.gov.uk/publictransport/mapping/transport_guide.asp" target="_blank">Aberdeenshire Council <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the Angus Council website" href="http://www.angus.gov.uk/transport/maps/default.htm" target="_blank">Angus Council <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the Cambridgeshire website" href="http://www.cambridgeshire.gov.uk/transport/around/buses/busroutes.htm" target="_blank">Cambridgeshire <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the London Bus website" href="http://www.tfl.gov.uk/gettingaround/9440.aspx" target="_blank">London Bus <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the Stagecoach Devon website" href="http://www.devon.gov.uk/index/transport/public_transport/buses/bus_maps.htm" target="_blank">Stagecoach Devon <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the Transport for Greater Manchester website" href="http://www.tfgm.com/journey_planning/Pages/Network-maps.aspx" target="_blank">Transport for Greater Manchester <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the Trent Barton website" href="https://www.trentbarton.co.uk/bus-information/area-maps" target="_blank">Trent Barton <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the West Yorkshire Metro website" href="http://www.wymetro.com/BusTravel/mapsandguides/" target="_blank">West Yorkshire Metro <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
<div align="right"><a class="jpt" href="/Web2/Maps/NetworkMaps.aspx#logoTop">Return to top ^</a></div>  
<div class="hdtypetwo">
    <h2><a id="Ferries" name="Ferries">Ferries and River Services</a></h2></div>  
		<p><a title="Open a new window displaying the Transport for London website (London River Services)" href="http://www.tfl.gov.uk/gettingaround/21311.aspx" target="_blank">London River Services <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
<div align="right"><a class="jpt" href="/Web2/Maps/NetworkMaps.aspx#logoTop">Return to top ^</a></div>  
<div class="hdtypetwo">
    <h2><a id="Cycling" name="Cycling">Cycling</a></h2></div>  
		<p><a title="Open a new window displaying the Transport for London website (London Underground Bicycle Map)" href="http://www.tfl.gov.uk/assets/downloads/bicycle-tube-map.pdf" target="_blank">London Underground Bicycle Map <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
<div align="right"><a class="jpt" href="/Web2/Maps/NetworkMaps.aspx#logoTop">Return to top ^</a></div>  
<div class="hdtypetwo">
    <h2><a id="AllPublicTransport" name="AllPublicTransport">All Public Transport</a></h2></div>  
		<p><a title="Open a new window displaying the Blackpool website" href="http://www.blackpooltransport.com/services/maps" target="_blank">Blackpool <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>  
		<p><a title="Open a new window displaying the Hampshire website" href="http://www3.hants.gov.uk/passengertransport/ptmaps.htm" target="_blank">Hampshire <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p> 
		<p><a title="Open a new window displaying the Network West Midlands website" href="http://www.networkwestmidlands.com/Maps/maps-home.aspx" target="_blank">Network West Midlands <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p> 
		<p><a title="Open a new window displaying the Transport for Greater Manchester website" href="http://www.tfgm.com/journey_planning/Pages/Maps.aspx" target="_blank">Transport for Greater Manchester <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p> 
<div align="right"><a class="jpt" href="/Web2/Maps/NetworkMaps.aspx#logoTop">Return to top ^</a></div></div></div>'
,
'
<div id="primcontent">  <div id="contentarea">  <div class="hdtypetwo">
    <h2><a id="Rail" name="Rail">Rheilffordd</h2></a></div>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan maps of rail stations with access for people with reduced mobililty on the National Rail" href="http://www.nationalrail.co.uk/passenger_services/disabled_passengers/" target="_blank">Maps of rail stations with access for people with reduced mobility <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan National Rail" href="http://www.nationalrail.co.uk/passenger_services/maps/" target="_blank">National&nbsp;Rail <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan ScotRail" href="http://www.scotrail.co.uk/timetables-routes" target="_blank">ScotRail <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
<div align="right"><a class="jpt" href="/Web2/Maps/NetworkMaps.aspx#logoTop">Yn &#244;l i&#8217;r brig ^</a></div>  
<div class="hdtypetwo">
    <h2><a id="Coach" name="Coach">Coets</h2></a></div>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan Megabus" href="http://uk.megabus.com/BusStops.aspx" target="_blank">Megabus <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan National Express" href="http://coach.nationalexpress.com/nxbooking/stop-finder" target="_blank">National Express <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan Scottish City Link" href="http://www.citylink.co.uk/routes.php" target="_blank">Scottish Citylink <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
<div align="right"><a class="jpt" href="/Web2/Maps/NetworkMaps.aspx#logoTop">Yn &#244;l i&#8217;r brig ^</a></div>  
<div class="hdtypetwo">
    <h2><a id="UndergroundMetro" name="UndergroundMetro">Rheilffordd danddaearol/Metro</a></h2></div>  
    <p><a title="Agorwch ffenestr newydd yn arddangos gwefan Blackpool Trams" href="http://www.blackpooltransport.com/services/maps" target="_blank">Blackpool Trams <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
    <p><a title="Agorwch ffenestr newydd yn arddangos gwefan Transport for London website (Croydon Tramlink)" href="http://www.tfl.gov.uk/gettingaround/9443.aspx" target="_blank">Croydon Tramlink <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
    <p><a title="Agorwch ffenestr newydd yn arddangos gwefan Transport for London website (Docklands Light Railway)" href="http://www.tfl.gov.uk/gettingaround/9441.aspx" target="_blank">Docklands Light Railway <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
    <p><a title="Agorwch ffenestr newydd yn arddangos gwefan Edinburgh Trams website" href="http://www.edinburghtrams.com" target="_blank">Edinburgh Trams <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
    <p><a title="Agorwch ffenestr newydd yn arddangos gwefan Glasgow Underground" href="http://www.spt.co.uk/subway/about_the_subway.aspx" target="_blank">Glasgow Underground <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
    <p><a title="Agorwch ffenestr newydd yn arddangos gwefan London Underground" href="http://www.tfl.gov.uk/gettingaround/1106.aspx" target="_blank">London Underground <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
    <p><a title="Agorwch ffenestr newydd yn arddangos gwefan Manchester Metrolink website (Route Map)" href="http://www.tfgm.com/journey_planning/Documents/PDFMaps/Metrolink-system-map.pdf" target="_blank">Manchester Metrolink <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
    <p><a title="Agorwch ffenestr newydd yn arddangos gwefan Nottingham Express Transit" href="http://www.thetram.net/where-does-the-tram-run/" target="_blank">Nottingham Express Transit <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
    <p><a title="Agorwch ffenestr newydd yn arddangos gwefan Sheffield Supertram website (Route Map)" href="http://www.supertram.net/uploads/RouteMap.pdf" target="_blank">Sheffield Supertram <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
    <p><a title="Agorwch ffenestr newydd yn arddangos gwefan Tyne &amp; Wear Metro" href="http://www.nexus.org.uk/travel-information/regional-maps" target="_blank">Tyne &amp; Wear Metro <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
	<p><a title="Agorwch ffenestr newydd yn arddangos gwefan West Midlands Metro" href="http://www.networkwestmidlands.com/metro/trammap.aspx" target="_blank">West Midlands Metro <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  

<div align="right"><a class="jpt" href="/Web2/Maps/NetworkMaps.aspx#logoTop">Yn &#244;l i&#8217;r brig ^</a></div>  
<div class="hdtypetwo">
    <h2><a id="Bus" name="Bus">Bws</a></h2></div>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan Aberdeenshire Council" href="http://www.aberdeenshire.gov.uk/publictransport/mapping/transport_guide.asp" target="_blank">Aberdeenshire Council <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan Angus Council" href="http://www.angus.gov.uk/transport/maps/default.htm" target="_blank">Angus Council <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan Cambridgeshire" href="http://www.cambridgeshire.gov.uk/transport/around/buses/busroutes.htm" target="_blank">Cambridgeshire <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan London Bus" href="http://www.tfl.gov.uk/gettingaround/9440.aspx" target="_blank">London Bus <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan Stagecoach Devon" href="http://www.devon.gov.uk/index/public_transport/buses/bus_maps.htm" target="_blank">Stagecoach Devon <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan Transport for Greater Manchester" href="http://www.tfgm.com/journey_planning/Pages/Network-maps.aspx" target="_blank">Transport for Greater Manchester <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan Trent Barton" href="http://www.trentbarton.co.uk/bus-information/area-maps" target="_blank">Trent Barton <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan West Yorkshire Metro" href="http://www.wymetro.com/BusTravel/mapsandguides/" target="_blank">West Yorkshire Metro <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
<div align="right"><a class="jpt" href="/Web2/Maps/NetworkMaps.aspx#logoTop">Yn &#244;l i&#8217;r brig ^</a></div>  
<div class="hdtypetwo">
    <h2><a id="Ferries" name="Ferries">Fferïau a Gwasanaethau Afon</a></h2></div>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan Transport for London website (London River Services)" href="http://www.tfl.gov.uk/gettingaround/21311.aspx" target="_blank">London River Services <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
<div align="right"><a class="jpt" href="/Web2/Maps/NetworkMaps.aspx#logoTop">Yn &#244;l i&#8217;r brig ^</a></div>  
<div class="hdtypetwo">
    <h2><a id="Cycling" name="Cycling">Beicio</a></h2></div>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan Transport for London (London Underground Bicycle Map)" href="http://www.tfl.gov.uk/assets/downloads/bicycle-tube-map.pdf" target="_blank">London Underground Bicycle Map <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
<div align="right"><a class="jpt" href="/Web2/Maps/NetworkMaps.aspx#logoTop">Yn &#244;l i&#8217;r brig ^</a></div>  
<div class="hdtypetwo">
    <h2><a id="AllPublicTransport" name="AllPublicTransport">All Public Transport</a></h2></div>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan Blackpool" href="http://www.blackpooltransport.com/services/maps" target="_blank">Blackpool <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan Hampshire" href="http://www3.hants.gov.uk/passengertransport/ptmaps.htm" target="_blank">Hampshire <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>  
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan Network West Midlands" href="http://www.networkwestmidlands.com/Maps/maps-home.aspx" target="_blank">Network West Midlands <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p> 
		<p><a title="Agorwch ffenestr newydd yn arddangos gwefan Transport for Greater Manchester" href="http://www.tfgm.com/journey_planning/Pages/Maps.aspx" target="_blank">Transport for Greater Manchester <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p> 
<div align="right"><a class="jpt" href="/Web2/Maps/NetworkMaps.aspx#logoTop">Yn &#244;l i&#8217;r brig ^</a></div>  
<div class="hdtypetwo"></div></div>'


EXEC AddtblContent
1, @GroupId,'Title Text','/Channels/TransportDirect/Maps/NetworkMaps',
'<table>
	<tr>
        <td><img src="/Web2/App_Themes/TransportDirect/images/gifs/softcontent/NetworkMapsLargeIcon.gif" alt="Network maps" width="70px" height="36px" /></td>
        <td><h1>Network maps</h1></td>
	</tr>
</table>'
,
'<table>
	<tr>
		<td><img src="/Web2/App_Themes/TransportDirect/images/gifs/softcontent/NetworkMapsLargeIcon.gif" alt="Mapiau rhwydwaith" width="70px" height="36px" /></td>
		<td><h1>Mapiau rhwydwaith</h1></td>
	</tr>
</table>'


GO
-------------------------------------------------------------------------
-- CHANGE LOG
-------------------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10009
SET @ScriptDesc = 'Updated content to the Networks Map page'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.27  $'

IF EXISTS (SELECT * FROM [dbo].[ChangeCatalogue] WHERE ScriptNumber = @ScriptNumber)
  BEGIN
    UPDATE [dbo].[ChangeCatalogue]
    SET ChangeDate = getDate(), Summary = @ScriptDesc, VersionInfo = @VersionInfo
    WHERE ScriptNumber = @ScriptNumber
  END
ELSE
  BEGIN
    INSERT INTO [dbo].[ChangeCatalogue] (ScriptNumber, ChangeDate, Summary, VersionInfo)
    VALUES (@ScriptNumber, getDate(), @ScriptDesc, @VersionInfo)
  END
GO

----------------------------------------------------------------------------