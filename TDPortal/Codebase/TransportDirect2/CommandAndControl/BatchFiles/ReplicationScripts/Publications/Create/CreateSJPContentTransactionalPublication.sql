-- Enabling the replication database
use master
exec sp_replicationdboption @dbname = N'SJPContent', @optname = N'publish', @value = N'true'
GO

exec [SJPContent].sys.sp_addlogreader_agent @job_login = N'sjp\installapp', @job_password = N'!password!2', @publisher_security_mode = 1
GO
exec [SJPContent].sys.sp_addqreader_agent @job_login = null, @job_password = null, @frompublisher = 1
GO
-- Adding the transactional publication
use [SJPContent]
exec sp_addpublication @publication = N'SJPContentTransactionalPublication', @description = N'Transactional publication of database ''SJPContent'' from Publisher ''MIS''.', @sync_method = N'concurrent', @retention = 0, @allow_push = N'true', @allow_pull = N'true', @allow_anonymous = N'true', @enabled_for_internet = N'false', @snapshot_in_defaultfolder = N'true', @compress_snapshot = N'false', @ftp_port = 21, @ftp_login = N'anonymous', @allow_subscription_copy = N'false', @add_to_active_directory = N'false', @repl_freq = N'continuous', @status = N'active', @independent_agent = N'true', @immediate_sync = N'true', @allow_sync_tran = N'false', @autogen_sync_procs = N'false', @allow_queued_tran = N'false', @allow_dts = N'false', @replicate_ddl = 1, @allow_initialize_from_backup = N'false', @enabled_for_p2p = N'false', @enabled_for_het_sub = N'false'
GO


exec sp_addpublication_snapshot @publication = N'SJPContentTransactionalPublication', @frequency_type = 1, @frequency_interval = 0, @frequency_relative_interval = 0, @frequency_recurrence_factor = 0, @frequency_subday = 0, @frequency_subday_interval = 0, @active_start_time_of_day = 0, @active_end_time_of_day = 235959, @active_start_date = 0, @active_end_date = 0, @job_login = N'sjp\installapp', @job_password = N'!password!2', @publisher_security_mode = 1
exec sp_grant_publication_access @publication = N'SJPContentTransactionalPublication', @login = N'sa'
GO
exec sp_grant_publication_access @publication = N'SJPContentTransactionalPublication', @login = N'NT AUTHORITY\SYSTEM'
GO
exec sp_grant_publication_access @publication = N'SJPContentTransactionalPublication', @login = N'BUILTIN\Administrators'
GO
exec sp_grant_publication_access @publication = N'SJPContentTransactionalPublication', @login = N'SJP\installapp'
GO
exec sp_grant_publication_access @publication = N'SJPContentTransactionalPublication', @login = N'NT SERVICE\SQLSERVERAGENT'
GO
exec sp_grant_publication_access @publication = N'SJPContentTransactionalPublication', @login = N'NT SERVICE\MSSQLSERVER'
GO
exec sp_grant_publication_access @publication = N'SJPContentTransactionalPublication', @login = N'distributor_admin'
GO

-- Adding the transactional articles
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'__RefactorLog', @source_owner = N'dbo', @source_object = N'__RefactorLog', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'__RefactorLog', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dbo__RefactorLog]', @del_cmd = N'CALL [dbo].[sp_MSdel_dbo__RefactorLog]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dbo__RefactorLog]'
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'AddChangeNotificationTable', @source_owner = N'dbo', @source_object = N'AddChangeNotificationTable', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddChangeNotificationTable', @destination_owner = N'dbo', @status = 16
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'AddContent', @source_owner = N'dbo', @source_object = N'AddContent', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddContent', @destination_owner = N'dbo', @status = 16
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'AddContentOverride', @source_owner = N'dbo', @source_object = N'AddContentOverride', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddContentOverride', @destination_owner = N'dbo', @status = 16
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'AddGroup', @source_owner = N'dbo', @source_object = N'AddGroup', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddGroup', @destination_owner = N'dbo', @status = 16
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'ChangeNotification', @source_owner = N'dbo', @source_object = N'ChangeNotification', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'ChangeNotification', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboChangeNotification]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboChangeNotification]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboChangeNotification]'
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'Content', @source_owner = N'dbo', @source_object = N'Content', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'Content', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboContent]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboContent]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboContent]'
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'ContentGroup', @source_owner = N'dbo', @source_object = N'ContentGroup', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'ContentGroup', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboContentGroup]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboContentGroup]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboContentGroup]'
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'ContentOverride', @source_owner = N'dbo', @source_object = N'ContentOverride', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'ContentOverride', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboContentOverride]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboContentOverride]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboContentOverride]'
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'DeleteAllGroupContent', @source_owner = N'dbo', @source_object = N'DeleteAllGroupContent', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'DeleteAllGroupContent', @destination_owner = N'dbo', @status = 16
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'DeleteContent', @source_owner = N'dbo', @source_object = N'DeleteContent', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'DeleteContent', @destination_owner = N'dbo', @status = 16
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'DeleteContentOverride', @source_owner = N'dbo', @source_object = N'DeleteContentOverride', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'DeleteContentOverride', @destination_owner = N'dbo', @status = 16
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'DeleteGroup', @source_owner = N'dbo', @source_object = N'DeleteGroup', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'DeleteGroup', @destination_owner = N'dbo', @status = 16
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'GetChangeTable', @source_owner = N'dbo', @source_object = N'GetChangeTable', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetChangeTable', @destination_owner = N'dbo', @status = 16
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'GetContent', @source_owner = N'dbo', @source_object = N'GetContent', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetContent', @destination_owner = N'dbo', @status = 16
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'UpdateChangeNotificationTable', @source_owner = N'dbo', @source_object = N'UpdateChangeNotificationTable', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'UpdateChangeNotificationTable', @destination_owner = N'dbo', @status = 16
GO
use [SJPContent]
exec sp_addarticle @publication = N'SJPContentTransactionalPublication', @article = N'VersionInfo', @source_owner = N'dbo', @source_object = N'VersionInfo', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'VersionInfo', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboVersionInfo]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboVersionInfo]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboVersionInfo]'
GO

-- Create a new snapshot job for the publication, using the defaults.
EXEC sp_addpublication_snapshot 
  @publication = N'SJPContentTransactionalPublication',
  @job_login = N'sjp\installapp',
  @job_password = N'!password!2';

-- Start the Snapshot Agent job.
EXEC sp_startpublication_snapshot @publication = N'SJPContentTransactionalPublication';
GO