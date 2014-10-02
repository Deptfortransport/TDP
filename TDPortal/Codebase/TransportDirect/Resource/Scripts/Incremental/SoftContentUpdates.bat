@echo off

@echo **************************************************
@echo Executing Soft content scripts
@echo **************************************************

if "%1" EQU "/s" GOTO skippromptone

@echo Ensure that none of the databases are in use before continuing
@echo e.g. make sure that SQL Query Analyzer and Enterprise Manager are not running

pause

:skippromptone

@echo Performing IIS Reset
iisreset

@echo --------------------------------------------------
@echo Transport Direct
@echo --------------------------------------------------

@echo Executing script SC10001_TransportDirect_Content_1_MiniHome_PlanAJourney.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10001_TransportDirect_Content_1_MiniHome_PlanAJourney.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10002_TransportDirect_Content_2_MiniHome_FindAPlace.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10002_TransportDirect_Content_2_MiniHome_FindAPlace.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10003_TransportDirect_Content_3_MiniHome_LiveTravel.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10003_TransportDirect_Content_3_MiniHome_LiveTravel.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10004_TransportDirect_Content_4_MiniHome_TipsAndTools.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10004_TransportDirect_Content_4_MiniHome_TipsAndTools.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10005_TransportDirect_Content_5_Home_RightHandPanel.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10005_TransportDirect_Content_5_Home_RightHandPanel.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10006_TransportDirect_Content_6_Home_TipsToolsPanel.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10006_TransportDirect_Content_6_Home_TipsToolsPanel.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10007_TransportDirect_Content_7_FAQs.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10007_TransportDirect_Content_7_FAQs.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10008_TransportDirect_Content_8_AboutUs.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10008_TransportDirect_Content_8_AboutUs.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10009_TransportDirect_Content_9_NetworkMaps.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10009_TransportDirect_Content_9_NetworkMaps.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10010_TransportDirect_Content_10_SiteMap.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10010_TransportDirect_Content_10_SiteMap.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10011_TransportDirect_Content_11_PrivacyPolicy.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10011_TransportDirect_Content_11_PrivacyPolicy.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10012_TransportDirect_Content_12_DataProviders.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10012_TransportDirect_Content_12_DataProviders.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10013_TransportDirect_Content_13_Accessibility.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10013_TransportDirect_Content_13_Accessibility.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10014_TransportDirect_Content_14_ContactUs.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10014_TransportDirect_Content_14_ContactUs.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10015_TransportDirect_Content_15_TermsConditions.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10015_TransportDirect_Content_15_TermsConditions.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10016_TransportDirect_Content_16_Relatedsites.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10016_TransportDirect_Content_16_Relatedsites.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10017_CyclePlanner_Content_17_RightHandPanel.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10017_CyclePlanner_Content_17_RightHandPanel.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10018_TransportDriect_Content_18_Help_JourneyDetails.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10018_TransportDriect_Content_18_Help_JourneyDetails.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10019_TransportDriect_Content_19_Help_JourneyPlannerInput.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10019_TransportDriect_Content_19_Help_JourneyPlannerInput.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10020_TransportDriect_Content_20_Help_FindNearestAccessibleStop.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10020_TransportDriect_Content_20_Help_FindNearestAccessibleStop.sql
if errorlevel 1 goto bomb1

@echo Executing script SC10021_TransportDirect_Content_21_DepartureBoards.sql
osql -S .\SQLExpress -Esa -iSoftContent\SC10021_TransportDirect_Content_21_DepartureBoards.sql
if errorlevel 1 goto bomb1

REM @echo CALL THE PARTNER SCRIPTS BATCH FILE

cd "D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental"

call "D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\PartnerSoftContentUpdates.bat"

cd "D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental"

REM @echo CALL THE PARTNER SCRIPTS BATCH FILE END

@echo **************************************************
@echo Executing Soft content scripts completed
@echo **************************************************

if "%1" EQU "/s" goto end

goto end

:bomb1

@echo !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
@echo Executing Soft content scripts - Process failed, exiting
@echo !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

if "%1" EQU "/s" goto end

goto end

:end
@echo END

pause
pause