:: ______________________________________________________________________________________________________________
::
::  Batch File:      	dft.bat
::  Author:          	Tushar Karsan     
::
::  Built/Tested On: 	Windows 2000 Advanced Server
::
::  Purpose:  	     	Used to process the weekly Naptan and Nptg data imports.
::		     	This batch file is called by a TNG job but can also be run manually.	
::
::  Last Update:     	Joe Morrissey 13/04/2004 - added new Esri automated processes
::			Anthony Coward 19/04/2005 - amended process flow to perform NPTG import into GAZ_Staging database
::			Anthony Coward 11/05/05 - NPTG Import into Staging removed from batch process due to problems
::						  with load when submitted by TNG. Needs to be run manually. 
::			Steve Craddock 05/07/05 - added NV 7 sections & additional comments
::                                                added reload option to bypass file transfer section
::                                                added test option to test
::						  amended dft.log to include start date & start time
::					          removed naptan check as this table is NOT required by NV7
::						  updated logging to record time stamps in log
::			Steve Craddock 20/07/05 - removed old gaz feed and added SINGLE replacement feed
::			Steve Craddock 22/07/05 - moved ynx354 file copy process into the NPTG.bat
::						- moved qxj544 file copy process into the qxj544.bat
::						- moved vfd300 file copy process into the NaPTANxml.bat
::			Steve Craddock 2/02/07  - added tidy up for TDP_MAPS folder 
::						- add mapnaptanloader for new db
::						- add naptanviewer dataloader for new db 
::						- add MapLocalitiesLoaderfor new db
::			Steve craddock 17/03/07 - added split for operation in ACP 
::                      John Frank     31/01/08 - added section to load jmd265 import at start
::			Richard Broddle 03/06/08 - added 207 code override at end for NPTG data to improve vias
::			Richard Broddle 10/02/10 - Updated from ACP - GA01 following change C2524223
::
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
	set output_file= D:\Gateway\log\dft%currdate%_%currTime%.log

	echo dft.bat import started %time%  >> %OUTPUT_FILE%

:: check user args
::	echo %date% %time% arg 1 = %1% >> %OUTPUT_FILE%
::
::	if "%1" == "" goto normal
::	if %1 == reload goto reload
::	if %1 == test goto success
::
::	echo cannot interpret arg 1
::	echo cannot interpret arg 1 >> %OUTPUT_FILE%
::
::	goto failure

:normal

echo about to run full import process


::
::
:: the following section moves / unpacks the data files in readiness for the import processes.
::
::

:: recycle TTBO service on the gateway to ensure it is operating correctly

	echo %date% %time% REcycle of TTBO starting  >> %OUTPUT_FILE%
	
	net stop "ttbo interface hosting service" >> %OUTPUT_FILE%
	net start "ttbo interface hosting service" >> %OUTPUT_FILE%

	echo %date% %time% Recycle of TTBO finished   >> %OUTPUT_FILE%	

:: Import NaPTAN / NPTG Import files from FTP server


echo tidy esri holding folders >> %OUTPUT_FILE%

	echo %date% %time% Deleting old import data files from ESRI folder gaz\nptg\   >> %OUTPUT_FILE%
	del D:\dataload\gaz\nptg\*.* /Q

	echo %date% %time% Deleting old import data files from ESRI folder gaz\naptan\   >> %OUTPUT_FILE%
	del D:\dataload\gaz\naptan\*.* /Q

	echo %date% %time% Deleting old import data files from ESRI folder osmap\naptan\  >> %OUTPUT_FILE% 
	del D:\dataload\osmap\naptan\*.* /Q

	echo %date% %time% Deleting old import data files from ESRI folder TDP_MAPS\naptan\   >> %OUTPUT_FILE%
	del D:\dataload\TDP_MAPS\naptan\*.* /Q

echo start jmd265

::
:: This runs the jmd265 import which loads data into GAZ_Stagings dft_places table if there is a 
:: newer version of the data.  The newer version may come from a datafeed to the GAZ database.
:: If new data is loaded the the ESRI importer is run for POI's.

	echo ************************************************************************************   	>> %OUTPUT_FILE%
	echo %date% %time% JMD265 import starting   	>> %OUTPUT_FILE%
	call D:\Gateway\bat\jmd265.bat %1
	echo %date% %time% JMD265 import ended   	>> %OUTPUT_FILE%
	echo ************************************************************************************   	>> %OUTPUT_FILE%

echo 1

::
:: This runs the NPTG.bat file which calls the td.NPTGImport.exe and inserts data into the NPTG_Staging DB
:: and logs into D:\Gateway\log\NPTG\ once completed the data files are copied in to D:\dataload\gaz\nptg for
:: the ESRI importer below.

	echo ************************************************************************************ 	>> %OUTPUT_FILE%
	echo %date% %time% YNX354 NPTG import starting  >> %OUTPUT_FILE%
	call D:\Gateway\bat\importA.bat ynx354 %1
	echo NPTG update (import id - ynx354) finished with Return Code %ErrorLevel%. Any non zero return code is an error.  >> %OUTPUT_FILE%
	if ErrorLevel 1 GoTo failure
	echo %date% %time% YNX354 NPTG import ended  							>> %OUTPUT_FILE%
	echo ************************************************************************************  	>> %OUTPUT_FILE%
:marker
echo 2

::
:: This runs the QXJ544.bat dummy process to bring the files from the ftp server into holding folder
:: once completed the data files are copied in to D:\dataload\gaz\naptan for the ESRI importer below.
:: files required by the ESRI LoadAndPrepareNaptanAndNPTG.xml
:: 
	echo ************************************************************************************ 
	echo %date% %time% QXJ544 NapTAN csv import starting >> %OUTPUT_FILE%
	call D:\Gateway\bat\importA.bat qxj544 %1 >> %OUTPUT_FILE%
	echo %date% %time% QXJ544 NapTAN csv update (import id - qxj544) finished with Return Code %ErrorLevel%. Any non zero return code is an error.  >> %OUTPUT_FILE%
	if ErrorLevel 1 GoTo failure
	echo ************************************************************************************  >> %OUTPUT_FILE%
echo 3

::
:: This runs the NaPTANxml.bat which loads data into the AdditionalData_staging DB using naptan.xml
:: logging info is written to D:\Gateway\log\AdditionalData\
:: The first step is to copy the data file Naptan.xml in to D:\dataload\osmap\naptan for the 
:: ESRI importer mapnaptanloader below
::
	echo ************************************************************************************ 
	echo %date% %time% VFD300 NapTAN xml import starting  >> %OUTPUT_FILE%
	call D:\Gateway\bat\importA.bat vfd300 %1
	echo %date% %time% NapTAN xml update (user id - vfd300) finished with Return Code %ErrorLevel%. Any non zero return code is an error.  >> %OUTPUT_FILE%
	if ErrorLevel 1 GoTo failure
	echo ************************************************************************************ >> %OUTPUT_FILE%
echo 4 end of vfd300 >> %OUTPUT_FILE%

echo 4

:: This whole section is manually run using NPTG_Adhoc_Load.bat due to missing row import 
:: problems when run using TNG
::
::
:: loading data into GAZ from the NAPTAN and NPTG csv import files 
:: NPTG files are copied into D:\dataload\gaz\nptg by NPTG.bat (import YNX354) process above
::
:: recreates and populates the gaz tables ie gzw---
:: does not take data from any other database object
:: does not use any esri geographic services
:: codegaz MUST be last....
::
::echo x
::	echo %date% %time% NPTG load into GAZ_Staging >> %OUTPUT_FILE%
::	echo ************************************************************************************ >> %OUTPUT_FILE%
::	echo %date% %time% importing LoadAndPrepareNaptanAndNPTG.xml using dataimporter.exe  >> %OUTPUT_FILE%
::	echo *** importing nptgdataprepare.xml using dataimporter.exe ***   >> %OUTPUT_FILE%
::	D:\gateway\bin\utils\naptannptg\dataimporter\dataimporter.exe -c "D:\gateway\bin\utils\naptannptg\dataimporter\Configuration\LoadAndPrepareNaptanAndNPTG.xml" >> %OUTPUT_FILE%
::	if errorlevel 1 goto failure
::
::	echo %date% %time%  *** Configuration/LoadAndPrepareNaptanAndNPTG.xml imported ***   >> %OUTPUT_FILE%
::
::	echo ************************************************************************************  >> %OUTPUT_FILE%
:: echo y

echo 5

ECHO END OF FILE PREPARATION SECTION  %date% %time% >> %OUTPUT_FILE%

:success
rem set the date and time stamps for the finishing message
	for /f "tokens=2-4 delims=/ " %%a in ('Date /t') do set Date=%%a/%%b/%%c
	for /f "tokens=1-2 delims=: " %%a in ('Time /t') do set Time=%%a:%%b

rem set Import flag to YES for 2nd part of load
	echo Import flag changed to NaPTAN_Import.Yes >> %OUTPUT_FILE% 
	rename d:\Gateway\NaPTAN_Import.NO NaPTAN_Import.YES

rem success message
	echo ************************************************************************************ >> %OUTPUT_FILE%
	echo %Date% %Time% dft.bat completed successfully >> %OUTPUT_FILE%
	goto end


::local_failure
::	echo Localities information not found as expected - check NPTG import phase has run successfully >> %OUTPUT_FILE%
::	echo ************************************************************************************ >> %OUTPUT_FILE%

::	goto failure


:failure
rem set the date and time for the finishing message
	for /f "tokens=2-4 delims=/ " %%a in ('Date /t') do set Date=%%a/%%b/%%c
	for /f "tokens=1-2 delims=: " %%a in ('Time /t') do set Time=%%a:%%b
rem failure message
	echo ************************************************************************************>> %OUTPUT_FILE%
	echo %Date% %Time% dft.bat failed with errorlevel: %errorlevel% >> %OUTPUT_FILE%
	echo See previous messages to see where the failure occurred. >> %OUTPUT_FILE%

:end






