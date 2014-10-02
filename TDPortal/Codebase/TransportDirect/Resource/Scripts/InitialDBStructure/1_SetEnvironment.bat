@echo off
if "%1"=="" echo "Usage: SetEnvironment.bat ACP|BBP" && goto end

@echo Creating environment table in master database...
osql -Esa -S%TDP_DB_NAME% -iCreateEnvironmentSettingsfor%1.sql
if errorlevel 1 goto bomb1 
@echo .
@echo Environment set for %1.
@echo **************************************************

goto finish

:bomb1
@echo Set Environment - Process failed, exiting setup
goto end

:finish
@echo .
@echo SetEnvironment has been configured
@echo **************************************************

:end
@echo