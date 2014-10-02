-- *************************************************************************************
-- NAME 		: SC10003_TransportDirect_Content_3_MiniHome_LiveTravel.sql
-- DESCRIPTION  : Updates to Live Travel homepage information panel
-- AUTHOR		: Mitesh Modi
-- DATE			: 16 Apr 2008 15:00:00
-- *************************************************************************************


USE [Content]
GO

DECLARE @GroupId int,
	@ThemeId int
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'livetravel_home')
SET @ThemeId = 1

-- Add the html text
EXEC AddtblContent
@ThemeId, @GroupId, 'HomeTravelInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/LiveTravel/Home'
-- ENGLISH
,
'<div class="MinihomeHyperlinksInfoContent">
<div class="MinihomeHyperlinksInfoHeader">
<div class="txtsevenbbl">
<h2>Finding your news</h2>
</div>
<!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>

<div class="MinihomeSoftContent">
<h3>Are there any incidents that could affect my journey?</h3>

<p>&nbsp;</p><img style="PADDING-LEFT: 30px" alt=
"Image of a map showing incident symbols" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeLiveTravelNewsIncidents.JPG"
align="right" border="0" />

<p>You can check for any incidents on the roads or the public transport network that may
affect your journey, either on a map or as a list, on our <a href=
"/Web2/LiveTravel/TravelNews.aspx">Live travel news</a> page.</p><br clear="right" />

<p>&nbsp;</p>

<p><img style="PADDING-RIGHT: 40px" alt=
"Screenshot of the map with additional hover-over info" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeLiveTravelNewsIncidentPopup1.JPG"
align="left" border="0" /></p>

<p>Hover-over the incident symbols on the map to find further details.<br />
<br />
We also show future planned roadworks and public transport engineering
works.</p><br clear="left" />

<p>&nbsp;</p>

<h3>When is the next train?</h3>

<p>&nbsp;</p>

<p>Find out when the next train or bus leaves, or whether there are any current delays to
your service, by visiting our <a href="/Web2/LiveTravel/DepartureBoards.aspx">departure
boards</a> page.</p>
</div>
</div>
'
-- WELSH
,
'<div class="MinihomeHyperlinksInfoContent">
<div class="MinihomeHyperlinksInfoHeader">
<div class="txtsevenbbl">
<h2>Dywedwch fwy wrthyf...</h2>
<!-- New heading should read "Finding your news" replacing "Tell me more" -->
</div>
<!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>

<div class="MinihomeSoftContent">
<h3>Oes yna unrhyw ddigwyddiadau a allai effeithio ar fy siwrnai?</h3>

<p>&nbsp;</p><img style="PADDING-LEFT: 30px" alt=
"Delwedd o fap yn dangos symbolau digwyddiadau" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeLiveTravelNewsIncidents.JPG"
align="right" border="0" />

<p>Gallwch weld a oes unrhyw ddigwyddiadau ar y ffyrdd neu''r rhwydwaith cludiant
cyhoeddus a all effeithio ar eich siwrnai, naill ai ar fap neu fel rhestr, ar ein tudalen
<a href="/Web2/LiveTravel/TravelNews.aspx">Newyddion teithio byw</a>.</p>
<br clear="right" />

<p>&nbsp;</p>

<p><img style="PADDING-RIGHT: 40px" alt=
"Delwedd sgr&Atilde;&reg;n o fap gyda gwybodaeth ychwanegol i hofran drosto" src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeLiveTravelNewsIncidentPopup2.JPG"
align="left" border="0" /></p>

<p>Arhoswch uwchben y symbolau digwyddiadau ar y map i ddarganfod manylion pellach.<br />
<br />
Rydym hefyd yn dangos gwaith ffyrdd sy''n cael eu cynllunio yn y dyfodol a gwaith
peirianyddol yn ymwneud â chludiant cyhoeddus.</p><br clear="left" />

<p>&nbsp;</p>

<h3>Pryd mae''r tr&Atilde;&ordf;n nesaf?</h3>

<p>&nbsp;</p>

<p>Darnganfyddwch pryd mae''r trên neu''r bws nesaf yn gadael, neu a oes
unrhyw oedi i''ch gwasanaeth chi ar hyn o bryd drwy ymweld â''n tudalen
<a href="/Web2/LiveTravel/DepartureBoards.aspx">byrddau cyrraedd a chychwyn</a>.</p>
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

SET @ScriptNumber = 10003
SET @ScriptDesc = 'Updates to Live Travel homepage information panel'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.4  $'

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

