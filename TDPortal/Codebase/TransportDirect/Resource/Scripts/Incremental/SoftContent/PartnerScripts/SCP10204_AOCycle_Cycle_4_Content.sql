-- *************************************************************************************
-- NAME 		: SCP10204_AOCycle_Cycle_4_Content.sql
-- DESCRIPTION  : Content for AOCycle Cycle WhiteLabel site
-- AUTHOR		: Amit Patel
-- *************************************************************************************

-- ************************************************
-- NOTE: AOCycle partner setup purely for test purpose
-- ************************************************

USE [Content]
GO


DECLARE @ThemeId INT
SET @ThemeId = 200


----------------------------------------------------------------
-- Page Headings
----------------------------------------------------------------

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpCarbon', 
'<div><h1>Frequently Asked Questions (FAQ) - CO2 Information </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Gwybodaeth CO2 </h1></div>'


EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/Help/HelpCycle', 
'<div><h1>Frequently Asked Questions (FAQ) - Cycle Planning </h1></div>',
'<div><h1>Cwestiynau a Ofynnir yn Aml (COA) - Cynllunio Beicio </h1></div>'

----------------------------------------------------------
-- Header text
----------------------------------------------------------

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HeaderControl.headerHomepageLink.AlternateText', 'AOCycle cycle journey planner', 'AOCycle cycle journey planner'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HeaderControl.TDSmallBannerImage.AlternateText', 'AOCycle cycle journey planner', 'AOCycle cycle journey planner'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HeaderControl.defaultActionButton.AlternateText', 'AOCycle cycle journey planner', 'AOCycle cycle journey planner'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'PrintableHeaderControl.transportDirectLogoImg.AlternateText', 'AOCycle cycle journey planner', 'AOCycle cycle journey planner'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'PrintableHeaderControl.connectingPeopleImg.AlternateText', 'Provided by Transport Direct', 'Provided by Transport Direct'

----------------------------------------------------------
-- Footer links
----------------------------------------------------------

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'FooterControl1.ContactUsLinkButton', 'Contact Transport Direct', 'Cysylltu â Transport Direct '

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'FooterControl1.AboutLinkButton', 'About Transport Direct', 'Amdanom Transport Direct '


----------------------------------------------------------
-- Contact us page
----------------------------------------------------------

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'FeedbackInitialPage.ContactUsLabel', 'Contact Transport Direct', 'cy Contact Transport Direct'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'FeedbackInitialPage.labelTitle.Text', 'Send Transport Direct your feedback', 'cy Send Transport Direct your feedback'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HomeTipsTools.imageFeedback.AlternateText', 'Send Transport Direct your feedback', 'cy Send Transport Direct your feedback'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HomeTipsTools.imageFeedbackSkipLink.AlternateText', 'Skip to Send Transport Direct your feedback', 'cy Skip to Send Transport Direct your feedback'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HomeTipsTools.lblFeedback', 'Send Transport Direct <br /> your feedback', 'cy Send Transport Direct <br />your feedback'

----------------------------------------------------------
-- About us page
----------------------------------------------------------

EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/About/AboutUs', 'About Transport Direct', 'cy About Transport Direct'

GO

----------------------------------------------------------------
-- Powered by Transport Direct content
----------------------------------------------------------------

DECLARE @ThemeId INT
SET @ThemeId = 200


EXEC AddtblContent
@ThemeId, 1, 'langstrings', 'PoweredByControl.HTMLContent.LogoOnly',
'<div class="PoweredByLogo">
<a href="/Web2/About/AboutUs.aspx">
<img src="/Web2/App_Themes/TransportDirect/images/gifs/Poweredby1a.gif" alt="Powered by Transport Direct" />
</a>
</div>',

'<div class="PoweredByLogo">
<a href="/Web2/About/AboutUs.aspx">
<img src="/Web2/App_Themes/TransportDirect/images/gifs/Poweredby1a.gif" alt="Provided by Transport Direct" />
</a>
</div>'




----------------------------------------------------------------
-- Wait page
----------------------------------------------------------------

EXEC AddtblContent
@ThemeId, 32, 'MessageDefinition', '/Channels/TransportDirect/JourneyPlanning/WaitPage',
'We are always seeking to improve our service. If you cannot find what you want, please tell Transport Direct by clicking <b>Contact Transport Direct</b>',
'cy - We are always seeking to improve our service. If you cannot find what you want, please tell Transport Direct by clicking <b>Contact Transport Direct</b>'


----------------------------------------------------------------
-- Cycle planner FAQ links
----------------------------------------------------------------


EXEC AddtblContent
1, 1, 'langStrings', 'CyclePlanner.CycleJourneyGPXControl.labelDownloadDescription.Text', 
'Click on the link below to save a GPX file <a href="/Web2/Help/HelpCycle.aspx#A2.12">(What is GPX?)</a> of your route for use on a GPS device', 
'Cliciwch ar y ddolen isod i gadw ffeil GPX <a href="/Web2/Help/HelpCycle.aspx#A2.12">(Beth yw GPX)</a> o''ch llwybr i''w defnyddio ar ddyfais GPS'


GO



----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10204
SET @ScriptDesc = 'Contents for Cycling white label partner AOCycle'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.3  $'

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

