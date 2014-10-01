@echo off

@echo ************************************************************************
@echo Uninstalls the Site Status Loader Service 
@echo ************************************************************************

C:
cd C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727
call installutil C:\TDPortal\Utilities\SiteStatusLoaderService\SiteStatusLoaderService\bin\Release\AO.SiteStatusLoaderService.exe /u

@echo 

pause

