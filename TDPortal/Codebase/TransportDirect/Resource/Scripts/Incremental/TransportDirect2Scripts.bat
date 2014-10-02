@echo off

@echo **************************************************
@echo Executing scripts
@echo **************************************************

if "%1" EQU "/s" GOTO skippromptone

@echo Ensure that none of the databases are in use before continuing
@echo e.g. make sure that SQL Query Analyzer and Enterprise Manager are not running

pause

:skippromptone

REM @echo Performing IIS Reset
REM iisreset

@echo **************************************************
@echo Transport Direct
@echo **************************************************

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\PermanentPortal\DropDownLists\DropDownData.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\PermanentPortal\Properties\Properties_Database.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\PermanentPortal\Properties\Properties_DataLoader.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\PermanentPortal\Properties\Properties_DataLoader_EventLogging.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\PermanentPortal\Properties\Properties_TDP.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\PermanentPortal\Properties\Properties_TDPMobile.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\PermanentPortal\Properties\Properties_TDPMobile_EventLogging.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\PermanentPortal\Properties\Properties_TDPWeb.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\PermanentPortal\Properties\Properties_TDPWeb_EventLogging.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\PermanentPortal\Properties\Properties_EventReceiver.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\PermanentPortal\Properties\Properties_EventReceiver_EventLogging.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\PermanentPortal\ReferenceNum\ReferenceNum.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\PermanentPortal\Retailers\Retailers.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\PermanentPortal\DevSettings.sql"
if errorlevel 1 goto bomb1



@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\Content\ChangeNotification\ChangeNotification.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\Content\ContentGroup\ContentGroup.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\Content\Content\Content.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\Content\Content\ContentAnalytics.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\Content\Content\ContentHeaderFooter.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\Content\Content\ContentJourneyOutput.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\Content\Content\ContentMobile.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\Content\Content\ContentSitemap.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\Content\DevSettings.sql"
if errorlevel 1 goto bomb1



@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\TransientPortal\AdminArea\AdminArea.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\TransientPortal\ChangeNotification\ChangeNotificationData.data.sql"
if errorlevel 1 goto bomb1

@echo Executing script
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts\TransientPortal\Districts\Districts.data.sql"
if errorlevel 1 goto bomb1



@echo
@echo **************************************************
@echo Executing scripts completed
@echo **************************************************

if "%1" EQU "/s" goto end

goto end

:bomb1

@echo Executing scripts - Process failed, exiting

if "%1" EQU "/s" goto end

pause
goto end

:end
@echo
pause
pause