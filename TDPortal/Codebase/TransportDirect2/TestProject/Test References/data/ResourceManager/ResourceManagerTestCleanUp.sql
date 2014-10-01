--SQLHelperTest Clean up

if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ContentOverride') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	BEGIN
		-- delete test data added to ContentOverride table
		DELETE [tblContentOverride]
				
		DBCC CHECKIDENT('tblContentOverride', RESEED, 0)
	END

if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_Content') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	BEGIN
		-- delete test data added to Content table
		DELETE [tblContent]
		
		DBCC CHECKIDENT('tblContent', RESEED, 0)		
	END

if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ContentGroup') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	BEGIN
		-- delete test data added to ContentGroup table
		DELETE [tblGroup]
		
		--DBCC CHECKIDENT('tblGroup', RESEED, 0)		
	END

if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ContentGroup') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	BEGIN
		-- insert data in ContentGroup table
		INSERT INTO [tblGroup]
				([GroupId]
				,[Name])
			SELECT *
			FROM dbo.[temp_ContentGroup]

		-- and delete the temp tables
		DROP TABLE [temp_ContentGroup]
		
	END

if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_Content') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	BEGIN
		-- insert data in Content table
		INSERT INTO [tblContent]
				   ([ThemeId]
				   ,[GroupId]
				   ,[ControlName]
				   ,[PropertyName]
				   ,[Value-En]
				   ,[Value-Cy])
			SELECT  [ThemeId]
				   ,[GroupId]
				   ,[ControlName]
				   ,[PropertyName]
				   ,[Value-En]
				   ,[Value-Cy]
			FROM dbo.[temp_Content]

		
		-- and delete the temp tables
		DROP TABLE [temp_Content]
		
	END

if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ContentOverride') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	BEGIN
		-- insert data in ContentOverride table
		INSERT INTO [tblContentOverride]
				   ([ThemeId]
				   ,[GroupId]
				   ,[ControlName]
				   ,[PropertyName]
				   ,[Value-En]
				   ,[Value-Cy]
				   ,[StartDate]
				   ,[EndDate])
			SELECT  [ThemeId]
				   ,[GroupId]
				   ,[ControlName]
				   ,[PropertyName]
				   ,[Value-En]
				   ,[Value-Cy]
				   ,[StartDate]
				   ,[EndDate]
			FROM dbo.[temp_ContentOverride]

		
		-- and delete the temp tables
		DROP TABLE [temp_ContentOverride]
		
	END

