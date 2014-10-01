:: _______________________________________________________________________________________
::
::  Batch File:      	copy BatchService to Components.bat
::  Author:          	David Lane
::
::  Purpose:  	     	Copies necessary debug dll's from \TDPortal\Codebase folders to the \Components folder.
:: 			FOR DEVELOPMENT MACHINES USE ONLY
:: _________________________________________________________________________________________
::

@echo off

:: EXE, DLLs, CONFIG and XSLT
copy D:\TDPortal\Codebase\TransportDirect\BatchJourneyPlannerService\bin\Debug\*.exe C:\TDPortal\Components\BatchService\
copy D:\TDPortal\Codebase\TransportDirect\BatchJourneyPlannerService\bin\Debug\*.dll C:\TDPortal\Components\BatchService\
copy D:\TDPortal\Codebase\TransportDirect\BatchJourneyPlannerService\bin\Debug\*.config C:\TDPortal\Components\BatchService\
copy D:\TDPortal\Codebase\TransportDirect\BatchJourneyPlannerService\bin\Debug\*.xslt C:\TDPortal\Components\BatchService\

:: BAT
copy D:\TDPortal\Codebase\TransportDirect\BatchJourneyPlannerService\bin\Debug\*.bat C:\TDPortal\Components\BatchService\

:end