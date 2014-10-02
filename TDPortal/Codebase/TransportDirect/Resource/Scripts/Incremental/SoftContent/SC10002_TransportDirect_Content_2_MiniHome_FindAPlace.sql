-- *************************************************************************************
-- NAME 		: SC10002_TransportDirect_Content_2_MiniHome_FindAPlace.sql
-- DESCRIPTION  : Updates to Find a Place homepage information panel
-- AUTHOR		: Mitesh Modi
-- DATE			: 16 Apr 2008 15:00:00
-- *************************************************************************************

USE [Content]
GO

DECLARE @GroupId int,
	@ThemeId int
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'maps_home')
SET @ThemeId = 1

-- Add the html text
EXEC AddtblContent
@ThemeId, @GroupId, 'FindAPlaceInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Maps/Home'
-- ENGLISH
,
'<div class="MinihomeHyperlinksInfoContent">
<div class="MinihomeHyperlinksInfoHeader">
<div class="txtsevenbbl">
<h2>Finding the place you want</h2>
</div>
<!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>

<div class="MinihomeSoftContent">
<h3>Find me a map...</h3><br />
<br />

<p>You can <a href="/Web2/Maps/FindMapInput.aspx">find a map</a> of a place,
city, attraction, station, stop, address or postcode.</p>

<p>&nbsp;</p><img style="PADDING-LEFT: 10px" alt=
"Image of a map showing a specific location" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceLocationMap1.JPG"
align="right" border="0" />

<p>You can view the local transport stops and stations in the area and local amenities
such as hotels, attractions and public facilities.</p><br clear="right" />

<p>&nbsp;</p>

<h3>Now plan me a journey there...</h3>

<p>&nbsp;</p>

<p><img style="PADDING-RIGHT: 70px; PADDING-LEFT: 10px" alt=
"Screenshot of a Transport Direct map and the buttons from which to plan a journey" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceMapPlan1.JPG"
align="right" border="0" />You can then choose to plan a journey to or from the location
shown on the map.</p><br clear="right" />

<p>&nbsp;</p>

<h3>Where''s the nearest transport?</h3>

<p>&nbsp;</p>

<p>Alternatively, you may wish to <a href=
"/Web2/JourneyPlanning/FindAStationInput.aspx">find the nearest stations &amp;
airports</a> to a location.</p><br />
<img alt=
"Screenshot of the output from the Find nearest station/airport feature on Transport Direct"
src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceNearest1.JPG"
align="right" border="0" />

<p>You can view them as a list, in order of their distance from your
location...</p><br clear="right" />

<p>&nbsp;</p>

<p><img style="PADDING-RIGHT: 10px" alt="Image of map showing numbered locations" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceNearestMap1.JPG"
align="left" border="0" />...or you can view them on a map.</p><br clear="left" />

<p>&nbsp;</p>

<h3>Now plan me a journey there...</h3>

<p>&nbsp;</p>

<p><img style="PADDING-LEFT: 10px" alt=
"Screenshot of the results from the Find nearest station/airport feature on Transport Direct"
src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceStationJourneyResults.JPG"
align="right" border="0" />The closest station/airport is already selected. Once you have selected 
the station/airport required, click Travel from or Travel to. Then repeat for the other journey 
leg and click next.<br />
<br />
You will then be able to ''Find journeys between stations/airports'' from the selection you
have made.<br />
<br />
You may not always get journeys to or from all of your selected stations but the results
returned will be the best journeys available between the chosen origin(s) and
destination(s).</p><br clear="right" />

<p>&nbsp;</p>

<h3>Where''s the nearest car park?</h3>

<p>&nbsp;</p>

<p>Similarly, you may wish to <a href="/Web2/JourneyPlanning/FindCarParkInput.aspx">find
the nearest car parks</a> to a location.</p><br />
<img alt=
"Screenshot of the output from the Find nearest car park feature on Transport Direct"
src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceCarParkResults.JPG"
align="right" border="0" />

<p>You can view them as a list, in order of their distance from your location, the total number 
of spaces, and if they have disabled spaces...</p><br clear="right" />

<p>&nbsp;</p>

<p><img style="PADDING-RIGHT: 10px" alt="Image of map showing available car parks" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceCarParkMap.JPG"
align="left" border="0" />...or you can view them on a map.</p><br clear="left" />

<p>&nbsp;</p>

<h3>Now plan me a journey there...</h3>

<p>&nbsp;</p>

<p><img style="PADDING-LEFT: 10px" alt=
"Screenshot of the output from the Find nearest car park feature on Transport Direct"
src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceCarParkJourneyResults.JPG"
align="right" border="0" />The closest car park is already selected. Once you have selected
the option required, click Drive from or Drive to.<br />
<br />
You will then be able to ''Plan a car route'' from/to the car park selected.</p>
<br clear="right" />

<p>&nbsp;</p>

<h3>What will the roads be like?</h3>

<p>&nbsp;</p>

<p>If you are planning to visit a place, you may want to know what the traffic will be
like when you travel there.</p><br />

<p><img style="PADDING-LEFT: 10px" alt=
"Image of a map showing predicted traffic levels using different colours to represent estimated congestion"
src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceTrafficMap1.JPG"
align="right" border="0" />Our <a href="/Web2/Maps/TrafficMaps.aspx">traffic maps</a> are
based on recorded traffic levels over the last few years and display an accurate
prediction of the likely traffic levels at your precise time of travel.</p>
<br clear="right" />

<p>&nbsp;</p>

<h3>I need a tube map...</h3>

<p>&nbsp;</p>

<p>Finally, you may just need a map showing part of the rail network, bus routes or a map
of the London Underground. Our <a href="/Web2/Maps/NetworkMaps.aspx">transport network
maps</a> page links you to a range of sites containing these maps.</p>
</div>
</div>
'
-- WELSH
,
'<div class="MinihomeHyperlinksInfoContent">
<div class="MinihomeHyperlinksInfoHeader">
<div class="txtsevenbbl">
<h2>Dywedwch fwy wrthyf</h2>
</div>
<!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>

<div class="MinihomeSoftContent">
<h3>Darganfyddwch fap i mi...</h3><br />
<br />

<p>Gallwch <a href="/Web2/Maps/FindMapInput.aspx">ddarganfyddwch map</a> o le, dinas, atyniad, gorsaf, arhosfan, 
cyfeiriad neu god post.</p>

<p>&nbsp;</p><img style="PADDING-LEFT: 10px" alt=
"Delwedd o fap yn dangos lleoliad penodol" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceLocationMap1.JPG"
align="right" border="0" />

<p>Gallwch weld yr arosfannau a’r gorsafoedd cludiant lleol yn yr ardal a mwynderau lleol fel gwestai, 
atyniadau a chyfleusterau cyhoeddus.</p><br clear="right" />

<p>&nbsp;</p>

<h3>Yn awr cynlluniwch siwrnai i mi yno...</h3>

<p>&nbsp;</p>

<p><img style="PADDING-RIGHT: 70px; PADDING-LEFT: 10px" alt=
"Delwedd sgrîn o fap Transport Direct a’r botymau y bwriadwch gynllunio siwrneion ohonynt" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceMapPlan1.JPG"
align="right" border="0" />Yna gallwch ddewis cynllunio siwrnai i neu o’r lleoliad a ddangosir ar y map.</p><br clear="right" />

<p>&nbsp;</p>

<h3>Ble mae’r cludiant agosaf?</h3>

<p>&nbsp;</p>

<p>Neu efallai y dymunwch <a href=
"/Web2/JourneyPlanning/FindAStationInput.aspx">dod o hyd i''r gorsafoedd a''r meysydd awyr agosaf</a> at leoliad.</p><br />
<img alt=
"Delwedd sgrîn o’r allbwn o’r nodwedd Dod ohyd i''r orsaf/maes awyr agosaf ar Transport Direct"
src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceNearest1.JPG"
align="right" border="0" />

<p>Gallwch edrych arnynt fel rhestr, yn nhrefn eu pellter o’ch lleoliad...</p><br clear="right" />

<p>&nbsp;</p>

<p><img style="PADDING-RIGHT: 10px" alt="Delwedd o fap yn dangos lleoliadau wedi eu rhifo" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceNearestMap1.JPG"
align="left" border="0" />...neu gallwch eu gweld ar fap.</p><br clear="left" />

<p>&nbsp;</p>

<h3>Yn awr cynlluniwch siwrnai i mi yno...</h3>

<p>&nbsp;</p>

<p><img style="PADDING-LEFT: 10px" alt=
"Llun sgrîn o’r allbwn o’r nodwedd Dod o hyd i''r orsaf/maes awyr agosaf ar Transport Direct"
src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceStationJourneyResults.JPG"
align="right" border="0" />Mae’r orsaf/maes awyr agosaf wedi ei dicio yn barod. Wedi i chi ddewis yr 
opsiwn/opsiynau angenrheidiol, cliciwch ar Teithio o neu Teithio i. Yna ailadroddwch hyn ar gyfer yr 
adran arall o’r siwrnai a chliciwch nesaf.<br />
<br />
Yna byddwch yn gallu ‘Darganfyddwch siwrneion rhwng gorsafoedd/meysydd awyr’ o’r dewis yr ydych wedi ei wneud.<br />
<br />
Efallai na fydd wastad yn bosibl i chi gael siwrneion sy''n gadael neu''n cyrraedd pob gorsaf a ddewiswyd gennych ond 
byddwn yn dychwelyd manylion y siwrneion gorau sydd ar gael rhwng y man(nau) cychwyn a''r cyrchfan(nau) a ddewiswyd.</p><br clear="right" />

<p>&nbsp;</p>

<h3>Ble mae’r maes parcio agosaf?</h3>

<p>&nbsp;</p>

<p>Yn yr un modd, mae’n bosibl y byddwch yn dymuno <a href="/Web2/JourneyPlanning/FindCarParkInput.aspx">darganfod 
y meysydd parcio agosaf</a> i leoliad.</p><br />
<img alt=
"Llun sgrîn o’r allbwn o’r nodwedd Darganfyddwch y maes parcio agosaf ar Transport Direct"
src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceCarParkResults.JPG"
align="right" border="0" />

<p>Gallwch eu gweld fel rhestr, yn nhrefn eu pellter o’ch lleoliad chi...</p><br clear="right" />

<p>&nbsp;</p>

<p><img style="PADDING-RIGHT: 10px" alt="Delwedd o fap yn dangos y meysydd parcio sydd ar gael" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceCarParkMap.JPG"
align="left" border="0" />...neu gallwch eu gweld ar fap.</p><br clear="left" />

<p>&nbsp;</p>

<h3>Yn awr cynlluniwch siwrnai i mi yno...</h3>

<p>&nbsp;</p>

<p><img style="PADDING-LEFT: 10px" alt=
"Llun sgrîn o’r allbwn o’r nodwedd Darganfyddwch y maes parcio agosaf ar Transport Direct"
src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceCarParkJourneyResults.JPG"
align="right" border="0" />Mae’r maes parcio agosaf wedi ei ddewis yn barod. Wedi i chi ddewis yr 
opsiwn angenrheidiol, cliciwch ar Gyrrwch o neu Gyrrwch i<br />
<br />
Yna byddwch yn gallu ‘Cynlluniwch lwybr car’ o/i’r maes parcio a ddewiswyd.</p>
<br clear="right" />

<p>&nbsp;</p>

<h3>Sut rai fydd y ffyrdd?</h3>

<p>&nbsp;</p>

<p>Os ydych chi’n cynllunio ymweld â lle, efallai y byddwch yn dymuno gwybod sut fydd y traffig pan 
deithiwch yno.</p><br />

<p><img style="PADDING-LEFT: 10px" alt=
"Delwedd o fap yn dangos y lefelau traffig a ragfynegir gan ddefnyddio lliwiau gwahanol i gynrychioli amcangyfrif o’r tagfeydd"
src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceTrafficMap1.JPG"
align="right" border="0" />Seilir ein <a href="/Web2/Maps/TrafficMaps.aspx">mapiau traffig</a> ar lefelau traffig a gofnodwyd dros
 yr ychydig flynyddoedd diwethaf ac maent yn dangos rhagfynegiad cywir o’r lefelau traffig tebygol ar yr union adeg y byddwch yn 
teithio.</p>
<br clear="right" />

<p>&nbsp;</p>

<h3>Mae arnaf angen map o’r trenau tanddaearol...</h3>

<p>&nbsp;</p>

<p>Yn olaf, efallai y bydd arnoch angen map yn dangos rhan o rwydwaith y rheilffordd, y llwybrau bysiau neu fap o drenau 
tanddaearol Llundain. Mae ein tudalen  <a href="/Web2/Maps/NetworkMaps.aspx">mapiau’r rhwydwaith cludiant</a> 
yn eich cysylltu ag ystod o safleoedd yn cynnwys y mapiau hyn.</p>
</div>
</div>
'

GO


----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10002
SET @ScriptDesc = 'Updates to Find a Place homepage information panel'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.6  $'

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

