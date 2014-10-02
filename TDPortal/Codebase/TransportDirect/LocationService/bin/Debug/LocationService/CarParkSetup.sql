USE TransientPortal
GO
-- Setup script for Test Car Park Import
--copy data into temp tables

--ParkAndRideCarPark
if exists (select * from dbo.sysobjects where id = object_id(N'dbo.temp_ParkAndRideCarPark') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE dbo.temp_ParkAndRideCarPark
GO
SELECT * INTO dbo.temp_ParkAndRideCarPark FROM ParkAndRideCarPark
GO

--CarPark
if exists (select * from dbo.sysobjects where id = object_id(N'dbo.temp_CarPark') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE dbo.temp_CarPark
GO
SELECT * INTO dbo.temp_CarPark FROM CarPark
GO

--ExternalLinks
if exists (select * from dbo.sysobjects where id = object_id(N'dbo.temp_ExternalLinks') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE dbo.temp_ExternalLinks
GO

SELECT * INTO dbo.temp_ExternalLinks FROM ExternalLinks WHERE (Id IN (SELECT ExternalLinksId FROM CarPark UNION SELECT URLKey FROM ParkAndRide))
GO

--ParkAndRide
if exists (select * from dbo.sysobjects where id = object_id(N'dbo.temp_ParkAndRide') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE dbo.temp_ParkAndRide
GO
SELECT * INTO dbo.temp_ParkAndRide FROM ParkAndRide
GO

--now delete data from tables
DELETE FROM ParkAndRideCarPark
DELETE FROM CarPark 
DELETE FROM ParkAndRide
DELETE FROM ExternalLinks WHERE ID IN (SELECT ExternalLinksId FROM temp_CarPark UNION SELECT URLKey FROM temp_ParkAndRide)



