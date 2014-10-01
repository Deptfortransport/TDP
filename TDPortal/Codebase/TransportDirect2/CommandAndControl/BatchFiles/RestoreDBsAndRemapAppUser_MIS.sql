--Restore DB's:
Use master
Go
RESTORE DATABASE [AirInterchange] FROM  DISK = N'D:\FTPTemp\DatabaseBackups\AirInterchange.bak' 
WITH  FILE = 1,   NOUNLOAD,  REPLACE,  STATS = 10,
MOVE 'AirInterchange' TO 'g:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\AirInterchange.MDF',
MOVE 'AirInterchange_log' TO 'h:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\AirInterchange.LDF'
GO
RESTORE DATABASE [CP_Routing_B15_3] FROM  DISK = N'D:\FTPTemp\DatabaseBackups\CP_Routing_B15_3.bak' 
WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10,
MOVE 'Routing' TO 'g:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\CP_Routing_B15_3.MDF',
MOVE 'Routing_log' TO 'h:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\CP_Routing_B15_3.LDF'
GO
RESTORE DATABASE [NPTG] FROM  DISK = N'D:\FTPTemp\DatabaseBackups\NPTG.bak' 
WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10,
MOVE 'NPTG_Staging' TO 'g:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\NPTG.MDF',
MOVE 'NPTG_Staging_log' TO 'h:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\NPTG.LDF'
GO
RESTORE DATABASE [Routing] FROM  DISK = N'D:\FTPTemp\DatabaseBackups\Routing.bak' 
WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10,
MOVE 'Routing_Data' TO 'g:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\Routing.MDF',
MOVE 'Routing_log' TO 'h:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\Routing.LDF'
GO
RESTORE DATABASE [SJPConfiguration] FROM  DISK = N'D:\FTPTemp\DatabaseBackups\SJPConfiguration.bak' 
WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10,
MOVE 'SJPConfiguration' TO 'g:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\SJPConfiguration.MDF',
MOVE 'SJPConfiguration_log' TO 'h:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\SJPConfiguration.LDF'
GO
RESTORE DATABASE [SJPContent] FROM  DISK = N'D:\FTPTemp\DatabaseBackups\SJPContent.bak' 
WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10,
MOVE 'SJPContent' TO 'g:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\SJPContent.MDF',
MOVE 'SJPContent_log' TO 'h:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\SJPContent.LDF'
GO
RESTORE DATABASE [SJPGazetteer] FROM  DISK = N'D:\FTPTemp\DatabaseBackups\SJPGazetteer.bak' 
WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10,
MOVE 'SJPGazetteer' TO 'g:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\SJPGazetteer.MDF',
MOVE 'SJPGazetteer_log' TO 'h:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\SJPGazetteer.LDF'
GO
RESTORE DATABASE [SJPTransientPortal] FROM  DISK = N'D:\FTPTemp\DatabaseBackups\SJPTransientPortal.bak' 
WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10,
MOVE 'SJPTransientPortal' TO 'g:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\SJPTransientPortal.MDF',
MOVE 'SJPTransientPortal_log' TO 'h:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\SJPTransientPortal.LDF'
GO

Use master
Go
--Auto close:
ALTER DATABASE AirInterchange SET AUTO_CLOSE OFF
ALTER DATABASE CommandAndControl SET AUTO_CLOSE OFF
ALTER DATABASE CP_Routing_B15_3 SET AUTO_CLOSE OFF
ALTER DATABASE NPTG SET AUTO_CLOSE OFF
ALTER DATABASE Routing SET AUTO_CLOSE OFF
ALTER DATABASE PermanentPortal SET AUTO_CLOSE OFF
ALTER DATABASE SJPConfiguration SET AUTO_CLOSE OFF
ALTER DATABASE SJPContent SET AUTO_CLOSE OFF
ALTER DATABASE SJPGazetteer SET AUTO_CLOSE OFF
ALTER DATABASE SJPReportStaging SET AUTO_CLOSE OFF
ALTER DATABASE SJPTransientPortal SET AUTO_CLOSE OFF

--Authorization - required for replication
ALTER AUTHORIZATION ON DATABASE::AirInterchange TO sa
ALTER AUTHORIZATION ON DATABASE::CP_Routing_B15_3 TO sa
ALTER AUTHORIZATION ON DATABASE::NPTG TO sa
ALTER AUTHORIZATION ON DATABASE::Routing TO sa
ALTER AUTHORIZATION ON DATABASE::SJPConfiguration TO sa
ALTER AUTHORIZATION ON DATABASE::SJPContent TO sa
ALTER AUTHORIZATION ON DATABASE::SJPGazetteer TO sa
ALTER AUTHORIZATION ON DATABASE::SJPTransientPortal TO sa

Use master
Go
--fix orphaned users:
Use AirInterchange
EXEC sp_change_users_login 'auto_fix','sjp_user' 
GO
Use CommandAndControl
EXEC sp_change_users_login 'auto_fix','sjp_user' 
GO
Use CP_Routing_B15_3
EXEC sp_change_users_login 'auto_fix','sjp_user' 
GO
Use NPTG
EXEC sp_change_users_login 'auto_fix','sjp_user' 
GO
Use Routing
EXEC sp_change_users_login 'auto_fix','sjp_user' 
GO
Use PermanentPortal
EXEC sp_change_users_login 'auto_fix','sjp_user' 
GO
Use SJPConfiguration
EXEC sp_change_users_login 'auto_fix','sjp_user' 
GO
Use SJPContent
EXEC sp_change_users_login 'auto_fix','sjp_user' 
GO
Use SJPGazetteer
EXEC sp_change_users_login 'auto_fix','sjp_user' 
GO
Use SJPReportStaging
EXEC sp_change_users_login 'auto_fix','sjp_user' 
GO
Use SJPTransientPortal
EXEC sp_change_users_login 'auto_fix','sjp_user' 
GO

Use master
Go