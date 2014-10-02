:: _______________________________________________________________________________________
::
::  Batch File:      	CopyDLLsToGateway.bat
::  Author:          	Mitesh Modi
::
::  Purpose:  	     	Copies necessary debug dll's from \TDPortal\Codebase folders to the \Gateway\bin folder.
:: 			FOR DEVELOPMENT MACHINES USE ONLY
:: _________________________________________________________________________________________
::

@echo off

:: Common DLLs
copy D:\TDPortal\Codebase\TransportDirect\Common\bin\Debug\td.common.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\DatabaseInfrastructure\bin\Debug\td.common.databaseinfrastructure.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\EnhancedExposedServicesCommon\bin\Debug\td.enhancedexposedservices.common.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\EventLoggingService\bin\Debug\td.common.logging.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\PropertiesService\Properties\bin\Debug\td.common.propertyservice.properties.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\PropertiesService\DatabasePropertyProvider\bin\Debug\td.common.propertyservice.databasepropertyprovider.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\PropertiesService\FilePropertyProvider\bin\Debug\td.common.propertyservice.filepropertyprovider.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\ResourceManager\bin\Debug\td.common.resourcemanager.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\ReportDataProvider\TDPCustomEvents\bin\Debug\td.reportdataprovider.tdpcustomevents.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\ServiceDiscovery\bin\Debug\td.common.servicediscovery.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\ThemeInfrastructure\bin\Debug\TD.ThemeInfrastructure.dll D:\Gateway\bin\ 

copy D:\TDPortal\Codebase\TransportDirect\AdditionalDataModule\bin\Debug\td.userportal.additionaldatamodule.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\AirDataProvider\bin\Debug\td.userportal.airdataprovider.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\DataServices\bin\Debug\td.userportal.dataservices.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\ExternalLinkService\bin\Debug\td.userportal.externallinkservice.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\ScriptRepository\bin\Debug\td.userportal.scriptrepository.dll D:\Gateway\bin\ 

:: Importer EXEs
copy D:\TDPortal\Codebase\TransportDirect\DataGatewayFramework\bin\Debug\td.datagateway.framework.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\DataGatewayExport\bin\Debug\td.dataexport.exe D:\Gateway\bin\ 

copy D:\TDPortal\Codebase\TransportDirect\DataGatewayFTPMain\bin\Debug\td.ftppull.exe D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\DataGatewayFTPMain\bin\Debug\td.ftppull.exe.config D:\Gateway\bin\

copy D:\TDPortal\Codebase\TransportDirect\DataGatewayImportMain\bin\Debug\td.dataimport.exe D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\DataGatewayImportMain\bin\Debug\td.dataimport.exe.config D:\Gateway\bin\

copy D:\TDPortal\Codebase\TransportDirect\AvailabilityDataMaintenance\bin\Debug\td.userportal.availabilitydatamaintenance.exe D:\Gateway\bin\ 

:: Specific DLLs
copy D:\TDPortal\Codebase\TransportDirect\JourneyControl\bin\Debug\td.userportal.journeycontrol.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\LocationInformationService\bin\Debug\td.userportal.locationinformationservice.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\LocationService\bin\Debug\td.userportal.locationservice.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\PricingMessages\bin\Debug\td.userportal.pricingmessages.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\PricingRetail\bin\Debug\td.userportal.pricingretail.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\PricingRetail\bin\Debug\td.userportal.retailbusiness.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\SeasonalNoticeBoardImport\bin\Debug\td.userportal.seasonalnoticeboardimport.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\TravelNews\bin\Debug\td.userportal.travelnews.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\TravelNewsInterface\bin\Debug\td.userportal.travelnewsinterface.dll D:\Gateway\bin\ 
copy D:\TDPortal\Codebase\TransportDirect\ZonalServices\bin\Debug\td.userportal.zonalservices.dll D:\Gateway\bin\ 

:: Third Party DLLs
copy D:\TDPortal\ThirdParty\Atkins\CJP\AdditionalDataAccessModule.dll D:\Gateway\bin\ 
copy D:\TDPortal\ThirdParty\Atkins\CJP\td.CJP.dll D:\Gateway\bin\ 
copy D:\TDPortal\ThirdParty\Atkins\CJP\td.CJPInterface.dll D:\Gateway\bin\ 
copy D:\TDPortal\ThirdParty\Atkins\CJP\td.FaresProvider.dll D:\Gateway\bin\ 
copy D:\TDPortal\ThirdParty\Atkins\CJP\td.FaresProviderInterface.dll D:\Gateway\bin\ 
copy D:\TDPortal\ThirdParty\Atkins\CJP\td.InterchangeAccessModule.dll D:\Gateway\bin\ 
copy D:\TDPortal\ThirdParty\Atkins\CJP\td.JourneyWeb.dll D:\Gateway\bin\ 
copy D:\TDPortal\ThirdParty\Atkins\CJP\td.Logging.dll D:\Gateway\bin\ 
copy D:\TDPortal\ThirdParty\Atkins\CJP\td.NPTGAccessModule.dll D:\Gateway\bin\ 
copy D:\TDPortal\ThirdParty\Atkins\CJP\td.RoadInterface.dll D:\Gateway\bin\ 
copy D:\TDPortal\ThirdParty\Atkins\CJP\td.RoadRoutingEngine.dll D:\Gateway\bin\ 
copy D:\TDPortal\ThirdParty\Atkins\CJP\td.RoutingNetwork.dll D:\Gateway\bin\ 
copy D:\TDPortal\ThirdParty\Atkins\CJP\td.RoutingNetworkAccessModule.dll D:\Gateway\bin\ 
copy D:\TDPortal\ThirdParty\Atkins\CJP\td.Shared.dll D:\Gateway\bin\ 
copy D:\TDPortal\ThirdParty\Atkins\CJP\td.TTBOInterface.dll D:\Gateway\bin\ 

copy D:\TDPortal\ThirdParty\ESRI\td.interactivemapping.dll D:\Gateway\bin\ 

:: copy D:\TDPortal\ThirdParty\NUnit\nunit.framework.dll D:\Gateway\bin\ 

:end