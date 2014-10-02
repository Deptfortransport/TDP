:: _______________________________________________________________________________________
::
::  Batch File:      	NaPTANxml.bat
::  Author:          	Unknown     
::
::  Built/Tested On: 	Windows 2000 Advanced Server
::
::  Purpose:  	     	Used to process the weekly Nptg data import VFD300.
::		     	This batch file is called by DFT.bat	
::
::  Last Update:     	Steve Craddock 22/07/05 - added process to move the files from the
::						processing folder into D:\dataload\osmap\naptan
::						for SUBSEQUENT processing by ESRI gaz naptan 
::						importer ( mapnaptanloader
::						- added logging of this batch script
::
::			Steve Craddock 02/02/07 added copy for new TDP_MAPS import folder
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
	set output_file= D:\Gateway\log\VFD300_%currdate%_%currTime%.log

	echo %date% %time% vfd300 import started >> %OUTPUT_FILE%

:: copy the new files to the ESRI holding folders
:: this is done first case we wish to start the ESRI process as well
:: note these folders have been cleared down in dft.bat

	echo %date% %time% copying vfd300 files to ESRI folders  >> %OUTPUT_FILE%

	copy D:\Gateway\dat\Processing\vfd300\*.xml D:\dataload\osmap\naptan\ >> %OUTPUT_FILE%

	copy D:\Gateway\dat\Processing\vfd300\*.xml D:\dataload\TDP_MAPS\naptan\ >> %OUTPUT_FILE%

	echo %date% %time% copying vfd300 files to ESRI folder completed >> %OUTPUT_FILE%

:: start importer

	echo %date% %time% Starting loading NaPTAN >> %OUTPUT_FILE%

	"D:\Gateway\bin\Utils\Additional Data Import\AdditionalDataImport.exe" P NaPTAN Y
	if ErrorLevel==1 GoTo Error

	echo %date% %time% Completed loading NaPTAN >> %OUTPUT_FILE%

:: Loading Train taxi data

	echo %date% %time% Starting loading TrainTaxi  >> %OUTPUT_FILE%
	
	"D:\Gateway\bin\Utils\Additional Data Import\AdditionalDataImport.exe" A NaPTAN N TrainTaxiLink
	if ErrorLevel==1 GoTo error

	echo %date% %time% Completed loading TrainTaxi  >> %OUTPUT_FILE%

:: Loading TTBO station data to Naptan

	echo %date% %time% Starting loading TTBO into NaPTAN  >> %OUTPUT_FILE%

	cd /d "D:\Gateway\bin\Utils\Additional Data Import" 

	".\AdditionalDataImport.exe" A NaPTAN N NLCDat
	if ErrorLevel==1 GoTo error

	echo %date% %time% Completed loading TTBO into NaPTAN  >> %OUTPUT_FILE%
	
	goto end

:Error
	echo ErrorLevel %ErrorLevel%
	echo Please check log files for more details (D:\gateway\logs\AdditionalData)

	echo %date% %time% vfd300 import completed >> %OUTPUT_FILE%

:end

