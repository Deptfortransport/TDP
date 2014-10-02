rem switch the additionaldata database
osql -E  -n -SD03 -iswitch_additionaldatadb.sql 
if errorlevel 1 goto failure
	
echo *** AdditionalData database switched successfully *** 

PAUSE