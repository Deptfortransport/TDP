--Location Information Clean up
--copy items back to ZonalStop table from temp table.
use [TransientPortal]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'dbo.temp_LocationInformation') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from dbo.sysobjects where id = object_id(N'dbo.LocationInformation') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from dbo.sysobjects where id = object_id(N'dbo.temp_ExternalLinks') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from dbo.sysobjects where id = object_id(N'dbo.ExternalLinks') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	BEGIN
		-- delete external links data
		DELETE  FROM [ExternalLinks]
		WHERE [id] IN (SELECT ExternalLinkID FROM [LocationInformation])

		-- delete location information data
		DELETE  FROM [LocationInformation]

		-- insert data in External links
		INSERT INTO ExternalLinks([Id], URL, TestURL, Valid, [Description], StartDate, EndDate, LinkText)
			SELECT [Id], URL, TestURL, Valid, [Description], StartDate, EndDate, LinkText 
			FROM [temp_ExternalLinks] WHERE [Id] NOT IN (SELECT [Id] FROM [ExternalLinks] )		
		
		-- insert data in location information
		INSERT INTO LocationInformation(Naptan, ExternalLinkID, LinkType)
			SELECT Naptan, ExternalLinkID, LinkType FROM [temp_LocationInformation]
		
		DROP TABLE temp_LocationInformation
		DROP TABLE temp_ExternalLinks
	END
GO
