-- ***********************************************
-- NAME 		: SCP10304_BBC_4_Content.sql
-- DESCRIPTION 		: Script to add specific content for a Theme - BBC
-- AUTHOR		: Mitesh Modi
-- DATE			: 17 Apr 2008 18:00:00
-- ************************************************

-----------------------------------------------------
-- SOFT CONTENT
-----------------------------------------------------

USE [Content]
GO

DECLARE @ThemeId INT
SET @ThemeId = 3

----------------------------------------------------------
-- Header text
----------------------------------------------------------

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HeaderControl.headerHomepageLink.AlternateText', 'BBC Travel News', 'BBC Travel News'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HeaderControl.TDSmallBannerImage.AlternateText', 'BBC Travel News', 'BBC Travel News'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HeaderControl.defaultActionButton.AlternateText', 'BBC Travel News', 'BBC Travel News'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'PrintableHeaderControl.transportDirectLogoImg.AlternateText', 'BBC Travel News', 'BBC Travel News'

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


----------------------------------------------------------
-- HomePage - Tips and Tools panel
----------------------------------------------------------

EXEC AddtblContent
@ThemeId, 15, 'TDTipsHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'<div class="Column2Header">  
<h2><a class="Column2HeaderLink" title="Go to Tips and tools page" href="/Web2/Tools/Home.aspx"><span class="txtsevenbwl">Tips and tools</span></a></h2>
<a class="txtsevenbwrlink" title="Go to Tips and tools page" href="/Web2/Tools/Home.aspx">More... </a>
<!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
</div>  
<div></div>  
<div class="Column2Content2">  
<table class="TipsToolsTable" cellspacing="5">  
<tbody>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx"><img title="Check journey CO2" height="30" alt="Check CO2 emissions" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/Co2_30x30.gif" width="30" /></a></td>  <td><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx">Check CO2 emissions</a></td></tr>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/Tools/BusinessLinks.aspx"><img title="Business Links" height="30" alt="Business Links" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70.gif" width="30" /></a></td>  <td><a href="/Web2/Tools/BusinessLinks.aspx">Add Transport Direct to your website for free</a></td></tr>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/Tools/ToolbarDownload.aspx"><img title="Toolbar download" height="30" alt="Toolbar download" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/Toolbox30.gif" width="30" /></a></td>  <td><a href="/Web2/Tools/ToolbarDownload.aspx">Speed up your travel searches with our free toolbar</a></td></tr>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/TDOnTheMove/TDOnTheMove.aspx"><img title="Services available on your mobile" height="30" alt="Services available on your mobile" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/phone.gif" width="30" /></a></td>  <td><a href="/Web2/TDOnTheMove/TDOnTheMove.aspx">Get live departures on your mobile</a></td></tr>  
</tbody></table></div>', 
'<div class="Column2Header">  
<h2><a class="Column2HeaderLink" title="Ewch i''r dudalen Awgrymiadau a thedynnau" href="/Web2/Tools/Home.aspx"><span class="txtsevenbwl">Awgrymiadau a theclynnau</span></a></h2>
<a class="txtsevenbwrlink" title="Ewch i''r dudalen Awgrymiadau a thedynnau" href="/Web2/Tools/Home.aspx">Mwy... </a>
<!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
</div>  
<div></div>  
<div class="Column2Content2">  
<table class="TipsToolsTable" cellspacing="5">  <tbody>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx"><img title="Mesur CO2 y siwrnai" height="30" alt="Mesur CO2 y siwrnai" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/cy/Co2_30x30.gif" width="30" /></a></td>  <td><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx">Mesur CO2 y siwrnai</a></td></tr>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/Tools/BusinessLinks.aspx"><img title="Cysylltiadau Busnes" height="30" alt="Cysylltiadau Busnes" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70.gif" width="30" /></a></td>  <td><a href="/Web2/Tools/BusinessLinks.aspx">Ychwanegwch Transport Direct at eich gwefan am ddim</a></td></tr>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/Tools/ToolbarDownload.aspx"><img title="Lawrlwythwch y Bar Offer" height="30" alt="Lawrlwythwch y Bar Offer" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/Toolbox30.gif" width="30" /></a></td>  <td><a href="/Web2/Tools/ToolbarDownload.aspx">Cyflymwch eich chwiliadau teithio gyda''n bar offer am ddim</a></td></tr>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/TDOnTheMove/TDOnTheMove.aspx"><img title="Gwasanaethau sydd ar gael ar eich ff&#244;n symudol" height="30" alt="Gwasanaethau sydd ar gael ar eich ff&#244;n symudol" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/phone.gif" width="30" /></a></td>  <td><a href="/Web2/TDOnTheMove/TDOnTheMove.aspx">Derbyniwch ymadawiadau byw ar eich ff&#244;n symudol</a></td></tr>  </tbody></table></div>'


------------------------------------------------------------------------
-- Plan a Journey Home page information panel
------------------------------------------------------------------------

-- No changes


------------------------------------------------------------------------
-- Find A Place Home page information panel
------------------------------------------------------------------------

-- No changes


------------------------------------------------------------------------
-- Live Travel Home page information panel
------------------------------------------------------------------------

-- No changes


------------------------------------------------------------------------
-- Tips and tools Home page information panel
------------------------------------------------------------------------

-- No change


---------------------------------------------------------------
-- Home Page - Right hand info panel
---------------------------------------------------------------

-- No change


----------------------------------------------------------------
-- Sitemap
----------------------------------------------------------------

-- TDOnTheMove title
EXEC AddtblContent
@ThemeId, 43, 'TDOnTheMoveTitle', '/Channels/TransportDirect/SiteMap/SiteMap',
'<h3>About Transport Direct</h3>',
'<h3>Amdanom Transport Direct</h3>'

-- TDOnTheMove body
EXEC AddtblContent
@ThemeId, 43, 'TDOnTheMoveBody', '/Channels/TransportDirect/SiteMap/SiteMap',
'<div class="smcSiteMapLink"><a href="/Web2/About/AboutUs.aspx">About Transport Direct</a><br/>
<ul id="lister">
<li><a href="/Web2/About/AboutUs.aspx#EnablingIntelligentTravel">Enabling intelligent travel</a><br/></li>
<li><a href="/Web2/About/AboutUs.aspx#WhoOperates">Who operates Transport Direct? </a></li>
<li><a href="/Web2/About/AboutUs.aspx#WhoBuilds">Who builds this site?</a> </li>
</ul></div>
<div class="smcSiteMapLink"><a href="/Web2/About/Accessibility.aspx">Accessibility</a> <br/></div>
<div class="smcSiteMapLink"><a href="/Web2/ContactUs/Details.aspx">Contact details</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/About/DataProviders.aspx">Data providers</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/About/PrivacyPolicy.aspx">Privacy policy</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/About/TermsConditions.aspx">Terms &amp; conditions</a><br/></div>
', 

'<div class="smcSiteMapLink"><a href="/Web2/About/AboutUs.aspx">Amdanom Transport Direct</a><br/>
<ul id="lister">
<li><a href="/Web2/About/AboutUs.aspx#EnablingIntelligentTravel">Galluogi teithio deallus</a> </li>
<li><a href="/Web2/About/AboutUs.aspx#WhoOperates">Pwy sy''n gweithredu Transport Direct? </a></li>
<li><a href="/Web2/About/AboutUs.aspx#WhoBuilds">Pwy sy''n adeiladu a datblygu''r safle hwn?</a> </li>
</ul></div>
<div class="smcSiteMapLink"><a href="/Web2/About/Accessibility.aspx">Hygyrchedd</a> <br/></div>
<div class="smcSiteMapLink"><a href="/Web2/ContactUs/Details.aspx">Manylion cyswllt</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/About/DataProviders.aspx">Darparwyr data</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/About/PrivacyPolicy.aspx">Polisi preifatrwydd</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/About/TermsConditions.aspx">Amodau a thelerau</a><br/></div>
'

-- Remove the note at bottom of sitemap
EXEC AddtblContent
@ThemeId, 43, 'sitemapFooterNote', '/Channels/TransportDirect/SiteMap/SiteMap',
' ',
' '

GO

----------------------------------------------------------------
-- Ambiguity page
----------------------------------------------------------------

-- No change


----------------------------------------------------------------
-- Amend tab images
----------------------------------------------------------------

DECLARE @ThemeId INT
SET @ThemeId = 3

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendCarDetailsTabSelected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/AmendCarDetailsTabSelected.gif', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/AmendCarDetailsTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendCarDetailsTabUnselected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/AmendCarDetailsTabUnselected.gif','/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/AmendCarDetailsTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendDayTabSelected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/AmendDateTabSelected.gif','/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/AmendDateTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendDayTabUnselected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/AmendDateTabUnselected.gif','/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/AmendDateTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendTabSelected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/AmendTabSelected.gif','/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/AmendTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendTabUnselected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/AmendTabUnselected.gif','/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/AmendTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendViewTabSelected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/AmendViewTabSelected.gif','/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/AmendViewTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendViewTabUnSelected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/AmendViewTabUnSelected.gif','/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/AmendViewTabUnSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonCostSearchDateTabSelected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/AmendDateTabSelected.gif','/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/AmendDateTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonCostSearchDateTabUnselected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/AmendDateTabUnselected.gif','/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/AmendDateTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonFareTabSelected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/AmendFareDetailsTabSelected.gif','/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/AmendFareDetailsTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonFareTabUnselected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/AmendFareDetailsTabUnselected.gif','/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/AmendFareDetailsTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonStopoverTabSelected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/AmendStopoverBlue.gif','/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/AmendStopoverBlue.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonStopoverTabUnselected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/AmendStopoverGrey.gif','/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/AmendStopoverGrey.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonSaveTabSelected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/SaveTabSelected.gif', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/SaveTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonSaveTabUnselected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/SaveTabUnselected.gif', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/SaveTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonSendTabSelected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/SendTabSelected.gif', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/SendTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonSendTabUnselected', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/en/SendTabUnselected.gif', '/Web2/App_Themes/BBC/images/gifs/JourneyPlanning/cy/SendTabUnselected.gif'

GO


----------------------------------------------------------------
-- Powered by Transport Direct content
----------------------------------------------------------------

DECLARE @ThemeId INT
SET @ThemeId = 3

EXEC AddtblContent
@ThemeId, 1, 'langstrings', 'PoweredByControl.HTMLContent',
'<div class="Column3PoweredBy">
<div class="Column3Header Column3HeaderPoweredBy">
<div class="txtsevenbbl">
Powered by
<!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
</div>
</div>
<div class="Column3Content Column3ContentPoweredBy">
<table>
<tr>
<td>
<a href="/Web2/About/AboutUs.aspx">
<img src="/Web2/App_Themes/TransportDirect/images/gifs/td_logo_4whbg.gif" alt="Powered by Transport Direct" />
</a>
</td>
</tr>
</table>
</div>
</div>',

'<div class="Column3PoweredBy">
<div class="Column3Header Column3HeaderPoweredBy">
<div class="txtsevenbbl">
Powered by
<!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
</div>
</div>
<div class="Column3Content Column3ContentPoweredBy">
<table>
<tr>
<td>
<a href="/Web2/About/AboutUs.aspx">
<img src="/Web2/App_Themes/TransportDirect/images/gifs/td_logo_4whbg.gif" alt="Powered by Transport Direct" />
</a>
</td>
</tr>
</table>
</div>
</div>'

EXEC AddtblContent
@ThemeId, 1, 'langstrings', 'PoweredByControl.HTMLContent.LogoOnly',
'<div class="PoweredByLogo">
<a href="/Web2/About/AboutUs.aspx">
<img src="/Web2/App_Themes/TransportDirect/images/gifs/Poweredby1a.gif" alt="Powered by Transport Direct" />
</a>
</div>',

'<div class="PoweredByLogo">
<a href="/Web2/About/AboutUs.aspx">
<img src="/Web2/App_Themes/TransportDirect/images/gifs/Poweredby1a.gif" alt="Powered by Transport Direct" />
</a>
</div>'


----------------------------------------------------------
-- Map Symbols disclaimer text
----------------------------------------------------------

-- No change


----------------------------------------------------------------
-- Wait page
----------------------------------------------------------------

EXEC AddtblContent
@ThemeId, 32, 'MessageDefinition', '/Channels/TransportDirect/JourneyPlanning/WaitPage',
'We are always seeking to improve our service. If you cannot find what you want, please tell Transport Direct by clicking <b>Contact Transport Direct</b>',
'cy - We are always seeking to improve our service. If you cannot find what you want, please tell Transport Direct by clicking <b>Contact Transport Direct</b>'

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10304
SET @ScriptDesc = 'Content added for theme BBC'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.8  $'

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
