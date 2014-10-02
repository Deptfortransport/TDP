echo off
:: ______________________________________________________________________________________________________________
::
::  Batch File:      	IF067 (IJM369) gateway batch file
::  Author:          	Steve Craddock    
::
::  Built/Tested On: 	Windows 2008 Server
::
::  Purpose:  	     	Ugeneral framework batch file.
::		     	This batch file is called TDP as part of a process.	
::
::  Last Update:     	Steve Craddock 12/09/13 - v1.0 	ORIGINAL 
::
::
:: ______________________________________________________________________________________________________________
::

@echo off


echo running batch file
set procname=ijm369

set target1=
set target2=
set target3=

:: get start date for use later
set dt0=%date%
:: get 4 digit year=dt1, 2 digit month=dt2 and  2 digit day of month=dt3
set dt1=%date:~6,4%
set dt2=%date:~3,2%
set dt3=%date:~0,2%

echo value dt0=%dt0% value dt1=%dt1% value dt2=%dt2% value dt3=%dt3%

:: get start time for use later
set tm0=%time%
set tm1=%time:~0,2%
set tm2=%time:~3,2%
set tm3=%time:~6,2%
echo value tm0=%tm0% value tm1=%tm1% value tm2=%tm2% value tm3=%tm3%

:: set the name of the log file
	set output_file= D:\Gateway\log\%procname%%dt1%%dt2%%dt3%%tm1%%tm2%%tm3%.log

	echo %date% %time% %procname% batch process started   >> %OUTPUT_FILE%


:: example copy files around to new places

:: set working values based on environment

if %userdomain%==BBPTDPSIS (	set target1=\\ga01\d$\Gateway\LiveSupportData
				set target2=\\ga02\d$\Gateway\LiveSupportData
				set /A items=2
				)
if %userdomain%==BBPTDPS   (	set target1=\\ga02\d$\Gateway\LiveSupportData
				set /A items=1
				)
if %userdomain%==ACPTDPS   ( 	set target1=\\ga01\d$\Gateway\LiveSupportData
				set target2=\\ga02\d$\Gateway\LiveSupportData
				set /A items=2
				)
	
echo %date% %time% %items% target(s) selected %target1% %target2% %target3% >> %OUTPUT_FILE%	

:: loop through items to copy files to targets

set str1=target

setlocal ENABLEDELAYEDEXPANSION

::
:: Note zip file contains payload file called IF067_037_yyyymmddhhmm.csv this must be 
:: renamed to be connect.csv
::


for /L %%a in (1,1,%items%) do ( echo %date% %time% %procname% going to copy all files from D:\Gateway\dat\processing\%procname%\ to !%str1%%%a!\connect.csv >> %OUTPUT_FILE%
				echo %items% 
				echo %%a
				xcopy /y D:\Gateway\dat\processing\%procname%\*.csv !%str1%%%a!\connect.csv*
				if !errorlevel! neq 0 set xcopyerr=!errorlevel! & goto :copyerr 				
				echo !errorlevel! 
				echo copied successfully >> %OUTPUT_FILE%
				)

echo end of copy loop. 
goto :end

:end 
echo %date% %time% %procname% Successfull end of script. >> %OUTPUT_FILE%
EXIT /B 0


:copyerr
echo copy error detected 
echo error value %xcopyerr%
if %xcopyerr%==0 ( echo Files were copied without error.) >> %OUTPUT_FILE%
if %xcopyerr%==1 ( echo No files were found to copy. ) >> %OUTPUT_FILE%
if %xcopyerr%==2 ( echo The user pressed CTRL+C to terminate xcopy.) >> %OUTPUT_FILE%
if %xcopyerr%==4 ( echo Initialization error occurred. You entered an invalid drive name or invalid syntax on the command line or there is not enough memory or disk space.) >> %OUTPUT_FILE%
if %xcopyerr%==5 ( echo Disk write error occurred.) >> %OUTPUT_FILE%

echo %date% %time% %procname% End of script with errors.  >> %OUTPUT_FILE%
EXIT /B 1


