-- ***********************************************
-- NAME 		: DUP1999_AccessibleLocation_Content_5.sql
-- DESCRIPTION 	: Accessibility options - content
-- AUTHOR		: Mitesh Modi
-- DATE			: 11 Mar 13
-- ************************************************

USE [Content]
GO

-- Accessible radio options
EXEC AddtblContent
	1, 1, 'langStrings', 'DataServices.AccessibleOptionsRadio.Wheelchair',
	'I need a step free journey',
	'I need a step free journey'


EXEC AddtblContent
	1, 1, 'langStrings', 'DataServices.AccessibleOptionsRadio.WheelchairAndAssistance',
	'I need a step free journey with staff assistance',
	'I need a step free journey with staff assistance'


EXEC AddtblContent
	1, 1, 'langStrings', 'DataServices.AccessibleOptionsRadio.Assistance',
	'I need a journey with staff assistance',
	'I need a journey with staff assistance'


EXEC AddtblContent
	1, 1, 'langStrings', 'DataServices.AccessibleOptionsRadio.NoRequirement',
	'I do not have any accessibility requirements',
	'I do not have any accessibility requirements'


EXEC AddtblContent
	1, 1, 'langStrings', 'AcesssibleOptions.FewestChanges',
	'Plan my journey with the fewest changes',
	'Plan my journey with the fewest changes'


EXEC AddtblContent
	1, 1, 'langStrings', 'AcesssibleOptions.Wheelchair.Info',
	'This option uses all modes where it is possible for a step-free journey to be undertaken, both with and without assistance.  It includes buses and coaches that have access ramps fitted that would enable a wheelchair user to access the vehicle unaided, or vehicles with lifts.  It includes step-free tram and light rail stations, and underground stations that are step free from street to vehicle.  It also includes rail (stations and services) and coach, where you should book in advance to ensure staff are available to help you to board and alight a vehicle, and to secure a wheelchair space where required.',
	'This option uses all modes where it is possible for a step-free journey to be undertaken, both with and without assistance.  It includes buses and coaches that have access ramps fitted that would enable a wheelchair user to access the vehicle unaided, or vehicles with lifts.  It includes step-free tram and light rail stations, and underground stations that are step free from street to vehicle.  It also includes rail (stations and services) and coach, where you should book in advance to ensure staff are available to help you to board and alight a vehicle, and to secure a wheelchair space where required.'


EXEC AddtblContent
	1, 1, 'langStrings', 'AcesssibleOptions.WheelchairAndAssistance.Info',
	'This option includes a network of step-free accessible transport that can be accessed by a wheelchair user, and where staff assistance is also available.    It includes rail and coach services and a limited set of underground stations that are step free from street to vehicle.  It does not include the bus network, or tram and light rail services, where no staff assistance is available.',
	'This option includes a network of step-free accessible transport that can be accessed by a wheelchair user, and where staff assistance is also available.    It includes rail and coach services and a limited set of underground stations that are step free from street to vehicle.  It does not include the bus network, or tram and light rail services, where no staff assistance is available.'


EXEC AddtblContent
	1, 1, 'langStrings', 'AcesssibleOptions.Assistance.Info',
	'Choose this option if you require staff assistance on your journey, but not a step-free journey.  This option includes any stations and stops where staff assistance is provided, as well as places where assistance may only be available on the vehicle, rather than at the station or stop.  Please note you should always book assistance at National Rail stations at least 24 hours in advance.',
	'Choose this option if you require staff assistance on your journey, but not a step-free journey.  This option includes any stations and stops where staff assistance is provided, as well as places where assistance may only be available on the vehicle, rather than at the station or stop.  Please note you should always book assistance at National Rail stations at least 24 hours in advance.'


EXEC AddtblContent
	1, 1, 'langStrings', 'AcesssibleOptions.Assistance.InfoIcon',
	'/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/info_icon 20x20.png',
	'/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/info_icon 20x20.png'


-- Accessible advanced options
EXEC AddtblContent
	1, 1, 'langStrings', 'AccessibleOptionsControl.Type',
	'Further accessibility options',
	'Further accessibility options'


EXEC AddtblContent
	1, 1, 'langStrings', 'AccessibleOptionsControl.ScreenReader',
	'Further accessibility options',
	'Further accessibility options'


EXEC AddtblContent
	1, 1, 'langStrings', 'AccessibleOptionsControl.Question',
	'Need to add accessible transport options?',
	'Need to add accessible transport options?'


EXEC AddtblContent
	1, 1, 'langStrings', 'AccessibleOptionsControl.OptionsSelected',
	'* Options selected',
	'* Options selected'

EXEC AddtblContent
	1, 1, 'langStrings', 'AccessibleOptionsControl.Title',
	'Accessible options',
	'Accessible options'

EXEC AddtblContent
	1, 1, 'langStrings', 'AccessibleOptionsControl.FaqLink.Text',
	'Find out more details about the accessibility options',
	'Find out more details about the accessibility options'

EXEC AddtblContent
	1, 1, 'langStrings', 'AccessibleOptionsControl.FaqLink.Href',
	'../staticnoprint.aspx?id=_web2_help_helpaccessiblejourneyplanning',
	'../staticnoprint.aspx?id=_web2_help_helpaccessiblejourneyplanning'

EXEC AddtblContent
	1, 1, 'langStrings', 'FindCarPreferencesControl.PetrolPerLitre',
	'pence/litre for unleaded petrol,<br />&nbsp;&nbsp;&nbsp;&nbsp;',
	'ceiniog y litr ar gyfer petrol di-blwm,<br/>&nbsp;&nbsp;&nbsp;&nbsp;'

EXEC AddtblContent
	1, 1, 'langStrings', 'TransportTypesControl.Question',
	'Need to pick your mode of transport?',
	'Need to pick your mode of transport?'


EXEC AddtblContent
	1, 1, 'langStrings', 'TransportTypesControl.OptionsSelected',
	'* Options selected',
	'* Options selected'

EXEC AddtblContent
	1, 1, 'langStrings', 'ChangesControl.Question',
	'Need to limit the number of changes?',
	'Need to limit the number of changes?'


EXEC AddtblContent
	1, 1, 'langStrings', 'ChangesControl.OptionsSelected',
	'* Options selected',
	'* Options selected'

EXEC AddtblContent
	1, 1, 'langStrings', 'WalkingSpeedControl.Question',
	'Need to change your walking speed?',
	'Need to change your walking speed?'

EXEC AddtblContent
	1, 1, 'langStrings', 'WalkingSpeedControl.OptionsSelected',
	'* Options selected',
	'* Options selected'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'ViaControl.Question',
	'Need to travel via a specific stop or station?',
	'Need to travel via a specific stop or station?'


EXEC AddtblContent
	1, 1, 'langStrings', 'ViaControl.OptionsSelected',
	'* Options selected',
	'* Options selected'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'LocationViaControl.Question',
	'Need to travel via a specific stop or station?',
	'Need to travel via a specific stop or station?'


EXEC AddtblContent
	1, 1, 'langStrings', 'LocationViaControl.OptionsSelected',
	'* Options selected',
	'* Options selected'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'CarPreferencesControl.Question',
	'Need to change your car journey options?',
	'Need to change your car journey options?'


EXEC AddtblContent
	1, 1, 'langStrings', 'CarPreferencesControl.OptionsSelected',
	'* Options selected',
	'* Options selected'

EXEC AddtblContent
	1, 1, 'langStrings', 'FindPageOptionsControl.Save.Text',
	'Save',
	'Save'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'AdvancedOptions.Show.Text',
	'Show',
	'Dangos'

-- Error messages
EXEC AddtblContent
	1, 1, 'langStrings', 'ValidateAndRun.LocationNotAccessible',
	'Location is not accessible',
	'Location is not accessible'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'JourneyPlannerOutput.AccessibleJourneyNoResults',
	'<ul class="listerdisc"><li>Transport for accessible journeys is more limited. <a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx">View our FAQs on the types and availability of accessible public transport.</a></li></ul>',
	'<ul class="listerdisc"><li>Transport for accessible journeys is more limited. <a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx">View our FAQs on the types and availability of accessible public transport.</a></li></ul>'
	
-- Journey details legs
EXEC AddtblContent
	1, 1, 'langStrings', 'JourneyDetailsControl.LocationAccessibleLink.ToolTipText',
	'Display location and accessibility information for {0}',
	'Display location and accessibility information for {0}'

EXEC AddtblContent
	1, 1, 'langStrings', 'JourneyDetailsTableControl.WalkInterchangeTo',
	'Interchange to {0}',
	'Interchange to {0}'

EXEC AddtblContent
	1, 1, 'langStrings', 'JourneyDetailsTableControl.WalkInterchangeTo.Allow',
	'allow ',
	'allow	'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'JourneyDetailsTableControl.minutesString.Long',
	'minutes',
	'munudau'

EXEC AddtblContent
	1, 1, 'langStrings', 'JourneyDetailsTableControl.minuteString.Long',
	'minute',
	'munud'

EXEC AddtblContent
	1, 1, 'langStrings', 'JourneyDetailsControl.every',
	'Runs every {0} {1}',
	'Runs every {0} {1}'	
	

-- Journey details images
EXEC AddtblContent 1,1, 'langStrings', 'JourneyDetailsControl.imageWalkInterchangeUrl', 
	'/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/walk_interchange.png',
	'/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/walk_interchange.png'

EXEC AddtblContent 1,1, 'langStrings', 'TransportMode.WalkInterchange', 
	'Interchange',
	'Interchange'
	
EXEC AddtblContent 1,1, 'langStrings', 'TransportMode.WalkInterchange.ImageAlternateText', 
	'Interchange',
	'Interchange'
	
EXEC AddtblContent 1,1, 'langStrings', 'TransportModeLowerCase.WalkInterchange', 
	'interchange',
	'interchange'
	
-- Accessible Features Icons
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.ServiceAssistanceBoarding', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_assistance.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_assistance.png'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.ServiceAssistanceWheelchair', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_wheelchair_assistance_2.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_wheelchair_assistance_2.png'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.ServiceAssistanceWheelchairBooked', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_wheelchair_assistance_2.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_wheelchair_assistance_2.png'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.ServiceAssistancePorterage', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_assistance.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_assistance.png'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.ServiceAssistanceOther', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_assistance.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_assistance.png'

EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.ServiceLowFloor', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_low_floor_vehicle.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_low_floor_vehicle.png'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.ServiceLowFloorTram', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_platform_access.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_platform_access.png'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.ServiceMobilityImpairedAccess', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk_2.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk_2.png'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.ServiceMobilityImpairedAccessBus', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk_2.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk_2.png'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.ServiceMobilityImpairedAccessService', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk_2.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk_2.png'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.ServiceWheelchairBookingRequired', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk_2.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk_2.png'

--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.EscalatorFreeAccess', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_EscalatorFreeAccess.gif', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_EscalatorFreeAccess.gif'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.LiftFreeAccess', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_LiftFreeAccess.gif', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_LiftFreeAccess.gif'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.MobilityImpairedAccess', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk_2.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk_2.png'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.StepFreeAccess', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_StepFreeAccess.gif', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_StepFreeAccess.gif'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.WheelchairAccess', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk_2.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk_2.png'

EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessLiftUp', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_lift_up.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_lift_up.png'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessLiftDown', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_lift_down.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_lift_down.png'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessStairsUp', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_stairs_up.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_stairs_up.png'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessStairsDown', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_stairs_down.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_stairs_down.png'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessSeriesOfStairs', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_stairs_up.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_stairs_up.png'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessEscalatorUp', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_escalator_up.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_escalator_up.png'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessEscalatorDown', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_escalator_down.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_escalator_down.png'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessTravelator', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessTravelator.gif', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessTravelator.gif'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessRampUp', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_ramp_up.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_ramp_up.png'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessRampDown', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_ramp_down.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_ramp_down.png'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessShuttle', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessShuttle.gif', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessShuttle.gif'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessBarrier', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessBarrier.gif', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessBarrier.gif'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessNarrowEntrance', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessNarrowEntrance.gif', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessNarrowEntrance.gif'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessConfinedSpace', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessConfinedSpace.gif', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessConfinedSpace.gif'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessQueueManagement', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessQueueManagement.gif', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessQueueManagement.gif'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessNone', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessNone.gif', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessNone.gif'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessUnknown', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessUnknown.gif', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessUnknown.gif'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessOther', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessOther.gif', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessOther.gif'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessOpenSpace', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessOpenSpace.gif', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_AccessOpenSpace.gif'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessStreet', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk.png'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessPavement', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk.png'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessFootpath', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_walk.png'
--EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.ImageURL.AccessPassage', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_passage.png', '/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/en/Accessible_passage.png'

EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.ServiceAssistanceBoarding', 'Assistance with boarding and alighting', 'Assistance with boarding and alighting'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.ServiceAssistanceWheelchair', 'Assistance for wheelchair users', 'Assistance for wheelchair users'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.ServiceAssistanceWheelchairBooked', 'Assistance for wheelchair users', 'Assistance for wheelchair users'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.ServiceAssistancePorterage', 'Assistance with luggage', 'Assistance with luggage'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.ServiceAssistanceOther', 'Assistance available', 'Assistance available'

EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.ServiceLowFloor', 'Normally operated by a low floor wheelchair accessible bus', 'Normally operated by a low floor wheelchair accessible bus'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.ServiceLowFloorTram', 'Normally operated by a wheelchair accessible vehicle', 'Normally operated by a wheelchair accessible vehicle'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.ServiceMobilityImpairedAccess', 'Normally operated by a wheelchair accessible vehicle', 'Normally operated by a wheelchair accessible vehicle'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.ServiceMobilityImpairedAccessBus', 'Normally operated by a wheelchair accessible bus', 'Normally operated by a wheelchair accessible bus'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.ServiceMobilityImpairedAccessService', 'Normally operated by a wheelchair accessible service', 'Normally operated by a wheelchair accessible service'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.ServiceWheelchairBookingRequired', 'Booking required for wheelchair users', 'Booking required for wheelchair users'

EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.EscalatorFreeAccess', 'Escalator free access', 'Escalator free access'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.LiftFreeAccess', 'Lift free access', 'Lift free access'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.MobilityImpairedAccess', 'Access for wheelchair users', 'Access for wheelchair users' -- same as WheelchairAccess
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.StepFreeAccess', 'Step free access', 'Step free access'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.WheelchairAccess', 'Access for wheelchair users', 'Access for wheelchair users'

EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessLiftUp', 'Lift up', 'Lift up'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessLiftDown', 'Lift down', 'Lift down'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessStairsUp', 'Stairs up', 'Stairs up'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessStairsDown', 'Stairs down', 'Stairs down'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessSeriesOfStairs', 'Series of stairs', 'Series of stairs'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessEscalatorUp', 'Escalator up', 'Escalator up'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessEscalatorDown', 'Escalator down', 'Escalator down'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessTravelator', 'Travelator', 'Travelator'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessRampUp', 'Ramp up', 'Ramp up'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessRampDown', 'Ramp down', 'Ramp down'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessShuttle', 'Shuttle', 'Shuttle'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessBarrier', 'Barrier', 'Barrier'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessNarrowEntrance', 'Narrow entrance', 'Narrow entrance'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessConfinedSpace', 'Confined space', 'Confined space'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessQueueManagement', 'Queue management', 'Queue management'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessNone', 'No access', 'No access'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessUnknown', 'Unknown access', 'Unknown access'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessOther', 'Other access', 'Other access'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessOpenSpace', 'Open space', 'Open space'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessStreet', 'Street', 'Street'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessPavement', 'Pavement', 'Pavement'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessFootpath', 'Footpath', 'Footpath'
EXEC AddtblContent 1,1, 'JourneyResults', 'AccessibleFeaturesIcon.AltTextToolTip.AccessPassage', 'Passage', 'Passage'


-- Accessible booking messages
EXEC AddtblContent
	1, 1, 'langStrings', 'JourneyDetailsControl.AccessibleBooking.Wheelchair.Advanced',
	'Wheelchair travel should be booked in advance',
	'Wheelchair travel should be booked in advance'

EXEC AddtblContent
	1, 1, 'langStrings', 'JourneyDetailsControl.AccessibleBooking.Wheelchair.Today',
	'Wheelchair travel should be booked in advance - and may not be available if booked on day of travel',
	'Wheelchair travel should be booked in advance - and may not be available if booked on day of travel'

EXEC AddtblContent
	1, 1, 'langStrings', 'JourneyDetailsControl.AccessibleBooking.Assistance.Advanced',
	'Travel assistance should be booked in advance',
	'Travel assistance should be booked in advance'

EXEC AddtblContent
	1, 1, 'langStrings', 'JourneyDetailsControl.AccessibleBooking.Assistance.Today',
	'Assistance requests must be booked in advance - and may not be available if booked on the day of travel',
	'Assistance requests must be booked in advance - and may not be available if booked on the day of travel'

EXEC AddtblContent
	1, 1, 'langStrings', 'JourneyDetailsControl.AccessibleBooking.Wheelchair.Book.Phone',
	'To book a wheelchair space in advance please phone {0}',
	'To book a wheelchair space in advance please phone {0}'

EXEC AddtblContent
	1, 1, 'langStrings', 'JourneyDetailsControl.AccessibleBooking.Wheelchair.Book.Link',
	'Find out how to book a wheelchair space{0}',
	'Find out how to book a wheelchair space{0}'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'JourneyDetailsControl.AccessibleBooking.Assistance.Book.Phone',
	'To book assistance in advance please phone {0}',
	'To book assistance in advance please phone {0}'

EXEC AddtblContent
	1, 1, 'langStrings', 'JourneyDetailsControl.AccessibleBooking.Assistance.Book.Link',
	'Find out how to book assistance{0}',
	'Find out how to book assistance{0}'

-- Find nearest TDAN page
EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.AppendPageTitle',
	'Route Planner | Find Nearest Accessible Stop | ',
	'Route Planner | Find Nearest Accessible Stop | '
	
EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.Title',
	'Find nearest accessible stop',
	'Find nearest accessible stop'
	
EXEC AddtblContent -- Subsequent content strings are substituted in
	1, 1, 'langStrings', 'FindNearestAccessibleStop.SubHeading',
	'You requested {0}. 
	Transport Direct is not able to find a journey meeting your requirements from your chosen {1}. 
	Please search for stops or places near your {2} that meet your accessibility requirement.',
	'You requested {0}. 
	Transport Direct is not able to find a journey meeting your requirements from your chosen {1}. 
	Please search for stops or places near your {2} that meet your accessibility requirement.'

EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.SubHeading.WheelchairAssistance',
	'a journey that is step-free and you also require staff assistance',
	'a journey that is step-free and you also require staff assistance'

EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.SubHeading.Wheelchair',
	'a journey that is step-free',
	'a journey that is step-free'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.SubHeading.Assistance',
	'a journey with staff assistance, at stations, stops and piers',
	'a journey with staff assistance, at stations, stops and piers'	

EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.SubHeading.Origin',
	'origin',
	'origin'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.SubHeading.Destination',
	'destination',
	'destination'

EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.SubHeading.Via',
	'via',
	'via'	

EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.SubHeading.And',
	'and',
	'a'

EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.NextButton',
	'Next',
	'Nesaf'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.BackButton',
	'Back',
	'Yn ôl'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'viaSelect.labelLocationTitle',
	'Via',
	'Drwy'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.SelectStopType.Error',
	'Please select at least one stop type',
	'Please select at least one stop type'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.ChangeStopType.Error',
	'Please change your stop type selection',
	'Please change your stop type selection'

EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.NoAccessibleLocations.Error',
	'Please select back to amend your journey details',
	'Please select back to amend your journey details'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.SelectStop.Origin.Error',
	'Please choose an accessible stop for your ''origin'', then select ''Next''',
	'Please choose an accessible stop for your ''origin'', then select ''Next'''
	
EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.SelectStop.Destination.Error',
	'Please choose an accessible stop for your ''destination'', then select ''Next''',
	'Please choose an accessible stop for your ''destination'', then select ''Next'''
	
EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.SelectStop.Via.Error',
	'Please choose an accessible stop for your ''via'', then select ''Next''',
	'Please choose an accessible stop for your ''via'', then select ''Next'''
		
EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.HelpPageUrl',
	'Help/HelpFindNearestAccessibleStop.aspx',
	'Help/HelpFindNearestAccessibleStop.aspx'

EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.SearchDistance.Text',
	'Search distance (metres)',
	'Search distance (metres)'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'FindNearestAccessibleStop.Update.Text',
	'Update',
	'Update'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'FindTDANControl.MapButton.Text',
	'View on map',
	'View on map'
	--'Canfyddwch ar y map'

EXEC AddtblContent
	1, 1, 'langStrings', 'FindTDANControl.itemPleaseSelect',
	'Select your stop',
	'Select your stop'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'FindTDANControl.AccessibleLocationsNotFound.Error',
	'Sorry no places were found with your selection',
	'Sorry no places were found with your selection'

EXEC AddtblContent
	1, 1, 'langStrings', 'AccessibleTransportTypesControl.FindTDAN',
	'You can select the type of stop you would prefer to use.',
	'You can select the type of stop you would prefer to use.'	
	
EXEC AddtblContent
	1, 1, 'langStrings', 'AccessibleTransportTypesControl.Update',
	'Update',
	'Update'		

-- Accessible options radio data list
EXEC AddtblContent 1,1, 'langStrings', 'DataServices.AccessibleTransportTypes.Rail', 'Rail', 'Rail'
EXEC AddtblContent 1,1, 'langStrings', 'DataServices.AccessibleTransportTypes.Bus', 'Bus/coach', 'Bus/coach'
EXEC AddtblContent 1,1, 'langStrings', 'DataServices.AccessibleTransportTypes.Underground', 'Underground/Metro', 'Underground/Metro'
EXEC AddtblContent 1,1, 'langStrings', 'DataServices.AccessibleTransportTypes.LightRail', 'Tram/light rail', 'Tram/light rail'
EXEC AddtblContent 1,1, 'langStrings', 'DataServices.AccessibleTransportTypes.Ferry', 'Ferry', 'Ferry'
EXEC AddtblContent 1,1, 'langStrings', 'DataServices.AccessibleTransportTypes.DLR', 'DLR', 'DLR'
EXEC AddtblContent 1,1, 'langStrings', 'DataServices.AccessibleTransportTypes.Locality', 'Town/district/village', 'Town/district/village'


-- Modify page
EXEC AddtblContent 1,1, 'RefineJourney', 'RefineJourney.labelAccessibleNote', 
'Note: Your journey is planned with your selected accessibility options',
'Note: Your journey is planned with your selected accessibility options'

  
-- FAQ Page setup
DECLARE @GroupId int
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'ContentDatabase')

EXEC AddtblContent 1,@GroupId, '_Web2_Help_HelpAccessibleJourneyPlanning', 'Page', 
'/Web2/staticnoprint.aspx', '/Web2/staticnoprint.aspx'

EXEC AddtblContent 1,@GroupId, '_Web2_Help_HelpAccessibleJourneyPlanning', 'QueryString', 
'staticnoprint', 'staticnoprint'

EXEC AddtblContent 1,@GroupId, '_Web2_Help_HelpAccessibleJourneyPlanning', 'Channel', 
'/Channels/TransportDirect/Help/HelpAccessibleJourneyPlanning', 
'/Channels/TransportDirect/Help/HelpAccessibleJourneyPlanning'

EXEC AddtblContent 1,@GroupId, '_Web2_Help_HelpAccessibleJourneyPlanning', 'Title', 
'Accessible Journey Planning Help | Transport Direct', 
'Accessible Journey Planning Help | Transport Direct'


EXEC AddtblContent 1,@GroupId, '_Web2_Printer_HelpAccessibleJourneyPlanning', 'Page', 
'/Web2/staticdefault.aspx', '/Web2/staticdefault.aspx'

EXEC AddtblContent 1,@GroupId, '_Web2_Printer_HelpAccessibleJourneyPlanning', 'QueryString', 
'staticdefault', 'staticdefault'

EXEC AddtblContent 1,@GroupId, '_Web2_Printer_HelpAccessibleJourneyPlanning', 'Channel', 
'/Channels/TransportDirect/Printer/HelpAccessibleJourneyPlanning', 
'/Channels/TransportDirect/Printer/HelpAccessibleJourneyPlanning'

EXEC AddtblContent 1,@GroupId, '_Web2_Printer_HelpAccessibleJourneyPlanning', 'Title', 
'Accessible Journey Planning Help | Transport Direct', 
'Accessible Journey Planning Help | Transport Direct'


GO

-- Right hand info for FinadNearestAccessibleStop
IF NOT EXISTS(SELECT * FROM tblGroup WHERE [Name] like 'journeyplanning_findnearestaccessiblestop') 
	INSERT INTO tblGroup (GroupId, [Name])
	SELECT MAX(GroupId)+1, 'journeyplanning_findnearestaccessiblestop' FROM tblGroup

DECLARE @GroupId int
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'journeyplanning_findnearestaccessiblestop')

DECLARE @ThemeId int
SET @ThemeId = 1

EXEC AddtblContent
@ThemeId, @GroupId, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/JourneyPlanning/FindNearestAccessibleStop', 
'<div class="Column3Header">
	<div class="txtsevenbbl">
		Accessibility Information
	</div>
	<!-- Don''t remove spaces -->&nbsp;&nbsp;
</div>
<div class="Column3Content">
	<table width="100%" cellspacing="0" cellpadding="2" border="0" id="tableAccessibilityInformation">
		<tbody>
			<tr>
				<td class="txtseven">
					The Transport Direct accessible journey planner will plan journeys on a limited
					network where we have identified that the stations/stops and services are step free
					and/or have staff assistance available. The definition of these options can be found 
					by hovering over the information buttons or by checking 
					our <a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx">FAQ</a>
					<br />
				</td>
			</tr>
		</tbody>
	</table>
</div>',
'<div class="Column3Header">
	<div class="txtsevenbbl">
		Accessibility Information
	</div>
	<!-- Don''t remove spaces -->&nbsp;&nbsp;
</div>
<div class="Column3Content">
	<table width="100%" cellspacing="0" cellpadding="2" border="0" id="tableAccessibilityInformation">
		<tbody>
			<tr>
				<td class="txtseven">
					The Transport Direct accessible journey planner will plan journeys on a limited
					network where we have identified that the stations/stops and services are step free
					and/or have staff assistance available. The definition of these options can be found 
					by hovering over the information buttons or by checking 
					our <a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx">FAQ</a>
					<br />
				</td>
			</tr>
		</tbody>
	</table>
</div>'

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 1999
SET @ScriptDesc = 'Accessibility options content'

IF EXISTS (SELECT * FROM [dbo].[ChangeCatalogue] WHERE ScriptNumber = @ScriptNumber)
  BEGIN
    UPDATE [dbo].[ChangeCatalogue]
    SET ChangeDate = getDate(), Summary = @ScriptDesc
    WHERE ScriptNumber = @ScriptNumber
  END
ELSE
  BEGIN
    INSERT INTO [dbo].[ChangeCatalogue] (ScriptNumber, ChangeDate, Summary)
    VALUES (@ScriptNumber, getDate(), @ScriptDesc)
  END
GO