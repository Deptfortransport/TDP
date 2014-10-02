echo off 
::.................................................................................!
:: TransportDirect Portal                                                          !
::                                                                                 !
:: ESRI Carpark data importer batch file                                           !
::                                                                                 !
:: Version 1.0                                                                     !
:: Created 15/02/2008 By SC                                                        !
::                                                                                 !   
::                                                                                 !
::.................................................................................!
:: Date     !  Version  ! Description	                                           !   	
:: 15/02    !  1.0      ! original                                                 !
:: 22/04    !  1.1      ! added delete to remove old files                         !
:: 07/08    !  1.2      ! added additional import lines for all environments       !
:: 27/07/09 !  1.3      ! updated for Tech refresh                                 !

:: create data import folder if required.

if not exist D:\dataload\carparks ( mkdir d:\dataload\carparks )

echo removing old data from ESRI import folder

del /F /Q D:\dataload\carparks\*.xml
				
echo copying date to the ESRI import folder

copy D:\Gateway\dat\Processing\etd565\carpark*.xml D:\dataload\carparks\

echo running ESRI carparks importer

cd D:\Gateway\bin\Utils\MapCarParkLoader\

:: D:\Gateway\bin\Utils\MapCarParkLoader\mapcarparkloader.exe applicationpropertiesD03.xml D:\dataload\carparks

D:\Gateway\bin\Utils\MapCarParkLoader\mapcarparkloader.exe D:\Gateway\bin\Utils\MapCarParkLoader\applicationpropertiesD06.xml D:\dataload\carparks

::D:\Gateway\bin\Utils\MapCarParkLoader\mapcarparkloader.exe applicationpropertiesD07.xml D:\dataload\carparks


exit errorlevel