@echo off
@echo Creating TransientPortal Database...
osql -Esa -S%TDP_DB_NAME% -iCreateTransientPortalDatabase.sql
if errorlevel 1 goto bomb1 
@echo .
@echo Database Created
@echo **************************************************

@echo Creating TransientPortal Database Tables...
osql -Esa -S%TDP_DB_NAME% -iCreateTransientPortalDatabaseTables.sql
if errorlevel 1 goto bomb2
@echo .
@echo Tables Created
@echo **************************************************

goto finish

:bomb1
@echo Creating TransientPortal Database - Process failed, exiting setup
goto end

:bomb2
@echo Creating TransientPortal Tables - Process failed, exiting setup
goto end

:finish
@echo .
@echo TransientPortal Database Installation has finished
@echo **************************************************

:end
@echo 