USE [master]
GO
CREATE LOGIN [Replication_User] WITH PASSWORD=N'!password!1', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
EXEC master..sp_addsrvrolemember @loginame = N'Replication_User', @rolename = N'serveradmin'
GO
USE [AirInterchange]
GO
CREATE USER [Replication_User] FOR LOGIN [Replication_User]
GO
USE [AirInterchange]
GO
EXEC sp_addrolemember N'db_owner', N'Replication_User'
GO
--USE [distribution]
--GO
--CREATE USER [Replication_User] FOR LOGIN [Replication_User]
--GO
--USE [distribution]
--GO
--EXEC sp_addrolemember N'db_owner', N'Replication_User'
--GO
USE [master]
GO
CREATE USER [Replication_User] FOR LOGIN [Replication_User]
GO
USE [model]
GO
CREATE USER [Replication_User] FOR LOGIN [Replication_User]
GO
USE [msdb]
GO
CREATE USER [Replication_User] FOR LOGIN [Replication_User]
GO
USE [CP_Routing]
GO
CREATE USER [Replication_User] FOR LOGIN [Replication_User]
GO
USE [CP_Routing]
GO
EXEC sp_addrolemember N'db_owner', N'Replication_User'
GO
USE [NPTG]
GO
CREATE USER [Replication_User] FOR LOGIN [Replication_User]
GO
USE [NPTG]
GO
EXEC sp_addrolemember N'db_owner', N'Replication_User'
GO
USE [Routing]
GO
CREATE USER [Replication_User] FOR LOGIN [Replication_User]
GO
USE [Routing]
GO
EXEC sp_addrolemember N'db_owner', N'Replication_User'
GO
USE [SJPConfiguration]
GO
CREATE USER [Replication_User] FOR LOGIN [Replication_User]
GO
USE [SJPConfiguration]
GO
EXEC sp_addrolemember N'db_owner', N'Replication_User'
GO
USE [SJPContent]
GO
CREATE USER [Replication_User] FOR LOGIN [Replication_User]
GO
USE [SJPContent]
GO
EXEC sp_addrolemember N'db_owner', N'Replication_User'
GO
USE [SJPGazetteer]
GO
CREATE USER [Replication_User] FOR LOGIN [Replication_User]
GO
USE [SJPGazetteer]
GO
EXEC sp_addrolemember N'db_owner', N'Replication_User'
GO
--USE [SJPReportStaging]
--GO
--CREATE USER [Replication_User] FOR LOGIN [Replication_User]
--GO
USE [SJPTransientPortal]
GO
CREATE USER [Replication_User] FOR LOGIN [Replication_User]
GO
USE [SJPTransientPortal]
GO
EXEC sp_addrolemember N'db_owner', N'Replication_User'
GO
