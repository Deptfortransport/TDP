:: _______________________________________________________________________________________
::
::  Batch File:      	Copy Redirector to Components.bat
::  Author:          	Mitesh Modi
::  Purpose:  	     	This batch file copies necessary debug dll's to the \Components folder.
::
:: 						FOR DEVELOPMENT MACHINES USE ONLY
:: _________________________________________________________________________________________
::

@echo off

:: Go to Redirector folder
D:
cd D:\TDPortal\ThirdParty\Atos\Redirector

:: EXE, DLLs, Config
xcopy RedirectorPreCompiled D:\TDPortal\Components\Redirector\RedirectorPreCompiled\ /e /r /y
xcopy RedirectorPreCompiled64 D:\TDPortal\Components\Redirector\RedirectorPreCompiled64\ /e /r /y

:: Delete the readme txt files
del D:\TDPortal\Components\Redirector\RedirectorPreCompiled\ReadMe.txt
del D:\TDPortal\Components\Redirector\RedirectorPreCompiled64\ReadMe.txt
