:: _______________________________________________________________________________________
::
::  Batch File:      	Copy ReportStagingDataArchiver to Components.bat
::  Author:          	Mitesh Modi
::  Purpose:  	     	This batch file copies necessary debug dll's from \TDPortal\Codebase folders to the \Components folder.
::
:: 						FOR DEVELOPMENT MACHINES USE ONLY
:: _________________________________________________________________________________________
::

@echo off

:: Go to ReportStagingDataArchiver folder
D:
cd D:\TDPortal\Codebase\TransportDirect\ReportDataProvider\ReportStagingDataArchiver\bin\Debug

:: EXE and DLLs
copy *.exe D:\TDPortal\Components\ReportDataArchiver\
copy *.dll D:\TDPortal\Components\ReportDataArchiver\

:: CONFIG
copy *.config D:\TDPortal\Components\ReportDataArchiver\

:: BAT
copy BatchFiles\*.bat D:\TDPortal\Components\ReportDataArchiver\

:: PROPERTIES (ReportStagingDataArchiver uses file properties)
copy ReportStagingDataArchiverProperties.xml D:\TDPortal\Components\ReportDataArchiver\
copy ReportStagingDataArchiverThemes.xml D:\TDPortal\Components\ReportDataArchiver\


:: Delete any unnecessary files
del D:\TDPortal\Components\ReportDataArchiver\*vshost.exe*