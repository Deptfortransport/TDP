-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : TDPWeb and TDPMobile shared properties data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

------------------------------------------------
-- 'TDPWeb' and 'TDPMobile' properties
-- Also refer to the application specific properties defined in PropertiesDataWeb.sql and PropertiesDataMobile.sql
------------------------------------------------

DECLARE @AID varchar(50) = '<Default>'
DECLARE @GID varchar(50) = 'TDPortal'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

-- Property Service
EXEC AddUpdateProperty 'propertyservice.version', '1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'propertyservice.refreshrate', '300000', @AID, @GID, @PartnerID, @ThemeID

-- Site Version
EXEC AddUpdateProperty 'Site.VersionNumber', '2.7.1', @AID, @GID, @PartnerID, @ThemeID

-- Site Mode - Default to Paralympics switch date
EXEC AddUpdateProperty 'Site.DefaultSiteMode.Switch.Date', '13/08/2012 00:00', @AID, @GID, @PartnerID, @ThemeID

-- State Server
EXEC AddUpdateProperty 'StateServer.RetriesMax', '10', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'StateServer.SleepIntervalMilliSecs', '250', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'StateServer.ExpiryTimeMins', '120', @AID, @GID, @PartnerID, @ThemeID

-- Site redirect
-- Regex values come from http://detectmobilebrowsers.com version taken on 3/7/2012
EXEC AddUpdateProperty 'SiteRedirector.RegexB', 'android.+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|meego.+mobile|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'SiteRedirector.RegexV', '1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(di|rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-', @AID, @GID, @PartnerID, @ThemeID

-- Data Notification - Groups
EXEC AddUpdateProperty 'DataNotification.PollingInterval.Seconds', '60', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataNotification.Groups', 'Configuration,Content,Gazetteer,TravelNews,UndergroundNews', @AID, @GID, @PartnerID, @ThemeID

-- Data Notification - Tables
EXEC AddUpdateProperty 'DataNotification.Configuration.Database', 'DefaultDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataNotification.Configuration.Tables', '', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'DataNotification.Content.Database', 'ContentDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataNotification.Content.Tables', 'Content,ContentGroup,ContentOverride', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'DataNotification.Gazetteer.Database', 'AtosAdditionalDataDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataNotification.Gazetteer.Tables', '', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'DataNotification.TravelNews.Database', 'TransientPortalDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataNotification.TravelNews.Tables', 'TravelNewsImport', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'DataNotification.UndergroundNews.Database', 'TransientPortalDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataNotification.UndergroundNews.Tables', 'UndergroundStatusImport', @AID, @GID, @PartnerID, @ThemeID


-- Content
EXEC AddUpdateProperty 'Content.DailyDataRefreshTime', '0300', @AID, @GID, @PartnerID, @ThemeID

-- Cookies
EXEC AddUpdateProperty 'Cookie.RepeatVisitor.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Cookie.ExpiryTimeSpan.Seconds', '15778463', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Cookie.UserAgent.HeaderKey', 'User-Agent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Cookie.UserAgent.Robot.Pattern', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Cookie.UserAgent.Robot.RegularExpression', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Cookie.LoadJourneyRequest.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID

-- Cookies - Policy link
EXEC AddUpdateProperty 'Cookie.CookiePolicy.Hyperlink.VisibleFrom.Date', '14/05/2012', @AID, @GID, @PartnerID, @ThemeID

-- Location Service
EXEC AddUpdateProperty 'LocationService.Cache.LoadLocations', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.Cache.LoadPostcodes', 'true', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'LocationService.NaptanPrefix.Airport', '9200', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.NaptanPrefix.Coach', '9000', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.NaptanPrefix.Rail', '9100', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'LocationService.SearchLocations.InCache', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.SearchLocationsLimit.Count.Max', '1000', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.SearchLocationsShow.Count.Max', '20', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.SearchLocationsShow.GroupStationsLimit.Count', '100', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.SearchLocationsShow.RailStationsLimit.Count', '5', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.SearchLocationsShow.POILimit.Count', '5', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.SearchLocationsShow.CoachStationsLimit.Count', '2', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.SearchLocationsShow.TramStationsLimit.Count', '5', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.SearchLocationsShow.FerryStationsLimit.Count', '100', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.SearchLocationsShow.AirportStationsLimit.Count', '100', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'LocationService.CommonWords','rail,station,stations,bus,airport,terminal,Coach,tramway,tramlink,dlr,subway,spt,steam,rly,underground,metrolink,tram,rhdr,supertram,metro', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.SimilarityIndex.NoCommonWords.Min', '0.5', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.SimilarityIndex.NoCommonWordsAndSpace.Min', '0.5', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.SimilarityIndex.IndividualWords.Min', '0.65', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.SimilarityIndex.ChildLocalityAtEnd', 'true', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'LocationService.CoordinateLocation.LocalitySearch.EastingPadding.Metres', '100000', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.CoordinateLocation.LocalitySearch.NorthingPadding.Metres', '100000', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.CoordinateLocation.LocationName.AppendLocalityName.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'locationservice.servername', 'GIS', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'locationservice.servicename', 'tdparc93', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'locationservice.gazopsweburl', 'http://GAZOPS/GazopsWeb/GazopsWeb.asmx', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'locationservice.gazopsweb.servicebinding.config', 'GazopsServiceSoap', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'locationservice.addressgazetteerid', 'ADDP1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'locationservice.postcodegazetteerid', 'PCODE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'locationservice.partpostcodegazetteerid', 'PCODE2', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'locationservice.localitygazetteerid', 'DRILL1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'locationservice.codegazetteerid', 'CODEGAZ1', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'locationservice.addresspostcode.minscore', '20', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'locationservice.locality.minscore', '20', @AID, @GID, @PartnerID, @ThemeID

-- Journey Validate and Runner
EXEC AddUpdateProperty 'JourneyPlanner.Switch.CyclePlanner.Available', 'true', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyPlanner.Validate.Switch.DatesInGamesDateRange', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyPlanner.Validate.Switch.OneLocationIsVenue', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyPlanner.Validate.Switch.CycleLocationsDistance', 'true', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyPlanner.Validate.Switch.TimeTodayInThePast', 'false', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyPlanner.Dates.NumberOfMonthsToShow.Count', '3', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyPlanner.Dates.UseGamesDateRange.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyPlanner.Validate.Games.StartDate', '2013/07/15 00:00:00', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyPlanner.Validate.Games.EndDate', '2013/09/14 23:59:59', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyPlanner.Validate.Locations.CycleDistance.Metres', '5000000', @AID, @GID, @PartnerID, @ThemeID

-- Journey Control (CJP and CyclePlanner)
EXEC AddUpdateProperty 'JourneyControl.Log.NoJourneyResponses', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Log.JourneyWebFailures', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Log.TTBOFailures', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Log.CJPFailures', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Log.RoadEngineFailures', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Log.CyclePlannerFailures', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Log.DisplayableMessages', 'true', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.Code.CJP.OK', '0', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.CJP.NoPublicJourney', '18', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.CJP.JourneysRejected', '19', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.CJP.AwkwardOvernightRejected','30', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.CJP.RoadEngineOK', '100', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.CJP.RoadEngineMin', '100', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.CJP.RoadEngineMax', '199', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.Code.JourneyWeb.MajorNoResults', '1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.JourneyWeb.MinorPast', '1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.JourneyWeb.MinorFuture', '2', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.JourneyWeb.MajorGeneral', '9', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.JourneyWeb.MinorDisplayable', '2', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.Code.TTBO.MajorOK', '0', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.TTBO.MinorOK', '1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.TTBO.MinorNoResults', '1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.TTBO.NoTimetableServiceFound', '302', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.Code.CTP.OK', '100', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.CTP.UndeterminedError', '1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.CTP.SystemException', '2', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.CTP.OperationNotSupported', '3', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.CTP.InvalidRequest', '4', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.CTP.ErrorConnectingToDatabase', '5', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.Code.CTP.NoJourneyCouldBeFound', '6', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.Notes.TrapezeRegions', 'S,Y,NW,SW,W,WM', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.NullTollLink.Value', 'No URL has been set for this operator', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CongestionWarning.Value', '30', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.ToidPrefix', 'osgb', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.CJP.TimeoutMillisecs', '60000', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.RiverService.Interchange.Max.Minutes', '60', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.RiverService.Interchange.Minutes', '15', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.Algorithm.Public', 'Default', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.Algorithm.Public.MinChanges', 'MinChanges', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.DrtIsRequired', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.Sequence', '3', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.Sequence.RiverServicePlannerMode', '3', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.InterchangeSpeed', '0', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.WalkingSpeed.MetresPerMin', '80', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.WalkingSpeed.Assistance.MetresPerMin', '80', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.WalkingSpeed.StepFree.MetresPerMin', '80', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.WalkingSpeed.StepFreeAssistance.MetresPerMin', '80', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.MaxWalkingTime.Minutes', '25', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.WalkingDistance.Assistance.Metres', '2000', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.WalkingDistance.StepFree.Metres', '2000', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.WalkingDistance.StepFreeAssistance.Metres', '2000', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.RoutingGuideInfluenced', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.RoutingGuideCompliantJourneysOnly', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.RouteCodes', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.OlympicRequest', 'false', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.TravelDemandPlan.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.TravelDemandPlan.Off', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.TravelDemandPlan.Outward', 'AccessMajorEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.TravelDemandPlan.Return', 'LeaveMajorEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.TravelDemandPlan.Outward.Accessible.DoNotUseUnderground', 'AccessMajorEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.TravelDemandPlan.Return.Accessible.DoNotUseUnderground', 'LeaveMajorEvent', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.DontForceCoach.OriginDestinationLondon', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.DontForceCoach.Accessible.OriginDestinationLondon', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.DontForceCoach.Accessible.FewerChanges', 'true', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.Algorithm.Private', 'Fastest', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.AvoidMotorways', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.AvoidFerries', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.AvoidTolls', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.DrivingSpeed.KmPerHour', '112', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.DoNotUseMotorways', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.FuelConsumption.MetresPerLitre', '13807', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.FuelPrice.PencePerLitre', '1150', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CJP.JourneyRequest.RemoveAwkwardOvernight', 'false', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.TimeoutMillisecs', '120000', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.JourneyRequest.IncludeToids', 'false', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.PenaltyFunction.Algorithm', 'QuickestV913', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.PenaltyFunction.QuickestV913.DllPath', 'D:\TransportDirect\Services\CycleHost\', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.PenaltyFunction.QuickestV913.Dll', 'td.cp.CyclePenaltyFunctions.v3.dll', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.PenaltyFunction.QuickestV913.Prefix', 'TransportDirect.JourneyPlanning.CyclePenaltyFunctions', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.PenaltyFunction.QuickestV913.Suffix', 'QuickestV913', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.PenaltyFunction.QuietestV913.DllPath', 'D:\TransportDirect\Services\CycleHost\', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.PenaltyFunction.QuietestV913.Dll', 'td.cp.CyclePenaltyFunctions.v3.dll', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.PenaltyFunction.QuietestV913.Prefix', 'TransportDirect.JourneyPlanning.CyclePenaltyFunctions', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.PenaltyFunction.QuietestV913.Suffix', 'QuietestV913', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.PenaltyFunction.RecreationalV913.DllPath', 'D:\TransportDirect\Services\CycleHost\', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.PenaltyFunction.RecreationalV913.Dll', 'td.cp.CyclePenaltyFunctions.v3.dll', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.PenaltyFunction.RecreationalV913.Prefix', 'TransportDirect.JourneyPlanning.CyclePenaltyFunctions', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.PenaltyFunction.RecreationalV913.Suffix', 'RecreationalV913', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.NumberOfPreferences', '15', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.Preference.0', '850', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.Preference.1', '11000', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.Preference.2', 'Congestion', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.Preference.3', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.Preference.4', 'Bicycle', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.Preference.5', '19', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.Preference.6', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.Preference.7', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.Preference.8', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.Preference.9', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.Preference.10', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.Preference.11', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.Preference.12', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.Preference.13', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.UserPreference.Preference.14', 'false', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.JourneyResultSetting.IncludeToids', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.JourneyResultSetting.IncludeGeometry', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.JourneyResultSetting.IncludeText', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.JourneyResultSetting.PointSeperator', ' ', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.JourneyResultSetting.EastingNorthingSeperator', ',', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyControl.CyclePlanner.JourneyResult.IncludeLatitudeLongitude', 'true', @AID, @GID, @PartnerID, @ThemeID

-- Cycle Planner Service
EXEC AddUpdateProperty 'CyclePlanner.WebService.URL','http://localhost:666/cycleplannerservice/service.asmx', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'CyclePlanner.WebService.Timeout.Seconds', '120', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'CyclePlanner.WebService.MaxReceivedMessageSize','653560', @AID, @GID, @PartnerID, @ThemeID

-- Coordinate Convertor Service
EXEC AddUpdateProperty 'CoordinateConvertor.WebService.URL', 'http://localhost/TDPWebServices/CoordinateConvertorService/CoordinateConvertor.asmx', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'CoordinateConvertor.WebService.Timeout.Seconds', '30', @AID, @GID, @PartnerID, @ThemeID

-- Stop Event Service
EXEC AddUpdateProperty 'JourneyControl.StopEvents.CJP.TimeoutMillisecs', '30000', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.StopEvents.JourneyRequest.IncludeLocationFilter', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.StopEvents.JourneyRequest.RealTimeRequired', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.StopEvents.JourneyRequest.Range', '4', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyControl.StopEvents.JourneyRequest.Replan.Range', '6', @AID, @GID, @PartnerID, @ThemeID

-- Retail
EXEC AddUpdateProperty 'Retail.Retailers.ShowRetailers.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Retail.Retailers.ShowTestRetailers.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Retail.RetailHandoffXml.Schema.Path','D:\inetpub\wwwroot\TDPWeb\Schemas\SJPBookingSystemHandoff.xsd', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Retail.RetailHandoffXml.Xmlns', 'http://www.transportdirect.info/SJPBookingSystemHandoff', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Retail.RetailHandoffXml.Xmlns.Xs', 'http://www.w3.org/2001/XMLSchema', @AID, @GID, @PartnerID, @ThemeID

-- Retail - Travelcard
EXEC AddUpdateProperty 'Retail.Travelcard.ProcessJourneyLeg.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Retail.Travelcard.RetailHandoffXml.IncludeTravelcard.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID

-- Retail - Combined Coach and Rail scenario
EXEC AddUpdateProperty 'Retail.CombinedCoachRail.ShowCombinedCoachRail.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Retail.CombinedCoachRail.CoachServiceOperatorCode', '5364', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Retail.CombinedCoachRail.RailServiceOperatorCode', 'RailSE', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Retail.CombinedCoachRail.CoachStationNaptan', '2400100073', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Retail.CombinedCoachRail.RailStationNaptan', '9100EBSFDOM', @AID, @GID, @PartnerID, @ThemeID

-- Header Links
EXEC AddUpdateProperty 'Header.Link.SkipToContent.Visible.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID

-- EventDateControl
EXEC AddUpdateProperty 'EventDateControl.Time.QuickPick.Switch.DisableTimeTodayInThePast', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'EventDateControl.Time.QuickPick.IntervalMinutes', '15', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'EventDateControl.Time.Default.RoundedUpMinutes', '15', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'EventDateControl.DropDownTime.Outward.Default', '09:00', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'EventDateControl.DropDownTime.Return.Default', '17:00', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'EventDateControl.DropDownTime.IntervalMinutes', '5', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'EventDateControl.DropDownTime.OutwardReturnIntervalHours', '1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'EventDateControl.Now.Link.Switch', 'false', @AID, @GID, @PartnerID, @ThemeID


-- LocationControl
EXEC AddUpdateProperty 'LocationControl.VenueGrouping.Switch','true', @AID, @GID, @PartnerID, @ThemeID

-- Journey Options - Replan journey
EXEC AddUpdateProperty 'DetailsSummaryControl.Replan.Earlier.Interval.Minutes', '120', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DetailsSummaryControl.Replan.Later.Interval.Minutes', '1', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'JourneyOptions.Replan.Earlier.Interval.Minutes', '1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyOptions.Replan.Later.Interval.Minutes', '1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyOptions.Replan.Earlier.River.Interval.Minutes', '120', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyOptions.Replan.Later.River.Interval.Minutes', '1', @AID, @GID, @PartnerID, @ThemeID


-- Journey Details Leg - Instruction Underground color coding
EXEC AddUpdateProperty 'JourneyDetails.LondonUnderground.Operator','London Underground', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyDetails.LondonUnderground.Service.ColorCode','true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'JourneyDetails.LondonDLR.Operator','Dockland Light Railway', @AID, @GID, @PartnerID, @ThemeID

-- Journey Details Leg - Suppress accessibility icons for stop naptans with following prefix, comma seperated list
EXEC AddUpdateProperty 'JourneyDetails.Accessibility.Icons.SuppressForNaPTANs','8100', @AID, @GID, @PartnerID, @ThemeID

-- Journey Details Leg - Identify a venue location naptan
EXEC AddUpdateProperty 'JourneyDetails.Location.Venue.Naptan.Prefix','8100', @AID, @GID, @PartnerID, @ThemeID

-- Journey Details Leg - Identify telecabine location naptans (Greenwich MET, Greenwich PLT, Royal Docks MET, Royal Docks PLT)
EXEC AddUpdateProperty 'JourneyDetails.Location.Telecabine.Naptans','9400ZZALGWP,9400ZZALGWP1,9400ZZALRDK,9400ZZALRDK1', @AID, @GID, @PartnerID, @ThemeID

-- Journey Details Leg - GPXLink Switch
EXEC AddUpdateProperty 'DetailsLegControl.GPXLink.Available.Switch','true', @AID, @GID, @PartnerID, @ThemeID

-- Journey Details Leg - ServiceNumber for Bus and Coach on Journey Result
EXEC AddUpdateProperty 'DetailsLegControl.ShowServiceNumber.Switch','true', @AID, @GID, @PartnerID, @ThemeID

-- Journey Details Leg - Check constraint minimum duration total for the queue mode and icon to be shown
EXEC AddUpdateProperty 'DetailsLegControl.CheckConstraint.ShowQueue.MinimumDuration.Seconds','120', @AID, @GID, @PartnerID, @ThemeID

-- Journey Details Leg - Leg minimum duration for the allow time for entering/exiting venue to be shown (only used for venue legs)
EXEC AddUpdateProperty 'DetailsLegControl.AllowTime.ShowDelay.MinimumDuration.Seconds','121', @AID, @GID, @PartnerID, @ThemeID

-- Cycle Detail Formatter
EXEC AddUpdateProperty 'CyclePlanner.CycleJourneyDetailsControl.ImmediateTurnDistance','161', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'CyclePlanner.Display.AdditionalInstructionText','true', @AID, @GID, @PartnerID, @ThemeID

-- Car Detail Formatter
EXEC AddUpdateProperty 'Web.CarJourneyDetailsControl.ImmediateTurnDistance','161', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Web.CarJourneyDetailsControl.SlipRoadDistance','805', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'CCN0602LondonCCzoneExtraTextVisible','false', @AID, @GID, @PartnerID, @ThemeID

-- Accessibility Options
EXEC AddUpdateProperty 'JourneyPlannerInput.CheckForGNATStation.Switch','true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'AccessibilityOptions.DistrictList.AdminAreaCode.London','82', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'AccessibilityOptions.DistrictList.Visible.LondonOnly','true', @AID, @GID, @PartnerID, @ThemeID

-- The mall naptan
EXEC AddUpdateProperty 'JourneyPlannerInput.TheMallNaptan','8100MAL', @AID, @GID, @PartnerID, @ThemeID

-- Screenflow
EXEC AddUpdateProperty 'ScreenFlow.PageTimeout.Path','D:\inetpub\wwwroot\Mobile\PageTimeout.xml', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ScreenFlow.PageTimeout.Xsd','D:\inetpub\wwwroot\Mobile\PageTimeout.xsd', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ScreenFlow.PageTimeout.Xml.Node.Root','PageTimeout', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ScreenFlow.PageTimeout.Xml.Node.Page','page', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ScreenFlow.PageTimeout.Xml.Attribute.PageId','PageId', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ScreenFlow.PageTimeout.Xml.Attribute.ControlId','ControlId', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ScreenFlow.PageTimeout.Xml.Attribute.ShowTimeout','ShowTimeout', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'ScreenFlow.PageTransfer.Homepage.Host','www.transportdirect.info', @AID, @GID, @PartnerID, @ThemeID

-- AddtionalData
EXEC AddUpdateProperty 'AdditionalData.LookupCachePrefix','AdditionalDataLookup:', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'AdditionalData.LookupCacheTimeoutSeconds','1800', @AID, @GID, @PartnerID, @ThemeID

-- Stop Information
EXEC AddUpdateProperty 'StopInformation.Enabled.Switch','false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'StopInformation.StationBoard.MinutesLateErrorStyle.Minimum','5', @AID, @GID, @PartnerID, @ThemeID

-- Departure Board Service
EXEC AddUpdateProperty 'DepartureBoards.WebService.URL','http://localhost/TDPWebServices/DepartureBoardWebService/DepartureBoards.asmx', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DepartureBoards.WebService.Timeout.Seconds', '60', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DepartureBoards.WebService.MaxReceivedMessageSize','653560', @AID, @GID, @PartnerID, @ThemeID


-- =============================================
-- Data Services data
-- =============================================
-- Cycle Route types - to populate the cycle route types on journey locations page
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.CycleJourneyType.db',	'DefaultDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.CycleJourneyType.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''CycleJourneyType'' AND PartnerId = 0 And ThemeId = 1 ORDER BY SortOrder', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.CycleJourneyType.type','3', @AID, @GID, @PartnerID, @ThemeID

-- Travel News regions
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.NewsRegionDrop.db',	'DefaultDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.NewsRegionDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''NewsRegionDrop'' AND PartnerId = 0 And ThemeId = 1 ORDER BY SortOrder', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.NewsRegionDrop.type','3', @AID, @GID, @PartnerID, @ThemeID

-- NPTG Countries
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.CountryDrop.db',	'DefaultDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.CountryDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''CountryDrop'' AND PartnerId = 0 And ThemeId = 1 ORDER BY SortOrder', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.CountryDrop.type','3', @AID, @GID, @PartnerID, @ThemeID

-- Travel News mode
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.NewsViewMode.db',	'DefaultDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.NewsViewMode.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''NewsViewMode'' AND PartnerId = 0 And ThemeId = 1 ORDER BY SortOrder', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.NewsViewMode.type','3', @AID, @GID, @PartnerID, @ThemeID


-- =============================================
-- Debug information switch
-- =============================================
EXEC AddUpdateProperty 'Debug.Information.Show.Switch','false', @AID, @GID, @PartnerID, @ThemeID


GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'TDPWeb and TDPMobile shared properties data'