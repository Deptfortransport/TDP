-- Location Information import test Set up
use [TransientPortal]
GO

-- Delete Temp table so we start cleanly
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ExternalLinks') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[temp_ExternalLinks]
GO


-- Copy External links we want
BEGIN
	SELECT * INTO 
		[dbo].[temp_ExternalLinks]
	FROM 
		[ExternalLinks]
	WHERE 
		[id] IN (SELECT ExternalLinkID FROM [LocationInformation])
END



-- Delete temp table so we start cleanly
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_LocationInformation') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[temp_LocationInformation]
GO

--Create and copy data into temporary Location table
SELECT * INTO [dbo].[temp_LocationInformation] FROM [LocationInformation]
GO


-- Delete the Location information table data
BEGIN
	DELETE  FROM [LocationInformation]
END
GO


-- Delete the External links from External Links table
BEGIN
	DELETE  FROM [ExternalLinks]
	WHERE [id] IN (SELECT [ID] FROM [temp_ExternalLinks])
END
GO

