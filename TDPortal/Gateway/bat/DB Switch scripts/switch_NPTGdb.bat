rem switch the NPTG database
osql -E  -n -SD03 -iswitch_nptgdb.sql 
if errorlevel 1 goto failure
	
echo *** NPTG database switched successfully *** 

PAUSE