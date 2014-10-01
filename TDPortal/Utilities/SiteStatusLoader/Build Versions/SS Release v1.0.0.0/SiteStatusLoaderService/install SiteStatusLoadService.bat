@echo off

@echo ************************************************************************
@echo Installs the Site Status Loader Service as a Windows Service for Unit testing
@echo ************************************************************************

C:
cd C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727
call installutil C:\TDPortal\Utilities\SiteStatusLoaderService\SiteStatusLoaderService\bin\Release\AO.SiteStatusLoaderService.exe

@echo

pause