@ECHO OFF
REM *********************************************** 
REM NAME                 : Generate TDTransaction Proxy.bat 
REM AUTHOR               : Gary Eaton
REM DATE CREATED         : 11/11/2003 
REM DESCRIPTION  :  Batch file used to generate the TDTRansaction.cpp proxy.
REM This must be run to update TDTransaction.cpp whenever the TD Web Service interface changes.
REM ************************************************ 

cd C:\Program Files\Microsoft Visual Studio .NET\Common7\Tools
call vsvars32.bat
cd C:\TDPortal\DEL5\TransportDirect\ReportDataProvider\TransactionInjector
call wsdl http://localhost/TDPWebServices/TransactionWebService/TDTransactionService.asmx?wsdl

pause