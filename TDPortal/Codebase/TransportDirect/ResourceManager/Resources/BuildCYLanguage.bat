::
::  Batch File:      BuildCYLanguage.bat
::  Author:          Callum Shillan     
::
::  Built/Tested On: Windows 2000 Advanced Server
::
::  Purpose:  	     Used to build Welsh language satellite assembly used in Transport Direct Web project		     	
::
::  Last Update:     Richard Philpott 2005-07-28 - Resource file added for JourneyResults 

@echo off
rem
rem Extend the PATH environment variable so that we can locate the RESGEN.EXE and the AL.EXE programs
rem
@set PATH=%PATH%;%windir%\Microsoft.NET\Framework\v2.0.50727;%ProgramFiles%\Microsoft Visual Studio 8\SDK\v2.0\Bin;

echo Assuming Microsoft.NET Framework v2.0.50727
rem
rem Invoke the BuildLanguage JScript script
rem This assumes that we are currently in the directory Web/Resources (the build process conforms to this)
rem

rem Syntax for BuildLanguage.js is as follows:
rem cscript //nologo <Javascript file> <UIculture> <Out file name without resources.dll> <TargetResource> <referenceTemplate> <sourceResource_1> <sourceResource_2> <sourceResource_3> <sourceResource_etc....>

rem Parameters passed to BuildLanguage.js are as follows:
rem 1 - UIculture - e.g. cy-GB
rem 2 - Out file - without resources.dll
rem 3 - TargetResource - where the default english resources are embedded e.g the Web assembly
rem 4 - ReferenceTemplate - specifies the assembly to get default assembly info from e.g. td.userportal.web
rem 5 - Source Resources - if more than one, they should be in a space delimited list

echo Running BuildLanguage.js
cscript //nologo BuildLanguage.js cy-GB td.common.resourcemanager Resources td.common.resourcemanager ^
 langStrings ^
 UserSurveyStrings ^
 FindAFare ^
 FaresAndTickets ^
 VisitPlanner ^
 JourneyResults ^
 RefineJourney ^
 Tools ^
 JourneyPlannerService
 
 rem now copy the contents of the build resource file to the web and enhanced exposed services 
xcopy ..\bin\Debug\cy-GB\*.* c:\inetpub\wwwroot\web\bin\cy-GB /Y /I
xcopy ..\bin\Debug\cy-GB\*.* c:\inetpub\wwwroot\EnhancedExposedServices\bin\cy-GB /Y /I