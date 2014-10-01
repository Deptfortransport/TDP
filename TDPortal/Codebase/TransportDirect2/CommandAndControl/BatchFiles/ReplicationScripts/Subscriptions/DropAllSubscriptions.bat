
rem   Try dropping for all work units both NLE and Prod - will quickly
rem   skip the ones that are not actually present in the environment
 
rem  NLE Work Units:
call DropSubscriptions_WU SJP-NLE-WU-01

call DropSubscriptions_WU SJP-NLE-WU-02

rem  PROD Work Units:
call DropSubscriptions_WU SJP-PROD-WU-01

call DropSubscriptions_WU SJP-PROD-WU-02

call DropSubscriptions_WU SJP-PROD-WU-03

call DropSubscriptions_WU SJP-PROD-WU-04

call DropSubscriptions_WU SJP-PROD-WU-05

call DropSubscriptions_WU SJP-PROD-WU-06

rem  PROD MIS02:

call DropSubscriptions_WU SJP-PROD-MIS-02