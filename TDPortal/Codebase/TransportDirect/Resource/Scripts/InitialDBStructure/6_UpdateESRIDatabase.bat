@echo off
@echo Updating Mastermap Database...
osql -Esa -S%TDP_DB_NAME% -iUpdateMastermap.sql
if errorlevel 1 goto bomb1 
@echo .
@echo Database Updated
@echo **************************************************

goto finish

:bomb1
@echo Updating Mastermap Database - Process failed, exiting setup
goto end

:finish
@echo .
@echo Mastermap Update has finished
@echo **************************************************

:end
@echo 