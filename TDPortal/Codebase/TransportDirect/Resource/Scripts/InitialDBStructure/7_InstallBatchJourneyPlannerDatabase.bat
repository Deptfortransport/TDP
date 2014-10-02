@echo off
@echo Creating BatchJourneyPlanner Database...
osql -Esa -S%TDP_DB_NAME% -iCreateBatchJourneyPlannerDataBase.sql
if errorlevel 1 goto bomb1 
@echo .
@echo Database Created
@echo **************************************************

@echo Creating BatchJourneyPlanner Database Tables...
osql -Esa -S%TDP_DB_NAME% -iCreateBatchJourneyPlannerDataBaseTables.sql
if errorlevel 1 goto bomb2
@echo .
@echo Tables Created
@echo **************************************************

goto finish

:bomb1
@echo Creating BatchJourneyPlanner Database - Process failed, exiting setup
goto end

:bomb2
@echo Creating BatchJourneyPlanner Tables - Process failed, exiting setup
goto end

:finish
@echo .
@echo TransientPortal Database Installation has finished
@echo **************************************************

:end
@echo 