-- ***********************************************
-- NAME 		: SCP10202_AOCycle_Cycle_2_Properties.sql
-- DESCRIPTION 	: Script to add cycle planner white label properties
-- AUTHOR		: Amit Patel
-- DATE			: 24 Mar 2010
-- ************************************************

-- ************************************************
-- NOTE: AOCycle partner setup purely for test purpose
-- ************************************************

USE [PermanentPortal]
GO

DECLARE @ThemeId int


SET @ThemeId = 200 -- Cycle planner and CO2 white labelling partners with be 200-299


---------------------------------------------------------------
-- property to hide/ show cycle planner pages header tab 
---------------------------------------------------------------

-- FindCycleInput page
IF not exists (select top 1 * from properties where pName = 'FindCycleInput.HeaderTab.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'FindCycleInput.HeaderTab.Visible', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'FindCycleInput.HeaderTab.Visible' and ThemeId = @ThemeId
END

-- CycleJourneyDetails page
IF not exists (select top 1 * from properties where pName = 'CycleJourneyDetails.HeaderTab.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'CycleJourneyDetails.HeaderTab.Visible', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'CycleJourneyDetails.HeaderTab.Visible' and ThemeId = @ThemeId
END



---------------------------------------------------------------
-- property to hide/ show header tab on Journey Summary page
---------------------------------------------------------------

IF not exists (select top 1 * from properties where pName = 'CycleJourneySummary.HeaderTab.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'CycleJourneySummary.HeaderTab.Visible', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'CycleJourneySummary.HeaderTab.Visible' and ThemeId = @ThemeId
END


---------------------------------------------------------------
-- property to hide/ show header tab on Journey Map page
---------------------------------------------------------------

IF not exists (select top 1 * from properties where pName = 'CycleJourneyMap.HeaderTab.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'CycleJourneyMap.HeaderTab.Visible', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'CycleJourneyMap.HeaderTab.Visible' and ThemeId = @ThemeId
END


---------------------------------------------------------------
-- property to hide/ show Co2 pages header tab 
---------------------------------------------------------------

IF not exists (select top 1 * from properties where pName = 'JourneyEmissionsCompareJourney.HeaderTab.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'JourneyEmissionsCompareJourney.HeaderTab.Visible', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'JourneyEmissionsCompareJourney.HeaderTab.Visible' and ThemeId = @ThemeId
END



---------------------------------------------------------------
-- property to hide/ show transport direct powered by logo
---------------------------------------------------------------

IF not exists (select top 1 * from properties where pName = 'PoweredByControl.ShowPoweredBy' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'PoweredByControl.ShowPoweredBy', 
		'true', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'true'
	where pname = 'PoweredByControl.ShowPoweredBy' and ThemeId = @ThemeId
END

---------------------------------------------------------------
-- properties to hide/ show footer navigation links
---------------------------------------------------------------

-- Help link
IF not exists (select top 1 * from properties where pName = 'FooterControl.HelpLink.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'FooterControl.HelpLink.Visible', 
		'true', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'true'
	where pname = 'FooterControl.HelpLink.Visible' and ThemeId = @ThemeId
END

-- About link
IF not exists (select top 1 * from properties where pName = 'FooterControl.AboutLink.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'FooterControl.AboutLink.Visible', 
		'true', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'true'
	where pname = 'FooterControl.AboutLink.Visible' and ThemeId = @ThemeId
END

-- Contact Us link
IF not exists (select top 1 * from properties where pName = 'FooterControl.ContactUsLink.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'FooterControl.ContactUsLink.Visible', 
		'true', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'true'
	where pname = 'FooterControl.ContactUsLink.Visible' and ThemeId = @ThemeId
END

-- Site Map link
IF not exists (select top 1 * from properties where pName = 'FooterControl.SiteMapLink.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'FooterControl.SiteMapLink.Visible', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'FooterControl.SiteMapLink.Visible' and ThemeId = @ThemeId
END

-- Language Switch link
IF not exists (select top 1 * from properties where pName = 'FooterControl.LanguageSwitchLink.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'FooterControl.LanguageSwitchLink.Visible', 
		'true', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'true'
	where pname = 'FooterControl.LanguageSwitchLink.Visible' and ThemeId = @ThemeId
END

-- Related Sites link
IF not exists (select top 1 * from properties where pName = 'FooterControl.RelatedSitesLink.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'FooterControl.RelatedSitesLink.Visible', 
		'true', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'true'
	where pname = 'FooterControl.RelatedSitesLink.Visible' and ThemeId = @ThemeId
END

-- TermsConditions link
IF not exists (select top 1 * from properties where pName = 'FooterControl.TermsConditionsLink.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'FooterControl.TermsConditionsLink.Visible', 
		'true', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'true'
	where pname = 'FooterControl.TermsConditionsLink.Visible' and ThemeId = @ThemeId
END

-- Privacy Policy link
IF not exists (select top 1 * from properties where pName = 'FooterControl.PrivacyPolicyLink.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'FooterControl.PrivacyPolicyLink.Visible', 
		'true', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'true'
	where pname = 'FooterControl.PrivacyPolicyLink.Visible' and ThemeId = @ThemeId
END

-- Data Providers link
IF not exists (select top 1 * from properties where pName = 'FooterControl.DataProvidersLink.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'FooterControl.DataProvidersLink.Visible', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'FooterControl.DataProvidersLink.Visible' and ThemeId = @ThemeId
END

-- Accessibility link
IF not exists (select top 1 * from properties where pName = 'FooterControl.AccessibilityLink.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'FooterControl.AccessibilityLink.Visible', 
		'true', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'true'
	where pname = 'FooterControl.AccessibilityLink.Visible' and ThemeId = @ThemeId
END

------------------------------------------------------------------------------------------
-- AmendSaveSend control 'Send to friend' and 'Save as a favourite journey' tabs visibility 
-------------------------------------------------------------------------------------------

-- 'Send to friend' tab visibility
IF not exists (select top 1 * from properties where pName = 'AmendSaveSendControl.SendEmail.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'AmendSaveSendControl.SendEmail.Visible', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'AmendSaveSendControl.SendEmail.Visible' and ThemeId = @ThemeId
END

-- 'Save as a favourite journey' tab visibility
IF not exists (select top 1 * from properties where pName = 'AmendSaveSendControl.SaveFavourite.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'AmendSaveSendControl.SaveFavourite.Visible', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'AmendSaveSendControl.SaveFavourite.Visible' and ThemeId = @ThemeId
END

------------------------------------------------------------------------------------------
-- Header control logo enabled/disabled. 
-------------------------------------------------------------------------------------------

IF not exists (select top 1 * from properties where pName = 'HeaderControl.headerHomepageLink.Enabled' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'HeaderControl.headerHomepageLink.Enabled', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'HeaderControl.headerHomepageLink.Enabled' and ThemeId = @ThemeId
END

------------------------------------------------------------------------------------------
-- Wait page tip of the day message 
-------------------------------------------------------------------------------------------

IF not exists (select top 1 * from properties where pName = 'WaitPage.tipOfDay.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'WaitPage.tipOfDay.Visible', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'WaitPage.tipOfDay.Visible' and ThemeId = @ThemeId
END

------------------------------------------------------------------------------------------
-- Wait page header tabs 
-------------------------------------------------------------------------------------------

IF not exists (select top 1 * from properties where pName = 'WaitPage.HeaderTab.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'WaitPage.HeaderTab.Visible', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'WaitPage.HeaderTab.Visible' and ThemeId = @ThemeId
END


------------------------------------------------------------------------------------------
-- static pages header tabs
-------------------------------------------------------------------------------------------

IF not exists (select top 1 * from properties where pName = 'Links.HeaderTab.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'Links.HeaderTab.Visible', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'Links.HeaderTab.Visible' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'Help.HeaderTab.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'Help.HeaderTab.Visible', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'Help.HeaderTab.Visible' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'HelpFullJP.HeaderTab.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'HelpFullJP.HeaderTab.Visible', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'HelpFullJP.HeaderTab.Visible' and ThemeId = @ThemeId
END




------------------------------------------------------------------------------------------
-- Feedback page header tabs
-------------------------------------------------------------------------------------------

-- static default page
IF not exists (select top 1 * from properties where pName = 'FeedbackPage.HeaderTab.Visible' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'FeedbackPage.HeaderTab.Visible', 
		'false', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'false'
	where pname = 'FeedbackPage.HeaderTab.Visible' and ThemeId = @ThemeId
END

---------------------------------------------------------------
-- property to set custom error home page link for cycle white label 
---------------------------------------------------------------

-- bool value to determine if custom page required
IF not exists (select top 1 * from properties where pName = 'ErrorPage.IsCustomHomePage' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'ErrorPage.IsCustomHomePage', 
		'true', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'true'
	where pname = 'ErrorPage.IsCustomHomePage' and ThemeId = @ThemeId
END

-- Custom home page url - relative to root application path
IF not exists (select top 1 * from properties where pName = 'ErrorPage.CustomHomePage.Url' and ThemeId = @ThemeId)
BEGIN
	insert into properties values (
		'ErrorPage.CustomHomePage.Url', 
		'/JourneyPlanning/findcycleinput.aspx', 
		'Web', 
		'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = '/JourneyPlanning/findcycleinput.aspx'
	where pname = 'ErrorPage.CustomHomePage.Url' and ThemeId = @ThemeId
END



------------------------------------------------------------------------------------------
-- Properties to turn off images and links on homepage and min homepages
-------------------------------------------------------------------------------------------
IF not exists (select top 1 * from properties where pName = 'CarParkingAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'CarParkingAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'CarParkingAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'CheckJourneyCO2Available' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'CheckJourneyCO2Available', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'CheckJourneyCO2Available' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'CompareCityToCityJourneyAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'CompareCityToCityJourneyAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'CompareCityToCityJourneyAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'DepartureBoardsAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'DepartureBoardsAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'DepartureBoardsAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'DigitalTVInfoAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'DigitalTVInfoAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'DigitalTVInfoAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'DoorToDoorJourneyAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'DoorToDoorJourneyAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'DoorToDoorJourneyAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'FAQAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'FAQAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'FAQAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'FeedbackAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'FeedbackAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'FeedbackAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'FindABusAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'FindABusAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'FindABusAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'FindACarAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'FindACarAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'FindACarAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'FindACoachAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'FindACoachAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'FindACoachAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'FindAFlightAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'FindAFlightAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'FindAFlightAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'FindAPlaceImageButtonAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'FindAPlaceImageButtonAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'FindAPlaceImageButtonAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'FindAStationAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'FindAStationAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'FindAStationAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'FindATrainAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'FindATrainAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'FindATrainAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'HomeImageButtonAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'HomeImageButtonAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'HomeImageButtonAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'JourneyPlannerLocationMapAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'JourneyPlannerLocationMapAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'JourneyPlannerLocationMapAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'LinkToWebsiteAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'LinkToWebsiteAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'LinkToWebsiteAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'LiveTravelImageButtonAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'LiveTravelImageButtonAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'LiveTravelImageButtonAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'LoginRegisterImageButtonAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'LoginRegisterImageButtonAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'LoginRegisterImageButtonAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'MobileDemonstratorAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'MobileDemonstratorAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'MobileDemonstratorAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'NetworkMapsAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'NetworkMapsAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'NetworkMapsAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'PlanADayTripAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'PlanADayTripAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'PlanADayTripAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'PlanAJourneyImageButtonAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'PlanAJourneyImageButtonAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'PlanAJourneyImageButtonAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'PlanToParkAndRideAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'PlanToParkAndRideAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'PlanToParkAndRideAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'RelatedSitesAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'RelatedSitesAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'RelatedSitesAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'TipsAndToolsAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'TipsAndToolsAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'TipsAndToolsAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'TipsAndToolsImageButtonAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'TipsAndToolsImageButtonAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'TipsAndToolsImageButtonAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'ToolbarDownloadAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'ToolbarDownloadAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'ToolbarDownloadAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'TrafficMapsAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'TrafficMapsAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'TrafficMapsAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'TravelNewsAvailable' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'TravelNewsAvailable', 
  'false', 
  '<DEFAULT>', 
  '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'TravelNewsAvailable' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'InternationalPlanner.Available' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'InternationalPlanner.Available', 
  'false', 
  'Web', 
  'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'InternationalPlanner.Available' and ThemeId = @ThemeId
END

IF not exists (select top 1 * from properties where pName = 'EnvironmentalBenefitsCalculator.Available' and ThemeId = @ThemeId)
BEGIN
 insert into properties values (
  'EnvironmentalBenefitsCalculator.Available', 
  'false', 
  'Web', 
  'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
 update properties 
 set pvalue = 'false'
 where pname = 'EnvironmentalBenefitsCalculator.Available' and ThemeId = @ThemeId
END


GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10202
SET @ScriptDesc = 'Script to add cycle planner white label properties'


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
