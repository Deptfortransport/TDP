-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : DataLoader properties data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

------------------------------------------------
-- 'DataLoader' properties
------------------------------------------------

DECLARE @AID varchar(50) = 'DataLoader'
DECLARE @GID varchar(50) = 'DataGateway'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

-- Property Service
EXEC AddUpdateProperty 'propertyservice.version', '1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'propertyservice.refreshrate', '300000', @AID, @GID, @PartnerID, @ThemeID

-- Data loader configuration properties

-- LULGateway (London Underground) - Transfer onto the Gateway
EXEC AddUpdateProperty 'DataLoader.Configuration.LULGateway.Class.Transfer', 'TDP.DataLoader.XmlTransferTask', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Configuration.LULGateway.ClassAssembly.Transfer', 'tdp.dataloader.exe', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Configuration.LULGateway.Directory', 'D:\inetpub\wwwroot\Data\LUL\', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Configuration.LULGateway.Directory.Clean', 'false', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'DataLoader.Transfer.LULGateway.Locations', '1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Transfer.LULGateway.Location.1', 'http://cloud.tfl.gov.uk/TrackerNet/LineStatus', @AID, @GID, @PartnerID, @ThemeID
--EXEC AddUpdateProperty 'DataLoader.Transfer.LULGateway.Location.1.Username', '', @AID, @GID, @PartnerID, @ThemeID
--EXEC AddUpdateProperty 'DataLoader.Transfer.LULGateway.Location.1.Password', '', @AID, @GID, @PartnerID, @ThemeID
--EXEC AddUpdateProperty 'DataLoader.Transfer.LULGateway.Location.1.Domain', '', @AID, @GID, @PartnerID, @ThemeID
--EXEC AddUpdateProperty 'DataLoader.Transfer.LULGateway.Location.1.Proxy', 'jwproxyserver:8080', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Transfer.LULGateway.Location.1.Timeout.Seconds', '3', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Transfer.LULGateway.Location.1.LastUpdated.AppendDateTime.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Transfer.LULGateway.FileName.Save', 'LondonUnderground.xml', @AID, @GID, @PartnerID, @ThemeID

-- LUL (London Underground) - Transfer and Load onto WorkUnit
EXEC AddUpdateProperty 'DataLoader.Configuration.LUL.Class.Transfer', 'TDP.DataLoader.XmlTransferTask', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Configuration.LUL.ClassAssembly.Transfer', 'tdp.dataloader.exe', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Configuration.LUL.Class.Load', 'TDP.DataLoader.DatabaseLoadTask', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Configuration.LUL.ClassAssembly.Load', 'tdp.dataloader.exe', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Configuration.LUL.Directory', 'D:\Temp\LUL', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Configuration.LUL.Directory.Clean', 'true', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'DataLoader.Transfer.LUL.Locations', '1,2', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Transfer.LUL.Location.1', 'http://GA01/Data/LUL/LondonUnderground.xml', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Transfer.LUL.Location.2', 'http://GA02/Data/LUL/LondonUnderground.xml', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Transfer.LUL.FileName.Save', 'LondonUnderground.xml', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'DataLoader.Load.LUL.Xml.Validate', 'false', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Load.LUL.Database', 'TransientPortalDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Load.LUL.DatabaseStoredProcedure', 'ImportUndergroundStatusData', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Load.LUL.DatabaseTimeout', '3000', @AID, @GID, @PartnerID, @ThemeID

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'DataLoader properties data'