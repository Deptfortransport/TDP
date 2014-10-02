:: *************************************************************************************
:: NAME 		: UPDATE_retro_folders.bat
:: DESCRIPTION  	: This has been written to supplement the initial 
:: Create_retro_folders.bat script. It is intended to be run after the initial installation 
:: script to modify the folders to reflect subsequent requirements. It should also be run 
:: after a new deployment to update the Oldbookmarkredirector.dll
::
:: AUTHOR		: Steve Craddock
:: *************************************************************************************
:: updated for IR5009 
:: updated for IR5079
:: updated for IR5083

:: placeholder the following IR's
:: IR5083
:: to indicate this batchfile should be run to implement the modified Oldbookmarkredirector.dll
::

:: IR5079
:: create dummy file for an IIS redirect to use, to redirect /web/ to the new /web2/ site
:: once the file has been added, the default document for folder /web/ must be updated to include a permanent redirect to /default.aspx
echo creating DUMMY files for manual permanent redirect configuration using IIS

echo Legacy redirector for /web/ folder > D:\inetpub\wwwroot\web\redirect_web.htm

echo Legacy redirector for /transportdirect/en/ folder > D:\inetpub\wwwroot\transportdirect\en\Home.htm
echo Legacy redirector for /transportdirect/cy/ folder > D:\inetpub\wwwroot\transportdirect\cy\Home.htm

echo Legacy redirector for /transportdirect/en/LiveTravel/ folder > D:\inetpub\wwwroot\transportdirect\en\LiveTravel\DepartureBoards.htm
echo Legacy redirector for /transportdirect/cy/LiveTravel/ folder > D:\inetpub\wwwroot\transportdirect\cy\LiveTravel\DepartureBoards.htm
echo Legacy redirector for /transportdirect/en/LiveTravel/ folder > D:\inetpub\wwwroot\transportdirect\en\LiveTravel\TravelNews.htm
echo Legacy redirector for /transportdirect/cy/LiveTravel/ folder > D:\inetpub\wwwroot\transportdirect\cy\LiveTravel\TravelNews.htm

echo Legacy redirector for /transportdirect/en/Printer/ folder > D:\inetpub\wwwroot\transportdirect\en\Printer\AboutUs.htm
echo Legacy redirector for /transportdirect/cy/Printer/ folder > D:\inetpub\wwwroot\transportdirect\cy\Printer\AboutUs.htm

echo Legacy redirector for /web/Downloads/ folder > D:\inetpub\wwwroot\web\Downloads\TransportDirectCO2Data.pdf
echo Legacy redirector for /web/Downloads/ folder > D:\inetpub\wwwroot\web\Downloads\BusinessLinksBrochure.pdf


echo copying UPDATED redirector dll to target folders

copy /Y d:\Inetpub\wwwroot\web2\bin\OldBookmarkRedirector.dll D:\Inetpub\wwwroot\TransportDirect\bin\
copy /Y d:\Inetpub\wwwroot\web2\bin\OldBookmarkRedirector.dll D:\Inetpub\wwwroot\Directgov\bin\
copy /Y d:\Inetpub\wwwroot\web2\bin\OldBookmarkRedirector.dll D:\Inetpub\wwwroot\VisitBritain\bin\
copy /Y d:\Inetpub\wwwroot\web2\bin\OldBookmarkRedirector.dll D:\Inetpub\wwwroot\BBC\bin\
copy /Y d:\Inetpub\wwwroot\web2\bin\OldBookmarkRedirector.dll D:\Inetpub\wwwroot\web\bin\

pause