-- ***********************************************
-- NAME 		: DUP1310_ReferenceTransactionEvent_storedproc.sql
-- DESCRIPTION 	: Script to create/update stored procedures for ReferenceTransactionEvent for Transaction Injector
-- AUTHOR		: Mitesh Modi
-- DATE			: 13 Mar 2009
-- ************************************************

USE [ReportStagingDB]
GO

-- ************************************************************************************************
-- THIS SCRIPT MUST BE AMENDED TO ASSIGN PERMISSIONS TO THE CORRECT USER
-- ************************************************************************************************

----------------------------------------------------------------
-- Create AddReferenceTransactionEvent stored proc
----------------------------------------------------------------
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddReferenceTransactionEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE AddReferenceTransactionEvent 
		(@EventType varchar(50), 
		 @ServiceLevelAgreement bit, 
		 @Submitted datetime, 
		 @SessionId varchar(50), 
		 @TimeLogged datetime, 
		 @Successful bit)
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

----------------------------------------------------------------
-- Update AddReferenceTransactionEvent stored proc
----------------------------------------------------------------
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

ALTER  PROCEDURE AddReferenceTransactionEvent 
(@EventType varchar(50), 
 @ServiceLevelAgreement bit, 
 @Submitted datetime, 
 @SessionId varchar(50), 
 @TimeLogged datetime, 
 @Successful bit)
As
    set nocount off

    declare @localized_string_UnableToInsert AS varchar(60)
    set @localized_string_UnableToInsert = 'Unable to Insert a new record into ReferenceTransactionEvent Table'

    Insert into ReferenceTransactionEvent (Submitted, EventType, ServiceLevelAgreement, SessionId, TimeLogged, Successful)
    Values (@Submitted, @EventType, @ServiceLevelAgreement, @SessionId, @TimeLogged, @Successful)

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsert, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


----------------------------------------------------------------
-- Add permission to the appropriate user
----------------------------------------------------------------
GRANT EXEC ON [dbo].[AddReferenceTransactionEvent] TO [??????]
-- Should be ASPUSER_S

GO


----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 1310
SET @ScriptDesc = 'Updated AddReferenceTransactionEvent stored procedures'

IF EXISTS (SELECT * FROM [dbo].[ChangeCatalogue] WHERE ScriptNumber = @ScriptNumber)
  BEGIN
    UPDATE [dbo].[ChangeCatalogue]
    SET ChangeDate = getDate(), Summary = @ScriptDesc
    WHERE ScriptNumber = @ScriptNumber
  END
ELSE
  BEGIN
    INSERT INTO [dbo].[ChangeCatalogue] (ScriptNumber, ChangeDate, Summary)
    VALUES (@ScriptNumber, getDate(), @ScriptDesc)
  END
GO