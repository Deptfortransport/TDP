--Zonal Stop Clean up
--copy items back to ZonalStop table from temp table.
use TransientPortal
GO

if exists (select * from dbo.sysobjects where id = object_id(N'dbo.temp_ZonalStop') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from dbo.sysobjects where id = object_id(N'dbo.ZonalStop') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from dbo.sysobjects where id = object_id(N'dbo.temp_ExternalLinks') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from dbo.sysobjects where id = object_id(N'dbo.ExternalLinks') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	BEGIN
		--delete zonal stop
		DELETE  FROM ZonalStop

		--insert data in External links
		INSERT INTO ExternalLinks([Id], URL, TestURL, Valid, [Description], StartDate, EndDate, LinkText)
			SELECT [Id], URL, TestURL, Valid, [Description], StartDate, EndDate, LinkText 
			FROM temp_ExternalLinks WHERE [Id] NOT IN (SELECT [Id] FROM ExternalLinks )		
		
		
		
		INSERT INTO ZonalStop(Naptan, ExternalLinkID)
			SELECT Naptan, ExternalLinkID FROM temp_ZonalStop				
		
		DROP TABLE temp_ZonalStop
		DROP TABLE temp_ExternalLinks
	END
GO
