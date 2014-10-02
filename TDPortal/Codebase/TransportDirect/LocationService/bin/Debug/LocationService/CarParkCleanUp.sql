USE TransientPortal
GO
-- Setup script for Test Car Park Import
--restore data that was deleted as part of the test process

--externallinks
INSERT INTO ExternalLinks([Id], URL, TestURL, Valid, [Description], StartDate, EndDate, LinkText)
SELECT [Id], URL, TestURL, Valid, [Description], StartDate, EndDate, LinkText FROM temp_ExternalLinks

--car park
SET IDENTITY_INSERT CarPark ON

INSERT INTO CarPark(CarParkId, CarParkRef, CarParkName, ExternalLinksId, MinimumCost, Comments, Easting, Northing)
SELECT CarParkId, CarParkRef, CarParkName, ExternalLinksId, MinimumCost, Comments, Easting, Northing FROM temp_CarPark

SET IDENTITY_INSERT CarPark OFF

--park and ride
SET IDENTITY_INSERT ParkAndRide	ON

INSERT INTO ParkAndRide([Id], RegionId, Location, URLKey, Comments, Easting, Northing)
SELECT [Id], RegionId, Location, URLKey, Comments, Easting, Northing FROM temp_ParkAndRide

SET IDENTITY_INSERT ParkAndRide	OFF

--parkandridecarpark
SET IDENTITY_INSERT ParkAndRideCarPark	ON

INSERT INTO ParkAndRideCarPark(ParkAndRideCarParkID, ParkAndRideId, CarParkId)
SELECT ParkAndRideCarParkID, ParkAndRideId, CarParkId FROM temp_ParkAndRideCarPark

SET IDENTITY_INSERT ParkAndRideCarPark OFF

