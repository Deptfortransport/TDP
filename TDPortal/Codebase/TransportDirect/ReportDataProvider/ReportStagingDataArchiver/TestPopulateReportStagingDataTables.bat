@echo off

@echo Populating Permanent Portal Database Tables with Test Report Staging Data for use in Archive Test
osql -Esa -iTestReportStagingData.sql
if errorlevel 1 goto bomb2 
@echo .
@echo PermanentPortal database tables populated with Test Report Staging Data
@echo ***********************************************************************


goto finish

:bomb1
@echo Populating Transient Portal Database Tables - Process failed, exiting setup
goto end

:bomb2
@echo Populating Transient Portal Database Tables - Process failed, exiting setup
goto end

:finish
@echo .
@echo TransientPortal database update for Archive test has finished
@echo **************************************************************

:end
@echo on
pause
 