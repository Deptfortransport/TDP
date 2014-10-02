
set OLDDIR=%CD%

chdir dojo-release-1.3.2-src\util\buildscripts
CALL build.bat profile=esriuk action=release releaseDir=../../../../release releaseName= internStrings=true localeList=en,cy stripConsole=all cssOptimize=comments

chdir /d %OLDDIR%
