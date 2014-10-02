rem __________________________________________________________________
rem
rem  Batch File:      ImportGazDataAndGenerateJsFiles.bat
rem  Author:          Rich Broddle    
rem
rem  Built/Tested On: Windows 2008 
rem
rem  Purpose:  	     See full description below
rem
rem  Last Update:     
rem
rem  Rich Broddle   04/09/12 - created for sjp style Gaz generation.
rem __________________________________________________________________
rem
rem 
rem This script carries out the following tasks:
rem 
rem	1. Obtain & QA latest available feed versions for applicable feeds, and aggregate on Gateway (eg D:\Gateway\LiveSupportData\DropDownGazData\ or something)
rem Files required are:
rem a) mlp069 filtering feed files 
rem b) ynx354 data feed files : Localities.csv, Exchange Groups.csv ,Coach Exchanges.csv, FerryExchanges.csv and Rail Exchanges.csv
rem c) qxj544 data feed : Stops.csv
rem d) wsa980 alias data 
rem e) dtl669 POI data (added for del 12.2)
rem 
rem 2.	Call Rob’s OlympicGazetteerLoader on the above files to load data into GAZ_Staging.TDLocations as per SC email to RA 28/08/12
rem 
rem 3.	Get next available version number (from Atos Additional Data) for .js files
rem 
rem 4.  Call LocationJSGenerator app to generate .js files to shared area i.e. FS maps share.
rem
rem 5.	Housekeep all FS maps share Gaz .js files older than above version number minus 3. (we can copy manually to \tempscripts on web servers for 1st build so it works)
rem 
rem 

@echo off

echo Running..

rem set up log file name

set currDate=
set currTime=
set time=
set timenow=
set date=

echo getting date and time..

echo get todays date for use later..
rem get todays date for use later
	for /F "tokens=1,2,3 delims=/ " %%i in ('date /t') do (
	set Day=%%i
	set Month=%%j
	set Year=%%k
	set currDate=%%k%%j%%i
	)

echo get current time for use later..
rem get current time for use later

	for /F "tokens=1-2 delims=: " %%i in ('time /t') do (
	set xcopyhour=%%i
	set xcopymins=%%j
	set currTime=%%i%%j
	)

rem set the name of the log file
echo Setting the name of the log file..

	set SJPGAZ_OUTPUT_FILE=D:\Gateway\log\SjpStyleGaz%currdate%_%currTime%.log	

	echo %Date% %Time% Sjp Style Gaz Import started   >> %SJPGAZ_OUTPUT_FILE%

set result = 0

rem	1. Obtain & QA latest available feed versions for applicable feeds, and aggregate on Gateway (eg D:\Gateway\LiveSupportData\DropDownGazData\ or something)

echo Getting feed files

set Ifeedname=mlp069
for /f %%a in ('sqlcmd -E -Sd03 /d PermanentPortal -Q "exec GetLatestDataFeedFileName @FeedName=%Ifeedname%" ') do set fileName=%%a 
echo mlp069 filename is = %fileName%
echo Unzipping data file %fileName% >> %SJPGAZ_OUTPUT_FILE%
7z.exe e D:\gateway\dat\holding\mlp069\%fileName% -aoa -oD:\dataload\gaz\SjpStyleGaz >> %SJPGAZ_OUTPUT_FILE%
IF %ERRORLEVEL% NEQ 0 (set result = %errorlevel% goto Error ) 

set Ifeedname=avf956
for /f %%a in ('sqlcmd -E -Sd03 /d PermanentPortal -Q "exec GetLatestDataFeedFileName @FeedName=%Ifeedname%" ') do set fileName=%%a 
echo avf956 filename is = %fileName%
echo Unzipping data file %fileName% >> %SJPGAZ_OUTPUT_FILE%
7z.exe e D:\gateway\dat\holding\avf956\%fileName% -aoa -oD:\dataload\gaz\SjpStyleGaz >> %SJPGAZ_OUTPUT_FILE%
IF %ERRORLEVEL% NEQ 0 (set result = %errorlevel% goto Error ) 

set Ifeedname=ynx354
for /f %%a in ('sqlcmd -E -Sd03 /d PermanentPortal -Q "exec GetLatestDataFeedFileName @FeedName=%Ifeedname%" ') do set fileName=%%a 
echo ynx354 filename is = %fileName%
echo Unzipping data file %fileName% >> %SJPGAZ_OUTPUT_FILE%
7z.exe e D:\gateway\dat\holding\ynx354\%fileName% -aoa -oD:\dataload\gaz\SjpStyleGaz >> %SJPGAZ_OUTPUT_FILE%
IF %ERRORLEVEL% NEQ 0 (set result = %errorlevel% goto Error ) 

set Ifeedname=qxj544
for /f %%a in ('sqlcmd -E -Sd03 /d PermanentPortal -Q "exec GetLatestDataFeedFileName @FeedName=%Ifeedname%" ') do set fileName=%%a 
echo qxj544 filename is = %fileName%
echo Unzipping data file %fileName% >> %SJPGAZ_OUTPUT_FILE%
7z.exe e D:\gateway\dat\holding\qxj544\%fileName% -aoa -oD:\dataload\gaz\SjpStyleGaz >> %SJPGAZ_OUTPUT_FILE%
IF %ERRORLEVEL% NEQ 0 (set result = %errorlevel% goto Error ) 

set Ifeedname=dtl669
for /f %%a in ('sqlcmd -E -Sd03 /d PermanentPortal -Q "exec GetLatestDataFeedFileName @FeedName=%Ifeedname%" ') do set fileName=%%a 
echo dtl669 filename is = %fileName%
echo Unzipping data file %fileName% >> %SJPGAZ_OUTPUT_FILE%
7z.exe e D:\gateway\dat\holding\dtl669\%fileName% -aoa -oD:\dataload\gaz\SjpStyleGaz >> %SJPGAZ_OUTPUT_FILE%
IF %ERRORLEVEL% NEQ 0 (set result = %errorlevel% goto Error ) 

rem 2.	Call OlympicGazetteerLoader on the above files to load data into GAZ_Staging.TDLocations
echo calling D:\Gateway\bin\Utils\OlympicsGazetteerLoader\LoadAndProcessNonPostcodes.bat  >> %SJPGAZ_OUTPUT_FILE%

call D:\Gateway\bin\Utils\OlympicsGazetteerLoader\LoadAndProcessNonPostcodes.bat  >> %SJPGAZ_OUTPUT_FILE%
IF %ERRORLEVEL% NEQ 0 (set result = %errorlevel% goto Error ) 

echo LoadAndProcessNonPostcodes.bat completed  >> %SJPGAZ_OUTPUT_FILE%


rem 3.	Get next available version number (from Atos Additional Data) for .js files & call LocationJSGenerator app with it to generate .js files to shared area i.e. FS maps share.
	rem - getting current version
echo getting current locations version  >> %SJPGAZ_OUTPUT_FILE%
set LocationsVersion=0
for /f %%a in ('sqlcmd -E -Sd03 /d AtosAdditionalData -Q "exec GetLocationsVersion"') do set LocationsVersion=%%a 
IF %ERRORLEVEL% NEQ 0 (set result = %errorlevel% goto Error ) 
echo LocationsVersion=%LocationsVersion%  >> %SJPGAZ_OUTPUT_FILE%

	rem - setting next version
echo setting next locations version  >> %SJPGAZ_OUTPUT_FILE%
set /a NewLocationsVersion=LocationsVersion+1 >> %SJPGAZ_OUTPUT_FILE%
IF %ERRORLEVEL% NEQ 0 (set result = %errorlevel% goto Error ) 
echo echo NewLocationsVersion=%NewLocationsVersion% >> %SJPGAZ_OUTPUT_FILE%


rem 4.	Call LocationJSGenerator app to generate .js files to shared area i.e. FS maps share.
cd D:\Gateway\bin\Utils\LocationJsGenerator\
td.userportal.locationjsgenerator.exe /v %NewLocationsVersion%


rem 5.	COPY TO WEB SHARE and Housekeep all FS maps share Gaz .js files older than above version number – 3. (we can copy manually to \tempscripts on web servers for 1st build so it works)

rem work out housekeep version and delete from local and share
set /a HousekeepLocationsVersion=NewLocationsVersion-3  >> %SJPGAZ_OUTPUT_FILE%
echo echo HousekeepLocationsVersion=%HousekeepLocationsVersion%  >> %SJPGAZ_OUTPUT_FILE%
rem housekeep local
echo deleting D:\dataload\gaz\SjpStyleGaz\OutputScripts\Location_..._%HousekeepLocationsVersion%_?.js files
del D:\dataload\gaz\SjpStyleGaz\OutputScripts\Location_%HousekeepLocationsVersion%_?.js
del D:\dataload\gaz\SjpStyleGaz\OutputScripts\Location_NoGroups_%HousekeepLocationsVersion%_?.js
del D:\dataload\gaz\SjpStyleGaz\OutputScripts\Location_NoGroupsNoLocalities_%HousekeepLocationsVersion%_?.js
del D:\dataload\gaz\SjpStyleGaz\OutputScripts\Location_NoGroupsNoLocalitiesNoPOIs_%HousekeepLocationsVersion%_?.js
del D:\dataload\gaz\SjpStyleGaz\OutputScripts\Location_NoLocalitiesNoPOIs_%HousekeepLocationsVersion%_?.js
rem housekeep share
echo deleting \\FS03.acpTDPW.LOCAL\Maps\Javascript\Location_..._%HousekeepLocationsVersion%_?.js files
del \\FS03.acpTDPW.LOCAL\Maps\Javascript\Location_%HousekeepLocationsVersion%_?.js
del \\FS03.acpTDPW.LOCAL\Maps\Javascript\Location_NoGroups_%HousekeepLocationsVersion%_?.js
del \\FS03.acpTDPW.LOCAL\Maps\Javascript\Location_NoGroupsNoLocalities_%HousekeepLocationsVersion%_?.js
del \\FS03.acpTDPW.LOCAL\Maps\Javascript\Location_NoGroupsNoLocalitiesNoPOIs_%HousekeepLocationsVersion%_?.js
del \\FS03.acpTDPW.LOCAL\Maps\Javascript\Location_NoLocalitiesNoPOIs_%HousekeepLocationsVersion%_?.js
rem - now copy new files to share
copy /Y "D:\dataload\gaz\SjpStyleGaz\OutputScripts\*.*" "\\FS03.acpTDPW.LOCAL\Maps\Javascript\"
IF %ERRORLEVEL% NEQ 0 (
    rem ignore copy file errors on test environments as they will not work for now
    IF %userdomain% EQU ACPTDPS (set result = %errorlevel% goto HousekeepError ) 
		)

:Success
echo Success
goto end

:Error
echo %Time% ImportGazDataAndGenerateJsFiles.bat failed with errorlevel: %result% >> %SJPGAZ_OUTPUT_FILE%
goto end

:HousekeepError
echo %Time% ImportGazDataAndGenerateJsFiles.bat failed housekeeping and updating js files with errorlevel: %result% >> %SJPGAZ_OUTPUT_FILE%
goto end

:end
cd D:\gateway\bat
echo %Time% ImportGazDataAndGenerateJsFiles.bat completed>> %SJPGAZ_OUTPUT_FILE%

