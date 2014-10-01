echo taking offline...

ren \\%1\d$\inetpub\wwwroot\Pool1_OK.gif Pool1_OK.gif.off

ren \\%1\d$\inetpub\wwwroot\Pool2_OK.gif Pool2_OK.gif.off

dir \\%1\d$\inetpub\wwwroot\pool?_ok.gif

CHOICE /C YN /M "Please confirm server offline to continue deployment"

IF ERRORLEVEL 1 Goto Deploy
IF ERRORLEVEL 2 Goto Abort

:Deploy

echo deploying...

echo stopping windows services...

SC \\%1 stop CommandAndControlAgentService
SC \\%1 stop EventReceiver
SC \\%1 stop "TTBO Host"
SC \\%1 stop "Road Host"
SC \\%1 stop "Cycle Planner Road Host"

echo Stopping W3SVC...
SC \\%1 stop w3SVC

echo -----------------------------------------------------------------------
echo SJP, CJP AND W3SVC WINDOWS SERVICES STOPPED...
CHOICE /C YN /M "COPY FILES TO TARGET SERVER THEN HIT Y TO CONTINUE & RESTART SERVICES OR N TO ABORT..."
echo -----------------------------------------------------------------------
IF ERRORLEVEL 1 Goto Continue
IF ERRORLEVEL 2 Goto Abort

:Continue

echo Restarting W3SVC...

SC \\%1 start w3SVC
PING 127.0.0.1 -n 10 -w 1000 >NUL
SC \\%1 query w3SVC

echo starting windows services again...
SC \\%1 start CommandAndControlAgentService
SC \\%1 start EventReceiver
SC \\%1 start "TTBO Host"
SC \\%1 start "Road Host"
SC \\%1 start "Cycle Planner Road Host"

pause


:Abort
echo aborting deployment - please confirm online status...
pause


