-- ***********************************************
-- NAME 		: DUP2097_Update_Custom_EMAIL_Sender_Property.sql
-- DESCRIPTION 	: Logging.Publisher.Custom.EMAIL.Sender property update
-- AUTHOR		: Rich Broddle
-- DATE			: 27 Feb 2014
-- ************************************************

USE [PermanentPortal]
GO
------------------------------------------------

--Main site
DECLARE @AID varchar(50) = 'Web'
DECLARE @GID varchar(50) = 'UserPortal'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

EXEC AddUpdateProperty 'Logging.Publisher.Custom.EMAIL.Sender', 'noreply@transportdirect.info', @AID, @GID, @PartnerID, @ThemeID

--Microsite
SET @AID = 'Microsite'


EXEC AddUpdateProperty 'Logging.Publisher.Custom.EMAIL.Sender', 'noreply@transportdirect.info', @AID, @GID, @PartnerID, @ThemeID


-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2097, 'Update Custom EMAIL Sender Property'

GO