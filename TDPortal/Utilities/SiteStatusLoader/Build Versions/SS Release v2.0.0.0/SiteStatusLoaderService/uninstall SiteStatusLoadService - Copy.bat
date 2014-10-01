@echo off

@echo ************************************************************************
@echo Uninstalls the Site Status Loader Service 
@echo ************************************************************************

C:
cd C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727
call installutil "C:\TDPortal\Utilities\SiteStatusLoader\SS Release v2.0.0.0\SiteStatusLoaderService\AO.SiteStatusLoaderService.exe" /u

@echo 

pause

