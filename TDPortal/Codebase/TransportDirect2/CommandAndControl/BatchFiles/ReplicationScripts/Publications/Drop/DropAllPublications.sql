
-- Dropping the transactional articles
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'__RefactorLog', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'__RefactorLog', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'AddChangeNotificationTable', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'AddChangeNotificationTable', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'AdminAreas', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'AdminAreas', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'ChangeNotification', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'ChangeNotification', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'CycleAttribute', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'CycleAttribute', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'Districts', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'Districts', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'DropDownLists', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'DropDownLists', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetChangeTable', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetChangeTable', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetCycleAttributes', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetCycleAttributes', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetNPTGAdminAreas', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetNPTGAdminAreas', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetNPTGDistricts', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetNPTGDistricts', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetRouteEnds', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetRouteEnds', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetRoutes', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetRoutes', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetTravelcards', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetTravelcards', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetStopAccessibilityLinks', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetStopAccessibilityLinks', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetZoneModes', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetZoneModes', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetZones', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetZones', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetZoneStops', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetZoneStops', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'ImportSJPTravelcardData', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'ImportSJPTravelcardData', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'ImportSJPStopAccessibilityLinks', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'ImportSJPStopAccessibilityLinks', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'IsTravelNewsItemActive', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'IsTravelNewsItemActive', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'RouteEndStops', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPRouteEndStops', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPEventDates', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPEventDates', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPRouteEndZones', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPRouteEndZones', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPRouteModes', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPRouteModes', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPRoutes', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPRoutes', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPTravelcardRoutes', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPTravelcardRoutes', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPTravelcards', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPTravelcards', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPTravelcardZones', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPTravelcardZones', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPZoneAreaPoints', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPZoneAreaPoints', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPZoneAreas', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPZoneAreas', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPZoneModes', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPZoneModes', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPZones', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPZones', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPZoneStops', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPZoneStops', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPSplit', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPSplit', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'StopAccessibilityLinks', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'StopAccessibilityLinks', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'StripNaptan', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'StripNaptan', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNews', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNews', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsAll', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsAll', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsDataSource', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsDataSource', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsDataSources', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsDataSources', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsHeadlines', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsHeadlines', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsRegion', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsRegion', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsSeverity', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsSeverity', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsToid', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsToid', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'UpdateChangeNotificationTable', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'UpdateChangeNotificationTable', @force_invalidate_snapshot = 1
GO
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', @article = N'VersionInfo', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPTransientPortal]
exec sp_droparticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'VersionInfo', @force_invalidate_snapshot = 1
GO

-- Dropping the transactional publication
use [SJPTransientPortal]
exec sp_droppublication @publication = N'SJPTransientPortalTransactionalPublication'
GO


-- Disabling the replication database
use master
exec sp_replicationdboption @dbname = N'SJPTransientPortal', @optname = N'publish', @value = N'false'
GO


-- Dropping the transactional articles
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'__RefactorLog', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'__RefactorLog', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'AddChangeNotificationTable', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'AddChangeNotificationTable', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'ChangeNotification', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ChangeNotification', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetAllPeirVenueNavigationPaths', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetAllPeirVenueNavigationPaths', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetAllVenueCarParks', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetAllVenueCarParks', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetAllVenueCycleParks', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetAllVenueCycleParks', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetAllVenueRiverSerivces', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetAllVenueRiverSerivces', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetAlternativeSuggestionList', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetAlternativeSuggestionList', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetChangeTable', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetChangeTable', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetCycleVenueParkAvailability', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetCycleVenueParkAvailability', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetGNATAdminAreas', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetGNATAdminAreas', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetGNATList', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetGNATList', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetGNATStopType', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetGNATStopType', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetGroupLocation', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetGroupLocation', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetGroupNaPTANs', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetGroupNaPTANs', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetLocalityLocation', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetLocalityLocation', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetLocation', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetLocation', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetLocations', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetLocations', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetNaptanLocation', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetNaptanLocation', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetPierToVenueNavigationPaths', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetPierToVenueNavigationPaths', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetPostcodeLocation', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetPostcodeLocation', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetPostcodeLocations', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetPostcodeLocations', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetRiverServices', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetRiverServices', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetUnknownLocation', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetUnknownLocation', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueAccessData', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueAccessData', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueAdditonalData', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueAdditonalData', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueCarParkAvailability', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueCarParkAvailability', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueCarParkTransitShuttles', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueCarParkTransitShuttles', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueCarParkTransitShuttleTransfers', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueCarParkTransitShuttleTransfers', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueCycleParks', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueCycleParks', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueGateCheckConstraints', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueGateCheckConstraints', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueGateMaps', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueGateMaps', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueGateNavigationPaths', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueGateNavigationPaths', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueGates', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueGates', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueLocations', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueLocations', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenuesList', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenuesList', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPAdditionalVenueData', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPAdditionalVenueData', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPCycleParkLocations', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPCycleParkLocations', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPGNATAdminAreasData', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPGNATAdminAreasData', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPGNATLocationsData', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPGNATLocationsData', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPParkAndRideLocations', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPParkAndRideLocations', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPParkAndRideToids', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPParkAndRideToids', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPVenueAccessData', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPVenueAccessData', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPVenueGateData', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPVenueGateData', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParkAvailability', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParkAvailability', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParks', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParks', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParksCarParkAvailability', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParksCarParkAvailability', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParksCarParkTransitShuttles', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParksCarParkTransitShuttles', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParkTransitShuttles', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParkTransitShuttleTransfers', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParkTransitShuttleTransfers', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParkTransitShuttles', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCycleParkAvailability', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCycleParkAvailability', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCycleParks', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCycleParks', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCycleParksCycleParkAvailability', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCycleParksCycleParkAvailability', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPGNATAdminAreas', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPGNATAdminAreas', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPGNATLocations', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPGNATLocations', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPNonPostcodeLocations', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPNonPostcodeLocations', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPPierVenueNavigationPath', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPPierVenueNavigationPath', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPPierVenueNavigationPathTransfers', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPPierVenueNavigationPathTransfers', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPPostcodeLocations', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPPostcodeLocations', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPRiverServices', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPRiverServices', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueAccessData', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueAccessData', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueAccessTransfers', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueAccessTransfers', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueAdditionalData', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueAdditionalData', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueGateCheckConstraints', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueGateCheckConstraints', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueGateMaps', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueGateMaps', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueGateNavigationPaths', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueGateNavigationPaths', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueGates', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueGates', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'UpdateChangeNotificationTable', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'UpdateChangeNotificationTable', @force_invalidate_snapshot = 1
GO
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', @article = N'VersionInfo', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPGazetteer]
exec sp_droparticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'VersionInfo', @force_invalidate_snapshot = 1
GO

-- Dropping the transactional publication
use [SJPGazetteer]
exec sp_droppublication @publication = N'SJPGazetteerTransactionalPublication'
GO


-- Disabling the replication database
use master
exec sp_replicationdboption @dbname = N'SJPGazetteer', @optname = N'publish', @value = N'false'
GO


-- Dropping the transactional articles
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'__RefactorLog', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'__RefactorLog', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'AddChangeNotificationTable', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'AddChangeNotificationTable', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'AddContent', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'AddContent', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'AddContentOverride', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'AddContentOverride', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'AddGroup', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'AddGroup', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'ChangeNotification', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'ChangeNotification', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'Content', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'Content', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'ContentGroup', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'ContentGroup', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'ContentOverride', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'ContentOverride', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'DeleteAllGroupContent', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'DeleteAllGroupContent', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'DeleteContent', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'DeleteContent', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'DeleteContentOverride', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'DeleteContentOverride', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'DeleteGroup', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'DeleteGroup', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'GetChangeTable', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'GetChangeTable', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'GetContent', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'GetContent', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'UpdateChangeNotificationTable', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'UpdateChangeNotificationTable', @force_invalidate_snapshot = 1
GO
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', @article = N'VersionInfo', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPContent]
exec sp_droparticle @publication = N'SJPContentTransactionalPublication', @article = N'VersionInfo', @force_invalidate_snapshot = 1
GO

-- Dropping the transactional publication
use [SJPContent]
exec sp_droppublication @publication = N'SJPContentTransactionalPublication'
GO


-- Disabling the replication database
use master
exec sp_replicationdboption @dbname = N'SJPContent', @optname = N'publish', @value = N'false'
GO


-- Dropping the transactional articles
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'__RefactorLog', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'__RefactorLog', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'AddChangeNotificationTable', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'AddChangeNotificationTable', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'AddUpdateProperty', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'AddUpdateProperty', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'AddUpdateRetailer', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'AddUpdateRetailer', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'AddUpdateRetailerLookup', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'AddUpdateRetailerLookup', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'ChangeNotification', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'ChangeNotification', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'GetChangeTable', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'GetChangeTable', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'GetRetailerLookup', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'GetRetailerLookup', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'GetRetailers', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'GetRetailers', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'GetVersion', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'GetVersion', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'properties', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'properties', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'ReferenceNum', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'ReferenceNum', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'RetailerLookup', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'RetailerLookup', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'Retailers', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'Retailers', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'SelectApplicationProperties', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'SelectApplicationProperties', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'SelectGlobalProperties', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'SelectGlobalProperties', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'SelectGroupProperties', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'SelectGroupProperties', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'UpdateChangeNotificationTable', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'UpdateChangeNotificationTable', @force_invalidate_snapshot = 1
GO
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', @article = N'VersionInfo', @subscriber = N'all', @destination_db = N'all'
GO
use [SJPConfiguration]
exec sp_droparticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'VersionInfo', @force_invalidate_snapshot = 1
GO

-- Dropping the transactional publication
use [SJPConfiguration]
exec sp_droppublication @publication = N'SJPConfigurationTransactionalPublication'
GO


-- Disabling the replication database
use master
exec sp_replicationdboption @dbname = N'SJPConfiguration', @optname = N'publish', @value = N'false'
GO


-- Dropping the snapshot subscriptions
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @subscriber = N'SJP-NLE-WU-01', @destination_db = N'Routing', @article = N'all'
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @subscriber = N'SJP-NLE-WU-02', @destination_db = N'Routing', @article = N'all'
GO

-- Dropping the snapshot articles
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'sp_CCZONE_links', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'sp_CCZONE_links', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'spClearGazetteer', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'spClearGazetteer', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'spClearLimitedAccess', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'spClearLimitedAccess', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'spClearToidIndex', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'spClearToidIndex', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'spInsertLimitedAccess', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'spInsertLimitedAccess', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'spInsertToidType', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'spInsertToidType', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblCCZoneOsgbToid', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblCCZoneOsgbToid', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblCongestionZones', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblCongestionZones', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblFerryCompanies', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryCompanies', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblFerryDepartures', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryDepartures', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblFerryIntermediateLinks', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryIntermediateLinks', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblFerryIntermediateStops', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryIntermediateStops', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblFerryIntermediateStopsGeneral', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryIntermediateStopsGeneral', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblFerryIntermediateStopsSpecific', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryIntermediateStopsSpecific', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblFerryNotes', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryNotes', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblFerryPorts', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryPorts', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblFerryRoutes', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryRoutes', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblGazetteer', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblGazetteer', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblLimitedAccess', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblLimitedAccess', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblToidIndex', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblToidIndex', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblToidTypes', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblToidTypes', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblTollOperatorsData', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblTollOperatorsData', @force_invalidate_snapshot = 1
GO
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', @article = N'tblVersionInfo', @subscriber = N'all', @destination_db = N'all'
GO
use [Routing]
exec sp_droparticle @publication = N'RoutingSnapshotPublication', @article = N'tblVersionInfo', @force_invalidate_snapshot = 1
GO

-- Dropping the snapshot publication
use [Routing]
exec sp_droppublication @publication = N'RoutingSnapshotPublication'
GO


-- Disabling the replication database
use master
exec sp_replicationdboption @dbname = N'Routing', @optname = N'publish', @value = N'false'
GO


-- Dropping the snapshot articles
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'Admin Areas', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'Admin Areas', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'Alternate Names', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'Alternate Names', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'AREPs', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'AREPs', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'Call Centres', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'Call Centres', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'Districts', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'Districts', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'Exchange Points', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'Exchange Points', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'GetAREPs', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'GetAREPs', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'GetCapability', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'GetCapability', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'GetExchangePoints', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'GetExchangePoints', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'GetJWVersion', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'GetJWVersion', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'GetSecondaryRegionURL', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'GetSecondaryRegionURL', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'GetURL', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'GetURL', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'Hierarchy', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'Hierarchy', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'Localities', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'Localities', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'Traveline Regions', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'Traveline Regions', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'Trusted Clients', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'Trusted Clients', @force_invalidate_snapshot = 1
GO
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', @article = N'Unsupported', @subscriber = N'all', @destination_db = N'all'
GO
use [NPTG]
exec sp_droparticle @publication = N'NPTGSnapshotPublication', @article = N'Unsupported', @force_invalidate_snapshot = 1
GO

-- Dropping the snapshot publication
use [NPTG]
exec sp_droppublication @publication = N'NPTGSnapshotPublication'
GO


-- Disabling the replication database
use master
exec sp_replicationdboption @dbname = N'NPTG', @optname = N'publish', @value = N'false'
GO


-- Dropping the snapshot articles
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'AttributeList', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'AttributeList', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'Configuration', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'Configuration', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'Links', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'Links', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'ManoeuvresText', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'ManoeuvresText', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'PathLinks', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'PathLinks', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'QuadTileIndex', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'QuadTileIndex', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'sp_CCZONE_links', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'sp_CCZONE_links', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'spClearGazetteer', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'spClearGazetteer', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'spClearToidIndex', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'spClearToidIndex', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'spInsertToidType', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'spInsertToidType', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblCCZoneOsgbToid', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblCCZoneOsgbToid', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblCongestionZones', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblCongestionZones', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryCompanies', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryCompanies', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryDepartures', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryDepartures', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryIntermediateLinks', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryIntermediateLinks', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryIntermediateStops', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryIntermediateStops', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryIntermediateStopsGeneral', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryIntermediateStopsGeneral', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryIntermediateStopsSpecific', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryIntermediateStopsSpecific', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryNotes', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryNotes', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryPorts', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryPorts', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryRoutes', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblFerryRoutes', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblGazetteer', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblGazetteer', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblToidIndex', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblToidIndex', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblToidTypes', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblToidTypes', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblTollOperatorsData', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblTollOperatorsData', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tblVersionInfo', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tblVersionInfo', @force_invalidate_snapshot = 1
GO
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', @article = N'tempGazetteer', @subscriber = N'all', @destination_db = N'all'
GO
use [CP_Routing]
exec sp_droparticle @publication = N'CP_RoutingSnapshotPublication', @article = N'tempGazetteer', @force_invalidate_snapshot = 1
GO

-- Dropping the snapshot publication
use [CP_Routing]
exec sp_droppublication @publication = N'CP_RoutingSnapshotPublication'
GO


-- Disabling the replication database
use master
exec sp_replicationdboption @dbname = N'CP_Routing', @optname = N'publish', @value = N'false'
GO


-- Dropping the snapshot articles
use [AirInterchange]
exec sp_dropsubscription @publication = N'AirInterchangeSnapshotPublication', @article = N'Accessibility', @subscriber = N'all', @destination_db = N'all'
GO
use [AirInterchange]
exec sp_droparticle @publication = N'AirInterchangeSnapshotPublication', @article = N'Accessibility', @force_invalidate_snapshot = 1
GO
use [AirInterchange]
exec sp_dropsubscription @publication = N'AirInterchangeSnapshotPublication', @article = N'ImportDetails', @subscriber = N'all', @destination_db = N'all'
GO
use [AirInterchange]
exec sp_droparticle @publication = N'AirInterchangeSnapshotPublication', @article = N'ImportDetails', @force_invalidate_snapshot = 1
GO
use [AirInterchange]
exec sp_dropsubscription @publication = N'AirInterchangeSnapshotPublication', @article = N'Interchange', @subscriber = N'all', @destination_db = N'all'
GO
use [AirInterchange]
exec sp_droparticle @publication = N'AirInterchangeSnapshotPublication', @article = N'Interchange', @force_invalidate_snapshot = 1
GO
use [AirInterchange]
exec sp_dropsubscription @publication = N'AirInterchangeSnapshotPublication', @article = N'LegNode', @subscriber = N'all', @destination_db = N'all'
GO
use [AirInterchange]
exec sp_droparticle @publication = N'AirInterchangeSnapshotPublication', @article = N'LegNode', @force_invalidate_snapshot = 1
GO
use [AirInterchange]
exec sp_dropsubscription @publication = N'AirInterchangeSnapshotPublication', @article = N'Link', @subscriber = N'all', @destination_db = N'all'
GO
use [AirInterchange]
exec sp_droparticle @publication = N'AirInterchangeSnapshotPublication', @article = N'Link', @force_invalidate_snapshot = 1
GO
use [AirInterchange]
exec sp_dropsubscription @publication = N'AirInterchangeSnapshotPublication', @article = N'Mapping', @subscriber = N'all', @destination_db = N'all'
GO
use [AirInterchange]
exec sp_droparticle @publication = N'AirInterchangeSnapshotPublication', @article = N'Mapping', @force_invalidate_snapshot = 1
GO
use [AirInterchange]
exec sp_dropsubscription @publication = N'AirInterchangeSnapshotPublication', @article = N'NodeAction', @subscriber = N'all', @destination_db = N'all'
GO
use [AirInterchange]
exec sp_droparticle @publication = N'AirInterchangeSnapshotPublication', @article = N'NodeAction', @force_invalidate_snapshot = 1
GO
use [AirInterchange]
exec sp_dropsubscription @publication = N'AirInterchangeSnapshotPublication', @article = N'Schematics', @subscriber = N'all', @destination_db = N'all'
GO
use [AirInterchange]
exec sp_droparticle @publication = N'AirInterchangeSnapshotPublication', @article = N'Schematics', @force_invalidate_snapshot = 1
GO
use [AirInterchange]
exec sp_dropsubscription @publication = N'AirInterchangeSnapshotPublication', @article = N'spGetInterchangeDetails', @subscriber = N'all', @destination_db = N'all'
GO
use [AirInterchange]
exec sp_droparticle @publication = N'AirInterchangeSnapshotPublication', @article = N'spGetInterchangeDetails', @force_invalidate_snapshot = 1
GO
use [AirInterchange]
exec sp_dropsubscription @publication = N'AirInterchangeSnapshotPublication', @article = N'spGetInterchangeTime', @subscriber = N'all', @destination_db = N'all'
GO
use [AirInterchange]
exec sp_droparticle @publication = N'AirInterchangeSnapshotPublication', @article = N'spGetInterchangeTime', @force_invalidate_snapshot = 1
GO
use [AirInterchange]
exec sp_dropsubscription @publication = N'AirInterchangeSnapshotPublication', @article = N'spGetVersion', @subscriber = N'all', @destination_db = N'all'
GO
use [AirInterchange]
exec sp_droparticle @publication = N'AirInterchangeSnapshotPublication', @article = N'spGetVersion', @force_invalidate_snapshot = 1
GO

-- Dropping the snapshot publication
use [AirInterchange]
exec sp_droppublication @publication = N'AirInterchangeSnapshotPublication'
GO


-- Disabling the replication database
use master
exec sp_replicationdboption @dbname = N'AirInterchange', @optname = N'publish', @value = N'false'
GO

