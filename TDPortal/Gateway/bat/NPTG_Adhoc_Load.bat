:: ______________________________________________________________________________________________________________
::
::  Batch File:      dft.bat
::  Author:          Tushar Karsan     
::
::  Built/Tested On: Windows 2000 Advanced Server
::
::  Purpose:  	     Used to process the weekly Naptan and Nptg data imports.
::		     This batch file is called by a TNG job but can also be run manually.	
::
::  Last Update:     Joe Morrissey 13/04/2004 - added new Esri automated processes
::		     Anthony Coward 08/08/2004 - added entries to update D07 GAZ database
::  		     Anthony Coward 27/10/2004 - Removed D12 entries as no longer in use for Band3
::	             SC 24/03 modified to import data prior to running the updates....
::                   CR 05/08/05 modified for new DEL 7 dataimporter.exe 
:: ______________________________________________________________________________________________________________
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
	set output_file= D:\Gateway\log\dft1_NPTGAdhoc%currdate%_%currTime%.log

	echo NPTG_Adhoc_Load.bat import started %time%  >> %OUTPUT_FILE%

:: Verify import flag has been set
	rem check for NaPTAN_Import.No flag. Do not run if present
	if exist D:\Gateway\NaPTAN_Import.No GOTO Flag


rem importing LoadAndPrepareNaptanAndNPTG.xml using dataimporter.exe
	echo *** importing LoadAndPrepareNaptanAndNPTG.xml using dataimporter.exe *** >> %OUTPUT_FILE% 
	D:\gateway\bin\utils\naptannptg\dataimporter\dataimporter.exe -c "D:\Gateway\bin\Utils\NaPTANNPTG\DataImporter\Configuration\LoadAndPrepareNaptanAndNPTG.xml" 
	if errorlevel 1 goto failure
	echo *** LoadAndPrepareNaptanAndNPTG.xml imported *** >> %OUTPUT_FILE% 
echo ************************************************************************************ >> %OUTPUT_FILE%

:success
rem set the date and time stamps for the finishing message
for /f "tokens=2-4 delims=/ " %%a in ('Date /t') do set Date=%%a/%%b/%%c
for /f "tokens=1-2 delims=: " %%a in ('Time /t') do set Time=%%a:%%b
rem success message
echo ************************************************************************************ >> %OUTPUT_FILE%
echo %Date% %Time% adhoc NPTG load completed successfully >> %OUTPUT_FILE%
	rename d:\Gateway\NPTG_Adhoc.NO NPTG_Adhoc.YES

goto end

:Flag
echo incorrect naptan import flag detected - previous import stage not completed >> %OUTPUT_FILE%
echo incorrect naptan import flag detected - previous import stage not completed 

:failure
rem set the date and time for the finishing message
for /f "tokens=2-4 delims=/ " %%a in ('Date /t') do set Date=%%a/%%b/%%c
for /f "tokens=1-2 delims=: " %%a in ('Time /t') do set Time=%%a:%%b
rem failure message

echo ************************************************************************************>> %OUTPUT_FILE%
echo %Date% %Time% nptg_adhoc_load.bat failed with errorlevel: %errorlevel% >> %OUTPUT_FILE%
echo See previous messages to see where the failure occurred. >> %OUTPUT_FILE%


goto end


:end

rem rename the log file using the current date
Set CURRDATE=%TEMP%\CURRDATE.TMP
DATE /T > %CURRDATE%
Set PARSEARG="eol=; tokens=1,2,3,4* delims=/, "
For /F %PARSEARG% %%i in (%CURRDATE%) Do SET YYYYMMDD=%%l%%k%%j
rename D:\Gateway\log\dft1_NPTGAdhoc.log dft1_NPTGAdhoc_%YYYYMMDD%.log



