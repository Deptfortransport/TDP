echo off

:check
for /f "tokens=1-3 delims=/ " %%a in ('D:\TDPortal\BatchFiles\doff mm/dd/yyyy 0') do (
set mm=%%a
set dd=%%b
set yyyy=%%c)

echo Checking W01
messagequeuecounter2 W01
if %errorlevel%  == 0 goto 100


if %errorlevel%  == -99 goto 50
echo W01 requires processing
goto 100
:50


osql -E -S D03 -d ReportStagingDB -q "EXIT(select count(*) from OperationalEvent where TimeLogged >= convert(datetime,'%dd%/%mm%/%yyyy%',105) and machinename like 'W01')"
if %errorlevel% == 0 goto 75

echo  W01 transaction count OK
goto 100

:75
echo W01 requires processing

:100 
echo Checking W02
messagequeuecounter2 W02
if %errorlevel%  == 0 goto 200


if %errorlevel%  == -99 goto 150
echo W02 requires processing
goto 200
:150
osql -E -S D03 -d ReportStagingDB -q "EXIT(select count(*) from OperationalEvent where TimeLogged >= convert(datetime,'%dd%/%mm%/%yyyy%',105) and machinename like 'W02')"
if %errorlevel% == 0 goto 175
echo  W02 transaction count OK
goto 200
:175
echo W02 requires processing



:200 
echo Checking W03
messagequeuecounter2 W03
if %errorlevel%  == 0 goto 300
if %errorlevel%  == -99 goto 250
echo W03 requires processing
goto 300
:250
osql -E -S D03 -d ReportStagingDB -q "EXIT(select count(*) from OperationalEvent where TimeLogged >= convert(datetime,'%dd%/%mm%/%yyyy%',105) and machinename like 'W03')"
if %errorlevel% == 0 goto 275
echo  W03 transaction count OK
goto 300
:275
echo W03 requires processing


:300 
echo Checking W04
messagequeuecounter2 W04
if %errorlevel%  == 0 goto 400
if %errorlevel%  == -99 goto 350
echo W04 requires processing
goto 400
:350
osql -E -S D03 -d ReportStagingDB -q "EXIT(select count(*) from OperationalEvent where TimeLogged >= convert(datetime,'%dd%/%mm%/%yyyy%',105) and machinename like 'W04')"
if %errorlevel% == 0 goto 375
echo  W04 transaction count OK
goto 400
:375
echo W04 requires processing

:400 
goto 500
echo Checking W05
msgcounter W05
if %errorlevel%  == 0 goto 500
echo W05 requires processing


:500 


:800
echo Checking J01
msgcounter J01
if %errorlevel%  == 0 goto 900
echo J01 requires processing


:900
echo Checking J02
msgcounter J02
if %errorlevel%  == 0 goto 960
echo J02 requires processing


:960
echo Checking J03
msgcounter J03
if %errorlevel%  == 0 goto 970
echo J03 requires processing



:970


echo Checking J04
msgcounter J04
if %errorlevel%  == 0 goto 980
echo J04 requires processing






:980
echo Checking GA01
msgcounter GA01
if %errorlevel%  == 0 goto 1000
echo GA01 requires processing



:1000
echo Checking GA02
msgcounter GA02
if %errorlevel%  == 0 goto 1050
echo GA02 requires processing




:1050
echo Checking AP01
msgcounter AP01
if %errorlevel%  == 0 goto 1075
echo AP01 requires processing



:1075
echo Checking AP02
msgcounter AP02
if %errorlevel%  == 0 goto 1100
echo AP02 requires processing


:1100

::for /f "tokens=1-4 delims=/ " %%a in ('Date /t') do set Date=%%b%%a

for /f "tokens=1-3 delims=/ " %%a in ('D:\TDPortal\BatchFiles\doff mm/dd/yyyy -1') do (
set mm=%%a
set dd=%%b
set yyyy=%%c)

set Date=%mm%%dd%
echo %date%




if exist D:\WebIISlogs\W01logs\Archive\u_ex13%date%02.log.w01.zip goto 1200
echo weblogs not ready for W01 for date %date%02.log.zip


:1200
if exist D:\WebIISlogs\W02logs\Archive\u_ex13%date%02.log.w02.zip goto 1300
echo weblogs not ready for W02 for date %date%02.log.zip


:1300
if exist D:\WebIISlogs\W03logs\Archive\u_ex13%date%02.log.w03.zip goto 1400
echo weblogs not ready for W03 for date %date%02.log.zip


:1400
if exist D:\WebIISlogs\W04logs\Archive\u_ex13%date%02.log.w04.zip goto 1500
echo weblogs not ready for W04 for date %date%02.log.zip


:1500

goto 1600

if exist D:\WebIISlogs\W05logs\Archive\u_ex11%date%02.log goto 1600
echo weblogs not ready for W05


:1600



:1900
echo Checking Injectors

for /f "tokens=1-3 delims=/ " %%a in ('D:\TDPortal\BatchFiles\doff mm/dd/yyyy -1') do (
set mm=%%a
set dd=%%b
set yyyy=%%c)

osql -E -S D03 -d ReportStagingDB -q "EXIT(select count(*) from ReferenceTransactionEvent   where Submitted >= convert(datetime,'%dd%/%mm%/%yyyy%',105) and machinename like 'INJ01')"

if %errorlevel% == 0 goto 2100
echo  Injector INJ01 - reference transaction count OK

osql -E -S D03 -d ReportStagingDB -q "EXIT(select count(*) from ReferenceTransactionEvent   where Submitted >= convert(datetime,'%dd%/%mm%/%yyyy%',105) and machinename like 'INJ02')"

if %errorlevel% == 0 goto 2110
echo  Injector INJ02 - reference transaction count OK

osql -E -S D03 -d ReportStagingDB -q "EXIT(select count(*) from ReferenceTransactionEvent   where Submitted >= convert(datetime,'%dd%/%mm%/%yyyy%',105) and machinename like 'SiteConfidence')"

if %errorlevel% == 0 goto 2120
echo  Site Confidence - reference transaction count OK


goto 2200


:2100
echo Injector queue INJ01 not drained and requires processing

goto exit

:2110
echo Injector queue INJ02 not drained and requires processing
goto exit


:2120
echo No Site confidence stats
goto exit


:2200
echo  RDI  OK TO BE RUN


:2700

:exit
echo exitted %errorlevel%
:exit2
pause
