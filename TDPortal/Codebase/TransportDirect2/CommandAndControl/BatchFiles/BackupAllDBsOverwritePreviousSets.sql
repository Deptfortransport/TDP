--backups
BACKUP DATABASE [AirInterchange] 
TO  DISK = N'I:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup\AirInterchange.bak' 
WITH NOFORMAT, INIT,  
NAME = N'AirInterchange-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO
BACKUP DATABASE [CommandAndControl] 
TO  DISK = N'I:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup\CommandAndControl.bak' 
WITH NOFORMAT, INIT,  
NAME = N'CommandAndControl-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO
--UPDATE TO BE CORRECT CP_ROUTING VERSION NUMBER!!!!!!!!!!!!!!
BACKUP DATABASE [CP_Routing_B15_3] 
TO  DISK = N'I:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup\CP_Routing_B15_3.bak' 
WITH NOFORMAT, INIT,  
NAME = N'CP_Routing_B15_3-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO
BACKUP DATABASE [NPTG] 
TO  DISK = N'I:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup\NPTG.bak' 
WITH NOFORMAT, INIT,  
NAME = N'NPTG-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO
BACKUP DATABASE [PermanentPortal] 
TO  DISK = N'I:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup\PermanentPortal.bak' 
WITH NOFORMAT, INIT,  
NAME = N'PermanentPortal-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO
BACKUP DATABASE [Routing] 
TO  DISK = N'I:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup\Routing.bak' 
WITH NOFORMAT, INIT,  
NAME = N'Routing-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO
BACKUP DATABASE [SJPConfiguration] 
TO  DISK = N'I:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup\SJPConfiguration.bak' 
WITH NOFORMAT, INIT,  
NAME = N'SJPConfiguration-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO
BACKUP DATABASE [SJPContent] 
TO  DISK = N'I:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup\SJPContent.bak' 
WITH NOFORMAT, INIT,  
NAME = N'SJPContent-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO
BACKUP DATABASE [SJPGazetteer] 
TO  DISK = N'I:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup\SJPGazetteer.bak' 
WITH NOFORMAT, INIT,  
NAME = N'SJPGazetteer-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO
BACKUP DATABASE [SJPReportStaging] 
TO  DISK = N'I:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup\SJPReportStaging.bak' 
WITH NOFORMAT, INIT,  
NAME = N'SJPReportStaging-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO
BACKUP DATABASE [SJPTransientPortal] 
TO  DISK = N'I:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Backup\SJPTransientPortal.bak' 
WITH NOFORMAT, INIT,  
NAME = N'SJPTransientPortal-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO
