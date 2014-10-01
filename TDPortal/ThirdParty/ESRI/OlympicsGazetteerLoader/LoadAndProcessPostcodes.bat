
REM OlympicsGazetteerLoader
REM <create|append> <table> <rail|stops|coach|ferry|postcode|venues|localities|exchangegroups|generic> <inputfilename> [exclusionsfilename]
REM <process> <table> [<minID> <maxID>]");

REM Load POSTCODES into separate table (there are about 1.6m)
OlympicsGazetteerLoader create OlympicsGazetteerPostcodes postcode "D:\Gateway\bin\Utils\OlympicsGazetteerLoader\CodePoint.csv"

REM runs these in separate cmd windows to process postcodes (will take about 8 hours)
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes -1 100000
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes 100001 200000
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes 200001 300000
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes 300001 400000
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes 400001 500000
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes 500001 600000
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes 600001 700000
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes 700001 800000
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes 800001 900000
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes 900001 1000000
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes 1000001 1100000
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes 1100001 1200000
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes 1200001 1300000
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes 1300001 1400000
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes 1400001 1500000
start OlympicsGazetteerLoader process OlympicsGazetteerPostcodes 1500001 -1
