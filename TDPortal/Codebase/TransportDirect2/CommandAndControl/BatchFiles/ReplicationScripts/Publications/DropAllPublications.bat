
echo dropping all publications----------------------------

SQLCMD -U "sa" -P "!password!1" -S MIS -i ".\Drop\DropAllPublications.sql" -o ".\Logs\DropPublicationsOutput.log"

pause




