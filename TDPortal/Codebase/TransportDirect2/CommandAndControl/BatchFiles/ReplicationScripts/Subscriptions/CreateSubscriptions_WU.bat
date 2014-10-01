

if "%1" == "" goto help

echo creating AirInterchange subscriptions
SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateAirInterchangeSubscriptions_WU.sql" -v WorkUnit = %1 Password = "!password!2" -o ".\Logs\CreateAirInterchangeSubscription%1.log"

echo creating CP_Routing subscriptions
SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateCP_RoutingSubscriptions_WU.sql" -v WorkUnit = %1 Password = "!password!2" -o ".\Logs\CreateCPRoutingSubscription%1.log"

echo creating NPTG subscriptions
SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateNPTGSubscriptions_WU.sql" -v WorkUnit = %1 Password = "!password!2" -o ".\Logs\CreateNPTGSubscription%1.log"

echo creating Routing subscriptions
SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateRoutingSubscriptions_WU.sql" -v WorkUnit = %1 Password = "!password!2" -o ".\Logs\CreateRoutingSubscription%1.log"

echo creating SJPConfiguration subscriptions
SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateSJPConfigurationSubscriptions_WU.sql" -v WorkUnit = %1 Password = "!password!2" -o ".\Logs\CreateSJPConfigurationSubscription%1.log"

echo creating SJPContent subscriptions
SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateSJPContentSubscriptions_WU.sql" -v WorkUnit = %1 Password = "!password!2" -o ".\Logs\CreateSJPContentSubscription%1.log"

echo creating SJPGazetteer subscriptions
SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateSJPGazetteerSubscriptions_WU.sql" -v WorkUnit = %1 Password = "!password!2" -o ".\Logs\CreateSJPGazetteerSubscription%1.log"

echo creating SJPTransientPortal subscriptions
SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateSJPTransientPortalSubscriptions_WU.sql" -v WorkUnit = %1 Password = "!password!2" -o ".\Logs\CreateSJPTransientPortalSubscription%1.log"


goto end

:help
echo arguments are:
echo CreateSubscriptions.bat <workunit> 
pause

:end