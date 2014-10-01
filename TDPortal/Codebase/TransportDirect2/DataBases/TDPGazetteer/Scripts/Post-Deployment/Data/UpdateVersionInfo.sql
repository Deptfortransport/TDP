-- =============================================
-- Script Template
-- =============================================

USE TDPGazetteer
Go

DELETE [VersionInfo]
GO
INSERT INTO [VersionInfo] ([DatabaseVersionInfo])
     VALUES ('Build175')
GO

