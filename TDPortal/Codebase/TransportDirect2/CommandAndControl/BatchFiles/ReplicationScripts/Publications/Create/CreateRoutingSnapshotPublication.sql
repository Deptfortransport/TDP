-- Enabling the replication database
use master
exec sp_replicationdboption @dbname = N'Routing', @optname = N'publish', @value = N'true'
GO

exec [Routing].sys.sp_addlogreader_agent @job_login = null, @job_password = null, @publisher_security_mode = 1
GO
exec [Routing].sys.sp_addqreader_agent @job_login = null, @job_password = null, @frompublisher = 1
GO
-- Adding the snapshot publication
use [Routing]
exec sp_addpublication @publication = N'RoutingSnapshotPublication', @description = N'Snapshot publication of database ''Routing'' from Publisher ''MIS''.', @sync_method = N'native', @retention = 0, @allow_push = N'true', @allow_pull = N'true', @allow_anonymous = N'true', @enabled_for_internet = N'false', @snapshot_in_defaultfolder = N'true', @compress_snapshot = N'false', @ftp_port = 21, @ftp_login = N'anonymous', @allow_subscription_copy = N'false', @add_to_active_directory = N'false', @repl_freq = N'snapshot', @status = N'active', @independent_agent = N'true', @immediate_sync = N'true', @allow_sync_tran = N'false', @autogen_sync_procs = N'false', @allow_queued_tran = N'false', @allow_dts = N'false', @replicate_ddl = 1
GO


exec sp_addpublication_snapshot @publication = N'RoutingSnapshotPublication', @frequency_type = 1, @frequency_interval = 0, @frequency_relative_interval = 0, @frequency_recurrence_factor = 0, @frequency_subday = 0, @frequency_subday_interval = 0, @active_start_time_of_day = 0, @active_end_time_of_day = 235959, @active_start_date = 0, @active_end_date = 0, @job_login = N'sjp\installapp', @job_password = N'!password!2', @publisher_security_mode = 1
exec sp_grant_publication_access @publication = N'RoutingSnapshotPublication', @login = N'sa'
GO
exec sp_grant_publication_access @publication = N'RoutingSnapshotPublication', @login = N'NT AUTHORITY\SYSTEM'
GO
exec sp_grant_publication_access @publication = N'RoutingSnapshotPublication', @login = N'BUILTIN\Administrators'
GO
exec sp_grant_publication_access @publication = N'RoutingSnapshotPublication', @login = N'SJP\installapp'
GO
exec sp_grant_publication_access @publication = N'RoutingSnapshotPublication', @login = N'NT SERVICE\SQLSERVERAGENT'
GO
exec sp_grant_publication_access @publication = N'RoutingSnapshotPublication', @login = N'NT SERVICE\MSSQLSERVER'
GO
exec sp_grant_publication_access @publication = N'RoutingSnapshotPublication', @login = N'distributor_admin'
GO

-- Adding the snapshot articles
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'sp_CCZONE_links', @source_owner = N'dbo', @source_object = N'sp_CCZONE_links', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'sp_CCZONE_links', @destination_owner = N'dbo', @status = 16
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'spClearGazetteer', @source_owner = N'dbo', @source_object = N'spClearGazetteer', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'spClearGazetteer', @destination_owner = N'dbo', @status = 16
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'spClearLimitedAccess', @source_owner = N'dbo', @source_object = N'spClearLimitedAccess', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'spClearLimitedAccess', @destination_owner = N'dbo', @status = 16
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'spClearToidIndex', @source_owner = N'dbo', @source_object = N'spClearToidIndex', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'spClearToidIndex', @destination_owner = N'dbo', @status = 16
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'spInsertLimitedAccess', @source_owner = N'dbo', @source_object = N'spInsertLimitedAccess', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'spInsertLimitedAccess', @destination_owner = N'dbo', @status = 16
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'spInsertToidType', @source_owner = N'dbo', @source_object = N'spInsertToidType', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'spInsertToidType', @destination_owner = N'dbo', @status = 16
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblCCZoneOsgbToid', @source_owner = N'dbo', @source_object = N'tblCCZoneOsgbToid', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblCCZoneOsgbToid', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblCongestionZones', @source_owner = N'dbo', @source_object = N'tblCongestionZones', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblCongestionZones', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryCompanies', @source_owner = N'dbo', @source_object = N'tblFerryCompanies', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblFerryCompanies', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryDepartures', @source_owner = N'dbo', @source_object = N'tblFerryDepartures', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblFerryDepartures', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryIntermediateLinks', @source_owner = N'dbo', @source_object = N'tblFerryIntermediateLinks', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblFerryIntermediateLinks', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryIntermediateStops', @source_owner = N'dbo', @source_object = N'tblFerryIntermediateStops', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblFerryIntermediateStops', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryIntermediateStopsGeneral', @source_owner = N'dbo', @source_object = N'tblFerryIntermediateStopsGeneral', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblFerryIntermediateStopsGeneral', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryIntermediateStopsSpecific', @source_owner = N'dbo', @source_object = N'tblFerryIntermediateStopsSpecific', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblFerryIntermediateStopsSpecific', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryNotes', @source_owner = N'dbo', @source_object = N'tblFerryNotes', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblFerryNotes', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryPorts', @source_owner = N'dbo', @source_object = N'tblFerryPorts', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblFerryPorts', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblFerryRoutes', @source_owner = N'dbo', @source_object = N'tblFerryRoutes', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblFerryRoutes', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblGazetteer', @source_owner = N'dbo', @source_object = N'tblGazetteer', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblGazetteer', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblLimitedAccess', @source_owner = N'dbo', @source_object = N'tblLimitedAccess', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'manual', @destination_table = N'tblLimitedAccess', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblToidIndex', @source_owner = N'dbo', @source_object = N'tblToidIndex', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblToidIndex', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblToidTypes', @source_owner = N'dbo', @source_object = N'tblToidTypes', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblToidTypes', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblTollOperatorsData', @source_owner = N'dbo', @source_object = N'tblTollOperatorsData', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblTollOperatorsData', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [Routing]
exec sp_addarticle @publication = N'RoutingSnapshotPublication', @article = N'tblVersionInfo', @source_owner = N'dbo', @source_object = N'tblVersionInfo', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'tblVersionInfo', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO


-- Create a new snapshot job for the publication, using the defaults.
EXEC sp_addpublication_snapshot 
  @publication = N'RoutingSnapshotPublication',
  @job_login = N'sjp\installapp',
  @job_password = N'!password!2';

-- Start the Snapshot Agent job.
EXEC sp_startpublication_snapshot @publication = N'RoutingSnapshotPublication';
GO