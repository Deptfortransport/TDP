:: _______________________________________________________________________________________
::
::  Batch File:      	Copy ReportDataImporter to Components.bat
::  Author:          	Mitesh Modi
::  Purpose:  	     	This batch file copies necessary debug dll's from \TDPortal\Codebase folders to the \Components folder.
::
:: 						FOR DEVELOPMENT MACHINES USE ONLY
:: _________________________________________________________________________________________
::

@echo off

:: Go to ReportDataImporter folder
D:
cd D:\TDPortal\Codebase\TransportDirect\ReportDataProvider\ReportDataImporter\bin\Debug

:: EXE and DLLs
copy *.exe D:\TDPortal\Components\ReportDataImporter\
copy *.dll D:\TDPortal\Components\ReportDataImporter\

:: CONFIG
copy *.config D:\TDPortal\Components\ReportDataImporter\

:: BAT
copy BatchFiles\*.bat D:\TDPortal\Components\ReportDataImporter\

:: PROPERTIES (ReportDataImporter uses file properties)
copy ReportDataImporterProperties.xml D:\TDPortal\Components\ReportDataImporter\
copy ReportDataImporterThemes.xml D:\TDPortal\Components\ReportDataImporter\


