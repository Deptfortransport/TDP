@echo off

if .==%1. goto end

echo Copying TrainTaxi.xml
echo %1
copy /Y D:\Gateway\dat\Processing\yvs938\%1 D:\Gateway\bin\xml\TrainTaxi.xml
echo ErrorLevel %ErrorLevel%

:end
