:: _______________________________________________________________________________________
::
::  Batch File:      	Setup_SorryServer.bat
::  Author:          	Mitesh Modi
::  Purpose:  	     	This batch file creates and copies necessary StaticServerContent resources to the internet folder
:: _________________________________________________________________________________________
::

:: Pages
copy *.htm D:\Inetpub\wwwroot\

:: Images
mkdir D:\Inetpub\wwwroot\Images\

xcopy Images D:\Inetpub\wwwroot\Images\ /e /r /y

:: Sorry Server specific
xcopy robots.txt D:\Inetpub\wwwroot\
