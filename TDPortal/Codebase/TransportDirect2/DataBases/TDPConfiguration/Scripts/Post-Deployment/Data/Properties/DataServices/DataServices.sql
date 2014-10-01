-- =============================================
-- Script Template
-- =============================================

USE TDPConfiguration
GO

------------------------------------------------
-- 'DataServices' properties
------------------------------------------------

DECLARE @AID varchar(50) = 'DataServices'
DECLARE @GID varchar(50) = 'UserPortal'

-- Event dates - to populate the calendars
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.SJPEventDates.db',	'TransientPortalDB', @AID, @GID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.SJPEventDates.query','SELECT eventdate FROM SJPEventDates', @AID, @GID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.SJPEventDates.type','2', @AID, @GID

-- Cycle Route types - to populate the cycle route types on journey locations page
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.CycleRouteType.db',	'TransientPortalDB', @AID, @GID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.CycleRouteType.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''CycleRouteType'' ORDER BY SortOrder', @AID, @GID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.CycleRouteType.type','3', @AID, @GID

-- Travel News regions
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.NewsRegionDrop.db',	'TransientPortalDB', @AID, @GID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.NewsRegionDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''NewsRegionDrop'' ORDER BY SortOrder', @AID, @GID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.NewsRegionDrop.type','3', @AID, @GID

-- NPTG Countries
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.CountryDrop.db',	'TransientPortalDB', @AID, @GID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.CountryDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''CountryDrop'' ORDER BY SortOrder', @AID, @GID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.CountryDrop.type','3', @AID, @GID

-- Travel News mode
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.NewsViewMode.db',	'TransientPortalDB', @AID, @GID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.NewsViewMode.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''NewsViewMode'' ORDER BY SortOrder', @AID, @GID
EXEC AddUpdateProperty 'TDP.UserPortal.DataServices.NewsViewMode.type','3', @AID, @GID

GO