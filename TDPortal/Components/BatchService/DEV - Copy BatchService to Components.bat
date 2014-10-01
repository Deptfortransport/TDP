:: _______________________________________________________________________________________
::
::  Batch File:      	Copy BatchService to Components.bat
::  Author:          	David Lane
::  Purpose:  	     	Copies necessary debug dll's from \TDPortal\Codebase folders to the \Components folder.
::
:: 						FOR DEVELOPMENT MACHINES USE ONLY
:: _________________________________________________________________________________________
::

@echo off

:: Go to BatchService folder
D:
cd D:\TDPortal\Codebase\TransportDirect\BatchJourneyPlannerService\bin\Debug

:: EXE and DLLs
copy *.exe D:\TDPortal\Components\BatchService\
copy *.dll D:\TDPortal\Components\BatchService\

:: CONFIG
copy *.config D:\TDPortal\Components\BatchService\

:: BAT
copy BatchFiles\*.bat D:\TDPortal\Components\BatchService\

:: XSLT
copy *.xslt D:\TDPortal\Components\BatchService\

@echo 
@echo **********************
@echo Install with the following credentials:
@echo Username: .\SJP_User
@echo Password: !password!1
@echo **********************
@echo 
