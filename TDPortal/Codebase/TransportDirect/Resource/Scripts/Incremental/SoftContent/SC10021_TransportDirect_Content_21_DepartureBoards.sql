-- ***********************************************
-- NAME 		: SC10021_TransportDirect_Content_21_DepartureBoards.sql
-- DESCRIPTION 	: Script to add Departure Boards content
-- AUTHOR		: XXXX
-- DATE			: 29 Aug 2013
-- ************************************************

-- ***********************************************
-- PLEASE INCLUDE THIS SCRIPT WITH ALL DEPARTURE BOARD GROUPS AS THEY ARE UPDATED
-- ***********************************************

-- ***********************************************
-- ExternalLinks table must also be updated (incremental script) 
-- if the departure board links are updated
-- ***********************************************

USE [Content]
GO

DECLARE @GroupId int
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'General')

-----------------------------------------------------------------------------------------
-- Bus Departure Boards
-----------------------------------------------------------------------------------------
EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.1.Title',
'NextBuses',
'NextBuses'
EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.1.Description',
'Bus stop departures',
'Bus stop departures'

EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.2.Title', '', ''
EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.2.Description', '', ''
EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.3.Title', '', ''
EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.3.Description', '', ''
EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.4.Title', '', ''
EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.4.Description', '', ''
EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.5.Title', '', ''
EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.5.Description', '', ''

-- Old links, retained for reference:

--EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.1.Title',
--'Transport for Lancashire',
--'Transport for Lancashire'
--EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.1.Description',
--'Live bus stop departures for routes in:     <ul><li>Preston </li></ul>',
--'Ymadawiadau arosfannau bws bwy ar gyfer llwybrau yn:     <ul><li>Preston </li></ul>'

--EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.2.Title',
--'Acis Live',
--'Acis Live'
--EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.2.Description',
--'Live bus stop departures and current bus locations for routes in:     <ul><li>Bristol; </li>     <li>Cardiff; </li>     <li>Gwynedd &amp; Conwy; </li>     <li>Gloucestershire; </li>     <li>Nottingham; and </li>     <li>Surrey </li></ul>',
--'Ymadawiadau arosfannau bws bwy ar gyfer llwybrau yn:     <ul><li>Bryste; </li>     <li>Caerdydd; </li>     <li>Gwynedd a Conwy; </li>     <li>Gloucester; </li>     <li>Nottingham; a </li>     <li>Surrey </li></ul>'

--EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.3.Title',
--'Star Trak',
--'Star Trak'
--EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.3.Description',
--'Live bus stop departures plus details of live SMS updates for routes in:     <ul><li>Leicester; </li>     <li>Leicestershire; and </li>     <li>Loughborough </li></ul>',
--'Ymadawiadau arosfannau bws byw a manylion am ddiweddariadau SMS byw ar gyfer llwybrau yn:     <ul><li>Caerl r; </li>     <li>Swydd Caerl r; a </li>     <li>Loughborough </li></ul>'

--EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.4.Title',
--'Journey On',
--'Journey On'
--EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.4.Description',
--'Live bus stop departures for routes in:     <ul><li>Brighton and Hove</li></ul>',
--'Ymadawiadau arosfannau bws bwy ar gyfer llwybrau yn:     <ul><li>Brighton and Hove</li></ul>'

--EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.5.Title',
--'Help2Travel',
--'Help2Travel'
--EXEC AddtblContent 1, @GroupId, 'langStrings', 'DepartureBoard.Bus.5.Description',
--'Live bus stop departures for routes in:     <ul><li>Birmingham</li></ul>',
--'Ymadawiadau arosfannau bws bwy ar gyfer llwybrau yn:     <ul><li>Birmingham</li></ul>'
-----------------------------------------------------------------------------------------

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)
DECLARE @VersionInfo VARCHAR(24)

SET @ScriptNumber = 10021
SET @ScriptDesc = 'Add Departure Boards content'
SET @VersionInfo = '1.0'

IF EXISTS (SELECT * FROM [dbo].[ChangeCatalogue] WHERE ScriptNumber = @ScriptNumber)
  BEGIN
    UPDATE [dbo].[ChangeCatalogue]
    SET ChangeDate = getDate(), Summary = @ScriptDesc, VersionInfo = @VersionInfo
    WHERE ScriptNumber = @ScriptNumber
  END
ELSE
  BEGIN
    INSERT INTO [dbo].[ChangeCatalogue] (ScriptNumber, ChangeDate, Summary, VersionInfo)
    VALUES (@ScriptNumber, getDate(), @ScriptDesc, @VersionInfo)
  END
GO