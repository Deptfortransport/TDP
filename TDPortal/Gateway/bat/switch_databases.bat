:: __________________________________________________________________
::
::  Batch File:      switch_databases.bat
::  Author:          Joe Morrissey     
::
::  Built/Tested On: Windows 2000 Advanced Server
::
::  Purpose:  	     Used to switch temp databases to live after the weekly Naptan and Nptg data imports.
::		     This batch file is called by a TNG job.		
::
::  Last Update:     
::
::  Joe Morrissey 14/04/2004
::  Steve Craddock 20/07/05 - updated logging,timestamps and general layout
::  Anthony Coward 05/07/06 - updated to include full sql file locations to stop execution
::			      problems when being run by TNG scheduled task
::  Steve Craddock 05/02/07 - updated for new ARC9 databases.
::  Rich Broddle   04/09/12 - updated for sjp style Gaz generation.
:: __________________________________________________________________
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
	set output_file= D:\Gateway\log\dft%currdate%_%currTime%.log

	echo import started %time%  >> %OUTPUT_FILE%
	echo ************************************************************************************ >> %OUTPUT_FILE%

	rem switch the additionaldata database
	osql -E  -n -SD03 -i"D:\Gateway\bat\DB Switch scripts\switch_additionaldatadb.sql" >> %OUTPUT_FILE%
	if errorlevel 1 goto failure
	
	echo *** AdditionalData database switched successfully *** >> %OUTPUT_FILE%

:: switch the naptan database

	osql -E  -n -SD03 -i"D:\Gateway\bat\DB Switch scripts\switch_nptgdb.sql" >> %OUTPUT_FILE%
	if errorlevel 1 goto failure

	echo *** NPTG database switched successfully *** >> %OUTPUT_FILE%
 
:: switch the GAZ database

	osql -E  -n -SD03 -i"D:\Gateway\bat\DB Switch scripts\switch_GAZdb.sql" >> %OUTPUT_FILE%
	if errorlevel 1 goto failure

	echo *** GAZ database switched successfully *** >> %OUTPUT_FILE%
	
	rem Update Gaz location cache version number in Atos Additional Data & trigger change notification for web servers.
	rem THIS MUST ONLY HAPPEN IF REST OF NAPTAN LOAD RAN OK AND MUST BE DONE BY DB SWITCH SCRIPT!!!!!!!!!!!!
	echo getting current locations version  >> %OUTPUT_FILE%
	set LocationsVersion=0
	for /f %%a in ('sqlcmd -E -Sd03 /d AtosAdditionalData -Q "exec GetLocationsVersion"') do set LocationsVersion=%%a 
	IF %ERRORLEVEL% NEQ 0 (set result = %errorlevel% goto failure ) 
	echo LocationsVersion=%LocationsVersion%  >> %OUTPUT_FILE%
	rem - setting next version
	echo setting next locations version  >> %OUTPUT_FILE%
	set /a NewLocationsVersion=LocationsVersion+1 >> %OUTPUT_FILE%
	IF %ERRORLEVEL% NEQ 0 (set result = %errorlevel% goto failure ) 
	echo echo NewLocationsVersion=%NewLocationsVersion% - updating change notification >> %OUTPUT_FILE%
	sqlcmd -E -Sd03 /d AtosAdditionalData -Q "exec UpdateLocationsVersion @NotificationTable='LocationCache', @Version=N'%NewLocationsVersion%'"
	IF %ERRORLEVEL% NEQ 0 (set result = %errorlevel% goto failure ) 

:success

:: set the date and time stamps for the finishing message
:: success message
	echo ************************************************************************************ >> %OUTPUT_FILE%
	echo %Date% %Time% Database switching completed successfully ***>> %OUTPUT_FILE%

goto end

:failure

:: set the date and time for the finishing message
:: failure message

	echo ************************************************************************************>> %OUTPUT_FILE%
	echo %Date% %Time% switch_databases.bat failed with errorlevel: %errorlevel% >> %OUTPUT_FILE%
	echo See previous messages to see where the failure occurred. >> %OUTPUT_FILE%

:end
pause

