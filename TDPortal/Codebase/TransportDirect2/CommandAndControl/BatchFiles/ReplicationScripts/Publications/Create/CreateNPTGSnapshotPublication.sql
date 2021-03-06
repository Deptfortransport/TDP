-- Enabling the replication database
use master
exec sp_replicationdboption @dbname = N'NPTG', @optname = N'publish', @value = N'true'
GO

exec [NPTG].sys.sp_addlogreader_agent @job_login = null, @job_password = null, @publisher_security_mode = 1
GO
exec [NPTG].sys.sp_addqreader_agent @job_login = null, @job_password = null, @frompublisher = 1
GO
-- Adding the snapshot publication
use [NPTG]
exec sp_addpublication @publication = N'NPTGSnapshotPublication', @description = N'Snapshot publication of database ''NPTG'' from Publisher ''MIS''.', @sync_method = N'native', @retention = 0, @allow_push = N'true', @allow_pull = N'true', @allow_anonymous = N'true', @enabled_for_internet = N'false', @snapshot_in_defaultfolder = N'true', @compress_snapshot = N'false', @ftp_port = 21, @ftp_login = N'anonymous', @allow_subscription_copy = N'false', @add_to_active_directory = N'false', @repl_freq = N'snapshot', @status = N'active', @independent_agent = N'true', @immediate_sync = N'true', @allow_sync_tran = N'false', @autogen_sync_procs = N'false', @allow_queued_tran = N'false', @allow_dts = N'false', @replicate_ddl = 1
GO


exec sp_addpublication_snapshot @publication = N'NPTGSnapshotPublication', @frequency_type = 1, @frequency_interval = 0, @frequency_relative_interval = 0, @frequency_recurrence_factor = 0, @frequency_subday = 0, @frequency_subday_interval = 0, @active_start_time_of_day = 0, @active_end_time_of_day = 235959, @active_start_date = 0, @active_end_date = 0, @job_login = N'sjp\installapp', @job_password = N'!password!2', @publisher_security_mode = 1
exec sp_grant_publication_access @publication = N'NPTGSnapshotPublication', @login = N'sa'
GO
exec sp_grant_publication_access @publication = N'NPTGSnapshotPublication', @login = N'NT AUTHORITY\SYSTEM'
GO
exec sp_grant_publication_access @publication = N'NPTGSnapshotPublication', @login = N'BUILTIN\Administrators'
GO
exec sp_grant_publication_access @publication = N'NPTGSnapshotPublication', @login = N'SJP\installapp'
GO
exec sp_grant_publication_access @publication = N'NPTGSnapshotPublication', @login = N'NT SERVICE\SQLSERVERAGENT'
GO
exec sp_grant_publication_access @publication = N'NPTGSnapshotPublication', @login = N'NT SERVICE\MSSQLSERVER'
GO
exec sp_grant_publication_access @publication = N'NPTGSnapshotPublication', @login = N'distributor_admin'
GO

-- Adding the snapshot articles
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'Admin Areas', @source_owner = N'dbo', @source_object = N'Admin Areas', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'Admin Areas', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'Alternate Names', @source_owner = N'dbo', @source_object = N'Alternate Names', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'Alternate Names', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'AREPs', @source_owner = N'dbo', @source_object = N'AREPs', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'AREPs', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'Call Centres', @source_owner = N'dbo', @source_object = N'Call Centres', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'Call Centres', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'Districts', @source_owner = N'dbo', @source_object = N'Districts', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'Districts', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'Exchange Points', @source_owner = N'dbo', @source_object = N'Exchange Points', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'Exchange Points', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'GetAREPs', @source_owner = N'dbo', @source_object = N'GetAREPs', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetAREPs', @destination_owner = N'dbo', @status = 16
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'GetCapability', @source_owner = N'dbo', @source_object = N'GetCapability', @type = N'func schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetCapability', @destination_owner = N'dbo', @status = 16
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'GetExchangePoints', @source_owner = N'dbo', @source_object = N'GetExchangePoints', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetExchangePoints', @destination_owner = N'dbo', @status = 16
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'GetJWVersion', @source_owner = N'dbo', @source_object = N'GetJWVersion', @type = N'func schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetJWVersion', @destination_owner = N'dbo', @status = 16
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'GetSecondaryRegionURL', @source_owner = N'dbo', @source_object = N'GetSecondaryRegionURL', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetSecondaryRegionURL', @destination_owner = N'dbo', @status = 16
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'GetURL', @source_owner = N'dbo', @source_object = N'GetURL', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetURL', @destination_owner = N'dbo', @status = 16
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'Hierarchy', @source_owner = N'dbo', @source_object = N'Hierarchy', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'Hierarchy', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'Localities', @source_owner = N'dbo', @source_object = N'Localities', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'Localities', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'Traveline Regions', @source_owner = N'dbo', @source_object = N'Traveline Regions', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'Traveline Regions', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'Trusted Clients', @source_owner = N'dbo', @source_object = N'Trusted Clients', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'Trusted Clients', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO
use [NPTG]
exec sp_addarticle @publication = N'NPTGSnapshotPublication', @article = N'Unsupported', @source_owner = N'dbo', @source_object = N'Unsupported', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDD, @identityrangemanagementoption = N'none', @destination_table = N'Unsupported', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'SQL', @del_cmd = N'SQL', @upd_cmd = N'SQL'
GO

-- Create a new snapshot job for the publication, using the defaults.
EXEC sp_addpublication_snapshot 
  @publication = N'NPTGSnapshotPublication',
  @job_login = N'sjp\installapp',
  @job_password = N'!password!2';

-- Start the Snapshot Agent job.
EXEC sp_startpublication_snapshot @publication = N'NPTGSnapshotPublication';
GO