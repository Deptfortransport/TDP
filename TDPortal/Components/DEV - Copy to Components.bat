:: _______________________________________________________________________________________
::
::  Batch File:      	DEV - Copy to Components.bat
::  Author:          	Mitesh Modi
::  Purpose:  	     	Calls the batch files to copie services and applications to the \Components foldes.
::
:: 						FOR DEVELOPMENT MACHINES USE ONLY
:: _________________________________________________________________________________________
::

@echo off

@echo Starting

D:
cd D:\TDPortal\Components\

@echo ----------------------------------------------------------------------------
@echo BatchService

cd D:\TDPortal\Components\BatchService

@echo - stopping service
call "STOP BatchService.bat"

@echo - copying files
call "DEV - Copy BatchService to Components.bat"
@echo ----------------------------------------------------------------------------

@echo ----------------------------------------------------------------------------
@echo EventReceiver

cd D:\TDPortal\Components\EventReceiver

@echo - stopping service
call "STOP EventReceiver.bat"

@echo - copying files
call "DEV - Copy EventReceiver to Components.bat"
@echo ----------------------------------------------------------------------------

@echo ----------------------------------------------------------------------------
@echo EventReceiver2

cd D:\TDPortal\Components\EventReceiver2

@echo - stopping service
call "STOP EventReceiver.bat"

@echo - copying files
call "DEV - Copy TDEventReceiver to Components.bat"
@echo ----------------------------------------------------------------------------

@echo ----------------------------------------------------------------------------
@echo TransactionInjector

cd D:\TDPortal\Components\TransactionInjector

@echo - stopping service
call "STOP TransactionInjector.bat"

@echo - copying files
call "DEV - Copy TransactionInjector to Components.bat"
@echo ----------------------------------------------------------------------------

@echo ----------------------------------------------------------------------------
@echo WebLogReader

cd D:\TDPortal\Components\WebLogReader

@echo - copying files
call "DEV - Copy WebLogReader to Components.bat"
@echo ----------------------------------------------------------------------------


@echo ----------------------------------------------------------------------------
@echo CacheUpService

cd D:\TDPortal\Components\CacheUpService

@echo - stopping service
call "STOP CacheUpService.bat"

@echo - copying files
call "DEV - Copy CacheUpService to Components.bat"
@echo ----------------------------------------------------------------------------



@echo ----------------------------------------------------------------------------
@echo ReportDataImporter

cd D:\TDPortal\Components\ReportDataImporter

@echo - copying files
call "DEV - Copy ReportDataImporter to Components.bat"
@echo ----------------------------------------------------------------------------

@echo ----------------------------------------------------------------------------
@echo ReportDataArchiver

cd D:\TDPortal\Components\ReportDataArchiver

@echo - copying files
call "DEV - Copy ReportDataArchiver to Components.bat"
@echo ----------------------------------------------------------------------------


@echo ----------------------------------------------------------------------------
@echo Redirector

cd D:\TDPortal\Components\Redirector

@echo - copying files
call "DEV - Copy Redirector to Components.bat"
@echo ----------------------------------------------------------------------------


@echo ----------------------------------------------------------------------------
@echo StaticServerContent

cd D:\TDPortal\Components\StaticServerContent

@echo - copying files
call "DEV - Copy StaticServerContent to Components.bat"
@echo ----------------------------------------------------------------------------

@echo 
@echo Finished

pause