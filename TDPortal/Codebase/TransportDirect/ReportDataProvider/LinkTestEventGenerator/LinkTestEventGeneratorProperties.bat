@echo off

@echo Populating Permanent Portal Properties Table with LinkTestEventGenerator properties
osql -Esa -iLinkTestEventGeneratorProperties.sql
if errorlevel 1 goto bomb2 
@echo .
@echo PermanentPortal database tables populated with LinkTestEventGenerator properties
@echo ***********************************************************************


goto finish

:bomb1
@echo Populating Permanent Portal Properties Table - Process failed, exiting setup
goto end

:bomb2
@echo Populating Permanent Portal Properties Table - Process failed, exiting setup
goto end

:finish
@echo .
@echo Permanent Portal Properties Table update has finished
@echo **************************************************

:end
@echo on
pause
 