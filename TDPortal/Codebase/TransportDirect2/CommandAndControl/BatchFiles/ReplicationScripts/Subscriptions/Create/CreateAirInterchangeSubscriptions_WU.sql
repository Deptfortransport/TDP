-----------------BEGIN: Script to be run at Publisher 'SJP-NLE-MIS-01'-----------------

use [AirInterchange]
exec sp_addsubscription @publication = N'AirInterchangeSnapshotPublication', 
@subscriber = N'$(WorkUnit)', @destination_db = N'AirInterchange', 
@subscription_type = N'Push', @sync_type = N'automatic', @article = N'all', 
@update_mode = N'read only', @subscriber_type = 0

exec sp_addpushsubscription_agent @publication = N'AirInterchangeSnapshotPublication', 
@subscriber = N'$(WorkUnit)', @subscriber_db = N'AirInterchange', 
@job_login = N'sjp\installapp', @job_password = N'$(Password)', 
@subscriber_security_mode = 0, @subscriber_login = N'Replication_User', @subscriber_password = N'!password!1', @frequency_type = 1, @frequency_interval = 0, 
@frequency_relative_interval = 0, @frequency_recurrence_factor = 0, 
@frequency_subday = 0, @frequency_subday_interval = 0, @active_start_time_of_day = 0, 
@active_end_time_of_day = 0, @active_start_date = 0, @active_end_date = 19950101, 
@enabled_for_syncmgr = N'False', @dts_package_location = N'Distributor'
GO

-----------------END: Script to be run at Publisher 'SJP-NLE-MIS-01'-----------------

