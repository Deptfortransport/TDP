

for /f "tokens=1-3 delims=/ " %%a in ('D:\TDPortal\BatchFiles\doff mm/dd/yyyy -1') do (
set mm=%%a
set dd=%%b
set yyyy=%%c)


:2700
osql -S "D03" -E ReportStagingDB -q "EXIT(ReportStagingDB.dbo.UpdateReportProperties)"
goto exit2

:exit
