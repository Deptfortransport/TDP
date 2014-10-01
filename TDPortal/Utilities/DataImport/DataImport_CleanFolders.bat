:: ------------------------------------------------------------------------
:: ------ BATCH FILE TO CLEAN DATA IMPORT FOLDERS FOR TDP -----------------
::
:: Any existing feeds in the Gateway Holding folder will
:: be copied to the Incoming folder.
::
:: Files in the Gateway Holding folder will be DELETED
::
:: -------------------------------------------------------------------------

:: Go to Gateway folder
D:
cd \
cd Gateway

:: Go to Data folder
cd Dat

:: Copy all existing data feeds from holding to incoming folder
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
