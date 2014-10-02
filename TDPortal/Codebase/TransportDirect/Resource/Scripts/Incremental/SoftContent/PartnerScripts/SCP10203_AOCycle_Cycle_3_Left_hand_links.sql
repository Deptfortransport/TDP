-- ***********************************************
-- NAME 		: SCP10203_AOCycle_Cycle_3_Left_hand_links.sql
-- DESCRIPTION 	: Script to add all the left hand links for AOCycle Cycle Only partner theme
-- AUTHOR		: Amit Patel
-- DATE			: 26 Mar 2008
-- ************************************************

-- IMPORTANT: 
-- See script DUP0790_CreateDefaultPartnerMenuLinks.sql to generate the default list of links to add

-- ************************************************
-- NOTE: AOCycle partner setup purely for test purpose
-- ************************************************

USE [TransientPortal]
GO

-- LINKS REMOVED
	-- All links except cycle planner, About us, TermsConditions, PrivacyPolicy, Related Sites
	-- All links in FAQ  except CyclePlanning and CO2Information
	--'FindATrainCost'
	--'MobileDemonstrator'
	--'AboutUs.WhatNext'
	
-- LINKS ADDED
	-- CO2 faq links to reflect the changes to faq content as described below:
		--* 'JourneyEmissions.EstimateCarCO2EmissionsInfo.CycleWhiteLabel'
		--* 'JourneyEmissions.AboutCarCO2Emissions.CycleWhiteLabel'
		--* 'JourneyEmissions.EstimatingPublicTransportCO2.CycleWhiteLabel'
		--* 'JourneyEmissions.CO2FromDifferentTransport.CycleWhiteLabel'
		--* 'JourneyEmissions.ComparingCarAndPublicTransport.CycleWhiteLabel'


-- MANUAL ADDITION OF LINKS
	-- 'Accessibility', 'AccessibilityMenu' Root item
	-- 'DataProviders', 'DataProvidersMenu' Root item
	-- 'PrivacyPolicy', 'PrivacyPolicyMenu' Root item
	-- 'TermsConditions', 'TermsConditionsMenu' Root item

-----------------------------------------------------
-- LINKS
-----------------------------------------------------

DECLARE @ThemeId INT
SET @ThemeId = 200 -- AOCycle

-- Clear all existing links for this Theme
DELETE FROM ContextSuggestionLink
WHERE     (ThemeId = @ThemeId)


-- Add new links
-- EXEC AddContextSuggestionLink @StringLinkResourceName, @LinkCategoryName, @StringContextName, @ThemeId

EXEC AddContextSuggestionLink 'AboutUs','AboutUs','AboutUsMenu', @ThemeId
EXEC AddContextSuggestionLink 'AboutUs.Introduction','AboutUs','AboutUsMenu', @ThemeId
EXEC AddContextSuggestionLink 'AboutUs.EnablingIntelligentTravel','AboutUs','AboutUsMenu', @ThemeId
EXEC AddContextSuggestionLink 'AboutUs.WhoOperates','AboutUs','AboutUsMenu', @ThemeId
EXEC AddContextSuggestionLink 'AboutUs.WhoBuilds','AboutUs','AboutUsMenu', @ThemeId
--EXEC AddContextSuggestionLink 'AboutUs.WhatNext','AboutUs','AboutUsMenu', @ThemeId
--EXEC AddContextSuggestionLink 'Accessibility','TermsAndPolicy','AccessibilityMenu', @ThemeId
EXEC AddContextSuggestionLink 'TermsConditions','TermsAndPolicy','AccessibilityMenu', @ThemeId
EXEC AddContextSuggestionLink 'PrivacyPolicy','TermsAndPolicy','AccessibilityMenu', @ThemeId
--EXEC AddContextSuggestionLink 'DataProviders','TermsAndPolicy','AccessibilityMenu', @ThemeId
EXEC AddContextSuggestionLink 'Accessibility','TermsAndPolicy','AccessibilityMenu', @ThemeId
--EXEC AddContextSuggestionLink 'DataProviders','TermsAndPolicy','DataProvidersMenu', @ThemeId
EXEC AddContextSuggestionLink 'TermsConditions','TermsAndPolicy','DataProvidersMenu', @ThemeId
EXEC AddContextSuggestionLink 'PrivacyPolicy','TermsAndPolicy','DataProvidersMenu', @ThemeId
--EXEC AddContextSuggestionLink 'DataProviders','TermsAndPolicy','DataProvidersMenu', @ThemeId
EXEC AddContextSuggestionLink 'Accessibility','TermsAndPolicy','DataProvidersMenu', @ThemeId
EXEC AddContextSuggestionLink 'FAQ','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.AboutTransportDirect','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.JourneyPlanning','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.Train','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.Road','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.Taxi','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.Parking','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.Air','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.Bus','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.LiveTravel','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.Maps','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.TicketsAndCosts','FAQ','FAQMenu', @ThemeId
EXEC AddContextSuggestionLink 'FAQ.CO2Information','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.UsingTheWebsite','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.MobileDeviceService','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.Comments','FAQ','FAQMenu', @ThemeId
EXEC AddContextSuggestionLink 'FAQ.CyclePlanning','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.TouristInfo','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FAQ.AccessibleJourneyPlanning','FAQ','FAQMenu', @ThemeId
--EXEC AddContextSuggestionLink 'ParkAndRide','General','FindCarInput', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','General','FindCarInput', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','General','FindCarInput', @ThemeId
--EXEC AddContextSuggestionLink 'FindNearestCarPark','General','FindCarInput', @ThemeId
--EXEC AddContextSuggestionLink 'ParkAndRideInput','General','FindCarInput', @ThemeId
--EXEC AddContextSuggestionLink 'FindNearestCarParkDriveTo','General','FindCarInput', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','Tips and tools','FindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards','Tips and tools','FindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'FindNearestStation/Airport','Tips and tools','FindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'FindAMap','Tips and tools','FindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps','Tips and tools','FindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'MobileDemonstrator','Tips and tools','FindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','Tips and tools','FindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','Tips and tools','FindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards','Tips and tools','FindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'FindNearestStation/Airport','Tips and tools','FindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'FindAMap','Tips and tools','FindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps','Tips and tools','FindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'MobileDemonstrator','Tips and tools','FindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','Tips and tools','FindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'PlanAJourneyHomepage','General','HomeFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','General','HomeFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','General','HomeFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'ParkAndRideInput','Plan a journey','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FindBusInput','Plan a journey','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'PlanAJourney','Plan a journey','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'DoorToDoor','Plan a journey','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FindATrain','Plan a journey','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FindAUKFlight','Plan a journey','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FindCarInput','Plan a journey','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FindACoach','Plan a journey','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'CompareCityToCity','Plan a journey','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'DayTripPlanner','Plan a journey','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FindAPlace','Find a place','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FindAMap','Find a place','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FindNearestStation/Airport','Find a place','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps','Find a place','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'TrafficLevels','Find a place','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravel','Live travel','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','Live travel','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards','Live travel','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'TipsAndTools','Tips and tools','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'LinksToOurWebsite','Tips and tools','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'ToolbarDownload','Tips and tools','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'MobileDemonstrator','Tips and tools','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'ProvideFeedback','Tips and tools','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedSites','Tips and tools','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','Tips and tools','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissionsCompare','Tips and tools','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FindNearestCarPark','Find a place','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FindATrainCost','Plan a journey','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FindNearestCarParkDriveTo','Plan a journey','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards.Train','Departure boards','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards.London','Departure boards','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards.Airport','Departure boards','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards.Bus','Departure boards','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'Feedback','Provide Feedback','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'ContactDetails','Provide Feedback','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'TravelInformation','Provide Feedback','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'ProvideFeedback.TourismInformation','Provide Feedback','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps.Train','Network maps','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps.Coach','Network maps','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps.UndergroundMetro','Network maps','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps.Bus','Network maps','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps.Ferries','Network maps','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps.Cycling','Network maps','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps.AllPublicTransport','Network maps','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps.Train','Network maps','HomePageMenuFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps.Coach','Network maps','HomePageMenuFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps.UndergroundMetro','Network maps','HomePageMenuFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps.Bus','Network maps','HomePageMenuFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps.Ferries','Network maps','HomePageMenuFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps.Cycling','Network maps','HomePageMenuFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps.AllPublicTransport','Network maps','HomePageMenuFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'FindAPlace','Find a place','HomePageMenuFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'FindAMap','Find a place','HomePageMenuFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'FindNearestStation/Airport','Find a place','HomePageMenuFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'FindNearestCarPark','Find a place','HomePageMenuFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps','Find a place','HomePageMenuFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'TrafficLevels','Find a place','HomePageMenuFindAPlace', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravel','Live travel','HomePageMenuLiveTravel', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','Live travel','HomePageMenuLiveTravel', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards','Live travel','HomePageMenuLiveTravel', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards.Train','Departure boards','HomePageMenuLiveTravel', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards.London','Departure boards','HomePageMenuLiveTravel', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards.Airport','Departure boards','HomePageMenuLiveTravel', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards.Bus','Departure boards','HomePageMenuLiveTravel', @ThemeId
--EXEC AddContextSuggestionLink 'UserLoggedIn','UserLoggedIn','HomePageMenuLoggedIn', @ThemeId
--EXEC AddContextSuggestionLink 'UserLoggedIn.UpdateEmailAddress','UserLoggedIn','HomePageMenuLoggedIn', @ThemeId
--EXEC AddContextSuggestionLink 'UserLoggedIn.DeleteAccount','UserLoggedIn','HomePageMenuLoggedIn', @ThemeId
--EXEC AddContextSuggestionLink 'UserLoggedIn.LogOut','UserLoggedIn','HomePageMenuLoggedIn', @ThemeId
--EXEC AddContextSuggestionLink 'LoginRegister','LoginRegister','HomePageMenuLoginRegister', @ThemeId
--EXEC AddContextSuggestionLink 'LoginRegister.ExistingUser','LoginRegister','HomePageMenuLoginRegister', @ThemeId
--EXEC AddContextSuggestionLink 'LoginRegister.Register','LoginRegister','HomePageMenuLoginRegister', @ThemeId
--EXEC AddContextSuggestionLink 'LoginRegister.WhyRegister','LoginRegister','HomePageMenuLoginRegister', @ThemeId
--EXEC AddContextSuggestionLink 'FindATrainCost','Plan a journey','HomePageMenuPlanAJourney', @ThemeId
EXEC AddContextSuggestionLink 'PlanAJourney','Plan a journey','HomePageMenuPlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'DoorToDoor','Plan a journey','HomePageMenuPlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'FindATrain','Plan a journey','HomePageMenuPlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'FindAUKFlight','Plan a journey','HomePageMenuPlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'FindCarInput','Plan a journey','HomePageMenuPlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'FindACoach','Plan a journey','HomePageMenuPlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'CompareCityToCity','Plan a journey','HomePageMenuPlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'DayTripPlanner','Plan a journey','HomePageMenuPlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'ParkAndRideInput','Plan a journey','HomePageMenuPlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'FindBusInput','Plan a journey','HomePageMenuPlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'FindNearestCarParkDriveTo','Plan a journey','HomePageMenuPlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'TipsAndTools','Tips and tools','HomePageMenuTipsAndTools', @ThemeId
--EXEC AddContextSuggestionLink 'LinksToOurWebsite','Tips and tools','HomePageMenuTipsAndTools', @ThemeId
--EXEC AddContextSuggestionLink 'ToolbarDownload','Tips and tools','HomePageMenuTipsAndTools', @ThemeId
--EXEC AddContextSuggestionLink 'MobileDemonstrator','Tips and tools','HomePageMenuTipsAndTools', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissionsCompare','Tips and tools','HomePageMenuTipsAndTools', @ThemeId
--EXEC AddContextSuggestionLink 'ProvideFeedback','Tips and tools','HomePageMenuTipsAndTools', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedSites','Tips and tools','HomePageMenuTipsAndTools', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','Tips and tools','HomePageMenuTipsAndTools', @ThemeId
--EXEC AddContextSuggestionLink 'Feedback','Provide Feedback','HomePageMenuTipsAndTools', @ThemeId
--EXEC AddContextSuggestionLink 'ContactDetails','Provide Feedback','HomePageMenuTipsAndTools', @ThemeId
--EXEC AddContextSuggestionLink 'TravelInformation','Provide Feedback','HomePageMenuTipsAndTools', @ThemeId
--EXEC AddContextSuggestionLink 'ProvideFeedback.TourismInformation','Provide Feedback','HomePageMenuTipsAndTools', @ThemeId
--EXEC AddContextSuggestionLink 'ParkAndRide','General','HomePlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','General','HomePlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards','General','HomePlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'MobileDemonstrator','General','HomePlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','General','HomePlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'PlanAJourneyHomepage','General','HomeTipsTools', @ThemeId
--EXEC AddContextSuggestionLink 'PlanAJourneyHomepage','General','HomeTravelInfo', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.OffsetCarbonEmissions','General','JourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.CompareFuelEfficiency','General','JourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.FuelSavingTips','General','JourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.MotoringAndEnvironment','General','JourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.Flying','General','JourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.ActOnCo2','General','JourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.RelatedSitesCarSharing','General','JourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.EstimateCarCO2Emissions','General','JourneyEmissions', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.OffsetCarbonEmissions','General','JourneyEmissionsCompare', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.CompareFuelEfficiency','General','JourneyEmissionsCompare', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.FuelSavingTips','General','JourneyEmissionsCompare', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.MotoringAndEnvironment','General','JourneyEmissionsCompare', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.Flying','General','JourneyEmissionsCompare', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.PT.AirPollutants','General','JourneyEmissionsCompare', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.ActOnCo2','General','JourneyEmissionsCompare', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.Co2Railway','General','JourneyEmissionsCompare', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.RelatedSitesCarSharing','General','JourneyEmissionsCompare', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.EstimateCarCO2EmissionsInfo','General','JourneyEmissionsCompareInfo', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.AboutCarCO2Emissions','General','JourneyEmissionsCompareInfo', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.EstimatingPublicTransportCO2','General','JourneyEmissionsCompareInfo', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.CO2FromDifferentTransport','General','JourneyEmissionsCompareInfo', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.ComparingCarAndPublicTransport','General','JourneyEmissionsCompareInfo', @ThemeId
--EXEC AddContextSuggestionLink 'FindCarInput','General','ParkAndRide', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','General','ParkAndRide', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','General','ParkAndRide', @ThemeId
--EXEC AddContextSuggestionLink 'FindCarInput','General','ParkAndRideInput', @ThemeId
--EXEC AddContextSuggestionLink 'ParkAndRide','General','ParkAndRideInput', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','General','ParkAndRideInput', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','General','ParkAndRideInput', @ThemeId
EXEC AddContextSuggestionLink 'PrivacyPolicy','TermsAndPolicy','PrivacyPolicyMenu', @ThemeId
EXEC AddContextSuggestionLink 'TermsConditions','TermsAndPolicy','PrivacyPolicyMenu', @ThemeId
EXEC AddContextSuggestionLink 'PrivacyPolicy','TermsAndPolicy','PrivacyPolicyMenu', @ThemeId
--EXEC AddContextSuggestionLink 'DataProviders','TermsAndPolicy','PrivacyPolicyMenu', @ThemeId
EXEC AddContextSuggestionLink 'Accessibility','TermsAndPolicy','PrivacyPolicyMenu', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextExtendJourneyInput', @ThemeId
--EXEC AddContextSuggestionLink 'DisabilityInformation','Related links','RelatedLinksContextExtendJourneyInput', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextFindAStationInput', @ThemeId
--EXEC AddContextSuggestionLink 'DisabilityInformation','Related links','RelatedLinksContextFindAStationInput', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextFindBusInput', @ThemeId
--EXEC AddContextSuggestionLink 'DisabilityInformation','Related links','RelatedLinksContextFindBusInput', @ThemeId
--EXEC AddContextSuggestionLink 'DisabilityInformation','Related links','RelatedLinksContextFindCarInput', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextFindCarInput', @ThemeId
--EXEC AddContextSuggestionLink 'FindNearestCarPark','Related links','RelatedLinksContextFindCarInput', @ThemeId
--EXEC AddContextSuggestionLink 'ParkAndRide','Related links','RelatedLinksContextFindCarInput', @ThemeId
--EXEC AddContextSuggestionLink 'FindNearestCarParkPlanTo','Related links','RelatedLinksContextFindCarInput', @ThemeId
--EXEC AddContextSuggestionLink 'ParkAndRideInput','Related links','RelatedLinksContextFindCarInput', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','Related links','RelatedLinksContextFindCarInput', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','Related links','RelatedLinksContextFindCarInput', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextFindCarParkInput', @ThemeId
--EXEC AddContextSuggestionLink 'DisabilityInformation','Related links','RelatedLinksContextFindCarParkInput', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextFindCoachInput', @ThemeId
--EXEC AddContextSuggestionLink 'DisabilityInformation','Related links','RelatedLinksContextFindCoachInput', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextFindFlightInput', @ThemeId
--EXEC AddContextSuggestionLink 'DisabilityInformation','Related links','RelatedLinksContextFindFlightInput', @ThemeId
--EXEC AddContextSuggestionLink 'DisabilityInformation','Related links','RelatedLinksContextFindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextFindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','Related links','RelatedLinksContextFindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards','Related links','RelatedLinksContextFindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'FindNearestStation/Airport','Related links','RelatedLinksContextFindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'FindAMap','Related links','RelatedLinksContextFindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps','Related links','RelatedLinksContextFindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'MobileDemonstrator','Related links','RelatedLinksContextFindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','Related links','RelatedLinksContextFindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextFindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','Related links','RelatedLinksContextFindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards','Related links','RelatedLinksContextFindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'FindNearestStation/Airport','Related links','RelatedLinksContextFindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'FindAMap','Related links','RelatedLinksContextFindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'NetworkMaps','Related links','RelatedLinksContextFindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'MobileDemonstrator','Related links','RelatedLinksContextFindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','Related links','RelatedLinksContextFindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'DisabilityInformation','Related links','RelatedLinksContextFindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextFindTrunkInput', @ThemeId
--EXEC AddContextSuggestionLink 'DisabilityInformation','Related links','RelatedLinksContextFindTrunkInput', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextHomeJourneyPlanning', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','Related links','RelatedLinksContextHomeJourneyPlanning', @ThemeId
--EXEC AddContextSuggestionLink 'DepartureBoards','Related links','RelatedLinksContextHomeJourneyPlanning', @ThemeId
--EXEC AddContextSuggestionLink 'MobileDemonstrator','Related links','RelatedLinksContextHomeJourneyPlanning', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','Related links','RelatedLinksContextHomeJourneyPlanning', @ThemeId
--EXEC AddContextSuggestionLink 'ParkAndRide','Related links','RelatedLinksContextHomeJourneyPlanning', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextHomeLiveTravel', @ThemeId
--EXEC AddContextSuggestionLink 'PlanAJourneyHomepage','Related links','RelatedLinksContextHomeLiveTravel', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextHomeMaps', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','Related links','RelatedLinksContextHomeMaps', @ThemeId
--EXEC AddContextSuggestionLink 'PlanAJourneyHomepage','Related links','RelatedLinksContextHomeMaps', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','Related links','RelatedLinksContextHomeMaps', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextHomeTools', @ThemeId
--EXEC AddContextSuggestionLink 'PlanAJourneyHomepage','Related links','RelatedLinksContextHomeTools', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextJourneyDetailsFindCarRoute', @ThemeId
--EXEC AddContextSuggestionLink 'RealTimeTraffic','Related links','RelatedLinksContextJourneyDetailsFindCarRoute', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextJourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.EstimateCarCO2Emissions','Related links','RelatedLinksContextJourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.OffsetCarbonEmissions','Related links','RelatedLinksContextJourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.CompareFuelEfficiency','Related links','RelatedLinksContextJourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.FuelSavingTips','Related links','RelatedLinksContextJourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.MotoringAndEnvironment','Related links','RelatedLinksContextJourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.Flying','Related links','RelatedLinksContextJourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.ActOnCo2','Related links','RelatedLinksContextJourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.RelatedSitesCarSharing','Related links','RelatedLinksContextJourneyEmissions', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextJourneyEmissionsCompare', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.OffsetCarbonEmissions','Related links','RelatedLinksContextJourneyEmissionsCompare', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.CompareFuelEfficiency','Related links','RelatedLinksContextJourneyEmissionsCompare', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.FuelSavingTips','Related links','RelatedLinksContextJourneyEmissionsCompare', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.MotoringAndEnvironment','Related links','RelatedLinksContextJourneyEmissionsCompare', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.Flying','Related links','RelatedLinksContextJourneyEmissionsCompare', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.PT.AirPollutants','Related links','RelatedLinksContextJourneyEmissionsCompare', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.ActOnCo2','Related links','RelatedLinksContextJourneyEmissionsCompare', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.Co2Railway','Related links','RelatedLinksContextJourneyEmissionsCompare', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.RelatedSitesCarSharing','Related links','RelatedLinksContextJourneyEmissionsCompare', @ThemeId
EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextJourneyEmissionsCompareJourney', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.OffsetCarbonEmissions','Related links','RelatedLinksContextJourneyEmissionsCompareJourney', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.CompareFuelEfficiency','Related links','RelatedLinksContextJourneyEmissionsCompareJourney', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.FuelSavingTips','Related links','RelatedLinksContextJourneyEmissionsCompareJourney', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.MotoringAndEnvironment','Related links','RelatedLinksContextJourneyEmissionsCompareJourney', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.Flying','Related links','RelatedLinksContextJourneyEmissionsCompareJourney', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.PT.AirPollutants','Related links','RelatedLinksContextJourneyEmissionsCompareJourney', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.ActOnCo2','Related links','RelatedLinksContextJourneyEmissionsCompareJourney', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.Co2Railway','Related links','RelatedLinksContextJourneyEmissionsCompareJourney', @ThemeId
--EXEC AddContextSuggestionLink 'JourneyEmissions.RelatedSitesCarSharing','Related links','RelatedLinksContextJourneyEmissionsCompareJourney', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextJourneyPlannerAmbiguity', @ThemeId
--EXEC AddContextSuggestionLink 'DisabilityInformation','Related links','RelatedLinksContextJourneyPlannerAmbiguity', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextJourneyPlannerInput', @ThemeId
--EXEC AddContextSuggestionLink 'DisabilityInformation','Related links','RelatedLinksContextJourneyPlannerInput', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextJourneySummaryFindCarRoute', @ThemeId
--EXEC AddContextSuggestionLink 'RealTimeTraffic','Related links','RelatedLinksContextJourneySummaryFindCarRoute', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextParkAndRide', @ThemeId
--EXEC AddContextSuggestionLink 'FindCarInput','Related links','RelatedLinksContextParkAndRide', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','Related links','RelatedLinksContextParkAndRide', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','Related links','RelatedLinksContextParkAndRide', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextParkAndRideInput', @ThemeId
--EXEC AddContextSuggestionLink 'FindCarInput','Related links','RelatedLinksContextParkAndRideInput', @ThemeId
--EXEC AddContextSuggestionLink 'ParkAndRide','Related links','RelatedLinksContextParkAndRideInput', @ThemeId
--EXEC AddContextSuggestionLink 'LiveTravelNews','Related links','RelatedLinksContextParkAndRideInput', @ThemeId
--EXEC AddContextSuggestionLink 'HELPFAQ','Related links','RelatedLinksContextParkAndRideInput', @ThemeId
--EXEC AddContextSuggestionLink 'DisabilityInformation','Related links','RelatedLinksContextParkAndRideInput', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextTrafficMaps', @ThemeId
--EXEC AddContextSuggestionLink 'RealTimeTraffic','Related links','RelatedLinksContextTrafficMaps', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related links','RelatedLinksContextVisitPlannerInput', @ThemeId
--EXEC AddContextSuggestionLink 'DisabilityInformation','Related links','RelatedLinksContextVisitPlannerInput', @ThemeId
EXEC AddContextSuggestionLink 'RelatedSites','RelatedSites','RelatedSitesMenu', @ThemeId
EXEC AddContextSuggestionLink 'RelatedSites.NationalTransport','RelatedSites','RelatedSitesMenu', @ThemeId
EXEC AddContextSuggestionLink 'RelatedSites.LocalPublicTransport','RelatedSites','RelatedSitesMenu', @ThemeId
EXEC AddContextSuggestionLink 'RelatedSites.Motoring','RelatedSites','RelatedSitesMenu', @ThemeId
EXEC AddContextSuggestionLink 'RelatedSites.MotoringCosts','RelatedSites','RelatedSitesMenu', @ThemeId
EXEC AddContextSuggestionLink 'RelatedSites.CarSharing','RelatedSites','RelatedSitesMenu', @ThemeId
EXEC AddContextSuggestionLink 'RelatedSites.Government','RelatedSites','RelatedSitesMenu', @ThemeId
EXEC AddContextSuggestionLink 'RelatedSites.TouristInfo','RelatedSites','RelatedSitesMenu', @ThemeId
--EXEC AddContextSuggestionLink 'TermsConditions','TermsAndPolicy','TermsConditionsMenu', @ThemeId
EXEC AddContextSuggestionLink 'TermsConditions','TermsAndPolicy','TermsConditionsMenu', @ThemeId
EXEC AddContextSuggestionLink 'PrivacyPolicy','TermsAndPolicy','TermsConditionsMenu', @ThemeId
--EXEC AddContextSuggestionLink 'DataProviders','TermsAndPolicy','TermsConditionsMenu', @ThemeId
EXEC AddContextSuggestionLink 'Accessibility','TermsAndPolicy','TermsConditionsMenu', @ThemeId
--EXEC AddContextSuggestionLink 'RelatedLinks','Related Links','RelatedLinksContextFindCarParkResults', @ThemeId
--EXEC AddContextSuggestionLink 'FindCarPark.ParkMarkScheme','Related links','RelatedLinksContextFindCarParkResults', @ThemeId
EXEC AddContextSuggestionLink 'FindACycle','Plan a journey','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'FindACycle','Plan a journey','HomePageMenuPlanAJourney', @ThemeId
EXEC AddContextSuggestionLink 'RelatedLinks','Related Links','RelatedLinksContextFindCycleInput', @ThemeId
EXEC AddContextSuggestionLink 'CyclePlanner.CyclingEngland','Related Links','RelatedLinksContextFindCycleInput', @ThemeId
EXEC AddContextSuggestionLink 'CyclePlanner.CyclingScotland','Related Links','RelatedLinksContextFindCycleInput', @ThemeId
EXEC AddContextSuggestionLink 'CyclePlanner.CyclingWales','Related Links','RelatedLinksContextFindCycleInput', @ThemeId
EXEC AddContextSuggestionLink 'CyclePlanner.NationalCycleRoutes','Related Links','RelatedLinksContextFindCycleInput', @ThemeId
EXEC AddContextSuggestionLink 'CyclePlanner.CycleScheme','Related Links','RelatedLinksContextFindCycleInput', @ThemeId
EXEC AddContextSuggestionLink 'CyclePlanner.CycleSafety','Related Links','RelatedLinksContextFindCycleInput', @ThemeId
EXEC AddContextSuggestionLink 'CyclePlanner.CycleSchool','Related Links','RelatedLinksContextFindCycleInput', @ThemeId
EXEC AddContextSuggestionLink 'CyclePlanner.GPXDownload','Related Links','RelatedLinksContextFindCycleInput', @ThemeId
EXEC AddContextSuggestionLink 'CyclePlanner.Parking','Related Links','RelatedLinksContextFindCycleInput', @ThemeId
--EXEC AddContextSuggestionLink 'EnvironmentalBenefitsCalculator','Tips and tools','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'EnvironmentalBenefitsCalculator','Tips and tools','HomePageMenuTipsAndTools', @ThemeId

----------------------------------------------------------------------------------------------------
-- FAQ for cycle white label partners changed 
-- So Links added to show correct faq links on journey emissions page
----------------------------------------------------------------------------------------------------
EXEC AddContextSuggestionLink 'JourneyEmissions.EstimateCarCO2EmissionsInfo.CycleWhiteLabel','General','JourneyEmissionsCompareInfo', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.AboutCarCO2Emissions.CycleWhiteLabel','General','JourneyEmissionsCompareInfo', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.EstimatingPublicTransportCO2.CycleWhiteLabel','General','JourneyEmissionsCompareInfo', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.CO2FromDifferentTransport.CycleWhiteLabel','General','JourneyEmissionsCompareInfo', @ThemeId
EXEC AddContextSuggestionLink 'JourneyEmissions.ComparingCarAndPublicTransport.CycleWhiteLabel','General','JourneyEmissionsCompareInfo', @ThemeId
GO


--------------------------------------------------------------------
-- Specific changes for links
--------------------------------------------------------------------

-- none


--------------------------------------------------------------------
-- Manual insert - ensure correct Find trains by cost link is used
--------------------------------------------------------------------
USE [TransientPortal]
GO

DECLARE
	@StringResourceName		varchar(100),
	@StringResourceNameId		int,
	@StringLinkCategoryName		varchar(100),
	@StringLinkCategoryId		int,
	@SuggestionLinkId		int,
	@StringContextName		varchar(100),
	@StringContextId		int,
	@ThemeId			int,
	@ContextSuggestionLinkID	int
	

SET @ThemeId = 200 -- AOCycle

--SET @StringResourceName = 'FindATrainCost'
--SET @StringResourceNameId = (SELECT ResourceNameId FROM ResourceName WHERE [ResourceName] = @StringResourceName)

--SET @StringLinkCategoryName = 'Plan a journey'
--SET @StringLinkCategoryId = (SELECT LinkCategoryId FROM LinkCategory WHERE [Name] = @StringLinkCategoryName)

--SET @SuggestionLinkId = (SELECT top 1 SuggestionLinkId FROM SuggestionLink 
--				WHERE ResourceNameId = @StringResourceNameId AND
--					LinkCategoryId = @StringLinkCategoryId
--				ORDER BY Priority DESC)

----EXEC AddContextSuggestionLink 'FindATrainCost','Plan a journey','HomePageMenu', @ThemeId
--SET @StringContextName = 'HomePageMenu'
--SET @StringContextId = (SELECT ContextId FROM Context WHERE [Name] = @StringContextName)

--SET @ContextSuggestionLinkID = (select MAX(ContextSuggestionLinkId) from [dbo].[ContextSuggestionLink]) + 1
	
--INSERT INTO [ContextSuggestionLink] ([ContextSuggestionLinkId], [ContextId], [SuggestionLinkId], [ThemeId])
--SELECT @ContextSuggestionLinkId, @StringContextId, @SuggestionLinkID, @ThemeId


----EXEC AddContextSuggestionLink 'FindATrainCost','Plan a journey','HomePageMenuPlanAJourney', @ThemeId
--SET @StringContextName = 'HomePageMenuPlanAJourney'
--SET @StringContextId = (SELECT ContextId FROM Context WHERE [Name] = @StringContextName)

--SET @ContextSuggestionLinkID = (select MAX(ContextSuggestionLinkId) from [dbo].[ContextSuggestionLink]) + 1
	
--INSERT INTO [ContextSuggestionLink] ([ContextSuggestionLinkId], [ContextId], [SuggestionLinkId], [ThemeId])
--SELECT @ContextSuggestionLinkId, @StringContextId, @SuggestionLinkID, @ThemeId

GO


--------------------------------------------------------------------
-- Manual insert - ensure correct Mobile Demonstrator link is used
--------------------------------------------------------------------

USE [TransientPortal]
GO

DECLARE
	@StringResourceName		varchar(100),
	@StringResourceNameId		int,
	@StringLinkCategoryName		varchar(100),
	@StringLinkCategoryId		int,
	@SuggestionLinkId		int,
	@StringContextName		varchar(100),
	@StringContextId		int,
	@ThemeId			int,
	@ContextSuggestionLinkID	int
	

SET @ThemeId = 200 -- AOCycle

-- Following links have been removed from above list and manually inserted
--EXEC AddContextSuggestionLink 'MobileDemonstrator','Tips and tools','FindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'MobileDemonstrator','Tips and tools','FindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'MobileDemonstrator','Tips and tools','HomePageMenu', @ThemeId
--EXEC AddContextSuggestionLink 'MobileDemonstrator','Tips and tools','HomePageMenuTipsAndTools', @ThemeId
--EXEC AddContextSuggestionLink 'MobileDemonstrator','General','HomePlanAJourney', @ThemeId
--EXEC AddContextSuggestionLink 'MobileDemonstrator','Related links','RelatedLinksContextFindTrainCostInput', @ThemeId
--EXEC AddContextSuggestionLink 'MobileDemonstrator','Related links','RelatedLinksContextFindTrainInput', @ThemeId
--EXEC AddContextSuggestionLink 'MobileDemonstrator','Related links','RelatedLinksContextHomeJourneyPlanning', @ThemeId


--SET @StringResourceName = 'MobileDemonstrator'
--SET @StringResourceNameId = (SELECT ResourceNameId FROM ResourceName WHERE [ResourceName] = @StringResourceName)

------------------------------------------
---- Category
--SET @StringLinkCategoryName = 'Tips and tools'
--SET @StringLinkCategoryId = (SELECT LinkCategoryId FROM LinkCategory WHERE [Name] = @StringLinkCategoryName)

--SET @SuggestionLinkId = (SELECT top 1 SuggestionLinkId FROM SuggestionLink 
--				WHERE ResourceNameId = @StringResourceNameId AND
--					LinkCategoryId = @StringLinkCategoryId AND
--					[ExternalInternalLinkType] like '%Internal%'
--				ORDER BY Priority DESC)

----EXEC AddContextSuggestionLink 'MobileDemonstrator','Tips and tools','FindTrainCostInput', @ThemeId
--SET @StringContextName = 'FindTrainCostInput'
--SET @StringContextId = (SELECT ContextId FROM Context WHERE [Name] = @StringContextName)
--SET @ContextSuggestionLinkID = (select MAX(ContextSuggestionLinkId) from [dbo].[ContextSuggestionLink]) + 1
--INSERT INTO [ContextSuggestionLink] ([ContextSuggestionLinkId], [ContextId], [SuggestionLinkId], [ThemeId])
--SELECT @ContextSuggestionLinkId, @StringContextId, @SuggestionLinkID, @ThemeId


----EXEC AddContextSuggestionLink 'MobileDemonstrator','Tips and tools','FindTrainInput', @ThemeId
--SET @StringContextName = 'FindTrainInput'
--SET @StringContextId = (SELECT ContextId FROM Context WHERE [Name] = @StringContextName)
--SET @ContextSuggestionLinkID = (select MAX(ContextSuggestionLinkId) from [dbo].[ContextSuggestionLink]) + 1
--INSERT INTO [ContextSuggestionLink] ([ContextSuggestionLinkId], [ContextId], [SuggestionLinkId], [ThemeId])
--SELECT @ContextSuggestionLinkId, @StringContextId, @SuggestionLinkID, @ThemeId

----EXEC AddContextSuggestionLink 'MobileDemonstrator','Tips and tools','HomePageMenu', @ThemeId
--SET @StringContextName = 'HomePageMenu'
--SET @StringContextId = (SELECT ContextId FROM Context WHERE [Name] = @StringContextName)
--SET @ContextSuggestionLinkID = (select MAX(ContextSuggestionLinkId) from [dbo].[ContextSuggestionLink]) + 1
--INSERT INTO [ContextSuggestionLink] ([ContextSuggestionLinkId], [ContextId], [SuggestionLinkId], [ThemeId])
--SELECT @ContextSuggestionLinkId, @StringContextId, @SuggestionLinkID, @ThemeId

----EXEC AddContextSuggestionLink 'MobileDemonstrator','Tips and tools','HomePageMenuTipsAndTools', @ThemeId
--SET @StringContextName = 'HomePageMenuTipsAndTools'
--SET @StringContextId = (SELECT ContextId FROM Context WHERE [Name] = @StringContextName)
--SET @ContextSuggestionLinkID = (select MAX(ContextSuggestionLinkId) from [dbo].[ContextSuggestionLink]) + 1
--INSERT INTO [ContextSuggestionLink] ([ContextSuggestionLinkId], [ContextId], [SuggestionLinkId], [ThemeId])
--SELECT @ContextSuggestionLinkId, @StringContextId, @SuggestionLinkID, @ThemeId
------------------------------------------


------------------------------------------
-- Category
SET @StringLinkCategoryName = 'General'
SET @StringLinkCategoryId = (SELECT LinkCategoryId FROM LinkCategory WHERE [Name] = @StringLinkCategoryName)

SET @SuggestionLinkId = (SELECT top 1 SuggestionLinkId FROM SuggestionLink 
				WHERE ResourceNameId = @StringResourceNameId AND
					LinkCategoryId = @StringLinkCategoryId AND
					[ExternalInternalLinkType] like '%Internal%'
				ORDER BY Priority DESC)

--EXEC AddContextSuggestionLink 'MobileDemonstrator','General','HomePlanAJourney', @ThemeId
--SET @StringContextName = 'HomePlanAJourney'
--SET @StringContextId = (SELECT ContextId FROM Context WHERE [Name] = @StringContextName)
--SET @ContextSuggestionLinkID = (select MAX(ContextSuggestionLinkId) from [dbo].[ContextSuggestionLink]) + 1
--INSERT INTO [ContextSuggestionLink] ([ContextSuggestionLinkId], [ContextId], [SuggestionLinkId], [ThemeId])
--SELECT @ContextSuggestionLinkId, @StringContextId, @SuggestionLinkID, @ThemeId
------------------------------------------

------------------------------------------
-- Category
--SET @StringLinkCategoryName = 'Related links'
--SET @StringLinkCategoryId = (SELECT LinkCategoryId FROM LinkCategory WHERE [Name] = @StringLinkCategoryName)

--SET @SuggestionLinkId = (SELECT top 1 SuggestionLinkId FROM SuggestionLink 
--				WHERE ResourceNameId = @StringResourceNameId AND
--					LinkCategoryId = @StringLinkCategoryId AND
--					[ExternalInternalLinkType] like '%Internal%'
--				ORDER BY Priority DESC)

----EXEC AddContextSuggestionLink 'MobileDemonstrator','Related links','RelatedLinksContextFindTrainCostInput', @ThemeId
--SET @StringContextName = 'RelatedLinksContextFindTrainCostInput'
--SET @StringContextId = (SELECT ContextId FROM Context WHERE [Name] = @StringContextName)
--SET @ContextSuggestionLinkID = (select MAX(ContextSuggestionLinkId) from [dbo].[ContextSuggestionLink]) + 1
--INSERT INTO [ContextSuggestionLink] ([ContextSuggestionLinkId], [ContextId], [SuggestionLinkId], [ThemeId])
--SELECT @ContextSuggestionLinkId, @StringContextId, @SuggestionLinkID, @ThemeId

----EXEC AddContextSuggestionLink 'MobileDemonstrator','Related links','RelatedLinksContextFindTrainInput', @ThemeId
--SET @StringContextName = 'RelatedLinksContextFindTrainInput'
--SET @StringContextId = (SELECT ContextId FROM Context WHERE [Name] = @StringContextName)
--SET @ContextSuggestionLinkID = (select MAX(ContextSuggestionLinkId) from [dbo].[ContextSuggestionLink]) + 1
--INSERT INTO [ContextSuggestionLink] ([ContextSuggestionLinkId], [ContextId], [SuggestionLinkId], [ThemeId])
--SELECT @ContextSuggestionLinkId, @StringContextId, @SuggestionLinkID, @ThemeId

----EXEC AddContextSuggestionLink 'MobileDemonstrator','Related links','RelatedLinksContextHomeJourneyPlanning', @ThemeId
--SET @StringContextName = 'RelatedLinksContextHomeJourneyPlanning'
--SET @StringContextId = (SELECT ContextId FROM Context WHERE [Name] = @StringContextName)
--SET @ContextSuggestionLinkID = (select MAX(ContextSuggestionLinkId) from [dbo].[ContextSuggestionLink]) + 1
--INSERT INTO [ContextSuggestionLink] ([ContextSuggestionLinkId], [ContextId], [SuggestionLinkId], [ThemeId])
--SELECT @ContextSuggestionLinkId, @StringContextId, @SuggestionLinkID, @ThemeId
------------------------------------------

--------------------------------------------------------------------
-- Manual insert - ensure correct Root links are used other the pages will show no menu
--------------------------------------------------------------------

--EXEC AddContextSuggestionLink 'Accessibility','TermsAndPolicy','AccessibilityMenu', @ThemeId
--EXEC AddContextSuggestionLink 'DataProviders','TermsAndPolicy','DataProvidersMenu', @ThemeId
--EXEC AddContextSuggestionLink 'PrivacyPolicy','TermsAndPolicy','PrivacyPolicyMenu', @ThemeId
--EXEC AddContextSuggestionLink 'TermsConditions','TermsAndPolicy','TermsConditionsMenu', @ThemeId

----------------------------------------
-- Resource
SET @StringResourceName = 'Accessibility'
SET @StringResourceNameId = (SELECT ResourceNameId FROM ResourceName WHERE [ResourceName] = @StringResourceName)

-- Category
SET @StringLinkCategoryName = 'TermsAndPolicy'
SET @StringLinkCategoryId = (SELECT LinkCategoryId FROM LinkCategory WHERE [Name] = @StringLinkCategoryName)

SET @SuggestionLinkId = (SELECT top 1 SuggestionLinkId FROM SuggestionLink 
				WHERE ResourceNameId = @StringResourceNameId AND
					LinkCategoryId = @StringLinkCategoryId AND
					[ExternalInternalLinkType] like '%Internal%'
				ORDER BY Priority ASC)

--EXEC AddContextSuggestionLink 'Accessibility','TermsAndPolicy','AccessibilityMenu', @ThemeId
SET @StringContextName = 'AccessibilityMenu'
SET @StringContextId = (SELECT ContextId FROM Context WHERE [Name] = @StringContextName)
SET @ContextSuggestionLinkID = (select MAX(ContextSuggestionLinkId) from [dbo].[ContextSuggestionLink]) + 1
INSERT INTO [ContextSuggestionLink] ([ContextSuggestionLinkId], [ContextId], [SuggestionLinkId], [ThemeId])
SELECT @ContextSuggestionLinkId, @StringContextId, @SuggestionLinkID, @ThemeId


----------------------------------------
-- Resource
SET @StringResourceName = 'DataProviders'
SET @StringResourceNameId = (SELECT ResourceNameId FROM ResourceName WHERE [ResourceName] = @StringResourceName)

-- Category
SET @StringLinkCategoryName = 'TermsAndPolicy'
SET @StringLinkCategoryId = (SELECT LinkCategoryId FROM LinkCategory WHERE [Name] = @StringLinkCategoryName)

SET @SuggestionLinkId = (SELECT top 1 SuggestionLinkId FROM SuggestionLink 
				WHERE ResourceNameId = @StringResourceNameId AND
					LinkCategoryId = @StringLinkCategoryId AND
					[ExternalInternalLinkType] like '%Internal%'
				ORDER BY Priority ASC)

--EXEC AddContextSuggestionLink 'DataProviders','TermsAndPolicy','DataProvidersMenu', @ThemeId
SET @StringContextName = 'DataProvidersMenu'
SET @StringContextId = (SELECT ContextId FROM Context WHERE [Name] = @StringContextName)
SET @ContextSuggestionLinkID = (select MAX(ContextSuggestionLinkId) from [dbo].[ContextSuggestionLink]) + 1
INSERT INTO [ContextSuggestionLink] ([ContextSuggestionLinkId], [ContextId], [SuggestionLinkId], [ThemeId])
SELECT @ContextSuggestionLinkId, @StringContextId, @SuggestionLinkID, @ThemeId


----------------------------------------
-- Resource
SET @StringResourceName = 'PrivacyPolicy'
SET @StringResourceNameId = (SELECT ResourceNameId FROM ResourceName WHERE [ResourceName] = @StringResourceName)

-- Category
SET @StringLinkCategoryName = 'TermsAndPolicy'
SET @StringLinkCategoryId = (SELECT LinkCategoryId FROM LinkCategory WHERE [Name] = @StringLinkCategoryName)

SET @SuggestionLinkId = (SELECT top 1 SuggestionLinkId FROM SuggestionLink 
				WHERE ResourceNameId = @StringResourceNameId AND
					LinkCategoryId = @StringLinkCategoryId AND
					[ExternalInternalLinkType] like '%Internal%'
				ORDER BY Priority ASC)

--EXEC AddContextSuggestionLink 'PrivacyPolicy','TermsAndPolicy','PrivacyPolicyMenu', @ThemeId
SET @StringContextName = 'PrivacyPolicyMenu'
SET @StringContextId = (SELECT ContextId FROM Context WHERE [Name] = @StringContextName)
SET @ContextSuggestionLinkID = (select MAX(ContextSuggestionLinkId) from [dbo].[ContextSuggestionLink]) + 1
INSERT INTO [ContextSuggestionLink] ([ContextSuggestionLinkId], [ContextId], [SuggestionLinkId], [ThemeId])
SELECT @ContextSuggestionLinkId, @StringContextId, @SuggestionLinkID, @ThemeId


----------------------------------------
-- Resource
SET @StringResourceName = 'TermsConditions'
SET @StringResourceNameId = (SELECT ResourceNameId FROM ResourceName WHERE [ResourceName] = @StringResourceName)

-- Category
SET @StringLinkCategoryName = 'TermsAndPolicy'
SET @StringLinkCategoryId = (SELECT LinkCategoryId FROM LinkCategory WHERE [Name] = @StringLinkCategoryName)

SET @SuggestionLinkId = (SELECT top 1 SuggestionLinkId FROM SuggestionLink 
				WHERE ResourceNameId = @StringResourceNameId AND
					LinkCategoryId = @StringLinkCategoryId AND
					[ExternalInternalLinkType] like '%Internal%'
				ORDER BY Priority ASC)

--EXEC AddContextSuggestionLink 'TermsConditions','TermsAndPolicy','TermsConditionsMenu', @ThemeId
SET @StringContextName = 'TermsConditionsMenu'
SET @StringContextId = (SELECT ContextId FROM Context WHERE [Name] = @StringContextName)
SET @ContextSuggestionLinkID = (select MAX(ContextSuggestionLinkId) from [dbo].[ContextSuggestionLink]) + 1
INSERT INTO [ContextSuggestionLink] ([ContextSuggestionLinkId], [ContextId], [SuggestionLinkId], [ThemeId])
SELECT @ContextSuggestionLinkId, @StringContextId, @SuggestionLinkID, @ThemeId


GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10203
SET @ScriptDesc = 'Script to add all the left hand links for AOCycle Cycle Only partner theme'


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
