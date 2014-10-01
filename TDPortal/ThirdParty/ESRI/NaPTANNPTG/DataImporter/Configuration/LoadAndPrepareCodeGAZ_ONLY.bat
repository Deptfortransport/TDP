echo started at %time%
echo started at %time% >> log.txt
D:\gateway\bin\utils\naptannptg\dataimporter\dataimporter.exe -c "D:\gateway\bin\utils\naptannptg\dataimporter\Configuration\LoadAndPrepareCodeGAZ_ONLY.xml" >> log.txt

echo ended at %time%
echo ended at %time% >> log.txt

pause