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
:: NOTE: This script assumes all feeds import successfully. The caller
:: will need to tidy up the Gateway folders if any feeds fail mid-import.
:: -------------------------------------------------------------------------


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
xcopy Holding\afc743 Incoming\afc743 /y /r
xcopy Holding\avf956 Incoming\avf956 /y /r
xcopy Holding\efw677 Incoming\efw677 /y /r
xcopy Holding\ert444 Incoming\ert444 /y /r
xcopy Holding\loe147 Incoming\loe147 /y /r
xcopy Holding\mkw489 Incoming\mkw489 /y /r
xcopy Holding\sul834 Incoming\sul834 /y /r
xcopy Holding\wsa980 Incoming\wsa980 /y /r

:: Delete files in holding folder
del Holding\afc743\*.* /f /s /q
del Holding\avf956\*.* /f /s /q
del Holding\efw677\*.* /f /s /q
del Holding\ert444\*.* /f /s /q
del Holding\loe147\*.* /f /s /q
del Holding\mkw489\*.* /f /s /q
del Holding\sul834\*.* /f /s /q
del Holding\wsa980\*.* /f /s /q

:: Remove readonly attributes from files 
:: This is for any new files downloaded and not copied in above command
attrib -r Incoming\afc743\*
attrib -r Incoming\avf956\*
attrib -r Incoming\efw677\*
attrib -r Incoming\ert444\*
attrib -r Incoming\loe147\*
attrib -r Incoming\mkw489\*
attrib -r Incoming\sul834\*
attrib -r Incoming\wsa980\*

:: Go to Bin folder to start the imports
cd ..
cd Bin

td.dataimport.exe afc743 /notransfer
set resultcodes=%resultcodes%,%ERRORLEVEL%

td.dataimport.exe avf956 /notransfer
set resultcodes=%resultcodes%,%ERRORLEVEL%

td.dataimport.exe efw677 /notransfer
set resultcodes=%resultcodes%,%ERRORLEVEL%

td.dataimport.exe ert444 /notransfer
set resultcodes=%resultcodes%,%ERRORLEVEL%

td.dataimport.exe loe147 /notransfer
set resultcodes=%resultcodes%,%ERRORLEVEL%

td.dataimport.exe mkw489 /notransfer
set resultcodes=%resultcodes%,%ERRORLEVEL%

td.dataimport.exe sul834 /notransfer
set resultcodes=%resultcodes%,%ERRORLEVEL%

td.dataimport.exe wsa980 /notransfer
set resultcodes=%resultcodes%,%ERRORLEVEL%

:: Finished, assume all data feeds imported successfully
pause
