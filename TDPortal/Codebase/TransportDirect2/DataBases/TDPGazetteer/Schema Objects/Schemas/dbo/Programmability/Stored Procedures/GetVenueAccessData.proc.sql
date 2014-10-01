CREATE PROCEDURE [dbo].[GetVenueAccessData]
AS
BEGIN
	    SELECT venueAccess.[VenueNaPTAN]
			  ,[VenueName]
			  ,[AccessFrom]
			  ,[AccessTo]
			  ,[AccessDuration]
			  ,venueAccess.[StationNaPTAN]
			  ,[StationName]
			  ,[ToVenue] AS [TransferToVenue]
			  ,[CultureCode] AS [TransferLanguage]
			  ,[TransferDescription] AS [TransferText]
		  FROM [dbo].[TDPVenueAccessData] venueAccess
     LEFT JOIN [dbo].[TDPVenueAccessTransfers] venueTransfer
		ON venueAccess.[StationNaPTAN] = venueTransfer.[StationNaPTAN]
END
