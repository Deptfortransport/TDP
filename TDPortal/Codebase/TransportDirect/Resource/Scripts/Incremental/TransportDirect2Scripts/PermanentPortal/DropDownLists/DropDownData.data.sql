-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : DropDownLists data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

------------------------------------------------
-- Clear data
------------------------------------------------
DELETE FROM [dbo].[DropDownLists]
WHERE [DataSet] in ('NewsRegionDrop', 'CountryDrop', 'NewsViewMode')

------------------------------------------------
-- Travel News Regions dropdown
------------------------------------------------
-- IF order is changed, then ensure Properties for UKRegionImageMap are updated to reflect order
INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('NewsRegionDrop','All','All',0,1)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('NewsRegionDrop','London','London',1,2)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('NewsRegionDrop','EastAnglia','East Anglia',0,3)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('NewsRegionDrop','EastMidlands','East Midlands',0,4)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('NewsRegionDrop','SouthEast','South East',0,5)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('NewsRegionDrop','SouthWest','South West',0,6)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('NewsRegionDrop','WestMidlands','West Midlands',0,7)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('NewsRegionDrop','YorkshireandHumber','Yorkshire and Humber',0,8)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('NewsRegionDrop','NorthEast','North East',0,9)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('NewsRegionDrop','NorthWest','North West',0,10)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('NewsRegionDrop','Scotland','Scotland',0,11)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('NewsRegionDrop','Wales','Wales',0,12)


------------------------------------------------
-- Country dropdown
------------------------------------------------
INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('CountryDrop','Default','',1,1)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('CountryDrop','England','Eng',0,2)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('CountryDrop','Scotland','Sco',0,3)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('CountryDrop','Wales','Wal',0,4)


------------------------------------------------
-- Travel News view mode dropdown
------------------------------------------------
INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('NewsViewMode','All','All',1,1)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('NewsViewMode','LondonUnderground','LUL',0,2)

INSERT INTO [dbo].[DropDownLists]
           ([DataSet],[ResourceID],[ItemValue],[IsSelected],[SortOrder])
     VALUES
           ('NewsViewMode','Venue','Venue',0,3)



-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'DropDownLists data'