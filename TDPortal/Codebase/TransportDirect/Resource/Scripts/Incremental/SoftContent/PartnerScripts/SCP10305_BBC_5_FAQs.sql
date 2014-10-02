-- *************************************************************************************
-- NAME 		: SCP10305_BBC_5_FAQs.sql
-- DESCRIPTION  : FAQs (Full Set) - modified for BBC
-- AUTHOR		: XXXX
-- *************************************************************************************

-- This FAQ set is currently identical to DUPxxxx_WhiteLabel_ 5_FAQs_All.sql

USE [Content]
GO


DECLARE @ThemeId INT
SET @ThemeId = 3 -- BBC

-------------------------------------------------------------
-- FAQs - Main page
-------------------------------------------------------------

EXEC AddtblContent
@ThemeId, 27, 'Body Text', '/Channels/TransportDirect/Help/NewHelp', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="Info" name="Info"></a><h2>1. About the Journey Planner&nbsp;</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.1">1.1)&nbsp;How much of Britain does the Journey Planner cover?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.2">1.2) Where does the Journey Planner get its information?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.3">1.3) Is the information on the Journey Planner accurate?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.4">1.4) How often is the information on the Journey Planner updated?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.5">1.5) What can I do if I know of information that is missing or find&nbsp;inaccurate information?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.6">1.6) How do I find out more about the Journey Planner?</a></p>
<div align="right"><a name="WhoOperates"></a><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>2. General journey planning</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.1">2.1) Which journey planner should I use?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.2">2.2) How do I plan a journey using the Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.3">2.3) What if I don''t know the exact address(es) of where I want to travel from or to?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.4">2.4) How do I find a station or an airport?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.5">2.5) How do I add specific details or set travel preferences for my journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.6">2.6) When I add a via point to my journey why isn''t it always shown in my journey results?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.7">2.7) How can I plan a day trip to two destinations?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.8">2.8) How do I '
, 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="Info" name="Info"></a><h2>1. Ynglyn â Journey Planner&nbsp;</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.1">1.1) Faint o Brydain y mae Journey Planner yn ymdrin ag ef?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.2">1.2) O ble y caiff Journey Planner ei wybodaeth?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.3">1.3) A yw&#8217;r wybodaeth ar Journey Planner yn gywir? </a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.4">1.4) Pa mor aml y mae&#8217;r wybodaeth ar Journey Planner yn cael ei diweddaru?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.5">1.5) Beth alla&#8217; i ei wneud os ydw i&#8217;n gwybod am wybodaeth sydd ar goll neu yn dod o hyd i wybodaeth sy&#8217;n anghywir?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.6">1.6) Sut ydw i''n cael rhagor o wybodaeth am Journey Planner?</a></p>
<div align="right"><a name="EnablingIntelligentTravel"></a><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>2. Cynllunio Siwrnai</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.1">2.1) Pa gynlluniwr siwrnai y dylwn ei ddefnyddio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.2">2.2) Sut ydw i&#8217;n cynllunio siwrnai yn defnyddio Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.3">2.3) Beth os nad ydw i&#8217;n gwybod yr union gyfeiriad(au) y lle yr ydw i eisiau teithio ohono neu iddo?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.4">2.4) Sut ydw i''n dod o hyd i orsaf neu faes awyr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.5">2.5) Sut y gallaf ychwanegu manylion penodol neu osod hoffterau teithio ar gyfer fy siwrneion?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.6">2.6) When I add a via point to my journey why isn''t it always shown in my journey results?</a></p>
<p class="rsviewcontentrow"><a '
DECLARE @ptrValText BINARY(16)

-- Update text field: Value-En
SET @ptrValText = NULL
SELECT @ptrValText = TEXTPTR([Value-En]) FROM [dbo].[tblContent] WHERE themeid = @ThemeId and groupid = 27 and controlname ='Body Text' and propertyname = '/Channels/TransportDirect/Help/NewHelp'

IF @ptrValText IS NOT NULL BEGIN

   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'add a connecting journey to my overall journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.9">2.9) If I add a connecting public transport journey to a car/taxi journey, how can I make sure I have enough time to park my car/pay the taxi fare before boarding the train?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.10">2.10) How do I change the leaving or arrival times for the start or the end of my journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.11">2.11) How is the Journey Planner (Powered By) different from other journey planners?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.12">2.12) How can I find out about the accessibility of the types of transport in the journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.13">2.13) Does Transport Direct offer walk planning?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.14">2.14) How can I find the detailed directions for a walk?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.15">2.15) In which areas will I see a link to the walkit.com site?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.16">2.16) Is real time information included in my journey plan?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.17">2.17) I can''t find the place I want to travel from or to?</a></p>
<!-- <p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.18">2.18) How do I plan journeys to destinations outside Great Britain?</a></p> -->
<div align="right"><a name="WhoOperates"></a><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a name="Maps"></a><h2>3. Train</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.1">3.1) How can I find departure times for the stations listed in my journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.2">3.2) How can I find out what stations my train will be stopping at?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.3">3.3) How can I find information about train operators?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.4">3.4) How can I find out what on-board facilities will be available on a train?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.5">3.5) How can I find out if I need to or can make a reservation in advance on a train?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.6">3.6) How can I find out which classes of travel are available on a train?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.7">3.7) How can I find out about station accessibility and facilities on my journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.8">3.8) How can I get a list of all the direct rail services to a destination (including overtaken services), rather than just the fastest ones?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.9">3.9) How do I plan a journey from or to an underground/light rail station?</a></p>
<div align="right"><a name="WhoOperates"></a><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a name="Maps"></a><h2>4. Road journey planning</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.1">4.1) Are all journeys planned by the quickest route?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.2">4.2) How do I avoid particular roads?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.3">4.3) How do I choose particular roads to use?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.4">4.4) How do I avoid all motorways?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.5">4.5) How do I find a car/taxi route that avoids all tolls or ferries?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.6">4.6) How do you calculate car journey times?</a></p>
<div align="right"><a name="WhoOperates"></a><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="Taxis" name="Taxis"></a><h2>5. Taxis</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTaxi.aspx#A5.1">5.1) How can I find taxi information for stations I am travelling to in my journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTaxi.aspx#A5.2">5.2) Can I find out if the taxis are wheelchair accessible?</a></p>
<div align="right"><a name="WhoOperates"></a><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="Parking" name="Parking"></a><h2>6. Parking</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.1">6.1) How do I find a car park?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.2">6.2) How can I find out about Park and Ride schemes?</a> </p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.3">6.3) How can I plan a Park and Ride journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.4">6.4) Where does car park data come from?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.5">6.5) What is the Park Mark scheme?</a></p>
<div align="right"><a name="WhoOperates"></a><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a name="Maps"></a><h2>7. Air</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAir.aspx#A7.1">7.1) How do you calculate Journey times for flights?</a></p>
<div align="right"><a name="WhoOperates"></a><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="Bus" name="Bus"></a><h2>8. Bus</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.1">8.1)&nbsp; How can I find out about buses that need to be booked in advance?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.2">8.2)&nbsp; Can I plan a bus journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.3">8.3)&nbsp; How accurate are bus times?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.4">8.4)&nbsp; What is the English Concessionary Travel Scheme?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.5">8.5)&nbsp; Which bus services on Transport Direct are included in the English Concessionary Travel Scheme?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.6">8.6)&nbsp; What is the Scottish Concessionary Travel Scheme?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.7">8.7)&nbsp; What is the Welsh Concessionary Travel Scheme?</a></p>
<div align="right"><a name="WhoOperates"></a><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="LiveTravel" name="LiveTravel"></a><h2>9. Live travel</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.1">9.1) Can I get up-to-date travel news in my region?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.2">9.2) How often is "Live travel" information updated?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.3">9.3) How can I see a live departure board?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.4">9.4) How can I see if there is any travel news impacting my road journey?</a></p>
<div align="right"><a name="WhoOperates"></a><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a name="Maps"></a><h2>10. Maps</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.1">10.1) How do I find a place on a map?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.2">10.2) How can I see a map of incidents in my area?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.3">10.3) How do I find a map that shows traffic levels?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.4">10.4) How do I find a public transport network map?</a></p>
<div align="right"><a name="WhoOperates"></a><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="TicketsAndCosts" name="TicketsAndCosts"></a><h2>11. Tickets and costs</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.1">11.1)&nbsp;Once I&#8217;ve found a journey, can I find out what the fare is?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.2">11.2)&nbsp;Can I find information about bus fares?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.3">11.3)&nbsp;How can I find out how much the fuel will cost for a car journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.4">11.4)&nbsp;How can I find out the full costs of a car journey taking into account how much I have to pay to own and run a car?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.5">11.5)&nbsp;What types of rail fares can I see on the Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.6">11.6)&nbsp;What rail fares are shown in advance of a fares change?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.7">11.7)&nbsp;What is meant by &#8216;Route&#8217; in the list of rail fares shown for a journey?</a></p>
<div align="right"><a name="WhoOperates"></a><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="CalcCarbon" name="CalcCarbon"></a><h2>12. CO2 Information</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.1">12.1)&nbsp;How do you estimate my car''s CO2 emissions?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.2">12.2)&nbsp;What assumptions do you make about cars'' CO2 emissions?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.3">12.3)&nbsp;What assumptions do you make in estimating the CO2 emissions produced for public transport and air journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.4">12.4)&nbsp;What assumptions do you make in comparing CO2 emissions for car journeys with public transport journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.5">12.5)&nbsp;What assumptions do you make about bus and coach journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.6">12.6)&nbsp;Why do you use energy efficiency ratings?</a></p>
<div align="right"><a name="WhoOperates"></a><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="Printing" name="Printing"></a><h2>13. Using the website</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.1">13.1)&nbsp;What browsers does the website work best on?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.2">13.2) Can I print information from the website?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.3">13.3) How can I save my travel preferences and my favourite journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.4">13.4) Can I bookmark certain journeys (save as favourites)?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.5">13.5) Why should I register on the Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.6">13.6) How can I email information to other people?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.7">13.7) What is the purpose of the Journey Planner Toolbar?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.8">13.8) How can I add a Journey Planner link to my website?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.9">13.9) Do I need to have JavaScript enabled to use the Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.10">13.10) Is the Journey Planner an accessible website?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.11">13.11) Why is my company browser blocking some Journey Planner functionality?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.12">13.12) What is social bookmarking?</a></p>
<div align="right"><a name="WhoOperates"></a><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="TDOnPDAOrMobile" name="TDOnPDAOrMobile"></a><h2>14. Using the Journey Planner services on your PDA/Mobile</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMobile.aspx#A14.1">14.1)&nbsp;How can I use the latest PDAs or mobile phones to find out the departure and arrival times for railway stations throughout Britain and for some bus or coach stops in areas where SMS codes are available for the bus or coach stop?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMobile.aspx#A14.2">14.2)&nbsp;How can I use the latest PDAs or mobile phones to view Live travel news for public transport or car (e.g. check for delays and incidents) either for a region or for the whole of Britain?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMobile.aspx#A14.3">14.3)&nbsp;How can I use the latest PDAs or mobile phones to find out whether there is a taxi rank or cab (private hire car) firm at the railway station I am travelling to, and get phone numbers for taxi and cab firms serving the station?</a></p>
<div align="right"><a name="WhoOperates"></a><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="CommentFeedback" name="CommentFeedback"></a><h2>15. Comment and feedback</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpComm.aspx#A15.1">15.1)&nbsp;What can I do if I have a problem with this website or the information it has provided me with?</a></p>
<div align="right"><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>

<div class="hdtypethree"><a id="CyclePlanning" name="CyclePlanning"></a><h2>16. Cycle Planning</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.1">16.1)&nbsp;Does Transport Direct offer cycling options?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.2">16.2)&nbsp;Which areas can I plan a cycle journey in?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.3">16.3)&nbsp;Are all journeys planned by the quickest route?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.4">16.4)&nbsp;I will be travelling when it’s dark, how do I try and ensure my journey goes through reasonably well lit areas?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.5">16.5)&nbsp;How do I plan a journey where I do not have to get off and on my bike?</a></p>
<!-- <p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.6">16.6)&nbsp;I’m planning a journey with my young children, how do I avoid cycle journeys with steep climbs?</a></p> -->
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.7">16.7)&nbsp;What are time-based restrictions and what impact could they have on my journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.8">16.8)&nbsp;How do you calculate cycle journey times?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.9">16.9)&nbsp;Why am I being asked to download Active X controls?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.10">16.10)&nbsp;What should I do if I am prompted to download the control?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.11">16.11)&nbsp;What should I do if I cannot see the graph and have not been prompted to download the ActiveX control?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.12">16.12)&nbsp;What is a GPX file?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.13">16.13)&nbsp;How does Transport Direct Calculate Calories used?</a></p>
<div align="right"><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>

<div id="hdtypethree"><a id="TouristInformation" name="TouristInformation"></a><h2>17. Transport Direct Tourist Information</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTouristInfo.aspx#A17.1">17.1)&nbsp;How can I find travel information when visiting from abroad?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTouristInfo.aspx#A17.2">17.2)&nbsp;How can I find tourist information when visiting from abroad?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTouristInfo.aspx#A17.3">17.3)&nbsp;Can I book tickets on Transport Direct from abroad?</a></p>
<div align="right"><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>

<div class="hdtypethree"><a id="AccessiblePlanning" name="AccessiblePlanning"></a><h2>18. Accessible Journey Planning</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.1">18.1) What information do you provide about accessible services, and which areas are covered?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.2">18.2) How accurate is your information on accessible journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.3">18.3) What do you mean by ''assistance''?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.4">18.4) Why do you include journey legs that require assistance in your ''step-free only'' results?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.5">18.5) Why do I need to book in advance?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.6">18.6) How can I book in advance?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.7">18.7) How does the ''Find Nearest Accessible Stop'' work?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.8">18.8) How can I get to the nearest accessible station or stop?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.9">18.9) Can I choose an accessible journey that only uses particular types of transport?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.10">18.10) Can I obtain more information about the accessibility of a station?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.11">18.11) Can you provide fares information?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.12">18.12) Why didn''t I get any results for my accessible journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.13">18.13) I don''t believe a service, station or stop included in your journey results is accessible.  Can you correct this?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.14">18.14) Can I use my mobility scooter on public transport?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.15">18.15) Why can''t I plan to an airport, even though I know it is accessible?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.16">18.16) How do I find out about live incidents on the transport network once my journey has started?</a></p>
<div align="right"><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>

</div></div>'

END


-- Update text field: Value-Cy
SET @ptrValText = NULL
SELECT @ptrValText = TEXTPTR([Value-Cy]) FROM [dbo].[tblContent] WHERE themeid = @ThemeId and groupid = 27 and controlname ='Body Text' and propertyname = '/Channels/TransportDirect/Help/NewHelp'

IF @ptrValText IS NOT NULL BEGIN

   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 'href="/Web2/Help/HelpJplan.aspx#A2.7">2.7) Sut y gallaf gynllunio taith dydd i ddau gyrchfan?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.8">2.8) Sut y gallaf ychwanegu siwrnai gysylltiol at fy siwrnai gyffredinol?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.9">2.9) Pe byddwn yn ychwanegu siwrnai trafnidiaeth gyhoeddus gysylltiol at siwrnai gar/tacsi, sut y gallaf sicrhau bod gennyf ddigon o amser i barcio fy nghar/talu am y tacsi cyn mynd ar y tr&#234;n?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.10">2.10) Sut ydw i''n newid amserau gadael neu gyrraedd ar gyfer dechrau neu ddiwedd fy siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.11">2.11) Sut mae Journey Planner yn wahanol i gynllunwyr siwrneion eraill?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.12">2.12) Sut y gallaf ddarganfod am hygyrchedd y mathau o gludiant yn y siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.13">2.13) A yw Transport Direct yn cynnig cynlluniau cerdded?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.14">2.14) Sut allaf i ddarganfod y cyfarwyddiadau manwl ar gyfer taith gerdded?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.15">2.15) Ym mha ardaloedd allaf i ddarganfod manylion ar gyfer taith gerdded?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.16">2.16) A yw gwybodaeth amser real yn cael ei chynnwys yng nghynllun fy siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.17">2.17) Ni allaf ddarganfod y lle yr wyf eisiau teithio ohono/iddo?</a></p>
<!-- <p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.18">2.18) How do I plan journeys to destinations outside Great Britain?</a></p> -->
<div align="right"><a name="EnablingIntelligentTravel"></a><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="Train" name="Train"></a><h2>3. Trên</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.1">3.1) Sut y gallaf ddarganfod amserau ymadael ar gyfer y gorsafoedd a restrwyd yn fy siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.2">3.2) Sut y gallaf ddarganfod pa orsafoedd y bydd fy nhr&#234;n i yn aros ynddynt?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.3">3.3) Sut y gallaf ddarganfod gwybodaeth am weithredwyr trenau?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.4">3.4) Sut y gallaf ddarganfod pa gyfleusterau fydd ar gael ar dr&#234;n?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.5">3.5) Sut y gallaf ddarganfod a oes arnaf angen neu os gallaf archebu lle ymlaen llaw ar dr&#234;n?</a></p>
<p class="rsviewcontentrow"><a href="/WhiteLabel/Help/HelpTrain.aspx#A3.6">3.6) Sut y gallaf ddarganfod pa ddosbarthiadau o deithio sydd ar gael ar dr&#234;n?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.7">3.7) Sut y gallaf ddarganfod am hygyrchedd gorsaf a&#8217;r cyfleusterau sydd ar gael ar fy siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.8">3.8) Sut y gallaf gael rhestr o''r holl wasanaethau rheilffordd uniongyrchol i gyrchfan, yn hytrach na dim ond y rhai cyflymaf?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.9">3.9) Sut ydw i''n cynllunio siwrnai i orsaf dan ddaear/gorsaf drenau ysgafn neu oddi yno?</a></p>
<div align="right"><a name="EnablingIntelligentTravel"></a><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="Road" name="Road"></a><h2>4. Cynllunio siwrnai ffordd</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.1">4.1) A yw''r holl siwrneiau yn cael eu cynllunio drwy ddefnyddio''r llwybr teithio cyflymaf?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.2">4.2) Sut y gallaf osgoi ffyrdd penodol?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.3">4.3) Sut y dewisaf ffyrdd penodol i&#8217;w defnyddio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.4">4.4) Sut ydw i''n osgoi''r traffyrdd i gyd?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.5">4.5) Sut ydw i''n dod o hyd i lwybr car/tacsi sy''n osgoi pob toll neu fferi?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.6">4.6) Sut ydych chi''n cyfrif amserau siwrneion car?</a></p>
<div align="right"><a name="EnablingIntelligentTravel"></a><a class="jpt" href="/TransportDirect/cy/Help/NewHelp.aspx#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="Taxi" name="Taxi"></a><h2>5. Tacsis</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTaxi.aspx#A5.1">5.1) Sut y gallaf ddarganfod gwybodaeth am dacsis ar gyfer gorsafoedd rwyf yn teithio iddynt yn ystod fy siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTaxi.aspx#A5.2">5.2) Fedra" i ddarganfod a yw''r tacsis yn hygyrch i gadeiriau olwyn?</a></p>
<div align="right"><a name="EnablingIntelligentTravel"></a><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="Taxi" name="Taxi"></a><h2>6. Parcio</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.1">6.1) Sut y gallaf ddod o hyd i faes parcio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.2">6.2) Sut y gallaf ddarganfod gwybodaeth am gynlluniau Parcio a Theithio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.3">6.3) Sut y gallaf gynllunio siwrnai Parcio a Theithio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.4">6.4) O ble y daw data am feysydd parcio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.5">6.5) Beth yw''r cynllun Park Mark?</a></p>
<div align="right"><a name="EnablingIntelligentTravel"></a><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="Air" name="Air"></a><h2>7. Awyr</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAir.aspx#A7.1">7.1) Sut ydych chi''n cyfrif amserau Siwrnai ar gyfer ehediadau?</a></p>
<div align="right"><a name="EnablingIntelligentTravel"></a><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="Bus" name="Bus"></a><h2>8. Bws</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.1">8.1) Sut y gallaf ddarganfod am fysiau y mae angen archebu lle ymlaen llaw arnynt?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.2">8.2)&nbsp; Allaf i gynllunio siwrnai bws?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.3">8.3)&nbsp; Pa mor gywir yw''r amserau bws?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.4">8.4)&nbsp; Beth yw Cynllun Teithio Rhatach Lloegr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.5">8.5)&nbsp; Pa wasanaethau bysiau Transport Direct sy''n gynwysedig yng Nghynllun Teithio Rhatach Lloegr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.6">8.6)&nbsp; Beth yw Cynllun Teithio Rhatach yr Alban?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.7">8.7)&nbsp; Beth yw Cynllun Teithio Rhatach Cymru?</a></p>
<div align="right"><a name="EnablingIntelligentTravel"></a><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="LiveTravel" name="LiveTravel"></a><h2>9. Teithio byw</h2> </div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.1">9.1) Fedra&#8217; i gael y newyddion diweddaraf am deithio yn fy rhanbarth i?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.2">9.2) Pa mor aml y mae gwybodaeth "Teithio Byw" yn cael ei diweddaru?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.3">9.3) Sut allaf i weld bwrdd ymadael byw?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.4">9.4) Sut allaf fi weld a oes unrhyw newddion teithio yn effeithio ar fy nhaith ar y ffordd?</a></p>
<div align="right"><a name="EnablingIntelligentTravel"></a><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a name="Maps"></a><h2>10. Mapiau</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.1">10.1) Sut ydw i&#8217;n dod o hyd i le ar y map? </a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.2">10.2) Sut y gallaf weld map o ddigwyddiadau yn fy ardal?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.3">10.3) Sut ydw i&#8217;n dod o hyd i fap sy&#8217;n dangos lefelau traffig?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.4">10.4) Sut ydw i''n dod o hyd i fap rhwydwaith trafnidiaeth gyhoeddus?</a></p>
<div align="right"><a name="EnablingIntelligentTravel"></a><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="TicketsAndCosts" name="TicketsAndCosts"></a><h2>11. Tocynnau a chostau</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.1">11.1) Wedi i mi ddarganfod siwrnai, fedra&#8217; i ddarganfod beth yw pris y tocyn?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.2">11.2) Fedra" i gael gwybodaeth am brisiau tocynnau bws?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.3">11.3) Sut y gallaf ddarganfod faint y bydd y tanwydd yn ei gostio am siwrnai mewn car?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.4">11.4) Sut y gallaf ddarganfod costau llawn siwrnai car gan roi ystyriaeth i faint mae&#8217;n rhaid i mi ei dalu i fod yn berchen ar a chynnal car?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.5">11.5)&nbsp;Pa fathau o docynnau rheilffordd y gallaf eu gweld ar Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.6">11.6)&nbsp;Pa brisiau tocynnau rheilffordd a ddangosir cyn unrhyw newid mewn prisiau tocynnau?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.7">11.7)&nbsp;Beth yw ystyr &#8216;Llwybr&#8217; yn y rhestr o brisiau tr&#234;n a ddangosir ar gyfer siwrnai?</a></p>
<div align="right"><a name="EnablingIntelligentTravel"></a><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="CalcCarbon" name="CalcCarbon"></a><h2>12. Gwybodaeth CO2</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.1">12.1)&nbsp;Sut ydych chi''n amcangyfrif allyriadau CO2 fy nghar?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.2">12.2)&nbsp;Beth ydych chi''n ei dybio am allyriadau CO2 ceir?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.3">12.3)&nbsp;Beth ydych chi''n ei dybio wrth amcangyfrif yr allyriadau CO2 a gynhyrchir ar gyfer siwrneiau trafnidiaeth gyhoeddus ac awyr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.4">12.4)&nbsp;Beth ydych chi''n ei dybio wrth gymharu allyriadau CO2 ar gyfer siwrneiau car &#226; siwrneiau trafnidiaeth gyhoeddus?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.5">12.5)&nbsp;Beth ydych chi''n ei dybio am siwrneiau bws a choets?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.6">12.6)&nbsp;Pam ydych chi''n defnyddio cyfraddau effeithlonrwydd ynni?</a></p>
<div align="right"><a name="EnablingIntelligentTravel"></a><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="Printing" name="Printing"></a><h2>13. Defnyddio''r wefan</h2> </div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.1">13.1) Pa borwyr y mae''r wefan yn gweithio orau arnynt?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.2">13.2) Alla&#8217; i argraffu gwybodaeth o&#8217;r wefan?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.3">13.3) Sut y gallaf gadw fy hoffterau teithio a&#8217;m hoff siwrneion?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.4">13.4) Alla&#8217; i roi roi nod llyfr ar rai siwrneion (eu cadw fel ffefrynnau)?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.5">13.5) Pam y dylwn gofrestru ar Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.6">13.6) Sut y gallaf ebostio gwybodaeth i bobl eraill?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.7">13.7) A yw Journey Planner yn wefan hygyrch?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.8">13.8) Sut y gallaf ychwanegu cyswllt Journey Planner at fy ngwefan?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.9">13.9) A oes angen i mi fod &#226; JavaScript wedi ei alluogi i ddefnyddio Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.10">13.10) A yw Journey Planner yn wefan hygyrch?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.11">13.11) Pam fod porwr fy nghwmni yn rhwystro rhai swyddogaethau Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.12">13.12) Beth yw llyfrnodi cymdeithasol?</a></p>
<div align="right"><a name="EnablingIntelligentTravel"></a><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="TDOnPDAOrMobile" name="TDOnPDAOrMobile"></a><h2>14. Defnyddio gwasanaethau teithio byw Journey Planner ar PDA/ff&#244;n symudol</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMobile.aspx#A14.1">14.1)&nbsp;Sut y gallaf ddefnyddio&#8217;r PDAs neu&#8217;r ffonau symudol diweddaraf i ddarganfod amserau ymadael a chyrraedd ar gyfer gorsafoedd rheilffordd ledled Prydain ac ar gyfer rhai arosfannau bysiau neu fysiau moethus mewn ardaloedd lle mae codau SMS ar gael ar gyfer yr arhosfan bysiau neu fysiau moethus?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMobile.aspx#A14.2">14.2)&nbsp;Sut y gallaf ddefnyddio PDAs neu&#8217;r ffonau symudol ddiweddaraf i edrych ar Newyddion Teithio Byw ar gyfer cludiant cyhoeddus neu gar (e.e. edrychwch a oes unrhyw oedi ac unrhyw ddigwyddiadau) naill ai ar gyfer rhanbarth neu ar gyfer Prydain gyfan?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMobile.aspx#A14.3">14.3)&nbsp;Sut y gallaf ddefnyddio&#8217;r PDAs neu&#8217;r ffonau symudol diweddaraf i ddarganfod a oes ranc tacsi neu gwmni cabiau (ceir hurio preifat) yn yr orsaf rheilffordd yr wyf yn teithio iddi, ac i gael rhifau ff&#244;n ar gyfer cwmn&#239;au tacsi neu gabiau sy&#8217;n gwasanaethu&#8217;r orsaf?</a></p>
<div align="right"><a name="EnablingIntelligentTravel"></a><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="CommentFeedback" name="CommentFeedback"></a><h2>15. Sylwadau ac adborth</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpComm.aspx#A15.1">15.1) Beth y gallaf ei wneud os oes gennyf broblem gyda&#8217;r wefan hon neu&#8217;r wybodaeth y mae wedi ei rhoi i mi?</a></p>
<div align="right"><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>

<div class="hdtypethree"><a id="CyclePlanning" name="CyclePlanning"></a><h2>16. Trefnu Teithiau Beicio</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.1">16.1)&nbsp;A yw Transport Direct yn cynnig dewisiadau beicio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.2">16.2)&nbsp;Ym mha ardaloedd y gallaf gynllunio taith feicio? </a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.3">16.3)&nbsp;A yw pob siwrnai''n cael ei chynllunio yn &#244;l y llwybr cyflymaf?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.4">16.4)&nbsp;Byddaf yn teithio yn y tywyllwch, sut ydw i''n ceisio sicrhau bod fy siwrnai''n mynd drwy ardaloedd wedi''u goleuo''n rhesymol o dda?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.5">16.5)&nbsp;Sut ydw i''n trefnu siwrnai heb fod yn rhaid imi ddod oddi ar fy meic?</a></p>
<!-- <p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.6">16.6)&nbsp;Rwyf yn cynllunio siwrnai gyda fy mhlant ifanc, sut ydw i''n osgoi siwrneiau beicio sy''n dringo''n serth?</a></p> -->
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.7">16.7)&nbsp;Beth yw''r cyfyngiadau amser a pha effaith y gallent ei chael ar fy siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.8">16.8)&nbsp;Sut ydych chi''n cyfrifo amseroedd siwrnai beicio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.9">16.9)&nbsp;Pam mae gofyn imi lwytho rheolaethau Active X i lawr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.10">16.10)&nbsp;Beth ddylwn ei wneud os gofynnir imi lwytho''r rheolaeth i lawr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.11">16.11)&nbsp;Beth ddylwn ei wneud os nad wyf yn gallu gweld y graff ac os na ofynnwyd imi lwytho''r rheolaeth ActiveX i lawr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.12">16.12)&nbsp;Beth yw ffeil GPX?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A16.13">16.13)&nbsp;Sut mae Transport Direct yn Cyfrifo Calorïau a ddefnyddir?</a></p>
<div align="right"><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>

<div id="hdtypethree"><a id="TouristInformation" name="TouristInformation"></a><h2>17. Twrist Transport Direct </h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTouristInfo.aspx#A17.1">17.1)&nbsp;Sut allaf i ddarganfod gwybodaeth am deithio pan fyddaf yn ymweld â''r wlad o dramor?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTouristInfo.aspx#A17.2">17.2)&nbsp;Sut allaf i ddarganfod gwybodaeth i dwristiaid pan fyddaf yn ymweld â''r wlad o dramor?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTouristInfo.aspx#A17.3">17.3)&nbsp;Allaf i archebu tocynnau ar Transport Direct o dramor?</a></p>
<DIV align="right"><A name="TouristInformation"></A><A id="jpt" href="/Web2/Help/NewHelp.aspx#logoTop">Top of page <IMG alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0"></A></DIV>

<div class="hdtypethree"><a id="AccessiblePlanning" name="AccessiblePlanning"></a><h2>18. Accessible Journey Planning</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.1">18.1) What information do you provide about accessible services, and which areas are covered?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.2">18.2) How accurate is your information on accessible journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.3">18.3) What do you mean by ''assistance''?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.4">18.4) Why do you include journey legs that require assistance in your ''step-free only'' results?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.5">18.5) Why do I need to book in advance?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.6">18.6) How can I book in advance?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.7">18.7) How does the ''Find Nearest Accessible Stop'' work?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.8">18.8) How can I get to the nearest accessible station or stop?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.9">18.9) Can I choose an accessible journey that only uses particular types of transport?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.10">18.10) Can I obtain more information about the accessibility of a station?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.11">18.11) Can you provide fares information?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.12">18.12) Why didn''t I get any results for my accessible journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.13">18.13) I don''t believe a service, station or stop included in your journey results is accessible.  Can you correct this?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.14">18.14) Can I use my mobility scooter on public transport?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.15">18.15) Why can''t I plan to an airport, even though I know it is accessible?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx#A18.16">18.16) How do I find out about live incidents on the transport network once my journey has started?</a></p>
<div align="right"><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>

</div></div>'

END



-------------------------------------------------------------
-- FAQs - Air
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpAir', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>7. Air</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAir.aspx#A7.1">7.1) How do you calculate journey times for flights?</a></p>
<p>&nbsp;</p>
<div class="hdtypethree">
<h2>7. Air</h2></div>
<br />
<h3><a class="QuestionLink" id="A7.1" name="A7.1"></a>7.1) How do you calculate journey times for flights?</h3>
<p>When calculating journeys by plane the Journey Planner includes in the journey plan the minimum time you need to arrive at the airport to include check-in time. &#8216;Leave&#8217; is the latest time that you may reach the &#8216;check-in-desk&#8217; and arrive is the time we estimate you will be able to exit the airport after you land and collect your luggage. However, check-in times vary between airlines and you must check with the individual airline before travelling. You may be able to check-in or leave the airport in more or less time than the times given by the Journey Planner.</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>
<div></div>
<div></div>
<div></div>', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>7. Awyr</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpAir.aspx#A7.1">7.1) Sut ydych chi''n cyfrif amserau Siwrnai ar gyfer ehediadau?</a></p>
<p>&nbsp;</p>
<div class="hdtypethree">
<h2>7. Awyr</h2></div>
<br />
<h3><a class="QuestionLink" id="A7.1" name="A7.1"></a>7.1) Sut ydych chi''n cyfrif amserau Siwrnai ar gyfer ehediadau?</h3>
<p>Wrth gyfrifo siwrneion yn yr awyr bydd Journey Planner yn cynnwys yn y cynllun siwrnai yr amser gofynnol sy''n rhaid i chi gyrraedd y maes awyr er mwyn cofrestru. &#8216;Gadael&#8217; yw&#8217;r amser diweddaraf y gallwch gyrraedd y &#8216;Ddesg gofrestru&#8217; a &#8216;Cyrraedd&#8217; yw&#8217;r amser a amcangyfrifwn y gallwch fynd allan o&#8217;r maes awyr wedi i chi lanio a chasglu eich bagiau. Ond mae amserau cofrestru yn amrywio rhwng cwmn&#239;au awyrennau a dylech ofyn i&#8217;r cwmni awyr unigol cyn teithio. Mae&#8217;n bosibl y byddwch hefyd yn gallu cofrestru, neu adael maes awyr, mewn mwy neu lai o amser na&#8217;r amserau a roddwyd gan Journey Planner.</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>
<div></div>
<div></div>
<div></div>'



-------------------------------------------------------------
-- FAQs - Bus
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpBus', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>8. Bus</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.1">8.1)&nbsp; How can I find out about buses that need to be booked in advance?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.2">8.2)&nbsp; Can I plan a bus journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.3">8.3)&nbsp; How accurate are bus times?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.4">8.4)&nbsp; What is the English Concessionary Travel Scheme?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.5">8.5)&nbsp; Which bus services on Transport Direct are included in the English Concessionary Travel Scheme?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.6">8.6)&nbsp; What is the Scottish Concessionary Travel Scheme?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.7">8.7)&nbsp; What is the Welsh Concessionary Travel Scheme?</a></p>
<p>&nbsp;</p>
<div class="hdtypethree">
<h2>8. Bus</h2></div>
<br />
<h3><a class="QuestionLink" id="A8.1" name="A8.1"></a>8.1) How can I find out about buses that need to be booked in advance?</h3>
<p>Some bus journeys require advance booking.&nbsp; These are usually more flexible than normal bus services, for example they may pick up or set down at a place you choose.&nbsp; </p>
<p>&nbsp;</p>
<p>If a bus that needs to be booked in advance is listed in your journey plan transport options, the telephone number for booking the bus will be shown on the &#8216;Details&#8217; page (or in the &#8216;Instructions&#8217; column in the table version of the page).</p>
<br />
<h3><a class="QuestionLink" id="A8.2" name="A8.2"></a>8.2) Can I plan a bus journey?</h3>
<ul>
<li>From the homepage click on &#8216;Find a bus&#8217; on the left hand navigation or </li>
<li>Click on the &#8216;Plan a journey&#8217; tab then click &#8216;Find a bus&#8217; or </li>
<li>Select &#8216;Door-to-door&#8217; then advanced options and deselect all modes of transport other than bus </li></ul>
<h3><a class="QuestionLink" id="A8.3" name="A8.3"></a>8.3) How accurate are bus times?</h3>
<p>The Journey Planner aims to give you times for every bus stop on every route. Currently, we can find you times for most bus stops on most routes and our coverage is continually improving. Please note that when we do give you a time at a bus stop it may be an estimate based on the times given for the major stops along the route or estimated from the frequency of the bus service. Therefore it is important that you allow some flexibility in your schedule when travelling by bus.</p>
<h3><a class="QuestionLink" id="A8.4" name="A8.4"></a>8.4) What is the English Concessionary Travel Scheme?</h3>
<p>
	Since 1 April 2008 people living in England who are of eligible age, or eligible disabled people, 
	have been able to use their England-wide passes, which entitle them to free off-peak local bus 
	travel <b>anywhere</b> in England. Those born before 6 April 1950 are eligible from their 
	60th birthday. The age of eligibility for those turning 60 after 6 April 2010 is:</p>
	<p>&nbsp;</p>
		<ul class="listerdisc">
			<li>For women - pensionable age</li>
			<li>For men - pensionable age of a woman born on the same day</li>
		</ul>
	<p>&nbsp;</p>
	<p>The statutory minimum time period to which this applies is Monday to Friday, 
	from 9.30am to 11pm and any time at weekends and bank holidays. Local authorities 
	may offer extra benefits to their residents as part of their concessionary 
	scheme – for example, free or reduced off-peak tram or rail travel, or free bus travel 
	before 9.30am Monday to Friday; details should be checked with the local authority.
</p>
<p>&nbsp;</p>
<p>Further information can be found at:
<a target="_child" href="http://www.direct.gov.uk/en/TravelAndTransport/PublicTransport/BusAndCoachTravel/DG_071640" title="Click to view information in a new browser window">
http://www.direct.gov.uk/en/TravelAndTransport/PublicTransport/BusAndCoachTravel/DG_071640 <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</p>
<h3><a class="QuestionLink" id="A8.5" name="A8.5"></a>8.5) Which bus services on Transport Direct are included in the English Concessionary Travel Scheme?</h3>
<p>The scheme applies to off-peak journeys on almost all services in England described on this site as "bus". We hope to indicate where this is not the case as more information about exclusions becomes available. For journeys shown as "coach", passengers are advised to check details costs and associated issues in advance with the operator.  For example if the service requires pre-booking, a booking fee may still be charged. Most long-distance coach services are not included in the scheme, although coach operators such as National Express already have special fares for the over-60''s so please check with the operator.</p>
<h3><a class="QuestionLink" id="A8.6" name="A8.6"></a>8.6) What is the Scottish Concessionary Travel Scheme?</h3>
<p>Scotland-Wide Free Bus Travel started on 1st April 2006 and allows anyone aged over 60 and eligible disabled people, to travel free on both local registered services and long distance bus services within Scotland.  There are no peak-time restrictions.</p>
<p>&nbsp;</p>
<p>To take advantage of this free bus travel you need to have a National Entitlement Card. Only a small number of services will not recognise the card, for example premium fare buses and City Sightseeing buses.</p>
<p>&nbsp;</p>
<p>Further information can be found at: <a target="_child" href="http://www.transportscotland.gov.uk/concessionarytravel">http://www.transportscotland.gov.uk/concessionarytravel <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>
<h3><a class="QuestionLink" id="A8.7" name="A8.7"></a>8.7) What is the Welsh Concessionary Travel Scheme?</h3>
<p>The Welsh Assembly Government provides financial support to enable local authorities in Wales to provide free travel on registered local bus services for residents of Wales aged over 60 years and disabled of any age. It also provides free travel on local buses by companions to disabled persons.</p>
<p>&nbsp;</p>
<p>The scheme operates across Wales and concessionary pass holders can travel free at any time of day. </p>
<p>&nbsp;</p>
<p>Further information can be found at: <a target="_child" href="http://new.wales.gov.uk/topics/transport/integrated/concessionary/fares/?lang=en">
http://new.wales.gov.uk/topics/transport/integrated/concessionary/fares/?lang=en <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>
<div></div>
<div></div>
<div></div>', 

'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>8. Bws</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.1">8.1)&nbsp; Sut y gallaf ddarganfod am fysiau y mae angen archebu lle ymlaen llaw arnynt?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.2">8.2)&nbsp; A allaf i gynllunio siwrnai fws?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.3">8.3)&nbsp; Pa mor gywir yw''r amserau bws?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.4">8.4)&nbsp; Beth yw Cynllun Teithio Rhatach Lloegr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.5">8.5)&nbsp; Pa wasanaethau bysiau Transport Direct sy''n gynwysedig yng Nghynllun Teithio Rhatach Lloegr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.6">8.6)&nbsp; Beth yw Cynllun Teithio Rhatach yr Alban?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpBus.aspx#A8.7">8.7)&nbsp; Beth yw Cynllun Teithio Rhatach Cymru?</a></p>
<p>&nbsp;</p>
<div class="hdtypethree">
<h2>8. Bws</h2></div>
<br />
<h3><a class="QuestionLink" id="A8.1" name="A8.1"></a>8.1) Sut y gallaf ddarganfod am fysiau y mae angen archebu lle ymlaen llaw arnynt?</h3>
<p>Mae rhai siwrneion ar fysiau yn gofyn am archebu lle ymlaen llaw.&nbsp; Mae&#8217;r rhain fel arfer yn fwy hyblyg na gwasanaethau bysiau arferol, er enghraifft gallant godi neu ollwng pobl mewn lle o''ch dewis.&nbsp; </p>
<p>&nbsp;</p>
<p>Os rhestrir bws sydd angen archebu lle arno ymlaen llaw yn eich dewisiadau cludiant ar gyfer cynllun eich siwrnai, dangosir y rhif ff&#244;n ar gyfer archebu lle ar y bws ar y dudalen &#8216;Manylion&#8217; (neu yn y golofn &#8216;Cyfarwyddiadau&#8217; yn fersiwn tabl y dudalen).</p>
<h3><a class="QuestionLink" id="A8.2" name="A8.2"></a>8.2) A allaf i gynllunio siwrnai fws?</h3>
<ul>
<li>O''r hafan cliciwch ar "Canfyddwch fws" ar ochr chwith y sgrin neu </li>
<li>Cliciwch ar y tab "Cynlluniwch siwrnai" yna clicio ar "Canfyddwch fws" neu </li>
<li>Dewiswch "Ddrws-i-ddrws" yna dewisiadau mwy cymhleth a dad-ddewiswch yr holl ddulliau teithio heblaw am fws</li></ul>
<h3><a class="QuestionLink" id="A8.3" name="A8.3"></a>8.3) Pa mor gywir yw''r amserau bws?</h3>
<p>Mae Journey Planner yn ceisio rhoi amserau ar gyfer pob arhosfan fysiau ar bob llwybr teithio i chi. Ar hyn o bryd, gallwch ffeindio amserau ar gyfer y mwyafrif o&#8217;r arosfannau ar y mwyafrif o&#8217;r llwybrau ac mae hyn yn gwella yn barhaus. Cofiwch, pan fyddwn yn rhoi amser wrth arhosfan i chi gallai fod yn amcangyfrif yn seiliedig ar y prif arosfannau ar hyd y llwybr teithio neu&#8217;n seiliedig ar amlder y gwasanaeth bysiau. Felly mae&#8217;n bwysig eich bod yn caniat&#225;u rhywfaint o hyblygrwydd yn eich rhaglen pan fyddwch yn teithio ar y bws.</p>
<h3><a class="QuestionLink" id="A8.4" name="A8.4"></a>8.4) Beth yw Cynllun Teithio Rhatach Lloegr?</h3>
<p>O 1af Ebrill 2008 ymlaen, bydd pobl dros 60 oed, a phobl anabl cymwys, sy''n byw yn Lloegr yn gallu defnyddio''u cardiau Lloegr-gyfan newydd sy''n rhoi''r hawl iddynt deithio''n ddi-dâl ar fysiau lleol ar adegau tawel yn unrhyw le yn Lloegr. Y cyfnod amser minimwm statudol y mae hyn yn berthnasol iddo yw dydd Llun i ddydd Gwener, o 9.30am i 11pm ac unrhyw bryd ar benwythnosau a gwyliau banc. Gall awdurdodau lleol gynnig buddion ychwanegol i''w trigolion fel rhan o''u cynllun teithio rhatach – er enghraifft, teithiau di-dâl neu brisiau gostyngol ar dramiau neu drenau ar adegau tawel, neu deithiau di-dâl ar fysiau cyn 9.30 am o Ddydd Llun i Ddydd Gwener; dylid gwirio''r manylion drwy gysylltu â''r awdurdod lleol.</p>
<p>&nbsp;</p>
<p>Gellir cael mwy o wybodaeth ar:
<a target="_child" href="http://www.direct.gov.uk/en/TravelAndTransport/PublicTransport/BusAndCoachTravel/DG_071640">
http://www.direct.gov.uk/en/TravelAndTransport/PublicTransport/BusAndCoachTravel/DG_071640 <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a>
</p>
<h3><a class="QuestionLink" id="A8.5" name="A8.5"></a>8.5) Pa wasanaethau bysiau Transport Direct sy''n gynwysedig yng Nghynllun Teithio Rhatach Lloegr?</h3>
<p>Mae''r cynllun yn gymwys i deithiau ar adegau tawel ar bron bob gwasanaeth yn Lloegr a ddisgrifir ar y wefan hon fel “gwasanaeth bysiau”. Gobeithiwn nodi pan na fydd hyn yn gymwys wrth inni gael mwy o wybodaeth am eithriadau. Yn achos teithiau a ddisgrifir fel teithiau “bysiau moethus/coets”, cynghorir teithwyr i wirio manylion, costau a materion cysylltiedig ymlaen llaw gyda''r gweithredwr. Er enghraifft, os bydd angen archebu''r gwasanaeth ymlaen llaw, efallai y bydd ffi yn daladwy. Nid yw''r rhan fwyaf o wasanaethau bysiau moethus pellter hir yn gynwysedig yn y cynllun, er bod gan gwmnïau bysiau moethus megis National Express eisoes brisiau arbennig i bobl dros 60 felly gwiriwch drwy gysylltu â''r cwmni.</p>
<h3><a class="QuestionLink" id="A8.6" name="A8.6"></a>8.6) Beth yw Cynllun Teithio Rhatach yr Alban?</h3>
<p>Cychwynodd Scotland-Wide Free Bus Travel ar 1af Ebrill 2006 ac mae''n caniatáu i unrhyw un sydd dros 60 ac i bobl anabl cymwys, deithio''n ddi-dâl ar wasanaethau bysiau rheolaidd ar deithiau lleol a phell o fewn yr Alban. Nid oes unrhyw gyfyngiadau amseroedd brig.</p>
<p>&nbsp;</p>
<p>Er mwyn manteisio ar y gwasanaeth bysiau di-dâl hwn mae angen ichi gael Cerdyn Hawl Cenedlaethol. Nifer fechan yn unig o wasanaethau na fyddant yn cydnabod y cerdyn, er enghraifft bysiau prisiau premiwm a bysiau City Sightseeing.</p>
<p>&nbsp;</p>
<p>Gellir cael mwy o wybodaeth ar: <a target="_child" href="http://www.transportscotland.gov.uk/concessionarytravel">http://www.transportscotland.gov.uk/concessionarytravel <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>
<h3><a class="QuestionLink" id="A8.7" name="A8.7"></a>8.7) Beth yw Cynllun Teithio Rhatach Cymru?</h3>
<p>Mae Llywodraeth Cynulliad Cymru yn rhoi cymorth ariannol i ganiatáu i awdurdodau lleol yng Nghymru ddarparu teithiau di-dâl ar wasanaethau bysiau rheolaidd yn lleol i drigolion Cymru sy''n 60 neu hyn ac i bobl anabl o unrhyw oedran. Mae hefyd yn darparu gwasanaeth teithio di-dâl ar fysiau lleol i rai sy''n cyd-deithio â phobl anabl.</p>
<p>&nbsp;</p>
<p>Mae''r cynllun yn gweithredu ledled Cymru a gall deiliaid pàs rhatach deithio''n ddi-dâl ar unrhyw adeg o''r dydd.  </p>
<p>&nbsp;</p>
<p>Gellir cael mwy o wybodaeth ar: <a target="_child" href="http://new.wales.gov.uk/topics/transport/integrated/concessionary/fares/?lang=cy">
http://new.wales.gov.uk/topics/transport/integrated/concessionary/fares/?lang=cy <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>
<div></div>
<div></div>
<div></div>'




-------------------------------------------------------------
-- FAQs - Carbon
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpCarbon', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="Carbon" name="Carbon"></a><h2>12. CO2 Information</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.1">12.1) How do you estimate my car''s CO2 emissions?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.2">12.2) What assumptions do you make about cars'' CO2 emissions?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.3">12.3) What assumptions do you make in estimating the CO2 emissions produced for your public transport and air journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.4">12.4) What assumptions do you make in comparing CO2 emissions for car journeys with public transport journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.5">12.5) What assumptions do you make about bus and coach journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.6">12.6) Why do you use energy efficiency ratings?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>12. CO2 Information</h2></div>
<br />
<h3><a class="QuestionLink" id="A12.1" name="A12.1"></a>12.1)&nbsp;How do you estimate my car''s CO2 emissions?</h3>
<p>To estimate the CO2 produced by a car journey we first calculate the distance travelled along the planned road route. We calculate the total distance by summing up the distance for each small segment of the journey along the road network. Usually each segment is made up of the distance between adjacent road junctions. We then estimate the fuel your car will use travelling along that route.</p>
<p>&nbsp;</p>
<p>To do this we take account of how many miles per gallon your car does and the predicted congestion. We also account for urban driving where there is a higher frequency of road junctions. This enables us to factor in the stop start nature of driving in a built up area which uses more fuel and produces more CO2 than doing a journey of the same distance on a rural road or motorways.</p>
<p>&nbsp;</p>
<p>Once we have calculate', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="Carbon" name="Carbon"></a><h2>12. Gwybodaeth CO2</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.1">12.1) Sut ydych chi''n amcangyfrif allyriadau CO2 fy nghar?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.2">12.2) Beth ydych chi''n ei dybio am allyriadau CO2 ceir?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.3">12.3) Beth ydych chi''n ei dybio wrth amcangyfrif yr allyriadau CO2 a gynhyrchir ar gyfer siwrneiau trafnidiaeth gyhoeddus ac awyr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.4">12.4) Beth ydych chi''n ei dybio wrth gymharu allyriadau CO2 ar gyfer siwrneiau car &#226; siwrneiau trafnidiaeth gyhoeddus?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.5">12.5) Beth ydych chi''n ei dybio am siwrneiau bws a choets?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A12.6">12.6) Pam ydych chi''n defnyddio cyfraddau effeithlonrwydd ynni?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>12. Gwybodaeth CO2</h2></div>
<br />
<h3><a class="QuestionLink" id="A12.1" name="A12.1"></a>12.1)&nbsp;Sut ydych chi''n amcangyfrif allyriadau CO2 fy nghar?</h3>
<p>I amcangyfrif y CO2 a gynhyrchir gan siwrnai gar, yn gyntaf rydym yn cyfrifo pellter y daith ar hyd y llwybr ffordd a fwriedir. Rydym yn cyfrif cyfanswm y pellter drwy gyfrif y pellter ar gyfer pob rhan fechan o''r siwrnai ar hyd y rhwydwaith ffordd. Fel arfer, mae pob rhan yn cynnwys y pellter rhwng cyffyrdd cyfagos. Wedyn rydym yn amcangyfrif y tanwydd y bydd eich car yn ei ddefnyddio wrth deithio ar hyd y llwybr hwnnw. </p>
<p><br />I wneud hyn, ystyriwn sawl milltir y galwyn a wnaiff eich car a''r tagfeydd a ragwelir. Rydym hefyd yn ystyried gyrru trefol lle ceir mwy o gyffyrdd. Mae hyn yn ein galluogi i ystyried natur atal a chychwyn gyrru mewn ardal adeiledig sy''n defnyddio mwy o danwydd ac yn cynhyrchu mwy o CO2 na theithio''r un pellter ar ffordd wledig neu draffyrdd. </p>
<p><br />Ar &'
--DECLARE @ptrValText BINARY(16)

-- Update text field: Value-En
SET @ptrValText = NULL
SELECT @ptrValText = TEXTPTR([Value-En]) FROM [dbo].[tblContent] WHERE themeid = @ThemeId and groupid = 19 and controlname ='Body Text' and propertyname = '/Channels/TransportDirect/Help/HelpCarbon'

IF @ptrValText IS NOT NULL BEGIN

   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'd the fuel used we convert this to the CO2 produced using a conversion factor depending on whether the fuel you use is petrol or diesel.</p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A12.2" name="A12.2"></a>12.2)&nbsp;What assumptions do you make about cars'' CO2 emissions?</h3>
<p>To find out the amount of CO2 your car produces we calculate the amount of fuel used for a specific journey. We convert this to the CO2 produced using a conversion factor depending on whether the fuel you use is petrol or diesel</p>
<p>&nbsp;</p>
<p>You can enter your car''s fuel efficiency if you know it. This will mean that the calculations we provide are specific to the fuel efficiency of your car.</p>
<p>&nbsp;</p>
<p>Many people are not sure of their car&#8217;s fuel efficiency. If you do not know this we can provide an approximation of the fuel efficiency and CO2 emissions for your car if you tell us whether it has a small medium or large engine and whether it uses petrol or diesel. If you don''t tell us we will assume that you have a medium sized petrol engine car.</p>
<p>&nbsp;</p>
<p><a href="../Web2/Downloads/TransportDirectCO2Data.pdf" target="_child">Click here to see the assumptions we make about the miles per gallon for different sized engines <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />.</a></p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A12.3" name="A12.3"></a>12.3) What assumptions do you make in estimating the CO2 emissions produced for your public transport and air journeys?</h3>
<p>First we calculate your journey distance</p>
<p>&nbsp;</p>
<p>Second we multiply this by a specific factor for the particular modes you will use. This gives the CO2 emissions for your journey.</p>
<p>&nbsp;</p>
<p>Currently these figures are general averages for each type of public transport. Each figure assumes an average number of passengers for the typical sort of vehicles used when travelling by the particular type of transport. The figures come from Department for Transport (DfT) and have been agreed with Department for Environment Food and Rural Affairs (DEFRA)'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 '. <a href="../Web2/Downloads/TransportDirectCO2Data.pdf" target="_child">Click here to see these figures <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />.</a></p>
<p>&nbsp;</p>
<p>We do not always know your exact journey distance by public transport so we have to make a number of assumptions.</p>
<p>&nbsp;</p>
<p>For a bus or coach journey, or the bus section of a longer journey, we know the distance, as the crow flies, between the stop you will get on the bus and the stop you will get off it. We therefore take the distance as the crow flies and multiply it by 1.25 to give an estimation of the bus journey&#8217;s length. We reached this figure by taking a sample of urban, semi urban and rural bus journeys and comparing the distance as the crow flies with the road distance calculated by the car route planner.</p>
<p>&nbsp;</p>
<p>For a rail journey, or the rail section of a longer journey, we know the distance between each intermediate stop on the journey. We add these distances between stations up to give us the total distance for the train part of your journey.</p>
<p>&nbsp;</p>
<p>For a plane journey we know the distance as the crow flies between the two airports you will fly between. We take this distance and multiply this by 1.09 to take account of the additional circling that a typical plane will do around take off and landing. This figure comes from Department for Transport (DfT) and has been agreed with the Department for Environment Food and Rural Affairs (DEFRA).</p>
<p>&nbsp;</p>
<p>Check Journey CO2 calculations are based on point to point distances only.</p>
<p>&nbsp;</p>
<p>For a taxi journey we take the distance as the crow flies between the start and end location of the taxi journey. Once we have this distance we multiply it by 1.03 to take account of average congestion with an average amount of urban driving and we assume the taxi''s miles per gallon are the same as those for a RAC medium sized diesel engine.</p>
<p>&nbsp;</p>
<p>Over time we aim to refine the accuracy of these estimates. To do this we will need to gather more detailed information about the sorts of vehicles used by trans'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'port operators on a journey. For example we may distinguish between diesel and electric trains and how fast the average speed of the train will be on the journey. For buses we may be able to take account of the different numbers of passengers for different sorts of buses to reflect the difference between a typical London bus, a typical rural bus and a typical urban bus outside London.</p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A12.4" name="A12.4"></a>12.4) What assumptions do you make in comparing CO2 emissions for car journeys with public transport journeys?</h3>
<p>We try to give you an overall feel for what the difference in CO2 emissions produced per traveller would be.</p>
<p>&nbsp;</p>
<p>If the journey you have chosen is by car, we calculate the emissions for that journey following the actual route to be taken, if you then select a public transport comparison we estimate the emissions based on travelling the same straight line distance as the car using the different types of public transport. This is for comparison only and there may or may not be viable public transport links between the original car journey locations. If you want to actually travel by public transport use the appropriate planer to find your travel options.</p>
<p>&nbsp;</p>
<p>Similarly if we have planned you an actual public transport journey, when you ask for a comparison we estimate both car and other types of public transport emissions based on travelling the same straight line distance as the selected public transport journey. For the car we estimate how much fuel would be used and hence emissions generated. In this case we assume you would be travelling in a medium sized petrol car travelling on a combination of urban and interurban roads, with average congestion (resulting in the emissions being increased by 1.03 compared with the unrestricted values).</p>
<p>&nbsp;</p>
<p>We compare the predicted CO2 emissions produced for a car journey with a specifi'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'ed number of occupants with an estimate of the total CO2 emissions that would be produced for each traveller on one or more types of public transport.</p>
<p>&nbsp;</p>
<p>However, a car and public transport are different. In absolute terms of reducing CO2 it is always better to use a scheduled public transport service. This is because when you use a car the result is an extra journey is made and extra CO2 is emitted into the atmosphere. On the other hand, when you use public transport journey by bus, coach, plane or train the journey is always scheduled and would take place whether or not you travelled. So in real terms there is no additional CO2 emitted.</p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A12.5" name="A12.5"></a>12.5) What assumptions do you make about bus and coach journeys?</h3>
<p>To determine whether emissions for either a bus or a coach are shown we first estimate the journey distance.</p>
<p>&nbsp;</p>
<p>We assume you will be travelling by bus on short journeys of less than 30KM. So on short journeys we only show the predicted CO2 emissions for a bus.</p>
<p>&nbsp;</p>
<p>On the other hand, if we estimate that the journey distance is longer than 30KM we assume you will be travelling by coach. So on longer journeys we only show the CO2 predicted emissions for a coach.</p>
<p>&nbsp;</p>
<p>To calculate bus emissions we use a factor based on the average bus miles per gallon and number of passengers carried and to calculate coach emissions we use a factor based on the average coach miles per gallon and number of passengers carried.</p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A12.6" name="A12.6"></a>12.6) Why do you use the same colours as the Energy Efficiency Ratings?</h3>
<p>For a number of years it has been widely recognised that electrical goods such as washing machines have been supplied with a standard efficiency rating and this has been presented on a standard A-G chart, from green to red'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 '. This chart''s usage has now been widened and is recommended for use by the Vehicle Certification Agency (VCA) for all new cars. The Journey Planner assumes that cars with one occupant and low emissions are likely to be in the green and yellow area of the bar and cars with one occupant and high emissions will be in the orange and red area. This &#8220;rating&#8221; will remain constant irrespective of the distance of the journey, but the amount of emissions generated will increase as the journey distance increases.</p>
<p>&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>
<div></div>
<div></div>
<div></div>'

END


-- Update text field: Value-Cy
SET @ptrValText = NULL
SELECT @ptrValText = TEXTPTR([Value-Cy]) FROM [dbo].[tblContent] WHERE themeid = @ThemeId and groupid = 19 and controlname ='Body Text' and propertyname = '/Channels/TransportDirect/Help/HelpCarbon'

IF @ptrValText IS NOT NULL BEGIN

   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 '#244;l inni gyfrifo''r tanwydd a ddefnyddir rydym yn troi hwn yn CO2 a gynhyrchir gan ddefnyddio ffactor trawsnewid gan ddibynnu a ydych chi''n defnyddio tanwydd petrol neu ddisel.</p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A12.2" name="A12.2"></a>12.2)&nbsp;Beth ydych chi''n ei dybio am allyriadau CO2 ceir?</h3>
<p>I ganfod faint o CO2 mae eich car yn ei gynhyrchu, rydym yn cyfrifo maint y tanwydd a ddefnyddir ar gyfer siwrnai benodol. Rydym yn troi hwn yn CO2 a gynhyrchir gan ddefnyddio ffactor trawsnewid gan ddibynnu a ydych chi''n defnyddio tanwydd petrol neu ddisel.</p>
<p><br />Gallwch nodi effeithlonrwydd tanwydd eich car os ydych yn ei wybod eisoes. Bydd hyn yn golygu bod y cyfrifiadau a rown yn benodol i effeithlonrwydd tanwydd eich car.</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />Nid yw llawer o bobl yn sicr beth yw effeithlonrwydd tanwydd eu ceir. Os nad ydych yn gwybod hyn, gallwn roi brasamcan o''r effeithlonrwydd tanwydd a''r allyriadau CO2 ar gyfer eich car os gallwch ddweud wrthym a oes ganddo fodur bach, canolig neu fawr ac a yw''n defnyddio petrol neu ddisel. Os na fyddwch yn dweud wrthym, byddwn yn tybio bod gennych gar modur petrol canolig.&nbsp;</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br /><a href="../Web2/Downloads/TransportDirectCO2Data_CY.pdf" target="_child">Cliciwch yma i weld beth rydym yn ei dybio am y milltiroedd y galwyn ar gyfer moduron o wahanol faint <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" />.</a></p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A12.3" name="A12.3"></a>12.3) Beth ydych chi''n ei dybio wrth amcangyfrif yr allyriadau CO2 a gynhyrchir ar gyfer siwrneiau trafnidiaeth gyhoeddus ac awyr?</h3>
<p>Yn gyntaf rydym yn cyfrifo pellter eich siwrnai. </p>
<p><br />Yn ail, rydym yn lluosi''r ffigur hwn gan ffactor penodol ar gyfer y dulliau penodol a ddefnyd'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 'diwch. Mae hyn yn rhoi''r allyriadau CO2 ar gyfer eich siwrnai.</p>
<p><br />Ar hyn o bryd, mae''r ffigurau hyn yn gyfartalion cyffredinol ar gyfer pob math o drafnidiaeth gyhoeddus. Mae pob ffigur yn tybio nifer gyfartalog o deithwyr ar gyfer y math nodweddiadol o gerbydau a ddefnyddir wrth deithio gyda''r math penodol o drafnidiaeth. Daw''r ffigurau o''r Adran Drafnidiaeth ac fe''u cytunwyd &#226; DEFRA.&nbsp;<a href="../Web2/Downloads/TransportDirectCO2Data_CY.pdf" target="_child">Cliciwch yma i weld y ffigurau hyn <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" />.</a></p>
<p>&nbsp;</p>
<p>Nid ydym bob amser yn gwybod union bellter eich siwrnai drwy drafnidiaeth gyhoeddus felly mae''n rhaid inni dybio sawl peth.</p>
<p><br />Ar gyfer siwrnai fws neu goets, neu''r rhan mewn bws o siwrnai hwy, rydym yn gwybod y pellter, fel yr hed y fr&#226;n, rhwng y man y byddwch yn mynd ar y bws a''r man y dewch oddi arno. Cymerwn felly''r pellter fel yr hed y fr&#226;n a''i luosi gan 1.25 i amcangyfrif hyd y siwrnai fws. Cawsom y ffigur hwn drwy gymryd sampl o siwrneiau bws trefol, lled-drefol a gwledig a chymharu''r pellter fel yr hed y fr&#226;n &#226; phellter y ffordd a gyfrifwyd gan y trefnydd llwybr car.&nbsp;&nbsp;&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />Ar gyfer siwrnai ar dr&#234;n, neu ran ar dr&#234;n o siwrnai hwy, rydym yn gwybod y pellter rhwng pob man y mae''r tr&#234;n yn aros ar y siwrnai. Rydym yn adio''r pellteroedd hyn rhwng gorsafoedd i gyfrifo''r pellter cyfan ar gyfer y rhan mewn tr&#234;n o''ch siwrnai.&nbsp;&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />Ar gyfer siwrnai ar awyren, rydym yn gwybod y pellter fel yr hed y fr&#226;n rhwng y ddau faes awyr y byddwch yn hedfan rhyngddynt. Cymerwn y pellter hwn a''i luosi gan 1.09 i ystyried y cylchu ychwanegol a wnaiff awyren nodweddiadol wrth esgyn a glanio. Daw''r ffigur hwn o''r Adran'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 ' Drafnidiaeth ac fe''i cytunwyd ag Adran yr Amgylchedd, Bwyd a Materion Gwledig (DEFRA).</p>
<p><br />Ar gyfer siwrnai dacsi, cymerwn y pellter fel yr hed y fr&#226;n rhwng man cychwyn a diwedd y siwrnai dacsi. Pan fydd gennym y pellter hwn, rydym yn ei luosi gan 1.03 i ystyried y tagfeydd cyfartalog gyda swm cyfartalog o yrru trefol ac rydym yn tybio bod milltiroedd y galwyn y tacsi yr un fath &#226;"r rheini ar gyfer modur diesel canolig RAC. </p>
<p><br />Dros amser, anelwn at fireinio cywirdeb yr amcangyfrifon hyn. I wneud hyn, bydd angen inni gasglu gwybodaeth fanylach am y mathau o gerbydau a ddefnyddir gan weithredwyr trafnidiaeth ar siwrnai. Er enghraifft, gallwn wahaniaethu rhwng trenau diesel a thrydanol a pha mor gyflym y bydd y tr&#234;n yn teithio ar gyfartaledd ar y siwrnai. Ar gyfer bysiau, efallai y gallwn ystyried gwahanol niferoedd y teithwyr ar gyfer gwahanol fathau o fysiau i adlewyrchu''r gwahaniaeth rhwng bws Llundain nodweddiadol, bws gwledig nodweddiadol a bws trefol nodweddiadol y tu allan i Lundain.</p>
<p>&nbsp;</p>
<br />
<h3><a class="QuestionLink" id="A12.4" name="A12.4"></a>12.4) Beth ydych chi''n ei dybio wrth gymharu allyriadau CO2 ar gyfer siwrneiau car &#226; siwrneiau trafnidiaeth gyhoeddus?</h3>
<p>Rydym yn ceisio rhoi teimlad cyffredinol o''r gwahaniaeth mewn allyriadau CO2 a fyddai''n cael eu cynhyrchu fesul teithiwr.</p>
<p><br />Os ydych wedi dewis siwrnai gar, rydym yn cyfrifo''r allyriadau ar gyfer y siwrnai honno gan ddilyn y llwybr gwirioneddol i''w gymryd. Os byddwch wedyn yn dewis siwrnai trafnidiaeth gyhoeddus i gymharu, amcangyfrifwn yr allyriadau yn seiliedig ar deithio''r un pellter llinell syth &#226;"r car gan ddefnyddio''r gwahanol fathau o drafnidiaeth gyhoeddus. Er mwyn cymharu yn unig y mae hyn ac efallai bydd cysylltiadau trafnidiaeth gyhoeddus ymarferol rhwng lleoliadau''r siwrnai gar wreiddiol. Os hoffech deithio ar drafnidiaeth gyhoeddus wedi''r cyfan, defnyddiwch y cynlluniwr priodol i ddod o hyd i''ch opsiynau teithio'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 '.</p>
<p><br />Yn yr un modd, os ydym wedi cynllunio siwrnai trafnidiaeth gyhoeddus ichi, pan fyddwch yn gofyn am gymhariaeth rydym yn amcangyfrif yr allyriadau car ac allyriadau mathau eraill o drafnidiaeth gyhoeddus yn seiliedig ar deithio''r un pellter llinell syth &#226;"r siwrnai trafnidiaeth gyhoeddus a ddewiswyd. Ar gyfer y car, rydym yn amcangyfrif faint o danwydd a fyddai''n cael ei ddefnyddio ac felly faint o allyriadau a gynhyrchir. Yn yr achos hwn tybiwn y byddech yn teithio mewn car petrol canolig gan deithio ar gyfuniad o ffyrdd trefol a rhyngdrefol, gyda thagfeydd cyfartalog (sy''n arwain at gynyddu''r allyriadau 1.03 o gymharu &#226;"r gwerthoedd anghyfyngedig).</p>
<p><br />Rydym yn cymharu''r allyriadau CO2 y rhagwelir y''u cynhyrchir ar gyfer siwrnai gar gyda nifer benodol o deithwyr ag amcangyfrif o gyfanswm yr allyriadau CO2 a fyddai''n cael eu cynhyrchu ar gyfer pob teithiwr mewn un neu fwy math o drafnidiaeth gyhoeddus. </p>
<p><br />Fodd bynnag, mae car a thrafnidiaeth gyhoeddus yn wahanol. Mewn termau absoliwt o leihau CO2, mae bob amser yn well defnyddio gwasanaeth trafnidiaeth gyhoeddus wedi''i amserlennu oherwydd pan ddefnyddiwch gar fe wneir siwrnai ychwanegol a gollyngir CO2 ychwanegol i''r atmosffer. Ar y llaw arall, pan ddefnyddiwch siwrnai trafnidiaeth gyhoeddus ar fws, coets, awyren neu dr&#234;n, mae''r siwrnai bob amser wedi''i hamserlennu a byddai''n digwydd ni waeth a fyddwch chi''n teithio neu beidio. Felly, mewn gwirionedd, ni ollyngir unrhyw CO2 ychwanegol.&nbsp;</p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A12.5" name="A12.5"></a>12.5) Beth ydych chi''n ei dybio am siwrneiau bws a choets?</h3>
<p>I bennu a ddangosir allyriadau ar gyfer bws neu goets, rydym yn amcangyfrif pellter y siwrnai yn gyntaf.&nbsp; </p>
<p><br />Tybiwn y byddwch yn teithio ar fws ar siwrneiau byr sy''n llai na 30 cilometr. Felly ar siwrneiau byr, dim ond yr allyriadau CO2 a ragwelir ar gyfer bws a ddangoswn.</p>
<p><br />Ar y llaw arall, os amcang'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 'yfrifwn fod pellter y siwrnai dros 30 cilometr rydym yn tybio y byddwch yn teithio mewn coets. Felly ar siwrneiau hwy, dim ond yr allyriadau CO2 a ragwelir ar gyfer coets a ddangoswn.</p>
<p><br />I gyfrifo allyriadau bws defnyddiwn ffactor sy''n seiliedig ar y milltiroedd bws cyfartalog y galwyn a nifer y teithwyr a gludir ac i gyfrifo allyriadau coets defnyddiwn ffactor sy''n seiliedig ar filltiroedd coets cyfartalog y galwyn a nifer y teithwyr a gludir.<br /></p>
<br />
<h3><a class="QuestionLink" id="A12.6" name="A12.6"></a>12.6) Pam ydych chi''n defnyddio cyfraddau effeithlonrwydd ynni?</h3>
<p>Ers sawl blynedd, cydnabuwyd yn eang fod nwyddau trydanol fel peiriannau golchi wedi''u cyflenwi gyda chyfradd effeithlonrwydd safonol ac fe gyflwynwyd hyn ar siart A-G safonol, o wyrdd i goch. Defnyddir y siart hwn yn ehangach bellach ac argymhellir bod yr Asiantaeth Tystysgrifo Cerbydau (VCA) yn ei ddefnyddio ar gyfer pob car newydd. Mae Journey Planner yn tybio bod ceir ag un teithiwr ac allyriadau isel yn debygol o fod yn ardal werdd a melyn y bar ac y bydd ceir ag un teithiwr ac allyriadau uchel yn yr ardal oren a choch. Bydd y "gyfradd" hon yn parhau''n gyson ni waeth beth yw pellter y siwrnai, ond bydd swm yr allyriadau a gynhyrchir yn cynyddu wrth i bellter y siwrnai gynyddu.</p>
<p>&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>
<div></div>
<div></div>
<div></div>'

END




-------------------------------------------------------------
-- FAQs - Comm
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpComm', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="CommentFeedback" name="CommentFeedback"></a><h2>15. Comment and feedback</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpComm.aspx#A15.1">15.1)&nbsp;What can I do if I have a problem with this website or the information it has provided me with?</a></p>
<p>&nbsp;</p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>15. Comment and feedback</h2></div>
<br />
<h3><a class="QuestionLink" id="A15.1" name="A15.1"></a>15.1)&nbsp;What can I do if I have a problem with this website or the information it has provided me with?</h3>
<p>If you have a query or complaint, or you want to offer feedback regarding the Journey Planner:</p>
<p>&nbsp;</p>
<ul class="lister">
<li>Click on the &#8216;Contact Transport Direct&#8217; link found at the bottom of the page </li>
<li>Fill in the &#8216;Feedback&#8217; form</li></ul>
<p>&nbsp;</p>
<p>If you require help when using the website, click the &#8216;Help&#8217; button next to the relevant section of the page.&nbsp; This will open a box that shows helpful information.</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>
<div></div>
<div></div>
<div></div>', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="CommentFeedback" name="CommentFeedback"></a><h2>15. Sylwadau ac adborth</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpComm.aspx#A15.1">15.1) Beth y gallaf ei wneud os oes gennyf broblem gyda&#8217;r wefan hon neu&#8217;r wybodaeth y mae wedi ei rhoi i mi?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>15. Sylwadau ac adborth</h2></div>
<br />
<h3><a class="QuestionLink" id="A15.1" name="A15.1"></a>15.1)&nbsp;Beth y gallaf ei wneud os oes gennyf broblem gyda&#8217;r wefan hon neu&#8217;r wybodaeth y mae wedi ei rhoi i mi?</h3>
<p>Os oes gennych ymholiad neu gwyn, neu yr hoffech roi adborth ynglyn &#226; Journey Planner:</p>
<p>&nbsp;</p>
<ul class="lister">
<li>Cliciwch ar y ddolen &#8216;Cysylltwch &#226; Transport Direct&#8217; ar waelod y dudalen. </li>
<li>Llenwch y ffurflen &#8216;Adborth&#8217;.</li></ul>
<p>&nbsp;</p>
<p>Os oes arnoch angen cymorth wrth ddefnyddio&#8217;r wefan, cliciwch y botwm &#8216;Help&#8217; ger yr adran berthnasol o&#8217;r dudalen. Bydd hyn yn agor blwch sy&#8217;n dangos gwybodaeth ddefnyddiol.</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>'



-------------------------------------------------------------
-- FAQs - Costs
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpCosts', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="TicketsAndCosts" name="TicketsAndCosts"></a><h2>11. Tickets and costs</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.1">11.1)&nbsp;Once I&#8217;ve found a journey, can I find out what the fare is?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.2">11.2)&nbsp;Can I find information about bus fares?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.3">11.3)&nbsp;How can I find out how much the fuel will cost for a car journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.4">11.4)&nbsp;How can I find out the full costs of a car journey taking into account how much I have to pay to own and run a car?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.5">11.5)&nbsp;What types of rail fares can I see on the Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.6">11.6)&nbsp;What rail fares are shown in advance of a fares change?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.7">11.7)&nbsp;What is meant by "Route" in the list of rail fares shown for a journey?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>11. Tickets and costs</h2></div>
<br />
<h3><a class="QuestionLink" id="A11.1" name="A11.1"></a>11.1)&nbsp;Once I&#8217;ve found a journey, can I find out what the fare is?</h3>
<p>If you find a journey on the Journey Planner and would like to find out the fare for that journey, select the journey and click &#8216;Ticket/Costs&#8217;. The train or coach ticket(s) required for each part of the journey will be displayed along with their fares, if these are known. You can choose to see &#8216;Child fares&#8217; instead of &#8216;Adult fares&#8217; and you can also enter your &#8216;Discount cards&#8217; to see the discounted fares.</p>
<div><br /></div>
<p><strong>Now you can buy the tickets from online or offline ticket retailers by clicking &#8216;Buy tickets&#8217;.</strong></p>
<div><br /></div>
<p>If you click &#8216;Buy tickets&#8217; a page will list the ticket(s) required for the journey. You need to click &#8216;Select&#8217; at the bottom of a column to see ticket retailer(s) that sell the ticket(s).</p>
<div><br /></div>
<p>Click &#8216;Buy&#8217; across from your preferred ticket retailer to continue the transaction on the ticket retailer&#8217;s site. Please note: All the information you entered on the Journey Planner will be carried over to the retailer&#8217;s site.</p>
<h3><a class="QuestionLink" id="A11.2" name="A11.2"></a>11.2)&nbsp;Can I find information about bus fares?</h3>
<p>In general, the Journey Planner does not have bus fare information.&nbsp;However, you can view local fare information for some stations.</p>
<div><br /></div>
<p>Clicking the station name on a journey "Details" page, or selecting the station using &#8216;Find a Place&#8217; and clicking &#8216;i" (information) will open the "Station Information" page, which contains details about taxi operators, facilities and car parking at the station.</p>
<div><br /></div>
<p>Clicking "Local Fare Information" will display links to any local fare schemes that operate around the station, for example PlusBus schemes that can be bought as an add-on to a train ticket.&nbsp;These links will open in a new browser window.</p>
<h3><a class="QuestionLink" id="A11.3" name="A11.3"></a>11.3)&nbsp;How can I find out how much the fuel will cost for a car journey?</h3>
<p>When you are planning a Door-to-door journey or using &#8216;Find a car route&#8217;, you can enter specific information about your car and the journey you plan to make in order for the Journey Planner to cost your journey.</p>
<div><br /></div>
<p>The type of information you can enter is the size of your car, your car&#8217;s fuel consumption and fuel type, the cost of your fuel, whether you&#8217;d like to avoid tolls, or ferries, etc. The Journey Planner will calculate the &#8216;Fuel cost&#8217;, and will take into account any tolls or ferry costs to give you the total cost for the journey.</p>
<div><br /></div>
<p>These costs are approximate and may vary by 50% or more depending on factors such as weather, driving style, high congestion levels, number of passengers and tyre pressures. They are intended to give you a rough estimate of how much a journey by car might cost.</p>
<div><br /></div>
<p>Once you have planned a car journey, click &#8216;Tickets/Costs&#8217; to find out how much the fuel for the journey is going to cost. The Journey Planner will take into account any information that you entered in the &#8216;Add car journey details&#8217; section. If you have not entered any details in this section, then the Journey Planner uses the defaults which are based on average car size, average fuel costs for an unleaded petrol engine, average fuel consumption.</p>
<div><br /></div>
<h3><a class="QuestionLink" id="A11.4" name="A11.4"></a>11.4)&nbsp;How can I find out the full costs of a car journey taking into account how much I have to pay to own and run a car?</h3>
<p>The Journey Planner can also give you an estimate of the full cost of a journey taking into account the costs of running and owning a car.</p>
<div><br /></div>
<p>On the Tickets/Costs page you will need to select &#8216;Total costs&#8217; rather than &#8216;Fuel costs&#8217; from the drop-down list.</p>
<div><br /></div>
<p>This &#8217;Total cost&#8217; is approximate and will take into account the ongoing costs of running and owning a car. In estimating these costs we will assume you have a medium sized car. If you have a large or small car you will need to select the size on the &#8216;Car details&#8217; page. We will assume your car is up to three years old and that you drive 12,000 miles a year in estimating this cost. More details can be found from the <a href="http://www.theaa.com/motoring_advice/running_costs/index.html" target="_blank">AA <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a> or the <a href="http://www.emmerson-hill.co.uk/documents/RAC_Illustrative_Motoring_costs_April_2013.pdf" target="_blank">RAC <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>. <a href="../Downloads/TransportDirectCO2Data.pdf" target="_child">Click here to see the assumptions we make about the miles per gallon for different sized engines <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />.</a></p>
<div><br /></div>
<p>Fuel is likely to account for around a quarter to a third of the full cost of the journey. Fuel costs may vary by 50% or more depending on factors such as weather, driving style, high congestion levels, number of passengers and tyre pressures.</p>
<div><br /></div>
<h3><a class="QuestionLink" id="A11.5" name="A11.5"></a>11.5)&nbsp;What types of rail fares can I see on the Journey Planner?</h3>
<p>When you click the "Tickets/Costs" button, the Journey Planner searches for, then shows the full range of fares that are available to the general public. &nbsp;If you require a return fare and have not entered a return date or time, you should select "Return" from the dropdown menu in the "Amend fare" section, at the bottom of the page.&nbsp; Zonal fares and season tickets cannot be seen at present.</p>
<div><br /></div>
<h3><a class="QuestionLink" id="A11.6" name="A11.6"></a>11.6)&nbsp;What rail fares are shown in advance of a fares change?</h3>
<p>If you plan a journey for a date in the future, the fares shown will be those that apply on your date of travel provided that your ticket is purchased today.</p>
<div><br /></div>
<h3><a class="QuestionLink" id="A11.7" name="A11.7"></a>11.7)&nbsp;What is meant by &#8216;Route&#8217; in the list of rail fares shown for a journey?</h3>
<p>Rail fares sometimes apply to a particular route or train operator. The route description shown under the ticket type is based on the route shown on the ticket. The validity of rail tickets is explained in the National Rail Conditions of Carriage available on the National Rail website.</p>
<p>&nbsp;</p>
<p>&nbsp;</p></div>
<div></div>
<div></div>
<div></div>
<div></div></div>', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="TicketsAndCosts" name="TicketsAndCosts"></a><h2>11. Tocynnau a chostau</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.1">11.1) Wedi i mi ddarganfod siwrnai, fedra&#8217; i ddarganfod beth yw pris y tocyn?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.2">11.2) Fedra" i gael gwybodaeth am brisiau tocynnau bws?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.3">11.3) Sut y gallaf ddarganfod faint y bydd y tanwydd yn ei gostio am siwrnai mewn car?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.4">11.4) Sut y gallaf ddarganfod costau llawn siwrnai car gan roi ystyriaeth i faint mae&#8217;n rhaid i mi ei dalu i fod yn berchen ar a chynnal car?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.5">11.5)&nbsp;Pa fathau o docynnau rheilffordd y gallaf eu gweld ar Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.6">11.6)&nbsp;Pa brisiau tocynnau rheilffordd a ddangosir cyn unrhyw newid mewn prisiau tocynnau?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCosts.aspx#A11.7">11.7)&nbsp;Beth yw ystyr &#8216;Llwybr&#8217; yn y rhestr o brisiau tr&#234;n a ddangosir ar gyfer siwrnai?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>11. Tocynnau a chostau</h2></div>
<br />
<h3><a class="QuestionLink" id="A11.1" name="A11.1"></a>11.1)&nbsp;Wedi i mi ddarganfod siwrnai, fedra&#8217; i ddarganfod beth yw pris y tocyn?</h3>
<p></p>
<p>Os dewch o hyd i siwrnai ar Journey Planner ac yr hoffech ddarganfod beth yw pris y tocyn am y siwrnai honno, dewiswch y siwrnai a chliciwch &#8216;Tocynnau/Costau&#8217;. Bydd y tocyn(nau) tr&#234;n neu fws moethus sy&#8217;n angenrheidiol ar gyfer pob rhan o&#8217;r siwrnai yn cael ei arddangos ynghyd &#226; phris y tocynnau, os yw''r rhain yn wybyddus. Gallwch ddewis gweld &#8216;Tocynnau plant&#8217; yn lle &#8216;Tocynnau oedolion&#8217; a gallwch hefyd roi eich &#8216;Cardiau disgownt&#8217; i weld beth yw&#8217'
--DECLARE @ptrValText BINARY(16)

-- Update text field: Value-Cy
SET @ptrValText = NULL
SELECT @ptrValText = TEXTPTR([Value-Cy]) FROM [dbo].[tblContent] WHERE themeid = @ThemeId and groupid = 19 and controlname ='Body Text' and propertyname = '/Channels/TransportDirect/Help/HelpCosts'

IF @ptrValText IS NOT NULL BEGIN

   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 ';r prisiau disgownt.</p>
<div><br /></div>
<p><strong>Yn awr gallwch brynu&#8217;r tocynnau oddi wrth adwerthwyr tocynnau ar-lein neu oddi ar lein drwy glicio ar &#8216;Prynu tocynnau&#8217;.</strong></p>
<div><br /></div>
<p>Os cliciwch &#8216;Prynu tocynnau"&nbsp;bydd tudalen yn rhestr tocyn(nau) sy&#8217;n angenrheidiol ar gyfer y siwrnai. Mae angen i chi glicio ar &#8216;Dewiswch&#8217; ar waelod colofn i weld yr adwerthwr/wyr tocynnau sy&#8217;n gwerthu&#8217;r tocyn(nau).</p>
<div><br /></div>
<p>Cliciwch &#8216;Prynwch&#8217; ar draws oddi wrth yr adwerthwr tocynnau a ffafrir gennych i barhau &#226;&#8217;r pryniad ar safle&#8217;r adwerthwr tocynnau. Nodwch: Bydd yr holl wybodaeth a roddwch ar Journey Planner yn cael ei throsglwyddo hefyd i safle&#8217;r adwerthwr.</p>
<p></p>
<h3><a class="QuestionLink" id="A11.2" name="A11.2"></a>11.2)&nbsp;Fedra" i gael gwybodaeth am brisiau tocynnau bws?</h3>
<p>Yn gyffredinol, nid oes gan Journey Planner wybodaeth am brisiau tocynnau bws.&nbsp;Ond gallwch weld gwybodaeth am brisiau lleol ar gyfer rhai gorsafoedd.</p>
<div><br /></div>
<p>Bydd clicio ar enw gorsaf ar dudalen "Manylion" siwrnai, neu ddewis yr orsaf gan ddefnyddio "Canfyddwch Le" a chlicio ar "g" (gwybodaeth) yn agor y dudalen "Gwybodaeth am orsafoedd", sy''n cynnwys manylion am weithredwyr tacsi, cyfleusterau a pharcio ceir yn yr orsaf.</p>
<div><br /></div>
<p>Bydd clicio ar "Gwybodaeth am Brisiau Tocynnau Lleol" yn dangos dolennau ag unrhyw gynlluniau prisiau tocynnau lleol sy''n gweithredu o amgylch yr orsaf, er enghraifft cynllunion PlusBus y gellir eu prynu fel ychwanegiadau i docyn tr&#234;n.&nbsp;Bydd y dolennau hyn yn agor mewn ffenestr porwr newydd.</p>
<h3><a class="QuestionLink" id="A11.3" name="A11.3"></a>11.3) Sut y gallaf ddarganfod faint y bydd y tanwydd yn ei gostio am siwrnai mewn car?</h3>
<p>Pan ydych yn cynllunio siwrnai o Ddrws i ddrws neu yn defnyddio &#8216;Canfyddwch lwybr car&#8217;, gallwch nodi gwybodaeth benodol am eich ca'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 'r a&#8217;r siwrnai y bwriadwch ei gwneud er mwyn i Journey Planner gostio eich siwrnai.</p>
<div><br /></div>
<p>Y math o wybodaeth y gallwch ei roi yw maint eich car, faint o danwydd a ddefnyddir gan eich car a&#8217;r math o danwydd, cost eich tanwydd, p&#8217;run ai a hoffech osgoi tollau neu ffer&#239;au, ac ati. Bydd Journey Planner yn cyfrifo &#8216;Cost y tanwydd&#8217; ac yn rhoi ystyriaeth i unrhyw dollau neu gostau fferi i roi cyfanswm cost y siwrnai i chi.</p>
<div><br /></div>
<p>Mae&#8217;r costau hyn yn rhai bras a gallant amrywio o 50% neu fwy yn dibynnu ar ffactorau fel y tywydd, dull o yrru, lefelau tagfeydd uchel, nifer y teithwyr a phwysedd y teiars. Bwriedir iddynt roi amcangyfrif bras i chi o faint y gallai siwrnai mewn car ei gostio.</p>
<div><br /></div>
<p>Wedi i chi gynllunio siwrnai mewn car, cliciwch ar &#8216;Tocynnau/costau&#8217; i ddarganfod faint mae&#8217;r tanwydd am y siwrnai yn mynd i&#8217;w gostio. Bydd Journey Planner yn rhoi ystyriaeth i unrhyw wybodaeth yr ydych wedi ei rhoi yn yr adran &#8216;Ychwanegwch fanylion y siwrnai car&#8217;. Os nad ydych wedi rhoi unrhyw fanylion yn yr adran hon, yna mae Journey Planner yn defnyddio&#8217;r adrannau diofyn a seilir ar faint car cyfartalog, costau tanwydd cyfartalog ar gyfer injan petrol diblwm, a&#8217;r defnydd cyfartalog o danwydd.</p>
<div><br /></div>
<h3><a class="QuestionLink" id="A11.4" name="A11.4"></a>11.4) Sut y gallaf ddarganfod costau llawn siwrnai car gan roi ystyriaeth i faint mae&#8217;n rhaid i mi ei dalu i fod yn berchen ar a chynnal car?</h3>
<p>Gall Journey Planner roi amcangyfrif i chi hefyd o gost lawn siwrnai gan roi ystyriaeth i gostau cynnal a bod yn berchen ar gar.</p>
<div><br /></div>
<p>Ar y dudalen Tocynnau/Costau bydd angen i chi ddewis &#8216;Cyfanswm y gost&#8217; yn hytrach na &#8216;Costau tanwydd&#8217; o&#8217;r rhestr a ollyngir i lawr.</p>
<div><br /></div>
<p>Mae &#8216;Cyfanswm y gost&#8217; yn fras a bydd yn rhoi ystyriaeth i gostau p'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 'arhaus cynnal a bod yn berchen ar gar. Wrth amcangyfrif y costau hyn byddwn yn tybio bod gennych gar o faint canolig. Os oes gennych gar mawr neu gar bach bydd angen i chi ddewis y maint ar y dudalen &#8216;Manylion car&#8217;. Byddwn yn rhagdybio bod eich car hyd at dair mlwydd oed a&#8217;ch bod yn gyrru 12,000 o filltiroedd y flwyddyn wrth amcangyfrif y gost hon. Gellir cael mwy o fanylion oddi wrth yr <a href="http://www.theaa.com/motoring_advice/running_costs/index.html" target="_blank">AA <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a> neu''r <a href="http://www.emmerson-hill.co.uk/documents/RAC_Illustrative_Motoring_costs_April_2013.pdf" target="_blank">RAC <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a>. <a href="../Downloads/TransportDirectCO2Data_CY.pdf" target="_child">Cliciwch yma i weld beth rydym yn ei dybio am y milltiroedd y galwyn ar gyfer moduron o wahanol faint <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" />.</a></p>
<div><br /></div>
<p>Mae tanwydd yn debygol o gyfrif am tua chwarter i draean costau llawn eich siwrnai. Gall costau tanwydd amrywio o 50% neu fwy yn dibynnu ar ffactorau fel y tywydd, dull o yrru, lefelau tagfeydd uchel, nifer y teithwyr a phwysedd y teiars.</p>
<div><br /></div>
<h3><a class="QuestionLink" id="A11.5" name="A11.5"></a>11.5)&nbsp;Pa fathau o docynnau rheilffordd y gallaf eu gweld ar Journey Planner?</h3>
<p>Pan gliciwch y botwm "Tocynnau/Costau", mae Journey Planner yn chwilio am, ac yna yn dangos yr ystod lawn o docynnau sydd ar gael i''r cyhoedd. &nbsp;Os oes arnoch angen tocyn mynd a dod ac nad ydych wedi cofnodi dyddiad nac amser dychwelyd, dylech ddewis "Mynd a dod" o''r rhestr a ollyngir i lawr yn yr adran "Diwygiwch fanylion y tocyn" ar waelod y dudalen.&nbsp; Ni ellir gweld prisiau tocynnau cylchfaol a thocynnau tymor ar hyn o bryd.</p>
<div><br /></div>
<h3><a class="QuestionLink" id="A11.6" name="A11.6"></a>11.6)&nbsp;Pa brisiau tocynnau rheilffordd a ddangosir cyn unrhyw newid mewn prisiau tocynnau?</h3>
<p>Os cynlluniwch siwrnai ar gyfer dyddiad yn y dyfodol, y prisiau tocynnau a ddangosir fydd y rhai hynny sy''n berthnasol ar eich dyddiad teithio cyn belled ag y pryn'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 'ir eich tocyn heddiw.</p>
<div><br /></div>
<h3><a class="QuestionLink" id="A11.7" name="A11.7"></a>11.7)&nbsp;Beth yw ystyr &#8216;Llwybr&#8217; yn y rhestr o brisiau tr&#234;n a ddangosir ar gyfer siwrnai?</h3>
<p>Mae prisiau tr&#234;n weithiau yn berthnasol i lwybr neu weithredwr trenau penodol. Mae''r disgrifiad llwybr a ddangosir o dan y math o docyn yn seiliedig ar y llwybr a ddangosir ar y tocyn. Eglurir dilysrwydd tocynnau tr&#234;n yn Amodau Cludo National Rail sydd ar gael ar wefan National Rail.</p>
<p>&nbsp;</p>
<p>&nbsp;</p></div>
<div></div>
<div></div>
<div></div>
<div></div></div>'

END





-------------------------------------------------------------
-- FAQs - Info
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpInfo', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="Info" name="Info"></a><h2>1. About the Journey Planner&nbsp;</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.1">1.1)&nbsp;How much of Britain does the Journey Planner cover?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.2">1.2) Where does the Journey Planner get its information?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.3">1.3) Is the information on the Journey Planner accurate?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.4">1.4) How often is the information on the Journey Planner updated?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.5">1.5) What can I do if I know of information that is missing or find&nbsp;inaccurate infomation?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.6">1.6) How do I find out more about the Journey Planner?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>1. About the Journey Planner</h2></div>
<br />
<h3><a class="QuestionLink" id="A1.1" name="A1.1"></a>1.1)&nbsp;How much of Britain does the Journey Planner cover?</h3>
<p>The Journey Planner currently covers Great Britain excluding Northern Ireland.&nbsp; We may have more information on certain areas than others,&nbsp;but we are constantly seeking new sources of information for the website.</p>
<p>&nbsp;</p>
<br />
<h3><a class="QuestionLink" id="A1.2" name="A1.2"></a>1.2)&nbsp;Where does the Journey Planner get its information?</h3>
<p>The majority of the public transport information is obtained from the regional &#8216;traveline&#8217; partners. </p>
<p>&nbsp;</p>
<p><strong>For rail information</strong></p>
<p><strong></strong>&nbsp;</p>
<ul class="listerdisc">
<li>ATOC &#8211; &#8220;Association of Train Operating Companies&#8221;</li></ul>
<p>&nbsp;</p>
<p><strong>For coach information</strong></p>
<p><strong></strong>&nbsp;</p>
<ul class="listerdisc">
<li>National Express </li>
<li>Scottish Citylink</li></ul>
<p>&nbsp;</p>
<p><strong>For major road information</strong></p>
<p><strong></strong>&nbsp;</p>
<ul class="listerdisc">
<li>Highways Agency </li>
<li>Traffic Scotland </li>
<li>Traffic Wales</li></ul>
<p>&nbsp;</p>
<p>In the future, we plan to include information for Northern Ireland, the Channel Islands and the Isle of Man on the Journey Planner website.&nbsp; For information on all of the Journey Planner data providers, see &#8216;Data Providers&#8217; in the &#8216;About Transport Direct&#8217; section.<br /></p>
<p>&nbsp;</p>
<br />
<h3><a class="QuestionLink" id="A1.3" name="A1.3"></a>1.3)&nbsp;Is the information on the Journey Planner accurate?</h3>
<p>While the Journey Planner aims to provide accurate and up-to-date information, we do not guarantee that the information will always be complete or correct. For further details, please read the &#8216;Terms &amp; conditions&#8217;. </p>
<p>&nbsp;</p>
<br />
<h3><a class="QuestionLink" id="A1.4" name="A1.4"></a>1.4)&nbsp;How often is the information on the Journey Planner updated?</h3>
<p>Journey planning and timetable information is updated on a daily or weekly basis, depending on type of transport.</p>
<p>&nbsp;</p>
<ul class="listerdisc">
<li>Rail timetable and fares information is updated every night, whereas bus timetables are generally updated weekly. &nbsp;Changes to schedules and fares may occur after the information has been obtained by the Journey Planner </li>
<li>&#8216;Live travel&#8217; news shown on the website is updated every 15 minutes<br /></li></ul>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A1.5" name="A1.5"></a>1.5)&nbsp;What can I do if I know of information that is missing or find inaccurate information?</h3>
<p>If you find any inaccurate information or if you have a complaint or suggestion regarding the information on the Journey Planner, you can send Transport Direct feedback:</p>
<p>&nbsp;</p>
<ul class="listerdisc">
<li>Click on the &#8216;Contact Transport Direct&#8217; link found at the bottom of the page </li>
<li>Fill in the &#8216;Feedback&#8217; form<br /></li></ul>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A1.6" name="A1.6"></a>1.6)&nbsp;How do I find out more about Transport Direct?</h3>
<p>For further information on Transport Direct please visit <a href="http://www.dft.gov.uk/transportdirect" target="_child" >http://www.dft.gov.uk/transportdirect <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>. This website provides all the information on the set up of Transport Direct.</p>
<p>&nbsp;</p></div></div>', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="Info" name="Info"></a><h2>1. Yngl&#375;n &#226; Journey Planner&nbsp;</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.1">1.1) Faint o Brydain y mae Journey Planner yn ymdrin ag ef?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.2">1.2) O ble y caiff Journey Planner ei wybodaeth?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.3">1.3) A yw&#8217;r wybodaeth ar Journey Planner yn gywir? </a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.4">1.4) Pa mor aml y mae&#8217;r wybodaeth ar Journey Planner yn cael ei diweddaru?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.5">1.5) Beth alla&#8217; i ei wneud os ydw i&#8217;n gwybod am wybodaeth sydd ar goll neu yn dod o hyd i wybodaeth sy&#8217;n anghywir?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpInfo.aspx#A1.6">1.6) Sut ydw i''n cael rhagor o wybodaeth am Journey Planner?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>1. Yngl&#375;n &#226; Journey Planner</h2></div>
<h3><a class="QuestionLink" id="A1.1" name="A1.1"></a>1.1)&nbsp;Faint o Brydain y mae Journey Planner yn ymdrin ag ef?</h3>
<p>Mae Journey Planner ar hyn o bryd yn ymdrin &#226;&#8217;r Deyrnas Gyfunol heb gynnwys Gogledd Iwerddon.&nbsp; Mae''n bosibl fod gennym fwy o wybodaeth am rai ardaloedd nag sydd gennym am eraill, ond rydym yn chwilio am ffynonellau gwybodaeth newydd ar gyfer y wefan yn barhaus.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A1.2" name="A1.2"></a>1.2) O ble y caiff Journey Planner ei wybodaeth?</h3>
<p>Ceir mwyafrif yr wybodaeth am gludiant cyhoeddus oddi wrth ein partneriaid "traveline" rhanbarthol. </p>
<p>&nbsp;</p>
<p><strong>I gael gwybodaeth am y rheilffyrdd</strong></p>
<p><strong></strong>&nbsp;</p>
<ul class="listerdisc">
<li>ATOC - "Cymdeithas y Cwmn&#239;au Gweithredu Trenau"</li></ul>
<p>&nbsp;</p>
<p><strong>I gael gwybodaeth am fysiau moethus</strong></p>
<p><strong></strong>&nbsp;</p>
<ul class="listerdisc">
<li>National Express </li>
<li>Scottish Citylink</li></ul>
<p>&nbsp;</p>
<p><strong>I gael gwybodaeth am briffyrdd</strong></p>
<p><strong></strong>&nbsp;</p>
<ul class="listerdisc">
<li>Asiantaeth y Priffyrdd </li>
<li>Trafnidiaeth Alban </li>
<li>Trafnidiaeth Cymru</li></ul>
<p>&nbsp;</p>
<p>Yn y dyfodol, bwriadwn gynnwys gwybodaeth am Ogledd Iwerddon, Ynysoedd y Sianel ac Ynys Manaw ar wefan Journey Planner.&nbsp; Ar gyfer gwybodaeth am holl ddarparwyr data Journey Planner, gweler &#8220;Darparwyr Data&#8221;&nbsp; yn yr adran &#8220;Amdanom ni&#8221;.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A1.3" name="A1.3"></a>1.3) A yw&#8217;r wybodaeth ar Journey Planner yn gywir?</h3>
<p>Tra bo Journey Planner yn amcanu at ddarparu gwybodaeth gywir a&#8217;r wybodaeth ddiweddaraf, nid ydym yn sicrhau y bydd yr wybodaeth bob amser yn gyflawn nac yn gywir. I gael manylion pellach, darllenwch yr "Amodau a&#8217;r Telerau". </p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A1.4" name="A1.4"></a>1.4) Pa mor aml y mae&#8217;r wybodaeth ar Journey Planner yn cael ei diweddaru?</h3>
<p>Mae gwybodaeth am gynllunio siwrneion ac am yr amserlen yn cael ei diweddaru yn ddyddiol neu yn wythnosol, yn dibynnu ar y math o gludiant.</p>
<p>&nbsp;</p>
<ul class="listerdisc">
<li>Mae amserlen y rheilffyrdd a gwybodaeth am brisiau tocynnau yn cael eu diweddaru bob nos, tra bo amserlenni bysiau fel arfer yn cael eu diwedddaru yn wythnosol. &nbsp;Gall newidiadau i amserlenni a phrisiau tocynnau ddigwydd wedi i Journey Planner gael y wybodaeth</li>
<li>Caiff newyddion "Teithio byw" a ddangosir ar Journey Planner ei ddiweddaru bob 15 munud<br /></li></ul>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A1.5" name="A1.5"></a>1.5) Beth alla&#8217; i ei wneud os ydw i&#8217;n gwybod am wybodaeth sydd ar goll neu yn dod o hyd i wybodaeth sy&#8217;n anghywir?</h3>
<p>Os deuwch ar draws unrhyw wybodaeth anghywir neu os oes gennych gwyn neu awgrym am yr wybodaeth ar Journey Planner, gallwch anfon adborth atom:</p>
<p>&nbsp;</p>
<ul class="listerdisc">
<li>Cliciwch ar y ddolen "Cysylltwch &#226; Transport Direct" a welir ar waelod y dudalen </li>
<li>Llenwch y ffurflen "Adborth"</li></ul>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A1.6" name="A1.6"></a>1.6)&nbsp;Sut ydw i''n cael rhagor o wybodaeth am Journey Planner?</h3>
<p>For further information on Transport Direct please visit <a href="http://www.dft.gov.uk/transportdirect" target="_child" >http://www.dft.gov.uk/transportdirect <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>. This website provides all the information on the set up of Transport Direct.</p>
<p>&nbsp;</p></div></div>
'




-------------------------------------------------------------
-- FAQs - JPlan
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpJplan', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>2. General Journey planning</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.1">2.1) Which journey planner should I use?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.2">2.2) How do I plan a journey using the Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.3">2.3) What if I don''t know the exact address(es) of where I want to travel from or to?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.4">2.4) How do I find a station or an airport?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.5">2.5) How do I add specific details or set travel preferences for my journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.6">2.6) When I add a via point to my journey why isn''t it always shown in my journey results?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.7">2.7) How can I plan a day trip to two destinations?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.8">2.8) How do I add a connecting journey to my overall journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.9">2.9) If I add a connecting public transport journey to a car/taxi journey, how can I make sure I have enough time to park my car/pay the taxi fare before boarding the train?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.10">2.10) How do I change the leaving or arrival times for the start or the end of my journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.11">2.11) How is the Journey Planner (Powered By) different from other journey planners?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.12">2.12) How can I find out about the accessibility of the types of transport in the journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.13">2.13) Does Transport Direct offer walk planning? </a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.14">2.14) How can I find the detailed directions for a walk?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.15">2.15) In which areas will I see a link to the walkit.com site?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.16">2.16) Is real time information included in my journey plan?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.17">2.17) I can''t find the place I want to travel from or to?</a></p>
<!-- <p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.18">2.18) How do I plan journeys to destinations outside Great Britain?</a></p> -->
<p>&nbsp;</p>
<div class="hdtypethree">
<h2>2. General Journey Planning</h2></div>
<br />
<h3><a class="QuestionLink" id="A2.1" name="A2.1"></a>2.1) Which journey planner should I use?</h3>
<p>If you know the type of transport you would like to travel by, choose one of the planners which is specific to that type of transport e.g. Find a train, Find a f', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>2. Cynllunio Siwrnai Cyffredinol</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.1">2.1) Pa gynlluniwr siwrnai y dylwn ei ddefnyddio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.2">2.2) Sut ydw i&#8217;n cynllunio siwrnai yn defnyddio Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.3">2.3) Beth os nad ydw i&#8217;n gwybod yr union gyfeiriad(au) y lle yr ydw i eisiau teithio ohono neu iddo?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.4">2.4) Sut ydw i''n dod o hyd i orsaf neu faes awyr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.5">2.5) Sut y gallaf ychwanegu manylion penodol neu osod hoffterau teithio ar gyfer fy siwrneion?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.6">2.6) When I add a via point to my journey why isn''t it always shown in my journey results?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.7">2.7) Sut y gallaf gynllunio taith dydd i ddau gyrchfan?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.8">2.8) Sut y gallaf ychwanegu siwrnai gysylltiol at fy siwrnai gyffredinol?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.9">2.9) Pe byddwn yn ychwanegu siwrnai trafnidiaeth gyhoeddus gysylltiol at siwrnai gar/tacsi, sut y gallaf sicrhau bod gennyf ddigon o amser i barcio fy nghar/talu am y tacsi cyn mynd ar y tr&#234;n?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.10">2.10) Sut ydw i''n newid amserau gadael neu gyrraedd ar gyfer dechrau neu ddiwedd fy siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.11">2.11) Sut mae Transport Direct yn wahanol i gynllunwyr siwrneion eraill?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.12">2.12) Sut y gallaf ddarganfod am hygyrchedd y mathau o gludiant yn y siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.13">2.13) A yw Transport Direct yn cynnig cynlluniau cerdded? </a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.14">2.14) Sut allaf i ddarganfod y cyfarwyddiadau manwl ar gyfer taith gerdded?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.15">2.15) Ym mha ardaloedd allaf i ddarganfod manylion ar gyfer taith gerdded?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.16">2.16) A yw gwybodaeth amser real yn cael ei chynnwys yng nghynllun fy siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.17">2.17) Ni allaf ddarganfod y lle yr wyf eisiau teithio ohono/iddo?</a></p>
<!-- <p class="rsviewcontentrow"><a href="/Web2/Help/HelpJplan.aspx#A2.18">2.18) How do I plan journeys to destinations outside Great Britain?</a></p> -->
<p>&nbsp;</p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>2. Cynllunio Siwrnai Cyffredinol</h2></div>
<h3><a class="QuestionLink" id="A2.1" name="A2.1"></a>2.1) Pa gynlluniwr siwrnai y dylwn ei ddefnyddio?</h3>
<p>Os ydych yn gwybod y math o'
--DECLARE @ptrValText BINARY(16)

-- Update text field: Value-En
SET @ptrValText = NULL
SELECT @ptrValText = TEXTPTR([Value-En]) FROM [dbo].[tblContent] WHERE themeid = @ThemeId and groupid = 19 and controlname ='Body Text' and propertyname = '/Channels/TransportDirect/Help/HelpJplan'

IF @ptrValText IS NOT NULL BEGIN

   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'light. <br /><br />If you would like to compare public transport travel options with directions by car, or if you simply want a complete journey by joined-up public transport, use the door-to-door journey planner. <br /><br />The planners which are specific to a certain type of transport may restrict you to how you specify the &#8216;to&#8217; and &#8216;from&#8217; locations e.g. &#8216;Find a train&#8217; will assume that the place which you enter is a railway station. <br /><br />The door-to-door journey planner asks you to specify the type of location e.g. station/airport, Town/district/village. </p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.2" name="A2.2"></a>2.2) How do I plan a journey using the Journey Planner?</h3>
<p><strong>Step 1:&nbsp; Choose a planner</strong></p> 
<p>Click on the &#8216;Plan a journey&#8217; tab&nbsp;and</p>
<ol class="numberlist">
<li>Click on the type of transport you would like to travel by &#8211; train, flight, coach, car, door to door. </li>
<li>Alternatively, use the door to door journey planner on the homepage.</li></ol>
<p>&nbsp;</p>
<p><strong>Step 2&nbsp;: Enter locations</strong></p><br />
<p><strong>If you have chosen Train or Coach</strong></p> 
<div>
<ol class="numberlist">
<li>In the &#8216;From&#8217; section, type the train or coach station you would like to start your journey from in the box. </li>
<li>In the &#8216;To&#8217; section, type the station you would like to travel to in the box.</li></ol></div>
<p>You can use a map to find the stations nearest to a location you specify by clicking the &#8216;Find nearest&#8230;&#8217; button.</p><br />
<p><strong>If you have chosen Car</strong><br /></p>
<div>
<ol class="numberlist">
<li>In the &#8216;From&#8217; section, select the kind of location you want to enter as a location to start your journey from (e.g. &#8216;Town/district/village&#8217;, &#8216;Address/postcode&#8217;&#8230;etc). </li> 
<li>Then type the location you would like to start your journey from in the box. </li>
<li>In the &#8216;To&#8217;'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 ' section, select the kind of location you want to enter as a destination (e.g. &#8216;Town/district/village&#8217;, &#8216;Address/postcode&#8217;&#8230;etc). </li>
<li>Then type the location you would like to travel to in the box. </li></ol></div>
<p></p>
<p>You can use a map to help you find locations by clicking the &#8216;Find on map&#8217; button.</p><br />
<p><strong>If you have chosen Flight</strong><br /></p>
<div>
<ol class="numberlist">
<li>In the &#8216;From&#8217; section, select either the region or the airport you would like to fly from.&nbsp; If you select a region you will see the airports in that region appear below the region.&nbsp;They will all be ticked, so if the list contains any airports you do not want to fly from, untick them before continuing. </li>
<li>There is a tickbox for direct flights only in the next section. If you are only willing to fly direct to your destination, leave it ticked.&nbsp; If you don&#8217;t mind stopping somewhere en route to your destination, untick the box. </li>
<li>In the &#8216;To&#8217; section, select either the region or the airport you would like to fly to.&nbsp; Again, if you select a region you will see the airports in that region appear below the region and you can untick any you do not want to fly to. </li></ol></div>
<p></p>
<p>You can use a map to help you find airports near to a location by clicking the &#8216;Find nearest &#8230;&#8217; button. </p>
<br />
<p><strong>Step 3: Select dates and times</strong></p>
<div>
<ol class="numberlist">
<li>Use the drop-down lists to&nbsp;select the dates and times of your journey.</li>
<li>You can choose to either <strong>leave at</strong> or <strong>arrive by</strong> the date/time you specify.</li>
<li>If you are planning a return journey, choose the date and time you would like to come back on from the drop-down lists.</li>
<li>If you '
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'are not sure of the date you would like to return, you can choose "Open return".&nbsp; This will not plan a return journey, but will be used in any fare calculations.</li></ol></div>
<p>&nbsp;</p>
<p><strong>Step 4: Optional&nbsp;- Click the &#8216;Advanced options&#8217; button to add more detail to your request</strong></p>
<div>
<ol class="numberlist">
<li>If you would like to specify certain details for that type of transport or travel via a location, you can specify this in this section. </li></ol></div>
<p><strong></strong>&nbsp;</p>
<p><strong>Step 5: Click &#8216;Next&#8217;</strong></p>
<ol class="numberlist">
<li>Once you have filled in all the boxes on this page, click &#8216;Next&#8217;.</li></ol>
<p><strong></strong>&nbsp;</p>
<p><strong>Step 6: Confirm your details</strong></p>
<p><strong></strong>&nbsp;</p>
<p>If the Journey Planner is not able to find an exact match for the locations/stations you typed in, a &#8216;Confirmation&#8217; page may open up once you have clicked &#8216;Next&#8217;. A drop-down list will show you one or more similar matches for that location.</p>
<p>&nbsp;</p>
<p>Depending on the information you entered, you will need to do <strong>one</strong> of the following:</p>
<p>&nbsp;</p>
<ul class="lister">
<li>Select a location option from the drop down list</li></ul>
<ul class="lister">
<li>Search for the location as a different kind of location (e.g. &#8220;Address/postcode&#8221;)</li></ul>
<p>&nbsp;</p>
<p>Also, if there are any problems with the date, time or travel preferences/details you have entered, these will be highlighted in red with a message explaining what you must do.</p>
<div>
<ol class="numberlist">
<li>Once you have confirmed all the highlighted sections in this page, click &#8216;Next&#8217;.</li>
<li>Please note, some locations have further locations within them.&nbsp; </li>
<li>Some have the words &#8220;More op'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'tions for&#8230;&#8221; written in front of them.</li>
<li>If you select one of these and click &#8216;Next&#8217;, you will be given a list of all the locations that exist within this first location. </li>
<li>Select one of these and click "Next". </li>
<li>Alternately, if you click "Back", the first list of locations will appear again.</li></ol>
<p>&nbsp;</p>
<br />
<h3><a class="QuestionLink" id="A2.3" name="A2.3"></a>2.3) What if I don''t know the exact address(es) of where I want to travel from or to?</h3>
<p>When you use the door-to-door Journey Planner you can choose to enter locations that are not full addresses, such as town names, postcodes or station names.&nbsp; You may also enter names of attractions or facilities, such as hotel names.</p>
<p>&nbsp;</p>
<p>You can also use a map to find the exact location you want to travel to or from. </p>
<p>&nbsp;</p>
<p><strong>Example 1</strong></p>
<p><strong>You would like to travel to an address for which you do not know the postcode:</strong></p>
<ol class="numberlist">
<li>&#8216;Address/postcode&#8217; is already selected, so there is no need to choose another category. </li>
<li>Type in as much of the address as you know (e.g. 123 Victoria Street,London).&nbsp;</li></ol>
<p><br /><strong>Example 2</strong></p>
<p><strong>You know the postcode of the location:</strong></p>
<ol class="numberlist">
<li>&#8216;Address/postcode&#8217; is already selected, so there is no need to choose another category. </li>
<li>Type in the full postcode (e.g.&#8221;EN4 7QQ&#8221;) or the partial postcode (e.g. &#8220;SE1&#8221;).</li></ol>
<p><strong>Example 3</strong></p>
<p><strong>You think you know a main station near to where you would like to travel from:</strong></p>
<ol class="numberlist">
<li>Select &#8216;Station/airport&#8217;. </li>
<li>Type the name of the station in the box (e.g. &#8220;Waterloo&#8221;).</li></ol>
<p><strong>Example 4</strong></p>
<p><strong>For a door-to-door journey, if you think you know '
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'a bus stop near to where you would like to travel from:</strong></p>
<ol class="numberlist">
<li>Select &#8216;All stops&#8217;. </li>
<li>Type the SMS code of the stop if you know it, or the place where the stop is located (e.g. London Road, Buckingham), in the box.</li></ol>
<p><strong>Example 5</strong></p>
<p><strong>You want to know how to get to a general area, such as a town or suburb, rather than a specific location:</strong></p>
<ol class="numberlist">
<li>Select &#8216;Town/district/village&#8217;. </li>
<li>Type, for example, &#8220;Manchester&#8221; (city), or &#8220;Stretford&#8221; (a suburb of Manchester) or &#8220;Bollington&#8221; (a village near Manchester) in the box.</li></ol>
<p><strong>Example 6</strong></p>
<p><strong>You want to travel to an attraction or facility, but don&#8217;t know the address:</strong></p>
<ol>
<li>Select &#8216;Facility/attraction&#8217;. </li>
<li>Type an attraction or a facility name (e.g. &#8220;British Library&#8221; or &#8220;Edinburgh Castle&#8221;) in the box.<br /><strong></strong></li></ol>
<p><strong>Example 7</strong></p>
<p><strong>You are not sure of the exact location that you want to travel to, but you know the general area of its whereabouts.&nbsp; In this case, you can use a map to help you choose the correct location:</strong></p>
<ol class="numberlist">
<li>Select the kind of location e.g. &#8216;Town/district/village&#8217; in the &#8216;To&#8217; section. </li>
<li>Type in the location (e.g. &#8220;Edinburgh&#8221;). </li>
<li>Click the &#8216;Find on map&#8217; button next to where you have typed in the location. </li>
<li>If necessary, confirm the location you have entered by selecting an option from the drop down list (highlighted in yellow) and click &#8216;Next&#8217;. </li>
<li>Use the map tools to select the exact location you want to travel to. </li>
<li>Once you have selected where you want to travel to, click &#8216;Next&#8217;.</li></ol><br />
<h3><a class="QuestionLink" id="A2.4" name="A2.4"></a>2.4) How do I find a stat'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'ion or an airport?</h3>
<p><strong>Step 1: Open "Find a place"</strong></p>
<ul class="lister">
<li>Click &#8216;Find nearest Stations and Airports".</li></ul>
<p><strong></strong>&nbsp;</p>
<p><strong>Step 2: Select the type of stations you would like to search for</strong></p>
<ul class="lister">
<li>Tick any of the station types you are interested in.</li></ul>
<p><strong></strong>&nbsp;</p>
<p><strong>Step 3: Select the kind of location the station is near</strong></p>
<ul class="listerdisc">
<li>Select the kind of location you want to type in (e.g. &#8216;Town/district/village&#8217;, &#8216;Address/postcode&#8217;, &#8216;Facility/attraction&#8217; &#8230;etc)</li></ul>
<ul class="lister">
<li>Type the location name in the box.</li></ul>
<p><strong></strong>&nbsp;</p>
<p><strong>Step 4: Click "Next"</strong></p>
<p>&nbsp;</p>
<p><strong>Step 5: Confirm your location</strong></p>
<p>Once you have clicked &#8216;Next&#8217;, if the Journey Planner is not able to find an exact match for the location you typed in, a drop-down list (highlighted in yellow) will show you one or more similar matches for that location.</p>
<p>&nbsp;</p>
<p>Depending on the information you entered, you will need to do <strong>one</strong> of the following:</p>
<ul class="listerdisc">
<li>Select a location option from the drop-down list.&nbsp; Please note if the location you want is already visible in the yellow box, you do not need to re-select it </li>
<li>Search for the location as a different kind of location (e.g. &#8220;Address/postcode&#8221;) </li>
<li>Enter a new location</li></ul>
<p>&nbsp;</p>
<p>Once your information is confirmed, stations near to that location will be listed on the page.&nbsp; You can click &#8216;Show map&#8217; to see a map with the station(s) on it.</p>
<p>&nbsp;</p>
<p><strong>Step 6: If you would like to plan a journey once you find the station you are looking for, make sure it is ticked and click &#8216;Travel from&#8217; or &#8216;Travel to&#8217;. (optional)</strong>'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 '</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.5" name="A2.5"></a>2.5) How do I add specific details or set travel preferences for my journeys?</h3>
<p>1. Click &#8216;Advanced options&#8217; at the bottom of the input page</p>
<p>&nbsp;</p>
<p>2. Enter any preferences you would like, for example:</p>
<ul class="listerdisc">
<li>How quickly you are able to make changes at stops and stations </li>
<li>Your preferred number of changes </li>
<li>Your preferred walking speed </li>
<li>Your preferred maximum driving speed </li>
<li>Where you would like to travel via</li></ul>
<p>&nbsp;</p>
<p>If you would like to save these preferences for future journeys that you plan, log in to the site and then tick &#8216;Save these details&#8217; next to the preferences section.&nbsp; The Journey Planner will then apply them to you journeys in future, unless you change them.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.6" name="A2.6"></a>2.6) When I add a via point to my journey why isn''t it always shown in my journey results?</h3>
<p>A via location can be added to your journey by clicking the advanced options button on the journey input page and inputting the location that you would like to go via.  Once you have done this, the journey that Transport Direct produces will always go via your selected point.  However if no change of service is required (e.g. your train stops at the via station but you do not need to get off there) the via point will not be shown in the journey results table which just lists the origin and destination of your trip.  When your chosen journey is by train you can click on the train icon on the results page and Transport Direct will display the train services calling points (including your requested via station).  Via points will be shown in the details of all door-to-door journey plans.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.7" name="A2.7"></a>2.7) How can I plan a day trip to two destinations?</h3>
<p>The &#8216;Day trip planner&#8217; is on the "Plan a journey" page.</p>
<p>&nbsp;</p>
<p>Once you have opened the Day trip planner, enter the location you are starting your day trip from, the first location you would like to visit, the second location you would like to visit (you can enter the length of time you&#8217;d like to stay in these locations) and the date and time of your day trip. </p>
<p>&nbsp;</p>
<p>You will get journey options to choose from so that you can plan your perfect day trip.</p>
<p>&nbsp;</p>
<p>Choose from the drop-down list to view details or maps for the options you select.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.8" name="A2.8"></a>2.8) How do I add a connecting journey to my overall journey?</h3>
<p>Once you have planned a journey you are able to add a connecting journey to the overall journey.&nbsp; To do this, select the journey you are interested in and click the &#8216;Modify journey&#8217; button.</p>
<p>&nbsp;</p>
<p>You ca'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'n choose to add to either end of your original journey.&nbsp; Enter an additional location to join to your original locations.&nbsp; The Journey Planner will search for suitable journey options for this new additional journey.&nbsp; Choose the option which best suits your needs and add this to your original journey.</p><br />
<p>You may add up to two additional journeys.&nbsp; For example, find a train journey, then add shorter journeys to get to and from the stations at each end.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.9" name="A2.9"></a>2.9) If I add a connecting public transport journey to a car/taxi journey, how can I make sure I have enough time to park my car/pay the taxi fare before boarding the train? </h3>
<p>When you add a connecting journey in the Journey Planner, there is the option to specify a &#8216;Stop-over time&#8217;. </p><br />
<p>You should select enough time to find a car park, park your car/pay the taxi fare and get from the car park/taxi rank into the station and board the train. You should also consider the stop-over time on your return journey as well. Select these times from the drop-down lists. The Journey Planner will then search for journey options which take account of this additional time.</p>
<br />
<h3><a class="QuestionLink" id="A2.10" name="A2.10"></a>2.10) How do I change the leaving or arrival times for the start or the end of my journey?</h3>
<ol>
<li>Click the &#8216;Details&#8217; button on the Summary page </li>
<li>Click the &#8216;Amend date/time&#8217; tab at the bottom of the page </li>
<li>Select a new leaving or arrival time </li>
<li>Click on the &#8216;OK&#8217; button.</li></ol>
<h3><a class="QuestionLink" id="A2.11" name="A2.11"></a>2.11) How is The Journey Planner (Powered by Transport Direct) different from other journey planners?</h3>
<p>Transport Direct differs from other journey planners in the following ways:</p>
<p>&nbsp;</p>
<ul class="listerdisc">
<li>It enables you to plan and compare door-to-door public transport or car journeys throughout B'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'ritain whereas other sites only provide information for either public transport <em>or</em> car</li></ul>
<ul class="listerdisc">
<li>It covers the whole of Britain (UK excluding Northern Ireland), not just a specific region</li>
<li>It takes expected traffic levels into account so that you can plan car journeys more realistically</li>
<li>It provides information on a range of transport types (e.g. bus, coach, train...etc.) therefore&nbsp;offering a &#8216;one-stop-shop&#8217; for transport information</li>
<li>It enables you to buy train and coach tickets by providing you with a direct link to travel operator sites where your journey details are automatically transferred</li></ul>
<h3><a class="QuestionLink" id="A2.12" name="A2.12"></a>2.12) How can I find out about the accessibility of the types of transport in the journey?</h3>
    <p>Transport Direct provides three types of accessibility information which may be useful for travellers with disabilities.</p><p>&nbsp;</p>
    <p>
        Links to DPTAC (Disabled Persons Travel Advisory Committee) pages can be found in
        the left hand menu of journey entry pages and from the Notes section at the end
        of your journey results. These contain information relating to general accessibility
        issues or transport mode specific information.</p><p>&nbsp;</p>
    <p>
        Where more specific information on a service is available, a link will be provided
        in the journey details next to the relevant part of your journey.</p><p>&nbsp;</p>
    <p>
        For more information relating to stations or stops click on the name of the station
        you''re interested in from the journey details screen and follow the accessibility
        information links from the station information screen.</p><p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.13" name="A2.13"></a>2.13) Does Transport Direct offer walk planning?</h3>
	<p>Transport Direct returns walks as part of a public transport journey plan. If the origin
		and destination are close together a walk only journey may be returned.</p><p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.14" name="A2.14"></a>2.14) How can I find the detailed directions for a walk?</h3>
	<p>When you have planned a journey that contains a walk leg, the details provided by Transport Direct 
	will include a pre-populated link to walkit.com. If you click this link a new window will open with 
	your origin and destination prefilled and walkit.com will plan your walk. The site will provide you with 
	detailed directions as well as a map for the walk.<br/>
	<a href="/web2/journeyplanning/jplandingpage.aspx?id=FAQLink&oo=p&o=DE1%201DQ&do=p&d=cb2%201st&p=1">Click here to see an example of a Transport Direct door to door journey containing links to Walkit.com</a>
	</p><p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.15" name="A2.15"></a>2.15) In which areas will I see a link to the walkit.com site?</h3>
	<p>When walkit.com add new cities and towns to their site they send the details to Transport Direct and we add the areas to our site. 
	We do this as quickly as possible to provide the best possible service; in 
	most cases the list will be updated within a month. 
	Please visit  <br/>
	<a href="http://www.walkit.com/" target="_child" >http://www.walkit.com/<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/newwindow.gif" alt="Opens in new window"></a> 
	for details of the cities and towns they have visited so far for which they have detailed walk information. </p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.16" name="A2.16"></a>2.16) Is real time information included in my journey plan?</h3>
	<p>At present we are not able to take real time information into account when producing plans for public transport and
	 car journeys, although our rail timetables do take account of most planned changes (such as scheduled engineering work). 
	 For up to date information you can check our live travel news page which has details of current and planned incidents 
	 affecting both public transport and the road network.  This is updated every 15 minutes.</p><p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.17" name="A2.17"></a>2.17) I can''t find the place I want to travel from or to?</h3>
	<p>If you are using one of our quick planners e.g. find a train, find a coach, your origins and destinations will be 
	limited to rail stations and coach stops.  If you cannot find the place you want to travel to or from, try using our door to door planner instead.</p>
	<p>&nbsp;</p><p>Alternatively you can ‘find a map’ of any location in Great Britain and select your origin and destination from 
	the map.  Either click on any of the existing icons for transport points (e.g. bus stops or rail stations) and then 
	follow the link from the information window.  Or select the ‘pushpin’ tool and click on any point of the map to 
	create your own personal origin or destination.</p><p>&nbsp;</p>
<!-- <h3><a class="QuestionLink" id="A2.18" name="A2.18"></a>2.18) How do I plan journeys to destinations outside Great Britain?</h3>
	<p>	Transport Direct plans trunk journeys by air, coach, rail and car between London and a number of cities elsewhere 
	in Europe using the Transport Direct Extra Planner. This is available from a link on the Transport Direct Home page.
	</p><p>&nbsp;</p>
	<p><a href="/Web2/JourneyPlanning/FindInternationalInput.aspx">Go to the Transport Direct Extra planner</a></p> -->
</div></div></div>'

END


-- Update text field: Value-Cy
SET @ptrValText = NULL
SELECT @ptrValText = TEXTPTR([Value-Cy]) FROM [dbo].[tblContent] WHERE themeid = @ThemeId and groupid = 19 and controlname ='Body Text' and propertyname = '/Channels/TransportDirect/Help/HelpJplan'

IF @ptrValText IS NOT NULL BEGIN

   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 ' gludiant yr hoffech deithio arno, dewiswch un o''r cynllunwyr sy''n benodol ar gyfer y math hwnnw o gludiant e.e. Canfyddwch dr&#234;n, Canfyddwch ehediad. <br /><br />Os hoffech gymharu dewisiadau teithio ar gludiant cyhoeddus gyda chyfarwyddiadau mewn car, neu os ydych yn dymuno siwrnai i gyd gyda chludiant cyhoeddus cydlynus, defnyddiwch y cynlluniwr siwrnai o ddrws-i-ddrws. <br /><br />Gall y cynllunwyr sy''n benodol ar gyfer math arbennig o gludiant eich cyfyngu i sut rydych yn disgrifio''r lleoliadau "i" ac "o", e.e. bydd "Canfyddwch dr&#234;n" yn tybio mai''r lle a roddwch yw gorsaf y rheilffordd. <br /><br />Mae''r cynlluniwr siwrnai o ddrws-i-ddrws yn gofyn i chi nodi''r math o leoliad e.e. gorsaf/maes awyr, Tref/rhanbarth/pentref. </p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.2" name="A2.2"></a>2.2) Sut ydw i&#8217;n cynllunio siwrnai yn defnyddio Journey Planner?</h3>
<p><strong>Cam 1:&nbsp; Dewiswch gynlluniwr.</strong></p><br />
<p>Cliciwch ar y tab "Cynlluniwch siwrnai"&nbsp;a</p>
<ol class="numberlist">
<li>Chliciwch ar y math o gludiant yr hoffech deithio arno - tr&#234;n, ehediad, coets, car, o ddrws i ddrws. </li>
<li>Neu defnyddiwch y cynlluniwr siwrnai o ddrws-i-ddrws ar y dudalen gartref.</li></ol>
<p>&nbsp;</p>
<p><strong>Cam 2&nbsp;: Rhowch leoedd</strong> </p>
<p><strong>Os ydych chi wedi dewis Tr&#234;n neu Goets</strong><br /></p>
<div>
<ol class="numberlist">
<li>Yn yr adran &#8216;O&#8217;, teipiwch yr orsaf dr&#234;n neu fws moethus yr hoffech chi ddechrau eich siwrnai ohoni yn y blwch </li>
<li>Yn yr adran &#8216;I&#8217;, teipiwch yr orsaf yr hoffech chi deithio iddi yn y blwch. </li></ol></div>
<p>Gallwch ddefnyddio map i edrych am y gorsafoedd agosaf at leoliad a nodoch drwy glicio ar y botwm &#8216;Dod o hyd i''r... agosaf&#8217;.</p><br />
<p><strong>Os ydych chi wedi dewis Car</strong><br /></p>
<div>
<ol class="numberlist">
<li>Yn yr adran &#8216;O&#8217;, dewiswch y math o leoliad yr ydych chi eisiau ei roi fel lleoliad i ddechrau eich siwrnai ohono (e.e. &#8216'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 ';Tref/rhanbarth/pentref&#8217;, &#8216;Cyfeiriad/c&#244;d post&#8217;&#8230;. a.y.y.b) </li>
<li>Yna teipiwch y lleoliad yr hoffech chi ddechrau eich siwrnai ohono yn y blwch. </li>
<li>Yn yr adran &#8216;I&#8217;, dewiswch y math o leoliad yr ydych chi eisiau ei roi fel cyrchfan (e.e. &#8216;Tref/rhanbarth/pentref&#8217;, &#8216;Cyfeiriad/c&#244;d post&#8217;&#8230;. a.y.y.b) </li>
<li>Yna teipiwch y lleoliad yr hoffech chi deithio iddo yn y blwch. </li></ol></div>
<p></p>
<p>Gallwch ddefnyddio map i&#8217;ch cynorthwyo i ddod o hyd i leoliadau drwy glicio ar y botwm &#8216;Canfyddwch ar y map&#8217;.</p><br />
<p><strong>Os ydych chi wedi dewis Ehediad</strong><br /></p>
<div>
<ol class="numberlist">
<li>Yn yr adran &#8216;O&#8217;, dewiswch naill ai&#8217;r rhanbarth neu&#8217;r maes awyr yr hoffech chi deithio ohono.&nbsp; Os ydych chi wedi dewis rhanbarth, byddwch yn gweld y meysydd awyr yn y rhanbarth hwnnw yn ymddangos islaw&#8217;r rhanbarth.&nbsp;Byddant i gyd wedi&#8217;u ticio, felly os yw''r rhestr yn cynnwys unrhyw feysydd awyr nad ydych yn dymuno hedfan ohonynt, dad-diciwch nhw cyn parhau. </li>
<li>Ceir blwch ticio ar gyfer ehediadau uniongyrchol yn unig yn yr adran nesaf. Os ydych chi ond yn fodlon teithio&#8217;n uniongyrchol i&#8217;r cyrchfan, gadewch y blwch hwnnw wedi&#8217;i dicio.&nbsp; Os nad os gwahaniaeth gennych aros yn rhywle ar eich siwrnai i&#8217;ch cyrchfan, dad-diciwch y blwch. </li>
<li>Yn yr adran &#8216;I&#8217;, dewiswch naill ai y rhanbarth neu&#8217;r maes awyr yr hoffech chi hedfan iddo.&nbsp; Eto, os ydych chi wedi dewis rhanbarth, byddwch yn gweld y meysydd awyr yn y rhanbarth hwnnw yn ymddangos islaw&#8217;r rhanbarth a gallwch dad-dicio unrhyw rai nad ydych chi&#8217;n dymuno hedfan iddynt. </li></ol></div>
<p></p>
<p>Gallwch ddefnyddio map i&#8217;ch cynorthwyo i ddod o hyd i feysydd awyr ger lleoliad drwy glicio&#8217;r botwm &#8216;Dod o hyd i''r ... agosaf&#8217;. </p>
<br />
<p><strong>Cam 3: Dewiswch ddyddiadau ac amserau</strong'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 '></p>
<p><strong></strong>&nbsp;</p>
<div>
<ol class="numberlist">
<li>Defnyddiwch y gwymplen i ddewis dyddiadau ac amserau eich siwrnai.</li>
<li>Gallwch ddewis naill ai <strong>gadael ar</strong> neu <strong>gyrraedd erbyn</strong> y dyddiad/amser y nodwch.</li>
<li>Os ydych chi&#8217;n cynllunio siwrnai ddychwelyd, dewiswch y dyddiad a&#8217;r amser yr hoffech chi ddychwelyd o&#8217;r cwymplenni.</li>
<li>Os nad ydych chi&#8217;n siwr ynglyn &#226;&#8217;r dyddiad yr hoffech chi ddychwelyd, gallwch ddewis &#8216;Tocyn dychwel agored&#8217;. Os mai dim ond siwrnai allan/sengl yr ydych chi ei heisiau, gadewch y blychau hyn wedi&#8217;u gosod ar &#8216;Dim dychwelyd&#8217;.</li></ol></div>
<p>&nbsp;</p>
<p><strong></strong>&nbsp;</p>
<p><strong>Cam 4: Dewisol&nbsp;&#8211; Cliciwch ar y botwm &#8216;Dewisiadau mwy cymhleth&#8217; i ychwanegu mwy o fanylion i&#8217;ch cais</strong></p>
<p>&nbsp;</p>
<div>
<ol class="numberlist">
<li>Os hoffech chi nodi rhai manylion ar gyfer y math o drafnidiaeth neu leoliad teithio trwyddo, gallwch nodi hynny yn yr adran hon. </li></ol></div>
<p><strong></strong>&nbsp;</p>
<p><strong>Cam 5: Cliciwch &#8216;Nesaf&#8217;</strong></p>
<p><strong></strong>&nbsp;</p>
<ol class="numberlist">
<li>Wedi i chi lenwi&#8217;r blychau i gyd ar y dudalen hon, cliciwch &#8216;Nesaf&#8217;.</li></ol>
<p><strong></strong>&nbsp;</p>
<p><strong>Cam 6: Cadarnhewch eich manylion </strong></p>
<p><strong></strong>&nbsp;</p>
<p>Os nad yw&#8217;r Cynlluniwr Siwrnai yn gallu dod o hyd i ganlyniad sy&#8217;n cyd-fynd yn union &#226;&#8217;r lleoliadau/gorsafoedd a nodoch, mae&#8217;n bosibl y bydd tudalen &#8216;Gadarnhau&#8217; yn agor wedi i chi glicio &#8216;Nesaf&#8217;.&nbsp;Bydd cwymplen (wedi&#8217;i amlygu mewn melyn) yn dangos un neu&#8217;n fwy o ganlyniadau tebyg ar gyfer y lleoliad hwnnw.</p>
<p>&nbsp;</p>
<p>Yn dibynnu ar yr wybodaeth y gwnaethoch chi'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 ' ei rhoi, bydd yn rhaid i chi wneud <strong>un</strong> o&#8217;r canlynol:</p>
<p>&nbsp;</p>
<ul class="lister">
<li>Dewiswch opsiwn lleoliad o&#8217;r gwymplen. </li></ul>
<ul class="lister">
<li>Edrychwch am y lleoliad fel gwahanol fath o leoliad (e.e. &#8220;Cyfeiriad/c&#244;d post")</li></ul>
<p>&nbsp;</p>
<p>Hefyd, os oes unrhyw broblemau gyda&#8217;r dyddiad, amser neu hoffterau/manylion teithio yr ydych chi wedi&#8217;u rhoi, bydd y rhain yn ymddangos yn goch gyda neges yn esbonio&#8217;r hyn sy&#8217;n rhaid i chi wneud.</p>
<p>&nbsp;</p>
<p>Wedi i chi gadarnhau&#8217;r holl adrannau sydd wedi&#8217;u goleuo ar y dudalen hon, cliciwch &#8216;Nesaf&#8217;.</p>
<p>&nbsp;</p>
<p>Nodwch, mae gan rai leoliadau, lleoliadau pellach o&#8217;u mewn: </p>
<p>&nbsp;</p>
<ul class="lister">
<li>Mae gan rai y geiriau &#8220;Mwy o opsiynau ar gyfer&#8230;&#8221; wedi&#8217;u hysgrifennu o&#8217;u blaen</li></ul>
<ul class="lister">
<li>Os ydych chi&#8217;n dewis un o&#8217;r rhain ac yn clicio &#8216;Nesaf&#8217;, rhoddir rhestr i chi o&#8217;r holl leoliadau sy&#8217;n bodoli o fewn y lleoliad cyntaf </li>
<li>Dewiswch un o&#8217;r rhain a chliciwch &#8216;Nesaf&#8217; </li>
<li>Fel arall, os wnewch chi glicio ar &#8216;Yn &#244;l&#8217;, bydd y rhestr gyntaf o leoliadau yn ymddangos eto.</li></ul>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.3" name="A2.3"></a>2.3) Beth os nad ydw i&#8217;n gwybod yr union gyfeiriad(au) y lle yr ydw i eisiau teithio ohono neu iddo? </h3>
<p>Pan ddefnyddiwch y Cynlluniwr Siwrnai drws-i-ddrws gallwch ddewis rhoi lleoliadau nad ydynt yn gyfeiriadau llawn, fel enwau trefi, codau post neu enwau gorsafoedd.&nbsp; Gallwch hefyd roi enwau atyniadau neu gyfleusterau, fel enwau gwestai.</p>
<p>&nbsp;</p>
<p>Gallwch hefyd ddefnyddio map i ddarganfod yr union leoliad y dymunwch deithio iddo neu ohono.</p>
<p>&nbsp;</p>
<p><strong>Enghraifft 1</strong></p>
<p><strong>Os hoffech deithio i gyfeiriad nad ydych yn gwybod y c&#244;d post ar ei g'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 'yfer:</strong></p>
<ol class="numberlist">
<li>Mae "cyfeiriad/c&#244;d post" wedi ei ddewis yn barod, felly nid oes angen dewis categori arall. </li>
<li>Teipiwch gymaint o''r cyfeiriad ag a wyddoch (e.e. 123 Victoria Street, Llundain).</li></ol>
<p><strong>Enghraifft 2</strong></p>
<p><strong>Rydych yn gwybod c&#244;d post y lleoliad:</strong></p>
<ol class="numberlist">
<li>Mae "Cyfeiriad/c&#244;d post" wedi ei ddewis yn barod, felly does dim angen dewis categori arall. </li>
<li>Teipiwch y c&#244;d post yn llawn (e.e. &#8220;EN4 7QQ&#8221;) neu ran o&#8217;r cod post (e.e. &#8220;SE1&#8221;).</li></ol>
<p><strong>Enghraifft 3</strong></p>
<p><strong>Rydych yn meddwl eich bod yn gwybod am brif orsaf ger ble yr hoffech deithio ohono:</strong></p>
<ol class="numberlist">
<li>Dewiswch &#8216;Gorsaf/maes awyr&#8217;. </li>
<li>Teipiwch enw&#8217;r orsaf yn y blwch (e.e. &#8220;Waterloo&#8221;).</li></ol>
<p><strong>Enghraifft 4</strong></p>
<p><strong>Gyda siwrnai o ddrws-i-ddrws, os credwch eich bod yn gwybod am arhosfan bws ger ble yr hoffech deithio ohono:</strong></p>
<ol class="numberlist">
<li>Dewiswch &#8216;Pob arhosfan&#8217;. </li>
<li>Teipiwch g&#244;d SMS yr arhosfan os ydych yn ei wybod, neu''r lle y lleolir yr arhosfan (e.e. London Road, Buckingham), yn y blwch.</li></ol>
<p><strong>Enghraifft 5</strong></p>
<p><strong>Rydych yn dymuno gwybod sut i fynd i ardal gyffredinol, fel tref neu faestref, yn hytrach na lleoliad penodol:</strong></p>
<ol class="numberlist">
<li>Dewiswch "Tref/rhanbarth/pentref". </li>
<li>Teipiwch, er enghraifft, &#8220;Manceinion&#8221; (dinas), neu &#8220;Stretford&#8221; (maestref o Fanceinion) neu &#8220;Bollington&#8221; (pentref ger Manceinion) yn y blwch.</li></ol>
<p><strong>Enghraifft 6</strong></p>
<p><strong>Rydych am deithio i atyniad neu gyfleuster, ond dydych chi ddim yn gwybod y cyfeiriad:</strong></p>
<ol>
<li>Dewiswch "Cyfleuster/atyniad". </li>
<li>Teipiwch enw atyniad neu gyfleuster (e.e. "Llyfrgell Brydeinig'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 '" neu "Gastell Caeredin") yn y blwch.<br /><strong></strong></li></ol>
<p><strong>Enghraifft 7</strong></p>
<p><strong>Dydych chi ddim yn siwr o&#8217;r union leoliad yr ydych am deithio iddo, ond rydych yn gwybod pa un yw&#8217;r ardal gyffredinol y mae ynddo.&nbsp; Yn yr achos hwn, gallwch ddefnyddio map i&#8217;ch helpu i ddewis y lleoliad cywir:</strong></p>
<ol class="numberlist">
<li>Dewiswch y math o leoliad e.e. "Tref/rhanbarth/pentref" yn yr adran "I". </li>
<li>Teipiwch y lleoliad (e.e. &#8220;Caeredin&#8221;) . </li>
<li>Cliciwch y botwm &#8216;Canfyddwch ar y map&#8217; ger ble rydych wedi teipio&#8217;r lleoliad. </li>
<li>Os oes angen, cadarnhewch y lleoliad yr ydych wedi ei roi drwy ddewis opsiwn o&#8217;r rhestr a ollyngir i lawr (a amlygir mewn melyn) a chliciwch ar &#8216;Nesaf. </li>
<li>Defnyddiwch offer y map i ddewis yr union leoliad y dymunwch deithio iddo. </li>
<li>Wedi i chi ddewis ble dymunwch deithio iddo, cliciwch &#8216;Nesaf&#8217;.</li></ol><br />
<h3><a class="QuestionLink" id="A2.4" name="A2.4"></a>2.4) Sut ydw i''n dod o hyd i orsaf neu faes awyr?</h3>
<p><strong>Cam 1: Agorwch "Canfyddwch le"</strong></p>
<ul class="lister">
<li>Cliciwch ar "Dod o hyd i''r gorsafoedd a''r meysydd awyr agosaf".</li></ul>
<p><strong></strong>&nbsp;</p>
<p><strong>Cam 2: Dewiswch y math o orsafoedd yr hoffech chi edrych amdanynt</strong></p>
<ul class="lister">
<li>Ticiwch unrhyw un o''r mathau o orsafoedd y mae gennych chi ddiddordeb ynddynt.</li></ul>
<p><strong></strong>&nbsp;</p>
<p><strong>Cam 3: Dewiswch y math o leoliad mae''r orsaf ar ei bwys</strong></p>
<ul class="lister">
<li>Dewiswch y math o leoliad yr ydych chi eisiau ei deipio (e.e. "Tref/rhanbarth/pentref", "Cyfeiriad/c&#244;d post", "Cyfleuster/atyniad" ...a.y.y.b)</li></ul>
<ul class="lister">
<li>Teipiwch enw''r lleoliad yn y blwch.</li></ul>
<p><strong></strong>&nbsp;</p>
<p><strong>Cam 4: Cliciwch "Nesaf"</strong></p>
<p>&nbsp;</p>
<p><strong>Cam 5: Cadarnhau eich lleoliad</strong></p>
<p>Wedi i chi g'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 'licio "Nesaf", os nad yw Journey Planner yn gallu dod o hyd i ganlyniad sy''n cyd-fynd yn union &#226;"r lleoliad y gwnaethoch chi ei deipio, bydd cwymplen (wedi''i goleuo mewn melyn) yn dangos un neu fwy o ganlyniadau tebyg sy''n cyd-fynd &#226;"r lleoliad hwnnw.</p>
<p>&nbsp;</p>
<p>Yn dibynnu ar yr wybodaeth y gwnaethoch chi ei rhoi, bydd angen i chi wneud <strong>un</strong> o''r canlynol: </p>
<ul class="lister">
<li>Dewis opsiwn lleoliad o''r gwymplen. Nodwch os yw''r lleoliad yr ydych chi ei eisiau eisoes yn y blwch melyn, nid oes yn rhaid i chi ei ailddewis </li>
<li>Edrychwch am y lleoliad fel gwahanol fath o leoliad (e.e. "Cyfeiriad/c&#244;d post") </li>
<li>Rhowch leoliad newydd.</li></ul><br />
<p>Wedi i''ch gwybodaeth gael ei chadarnhau, bydd gorsafoedd ger y lleoliad hwnnw yn cael eu rhestru ar y dudalen. Gallwch glicio "Dangos Map" i weld map gyda''r gorsaf(oedd) arno. </p>
<p>&nbsp;</p>
<p><strong>Cam 6: Os hoffech chi gynllunio siwrnai wedi i chi ddod o hyd i''r orsaf yr ydych chi''n edrych amdani, gwnewch yn si?r ei bod wedi''i thicio a chliciwch ar "Teithio o" neu "Teithio i". (dewisol)</strong></p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.5" name="A2.5"></a>2.5) Sut y gallaf ychwanegu manylion penodol neu osod hoffterau teithio ar gyfer fy siwrneion?</h3>
<p>1. Cliciwch ar "Dewisiadau mwy cymhleth" ar waelod y dudalen mewnbynnu.</p>
<p>&nbsp;</p>
<p>2. Rhowch unrhyw hoffterau yr ydych chi&#8217;n dymuno, er enghraifft:</p>
<ul>
<li>Pa mor gyflym y gallwch wneud newidiadau mewn arosfannau a gorsafoedd </li>
<li>Y nifer o newidiadau sydd orau gennych </li>
<li>Y cyflymdra cerdded sydd orau gennych </li>
<li>Y cyflymdra gyrru uchaf sydd orau gennych </li>
<li>Ble byddech yn hoffi teithio drwyddo</li></ul>
<p>&nbsp;</p>
<p>Os hoffech chi gadw&#8217;r hoffterau yma ar gyfer siwrneion y dyfodol yr ydych chi&#8217;n eu cynllunio, mewngofnodwch i&#8217;r safle ac yna ticiwch &#8216;Cadwch y manylion hyn&#8217; sydd wrth ymyl yr adran hoffterau. Yna bydd Transport Direc'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 't yn eu defnyddio ar gyfer eich siwrneion yn y dyfodol, heblaw eich bod yn eu newid.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.6" name="A2.6"></a>2.6) When I add a via point to my journey why isn''t it always shown in my journey results?</h3>
<p>A via location can be added to your journey by clicking the advanced options button on the journey input page and inputting the location that you would like to go via.  Once you have done this, the journey that Transport Direct produces will always go via your selected point.  However if no change of service is required (e.g. your train stops at the via station but you do not need to get off there) the via point will not be shown in the journey results table which just lists the origin and destination of your trip.  When your chosen journey is by train you can click on the train icon on the results page and Transport Direct will display the train services calling points (including your requested via station).  Via points will be shown in the details of all door-to-door journey plans.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.7" name="A2.7"></a>2.7) Sut y gallaf gynllunio taith dydd i ddau gyrchfan?</h3>
<p>Mae''r "Cynllunydd teithiau dydd" ar y dudalen "Cynlluniwch siwrnai".</p>
<p>&nbsp;</p>
<p>Wedi i chi agor y Cynllunydd teithiau dydd, rhowch y lleoliad yr ydych yn dechrau eich taith dydd ohono, y lleoliad cyntaf yr hoffech ymweld ag ef, yr ail leoliad yr hoffech ymweld ag ef (gallwch roi hyd yr amser yr hoffech aros yn y lleoliadau hyn) a dyddiad ac amser eich taith dydd. </p>
<p>&nbsp;</p>
<p>Fe gewch ddewisiadau o ran siwrneion i ddewis ohonynt fel y gallwch gynllunio eich taith dydd berffaith.</p>
<p>&nbsp;</p>
<p>Dewiswch o''r rhestr a ollyngir i lawr i weld manylion neu fapiau ar gyfer y dewisiadau yr ydych yn eu dewis.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.8" name="A2.8"></a>2.8) Sut y gallaf ychwanegu siwrnai gysylltiol at fy siwrnai gyffredinol? </h3>
<p>Wedi i chi gynllunio siwrnai ar Journey Planner rydych yn gallu ychwanegu siwrnai gysylltiol at y siwrnai gyffredinol.&nbsp; I wneud hyn, dewiswch y siwrnai y mae gennych ddiddordeb ynddi a chliciwch ar fotwm "Diwygiwch y siwrnai hon".</p>
<p>&nbsp;</p>
<p>Gallwch ddewis ychwanegu at y naill ben neu''r llall o''ch siwrnai wreiddiol.&nbsp; Rhowch leoliad ychwanegol i ymuno &#226;"ch lleoliadau gwreiddiol.&nbsp; Bydd Journey Planner yn chwilio am ddewisiadau siwrnai addas ar gyfer y siwrnai ychwanegol newydd hon.&nbsp; Dewiswch y dewis sy''n gweddu orau i''ch anghenion ac ychwanegwch hwn at eich siwrnai wreiddiol.</p><br />
<p>Gallwch ychwanegu hyd at ddwy siwrnai ychwanegol.&nbsp; Er enghraifft, darganfyddwch siwrnai tr&#234;n, yna ychwanegwch siwrneion byrrach i fynd i ac o''r gorsafoedd ym mhob pen.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.9" name="A2.9"></a>2.9) Pe byddwn yn ychwanegu siwrnai trafnidiaeth gyhoeddus gysylltiol at siwrnai gar/tacsi, sut y g'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 'allaf sicrhau bod gennyf ddigon o amser i barcio fy nghar/talu am y tacsi cyn mynd ar y tr&#234;n? </h3>
<p>Pan fyddwch yn ychwanegu siwrnai gysylltiol yn Journey Planner, mae''r opsiwn gennych i nodi "Amser aros drosodd". </p>
<p>Dylech ddewis digon o amser i ddod o hyd i faes parcio, parcio eich car/talu am y tacsi, a mynd o''r maes parcio/safle tacsi i mewn i''r orsaf ac ar y tr&#234;n. Dylech hefyd ystyried yr amser aros drosodd ar eich siwrnai ddychwelyd. Dewiswch yr amseroedd hyn o''r rhestri cwympo. Yna bydd Journey Planner yn chwilio am opsiynau siwrnai sy''n ystyried yr amser ychwanegol hwn.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.10" name="A2.10"></a>2.10) Sut ydw i''n newid yr amserau gadael neu gyrraedd ar gyfer dechrau neu ddiwedd fy siwrnai?</h3>
<ol>
<li>Cliciwch ar y botwm "Manylion" ar y dudalen Grynodeb </li>
<li>Cliciwch ar y tab "Newid dyddiad/amser" ar waelod y dudalen </li>
<li>Dewiswch amser gadael neu gyrraedd newydd </li>
<li>Cliciwch ar y botwm "Iawn".</li></ol>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.11" name="A2.11"></a>2.11) Sut mae Journey Planner yn wahanol i gynllunwyr siwrneion eraill?</h3>
<p>Mae Journey Planner yn wahanol i gynllunwyr siwrneion eraill yn y ffyrdd canlynol:</p>
<p>&nbsp;</p>
<ul class="lister">
<li>Mae&#8217;n eich galluogi i gynllunio a chymharu cludiant cyhoeddus o ddrws i ddrws neu deithiau mewn ceir ar gyfer pob rhan o Brydain pan fo safleoedd eraill yn rhoi gwybodaeth naill ai ar gyfer cludiant cyhoeddus <em>neu</em> gar yn unig.</li></ul>
<ul class="lister">
<li>Mae&#8217;n darparu gwybodaeth ar gyfer Prydain gyfan (y DG heb gynnwys Gogledd Iwerddon) nid dim ond rhanbarth penodol</li></ul>
<ul class="lister">
<li>Mae&#8217;n cymryd lefelau trafnidiaeth ddisgwyliedig i ystyriaeth fel y gallwch gynllunio siwrneion car yn fwy realistig.</li></ul>
<ul class="lister">
<li>Mae&#8217;n rhoi gwybodaeth am amrediad o fathau o gludiant (e.e. bws, bws moethus, tr&#234;n ... ac ati) ac felly yn cynnig "siop'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 ' un alwad" ar gyfer gwybodaeth am gludiant.</li></ul>
<ul class="lister">
<li>Mae''n eich galluogi i brynu tocynnau tr&#234;n a bws moethus drwy roi cyswllt uniongyrchol i chi &#226; safleoedd gweithredwyr teithio lle caiff manylion eich siwrnai eu trosglwyddo yn awtomatig.</li></ul>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.12" name="A2.12"></a>2.12) Sut y gallaf ddarganfod am hygyrchedd y mathau o gludiant yn y siwrnai? </h3>
    <p>Mae Transport Direct yn rhoi tri math o wybodaeth am hygyrchedd a allai fod yn ddefnyddiol i deithwyr ag anableddau.</p><p>&nbsp;</p>
    <p>
        Gellir gweld dolenni i dudalennau DPTAC (Pwyllgor Ymgynghorol Teithio i Bobl Anabl) yn y ddewislen ar ochr chwith y tudalennau nodi siwrnai ac o''r adran Nodiadau ar ddiwedd eich canlyniadau siwrnai. Mae''r rhain yn cynnwys gwybodaeth ynghylch materion hygyrchedd cyffredinol neu wybodaeth benodol am ddull cludiant.</p><p>&nbsp;</p>
    <p>
        Os oes gwybodaeth fwy penodol ar gael am wasanaeth, bydd dolen yn cael ei darparu ym manylion y siwrnai wrth ymyl y rhan berthnasol o''ch siwrnai.</p><p>&nbsp;</p>
    <p>
        Am fwy o wybodaeth am orsafoedd neu arosfannau cliciwch ar enw''r orsaf y mae gennych ddiddordeb ynddi o''r sgrin manylion siwrnai a dilynwch y dolenni gwybodaeth hygyrchedd o sgrin wybodaeth yr orsaf.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.13" name="A2.13"></a>2.13) A yw Transport Direct yn cynnig cynlluniau cerdded?</h3>
	<p>Mae Transport Direct yn sicrhau eich cludo yn ôl o''ch taith gerdded fel rhan o gynllun taith cludiant cyhoeddus. 
	Os yw''r man cychwyn a''r gyrchfan yn agos at ei gilydd gellir cerdded y ddwy ffordd.</p><p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.14" name="A2.14"></a>2.14) Sut allaf i ddarganfod cyfarwyddiadau manwl ar gyfer taith gerdded?</h3>
	<p>Pan fydd rhan o daith fyddwch wedi ei chynllunio yn cynnwys cerdded, bydd y manylion a roddir gan Transport Direct yn cynnwys cyswllt rhag-boblogi â walkit.com. 
	Os byddwch yn clicio ar y cyswllt hwn bydd ffenestr newydd yn agor lle bydd eich man cychwyn a''ch cyrchfan wedi eu cwblhau eisoes a bydd walkit.com yn cynllunio eich taith gerdded. 
	Bydd y safle yn rhoi cyfarwyddiadau manwl ichi yn ogystal â map ar gyfer y daith gerdded.<br/>
	<a href="/web2/journeyplanning/jplandingpage.aspx?id=FAQLink&oo=p&o=DE1%201DQ&do=p&d=cb2%201st&p=1">Cliciwch yma i weld enghraifft o daith drws-i-ddrws Transport Direct sy''n cynnwys cysylltiadau â Walkit.com</a>
	</p><p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.15" name="A2.15"></a>2.15) Ym mha ardaloedd allaf i ddarganfod manylion ar gyfer taith gerdded?</h3>
	<p>Pan fydd walkit.com yn ychwanegu trefi a dinasoedd newydd at ei safle mae''n anfon y manylion i Transport Direct ac rydym yn ychwanegu''r ardaloedd at ein safle. Rydym yn gwneud hyn mor gyflym ag sydd bosibl i roi''r 
	gwasanaeth gorau posibl; gan amlaf bydd y rhestr yn cael ei diweddaru o fewn mis. Ewch i  
	<a href="http://www.walkit.com/" target="_child" >http://www.walkit.com/<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/newwindow.gif" alt="Opens in new window"></a> 
	i gael manylion am y trefi a''r dinasoedd yr ymwelwyd â hwy hyd yma ac y ceir gwybodaeth fanwl ar eu cyfer o safbwynt teithiau cerdded. </p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.16" name="A2.16"></a>2.16) A yw gwybodaeth amser real yn cael ei chynnwys yng nghynllun fy siwrnai?</h3>
	<p>Ar hyn o bryd ni allwn roi ystyriaeth i wybodaeth amser real wrth lunio cynlluniau ar gyfer cludiant 
	cyhoeddus a siwrneiau mewn ceir, er bod ein hamserlenni trên yn cymryd y rhan fwyaf o newidiadau 
	cynlluniedig i ystyriaeth (megis gwaith peirianneg trefnedig). I gael yr wybodaeth ddiweddaraf gallwch 
	fynd i''n tudalen newyddion teithio fyw lle ceir manylion am ddigwyddiadau cyfredol ac arfaethedig sy''n 
	effeithio ar gludiant cyhoeddus ac ar rwydwaith y ffyrdd.  Bydd hon yn cael ei diweddaru bob 15 munud.</p>
	<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A2.17" name="A2.17"></a>2.17) Ni allaf ddarganfod y lle yr wyf eisiau teithio ohono/iddo?</h3>
	<p>Os ydych yn defnyddio un o''n cynllunwyr cyflym e.e. darganfod trên, darganfod bws foethus, bydd eich 
	mannau cychwyn a''ch cyrchfannau yn cael eu cyfyngu i orsafoedd rheilffordd ac arosfannau bysiau moethus. 
	Os na allwch ddarganfod y lle yr ydych eisiau teithio iddo neu ohono, ceisiwch ddefnyddio ein cynllunydd 
	drws-i-ddrws.</p>
	<p>&nbsp;</p>
	<p>Fel arall gallwch ‘ddarganfod map’ o unrhyw leoliad ym Mhrydain Fawr a dewis eich man cychwyn a''ch 
	cyrchfan o''r map. Un ai cliciwch ar unrhyw un o''r eiconau sy’n bodoli ar gyfer pwyntiau cludiant (e.e. 
	arosfannau bysiau neu orsafoedd rheilffordd) ac yna dilynwch y cyswllt o''r ffenestr wybodaeth. Neu dewiswch 
	y teclyn ‘pin bawd’ a chliciwch ar unrhyw bwynt ar y map i lunio eich man cychwyn neu gyrchfan arbennig eich 
	hun.</p>
	<p>&nbsp;</p>
<!-- <h3><a class="QuestionLink" id="A2.18" name="A2.18"></a>2.18) How do I plan journeys to destinations outside Great Britain?</h3>
	<p>	Transport Direct plans trunk journeys by air, coach, rail and car between London and a number of cities elsewhere 
	in Europe using the Transport Direct Extra Planner. This is available from a link on the Transport Direct Home page.
	</p><p>&nbsp;</p>
	<p><a href="/Web2/JourneyPlanning/FindInternationalInput.aspx">Go to the Transport Direct Extra planner</a></p> -->
</div></div>'

END



-------------------------------------------------------------
-- FAQs - LiveTravel
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpLiveTravel', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="LiveTravel" name="LiveTravel"></a><h2>9. Live travel</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.1">9.1) Can I get up-to-date travel news in my region?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.2">9.2) How often is "Live travel" information updated?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.3">9.3) How can I see a live departure board?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.4">9.4) How can I see if there is any travel news impacting my road journey?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>9. Live travel</h2></div>
<br />
<h3><a class="QuestionLink" id="A9.1" name="A9.1"></a>9.1)&nbsp;Can I get up-to-date travel news in my region?</h3>
<p>Yes, you can get up-to-date travel news for all regions in Britain on the &#8216;Live travel news&#8217; page. </p>
<p>&nbsp;</p>
<p>You can get to the Live Travel news page in these ways:</p>
<p>&nbsp;</p>
<ul class="listerdisc">
<li>Open the &#8216;Live travel&#8217; tab </li>
<li>Click on a live travel incident on the homepage (JavaScript required) </li>
<li>Click on &#8216;Live travel&#8217; from the left hand navigation on the homepage</li></ul>
<p>&nbsp;</p>
<p>The Live travel page can advise you of events that can affect or delay journeys, such as traffic accidents, severe weather warnings or emergency road works.&nbsp; </p>
<p>&nbsp;</p>
<p>You can choose to view only the information that interests you by selecting options from the drop-down lists provided.&nbsp; You can view the information by:</p>
<p>&nbsp;</p>
<ul class="listerdisc">
<li>Type of transport </li>
<li>Region </li>
<li>Delay severity e.g. &#8216;Major delays&#8217;, or &#8216;All delays&#8217;, or "Recent delays"</li></ul>
<p>&nbsp;</p>
<p>You can also view train departures and arrivals (for a station) and live bus information.&nbsp; </p>
<ol class="numberlist">
<li>Click the &#8216;Departure boards&#8217; link. </li> 
<li>Click either a bus information link or the national rail link.&nbsp; This will open a new browser window. </li>
<li>Follow the instructions given on that site. </li>
<li>Once you have finished viewing the departure board information, you may return to this Journey Planner site by closing the browser window.</li></ol><br />
<br />
<h3><a class="QuestionLink" id="A9.2" name="A9.2"></a>9.2)&nbsp;How often is "Live travel" information updated?</h3>
<ul class="lister">
<li>&#8216;Live travel&#8217; news information is updated every 15 minutes on the Journey Planner </li>
<li>&#8216;Departure boards&#8217; information is updated at the discretion of the specific site owners.<br /></li></ul>
<p>&nbsp;</p>
<br />
<h3><a class="QuestionLink" id="A9.3" name="A9.3"></a>9.3)&nbsp;How can I see a live departure board?</h3>
<p>Open the "Live travel" tab. </p>
<p>&nbsp;</p>
<p>Click the &#8216;Departure boards&#8217; icon or link. </p>
<div>
<ol>
<li>Click any of the information links to open a new browser window. </li> 
<li>Follow the instructions&nbsp;given on that site. </li> 
<li>Once you have finished viewing the departure board information, you may return to the Journey Planner site by closing the browser window.</li></ol></div>
<p>&nbsp;</p>
<br />
<h3><a class="QuestionLink" id="A9.4" name="A9.4"></a>9.4)&nbsp;How can I see if there is any travel news impacting my road journey?</h3>
<p>Transport Direct will match travel news reports against the road journey you have planned and allow you to see any incidents on your route in the road journey details.
Additionally the map of a road journey will allow you to see any travel news items that are planned to occur on your day of travel.</p>
<p>&nbsp;</p></div></div>
<div></div>
<div></div>
<div></div>
<div></div>', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="LiveTravel" name="LiveTravel"></a><h2>9. Teithio byw</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.1">9.1) Fedra&#8217; i gael y newyddion diweddaraf am deithio yn fy rhanbarth i?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.2">9.2) Pa mor aml y mae gwybodaeth "Teithio Byw" yn cael ei diweddaru?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.3">9.3) Sut allaf i weld bwrdd ymadael byw?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpLiveTravel.aspx#A9.4">9.4) Sut allaf fi weld a oes unrhyw newddion teithio yn effeithio ar fy nhaith ar y ffordd?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>9. Teithio byw</h2></div>
<br />
<h3><a class="QuestionLink" id="A9.1" name="A9.1"></a>9.1)&nbsp;Fedra&#8217; i gael y newyddion diweddaraf am deithio yn fy rhanbarth i?</h3>
<p>Gallwch, gallwch gael y newyddion teithio diweddaraf ar gyfer pob rhanbarth ym Mhrydain ar y dudalen &#8216;Newyddion teithio byw&#8217;. </p>
<p>&nbsp;</p>
<p>Gallwch fynd i dudalen "Newyddion teithio byw" fel a ganlyn:</p>
<p>&nbsp;</p>
<ul class="lister">
<li>Agorwch y tab "Teithio byw" </li>
<li>Cliciwch ar ddigwyddiad teithio byw ar y dudalen gartref (mae angen javascript) </li>
<li>Cliciwch ar "Teithio byw" o''r cyfarwyddiadau ar y llaw chwith ar y dudalen gartref.</li></ul>
<p>&nbsp;</p>
<p>Gall y dudalen Teithio byw eich cynghori am ddigwyddiadau a all effeithio ar neu oedi siwrneion, fel damweiniau traffig, rhybuddion tywydd difrifol neu waith brys ar y ffyrdd.&nbsp; </p>
<p>&nbsp;</p>
<p>Gallwch ddewis gweld yr wybodaeth sydd o ddiddordeb i chi yn unig drwy ddewis opsiynau o''r rhestrau a ollyngir i lawr a ddarperir.&nbsp; Gallwch weld yr wybodaeth yn &#244;l:</p>
<p>&nbsp;</p>
<ul class="lister">
<li>Math o gludiant </li>
<li>Rhanbarth </li>
<li>Difrifoldeb yr oedi, e.e. "Oedi mawr" neu "Pob oedi" neu "Oediadau diweddar".</li></ul>
<p>&nbsp;</p>
<p>Gallwch weld amserau y mae trenau yn ymadael ac yn cyrraedd (ar gyfer gorsaf) a gwybodaeth fyw am fysiau. </p>
<ol class="numberlist">
<li>Cliciwch y ddolen "Byrddau cyrraedd a chychwyn" </li>
<li>Cliciwch naill ai''r ddolen gwybodaeth bysiau neu''r ddolen &#226;"r rheilffyrdd cenedlaethol. Bydd hon yn agor ffenestr porwr newydd </li>
<li>Dilynwch y cyfarwyddiadau a roddir ar y safle hwnnw </li>
<li>Wedi i chi orffen edrych ar yr wybodaeth ar y bwrdd ymadael, gallwch ddychwelyd i safle Journey Planner drwy gau ffenestr y porwr.</li></ol>
<h3><a class="QuestionLink" id="A9.2" name="A9.2"></a>9.2)&nbsp;Pa mor aml y mae gwybodaeth "Teithio Byw" yn cael ei diweddaru?</h3>
<ul class="lister">
<li>Diweddarir gwybodaeth newyddion "Teithio byw" bob 15 munud ar Journey Planner </li>
<li>Diweddarir gwybodaeth "Byrddau ymadael" yn &#244;l dewis perchnogion y safleoedd penodol</li></ul>
<br />
<h3><a class="QuestionLink" id="A9.3" name="A9.3"></a>9.3)&nbsp;Sut allaf i weld bwrdd ymadael byw?</h3>
<p>Agorwch y tab &#8216;Teithio byw&#8217;.</p>
<p>&nbsp;</p>
<p>Cliciwch ar yr eicon neu''r ddolen &#8216;Byrddau cyrraedd a chychwyn&#8217;</p>
<div>
<ol>
<li>Cliciwch ar naill ai&#8217;r ddolen gwybodaeth fws neu&#8217;r ddolen rheilffordd genedlaethol. Bydd hyn yn agor ffenestr porwr newydd. </li>
<li>Dilynwch y cyfarwyddiadau a roddir ar y safle hwnnw. </li>
<li>Wedi i chi orffen edrych ar wybodaeth y bwrdd ymadael, gallwch ddychwelyd at safle Journey Planner drwy gau ffenestr y porwr.</li></ol></div>
<br />
<h3><a class="QuestionLink" id="A9.4" name="A9.4"></a>9.4)&nbsp;Sut allaf fi weld a oes unrhyw newddion teithio yn effeithio ar fy nhaith ar y ffordd?</h3>
<p><p>Bydd Transport Direct yn cymharu newyddion teithio â''r daith yr ydych wedi ei chynllunio ac yn caniatáu ichi weld unrhyw ddigwyddiadau ar eich llwybr yn y manylion teithio ar y ffordd. Hefyd bydd y map o daith ar y ffordd yn caniatáu ichi weld unrhyw eitemau newyddion a gynlluniwyd ar y diwrnod y byddwch yn teithio.</p>
<p>&nbsp;</p></div></div>
<div></div>'


-------------------------------------------------------------
-- FAQs - Maps
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpMaps', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="Maps" name="Maps"></a><h2>10. Maps</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.1">10.1) How do I find a place on a map?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.2">10.2) How can I see a map of incidents in my area?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.3">10.3) How do I find a map that shows traffic levels?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.4">10.4) How do I find a public transport network map?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>10. Maps</h2></div>
<p></p>
<h3><a class="QuestionLink" id="A10.1" name="A10.1"></a>10.1) How do I find a place on a map?&nbsp;</h3>
<p>Open "Find a place".</p><br />
<p>Click on &#8216;Find a map&#8217; and enter the name of the place.</p><br />
<p>Once you are looking at a map you can choose to view many different places in the &#8216;Map symbols&#8217; section.&nbsp; &#8216;Transport&#8217; symbols show on the map by default, but if you would like to show some of the other options on the map, click the one you are interested in, untick any symbols you are not interested in and click &#8216;Show selected symbols&#8217;.&nbsp; (Note:&nbsp; These can only be shown at the 5 closest zoom levels). </p>
<p>&nbsp;</p>
<p>The Journey Planner can show the following categories of places (&#8216;Attraction/facilities&#8217;) on maps. </p>
<p>&nbsp;</p>
<div>
<ul class="lister">
<li><strong>Transport information</strong></li></ul></div>
<div>
<ul class="lister">
<li>Railway stations <br />Underground/Metro stations<br />Bus/coach stops<br />Airports<br />Ferry terminals<br />Taxi ranks</li></ul><br />
<div>
<ul class="lister">
<li><strong>Accommodation, eating and drinking</strong></li></ul></div>
<div>
<ul class="lister">
<li>Eating and drinking<br />Hotels and guesthouses<br />Camping, caravanning and mobile homes<br />Youth hostels</li></ul><br />
<div>
<ul class="lister">
<li><strong>Sport, entertainment and retail</strong></li></ul></div>
<div>
<ul class="lister">
<li>Outdoor pursuits<br />Sports complexes<br />Venues: Stages and screens<br />Retail</li></ul><br />
<div>
<ul class="lister">
<li><strong>Attractions</strong></li></ul></div>
<ul class="lister">
<li>Botanical and zoological<br />Historical and cultural<br />Recreational and scenic<br />Tourist attractions</li></ul><br />
<div>
<ul class="lister">
<li><strong>Health</strong></li></ul></div>
<ul class="lister">
<li>Surgeries (doctor and dental)<br />Hospitals, clinics and health centres<br />Nursing and care homes<br />Chemists and opticians</li></ul><br />
<div>
<ul class="lister">
<li><strong>Education</strong></li></ul></div>
<ul class="lister">
<li>Primary and nursery<br />Secondary, middle and independent<br />Colleges and universities<br />Recreational</li></ul><br />
<div>
<ul class="lister">
<li><strong>Public infrastructure</strong></li></ul></div>
<ul class="lister">
<li>Police and court services<br />Local services<br />Other government services<br />Public facilities</li></ul>&nbsp;&nbsp;&nbsp; 
<p></p>
<h3><a class="QuestionLink" id="A10.2" name="A10.2"></a>10.2)&nbsp;How can I see a map of incidents in my area?</h3>
<p>From the &#8216;Live travel news&#8217; page you can view incidents, road works and engineering works in Britain.&nbsp; You can view these incidents on a map.&nbsp; To do this, select a region from the drop-down list and click &#8216;Show on map&#8217;.&nbsp; </p>
<p><br />Once you are viewing the map, move your cursor over any existing symbol on the page and information about the incident will pop up. &nbsp;&nbsp;&nbsp;&nbsp; </p>
<p></p>
<h3><a class="QuestionLink" id="A10.3" name="A10.3"></a>10.3)&nbsp;How do I find a map that shows traffic levels?</h3>
<p>Open "Find a place". </p>
<p>&nbsp;</p>
<p>Click on "Traffic maps". </p>
<p>&nbsp;</p>
<p>1.&nbsp;Enter the location for which you are interested in viewing traffic levels:</p>
<p>&nbsp;</p>
<ul class="lister">
<li>Select the kind of location </li>
<li>Type in the location </li>
<li>Confirm the location (if prompted)</li></ul>
<p>&nbsp;</p>
<p>2.&nbsp;Select the date and time for which you want to see the traffic levels<br />&nbsp;</p>
<h3><a class="QuestionLink" id="A10.4" name="A10.4"></a>10.4)&nbsp;How do I find a public transport network map?</h3>
<p>Open "Find a Place"</p>
<p>&nbsp;</p>
<p>Click on "Transport network maps".</p>
<p>&nbsp;</p>
<p>Click one of the links relating to the various types of transport.&nbsp; </p>
<p>&nbsp;</p>
<p>&nbsp;</p></div></div></div></div></div>
<div></div>', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="Maps" name="Maps"></a><h2>10. Mapiau</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.1">10.1) Sut ydw i&#8217;n dod o hyd i le ar y map? </a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.2">10.2) Sut y gallaf weld map o ddigwyddiadau yn fy ardal?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.3">10.3) Sut ydw i&#8217;n dod o hyd i fap sy&#8217;n dangos lefelau traffig?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMaps.aspx#A10.4">10.4) Sut ydw i''n dod o hyd i fap rhwydwaith trafnidiaeth gyhoeddus?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>10. Mapiau</h2></div>
<br />
<h3><a class="QuestionLink" id="A10.1" name="A10.1"></a>10.1) Sut ydw i&#8217;n dod o hyd i le ar y map?</h3>
<p>Agorwch "Canfyddwch le".</p><br />
<p>Cliciwch ar "Darganfyddwch fap" a rhowch enw''r lle.</p><br />
<p>Unwaith yr ydych chi&#8217;n edrych ar fap gallwch ddewis gweld nifer o wahanol lefydd yn yr adran &#8216;Symbolau map&#8217;. Mae symbolau &#8216;Trafnidiaeth&#8217; yn ymddangos ar y map yn ddiofyn, ond os hoffech chi ddangos rhai o&#8217;r opsiynau eraill ar y map, cliciwch ar yr un y mae gennych chi ddiddordeb ynddi, dad-diciwch y symbolau nad oes gennych ddiddordeb ynddynt a chliciwch &#8216;Dangos symbolau a ddewiswyd&#8217; (Noder: Gellir ond dangos y rhain ar gyfer y 5 lefel chwyddo agosaf).</p>
<p>&nbsp;</p>
<p>Gall Journey Planner ddangos y categor&#239;au llefydd canlynol (&#8216;Atyniadau/cyfleusterau) ar fapiau. </p>
<p>&nbsp;</p>
<div>
<ul class="lister">
<li><strong>Gwybodaeth drafnidiaeth</strong></li></ul></div>
<div>
<ul class="lister">
<li>Gorsafoedd rheilffordd<br />Gorsafoedd Tanddaearol/Metro<br />Arosfannau bws/bws moethus<br />Meysydd awyr<br />Terfynfeydd fferi<br />Rhengoedd tacsi</li></ul><br />
<div>
<ul class="lister">
<li><strong>Llety, bwyta ac yfed</strong></li></ul></div>
<ul class="lister">
<li>Bwyta ac yfed<br />Gwestai a gwestai bach<br />Gwersylla, carafanio a chartrefi symudol<br />Hosteli Ieuenctid</li></ul>
<div><br /></div>
<div>
<ul class="lister">
<li><strong>Chwaraeon, hamdden ac adwerthu</strong></li></ul></div>
<ul class="lister">
<li>Gweithgareddau awyr agored<br />Canolfannau chwaraeon<br />Mannau cyfarfod: Llwyfannau a sgriniau<br />Adwerthu</li></ul>
<div><br /></div>
<div>
<ul class="lister">
<li><strong>Atyniadau</strong></li></ul></div>
<ul class="lister">
<li>Botanegol a s&#246;olegol <br />Hanesyddol a diwylliannol <br />Adloniadol a golygfaol<br />Atyniadau twristiaid</li></ul>
<div><br /></div>
<div>
<ul class="lister">
<li><strong>Iechyd</strong></li></ul></div>
<ul class="lister">
<li>Meddygfeydd (doctoriaid a deintyddion)<br />Ysbytai, clinigiau a chanolfannau iechyd<br />Cartrefi nyrsio a gofal <br />Fferyllyddion ac optegwyr</li></ul>
<div><br /></div>
<div>
<ul class="lister">
<li><strong>Addysg</strong></li></ul></div>
<ul class="lister">
<li>Ysgolion cynradd a meithrynfeydd <br />Ysgolion uwchradd, canol ac annibynnol <br />Colegau a phrifysgolion<br />Adloniadol</li></ul>
<div><br /></div>
<div>
<ul class="lister">
<li><strong>Is-adeiledd cyhoeddus </strong></li></ul></div>
<ul class="lister">
<li>Gwasanaethau heddlu a llys<br />Llywodraethau lleol<br />Gwasanaethau eraill y llywodraeth <br />Cyfleusterau cyhoeddus</li></ul><br /><br />
<h3><a class="QuestionLink" id="A10.2" name="A10.2"></a>10.2) Sut y gallaf weld map o ddigwyddiadau yn fy ardal?</h3>
<p>O&#8217;r dudalen &#8216;Newyddion teithio byw&#8217; gallwch weld digwyddiadau, gwaith ffordd a gwaith peirianneg ym Mhrydain. Gallwch edrych ar y digwyddiadau hyn ar fap. I wneud hyn, dewiswch ranbarth o&#8217;r rhestr a ollyngir i lawr a chliciwch &#8216;Dangoswch ar y map&#8217;.&nbsp; </p>
<p><br />Cyn gynted ag y boch yn edrych ar y map, symudwch eich cyrchydd dros unrhyw symbol sydd ar y dudalen yn barod a bydd gwybodaeth am y digwyddiad yn ymddangos. </p>
<p></p>
<h3><a class="QuestionLink" id="A10.3" name="A10.3"></a>10.3)&nbsp;Sut ydw i&#8217;n dod o hyd i fap sy&#8217;n dangos lefelau traffig?</h3>
<p>Agorwch "Canfyddwch le". </p>
<p>&nbsp;</p>
<p>Cliciwch ar "Mapiau trafnidiaeth". </p>
<p>&nbsp;</p>
<p>1.&nbsp;Nodwch y lleoliad y mae gennych ddiddordeb mewn gweld y lefelau trafnidiaeth ar ei gyfer:</p>
<p>&nbsp;</p>
<ul class="lister">
<li>Dewiswch y math o leoliad </li>
<li>Teipiwch y lleoliad </li>
<li>Cadarnhewch y lleoliad (os gofynnir i chi)</li></ul>
<p>&nbsp;</p>
<p>2.&nbsp;Dewiswch y dyddiad a''r amser y dymunwch weld y lefelau trafnidiaeth ar eu cyfer</p>
<p></p>
<h3><a class="QuestionLink" id="A10.4" name="A10.4"></a>10.4)&nbsp;Sut ydw i''n dod o hyd i fap rhwydwaith trafnidiaeth gyhoeddus?</h3>
<p>Agorwch "Canfyddwch le".</p>
<p>&nbsp;</p>
<p>Cliciwch ar "Mapiau rhwydwaith cludiant".</p>
<p>&nbsp;</p>
<p>Cliciwch ar un o&#8217;r dolenni sy&#8217;n perthyn i&#8217;r gwahanol fathau o drafnidiaeth.</p>
<p>&nbsp;</p>
<p>&nbsp;</p></div></div></div>'



-------------------------------------------------------------
-- FAQs - Mobile
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpMobile', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="TDOnPDAOrMobile" name="TDOnPDAOrMobile"></a><h2>14. Using the Journey Planner services on your PDA/Mobile</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMobile.aspx#A14.1">14.1)&nbsp;How can I use the latest PDAs or mobile phones to find out the departure and arrival times for railway stations throughout Britain and for some bus or coach stops in areas where SMS codes are available for the bus or coach stop?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMobile.aspx#A14.2">14.2)&nbsp;How can I use the latest PDAs or mobile phones to view Live travel news for public transport or car (e.g. check for delays and incidents) either for a region or for the whole of Britain?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMobile.aspx#A14.3">14.3)&nbsp;How can I use the latest PDAs or mobile phones to find out whether there is a taxi rank or cab (private hire car) firm at the railway station I am travelling to, and get phone numbers for taxi and cab firms serving the station?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>14. Using the Journey Planner services on your PDA/Mobile</h2></div>
<br />
<h3><a class="QuestionLink" id="A14.1" name="A14.1"></a>14.1)&nbsp;How can I use the latest PDAs or mobile phones to find out the departure and arrival times for railway stations throughout Britain and for some bus or coach stops in areas where SMS codes are available for the bus or coach stop?</h3>
<p>You can learn how to use these services on a PDA by using the PDA simulator which can be found on the "Tips &amp; tools" page, reached by clicking the tab at the top of the website.</p><br />
<p>On a PDA or mobile phone using the latest browser technology (WAP2.0) over a GPRS or a 3G connection you can get scheduled arrival and departure times and in some cases provide the expected arrival or departure times for rail stations.</p>
<p>&nbsp;</p>
<ul class="lister">
    <li>You need to enter either the name of the station or stop or the location code for where you are departing from, arriving at or enter both the arrival and departure locations/codes. </li>
    <li>It is always quicker to use the location code for the station or stop you are travelling to or from.</li></ul>
<ul class="sublister">
    <li>For stations and airports these are always three letter codes such as BHM for Birmingham New Street, or CLJ for Clapham Junction. </li>
    <li>For bus and coach stops, the codes are the seven or eight character SMS codes that you can find on the bus stops.</li></ul>
    <br />
<ul class="lister">
    <li>You can then choose to view times for:</li></ul>
<ul class="sublister">
    <li>The current time </li>
    <li>The first service tomorrow </li>
    <li>The last service of today or tomorrow </li>
    <li>Specify a time for today or tomorrow.</li></ul>
    <br />
<h3><a class="QuestionLink" id="A14.2" name="A14.2"></a>14.2)&nbsp;How can I use the latest PDAs or mobile phones to view Live travel news for public transport or car (e.g. check for delays and incidents) either for a region or for the whole of Britain?</h3>
<p>You can learn how to use these services on a PDA by using the PDA simulator which can be found on the "Tips &amp; tools" page, reached by clicking the tab at the top of the website.</p><br />
<p>On a PDA or mobile phone using the latest browser technology (WAP2.0) over a GPRS or a 3G connection click on the &#8216;Travel news&#8217; link.</p><br />
<p>The page will be set to show information for the UK initially. However, if you have just looked up travel times for a station or stop it will default to show the information for only that region.</p><br />
<p>The page will list both current incidents and planned events that can affect or delay journeys, such as traffic accidents, road works, etc. You can tailor the information so that it is of interest to you by selecting options from the drop-down lists.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A14.3" name="A14.3"></a>14.3)&nbsp;How can I use the latest PDAs or mobile phones to find out whether there is a taxi rank or cab (private hire car) firm at the railway station I am travelling to, and get phone numbers for taxi or cab firms serving the station?</h3>
<p>You can learn how to use these services on a PDA by using the PDA simulator which can be found on the "Tips &amp; tools" page, reached by clicking the tab at the top of the website.</p><br />
<p>To get details about departures and/or arrivals for a station or between two stations see FAQ 14.1 above.</p><br />
<p>On a PDA (using the latest browser technology (WAP2.0) over a GPRS or a 3G connection), when you have the results for arrivals and/or departures you will need to click on the name of the station you want to find out about in your results &#8211; this will take you to &#8216;Station information&#8217; where phone numbers for taxi and/or cab (private hire car) firms serving the station are listed.</p><br />
<p>On a mobile phone (using the latest browser technology (WAP2.0) over a GPRS or a 3G connection), the results page will list the station(s) you requested travel times for at the top of the page. These stations can be clicked to show &#8216;Station information&#8217; where phone numbers for taxi and/or cab (private hire car) firms serving the station are listed. If you click on the times in the results you will see a page that displays all the stations the service stops at. These station names can also be clicked to find out phone numbers for taxi and/or cab (private hire car) firms serving the stations.</p>
<p>&nbsp;</p>
</div></div>', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="TDOnPDAOrMobile" name="TDOnPDAOrMobile"></a><h2>14. Defnyddio gwasanaethau teithio byw Journey Planner ar PDA/ff&#244;n symudol</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMobile.aspx#A14.1">14.1)&nbsp;Sut y gallaf ddefnyddio&#8217;r PDAs neu&#8217;r ffonau symudol diweddaraf i ddarganfod amserau ymadael a chyrraedd ar gyfer gorsafoedd rheilffordd ledled Prydain ac ar gyfer rhai arosfannau bysiau neu fysiau moethus mewn ardaloedd lle mae codau SMS ar gael ar gyfer yr arhosfan bysiau neu fysiau moethus?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMobile.aspx#A14.2">14.2)&nbsp;Sut y gallaf ddefnyddio PDAs neu&#8217;r ffonau symudol ddiweddaraf i edrych ar Newyddion Teithio Byw ar gyfer cludiant cyhoeddus neu gar (e.e. edrychwch a oes unrhyw oedi ac unrhyw ddigwyddiadau) naill ai ar gyfer rhanbarth neu ar gyfer Prydain gyfan?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpMobile.aspx#A14.3">14.3)&nbsp;Sut y gallaf ddefnyddio&#8217;r PDAs neu&#8217;r ffonau symudol diweddaraf i ddarganfod a oes ranc tacsi neu gwmni cabiau (ceir hurio preifat) yn yr orsaf rheilffordd yr wyf yn teithio iddi, ac i gael rhifau ff&#244;n ar gyfer cwmn&#239;au tacsi neu gabiau sy&#8217;n gwasanaethu&#8217;r orsaf?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>14. Defnyddio gwasanaethau teithio byw Journey Planner ar PDA/ff&#244;n symudol</h2></div>
<br />
<h3><a class="QuestionLink" id="A14.1" name="A14.1"></a>14.1)&nbsp;Sut y gallaf ddefnyddio&#8217;r PDAs neu&#8217;r ffonau symudol diweddaraf i ddarganfod amserau ymadael a chyrraedd ar gyfer gorsafoedd rheilffordd ledled Prydain ac ar gyfer rhai arosfannau bysiau neu fysiau moethus mewn ardaloedd lle mae codau SMS ar gael ar gyfer yr arhosfan bysiau neu fysiau moethus?</h3>
<p>Gallwch ddysgu sut i ddefnyddio&#8217;r gwasanaethau hyn ar PDA drwy ddefnyddio&#8217;r efelychydd PDA y gellir ei weld ar y dudalen Awgrymiadau a theclynnau, a gyrhaeddir drwy glicio ar y tab ar frig y wefan.</p><br />
<p>Ar PDA neu ff&#244;n symudol sy&#8217;n defnyddio&#8217;r dechnoleg pori ddiweddaraf (WAP2.0) dros gysylltiad GPRS neu 3G gallwch gael yr amserau y bwriedir cyrraedd ac ymadael ac mewn rhai achosion roi&#8217;r amserau cyrraedd neu ymadael disgwyliedig ar gyfer gorsafoedd rheilffordd.</p>
<p>&nbsp;</p>
<ul class="lister">
    <li>Bydd angen i chi naill ai roi enw&#8217;r orsaf neu&#8217;r arhosfan neu&#8217;r c&#244;d lleoliad ar gyfer y lle rydych yn ymadael ohono, neu yn cyrraedd ynddo neu roi&#8217;r lleoliadau/codau cyrraedd ac ymadael. </li>
    <li>Mae bob amser yn gyflymach defnyddio&#8217;r c&#244;d lleoliad ar gyfer yr orsaf neu arhosfan yr ydych yn teithio iddo neu ohono. </li></ul>
<ul class="sublister">
    <li>Ar gyfer gorsafoedd a meysydd awyr mae&#8217;r rhain bob amser yn godau tair llythyren fel BHM ar gyfer Birmingham New Street neu CLJ ar gyfer Clapham Junction. </li>
    <li>Ar gyfer arosfannau bysiau a bysiau moethus, y codau yw&#8217;r codau SMS saith neu wyth symbol y gallwch eu darganfod ar yr arosfannau bysiau.</li></ul>
    <br />
<ul class="lister">
    <li>Yna gallwch ddewis edrych ar amserau ar gyfer:</li></ul>
<ul class="sublister">
    <li>Yr amser presennol </li>
    <li>Y gwasanaeth cyntaf yfory </li>
    <li>Y gwasanaeth olaf heddiw neu yfory </li>
    <li>Nodi amser ar gyfer heddiw neu yfory.</li></ul>
    <br />
<h3><a class="QuestionLink" id="A14.2" name="A14.2"></a>14.2)&nbsp;Sut y gallaf ddefnyddio PDAs neu&#8217;r ffonau symudol ddiweddaraf i edrych ar Newyddion Teithio Byw ar gyfer cludiant cyhoeddus neu gar (e.e. edrychwch a oes unrhyw oedi ac unrhyw ddigwyddiadau) naill ai ar gyfer rhanbarth neu ar gyfer Prydain gyfan?</h3>
<p>Gallwch ddysgu sut i ddefnyddio&#8217;r gwasanaethau hyn ar PDA drwy ddefnyddio&#8217;r efelychydd PDA, y gellir ei weld ar y dudalen Awgrymiadau a theclynnau, a gyrhaeddir drwy glicio ar y tab ar frig y wefan.</p><br />
<p>Ar PDA neu ff&#244;n symudol sy&#8217;n defnyddio&#8217;r dechnoleg pori ddiweddaraf (WAP2.0) dros gysylltiad GPRS neu 3G cliciwch ar y ddolen &#8216;Newyddion teithio&#8217;.</p><br />
<p>Bydd y dudalen hon yn dangos gwybodaeth ar gyfer y DU i ddechrau. Ond os ydych wedi edrych ar amserau teithio ar gyfer gorsaf neu arhosfan mewn rhanbarth penodol bydd yn dangos gwybodaeth am y rhanbarth hwnnw.</p><br />
<p>Bydd y dudalen yn rhestru&#8217;r digwyddiadau cyfredol a digwyddiadau a gynllunir a all effeithio ar neu oedi siwrneion, fel damweiniau traffig, gwaith ffyrdd ac ati. Er hynny, gallwch deilwrio&#8217;r wybodaeth fel ei bod o ddiddordeb i chi drwy ddewis dewisiadau o&#8217;r rhestrau a ollyngir i lawr.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A14.3" name="A14.3"></a>14.3)&nbsp;Sut y gallaf ddefnyddio&#8217;r PDAs neu&#8217;r ffonau symudol diweddaraf i ddarganfod a oes ranc tacsi neu gwmni cabiau (ceir hurio preifat) yn yr orsaf rheilffordd yr wyf yn teithio iddi, ac i gael rhifau ff&#244;n ar gyfer cwmn&#239;au tacsi neu gabiau sy&#8217;n gwasanaethu&#8217;r orsaf?</h3>
<p>Gallwch ddysgu sut i ddefnyddio&#8217;r gwasanaethau hyn ar PDA drwy ddefnyddio&#8217;r efelychydd PDA, y gellir ei weld ar y dudalen Awgrymiadau a theclynnau, a gyrhaeddir drwy glicio ar y tab ar frig y wefan.</p><br />
<p>I gael manylion am amserau ymadael a/neu gyrraedd ar gyfer gorsaf neu rhwng dwy orsaf gweler COA 14.1 uchod.</p><br />
<p>Ar PDA (gan ddefnyddio&#8217;r dechnoleg pori diweddaraf (WAP2.0) dros gysylltiad GPRS neu 3G), pan fydd gennych y canlyniadau ar gyfer amserau cyrraedd a/neu ymadael bydd angen i chi glicio ar enw&#8217;r orsaf y dymunwch ddarganfod amdani yn eich canlyniadau &#8211; bydd hyn yn mynd &#226; chi at &#8216;Gwybodaeth am orsafoedd&#8217; lle rhestrir rhifau ff&#244;n cwmn&#239;au tacsi a/neu gabiau (ceir hurio preifat) sy&#8217;n gwasanaethu&#8217;r orsaf.</p><br />
<p>Ar ff&#244;n symudol (gan ddefnyddio&#8217;r dechnoleg pori ddiweddaraf (WAP2.0) dros gysylltiad GPRS neu 3G), bydd y dudalen canlyniadau yn rhestru&#8217;r gorsaf(oedd) y bu i chi ofyn am amserau teithio ar eu cyfer ar frig y dudalen. Gellir clicio ar y gorsafoedd hyn i ddangos &#8216;Gwybodaeth am orsafoedd&#8217; lle rhestrir rhifau ff&#244;n ar gyfer cwmn&#239;au tacsi a/neu gabiau (ceir hurio preifat) sy&#8217;n gwasanaethu&#8217;r orsaf. Os cliciwch ar yr amserau hyn yn y canlyniadau fe welwch dudalen sy&#8217;n dangos yr holl orsafoedd y mae&#8217;r gwasanaeth yn aros ynddynt. Gellir clicio ar enwau&#8217;r gorsafoedd hyn hefyd i ddarganfod rhifau ff&#244;n cwmn&#239;au tacsi a/neu gabiau (ceir hurio preifat) sy&#8217;n gwasanaethu&#8217;r gorsafoedd.</p>
<p>&nbsp;</p>
</div></div>'



-------------------------------------------------------------
-- FAQs - Parking
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpParking', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>6. Parking</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.1">6.1) How do I find a car park?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.2">6.2) How can I find out about Park and Ride schemes?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.3">6.3) How can I plan a Park and Ride journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.4">6.4) Where does car park data come from?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.5">6.5) What is the Park Mark scheme?</a></p>
<p>&nbsp;</p>
<div class="hdtypethree">
<h2>6. Parking</h2></div>
<br />
<h3><a class="QuestionLink" id="A6.1" name="A6.1"></a>6.1) How do I find a car park?</h3>
<p><strong>Step 1: Open &#8216;Find a place&#8217;</strong></p>
<ul class="lister">
<li>Click &#8216;Find nearest car park&#8217;.</li></ul>
<p><strong></strong>&nbsp;</p>
<p><strong>Step 2: Select the kind of location the car park is near</strong></p>
<ul class="lister">
<li>Select the kind of location you want to type in (e.g. &#8216;Town/district/village&#8217;, &#8216;Address/postcode&#8217;, &#8216;Facility/attraction&#8217; &#8230;etc)</li></ul>
<ul class="lister">
<li>Type the location name in the box.</li></ul>
<p><strong></strong>&nbsp;</p>
<p><strong>Step 3: Click "Next"</strong></p>
<p>&nbsp;</p>
<p><strong>Step 4: Confirm your location</strong></p>
<p>Once you have clicked &#8216;Next&#8217;, if the Journey Planner is not able to find an exact match for the location you typed in, a drop-down list (highlighted in yellow) will show you one or more similar matches for that location.</p>
<p>&nbsp;</p>
<p>Depending on the information you entered, you will need to do <strong>one</strong> of the following:</p>
<ul class="lister">
<li><strong>Select a location option from the drop-down list.</strong></li></ul>
<p><strong>OR</strong>&nbsp;</p>
<ul class="lister">
<li><strong>Select a location option from the drop-down list and search for the location as a different kind of location (e.g. &#8220;Address/postcode&#8221;)</strong></li></ul>
<p><strong>OR</strong>&nbsp;</p>
<ul class="lister">
<li><strong>Click &#8216;Back&#8217; and enter a new location.</strong></li></ul>
<p>Once your information is confirmed, car parks near to that location will be listed on the page. You can click &#8216;Show map&#8217; to see a map with the car park(s) on it.</p>
<p>&nbsp;</p>
<p><strong>Step 5: If you would like to plan a journey once you find the car park you are looking for, make sure it is selected and click &#8216;Drive from&#8217; or &#8216;Drive to&#8217;.</strong></p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A6.2" name="A6.2"></a>6.2) How can I find out about Park and Ride schemes?</h3>
<p>To find out about Park and Ride schemes, click the link on the "Find a car route" page. This will take you to a page containing information and web links for Park and Ride schemes across Britain. </p>
<h3><a class="QuestionLink" id="A6.3" name="A6.3"></a>6.3) How can I plan a Park and Ride journey?</h3>
<p>There are two ways to plan a park and ride journey:</p>
<p>&nbsp;</p>
<ul class="listerdisc">
<li>If you access the Park and Ride schemes page from the Find a car route input page, you can select &#8216;Drive to&#8217; for the scheme you want to use <br /> </li>
<li>You can select &#8216;Plan to Park and Ride&#8217; from the &#8216;Plan a journey&#8217; page. This will open an input page that allows you to select a Park and Ride scheme from a drop-down list</li></ul>
<p>&nbsp;</p>
<p>For both options you can enter all other information as for a normal car journey plan.&nbsp; This includes advanced options such as maximum speed, fuel consumption, roads to use or avoid etc.</p>
<p>&nbsp;</p>
<p>If there is more than one car park in the scheme you are travelling to, the Journey Planner will plan to the car park that gives you the &#8216;best&#8217; car journey.&nbsp; This will be quickest journey unless you have selected a different advanced option, for example shortest or most fuel economic journey.</p>
<h3><a class="QuestionLink" id="A6.4" name="A6.4"></a>6.4)&nbsp;Where does car park data come from?</h3>
<p>Landmark Information Group provides us with detailed information on car parks within the UK.<br /><br /><a href="/Web/downloads/CarParkProviders.pdf" target="_blank">Please click here to see a comprehensive list of our sources of car park information (PDF) <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />.</a></p>
<p><br /><a href="http://www.adobe.com/products/acrobat/readstep2.html" target="_blank"><img alt="To read PDF files, download Acrobat Reader. This will open a new window." src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/getacro1.gif" border="0" /></a></p>
<h3><a class="QuestionLink" id="A6.5" name="A6.5"></a>6.5)&nbsp;What is the Park Mark scheme?</h3>
<p>Park Mark® status is awarded to car parks that have been assessed by the Police under the Safer Parking Scheme. The assessement checks that the parking operator has put in place measures to help prevent criminal activity and anti-social behaviour, and create a safe environment.</p><br />
<p>The Safer Parking Scheme is managed by the British Parking Association on behalf of the Association of Chief Police Officers and supported by the Home Office and Scottish Government.</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>
<div></div>
<div></div>
<div></div>', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>6. Parcio</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.1">6.1) Sut y gallaf ddod o hyd i faes parcio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.2">6.2) Sut y gallaf ddarganfod gwybodaeth am gynlluniau Parcio a Theithio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.3">6.3) Sut y gallaf gynllunio siwrnai Parcio a Theithio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.4">6.4) O ble y daw data am feysydd parcio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpParking.aspx#A6.5">6.5) Beth yw''r cynllun Park Mark?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>6. Parcio</h2></div>
<br />
<h3><a class="QuestionLink" id="A6.1" name="A6.1"></a>6.1) Sut y gallaf ddod o hyd i faes parcio?</h3>
<p><strong>Cam 1: Agorwch &#8216;Canfyddwch le&#8217;</strong></p>
<ul class="lister">
<li>Cliciwch &#8216;Darganfyddwch y maes parcio agosaf&#8217;.</li></ul>
<p><strong></strong>&nbsp;</p>
<p><strong>Cam 2: Dewiswch y math o leoliad y mae&#8217;r maes parcio yn agos ato</strong></p>
<ul class="lister">
<li>Dewiswch y math o leoliad y dymunwch ei deipio (e.e Tref/rhanbarth/pentref&#8217;, &#8216;Cyfeiriad/c&#244;d post&#8217;, &#8216;Cyfleuster/atyniad&#8217;... ac ati)</li></ul>
<ul class="lister">
<li>Teipiwch enw&#8217;r lleoliad yn y blwch.</li></ul>
<p><strong></strong>&nbsp;</p>
<p><strong>Cam 3: Cliciwch &#8216;Nesaf&#8217;</strong></p>
<p>&nbsp;</p>
<p><strong>Cam 4: Cadarnhewch eich lleoliad</strong></p>
<p>Wedi i chi glicio &#8216;Nesaf&#8217;, os nad yw Journey Planner yn gallu dod o hyd i gyfatebiaeth union ar gyfer y lleoliad y bu i chi ei deipio, bydd rhestr a ollyngir i lawr (a amlygir mewn melyn) yn dangos un neu fwy o gyfatebiaethau tebyg i chi ar gyfer y lleoliad hwnnw.</p>
<p>&nbsp;</p>
<p>Yn dibynnu ar y wybodaeth y bu i chi ei rhoi, bydd angen i chi wneud <strong>un</strong> o&#8217;r canlynol:</p>
<ul class="lister">
<li><strong>Dewiswch leoliad o&#8217;r rhestr a ollyngir i lawr</strong></li></ul>
<p><strong>NEU</strong>&nbsp;</p>
<ul class="lister">
<li><strong>Dewiswch leoliad o&#8217;r rhestr a ollyngir i lawr a chwiliwch am y lleoliad fel math gwahanol o leoliad (e.e. &#8216;Cyfeiriad/c&#244;d post&#8217;)</strong></li></ul>
<p><strong>NEU</strong>&nbsp;</p>
<ul class="lister">
<li><strong>Cliciwch &#8216;Yn &#244;l&#8217; a rhowch leoliad newydd.</strong></li></ul>
<p>Cyn gynted ag y bydd eich gwybodaeth wedi ei chadarnhau, bydd meysydd parcio ger y lleoliad hwnnw yn cael eu rhestru ar y dudalen. Gallwch glicio &#8216;Dangoswch fap&#8217; i weld map gyda&#8217;r maes/meysydd parcio arno.</p>
<p>&nbsp;</p>
<p><strong>Cam 5: Os hoffech gynllunio siwrnai unwaith y deuwch o hyd i&#8217;r maes parcio yr ydych yn chwilio amdano, gofalwch ei fod wedi ei ddewis a chliciwch &#8216;Gyrrwch o&#8217; neu &#8216;Gyrrwch i&#8217;.</strong></p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A6.2" name="A6.2"></a>6.2) Sut y gallaf ddarganfod gwybodaeth am gynlluniau Parcio a Theithio? </h3>
<p></p>
<p>I ddarganfod gwybodaeth am gynlluniau Parcio a Theithio, cliciwch y ddolen ar y dudalen "Canfyddwch lwybr car". Bydd hyn yn mynd &#226; chi i dudalen sy''n cynnwys gwybodaeth a dolennau gwe ar gyfer cynlluniau Parcio a Theithio ar draws Prydain. </p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A6.3" name="A6.3"></a>6.3) Sut y gallaf gynllunio siwrnai Parcio a Theithio?</h3>
<p>Mae dwy ffordd o gynllunio siwrnai parcio a theithio:</p>
<p>&nbsp;</p>
<ul>
<li>Os ewch i dudalen y cynlluniau Parcio a Theithio o''r dudalen Darganfyddwch lwybr car, gallwch ddewis "Gyrrwch i" ar gyfer y cynllun y dymunwch ei ddefnyddio. <br /></li>
<li>Gallwch ddewis "Cynlluniwch i Barcio a Theithio" o''r dudalen "Cynlluniwch siwrnai". Bydd hon yn agor tudalen mewnbynnu i chi sy''n caniatau i chi ddewis cynllun Parcio a Theithio o restr a ollyngir i lawr.</li></ul>
<p>&nbsp;</p>
<p>Ar gyfer y ddau ddewis gallwch nodi''r holl wybodaeth arall fel ar gyfer cynllun siwrnai car arferol.&nbsp; Mae hyn yn cynnwys dewisiadau mwy cymhleth fel uchafswm cyflymder, defnydd o danwydd, ffyrdd i''w defnyddio neu eu hosgoi ac ati.</p>
<p>&nbsp;</p>
<p>Os oes mwy nag un maes parcio yn y cynllun yr ydych yn teithio iddo, bydd Journey Planner yn cynllunio at y maes parcio sy''n rhoi''r siwrnai car "orau" i chi.&nbsp; Hon fydd y siwrnai gyflymaf oni bai eich bod wedi mynd am ddewis mwy cymhleth gwahanol, er enghraifft y siwrnai fyrraf neu''r un fwyaf economaidd o ran tanwydd.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A6.4" name="A6.4"></a>6.4) O ble y daw data am feysydd parcio?</h3>
<p>Mae''r Gr&#373;p Gwybodaeth Tirnodau yn rhoi gwybodaeth fanwl inni am feysydd parcio yn y DU.<br /><br /><a href="/Web/downloads/CarParkProviders_CY.pdf" target="_blank">Cliciwch yma i weld rhestr gynhwysfawr o ddarparwyr meysydd parcio (PDF) <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" />.</a></p>
<p><br /><a href="http://www.adobe.com/products/acrobat/readstep2.html" target="_blank"><img alt="I ddarllen ffeiliau PDF, lawrlwythwch Acrobat Reader. Bydd hwn yn agor ffenestr newydd." src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/getacro1.gif" border="0" /></a></p>
<h3><a class="QuestionLink" id="A6.5" name="A6.5"></a>6.5) Beth yw''r cynllun Park Mark?</h3>
<p>Rhoddir statws Park Mark® i feysydd parcio sydd wedi''u hasesu gan yr Heddlu dan y Cynllun Parcio Mwy Diogel. Mae''r asesiad yn gwirio bod gweithredwr y maes parcio wedi rhoi mesurau yn eu lle i helpu wrth atal gweithgarwch troseddol ac ymddygiad gwrthgymdeithasol, a chreu amgylchedd diogel.</p><br />
<p>Mae''r Cynllun Parcio Mwy Diogel wedi''i reoli gan Gymdeithas Barcio Prydain ar ran Cymdeithas Prif Swyddogion yr Heddlu ac mae wedi''i gefnogi gan y Swyddfa Gartref a Llywodraeth yr Alban.</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>'



-------------------------------------------------------------
-- FAQs - Road
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpRoad', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>4. Road Journey Planning</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.1">4.1) Are all journeys planned by the quickest route?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.2">4.2) How do I avoid particular roads?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.3">4.3) How do I choose particular roads to use?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.4">4.4) How do I avoid all motorways?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.5">4.5) How do I find a car/taxi route that avoids all tolls or ferries?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.6">4.6) How do you calculate car journey times?</a></p>
<p>&nbsp;</p>
<div class="hdtypethree">
<h2>4. Road Journey Planning</h2></div>
<br />
<h3><a class="QuestionLink" id="A4.1" name="A4.1"></a>4.1) Are all journeys planned by the quickest route?</h3>
<p>The advanced options button in the &#8216;Find a car route&#8217; allows you to select a number of different parameters for your journey. So you can now plan your journey by the:</p><br />
<ul>
<li>Quickest route </li>
<li>Shortest route </li>
<li>Most fuel economic </li>
<li>Cheapest overall (Taking into account toll roads and ferry charges) </li></ul><br />
<p>Just by selecting your preferred option from the drop down menu marked &#8216;Type of journey&#8217;.</p>
<h3><a class="QuestionLink" id="A4.2" name="A4.2"></a>4.2) How do I avoid particular roads?</h3>
<p>You can choose to <strong>avoid</strong> particular roads when planning your car/taxi journey by clicking &#8216;Advanced options&#8217; at the bottom of the input pages and typing up to six roads or motorways that you would like to <strong>avoid</strong> into the box(es) (e.g. M6, or, A23). The Journey Planner will, where possible, avoid these roads when planning your journey. </p>
<h3><a class="QuestionLink" id="A4.3" name="A4.3"></a>4.3) How do I choose particular roads to use?</h3>
<p>You can choose to <strong>use</strong> particular roads when planning your car/taxi journey by clicking &#8216;Advanced options&#8217; at the bottom of the input pages and typing up to six roads or motorways that you would like to <strong>use</strong> into the box(es) (e.g. M6, or, A23). The Journey Planner will, where possible, use these roads when planning your journey.</p>
<h3><a class="QuestionLink" id="A4.4" name="A4.4"></a>4.4) How do I avoid all motorways?</h3>
<p>You can find a car/taxi route that avoids motorways by clicking &#8216;Advanced options&#8217; at the bottom of the input pages and ticking the (avoid) &#8216;motorways&#8217; boxes.</p>
<h3><a class="QuestionLink" id="A1.4.5" name="A4.5"></a>4.5) How do I find a car/taxi route that avoids all tolls or ferries?</h3>
<p>You can find a car/taxi route that avoids tolls and ferries by clicking &#8216;Advanced options&#8217; at the bottom of the input pages and ticking the (avoid) &#8216;tolls&#8217; and/or &#8216;ferries&#8217; boxes.</p>
<h3><a class="QuestionLink" id="A4.6" name="A4.6"></a>4.6) How do you calculate car journey times?</h3>
<p>The car journey results on the Journey Planner take into account the appropriate speed levels and the estimated traffic speeds for the time, day and type of roads in the journey. Where possible we base this on specific, historic information about past traffic levels. Where this information is not available we base our estimations on the National Traffic Model. The additional information about particular roads is based on past traffic measurements provided by the Highways Agency, the Welsh Assembly and the Scottish Government.</p>
<p>&nbsp;</p></div></div>
<div></div>
<div></div>
<div></div>
<div></div>', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>4. Cynllunio siwrnai ffordd</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.1">4.1) A yw''r holl siwrneion yn cael eu cynllunio yn &#244;l y llwybr cyflymaf?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.2">4.2) Sut y gallaf osgoi ffyrdd penodol?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.3">4.3) Sut y dewisaf ffyrdd penodol i&#8217;w defnyddio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.4">4.4) Sut ydw i''n osgoi''r traffyrdd i gyd?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.5">4.5) Sut ydw i''n dod o hyd i lwybr car/tacsi sy''n osgoi pob toll neu fferi?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpRoad.aspx#A4.6">4.6) Sut ydych chi''n cyfrif amserau siwrneion car?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>4. Cynllunio siwrnai ffordd</h2></div>
<br />
<h3><a class="QuestionLink" id="A4.1" name="A4.1"></a>4.1) A yw''r holl siwrneion yn cael eu cynllunio yn &#244;l y llwybr cyflymaf?</h3>
<p>Mae''r botwm dewisiadau mwy cymhleth yn "Canfyddwch lwybr car" yn caniatáu i chi ddethol nifer o wahanol baramedrau ar gyfer eich siwrnai. Felly cewch gynllunio eich siwrnai yn &#244;l y:</p><br />
<ul>
<li>Llwybr cyflymaf </li>
<li>Llwybr byrraf </li>
<li>Mwyaf economaidd o ran tanwydd</li>
<li>Rhataf yn gyffredinol (Gan ystyried tollau ffordd a chostau llongau)</li></ul><br />
<p>Drwy ddewis eich hoff un o''r rhestr a ollyngir i lawr o''r enw "Math o siwrnai".</p>
<h3><a class="QuestionLink" id="A4.2" name="A4.2"></a>4.2) Sut y gallaf osgoi ffyrdd penodol?</h3>
<p>Gallwch ddewis <strong>osgoi</strong> ffyrdd penodol wrth gynllunio eich siwrnai gar/tacsi drwy glicio ar "Dewisiadau mwy cymhleth" ar waelod y tudalennau mewnbwn a theipio hyd at chwe ffordd neu draffordd yr hoffech eu <strong>hosgoi</strong> yn y blwch/blychau (e.e. M6 neu A23). Lle bo''n bosibl, bydd Journey Planner yn osgoi''r ffyrdd hyn wrth gynllunio eich siwrnai.</p>
<h3><a class="QuestionLink" id="A4.3" name="A4.3"></a>4.3) Sut y dewisaf ffyrdd penodol i&#8217;w defnyddio? </h3>
<p>Gallwch ddewis <strong>defnyddio</strong> ffyrdd penodol wrth gynllunio eich siwrnai gar/tacsi drwy glicio "Dewisiadau mwy cymhleth" ar waelod y tudalennau mewnbwn a theipio hyd at chwe ffordd neu draffordd yr hoffech eu <strong>defnyddio</strong> yn y blwch/blychau (e.e. M6 neu A23). Lle bo''n bosibl, bydd Journey Planner yn defnyddio''r ffyrdd hyn wrth gynllunio eich siwrnai.</p>
<h3><a class="QuestionLink" id="A4.4" name="A4.4"></a>4.4) Sut ydw i''n osgoi''r traffyrdd i gyd?</h3>
<p>Gallwch ddod o hyd i lwybr car/tacsi sy''n osgoi traffyrdd drwy glicio ar "Dewisiadau mwy cymhleth" ar waelod y tudalennau mewnbwn a thicio''r blychau (osgoi) "traffyrdd".</p>
<h3><a class="QuestionLink" id="A4.5" name="A4.5"></a>4.5) Sut rwy&#8217;n dod o hyd i lwybr car sy&#8217;n osgoi pob toll neu fferi?</h3>
<p>Gallwch ddod o hyd i lwybr car/tacsi sy''n osgoi tollau a ffer&#239;au drwy glicio ar "Dewisiadau mwy cymhleth" ar waelod y tudalennau mewnbwn a thicio''r blychau (osgoi) "tollau" and/neu "ffer&#239;au".</p>
<h3><a class="QuestionLink" id="A4.6" name="A4.6"></a>4.6) Sut ydych chi''n cyfrif amserau siwrneion car?</h3>
<p>Mae canlyniadau''r siwrnai car ar Journey Planner yn ystyried y lefelau cyflymder priodol a''r cyflymderau traffig amcangyfrifedig ar gyfer yr amser, y diwrnod a''r math o ffyrdd yn y siwrnai. Lle fo''n bosibl, rydym yn seilio hyn ar wybodaeth benodol, hanesyddol ynghylch lefelau traffig y gorffennol. Os nad yw''r wybodaeth hon ar gael byddwn yn seilio ein hamcangyfrifon ar y Model Traffig Cenedlaethol. Seilir y wybodaeth ychwanegol ynghylch ffyrdd penodol ar fesuriadau traffig y gorffennol a ddarparwyd gan yr Asiantaeth Priffyrdd, Cynulliad Cenedlaethol Cymru a Gweithrediaeth yr Alban.</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>'


-------------------------------------------------------------
-- FAQs - Taxi
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpTaxi', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>5. Taxis</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTaxi.aspx#A5.1">5.1) How can I find taxi information for stations I am travelling to in my journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTaxi.aspx#A5.2">5.2) Can I find out if the taxis are wheelchair accessible?</a></p>
<p>&nbsp;</p>
<div class="hdtypethree">
<h2>5. Taxis</h2></div>
<br />
<h3><a class="QuestionLink" id="A5.1" name="A5.1"></a>5.1) How can I find taxi information for stations I am travelling to in my journey?</h3>
<p>If you&#8217;d like to take a taxi from or to a station, airport or ferry terminal, and want to book in advance, the Journey Planner may have contact details for taxi or private hire vehicle ("minicab") operators serving that station.&nbsp; To find out, click on the station name on the "Details" page of your journey plan.&nbsp; This will open the "Station information" page, which lists any taxi or private hire car operators known to serve that station.</p>
<h3><a class="QuestionLink" id="A5.2" name="A5.2"></a>5.2) Can I find out if the taxis are wheelchair accessible?</h3>
<p>A wheelchair symbol alongside an operator''s name indicates that some of that operator''s vehicles are wheelchair accessible. We recommend that you check availability and suitability before travelling.</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>
<div></div>
<div></div>
<div></div>', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>5. Tacsis</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTaxi.aspx#A5.1">5.1) Sut y gallaf ddarganfod gwybodaeth am dacsis ar gyfer gorsafoedd rwyf yn teithio iddynt yn ystod fy siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTaxi.aspx#A5.2">5.2) Fedra" i ddarganfod a yw''r tacsis yn hygyrch i gadeiriau olwyn?</a></p>
<p>&nbsp;</p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>5. Tacsis</h2></div>
<br />
<h3><a class="QuestionLink" id="A5.1" name="A5.1"></a>5.1) Sut y gallaf ddarganfod gwybodaeth am dacsis ar gyfer gorsafoedd rwyf yn teithio iddynt yn ystod fy siwrnai?</h3>
<p>Os hoffech gymryd tacsi o neu i orsaf, maes awyr neu derfynnell fferi, ac yn dymuno archebu ymlaen llaw, mae''n bosibl y bydd gan Journey Planner fanylion cyswllt ar gyfer gweithredwyr tacsis neu gerbydau hurio preifat ("minicabiau") sy''n gwasanaethu''r orsaf honno. I ddarganfod, cliciwch ar enw''r orsaf ar dudalen "Manylion" cynllun eich siwrnai. Bydd hyn yn agor y dudalen "Gwybodaeth am orsafoedd", sy''n rhestru unrhyw weithredwyr tacsis neu geir hurio preifat y gwyddys sy''n gwasanaethu''r orsaf honno.</p>
<h3><a class="QuestionLink" id="A5.2" name="A5.2"></a>5.2) Fedra" i ddarganfod a yw''r tacsis yn hygyrch i gadeiriau olwyn?</h3>
<p>Mae symbol cadair olwyn yn ymyl enw gweithredydd yn nodi bod rhai o gerbydau''r gweithredydd hwnnw yn hygyrch i gadaeiriau olwyn. Argymhellwn eich bod yn gwirio a oes un ar gael ac a yw yn addas cyn teithio.</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>'


-------------------------------------------------------------
-- FAQs - Train
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpTrain', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>3. Train</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.1">3.1) How can I find departure times for the stations listed in my journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.2">3.2) How can I find out what stations my train will be stopping at?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.3">3.3) How can I find information about train operators?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.4">3.4) How can I find out what on-board facilities will be available on a train?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.5">3.5) How can I find out if I need to or can make a reservation in advance on a train?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.6">3.6) How can I find out which classes of travel are available on a train?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.7">3.7) How can I find out about station accessibility and facilities on my journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.8">3.8) How can I get a list of all the direct rail services to a destination (including overtaken services), rather than just the fastest ones?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.9">3.9) How do I plan a journey to or from an underground/light rail station?</a></p>
<p>&nbsp;</p>
<div class="hdtypethree">
<h2>3. Train</h2></div>
<br />
<h3><a class="QuestionLink" id="A3.1" name="A3.1"></a>3.1) How can I find departure times for the stations listed in my journey?</h3>
<p>Once you have planned a journey, you can click the station name(s) on the &#8216;Details&#8217; page to see an &#8216;Information&#8217; page about the station.&nbsp; The &#8216;Station information&#8217; page has a &#8216;Departure board&#8217; link that will link you directly to the Departure board for that station.</p>
<h3><a class="QuestionLink" id="A3.2" name="A3.2"></a>3.2) How can I find out what stations my train will be stopping at?</h3>
<p>To find out where your train will be stopping during the journey, you can click the word &#8216;Train&#8217; under the train symbol on the &#8216;Details&#8217; page (or in the &#8216;Transport&#8217; column in the table version of the page).&nbsp; This links to the &#8216;Service information&#8217; page, which lists all the stations that the train stops at during your journey, as well as the stations that it stops at before and after your journey.<br /></p>
<h3><a class="QuestionLink" id="A3.3" name="A3.3"></a>3.3) How can I find information about train operators?</h3>
<p>For information on a train operator, click on the train operator&#8217;s name on the &#8216;Details&#8217; page (or in the &#8216;Instructions&#8217; column in the table version of the page).&nbsp; This links to a page summarising train operator information including contact details.</p>
<h3><a class="QuestionLink" id="A3.4" name="A3.4"></a>3.4) How can I find out what on-board facilities will be available on a train?</h3>
<p>Facilities that are available on board a train are shown on the journey &#8216;Details&#8217; page across from the train journey, or, when you click the word &#8216;Train&#8217; under the train symbol (or in the &#8216;Instructions&#8217; column in the table version of the page).&nbsp;<br /></p>
<h3><a class="QuestionLink" id="A3.5" name="A3.5"></a>3.5) How can I find out if I need to or can make a reservation in advance on a train?</h3>
<p>Some trains allow passengers to reserve seats.&nbsp; Reservations may either be &#8216;available&#8217;, &#8216;recommended&#8217;, or &#8216;compulsory&#8217;.&nbsp; Move your cursor over the symbols, which appear on &#8216;Details&#8217; and &#8216;Service information&#8217; pages for the type of reservation.</p>
<p>&nbsp;</p>
<p>If you would like to contact the train operator, click on the train operator&#8217;s name which links to a page containing train operator information including the telephone number.</p>
<h3><a class="QuestionLink" id="A3.6" name="A3.6"></a>3.6) How can I find out which classes of travel are available on a train?</h3>
<p>There are two types of classes on a train &#8211; Standard and First Class.&nbsp; The &#8216;Details&#8217; and/or &#8216;Service information&#8217; pages will show whether First Class is available and if the service is First Class only. </p>
<p>&nbsp;</p>
<p>If you would like to contact the train operator, click on the train operator&#8217;s name which links to a page summarising train operator information including contact details.<br /></p>
<h3><a class="QuestionLink" id="A3.7" name="A3.7"></a>3.7) How can I find out about station accessibility and facilities at railway stations on my journey?</h3>
<p>You can view journey details by selecting a journey and clicking &#8216;Details&#8217;.&nbsp; You can click the station name(s) on the &#8216;Details&#8217; page to see an &#8216;Information&#8217; page about the station.&nbsp; On this page you can find out about station accessibility and facilities.</p>
<h3><a class="QuestionLink" id="A3.8" name="A3.8"></a>3.8) How can I get a list of all the direct rail services to a destination (including overtaken services), rather than just the fastest ones?</h3>
<p>Use Find a Train or Compare City to City journeys. Our Door to Door Planner only shows the fastest services closest to your specified departure or arrival time.</p>
<h3><a class="QuestionLink" id="A3.9" name="A3.9"></a>3.9) How do I plan a journey to or from an underground/light rail station?</h3>
<p><strong>Step 1:</strong> Click &#8216;door-to-door journey planner&#8217; in the left hand navigation.</p>
<p><strong>Step 2:</strong> If it&#8217;s not already selected, select the &#8216;station/airport&#8217; button.</p>
<p><strong>Step 3:</strong> Enter the name of the underground/light rail station you want in the from or to box.</p>
<p><strong>Step 4:</strong> Click "Next".</p>
<p><strong>Step 5:</strong> Confirm your location.</p><br />
<p>Once you have clicked &#8216;Next&#8217;, if the Journey Planner is not able to find an exact match for the location you typed in, a drop-down list (highlighted in yellow) will show you one or more similar matches for that location.</p>
<p></p><br />
<p>Select a location option from the drop-down list. Please note if the location you want is already visible in the yellow box, you do not need to re-select it.</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>
<div></div>
<div></div>
<div></div>', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="JourneyPlanning" name="JourneyPlanning"></a><h2>3. Tr&#234;n</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.1">3.1) Sut y gallaf ddarganfod amserau ymadael ar gyfer y gorsafoedd a restrwyd yn fy siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.2">3.2) Sut y gallaf ddarganfod pa orsafoedd y bydd fy nhr&#234;n i yn aros ynddynt?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.3">3.3) Sut y gallaf ddarganfod gwybodaeth am weithredwyr trenau?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.4">3.4) Sut y gallaf ddarganfod pa gyfleusterau fydd ar gael ar dr&#234;n?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.5">3.5) Sut y gallaf ddarganfod a oes arnaf angen neu os gallaf archebu lle ymlaen llaw ar dr&#234;n?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.6">3.6) Sut y gallaf ddarganfod pa ddosbarthiadau o deithio sydd ar gael ar dr&#234;n?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.7">3.7) Sut y gallaf ddarganfod am hygyrchedd gorsaf a&#8217;r cyfleusterau sydd ar gael ar fy siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.8">3.8) Sut y gallaf gael rhestr o''r holl wasanaethau rheilffordd uniongyrchol i gyrchfan, yn hytrach na dim ond y rhai cyflymaf?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTrain.aspx#A3.9">3.9) Sut ydw i''n cynllunio siwrnai i orsaf dan ddaear/gorsaf drenau ysgafn neu oddi yno?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>3. Tr&#234;n</h2></div>
<br />
<h3><a class="QuestionLink" id="A3.1" name="A3.16"></a>3.1) Sut y gallaf ddarganfod amserau ymadael ar gyfer y gorsafoedd a restrwyd yn fy siwrnai?</h3>
<p>Cyn gynted ag yr ydych wedi cynllunio siwrnai, gallwch glicio ar enw(au) yr orsaf ar y dudalen &#8216;Manylion&#8217; i weld tudalen &#8216;Gwybodaeth&#8217; am yr orsaf.&nbsp; Mae gan y dudalen &#8216;Gwybodaeth am orsafoedd&#8217; ddolen &#8216;Bwrdd ymadael&#8217; a fydd yn eich cysylltu&#8217;n uniongyrchol &#226;&#8217;r Bwrdd ymadael ar gyfer yr orsaf honno.</p>
<h3><a class="QuestionLink" id="A3.2" name="A3.2"></a>3.2) Sut y gallaf ddarganfod pa orsafoedd y bydd fy nhr&#234;n i yn aros ynddynt?</h3>
<p>I ddarganfod ble bydd eich tr&#234;n yn aros yn ystod y siwrnai, gallwch glicio ar y gair &#8216;Tr&#234;n&#8217; o dan y symbol tr&#234;n ar y dudalen &#8216;Manylion&#8217; (neu yn y golofn &#8216;Cludiant&#8217; yn fersiwn tabl y dudalen). Mae hyn yn cysylltu &#226;&#8217;r dudalen &#8216;Gwybodaeth am wasanaethau&#8217;, sy&#8217;n rhestru&#8217;r holl orsafoedd y mae&#8217;r tr&#234;n yn aros ynddynt yn ystod eich siwrnai, yn ogystal &#226;&#8217;r gorsafoedd y mae&#8217;n aros ynddynt cyn ac ar &#244;l eich siwrnai.<br /></p>
<h3><a class="QuestionLink" id="A3.3" name="A3.3"></a>3.3) Sut y gallaf ddarganfod gwybodaeth am weithredwyr trenau?</h3>
<p>I gael gwybodaeth am weithredydd trenau, cliciwch ar enw&#8217;r gweithredydd trenau ar y dudalen &#8216;Manylion&#8217; (neu yn y golofn &#8216;Cyfarwyddiadau&#8217; yn fersiwn tabl y dudalen). Mae hyn yn cysylltu &#226; thudalen sy&#8217;n crynhoi gwybodaeth am weithredwyr trenau gan gynnwys manylion cyswllt.</p>
<h3><a class="QuestionLink" id="A3.4" name="A3.4"></a>3.4) Sut y gallaf ddarganfod pa gyfleusterau fydd ar gael ar dr&#234;n?</h3>
<p>Dangosir cyfleusterau sydd ar gael ar dr&#234;n ar dudalen &#8216;Manylion&#8217; y siwrnai ar draws oddi wrth siwrnai&#8217;r tr&#234;n, neu pan gliciwch ar y gair &#8216;Tr&#234;n&#8217; o dan y symbol tr&#234;n (neu yn y golofn &#8216;Cyfarwyddiadau&#8217; yn fersiwn tabl y dudalen).<br /></p>
<h3><a class="QuestionLink" id="A3.5" name="A3.5"></a>3.5) Sut y gallaf ddarganfod a oes arnaf angen neu os gallaf archebu lle ymlaen llaw ar dr&#234;n?</h3>
<p>Mae rhai trenau yn caniat&#225;u i deithwyr gadw seddau. Gall cadw seddau fod &#8216;ar gael&#8217;, yn rhywbeth &#8216;a argymhellir&#8217; neu &#8216;yn orfodol&#8217;. Symudwch eich cyrchwr dros y symbolau, sy&#8217;n ymddangos ar y tudalennau &#8216;Manylion&#8217; a &#8216;Gwybodaeth am wasanaeth&#8217; ar gyfer cadw seddau.</p>
<p>&nbsp;</p>
<p>Os dymunwch gysylltu &#226;&#8217;r gweithredydd trenau, cliciwch ar enw&#8217;r gweithredydd trenau sy&#8217;n cysylltu &#226; thudalen sy&#8217;n cynnwys gwybodaeth am weithredwyr trenau gan gynnwys y rhif ff&#244;n.</p>
<h3><a class="QuestionLink" id="A3.6" name="A3.6"></a>3.6) Sut y gallaf ddarganfod pa ddosbarthiadau o deithio sydd ar gael ar dr&#234;n?</h3>
<p>Ceir dau fath o ddosbarth ar dr&#234;n &#8211; Cyffredin a Dosbarth Cyntaf. Bydd y tudalennau &#8216;Manylion&#8217; a/neu &#8216;Gwybodaeth am wasanaeth&#8217; yn dangos a oes Dosbarth Cyntaf ar gael ac a yw&#8217;r gwasanaeth yn un Dosbarth Cyntaf yn unig.</p>
<p>&nbsp;</p>
<p>Os hoffech gysylltu &#226;&#8217;r gweithredydd trenau, cliciwch ar enw&#8217;r gweithredydd trenau sy&#8217;n cysylltu &#226; thudalen sy&#8217;n crynhoi gwybodaeth am y gweithredydd trenau gan gynnwys y manylion cyswllt.<br /></p>
<h3><a class="QuestionLink" id="A3.7" name="A3.7"></a>3.7) Sut y gallaf ddarganfod am hygyrchedd gorsaf a&#8217;r cyfleusterau sydd ar gael ar fy siwrnai?</h3>
<p>Gallwch edrych ar fanylion y siwrnai drwy ddewis siwrnai a chlicio ar &#8216;Manylion&#8217;. Gallwch glicio ar enw(au) yr orsaf ar y dudalen &#8216;Manylion&#8217; i weld tudalen &#8216;Gwybodaeth&#8217; am yr orsaf. Ar y dudalen hon gallwch ddarganfod am hygyrchedd gorsaf a&#8217;r chyfleusterau.</p>
<h3><a class="QuestionLink" id="A3.8" name="A3.8"></a>3.8) Sut y gallaf gael rhestr o''r holl wasanaethau rheilffordd uniongyrchol i gyrchfan, yn hytrach na dim ond y rhai cyflymaf?</h3>
<p>Defnyddiwch siwrneion Canfyddwch Dr&#234;n neu Cymharu Siwrneion Dinas-i-ddinas.&nbsp;NID yw rhestrau llawn o''r holl wasanaethau rheilffordd uniongyrchol ar gael gan ddefnyddio ein Cynlluniwr Drws-i-ddrws sy''n dangos y gwasanaethau cyflymaf sydd agosaf at eich amser ymadael neu gyrraedd penodedig yn unig.</p>
<h3><a class="QuestionLink" id="A3.9" name="A3.9"></a>3.9) Sut ydw i''n cynllunio siwrnai i orsaf dan ddaear/gorsaf drenau ysgafn neu oddi yno?</h3>
<p><strong>Cam 1:</strong> Cliciwch ar "gynlluniwr siwrnai o ddrws-i-ddrws" ar ochr chwith y sgrin</p>
<p><strong>Cam 2:</strong> Os nad yw wedi''i ddewis eisoes, cliciwch ar fotwm "gorsaf/maes awyr"</p>
<p><strong>Cam 3:</strong> Nodwch enw''r orsaf dan ddaear/gorsaf drenau ysgafn o''ch dewis yn y blwch o ac i</p>
<p><strong>Cam 4:</strong> Cliciwch "Nesaf"</p>
<p><strong>Cam 5:</strong> Cadarnhewch eich lleoliad</p><br />
<p>Wedi i chi glicio "Nesaf", os nad yw Journey Planner yn gallu dod o hyd i gyfatebiaeth union ar gyfer y lleoliad y bu i chi ei deipio, bydd rhestr a ollyngir i lawr (a amlygir mewn melyn) yn dangos un neu fwy o gyfatebiaethau tebyg i chi ar gyfer y lleoliad hwnnw.</p>
<p></p><br />
<p>Dewiswch leoliad o''r rhestr a ollyngir i lawr. Nodwch os yw''r lleoliad o''ch dewis i''w weld yn y blwch melyn, nid oes yn rhaid i chi ei ail-ddewis.</p>
<p>&nbsp;&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>'



-------------------------------------------------------------
-- FAQs - Using
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpUsing', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="Printing" name="Printing"></a><h2>13. Using the website</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.1">13.1)&nbsp;What browsers does the website work best on?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.2">13.2) Can I print information from the website?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.3">13.3) How can I save my travel preferences and my favourite journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.4">13.4) Can I bookmark certain journeys (save as favourites)?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.5">13.5) Why should I register on the Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.6">13.6) How can I email information to other people?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.7">13.7) What is the purpose of the Journey Planner Toolbar?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.8">13.8) How can I add a Journey Planner link to my website?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.9">13.9) Do I need to have JavaScript enabled to use the Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.10">13.10) Is the Journey Planner an accessible website?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.11">13.11) Why is my company browser blocking some the Journey Planner functionality?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.12">13.12) What is social bookmarking?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>13. Using the website</h2></div>
<br />
<h3><a class="QuestionLink" id="A13.1" name="A13.1"></a>13.1)&nbsp;What browsers&nbsp;does the website work best on?</h3>
<p>The Transport Direct website is designed to support the following browsers: Internet Explorer versions 6, 7 and 8, Firefox 3, Safari 4 and Chrome 9. Some of the interactive services may require you to switch on javascript and cookies in your browser for them to work as designed.</p>
<p>&nbsp;</p>
<p>If you are not using one of the browsers above you should be able to access the site and use the pages', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="Printing" name="Printing"></a><h2>13. Defnyddio''r wefan</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.1">13.1) Pa borwyr y mae''r wefan yn gweithio orau arnynt?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.2">13.2) Alla&#8217; i argraffu gwybodaeth o&#8217;r wefan?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.3">13.3) Sut y gallaf gadw fy hoffterau teithio a&#8217;m hoff siwrneion?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.4">13.4) Alla&#8217; i roi roi nod llyfr ar rai siwrneion (eu cadw fel ffefrynnau)?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.5">13.5) Pam y dylwn gofrestru ar Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.6">13.6) Sut y gallaf ebostio gwybodaeth i bobl eraill?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.7">13.7) A yw Journey Planner yn wefan hygyrch?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.8">13.8) Sut y gallaf ychwanegu cyswllt Journey Planner at fy ngwefan?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.9">13.9) A oes angen i mi fod &#226; JavaScript wedi ei alluogi i ddefnyddio Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.10">13.10) A yw Journey Planner yn wefan hygyrch?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.11">13.11) Pam fod porwr fy nghwmni yn rhwystro rhai swyddogaethau Journey Planner?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpUsing.aspx#A13.12">13.12) Beth yw llyfrnodi cymdeithasol?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>13. Defnyddio''r wefan</h2></div>
<h3><a class="QuestionLink" id="A13.1" name="A13.1"></a>13.1) Pa borwyr y mae''r wefan yn gweithio orau arnynt?</h3>
<p>Cynlluniwyd gwefan Transport Direct i gefnogi’r porwyr canlynol: Internet Explorer fersiynau 6, 7 ac 8, Firefox 3, Safari 4 ac Chrome 9. Efallai bydd rhai o’r gwasanaethau rhyngweithiol yn gofyn ichi roi javascript ymlaen a chaniatáu cwcis yn eich porwr er mwyn iddynt weithio yn briodol.</p>
<p>&nbsp;</p>
<p>Os nad ydych yn defnyddio un o’r porwyr uchod dylech allu cael mynediad i’r safle a defnyddio’r tudalennau'
--DECLARE @ptrValText BINARY(16)

-- Update text field: Value-En
SET @ptrValText = NULL
SELECT @ptrValText = TEXTPTR([Value-En]) FROM [dbo].[tblContent] WHERE themeid = @ThemeId and groupid = 19 and controlname ='Body Text' and propertyname = '/Channels/TransportDirect/Help/HelpUsing'

IF @ptrValText IS NOT NULL BEGIN

   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 ' but it might not display the site as designed, nor provide you with the best experience of using the website.</p>
<p>&nbsp;</p>
<p>Please note that the site does not support the use of Internet Explorer on Macs.&nbsp; We recommend Mac users use Macintosh Safari.&nbsp;</p>
<p>&nbsp;</p>
<p>Accessing Transport Direct in more than one window within the same browser session may cause a serious error.&nbsp; In order to view Transport Direct in more than one window please open a new browser session rather than opening a new window (or tab) within the same browser.&nbsp;</p>
<p>&nbsp;</p>
<br />
<h3><a class="QuestionLink" id="A13.2" name="A13.2"></a>13.2) Can I print information&nbsp;from the website?</h3>
<p>Yes, you can print any page on the website.</p>
<p></p>
<p>Some pages will have a &#8216;Printer friendly&#8217; button in the top right corner of the page:&nbsp;</p>
<p>&nbsp;</p>
<ul class="listerdisc">
<li>Click &#8216;Printer friendly&#8217; and a new browser window will open with the &#8216;printer friendly&#8217; version of the page you would like to print&nbsp; </li>
<li>Click the printer icon on your browser or go to &#8216;File&#8217; and &#8216;Print&#8217; on your browser to print the page. Close the window by clicking the &#8216;x&#8217; in the top right corner of the window</li></ul>
<p>&nbsp;</p>
<p>The printer friendly version of a page showing a map will print more clearly than the normal web page.</p>
<p>&nbsp;</p>
<p>On printing the journey details the "itinerary line" may fail to appear on the printout. This issue occurs because the default settings on Internet Explorer and Firefox disable the printing of background images and coloured backgrounds. To solve this issue the default settings can be changed manually in both browsers.</p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A13.3" name="A13.3"></a>13.3) How can I save my travel preferences and my favourite j'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'ourneys?</h3>
<p>When you are logged in and using the "Door-to-door" planner, you can save travel preferences and favourite journey requests.</p><br />
<p><strong>To save travel preferences:</strong></p>
<ul>
<li>Tick the &#8216;Save these details&#8217; box across from &#8216;Travel preferences&#8217;. This saves the details you enter in this section so that you don&#8217;t have to re-enter your preferences each time you plan a journey. </li></ul><br />
<p><strong>To save your favourite Door-to-door&nbsp;journey:</strong></p>
<ul class="listerdisc">
<li>If you would like to save a Door-to-door journey request so that you can look up the results again without entering all the details each time, first plan the journey, then click the &#8216;Save as a favourite journey&#8217; tab on the results page </li>
<li>Then save the journey - either under the name the website gives it (i.e. Journey 1) or under a name you have chosen - and click &#8216;OK&#8217; </li>
<li>Once you have clicked &#8216;OK&#8217; the journey request will be saved </li></ul><br />
<p>The next time you are on the &#8216;Door-to-door&#8217; journey planner you can select saved journey requests in the &#8216;Favourite journeys&#8217; drop-down list at the top of the page and then click &#8216;OK&#8217; to get up-to-date results.</p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A13.4" name="A13.4"></a>13.4) Can I bookmark certain journeys (save as favourites)?</h3>
<p>Yes, using Internet Explorer, you can bookmark journey requests (not including "Advanced options") so that they are saved in your "Favourites" list even if you are not registered with the Journey Planner. You can do this by clicking the "Bookmark this journey for the future" link. This is useful if you make certain journeys frequently and don''t want to enter all the details for them each time you use the Journey Planner.</p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A13.5" name="A13.5"></a>13.5) Why should I register on Trans'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'port Direct?</h3>
<p>Registering on the Journey Planner enables you to login so that you can:</p>
<ul>
<li>Save your favourite journeys so that you can access them in future without having to re-enter any information </li>
<li>Save travel preferences when entering information for a journey so that you won&#8217;t have to enter them again &#8211; but you can change them if you wish </li>
<li>Email travel information to other people</li></ul><br />
<p>You can remove your registration details at any time on the Contact Transport Direct page.To do so, select "Other" from the "I have feedback..." drop-down list, type "unregister" followed by your registered email address in the "Details" box and click "Submit". Your registration details will then be removed from our system. We would appreciate it if you could let us know why you wish to remove your registration - by typing this into the "Details" section too.</p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A13.6" name="A13.6"></a>13.6) How can I email information to other people?</h3>
<p>When you are logged in you can email selected journey information to other people. (To log in you need to be a registered user.)</p><br />
<p>To send other people the journey information:</p>
<ul>
<li>Click the &#8216;Send to a friend&#8217; tab at the bottom of a results page </li>
<li>Type in the email address of the person you wish to send the information to </li>
<li>Click &#8216;Send&#8217;.</li></ul><br />
<p>An email containing the selected journey information as well as a map if applicable will be sent to that email address.</p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A13.7" name="A13.7"></a>13.7) What is the purpose of the Journey Planner Toolbar?</h3>
<p>The Journey Planner Toolbar puts the resources of the Journey Planner on your browser, giving you easier access to the travel information you need. </p>
<p>&nbsp;</p>
<p>The toolbar gives you a fast track into planning journeys or accessing the main feature'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 's of the Journey Planner. To plan a journey just enter the postcodes to travel to and from into the Toolbar and click "Go". The journey request will be processed by the Journey Planner and the results will be shown in a new window.</p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A13.8" name="A13.8"></a>13.8) How can I add a Journey Planner link to my website?</h3>
<p>Click the "Add Journey Planner to your website" link on the "Tips and Tools" page if you would like to add a journey planning feature to your website. This feature will allow your customers and visitors to plan a journey to your premises simply by entering the postcode they are travelling from.</p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A13.9" name="A13.9"></a>13.9) Do I need to have JavaScript enabled to use the Journey Planner?</h3>
<p>All the core functionality of the Journey Planner is available if you do not have JavaScript enabled.&nbsp; However, we recommend enabling JavaScript if possible, as this will give you some additional features and a better user experience.</p>
<p>&nbsp;</p>
<p>Without JavaScript enabled you may find some of the links to other pages within the Journey Planner web site do not work.&nbsp; You can avoid this problem if you click &#8216;Non-Javascript Version&#8217; in the bar beneath the tabs.&nbsp;The system will then display &#8216;Go&#8217; buttons next to these links to enable them to be activated.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A13.10" name="A13.10"></a>13.10) Is the Journey Planner an accessible website?</h3>
<p>The Journey Planner is committed to providing usable and accessible information. Several areas of the site have been given particular attention so as to make them increasingly accessible: </p>
<p>&nbsp;</p>
<p><strong>Design</strong></p>
<ul class="listerdisc">
<li>The site has been designed for 1024 x 768 screen resolutions to avoid horizontal scrolling on those sized screens </li>
<li>This site uses cascading style shee'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'ts for layout </li>
<li>This site uses only relative font sizes, compatible with the user-specified "text size" option in visual browsers </li>
<li>If your browser or browsing device does not support stylesheets at all, the content of each page is still readable </li>
<li>Many of the diagrams that exist on the portal can also be viewed in a table format.</li></ul>
<p>&nbsp;</p>
<p><strong>Links</strong></p>
<p>Unless the wording in the link already fully describes the target (the page it''s linking to), title attributes that describe the link in greater detail have been added.</p>
<p><strong></strong>&nbsp;</p>
<p><strong>Tab indexing<br /></strong>If you do not have a mouse or prefer to use your keyboard when using the Internet, you can jump from link to link using the tab key on your keyboard. When using a screenreader, the main and sub-navigation can be skipped in order to jump to the content of the pages.</p>
<p><strong></strong>&nbsp;</p>
<p><strong>Images</strong><br />In order to give you more information about an image on the site, all images include descriptive ALT attributes.&nbsp;This is useful for users with screenreaders as it give images meaningful descriptions. Purely decorative graphics include null (empty) ALT attributes. </p>
<p><strong></strong>&nbsp;</p>
<p><strong>Adobe Acrobat<br /></strong>Within the Journey Planner website you will need Adobe''s Acrobat Reader to view some of the Network maps which are in the form of PDF files. If you don''t already have it installed, there is an icon on the "Network maps" page linking to Acrobat Reader download page on the Adobe website.&nbsp; </p>
<p><strong></strong>&nbsp;</p>
<p><strong>Session timeout<br /></strong>When you log on as a registered user, you can extend the session timeout limit to make sure you have enough time to complete any actions within the site.  Once you are logged in, go to the ''logout / update'' tab, select ''user preferences'', tick the box to extend session timeout and press ''go''.  The session timeout will then change from the default 30 minutes to 5 hours.</p>
<p><br />The Journey Planner website currently conforms to level AA of the Web Accessibility Guidelines (WAI) produced by the World Wide Web Consortium (W3C).</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A13.11" name="A13.11"></a>13.11)&nbsp;Why is my company browser blocking some Journey Planner functionality?</h3>
<p>Many companies now employ third party Web Securi'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'ty Gateway applications to protect their employees from potentially malicious web sites. Some of the methods that these applications use to determine whether or not a site poses a threat can be quite unsophisticated. For example, long web addresses or large file sizes may be considered as indications of a possible threat. As the Journey Planner is a complex site, it often relies on passing large amounts of data across to users in order to display the complex journey plans requested. We have found that, in some cases, this can trigger alerts in the third party software, and some users have found themselves unable to access parts of the site.</p>
<p>&nbsp;</p>
<p>In these circumstances, the user will typically receive an error message from their company&#8217;s information technology support centre telling them that the page has been blocked. If you experience such a problem we ask that you contact your own company IT department to ask them to allow access to the www.transportdirect.info domain. They will then be able to place this site on an allowed list which should remove the issue for all your company&#8217;s employees. We also ask that you submit some feedback to Transport Direct, so that we may contact the third party supplier to ask that they also add us to their generic allowed domain list.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A13.12" name="A13.12"></a>13.12)&nbsp;What is social bookmarking?</h3>
<p>Social bookmarking is a method for Internet users to store, organize, search, and manage bookmarks of web pages on the Internet with the help of metadata.</p>
<p>&nbsp;</p>
<p>Transport Direct supports links to a number of social bookmarking sites to enable users to save and share journeys users have planned so that these journeys can be easily shared and recreated.</p>
<p>&nbsp;</p>
<p>Some social bookmarking sites have a URL limit of 255 characters; if this limit is breached they will be unable to save the URL.</p>
<p>&nbsp;</p>
</div></div>
<div></div>
<div></div>
<div></div>
<div></div>'

END


-- Update text field: Value-Cy
SET @ptrValText = NULL
SELECT @ptrValText = TEXTPTR([Value-Cy]) FROM [dbo].[tblContent] WHERE themeid = @ThemeId and groupid = 19 and controlname ='Body Text' and propertyname = '/Channels/TransportDirect/Help/HelpUsing'

IF @ptrValText IS NOT NULL BEGIN

   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 ' ond efallai na fydd yn arddangos y safle yn briodol, nac yn rhoi’r profiad gorau o ddefnyddio’r wefan ichi.</p>
<p>&nbsp;</p>
<p>Please note that the site does not support the use of Internet Explorer on Macs.&nbsp; We recommend Mac users use Macintosh Safari.&nbsp;</p>
<p>&nbsp;</p>
<p>Accessing Transport Direct in more than one window within the same browser session may cause a serious error.&nbsp; In order to view Transport Direct in more than one window please open a new browser session rather than opening a new window (or tab) within the same browser.&nbsp;</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A13.2" name="A13.2"></a>13.2) Alla&#8217; i argraffu gwybodaeth o&#8217;r wefan?</h3>
<p>Gallwch, gallwch argraffu unrhyw dudalen ar y wefan.</p>
<p></p>
<p>Bydd gan rai tudalennau fotwm "Hawdd ei argraffu" yng nghornel llaw dde uchaf y dudalen:</p>
<p>&nbsp;</p>
<ul class="lister">
<li>Cliciwch ar "Hawdd ei argraffu" a bydd ffenestr pori newydd yn agor gyda fersiwn "cymwys i argraffydd" o''r dudalen yr hoffech ei hargraffu&nbsp; </li>
<li>Cliciwch ar eicon yr argraffydd ar eich porwr neu ewch i "Ffeil" ac "Argraffwch" ar eich porwr i argraffu''r dudalen. Caewch y ffenestr drwy glicio ar yr "x" yng nghornel llaw dde uchaf y ffenestr.</li></ul>
<p>&nbsp;</p>
<p>Bydd fersiwn hawdd ei hargraffu yn dangos map yn argraffu yn gliriach na thudalen arferol y we.</p>
<p>&nbsp;</p>
<p>Wrth argraffu manylion y siwrnai mae''n bosibl na fydd y "llinell daith" yn ymddangos ar yr allbrint. Digwydd y broblem hon oherwydd bod gosodiadau arferol Internet Explorer a Firefox yn dialluogi argraffu delweddau cefndir a chefndiroedd lliw. Er mwyn datrys y broblem hon gellir newid y gosodiadau arferol &#226; llaw yn y ddau borwr.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A13.3" name="A13.3"></a>13.3) Sut y gallaf gadw fy hoffterau teithio a&#8217;m hoff siwrneion?'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 '</h3>
<p>Pan ydych wedi eich logio i mewn ac yn defnyddio''r cynlluniwr "Drws i ddrws" gallwch gadw hoffterau teithio a cheisiadau am hoff siwrneion.</p><br />
<p><strong>I gadw hoffterau teithio:</strong></p>
<ul>
<li>Ticiwch y blwch &#8216;Cadwch y manylion hyn&#8217; sydd ar draws oddi wrth &#8216;Hoffterau teithio&#8217;. Mae hyn yn cadw&#8217;r manylion a rowch yn yr adran hon fel nad oes angen i chi ail-deipio eich hoffterau i mewn bob tro y cynlluniwch siwrnai. </li></ul><br />
<p><strong>I gadw eich hoff siwrnai o Ddrws-i-ddrws:</strong></p>
<ul>
<li>Os hoffech gadw cais am siwrnai o Ddrws-i-ddrws fel y gallwch edrych ar y canlyniadau eto heb roi&#8217;r holl fanylion i mewn bob tro, mae angen i chi gynllunio&#8217;r siwrnai yn gyntaf, yna cliciwch y tab &#8216;Cadwch fel hoff siwrnai&#8217; ar y dudalen canlyniadau. </li>
<li>Yna cadwch y siwrnai - naill ai o dan yr enw y mae''r wefan yn ei rhoi iddi (h.y. Siwrnai 1) neu o dan enw yr ydych wedi ei ddewis - a chliciwch ar "Iawn". </li>
<li>Wedi i chi glicio &#8216;Iawn&#8217; bydd y cais am y siwrnai yn cael ei gadw. </li></ul><br />
<p>Y tro nesaf rydych ar y cynlluniwr siwrnai &#8216;Drws-i-ddrws&#8217; gallwch ddewis y ceisiadau am y siwrneion a gadwyd yn y rhestr a ollyngir i lawr o&#8217;r &#8216;Hoff siwrneion&#8217; ar frig y dudalen ac yna cliciwch &#8216;Iawn&#8217; i gael canlyniadau diweddaraf.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A13.4" name="A13.4"></a>13.4) Alla&#8217; i roi roi nod llyfr ar rai siwrneion (eu cadw fel ffefrynnau)?</h3>
<p>Gallwch, gan ddefnyddio Internet Explorer, gallwch roi nod llyfr ar geisiadau am siwrneion (heb gynnwys &#8216;Dewisiadau mwy cymhleth&#8217;) fel eu bod yn cael eu cadw yn eich rhestr &#8216;Ffefrynnau&#8217; hyd yn oed os nad ydych wedi cofrestru gyda Journey Planner. Gallwch wneud hyn drwy glicio ar y ddolen &#8216;Rhowch nod llyfr ar y siwrnai hon ar gyfer y dyfodol&#8217;. Mae hyn yn ddefnyddiol os ydych yn gwneud rhai siwrneion yn aml ac nad y'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 'dych yn dymuno cofnodi&#8217;r holl fanylion ar eu cyfer bob tro yr ydych yn defnyddio Journey Planner.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A13.5" name="A13.5"></a>13.5) Pam y dylwn gofrestru ar Journey Planner?</h3>
<p>Mae cofrestru ar Journey Planner yn galluogi i chi logio i mewn fel y gallwch:</p>
<ul>
<li>Gadw eich hoff siwrneion fel y gallwch gyrchu atynt yn y dyfodol heb orfod ail-deipio unrhyw wybodaeth </li>
<li>Cadw hoffterau teithio wrth roi gwybodaeth am siwrnai fel na fydd yn rhaid i chi eu hail-deipio &#8211; ond gallwch eu newid os dymunwch </li>
<li>Ebostio gwybodaeth am deithio i bobl eraill.</li></ul>
<p><br /></p>
<p>Gallwch ddileu eich manylion cofrestru unrhyw bryd ar y dudalen Cysylltwch &#226; ni. I wneud hynny, dewiswch "Arall" o''r rhestr a ollyngir i lawr "Mae gennyf adborth...", teipiwch "dadgofrestru" ac yna eich cyfeiriad ebost cofrestredig yn y blwch "Manylion" a chliciwch "Cyflwyno". Yna bydd eich manylion cofrestru yn cael eu dileu o''n system. Byddem yn ddiolchgar pe gallech roi gwybod i ni pam y dymunwch ddileu eich cofrestriad - drwy deipio hyn yn yr adran "Manylion" hefyd. </p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A13.6" name="A13.6"></a>13.6) Sut y gallaf ebostio gwybodaeth i bobl eraill?</h3>
<p>Pan ydych wedi logio i mewn gallwch ebostio gwybodaeth am siwrneion detholedig i bobl eraill. (I logio i mewn mae angen i chi fod yn ddefnyddiwr cofrestredig).</p><br />
<p>I anfon gwybodaeth am y siwrnai i bobl eraill:</p>
<ul>
<li>Cliciwch y tab &#8216;Anfonwch at ffrind&#8217; ar waelod y dudalen canlyniadau. </li>
<li>Teipiwch gyfeiriad ebost y sawl y dymunwch anfon yr wybodaeth ato. </li>
<li>Cliciwch &#8216;Anfon&#8217;.</li></ul><br />
<p>Anfonir ebost yn cynnwys yr wybodaeth am y siwrnai ddetholedig yn ogystal &#226; map os yn berthnasol i&#8217;r cyfeiriad ebost hwnnw.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A13.7" name="A13.7"></a>13.7) Beth yw pwrpas Bar Offer Journey Planner?</h3>
<p>Mae Bar Offe'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 'r Journey Planner yn rhoi adnoddau Journey Planner ar eich porwr, gan roi mynediad haws at y wybodaeth teithio yr ydych ei hangen.</p>
<p>&nbsp;</p>
<p>Mae''r bar offer yn rhoi llwybr cyflym at gynllunio siwrneion neu gyrchu at brif nodweddion Journey Planner. I gynllunio siwrnai rhowch y codau post i deithio iddynt ac ohonynt i''r Bar Offer a chliciwch "Ewch". Bydd cais y siwrnai yn cael ei brosesu gan Journey Planner a dangosir y canlyniadau mewn ffenestr newydd.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A13.8" name="A13.8"></a>13.8) Sut y gallaf ychwanegu cyswllt Journey Planner at fy ngwefan?</h3>
<p>Cliciwch y cyswllt "Ychwanegwch Journey Planner at eich gwefan" ar y dudalen "Awgrymiadau a theclynnau" os hoffech ychwanegu nodwedd cynllunio siwrnai at eich gwefan. Bydd y nodwedd hon yn caniatau i gwsmeriaid ac ymwelwyr gynllunio siwrnai i''ch adeilad drwy wneud dim mwy na rhoi''r c&#244;d post y maent yn teithio ohono.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A13.9" name="A13.9"></a>13.9) A oes angen i mi fod &#226; JavaScript wedi ei alluogi i ddefnyddio Journey Planner?</h3>
<p>Bydd swyddogaethau craidd Journey Planner i gyd ar gael i chi os nad yw JavaScript wedi ei alluogi.&nbsp; Ond argymhellwn y dylech alluogi JavaScript os yn bosib, oherwydd bydd hyn yn rhoi rhai nodweddion ychwanegol i chi a gwell profiad i''r defnyddiwr.</p>
<p>&nbsp;</p>
<p>Heb alluogi JavaScript efallai na fydd rhai o''r dolennau i dudalennau eraill o fewn gwefan Journey Planner yn gweithio.&nbsp; Gallwch osgoi''r broblem hon os cliciwch ar "Edrychwch ar dudalen heb fod yn gysylltiedig &#226; Javascript" yn y bar o dan y tabiau.&nbsp;Yna bydd y system yn dangos botymau "Ewch" yn ymyl y dolennau hyn i''w galluogi i weithio.</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A13.10" name="A13.10"></a>13.10) A yw Journey Planner yn wefan hygyrch?</h3>
<p>Mae Journey Planner wedi''i ymrwymo i ddarparu gwybodaeth ddefnyddiol a hygyrch. Mae nifer o ardaloedd a'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 'r y safle wedi derbyn sylw arbennig i''w gwneud yn fwy hygyrch: </p>
<p><strong>Cynllun</strong></p>
<ul>
<li>Mae''r safle wedi''i gynllunio ar gyfer cydraniadau sgr&#238;n 1024 x 768 er mwyn osgoi sgrolio llorweddol ar y sgriniau o''r maint hynny </li>
<li>Mae''r safle hwn yn defnyddio taflenni steil rhaeadru fel cynllun </li>
<li>Mae''r safle hwn ond yn defnyddio maint print cymharol, sy''n gyt&#251;n &#226; opsiwn "maint testun" a benodwyd gan y defnyddiwr mewn porwr gweledol </li>
<li>Os nad yw''ch porwr neu ddyfais pori yn cefnogi taflenni steil o gwbl, mae cynnwys pob tudalen yn dal i fod yn ddarllenadwy </li>
<li>Gellir gweld llawer o&#8217;r diagramau sydd ar y porth ar ffurf tabl hefyd.</li></ul>
<p><strong></strong>&nbsp;</p>
<p><strong>Dolenni</strong></p>
<p>Heblaw bod y geiriad yn y ddolen yn disgrifio''r targed yn llawn (y dudalen mae''n cysylltu iddi), mae priodoleddau sy''n disgrifio''r ddolen yn fanylach wedi''u hychwanegu. </p>
<p><strong></strong>&nbsp;</p>
<p><strong>Mynegeio tab <br /></strong>Os nad oes gennych lygoden neu os oes yn well gennych ddefnyddio''r allweddell wrth ddefnyddio''r Rhyngrwyd, gallwch neidio o ddolen i ddolen yn defnyddio''r allwedd tab ar eich allweddell. Wrth ddefnyddio darllenwr sgr&#238;n, gellir anwybyddu''r prif a''r is lywio er mwyn neidio i gynnwys y tudalennau.</p>
<p><strong></strong>&nbsp;</p>
<p><strong>Delweddau</strong><br />Er mwyn rhoi mwy o wybodaeth i chi ynglyn &#226; delwedd ar y safle, mae''r delweddau i gyd yn cynnwys priodoleddau amgen disgrifiadol. Mae hyn yn ddefnyddiol i ddefnyddwyr gyda darllenwyr sgr&#238;n gan ei fod yn rhoi disgrifiadau ystyrlon i ddelweddau. Mae graffegau sydd at ddiben addurniadol yn unig yn cynnwys priodoleddau amgen nwl (gwag). </p>
<p><strong></strong>&nbsp;</p>
<p><strong>Adobe Acrobat<br /></strong>O fewn y wefan Journey Planner byddwch angen Adobe''s Acrobat Reader i weld rhai o fapiau''r Rhwydwaith sydd ar ffurf ffeiliau PDF. Os nad oes gennych Adobe Acrobat wedi''i arsefydlu eisoes, ceir eicon ar'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 ' dudalen "Mapiau rhwydwaith" sy''n cysylltu &#226; thudalen llwytho i lawr Acrobat Reader ar wefan Adobe. </p>
<p><strong></strong>&nbsp;</p>
<p><strong>Session timeout<br /></strong>When you log on as a registered user, you can extend the session timeout limit to make sure you have enough time to complete any actions within the site.  Once you are logged in, go to the ''logout / update'' tab, select ''user preferences'', tick the box to extend session timeout and press ''go''.  The session timeout will then change from the default 30 minutes to 5 hours.</p>
<p><br />Mae gwefan Journey Planner ar hyn o bryd yn cydymffurfio &#226; Lefel AA Canllawiau Hygyrchedd y We (WAI) a gynhyrchwyd gan y Consortiwm y We Fyd-eang (W3C).</p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A13.11" name="A13.11"></a>13.11)&nbsp;Pam fod porwr fy nghwmni yn rhwystro rhai swyddogaethau Journey Planner?</h3>
<p>Mae llawer o gwmn&#239;au bellach yn defnyddio cymwysiadau Web Security Gateway trydydd parti i ddiogelu eu gweithwyr rhag gwefannau a allai fod yn faleisus. Gall rhai o''r dulliau y mae''r cymwysiadau hyn eu defnyddio i bennu a yw safle yn fygythiad ai peidio fod yn ansoffistigedig. Er enghraifft, gallai cyfeiriadau''r we sy''n hir neu ffeiliau mawr gael eu hystyried yn arwyddion o fygythiad posibl. Gan fod Journey Planner yn safle cymhleth, mae''n aml yn dibynnu ar basio symiau mawr o ddata draw at ddefnyddwyr er mwyn dangos y cynlluniau siwrneion cymhleth y gofynnir amdanynt. Rydym wedi gweld bod hyn, mewn rhai achosion, yn gallu sbarduno rhybuddion yn y meddalwedd trydydd parti, ac mae rhai defnyddwyr wedi profi anawsterau wrth gael mynediad at rannau o''r safle. </p>
<p>&nbsp;</p>
<p>O dan yr amgylchiadau hyn, bydd y defnyddiwr fel arfer yn derbyn neges wall o ganolfan cefnogi technoleg gwybodaeth eu cwmni yn dweud wrthynt fod y dudalen wedi''i rhwystro. Os ydych chi''n profi problem o''r fath rydym yn gofyn i chi gysylltu ag adran TG eich cwmni er mwyn gofyn iddynt ganiat&#225;u mynediad i''r parth www.transportdirect.info. Yna byddant yn gallu gosod y safle hwn ar restr a ganiateir a dylai hynny gael gwared ar y broblem ar gyfer holl weithwyr eich cwmni. Rydym yn gofyn i chi hefyd gyflwyno peth adborth i Journey Planner, er mwyn i ni allu cysylltu &#226;"r cyflenwr trydydd parti er mwyn gofyn iddynt hwy hefyd ychwanegu ein cyfeiriad at eu rhestr parthau cyffredinol a ganiateir. </p>
<p>&nbsp;</p>
<h3><a class="QuestionLink" id="A13.12" name="A13.12"></a>13.12)&nbsp;Beth yw llyfrnodi cymdeithasol?</h3>
<p>Dull ar gyfer defnyddwyr y Rhyngrwyd yw llyfrnodi cymdeithasol, i storio, trefnu, chwilio a rheoli llyfrnodau gwedudalennau ar y Rhyngrwyd gyda chymorth metadata.</p> 
<p>&nbsp;</p>
<p>Mae Transport Direct yn cefnogi cysylltiadau â nifer o safleoedd llyfrnodi cymdeithasol fel y gall defnyddwyr arbed a rhannu teithiau a gynlluniwyd gan ddefnyddwyr fel y gellir rhannu ac ailgreu''r teithiau hyn yn rhwydd.</p> 
<p>&nbsp;</p>
<p>Mae gan rai safleoedd llyfrnodi uchafswm URL o 255 nod; os byddwch yn mynd dros y nifer hwn ni fyddant yn gallu cadw''r URL.</p>
<p>&nbsp;</p>
<div align="right"></div>
'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 '

<p>&nbsp;</p></div></div>
<div></div>'

END



-------------------------------------------------------------
-- FAQs - Transport Direct Tourist Information
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpTouristInfo', 
'<div id="primcontent">
	<div id="contentarea">
		<div class="hdtypethree"><a id="TouristInformation" name="TouristInformation"></a><h2>17. Transport Direct Tourist Information</h2>
		</div>
		<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTouristInfo.aspx#A17.1">17.1)&nbsp;How can 
				I find travel information when visiting from abroad?</a></p>
		<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTouristInfo.aspx#A17.2">17.2)&nbsp;How can 
				I find tourist information when visiting from abroad?</a></p>
		<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTouristInfo.aspx#A17.3">17.3)&nbsp;Can I 
				book tickets on Transport Direct from abroad?</a></p>
		<p>&nbsp;</p>
		<div class="hdtypethree">
			<h2>17. Transport Direct Tourist Information</h2>
		</div>
			<h3><a class="QuestionLink" id="A17.1" name="A17.1"></a>17.1)&nbsp;How can I find travel 
					information when visiting from abroad?</h3>
		<p>Transport Direct currently covers travel across Great Britain (England, Wales 
            and Scotland); you can plan your GB journey on Transport Direct from anywhere 
			around the world.</p>
		<h3><a class="QuestionLink" id="A17.2" name="A17.2"></a>17.2)&nbsp;How can I find tourist 
				information when visiting from abroad?</h3>
		<p>Information on tourist destinations and accommodation in Britain can be found at <a href="http://www.visitbritain.com" target="_child">
				www.visitbritain.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>.</p>
		<h3><a class="QuestionLink" id="A17.3" name="A17.3"></a>17.3)&nbsp;Can I book tickets on 
				Transport Direct from abroad?</h3>
		<p>Yes, you can book tickets from abroad through our retail partners but tickets 
			may have to be collected at the relevant departure station.</p>
		<p>&nbsp;</p>
	</div>
</div>',
'<div id="primcontent">
	<div id="contentarea">
		<div class="hdtypethree"><a id="TouristInformation" name="TouristInformation"></a><h2>17. Gwybodaeth i Dwristiaid - Transport Direct</h2>
		</div>
		<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTouristInfo.aspx#A17.1">17.1)&nbsp;Sut allaf i ddarganfod gwybodaeth teithio pan fyddaf yn ymweld â''r wlad o dramor?</a></p>
		<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTouristInfo.aspx#A17.2">17.2)&nbsp;Sut allaf i ddarganfod gwybodaeth i dwristiaid pan fyddaf yn ymweld â''r wlad o dramor?</a></p>
		<p class="rsviewcontentrow"><a href="/Web2/Help/HelpTouristInfo.aspx#A17.3">17.3)&nbsp;Allaf i archebu tocynnau ar Transport Direct o dramor?</a></p>
		<p>&nbsp;</p>
		<div class="hdtypethree">
			<h2>17. Gwybodaeth i Dwristiaid - Transport Direct</h2>
		</div>
			<h3><a class="QuestionLink" id="A2" name="A17.1"></a>17.1)&nbsp;Sut allaf i ddarganfod gwybodaeth teithio pan fyddaf yn ymweld â''r wlad o dramor?</h3>
		<p>Ar hyn o bryd mae Transport Direct yn cwmpasu Prydain Fawr i gyd (Cymru, Lloegr a''r Alban); 
		gallwch gynllunio eich taith ym Mhrydain ar Transport Direct o unrhyw fan yn y byd.</p>
		<h3><a class="QuestionLink" id="A3" name="A17.2"></a>17.2)&nbsp;Sut allaf i ddarganfod gwybodaeth i dwristiaid pan fyddaf yn ymweld â''r wlad o dramor?</h3>
		<p>Gellir dod o hyd i wybodaeth am gyrchfannau twristaidd a llety ym Mhrydain yn <a href="http://www.visitbritain.com" target="_child">
				www.visitbritain.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>.</p>
		<h3><a class="QuestionLink" id="A4" name="A17.3"></a>17.3)&nbsp;Allaf i archebu tocynnau ar Transport Direct o dramor?</h3>
		<p>Gallwch. Mae''n bosibl archebu tocynnau o dramor drwy ein partneriaid manwerthu ond efallai y bydd yn rhaid mynd i nôl y tocynnau i''r orsaf y byddwch yn teithio ohoni.</p>
		<p>&nbsp;</p>
		<p>&nbsp;</p>
	</div>
</div>'



-------------------------------------------------------------
-- FAQs - Cycle Planning
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpCycle', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="CyclePlanning" name="CyclePlanning"></a><h2>16. Cycle Planning</h2></div>
<p class="rsviewcontentrow"><a href="#A16.1">16.1)&nbsp;Does Transport Direct offer cycling options?</a></p>
<p class="rsviewcontentrow"><a href="#A16.2">16.2)&nbsp;Which areas can I plan a cycle journey in?</a></p>
<p class="rsviewcontentrow"><a href="#A16.3">16.3)&nbsp;Are all journeys planned by the quickest route?</a></p>
<p class="rsviewcontentrow"><a href="#A16.4">16.4)&nbsp;I will be travelling when its dark, how do I try and ensure my journey goes through reasonably well lit areas?</a></p>
<p class="rsviewcontentrow"><a href="#A16.5">16.5)&nbsp;How do I plan a journey where I do not have to get off and on my bike?</a></p>
<!-- <p class="rsviewcontentrow"><a href="#A16.6">16.6)&nbsp;I’m planning a journey with my young children, how do I avoid cycle journeys with steep climbs?</a></p> -->
<p class="rsviewcontentrow"><a href="#A16.7">16.7)&nbsp;What are time-based restrictions and what impact could they have on my journey?</a></p>
<p class="rsviewcontentrow"><a href="#A16.8">16.8)&nbsp;How do you calculate cycle journey times?</a></p>
<p class="rsviewcontentrow"><a href="#A16.9">16.9)&nbsp;Why am I being asked to download Active X controls?</a></p>
<p class="rsviewcontentrow"><a href="#A16.10">16.10)&nbsp;What should I do if I am prompted to download the control?</a></p>
<p class="rsviewcontentrow"><a href="#A16.11">16.11)&nbsp;What should I do if I cannot see the graph and have not been prompted to download the ActiveX control?</a></p>
<p class="rsviewcontentrow"><a href="#A16.12">16.12)&nbsp;What is a GPX file?</a></p>
<p class="rsviewcontentrow"><a href="#A16.13">16.13)&nbsp;How does Transport Direct Calculate Calories used?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree"><h2>16. Cycle Planning</h2></div>
<br />
<h3><a class="QuestionLink" id="A16.1" name="A16.1"></a>16.1)&nbsp;Does Transport Direct offer cycling options?</h3>
<p>Transport Direct has launched the first version of its new cycle planner. Currently, this allows you to plan cycle journeys between places that are up to 50km apart in the areas 
listed in FAQ 16.2 below. We are now encouraging users and experts to provide feedback about the new planner and the data that it uses.</p>
<br />
<h3><a class="QuestionLink" id="A16.2" name="A16.2"></a>16.2)&nbsp;Which areas can I plan a cycle journey in?</h3>
<p>You can plan a cycle journey in areas listed below.</p>
<br />
<div class="clearboth divCycleAreas">
<table class="tableCycleAreas" width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <th class="colCycleName">Name</th>
        <th class="colCycleCovering">Covering</th>
    </tr>
	<tr>
        <td class="colCycleName">All England</td>
        <td class="colCycleCovering">All counties throughout England</td>
    </tr>
	<tr>
        <td class="colCycleName">Cardiff</td>
        <td class="colCycleCovering">Cardiff Council</td>
    </tr>
</table>
</div>
<br />
<p>If you find an error in the data that has been collected Transport Direct would welcome your feedback to help us improve the quality of the data or the way the planner works. You can do this using the <strong>contact us</strong> link on the page footer.</p>
<br />
<h3><a class="QuestionLink" id="A16.3" name="A16.3"></a>16.3)&nbsp;Are all journeys planned by the quickest route?</h3>
<p>The ''Find a cycle route'' planner allows you to select a number of different parameters for your journey. 
By selecting your preferred option from the drop down menu marked ''Type of journey'' you can plan your journey using the following options:</p>
<ul class="listerdisc">
<li>"Quietest" if you would like a route that prioritises the use of cycle paths, cycle lanes, quiet streets and routes recommended for cycling, and where possible avoids steep hills. (This is the default option of the cycle journey planner)</li>
<li>"Quickest" if you would like a route with the shortest cycling time (taking into account the gradient and appropriate speed for the paths and roads involved).</li>
<li>"Most recreational" if you would like a route that prioritises cycling through parks and green spaces in addition to the other parameters outlined above under ''Quietest''.</li>
</ul>
<br />
<h3><a class="QuestionLink" id="A16.4" name="A16.4"></a>16.4)&nbsp;I will be travelling when its dark, how do I try and ensure my journey goes through reasonably well lit areas?</h3>
<p>You can choose to <strong>avoid</strong> roads and paths that are unlit at night when planning your cycle journey by clicking ‘Advanced options’ at the bottom of the input pages ticking the <strong>avoid unlit roads</strong> option. Transport Direct will, where possible, avoid roads and paths that are unlit at night when planning your journey.</p>
<br />
<h3><a class="QuestionLink" id="A16.5" name="A16.5"></a>16.5)&nbsp;How do I plan a journey where I do not have to get off and on my bike?</h3>
<p>You can choose to <strong>avoid</strong> a cycle journey with sections where you would need to walk your bike by clicking ‘Advanced options’ at the bottom of the input pages ticking the <strong>avoid walking with your bike</strong> option. Transport Direct will, where possible, avoid returning a journey with sections that would require you to dismount and walk.</p>
<br />
<!-- <h3><a class="QuestionLink" id="A16.6" name="A16.6"></a>16.6)&nbsp;I’m planning a journey with my young children, how do I avoid cycle journeys with steep climbs?</h3>
<p>The cycle planner contains gradient information so it can detect roads and paths that are steep climbs. You can choose to <strong>avoid</strong> a steep climb by clicking ‘Advanced options’ at the bottom of the input pages ticking the <strong>avoid steep climbs</strong> option. Transport Direct will, where possible, avoid returning a journey with sections that contain steep climbs.</p>
<br /> -->
<h3><a class="QuestionLink" id="A16.7" name="A16.7"></a>16.7)&nbsp;What are time-based restrictions and what impact could they have on my journey?</h3>
<p>Some roads and paths have time based access restrictions such as ‘Closed on market day’ or ‘Park closes at dusk’. Because of the nature of these restrictions we cannot tell when planning your journey whether or not access will be permitted at the time of travel. Where the information is available we will try and show in the journey results that a restriction applies to the point of access.</p>
<p>You can choose to <strong>avoid</strong> all such potential time-based restrictions by clicking ‘Advanced options’ at the bottom of the input pages ticking the <strong>avoid timebased restrictions</strong> option. Transport Direct will, where possible, avoid returning a journey that uses roads and paths with time-based restrictions.</p>
<br />
<h3><a class="QuestionLink" id="A16.8" name="A16.8"></a>16.8)&nbsp;How do you calculate cycle journey times?</h3>
<p>The cycle journey results on Transport Direct take into account the input cycle speed, the gradient of the paths and roads and appropriate speeds for those paths and roads.</p>
<br />
<h3><a class="QuestionLink" id="A16.9" name="A16.9"></a>16.9)&nbsp;Why am I being asked to download Active X controls?</h3>
<p>You may be prompted to download ActiveX Controls when first viewing the Cycle Journey results page. This ActiveX Add-on is an accessory software program that extends the capabilities of an existing application. In the case of the cycle planner it is required to draw the gradient profile graph of your journey.</p>
<p>If you are prompted to download any "ActiveX Controls", you will need to accept them if you want to view the gradient profile graph. Alternatively you can click the ‘show table view’ button to see the gradients for the journey.</p>
<br />
<h3><a class="QuestionLink" id="A16.10" name="A16.10"></a>16.10)&nbsp;What should I do if I am prompted to download the control?</h3>
<p>By simply following the download instructions, the add-on will be added to your browser''s current Add-ons. In some cases you may need to restart your browser for the Add-ons to take effect. Once you have downloaded the Add-on you will not be prompted to do so again unless it is removed from your machine or disabled.</p>
<br />
<h3><a class="QuestionLink" id="A16.11" name="A16.11"></a>16.11)&nbsp;What should I do if I cannot see the graph and have not been prompted to download the ActiveX control?</h3>
<p>If you continue to have difficulties downloading the Add-on and viewing the image you should report the issue to your Internet Service Provider or IT Support Team.</p>
<br />
<h3><a class="QuestionLink" id="A16.12" name="A16.12"></a>16.12)&nbsp;What is a GPX file?</h3>
<p>GPX is an XML schema designed for transferring Global Positioning Software (GPS) data between software applications.</p>
<p>The GPX file produced by Transport Direct''s cycle planner describes the tracks and routes taken in your journey. It indicates the start and end location, journey directions, as well as latitude and longitude of points along the route. You can load the GPX file into GPS devices and other software computer programs. This will allow you, for example, to view your route on your GPS device during your journey, project your track on satellite images (in Google Earth), or annotate maps. The format is open and can be used without the need to pay licence fees.</p>
<br />
<h3><a class="QuestionLink" id="A16.13" name="A16.13"></a>16.13)&nbsp;How does Transport Direct Calculate Calories used?</h3>
<p>There are a lot of complex factors that can affect the number of Calories you burn when cycling – weight, age, gender, speed, inclines etc.  The Calorie figures output with your cycle journey plan are intended as a rough guide only and are calculated in relation to cycling speed, distance and time using the Metabolic Equivalents from the Compendium of Physical Activities published by Ainsworth et al, in the journal “Medicine and Science in Sports and Exercise”. We assume an average weight of 70kg.</p>
<br />
<br />
</div></div>
'
, 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="CyclePlanning" name="CyclePlanning"></a><h2>16. Trefnu Teithiau Beicio</h2></div>
<p class="rsviewcontentrow"><a href="#A16.1">16.1)&nbsp;A yw Transport Direct yn cynnig dewisiadau beicio?</a></p>
<p class="rsviewcontentrow"><a href="#A16.2">16.2)&nbsp;Ym mha ardaloedd y gallaf gynllunio taith feicio?</a></p>
<p class="rsviewcontentrow"><a href="#A16.3">16.3)&nbsp;A yw pob siwrnai''n cael ei chynllunio yn &#244;l y llwybr cyflymaf?</a></p>
<p class="rsviewcontentrow"><a href="#A16.4">16.4)&nbsp;Byddaf yn teithio yn y tywyllwch, sut ydw i''n ceisio sicrhau bod fy siwrnai''n mynd drwy ardaloedd wedi''u goleuo''n rhesymol o dda?</a></p>
<p class="rsviewcontentrow"><a href="#A16.5">16.5)&nbsp;Sut ydw i''n trefnu siwrnai heb fod yn rhaid imi ddod oddi ar fy meic?</a></p>
<!-- <p class="rsviewcontentrow"><a href="#A16.6">16.6)&nbsp;Rwyf yn cynllunio siwrnai gyda fy mhlant ifanc, sut ydw i''n osgoi siwrneiau beicio sy''n dringo''n serth?</a></p> -->
<p class="rsviewcontentrow"><a href="#A16.7">16.7)&nbsp;Beth yw''r cyfyngiadau amser a pha effaith y gallent ei chael ar fy siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="#A16.8">16.8)&nbsp;Sut ydych chi''n cyfrifo amseroedd siwrnai beicio?</a></p>
<p class="rsviewcontentrow"><a href="#A16.9">16.9)&nbsp;Pam mae gofyn imi lwytho rheolaethau Active X i lawr?</a></p>
<p class="rsviewcontentrow"><a href="#A16.10">16.10)&nbsp;Beth ddylwn ei wneud os gofynnir imi lwytho''r rheolaeth i lawr?</a></p>
<p class="rsviewcontentrow"><a href="#A16.11">16.11)&nbsp;Beth ddylwn ei wneud os nad wyf yn gallu gweld y graff ac os na ofynnwyd imi lwytho''r rheolaeth ActiveX i lawr?</a></p>
<p class="rsviewcontentrow"><a href="#A16.12">16.12)&nbsp;Beth yw ffeil GPX?</a></p>
<p class="rsviewcontentrow"><a href="#A16.13">16.13)&nbsp;Sut mae Transport Direct yn Cyfrifo Calorïau a ddefnyddir?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree"><h2>16. Trefnu Teithiau Beicio</h2></div>
<br />
<h3><a class="QuestionLink" id="A16.1" name="A16.1"></a>16.1)&nbsp;A yw Transport Direct yn cynnig dewisiadau beicio?</h3>
<p>Transport Direct has launched the first version of its new cycle planner. Currently, this allows you to plan cycle journeys between places that are up to 50km apart in the areas 
listed in FAQ 16.2 below. We are now encouraging users and experts to provide feedback about the new planner and the data that it uses.</p>
<br />
<h3><a class="QuestionLink" id="A16.2" name="A16.2"></a>16.2)&nbsp;Ym mha ardaloedd y gallaf gynllunio taith feicio? </h3>
<p>You can plan a cycle journey in areas listed below.</p>
<br />
<div class="clearboth divCycleAreas">
<table class="tableCycleAreas" width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <th class="colCycleName">Name</th>
        <th class="colCycleCovering">Covering</th>
    </tr>
	<tr>
        <td class="colCycleName">All England</td>
        <td class="colCycleCovering">All counties throughout England</td>
    </tr>
	<tr>
        <td class="colCycleName">Cardiff</td>
        <td class="colCycleCovering">Cardiff Council</td>
    </tr>
</table>
</div>
<br />
<p>Os gwelwch wall yn y data a gasglwyd byddai Transport Direct yn croesawu eich adborth i''n helpu i wella ansawdd y data neu''r ffordd mae''r trefnwr yn gweithio. Gallwch wneud hyn drwy ddefnyddio''r ddolen cysylltwch â ni ar droedyn y dudalen. </p>
<br />
<h3><a class="QuestionLink" id="A16.3" name="A16.3"></a>16.3)&nbsp;A yw pob siwrnai''n cael ei chynllunio yn &#244;l y llwybr cyflymaf?</h3>
<p>Mae’r cynllunydd ‘Darganfod llwybr beicio’ yn caniatáu ichi ddewis nifer o wahanol baramedrau ar gyfer eich taith. Drwy ddewis eich hoff opsiwn o’r ddewislen gwympo ‘Math o daith’ gallwch gynllunio eich taith gan ddewis yr opsiynau canlynol:</p>
<ul class="listerdisc">
<li>“Tawelaf” os dymunwch ddarganfod llwybr sy’n rhoi blaenoriaeth i ddefnyddio llwybrau beicio, lonydd beicio, strydoedd tawel a llwybrau a argymhellir i feicwyr, a phan fo’n bosibl rhai sy’n osgoi elltydd serth. (Dyma opsiwn ‘diofyn’ y cynllunydd taith feicio).</li>
<li>“Cyflymaf” os dymunwch ddarganfod llwybr sy’n cynnig yr amser beicio byrraf (gan gymryd i ystyriaeth graddiant a chyflymder priodol ar gyfer y llwybrau a’r ffyrdd dan sylw).</li>
<li>“Gorau ar gyfer hamdden” os hoffech lwybr sy’n blaenoriaethu beicio drwy barciau a glastiroedd yn ogystal â’r paramedrau eraill a amlinellir uchod dan ‘Tawelaf’.</li>
</ul>
<br />
<h3><a class="QuestionLink" id="A16.4" name="A16.4"></a>16.4)&nbsp;Byddaf yn teithio yn y tywyllwch, sut ydw i''n ceisio sicrhau bod fy siwrnai''n mynd drwy ardaloedd wedi''u goleuo''n rhesymol o dda?</h3>
<p>Gallwch ddewis osgoi ffyrdd a llwybrau heb eu goleuo gyda''r nos wrth drefnu eich siwrnai feicio drwy glicio ar ''Dewisiadau mwy cymhleth’ ar waelod y tudalennau mewnbynnu gan dicio''r opsiwn osgoi ffyrdd heb eu goleuo. Bydd Transport Direct, os oes modd, yn osgoi ffyrdd a llwybrau heb eu goleuo gyda''r nos wrth drefnu eich siwrnai.</p>
<br />
<h3><a class="QuestionLink" id="A16.5" name="A16.5"></a>16.5)&nbsp;Sut ydw i''n trefnu siwrnai heb fod yn rhaid imi ddod oddi ar fy meic?</h3>
<p>Gallwch ddewis osgoi siwrnai feicio ac iddi rannau lle byddai angen ichi gerdded eich beic drwy glicio ‘Dewisiadau mwy cymhleth’ ar waelod y tudalennau mewnbynnu gan dicio''r opsiwn osgoi cerdded gyda''ch beic. Bydd Transport Direct, os oes modd, yn osgoi dychwelyd siwrnai ac iddi rannau a fyddai''n mynnu eich bod yn dod oddi ar eich beic ac yn cerdded.</p>
<br />
<!-- <h3><a class="QuestionLink" id="A16.6" name="A16.6"></a>16.6)&nbsp;Rwyf yn cynllunio siwrnai gyda fy mhlant ifanc, sut ydw i''n osgoi siwrneiau beicio sy''n dringo''n serth?</h3>
<p>Mae''r trefnwr teithiau beicio''n cynnwys gwybodaeth am raddiant er mwyn iddo ddarganfod ffyrdd a llwybrau sy''n dringo''n serth. Gallwch ddewis osgoi dringo''n serth drwy glicio ‘Dewisiadau mwy cymhleth’ ar waelod y tudalennau mewnbynnu gan dicio''r opsiwn osgoi dringo''n serth. Bydd Transport Direct, os oes modd, yn osgoi dychwelyd siwrnai ac iddi rannau sy''n dringo''n serth.</p>
<br /> -->
<h3><a class="QuestionLink" id="A16.7" name="A16.7"></a>16.7)&nbsp;Beth yw''r cyfyngiadau amser a pha effaith y gallent ei chael ar fy siwrnai?</h3>
<p>Mae gan rai ffyrdd a llwybrau fynediad cyfyngedig sy''n seiliedig ar amser, er enghraifft ‘Ar gau ar ddiwrnod marchnad’ neu ‘Parc yn cau pan fydd yn nosi’. Oherwydd y cyfyngiadau hyn ni allwn ddweud wrth drefnu eich siwrnai a fydd mynediad yn cael ei ganiatáu neu beidio adeg y daith. Os yw''r wybodaeth ar gael, byddwn yn ceisio dangos yng nghanlyniadau''r siwrnai fod cyfyngiad yn berthnasol i''r fynedfa.</p>
<p>Gallwch ddewis osgoi pob cyfyngiad amser posibl felly drwy glicio ‘Dewisiadau mwy cymhleth’ ar waelod y tudalennau mewnbynnu dicio''r opsiwn osgoi cyfyngiadau amser. Bydd Transport Direct, os oes modd, yn osgoi dychwelyd siwrnai sy''n defnyddio ffyrdd a llwybrau ac iddynt gyfyngiadau amser. </p>
<br />
<h3><a class="QuestionLink" id="A16.8" name="A16.8"></a>16.8)&nbsp;Sut ydych chi''n cyfrifo amseroedd siwrnai beicio?  </h3>
<p>Mae canlyniadau''r siwrneiau beicio ar Transport Direct yn ystyried y cyflymdra beicio a nodwyd, graddiant y llwybrau a''r ffyrdd a chyflymderau priodol i''r llwybrau a''r ffyrdd hynny.</p>
<br />
<h3><a class="QuestionLink" id="A16.9" name="A16.9"></a>16.9)&nbsp; Pam mae gofyn imi lwytho rheolaethau Active X i lawr?</h3>
<p>Efallai bydd gofyn ichi lwytho Rheolaethau ActiveX i lawr y tro cyntaf y gwelwch y dudalen canlyniadau Siwrneiau Beicio. Mae''r ychwanegyn ActiveX hwn yn rhaglen feddalwedd atodol sy''n ymestyn galluoedd cymhwysiad presennol. Yn achos y trefnwr teithiau beicio mae''n ofynnol i dynnu graff proffil graddiant eich siwrnai.</p>
<p>Os gofynnir ichi lwytho unrhyw reolaethau "ActiveX" i lawr, bydd angen ichi eu derbyn os ydych eisiau gweld graff y proffil graddiant. Fel arall gallwch glicio''r botwm ‘dangos golwg tabl’ i weld y graddiannau ar gyfer y siwrnai.</p>
<br />
<h3><a class="QuestionLink" id="A16.10" name="A16.10"></a>16.10)&nbsp;Beth ddylwn ei wneud os gofynnir imi lwytho''r rheolaeth i lawr?</h3>
<p>Drwy ddilyn y cyfarwyddiadau llwytho i lawr yn unig, caiff yr ychwanegyn ei ychwanegu at Ychwanegion cyfredol eich porwr. Weithiau efallai bydd angen ichi ailddechrau eich porwr er mwyn i''r Ychwanegion fod yn effeithiol. Pan fyddwch wedi llwytho''r Ychwanegyn i lawr, ni fydd gofyn ichi wneud hynny eto oni chaiff ei dynnu oddi ar eich peiriant neu ei analluogi.</p>
<br />
<h3><a class="QuestionLink" id="A16.11" name="A16.11"></a>16.11)&nbsp;Beth ddylwn ei wneud os nad wyf yn gallu gweld y graff ac os na ofynnwyd imi lwytho''r rheolaeth ActiveX i lawr?</h3>
<p>Os byddwch yn parhau i gael anawsterau''n llwytho''r Ychwanegyn i lawr a gweld y ddelwedd, dylech adrodd y mater i''ch Darparwr Gwasanaeth Rhyngrwyd neu Dîm Cymorth TG. </p>
<br />
<h3><a class="QuestionLink" id="A16.12" name="A16.12"></a>16.12)&nbsp;Beth yw ffeil GPX?</h3>
<p>Sgema XML yw GPX a gynlluniwyd i drosglwyddo data Meddalwedd Leoli Fyd-eang (GPS) rhwng cymwysiadau meddalwedd.</p>
<p>Mae''r ffeil GPX a gynhyrchir gan drefnwr teithiau beicio Transport Direct yn disgrifio''r llwybrau a''r teithiau a ddilynir yn eich siwrnai. Mae''n nodi dechrau a diwedd y siwrnai, cyfarwyddiadau siwrnai, yn ogystal â lledred a hydred mannau ar hyd y ffordd. Gallwch lwytho''r ffeil GPX i mewn i ddyfeisiau GPS a rhaglenni cyfrifiadur meddalwedd eraill. Bydd hyn yn eich galluogi, er enghraifft, i weld eich llwybr ar eich dyfais GPS yn ystod eich siwrnai, taflunio eich llwybr ar ddelweddau lloeren (yn Google Earth), neu anodi mapiau. Mae''r fformat yn agored a gellir ei ddefnyddio heb fod angen talu ffioedd trwydded. </p>
<br />
<h3><a class="QuestionLink" id="A16.13" name="A16.13"></a>16.13)&nbsp;Sut mae Transport Direct yn Cyfrifo Calorïau a ddefnyddir?</h3>
<p>Mae llawer o ffactorau cymhleth yn gallu effeithio ar faint o Galorïau yr ydych yn eu llosgi wrth seiclo – pwysau, oedran, rhyw, buanedd, pa mor serth yw''r ffyrdd a.y.y.b.  Bwriadwyd y ffigurau calorïau a gynhyrchir gan eich cynllun seiclo fel brasamcan yn unig a chânt eu cyfrifo mewn perthynas â''r buanedd, y pellter a''r amser seiclo gan ddefnyddio''r Cywerthoedd Metabolig o''r Compendium of Physical Activities a gyhoeddwyd gan Ainsworth et al, yn y cylchgrawn “Medicine and Science in Sports and Exercise”. Rydym yn defnyddio pwysau cyfartalog o 70kg.</p>
<br />

<br />
</div></div>
'


----------------------------------------------------------------
-- FAQ - Accessible Journey Planning
----------------------------------------------------------------

EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpAccessibleJourneyPlanning', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="AccessiblePlanning" name="AccessiblePlanning"></a><h2>18. Accessible Journey Planning</h2></div>

<p class="rsviewcontentrow"><a href="#A18.1">18.1) What information do you provide about accessible services, and which areas are covered?</a></p>
<p class="rsviewcontentrow"><a href="#A18.2">18.2) How accurate is your information on accessible journeys?</a></p>
<p class="rsviewcontentrow"><a href="#A18.3">18.3) What do you mean by ''assistance''?</a></p>
<p class="rsviewcontentrow"><a href="#A18.4">18.4) Why do you include journey legs that require assistance in your ''step-free only'' results?</a></p>
<p class="rsviewcontentrow"><a href="#A18.5">18.5) Why do I need to book in advance?</a></p>
<p class="rsviewcontentrow"><a href="#A18.6">18.6) How can I book in advance?</a></p>
<p class="rsviewcontentrow"><a href="#A18.7">18.7) How does the ''Find Nearest Accessible Stop'' work?</a></p>
<p class="rsviewcontentrow"><a href="#A18.8">18.8) How can I get to the nearest accessible station or stop?</a></p>
<p class="rsviewcontentrow"><a href="#A18.9">18.9) Can I choose an accessible journey that only uses particular types of transport?</a></p>
<p class="rsviewcontentrow"><a href="#A18.10">18.10) Can I obtain more information about the accessibility of a station?</a></p>
<p class="rsviewcontentrow"><a href="#A18.11">18.11) Can you provide fares information?</a></p>
<p class="rsviewcontentrow"><a href="#A18.12">18.12) Why didn''t I get any results for my accessible journey?</a></p>
<p class="rsviewcontentrow"><a href="#A18.13">18.13) I don''t believe a service, station or stop included in your journey results is accessible.  Can you correct this?</a></p>
<p class="rsviewcontentrow"><a href="#A18.14">18.14) Can I use my mobility scooter on public transport?</a></p>
<p class="rsviewcontentrow"><a href="#A18.15">18.15) Why can''t I plan to an airport, even though I know it is accessible?</a></p>
<p class="rsviewcontentrow"><a href="#A18.16">18.16) How do I find out about live incidents on the transport network once my journey has started?</a></p>

<div>&nbsp;</div>
<div class="hdtypethree"><h2>18. Accessible Journey Planning</h2></div>
<br />

<h3><a class="QuestionLink" id="A18.1" name="A18.1"></a>18.1) What information do you provide about accessible services, and which areas are covered?</h3>
<p>We use a network of stations, stops and services which have step-free routes and/or offer staff assistance at a station or from a vehicle. We are able to provide information about rail and coach services, most tram, light rail and underground networks, and bus travel in the following areas as shown in the maps at : 
<a target="_child" href="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/AccessibilityStatusMap_North.jpg" title="Click to view map in a new browser window">Accessibility Status Map (North) <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>,
<a target="_child" href="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/AccessibilityStatusMap_South.jpg" title="Click to view map in a new browser window">Accessibility Status Map (South) <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
<br />
East Anglia (Cambridgeshire, Norfolk, Suffolk)<br />
East Midlands (Derbyshire, Lincolnshire, Northamptonshire, Rutland)<br />
Greater London<br />
North East (Durham, Northumberland, Teesside, Tyne & Wear)<br />
North West (Cumbria, Greater Manchester, Lancashire, Merseyside)<br />
Scotland (Aberdeenshire, Angus, Edinburgh, Fife, Renfrewshire)<br />
South East (Bedfordshire, Buckinghamshire, Essex, Hampshire, Hertfordshire, Isle of Wight, Kent, Oxfordshire, Sussex)<br />
South West (Cornwall, Devon, Dorset)<br />
Wales (Cardiff, Swansea)<br />
Yorkshire (East Riding, North Yorkshire, South Yorkshire, West Yorkshire)<br />

We will add local bus information for other areas as this becomes available.<br /></p>
<p>Rail stations are only included in the network when there is an accessible route from street to <strong>all</strong> platforms within a station and between platforms, although, in some places, you may need to leave the station and use nearby footpaths to change platforms.  Staff assistance may be provided by staff on the station, or for some services, the train conductor will provide assistance.</p>
<p>Coaches included in the accessible network may be low-floor with a ramp fitted or high-floor with a lift fitted.</p>
<p>Buses are only included if they are wheelchair accessible, with a ramp fitted to help with boarding and alighting.  Buses that have a low floor but no wheelchair facilities are not included in the accessible network.</p>
<p>Most tram and light rail networks are step-free, although staff assistance is generally not available at stations or on vehicles.</p>
<p>Transport for London makes a distinction between underground stations that are accessible from street to platform and those which are accessible from street to vehicle.  At present, the Transport Direct accessible network only includes the latter, more limited, set of stations.  Although staff assistance is available at all London Underground stations, staff may not be able to provide assistance to board or alight the train.</p>
<br />

<h3><a class="QuestionLink" id="A18.2" name="A18.2"></a>18.2) How accurate is your information on accessible journeys?</h3>
<p>We have worked with transport operators and local authorities to gather information about stations, stops and services that are accessible.  Although the information we provide is as accurate as possible, we recommend that you check directly with the transport operator(s) before undertaking your journey.  Operators may sometimes have to replace an accessible vehicle with a non-accessible one at short notice for operational reasons.  In addition, if the wheelchair space on a non-bookable service is already occupied, you will need to wait for the next service with space available.</p>
<br />
 
<h3><a class="QuestionLink" id="A18.3" name="A18.3"></a>18.3) What do you mean by ''assistance''?</h3>
<p>Assistance covers the availability of staff to assist in boarding and alighting a service.  This may be needed by travellers who are partially sighted, older travellers with luggage, or families with buggies, as well as by wheelchair users.  You should contact the service operator directly to find out what assistance can be provided and book this in advance where required.</p>
<br />

<h3><a class="QuestionLink" id="A18.4" name="A18.4"></a>18.4) Why do you include journey legs that require assistance in your ''step-free only'' results?</h3>
<p>For some modes of transport, a step-free journey is only possible with staff assistance (for example, to operate a vehicle-mounted wheelchair lift).   By contrast, our ''step-free with assistance'' journeys offer assistance with boarding and alighting at every stage of the journey, provided you have booked in advance where required.</p>
<br />

<h3><a class="QuestionLink" id="A18.5" name="A18.5"></a>18.5) Why do I need to book in advance?</h3>
<p>Some operators, particularly rail and coach operators, recommend that you book at least 24 hours in advance.  This enables them to plan ahead to ensure staff are available to help you to board and alight, and to provide an accessible vehicle where this may not always be available.  If you need a wheelchair space, booking in advance may enable you to reserve this space.  If you are unable to book in advance, operators will still try to assist, but if staff are not immediately available or if the spaces allocated for wheelchair users are already taken up, they may not be able to help at short notice.</p>
<br />

<h3><a class="QuestionLink" id="A18.6" name="A18.6"></a>18.6) How can I book in advance?</h3>
<p>Where an operator recommends that you book in advance, we will include either a contact telephone number or a link to a booking website in our journey results.</p>
<br />

<h3><a class="QuestionLink" id="A18.7" name="A18.7"></a>18.7) How does the ''Find Nearest Accessible Stop'' work?</h3>
<p>If you start and end your journey at accessible stations, or within areas that we know have accessible services running, your journey should calculate automatically.  If, however, either the start and/or the end of your journey are not close to an accessible service, we will provide you with a list of potential start and/or end points for your journey that offer accessible services.  This will normally include a number of nearby accessible stations, as well as some places that we know are served by accessible buses.  Once you select a suitable station or location from this list, we will provide you with accessible journey options.</p>
<br />

<h3><a class="QuestionLink" id="A18.8" name="A18.8"></a>18.8) How can I get to the nearest accessible station or stop?</h3>
<p>If you are unable to use private transport, you may wish to book an accessible taxi, or make use of community transport.  Details of taxi firms that offer accessible cabs can be found on our Station Information page, and the Community Transport Online website offers information about local schemes at: <a target="_child" href="http://www.ctonline.org.uk" title="http://www.ctonline.org.uk">http://www.ctonline.org.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>.</p>
<br />

<h3><a class="QuestionLink" id="A18.9" name="A18.9"></a>18.9) Can I choose an accessible journey that only uses particular types of transport?</h3> 
<p>Yes.  You should first open up the ''Need to change your modes of travel?'' option, then de-select those types of transport (e.g. underground, bus) you do not want included in results.  You can then plan an accessible journey as normal, and the results will exclude the deselected modes.</p>
<br />

<h3><a class="QuestionLink" id="A18.10" name="A18.10"></a>18.10) Can I obtain more information about the accessibility of a station?</h3>
<p>Yes.  From the journey results screen, click on the name of the station, and you will be taken to the Station Information page.  This page includes information about taxi services at or near the station and whether these are accessible, as well as links to find out more information about the station itself, for example from the National Rail Enquiries ''Stations Made Easy'' or Direct Enquiries web sites.</p>
<br />

<h3><a class="QuestionLink" id="A18.11" name="A18.11"></a>18.11) Can you provide fares information?</h3>
<p>Yes, for rail and some coach journeys.  For rail journeys, we may only be able to provide a separate fare for each leg of your journey, so you may find the cost of your journey to be cheaper once you book it directly with the operator.  Unfortunately, we are currently unable to link to a retail site for booking accessible journeys.</p>
<br />

<h3><a class="QuestionLink" id="A18.12" name="A18.12"></a>18.12) Why didn''t I get any results for my accessible journey?</h3>
<p>There may be no accessible services operating for all or part of your journey on the day or time selected.  If you believe this to be incorrect, please provide us with more information via our ''Contact Us'' facility.</p>
<br />

<h3><a class="QuestionLink" id="A18.13" name="A18.13"></a>18.13) I don''t believe a service, station or stop included in your journey results is accessible.  Can you correct this?</h3>
<p>The information we provide in our accessible journey plans is as accurate as possible, but we recognise that there will always be scope for improvement.  If you believe any of the information we have provided to be incorrect, please use the ''Contact Us'' facility to notify us, and we will investigate and make changes to our data where necessary.</p>
<br />

<h3><a class="QuestionLink" id="A18.14" name="A18.14"></a>18.14) Can I use my mobility scooter on public transport?</h3>  
<p>We hope to provide more information about journeys using a mobility scooter in the future.  For the time being, please contact the service operators to check if there are any restrictions on mobility scooters on these services.  Some operators have already signed up to the Confederation of Passenger Transport Code for the use and acceptance of Mobility Scooters on low floor buses, more information on which can be found at: <a target="_child" href="http://www.cpt-uk.org/_uploads/attachment/1941.pdf" title="http://www.cpt-uk.org/_uploads/attachment/1941.pdf">http://www.cpt-uk.org/_uploads/attachment/1941.pdf <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>.</p>
<br />

<h3><a class="QuestionLink" id="A18.15" name="A18.15"></a>18.15) Why can''t I plan to an airport, even though I know it is accessible?</h3>
<p>We are working on this and hope to include this capability soon.  For the time being, you can still plan an accessible journey to or from a station or coach stop at or near the airport.</p>
<br />

<h3><a class="QuestionLink" id="A18.16" name="A18.16"></a>18.16) How do I find out about live incidents on the transport network once my journey has started?</h3>
<p>We are currently limited in the amount of real-time information we can provide about a journey.  General information about current incidents is available in ''Live travel news''.  We hope to be able to improve the extent of detailed live incidents in future.</p>
<br />

<br />
</div>
</div>'
,
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="AccessiblePlanning" name="AccessiblePlanning"></a><h2>18. Accessible Journey Planning</h2></div>

<p class="rsviewcontentrow"><a href="#A18.1">18.1) What information do you provide about accessible services, and which areas are covered?</a></p>
<p class="rsviewcontentrow"><a href="#A18.2">18.2) How accurate is your information on accessible journeys?</a></p>
<p class="rsviewcontentrow"><a href="#A18.3">18.3) What do you mean by ''assistance''?</a></p>
<p class="rsviewcontentrow"><a href="#A18.4">18.4) Why do you include journey legs that require assistance in your ''step-free only'' results?</a></p>
<p class="rsviewcontentrow"><a href="#A18.5">18.5) Why do I need to book in advance?</a></p>
<p class="rsviewcontentrow"><a href="#A18.6">18.6) How can I book in advance?</a></p>
<p class="rsviewcontentrow"><a href="#A18.7">18.7) How does the ''Find Nearest Accessible Stop'' work?</a></p>
<p class="rsviewcontentrow"><a href="#A18.8">18.8) How can I get to the nearest accessible station or stop?</a></p>
<p class="rsviewcontentrow"><a href="#A18.9">18.9) Can I choose an accessible journey that only uses particular types of transport?</a></p>
<p class="rsviewcontentrow"><a href="#A18.10">18.10) Can I obtain more information about the accessibility of a station?</a></p>
<p class="rsviewcontentrow"><a href="#A18.11">18.11) Can you provide fares information?</a></p>
<p class="rsviewcontentrow"><a href="#A18.12">18.12) Why didn''t I get any results for my accessible journey?</a></p>
<p class="rsviewcontentrow"><a href="#A18.13">18.13) I don''t believe a service, station or stop included in your journey results is accessible.  Can you correct this?</a></p>
<p class="rsviewcontentrow"><a href="#A18.14">18.14) Can I use my mobility scooter on public transport?</a></p>
<p class="rsviewcontentrow"><a href="#A18.15">18.15) Why can''t I plan to an airport, even though I know it is accessible?</a></p>
<p class="rsviewcontentrow"><a href="#A18.16">18.16) How do I find out about live incidents on the transport network once my journey has started?</a></p>

<div>&nbsp;</div>
<div class="hdtypethree"><h2>18. Accessible Journey Planning</h2></div>
<br />

<h3><a class="QuestionLink" id="A18.1" name="A18.1"></a>18.1) What information do you provide about accessible services, and which areas are covered?</h3>
<p>We use a network of stations, stops and services which have step-free routes and/or offer staff assistance at a station or from a vehicle. We are able to provide information about rail and coach services, most tram, light rail and underground networks, and bus travel in the following areas as shown in the maps at : 
<a target="_child" href="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/AccessibilityStatusMap_North.jpg" title="Click to view map in a new browser window">Accessibility Status Map (North) <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>,
<a target="_child" href="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/AccessibilityStatusMap_South.jpg" title="Click to view map in a new browser window">Accessibility Status Map (South) <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
<br />
East Anglia (Cambridgeshire, Norfolk, Suffolk)<br />
East Midlands (Derbyshire, Lincolnshire, Northamptonshire, Rutland)<br />
Greater London<br />
North East (Durham, Northumberland, Teesside, Tyne & Wear)<br />
North West (Cumbria, Greater Manchester, Lancashire, Merseyside)<br />
Scotland (Aberdeenshire, Angus, Edinburgh, Fife, Renfrewshire)<br />
South East (Bedfordshire, Buckinghamshire, Essex, Hampshire, Hertfordshire, Isle of Wight, Kent, Oxfordshire, Sussex)<br />
South West (Cornwall, Devon, Dorset)<br />
Wales (Cardiff, Swansea)<br />
Yorkshire (East Riding, North Yorkshire, South Yorkshire, West Yorkshire)<br />

We will add local bus information for other areas as this becomes available.<br /></p>
<p>Rail stations are only included in the network when these are accessible from street to <strong>all</strong> platforms within a station and between platforms, although, in some places, you may need to leave the station and use nearby footpaths to change platforms.  Staff assistance may be provided by staff on the station, or for some services, the train conductor will provide assistance.</p>
<p>Coaches included in the accessible network may be low-floor with a ramp fitted or high-floor with a lift fitted.</p>
<p>Buses are only included if they are wheelchair accessible, with a ramp fitted to help with boarding and alighting.  Buses that have a low floor but no wheelchair facilities are not included in the accessible network.</p>
<p>Most tram and light rail networks are step-free, although staff assistance is generally not available at stations or on vehicles.</p>
<p>Transport for London makes a distinction between underground stations that are accessible from street to platform and those which are accessible from street to vehicle.  At present, the Transport Direct accessible network only includes the latter, more limited, set of stations.  Although staff assistance is available at all London Underground stations, staff may not be able to provide assistance to board or alight the train.</p>
<br />

<h3><a class="QuestionLink" id="A18.2" name="A18.2"></a>18.2) How accurate is your information on accessible journeys?</h3>
<p>We have worked with transport operators and local authorities to gather information about stations, stops and services that are accessible.  Although the information we provide is as accurate as possible, we recommend that you check directly with the transport operator(s) before undertaking your journey.  Operators may sometimes have to replace an accessible vehicle with a non-accessible one at short notice for operational reasons.  In addition, if the wheelchair space on a non-bookable service is already occupied, you will need to wait for the next service with space available.</p>
<br />
 
<h3><a class="QuestionLink" id="A18.3" name="A18.3"></a>18.3) What do you mean by ''assistance''?</h3>
<p>Assistance covers the availability of staff to assist in boarding and alighting a service.  This may be needed by travellers who are partially sighted, older travellers with luggage, or families with buggies, as well as by wheelchair users.  You should contact the service operator directly to find out what assistance can be provided and book this in advance where required.</p>
<br />

<h3><a class="QuestionLink" id="A18.4" name="A18.4"></a>18.4) Why do you include journey legs that require assistance in your ''step-free only'' results?</h3>
<p>For some modes of transport, a step-free journey is only possible with staff assistance (for example, to operate a vehicle-mounted wheelchair lift).   By contrast, our ''step-free with assistance'' journeys offer assistance with boarding and alighting at every stage of the journey, provided you have booked in advance where required.</p>
<br />

<h3><a class="QuestionLink" id="A18.5" name="A18.5"></a>18.5) Why do I need to book in advance?</h3>
<p>Some operators, particularly rail and coach operators, recommend that you book at least 24 hours in advance.  This enables them to plan ahead to ensure staff are available to help you to board and alight, and to provide an accessible vehicle where this may not always be available.  If you need a wheelchair space, booking in advance may enable you to reserve this space.  If you are unable to book in advance, operators will still try to assist, but if staff are not immediately available or if the spaces allocated for wheelchair users are already taken up, they may not be able to help at short notice.</p>
<br />

<h3><a class="QuestionLink" id="A18.6" name="A18.6"></a>18.6) How can I book in advance?</h3>
<p>Where an operator recommends that you book in advance, we will include either a contact telephone number or a link to a booking website in our journey results.</p>
<br />

<h3><a class="QuestionLink" id="A18.7" name="A18.7"></a>18.7) How does the ''Find Nearest Accessible Stop'' work?</h3>
<p>If you start and end your journey at accessible stations, or within areas that we know have accessible services running, your journey should calculate automatically.  If, however, either the start and/or the end of your journey are not close to an accessible service, we will provide you with a list of potential start and/or end points for your journey that offer accessible services.  This will normally include a number of nearby accessible stations, as well as some places that we know are served by accessible buses.  Once you select a suitable station or location from this list, we will provide you with accessible journey options.</p>
<br />

<h3><a class="QuestionLink" id="A18.8" name="A18.8"></a>18.8) How can I get to the nearest accessible station or stop?</h3>
<p>If you are unable to use private transport, you may wish to book an accessible taxi, or make use of community transport.  Details of taxi firms that offer accessible cabs can be found on our Station Information page, and the Community Transport Online website offers information about local schemes at: <a target="_child" href="http://www.ctonline.org.uk" title="http://www.ctonline.org.uk">http://www.ctonline.org.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>.</p>
<br />

<h3><a class="QuestionLink" id="A18.9" name="A18.9"></a>18.9) Can I choose an accessible journey that only uses particular types of transport?</h3> 
<p>Yes.  You should first open up the ''Need to change your modes of travel?'' option, then de-select those types of transport (e.g. underground, bus) you do not want included in results.  You can then plan an accessible journey as normal, and the results will exclude the deselected modes.</p>
<br />

<h3><a class="QuestionLink" id="A18.10" name="A18.10"></a>18.10) Can I obtain more information about the accessibility of a station?</h3>
<p>Yes.  From the journey results screen, click on the name of the station, and you will be taken to the Station Information page.  This page includes information about taxi services at or near the station and whether these are accessible, as well as links to find out more information about the station itself, for example from the National Rail Enquiries ''Stations Made Easy'' or Direct Enquiries web sites.</p>
<br />

<h3><a class="QuestionLink" id="A18.11" name="A18.11"></a>18.11) Can you provide fares information?</h3>
<p>Yes, for rail and some coach journeys.  For rail journeys, we may only be able to provide a separate fare for each leg of your journey, so you may find the cost of your journey to be cheaper once you book it directly with the operator.  Unfortunately, we are currently unable to link to a retail site for booking accessible journeys.</p>
<br />

<h3><a class="QuestionLink" id="A18.12" name="A18.12"></a>18.12) Why didn''t I get any results for my accessible journey?</h3>
<p>There may be no accessible services operating for all or part of your journey on the day or time selected.  If you believe this to be incorrect, please provide us with more information via our ''Contact Us'' facility.</p>
<br />

<h3><a class="QuestionLink" id="A18.13" name="A18.13"></a>18.13) I don''t believe a service, station or stop included in your journey results is accessible.  Can you correct this?</h3>
<p>The information we provide in our accessible journey plans is as accurate as possible, but we recognise that there will always be scope for improvement.  If you believe any of the information we have provided to be incorrect, please use the ''Contact Us'' facility to notify us, and we will investigate and make changes to our data where necessary.</p>
<br />

<h3><a class="QuestionLink" id="A18.14" name="A18.14"></a>18.14) Can I use my mobility scooter on public transport?</h3>  
<p>We hope to provide more information about journeys using a mobility scooter in the future.  For the time being, please contact the service operators to check if there are any restrictions on mobility scooters on these services.  Some operators have already signed up to the Confederation of Passenger Transport Code for the use and acceptance of Mobility Scooters on low floor buses, more information on which can be found at: <a target="_child" href="http://www.cpt-uk.org/_uploads/attachment/1941.pdf" title="http://www.cpt-uk.org/_uploads/attachment/1941.pdf">http://www.cpt-uk.org/_uploads/attachment/1941.pdf <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>.</p>
<br />

<h3><a class="QuestionLink" id="A18.15" name="A18.15"></a>18.15) Why can''t I plan to an airport, even though I know it is accessible?</h3>
<p>We are working on this and hope to include this capability soon.  For the time being, you can still plan an accessible journey to or from a station or coach stop at or near the airport.</p>
<br />

<h3><a class="QuestionLink" id="A18.16" name="A18.16"></a>18.16) How do I find out about live incidents on the transport network once my journey has started?</h3>
<p>We are currently limited in the amount of real-time information we can provide about a journey.  General information about current incidents is available in ''Live travel news''.  We hope to be able to improve the extent of detailed live incidents in future.</p>
<br />

<br />
</div>
</div>'



----------------------------------------------------------------
-- Page Headings
----------------------------------------------------------------

EXEC AddtblContent
@ThemeId, 27, 'TitleText', '/Channels/TransportDirect/Help/NewHelp', 
'<div><h1>Frequently Asked Questions (FAQ) </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpAir', 
'<div><h1>Frequently Asked Questions (FAQ) - Air </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Awyr </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpBus', 
'<div><h1>Frequently Asked Questions (FAQ) - Bus </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Bws </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpCarbon', 
'<div><h1>Frequently Asked Questions (FAQ) - CO2 Information </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Gwybodaeth CO2 </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpComm', 
'<div><h1>Frequently Asked Questions (FAQ) - Comment and Feedback </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Sylwadau ac adborth </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpCosts', 
'<div><h1>Frequently Asked Questions (FAQ) - Tickets and Costs </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Tocynnau a chostau </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpInfo', 
'<div><h1>Frequently Asked Questions (FAQ) - Information on the website </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Yngl&#375;n &#226; Transport Direct </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpJplan', 
'<div><h1>Frequently Asked Questions (FAQ) - General Journey Planning </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Cynllunio Siwrnai Cyffredinol  </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpLiveTravel', 
'<div><h1>Frequently Asked Questions (FAQ) - Live Travel </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Teithio byw </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpMaps', 
'<div><h1>Frequently Asked Questions (FAQ) - Maps </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Mapiau </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpMobile', 
'<div><h1>Frequently Asked Questions (FAQ) - Using Transport Direct services on&nbsp;your PDA/Mobile</h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Defnyddio gwasanaethau teithio byw Transport Direct ar PDA/ff&#244;n symudol</h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpParking', 
'<div><h1>Frequently Asked Questions (FAQ) - Parking </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Parcio </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpRoad', 
'<div><h1>Frequently Asked Questions (FAQ) - Road Journey Planning </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Cynllunio siwrnai ffordd </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpTaxi', 
'<div><h1>Frequently Asked Questions (FAQ) - Taxis </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Tacsis </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpTrain', 
'<div><h1>Frequently Asked Questions (FAQ) - Train </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Tr&#234;n </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpUsing', 
'<div><h1>Frequently Asked Questions (FAQ) - Using the website </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Defnyddio''r wefan </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpTouristInfo', 
'<div><h1>Frequently Asked Questions (FAQ) - Tourist Information </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - (Cy) Tourist Information </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpCycle', 
'<div><h1>Frequently Asked Questions (FAQ) - Cycle Planning </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Cynllunio Beicio </h1></div>'

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpAccessibleJourneyPlanning', 
'<div><h1>Frequently Asked Questions (FAQ) - Accessible Journey Planning </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) -  Accessible Journey Planning </h1></div>'

GO



----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10305
SET @ScriptDesc = 'Updates to FAQs - modified for BBC'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.84  $'

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

