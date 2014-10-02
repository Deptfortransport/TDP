echo off
for /f "tokens=1-3 delims=/ " %%a in ('D:\TDPortal\BatchFiles\doff mm/dd/yyyy 0') do (
set mm=%%a
set dd=%%b
set yyyy=%%c)
:check

:: set server list for each environment

if %userdomain%==BBPTDPS goto 200

echo Checking W01
MessageQueueCounter2 W01

if %errorlevel%  == 0 goto 100

if %errorlevel%  == -99 goto 50

echo %date% %time% W01 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
if %errorlevel% LSS 1000 goto check
echo W01 requires processing
goto exit

:50

osql -E -S D03 -d ReportStagingDB -q "EXIT(select count(*) from OperationalEvent where TimeLogged >= convert(datetime,'%dd%/%mm%/%yyyy%',105) and machinename like 'W01')"
if %errorlevel% == 0 goto 75

echo  W01 transaction count OK
goto 100

:75
echo %date% %time% W01 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
goto exit


:100 


echo Checking W02
MessageQueueCounter2 W02
if %errorlevel%  == 0 goto 200
if %errorlevel%  == -99 goto 150

echo %date% %time% W02 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
if %errorlevel%  LSS 1000 goto 100
echo W02 requires processing
goto exit


:150

osql -E -S D03 -d ReportStagingDB -q "EXIT(select count(*) from OperationalEvent where TimeLogged >= convert(datetime,'%dd%/%mm%/%yyyy%',105) and machinename like 'W02')"
if %errorlevel% == 0 goto 175

echo  W02 transaction count OK
goto 200

:175
echo %date% %time% W02 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
goto exit


:200 
echo Checking W03
MessageQueueCounter2 W03
echo %errorlevel% >> d:\TDPortal\ReportDataImporterFailureLog.txt
if %errorlevel%  == 0 goto 300
if %errorlevel%  == -99 goto 250

echo %date% %time% W03 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt

if %errorlevel%  lss 1000 goto 200
echo W03 requires processing
goto exit

:250

osql -E -S D03 -d ReportStagingDB -q "EXIT(select count(*) from OperationalEvent where TimeLogged >= convert(datetime,'%dd%/%mm%/%yyyy%',105) and machinename like 'W03')"
if %errorlevel% == 0 goto 275

echo  W03 transaction count OK
goto 300

:275
echo %date% %time% W02 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
goto exit

:300 
if %userdomain%==BBPTDPS goto 960

echo Checking W04
MessageQueueCounter2 W04


if %errorlevel%  == 0 goto 400
if %errorlevel%  == -99 goto 350

echo %date% %time% W04 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
if %errorlevel% lss 1000 goto 300
echo W04 requires processing
goto exit



:350

osql -E -S D03 -d ReportStagingDB -q "EXIT(select count(*) from OperationalEvent where TimeLogged >= convert(datetime,'%dd%/%mm%/%yyyy%',105) and machinename like 'W04')"
if %errorlevel% == 0 goto 375

echo  W04 transaction count OK
goto 400

:375
echo %date% %time% W02 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
goto exit


:400 
goto 500
echo Checking W05
msgcounter W05
if %errorlevel%  LSS 1 goto 500
echo %date% %time% W05 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
if %errorlevel% lss 1000 goto 400
echo W05 requires processing
goto exit

:500
:800
echo Checking J01
msgcounter J01
if %errorlevel%  == 0 goto 900
echo %date% %time% J01 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
if %errorlevel% lss 1000 goto 800
echo J01 requires processing
goto exit

:900
echo Checking J02
msgcounter J02
if %errorlevel%  == 0 goto 960
echo %date% %time% J02 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
if %errorlevel% lss 1000 goto 900
echo J02 requires processing
goto exit

:960
echo Checking J03
msgcounter J03
if %errorlevel%  == 0 goto 970
echo %date% %time% J03 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
if %errorlevel% lss 1000 goto 960
echo J03 requires processing
goto exit

:970
if %userdomain%==BBPTDPS goto 1000

echo Checking J04
msgcounter J04
if %errorlevel%  == 0 goto 980
echo %date% %time% J04 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
if %errorlevel% lss 1000 goto 970
echo J04 requires processing
goto exit





:980
echo Checking GA01
msgcounter GA01
if %errorlevel%  == 0 goto 1000
echo %date% %time% GA01 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
if %errorlevel% lss 1000 goto 980
echo GA01 requires processing
goto exit



:1000
echo Checking GA02
msgcounter GA02
if %errorlevel%  == 0 goto 1050
echo %date% %time% GA02 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
if %errorlevel% lss 1000 goto 1000
echo GA02 requires processing
goto exit



:1050

if %userdomain%==BBPTDPS goto 1080
echo Checking AP01
msgcounter AP01
if %errorlevel%  == 0 goto 1075
echo %date% %time% AP01 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
if %errorlevel% lss 1000 goto 1050
echo AP01 requires processing
goto exit


:1075
echo Checking AP02
msgcounter AP02
if %errorlevel%  == 0 goto 1100
echo %date% %time% AP02 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
if %errorlevel% lss 1000 goto 1075
echo AP02 requires processing
goto exit

:1080
echo Checking AP03
msgcounter AP03
if %errorlevel%  == 0 goto 1100
echo %date% %time% AP03 requires re-processing %errorlevel%  >> d:\TDPortal\ReportDataImporterFailureLog.txt
if %errorlevel% lss 1000 goto 1080
echo AP03 requires processing
goto exit

:1100

::for /f "tokens=1-4 delims=/ " %%a in ('Date /t') do set Date=%%b%%a

for /f "tokens=1-3 delims=/ " %%a in ('D:\TDPortal\BatchFiles\doff mm/dd/yyyy -0') do (
set mm=%%a
set dd=%%b
set yyyy=%%c)

set Date=%mm%%dd%
echo %date%

if %userdomain%==BBPTDPS goto 1300

if exist D:\WebIISlogs\W01logs\Archive\u_ex13%date%02.log.* goto 1200
echo weblogs not ready for W01 - u_ex13%date%02.log.w01.zip >> d:\TDPortal\ReportDataImporterFailureLog.txt
echo weblogs not ready for W01 - u_ex13%date%02.log.w01.zip
goto exit

:1200
if exist D:\WebIISlogs\W02logs\Archive\u_ex13%date%02.log.* goto 1300
echo weblogs not ready for W02- u_ex13%date%02.log.w02.zip >> d:\TDPortal\ReportDataImporterFailureLog.txt
echo weblogs not ready for W02- u_ex13%date%02.log.w02.zip
goto exit

:1300
if exist D:\WebIISlogs\W03logs\Archive\u_ex13%date%02.log.* goto 1400
echo weblogs not ready for W03- u_ex13%date%02.log.w03.zip >> d:\TDPortal\ReportDataImporterFailureLog.txt
echo weblogs not ready for W03- u_ex13%date%02.log.w03.zip
goto exit

:1400

if %userdomain%==BBPTDPS goto 1600
if exist D:\WebIISlogs\W04logs\Archive\u_ex13%date%02.log.* goto 1500
echo weblogs not ready for W04- u_ex13%date%02.log.w04.zip >> d:\TDPortal\ReportDataImporterFailureLog.txt
echo weblogs not ready for W04- u_ex13%date%02.log.w04.zip 
goto exit

:1500
goto 1600

if exist D:\WebIISlogs\W05logs\Archive\u_ex13%date%02.log.* goto 1600
echo weblogs not ready for W05- u_ex13%date%02.log.w05.zip >> d:\TDPortal\ReportDataImporterFailureLog.txt
echo weblogs not ready for W05- u_ex13%date%02.log.w05.zip
goto exit

:1600
:1900
echo Checking Injectors

for /f "tokens=1-3 delims=/ " %%a in ('D:\TDPortal\BatchFiles\doff mm/dd/yyyy -1') do (
set mm=%%a
set dd=%%b
set yyyy=%%c)

if %userdomain%==BBPTDPS goto 1950
osql -E -S D03 -d ReportStagingDB -q "EXIT(select count(*) from ReferenceTransactionEvent   where Submitted >= convert(datetime,'%dd%/%mm%/%yyyy%',105) and machinename like 'INJ01')"

if %errorlevel% == 0 goto 2100
echo  Injector INJ01 - reference transaction count OK

goto 1980
:1950
osql -E -S D02 -d ReportStagingDB -q "EXIT(select count(*) from ReferenceTransactionEvent   where Submitted >= convert(datetime,'%dd%/%mm%/%yyyy%',105) and machinename like 'INJ02')"

if %errorlevel% == 0 goto 2110
echo  Injector INJ02 - reference transaction count OK
goto 1990
:1980
osql -E -S D03 -d ReportStagingDB -q "EXIT(select count(*) from ReferenceTransactionEvent   where Submitted >= convert(datetime,'%dd%/%mm%/%yyyy%',105) and machinename like 'INJ02')"

if %errorlevel% == 0 goto 2110
echo  Injector INJ02 - reference transaction count OK

:1990
if %userdomain%==BBPTDPS goto 2200
osql -E -S D03 -d ReportStagingDB -q "EXIT(select count(*) from ReferenceTransactionEvent   where Submitted >= convert(datetime,'%dd%/%mm%/%yyyy%',105) and machinename like 'SiteConfidence')"

if %errorlevel% == 0 goto 2120
echo  Site Confidence - reference transaction count OK


goto 2200


:2100
echo Injector queue INJ01 not drained and requires processing >> d:\TDPortal\ReportDataImporterFailureLog.txt

goto exit

:2110
echo Injector queue INJ02 not drained and requires processing >> d:\TDPortal\ReportDataImporterFailureLog.txt
goto exit


:2120
echo No Site confidence stats >> d:\TDPortal\ReportDataImporterFailureLog.txt
goto exit


:2200
echo  RDI  OK TO BE RUN

echo OKTORUN >> \\D02\D$\TDPortal\D02OKTORUN

td.reportdataprovider.reportdataimporter 1

echo RDI has finished %errorlevel%

if %errorlevel% == 0 goto 2700

for /f "tokens=1-3 delims=/ " %%a in ('D:\TDPortal\BatchFiles\doff mm/dd/yyyy -1') do (
set mm=%%a
set dd=%%b
set yyyy=%%c)

if %userdomain%==BBPTDPS goto 2300


osql -S "D03" -E ReportStagingDB -q "EXIT(select count(*) from [ReportServer].[Reporting].[dbo].[RTTIINTERNALEVENTS] where RStartTime >= convert(datetime,'%dd%/%mm%/%yyyy%',105))"
 
 
echo  %errorlevel%

if %errorlevel% == 0 goto exit
goto 2400
:2300

osql -S "D02" -E ReportStagingDB -q "EXIT(select count(*) from [ReportServer].[Reporting].[dbo].[RTTIINTERNALEVENTS] where RStartTime >= convert(datetime,'%dd%/%mm%/%yyyy%',105))"
 
 
echo  %errorlevel%

if %errorlevel% == 0 goto exit

:2400


:2700

if %userdomain%==BBPTDPS goto 2800
osql -S "D03" -E ReportStagingDB -q "EXIT(ReportStagingDB.dbo.UpdateReportProperties)"
goto exit2
:2800

osql -S "D02" -E ReportStagingDB -q "EXIT(ReportStagingDB.dbo.UpdateReportProperties)"
goto exit2




:exit
echo exitted %errorlevel%

echo failed >> D:\TDPortal\Components\ReportDataImporter\RunAgain.txt

:exit2
