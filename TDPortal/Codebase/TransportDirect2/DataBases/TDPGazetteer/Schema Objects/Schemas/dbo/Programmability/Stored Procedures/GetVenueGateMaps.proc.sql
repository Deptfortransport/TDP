
CREATE PROCEDURE [dbo].[GetVenueGateMaps]
AS
	BEGIN
		SELECT VenueNaPTAN, VenueGate, VenueGateMapURL
		FROM dbo.TDPVenueGateMaps
	END