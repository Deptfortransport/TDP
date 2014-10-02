@echo off

if .%1==. GoTo end

cd D:\Gateway\bin
call D:\Gateway\bat\_ipAddress.bat

echo Trying %1 on %TDPServerS%
D:\Gateway\bin\td.dataimport.exe %1 /ipaddress:%TDPServerS%
echo ErrorLevel %errorlevel%

if ErrorLevel==1 GoTo secondary
GoTo end

:secondary
echo Trying %1 on %TDPServerP%
D:\Gateway\bin\td.dataimport.exe %1 /ipaddress:%TDPServerP%
echo ErrorLevel %errorlevel%

:end
cd D:\Gateway\bat
