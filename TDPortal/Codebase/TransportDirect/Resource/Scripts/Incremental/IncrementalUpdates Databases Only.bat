@echo off

@echo **************************************************
@echo Reinstating initial state
@echo **************************************************

if "%1" EQU "/s" GOTO skippromptone

@echo Ensure that none of the databases are in use before continuing
@echo e.g. make sure that SQL Query Analyzer and Enterprise Manager are not running
@echo Ensure the Batch service is stopped

pause

:skippromptone

@echo Performing IIS Reset
iisreset

@echo Dropping existing databases
osql -S .\SQLExpress -Esa -iLatestState\DropAllDatabases.sql
if errorlevel 1 goto bomb1

@echo Copying data files
copy "D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\LatestState\*.mdf" "C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA" /Y
if errorlevel 1 goto bomb1

@echo Attaching new data files
osql -S .\SQLExpress -Esa -iLatestState\AttachAllDatabases.sql
if errorlevel 1 goto bomb1

@echo
@echo **************************************************
@echo Reinstating initial state completed
@echo **************************************************

if "%1" EQU "/s" goto end

pause

goto end

:bomb1

@echo Executing Incremental Updates - Process failed, exiting

if "%1" EQU "/s" goto end

pause
goto end

:end
@echo
pause