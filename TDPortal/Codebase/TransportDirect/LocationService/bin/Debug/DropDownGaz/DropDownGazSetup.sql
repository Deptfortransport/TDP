-- DropDownGaz test Set up

-- SQL moves all existing DropDownGaz data into temp tables
-- and then deletes all data from the existing tables

USE [AtosAdditionalData]


BEGIN
-- Delete Temp table so we start cleanly
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_DropDownSyncStatus') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_DropDownSyncStatus]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_DropDownStop') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_DropDownStop]
	
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_StopAlias') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_StopAlias]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_NPTG_RailExchanges') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_NPTG_RailExchanges]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_NPTG_ExchangeGroups') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_NPTG_ExchangeGroups]
		
END


-- Copy DropDownGaz data in to new temp tables
BEGIN
	SELECT * INTO [dbo].[temp_DropDownSyncStatus] FROM [DropDownSyncStatus]
	SELECT * INTO [dbo].[temp_DropDownStop] FROM [DropDownStop]
	SELECT * INTO [dbo].[temp_StopAlias] FROM [StopAlias]
	SELECT * INTO [dbo].[temp_NPTG_RailExchanges] FROM [NPTG_RailExchanges]
	SELECT * INTO [dbo].[temp_NPTG_ExchangeGroups] FROM [NPTG_ExchangeGroups]
END



-- Delete the existing DropDownGaz
BEGIN
	TRUNCATE TABLE [DropDownSyncStatus]
	TRUNCATE TABLE [DropDownStop]
	TRUNCATE TABLE [StopAlias]
	TRUNCATE TABLE [NPTG_RailExchanges]
	TRUNCATE TABLE [NPTG_ExchangeGroups]
END




	