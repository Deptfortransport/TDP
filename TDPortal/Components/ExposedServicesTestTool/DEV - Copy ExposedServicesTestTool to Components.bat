:: _______________________________________________________________________________________
::
::  Batch File:      	Copy ExposedServicesTestTool to Components.bat
::  Author:          	Mitesh Modi
::  Purpose:  	     	This batch file copies necessary dll's from \Utilities\EnhancedExposedServicesTestTool folders to the \Components folder.
::
:: 						FOR DEVELOPMENT MACHINES USE ONLY
:: _________________________________________________________________________________________
::

@echo off

:: Go to folder
D:

:: Clean the folders
rmdir "D:\TDPortal\Components\ExposedServicesTestTool\ExposedServicesTestToolClient\" /S /Q
rmdir "D:\TDPortal\Components\ExposedServicesTestTool\TestWebService\" /S /Q

:: Build folders
mkdir "D:\TDPortal\Components\ExposedServicesTestTool\ExposedServicesTestToolClient\"
mkdir "D:\TDPortal\Components\ExposedServicesTestTool\TestWebService\"


:: Copy ExposedServicesTestToolClient
cd "D:\TDPortal\Utilities\EnhancedExposedServicesTestTool\ExposedServicesTestToolClient\"

:: /S - include all subdirectories
:: /Y - suppress prompts to overwrite
xcopy "*.xml"               	"D:\TDPortal\Components\ExposedServicesTestTool\ExposedServicesTestToolClient\"
xcopy "*.asax"              	"D:\TDPortal\Components\ExposedServicesTestTool\ExposedServicesTestToolClient\"
xcopy "*.gif"               	"D:\TDPortal\Components\ExposedServicesTestTool\ExposedServicesTestToolClient\"
xcopy "*.css"               	"D:\TDPortal\Components\ExposedServicesTestTool\ExposedServicesTestToolClient\"
xcopy "Web.config"          	"D:\TDPortal\Components\ExposedServicesTestTool\ExposedServicesTestToolClient\"
xcopy "bin\*.dll"           	"D:\TDPortal\Components\ExposedServicesTestTool\ExposedServicesTestToolClient\bin\"
xcopy "Requests\*.*"        	"D:\TDPortal\Components\ExposedServicesTestTool\ExposedServicesTestToolClient\Requests\" /S
xcopy "SoapHeaders\Empty\*.txt" "D:\TDPortal\Components\ExposedServicesTestTool\ExposedServicesTestToolClient\SoapHeaders\"
xcopy "Templates\*.aspx"    	"D:\TDPortal\Components\ExposedServicesTestTool\ExposedServicesTestToolClient\Templates\"
xcopy "xslt\*.*"        		"D:\TDPortal\Components\ExposedServicesTestTool\ExposedServicesTestToolClient\xslt\" /S

:: Copy and rename the DRTest config
xcopy "WebserviceConfig.xml.DRTest"  "D:\TDPortal\Components\ExposedServicesTestTool\ExposedServicesTestToolClient\WebserviceConfig.xml" /Y

:: Copy TestWebService
cd "D:\TDPortal\Utilities\EnhancedExposedServicesTestTool\TestWebService\"

xcopy "*.asmx"              	"D:\TDPortal\Components\ExposedServicesTestTool\TestWebService\"
xcopy "Web.config"            	"D:\TDPortal\Components\ExposedServicesTestTool\TestWebService\"
xcopy "bin\*.dll"           	"D:\TDPortal\Components\ExposedServicesTestTool\TestWebService\bin\"

:: Go to folder
cd "D:\TDPortal\Components\ExposedServicesTestTool"