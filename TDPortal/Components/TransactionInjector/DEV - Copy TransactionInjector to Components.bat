:: _______________________________________________________________________________________
::
::  Batch File:      	Copy TransactionInjector to Components.bat
::  Author:          	Mitesh Modi
::  Purpose:  	     	TransactionInjector is setup to run from the D:\TDPortal\Components\TransactionInjector folder.
::						This batch file copies necessary debug dll's from \TDPortal\Codebase folders to the \Components folder.
::
:: 						FOR DEVELOPMENT MACHINES USE ONLY
:: _________________________________________________________________________________________
::

@echo off

:: Go to TransactionInjector folder
D:
cd D:\TDPortal\Codebase\TransportDirect\ReportDataProvider\TransactionInjector\bin\Debug

:: EXE and DLLs
copy *.exe D:\TDPortal\Components\TransactionInjector\
copy *.dll D:\TDPortal\Components\TransactionInjector\

:: CONFIG
copy *.config D:\TDPortal\Components\TransactionInjector\

:: BAT
copy BatchFiles\*.bat D:\TDPortal\Components\TransactionInjector\

:: PROPERTIES (TransactionInjector uses file properties)
copy TransactionInjectorProperties.xml D:\TDPortal\Components\TransactionInjector\
copy TransactionInjectorThemes.xml D:\TDPortal\Components\TransactionInjector\

:: REQUESTS (Transaction requests to submit) 
:: Copy folder and files /e, overwrite readonly files /r, and suppress any confirms /y
:: make the Templates directory as it doesnt contain anything so Visual Studio does include in the bin\Debug folder
mkdir D:\TDPortal\Components\TransactionInjector\Templates
xcopy TransactionRequestData D:\TDPortal\Components\TransactionInjector\ /e /r /y

