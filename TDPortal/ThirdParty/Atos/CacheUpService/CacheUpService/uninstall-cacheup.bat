echo off
set SERVICE_HOME=D:\TDPortal\Components\CacheUpService
set SERVICE_EXE=wt.cacheupservice.exe
REM the following directory is for .NET 2.0 – double check the version
set INSTALL_UTIL_HOME=C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727

REM The name of the installation
set NAME=CacheUpService

set PATH=%PATH%;%INSTALL_UTIL_HOME%

cd %SERVICE_HOME%
echo Uninstalling Cache Up Service...

call installutil /u /name=%NAME% %SERVICE_EXE%

echo Done.
pause
