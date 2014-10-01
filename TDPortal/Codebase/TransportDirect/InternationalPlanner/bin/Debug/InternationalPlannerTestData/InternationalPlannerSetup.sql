-- International Planner test Set up

-- SQL moves all existing InternationalPlanner data into temp tables
-- and then deletes all data from the existing tables

USE [InternationalData]


BEGIN
-- Delete Temp table so we start cleanly
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_CountryJourney') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_CountryJourney]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_Country') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_Country]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_InterchangeTime') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_InterchangeTime]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_StopDistances') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_StopDistances]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_InternationalCityStop') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_InternationalCityStop]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_InternationalCity') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_InternationalCity]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_InternationalStop') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_InternationalStop]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ScheduleAir') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_ScheduleAir]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ScheduleCoachLeg') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_ScheduleCoachLeg]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ScheduleCoach') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_ScheduleCoach]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ScheduleRailLeg') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_ScheduleRailLeg]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ScheduleRail') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_ScheduleRail]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ScheduleCar') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_ScheduleCar]
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_DataVersion') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_DataVersion]
		
END


-- Copy International data in to new temp tables
BEGIN
	SELECT * INTO [dbo].[temp_CountryJourney] FROM [CountryJourney]
	SELECT * INTO [dbo].[temp_Country] FROM [Country]
	SELECT * INTO [dbo].[temp_InterchangeTime] FROM [InterchangeTime]
	SELECT * INTO [dbo].[temp_StopDistances] FROM [StopDistances]
	SELECT * INTO [dbo].[temp_InternationalCityStop] FROM [InternationalCityStop]
	SELECT * INTO [dbo].[temp_ScheduleCar] FROM [ScheduleCar]
	SELECT * INTO [dbo].[temp_InternationalCity] FROM [InternationalCity]
	SELECT * INTO [dbo].[temp_InternationalStop] FROM [InternationalStop]
	SELECT * INTO [dbo].[temp_DataVersion] FROM [DataVersion]
	SELECT * INTO [dbo].[temp_ScheduleAir] FROM [ScheduleAir]
	SELECT * INTO [dbo].[temp_ScheduleCoachLeg] FROM [ScheduleCoachLeg]
	SELECT * INTO [dbo].[temp_ScheduleCoach] FROM [ScheduleCoach]
	SELECT * INTO [dbo].[temp_ScheduleRailLeg] FROM [ScheduleRailLeg]
	SELECT * INTO [dbo].[temp_ScheduleRail] FROM [ScheduleRail]
	
END



-- Delete the existing International data
BEGIN
	TRUNCATE TABLE [CountryJourney]
	TRUNCATE TABLE [Country]
	TRUNCATE TABLE [InterchangeTime]
	TRUNCATE TABLE [StopDistances]
	TRUNCATE TABLE [InternationalCityStop]
	TRUNCATE TABLE [ScheduleCar]
	TRUNCATE TABLE [InternationalCity]
	TRUNCATE TABLE [InternationalStop]
	TRUNCATE TABLE [ScheduleAir]
	TRUNCATE TABLE [ScheduleCoachLeg]
	TRUNCATE TABLE [ScheduleCoach]
	TRUNCATE TABLE [ScheduleRailLeg]
	TRUNCATE TABLE [ScheduleRail]
	DELETE FROM [DataVersion]
END




	