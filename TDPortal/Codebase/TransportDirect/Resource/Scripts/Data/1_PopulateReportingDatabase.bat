@echo off
@echo Populating Reporting Database Tables...
osql -Esa -S%TDP_DB_NAME% -iPopulateReportingDatabaseTables.sql
if errorlevel 1 goto bomb1 
@echo .
@echo Reporting database tables populated
@echo **************************************************

goto finish

:bomb1
@echo Populating Reporting Database Tables - Process failed, exiting setup
goto end

:finish
@echo .
@echo Reporting database update has finished
@echo **************************************************

:end
@echo