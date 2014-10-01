--International Planner Clean up

-- SQL removes all the temporary data from the InternationalPlanner tables 
-- and then moves the actual data back in from the temporary tables

USE [InternationalData]

if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_CountryJourney') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.CountryJourney') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_Country') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.Country') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_InterchangeTime') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.InterchangeTime') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_StopDistances') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.StopDistances') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_InternationalCityStop') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.InternationalCityStop') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_InternationalCity') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.InternationalCity') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_InternationalStop') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.InternationalStop') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ScheduleAir') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.ScheduleAir') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ScheduleCoachLeg') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.ScheduleCoachLeg') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ScheduleCoach') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.ScheduleCoach') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ScheduleRailLeg') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.ScheduleRailLeg') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ScheduleRail') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.ScheduleRail') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_ScheduleCar') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.ScheduleCar') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_DataVersion') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.DataVersion') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	BEGIN
		-- delete test data added to International tables
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
		
		-- insert data in International tables
		INSERT INTO [CountryJourney] SELECT * FROM [temp_CountryJourney]
		INSERT INTO [Country] SELECT * FROM [temp_Country]
		INSERT INTO [InterchangeTime] SELECT * FROM [temp_InterchangeTime] 
		INSERT INTO [InternationalCity](CityName, CityCountryCode, CityURL, CityOSGREasting, CityOSGRNorthing) 
			SELECT CityName, CityCountryCode, CityURL, CityOSGREasting, CityOSGRNorthing FROM [temp_InternationalCity] 
		
		INSERT INTO [InternationalStop] SELECT * FROM [temp_InternationalStop] 
		INSERT INTO [InternationalCityStop] SELECT * FROM [temp_InternationalCityStop] 
		INSERT INTO [StopDistances] SELECT * FROM [temp_StopDistances]
		
		INSERT INTO [DataVersion] SELECT * FROM [temp_DataVersion]
		INSERT INTO [ScheduleAir](CarrierCode, FlightNumber, DepartureAirportStopCode, ArrivalAirportStopCode, DepartureTime, ArrivalTime, ArrivalDay, DaysOfOperation, AircraftTypeCode, EffectiveFromDate, EffectiveToDate, TerminalNumberFrom, TerminalNumberTo, VersionId, DataType )
			SELECT CarrierCode, FlightNumber, DepartureAirportStopCode, ArrivalAirportStopCode, DepartureTime, ArrivalTime, ArrivalDay, DaysOfOperation, AircraftTypeCode, EffectiveFromDate, EffectiveToDate, TerminalNumberFrom, TerminalNumberTo, VersionId, DataType FROM [temp_ScheduleAir]
		
		INSERT INTO [ScheduleCoachLeg] SELECT * FROM [temp_ScheduleCoachLeg] 
		INSERT INTO [ScheduleCoach](Id, OperatorCode, CoachNumber, DaysOfOperation, ServiceFacilities, EffectiveFromDate, EffectiveToDate, Notes, VersionId, DataType) 
			SELECT Id, OperatorCode, CoachNumber, DaysOfOperation, ServiceFacilities, EffectiveFromDate, EffectiveToDate, Notes, VersionId, DataType FROM [temp_ScheduleCoach] 
		
		INSERT INTO [ScheduleRailLeg] SELECT * FROM [temp_ScheduleRailLeg] 
		INSERT INTO [ScheduleRail] ( Id, OperatorCode, TrainNumber, DaysOfOperation, ServiceFacilities, EffectiveFromDate, EffectiveToDate, VersionId, DataType) 
			SELECT Id, OperatorCode, TrainNumber, DaysOfOperation, ServiceFacilities, EffectiveFromDate, EffectiveToDate, VersionId, DataType FROM [temp_ScheduleRail] 
		
		INSERT INTO [ScheduleCar] SELECT * FROM [temp_ScheduleCar] 
		
	
		-- and delete the temp tables
		DROP TABLE [temp_CountryJourney]
		DROP TABLE [temp_Country]
		DROP TABLE [temp_InterchangeTime] 
		DROP TABLE [temp_StopDistances]
		DROP TABLE [temp_InternationalCityStop] 
		DROP TABLE [temp_ScheduleCar]
		DROP TABLE [temp_InternationalCity] 
		DROP TABLE [temp_InternationalStop] 
		DROP TABLE [temp_ScheduleAir] 
		DROP TABLE [temp_ScheduleCoachLeg] 
		DROP TABLE [temp_ScheduleCoach] 
		DROP TABLE [temp_ScheduleRailLeg] 
		DROP TABLE [temp_ScheduleRail]  
		DROP TABLE [temp_DataVersion]  
	END

