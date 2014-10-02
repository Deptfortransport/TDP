:: ______________________________________________________________________________________________________________
::
::  Batch File:      dft1.bat
::  Author:          Steve Craddock     
::
::  Built/Tested On: Windows 2000 Advanced Server
::
::  Purpose:  	     Used to process the second half of the weekly Naptan and Nptg data imports.
::		     This batch file is basically the secodn half of the dft.bat batch file that is run 
::		     at BBP. The first half loads data into the Add' data, NPTG and GAZ staging tables and it may 
::		     be possibly to run during the day. This second half must only be run in a 'outage' window 
::		     due to the impact on the mapping databases.	
::
::  Last Update:     Steve Craddock 17/03/07 - original 
::		     Rich Broddle   22/05/07 - added call to secondary traveline regions importer at end
::		     Rich Broddle   04/09/08 - added section to add 207 unsupported codes at end
::		     Rich Broddle   10/02/10 - added section to Update TDP_MAPS.mapsadmin.STOPS_GAZ_NAME at end - Del 10.9 mapping requirement 
::			 Rich Broddle   04/09/12 - added new call for sjp style Gaz generation.
::			 Rich Broddle   22/01/13 - added accessiblity data calls and reordered numbering to correct
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
	set output_file= D:\Gateway\log\dft1%currdate%_%currTime%.log

	echo dft1.bat import started %time%  >> %OUTPUT_FILE%

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


	echo %date% %time% Naptan and Nptg Weekly Data Update (DFT_1.bat) Starting  >> %OUTPUT_FILE%
	echo ************************************************************************************ >> %OUTPUT_FILE%

:: Verify import flag has been set
	rem check for NaPTAN_Import.No flag. Do not run if present
	if exist D:\Gateway\NaPTAN_Import.No GOTO Flag

:: Verify import flag has been set
	rem check for NPTG_Adhoc.NO flag. Do not run if present
	if exist D:\Gateway\NPTG_Adhoc.NO GOTO Flag

:load

::
::NaPTAN load process
::

echo %date% %time% NaPTAN load process for D06 starting >> %OUTPUT_FILE%

:: Loading D06 Stops featureclass 
:: which is used by both Portal and Naptanviewer
:: Updates the TDP_MAPS db from the naptan.xml file (updates sde so cannot dbswitch osmap) 
:: DOES use any esri geographic services
:: 
echo 6	
	echo %date% %time% Loading Stops featureclass to D06 >> %OUTPUT_FILE%
	D:\Gateway\bin\Utils\MapNaptanLoader\mapnaptanloader.exe d:\gateway\bin\utils\MapNaptanLoader\ApplicationProperties_newD06.xml D:\dataload\TDP_MAPS\naptan\naptan.xml >> %OUTPUT_FILE%
	if errorlevel 1 goto failure	
	echo %date% %time% *** Stops featureclass loaded into D06 with Return Code %ErrorLevel% ***   >> %OUTPUT_FILE%
	echo ************************************************************************************ >> %OUTPUT_FILE%

echo 7
::Check if GROUPID rows >50
	echo %date% %time% *** checking D06 OSMap database rows where groupid instances are over 50 *** >> %OUTPUT_FILE% 
	osql -E  -n -Sd06 -i"D:\Gateway\bat\ExcessRows.sql" >> %OUTPUT_FILE%
	if errorlevel 1 goto failure
	echo %date% %time% *** D06 GROUPID rows checked and excess ones deleted *** >> %OUTPUT_FILE% 

:: importing D06 importnaptanviewer.xml using dataimporter
:: loads data into OSMAP from the NPTG csv import files
:: does not use any esri geographic services

	echo %date% %time% *** importing D06 importnaptanviewer.xml using dataimporter.exe *** >> %OUTPUT_FILE% 
	D:\gateway\bin\utils\naptannptg\dataimporter\dataimporter.exe -c "D:\Gateway\bin\Utils\NaPTANNPTG\DataImporter\Configuration\importnaptanviewer_newD06.xml"  >> %OUTPUT_FILE%
	if errorlevel 1 goto failure
	echo %date% %time% *** D06 importnaptanviewer.xml imported ***  >> %OUTPUT_FILE%
	echo %date% %time% ************************************************************************************ >> %OUTPUT_FILE%

echo 8
:: running D06 MapLocalitiesLoader
:: MUST FOLLOW dataimporter.exe importnaptanviewer.xml process AND MapnaptanLoader process
:: updates spatial data for localities
:: DOES use any esri geographic services
:: 

	echo %date% %time% *** Updating D06 localitiestable using MapLocalitiesLoader.exe ***  >> %OUTPUT_FILE%
	D:\gateway\bin\utils\MapLocalitiesLoader\MapLocalitiesLoader.exe "D:\Gateway\bin\Utils\MapLocalitiesLoader\ApplicationProperties_newD06.xml" >> %OUTPUT_FILE%
	if errorlevel 1 goto failure
	echo %date% %time% *** D06 MapLocalitiesLoader RUn Successfully ***  >> %OUTPUT_FILE%
	echo %date% %time% ************************************************************************************ >> %OUTPUT_FILE%

echo %date% %time% D06 load process completed >> %OUTPUT_FILE%


echo %date% %time% NaPTAN load process for D07 starting >> %OUTPUT_FILE%

echo %date% %time% NaPTAN load process for D07 starting >> %OUTPUT_FILE%

:: Loading D07 Stops featureclass 
:: which is used by both Portal and Naptanviewer
:: Updates the TDP_MAPS db from the naptan.xml file (updates sde so cannot dbswitch osmap) 
:: DOES use any esri geographic services
:: 
echo 9	
 	echo %date% %time% Skipping Stops featureclass to D07 >> %OUTPUT_FILE%
:: 	D:\Gateway\bin\Utils\MapNaptanLoader\mapnaptanloader.exe d:\gateway\bin\utils\MapNaptanLoader\ApplicationProperties_newD07.xml D:\dataload\TDP_MAPS\naptan\naptan.xml >> %OUTPUT_FILE%
:: 	if errorlevel 1 goto failure	
:: 	echo %date% %time% *** Stops featureclass loaded into D07 with Return Code %ErrorLevel% ***   >> %OUTPUT_FILE%
:: 	echo ************************************************************************************ >> %OUTPUT_FILE%

echo 10
::Check if GROUPID rows >50
	echo %date% %time% *** skipping checking D07 OSMap database rows where groupid instances are over 50 *** >> %OUTPUT_FILE% 
:: 	osql -E  -n -SD07 -i"D:\Gateway\bat\ExcessRows.sql" >> %OUTPUT_FILE%
:: 	if errorlevel 1 goto failure
:: 	echo %date% %time% *** D07 GROUPID rows checked and excess ones deleted *** >> %OUTPUT_FILE% 

:: importing D07 importnaptanviewer.xml using dataimporter
:: loads data into OSMAP from the NPTG csv import files
:: does not use any esri geographic services

	echo %date% %time% *** importing D07 importnaptanviewer.xml using dataimporter.exe *** >> %OUTPUT_FILE% 
:: 	D:\gateway\bin\utils\naptannptg\dataimporter\dataimporter.exe -c "D:\Gateway\bin\Utils\NaPTANNPTG\DataImporter\Configuration\importnaptanviewer_newD07.xml"  >> %OUTPUT_FILE%
:: 	if errorlevel 1 goto failure
:: 	echo %date% %time% *** D07 importnaptanviewer.xml imported ***  >> %OUTPUT_FILE%
:: 	echo %date% %time% ************************************************************************************ >> %OUTPUT_FILE%


echo 11
:: calling dataloader for accessible stops/localities - must precede call to "D:\gateway\bin\utils\MapLocalitiesLoader\MapLocalitiesLoader.exe"
D:\gateway\bin\utils\naptannptg\dataimporter\dataimporter.exe –c "D:\Gateway\bin\Utils\NaPTANNPTG\DataImporter\Configuration\LoadAccessibleLocalitiesExclusionList.xml"
D:\gateway\bin\utils\naptannptg\dataimporter\dataimporter.exe –c "D:\Gateway\bin\Utils\NaPTANNPTG\DataImporter\Configuration\LoadAccessibleStopsList.xml"
D:\gateway\bin\utils\naptannptg\dataimporter\dataimporter.exe –c "D:\Gateway\bin\Utils\NaPTANNPTG\DataImporter\Configuration\LoadAccessibleTDANStopsList.xml"

echo 12
:: running D07 MapLocalitiesLoader
:: MUST FOLLOW dataimporter.exe importnaptanviewer.xml process AND MapnaptanLoader process
:: updates spatial data for localities
:: DOES use any esri geographic services
:: 

	echo %date% %time% *** Skipping Updating D07 localitiestable using MapLocalitiesLoader.exe ***  >> %OUTPUT_FILE%
:: 	D:\gateway\bin\utils\MapLocalitiesLoader\MapLocalitiesLoader.exe "D:\Gateway\bin\Utils\MapLocalitiesLoader\ApplicationProperties_newD07.xml" >> %OUTPUT_FILE%
:: 	if errorlevel 1 goto failure
:: 	echo %date% %time% *** D07 MapLocalitiesLoader RUn Successfully ***  >> %OUTPUT_FILE%
:: 	echo %date% %time% ************************************************************************************ >> %OUTPUT_FILE%

:: echo %date% %time% D07 load process completed >> %OUTPUT_FILE%


:: load and preparenaptannptg was here for BBP


echo 13
:: running Atkins Secondary Regions Importer
:: updates NPTG localities table, adding "Secondary Traveline Region ID" column entries.

	echo %date% %time% *** Updating NPTG using Atkins Secondary Regions Importer ***   >> %OUTPUT_FILE%

D:\Gateway\bin\Utils\SecondaryRegionsImporter\SecondaryRegionsImporter.exe -i "D:\Gateway\bin\Utils\SecondaryRegionsImporter\Overlapping Region.csv" -c "Server=D03;Initial Catalog=NPTG_staging;Trusted_Connection=true;" -h 1 -l "D:\Gateway\log" >> %OUTPUT_FILE%

	if errorlevel 1 goto failure
	echo %date% %time% *** Secondary Regions Importer Run Successfully ***  	>> %OUTPUT_FILE%
	echo %date% %time% ************************************************************************************ 


echo 14

:: override NPTG Unsupported flag for code 207 via's - to force CJP to split requests in two to obtain better via results - Del 10.1 CCN457
	
	echo %date% %time% *** Over-riding 207 rows on NPTG.Unsupported table ***   >> %OUTPUT_FILE%

	for %%b in (EA EM L NE NW S SE SW W WM Y) do (
	echo %date% %time% *** Replacing 207 rows from NPTG.Unsupported for region %%b ***   >> %OUTPUT_FILE%
	osql -b -S D03 /E /Q "EXIT(USE NPTG_Staging DELETE Unsupported WHERE Capability = 207 AND [Traveline Region ID] = '%%b')" >> %OUTPUT_FILE%
	if errorlevel 1 goto failure
	osql -b -S D03 /E /Q "EXIT(USE NPTG_Staging INSERT INTO unsupported VALUES ('%%b',207,'20080530 09:00:00','20080530','001'))" >> %OUTPUT_FILE%
	if errorlevel 1 goto failure
	)

echo 15
	if errorlevel 1 goto failure
	echo %date% %time% *** NPTG Unsupported flag for code 207 via's updated successfully   ***  	>> %OUTPUT_FILE%
	echo %date% %time% ************************************************************************************ 


echo 16
:: update index stats for the additionaldata_staging database
	
	echo %date% %time% update index stats for the additionaldata_staging database started...   >> %OUTPUT_FILE%

	sqlcmd -S D03 -d AdditionalData_Staging -Q "exec sp_updatestats" >> %OUTPUT_FILE%


echo 17
	if errorlevel 1 goto failure
	eecho %date% %time% update index stats for the additionaldata_staging database completed successfully.  >> %OUTPUT_FILE%
	echo %date% %time% ************************************************************************************ 

echo 18
:: update coach exchange points using ESRI MapCoachLoader
	
echo %date% %time% update coach exchange points using ESRI MapCoachLoader started...   >> %OUTPUT_FILE%

D:\Gateway\bin\Utils\MapCoachLoader\MapCoachLoader.exe "D:\Gateway\bin\Utils\MapCoachLoader\ApplicationProperties.xml" "D:\dataload\gaz\nptg\Coach Exchanges.csv"  >> %OUTPUT_FILE%


echo 19
if errorlevel 1 goto failure
echo %date% %time% update coach exchange points using ESRI MapCoachLoader completed successfully.  >> %OUTPUT_FILE%
	echo %date% %time% ************************************************************************************

echo 20

:STOPS_GAZ_NAME

:: Update TDP_MAPS.mapsadmin.STOPS_GAZ_NAME - Del 10.9 mapping requirement 
:: This relies on the "gazRemote" linked server linking D03 and D06
	
	echo %date% %time% *** Updating TDP_MAPS.mapsadmin.STOPS_GAZ_NAME - Del 10.9 mapping requirement  ***   >> %OUTPUT_FILE%

	echo %date% %time% *** Clearing down existing mapsadmin.STOPS_GAZ_NAME contents ***   >> %OUTPUT_FILE%
	osql -b -S D06 /E /Q "EXIT(USE TDP_MAPS truncate table mapsadmin.STOPS_GAZ_NAME)" >> %OUTPUT_FILE%
	if errorlevel 1 goto failure

	echo %date% %time% *** Update mapsadmin.STOPS_GAZ_NAME contents ***   >> %OUTPUT_FILE%
	osql -b -S D06 /E /Q "EXIT(insert into TDP_MAPS.mapsadmin.STOPS_GAZ_NAME select ATCOCODE, REPORTING_NAME from gazRemote.GAZ_STAGING.gazadmin.ALLSTAT_LKP ORDER BY ATCOCODE asc)" >> %OUTPUT_FILE%
	if errorlevel 1 goto failure
echo %date% %time% Updating TDP_MAPS.mapsadmin.STOPS_GAZ_NAME - Del 10.9 mapping requirement - completed successfully.  >> %OUTPUT_FILE%
	echo %date% %time% ************************************************************************************

	
echo 21
	echo %date% %time% *** calling script to generate SJP style Gazetteer data and .js scripts ***   >> %OUTPUT_FILE%
    call D:\Gateway\Bat\ImportGazDataAndGenerateJsFiles.bat 
	if errorlevel 1 goto failure

:success


	rem success message
	echo %date% %time% dft1.bat completed successfully >> %OUTPUT_FILE%
	echo ************************************************************************************ >> %OUTPUT_FILE%

	echo %date% %time%  *** NOT *** run DB switch 
	echo %date% %time% call db_switch >> %OUTPUT_FILE%
	call d:\gateway\bat\switch_databases.bat
	echo %date% %time% *** DB Switch called - check switch.log to ensure successful run *** >> %OUTPUT_FILE% 
	
	rename d:\Gateway\NPTG_Adhoc.YES NPTG_Adhoc.NO

	goto end

:failure

	:: failure_message

	rem cawto -c red -n %1 DFT_1.bat failed on %servername% - Callout required for NFH-MRCoEN-NT

	echo ************************************************************************************>> %OUTPUT_FILE%
	echo %date% %time% dft1.bat failed with errorlevel: %errorlevel% >> %OUTPUT_FILE%
	echo See previous messages to see where the failure occurred. >> %OUTPUT_FILE%
	goto end

:Flag

	echo %date% %time% NaPTAN_Import.no or NPTG_Adhoc.NO flag detected
	echo %date% %time% Import not run because D:\Gateway\NaPTAN_Import.No or NPTG_Adhoc.NO flag detected >> %OUTPUT_FILE%
	echo ************************************************************************************>> %OUTPUT_FILE%
	goto end

:end

	echo import finished %date% %time% >> %OUTPUT_FILE%

:: tidying up variables 
	set currDate=
	set currTime=
	set time=
	set timenow=
	set date=



