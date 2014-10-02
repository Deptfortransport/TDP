:: ______________________________________________________________________________________________________________
::
::  Batch File:   TravelNews.bat
::  Author:        Tim Mollart/Joe Morrissey   
::
::  Built/Tested On: Windows 2003 Server
::
::  Purpose:       Used to import travel news into the ESRI OSMAP database
::
::  Parameters:   None
::
::  Instructions:   Ensure all drives are correct e.g C: or D: 
::	          Ensure Gateway servers are correctly named in "Identify Gateway servers section"	
::
::  Last Update:    Joe Morrissey 22/07/05 Initial Version
::	            Joe Morrissey 27/07/05 Updated after comments from Applications Support
::	            Steve Craddock16/09/05 added echo to log calling TNG server	
::					   added goto to prevent second gateway check being run when not reqd
::				  20/09/05 added fully qualified pathing to permit TNG automation to run the job
::					   added validation for %1 parameter to record if job submitted manually.
::
::		    Steve Craddock 02/02/2007 Amended to use NEW maps db - TDP_MAPS 
::						NOTE this importer does NOT use DBM hostname because it must import to 
::						multiple mapping DB's
:: ______________________________________________________________________________________________________________
::

@echo off

echo Running..

set currDate=
set currTime=
set time=
set timenow=
set date=

set tngserv=%1

if "%1" == "" set tngserv=Manual

:: get todays date for use later
	for /F "tokens=1-4 delims=/ " %%i in ('date /t') do (
	set Day=%%j
	set Month=%%k
	set Year=%%l
	set currDate=%%l%%k%%j
	)

:: get current time for use later

	for /F "tokens=1-2 delims=: " %%i in ('time /t') do (
	set xcopyhour=%%i
	set xcopymins=%%j
	set currTime=%%i%%j
	)

:: set the name of the log file
	::set output_file= D:\Gateway\log\mapincidentsloader\mapincidentsloader%currdate%_%currTime%.log	
	set output_file= D:\TDPortal\mapincidentsloader%currdate%_%currTime%.log	

	echo %Date% %Time% Esri Travel News Import started   >> %OUTPUT_FILE%

set result = 0

:: Get name of last imported travel news data file
set storedprocedure=GetLatestTravelNewsFileName
set fileName= 
for /f %%a in ('osql  -E  -SD03 /d PermanentPortal /Q %storedprocedure%') do set fileName=%%a 
if errorlevel 1 (set result = %errorlevel% goto Error )
echo %Time% Last imported travel news data file was %fileName% >> %OUTPUT_FILE%
echo ************************************************************************************ >> %OUTPUT_FILE%

::Identify Gateway servers
set currentserver=%COMPUTERNAME%
echo Submitting TNG server is %tngserv% for this run >> %OUTPUT_FILE%
echo Current server is %currentserver% >> %OUTPUT_FILE%
if %currentserver%==GA01 set otherGateway=GA02
if %currentserver%==GA02 set otherGateway=GA01
echo %Time% Other Gateway server is %otherGateway% >> %OUTPUT_FILE%
echo ************************************************************************************ >> %OUTPUT_FILE%

::Unzipping data file for Esri from GA01 or GA02
if not exist D:\gateway\dat\holding\afc743\%fileName% goto UseOtherGateway
pkzipc -extract -overwrite=all D:\gateway\dat\holding\afc743\%fileName% D:\dataload\TDP_MAPS\incidents >> %OUTPUT_FILE%
if ErrorLevel 1 (set result = %errorlevel% goto PkZipError ) 
goto foundfile

:UseOtherGateway 
echo Trying to find data file on %otherGateway% >> %OUTPUT_FILE%
pkzipc -extract -overwrite=all \\%otherGateway%\travelnewsdata\%fileName% D:\dataload\TDP_MAPS\incidents >> %OUTPUT_FILE%
if ErrorLevel 1 (set result = %errorlevel% goto PkZipError ) 

echo %Time% Travel News xml file unzipped successfully>> %OUTPUT_FILE%
echo ************************************************************************************ >> %OUTPUT_FILE%

echo Found file....
:foundfile

cd\
cd D:\gateway\bin\utils\mapincidentsloader

:: copy file back to original location

copy D:\dataload\TDP_MAPS\incidents\*.xml D:\dataload\OSMAP\incidents\ >> %OUTPUT_FILE%


rem The map incidents loader needs to be run for each OSMAP database; once for D06 and once for D07 (or D03 on BBP)
echo Calling ESRI Import Utility to update OSMAP on D03 >> %OUTPUT_FILE%

echo doing OSMAP
echo doing OSMAP >> %OUTPUT_FILE%
"D:\gateway\bin\utils\mapincidentsloader\mapincidentsloader.exe" "D:\gateway\bin\utils\mapincidentsloader\applicationpropertiesD03.xml" "D:\dataload\OSMAP\incidents\sercoincidents.xml" >> %OUTPUT_FILE%

echo doing TDP_MAPS
echo doing TDP_MAPS >> %OUTPUT_FILE%
"D:\gateway\bin\utils\mapincidentsloader\mapincidentsloader.exe" "D:\gateway\bin\utils\mapincidentsloader\applicationpropertiesD03_NEW.xml" "D:\dataload\TDP_MAPS\incidents\sercoincidents.xml" >> %OUTPUT_FILE%


if errorlevel 7 (set result = %errorlevel%
	goto EsriErrDel)
if errorlevel 6 (set result = %errorlevel%
	goto EsriErrVer)
if errorlevel 5 (set result = %errorlevel%
	goto EsriErrInsert)
if errorlevel 4 (set result = %errorlevel%
	goto EsriErrLoad)
if errorlevel 3 (set result = %errorlevel%
	goto EsriErrLoadProp)
if errorlevel 2 (set result = %errorlevel%
	goto EsriErrWrongArgs)
if errorlevel 1 (set result = %errorlevel%
	goto EsriErrGeneral)
echo %Time% Esri's mapincidentsloader completed updating D03 >> %OUTPUT_FILE%
echo ************************************************************************************ >> %OUTPUT_FILE%

:Success
echo Success
goto end

:FileCopyError 
echo %Time% File copying error: %result% >> %OUTPUT_FILE%
cawto -c red -n %tngserv% Travel News update failed - File copy error
goto end

:PkZipError
echo %Time% PkZip failed with errorlevel: %result% >> %OUTPUT_FILE%
cawto -c red -n %tngserv% Esri Travel News update failed - PKZip error, raise Vantive for Application Support
goto end

:EsriErrDel
echo %Time% Esri's mapincidentsloader application failed with errorlevel: %result%. Error deleting old incidents. >> %OUTPUT_FILE% 
cawto -c red -n %tngserv% Esri Travel News update failed - Esri MapIncidentsLoader error deleting old incidents, raise Vantive for Application Support
goto end

:EsriErrVer
echo %Time% Esri's mapincidentsloader application failed with errorlevel: %result%. Error updating version number. >> %OUTPUT_FILE% 
cawto -c red -n %tngserv% Esri Travel News update failed - Esri MapIncidentsLoader error updating version number, raise Vantive for Application Support
goto end

:EsriErrInsert
echo %Time% Esri's mapincidentsloader application failed with errorlevel: %result%. Error inserting incidents. >> %OUTPUT_FILE% 
cawto -c red -n %tngserv% Esri Travel News update failed - Esri MapIncidentsLoader error inserting incidents, raise Vantive for Application Support
goto end

:EsriErrLoad
echo %Time% Esri's mapincidentsloader application failed with errorlevel: %result%. Error loading incidents from file. >> %OUTPUT_FILE% 
cawto -c red -n %tngserv% Esri Travel News update failed - Esri MapIncidentsLoader error loading incidents from file, raise Vantive for Application Support
goto end

:EsriErrLoadProp
echo %Time% Esri's mapincidentsloader application failed with errorlevel: %result%. Error loading properties. >> %OUTPUT_FILE% 
cawto -c red -n %tngserv% Esri Travel News update failed - Esri MapIncidentsLoader error loading properties, raise Vantive for Application Support
goto end

:EsriErrWrongArgs
echo %Time% Esri's mapincidentsloader application failed with errorlevel: %result%. Error in arguments provided to mapincidentsloader. >> OUTPUT_FILE% 
cawto -c red -n %tngserv% Esri Travel News update failed - Esri MapIncidentsLoader error in arguments provided to mapincidentsloader, raise Vantive for Application Support
goto end

:EsriErrGeneral
echo %Time% Esri's mapincidentsloader application failed with errorlevel: %result% >> %OUTPUT_FILE% 
cawto -c red -n %tngserv% Esri Travel News update failed - Esri MapIncidentsLoader unspecified error, raise Vantive for Application Support
goto end

:Error
echo %Time% EsriTravelNews.bat failed with errorlevel: %result% >> %OUTPUT_FILE%
cawto -c red -n %tngserv% Esri Travel News update failed - raise Vantive for Application Support
goto end

:end

cd D:\gateway\bat
echo %Time% EsriTravelNewsUpdate.bat completed>> %OUTPUT_FILE%



