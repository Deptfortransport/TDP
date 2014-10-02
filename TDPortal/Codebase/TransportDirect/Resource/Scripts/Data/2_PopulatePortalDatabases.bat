@echo off
@echo Populating Transient Portal Database Tables...
osql -Esa -S%TDP_DB_NAME% -iPopulateTransientPortalDataBaseTables.sql
if errorlevel 1 goto bomb1 
@echo .
@echo TransientPortal database tables populated
@echo **************************************************

@echo Populating Permanent Portal Database Tables...
osql -Esa -S%TDP_DB_NAME% -iPopulatePermanentPortalDataBaseTables.sql
if errorlevel 1 goto bomb2 
@echo .
@echo PermanentPortal database tables populated
@echo **************************************************


goto finish

:bomb1
@echo Populating Transient Portal Database Tables - Process failed, exiting setup
goto end

:bomb2
@echo Populating Transient Portal Database Tables - Process failed, exiting setup
goto end

:finish
@echo .
@echo TransientPortal database update has finished
@echo **************************************************

:end
@echo 