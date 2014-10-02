echo off

if exist D:\TDPortal\Components\ReportDataImporter\RunAgain.txt goto check
goto exit2

:check

echo Checking W01
msgcounter W01
if %errorlevel%  == 0 goto 100
echo W01 requires processing
goto exit

:100 
echo Checking W02
msgcounter W02
if %errorlevel%  == 0 goto 200
echo W02 requires processing
goto exit

:200 
echo Checking W03
msgcounter W03
if %errorlevel%  == 0 goto 300
echo W03 requires processing
goto exit

:300 
echo Checking W04
msgcounter W04
if %errorlevel%  == 0 goto 400
echo W04 requires processing
goto exit

:400 



:800
echo Checking J01
msgcounter J01
if %errorlevel%  == 0 goto 900
echo J01 requires processing
goto exit

:900
echo Checking J02
msgcounter J02
if %errorlevel%  == 0 goto 960
echo J02 requires processing
goto exit

:960
echo Checking J03
msgcounter J03
if %errorlevel%  == 0 goto 970
echo J03 requires processing
goto exit

:970
echo Checking J04
msgcounter J04
if %errorlevel%  == 0 goto 980
echo J04 requires processing
goto exit





:980
echo Checking GA01
msgcounter GA01
if %errorlevel%  == 0 goto 1000
echo GA01 requires processing
goto exit



:1000
echo Checking GA02
msgcounter GA02
if %errorlevel%  == 0 goto 1050
echo GA02 requires processing
goto exit



:1050
echo Checking AP01
msgcounter AP01
if %errorlevel%  == 0 goto 1075
echo AP01 requires processing
goto exit


:1075
echo Checking AP02
msgcounter AP02
if %errorlevel%  == 0 goto 1100
echo AP02 requires processing
goto exit

:1100

::for /f "tokens=1-4 delims=/ " %%a in ('Date /t') do set Date=%%b%%a

for /f "tokens=1-3 delims=/ " %%a in ('D:\TDPortal\BatchFiles\doff mm/dd/yyyy -0') do (
set mm=%%a
set dd=%%b
set yyyy=%%c)

set Date=%mm%%dd%
echo %date%



if exist D:\WebIISlogs\W01logs\Archive\u_ex09%date%02.log.w01.zip goto 1200
echo weblogs not ready for W01 - u_ex09%date%02.log.w01.zip
goto exit

:1200
if exist D:\WebIISlogs\W02logs\Archive\u_ex09%date%02.log.w02.zip goto 1300
echo weblogs not ready for W02- u_ex09%date%02.log.w02.zip
goto exit

:1300
if exist D:\WebIISlogs\W03logs\Archive\u_ex09%date%02.log.w03.zip goto 1400
echo weblogs not ready for W03- u_ex09%date%02.log.w03.zip
goto exit

:1400
if exist D:\WebIISlogs\W04logs\Archive\u_ex09%date%02.log.w04.zip goto 1500
echo weblogs not ready for W04- u_ex09%date%02.log.w04.zip
goto exit

:1500

:1900
echo Checking Injectors

for /f "tokens=1-3 delims=/ " %%a in ('D:\TDPortal\BatchFiles\doff mm/dd/yyyy -1') do (
set mm=%%a
set dd=%%b
set yyyy=%%c)

osql -E -S D03 -d ReportStagingDB -q "EXIT(select count(*) from ReferenceTransactionEvent   where Submitted >= convert(datetime,'%dd%/%mm%/%yyyy%',105))"

if %errorlevel% == 0 goto 2100

goto 2200


:2100
echo Injector queues not drained
goto exit

:2200
echo Now running RDI


td.reportdataprovider.reportdataimporter 1

echo RDI has finished %errorlevel%

if %errorlevel% == 0 goto 2700

for /f "tokens=1-3 delims=/ " %%a in ('D:\TDPortal\BatchFiles\doff mm/dd/yyyy -1') do (
set mm=%%a
set dd=%%b
set yyyy=%%c)


osql -S "D03" -E ReportStagingDB -q "EXIT(select count(*) from [ReportServer].[Reporting].[dbo].[RTTIINTERNALEVENTS] where RStartTime >= convert(datetime,'%dd%/%mm%/%yyyy%',105))"
 
 
echo  %errorlevel%

if %errorlevel% == 0 goto exit

:2700
osql -S "D03" -E ReportStagingDB -q "EXIT(ReportStagingDB.dbo.UpdateReportProperties)"
goto exit2

:exit

delete D:\TDPortal\Components\ReportDataImporter\RunAgain.txt
echo exitted %errorlevel%
:exit2
