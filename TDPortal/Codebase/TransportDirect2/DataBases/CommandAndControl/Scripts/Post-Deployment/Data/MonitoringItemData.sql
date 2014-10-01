﻿
-- =============================================
-- Configure monitoring items
-- =============================================

Use CommandAndControl
Go

-- Add DB monitoring items
EXEC AddUpdateDatabaseMonitoringItem 1, 600, 0, N'TDPWeb Property Service Version', N'DefaultDB', N'SELECT pValue FROM PROPERTIES WHERE AID = ''TDPWeb'' AND pName = ''propertyservice.version''', N'> 1'
EXEC AddUpdateDatabaseMonitoringItem 2, 600, 1, N'DefaultDB Version', N'DefaultDB', N'SELECT Max(DatabaseVersionInfo) FROM VersionInfo', N'!Build175'
EXEC AddUpdateDatabaseMonitoringItem 3, 600, 1, N'ContentDB Version', N'ContentDB', N'SELECT Max(DatabaseVersionInfo) FROM VersionInfo', N'!Build175'
EXEC AddUpdateDatabaseMonitoringItem 4, 600, 1, N'GazetteerDB Version', N'GazetteerDB', N'SELECT Max(DatabaseVersionInfo) FROM VersionInfo', N'!Build175'
EXEC AddUpdateDatabaseMonitoringItem 5, 600, 1, N'TransientPortalDB Version', N'TransientPortalDB', N'SELECT Max(DatabaseVersionInfo) FROM VersionInfo', N'!Build175'

-- Add File monitoring items
EXEC AddUpdateFileMonitoringItem 1, 600, 1, N'Pool 1 Online', N'D:\inetpub\wwwroot\Pool1_Ok.gif',N'=Unable to check - file may not be present'
EXEC AddUpdateFileMonitoringItem 2, 600, 1, N'Pool 2 Online', N'D:\inetpub\wwwroot\Pool2_Ok.gif',N'=Unable to check - file may not be present'
EXEC AddUpdateFileMonitoringItem 3, 600, 1, N'Mobile Pool 1 Online', N'D:\inetpub\wwwroot\TDPMobile\Pool1_mobile_OK.gif',N'=Unable to check - file may not be present'
EXEC AddUpdateFileMonitoringItem 4, 600, 1, N'Mobile Pool 2 Online', N'D:\inetpub\wwwroot\TDPMobile\Pool2_mobile_OK.gif',N'=Unable to check - file may not be present'
EXEC AddUpdateFileMonitoringItem 5, 600, 1, N'tdp.userportal.tdpweb.dll details', N'D:\inetpub\wwwroot\TDPWeb\bin\tdp.userportal.tdpweb.dll', N'!Version: 2.7.0.1 Last Modified: 18/07/2012 15:18:26 Size(Kb): 266 Kb'
EXEC AddUpdateFileMonitoringItem 6, 600, 1, N'tdp.userportal.tdpmobile.dll details', N'D:\inetpub\wwwroot\TDPMobile\bin\tdp.userportal.tdpmobile.dll', N'!Version: 2.7.0.1 Last Modified: 18/07/2012 15:18:38 Size(Kb): 139 Kb'
EXEC AddUpdateFileMonitoringItem 7, 600, 1, N'CJP.dll details',N'D:\TransportDirect\Web\CJP\Bin\td.CJP.dll',N'!Version: 13.3.0.0 Last Modified: 09/05/2012 17:55:06 Size(Kb): 232 Kb'

-- Add WMI monitoring items
EXEC AddUpdateWMIMonitoringItem 1, 300, 1, N'Free space on D, Red if < 10Gb', N'SELECT FreeSpace FROM Win32_Volume WHERE DriveLetter = ''D:''', N'< 10000000000'
EXEC AddUpdateWMIMonitoringItem 2, 300, 1, N'CPU Usage - red if > 90%', N'SELECT PercentProcessorTime FROM Win32_PerfFormattedData_PerfOS_Processor where name = ''_total''', N'> 90'
EXEC AddUpdateWMIMonitoringItem 3, 300, 1, N'Road host mem usage (red = too low to be inflated or not running)', N'SELECT WorkingSetPrivate FROM Win32_PerfFormattedData_PerfProc_Process WHERE Name = ''td.RoadHostService''', N'< 700000000'
EXEC AddUpdateWMIMonitoringItem 4, 300, 1, N'Cycle host mem usage (red = too low to be inflated or not running)', N'SELECT WorkingSetPrivate FROM Win32_PerfFormattedData_PerfProc_Process WHERE Name = ''td.cp.RoadHostService''', N'< 700000000'
EXEC AddUpdateWMIMonitoringItem 5, 120, 1, N'TTBO host mem usage (red = too low to be inflated or not running)', N'SELECT WorkingSetPrivate FROM Win32_PerfFormattedData_PerfProc_Process WHERE Name = ''td.TTBOHost''', N'< 12000000'
EXEC AddUpdateWMIMonitoringItem 6, 300, 1, N'Current W3SVC Connections', N'SELECT CurrentConnections FROM Win32_PerfFormattedData_W3SVC_WebService', N'=0'
EXEC AddUpdateWMIMonitoringItem 7, 300, 1, N'sjpprimaryqueue MSMQ length ', N'SELECT MessagesinQueue FROM Win32_PerfRawData_msmq_MSMQQueue WHERE Name like ''%sjpprimaryqueue%''',N'> 0' 
EXEC AddUpdateWMIMonitoringItem 8, 300, 1, N'tdprimaryqueue  MSMQ length ', N'SELECT MessagesinQueue FROM Win32_PerfRawData_msmq_MSMQQueue WHERE Name like ''%tdprimaryqueue%''',N'> 0' 

-- Add Checksum monitoring items
EXEC AddUpdateChecksumMonitoringItem 1, 3600, 1, N'D:\inetpub\wwwroot\TDPWeb checksum', N'D:\inetpub\wwwroot\TDPWeb','.db|.txt|.log|.installlog|.xml', N'=Not Matched'
EXEC AddUpdateChecksumMonitoringItem 2, 3600, 1, N'D:\inetpub\wwwroot\TDPMobile checksum', N'D:\inetpub\wwwroot\TDPMobile','.db|.txt|.log|.installlog|.xml|.gif|.off', N'=Not Matched'
EXEC AddUpdateChecksumMonitoringItem 3, 3600, 1, N'D:\TDPortal\Components checksum', N'D:\TDPortal\Components','.db|.txt|.log|.installlog|.xml', N'=Not Matched'
EXEC AddUpdateChecksumMonitoringItem 4, 3600, 1, N'D:\TransportDirect\Web\CJP\Bin checksum', N'D:\TransportDirect\Web\CJP\Bin','.db|.txt|.log|.installlog|.xml', N'=Not Matched'
EXEC AddUpdateChecksumMonitoringItem 5, 3600, 1, N'D:\TransportDirect\Config checksum', N'D:\TransportDirect\Config','.db', N'=Not Matched'
EXEC AddUpdateChecksumMonitoringItem 6, 3600, 1, N'D:\TransportDirect\Services checksum', N'D:\TransportDirect\Services','.db|.txt|.log|.installlog|.xml', N'=Not Matched'

GO
