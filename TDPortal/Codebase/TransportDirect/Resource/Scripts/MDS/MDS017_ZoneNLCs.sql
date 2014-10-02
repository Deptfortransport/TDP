-- ***********************************************
-- AUTHOR      	: Mitesh Modi
-- NAME 	: MDS017_ZoneNLCs.sql
-- DESCRIPTION 	: Updates the list of Zone NLCs used by the fares (ZPBO) when dropping duplicate Zone fares
-- SOURCE 	: Manual Data Service
-- Version	: $Revision:   1.1  $
-- Additional Steps Required : IIS Reset Webservers
-- ************************************************
--$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS017_ZoneNLCs.sql-arc  $
--
--   Rev 1.1   Jan 30 2009 10:53:16   mmodi
--Updated with ZPBO related data from MDS
--Resolution for 5210: CCN487 - ZPBO Implementation workstream
--
--   Rev 1.0   Jan 12 2009 09:10:40   mmodi
--Initial revision.
--Resolution for 5210: CCN487 - ZPBO Implementation workstream
--

USE [PermanentPortal]
GO

-- **********************************************************************
-- PROCEDURE INFAddUpdateZoneNLCs
-- **********************************************************************
DECLARE @ObjectName AS varchar(128)
SET @ObjectName = N'INFAddUpdateZoneNLCs'
IF NOT EXISTS(SELECT 1 
                FROM INFORMATION_SCHEMA.ROUTINES
               WHERE ROUTINE_NAME = @ObjectName
                 AND ROUTINE_TYPE = N'PROCEDURE')
BEGIN
    EXEC ('CREATE PROCEDURE [dbo].INFAddUpdateZoneNLCs AS BEGIN SELECT 1 END')

    EXEC sp_addextendedproperty @name=N'Design', @value=N'SD00?? ?????' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Owner', @value=N'TDP' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Used By', @value=N'TDP.Infrastructure' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName

END

GO

-- **********************************************************************
-- PROCEDURE INFAddUpdateZoneNLCs
-- Description: 
-- 
-- **********************************************************************
ALTER PROCEDURE dbo.INFAddUpdateZoneNLCs
(
    @DataSet      	varchar(200),
    @NLC      		varchar(10),
    @NLCDescription	varchar(200),
    @ZoneGroup	    varchar(50)
) 
AS
BEGIN
    SET NOCOUNT ON
    IF EXISTS(SELECT 1
                FROM [dbo].[ZoneNLC]
               WHERE DataSet = @DataSet
				 AND NLC = @NLC)
        BEGIN
          UPDATE [dbo].[ZoneNLC]
             SET NLCDescription = @NLCDescription,
                 ZoneGroup  = @ZoneGroup
           WHERE DataSet   = @DataSet
			 AND NLC = @NLC
           PRINT 'Updated INFAddUpdateZoneNLCs NLC ' + @NLC
        END
    ELSE
        BEGIN
            INSERT INTO [dbo].[ZoneNLC]
            (
                DataSet, 
                NLC, 
                NLCDescription,
				ZoneGroup
            )
            VALUES
            (
                @DataSet, 
                @NLC, 
                @NLCDescription,
				@ZoneGroup
            )
            PRINT 'Inserted INFAddUpdateZoneNLCs NLC ' + @NLC
        END
    --END IF    
END
GO

-- Delete all rows
TRUNCATE TABLE [dbo].[ZoneNLC]
GO

-- Insert new rows
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '1072', 'LONDON TERMINALS', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '1072', 'LONDON TERMINALS', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '5121', 'CITY THAMESLINK', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '5112', 'LONDON BLKFRIARS', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '5148', 'LONDON BRIDGE', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '5142', 'LONDON CANNON ST', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '5143', 'LONDON CHARING X', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '1444', 'LONDON EUSTON', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '7490', 'LONDON FENCH ST', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '6121', 'LONDON KINGS X', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '6965', 'LONDON LIVRPL ST', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '1475', 'LONDON MARYLEBNE', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '3087', 'LONDON PADDINGTN', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '1555', 'LONDON ST PANCRS', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '5426', 'LONDON VICTORIA', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '5598', 'LONDON WATERLOO', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '5158', 'LONDON WATRLOO E', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '645', 'MOORGATE UND', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '665', 'OLD STREET UND', 'LondonTerminal'
EXEC INFAddUpdateZoneNLCs 'FareTerminalZoneNLC', '5597', 'VAUXHALL', 'LondonTerminal'

EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '785', 'ZONE U1*   LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '790', 'ZONE U12*  LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '791', 'ZONE U123* LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '792', 'ZONE U1234*LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '65', 'ZONE U1245 LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '786', 'ZONE U1256 LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '793', 'ZONE U2*   LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '797', 'ZONE U23*  LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '825', 'ZONE U234* LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '66', 'ZONE U2345 LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '829', 'ZONE U2356 LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '830', 'ZONE U3*   LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '835', 'ZONE U34*  LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '67', 'ZONE U345* LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '839', 'ZONE U3456 LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '841', 'ZONE U4*   LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '68', 'ZONE U45*  LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '844', 'ZONE U456  LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '69', 'ZONE U5*   LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '847', 'ZONE U56   LONDN', 'LondonUnderground'
EXEC INFAddUpdateZoneNLCs 'FareUndergroundZoneNLC', '70', 'ZONE U6*   LONDN', 'LondonUnderground'

EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '51', 'ZONE R1*   ZONE', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '32', 'ZONE R12*  ZONES', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '33', 'ZONE R123* ZONES', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '34', 'ZONE R1234*ZONES', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '64', 'ZONE R1245 ZONES', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '35', 'ZONE R1256 ZONES', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '52', 'ZONE R2*   ZONE', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '36', 'ZONE R23*  ZONES', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '37', 'ZONE R234* ZONES', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '63', 'ZONE R2345 ZONES', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '38', 'ZONE R2356 ZONES', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '53', 'ZONE R3*   ZONE', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '39', 'ZONE R34*  ZONES', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '62', 'ZONE R345* ZONES', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '40', 'ZONE R3456 ZONES', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '54', 'ZONE R4*   ZONE', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '61', 'ZONE R45*  ZONES', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '57', 'ZONE R456* ZONES', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '60', 'ZONE R5*   ZONE', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '55', 'ZONE R56*  ZONES', 'LondonTravelcard'
EXEC INFAddUpdateZoneNLCs 'FareTravelcardZoneNLC', '59', 'ZONE R6*   ZONE', 'LondonTravelcard'

GO


----------------
-- Change Log --
----------------

USE PermanentPortal

Declare @@value decimal(7,3)
Select @@value = left(right('$Revision:   1.1  $',8),7)


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 017 and VersionNumber = @@value)
BEGIN
	UPDATE dbo.MDSChangeCatalogue
	SET
		ChangeDate = getdate(),
		Summary = 'Updated with latest MDS data'
	WHERE ScriptNumber = 017 AND VersionNumber = @@value
END
ELSE
BEGIN
	INSERT INTO dbo.MDSChangeCatalogue
	(
		ScriptNumber,
		VersionNumber,
		Summary
	)
	VALUES
	(
		017,
		@@value,
		'Upadted with latest MDS data'
	)
END