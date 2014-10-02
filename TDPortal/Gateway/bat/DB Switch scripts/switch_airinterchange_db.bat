:: __________________________________________________________________
::
::  Batch File:      switch_airinterchange_db.bat
::  Author:          Atos Origin
::
::  Built/Tested On: Windows 2000 Advanced Server
::
::  Purpose:		Used to switch temp databases to live after the air interchange (IF061) data imports.
::					This batch file is called by a TNG job.		
::
::  Last Update:    Christopher Hosegood 28/05/2004
:: __________________________________________________________________
::

@echo off

rem set the name of the log file
set output_file=D:\Gateway\log\AirInterchange\switchAirDB.log

rem set the date and time for the starting message
for /f "tokens=2-4 delims=/ " %%a in ('Date /t') do set Date=%%a/%%b/%%c
for /f "tokens=1-2 delims=: " %%a in ('Time /t') do set Time=%%a:%%b
echo %Date% %Time% *** Air Database switching commencing ***>> %OUTPUT_FILE%
echo ************************************************************************************ >> %OUTPUT_FILE%

rem switch the AirInterchange database
osql -E  -n -SD03 -iswitch_airinterchangedb.sql >> %OUTPUT_FILE%
if errorlevel 1 goto failure
echo *** AirInterchange database switched successfully *** >> %OUTPUT_FILE%

:success
rem set the date and time stamps for the finishing message
for /f "tokens=2-4 delims=/ " %%a in ('Date /t') do set Date=%%a/%%b/%%c
for /f "tokens=1-2 delims=: " %%a in ('Time /t') do set Time=%%a:%%b
rem success message
echo ************************************************************************************ >> %OUTPUT_FILE%
echo %Date% %Time% Database switching completed successfully ***>> %OUTPUT_FILE%

goto end

:failure
rem set the date and time for the finishing message
for /f "tokens=2-4 delims=/ " %%a in ('Date /t') do set Date=%%a/%%b/%%c
for /f "tokens=1-2 delims=: " %%a in ('Time /t') do set Time=%%a:%%b
rem failure message
echo ************************************************************************************>> %OUTPUT_FILE%
echo %Date% %Time% switch_airinterchange_db.bat failed with errorlevel: %errorlevel% >> %OUTPUT_FILE%
echo See previous messages to see where the failure occurred. >> %OUTPUT_FILE%

rem rename the log file using the current date
Set CURRDATE=%TEMP%\CURRDATE.TMP
DATE /T > %CURRDATE%
Set PARSEARG="eol=; tokens=1,2,3,4* delims=/, "
For /F %PARSEARG% %%i in (%CURRDATE%) Do SET YYYYMMDD=%%l%%k%%j
rename D:\Gateway\log\AirInterchange\switchAirDB.log switchAirDB%YYYYMMDD%.log

:end