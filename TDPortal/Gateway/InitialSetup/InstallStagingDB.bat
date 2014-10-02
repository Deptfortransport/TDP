@echo off

echo Creating Additional Staging Database...
osql -Esa -S%TDP_DB_NAME% -iadditionaldatastaging.sql
echo ErrorLevel %errorlevel%
if errorlevel 1 goto bomb1 
echo .
echo Additional StagingDatabase Created
echo **************************************************

echo Creating NPTG Staging Database...
osql -Esa -S%TDP_DB_NAME% -inptgstaging.sql
echo ErrorLevel %errorlevel%
if errorlevel 1 goto bomb1 
echo .
echo NPTG StagingDatabase Created
echo **************************************************

:bomb1
