:: _______________________________________________________________________________________
::
::  Batch File:      	Setup_WebServer.bat
::  Author:          	Mitesh Modi
::  Purpose:  	     	This batch file creates and copies necessary StaticServerContent resources to the internet folder
:: _________________________________________________________________________________________
::

:: Pages
copy *.htm D:\Inetpub\wwwroot\

:: Images
mkdir D:\Inetpub\wwwroot\Images\
:: Copy folder and files /e, overwrite readonly files /r, and suppress any confirms /y
xcopy Images D:\Inetpub\wwwroot\Images\ /e /r /y

:: Web Server specific
xcopy robots.txt D:\Inetpub\wwwroot\ /r /y
xcopy urllist.txt D:\Inetpub\wwwroot\ /r /y
xcopy *Auth.xml D:\Inetpub\wwwroot\ /r /y
xcopy sitemap.* D:\Inetpub\wwwroot\ /r /y
xcopy googlec753c6ffd79206e2.html D:\Inetpub\wwwroot\ /r /y
xcopy y_key_1608411ce754f00e.html D:\Inetpub\wwwroot\ /r /y
xcopy gss.xsl D:\Inetpub\wwwroot\ /r /y

:: Icons
xcopy *.ico D:\Inetpub\wwwroot\ /r /y

:: Apple images
xcopy *.png D:\Inetpub\wwwroot\ /r /y
