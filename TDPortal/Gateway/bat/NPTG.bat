:: _______________________________________________________________________________________
::
::  Batch File:      	NPTG.bat
::  Author:          	Unknown     
::
::  Built/Tested On: 	Windows 2000 Advanced Server
::
::  Purpose:  	     	Used to process the weekly Nptg data import YNX354.
::		     	This batch file is called by a TNG job but can also be run manually.	
::
::  Last Update:     	Steve Craddock 22/07/05 - added process to move the files from the
::						processing folder into D:\dataload\gaz\nptg and \naptan
::						for SUBSEQUENT processing by ESRI gaz nptg 
::						importer ( part of LoadAndPrepareNaptanAndNPTG.xml) 
::						AND importnaptanviewer.xml
::						- added logging of this batch script
::
::			Mitesh Modi 15/06/10 - added call to NPTGDropDownGaz.bat, to allow processing 
::						of nptg data used by the drop down gaz "auto-suggest" functionality
::						on the Portal
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
	set output_file= D:\Gateway\log\YNX354_%currdate%_%currTime%.log

	echo %date% %time% td.NPTGImport import started >> %OUTPUT_FILE%

:: start importer

	"D:\Gateway\bin\Utils\NPTG Import\td.NPTGImport.exe"

	echo %date% %time% error level from importer %ErrorLevel% >> %OUTPUT_FILE%

:: copy in new files to BOTH ESRI gaz holding folders
:: note these folders have been cleared down in dft.bat

	echo %date% %time% copying files to ESRI folder  >> %OUTPUT_FILE%

	copy D:\Gateway\dat\Processing\ynx354\*.csv D:\dataload\gaz\nptg\ >> %OUTPUT_FILE%

	copy D:\Gateway\dat\Processing\ynx354\*.csv D:\dataload\gaz\naptan >> %OUTPUT_FILE%

	echo %date% %time% td.NPTGImport import ended >> %OUTPUT_FILE%
	
:: start drop down gaz data importer	
	echo ************************************************************************************   	>> %OUTPUT_FILE%
	echo %date% %time% Drop down gaz data import starting   	>> %OUTPUT_FILE%
	call C:\Gateway\bat\NPTGDropDownGaz.bat
	echo %date% %time% Drop down gaz data import ended   	>> %OUTPUT_FILE%
	echo ************************************************************************************   	>> %OUTPUT_FILE%

	
:end
