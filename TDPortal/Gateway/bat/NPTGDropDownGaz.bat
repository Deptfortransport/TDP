:: _______________________________________________________________________________________
::
::  Batch File:      	NPTGDropDownGaz.bat
::  Author:          	Mitesh Modi
::
::  Built/Tested On: 	Windows 2008 Server
::
::  Purpose:  	     	Uses the weekly Nptg data import (YNX354) to populate DropDownGaz tables in AtosAdditionalData database.
::		     	This batch file is called by the NPTG.bat (which in turn is called by a TNG job) but can also be run manually.	
::
::  Last Update:     	Mitesh Modi 10/06/2010 - added process to use BCP utility to bulk import NPTG 
::						into the AtosAdditionalData database for use by Drop Down Gaz
::						data file generation functionality. 
:: _________________________________________________________________________________________
::


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
	set output_file_DDG= D:\Gateway\log\YNX354_DropDownGaz_%currdate%_%currTime%.log

	
:: start import to the AtosAdditionalData drop down gaz tables

	echo %date% %time% drop down gaz data import started >> %output_file_DDG%
	
	:: Check if its OK to start import
	echo %date% %time% checking lock is ok to start nptg drop down gaz data load >> %output_file_DDG%
	set DropDownType="Rail"
	osql -b -S localhost /E /Q "EXIT(USE AtosAdditionalData EXEC SetServerSynchStatusLock %DropDownType%)" >> %output_file_DDG%
	if errorlevel 1 goto failure
	
	
	:: Delete existing data
	echo %date% %time% deleting existing nptg drop down gaz data >> %output_file_DDG%
	osql -b -S localhost /E /Q "EXIT(USE AtosAdditionalData DELETE NPTG_RailExchanges)" >> %output_file_DDG%
	if errorlevel 1 goto failure
	
	osql -b -S localhost /E /Q "EXIT(USE AtosAdditionalData DELETE NPTG_ExchangeGroups)" >> %output_file_DDG%
	if errorlevel 1 goto failure
	
	
	:: Import new data to staging tables
	echo %date% %time% importing new nptg drop down gaz data >> %output_file_DDG%
	:: -S servername
	:: -F first row in file to copy from
	:: -t field seperator
	:: -T trusted database connection, or use  –U and –P to successfully log in
	bcp AtosAdditionalData.dbo.NPTG_RailExchanges in "D:\Gateway\dat\Processing\ynx354\Rail Exchanges.csv" -Slocalhost -F2 -t, -T -c   >> %output_file_DDG% 
	if errorlevel 1 goto failure
	
	bcp AtosAdditionalData.dbo.NPTG_ExchangeGroups in "D:\Gateway\dat\Processing\ynx354\Exchange Groups.csv" -Slocalhost -F2 -t, -T -c   >> %output_file_DDG% 
	if errorlevel 1 goto failure
			
	
	:: Transfer data to the drop down gaz data display table
	echo %date% %time% transfer staging data to the drop down gaz display table >> %output_file_DDG%
	osql -b -S localhost /E /Q "EXIT(USE AtosAdditionalData EXEC TransferDropDownDataRail 1 )" >> %output_file_DDG%
	
	
	:: Update Change Notification Version numbers
	echo %date% %time% update change notification version number >> %output_file_DDG%
	osql -b -S localhost /E /Q "EXIT(USE AtosAdditionalData EXEC UpdateChangeNotification %DropDownType%,1 )" >> %output_file_DDG%
	if errorlevel 1 goto failure
			
			
			
	echo %date% %time% error level from drop down gaz data importer %ErrorLevel% >> %output_file_DDG%
	
	echo %date% %time% drop down gaz data import ended >> %output_file_DDG%
	
	goto end
	
	
:failure
	echo failure message  								>> %output_file_DDG%
	echo ************************************************************************************  		>> %output_file_DDG%
	echo %date% %time% NPTGDropDownGaz.bat failed with errorlevel: %errorlevel%  		>> %output_file_DDG%
	echo See previous messages to see where the failure occurred.  			>> %output_file_DDG%	

	
:end
