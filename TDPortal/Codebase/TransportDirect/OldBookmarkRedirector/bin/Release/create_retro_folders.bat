:: *************************************************************************************
:: NAME 		: Create_retro_folders.bat
:: DESCRIPTION  	: This has been written to create the initial legacy redirection
:: folders for DEL10. The UPDATE script MUST be run after the initial installation to 
:: modify the folders to reflect subsequent requirements. 
::
:: AUTHOR		: Steve Craddock
:: *************************************************************************************



:: create retro folders for old TDP URLS
:: updated for IR4870
:: updated for IR4871
:: updated for IR4872
:: updated for IR4873
:: updated for IR4874
:: updated for IR4880
:: updated for RS07904a regression test. IR4890
:: updated for IR4901
:: updated for IR4910 - more business links...


:: folder \TransportDirect
if not exist D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning ( mkdir D:\Inetpub\wwwroot\TransportDirect\en ) 
if not exist D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning ( mkdir D:\Inetpub\wwwroot\TransportDirect\cy ) 

:: folder \TDOnTheMove
if not exist D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning ( mkdir D:\Inetpub\wwwroot\TransportDirect\en\TDOnTheMove ) 
if not exist D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning ( mkdir D:\Inetpub\wwwroot\TransportDirect\cy\TDOnTheMove ) 

:: folder \JourneyPlanning
if not exist D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning ( mkdir D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning ) 
if not exist D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning ( mkdir D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning ) 

:: folder \Maps
if not exist D:\Inetpub\wwwroot\TransportDirect\en\Maps ( mkdir D:\Inetpub\wwwroot\TransportDirect\en\Maps ) 
if not exist D:\Inetpub\wwwroot\TransportDirect\cy\Maps ( mkdir D:\Inetpub\wwwroot\TransportDirect\cy\Maps ) 

:: folder \LiveTravel see IR 4872
if not exist D:\Inetpub\wwwroot\TransportDirect\en\LiveTravel ( mkdir D:\Inetpub\wwwroot\TransportDirect\en\LiveTravel ) 
if not exist D:\Inetpub\wwwroot\TransportDirect\cy\LiveTravel ( mkdir D:\Inetpub\wwwroot\TransportDirect\cy\LiveTravel ) 

:: folder \Help see IR 4873
if not exist D:\Inetpub\wwwroot\TransportDirect\en\Help ( mkdir D:\Inetpub\wwwroot\TransportDirect\en\Help ) 
if not exist D:\Inetpub\wwwroot\TransportDirect\cy\Help ( mkdir D:\Inetpub\wwwroot\TransportDirect\cy\Help ) 

:: folder \Tools
if not exist D:\Inetpub\wwwroot\TransportDirect\en\Tools ( mkdir D:\Inetpub\wwwroot\TransportDirect\en\Tools ) 
if not exist D:\Inetpub\wwwroot\TransportDirect\cy\Tools ( mkdir D:\Inetpub\wwwroot\TransportDirect\cy\Tools ) 

:: folder \About see IR 5009
if not exist D:\Inetpub\wwwroot\TransportDirect\en\About ( mkdir D:\Inetpub\wwwroot\TransportDirect\en\About ) 
if not exist D:\Inetpub\wwwroot\TransportDirect\cy\About ( mkdir D:\Inetpub\wwwroot\TransportDirect\cy\About )

:: folder \Printer
if not exist D:\Inetpub\wwwroot\TransportDirect\en\Printer ( mkdir D:\Inetpub\wwwroot\TransportDirect\en\Printer ) 
if not exist D:\Inetpub\wwwroot\TransportDirect\cy\Printer ( mkdir D:\Inetpub\wwwroot\TransportDirect\cy\Printer ) 

:: folder \FindA
if not exist D:\Inetpub\wwwroot\TransportDirect\en\FindA ( mkdir D:\Inetpub\wwwroot\TransportDirect\en\FindA ) 
if not exist D:\Inetpub\wwwroot\TransportDirect\cy\FindA ( mkdir D:\Inetpub\wwwroot\TransportDirect\cy\FindA ) 

:: folder \ContactUs
if not exist D:\Inetpub\wwwroot\TransportDirect\en\ContactUs ( mkdir D:\Inetpub\wwwroot\TransportDirect\en\ContactUs ) 
if not exist D:\Inetpub\wwwroot\TransportDirect\cy\ContactUs ( mkdir D:\Inetpub\wwwroot\TransportDirect\cy\ContactUs ) 

:: folder \Sitemap
if not exist D:\Inetpub\wwwroot\TransportDirect\en\Sitemap ( mkdir D:\Inetpub\wwwroot\TransportDirect\en\Sitemap ) 
if not exist D:\Inetpub\wwwroot\TransportDirect\cy\Sitemap ( mkdir D:\Inetpub\wwwroot\TransportDirect\cy\Sitemap ) 


:: Other folders
if not exist D:\Inetpub\wwwroot\TransportDirect\bin ( mkdir D:\Inetpub\wwwroot\TransportDirect\bin ) 
if not exist D:\Inetpub\wwwroot\Web\bin ( mkdir D:\Inetpub\wwwroot\Web\bin ) 
if not exist D:\Inetpub\wwwroot\Web\Downloads ( mkdir D:\Inetpub\wwwroot\Web\Downloads ) 
if not exist D:\Inetpub\wwwroot\Web\Templates ( mkdir D:\Inetpub\wwwroot\Web\Templates ) 

::updated for IR4901
:: Note a redirect is also required in IIS to folder /Web2/App_Themes/TransportDirect/images
if not exist D:\Inetpub\wwwroot\Web\images ( mkdir D:\Inetpub\wwwroot\Web\images ) 
if not exist D:\Inetpub\wwwroot\TransportDirect\en\iframes ( mkdir D:\Inetpub\wwwroot\TransportDirect\en\iframes ) 

if not exist D:\Inetpub\wwwroot\Directgov\bin ( mkdir D:\Inetpub\wwwroot\Directgov\bin ) 
if not exist D:\Inetpub\wwwroot\Directgov\en\JourneyPlanning ( mkdir D:\Inetpub\wwwroot\Directgov\en\JourneyPlanning ) 
if not exist D:\Inetpub\wwwroot\Directgov\cy\JourneyPlanning ( mkdir D:\Inetpub\wwwroot\Directgov\cy\JourneyPlanning ) 

if not exist D:\Inetpub\wwwroot\VisitBritain\bin ( mkdir D:\Inetpub\wwwroot\VisitBritain\bin ) 
if not exist D:\Inetpub\wwwroot\VisitBritain\en\JourneyPlanning ( mkdir D:\Inetpub\wwwroot\VisitBritain\en\JourneyPlanning ) 
if not exist D:\Inetpub\wwwroot\VisitBritain\cy\JourneyPlanning ( mkdir D:\Inetpub\wwwroot\VisitBritain\cy\JourneyPlanning ) 

if not exist D:\Inetpub\wwwroot\BBC\bin ( mkdir D:\Inetpub\wwwroot\BBC\bin ) 
if not exist D:\Inetpub\wwwroot\BBC\en\JourneyPlanning ( mkdir D:\Inetpub\wwwroot\BBC\en\JourneyPlanning ) 
if not exist D:\Inetpub\wwwroot\BBC\cy\JourneyPlanning ( mkdir D:\Inetpub\wwwroot\BBC\cy\JourneyPlanning ) 



:: clean target folders

	del D:\Inetpub\wwwroot\TransportDirect\*.* /s /q
	del D:\Inetpub\wwwroot\Directgov\*.* /s /q
	del D:\Inetpub\wwwroot\VisitBritain\*.* /s /q
	del D:\Inetpub\wwwroot\BBC\*.* /s /q
	del D:\Inetpub\wwwroot\web\*.* /s /q




:: create and copy the redirector pages into the correct place.

echo copying Default.aspx redirects

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Default.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Default.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\Web\Templates\Default.aspx



echo creating Home.aspx redirects

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Home.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Home.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Maps\Home.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Maps\Home.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\LiveTravel\Home.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\LiveTravel\Home.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Help\Home.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Help\Home.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Tools\Home.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Tools\Home.aspx



echo creating TransportDirect redirector templates

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\SeasonalNoticeBoard.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\SeasonalNoticeBoard.aspx



echo creating MAPS redirector templates

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Maps\JourneyPlannerLocationMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Maps\JourneyPlannerLocationMap.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Maps\FindMapInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Maps\FindMapInput.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Maps\NetworkMaps.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Maps\NetworkMaps.aspx


echo creating LIVETRAVEL redirector templates

:: the extension is delibrately missing off the end of these
:: an IIS redirect is required to make this work correctly see IR 4872 for details
::
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\LiveTravel\TravelNews
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\LiveTravel\TravelNews

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\LiveTravel\TravelNews.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\LiveTravel\TravelNews.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\LiveTravel\DepartureBoards.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\LiveTravel\DepartureBoards.aspx

:: added for IR4880
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\LiveTravel\TNLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\LiveTravel\TNLandingPage.aspx



echo creating HELP redirector templates

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Help\HelpToolbar.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Help\HelpToolbar.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Help\HelpCarbon.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Help\HelpCarbon.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Help\HelpCosts.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Help\HelpCosts.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Help\NewHelp.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Help\NewHelp.aspx


echo creating TOOLS redirector templates

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Tools\BusinessLinks.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Tools\BusinessLinks.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Tools\ToolbarDownload.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Tools\ToolbarDownload.aspx



echo creating ABOUT redirector templates

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\About\AboutUs.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\About\AboutUs.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\About\Accessibility.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\About\Accessibility.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\About\DataProviders.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\About\DataProviders.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\About\PrivacyPolicy.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\About\PrivacyPolicy.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\About\RelatedSites.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\About\RelatedSites.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\About\TermsConditions.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\About\TermsConditions.aspx



echo creating FindA redirector templates

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\FindA\FindTrunkInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\FindA\FindTrunkInput.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\FindA\FindTrainInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\FindA\FindTrainInput.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\FindA\FindCarInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\FindA\FindCarInput.aspx


echo creating ContactUs redirector templates

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\ContactUs\FeedbackPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\ContactUs\FeedbackPage.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\ContactUs\FeedbackInitialPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\ContactUs\FeedbackInitialPage.aspx


echo creating Sitemap redirector templates

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Sitemap\SiteMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Sitemap\SiteMap.aspx



echo creating DIRECTGOV VISITBRITAIN BBC redirector templates

:: added for RS07904a regression test. IR4890
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\Directgov\en\Journeyplanning\JPLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\Directgov\cy\Journeyplanning\JPLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\Directgov\en\Journeyplanning\CO2LandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\Directgov\cy\Journeyplanning\CO2LandingPage.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\VisitBritain\en\Journeyplanning\JPLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\VisitBritain\cy\Journeyplanning\JPLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\VisitBritain\en\Journeyplanning\CO2LandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\VisitBritain\cy\Journeyplanning\CO2LandingPage.aspx

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\BBC\en\Journeyplanning\JPLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\BBC\cy\Journeyplanning\JPLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\BBC\en\Journeyplanning\CO2LandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\BBC\cy\Journeyplanning\CO2LandingPage.aspx



echo creating IFRAME redirector templates

:: added for IR4901 for rail easy iframe redirector.
:: the extension is delibrately missing off the end of the second one
:: HOWEVER, an IIS redirect is required to make this work correctly to /Web2/iframes/iFrameJourneyPlanning.aspx$Q

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\iframes\iFrameJourneyPlanning.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\iframes\iFrameJourneyPlanning

:: added for IR4910 for train and bus and lastminute iframe redirector.
:: the extension is delibrately missing off the ends
:: HOWEVER, an IIS redirect is required to make them work correctly

:: redirect this one to /Web2/iframes/iFrameJourneyPlanning.aspx$Q
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\iframes\JourneyPlanning

:: redirect this one to /Web2/iframes/iFrameJourneyPlanning.aspx$Q
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\iframes\FindaPlace



:: removed as this causes a referal loop
::copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\Web\Default.aspx


echo creating TDOnTheMove redirector templates

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\TDOnTheMove\TDOnTheMove.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\TDOnTheMove\TDOnTheMove.aspx



echo creating JOURNEYPLANNING ENGLISH redirector templates

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\AboutUs.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\AdjustFullItinerarySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\BusinessLinks.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\CarDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\CarParkInformation.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\ClaimsPolicy.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\ClaimsPolicyPrinter.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\CO2LandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\CompareAdjustedJourney.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\DepartureBoards.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\DetailedLegMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\Details.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\ErrorPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\ErrorPageCookies.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\ExtendedFullItinerarySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\ExtendJourneyInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\ExtensionResultsSummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FeedbackViewer.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindAStationInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindBusInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindCarInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindCarParkInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindCarParkMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindCarParkResults.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindCoachInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindCycleInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindFareDateSelection.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindFareTicketSelection.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindFareTicketSelectionReturn.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindFareTicketSelectionSingles.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindFlightInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindNearestLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindStationMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindStationResults.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindTrainCostInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindTrainInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\FindTrunkInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\HelpFullJP.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\HelpFullJPrinter.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\HelpCarbon.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\HelpCosts.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\HelpToolbar.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\Home.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\iFrameFindAPlace.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\iFrameJourneyPlanning.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneyAccessibility.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneyAdjust.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneyDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneyEmissions.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneyEmissionsCompare.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneyEmissionsCompareJourney.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneyFares.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneyLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneyMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneyOverview.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneyPlannerAmbiguity.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneyPlannerDefault.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneyPlannerInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneyPlannerLocationMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneyReplanInputPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JourneySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\JPLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\LocationInformation.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\LocationLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\LogViewer.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\Maintenance.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\MapsDefault.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\NewHelp.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\ParkAndRide.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\ParkAndRideInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableAdjustFullItinerarySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableCarDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableCompareAdjustedJourney.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableExtendedFullItinerarySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableExtensionResultsSummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableFindCarParkMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableFindCarParkResults.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableFindFareDateSelection.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableFindFareTicketSelection.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableFindFareTicketSelectionReturn.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableFindFareTicketSelectionSingles.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableFindStationMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableFindStationResults.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableJourneyDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableJourneyEmissions.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableJourneyEmissionsCompare.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableJourneyEmissionsCompareJourney.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableJourneyFares.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableJourneyMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableJourneyMapInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableJourneyOverview.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableJourneySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableParkAndRide.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableRefineDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableRefineMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableRefineTickets.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableReplanFullItinerarySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableServiceDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableTicketRetailers.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableTicketRetailersHandOff.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableTrafficMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableTravelNews.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\PrintableVisitPlannerResults.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\RefineDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\RefineJourneyPlan.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\RefineMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\RefineTickets.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\ReplanFullItinerarySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\RetailerInformation.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\SeasonalNoticeBoard.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\ServiceDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\SiteMapDefault.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\SorryPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\SpecialNoticeBoard.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\StaticDefault.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\StaticNoPrint.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\StaticPrinterFriendly.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\StaticWithoutPrint.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\TDOnTheMove.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\TicketRetailers.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\TicketRetailersHandOff.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\TicketUpgrade.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\TimeOut.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\TNLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\ToolbarDownload.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\TrafficMaps.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\TravelNews.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\UserSurvey.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\VersionViewer.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\VisitPlannerInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\VisitPlannerResults.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\WaitPage.aspx


echo creating JOURNEY PLANNING WELSH redirector templates

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\AboutUs.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\AdjustFullItinerarySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\BusinessLinks.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\CarDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\CarParkInformation.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\ClaimsPolicy.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\ClaimsPolicyPrinter.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\CO2LandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\CompareAdjustedJourney.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\DepartureBoards.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\DetailedLegMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\Details.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\ErrorPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\ErrorPageCookies.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\ExtendedFullItinerarySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\ExtendJourneyInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\ExtensionResultsSummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FeedbackViewer.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindAStationInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindBusInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindCarInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindCarParkInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindCarParkMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindCarParkResults.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindCoachInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindCycleInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindFareDateSelection.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindFareTicketSelection.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindFareTicketSelectionReturn.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindFareTicketSelectionSingles.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindFlightInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindNearestLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindStationMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindStationResults.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindTrainCostInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindTrainInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\FindTrunkInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\HelpFullJP.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\HelpFullJPrinter.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\HelpCarbon.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\HelpCosts.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\HelpToolbar.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\Home.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\iFrameFindAPlace.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\iFrameJourneyPlanning.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneyAccessibility.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneyAdjust.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneyDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneyEmissions.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneyEmissionsCompare.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneyEmissionsCompareJourney.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneyFares.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneyLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneyMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneyOverview.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneyPlannerAmbiguity.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneyPlannerDefault.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneyPlannerInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneyPlannerLocationMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneyReplanInputPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JourneySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\JPLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\LocationInformation.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\LocationLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\LogViewer.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\Maintenance.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\MapsDefault.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\NewHelp.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\ParkAndRide.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\ParkAndRideInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableAdjustFullItinerarySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableCarDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableCompareAdjustedJourney.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableExtendedFullItinerarySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableExtensionResultsSummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableFindCarParkMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableFindCarParkResults.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableFindFareDateSelection.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableFindFareTicketSelection.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableFindFareTicketSelectionReturn.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableFindFareTicketSelectionSingles.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableFindStationMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableFindStationResults.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableJourneyDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableJourneyEmissions.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableJourneyEmissionsCompare.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableJourneyEmissionsCompareJourney.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableJourneyFares.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableJourneyMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableJourneyMapInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableJourneyOverview.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableJourneySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableParkAndRide.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableRefineDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableRefineMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableRefineTickets.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableReplanFullItinerarySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableServiceDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableTicketRetailers.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableTicketRetailersHandOff.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableTrafficMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableTravelNews.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\PrintableVisitPlannerResults.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\RefineDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\RefineJourneyPlan.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\RefineMap.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\RefineTickets.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\ReplanFullItinerarySummary.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\RetailerInformation.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\SeasonalNoticeBoard.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\ServiceDetails.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\SiteMapDefault.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\SorryPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\SpecialNoticeBoard.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\StaticDefault.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\StaticNoPrint.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\StaticPrinterFriendly.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\StaticWithoutPrint.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\TDOnTheMove.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\TicketRetailers.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\TicketRetailersHandOff.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\TicketUpgrade.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\TimeOut.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\TNLandingPage.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\ToolbarDownload.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\TrafficMaps.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\TravelNews.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\UserSurvey.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\VersionViewer.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\VisitPlannerInput.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\VisitPlannerResults.aspx
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\WaitPage.aspx


:: the extension is delibrately missing off the end of these
:: an IIS redirect is required to make this work correctly see IR 4872 for details
::
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\Home
copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\TransportDirect\cy\Journeyplanning\Home



:: copy templates to web\templates folder

echo copying redirect to web\templates folder

copy D:\Inetpub\wwwroot\TransportDirect\en\Journeyplanning\*.* D:\Inetpub\wwwroot\Web\Templates\ 


:: the extension is delibrately missing off the end of these
:: an IIS redirect is required to make this work correctly see IR 4872 for details

copy /Y D:\Inetpub\wwwroot\web2\Default_redirector_template.aspx D:\Inetpub\wwwroot\Web\Templates\Home



:: copy the redirector dll into the correct place.

echo copying redirector dll

copy /Y D:\Inetpub\wwwroot\web2\bin\OldBookmarkRedirector.dll D:\Inetpub\wwwroot\TransportDirect\bin\
copy /Y D:\Inetpub\wwwroot\web2\bin\OldBookmarkRedirector.dll D:\Inetpub\wwwroot\Directgov\bin\
copy /Y D:\Inetpub\wwwroot\web2\bin\OldBookmarkRedirector.dll D:\Inetpub\wwwroot\VisitBritain\bin\
copy /Y D:\Inetpub\wwwroot\web2\bin\OldBookmarkRedirector.dll D:\Inetpub\wwwroot\BBC\bin\
copy /Y D:\Inetpub\wwwroot\web2\bin\OldBookmarkRedirector.dll D:\Inetpub\wwwroot\web\bin\


echo
echo **   Remember to run UPDATE_retro_folders.bat script as well !!!!    **
echo

pause