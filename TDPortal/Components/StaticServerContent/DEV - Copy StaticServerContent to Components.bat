:: _______________________________________________________________________________________
::
::  Batch File:      	Copy StaticServerContent to Components.bat
::  Author:          	Mitesh Modi
::  Purpose:  	     	This batch file copies necessary StaticServerContent resources to the \Components folder.
::
:: 						FOR DEVELOPMENT MACHINES USE ONLY
:: _________________________________________________________________________________________
::

@echo off

:: Go to StaticServerContent folder
D:
cd D:\TDPortal\Utilities\StaticServerContent

:: Create output directories
mkdir D:\TDPortal\Components\StaticServerContent\SorryServer\
mkdir D:\TDPortal\Components\StaticServerContent\WebServer\

:: Pages
copy *.htm D:\TDPortal\Components\StaticServerContent\SorryServer\
copy *.htm D:\TDPortal\Components\StaticServerContent\WebServer\

:: Images
:: Copy folder and files /e, overwrite readonly files /r, and suppress any confirms /y
xcopy Images D:\TDPortal\Components\StaticServerContent\SorryServer\Images\ /e /r /y
xcopy Images D:\TDPortal\Components\StaticServerContent\WebServer\Images\ /e /r /y

:: Sorry Server specific
xcopy SorryServer D:\TDPortal\Components\StaticServerContent\SorryServer\ /e /r /y

:: Web Server specific
xcopy WebServer D:\TDPortal\Components\StaticServerContent\WebServer\ /e /r /y
