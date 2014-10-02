-- *************************************************************************************
-- NAME 		: SC10004_TransportDirect_Content_4_MiniHome_TipsAndTools.sql
-- DESCRIPTION  : Updates to Tips and Tools homepage information panel
-- AUTHOR		: Mitesh Modi
-- DATE			: 16 Apr 2008 15:00:00
-- *************************************************************************************


USE [Content]
GO

DECLARE @GroupId int,
	@ThemeId int
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'tools_home')
SET @ThemeId = 1

-- Add the html text
EXEC AddtblContent
@ThemeId, @GroupId, 'TipsToolsInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Tools/Home'
-- ENGLISH
,
'<div class="MinihomeHyperlinksInfoContent">
<div class="MinihomeHyperlinksInfoHeader">
<div class="txtsevenbbl">
<h2>Helping you get the most from Transport Direct</h2>
</div>
<!-- Following spaces help   formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice   wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>

<div class="MinihomeSoftContent" summary="Tips and tools content">
<h3>Compare CO2 emissions for your journey...</h3>

<p>&nbsp;</p>

<p>You can now compare the CO2 emissions of the four main modes of transport (Car, Rail,
Bus/coach, and Plane) for your journey.</p><br />
<img alt="Screen shot of CO2 Emissions page" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeTipsToolsEmissions2.jpg"
align="right" border="0" />

<p>You can do a simple emissions comparison by inputting a distance in miles or
kilometres. TD will tell you how much CO2 would be emitted if you travelled that distance
by car, train, bus, or plane.</p><br clear="right" />

<p>&nbsp;</p>

<p><img alt="Screen shot of CO2 Emissions for your journey" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeTipsToolsEmissionsJourney2.jpg"
align="left" border="0" />Or look at the emissions for your journey by different modes of
transport by following the links from your journey details page.</p><br clear="left" />

<p>&nbsp;</p>

<p>Go to the <a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx">CO2
emissions</a> page to find out.</p>

<p>&nbsp;</p>

<h3>Getting Transport Direct on the move...</h3>

<p>&nbsp;</p>

<p>You can also get our Transport Direct Mobile service on your mobile phone or PDA for
free.</p><br />
<img style="PADDING-RIGHT: 10px" alt=
"Screenshot of mobile simulator showing a real-time &#13; departures board" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeTipsToolsMobile.jpg" align=
"left" border="0" />

<p>You can search for next train departures and arrivals, check whether a train is
running on time and look up times for tomorrow.</p><br />

<p>You can check scheduled bus times (for most of Britain).</p><br />

<p>You can also check if there are any current or planned disruptions for both road and
public transport.</p><br />

<p>Visit mobile.transportdirect.info on your phone or PDA or use our <a href=
"/Web2/TDOnTheMove/TDOnTheMove.aspx">mobile simulator</a> to see how the service works
and to send a link free-of-charge to your phone or PDA.</p><br clear="left" />

<p>&nbsp;</p>

<h3>Speed up my searches with a toolbar...</h3>

<p>&nbsp;</p>

<p>Now you can search for journeys more quickly and easily by downloading our free
toolbar.</p>

<p>&nbsp;</p><img alt=
"Screenshot of the Transport Direct toolbar when added to a browser menu bar" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeTipsToolsToolbar.JPG"
border="0" />

<p>With the toolbar, you can plan a journey without having to open the Transport Direct
site. You can also easily access maps pages and live travel news in a single click.</p>

<p>&nbsp;</p>

<p>Go to the <a href="/Web2/Tools/ToolbarDownload.aspx">toolbar</a> page to find out more
and to download it for free.</p>

<p>&nbsp;</p>

<h3>I want to add Transport Direct features to my website...</h3>

<p>&nbsp;</p>

<p>We can provide you with links and fields for your website that will allow your
customers, guests or employees to quickly and easily find out how to get to&nbsp;one or
more locations of your choosing.</p>

<p>&nbsp;</p><img style="PADDING-RIGHT: 20px" alt=
"Image of a sample Transport Direct planner which &#13; can be added to a website" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeTipsToolsAddToSite.JPG"
align="left" border="0" />

<p>You can place our features on your site, either as a simple text link, a Transport
Direct mini-form or a form with your site''s formatting and visual branding.<br />
<br />
You can do this in three easy steps by visiting <a href=
"/Web2/Tools/BusinessLinks.aspx">Add Transport Direct to your website for
free</a>.</p><br clear="left" />

<p>&nbsp;</p>

<p>&nbsp;</p>

<h3>Get stats and directions for multiple journeys...</h3>

<p>&nbsp;</p>

<p>Are you a travel planner or an employer considering relocation?  
Our new Batch Journey Planner can provide step-by-step directions 
or statistics for a large number of journeys.  So whether you need 
to give a number of people personalised journey plans or view distances, 
durations and CO2 for a set of journeys, the batch journey planner 
could be for you.  It''s free and easy to use.  See the 
<a href="/Web2/Downloads/BatchJourneyPlannerBrochure.pdf">brochure</a> for more details.</p>

<p>&nbsp;</p>

<p>&nbsp;</p>

<h3>I use a screen-reader...</h3>

<p>&nbsp;</p>

<p>We aim to provide an equivalent experience for screen-reader users as well as users
who do not need this technology. In order for you to best understand how the site works
and how to get the most out of the journey planning features, visit the <a href=
"/Web2/About/Accessibility.aspx">Accessibility</a> section.</p>
</div>
</div>
'
-- WELSH
,
'<div class="MinihomeHyperlinksInfoContent">
<div class="MinihomeHyperlinksInfoHeader">
<div class="txtsevenbbl">
<h2>Dywedwch fwy wrthyf...</h2>
<!-- New heading text should read "Helping you get the most from Transport Direct" replacing "Tell me more" -->
</div>
<!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>

<div class="MinihomeSoftContent" summary="Tips and tools content">
<h3>Cymharwch allyriadau CO2 ar gyfer eich siwrnai...</h3>

<p>&nbsp;</p>

<p>Nawr fe allwch gymharu allyriadau CO2 y pedwar prif fath o drafnidiaeth (Car,
Tr&ecirc;n, Bws/coets, ac Awyren) ar gyfer eich siwrnai.</p><br />
&nbsp;

<p><img alt="Llun sgrin o dudalen Allyriadau CO2" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeTipsToolsEmissions2.jpg"
align="right" border="0" />Gallwch gymharu allyriadau yn syml drwy nodi pellter mewn
milltiroedd neu gilometrau. Bydd Transport Direct yn dweud wrthych faint o CO2 fyddai''n
cael ei ollwng pe byddech chi''n teithio''r pellter hwnnw mewn car, tr&ecirc;n, bws, neu
awyren.</p><br clear="right" />

<p>&nbsp;</p>

<p><img alt="Llun sgrin o Allyriadau CO2 ar gyfer eich siwrnai" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeTipsToolsEmissionsJourney2.jpg"
align="left" border="0" />Neu edrychwch ar yr allyriadau ar gyfer eich siwrnai yn &ocirc;l
gwahanol fathau o drafnidiaeth drwy ddilyn y cysylltau o dudalen manylion eich
siwrnai.</p><br clear="left" />

<p>&nbsp;</p>

<p>Ewch i''r dudalen <a href=
"/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx">allyriadau CO2</a> i gael
gwybod.</p>

<p>&nbsp;</p>

<h3>Cael Transport Direct i symud...</h3>

<p>&nbsp;</p>

<p>Gallwch hefyd gael ein gwasanaeth Transport Direct Symudol ar eich ff&ocirc;n symudol
neu PDA am ddim.</p><br />
<img style="PADDING-RIGHT: 10px" alt=
"Delwedd sgr&icirc;n o ysgogydd symudol yn dangos bwrdd ymadawiadau mewn amser real" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeTipsToolsMobile1.jpg"
align="left" border="0" />

<p>Gallwch chwilio am yr amserau y mae&rsquo;r tr&ecirc;n nesaf yn ymadael ac yn
cyrraedd, gweld a yw tr&ecirc;n yn rhedeg mewn pryd a chwilio am yr amserau ar gyfer
yfory.</p><br />

<p>Gallwch edrych ar amserau bysiau rhestredig (ar gyfer y rhan fwyaf o Brydain).</p><br />

<p>Gallwch hefyd weld a oes unrhyw achosion o darfu ar gludiant y ffyrdd a chludiant
cyhoeddus ar hyn o bryd neu rai wedi eu cynllunio.</p><br />

<p>Ymwelwch &acirc; mobile.transportdirect.info ar eich ff&ocirc;n neu PDA neu
defnyddiwch ein <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx">ysgogydd efelchydd</a> i
weld sut mae&rsquo;r gwasanaeth yn gweithio ac i anfon dolen am ddim i&rsquo;ch
ff&ocirc;n neu PDA.</p><br clear="left" />

<p>&nbsp;</p>

<h3>Cyflymwch fy chwiliadau gyda bar offer...</h3>

<p>&nbsp;</p>

<p>Yn awr gallwch chwilio am siwrneion yn gyflymach ac yn haws drwy lawrlwytho ein bar
offer am ddim.</p>

<p>&nbsp;</p><img alt=
"Delwedd sgr&icirc;n o far offer Transport Direct pan gaiff ei ychwanegu at far dewislen porwr"
src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeTipsToolsToolbar1.JPG"
border="0" />

<p>Gyda&rsquo;r bar offer gallwch gynllunio siwrnai heb orfod agor safle Transport
Direct. Gallwch hefyd gyrchu at dudalennau mapiau a newyddion teithio byw gydag un
clic.</p>

<p>&nbsp;</p>

<p>Ewch i&rsquo;r dudalen <a href="/Web2/Tools/ToolbarDownload.aspx">bar offer</a> i
ddarganfod mwy ac i&rsquo;w lawrlwytho am ddim.</p>

<p>&nbsp;</p>

<h3>Rwy&rsquo;n dymuno ychwanegu nodweddion Transport Direct at fy ngwefan...</h3>

<p>&nbsp;</p>

<p>Gallwn roi dolennau a meysydd i chi ar gyfer eich gwefan a fydd yn caniat&aacute;u
i&rsquo;ch cwsmeriaid, gwesteion neu weithwyr ddarganfod yn gyflym ac yn hawdd sut i fynd
i un neu fwy o leoliadau o&rsquo;ch dewis.</p>

<p>&nbsp;</p><img style="PADDING-RIGHT: 20px" alt=
"Delwedd o enghraifft o gynllunydd Transport Direct y gellir ei ychwanegu at wefan" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeTipsToolsAddToSite.jpg"
align="left" border="0" />

<p>Gallwch roi ein nodweddion ar eich safle, naill ai fel dolen testun syml, ffurf mini
Transport Direct neu ar ffurflen gyda brand a fformat eich safle.<br />
<br />
Gallwch wneud hyn mewn tri cham rhwydd drwy ymweld &acirc; <a href=
"/Web2/Tools/BusinessLinks.aspx">Ychwanegwch Transport Direct at eich gwefan am
ddim</a>.</p><br clear="left" />

<p>&nbsp;</p>

<p>&nbsp;</p>

<h3>Beth am gael ystadegau a chyfarwyddiadau ar gyfer nifer o deithiau...</h3>

<p>&nbsp;</p>

<p>Ydych chi''n gynllunydd teithio neu''n gyflogwr sy''n ystyried adleoli?  
Gall ein Cynllunydd Amldeithiau newydd ddarparu cyfarwyddiadau cam-wrth-gam neu ystadegau 
ar gyfer nifer fawr o deithiau.  Felly beth bynnag sydd ei angen arnoch, cynlluniau teithio 
personol i nifer o bobl neu weld pellterau, amseroedd teithio ac allyriadau CO2 ar gyfer 
set o deithiau, gallai''r cynllunydd amldeithiau fod yr union beth ichi. Mae''n rhad ac am 
ddim ac yn hawdd i''w ddefnyddio. 
<a href="/Web2/Downloads/BatchJourneyPlannerBrochure.pdf">I gael rhagor o fanylion, gweler y llyfryn.</a></p>

<p>&nbsp;</p>

<p>&nbsp;</p>

<h3>Rwy&rsquo;n defnyddio darllenwr sgr&icirc;n...</h3>

<p>&nbsp;</p>

<p>Anelwn at ddarparu profiad cyfwerth ar gyfer defnyddwyr darllenwyr sgr&icirc;n yn
ogystal &acirc; defnyddwyr nad oes arnynt angen y dechnoleg hon. Er mwyn i chi ddeall
orau sut mae''r safle yn gweithio a sut i gael y gorau o''r nodweddion cynllunio siwrnai,
ymwelwch &acirc;"r adran <a href=
"/TransportDirect/cy/About/Accessibility.aspx">Hygyrchedd</a> yn y tudalennau Amdanom
ni.</p>
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

SET @ScriptNumber = 10004
SET @ScriptDesc = 'Updates to Tips and Tools homepage information panel'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.14  $'

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

