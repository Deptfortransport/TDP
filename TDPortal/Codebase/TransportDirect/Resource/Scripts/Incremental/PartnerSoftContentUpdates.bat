@echo off

@echo **************************************************
@echo Executing Soft content scripts for White label partners
@echo **************************************************

REM @echo PARTNER SCRIPTS START

@echo --------------------------------------------------
@echo BBC
@echo --------------------------------------------------

@echo Executing script SCP10301_BBC_1_Theme.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10301_BBC_1_Theme.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10302_BBC_2_Properties.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10302_BBC_2_Properties.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10303_BBC_3_Left_hand_links.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10303_BBC_3_Left_hand_links.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10304_BBC_4_Content.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10304_BBC_4_Content.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10305_BBC_5_FAQs.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10305_BBC_5_FAQs.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10306_BBC_6_AboutUs.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10306_BBC_6_AboutUs.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10307_BBC_7_Accessibility.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10307_BBC_7_Accessibility.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10308_BBC_8_Content.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10308_BBC_8_Content.sql
if errorlevel 1 goto bomb1


@echo --------------------------------------------------
@echo BusinessGateway
@echo --------------------------------------------------

@echo Executing script SCP10401_BusinessGateway_Theme_1.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10401_BusinessGateway_Theme_1.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10402_BusinessGateway_2_Properties.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10402_BusinessGateway_2_Properties.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10403_BusinessGateway_3_Left_hand_links.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10403_BusinessGateway_3_Left_hand_links.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10404_BusinessGateway_4_Content.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10404_BusinessGateway_4_Content.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10405_BusinessGateway_5_FAQs.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10405_BusinessGateway_5_FAQs.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10406_BusinessGateway_6_AboutUs.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10406_BusinessGateway_6_AboutUs.sql
if errorlevel 1 goto bomb1


@echo Executing script SCP10407_BusinessGateway_7_Accessibility.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10407_BusinessGateway_7_Accessibility.sql
if errorlevel 1 goto bomb1


@echo --------------------------------------------------
@echo BusinessLink - Discontinued site
@echo --------------------------------------------------


@echo --------------------------------------------------
@echo DirectGov - Discontinued site
@echo --------------------------------------------------


@echo --------------------------------------------------
@echo VisitBritain
@echo --------------------------------------------------

@echo Executing script SCP10801_VisitBritain_1_Theme.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10801_VisitBritain_1_Theme.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10802_VisitBritain_2_Properties.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10802_VisitBritain_2_Properties.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10803_VisitBritain_3_Left_hand_links.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10803_VisitBritain_3_Left_hand_links.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10804_VisitBritain_4_Content.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10804_VisitBritain_4_Content.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10805_VisitBritain_5_FAQs.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10805_VisitBritain_5_FAQs.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10806_VisitBritain_6_AboutUs.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10806_VisitBritain_6_AboutUs.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10807_VisitBritain_7_Accessibility.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10807_VisitBritain_7_Accessibility.sql
if errorlevel 1 goto bomb1


@echo --------------------------------------------------
@echo AOCycle
@echo --------------------------------------------------

@echo Executing script SCP10201_AOCycle_Cycle_1_Theme.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10201_AOCycle_Cycle_1_Theme.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10202_AOCycle_Cycle_2_Properties.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10202_AOCycle_Cycle_2_Properties.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10203_AOCycle_Cycle_3_Left_hand_links.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10203_AOCycle_Cycle_3_Left_hand_links.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10204_AOCycle_Cycle_4_Content.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10204_AOCycle_Cycle_4_Content.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10205_AOCycle_Cycle_5_FAQs.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10205_AOCycle_Cycle_5_FAQs.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10206_AOCycle_Cycle_6_AboutUs.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10206_AOCycle_Cycle_6_AboutUs.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10207_AOCycle_Cycle_7_Accessibility.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10207_AOCycle_Cycle_7_Accessibility.sql
if errorlevel 1 goto bomb1

@echo --------------------------------------------------
@echo CycleRoutes
@echo --------------------------------------------------

@echo Executing script SCP10901_CycleRoutes_Cycle_1_Theme.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10901_CycleRoutes_Cycle_1_Theme.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10902_CycleRoutes_Cycle_2_Properties.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10902_CycleRoutes_Cycle_2_Properties.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10903_CycleRoutes_Cycle_3_Left_hand_links.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10903_CycleRoutes_Cycle_3_Left_hand_links.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10904_CycleRoutes_Cycle_4_Content.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10904_CycleRoutes_Cycle_4_Content.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10905_CycleRoutes_Cycle_5_FAQs.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10905_CycleRoutes_Cycle_5_FAQs.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10906_CycleRoutes_Cycle_6_AboutUs.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10906_CycleRoutes_Cycle_6_AboutUs.sql
if errorlevel 1 goto bomb1

@echo Executing script SCP10907_CycleRoutes_Cycle_7_Accessibility.sql
osql -S .\SQLExpress -Esa -iSoftContent\PartnerScripts\SCP10907_CycleRoutes_Cycle_7_Accessibility.sql
if errorlevel 1 goto bomb1

REM @echo PARTNER SCRIPTS END


if "%1" EQU "/s" goto end

goto end

:bomb1

@echo Executing White label partner Soft content scripts - Process failed, exiting

if "%1" EQU "/s" goto end

goto end

:end
