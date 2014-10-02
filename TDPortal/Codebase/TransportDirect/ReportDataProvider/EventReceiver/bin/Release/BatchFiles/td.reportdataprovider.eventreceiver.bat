@ECHO OFF
REM ******************************************************************** 
REM NAME                 : td.reportdataprovider.eventreceiver.bat 
REM AUTHOR               : Jatinder S. Toor
REM DATE CREATED         : 28/08/2003 
REM DESCRIPTION  :  Batch file used to simplify starting,
REM stoppping and testing the event receiver service.
REM Primarily to be used by the TNG control application.
REM ******************************************************************* 

IF "%1"=="test"  GOTO TEST 
IF "%1"=="start" GOTO START 
IF "%1"=="stop"  GOTO STOP
GOTO HELPMSG

:TEST
net start /test EventReceiver
GOTO END


:START
net start EventReceiver
GOTO END

:STOP
net stop EventReceiver
GOTO END


:HELPMSG

ECHO Command line usage :
ECHO     td.reportdataprovider.eventreceiver.bat  test
ECHO     td.reportdataprovider.eventreceiver.bat  start
ECHO     td.reportdataprovider.eventreceiver.bat  stop


:END
