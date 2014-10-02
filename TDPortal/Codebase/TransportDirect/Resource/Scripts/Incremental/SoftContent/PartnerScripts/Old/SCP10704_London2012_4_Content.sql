-- ***********************************************
-- NAME 		: SCP10704_London2012_4_Content.sql
-- DESCRIPTION 		: Script to add specific content for a Theme - London2012
-- AUTHOR		: Mitesh Modi
-- DATE			: 02 Sep 2008
-- ************************************************

-----------------------------------------------------
-- SOFT CONTENT
-----------------------------------------------------

USE [Content]
GO

DECLARE @ThemeId INT
SET @ThemeId = 8

----------------------------------------------------------
-- Header text
----------------------------------------------------------

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HeaderControl.headerHomepageLink.AlternateText', 'London 2012', 'London 2012'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HeaderControl.TDSmallBannerImage.AlternateText', 'London 2012', 'London 2012'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HeaderControl.defaultActionButton.AlternateText', 'London 2012', 'London 2012'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'PrintableHeaderControl.transportDirectLogoImg.AlternateText', 'London 2012', 'London 2012'

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

-- No changes


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
'<H3>About Transport Direct</H3>',
'<H3>Amdanom Transport Direct</H3>'

-- TDOnTheMove body
EXEC AddtblContent
@ThemeId, 43, 'TDOnTheMoveBody', '/Channels/TransportDirect/SiteMap/SiteMap',
'<P><DIV class="smcSiteMapLink"><A href="/Web2/About/AboutUs.aspx">About Transport Direct</A><BR>
<UL id="lister">
<LI><A href="/Web2/About/AboutUs.aspx#EnablingIntelligentTravel">Enabling intelligent travel</A><BR>
<LI><A href="/Web2/About/AboutUs.aspx#WhoOperates">Who operates Transport Direct? </A>
<LI><A href="/Web2/About/AboutUs.aspx#WhoBuilds">Who builds this site?</A> 
<LI><A href="/Web2/About/AboutUs.aspx#WhatsNext">What''s next?</A> </LI></UL></DIV>
<P><DIV class="smcSiteMapLink"><A href="/Web2/About/Accessibility.aspx">Accessibility</A> <BR></DIV>
<DIV class="smcSiteMapLink"><A href="/Web2/ContactUs/Details.aspx">Contact details</A><BR></DIV>
<DIV class="smcSiteMapLink"><A href="/Web2/About/DataProviders.aspx">Data providers</A><BR></DIV>
<DIV class="smcSiteMapLink"><A href="/Web2/About/PrivacyPolicy.aspx">Privacy policy</A><BR></DIV>
<DIV class="smcSiteMapLink"><A href="/Web2/About/TermsConditions.aspx">Terms &amp; conditions</A><BR></DIV>
</P>', 

'<P><DIV class="smcSiteMapLink"><A href="/Web2/About/AboutUs.aspx">Amdanom Transport Direct</A><BR>
<UL id="lister">
<LI><A href="/Web2/About/AboutUs.aspx#EnablingIntelligentTravel">Galluogi teithio deallus</A> 
<LI><A href="/Web2/About/AboutUs.aspx#WhoOperates">Pwy sy''n gweithredu Transport Direct? </A>
<LI><A href="/Web2/About/AboutUs.aspx#WhoBuilds">Pwy sy''n adeiladu a datblygu''r safle hwn?</A> 
<LI><A href="/Web2/About/AboutUs.aspx#WhatsNext">Beth nesaf?</A> </LI></UL></DIV>
<P><DIV class="smcSiteMapLink"><A href="/Web2/About/Accessibility.aspx">Hygyrchedd</A> <BR></DIV>
<DIV class="smcSiteMapLink"><A href="/Web2/ContactUs/Details.aspx">Manylion cyswllt</A><BR></DIV>
<DIV class="smcSiteMapLink"><A href="/Web2/About/DataProviders.aspx">Darparwyr data</A><BR></DIV>
<DIV class="smcSiteMapLink"><A href="/Web2/About/PrivacyPolicy.aspx">Polisi preifatrwydd</A><BR></DIV>
<DIV class="smcSiteMapLink"><A href="/Web2/About/TermsConditions.aspx">Amodau a thelerau</A><BR></DIV>
</P>'

-- Remove the note at bottom of sitemap
EXEC AddtblContent
@ThemeId, 43, 'sitemapFooterNote', '/Channels/TransportDirect/SiteMap/SiteMap',
' ',
' '

----------------------------------------------------------------
-- Ambiguity page
----------------------------------------------------------------

-- No change


----------------------------------------------------------------
-- Amend tab images
----------------------------------------------------------------

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendCarDetailsTabSelected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/AmendCarDetailsTabSelected.gif', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/AmendCarDetailsTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendCarDetailsTabUnselected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/AmendCarDetailsTabUnselected.gif','/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/AmendCarDetailsTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendDayTabSelected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/AmendDateTabSelected.gif','/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/AmendDateTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendDayTabUnselected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/AmendDateTabUnselected.gif','/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/AmendDateTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendTabSelected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/AmendTabSelected.gif','/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/AmendTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendTabUnselected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/AmendTabUnselected.gif','/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/AmendTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendViewTabSelected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/AmendViewTabSelected.gif','/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/AmendViewTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendViewTabUnSelected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/AmendViewTabUnSelected.gif','/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/AmendViewTabUnSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonCostSearchDateTabSelected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/AmendDateTabSelected.gif','/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/AmendDateTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonCostSearchDateTabUnselected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/AmendDateTabUnselected.gif','/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/AmendDateTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonFareTabSelected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/AmendFareDetailsTabSelected.gif','/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/AmendFareDetailsTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonFareTabUnselected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/AmendFareDetailsTabUnselected.gif','/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/AmendFareDetailsTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonStopoverTabSelected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/AmendStopoverBlue.gif','/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/AmendStopoverBlue.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonStopoverTabUnselected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/AmendStopoverGrey.gif','/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/AmendStopoverGrey.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonSaveTabSelected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/SaveTabSelected.gif', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/SaveTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonSaveTabUnselected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/SaveTabUnselected.gif', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/SaveTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonSendTabSelected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/SendTabSelected.gif', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/SendTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonSendTabUnselected', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/en/SendTabUnselected.gif', '/Web2/App_Themes/London2012/images/gifs/JourneyPlanning/cy/SendTabUnselected.gif'


----------------------------------------------------------------
-- Powered by Transport Direct content
----------------------------------------------------------------

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

SET @ScriptNumber = 10704
SET @ScriptDesc = 'Content added for theme London2012'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.2  $'

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
