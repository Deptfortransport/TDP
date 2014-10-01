:: ------------------------------------------------------------------------
::
::  Batch File:      	CleanLogFolders.bat
::  Author:          	Mitesh Modi
::  Purpose:  	     	Deletes all folders containing log files
::
::						FOR DEVELOPMENT MACHINES USE ONLY
::
:: -------------------------------------------------------------------------

:: Go to folder
D:
cd \
cd TDPortal

:: Delete log files
del TDPMobile*.txt /f /q
del TDPWeb*.txt /f /q
del Web*.txt /f /q
del TDRemotingHost*.txt /f /q
del td.UserPortal.TDRemotingHost.log /f /q
del EnhancedExposedServices*.txt /f /q
del td.enhancedexposedservices*.log /f /q
del CoordinateConvertorService*.txt /f /q
del BatchJourneyPlannerService*.txt /f /q
del DepartureBoardWebService*.txt /f /q
del EventReceiver*.txt /f /q
del TDPEventReceiver*.txt /f /q
del WebLogReader*.txt /f /q
del TicketTypeFeed*.txt /f /q
del TransactionWebService*.txt /f /q
del SiteStatusLoaderService*.txt /f /q
del CacheUp*.txt /f /q
del ReportDataImporter*.txt /f /q
del LocationJsGenerator*.txt /f /q
del DataGatewayImport*.txt /f /q
del DataLoader*.txt /f /q
del SiteStatusLoaderMonitor*.txt /f /q
del *HistoricStatus.csv /f /q
del SiteStatusLoaderServiceStatus.xml /f /q
del RealtimeStatus.xml /f /q
del TestApp*.txt /f /q

del TILogs\OP\TransactionInjector*.txt /f /q
del TILogs\REF\TransactionInjector*.txt /f /q