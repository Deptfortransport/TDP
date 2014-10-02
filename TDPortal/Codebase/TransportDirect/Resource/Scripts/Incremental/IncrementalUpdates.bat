@echo off

@echo **************************************************
@echo Reinstating initial state
@echo **************************************************

if "%1" EQU "/s" GOTO skippromptone

@echo Ensure that none of the databases are in use before continuing
@echo e.g. make sure that SQL Query Analyzer and Enterprise Manager are not running
@echo Ensure the Batch service is stopped

pause

:skippromptone

@echo Performing IIS Reset
iisreset

@echo Dropping existing databases
osql -S .\SQLExpress -Esa -iLatestState\DropAllDatabases.sql
if errorlevel 1 goto bomb1

@echo Copying data files
copy "D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\LatestState\*.mdf" "C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA" /Y
if errorlevel 1 goto bomb1

@echo Attaching new data files
osql -S .\SQLExpress -Esa -iLatestState\AttachAllDatabases.sql
if errorlevel 1 goto bomb1

@echo
@echo **************************************************
@echo Executing Incremental Updates...
@echo **************************************************

echo Executing Incremental Update DUP1990
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP1990_AccessibleLocation_Content_4.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP1991
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP1991_StopInformation_AccessibleLink.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP1992
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP1992_AccessibleLocation_DataImport_Procs.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP1993
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP1993_BatchLoggingProperties.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP1993
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP1994_BatchCoordinateDefence.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP1995
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP1995_GISQueryEvent_Properties_Update.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP1996
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP1996_AccessibleLocation_Properties.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP1997
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP1997_BatchCoordinateDefence_ProdValues.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP1998
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP1998_ExternalLinkes_AccessibleInformationLinks.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP1999
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP1999_AccessibleLocation_Content_5.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2000
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2000_AccessibleOperator_Tables_Update.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2001
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2001_AccessibleOperator_DataImport_Procs_Update.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2002
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2002_BatchJourneyPlannerMaxLines_Property.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2003
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2003_BatchJourneyPlanner_FAQ_Menu_Link.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2004
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2004_JourneyNoteFilter_Tables.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2005
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2005_JourneyNoteFilter_StoredProcs.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2006
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2006_JourneyNoteFilter_DataImport.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2007
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2007_JourneyNoteFilter_DataImport_Procs.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2008
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2008_JourneyNoteFilter_DataImport_Properties.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2009
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2009_JourneyNoteFilter_Properties_DataNotification.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2010
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2010_ServiceDetailRequestOperatorCodes_DataImport.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2011
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2011_ServiceDetailRequestOperatorCodes_DataImport_Properties.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2012
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2012_ServiceDetailRequestOperatorCodes_DataImport_Procs.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2013
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2013_ServiceDetailRequestOperatorCodes_Properties.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2014
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2014_ServiceDetailRequestOperatorCodes_Database_Objects.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2015
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2015_ServiceDetailRequestOperatorCodes_Properties_DataNotification.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2016
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2016_BatchUpdates.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2017
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2017_BatchConnectionString.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2018
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2018_BatchQueuedPosition.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2019
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2019_AccessibleLocation_MultiRegionFlag_Properties_Update.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2020
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2020_UpdateTrapezeRegionsParameter.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2021
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2021_BatchStartDate.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2022
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2022_BatchJourneyPlanner_StoredProc_Update.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2023
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2023_UpdateTransferLandingPageEventSP.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2024
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2024_AccessibleLocation_Content_5.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2025
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2025_UpdateLocationInformationFurtherDetailsURL.sql
if errorlevel 1 goto bomb1


echo Executing Incremental Update DUP2027
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2027_Batch_PreValidation_Changes.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2028
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2028_EES_Coordinate_Updates.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2029
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2029_POI_Location_Updates.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2030
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2030_UPdatePromLinksSchedule_IncludeRideLondon.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2031
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2031_SpecialEventsObjects.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2032
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2032_SpecialEventsProperties.sql
if errorlevel 1 goto bomb1



REM --------------Start TDP2 scripts------------------

REM PermanentPortal database
echo Executing Incremental Update DUP2033
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2033_UpdateChangeCatalogue.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2034
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2034_Retailers.table.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2035
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2035_AddUpdateProperty.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2036
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2036_AddUpdateRetailer.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2037
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2037_AddUpdateRetailerLookup.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2038
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2038_GetRetailers.proc.sql
if errorlevel 1 goto bomb1

REM Content database
echo Executing Incremental Update DUP2039
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2039_ChangeNotification.table.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2040
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2040_AddChangeNotificationTable.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2041
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2041_UpdateChangeNotificationTable.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2042
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2042_GetChangeTable.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2043
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2043_AddContent.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2044
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2044_AddGroup.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2045
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2045_DeleteGroup.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2046
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2046_DeleteContentOverride.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2047
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2047_DeleteContent.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2048
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2048_DeleteAllGroupContent.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2049
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2049_GetContent.proc.sql
if errorlevel 1 goto bomb1

REM TransientPortal database
echo Executing Incremental Update DUP2050
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2050_AdminAreas.table.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2051
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2051_GetNPTGAdminAreas.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2052
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2052_Districts.table.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2053
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2053_GetNPTGDistricts.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2054
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2054_StopAccessibilityLinks.table.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2055
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2055_ImportStopAccessibilityLinks.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2056
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2056_GetStopAccessibilityLinks.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2057
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2057_UndergroundStatus.table.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2058
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2058_ImportUndergroundStatusData.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2059
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2059_GetUndergroundStatus.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2060
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2060_AccessibleOperator_Objects_Update.sql
if errorlevel 1 goto bomb1


REM ------------------End TDP2 scripts-------------------

echo Executing Incremental Update DUP2061
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2061_BatchUploadMessageChange.sql
if errorlevel 1 goto bomb1


echo Executing Incremental Update DUP2062
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2062_BatchUploadStatusFix.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2063
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2063_SiteRedirect_Properties.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2064
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2064_Footer_ContentUpdate.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2065
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2065_Footer_PropertiesUpdate.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2066
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2066_Add_RSL_Middlesbrough_EES_Partner.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2067
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2067_UndergroundStatus.table.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2068
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2068_ImportUndergroundStatusData.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2069
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2069_GetUndergroundStatus.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2070
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2070_POI_DataImport.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2071
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2071_Reporting_GazetteerType_Update.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2072
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2072_Reporting_PageEntryType_Update.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2073
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2073_Batch_Properties.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2074
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2074_GetPOILocation_Update.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2075
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2075_BatchStoredProcChange.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2076
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2076_DepartureBoardLinks_Update.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2077
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2077_GetRetailers.proc.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2078
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2078_Reporting_ReferenceTransactionType_Update.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2079
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2079_Update_air_feeds_for_GZI5907193.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2080
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2080_BatchHousekeepingChange.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2081
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2081_Add_Nexus_EES_Partner.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2082
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2082_Add_NUHT_EES_Partner.sql
if errorlevel 1 goto bomb1


echo Executing Incremental Update DUP2083
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2083_Add_C2S_EES_Partner.sql
if errorlevel 1 goto bomb1


echo Executing Incremental Update DUP2084
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2084_Add_Worcester_EES_Partner.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2084
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2084_CarCosts_Content_Update.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2085
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2085_Gateway_PropertiesUpdate.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2086
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2086_WelshContent.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2087
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2087_Content_Overide_Promotion_Calendar_2014.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2088
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2088_Add_Hampshire_EES_Partner.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2089
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2089_AccessibleLinks_Content_Update.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2090
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2090_AccessibleLinks_ExternalLinks_Update.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2091
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2091_LoginPage_Content_Update.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2092
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2092_CarParks_Content_Update.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2093
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2093_Properties_DepartureBoardService.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2094
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2094_Properties_DepartureBoardWebService.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2095
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2095_Properties_DepartureBoardWebService_EventLogging.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2096
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2096_DataSet_DepartureBoardService_MobileTimeRequestDrop.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2097
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2097_Update_Custom_EMAIL_Sender_Property.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2098
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2098_Content_Overide_Promotion_Calendar_2014.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2099
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2099_MAS_Content.sql
if errorlevel 1 goto bomb1

REM echo Executing Incremental Update DUP2100
REM osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2100_Add_SEFTON COUNCIL_EES_Partner.sql 
REM if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2101
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2101_Add_MX_DATA_TRUST_EES_Partner.sql 
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2102
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2102_Update_LUL_Map_Url.sql 
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2103
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2103_Add_Norfolk_EES_Partner.sql 
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2104
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2104_Content_Overide_Promotion_Calendar_2014.sql 
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2105
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2105_DigitalTV_Properties_Update.sql 
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2106
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2106_DigitalTV_FAQ_LinkName_Update.sql
if errorlevel 1 goto bomb1

echo Executing Incremental Update DUP2107
osql -S .\SQLExpress -Esa -iCurrentScripts\DUP2107_BatchNewRegUnavailableMsg.sql
if errorlevel 1 goto bomb1

REM **************** Template ****************
REM @echo Executing Incremental Update DUPXXXX
REM osql -S .\SQLExpress -Esa -iCurrentScripts\DUPXXXX_ScriptName.sql
REM if errorlevel 1 goto bomb1

@echo **************************************************
@echo Executing Incremental Updates completed
@echo **************************************************

if "%1" EQU "/s" goto end

pause

goto end

:bomb1

@echo Executing Incremental Updates - Process failed, exiting

if "%1" EQU "/s" goto end

pause
goto end

:end
@echo
pause