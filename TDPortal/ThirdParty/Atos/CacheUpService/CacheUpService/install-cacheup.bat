echo off

echo Installing Cache up service...

echo off
set SERVICE_HOME=D:\TDPortal\Components\CacheUpService
set SERVICE_EXE=wt.cacheupservice.exe
REM the following directory is for .NET 2.0 – double check the version
set INSTALL_UTIL_HOME=C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727

set NAME=CacheUpService

REM POSSIBLE PARAMETERS TO THE INSTALL UTILITY ARE:
REM ACCOUNT=user|localsystem|localservice|networkservice
Set ACCOUNT=localservice

REM SET USER=.\SJP_User
REM SET PASSWORD=!password!1

set PATH=%PATH%;%INSTALL_UTIL_HOME%

cd %SERVICE_HOME%

echo Installing Service...
REM Note – line below has wrapped
call installutil /NAME=%NAME% /ACCOUNT=%ACCOUNT% /USERNAME=%USER% /PASSWORD=%PASSWORD% %SERVICE_EXE%

echo Done.
pause
