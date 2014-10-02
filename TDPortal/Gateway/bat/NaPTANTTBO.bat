@echo off

echo starting ttbo

cd /d "D:\Gateway\bin\Utils\Additional Data Import" 

".\AdditionalDataImport.exe" A NaPTAN N NLCDat
echo NaPTAN TTBO ErrorLevel %ErrorLevel%

cd /d "D:\Gateway\bat"

echo NaPTAN TTBO end 

:end
