:: _______________________________________________________________________________________
::
::  Batch File:      	Copy TransportDirect2 TDEventReceiver to Components.bat
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
cd D:\TDPortal\Codebase\TransportDirect2\Reporting\EventReceiver\bin\Debug

:: EXE and DLLs
copy *.exe D:\TDPortal\Components\EventReceiver2\
copy *.dll D:\TDPortal\Components\EventReceiver2\

:: CONFIG
copy *.config D:\TDPortal\Components\EventReceiver2\

:: BAT
copy BatchFiles\*.bat D:\TDPortal\Components\EventReceiver2\

@echo 
@echo **********************
@echo Install with the following credentials:
@echo Username: .\SJP_User
@echo Password: !password!1
@echo **********************
@echo 


