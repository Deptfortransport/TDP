-- *************************************************************************************
-- NAME 		: SCP10205_AOCycle_Cycle_5_FAQs.sql
-- DESCRIPTION  : The FAQs amended for AOCycle Cycle WhiteLabel site:
-- 			    : Cycle White lable FAQs has only Cycle and CO2 sections
-- AUTHOR		: Amit Patel
-- *************************************************************************************

-- ************************************************
-- NOTE: AOCycle partner setup purely for test purpose
-- ************************************************

-- ************************************* NOTE :**************************************************
-- CYCLE WHITELABEL SITE FAQ CONTAINS ONLY CO2 INFORMATION AND CYCLE PLANNING SECTIONS
-- OTHER SECTIONS ARE REMOVED FROM THE FAQ CONTENT AS THEY NOT REQUIRED BY CYCLE WHITE LABEL SITE
-- THE CO2 INFORMATION AND CYCLE PLANNING SECTION NUMBERS ARE CHANGED TO 1 AND 2 RESPECTIVELY.
-- **********************************************************************************************

USE [Content]
GO


DECLARE @ThemeId INT
SET @ThemeId = 200

-------------------------------------------------------------
-- FAQs - Main page
-------------------------------------------------------------

EXEC AddtblContent
@ThemeId, 27, 'Body Text', '/Channels/TransportDirect/Help/NewHelp', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="CalcCarbon" name="CalcCarbon"></a><h2>1. CO2 Information</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.1">1.1)&nbsp;How do you estimate my car''s CO2 emissions?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.2">1.2)&nbsp;What assumptions do you make about cars'' CO2 emissions?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.3">1.3)&nbsp;What assumptions do you make in estimating the CO2 emissions produced for public transport and air journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.4">1.4)&nbsp;What assumptions do you make in comparing CO2 emissions for car journeys with public transport journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.5">1.5)&nbsp;What assumptions do you make about bus and coach journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.6">1.6)&nbsp;Why do you use energy efficiency ratings?</a></p>
<div align="right"><a name="WhoOperates"></a><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="CyclePlanning" name="CyclePlanning"></a><h2>2. Cycle Planning</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.1">2.1)&nbsp;Does Transport Direct offer cycling options?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.2">2.2)&nbsp;Which areas can I plan a cycle journey in?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.3">2.3)&nbsp;Are all journeys planned by the quickest route?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.4">2.4)&nbsp;I will be travelling when it’s dark, how do I try and ensure my journey goes through reasonably well lit areas?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.5">2.5)&nbsp;How do I plan a journey where I do not have to get off and on my bike?</a></p>
<!-- <p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.6">2.6)&nbsp;I’m planning a journey with my young children, how do I avoid cycle journeys with steep climbs?</a></p> -->
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.7">2.7)&nbsp;What are time-based restrictions and what impact could they have on my journey?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.8">2.8)&nbsp;How do you calculate cycle journey times?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.9">2.9)&nbsp;Why am I being asked to download Active X controls?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.10">2.10)&nbsp;What should I do if I am prompted to download the control?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.11">2.11)&nbsp;What should I do if I cannot see the graph and have not been prompted to download the ActiveX control?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.12">2.12)&nbsp;What is a GPX file?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.13">2.13)&nbsp;How does Transport Direct Calculate Calories used?</a></p>
<div align="right"><a class="jpt" href="#logoTop">Top of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
</div></div>'
, 
'<div id="primcontent">
<div id="contentarea"><div class="hdtypethree"><a id="CalcCarbon" name="CalcCarbon"></a><h2>1. Gwybodaeth CO2</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.1">1.1)&nbsp;Sut ydych chi''n amcangyfrif allyriadau CO2 fy nghar?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.2">1.2)&nbsp;Beth ydych chi''n ei dybio am allyriadau CO2 ceir?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.3">1.3)&nbsp;Beth ydych chi''n ei dybio wrth amcangyfrif yr allyriadau CO2 a gynhyrchir ar gyfer siwrneiau trafnidiaeth gyhoeddus ac awyr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.4">1.4)&nbsp;Beth ydych chi''n ei dybio wrth gymharu allyriadau CO2 ar gyfer siwrneiau car &#226; siwrneiau trafnidiaeth gyhoeddus?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.5">1.5)&nbsp;Beth ydych chi''n ei dybio am siwrneiau bws a choets?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.6">1.6)&nbsp;Pam ydych chi''n defnyddio cyfraddau effeithlonrwydd ynni?</a></p>
<div align="right"><a name="EnablingIntelligentTravel"></a><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
<div class="hdtypethree"><a id="CyclePlanning" name="CyclePlanning"></a><h2>2. Trefnu Teithiau Beicio</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.1">2.1)&nbsp;A yw Transport Direct yn cynnig dewisiadau beicio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.2">2.2)&nbsp;Ym mha ardaloedd y gallaf gynllunio taith feicio? </a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.3">2.3)&nbsp;A yw pob siwrnai''n cael ei chynllunio yn &#244;l y llwybr cyflymaf?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.4">2.4)&nbsp;Byddaf yn teithio yn y tywyllwch, sut ydw i''n ceisio sicrhau bod fy siwrnai''n mynd drwy ardaloedd wedi''u goleuo''n rhesymol o dda?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.5">2.5)&nbsp;Sut ydw i''n trefnu siwrnai heb fod yn rhaid imi ddod oddi ar fy meic?</a></p>
<!-- <p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.6">2.6)&nbsp;Rwyf yn cynllunio siwrnai gyda fy mhlant ifanc, sut ydw i''n osgoi siwrneiau beicio sy''n dringo''n serth?</a></p> -->
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.7">2.7)&nbsp;Beth yw''r cyfyngiadau amser a pha effaith y gallent ei chael ar fy siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.8">2.8)&nbsp;Sut ydych chi''n cyfrifo amseroedd siwrnai beicio?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.9">2.9)&nbsp;Pam mae gofyn imi lwytho rheolaethau Active X i lawr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.10">2.10)&nbsp;Beth ddylwn ei wneud os gofynnir imi lwytho''r rheolaeth i lawr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.11">2.11)&nbsp;Beth ddylwn ei wneud os nad wyf yn gallu gweld y graff ac os na ofynnwyd imi lwytho''r rheolaeth ActiveX i lawr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.12">2.12)&nbsp;Beth yw ffeil GPX?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCycle.aspx#A2.13">2.13)&nbsp;Sut mae Transport Direct yn Cyfrifo Calorïau a ddefnyddir?</a></p>
<div align="right"><a class="jpt" href="#logoTop">Yn &#244;l i''r brig <img alt="Yn cl &#13;&#10;i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif" border="0" /></a></div>
</div></div>'


-------------------------------------------------------------
-- FAQs - Carbon
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpCarbon', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="Carbon" name="Carbon"></a><h2>1. CO2 Information</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.1">1.1) How do you estimate my car''s CO2 emissions?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.2">1.2) What assumptions do you make about cars'' CO2 emissions?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.3">1.3) What assumptions do you make in estimating the CO2 emissions produced for your public transport and air journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.4">1.4) What assumptions do you make in comparing CO2 emissions for car journeys with public transport journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.5">1.5) What assumptions do you make about bus and coach journeys?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.6">1.6) Why do you use energy efficiency ratings?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>1. CO2 Information</h2></div>
<br />
<h3><a class="QuestionLink" id="A12.1" name="A1.1"></a>1.1)&nbsp;How do you estimate my car''s CO2 emissions?</h3>
<p>To estimate the CO2 produced by a car journey we first calculate the distance travelled along the planned road route. We calculate the total distance by summing up the distance for each small segment of the journey along the road network. Usually each segment is made up of the distance between adjacent road junctions. We then estimate the fuel your car will use travelling along that route.</p>
<p>&nbsp;</p>
<p>To do this we take account of how many miles per gallon your car does and the predicted congestion. We also account for urban driving where there is a higher frequency of road junctions. This enables us to factor in the stop start nature of driving in a built up area which uses more fuel and produces more CO2 than doing a journey of the same distance on a rural road or motorways.</p>
<p>&nbsp;</p>
<p>Once we have calculate', '<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="Carbon" name="Carbon"></a><h2>1. Gwybodaeth CO2</h2></div>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.1">1.1) Sut ydych chi''n amcangyfrif allyriadau CO2 fy nghar?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.2">1.2) Beth ydych chi''n ei dybio am allyriadau CO2 ceir?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.3">1.3) Beth ydych chi''n ei dybio wrth amcangyfrif yr allyriadau CO2 a gynhyrchir ar gyfer siwrneiau trafnidiaeth gyhoeddus ac awyr?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.4">1.4) Beth ydych chi''n ei dybio wrth gymharu allyriadau CO2 ar gyfer siwrneiau car &#226; siwrneiau trafnidiaeth gyhoeddus?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.5">1.5) Beth ydych chi''n ei dybio am siwrneiau bws a choets?</a></p>
<p class="rsviewcontentrow"><a href="/Web2/Help/HelpCarbon.aspx#A1.6">1.6) Pam ydych chi''n defnyddio cyfraddau effeithlonrwydd ynni?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree">
<h2>1. Gwybodaeth CO2</h2></div>
<br />
<h3><a class="QuestionLink" id="A1.1" name="A1.1"></a>1.1)&nbsp;Sut ydych chi''n amcangyfrif allyriadau CO2 fy nghar?</h3>
<p>I amcangyfrif y CO2 a gynhyrchir gan siwrnai gar, yn gyntaf rydym yn cyfrifo pellter y daith ar hyd y llwybr ffordd a fwriedir. Rydym yn cyfrif cyfanswm y pellter drwy gyfrif y pellter ar gyfer pob rhan fechan o''r siwrnai ar hyd y rhwydwaith ffordd. Fel arfer, mae pob rhan yn cynnwys y pellter rhwng cyffyrdd cyfagos. Wedyn rydym yn amcangyfrif y tanwydd y bydd eich car yn ei ddefnyddio wrth deithio ar hyd y llwybr hwnnw. </p>
<p><br />I wneud hyn, ystyriwn sawl milltir y galwyn a wnaiff eich car a''r tagfeydd a ragwelir. Rydym hefyd yn ystyried gyrru trefol lle ceir mwy o gyffyrdd. Mae hyn yn ein galluogi i ystyried natur atal a chychwyn gyrru mewn ardal adeiledig sy''n defnyddio mwy o danwydd ac yn cynhyrchu mwy o CO2 na theithio''r un pellter ar ffordd wledig neu draffyrdd. </p>
<p><br />Ar &'


DECLARE @ptrValText BINARY(16)

-- Update text field: Value-En
SET @ptrValText = NULL
SELECT @ptrValText = TEXTPTR([Value-En]) FROM [dbo].[tblContent] WHERE themeid = @ThemeId and groupid = 19 and controlname ='Body Text' and propertyname = '/Channels/TransportDirect/Help/HelpCarbon'

IF @ptrValText IS NOT NULL BEGIN

   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 'd the fuel used we convert this to the CO2 produced using a conversion factor depending on whether the fuel you use is petrol or diesel.</p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A1.2" name="A1.2"></a>1.2)&nbsp;What assumptions do you make about cars'' CO2 emissions?</h3>
<p>To find out the amount of CO2 your car produces we calculate the amount of fuel used for a specific journey. We convert this to the CO2 produced using a conversion factor depending on whether the fuel you use is petrol or diesel</p>
<p>&nbsp;</p>
<p>You can enter your car''s fuel efficiency if you know it. This will mean that the calculations we provide are specific to the fuel efficiency of your car.</p>
<p>&nbsp;</p>
<p>Many people are not sure of their car&#8217;s fuel efficiency. If you do not know this we can provide an approximation of the fuel efficiency and CO2 emissions for your car if you tell us whether it has a small medium or large engine and whether it uses petrol or diesel. If you don''t tell us we will assume that you have a medium sized petrol engine car.</p>
<p>&nbsp;</p>
<p><a href="../Web2/Downloads/TransportDirectCO2Data.pdf" target="_child">Click here to see the assumptions we make about the miles per gallon for different sized engines <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)">.</a></p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A1.3" name="A1.3"></a>1.3) What assumptions do you make in estimating the CO2 emissions produced for your public transport and air journeys?</h3>
<p>First we calculate your journey distance</p>
<p>&nbsp;</p>
<p>Second we multiply this by a specific factor for the particular modes you will use. This gives the CO2 emissions for your journey.</p>
<p>&nbsp;</p>
<p>Currently these figures are general averages for each type of public transport. Each figure assumes an average number of passengers for the typical sort of vehicles used when travelling by the particular type of transport. The figures come from Department for Transport (DfT) and have been agreed with Department for Environment Food and Rural Affairs (DEFRA)'
   UPDATETEXT [dbo].[tblContent].[Value-En] @ptrValText NULL 0 '. <a href="../Web2/Downloads/TransportDirectCO2Data.pdf" target="_child">Click here to see these figures <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)">.</a></p>
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
<h3><a class="QuestionLink" id="A1.4" name="A1.4"></a>1.4) What assumptions do you make in comparing CO2 emissions for car journeys with public transport journeys?</h3>
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
<h3><a class="QuestionLink" id="A1.5" name="A1.5"></a>1.5) What assumptions do you make about bus and coach journeys?</h3>
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
<h3><a class="QuestionLink" id="A1.6" name="A1.6"></a>1.6) Why do you use the same colours as the Energy Efficiency Ratings?</h3>
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
<h3><a class="QuestionLink" id="A1.2" name="A1.2"></a>1.2)&nbsp;Beth ydych chi''n ei dybio am allyriadau CO2 ceir?</h3>
<p>I ganfod faint o CO2 mae eich car yn ei gynhyrchu, rydym yn cyfrifo maint y tanwydd a ddefnyddir ar gyfer siwrnai benodol. Rydym yn troi hwn yn CO2 a gynhyrchir gan ddefnyddio ffactor trawsnewid gan ddibynnu a ydych chi''n defnyddio tanwydd petrol neu ddisel.</p>
<p><br />Gallwch nodi effeithlonrwydd tanwydd eich car os ydych yn ei wybod eisoes. Bydd hyn yn golygu bod y cyfrifiadau a rown yn benodol i effeithlonrwydd tanwydd eich car.</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />Nid yw llawer o bobl yn sicr beth yw effeithlonrwydd tanwydd eu ceir. Os nad ydych yn gwybod hyn, gallwn roi brasamcan o''r effeithlonrwydd tanwydd a''r allyriadau CO2 ar gyfer eich car os gallwch ddweud wrthym a oes ganddo fodur bach, canolig neu fawr ac a yw''n defnyddio petrol neu ddisel. Os na fyddwch yn dweud wrthym, byddwn yn tybio bod gennych gar modur petrol canolig.&nbsp;</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br /><a href="../Web2/Downloads/TransportDirectCO2Data_CY.pdf" target="_child">Cliciwch yma i weld beth rydym yn ei dybio am y milltiroedd y galwyn ar gyfer moduron o wahanol faint <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)">.</a></p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A1.3" name="A1.3"></a>1.3) Beth ydych chi''n ei dybio wrth amcangyfrif yr allyriadau CO2 a gynhyrchir ar gyfer siwrneiau trafnidiaeth gyhoeddus ac awyr?</h3>
<p>Yn gyntaf rydym yn cyfrifo pellter eich siwrnai. </p>
<p><br />Yn ail, rydym yn lluosi''r ffigur hwn gan ffactor penodol ar gyfer y dulliau penodol a ddefnyd'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 'diwch. Mae hyn yn rhoi''r allyriadau CO2 ar gyfer eich siwrnai.</p>
<p><br />Ar hyn o bryd, mae''r ffigurau hyn yn gyfartalion cyffredinol ar gyfer pob math o drafnidiaeth gyhoeddus. Mae pob ffigur yn tybio nifer gyfartalog o deithwyr ar gyfer y math nodweddiadol o gerbydau a ddefnyddir wrth deithio gyda''r math penodol o drafnidiaeth. Daw''r ffigurau o''r Adran Drafnidiaeth ac fe''u cytunwyd &#226; DEFRA.&nbsp;<a href="../Web2/Downloads/TransportDirectCO2Data_CY.pdf" target="_child">Cliciwch yma i weld y ffigurau hyn <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)">.</a></p>
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
<h3><a class="QuestionLink" id="A1.4" name="A1.4"></a>1.4) Beth ydych chi''n ei dybio wrth gymharu allyriadau CO2 ar gyfer siwrneiau car &#226; siwrneiau trafnidiaeth gyhoeddus?</h3>
<p>Rydym yn ceisio rhoi teimlad cyffredinol o''r gwahaniaeth mewn allyriadau CO2 a fyddai''n cael eu cynhyrchu fesul teithiwr.</p>
<p><br />Os ydych wedi dewis siwrnai gar, rydym yn cyfrifo''r allyriadau ar gyfer y siwrnai honno gan ddilyn y llwybr gwirioneddol i''w gymryd. Os byddwch wedyn yn dewis siwrnai trafnidiaeth gyhoeddus i gymharu, amcangyfrifwn yr allyriadau yn seiliedig ar deithio''r un pellter llinell syth &#226;"r car gan ddefnyddio''r gwahanol fathau o drafnidiaeth gyhoeddus. Er mwyn cymharu yn unig y mae hyn ac efallai bydd cysylltiadau trafnidiaeth gyhoeddus ymarferol rhwng lleoliadau''r siwrnai gar wreiddiol. Os hoffech deithio ar drafnidiaeth gyhoeddus wedi''r cyfan, defnyddiwch y cynlluniwr priodol i ddod o hyd i''ch opsiynau teithio'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 '.</p>
<p><br />Yn yr un modd, os ydym wedi cynllunio siwrnai trafnidiaeth gyhoeddus ichi, pan fyddwch yn gofyn am gymhariaeth rydym yn amcangyfrif yr allyriadau car ac allyriadau mathau eraill o drafnidiaeth gyhoeddus yn seiliedig ar deithio''r un pellter llinell syth &#226;"r siwrnai trafnidiaeth gyhoeddus a ddewiswyd. Ar gyfer y car, rydym yn amcangyfrif faint o danwydd a fyddai''n cael ei ddefnyddio ac felly faint o allyriadau a gynhyrchir. Yn yr achos hwn tybiwn y byddech yn teithio mewn car petrol canolig gan deithio ar gyfuniad o ffyrdd trefol a rhyngdrefol, gyda thagfeydd cyfartalog (sy''n arwain at gynyddu''r allyriadau 1.03 o gymharu &#226;"r gwerthoedd anghyfyngedig).</p>
<p><br />Rydym yn cymharu''r allyriadau CO2 y rhagwelir y''u cynhyrchir ar gyfer siwrnai gar gyda nifer benodol o deithwyr ag amcangyfrif o gyfanswm yr allyriadau CO2 a fyddai''n cael eu cynhyrchu ar gyfer pob teithiwr mewn un neu fwy math o drafnidiaeth gyhoeddus. </p>
<p><br />Fodd bynnag, mae car a thrafnidiaeth gyhoeddus yn wahanol. Mewn termau absoliwt o leihau CO2, mae bob amser yn well defnyddio gwasanaeth trafnidiaeth gyhoeddus wedi''i amserlennu oherwydd pan ddefnyddiwch gar fe wneir siwrnai ychwanegol a gollyngir CO2 ychwanegol i''r atmosffer. Ar y llaw arall, pan ddefnyddiwch siwrnai trafnidiaeth gyhoeddus ar fws, coets, awyren neu dr&#234;n, mae''r siwrnai bob amser wedi''i hamserlennu a byddai''n digwydd ni waeth a fyddwch chi''n teithio neu beidio. Felly, mewn gwirionedd, ni ollyngir unrhyw CO2 ychwanegol.&nbsp;</p>
<p><br /></p>
<p><br /></p>
<br />
<h3><a class="QuestionLink" id="A1.5" name="A1.5"></a>1.5) Beth ydych chi''n ei dybio am siwrneiau bws a choets?</h3>
<p>I bennu a ddangosir allyriadau ar gyfer bws neu goets, rydym yn amcangyfrif pellter y siwrnai yn gyntaf.&nbsp; </p>
<p><br />Tybiwn y byddwch yn teithio ar fws ar siwrneiau byr sy''n llai na 30 cilometr. Felly ar siwrneiau byr, dim ond yr allyriadau CO2 a ragwelir ar gyfer bws a ddangoswn.</p>
<p><br />Ar y llaw arall, os amcang'
   UPDATETEXT [dbo].[tblContent].[Value-Cy] @ptrValText NULL 0 'yfrifwn fod pellter y siwrnai dros 30 cilometr rydym yn tybio y byddwch yn teithio mewn coets. Felly ar siwrneiau hwy, dim ond yr allyriadau CO2 a ragwelir ar gyfer coets a ddangoswn.</p>
<p><br />I gyfrifo allyriadau bws defnyddiwn ffactor sy''n seiliedig ar y milltiroedd bws cyfartalog y galwyn a nifer y teithwyr a gludir ac i gyfrifo allyriadau coets defnyddiwn ffactor sy''n seiliedig ar filltiroedd coets cyfartalog y galwyn a nifer y teithwyr a gludir.<br /></p>
<br />
<h3><a class="QuestionLink" id="A1.6" name="A1.6"></a>1.6) Pam ydych chi''n defnyddio cyfraddau effeithlonrwydd ynni?</h3>
<p>Ers sawl blynedd, cydnabuwyd yn eang fod nwyddau trydanol fel peiriannau golchi wedi''u cyflenwi gyda chyfradd effeithlonrwydd safonol ac fe gyflwynwyd hyn ar siart A-G safonol, o wyrdd i goch. Defnyddir y siart hwn yn ehangach bellach ac argymhellir bod yr Asiantaeth Tystysgrifo Cerbydau (VCA) yn ei ddefnyddio ar gyfer pob car newydd. Mae Journey Planner yn tybio bod ceir ag un teithiwr ac allyriadau isel yn debygol o fod yn ardal werdd a melyn y bar ac y bydd ceir ag un teithiwr ac allyriadau uchel yn yr ardal oren a choch. Bydd y "gyfradd" hon yn parhau''n gyson ni waeth beth yw pellter y siwrnai, ond bydd swm yr allyriadau a gynhyrchir yn cynyddu wrth i bellter y siwrnai gynyddu.</p>
<p>&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>
<div></div>
<div></div>
<div></div>'

END


-------------------------------------------------------------
-- FAQs - Cycle Planning
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/Help/HelpCycle', 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="CyclePlanning" name="CyclePlanning"></a><h2>2. Cycle Planning</h2></div>
<p class="rsviewcontentrow"><a href="#A2.1">2.1)&nbsp;Does Transport Direct offer cycling options?</a></p>
<p class="rsviewcontentrow"><a href="#A2.2">2.2)&nbsp;Which areas can I plan a cycle journey in?</a></p>
<p class="rsviewcontentrow"><a href="#A2.3">2.3)&nbsp;Are all journeys planned by the quickest route?</a></p>
<p class="rsviewcontentrow"><a href="#A2.4">2.4)&nbsp;I will be travelling when its dark, how do I try and ensure my journey goes through reasonably well lit areas?</a></p>
<p class="rsviewcontentrow"><a href="#A2.5">2.5)&nbsp;How do I plan a journey where I do not have to get off and on my bike?</a></p>
<!-- <p class="rsviewcontentrow"><a href="#A2.6">2.6)&nbsp;I’m planning a journey with my young children, how do I avoid cycle journeys with steep climbs?</a></p> -->
<p class="rsviewcontentrow"><a href="#A2.7">2.7)&nbsp;What are time-based restrictions and what impact could they have on my journey?</a></p>
<p class="rsviewcontentrow"><a href="#A2.8">2.8)&nbsp;How do you calculate cycle journey times?</a></p>
<p class="rsviewcontentrow"><a href="#A2.9">2.9)&nbsp;Why am I being asked to download Active X controls?</a></p>
<p class="rsviewcontentrow"><a href="#A2.10">2.10)&nbsp;What should I do if I am prompted to download the control?</a></p>
<p class="rsviewcontentrow"><a href="#A2.11">2.11)&nbsp;What should I do if I cannot see the graph and have not been prompted to download the ActiveX control?</a></p>
<p class="rsviewcontentrow"><a href="#A2.12">2.12)&nbsp;What is a GPX file?</a></p>
<p class="rsviewcontentrow"><a href="#A2.13">2.13)&nbsp;How does Transport Direct Calculate Calories used?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree"><h2>2. Cycle Planning</h2></div>
<br />
<h3><a class="QuestionLink" id="A2.1" name="A2.1"></a>2.1)&nbsp;Does Transport Direct offer cycling options?</h3>
<p>Transport Direct has launched the first version of its new cycle planner. Currently, this allows you to plan cycle journeys between places that are up to 50km apart in the areas 
listed in FAQ 16.2 below. We are now encouraging users and experts to provide feedback about the new planner and the data that it uses.</p>
<br />
<h3><a class="QuestionLink" id="A2.2" name="A2.2"></a>2.2)&nbsp;Which areas can I plan a cycle journey in?</h3>
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
<h3><a class="QuestionLink" id="A2.3" name="A2.3"></a>2.3)&nbsp;Are all journeys planned by the quickest route?</h3>
<p>The ''Find a cycle route'' planner allows you to select a number of different parameters for your journey. 
By selecting your preferred option from the drop down menu marked ''Type of journey'' you can plan your journey using the following options:</p>
<ul class="listerdisc">
<li>"Quietest" if you would like a route that prioritises the use of cycle paths, cycle lanes, quiet streets and routes recommended for cycling, and where possible avoids steep hills. (This is the default option of the cycle journey planner)</li>
<li>"Quickest" if you would like a route with the shortest cycling time (taking into account the gradient and appropriate speed for the paths and roads involved).</li>
<li>"Most recreational" if you would like a route that prioritises cycling through parks and green spaces in addition to the other parameters outlined above under ''Quietest''.</li>
</ul>
<br />
<h3><a class="QuestionLink" id="A2.4" name="A2.4"></a>2.4)&nbsp;I will be travelling when its dark, how do I try and ensure my journey goes through reasonably well lit areas?</h3>
<p>You can choose to <strong>avoid</strong> roads and paths that are unlit at night when planning your cycle journey by clicking ‘Advanced options’ at the bottom of the input pages ticking the <strong>avoid unlit roads</strong> option. Transport Direct will, where possible, avoid roads and paths that are unlit at night when planning your journey.</p>
<br />
<h3><a class="QuestionLink" id="A2.5" name="A2.5"></a>2.5)&nbsp;How do I plan a journey where I do not have to get off and on my bike?</h3>
<p>You can choose to <strong>avoid</strong> a cycle journey with sections where you would need to walk your bike by clicking ‘Advanced options’ at the bottom of the input pages ticking the <strong>avoid walking with your bike</strong> option. Transport Direct will, where possible, avoid returning a journey with sections that would require you to dismount and walk.</p>
<br />
<!-- <h3><a class="QuestionLink" id="A2.6" name="A2.6"></a>2.6)&nbsp;I’m planning a journey with my young children, how do I avoid cycle journeys with steep climbs?</h3>
<p>The cycle planner contains gradient information so it can detect roads and paths that are steep climbs. You can choose to <strong>avoid</strong> a steep climb by clicking ‘Advanced options’ at the bottom of the input pages ticking the <strong>avoid steep climbs</strong> option. Transport Direct will, where possible, avoid returning a journey with sections that contain steep climbs.</p>
<br /> -->
<h3><a class="QuestionLink" id="A2.7" name="A2.7"></a>2.7)&nbsp;What are time-based restrictions and what impact could they have on my journey?</h3>
<p>Some roads and paths have time based access restrictions such as ‘Closed on market day’ or ‘Park closes at dusk’. Because of the nature of these restrictions we cannot tell when planning your journey whether or not access will be permitted at the time of travel. Where the information is available we will try and show in the journey results that a restriction applies to the point of access.</p>
<p>You can choose to <strong>avoid</strong> all such potential time-based restrictions by clicking ‘Advanced options’ at the bottom of the input pages ticking the <strong>avoid timebased restrictions</strong> option. Transport Direct will, where possible, avoid returning a journey that uses roads and paths with time-based restrictions.</p>
<br />
<h3><a class="QuestionLink" id="A2.8" name="A2.8"></a>2.8)&nbsp;How do you calculate cycle journey times?</h3>
<p>The cycle journey results on Transport Direct take into account the input cycle speed, the gradient of the paths and roads and appropriate speeds for those paths and roads.</p>
<br />
<h3><a class="QuestionLink" id="A2.9" name="A2.9"></a>2.9)&nbsp;Why am I being asked to download Active X controls?</h3>
<p>You may be prompted to download ActiveX Controls when first viewing the Cycle Journey results page. This ActiveX Add-on is an accessory software program that extends the capabilities of an existing application. In the case of the cycle planner it is required to draw the gradient profile graph of your journey.</p>
<p>If you are prompted to download any "ActiveX Controls", you will need to accept them if you want to view the gradient profile graph. Alternatively you can click the ‘show table view’ button to see the gradients for the journey.</p>
<br />
<h3><a class="QuestionLink" id="A2.10" name="A2.10"></a>2.10)&nbsp;What should I do if I am prompted to download the control?</h3>
<p>By simply following the download instructions, the add-on will be added to your browser''s current Add-ons. In some cases you may need to restart your browser for the Add-ons to take effect. Once you have downloaded the Add-on you will not be prompted to do so again unless it is removed from your machine or disabled.</p>
<br />
<h3><a class="QuestionLink" id="A2.11" name="A2.11"></a>2.11)&nbsp;What should I do if I cannot see the graph and have not been prompted to download the ActiveX control?</h3>
<p>If you continue to have difficulties downloading the Add-on and viewing the image you should report the issue to your Internet Service Provider or IT Support Team.</p>
<br />
<h3><a class="QuestionLink" id="A2.12" name="A2.12"></a>2.12)&nbsp;What is a GPX file?</h3>
<p>GPX is an XML schema designed for transferring Global Positioning Software (GPS) data between software applications.</p>
<p>The GPX file produced by Transport Direct''s cycle planner describes the tracks and routes taken in your journey. It indicates the start and end location, journey directions, as well as latitude and longitude of points along the route. You can load the GPX file into GPS devices and other software computer programs. This will allow you, for example, to view your route on your GPS device during your journey, project your track on satellite images (in Google Earth), or annotate maps. The format is open and can be used without the need to pay licence fees.</p>
<br />
<h3><a class="QuestionLink" id="A2.13" name="A2.13"></a>2.13)&nbsp;How does Transport Direct Calculate Calories used?</h3>
<p>There are a lot of complex factors that can affect the number of Calories you burn when cycling – weight, age, gender, speed, inclines etc.  The Calorie figures output with your cycle journey plan are intended as a rough guide only and are calculated in relation to cycling speed, distance and time using the Metabolic Equivalents from the Compendium of Physical Activities published by Ainsworth et al, in the journal “Medicine and Science in Sports and Exercise”. We assume an average weight of 70kg.</p>
<br />
<p><a href="/Web2/JourneyPlanning/FindCycleInput.aspx">Click here to go to the Cycle Planner input page</a></p>
<br />
</div></div>
'
, 
'<div id="primcontent">
<div id="contentarea">
<div class="hdtypethree"><a id="CyclePlanning" name="CyclePlanning"></a><h2>2. Trefnu Teithiau Beicio</h2></div>
<p class="rsviewcontentrow"><a href="#A2.1">2.1)&nbsp;A yw Transport Direct yn cynnig dewisiadau beicio?</a></p>
<p class="rsviewcontentrow"><a href="#A2.2">2.2)&nbsp;Ym mha ardaloedd y gallaf gynllunio taith feicio?</a></p>
<p class="rsviewcontentrow"><a href="#A2.3">2.3)&nbsp;A yw pob siwrnai''n cael ei chynllunio yn &#244;l y llwybr cyflymaf?</a></p>
<p class="rsviewcontentrow"><a href="#A2.4">2.4)&nbsp;Byddaf yn teithio yn y tywyllwch, sut ydw i''n ceisio sicrhau bod fy siwrnai''n mynd drwy ardaloedd wedi''u goleuo''n rhesymol o dda?</a></p>
<p class="rsviewcontentrow"><a href="#A2.5">2.5)&nbsp;Sut ydw i''n trefnu siwrnai heb fod yn rhaid imi ddod oddi ar fy meic?</a></p>
<!-- <p class="rsviewcontentrow"><a href="#A2.6">2.6)&nbsp;Rwyf yn cynllunio siwrnai gyda fy mhlant ifanc, sut ydw i''n osgoi siwrneiau beicio sy''n dringo''n serth?</a></p> -->
<p class="rsviewcontentrow"><a href="#A2.7">2.7)&nbsp;Beth yw''r cyfyngiadau amser a pha effaith y gallent ei chael ar fy siwrnai?</a></p>
<p class="rsviewcontentrow"><a href="#A2.8">2.8)&nbsp;Sut ydych chi''n cyfrifo amseroedd siwrnai beicio?</a></p>
<p class="rsviewcontentrow"><a href="#A2.9">2.9)&nbsp;Pam mae gofyn imi lwytho rheolaethau Active X i lawr?</a></p>
<p class="rsviewcontentrow"><a href="#A2.10">2.10)&nbsp;Beth ddylwn ei wneud os gofynnir imi lwytho''r rheolaeth i lawr?</a></p>
<p class="rsviewcontentrow"><a href="#A2.11">2.11)&nbsp;Beth ddylwn ei wneud os nad wyf yn gallu gweld y graff ac os na ofynnwyd imi lwytho''r rheolaeth ActiveX i lawr?</a></p>
<p class="rsviewcontentrow"><a href="#A2.12">2.12)&nbsp;Beth yw ffeil GPX?</a></p>
<p class="rsviewcontentrow"><a href="#A2.13">2.13)&nbsp;Sut mae Transport Direct yn Cyfrifo Calorïau a ddefnyddir?</a></p>
<div>&nbsp;</div>
<div class="hdtypethree"><h2>2. Trefnu Teithiau Beicio</h2></div>
<br />
<h3><a class="QuestionLink" id="A2.1" name="A2.1"></a>2.1)&nbsp;A yw Transport Direct yn cynnig dewisiadau beicio?</h3>
<p>Transport Direct has launched the first version of its new cycle planner. Currently, this allows you to plan cycle journeys between places that are up to 50km apart in the areas 
listed in FAQ 16.2 below. We are now encouraging users and experts to provide feedback about the new planner and the data that it uses.</p>
<br />
<h3><a class="QuestionLink" id="A2.2" name="A2.2"></a>2.2)&nbsp;Ym mha ardaloedd y gallaf gynllunio taith feicio? </h3>
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
<h3><a class="QuestionLink" id="A2.3" name="A2.3"></a>2.3)&nbsp;A yw pob siwrnai''n cael ei chynllunio yn &#244;l y llwybr cyflymaf?</h3>
<p>The ''Find a cycle route'' planner allows you to select a number of different parameters for your journey. 
By selecting your preferred option from the drop down menu marked ''Type of journey'' you can plan your journey using the following options:</p>
<ul class="listerdisc">
<li>"Quietest" if you would like a route that prioritises the use of cycle paths, cycle lanes, quiet streets and routes recommended for cycling, and where possible avoids steep hills. (This is the default option of the cycle journey planner)</li>
<li>"Quickest" if you would like a route with the shortest cycling time (taking into account the gradient and appropriate speed for the paths and roads involved).</li>
<li>"Most recreational" if you would like a route that prioritises cycling through parks and green spaces in addition to the other parameters outlined above under ''Quietest''.</li>
</ul>
<br />
<h3><a class="QuestionLink" id="A2.4" name="A2.4"></a>2.4)&nbsp;Byddaf yn teithio yn y tywyllwch, sut ydw i''n ceisio sicrhau bod fy siwrnai''n mynd drwy ardaloedd wedi''u goleuo''n rhesymol o dda?</h3>
<p>Gallwch ddewis osgoi ffyrdd a llwybrau heb eu goleuo gyda''r nos wrth drefnu eich siwrnai feicio drwy glicio ar ''Dewisiadau mwy cymhleth’ ar waelod y tudalennau mewnbynnu gan dicio''r opsiwn osgoi ffyrdd heb eu goleuo. Bydd Transport Direct, os oes modd, yn osgoi ffyrdd a llwybrau heb eu goleuo gyda''r nos wrth drefnu eich siwrnai.</p>
<br />
<h3><a class="QuestionLink" id="A2.5" name="A2.5"></a>2.5)&nbsp;Sut ydw i''n trefnu siwrnai heb fod yn rhaid imi ddod oddi ar fy meic?</h3>
<p>Gallwch ddewis osgoi siwrnai feicio ac iddi rannau lle byddai angen ichi gerdded eich beic drwy glicio ‘Dewisiadau mwy cymhleth’ ar waelod y tudalennau mewnbynnu gan dicio''r opsiwn osgoi cerdded gyda''ch beic. Bydd Transport Direct, os oes modd, yn osgoi dychwelyd siwrnai ac iddi rannau a fyddai''n mynnu eich bod yn dod oddi ar eich beic ac yn cerdded.</p>
<br />
<!-- <h3><a class="QuestionLink" id="A2.6" name="A2.6"></a>2.6)&nbsp;Rwyf yn cynllunio siwrnai gyda fy mhlant ifanc, sut ydw i''n osgoi siwrneiau beicio sy''n dringo''n serth?</h3>
<p>Mae''r trefnwr teithiau beicio''n cynnwys gwybodaeth am raddiant er mwyn iddo ddarganfod ffyrdd a llwybrau sy''n dringo''n serth. Gallwch ddewis osgoi dringo''n serth drwy glicio ‘Dewisiadau mwy cymhleth’ ar waelod y tudalennau mewnbynnu gan dicio''r opsiwn osgoi dringo''n serth. Bydd Transport Direct, os oes modd, yn osgoi dychwelyd siwrnai ac iddi rannau sy''n dringo''n serth.</p>
<br /> -->
<h3><a class="QuestionLink" id="A2.7" name="A2.7"></a>2.7)&nbsp;Beth yw''r cyfyngiadau amser a pha effaith y gallent ei chael ar fy siwrnai?</h3>
<p>Mae gan rai ffyrdd a llwybrau fynediad cyfyngedig sy''n seiliedig ar amser, er enghraifft ‘Ar gau ar ddiwrnod marchnad’ neu ‘Parc yn cau pan fydd yn nosi’. Oherwydd y cyfyngiadau hyn ni allwn ddweud wrth drefnu eich siwrnai a fydd mynediad yn cael ei ganiatáu neu beidio adeg y daith. Os yw''r wybodaeth ar gael, byddwn yn ceisio dangos yng nghanlyniadau''r siwrnai fod cyfyngiad yn berthnasol i''r fynedfa.</p>
<p>Gallwch ddewis osgoi pob cyfyngiad amser posibl felly drwy glicio ‘Dewisiadau mwy cymhleth’ ar waelod y tudalennau mewnbynnu dicio''r opsiwn osgoi cyfyngiadau amser. Bydd Transport Direct, os oes modd, yn osgoi dychwelyd siwrnai sy''n defnyddio ffyrdd a llwybrau ac iddynt gyfyngiadau amser. </p>
<br />
<h3><a class="QuestionLink" id="A2.8" name="A2.8"></a>2.8)&nbsp;Sut ydych chi''n cyfrifo amseroedd siwrnai beicio?  </h3>
<p>Mae canlyniadau''r siwrneiau beicio ar Transport Direct yn ystyried y cyflymdra beicio a nodwyd, graddiant y llwybrau a''r ffyrdd a chyflymderau priodol i''r llwybrau a''r ffyrdd hynny.</p>
<br />
<h3><a class="QuestionLink" id="A2.9" name="A2.9"></a>2.9)&nbsp; Pam mae gofyn imi lwytho rheolaethau Active X i lawr?</h3>
<p>Efallai bydd gofyn ichi lwytho Rheolaethau ActiveX i lawr y tro cyntaf y gwelwch y dudalen canlyniadau Siwrneiau Beicio. Mae''r ychwanegyn ActiveX hwn yn rhaglen feddalwedd atodol sy''n ymestyn galluoedd cymhwysiad presennol. Yn achos y trefnwr teithiau beicio mae''n ofynnol i dynnu graff proffil graddiant eich siwrnai.</p>
<p>Os gofynnir ichi lwytho unrhyw reolaethau "ActiveX" i lawr, bydd angen ichi eu derbyn os ydych eisiau gweld graff y proffil graddiant. Fel arall gallwch glicio''r botwm ‘dangos golwg tabl’ i weld y graddiannau ar gyfer y siwrnai.</p>
<br />
<h3><a class="QuestionLink" id="A2.10" name="A2.10"></a>2.10)&nbsp;Beth ddylwn ei wneud os gofynnir imi lwytho''r rheolaeth i lawr?</h3>
<p>Drwy ddilyn y cyfarwyddiadau llwytho i lawr yn unig, caiff yr ychwanegyn ei ychwanegu at Ychwanegion cyfredol eich porwr. Weithiau efallai bydd angen ichi ailddechrau eich porwr er mwyn i''r Ychwanegion fod yn effeithiol. Pan fyddwch wedi llwytho''r Ychwanegyn i lawr, ni fydd gofyn ichi wneud hynny eto oni chaiff ei dynnu oddi ar eich peiriant neu ei analluogi.</p>
<br />
<h3><a class="QuestionLink" id="A2.11" name="A2.11"></a>2.11)&nbsp;Beth ddylwn ei wneud os nad wyf yn gallu gweld y graff ac os na ofynnwyd imi lwytho''r rheolaeth ActiveX i lawr?</h3>
<p>Os byddwch yn parhau i gael anawsterau''n llwytho''r Ychwanegyn i lawr a gweld y ddelwedd, dylech adrodd y mater i''ch Darparwr Gwasanaeth Rhyngrwyd neu Dîm Cymorth TG. </p>
<br />
<h3><a class="QuestionLink" id="A2.12" name="A2.12"></a>2.12)&nbsp;Beth yw ffeil GPX?</h3>
<p>Sgema XML yw GPX a gynlluniwyd i drosglwyddo data Meddalwedd Leoli Fyd-eang (GPS) rhwng cymwysiadau meddalwedd.</p>
<p>Mae''r ffeil GPX a gynhyrchir gan drefnwr teithiau beicio Transport Direct yn disgrifio''r llwybrau a''r teithiau a ddilynir yn eich siwrnai. Mae''n nodi dechrau a diwedd y siwrnai, cyfarwyddiadau siwrnai, yn ogystal â lledred a hydred mannau ar hyd y ffordd. Gallwch lwytho''r ffeil GPX i mewn i ddyfeisiau GPS a rhaglenni cyfrifiadur meddalwedd eraill. Bydd hyn yn eich galluogi, er enghraifft, i weld eich llwybr ar eich dyfais GPS yn ystod eich siwrnai, taflunio eich llwybr ar ddelweddau lloeren (yn Google Earth), neu anodi mapiau. Mae''r fformat yn agored a gellir ei ddefnyddio heb fod angen talu ffioedd trwydded. </p>
<br />
<h3><a class="QuestionLink" id="A2.13" name="A2.13"></a>2.13)&nbsp;Sut mae Transport Direct yn Cyfrifo Calorïau a ddefnyddir?</h3>
<p>Mae llawer o ffactorau cymhleth yn gallu effeithio ar faint o Galorïau yr ydych yn eu llosgi wrth seiclo – pwysau, oedran, rhyw, buanedd, pa mor serth yw''r ffyrdd a.y.y.b.  Bwriadwyd y ffigurau calorïau a gynhyrchir gan eich cynllun seiclo fel brasamcan yn unig a chânt eu cyfrifo mewn perthynas â''r buanedd, y pellter a''r amser seiclo gan ddefnyddio''r Cywerthoedd Metabolig o''r Compendium of Physical Activities a gyhoeddwyd gan Ainsworth et al, yn y cylchgrawn “Medicine and Science in Sports and Exercise”. Rydym yn defnyddio pwysau cyfartalog o 70kg.</p>
<br />

<p><a href="/Web2/JourneyPlanning/FindCycleInput.aspx">Click here to go to the Cycle Planner input page</a></p>
<br />
</div></div>
'





----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10205
SET @ScriptDesc = 'The FAQs amended for AOCycle Cycle WhiteLabel site'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.37  $'

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

