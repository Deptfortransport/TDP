@echo off
@echo Creating Reporting Database...
osql -Esa -S%TDP_DB_NAME% -iCreateReportingDatabase.sql
if errorlevel 1 goto bomb1 
@echo .
@echo Database Created
@echo **************************************************

@echo Creating Reporting Database Tables...
osql -Esa -S%TDP_DB_NAME% -iCreateReportingDatabaseTables.sql
if errorlevel 1 goto bomb2
@echo .
@echo Tables Created
@echo **************************************************

goto finish

:bomb1
@echo Creating Reporting Database - Process failed, exiting setup
goto end

:bomb2
@echo Creating Reporting Tables - Process failed, exiting setup
goto end

:finish
@echo .
@echo ReportingDatabase Installation has finished
@echo **************************************************

:end
@echo 