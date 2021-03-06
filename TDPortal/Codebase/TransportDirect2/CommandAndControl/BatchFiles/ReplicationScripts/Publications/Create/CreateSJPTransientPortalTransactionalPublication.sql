-- Enabling the replication database
use master
exec sp_replicationdboption @dbname = N'SJPTransientPortal', @optname = N'publish', @value = N'true'
GO

exec [SJPTransientPortal].sys.sp_addlogreader_agent @job_login = N'sjp\installapp', @job_password = N'!password!2', @publisher_security_mode = 1
GO
exec [SJPTransientPortal].sys.sp_addqreader_agent @job_login = null, @job_password = null, @frompublisher = 1
GO
-- Adding the transactional publication
use [SJPTransientPortal]
exec sp_addpublication @publication = N'SJPTransientPortalTransactionalPublication', @description = N'Transactional publication of database ''SJPTransientPortal'' from Publisher ''MIS''.', @sync_method = N'concurrent', @retention = 0, @allow_push = N'true', @allow_pull = N'true', @allow_anonymous = N'true', @enabled_for_internet = N'false', @snapshot_in_defaultfolder = N'true', @compress_snapshot = N'false', @ftp_port = 21, @ftp_login = N'anonymous', @allow_subscription_copy = N'false', @add_to_active_directory = N'false', @repl_freq = N'continuous', @status = N'active', @independent_agent = N'true', @immediate_sync = N'true', @allow_sync_tran = N'false', @autogen_sync_procs = N'false', @allow_queued_tran = N'false', @allow_dts = N'false', @replicate_ddl = 1, @allow_initialize_from_backup = N'false', @enabled_for_p2p = N'false', @enabled_for_het_sub = N'false'
GO


exec sp_addpublication_snapshot @publication = N'SJPTransientPortalTransactionalPublication', @frequency_type = 1, @frequency_interval = 0, @frequency_relative_interval = 0, @frequency_recurrence_factor = 0, @frequency_subday = 0, @frequency_subday_interval = 0, @active_start_time_of_day = 0, @active_end_time_of_day = 235959, @active_start_date = 0, @active_end_date = 0, @job_login = N'sjp\installapp', @job_password = N'!password!2', @publisher_security_mode = 1
exec sp_grant_publication_access @publication = N'SJPTransientPortalTransactionalPublication', @login = N'sa'
GO
exec sp_grant_publication_access @publication = N'SJPTransientPortalTransactionalPublication', @login = N'NT AUTHORITY\SYSTEM'
GO
exec sp_grant_publication_access @publication = N'SJPTransientPortalTransactionalPublication', @login = N'BUILTIN\Administrators'
GO
exec sp_grant_publication_access @publication = N'SJPTransientPortalTransactionalPublication', @login = N'SJP\installapp'
GO
exec sp_grant_publication_access @publication = N'SJPTransientPortalTransactionalPublication', @login = N'NT SERVICE\SQLSERVERAGENT'
GO
exec sp_grant_publication_access @publication = N'SJPTransientPortalTransactionalPublication', @login = N'NT SERVICE\MSSQLSERVER'
GO
exec sp_grant_publication_access @publication = N'SJPTransientPortalTransactionalPublication', @login = N'distributor_admin'
GO

-- Adding the transactional articles
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'__RefactorLog', @source_owner = N'dbo', @source_object = N'__RefactorLog', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'__RefactorLog', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dbo__RefactorLog]', @del_cmd = N'CALL [dbo].[sp_MSdel_dbo__RefactorLog]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dbo__RefactorLog]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'AddChangeNotificationTable', @source_owner = N'dbo', @source_object = N'AddChangeNotificationTable', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddChangeNotificationTable', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'AdminAreas', @source_owner = N'dbo', @source_object = N'AdminAreas', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'AdminAreas', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboAdminAreas]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboAdminAreas]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboAdminAreas]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'ChangeNotification', @source_owner = N'dbo', @source_object = N'ChangeNotification', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'ChangeNotification', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboChangeNotification]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboChangeNotification]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboChangeNotification]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'CycleAttribute', @source_owner = N'dbo', @source_object = N'CycleAttribute', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'CycleAttribute', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboCycleAttribute]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboCycleAttribute]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboCycleAttribute]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'Districts', @source_owner = N'dbo', @source_object = N'Districts', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'Districts', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboDistricts]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboDistricts]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboDistricts]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'DropDownLists', @source_owner = N'dbo', @source_object = N'DropDownLists', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'DropDownLists', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboDropDownLists]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboDropDownLists]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboDropDownLists]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetChangeTable', @source_owner = N'dbo', @source_object = N'GetChangeTable', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetChangeTable', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetCycleAttributes', @source_owner = N'dbo', @source_object = N'GetCycleAttributes', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetCycleAttributes', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetNPTGAdminAreas', @source_owner = N'dbo', @source_object = N'GetNPTGAdminAreas', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetNPTGAdminAreas', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetNPTGDistricts', @source_owner = N'dbo', @source_object = N'GetNPTGDistricts', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetNPTGDistricts', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetRouteEnds', @source_owner = N'dbo', @source_object = N'GetRouteEnds', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetRouteEnds', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetRoutes', @source_owner = N'dbo', @source_object = N'GetRoutes', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetRoutes', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetStopAccessibilityLinks', @source_owner = N'dbo', @source_object = N'GetStopAccessibilityLinks', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetStopAccessibilityLinks', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetTravelcards', @source_owner = N'dbo', @source_object = N'GetTravelcards', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetTravelcards', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetZoneModes', @source_owner = N'dbo', @source_object = N'GetZoneModes', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetZoneModes', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetZones', @source_owner = N'dbo', @source_object = N'GetZones', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetZones', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetZoneStops', @source_owner = N'dbo', @source_object = N'GetZoneStops', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetZoneStops', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'ImportSJPStopAccessibilityLinks', @source_owner = N'dbo', @source_object = N'ImportSJPStopAccessibilityLinks', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'ImportSJPStopAccessibilityLinks', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'ImportSJPTravelcardData', @source_owner = N'dbo', @source_object = N'ImportSJPTravelcardData', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'ImportSJPTravelcardData', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'ImportSJPTravelNewsData', @source_owner = N'dbo', @source_object = N'ImportSJPTravelNewsData', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'ImportSJPTravelNewsData', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsVenue', @source_owner = N'dbo', @source_object = N'TravelNewsVenue', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'TravelNewsVenue', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboTravelNewsVenue]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboTravelNewsVenue]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboTravelNewsVenue]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'IsTravelNewsItemActive', @source_owner = N'dbo', @source_object = N'IsTravelNewsItemActive', @type = N'func schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'IsTravelNewsItemActive', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPEventDates', @source_owner = N'dbo', @source_object = N'SJPEventDates', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPEventDates', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPEventDates]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPEventDates]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPEventDates]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPRouteEndStops', @source_owner = N'dbo', @source_object = N'SJPRouteEndStops', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPRouteEndStops', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPRouteEndStops]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPRouteEndStops]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPRouteEndStops]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPRouteEndZones', @source_owner = N'dbo', @source_object = N'SJPRouteEndZones', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPRouteEndZones', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPRouteEndZones]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPRouteEndZones]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPRouteEndZones]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPRouteModes', @source_owner = N'dbo', @source_object = N'SJPRouteModes', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPRouteModes', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPRouteModes]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPRouteModes]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPRouteModes]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPRoutes', @source_owner = N'dbo', @source_object = N'SJPRoutes', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPRoutes', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPRoutes]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPRoutes]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPRoutes]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPTravelcardRoutes', @source_owner = N'dbo', @source_object = N'SJPTravelcardRoutes', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPTravelcardRoutes', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPTravelcardRoutes]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPTravelcardRoutes]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPTravelcardRoutes]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPTravelcards', @source_owner = N'dbo', @source_object = N'SJPTravelcards', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPTravelcards', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPTravelcards]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPTravelcards]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPTravelcards]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPTravelcardZones', @source_owner = N'dbo', @source_object = N'SJPTravelcardZones', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPTravelcardZones', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPTravelcardZones]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPTravelcardZones]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPTravelcardZones]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPZoneAreaPoints', @source_owner = N'dbo', @source_object = N'SJPZoneAreaPoints', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPZoneAreaPoints', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPZoneAreaPoints]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPZoneAreaPoints]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPZoneAreas]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPZoneAreas', @source_owner = N'dbo', @source_object = N'SJPZoneAreas', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPZoneAreas', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPZoneAreas]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPZoneAreas]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPZoneAreas]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPZoneModes', @source_owner = N'dbo', @source_object = N'SJPZoneModes', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPZoneModes', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPZoneModes]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPZoneModes]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPZoneModes]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPZones', @source_owner = N'dbo', @source_object = N'SJPZones', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPZones', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPZones]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPZones]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPZones]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPZoneStops', @source_owner = N'dbo', @source_object = N'SJPZoneStops', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPZoneStops', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPZoneStops]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPZoneStops]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPZoneStops]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'SJPSplit', @source_owner = N'dbo', @source_object = N'SJPSplit', @type = N'func schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'SJPSplit', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'StopAccessibilityLinks', @source_owner = N'dbo', @source_object = N'StopAccessibilityLinks', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'StopAccessibilityLinks', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboStopAccessibilityLinks]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboStopAccessibilityLinks]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboStopAccessibilityLinks]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'StripNaptan', @source_owner = N'dbo', @source_object = N'StripNaptan', @type = N'func schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'StripNaptan', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNews', @source_owner = N'dbo', @source_object = N'TravelNews', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'TravelNews', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboTravelNews]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboTravelNews]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboTravelNews]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsAll', @source_owner = N'dbo', @source_object = N'TravelNewsAll', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'TravelNewsAll', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsDataSource', @source_owner = N'dbo', @source_object = N'TravelNewsDataSource', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'TravelNewsDataSource', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboTravelNewsDataSource]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboTravelNewsDataSource]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboTravelNewsDataSource]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsDataSources', @source_owner = N'dbo', @source_object = N'TravelNewsDataSources', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'TravelNewsDataSources', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboTravelNewsDataSources]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboTravelNewsDataSources]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboTravelNewsDataSources]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsHeadlines', @source_owner = N'dbo', @source_object = N'TravelNewsHeadlines', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'TravelNewsHeadlines', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsRegion', @source_owner = N'dbo', @source_object = N'TravelNewsRegion', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'TravelNewsRegion', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboTravelNewsRegion]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboTravelNewsRegion]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboTravelNewsRegion]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsSeverity', @source_owner = N'dbo', @source_object = N'TravelNewsSeverity', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'TravelNewsSeverity', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboTravelNewsSeverity]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboTravelNewsSeverity]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboTravelNewsSeverity]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsToid', @source_owner = N'dbo', @source_object = N'TravelNewsToid', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'TravelNewsToid', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboTravelNewsToid]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboTravelNewsToid]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboTravelNewsToid]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'UpdateChangeNotificationTable', @source_owner = N'dbo', @source_object = N'UpdateChangeNotificationTable', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'UpdateChangeNotificationTable', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'VersionInfo', @source_owner = N'dbo', @source_object = N'VersionInfo', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'VersionInfo', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboVersionInfo]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboVersionInfo]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboVersionInfo]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'UndergroundStatus', @source_owner = N'dbo', @source_object = N'UndergroundStatus', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'UndergroundStatus', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboUndergroundStatus]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboUndergroundStatus]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboUndergroundStatus]'
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'ImportSJPUndergroundStatusData', @source_owner = N'dbo', @source_object = N'ImportSJPUndergroundStatusData', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'ImportSJPUndergroundStatusData', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'GetUndergroundStatus', @source_owner = N'dbo', @source_object = N'GetUndergroundStatus', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetUndergroundStatus', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'TravelNewsVenues', @source_owner = N'dbo', @source_object = N'TravelNewsVenues', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'TravelNewsVenues', @destination_owner = N'dbo', @status = 16
GO
use [SJPTransientPortal]
exec sp_addarticle @publication = N'SJPTransientPortalTransactionalPublication', @article = N'fn_TravelNewsRegion', @source_owner = N'dbo', @source_object = N'fn_TravelNewsRegion', @type = N'func schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'fn_TravelNewsRegion', @destination_owner = N'dbo', @status = 16
GO

-- Create a new snapshot job for the publication, using the defaults.
EXEC sp_addpublication_snapshot 
  @publication = N'SJPTransientPortalTransactionalPublication',
  @job_login = N'sjp\installapp',
  @job_password = N'!password!2';

-- Start the Snapshot Agent job.
EXEC sp_startpublication_snapshot @publication = N'SJPTransientPortalTransactionalPublication';
GO