rem switch the GAZ database
osql -E  -n -SD03 -iswitch_GAZdb.sql 
if errorlevel 1 goto failure
	
echo *** GAZ database switched successfully *** 

PAUSE