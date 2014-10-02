echo off
:: ______________________________________________________________________________________________________________
::
::  Batch File:   	Travelnewsfailed.bat aka SercoDummy.bat
::  Author:       	Steve Craddock    
::
::  Built/Tested On: 	Windows 2008 Server
::
::  Purpose:       	Used to set travel news message to "we are unable to display travel news currently"
::
::  Parameters:    	None
::
::  Instructions:   	Ensure all drives are correct e.g C: or D: 
::	            	Ensure IF versions and XML structures are kept up to date	
::
::  VERSION HISTORY:
::  
::  Date     	!  Version  ! Description	                                             	
::
::  16/09/2004  !  1.0      ! original (by Joe Morrissey using java)   
::              !           !   
::  22/07/2013  !  1.1      ! refactored for Win2008 and to remove need for java due to GZ:I5491639 
::              !           ! 
::              !           ! 
::              !           ! 
:: ______________________________________________________________________________________________________________

:setup working variables

	set IFNO=IF009
	set IFVER=08
	set SUPNO=010
	set IFNAME=afc743
	set MESSAGE1=We are unable to bring you live travel news at the moment. Please try later.
	set FOLDER=D:\Gateway\bat
	set HEADER=%FOLDER%\TDHeader.xml
	set PAYLOAD=%FOLDER%\SercoIncidents.xml

:clear down variables ready for use
	set DD=
	set MM=
	set YYYY=
	set currDate=
	set HH=
	set Mi=
	set AP=


:: get todays date for use later
	for /F "tokens=1-4 delims=/ " %%i in ('date /t') do (
	set DD=%%i
	set MM=%%j
	set YYYY=%%k
	)


:: get current time for use later

	for /F "tokens=1-2 delims=: " %%i in ('time /t') do (
	set HH=%%i
	set Mi=%%j
	rem set AP=%%k
	)


	echo %YYYY% %MM% %DD% %HH%:%Mi% 

:: set the name of the log file

	echo Setting the name of the log file..
	
	set output_file=D:\TDPortal\TravelNewsFailed%YYYY%%MM%%DD%_%HH%%Mi%.log	
	echo %Date% %Time% Travel News not available message import started   >> %OUTPUT_FILE%
	echo .  >> %OUTPUT_FILE%
	echo %Date% %Time% Variables: info %IFNO% IFVER %IFVER% SUPNO %SUPNO% IFNAME %IFNAME% MESSAGE1 %MESSAGE1%  >> %OUTPUT_FILE%


:createheader file
:build file contents using ^ at escape special characters

	IF EXIST %HEADER% (del /Q %HEADER% )

	echo ^<^?xml version="1.0" encoding="utf-8" ^?^> >> %HEADER%
	echo ^<DataFeedInfo xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.transportdirect.info/datafeedinfo http://localhost/datafeedinfo.xsd" xmlns="http://www.transportdirect.info/datafeedinfo" version="1.0"^> >> %HEADER%
	echo ^<SupplierID^>%SUPNO%^</SupplierID^> >> %HEADER%
	echo ^<InterfaceNumber version="%IFVER%"^>%IFNO%^</InterfaceNumber^> >> %HEADER%
	echo ^<TimePrepared^>%YYYY%-%MM%-%DD%T%HH%:%Mi%:00^</TimePrepared^> >> %HEADER%
	echo ^<DataFeed isPresent="Y"^> >> %HEADER%
	echo ^<Filename^>SercoIncidents.xml^</Filename^> >> %HEADER%
	echo ^</DataFeed^> >> %HEADER%
	echo ^</DataFeedInfo^> >> %HEADER%

	echo %Date% %Time% header file created  >> %OUTPUT_FILE%

:create payload file

	IF EXIST %PAYLOAD% (del /Q %PAYLOAD% )
	
	echo ^<^?xml version="1.0" encoding="utf-8" ^?^> >> %PAYLOAD%
	echo ^<!-- Travel news feed failure message --^> >> %PAYLOAD%
	echo ^<TrafficAndTravelNewsData xmlns="http://www.transportdirect.info/travelnews" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.transportdirect.info/travelnews ./TravelNews.xsd" version="%IFNO%%IFVER%"^> >> %PAYLOAD%
	echo ^<Incident uid="RTM999980" severity="Very Severe" incidentType="Incidents" parent_uid="0" incident_change_status="Amended" modeOfTransport="Road" location=" " incidentStatus="O" roadType="A" headlineText="%MESSAGE1%" detailText="%MESSAGE1%" reportedDatetime="2003-01-01T00:00:00" lastModifiedDatetime="2003-01-01T00:00:00" startDatetime="2003-01-01T00:00:00" expiryDatetime="2050-09-23T10:41:18" plannedIncident="false" easting="0" northing="0" publicTransportOperator="N/A"^> >> %PAYLOAD%
	echo ^<Region uid="RTM999980" regionName="London"/^> >> %PAYLOAD%
	echo ^<IncidentDataSource^>Government Agency (71)^</IncidentDataSource^> >> %PAYLOAD%
	echo ^</Incident^> >> %PAYLOAD%
	echo ^</TrafficAndTravelNewsData^> >> %PAYLOAD%

	echo %Date% %Time% payload file created  >> %OUTPUT_FILE%

:create zip package

	7z a %FOLDER%\%IFNO%_%SUPNO%%YYYY%%MM%%DD%%HH%%Mi%.zip %HEADER% %PAYLOAD%

	echo %Date% %Time% creating travel news filename %IFNO%_%SUPNO%%YYYY%%MM%%DD%%HH%%Mi%.zip  >> %OUTPUT_FILE%

:remove temporary files

	echo %Date% %Time% remove temporary files >> %OUTPUT_FILE%
	IF EXIST %HEADER% (del /Q %HEADER% )  >> %OUTPUT_FILE%
	IF EXIST %PAYLOAD% (del /Q %PAYLOAD%)  >> %OUTPUT_FILE%


:move new feed file to feed incoming folder ( set to copy for initial runs)
	
	echo %Date% %Time% move travel news file to incoming folder >> %OUTPUT_FILE%
	copy /Y %FOLDER%\%IFNO%_%SUPNO%%YYYY%%MM%%DD%%HH%%Mi%.zip d:\Gateway\dat\incoming\%IFNAME% >> %OUTPUT_FILE%

:submit travel news importer to load file

	echo %Date% %Time% run travel news importer to load file >> %OUTPUT_FILE%
	::%FOLDER%\importa.bat %IFNAME% /notransfer

	D:\Gateway\bin\td.dataimport.exe %IFNAME% /notransfer

:submit esri importer

	echo %Date% %Time% run esri importer to load file >> %OUTPUT_FILE%
	D:\Gateway\bat\EsriTravelNewsImport.bat

goto end


:end
	echo %Date% %Time% Travel News not available message import finished   >> %OUTPUT_FILE%