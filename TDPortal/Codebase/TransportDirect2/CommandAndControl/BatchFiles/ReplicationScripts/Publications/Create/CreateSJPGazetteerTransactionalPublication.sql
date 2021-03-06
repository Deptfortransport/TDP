-- Enabling the replication database
use master
exec sp_replicationdboption @dbname = N'SJPGazetteer', @optname = N'publish', @value = N'true'
GO

exec [SJPGazetteer].sys.sp_addlogreader_agent @job_login = N'sjp\installapp', @job_password = N'!password!2', @publisher_security_mode = 1
GO
exec [SJPGazetteer].sys.sp_addqreader_agent @job_login = null, @job_password = null, @frompublisher = 1
GO
-- Adding the transactional publication
use [SJPGazetteer]
exec sp_addpublication @publication = N'SJPGazetteerTransactionalPublication', @description = N'Transactional publication of database ''SJPGazetteer'' from Publisher ''MIS''.', @sync_method = N'concurrent', @retention = 0, @allow_push = N'true', @allow_pull = N'true', @allow_anonymous = N'true', @enabled_for_internet = N'false', @snapshot_in_defaultfolder = N'true', @compress_snapshot = N'false', @ftp_port = 21, @ftp_login = N'anonymous', @allow_subscription_copy = N'false', @add_to_active_directory = N'false', @repl_freq = N'continuous', @status = N'active', @independent_agent = N'true', @immediate_sync = N'true', @allow_sync_tran = N'false', @autogen_sync_procs = N'false', @allow_queued_tran = N'false', @allow_dts = N'false', @replicate_ddl = 1, @allow_initialize_from_backup = N'false', @enabled_for_p2p = N'false', @enabled_for_het_sub = N'false'
GO


exec sp_addpublication_snapshot @publication = N'SJPGazetteerTransactionalPublication', @frequency_type = 1, @frequency_interval = 0, @frequency_relative_interval = 0, @frequency_recurrence_factor = 0, @frequency_subday = 0, @frequency_subday_interval = 0, @active_start_time_of_day = 0, @active_end_time_of_day = 235959, @active_start_date = 0, @active_end_date = 0, @job_login = N'sjp\installapp', @job_password = N'!password!2', @publisher_security_mode = 1
exec sp_grant_publication_access @publication = N'SJPGazetteerTransactionalPublication', @login = N'sa'
GO
exec sp_grant_publication_access @publication = N'SJPGazetteerTransactionalPublication', @login = N'NT AUTHORITY\SYSTEM'
GO
exec sp_grant_publication_access @publication = N'SJPGazetteerTransactionalPublication', @login = N'BUILTIN\Administrators'
GO
exec sp_grant_publication_access @publication = N'SJPGazetteerTransactionalPublication', @login = N'SJP\installapp'
GO
exec sp_grant_publication_access @publication = N'SJPGazetteerTransactionalPublication', @login = N'NT SERVICE\SQLSERVERAGENT'
GO
exec sp_grant_publication_access @publication = N'SJPGazetteerTransactionalPublication', @login = N'NT SERVICE\MSSQLSERVER'
GO
exec sp_grant_publication_access @publication = N'SJPGazetteerTransactionalPublication', @login = N'distributor_admin'
GO

-- Adding the transactional articles
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'__RefactorLog', @source_owner = N'dbo', @source_object = N'__RefactorLog', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'__RefactorLog', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dbo__RefactorLog]', @del_cmd = N'CALL [dbo].[sp_MSdel_dbo__RefactorLog]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dbo__RefactorLog]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'AddChangeNotificationTable', @source_owner = N'dbo', @source_object = N'AddChangeNotificationTable', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddChangeNotificationTable', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ChangeNotification', @source_owner = N'dbo', @source_object = N'ChangeNotification', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'ChangeNotification', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboChangeNotification]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboChangeNotification]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboChangeNotification]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetAllVenueCarParks', @source_owner = N'dbo', @source_object = N'GetAllVenueCarParks', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetAllVenueCarParks', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetAllVenueCycleParks', @source_owner = N'dbo', @source_object = N'GetAllVenueCycleParks', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetAllVenueCycleParks', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetAllVenueRiverSerivces', @source_owner = N'dbo', @source_object = N'GetAllVenueRiverSerivces', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetAllVenueRiverSerivces', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetAlternativeSuggestionList', @source_owner = N'dbo', @source_object = N'GetAlternativeSuggestionList', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetAlternativeSuggestionList', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetChangeTable', @source_owner = N'dbo', @source_object = N'GetChangeTable', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetChangeTable', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetCycleVenueParkAvailability', @source_owner = N'dbo', @source_object = N'GetCycleVenueParkAvailability', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetCycleVenueParkAvailability', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetGNATAdminAreas', @source_owner = N'dbo', @source_object = N'GetGNATAdminAreas', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetGNATAdminAreas', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetGNATList', @source_owner = N'dbo', @source_object = N'GetGNATList', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetGNATList', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetGNATStopType', @source_owner = N'dbo', @source_object = N'GetGNATStopType', @type = N'func schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetGNATStopType', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetGroupLocation', @source_owner = N'dbo', @source_object = N'GetGroupLocation', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetGroupLocation', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetGroupNaPTANs', @source_owner = N'dbo', @source_object = N'GetGroupNaPTANs', @type = N'func schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetGroupNaPTANs', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetLocalityLocation', @source_owner = N'dbo', @source_object = N'GetLocalityLocation', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetLocalityLocation', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetLocalityLocations', @source_owner = N'dbo', @source_object = N'GetLocalityLocations', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetLocalityLocations', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetLocation', @source_owner = N'dbo', @source_object = N'GetLocation', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetLocation', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetLocations', @source_owner = N'dbo', @source_object = N'GetLocations', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetLocations', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetNaptanLocation', @source_owner = N'dbo', @source_object = N'GetNaptanLocation', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetNaptanLocation', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetPierToVenueNavigationPaths', @source_owner = N'dbo', @source_object = N'GetPierToVenueNavigationPaths', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetPierToVenueNavigationPaths', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetPostcodeLocation', @source_owner = N'dbo', @source_object = N'GetPostcodeLocation', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetPostcodeLocation', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetPostcodeLocations', @source_owner = N'dbo', @source_object = N'GetPostcodeLocations', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetPostcodeLocations', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetRiverServices', @source_owner = N'dbo', @source_object = N'GetRiverServices', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetRiverServices', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetUnknownLocation', @source_owner = N'dbo', @source_object = N'GetUnknownLocation', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetUnknownLocation', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueAccessData', @source_owner = N'dbo', @source_object = N'GetVenueAccessData', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetVenueAccessData', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueAdditonalData', @source_owner = N'dbo', @source_object = N'GetVenueAdditonalData', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetVenueAdditonalData', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueCarParkAvailability', @source_owner = N'dbo', @source_object = N'GetVenueCarParkAvailability', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetVenueCarParkAvailability', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueCarParkTransitShuttles', @source_owner = N'dbo', @source_object = N'GetVenueCarParkTransitShuttles', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetVenueCarParkTransitShuttles', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueCarParkTransitShuttleTransfers', @source_owner = N'dbo', @source_object = N'GetVenueCarParkTransitShuttleTransfers', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetVenueCarParkTransitShuttleTransfers', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueCycleParks', @source_owner = N'dbo', @source_object = N'GetVenueCycleParks', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetVenueCycleParks', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueGateCheckConstraints', @source_owner = N'dbo', @source_object = N'GetVenueGateCheckConstraints', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetVenueGateCheckConstraints', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueGateMaps', @source_owner = N'dbo', @source_object = N'GetVenueGateMaps', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetVenueGateMaps', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueGateNavigationPaths', @source_owner = N'dbo', @source_object = N'GetVenueGateNavigationPaths', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetVenueGateNavigationPaths', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueGates', @source_owner = N'dbo', @source_object = N'GetVenueGates', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetVenueGates', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueLocations', @source_owner = N'dbo', @source_object = N'GetVenueLocations', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetVenueLocations', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenuesList', @source_owner = N'dbo', @source_object = N'GetVenuesList', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetVenuesList', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPAdditionalVenueData', @source_owner = N'dbo', @source_object = N'ImportSJPAdditionalVenueData', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'ImportSJPAdditionalVenueData', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPCycleParkLocations', @source_owner = N'dbo', @source_object = N'ImportSJPCycleParkLocations', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'ImportSJPCycleParkLocations', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPGNATAdminAreasData', @source_owner = N'dbo', @source_object = N'ImportSJPGNATAdminAreasData', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'ImportSJPGNATAdminAreasData', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPGNATLocationsData', @source_owner = N'dbo', @source_object = N'ImportSJPGNATLocationsData', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'ImportSJPGNATLocationsData', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPParkAndRideLocations', @source_owner = N'dbo', @source_object = N'ImportSJPParkAndRideLocations', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'ImportSJPParkAndRideLocations', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPParkAndRideToids', @source_owner = N'dbo', @source_object = N'ImportSJPParkAndRideToids', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'ImportSJPParkAndRideToids', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPVenueAccessData', @source_owner = N'dbo', @source_object = N'ImportSJPVenueAccessData', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'ImportSJPVenueAccessData', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'ImportSJPVenueGateData', @source_owner = N'dbo', @source_object = N'ImportSJPVenueGateData', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'ImportSJPVenueGateData', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParkAvailability', @source_owner = N'dbo', @source_object = N'SJPCarParkAvailability', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPCarParkAvailability', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPCarParkAvailability]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPCarParkAvailability]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPCarParkAvailability]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParks', @source_owner = N'dbo', @source_object = N'SJPCarParks', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPCarParks', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPCarParks]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPCarParks]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPCarParks]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParksCarParkAvailability', @source_owner = N'dbo', @source_object = N'SJPCarParksCarParkAvailability', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPCarParksCarParkAvailability', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPCarParksCarParkAvailability]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPCarParksCarParkAvailability]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPCarParksCarParkAvailability]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParksCarParkTransitShuttles', @source_owner = N'dbo', @source_object = N'SJPCarParksCarParkTransitShuttles', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPCarParksCarParkTransitShuttles', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPCarParksCarParkTransitShuttles]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPCarParksCarParkTransitShuttles]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPCarParksCarParkTransitShuttles]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParkToids', @source_owner = N'dbo', @source_object = N'SJPCarParkToids', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPCarParkToids', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPCarParkToids]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPCarParkToids]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPCarParkToids]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParkTransitShuttles', @source_owner = N'dbo', @source_object = N'SJPCarParkTransitShuttles', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPCarParkTransitShuttles', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPCarParkTransitShuttles]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPCarParkTransitShuttles]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPCarParkTransitShuttles]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParkTransitShuttleTransfers', @source_owner = N'dbo', @source_object = N'SJPCarParkTransitShuttleTransfers', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPCarParkTransitShuttleTransfers', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPCarParkTransitShuttleTransfers]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPCarParkTransitShuttleTransfers]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPCarParkTransitShuttleTransfers]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCycleParkAvailability', @source_owner = N'dbo', @source_object = N'SJPCycleParkAvailability', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPCycleParkAvailability', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPCycleParkAvailability]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPCycleParkAvailability]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPCycleParkAvailability]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCycleParks', @source_owner = N'dbo', @source_object = N'SJPCycleParks', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPCycleParks', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPCycleParks]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPCycleParks]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPCycleParks]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCycleParksCycleParkAvailability', @source_owner = N'dbo', @source_object = N'SJPCycleParksCycleParkAvailability', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPCycleParksCycleParkAvailability', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPCycleParksCycleParkAvailability]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPCycleParksCycleParkAvailability]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPCycleParksCycleParkAvailability]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPGNATAdminAreas', @source_owner = N'dbo', @source_object = N'SJPGNATAdminAreas', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPGNATAdminAreas', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPGNATAdminAreas]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPGNATAdminAreas]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPGNATAdminAreas]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPGNATLocations', @source_owner = N'dbo', @source_object = N'SJPGNATLocations', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPGNATLocations', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPGNATLocations]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPGNATLocations]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPGNATLocations]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPNonPostcodeLocations', @source_owner = N'dbo', @source_object = N'SJPNonPostcodeLocations', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'SJPNonPostcodeLocations', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPNonPostcodeLocations]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPNonPostcodeLocations]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPNonPostcodeLocations]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPPierVenueNavigationPath', @source_owner = N'dbo', @source_object = N'SJPPierVenueNavigationPath', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPPierVenueNavigationPath', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPPierVenueNavigationPath]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPPierVenueNavigationPath]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPPierVenueNavigationPath]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPPierVenueNavigationPathTransfers', @source_owner = N'dbo', @source_object = N'SJPPierVenueNavigationPathTransfers', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPPierVenueNavigationPathTransfers', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPPierVenueNavigationPathTransfers]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPPierVenueNavigationPathTransfers]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPPierVenueNavigationPathTransfers]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPPostcodeLocations', @source_owner = N'dbo', @source_object = N'SJPPostcodeLocations', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'SJPPostcodeLocations', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPPostcodeLocations]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPPostcodeLocations]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPPostcodeLocations]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPRiverServices', @source_owner = N'dbo', @source_object = N'SJPRiverServices', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPRiverServices', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPRiverServices]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPRiverServices]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPRiverServices]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueAccessData', @source_owner = N'dbo', @source_object = N'SJPVenueAccessData', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPVenueAccessData', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPVenueAccessData]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPVenueAccessData]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPVenueAccessData]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueAccessTransfers', @source_owner = N'dbo', @source_object = N'SJPVenueAccessTransfers', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPVenueAccessTransfers', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPVenueAccessTransfers]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPVenueAccessTransfers]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPVenueAccessTransfers]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueAdditionalData', @source_owner = N'dbo', @source_object = N'SJPVenueAdditionalData', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPVenueAdditionalData', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPVenueAdditionalData]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPVenueAdditionalData]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPVenueAdditionalData]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueGateCheckConstraints', @source_owner = N'dbo', @source_object = N'SJPVenueGateCheckConstraints', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPVenueGateCheckConstraints', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPVenueGateCheckConstraints]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPVenueGateCheckConstraints]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPVenueGateCheckConstraints]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueGateMaps', @source_owner = N'dbo', @source_object = N'SJPVenueGateMaps', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPVenueGateMaps', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPVenueGateMaps]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPVenueGateMaps]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPVenueGateMaps]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueGateNavigationPaths', @source_owner = N'dbo', @source_object = N'SJPVenueGateNavigationPaths', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPVenueGateNavigationPaths', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPVenueGateNavigationPaths]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPVenueGateNavigationPaths]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPVenueGateNavigationPaths]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPVenueGates', @source_owner = N'dbo', @source_object = N'SJPVenueGates', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPVenueGates', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPVenueGates]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPVenueGates]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPVenueGates]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'UpdateChangeNotificationTable', @source_owner = N'dbo', @source_object = N'UpdateChangeNotificationTable', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'UpdateChangeNotificationTable', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'VersionInfo', @source_owner = N'dbo', @source_object = N'VersionInfo', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'VersionInfo', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboVersionInfo]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboVersionInfo]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboVersionInfo]'
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'GetVenueCarParkInformation', @source_owner = N'dbo', @source_object = N'GetVenueCarParkInformation', @type = N'proc schema only', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetVenueCarParkInformation', @destination_owner = N'dbo', @status = 16
GO
use [SJPGazetteer]
exec sp_addarticle @publication = N'SJPGazetteerTransactionalPublication', @article = N'SJPCarParkInformation', @source_owner = N'dbo', @source_object = N'SJPCarParkInformation', @type = N'logbased', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'none', @destination_table = N'SJPCarParkInformation', @destination_owner = N'dbo', @status = 24, @vertical_partition = N'false', @ins_cmd = N'CALL [dbo].[sp_MSins_dboSJPCarParkInformation]', @del_cmd = N'CALL [dbo].[sp_MSdel_dboSJPCarParkInformation]', @upd_cmd = N'SCALL [dbo].[sp_MSupd_dboSJPCarParkInformation]'
GO

-- Create a new snapshot job for the publication, using the defaults.
EXEC sp_addpublication_snapshot 
  @publication = N'SJPGazetteerTransactionalPublication',
  @job_login = N'sjp\installapp',
  @job_password = N'!password!2';

-- Start the Snapshot Agent job.
EXEC sp_startpublication_snapshot @publication = N'SJPGazetteerTransactionalPublication';
GO