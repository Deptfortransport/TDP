:: _______________________________________________________________________________________
::
::  Batch File:      	Copy EventReceiver to Components.bat
::  Author:          	Mitesh Modi
::  Purpose:  	     	EventReceiver is setup to run from the D:\TDPortal\Components\EventReceiver folder.
::						This batch file copies necessary debug dll's from \TDPortal\Codebase folders to the \Components folder.
::
:: 						FOR DEVELOPMENT MACHINES USE ONLY
:: _________________________________________________________________________________________
::

@echo off

:: Go to EventReceiver folder
D:
cd D:\TDPortal\Codebase\TransportDirect\ReportDataProvider\EventReceiver\bin\Debug

:: EXE and DLLs
copy *.exe D:\TDPortal\Components\EventReceiver\
copy *.dll D:\TDPortal\Components\EventReceiver\

:: CONFIG
copy *.config D:\TDPortal\Components\EventReceiver\

:: BAT
copy BatchFiles\*.bat D:\TDPortal\Components\EventReceiver\

@echo 
@echo **********************
@echo Install with the following credentials:
@echo Username: .\SJP_User
@echo Password: !password!1
@echo **********************
@echo 


