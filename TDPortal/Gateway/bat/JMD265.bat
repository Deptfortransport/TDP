:: _______________________________________________________________________________________
::
::  Batch File:      	JMD265.bat
::  Author:          	John Frank     
::
::  Purpose:  	     	Runs as part of the NaPTAN load.  Runs data feed jmd265 and determines if the 
::		     	data has been updated.  If it has the ESRI data importer needs to be run.
::
::  Last Update:     	John Frank  30/01/08 - Initial revision
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
	set output_file_JMD= D:\Gateway\log\JMD265_%currdate%_%currTime%.log

	echo %date% %time% JMD265 import started >> %output_file_JMD%


:: Get the current data version
osql -S D03 /E /Q "EXIT(USE GAZ_Staging SELECT MIN(Version) from gazadmin.dft_places)"
set prev_version=%errorlevel%

:: Run the Data Importer

cd D:\Gateway\bin
call D:\Gateway\bat\_ipAddress.bat

echo Trying jmd265 on %TDPServerP%
D:\Gateway\bin\td.dataimport.exe jmd265 /ipaddress:%TDPServerP% %1 >> %output_file_JMD%

:: If the gateway feed failed (possibly due to no file) run the SP so that newer data
:: in GAZ will be copied to staging

if %errorlevel%==0 GOTO :importOK

echo jmd265 failed with code:%errorlevel% so running ImportAttractionAliasData SP >> %output_file_JMD%
osql -S D03 /E /Q "USE GAZ_Staging EXEC dbo.ImportAttractionAliasData null"

:importOK

rem determine if ERSI importer needs to be run

osql -S D03 /E /Q "EXIT(USE GAZ_Staging SELECT MIN(Version) from gazadmin.dft_places)"

set new_version=%errorlevel%

:: if version number is now greater run the ESRI importer.
if %prev_version% geq %new_version% GOTO :NoRun

echo Run ESRI data importer >> %output_file_JMD%
D:\gateway\bin\utils\naptannptg\dataimporter\dataimporter.exe -c "D:\gateway\bin\utils\naptannptg\dataimporter\Configuration\PreparePOI.xml"  >> %output_file_JMD% 
echo ESRI data importer complete >> %output_file_JMD%

GOTO :end

:NoRun

echo Didnt need to run ESRI data importer >> %output_file_JMD%

:end

cd D:\Gateway\bat

echo %date% %time% JMD265 import complete >> %output_file_JMD%
