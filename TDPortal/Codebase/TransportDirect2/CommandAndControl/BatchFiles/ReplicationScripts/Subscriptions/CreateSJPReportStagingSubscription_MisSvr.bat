

if "%1" == "" goto help

echo creating SJPReportStaging subscriptions
SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateSJPReportStagingSubscription_Server.sql" -v WorkUnit = %1 Password = "!password!2" -o ".\Logs\CreateSJPReportStagingSubscription%1.log"


goto end

:help
echo arguments are:
echo CreateSubscriptions.bat <workunit> 
pause

:end