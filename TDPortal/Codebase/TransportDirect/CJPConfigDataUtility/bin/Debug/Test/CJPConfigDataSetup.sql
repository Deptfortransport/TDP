-- CJPConfigData test Set up

-- SQL moves all existing CJP config property data into temp tables
-- and then deletes all data from the existing tables

USE [TransientPortal]


BEGIN
-- Delete Temp table so we start cleanly
if exists (select * from [dbo].[sysobjects] where id = object_id(N'dbo.temp_CJPConfigData') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_CJPConfigData]

END


-- Copy DropDownGaz data in to new temp tables
BEGIN
	SELECT * INTO [dbo].[temp_CJPConfigData] FROM [CJPConfigData]
END



-- Delete the existing DropDownGaz
BEGIN
	TRUNCATE TABLE [CJPConfigData]
	
END




	