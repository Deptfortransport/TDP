-- Enabling the replication database
use master
exec sp_replicationdboption @dbname = N'SJPConfiguration', @optname = N'publish', @value = N'true'
GO

exec [SJPConfiguration].sys.sp_addlogreader_agent @job_login = N'sjp\installapp', @job_password = N'!password!2', @publisher_security_mode = 1
GO
exec [SJPConfiguration].sys.sp_addqreader_agent @job_login = null, @job_password = null, @frompublisher = 1
GO
-- Adding the transactional publication
use [SJPConfiguration]
exec sp_addpublication @publication = N'SJPConfigurationTransactionalPublication', @description = N'Transactional publication of database ''SJPConfiguration'' from Publisher ''MIS''.', @sync_method = N'concurrent', @retention = 0, @allow_push = N'true', @allow_pull = N'true', @allow_anonymous = N'true', @enabled_for_internet = N'false', @snapshot_in_defaultfolder = N'true', @compress_snapshot = N'false', @ftp_port = 21, @ftp_login = N'anonymous', @allow_subscription_copy = N'false', @add_to_active_directory = N'false', @repl_freq = N'continuous', @status = N'active', @independent_agent = N'true', @immediate_sync = N'true', @allow_sync_tran = N'false', @autogen_sync_procs = N'false', @allow_queued_tran = N'false', @allow_dts = N'false', @replicate_ddl = 1, @allow_initialize_from_backup = N'false', @enabled_for_p2p = N'false', @enabled_for_het_sub = N'false'
GO


exec sp_addpublication_snapshot @publication = N'SJPConfigurationTransactionalPublication', @frequency_type = 1, @frequency_interval = 0, @frequency_relative_interval = 0, @frequency_recurrence_factor = 0, @frequency_subday = 0, @frequency_subday_interval = 0, @active_start_time_of_day = 0, @active_end_time_of_day = 235959, @active_start_date = 0, @active_end_date = 0, @job_login = N'sjp\installapp', @job_password = N'!password!2', @publisher_security_mode = 1
exec sp_grant_publication_access @publication = N'SJPConfigurationTransactionalPublication', @login = N'sa'
GO
exec sp_grant_publication_access @publication = N'SJPConfigurationTransactionalPublication', @login = N'NT AUTHORITY\SYSTEM'
GO
exec sp_grant_publication_access @publication = N'SJPConfigurationTransactionalPublication', @login = N'BUILTIN\Administrators'
GO
exec sp_grant_publication_access @publication = N'SJPConfigurationTransactionalPublication', @login = N'SJP\installapp'
GO
exec sp_grant_publication_access @publication = N'SJPConfigurationTransactionalPublication', @login = N'NT SERVICE\SQLSERVERAGENT'
GO
exec sp_grant_publication_access @publication = N'SJPConfigurationTransactionalPublication', @login = N'NT SERVICE\MSSQLSERVER'
GO
exec sp_grant_publication_access @publication = N'SJPConfigurationTransactionalPublication', @login = N'distributor_admin'
GO

-- Adding the transactional articles
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'__RefactorLog', @source_owner = N'dbo', @source_object = N'__RefactorLog', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'__RefactorLog', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dbo__RefactorLog]', @del_cmd = N'CALL [dbo].[sp_MSdel_dbo__RefactorLog]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dbo__RefactorLog]'
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'AddChangeNotificationTable', @source_owner = N'dbo', @source_object = N'AddChangeNotificationTable', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddChangeNotificationTable', @destination_owner = N'dbo', @status = 16
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'AddUpdateProperty', @source_owner = N'dbo', @source_object = N'AddUpdateProperty', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddUpdateProperty', @destination_owner = N'dbo', @status = 16
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'AddUpdateRetailer', @source_owner = N'dbo', @source_object = N'AddUpdateRetailer', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddUpdateRetailer', @destination_owner = N'dbo', @status = 16
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'AddUpdateRetailerLookup', @source_owner = N'dbo', @source_object = N'AddUpdateRetailerLookup', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddUpdateRetailerLookup', @destination_owner = N'dbo', @status = 16
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'ChangeNotification', @source_owner = N'dbo', @source_object = N'ChangeNotification', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'ChangeNotification', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboChangeNotification]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboChangeNotification]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboChangeNotification]'
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'GetChangeTable', @source_owner = N'dbo', @source_object = N'GetChangeTable', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetChangeTable', @destination_owner = N'dbo', @status = 16
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'GetRetailerLookup', @source_owner = N'dbo', @source_object = N'GetRetailerLookup', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetRetailerLookup', @destination_owner = N'dbo', @status = 16
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'GetRetailers', @source_owner = N'dbo', @source_object = N'GetRetailers', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetRetailers', @destination_owner = N'dbo', @status = 16
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'GetVersion', @source_owner = N'dbo', @source_object = N'GetVersion', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetVersion', @destination_owner = N'dbo', @status = 16
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'properties', @source_owner = N'dbo', @source_object = N'properties', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'properties', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboproperties]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboproperties]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboproperties]'
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'ReferenceNum', @source_owner = N'dbo', @source_object = N'ReferenceNum', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'ReferenceNum', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboReferenceNum]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboReferenceNum]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboReferenceNum]'
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'RetailerLookup', @source_owner = N'dbo', @source_object = N'RetailerLookup', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'RetailerLookup', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboRetailerLookup]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboRetailerLookup]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboRetailerLookup]'
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'Retailers', @source_owner = N'dbo', @source_object = N'Retailers', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'Retailers', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboRetailers]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboRetailers]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboRetailers]'
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'SelectApplicationProperties', @source_owner = N'dbo', @source_object = N'SelectApplicationProperties', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'SelectApplicationProperties', @destination_owner = N'dbo', @status = 16
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'SelectGlobalProperties', @source_owner = N'dbo', @source_object = N'SelectGlobalProperties', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'SelectGlobalProperties', @destination_owner = N'dbo', @status = 16
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'SelectGroupProperties', @source_owner = N'dbo', @source_object = N'SelectGroupProperties', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'SelectGroupProperties', @destination_owner = N'dbo', @status = 16
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'UpdateChangeNotificationTable', @source_owner = N'dbo', @source_object = N'UpdateChangeNotificationTable', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'UpdateChangeNotificationTable', @destination_owner = N'dbo', @status = 16
GO
use [SJPConfiguration]
exec sp_addarticle @publication = N'SJPConfigurationTransactionalPublication', @article = N'VersionInfo', @source_owner = N'dbo', @source_object = N'VersionInfo', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'VersionInfo', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboVersionInfo]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboVersionInfo]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboVersionInfo]'
GO

-- Create a new snapshot job for the publication, using the defaults.
EXEC sp_addpublication_snapshot 
  @publication = N'SJPConfigurationTransactionalPublication',
  @job_login = N'sjp\installapp',
  @job_password = N'!password!2';

-- Start the Snapshot Agent job.
EXEC sp_startpublication_snapshot @publication = N'SJPConfigurationTransactionalPublication';
GO