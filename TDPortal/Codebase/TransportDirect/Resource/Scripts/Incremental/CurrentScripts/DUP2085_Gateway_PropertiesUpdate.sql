-- ***********************************************
-- NAME 		: DUP2085_Gateway_PropertiesUpdate.sql
-- DESCRIPTION 	: Gateway properties update
-- AUTHOR		: Mitesh Modi
-- DATE			: 22 Nov 2013
-- ************************************************

USE [PermanentPortal]
GO
------------------------------------------------

DECLARE @AID varchar(50) = ''
DECLARE @GID varchar(50) = 'DataGateway'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

EXEC AddUpdateProperty 'Gateway.Transfer.Wait.Milliseconds.afc743', '5000', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Gateway.Process.Wait.Milliseconds.afc743', '7500', @AID, @GID, @PartnerID, @ThemeID



-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2085, 'Gateway properties update'

GO