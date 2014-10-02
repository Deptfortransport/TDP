-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : Content TDPMobile data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
Go
------------------------------------------------
-- Mobile general content, all added to the group 'TDPMobile'
-- Mobile also uses content from the Group 'TDPGeneral' - see Content.sql
------------------------------------------------

DECLARE @ThemeId int = 1
DECLARE @Group varchar(100) = 'TDPMobile'
DECLARE @Collection varchar(100) = 'General'
DECLARE @CultEn varchar(2) = 'en'
DECLARE @CultCy varchar(2) = 'cy'

-- Tidy up first, helps keep content table clean,
-- and ensures this script contains complete control of content for this Group
EXEC DeleteAllGroupContent @Group

------------------------------------------------------------------------------------------------------------------
-- Page headings
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileDefault.Heading.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileDefault.Heading.ScreenReader.Text', 'Transport Direct'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileError.Heading.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileError.Heading.ScreenReader.Text', 'Error'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileSorry.Heading.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileSorry.Heading.ScreenReader.Text', 'Sorry'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobilePageNotFound.Heading.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobilePageNotFound.Heading.ScreenReader.Text', 'Page Not Found'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileInput.Heading.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileInput.Heading.ScreenReader.Text', 'Journey Input'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileSummary.Heading.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileSummary.Heading.ScreenReader.Text', 'Journey Summary'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileDetail.Heading.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileDetail.Heading.ScreenReader.Text', 'Journey Detail'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileDirection.Heading.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileDirection.Heading.ScreenReader.Text', 'Journey Direction'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileMap.Heading.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileMap.Heading.ScreenReader.Text', 'Map'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileTravelNews.Heading.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'MobileTravelNews.Heading.ScreenReader.Text', 'Travel News'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'MobileError.Heading.ScreenReader.Text', 'Cyfeiliorn'
	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'MobileSummary.Heading.ScreenReader.Text', 'Crynodeb o''r siwrnai'
	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'MobileSorry.Heading.ScreenReader.Text', 'Mae''n ddrwg gennym'

------------------------------------------------------------------------------------------------------------------
-- Header
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'HeaderControl.Logo.ImageUrl','logos/logo-pink.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'HeaderControl.Logo.AlternateText','Transport Direct'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'HeaderControl.Logo.ToolTip','Transport Direct'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'HeaderControl.Language.Link.Text','Cymraeg'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'HeaderControl.Language.Link.ToolTip','Cymraeg'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'HeaderControl.Menu.Link.Text',''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'HeaderControl.Menu.Link.ToolTip','Menu'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'HeaderControl.Language.Link.Text','English'
	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'HeaderControl.Language.Link.ToolTip','English'

------------------------------------------------------------------------------------------------------------------
-- Footer
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'FooterControl.Privacy.Text','Privacy'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'FooterControl.Privacy.ToolTip','Privacy'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'FooterControl.FullSite.Text','Full Site'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'FooterControl.FullSite.ToolTip','Full Site'

------------------------------------------------------------------------------------------------------------------
-- Default
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Default.PublicTransportModeButton.Text', 'Plan your journey'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Default.PublicTransportModeButton.ToolTip', 'Plan your journey'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Default.CycleModeButton.Text', 'Cycle'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Default.CycleModeButton.ToolTip', 'Cycle'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Default.TravelNewsButton.Text', 'Travel news'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Default.TravelNewsButton.ToolTip', 'Travel news'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'Default.CycleModeButton.Text', 'Beic'
	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'Default.CycleModeButton.ToolTip', 'Beic'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PublicJourneyOptions.MobiltyOptionsHeading.Text', 'Mobility options'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PublicJourneyOptions.MobiltyOption.StepFree.Text', 'Step free journey'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PublicJourneyOptions.MobiltyOption.StepFree.Info.Text', 'This option uses all modes where it is possible for a step-free journey to be undertaken, both with and without assistance. It includes buses and coaches that have access ramps fitted that would enable a wheelchair user to access the vehicle unaided, or vehicles with lifts. It includes step-free tram and light rail stations, and underground stations that are step free from street to vehicle. It also includes rail (stations and services) and coach, where you should book in advance to ensure staff are available to help you to board and alight a vehicle, and to secure a wheelchair space where required.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PublicJourneyOptions.MobiltyOption.StepFree.InfoLink.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PublicJourneyOptions.MobiltyOption.StepFree.InfoLink.ToolTip', 'View addtional information about a Step free journey'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PublicJourneyOptions.MobiltyOption.Assistance.Text', 'Assistance needed'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PublicJourneyOptions.MobiltyOption.Assistance.Info.Text', 'Choose this option if you require staff assistance on your journey. This option includes any stations and stops where staff assistance is provided, as well as places where assistance may only be available on the vehicle, rather than at the station or stop. Please note you should always book assistance at National Rail stations at least 24 hours in advance.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PublicJourneyOptions.MobiltyOption.Assistance.InfoLink.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PublicJourneyOptions.MobiltyOption.Assistance.InfoLink.ToolTip', 'View addtional information about Assistance needed'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PublicJourneyOptions.MobiltyOption.FewestChanges.Text', 'Use fewest changes'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PublicJourneyOptions.MobiltyOption.ExcludeUnderground.Text', 'Exclude underground'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PublicJourneyOptions.MobiltyOptionSelected.Text', 'With accessibility options'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PublicJourneyOptions.MobiltyOptionNotSelected.Text', 'No accessibility options'


EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PublicJourneyOptions.MobiltyOptionsLegend.Text', 'Select to view accessibility options'


------------------------------------------------------------------------------------------------------------------
-- Input
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Location.From.Text', 'From location.' -- screenreader
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Location.From.Watermark', 'From postcode, station or airport'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Location.To.Text', 'To location.' -- screenreader
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Location.To.Watermark', 'To postcode, station or airport'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Location.Venues.Text', 'Venues'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Location.Venues.ToolTip', 'Choose a venue'


EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.LocationPark.Text', 'Choose a cycle parking location'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.LocationPark.Disabled.Text', 'Select a venue first and then choose a cycle parking location'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.LocationPark.Map.Text', 'Choose a cycle parking location from the venue map.'


EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.LocationPark.Map.Text', 'Choose preferred cycle park'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.LocationPark.DropDown.NoParks', 'No cycle parking at this venue'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.LocationPark.ValidationError.NoParks', 'There is currently no cycle parking at this venue. You can plan a journey by public transport.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.LocationPark.ValidationError.SelectPark', 'Please choose a cycle parking location before planning your journey'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.TypeOfRouteHeading.Text', 'Choose your type of route'


EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Date.Outward.Text', 'Date'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Time.Outward.Text', 'Time'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneyInput.Date.Outward.Text', 'Dyddiad'
	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneyInput.Time.Outward.Text', 'Amser'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Date.SetDate.Text', 'Set travel date'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Date.SetDate.ToolTip', 'Set travel date'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Date.Today.Text', 'Today'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Time.ArrivalTime.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Time.ArrivalTime.ToolTip', 'Set travel time'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Time.DepartureTime.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Time.DepartureTime.ToolTip', 'Set travel time'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Time.OtherTimes.Text', 'Other times'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Time.OtherTimes.ToolTip', 'Other times'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Time.OK.Text', 'OK'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Time.OK.ToolTip', 'OK'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneyInput.Time.OK.Text', 'Iawn'
	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneyInput.Time.OK.ToolTip', 'Iawn'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.LeaveAt.Text', 'Leave at'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.ArriveBy.Text', 'Arrive by'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.LeaveOn.Text', 'Leave on'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.ArriveOn.Text', 'Arrive on'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneyInput.ArriveBy.Text', 'Cyrraedd erbyn'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Now.Link.Text', 'Now'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Now.Link.ToolTip', 'Set date and time to now'


EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.TravelFromToToggle.ToolTip','Switch origin and destination'


EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.AdvancedOptions.ScreenReader','Set travel by options'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.AdvancedOptionsButton.Text','Travel by'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.AdvancedOptionsButton.ToolTip','Set travel by options'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.AdvancedOptionsOKButton.Text','Apply'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.AdvancedOptionsOKButton.ToolTip','Apply'
	
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.TransportModesHeading.Text','Travel modes'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.TransportModesSelected.Text','Selected types of transport'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.TransportModesSelectedAll.Text','All types of transport'
	
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.PlanJourney.Text', 'Plan your journey'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.PlanJourney.ToolTip', 'Plan your journey'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Close.ToolTip', 'Close.'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.LoadingMessage.Text', 'Please wait while we check your travel choices'
	

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Back.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Back.ToolTip', 'Back'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Back.MobileDefault.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Back.MobileDefault.ToolTip', 'Back to Home Page'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Back.MobileInput.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Back.MobileInput.ToolTip', 'Back to journey input'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Back.MobileSummary.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Back.MobileSummary.ToolTip', 'Back to summary results'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Back.MobileDetail.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Back.MobileDetail.ToolTip', 'Back to journey details'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneyInput.Back.ToolTip', 'Yn ôl'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Next.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Next.ToolTip', 'Next'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Next.MobileMap.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Next.MobileMap.ToolTip', 'View my location'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Next.MobileMapJourney.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Next.MobileMapJourney.ToolTip', 'View map'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Next.MobileDirection.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Next.MobileDirection.ToolTip', 'Detailed directions'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Next.MobileTravelNews.Text', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.Next.MobileTravelNews.ToolTip', 'Travel news'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneyInput.Next.ToolTip', 'Nesaf'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.GetTime.Text', 'Get Time'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.SelectDay.Text', 'Select Day'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.SelectVenue.Text', 'Select Venue'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.SelectVenue.AllVenuesButton.Text', 'All Venues'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyInput.SelectCyclePark.Text', 'Select Cycle Park'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'ValidateAndRun.OriginAndDestinationOverlaps', 'The venues you have selected are in the same location. Your best transport option is likely to be a walk between the venues.<br />Within some venues, such as the Olympic Park, disabled spectators will be able to make use of Games Mobility. This free service will be easy to find inside the venue and will loan out manual wheelchairs and mobility scooters.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'ValidateAndRun.DateTimeIsBeforeEvent', 'Transport Direct enables you to plan a journey for the next three months only. Please select a date in this period.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'ValidateAndRun.DateTimeIsAfterEvent',	'Transport Direct enables you to plan a journey for the next three months only. Please select a date in this period.'


------------------------------------------------------------------------------------------------------------------
-- Summary
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneySummary.LoadingImage.Imageurl', 'Icons/hourglass_small.gif'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneySummary.LoadingImage.AlternateText', 'Please wait...'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneySummary.LoadingImage.ToolTip', 'Please wait...'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneySummary.LoadingMessage.Text', 'Please wait while we prepare your journey plan'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneySummary.LoadingImage.AlternateText', 'Arhoswch os gwelwch yn dda...'
	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneySummary.LoadingImage.ToolTip', 'Arhoswch os gwelwch yn dda...'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneySummary.LongWaitMessage.Text', 'If the results do not appear within 30 seconds, '
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneySummary.LongWaitMessageLink.Text', 'please click this link.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneySummary.LongWaitMessageLink.ToolTip', 'If the results do not appear within 30 seconds, please click this link.'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneySummary.LongWaitMessage.Text', 'Os nad yw''r canlyniadau yn ymddangos o fewn 30 eiliad, '
	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneySummary.LongWaitMessageLink.Text', 'cliciwch ar y ddolen hon.'
	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneySummary.LongWaitMessageLink.ToolTip', 'Os nad yw''r canlyniadau yn ymddangos o fewn 30 eiliad, cliciwch ar y ddolen hon.'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneySummary.NoResultsFound.Timeout.Error', 'Sorry we are currently unable to obtain journey options using the details you have entered.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneySummary.NoResultsFound.UserInfo', ''

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyOutput.SelectJourney.Show.ToolTip','Select to view journey details.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyOutput.Message.JourneyWebNoResults', 'Sorry, Transport Direct is currently unable to find a journey. You may wish to try again using a different date or time.'


EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyOutput.Message.CyclePlannerInternalError', 'Transport Direct is unable to plan a cycle journey using the details you have entered. This may be because your start point is on a road with restrictions - please choose another start location.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyOutput.Message.CyclePlannerPartialReturn', 'Transport Direct is unable to plan a cycle journey using the details you have entered. This may be because your start point is on a road with restrictions - please choose another start location.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyOutput.Message.CyclePlannerNoResults', 'Transport Direct is unable to plan a cycle journey using the details you have entered. This may be because your start point is on a road with restrictions - please choose another start location.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'CycleJourneyLocations.CycleParkNoneFound.Text', 'No cycle parking is available at the time you have chosen. Cycle parking is normally open a few hours either side of an event. Please try again.'


EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneySummary.EarlierJourney.Outward.Text','Earlier'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneySummary.EarlierJourney.Outward.ToolTip','Get earlier journeys'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneySummary.LaterJourney.Outward.Text','Later'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneySummary.LaterJourney.Outward.ToolTip','Get later journeys'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneySummary.EarlierJourney.Outward.Text','Cynharach'
	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneySummary.LaterJourney.Outward.Text','Hwyrach'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneySummary.PlanReturnJourney.Text','Plan return journey'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneySummary.PlanReturnJourney.ToolTip','Plan return journey'


------------------------------------------------------------------------------------------------------------------
-- Detail
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetail.JourneyPaging.Next.Text', 'Next'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetail.JourneyPaging.Next.ToolTip', 'Next journey'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetail.JourneyPaging.Previous.Text', 'Previous'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetail.JourneyPaging.Previous.ToolTip', 'Previous journey'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetail.JourneyPaging.Heading.Text', 'Plan'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetail.JourneyPaging.Heading.ToolTip', 'Show details for plan'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneyDetail.JourneyPaging.Next.Text', 'Nesaf'
	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneyDetail.JourneyPaging.Previous.Text', 'Yn ôl'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetail.VenueMapPage.Heading.Text', 'Venue map'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetail.venueMapPage.InfoLabel.Text', ''


------------------------------------------------------------------------------------------------------------------
-- Find nearest accessible stop
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.Heading.Text', 'Choose nearest accessible location'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.Message', 'You requested a {0}. Transport Direct is not able to find a journey meeting your requirements from your chosen {1}. Please select stops or places near your {1} that meet your accessiblity requirement.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.Message.Origin', 'origin'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.Message.Destination', 'destination'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.Message.OriginDestination', 'origin and destination'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.Message.StepFree', '<span class="accessibleType">step free</span> journey'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.Message.Assistance', 'journey <span class="accessibleType">with assistance</span>'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.Message.StepFreeAndAssistance', '<span class="accessibleType">step free</span> journey <span class="accessibleType">with assistance</span>'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.Message.NoStops', 'Sorry no places were found with your selection, please alter your input location.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.Origin.Label', 'From: <span class="accessibleLocation">{0}</span>'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.OriginLabel.Text', 'From: <span class="accessibleLocation">{0}</span>'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.Destination.Label', 'To: <span class="accessibleLocation">{0}</span>'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.DestinationLabel.Text', 'To: <span class="accessibleLocation">{0}</span>'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibleStops.ItemPleaseSelect.Origin', 'Choose an origin location'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibleStops.ItemPleaseSelect.Destination', 'Choose a destination location'

	EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.Origin.Label', 'O: <span class="accessibleLocation">{0}</span>'
	EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.OriginLabel.Text', 'O: <span class="accessibleLocation">{0}</span>'
	EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.Destination.Label', 'I: <span class="accessibleLocation">{0}</span>'
	EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'AccessibilityOptions.DestinationLabel.Text', 'I: <span class="accessibleLocation">{0}</span>'

-- Accessible booking messages
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetailsControl.AccessibleBooking.Wheelchair.Advanced',
	'Wheelchair travel should be booked in advance'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetailsControl.AccessibleBooking.Wheelchair.Today',
	'Wheelchair travel should be booked in advance - and may not be available if booked on day of travel'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetailsControl.AccessibleBooking.Assistance.Advanced',
	'Travel assistance should be booked in advance'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetailsControl.AccessibleBooking.Assistance.Today',
	'Assistance requests must be booked in advance - and may not be available if booked on the day of travel'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetailsControl.AccessibleBooking.Wheelchair.Book.Phone',
	'To book a wheelchair space in advance please phone {0}'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetailsControl.AccessibleBooking.Wheelchair.Book.Link',
	'Find out how to book a wheelchair space'
	
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetailsControl.AccessibleBooking.Assistance.Book.Phone',
	'To book assistance in advance please phone {0}'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyDetailsControl.AccessibleBooking.Assistance.Book.Link',
	'Find out how to book assistance'

------------------------------------------------------------------------------------------------------------------
-- Map
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyMap.MapLoading.Text', 'Please wait...'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyMap.UseLocation.Text', 'Use my location'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyMap.UseLocation.ToolTip', 'Use my location'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyMap.ViewJourney.Text', 'View journey'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyMap.ViewJourney.ToolTip', 'View journey'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'JourneyMap.MapInfo.NonJavascript', 'Maps can only be displayed with javascript enabled'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'JourneyMap.MapLoading.Text', 'Arhoswch os gwelwch yn dda'

------------------------------------------------------------------------------------------------------------------
-- Travl News
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'TravelNews.ProvidedBy.Text', 'Travel news provided by <a href="http://www.transportdirect.info" rel="external">Transport Direct</a>'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'TravelNews.LondonUnderground.ProvidedBy.Text', 'London Underground service details provided by <a href="http://m.tfl.gov.uk/" rel="external">Transport for London</a>'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'TravelNews.NewsModeOptionsLegend.Text', 'Choose travel news filter'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'TravelNews.DisplayedFor.Venues', 'Travel news displayed for {0}'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'TravelNews.DisplayedFor.AllVenues', 'Travel news displayed for All venues'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'TravelNews.LoadingMessage.Text', 'Please wait while we retrieve the latest travel news'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'TravelNews.FilterButtonNonJS.Text', 'Go'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'TravelNewsDetail.Heading.Text', 'Travel news details'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'TravelNews.FilterButtonNonJS.Text', 'Mwy'
	
------------------------------------------------------------------------------------------------------------------
-- Error
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Error.HeadingTitle.Text', 'Sorry an error has occurred, this may be due to a technical problem.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Error.Message.Text', 'For the <a href="{0}">Transport Direct Mobile Journey Planner please click here</a>'

------------------------------------------------------------------------------------------------------------------
-- Sorry
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Sorry.HeadingTitle.Text', 'Sorry, Transport Direct is currently unavailable.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Sorry.Message.Text', 'We are working to resolve this. Please try again shortly.'
	
------------------------------------------------------------------------------------------------------------------
-- PageNotFound
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PageNotFound.HeadingTitle.Text', 'Sorry that page was not found, please check your typing and try again.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'PageNotFound.Message.Text', 'For the <a href="{0}">Transport Direct Mobile Journey Planner please click here</a>'

------------------------------------------------------------------------------------------------------------------
-- Terms
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Terms.HeadingTitle.Text', 'Terms & Conditions'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Terms.ContentDiv.Html', 
'<h2>This Website</h2>
<p>This site is powered by Transport Direct and by continuing to access and use the Website you the user ("you" or "your") are agreeing to be bound by these terms and conditions (the "Terms") which govern your access to and use of the Website and any data or information therein.</p>
<h2>Transport Direct</h2>
<p>"Transport Direct" means the Secretary of State for Transport acting through the Transport Direct division of the Department for Transport which aims to provide through the Transport Direct Portal located at <a href="http://www.transportdirect.info">www.TransportDirect.info</a> or the Transport Direct mobile site located at <a hred="http://m.transportdirect.info">m.transportdirect.info</a> (hereafter the "Website"), a travel information and journey planning service for Great Britain. An on-line, web feedback service for feedback regarding the results and / or performance of the site is <a href="http://www.transportdirect.info/Web2/ContactUs/FeedbackPage.aspx">available here</a>. All other correspondence should be directed to Portal Service Team, Transport Direct, Department for Transport, Zone 2/17, Great Minster House, 76 Marsham Street, LONDON, SW1P 4DR.</p>
<p>The full terms and conditions governing your use of this site can be <a href="http://www.transportdirect.info/Web2/About/TermsConditions.aspx">found here</a></p>'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'Terms.HeadingTitle.Text', 'Amodau a thelerau'

------------------------------------------------------------------------------------------------------------------
-- Privacy
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Privacy.HeadingTitle.Text', 'Privacy Policy'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Privacy.ContentDiv.Html', 
'<p>Transport Direct is a division of the Department for Transport whose main contact address for the purpose of this Privacy Policy is Portal Service Team, Transport Direct, Department for Transport, Zone 2/17, Great Minster House, 76 Marsham Street, LONDON, SW1P 4DR.</p>
<p>You should be aware that information in data may be automatically collected through the use of "cookies".  Cookies are small text files a website can use to recognise you and allow Transport Direct to observe behaviour and compile accurate data in order to improve the website experience for you.  More information on cookies and how to disable them, along with the complete <a href="http://www.transportdirect.info/Web2/About/PrivacyPolicy.aspx">privacy policy covering this site can be found here</a></p>'

	EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'Privacy.HeadingTitle.Text', 'Polisi preifatrwydd'

------------------------------------------------------------------------------------------------------------------
-- Cookie
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Cookie.HeadingTitle.Text', 'Cookie Policy'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Cookie.ContentDiv.Html', 
'<p>You should be aware that information about your browser may be automatically collected through the use of "cookies". Cookies are small text files a website can use to recognise a browser and allow Transport Direct to compile accurate data in order to improve the website experience for our users.</p>
<h2>Cookies used on this site</h2>
<p>"ASP.NET_SessionId" - This cookie is used to track your Session ID and is required for the majority of the functionality of the site.</p>
<p>"TDPTestCookie" - This cookie is added and removed straight away to test if a browser supports cookies.</p>
<p>"TDP" - This cookie is used to detect new or repeat visitors of the site and contains the following details:
<br />Session ID
<br />Appropriate branding
<br />Language indicator
<br />Last visited date and time
<br />Last page visited 
</p>
<p>"vidi" - This cookie is used to track site usage by Cognesia - using tags.transportdirect.info. These services help us to learn more about the usage of Transport Direct. The information that we collect and share is anonymous and not personally identifiable. It does not reveal your name, address, telephone number, or email address. For more information about Cognesia, go to <a href="http://www.cognesia.com/index.php/" target="_blank">www.cognesia.com</a></p>
<h2>Opting out</h2>
<p>Most internet browsers enable you to delete cookies or receive a warning that cookies are being installed. If you do not want information to be collected through these cookies there is a simple procedure by which most browsers allow you to block or deny the cookies. Please refer to your browser instructions or help pages to learn more about these functions. Or visit one of the sites below, which have detailed information on how to manage, control or delete cookies.
<br /><a href="http://www.aboutcookies.org/">About Cookies</a>
<br /><a href="http://www.allaboutcookies.org/">All about Cookies</a>
</p>'

------------------------------------------------------------------------------------------------------------------
-- Landing Page
------------------------------------------------------------------------------------------------------------------
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'Landing.Message.InvalidLocations.Mobile.Text', 'At least one location entered must be a <strong>venue</strong>. Please use the venue button to select a venue.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'Landing.Message.InvalidDestination.Mobile.Text', 'The location entered in the ''To'' box must be a <strong>venue</strong>. Please use the venue button to select a venue.'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'Landing.Message.InvalidOrigin.Mobile.Text', 'The location entered in the ''From'' box must be a <strong>venue</strong>.  Please use the venue button to select a venue.'

------------------------------------------------------------------------------------------------------------------
-- Cycle Maps
------------------------------------------------------------------------------------------------------------------
-- Maps - Brands Hatch
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100BND.CycleParkM.Url', 'maps/CycleParksMaps/8100BRH_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100BND.CycleParkM.AlternateText', 'Map of cycle parking for Brands Hatch'


-- Maps - Olympic park
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100OPK.CycleParkM.Url', 'maps/CycleParksMaps/8100OPK_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100OPK.CycleParkM.AlternateText', 'Map of cycle parking for Olympic Park'


-- Maps - Victoria Park Live (Olympic park)
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100VPL.CycleParkM.Url', 'maps/CycleParksMaps/8100VPL_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100VPL.CycleParkM.AlternateText', 'Map of cycle parking for Victoria Park Live Site'


-- Maps - Greenwich park
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100GRP.CycleParkM.Url', 'maps/CycleParksMaps/8100GRP_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100GRP.CycleParkM.AlternateText', 'Map of cycle parking for Greenwich Park'


-- Maps - Earls Court
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100EAR.CycleParkM.Url', 'maps/CycleParksMaps/8100EAR_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100EAR.CycleParkM.AlternateText', 'Map of cycle parking for Earls Court'


-- Maps - Eton Dorney
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100ETD.CycleParkM.Url', 'maps/CycleParksMaps/8100ETD_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100ETD.CycleParkM.AlternateText', 'Map of cycle parking for Eton Dorney'


-- Maps - ExCeL
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100EXL.CycleParkM.Url', 'maps/CycleParksMaps/8100EXL_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100EXL.CycleParkM.AlternateText', 'Map of cycle parks for ExCeL'


-- Maps - Hadleigh Farm
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100HAD.CycleParkM.Url', 'maps/CycleParksMaps/8100HAD_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100HAD.CycleParkM.AlternateText', 'Map of cycle parking for Hadleigh Farm'


-- Maps - Hampden Park
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100HAM.CycleParkM.Url', 'maps/CycleParksMaps/8100HAM_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100HAM.CycleParkM.AlternateText', 'Map of cycle parking for Hampden Park'


-- Maps - Horse Guards Parade
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100HGP.CycleParkM.Url', 'maps/CycleParksMaps/8100HGP_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100HGP.CycleParkM.AlternateText', 'Map of cycle parking for Horse Guards Parade'


-- Maps - Hyde Park
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100HYD.CycleParkM.Url', 'maps/CycleParksMaps/8100HYD_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100HYD.CycleParkM.AlternateText', 'Map of cycle parking for Hyde Park'


-- Maps - Hyde Park Live
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100HPL.CycleParkM.Url', 'maps/CycleParksMaps/8100HYD_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100HPL.CycleParkM.AlternateText', 'Map of cycle parking for Hyde Park Live'


-- Maps - Lords Cricket Ground
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100LCG.CycleParkM.Url', 'maps/CycleParksMaps/8100LCG_CycleParkMapsM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100LCG.CycleParkM.AlternateText', 'Map of cycle parking for Lords Cricket Ground'


-- Maps - Lee Valley White Water Centre
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100LVC.CycleParkM.Url', 'maps/CycleParksMaps/8100LVC_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100LVC.CycleParkM.AlternateText', 'Map of cycle parking for Lee Valley White Water Centre'


-- Maps - Millennium Stadium
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100MIL.CycleParkM.Url', 'maps/CycleParksMaps/8100MIL_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100MIL.CycleParkM.AlternateText', 'Map of cycle parking for Millennium Stadium'


-- Maps - The Mall
--EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100MAL.CycleParkM.Url', 'maps/CycleParksMaps/8100MAL_CycleParksMap.png'
--EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100MAL.CycleParkM.AlternateText', 'Map of cycle parking for The Mall'

-- Maps - The Mall - North
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100MLN.CycleParkM.Url', 'maps/CycleParksMaps/8100MLL_CycleParksMapNorthM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100MLN.CycleParkM.AlternateText', 'Map of cycle parking for The Mall - North'


-- Maps - The Mall - South
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100MLS.CycleParkM.Url', 'maps/CycleParksMaps/8100MLL_CycleParksMapSouthM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100MLS.CycleParkM.AlternateText', 'Map of cycle parking for The Mall - South'


-- Maps - North Greenwich Arena
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100NGA.CycleParkM.Url', 'maps/CycleParksMaps/8100NGA_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100NGA.CycleParkM.AlternateText', 'Map of cycle parking for North Greenwich Arena'


-- Maps - Old Trafford
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100OLD.CycleParkM.Url', 'maps/CycleParksMaps/8100OLD_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100OLD.CycleParkM.AlternateText', 'Map of cycle parking for Old Trafford'


-- Maps - The Royal Artillery Barracks
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100RAB.CycleParkM.Url', 'maps/CycleParksMaps/8100RAB_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100RAB.CycleParkM.AlternateText', 'Map of cycle parking for The Royal Artillery Barracks'


-- Maps - Woolwich Live (nearr The Royal Artillery Barracks)
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100WOL.CycleParkM.Url', 'maps/CycleParksMaps/8100RAB_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100WOL.CycleParkM.AlternateText', 'Map of cycle parking for Woolwich Live Site'


-- Maps - Weymouth and Portland - The Nothe
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100WAP.CycleParkM.Url', 'maps/CycleParksMaps/8100WAP_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100WAP.CycleParkM.AlternateText', 'Map of cycle parking for Weymouth and Portland - The Nothe'


-- Maps - Weymouth Live
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100WLB.CycleParkM.Url', 'maps/CycleParksMaps/8100WAP_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100WLB.CycleParkM.AlternateText', 'Map of cycle parking for Weymouth Live'


-- Maps - Wembley Arena
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100WEA.CycleParkM.Url', 'maps/CycleParksMaps/8100WEA_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100WEA.CycleParkM.AlternateText', 'Map of cycle parking for Wembley Arena'


-- Maps - Wembley Stadium
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100WEM.CycleParkM.Url', 'maps/CycleParksMaps/8100WEM_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100WEM.CycleParkM.AlternateText', 'Map of cycle parking for Wembley Stadium'


-- Maps - Wimbledon
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100WIM.CycleParkM.Url', 'maps/CycleParksMaps/8100WIM_CycleParksMapM.png'
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection,'JourneyLocations.8100WIM.CycleParkM.AlternateText', 'Map of cycle parking for Wimbledon'



GO


-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'Content TDPMobile data'

GO
