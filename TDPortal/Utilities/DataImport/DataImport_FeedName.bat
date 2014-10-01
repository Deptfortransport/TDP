:: ------------------------------------------------------------------------
:: ------ BATCH FILE TO RUN ALL DATA IMPORT FEEDS FOR TDP -----------------
::
:: The script will reset the FTP Configuration table ready to run
:: the feeds. Any existing feeds in the Gateway Holding folder will
:: be copied to the Incoming folder, to allow re-running of feeds.
:: The readonly file attribute will be removed from all files in the 
:: incoming folder (in case the files were downloaded from the 
:: source control).
:: The importer will only import the newest data feed, and copy all
:: feeds to the holding folder.
::
:: This version accepts a feedname to import as a parameter
::
:: NOTE: This script assumes all feeds import successfully. The caller
:: will need to tidy up the Gateway folders if any feeds fail mid-import.
:: -------------------------------------------------------------------------

:: Specific feed
SET feedname=%1

if "%feedname%"=="" goto exit


:: Call stored procedure to reset last data import date
set server=".\SQLExpress"
set database="PermanentPortal"
::set user=""
::set password=""
::sqlcmd -U %user% -P %password% -S %server% -d %database% -Q "EXEC [dbo].[ResetDataFeedImport]"
sqlcmd -S %server% -d %database% -Q "EXEC [dbo].[ResetDataFeedImport]"

:: Go to Gateway folder
D:
cd \
cd Gateway

:: Go to Data folder
cd Dat

:: Copy all existing data feeds from holding to incoming folder, to allow re-running of last feed
:: Overwrite readonly files /r, and suppress any confirms /y
xcopy Holding\%feedname% Incoming\%feedname% /y /r

:: Delete files in holding folder
del Holding\%feedname%\*.* /f /s /q

:: Remove readonly attributes from files 
:: This is for any new files downloaded and not copied in above command
attrib -r Incoming\%feedname%\*

:: Go to Bin folder to start the imports
cd ..
cd Bin
td.dataimport.exe %feedname% /notransfer

:: Finished, assume all data feeds imported successfully
:exit
pause
