--Zonal Stop Set up
-- Delete data in ZonalStop and External link tables 
use TransientPortal
GO

if exists (select * from dbo.sysobjects where id = object_id(N'dbo.temp_ExternalLinks') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE dbo.temp_ExternalLinks
GO

BEGIN
	SELECT * INTO 
		dbo.temp_ExternalLinks 
	FROM 
		ExternalLinks 
	WHERE 
		[id] IN (SELECT ExternalLinkID FROM ZonalStop) 
END

if exists (select * from dbo.sysobjects where id = object_id(N'dbo.temp_ZonalStop') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table dbo.temp_ZonalStop
GO

--Create temporary table
SELECT * INTO dbo.temp_ZonalStop FROM ZonalStop
go

