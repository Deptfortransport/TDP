USE TransientPortal
GO

DECLARE @ParkAndRideID int, @URLKey varchar(500)
DECLARE @Value int, @ValueText varchar(2)
--delete all data in car Parks
DELETE FROM CarPark

SET @Value = 1

DECLARE ImportCursor CURSOR FOR
	SELECT TOP 10 [ID] AS ParkAndRideID, URLKey FROM ParkAndRide

OPEN ImportCursor

FETCH NEXT FROM ImportCursor INTO @ParkAndRideID, @URLKey 
WHILE @@FETCH_STATUS = 0
BEGIN
	SET @ValueText = CONVERT(varchar, @Value)
	INSERT INTO CarPark(ParkAndRideId, CarParkName, ExternalLinksId, MinimumCost, Comments, 
			Easting, Northing)
	VALUES(
		@ParkAndRideID, 
		'Name' + @ValueText,
		@URLKey,
		@Value,
		'Comments: ' + @ValueText,
		@Value,
		@Value
		)
   	FETCH NEXT FROM ImportCursor INTO @ParkAndRideID, @URLKey 
	SET @Value = @Value + 1
END
CLOSE ImportCursor
DEALLOCATE ImportCursor
