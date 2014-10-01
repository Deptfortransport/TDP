--CJPConfigData Clean up

-- SQL removes all the temporary data from the CJPConfigData tables
-- and then moves the actual data back in from the temporary tables

USE [TransientPortal]

if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_CJPConfigData') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	BEGIN
		-- delete test data added to DropDownGaz tables
		TRUNCATE TABLE [CJPConfigData]
		
		-- insert data in DropDownGaz tables
		INSERT INTO [CJPConfigData] SELECT * FROM [temp_CJPConfigData]
		
		-- and delete the temp tables
		DROP TABLE [temp_CJPConfigData]
		
	END

