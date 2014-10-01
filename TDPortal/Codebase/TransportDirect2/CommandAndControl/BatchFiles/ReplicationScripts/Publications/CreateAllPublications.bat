
echo creating snapshot publications----------------------------

SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateAirInterchangeSnapshotPublication.sql" -o ".\Logs\CreateAirInterchangePublication.log"

SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateCP_RoutingSnapshotPublication.sql" -o ".\Logs\CreateCP_RoutingPublication.log"

SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateNPTGSnapshotPublication.sql" -o ".\Logs\CreateNPTGPublication.log"

SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateRoutingSnapshotPublication.sql" -o ".\Logs\CreateRoutingPublication.log"


echo creating transactional publications----------------------------

SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateSJPConfigurationTransactionalPublication.sql" -o ".\Logs\CreateSJPConfigurationPublication.log"

SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateSJPContentTransactionalPublication.sql" -o ".\Logs\CreateSJPContentPublication.log"

SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateSJPGazetteerTransactionalPublication.sql" -o ".\Logs\CreateSJPGazetteerPublication.log"

SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Create\CreateSJPTransientPortalTransactionalPublication.sql" -o ".\Logs\CreateSJPTransientPortalPublication.log"

pause



