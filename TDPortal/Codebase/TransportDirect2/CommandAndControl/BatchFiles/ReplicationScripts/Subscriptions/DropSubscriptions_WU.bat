

if "%1" == "" goto help

echo dropping subscriptions
SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Drop\DropAllSubscriptions.sql" -v WorkUnit = %1 -o ".\Logs\DropSubscriptions%1.log"
goto end

:help
echo arguments are:
echo DropSubscriptions.bat <workunit> 
pause

:end