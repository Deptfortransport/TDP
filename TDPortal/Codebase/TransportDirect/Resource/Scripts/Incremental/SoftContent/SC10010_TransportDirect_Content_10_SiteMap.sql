-- *************************************************************************************
-- NAME 		: SC10010_TransportDirect_Content_10_SiteMap.sql
-- DESCRIPTION  	: Updates to Sitemap (all columns)
-- AUTHOR		: Mitesh Modi
-- *************************************************************************************

USE [Content]
GO

-------------------------------------------------------------
-- Site map
-------------------------------------------------------------

-- Plan a journey

EXEC AddtblContent
1, 43, 'JourneyPlannerTitle', '/Channels/TransportDirect/SiteMap/SiteMap', 
'<h2>Find a place</h2>',
'<h2>Canfyddwch le</h2>'

EXEC AddtblContent
1, 43, 'JourneyPlannerBody', '/Channels/TransportDirect/SiteMap/SiteMap', 
'
<div class="smcSiteMapLink"><a href="/Web2/Maps/FindMapInput.aspx">Find a map of a location</a><br /></div>
<div class="smcSiteMapSubCategory">
<div class="smcSiteMapSubCategoryContent">Plan a journey to location</div>
<div class="smcSiteMapSubCategoryContent">Plan a journey from location</div>
</div> 
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/FindAStationInput.aspx?NotFindAMode=true">Find nearest station/airport</a><br /></div>
<div class="smcSiteMapSubCategory">
<div class="smcSiteMapSubCategoryContent">Travel to station/airport</div>
<div class="smcSiteMapSubCategoryContent">Travel from station/airport</div>
</div> 
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/FindCarParkInput.aspx?NotFindAMode=true&amp;DriveFromTo=true">Find nearest car park</a><br /></div>
<div class="smcSiteMapSubCategory">
<div class="smcSiteMapSubCategoryContent">Drive to this car park</div>
<div class="smcSiteMapSubCategoryContent">Drive from this car park</div>
</div>
<div class="smcSiteMapLink"><a href="/Web2/Maps/NetworkMaps.aspx">Transport network maps</a>
<ul class="lister">
<li><a href="/Web2/Maps/NetworkMaps.aspx#Rail">Rail</a></li>
<li><a href="/Web2/Maps/NetworkMaps.aspx#Coach">Coach </a></li>
<li><a href="/Web2/Maps/NetworkMaps.aspx#UndergroundMetro">Underground/Metro </a></li>
<li><a href="/Web2/Maps/NetworkMaps.aspx#Bus">Bus </a></li></ul></div>
<div class="smcSiteMapLink"><a href="/Web2/Maps/TrafficMaps.aspx">Predicted traffic level maps</a><br /></div>', 

'
<div class="smcSiteMapLink"><a href="/Web2/Maps/FindMapInput.aspx">Darganfyddwch fap o leoliad</a><br /></div>
<div class="smcSiteMapSubCategory">
<div class="smcSiteMapSubCategoryContent">Cynllunio taith i leoliad</div>
<div class="smcSiteMapSubCategoryContent">Cynllunio taith o leoliad</div>
</div> 
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/FindAStationInput.aspx?NotFindAMode=true">Dod o hyd i''r orsaf/maes awyr agosaf</a><br /></div>
<div class="smcSiteMapSubCategory">
<div class="smcSiteMapSubCategoryContent">Teithio i orsaf/faes awyr</div>
<div class="smcSiteMapSubCategoryContent">Teithio o orsaf/faes awyr</div>
</div> 
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/FindACarParkInput.aspx?NotFindAMode=true&amp;DriveFromTo=true">Darganfod maes parcio</a><br /></div>
<div class="smcSiteMapSubCategory">
<div class="smcSiteMapSubCategoryContent">Gyrru i''r maes parcio hwn</div>
<div class="smcSiteMapSubCategoryContent">Gyrru o''r maes parcio hwn</div>
</div>
<div class="smcSiteMapLink"><a href="/Web2/Maps/NetworkMaps.aspx">Mapiau''r rhwydwaith cludiant</a> 
<ul class="lister">
<li><a href="/Web2/Maps/NetworkMaps.aspx#Rail">Rheilffordd </a></li>
<li><a href="/Web2/Maps/NetworkMaps.aspx#Coach">Bws moethus </a></li>
<li><a href="/Web2/Maps/NetworkMaps.aspx#UndergroundMetro">Tr&#234;n tanddaearol/metro</a></li>
<li><a href="/Web2/Maps/NetworkMaps.aspx#Bus">Bws </a></li></ul></div>
<div class="smcSiteMapLink"><a href="/Web2/Maps/TrafficMaps.aspx">Mapiau lefelau trafnidiaeth rhagweledig</a><br /></div>'
GO

-- Live travel
EXEC AddtblContent
1, 43, 'LiveTravelTitle', '/Channels/TransportDirect/SiteMap/SiteMap', 
'<h2>Tips and tools</h2>',
'<h2>Awgrymiadau a theclynnau</h2>'

EXEC AddtblContent
1, 43, 'LiveTravelBody', '/Channels/TransportDirect/SiteMap/SiteMap', 
'
<div class="smcSiteMapLink"><a href="/Web2/Tools/BusinessLinks.aspx">Link to our website</a> <br /></div>
<div class="smcSiteMapLink"><a href="/Web2/Tools/ToolbarDownload.aspx">Download our toolbar</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/TDOnTheMove/TDOnTheMove.aspx">Mobile demonstrator</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx">Check Journey CO2</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/ContactUs/FeedbackPage.aspx">Provide feedback</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/About/RelatedSites.aspx">Related sites</a><br />
<ul class="lister">
<li><a href="/Web2/About/RelatedSites.aspx#NationalTransport">National transport</a></li>
<li><a href="/Web2/About/RelatedSites.aspx#LocalPublicTransport">Local public transport</a></li>
<li><a href="/Web2/About/RelatedSites.aspx#Motoring">Motoring</a></li>
<li><a href="/Web2/About/RelatedSites.aspx#MotoringCosts">Motoring Costs</a></li>
<li><a href="/Web2/About/RelatedSites.aspx#CarSharing">Car Sharing</a></li>
<li><a href="/Web2/About/RelatedSites.aspx#Government">Government</a> </li></ul></div>
<div class="smcSiteMapLink"><a href="/Web2/Help/NewHelp.aspx">Frequently Asked Questions</a><br /></div>
<p><a href="/Web2/LoginRegister.aspx">Login/Register</a></p>
<div class="smcSiteMapSubCategory">
<div class="smcSiteMapSubCategoryContent">Save Preferences</div>
<div class="smcSiteMapSubCategoryContent">Save Favourites</div>
<div class="smcSiteMapSubCategoryContent">Send to Friend</div>
</div>
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/FindEBCInput.aspx">Freight Grants - Environmental Benefits Calculator</a><br /></div>', 

'
<div class="smcSiteMapLink"><a href="/Web2/Tools/BusinessLinks.aspx">Creu dolen i''n gwefan</a> <br /></div>
<div class="smcSiteMapLink"><a href="/Web2/Tools/ToolbarDownload.aspx">Lawrlwythwch ein bar offer</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/TDOnTheMove/TDOnTheMove.aspx">Arddangosydd symudol</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx">Mesur CO2 y siwrnai</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/ContactUs/FeedbackPage.aspx">Rhowch adborth</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/About/RelatedSites.aspx">Safleoedd cysylltiedig</a><br />
<ul class="lister">
<li><a href="/Web2/About/RelatedSites.aspx#NationalTransport">Cludiant cenedlaethol</a></li>
<li><a href="/Web2/About/RelatedSites.aspx#LocalPublicTransport">Cludiant cyhoeddus lleol</a></li>
<li><a href="/Web2/About/RelatedSites.aspx#Motoring">Moduro</a></li>
<li><a href="/Web2/About/RelatedSites.aspx#MotoringCosts">Costau moduro</a></li>
<li><a href="/Web2/About/RelatedSites.aspx#CarSharing">Rhannu ceir</a></li>
<li><a href="/Web2/About/RelatedSites.aspx#Government">Llywodraeth</a> </li></ul></div>
<div class="smcSiteMapLink"><a href="/Web2/Help/NewHelp.aspx">Cwestiynau a Ofynnir yn Aml</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/Tools/Home.aspx#digitvinfo">Deledu digidol</a><br /></div>
<p><a href="/Web2/LoginRegister.aspx">Logiwch i mewn/Cofrestrwch</a></p>
<div class="smcSiteMapSubCategory">
<div class="smcSiteMapSubCategoryContent">Cadw hoffterau</div>
<div class="smcSiteMapSubCategoryContent">Cadwch fel hoff siwrnai</div>
<div class="smcSiteMapSubCategoryContent">Anfonwch at ffrind</div>
</div>
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/FindEBCInput.aspx">Grantiau Cludo Nwyddau - Cyfrifiannell Buddion Amgylcheddol</a><br /></div>'
GO

-- Maps
EXEC AddtblContent
1, 43, 'MapsTitle', '/Channels/TransportDirect/SiteMap/SiteMap', 
'<h2>Live travel</h2>',
'<h2>Teithio byw</h2>'

EXEC AddtblContent
1, 43, 'MapsBody', '/Channels/TransportDirect/SiteMap/SiteMap', 
'
<div class="smcSiteMapLink"><a href="/Web2/LiveTravel/TravelNews.aspx">Table of live travel news</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/LiveTravel/DepartureBoards.aspx">Departure board information</a><br />
<ul class="lister">
<li><a href="/Web2/LiveTravel/DepartureBoards.aspx#Train">Train departure board</a></li>
<li><a href="/Web2/LiveTravel/DepartureBoards.aspx#top">Bus departure board</a></li>
</ul></div>', 

'
<div class="smcSiteMapLink"><a href="/Web2/LiveTravel/TravelNews.aspx">Tabl o newyddion teithio byw</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/LiveTravel/DepartureBoards.aspx">Gwybodaeth am y bwrdd ymadael</a><br />
<ul class="lister">
<li><a href="/Web2/LiveTravel/DepartureBoards.aspx#Train">Bwrdd trenau yn ymadael</a></li>
<li><a href="/Web2/LiveTravel/DepartureBoards.aspx#top">Bwrdd bysiau yn ymadael</a></li>
</ul></div>'
GO

-- Quick planners
EXEC AddtblContent
1, 43, 'QuickPlannersTitle', '/Channels/TransportDirect/SiteMap/SiteMap',
'<h2>Plan a journey</h2>',
'<h2>Cynlluniwch siwrnai</h2>'

EXEC AddtblContent
1, 43, 'QuickPlannersBody', '/Channels/TransportDirect/SiteMap/SiteMap',
'
<div class="smcSiteMapLink">
    <a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx">Door-to-door journey planner</a><br />
</div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Ticket/Costs</div>
    <div class="smcSiteMapSubCategoryContent">Buy Tickets</div>
    <div class="smcSiteMapSubCategoryContent">Check CO2</div>
</div> 
<div class="smcSiteMapLink">
    <a href="/Web2/JourneyPlanning/FindTrainInput.aspx">Find a train</a><br />
</div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Quickest</div>
    <div class="smcSiteMapSubCategoryContent">Cheapest</div>
    <div class="smcSiteMapSubCategoryContent">Ticket/Costs</div>
    <div class="smcSiteMapSubCategoryContent">Buy Tickets</div>
    <div class="smcSiteMapSubCategoryContent">Check CO2</div>
</div>
<div class="smcSiteMapLink">
    <a href="/Web2/JourneyPlanning/FindTrainCostInput.aspx">Find cheaper rail fares</a><br />
</div>
<div class="smcSiteMapLink">
    <a href="/Web2/JourneyPlanning/FindFlightInput.aspx">Find a flight</a><br />
</div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Ticket/Costs</div>
    <div class="smcSiteMapSubCategoryContent">Check CO2</div>
</div>
<div class="smcSiteMapLink">
    <a href="/Web2/JourneyPlanning/FindCarInput.aspx">Find a car route</a><br />
</div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Fastest</div>
    <div class="smcSiteMapSubCategoryContent">Shortest</div>
    <div class="smcSiteMapSubCategoryContent">Most fuel economic</div>
    <div class="smcSiteMapSubCategoryContent">Cheapest overall</div>
    <div class="smcSiteMapSubCategoryContent">Costs</div>
    <div class="smcSiteMapSubCategoryContent">Check CO2</div>
</div>
<div>
    <ul class="lister"> 
        <li><a href="/Web2/JourneyPlanning/ParkAndRide.aspx">Park and ride schemes</a></li>
    </ul>
</div>
<div class="smcSiteMapLink">
    <a href="/Web2/JourneyPlanning/FindCoachInput.aspx">Find a coach</a><br />
</div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Ticket/Costs</div>
    <div class="smcSiteMapSubCategoryContent">Check CO2</div>
</div>
<div class="smcSiteMapLink">
    <a href="/Web2/JourneyPlanning/FindTrunkInput.aspx?ClassicMode=true">Compare city-to-city journeys</a><br />
</div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Ticket/Costs</div>
    <div class="smcSiteMapSubCategoryContent">Check CO2</div>
</div>
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/VisitPlannerInput.aspx">Day trip planner</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/FindCarParkInput.aspx?DriveFromTo=true">Drive to a car park</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to Park and Ride</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/FindBusInput.aspx">Find a bus</a></div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Ticket/Costs</div>
    <div class="smcSiteMapSubCategoryContent">Check CO2</div>
</div>
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/FindCycleInput.aspx">Find a cycle route</a><br />
</div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Check CO2</div>
</div>
',

'
<div class="smcSiteMapLink">
    <a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx">Cynlluniwr siwrnai drws-i-ddrws</a><br />
</div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Tocynnau/Costau</div>
    <div class="smcSiteMapSubCategoryContent">Prynwch tocynnau</div>
    <div class="smcSiteMapSubCategoryContent">Mesur CO2</div>
</div>
<div class="smcSiteMapLink">
    <a href="/Web2/JourneyPlanning/FindTrainInput.aspx">Canfyddwch dr&#234;n</a><br />
</div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Cyflymaf</div>
    <div class="smcSiteMapSubCategoryContent">Rhataf yn gyffredinol</div>
    <div class="smcSiteMapSubCategoryContent">Tocynnau/Costau</div>
    <div class="smcSiteMapSubCategoryContent">Prynwch tocynnau</div>
    <div class="smcSiteMapSubCategoryContent">Mesur CO2</div>
</div>
<div class="smcSiteMapLink">
    <a href="/Web2/JourneyPlanning/FindTrainCostInput.aspx">Canfod tocynnau tr''n rhatach</a><br />
</div>
<div class="smcSiteMapLink">
    <a href="/Web2/JourneyPlanning/FindFlightInput.aspx">Canfyddwch ehediad</a><br />
</div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Tocynnau/Costau</div>
    <div class="smcSiteMapSubCategoryContent">Mesur CO2</div>
</div>
<div class="smcSiteMapLink">
    <a href="/Web2/JourneyPlanning/FindCarInput.aspx">Canfyddwch lwybr car</a><br />
</div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Cyflymaf</div>
    <div class="smcSiteMapSubCategoryContent">Byrraf</div>
    <div class="smcSiteMapSubCategoryContent">Mwyaf economaidd o ran tanwydd</div>
    <div class="smcSiteMapSubCategoryContent">Rhataf yn gyffredinol</div>
    <div class="smcSiteMapSubCategoryContent">Costau</div>
    <div class="smcSiteMapSubCategoryContent">Mesur CO2</div>
</div>
<div>
    <ul class="lister"> 
        <li><a href="/Web2/JourneyPlanning/ParkAndRide.aspx">Cynlluniau parcio a theithio</a></li>
    </ul>
</div>
<div class="smcSiteMapLink">
    <a href="/Web2/JourneyPlanning/FindCoachInput.aspx">Canfyddwch fws moethus</a><br />
</div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Tocynnau/Costau</div>
    <div class="smcSiteMapSubCategoryContent">Mesur CO2</div>
</div>
<div class="smcSiteMapLink">
    <a href="/Web2/JourneyPlanning/FindTrunkInput.aspx">Cymharu siwrneion dinas-i-ddinas</a><br />
</div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Tocynnau/Costau</div>
    <div class="smcSiteMapSubCategoryContent">Mesur CO2</div>
</div>
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/VisitPlannerInput.aspx">Cynllunydd teithiau dydd</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/FindCarParkInput.aspx?DriveFromTo=true">Gyrru i faes parcio</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Cynlluniwch i barcio a theithio</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/FindBusInput.aspx">Canfyddwch fws</a></div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Tocynnau/Costau</div>
    <div class="smcSiteMapSubCategoryContent">Mesur CO2</div>
</div>
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/FindCycleInput.aspx">Canfod llwybr beicio</a><br />
</div>
<div class="smcSiteMapSubCategory">
    <div class="smcSiteMapSubCategoryContent">Mesur CO2</div>
</div>
'


-- TDOnTheMove
EXEC AddtblContent
1, 43, 'TDOnTheMoveTitle', '/Channels/TransportDirect/SiteMap/SiteMap',
'<h2>About us</h2>',
'<h2>Amdanom ni</h2>'

EXEC AddtblContent
1, 43, 'TDOnTheMoveBody', '/Channels/TransportDirect/SiteMap/SiteMap',
'
<div class="smcSiteMapLink"><a href="/Web2/About/AboutUs.aspx">About us</a><br />
<ul class="lister">
<li><a href="/Web2/About/AboutUs.aspx#EnablingIntelligentTravel">Enabling intelligent travel</a></li>
<li><a href="/Web2/About/AboutUs.aspx#WhoOperates">Who operates Transport Direct? </a></li>
<li><a href="/Web2/About/AboutUs.aspx#WhoBuilds">Who builds this site?</a></li></ul></div>
<div class="smcSiteMapLink"><a href="/Web2/About/Accessibility.aspx">Accessibility</a> <br /></div>
<div class="smcSiteMapLink"><a href="/Web2/ContactUs/Details.aspx">Contact details</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/About/DataProviders.aspx">Data providers</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/About/PrivacyPolicy.aspx">Privacy policy</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/About/TermsConditions.aspx">Terms &amp; conditions</a><br /></div>', 

'
<div class="smcSiteMapLink"><a href="/Web2/About/AboutUs.aspx">Amdanom ni</a><br />
<ul class="lister">
<li><a href="/Web2/About/AboutUs.aspx#EnablingIntelligentTravel">Galluogi teithio deallus</a></li>
<li><a href="/Web2/About/AboutUs.aspx#WhoOperates">Pwy sy''n gweithredu Transport Direct? </a></li>
<li><a href="/Web2/About/AboutUs.aspx#WhoBuilds">Pwy sy''n adeiladu a datblygu''r safle hwn?</a></li></ul></div>
<div class="smcSiteMapLink"><a href="/Web2/About/Accessibility.aspx">Hygyrchedd</a> <br /></div>
<div class="smcSiteMapLink"><a href="/Web2/ContactUs/Details.aspx">Manylion cyswllt</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/About/DataProviders.aspx">Darparwyr data</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/About/PrivacyPolicy.aspx">Polisi preifatrwydd</a><br /></div>
<div class="smcSiteMapLink"><a href="/Web2/About/TermsConditions.aspx">Amodau a thelerau</a><br /></div>
'
GO

EXEC AddtblContent
1, 43, 'sitemapFooterNote', '/Channels/TransportDirect/SiteMap/SiteMap',
'NOTE Items in black are secondary options of the preceding blue action
<br />
<br />',
'NODYN Opsiynau eilaidd y gweithrediad blaenorol glas yw''r eitemau mewn du
<br />
<br />'
GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10010
SET @ScriptDesc = 'Updates to Sitemap'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.17  $'

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

