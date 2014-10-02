@echo off
@echo Creating PermanentPortal Database...
osql -Esa -S%TDP_DB_NAME% -iCreatePermanentPortalDatabase.sql
if errorlevel 1 goto bomb1 
@echo .
@echo PermanentPortal Database Created
@echo **************************************************

@echo Creating DataServicesTables...
osql -Esa -S%TDP_DB_NAME% -iCreateDataServicesTables.sql
if errorlevel 1 goto bomb2
@echo .
@echo DataServicesTable Created
@echo **************************************************

@echo Creating FTPServerConfigurationTables...
osql -Esa -S%TDP_DB_NAME% -iCreateFTPServerConfigurationTables.sql
if errorlevel 1 goto bomb3
@echo .
@echo FTPServerConfigurationTables Created
@echo **************************************************

@echo Creating ReportDataStagingTables...
osql -Esa -S%TDP_DB_NAME% -iCreateReportDataStagingTables.sql
if errorlevel 1 goto bomb4
@echo .
@echo ReportDataStagingTables Created
@echo **************************************************

@echo Creating PropertyServiceTables...
osql -Esa -S%TDP_DB_NAME% -iCreatePropertyServiceTables.sql
if errorlevel 1 goto bomb5
@echo .
@echo PropertyServiceTables Created
@echo **************************************************


goto finish

:bomb1
@echo Creating PermanentPortal Database - Process failed, exiting setup
goto end

:bomb2
@echo Creating DataServices Tables - Process failed, exiting setup
goto end

:bomb3
@echo Creating FTPServerConfigurationTables - Process failed, exiting setup
goto end

:bomb4
@echo Creating ReportDataStagingTables - Process failed, exiting setup
goto end

:bomb5
@echo Creating PropertyServiceTables - Process failed, exiting setup
goto end


:finish
@echo .
@echo Permanent Portal Database Installation has finished
@echo **************************************************

:end
@echo 