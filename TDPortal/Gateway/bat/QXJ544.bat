:: _______________________________________________________________________________________
::
::  Batch File:      	QXJ544.bat
::  Author:          	Steve Craddock     
::
::  Built/Tested On: 	Windows 2000 Advanced Server
::
::  Purpose:  	     	Used to copy the weekly Nptg data import QXJ544 into the ESRI for later processing
::		     	This batch file is called by DFT.bat
::
::  Last Update:     	Steve Craddock 22/07/05 - added process to move the files from the
::						processing folder into D:\dataload\gaz\naptan
::						for SUBSEQUENT processing by ESRI gaz naptan 
::						importer ( part of LoadAndPrepareNaptanAndNPTG.xml) 
::						- added logging of this batch script
::
:: _________________________________________________________________________________________
::


@echo off
set currDate=
set currTime=
set time=
set timenow=
set date=

:: get todays date for use later
	for /F "tokens=1-4 delims=/ " %%i in ('date /t') do (
	rem set Day=%%j
	rem set Month=%%k
	rem set Year=%%l
	set currDate=%%l%%k%%j
	)

:: get current time for use later

	for /F "tokens=1-2 delims=: " %%i in ('time /t') do (
	set xcopyhour=%%i
	set xcopymins=%%j
	set currTime=%%i%%j
	)

:: set the name of the log file
	set output_file= D:\Gateway\log\QXJ544_%currdate%_%currTime%.log

	echo %date% %time% QXJ544 import started >> %OUTPUT_FILE%

:: copy in new files

	echo %date% %time% copying files to ESRI folder  >> %OUTPUT_FILE%

	copy D:\Gateway\dat\Processing\qxj544\*.csv D:\dataload\gaz\naptan >> %OUTPUT_FILE%

	echo %date% %time% copying qxj544 ended >> %OUTPUT_FILE%
:end
