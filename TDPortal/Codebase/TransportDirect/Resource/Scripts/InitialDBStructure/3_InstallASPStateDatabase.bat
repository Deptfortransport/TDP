@echo off
@echo Creating ASPState Database...
osql -Esa -S%TDP_DB_NAME% -iCreateASPStateDatabase.sql
if errorlevel 1 goto bomb1 
@echo .
@echo Database Created
@echo **************************************************

@echo Creating ASPState Database Tables...
@echo .
@echo Note creation of S_Proc usp_SaveDeferredData is expected to error during creation
@echo THIS IS NORMAL
@echo **************************************************
pause

osql -Esa -S%TDP_DB_NAME% -iCreateASPStateDatabaseTables.sql
if errorlevel 1 goto bomb2
@echo .
@echo Tables Created
@echo **************************************************

goto finish

:bomb1
@echo Creating ASPState Database - Process failed, exiting setup
goto end

:bomb2
@echo Creating ASPState Tables - Process failed, exiting setup
goto end

:finish
@echo .
@echo ASPState Installation has finished
@echo **************************************************

:end
@echo 