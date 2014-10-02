-- ***********************************************
-- AUTHOR      	: Mitesh Modi
-- NAME 	: MDS016_TicketTypeGroups.sql
-- DESCRIPTION 	: Updates the list of Ticket types and their groups (primarily used for Travelcards)
-- SOURCE 	: Manual Data Service
-- Version	: $Revision:   1.1  $
-- Additional Steps Required : IIS Reset Webservers
-- ************************************************
--$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS016_TicketTypeGroups.sql-arc  $
--
--   Rev 1.1   Jan 30 2009 10:53:16   mmodi
--Updated with ZPBO related data from MDS
--Resolution for 5210: CCN487 - ZPBO Implementation workstream
--
--   Rev 1.0   Jan 12 2009 09:09:38   mmodi
--Initial revision.
--Resolution for 5210: CCN487 - ZPBO Implementation workstream
--

USE [TransientPortal]
GO

-- **********************************************************************
-- PROCEDURE INFAddUpdateTicketTypeGroups
-- **********************************************************************
DECLARE @ObjectName AS varchar(128)
SET @ObjectName = N'INFAddUpdateTicketTypeGroups'
IF NOT EXISTS(SELECT 1 
                FROM INFORMATION_SCHEMA.ROUTINES
               WHERE ROUTINE_NAME = @ObjectName
                 AND ROUTINE_TYPE = N'PROCEDURE')
BEGIN
    EXEC ('CREATE PROCEDURE [dbo].INFAddUpdateTicketTypeGroups AS BEGIN SELECT 1 END')

    EXEC sp_addextendedproperty @name=N'Design', @value=N'SD00?? ?????' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Owner', @value=N'TDP' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Used By', @value=N'TDP.Infrastructure' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName

END

GO

-- **********************************************************************
-- PROCEDURE INFAddUpdateTicketTypeGroups
-- Description: 
-- 
-- **********************************************************************
ALTER PROCEDURE dbo.INFAddUpdateTicketTypeGroups
(
    @TicketTypeCode      		varchar(10),
    @TicketTypeDescription      varchar(200),
    @TicketTypeGroup	        varchar(50)
) 
AS
BEGIN
    SET NOCOUNT ON
    IF EXISTS(SELECT 1
                FROM [dbo].[TicketTypeGroup]
               WHERE TicketTypeCode = @TicketTypeCode)
        BEGIN
          UPDATE [dbo].[TicketTypeGroup]
             SET TicketTypeDescription = @TicketTypeDescription,
                 TicketTypeGroup  = @TicketTypeGroup
           WHERE TicketTypeCode   = @TicketTypeCode
           PRINT 'Updated INFAddUpdateTicketTypeGroups TicketTypeCode ' + @TicketTypeCode
        END
    ELSE
        BEGIN
            INSERT INTO [dbo].[TicketTypeGroup]
            (
                TicketTypeCode, 
                TicketTypeDescription, 
                TicketTypeGroup
            )
            VALUES
            (
                @TicketTypeCode, 
                @TicketTypeDescription, 
                @TicketTypeGroup
            )
            PRINT 'Inserted INFAddUpdateTicketTypeGroups TicketTypeCode ' + @TicketTypeCode
        END
    --END IF    
END
GO

-- Delete all rows
TRUNCATE TABLE [dbo].[TicketTypeGroup]
GO

-- Insert new rows
EXEC INFAddUpdateTicketTypeGroups 'ADT', 'Anytime Day Travelcard', 					'Travelcard'
EXEC INFAddUpdateTicketTypeGroups 'DTP', 'Off-Peak Day Travelcard Plus', 			'Travelcard'
EXEC INFAddUpdateTicketTypeGroups 'FDT', 'First Class Anytime Day Travelcard', 		'Travelcard'
EXEC INFAddUpdateTicketTypeGroups 'FTP', 'First Class Off-Peak Day Travelcard Plus', 'Travelcard'
EXEC INFAddUpdateTicketTypeGroups 'ODT', 'Off-peak Day Travelcard', 				'Travelcard'
EXEC INFAddUpdateTicketTypeGroups 'OTF', 'First Class Off-Peak Day Travelcard', 	'Travelcard'
EXEC INFAddUpdateTicketTypeGroups 'STO', 'Super Off-Peak Day Travelcard', 			'Travelcard'
EXEC INFAddUpdateTicketTypeGroups 'WDT', 'Super Off-Peak Day Travelcard', 			'Travelcard'
EXEC INFAddUpdateTicketTypeGroups 'WRE', 'Super Off-Peak Day Travelcard', 			'Travelcard'

GO



----------------
-- Change Log --
----------------

USE PermanentPortal

Declare @@value decimal(7,3)
Select @@value = left(right('$Revision:   1.1  $',8),7)


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 016 and VersionNumber = @@value)
BEGIN
	UPDATE dbo.MDSChangeCatalogue
	SET
		ChangeDate = getdate(),
		Summary = 'Updated with latest MDS data'
	WHERE ScriptNumber = 016 AND VersionNumber = @@value
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
		016,
		@@value,
		'Upadted with latest MDS data'
	)
END