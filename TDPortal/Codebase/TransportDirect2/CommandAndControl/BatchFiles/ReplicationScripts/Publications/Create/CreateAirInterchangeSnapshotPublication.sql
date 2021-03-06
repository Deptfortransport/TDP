-- Enabling the replication database
use master
exec sp_replicationdboption @dbname = N'AirInterchange', @optname = N'publish', @value = N'true'
GO

exec [AirInterchange].sys.sp_addlogreader_agent @job_login = null, @job_password = null, @publisher_security_mode = 1
GO
exec [AirInterchange].sys.sp_addqreader_agent @job_login = null, @job_password = null, @frompublisher = 1
GO
-- Adding the snapshot publication
use [AirInterchange]
exec sp_addpublication @publication = N'AirInterchangeSnapshotPublication', @description = N'Snapshot publication of database ''AirInterchange'' from Publisher ''MIS''.', @sync_method = N'native', @retention = 0, @allow_push = N'true', @allow_pull = N'true', @allow_anonymous = N'true', @enabled_for_internet = N'false', @snapshot_in_defaultfolder = N'true', @compress_snapshot = N'false', @ftp_port = 21, @ftp_login = N'anonymous', @allow_subscription_copy = N'false', @add_to_active_directory = N'false', @repl_freq = N'snapshot', @status = N'active', @independent_agent = N'true', @immediate_sync = N'true', @allow_sync_tran = N'false', @autogen_sync_procs = N'false', @allow_queued_tran = N'false', @allow_dts = N'false', @replicate_ddl = 1
GO


exec sp_addpublication_snapshot @publication = N'AirInterchangeSnapshotPublication', @frequency_type = 1, @frequency_interval = 0, @frequency_relative_interval = 0, @frequency_recurrence_factor = 0, @frequency_subday = 0, @frequency_subday_interval = 0, @active_start_time_of_day = 0, @active_end_time_of_day = 235959, @active_start_date = 0, @active_end_date = 0, @job_login = N'sjp\installapp', @job_password = N'!password!2', @publisher_security_mode = 1
exec sp_grant_publication_access @publication = N'AirInterchangeSnapshotPublication', @login = N'sa'
GO
exec sp_grant_publication_access @publication = N'AirInterchangeSnapshotPublication', @login = N'NT AUTHORITY\SYSTEM'
GO
exec sp_grant_publication_access @publication = N'AirInterchangeSnapshotPublication', @login = N'BUILTIN\Administrators'
GO
exec sp_grant_publication_access @publication = N'AirInterchangeSnapshotPublication', @login = N'SJP\installapp'
GO
exec sp_grant_publication_access @publication = N'AirInterchangeSnapshotPublication', @login = N'NT SERVICE\SQLSERVERAGENT'
GO
exec sp_grant_publication_access @publication = N'AirInterchangeSnapshotPublication', @login = N'NT SERVICE\MSSQLSERVER'
GO
exec sp_grant_publication_access @publication = N'AirInterchangeSnapshotPublication', @login = N'distributor_admin'
GO

-- Adding the snapshot articles
use [AirInterchange]
exec sp_addarticle @publication = N'AirInterchangeSnapshotPublication', @article = N'Accessibility', @source_owner = N'dbo', @source_object = N'Accessibility', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'Accessibility', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [AirInterchange]
exec sp_addarticle @publication = N'AirInterchangeSnapshotPublication', @article = N'ImportDetails', @source_owner = N'dbo', @source_object = N'ImportDetails', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'manual', @destination_table = N'ImportDetails', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [AirInterchange]
exec sp_addarticle @publication = N'AirInterchangeSnapshotPublication', @article = N'Interchange', @source_owner = N'dbo', @source_object = N'Interchange', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'Interchange', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [AirInterchange]
exec sp_addarticle @publication = N'AirInterchangeSnapshotPublication', @article = N'LegNode', @source_owner = N'dbo', @source_object = N'LegNode', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'LegNode', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [AirInterchange]
exec sp_addarticle @publication = N'AirInterchangeSnapshotPublication', @article = N'Link', @source_owner = N'dbo', @source_object = N'Link', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'Link', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [AirInterchange]
exec sp_addarticle @publication = N'AirInterchangeSnapshotPublication', @article = N'Mapping', @source_owner = N'dbo', @source_object = N'Mapping', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'Mapping', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [AirInterchange]
exec sp_addarticle @publication = N'AirInterchangeSnapshotPublication', @article = N'NodeAction', @source_owner = N'dbo', @source_object = N'NodeAction', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'NodeAction', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [AirInterchange]
exec sp_addarticle @publication = N'AirInterchangeSnapshotPublication', @article = N'Schematics', @source_owner = N'dbo', @source_object = N'Schematics', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'Schematics', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [AirInterchange]
exec sp_addarticle @publication = N'AirInterchangeSnapshotPublication', @article = N'spGetInterchangeDetails', @source_owner = N'dbo', @source_object = N'spGetInterchangeDetails', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'spGetInterchangeDetails', @destination_owner = N'dbo', @status = 16
GO
use [AirInterchange]
exec sp_addarticle @publication = N'AirInterchangeSnapshotPublication', @article = N'spGetInterchangeTime', @source_owner = N'dbo', @source_object = N'spGetInterchangeTime', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'spGetInterchangeTime', @destination_owner = N'dbo', @status = 16
GO
use [AirInterchange]
exec sp_addarticle @publication = N'AirInterchangeSnapshotPublication', @article = N'spGetVersion', @source_owner = N'dbo', @source_object = N'spGetVersion', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'spGetVersion', @destination_owner = N'dbo', @status = 16
GO

-- Create a new snapshot job for the publication, using the defaults.
EXEC sp_addpublication_snapshot 
  @publication = N'AirInterchangeSnapshotPublication',
  @job_login = N'sjp\installapp',
  @job_password = N'!password!2';

-- Start the Snapshot Agent job.
EXEC sp_startpublication_snapshot @publication = N'AirInterchangeSnapshotPublication';
GO