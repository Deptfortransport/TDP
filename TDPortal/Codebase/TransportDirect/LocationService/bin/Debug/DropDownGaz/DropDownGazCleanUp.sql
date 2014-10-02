--DropDownGaz Clean up

-- SQL removes all the temporary data from the DropDownGaz tables 
-- and then moves the actual data back in from the temporary tables

USE [AtosAdditionalData]

if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_DropDownSyncStatus') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_DropDownStop') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_StopAlias') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_NPTG_RailExchanges') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_NPTG_ExchangeGroups') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	BEGIN
		-- delete test data added to DropDownGaz tables
		TRUNCATE TABLE [DropDownSyncStatus]
		TRUNCATE TABLE [DropDownStop]
		TRUNCATE TABLE [StopAlias]
		TRUNCATE TABLE [NPTG_RailExchanges]
		TRUNCATE TABLE [NPTG_ExchangeGroups]
		
		-- insert data in DropDownGaz tables
		INSERT INTO [DropDownSyncStatus] SELECT * FROM [temp_DropDownSyncStatus]
		INSERT INTO [DropDownStop] SELECT * FROM [temp_DropDownStop]
		INSERT INTO [StopAlias] SELECT * FROM [temp_StopAlias]
		INSERT INTO [NPTG_RailExchanges] SELECT * FROM [temp_NPTG_RailExchanges]
		INSERT INTO [NPTG_ExchangeGroups] SELECT * FROM [temp_NPTG_ExchangeGroups]
		
	
		-- and delete the temp tables
		DROP TABLE [temp_DropDownSyncStatus]
		DROP TABLE [temp_DropDownStop]
		DROP TABLE [temp_StopAlias]
		DROP TABLE [temp_NPTG_RailExchanges]
		DROP TABLE [temp_NPTG_ExchangeGroups]
	END

