@echo off

@echo **************************************************
@echo Executing scripts
@echo **************************************************

if "%1" EQU "/s" GOTO skippromptone

@echo Ensure that none of the databases are in use before continuing
@echo e.g. make sure that SQL Query Analyzer and Enterprise Manager are not running

pause

:skippromptone

REM @echo Performing IIS Reset
REM iisreset

@echo **************************************************
@echo MDS
@echo **************************************************

@echo Executing script MDS001
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS001_RailTicketTypes.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS002
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS002_Tip_of_the_day_data.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS003
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS003_HomepageText.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS004
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS004_AvailabilityMatrix_Update.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS005
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS005_Special_Notice_Board_Update.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS006
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS006_FindTrainPromoText.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS007
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS007_TicketCategory.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS008
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS008_TicketRouteRestrictions.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS010
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS010_AddWrexhamAndShropshire.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS011
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS011_journeycallpartner.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS012
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS012_UpdateEmissionFactors.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS013
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS013_CityToCityTables.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS014
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS014_TraveNewsDataSources.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS016
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS016_TicketTypeGroups.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS017
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS017_ZoneNLCs.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS018
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS018_EBC_Data.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS019
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS019_BankHoliday_Update.sql"
if errorlevel 1 goto bomb1

@echo Executing script MDS021
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\MDS\MDS021_Special_Event_Update.sql"
if errorlevel 1 goto bomb1

@echo
@echo **************************************************
@echo Executing scripts completed
@echo **************************************************

if "%1" EQU "/s" goto end

goto end

:bomb1

@echo Executing scripts - Process failed, exiting

if "%1" EQU "/s" goto end

pause
goto end

:end
@echo
pause
pause