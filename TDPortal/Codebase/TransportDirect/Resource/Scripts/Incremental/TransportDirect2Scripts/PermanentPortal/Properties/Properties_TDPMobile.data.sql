-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : TDPMobile properties data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

------------------------------------------------
-- 'TDPMobile' properties
------------------------------------------------

DECLARE @AID varchar(50) = 'TDPMobile'
DECLARE @GID varchar(50) = 'TDPortal'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

-- State Server
EXEC AddUpdateProperty 'StateServer.ApplicationName', 'TDPMobileStateServer', @AID, @GID, @PartnerID, @ThemeID

-- Styling
EXEC AddUpdateProperty 'Style.Version', 'Version', @AID, @GID, @PartnerID, @ThemeID

-- StyleSheets
EXEC AddUpdateProperty 'StyleSheet.Files.Keys', '', @AID, @GID, @PartnerID, @ThemeID

-- Javascripts
EXEC AddUpdateProperty 'Javscript.Files.Keys', 'jquery', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Javscript.File.jquery', 'https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js', @AID, @GID, @PartnerID, @ThemeID

-- Google Analytics and Adverts
EXEC AddUpdateProperty 'Analytics.Tag.Include.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Adverts.Tag.Include.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID

-- Canonical Tags
EXEC AddUpdateProperty 'Canonical.Tag.Include.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID

-- Language Link
EXEC AddUpdateProperty 'Header.Link.Language.Visible.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Header.Link.Language.Paralympics.Visible.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID

-- Menu - Pages to show from sitemap
EXEC AddUpdateProperty 'Header.Menu.Pages', 'KizoomRail,KizoomBus,KizoomTravelNews,MobileTravelNews,MobileInput,MobileTerms,MobilePrivacy,MobileCookie', @AID, @GID, @PartnerID, @ThemeID

-- JourneyOptions - Cycle
EXEC AddUpdateProperty 'CyclePlanner.Enabled.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID

-- JourneyOptions - Advanced Options
EXEC AddUpdateProperty 'PublicJourneyOptions.AdvancedOptions.Visible', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'PublicJourneyOptions.TransportModeOptions.Visible', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'PublicJourneyOptions.AccessibilityOptions.Visible', 'true', @AID, @GID, @PartnerID, @ThemeID

-- Transport modes to show for the TDPJourneyPlannerMode enum
EXEC AddUpdateProperty 'PublicJourneyOptions.TransportModes.PublicTransport', '1,2,3,4,5,6,7', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'PublicJourneyOptions.TransportModes.PublicTransport.1', 'Rail', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'PublicJourneyOptions.TransportModes.PublicTransport.2', 'Underground,Metro', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'PublicJourneyOptions.TransportModes.PublicTransport.3', 'Bus,Coach', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'PublicJourneyOptions.TransportModes.PublicTransport.4', 'Tram', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'PublicJourneyOptions.TransportModes.PublicTransport.5', 'Ferry', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'PublicJourneyOptions.TransportModes.PublicTransport.6', 'Air', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'PublicJourneyOptions.TransportModes.PublicTransport.7', 'Telecabine', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'PublicJourneyOptions.TransportModes.AllSelected.Order', 'Rail,Underground,Bus,Tram,Ferry', @AID, @GID, @PartnerID, @ThemeID

-- Journey Location Control
--EXEC AddUpdateProperty 'ScriptRepository.LocationSuggest.Script.Name.Mobile', 'Location_NoLocalitiesNoPOIs_', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ScriptRepository.LocationSuggest.Script.Name.Mobile', 'Location_NoLocalities_', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ScriptRepository.LocationSuggest.ScriptPath', 'http://maps.transportdirect.info/output/javascript/', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ScriptRepository.LocationSuggest.ScriptPath.IncludeVersion', 'false', @AID, @GID, @PartnerID, @ThemeID

-- Journey Location - Toggle
EXEC AddUpdateProperty 'JourneyLocations.Toggle.Enabled.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID

-- Journey Options - Wait/Refresh
EXEC AddUpdateProperty 'JourneySummary.Wait.RefreshTime.Seconds', '5', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneySummary.Wait.RefreshCount.Max', '6', @AID, @GID, @PartnerID, @ThemeID

-- Journey Options - Return Journey button
EXEC AddUpdateProperty 'JourneySummary.PlanReturnJourney.Visible.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID

-- Journey Options - Replan
EXEC AddUpdateProperty 'DetailsSummaryControl.Replan.EarlierLink.Visible.Outward.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DetailsSummaryControl.Replan.EarlierLink.MaxJourneys.Visible', '4', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'DetailsSummaryControl.Replan.LaterLink.Visible.Outward.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DetailsSummaryControl.Replan.LaterLink.MaxJourneys.Visible', '4', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneySummary.Replan.RetainPreviousJourneys.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneySummary.Replan.RetainPreviousJourneysWhenNoResults.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID

-- Map
EXEC AddUpdateProperty 'Map.Enabled.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Map.Input.Enabled.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Map.Summary.Enabled.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Map.Journey.Cycle.Enabled.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Map.Journey.Walk.Enabled.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID

-- Landing Page
EXEC AddUpdateProperty 'LandingPage.Location.Coordinate.LatitudeLongitude.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID

-- Travel News
EXEC AddUpdateProperty 'TravelNews.DefaultPage.Enabled.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'TravelNews.Enabled.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID

-- Venue Travel News
EXEC AddUpdateProperty 'VenueNews.Enabled.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID

-- Travel News - Auto Refresh
EXEC AddUpdateProperty 'TravelNews.AutoRefresh.Enabled.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'TravelNews.AutoRefresh.ShowRefreshLink.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'TravelNews.AutoRefresh.Refresh.Seconds', '360', @AID, @GID, @PartnerID, @ThemeID

-- Underground News
EXEC AddUpdateProperty 'UndergroundNews.Enabled.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'UndergroundNews.ExpiryTime.Minutes', '60', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'UndergroundNews.Status.GoodService.Id', 'GS', @AID, @GID, @PartnerID, @ThemeID

-- Accessiblity
EXEC AddUpdateProperty 'AccessibleOptions.IsPointAccessible.Stops.SearchDistance.Metres', '1400', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'AccessibleOptions.IsPointAccessible.Localities.SearchDistance.Metres', '1400', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'AccessibleOptions.FindNearestLocations.Stops.Count.Max', '10', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'AccessibleOptions.FindNearestLocations.Localities.Count.Max', '3', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'AccessibleOptions.FindNearestLocations.Localities.SearchDistance.Metres', '20000', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'AccessibleOptions.FindNearestLocations.Stops.SearchDistance.Metres', '20000', @AID, @GID, @PartnerID, @ThemeID

GO


-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'TDPMobile shared properties data'