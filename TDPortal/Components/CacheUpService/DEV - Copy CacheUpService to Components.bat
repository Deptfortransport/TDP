:: _______________________________________________________________________________________
::
::  Batch File:      	Copy CacheUpService to Components.bat
::  Author:          	Mitesh Modi
::  Purpose:  	     	This batch file copies necessary debug dll's to the \Components folder.
::
:: 						FOR DEVELOPMENT MACHINES USE ONLY
:: _________________________________________________________________________________________
::

@echo off

:: Go to CacheUpService folder
D:
cd D:\TDPortal\ThirdParty\Atos\CacheUpService\CacheUpService\bin\Debug

:: EXE and DLLs
copy *.exe D:\TDPortal\Components\CacheUpService\
copy *.dll D:\TDPortal\Components\CacheUpService\

:: CONFIG
copy *.config D:\TDPortal\Components\CacheUpService\
copy *.xml D:\TDPortal\Components\CacheUpService\

:: BAT
copy *.bat D:\TDPortal\Components\CacheUpService\

:: Delete any unnecessary files
del D:\TDPortal\Components\CacheUpService\*vshost.exe*