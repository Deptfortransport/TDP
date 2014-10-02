:: ______________________________________________________________________________________________________________
::
::  Batch File:   AirInterchangeOverlaysImport.bat
::  Author:       Steve Craddock  
::
::  Built/Tested On: Windows 2003 Server
::
::  Purpose:       Used to manually import air interchange overlays into AirInterchange..Interchanges
::                 
::  Parameters:   None
::
::  Instructions:   Ensure all drives are correct e.g C: or D: 
::	            Ensure Gateway servers are correctly named in "Identify Gateway servers section"	
::
::  Last Update:    Steve Craddock	05/11/2007 	Original	
::
::		    Steve Craddock 	20/05/2010	Updated for new SQL tools path	
::
:: ______________________________________________________________________________________________________________
::


:: set server list for each environment

if %userdomain%==BBPTDPSIS ( 	set dbserver=D01.bbptdpsis.local
				set dbname=PermanentPortal)

if %userdomain%==BBPTDPS ( 	set dbserver=D02.bbptdps.local
				set dbname=PermanentPortal)

if %userdomain%==ACPTDPS ( 	set dbserver=D03.acptdps.local
				set dbname=PermanentPortal)

@echo off
set currDate=
set currTime=
set time=
set timenow=
set date=
:: get todays date for use later
	for /F "tokens=1-4 delims=/ " %%i in ('date /t') do (
	rem set Day=%%j
	rem set Month=%%k
	rem set Year=%%l
	set currDate=%%l%%k%%j
	)
:: get current time for use later
	for /F "tokens=1-2 delims=: " %%i in ('time /t') do (
	set xcopyhour=%%i
	set xcopymins=%%j
	set currTime=%%i%%j
	)
:: set the name of the log file
	set output_file= D:\Gateway\log\AIOverlays_%currdate%_%currTime%.log

	echo %date% %time% import AI Overlays started >> %OUTPUT_FILE%


echo query requested
c:
cd c:\"Program Files (x86)"\"Microsoft SQl Server"\100\Tools\
echo running SQL >> %OUTPUT_FILE%
osql -E -S %dbserver% -d %dbname% -q "EXIT(EXEC usp_ImportAirInterchangeOverlays)" >> %OUTPUT_FILE%

d:

echo Script completed %currdate% %currTime% >> %OUTPUT_FILE%
echo finished OK
:end

exit /B 0