rem batch file for backing up SJP servers - backs all work unit & gateway folders so can be used for either type of server...

mkdir .\AppFileBackups\%1\ 
echo batch file for backing up SJP servers - backs all work unit & gateway folders so can be used for either type of server > .\AppFileBackups\%1\backup.log

"D:\Program Files\7-Zip\7z" a -tzip .\AppFileBackups\%1\inetpub.zip \\%1\d$\inetpub\ >> .\AppFileBackups\%1\backup.log

"D:\Program Files\7-Zip\7z" a -tzip .\AppFileBackups\%1\gateway.zip \\%1\d$\gateway\ >> .\AppFileBackups\%1\backup.log

"D:\Program Files\7-Zip\7z" a -tzip .\AppFileBackups\%1\SJP.zip \\%1\d$\SJP\Components\ >> .\AppFileBackups\%1\backup.log

"D:\Program Files\7-Zip\7z" a -tzip .\AppFileBackups\%1\SJP.zip \\%1\d$\SJP\BatchFiles\ >> .\AppFileBackups\%1\backup.log

"D:\Program Files\7-Zip\7z" a -tzip .\AppFileBackups\%1\TDPortal.zip \\%1\d$\TDPortal\Components\ >> .\AppFileBackups\%1\backup.log

"D:\Program Files\7-Zip\7z" a -tzip .\AppFileBackups\%1\TDPortal.zip \\%1\d$\TDPortal\BatchFiles\ >> .\AppFileBackups\%1\backup.log

"D:\Program Files\7-Zip\7z" a -tzip .\AppFileBackups\%1\TransportDirect.zip \\%1\d$\TransportDirect\CJPTest\ >> .\AppFileBackups\%1\backup.log
"D:\Program Files\7-Zip\7z" a -tzip .\AppFileBackups\%1\TransportDirect.zip \\%1\d$\TransportDirect\Config\ >> .\AppFileBackups\%1\backup.log
"D:\Program Files\7-Zip\7z" a -tzip .\AppFileBackups\%1\TransportDirect.zip \\%1\d$\TransportDirect\Services\ >> .\AppFileBackups\%1\backup.log
"D:\Program Files\7-Zip\7z" a -tzip .\AppFileBackups\%1\TransportDirect.zip \\%1\d$\TransportDirect\Stylesheets\ >> .\AppFileBackups\%1\backup.log
"D:\Program Files\7-Zip\7z" a -tzip .\AppFileBackups\%1\TransportDirect.zip \\%1\d$\TransportDirect\Web\ >> .\AppFileBackups\%1\backup.log


