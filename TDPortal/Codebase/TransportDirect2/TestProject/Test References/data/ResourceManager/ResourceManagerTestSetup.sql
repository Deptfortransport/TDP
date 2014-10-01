USE [Content]




	-- Delete Temp table so we start cleanly
BEGIN
	if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_Content') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		DROP TABLE [dbo].[temp_Content]


	if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ContentGroup') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		DROP TABLE [dbo].[temp_ContentGroup]


	if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ContentOverride') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		DROP TABLE [dbo].[temp_ContentOverride]
END



-- Copy data in to new temp tables
BEGIN
	SELECT * INTO [dbo].[temp_Content] FROM [tblContent]
	SELECT * INTO [dbo].[temp_ContentGroup] FROM [tblGroup]
	SELECT * INTO [dbo].[temp_ContentOverride] FROM [tblContentOverride]
END



-- Delete the existing data
BEGIN
	DELETE [dbo].[tblContent]
	DELETE [dbo].[tblContentOverride]
	DELETE [dbo].[tblGroup]
	
	DBCC CHECKIDENT('tblContent', RESEED, 0)
	DBCC CHECKIDENT('tblContentOverride', RESEED, 0)
	--DBCC CHECKIDENT('tblGroup', RESEED, 0)
END





