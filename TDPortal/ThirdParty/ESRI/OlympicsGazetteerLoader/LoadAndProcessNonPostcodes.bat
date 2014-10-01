echo on
echo 1
:: OlympicsGazetteerLoader
:: <create|append> <table> <rail|stops|coach|ferry|postcode|venues|localities|exchangegroups|generic> <inputfilename> [exclusionsfilename]
:: <process> <table> [<minID> <maxID>]");

:: Loading (will take about 2 minutes) [about 50,000 records]
rem OlympicsGazetteerLoader create OlympicsGazetteer rail "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Data\Rail Exchanges.csv" "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Filters\RAIL EXCHANGE FILTERING.csv"
echo 2
OlympicsGazetteerLoader create TDLocations rail "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Data\Rail Exchanges.csv" "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Filters\RAIL EXCHANGE FILTERING.csv"
OlympicsGazetteerLoader append TDLocations stops "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Data\Stops.csv" "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Filters\NAPTAN STOP FILTERING.csv"
OlympicsGazetteerLoader append TDLocations coach "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Data\Coach Exchanges.csv" "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Filters\COACH EXCHANGE FILTERING.csv"
OlympicsGazetteerLoader append TDLocations ferry "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Data\Ferry Exchanges.csv" "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Filters\FERRY EXCHANGE FILTERING.csv"
OlympicsGazetteerLoader append TDLocations localities "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Data\Localities.csv" "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Filters\LOCALITY FILTERING.csv"
OlympicsGazetteerLoader append TDLocations exchangegroups "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Data\Exchange Groups.csv" "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Filters\EXCHANGE GROUP FILTERING.csv"
OlympicsGazetteerLoader append TDLocations venues "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Data\Olympic Venues 3.3.xml"
OlympicsGazetteerLoader append TDLocations poi "D:\TDPortal\ThirdParty\ESRI\OlympicsGazetteerLoader\Data\Points of Interest.csv"
echo 3
:: Process the data all in one go (will take about 5 hours)
:: OlympicsGazetteerLoader process OlympicsGazetteer
echo 4
:: or Process it in chunks like below in 5 cmd windows simulataneously (will take about 30 mins!)
:: this section is NOT required for TDP
:: start OlympicsGazetteerLoader process TDLocations -1 5000
:: start OlympicsGazetteerLoader process TDLocations 5001 10000
:: start OlympicsGazetteerLoader process TDLocations 10001 15000
:: start OlympicsGazetteerLoader process TDLocations 15001 20000
:: start OlympicsGazetteerLoader process TDLocations 20001 25000
:: start OlympicsGazetteerLoader process TDLocations 25001 30000
:: start OlympicsGazetteerLoader process TDLocations 30001 35000
:: start OlympicsGazetteerLoader process TDLocations 35001 40000
:: start OlympicsGazetteerLoader process TDLocations 40001 45000
:: start OlympicsGazetteerLoader process TDLocations 45001 -1

echo 5