:: ______________________________________________________________________________________________________________
::
::  Batch File:      importA.bat
::  Author:          Joe Morrissey     
::
::  Built/Tested On: Windows 2000 Advanced Server
::
::  Purpose:  	     Used to call td.dataimport.exe	
::
::  Last Update:     Joe Morrissey 07/12/2004 - removed call to _ipAddress.bat and 
::  use of primary and secondary ftp servers
:: ______________________________________________________________________________________________________________
::
@echo off

rem if no userID parameter passed in then no point carrying on
if .%1==. GoTo end

cd d:\Gateway\bin

rem call td.dataimport.exe with parameter 1 - userID, and optional parmeter 2 - /notransfer flag 
echo Trying %1 on localhost
d:\Gateway\bin\td.dataimport.exe %1 /ipaddress:localhost %2
echo ErrorLevel %errorlevel%

:end
cd d:\Gateway\bat
