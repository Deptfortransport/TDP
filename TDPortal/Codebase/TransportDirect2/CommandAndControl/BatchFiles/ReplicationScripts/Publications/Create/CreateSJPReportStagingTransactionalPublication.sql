use [SJPReportStaging]
exec sp_replicationdboption @dbname = N'SJPReportStaging', @optname = N'publish', @value = N'true'
GO
use [SJPReportStaging]
exec [SJPReportStaging].sys.sp_addlogreader_agent @job_login = N'sjp\installapp', @job_password = N'!password!2', @publisher_security_mode = 1, @job_name = null
GO
-- Adding the transactional publication
use [SJPReportStaging]
exec sp_addpublication @publication = N'SJPReportStagingTransactionalPublication', @description = N'Transactional publication of database ''SJPReportStaging'' from Publisher ''MIS''.', @sync_method = N'concurrent', @retention = 0, @allow_push = N'true', @allow_pull = N'true', @allow_anonymous = N'true', @enabled_for_internet = N'false', @snapshot_in_defaultfolder = N'true', @compress_snapshot = N'false', @ftp_port = 21, @ftp_login = N'anonymous', @allow_subscription_copy = N'false', @add_to_active_directory = N'false', @repl_freq = N'continuous', @status = N'active', @independent_agent = N'true', @immediate_sync = N'true', @allow_sync_tran = N'false', @autogen_sync_procs = N'false', @allow_queued_tran = N'false', @allow_dts = N'false', @replicate_ddl = 1, @allow_initialize_from_backup = N'false', @enabled_for_p2p = N'false', @enabled_for_het_sub = N'false'
GO


exec sp_addpublication_snapshot @publication = N'SJPReportStagingTransactionalPublication', 
@frequency_type = 1, @frequency_interval = 0, @frequency_relative_interval = 0, @frequency_recurrence_factor = 0, 
@frequency_subday = 0, @frequency_subday_interval = 0, @active_start_time_of_day = 0, 
@active_end_time_of_day = 235959, @active_start_date = 0, @active_end_date = 0, 
@job_login = N'sjp\installapp', @job_password = N'!password!2', @publisher_security_mode = 1


use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'__RefactorLog', @source_owner = N'dbo', @source_object = N'__RefactorLog', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x000000000803509F, @identityrangemanagementoption = N'manual', @destination_table = N'__RefactorLog', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dbo__RefactorLog', @del_cmd = N'CALL sp_MSdel_dbo__RefactorLog', @upd_cmd = N'SCALL sp_MSupd_dbo__RefactorLog'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'ChangeCatalogue', @source_owner = N'dbo', @source_object = N'ChangeCatalogue', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'ChangeCatalogue', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboChangeCatalogue', @del_cmd = N'CALL sp_MSdel_dboChangeCatalogue', @upd_cmd = N'SCALL sp_MSupd_dboChangeCatalogue'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'CyclePlannerRequestEvent', @source_owner = N'dbo', @source_object = N'CyclePlannerRequestEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'CyclePlannerRequestEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboCyclePlannerRequestEvent', @del_cmd = N'CALL sp_MSdel_dboCyclePlannerRequestEvent', @upd_cmd = N'SCALL sp_MSupd_dboCyclePlannerRequestEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'CyclePlannerResultEvent', @source_owner = N'dbo', @source_object = N'CyclePlannerResultEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'CyclePlannerResultEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboCyclePlannerResultEvent', @del_cmd = N'CALL sp_MSdel_dboCyclePlannerResultEvent', @upd_cmd = N'SCALL sp_MSupd_dboCyclePlannerResultEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'DataGatewayEvent', @source_owner = N'dbo', @source_object = N'DataGatewayEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'DataGatewayEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboDataGatewayEvent', @del_cmd = N'CALL sp_MSdel_dboDataGatewayEvent', @upd_cmd = N'SCALL sp_MSupd_dboDataGatewayEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'EBCCalculationEvent', @source_owner = N'dbo', @source_object = N'EBCCalculationEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'EBCCalculationEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboEBCCalculationEvent', @del_cmd = N'CALL sp_MSdel_dboEBCCalculationEvent', @upd_cmd = N'SCALL sp_MSupd_dboEBCCalculationEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'EnhancedExposedServiceEvent', @source_owner = N'dbo', @source_object = N'EnhancedExposedServiceEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'EnhancedExposedServiceEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboEnhancedExposedServiceEvent', @del_cmd = N'CALL sp_MSdel_dboEnhancedExposedServiceEvent', @upd_cmd = N'SCALL sp_MSupd_dboEnhancedExposedServiceEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'ExposedServicesEvent', @source_owner = N'dbo', @source_object = N'ExposedServicesEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'ExposedServicesEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboExposedServicesEvent', @del_cmd = N'CALL sp_MSdel_dboExposedServicesEvent', @upd_cmd = N'SCALL sp_MSupd_dboExposedServicesEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'GazetteerEvent', @source_owner = N'dbo', @source_object = N'GazetteerEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'GazetteerEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboGazetteerEvent', @del_cmd = N'CALL sp_MSdel_dboGazetteerEvent', @upd_cmd = N'SCALL sp_MSupd_dboGazetteerEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'GradientProfileEvent', @source_owner = N'dbo', @source_object = N'GradientProfileEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'GradientProfileEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboGradientProfileEvent', @del_cmd = N'CALL sp_MSdel_dboGradientProfileEvent', @upd_cmd = N'SCALL sp_MSupd_dboGradientProfileEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'InternalRequestEvent', @source_owner = N'dbo', @source_object = N'InternalRequestEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'InternalRequestEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboInternalRequestEvent', @del_cmd = N'CALL sp_MSdel_dboInternalRequestEvent', @upd_cmd = N'SCALL sp_MSupd_dboInternalRequestEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'InternationalPlannerEvent', @source_owner = N'dbo', @source_object = N'InternationalPlannerEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'InternationalPlannerEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboInternationalPlannerEvent', @del_cmd = N'CALL sp_MSdel_dboInternationalPlannerEvent', @upd_cmd = N'SCALL sp_MSupd_dboInternationalPlannerEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'InternationalPlannerRequestEvent', @source_owner = N'dbo', @source_object = N'InternationalPlannerRequestEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'InternationalPlannerRequestEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboInternationalPlannerRequestEvent', @del_cmd = N'CALL sp_MSdel_dboInternationalPlannerRequestEvent', @upd_cmd = N'SCALL sp_MSupd_dboInternationalPlannerRequestEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'InternationalPlannerResultEvent', @source_owner = N'dbo', @source_object = N'InternationalPlannerResultEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'InternationalPlannerResultEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboInternationalPlannerResultEvent', @del_cmd = N'CALL sp_MSdel_dboInternationalPlannerResultEvent', @upd_cmd = N'SCALL sp_MSupd_dboInternationalPlannerResultEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'JourneyPlanRequestEvent', @source_owner = N'dbo', @source_object = N'JourneyPlanRequestEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'JourneyPlanRequestEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboJourneyPlanRequestEvent', @del_cmd = N'CALL sp_MSdel_dboJourneyPlanRequestEvent', @upd_cmd = N'SCALL sp_MSupd_dboJourneyPlanRequestEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'JourneyPlanRequestVerboseEvent', @source_owner = N'dbo', @source_object = N'JourneyPlanRequestVerboseEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'JourneyPlanRequestVerboseEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboJourneyPlanRequestVerboseEvent', @del_cmd = N'CALL sp_MSdel_dboJourneyPlanRequestVerboseEvent', @upd_cmd = N'SCALL sp_MSupd_dboJourneyPlanRequestVerboseEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'JourneyPlanResultsEvent', @source_owner = N'dbo', @source_object = N'JourneyPlanResultsEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'JourneyPlanResultsEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboJourneyPlanResultsEvent', @del_cmd = N'CALL sp_MSdel_dboJourneyPlanResultsEvent', @upd_cmd = N'SCALL sp_MSupd_dboJourneyPlanResultsEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'JourneyPlanResultsVerboseEvent', @source_owner = N'dbo', @source_object = N'JourneyPlanResultsVerboseEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'JourneyPlanResultsVerboseEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboJourneyPlanResultsVerboseEvent', @del_cmd = N'CALL sp_MSdel_dboJourneyPlanResultsVerboseEvent', @upd_cmd = N'SCALL sp_MSupd_dboJourneyPlanResultsVerboseEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'JourneyWebRequestEvent', @source_owner = N'dbo', @source_object = N'JourneyWebRequestEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'JourneyWebRequestEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboJourneyWebRequestEvent', @del_cmd = N'CALL sp_MSdel_dboJourneyWebRequestEvent', @upd_cmd = N'SCALL sp_MSupd_dboJourneyWebRequestEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'LandingPageEvent', @source_owner = N'dbo', @source_object = N'LandingPageEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'LandingPageEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboLandingPageEvent', @del_cmd = N'CALL sp_MSdel_dboLandingPageEvent', @upd_cmd = N'SCALL sp_MSupd_dboLandingPageEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'LocationRequestEvent', @source_owner = N'dbo', @source_object = N'LocationRequestEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'LocationRequestEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboLocationRequestEvent', @del_cmd = N'CALL sp_MSdel_dboLocationRequestEvent', @upd_cmd = N'SCALL sp_MSupd_dboLocationRequestEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'LoginEvent', @source_owner = N'dbo', @source_object = N'LoginEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'LoginEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboLoginEvent', @del_cmd = N'CALL sp_MSdel_dboLoginEvent', @upd_cmd = N'SCALL sp_MSupd_dboLoginEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'MailingProperties', @source_owner = N'dbo', @source_object = N'MailingProperties', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'MailingProperties', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboMailingProperties', @del_cmd = N'CALL sp_MSdel_dboMailingProperties', @upd_cmd = N'SCALL sp_MSupd_dboMailingProperties'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'MapAPIEvent', @source_owner = N'dbo', @source_object = N'MapAPIEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'MapAPIEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboMapAPIEvent', @del_cmd = N'CALL sp_MSdel_dboMapAPIEvent', @upd_cmd = N'SCALL sp_MSupd_dboMapAPIEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'MapEvent', @source_owner = N'dbo', @source_object = N'MapEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'MapEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboMapEvent', @del_cmd = N'CALL sp_MSdel_dboMapEvent', @upd_cmd = N'SCALL sp_MSupd_dboMapEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'NoResultsEvent', @source_owner = N'dbo', @source_object = N'NoResultsEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'NoResultsEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboNoResultsEvent', @del_cmd = N'CALL sp_MSdel_dboNoResultsEvent', @upd_cmd = N'SCALL sp_MSupd_dboNoResultsEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'OperationalEvent', @source_owner = N'dbo', @source_object = N'OperationalEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'OperationalEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboOperationalEvent', @del_cmd = N'CALL sp_MSdel_dboOperationalEvent', @upd_cmd = N'SCALL sp_MSupd_dboOperationalEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'PageEntryEvent', @source_owner = N'dbo', @source_object = N'PageEntryEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'PageEntryEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboPageEntryEvent', @del_cmd = N'CALL sp_MSdel_dboPageEntryEvent', @upd_cmd = N'SCALL sp_MSupd_dboPageEntryEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'ReferenceTransactionEvent', @source_owner = N'dbo', @source_object = N'ReferenceTransactionEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'ReferenceTransactionEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboReferenceTransactionEvent', @del_cmd = N'CALL sp_MSdel_dboReferenceTransactionEvent', @upd_cmd = N'SCALL sp_MSupd_dboReferenceTransactionEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'RepeatVisitorEvent', @source_owner = N'dbo', @source_object = N'RepeatVisitorEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'RepeatVisitorEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboRepeatVisitorEvent', @del_cmd = N'CALL sp_MSdel_dboRepeatVisitorEvent', @upd_cmd = N'SCALL sp_MSupd_dboRepeatVisitorEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'ReportStagingDataAudit', @source_owner = N'dbo', @source_object = N'ReportStagingDataAudit', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'ReportStagingDataAudit', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboReportStagingDataAudit', @del_cmd = N'CALL sp_MSdel_dboReportStagingDataAudit', @upd_cmd = N'SCALL sp_MSupd_dboReportStagingDataAudit'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'ReportStagingDataAuditType', @source_owner = N'dbo', @source_object = N'ReportStagingDataAuditType', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'ReportStagingDataAuditType', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboReportStagingDataAuditType', @del_cmd = N'CALL sp_MSdel_dboReportStagingDataAuditType', @upd_cmd = N'SCALL sp_MSupd_dboReportStagingDataAuditType'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'ReportStagingDataType', @source_owner = N'dbo', @source_object = N'ReportStagingDataType', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'ReportStagingDataType', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboReportStagingDataType', @del_cmd = N'CALL sp_MSdel_dboReportStagingDataType', @upd_cmd = N'SCALL sp_MSupd_dboReportStagingDataType'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'ReportStagingProperties', @source_owner = N'dbo', @source_object = N'ReportStagingProperties', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'ReportStagingProperties', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboReportStagingProperties', @del_cmd = N'CALL sp_MSdel_dboReportStagingProperties', @upd_cmd = N'SCALL sp_MSupd_dboReportStagingProperties'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'RetailerHandoffEvent', @source_owner = N'dbo', @source_object = N'RetailerHandoffEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'RetailerHandoffEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboRetailerHandoffEvent', @del_cmd = N'CALL sp_MSdel_dboRetailerHandoffEvent', @upd_cmd = N'SCALL sp_MSupd_dboRetailerHandoffEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'RTTIEvent', @source_owner = N'dbo', @source_object = N'RTTIEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'RTTIEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboRTTIEvent', @del_cmd = N'CALL sp_MSdel_dboRTTIEvent', @upd_cmd = N'SCALL sp_MSupd_dboRTTIEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'RTTIInternalEvent', @source_owner = N'dbo', @source_object = N'RTTIInternalEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'RTTIInternalEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboRTTIInternalEvent', @del_cmd = N'CALL sp_MSdel_dboRTTIInternalEvent', @upd_cmd = N'SCALL sp_MSupd_dboRTTIInternalEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'StopEventRequestEvent', @source_owner = N'dbo', @source_object = N'StopEventRequestEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'StopEventRequestEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboStopEventRequestEvent', @del_cmd = N'CALL sp_MSdel_dboStopEventRequestEvent', @upd_cmd = N'SCALL sp_MSupd_dboStopEventRequestEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'TempReferenceTransactionEvent', @source_owner = N'dbo', @source_object = N'TempReferenceTransactionEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'TempReferenceTransactionEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboTempReferenceTransactionEvent', @del_cmd = N'CALL sp_MSdel_dboTempReferenceTransactionEvent', @upd_cmd = N'SCALL sp_MSupd_dboTempReferenceTransactionEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'UserFeedbackEvent', @source_owner = N'dbo', @source_object = N'UserFeedbackEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'UserFeedbackEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboUserFeedbackEvent', @del_cmd = N'CALL sp_MSdel_dboUserFeedbackEvent', @upd_cmd = N'SCALL sp_MSupd_dboUserFeedbackEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'UserPreferenceSaveEvent', @source_owner = N'dbo', @source_object = N'UserPreferenceSaveEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'UserPreferenceSaveEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboUserPreferenceSaveEvent', @del_cmd = N'CALL sp_MSdel_dboUserPreferenceSaveEvent', @upd_cmd = N'SCALL sp_MSupd_dboUserPreferenceSaveEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'VersionInfo', @source_owner = N'dbo', @source_object = N'VersionInfo', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'VersionInfo', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboVersionInfo', @del_cmd = N'CALL sp_MSdel_dboVersionInfo', @upd_cmd = N'SCALL sp_MSupd_dboVersionInfo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'WorkloadEvent', @source_owner = N'dbo', @source_object = N'WorkloadEvent', @type = N'logbased', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048037FDF, @identityrangemanagementoption = N'manual', @destination_table = N'WorkloadEvent', @destination_owner = N'dbo', @vertical_partition = N'false', @ins_cmd = N'CALL sp_MSins_dboWorkloadEvent', @del_cmd = N'CALL sp_MSdel_dboWorkloadEvent', @upd_cmd = N'SCALL sp_MSupd_dboWorkloadEvent'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddCyclePlannerRequestEvent', @source_owner = N'dbo', @source_object = N'AddCyclePlannerRequestEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddCyclePlannerRequestEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddCyclePlannerResultEvent', @source_owner = N'dbo', @source_object = N'AddCyclePlannerResultEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddCyclePlannerResultEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddDataGatewayEvent', @source_owner = N'dbo', @source_object = N'AddDataGatewayEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddDataGatewayEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddEBCCalculationEvent', @source_owner = N'dbo', @source_object = N'AddEBCCalculationEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddEBCCalculationEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddEnhancedExposedServiceEvent', @source_owner = N'dbo', @source_object = N'AddEnhancedExposedServiceEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddEnhancedExposedServiceEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddExposedServicesEvent', @source_owner = N'dbo', @source_object = N'AddExposedServicesEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddExposedServicesEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddGazetteerEvent', @source_owner = N'dbo', @source_object = N'AddGazetteerEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddGazetteerEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddGradientProfileEvent', @source_owner = N'dbo', @source_object = N'AddGradientProfileEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddGradientProfileEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddInternalRequestEvent', @source_owner = N'dbo', @source_object = N'AddInternalRequestEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddInternalRequestEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddInternationalPlannerEvent', @source_owner = N'dbo', @source_object = N'AddInternationalPlannerEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddInternationalPlannerEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddInternationalPlannerRequestEvent', @source_owner = N'dbo', @source_object = N'AddInternationalPlannerRequestEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddInternationalPlannerRequestEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddInternationalPlannerResultEvent', @source_owner = N'dbo', @source_object = N'AddInternationalPlannerResultEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddInternationalPlannerResultEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddJourneyPlanRequestEvent', @source_owner = N'dbo', @source_object = N'AddJourneyPlanRequestEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddJourneyPlanRequestEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddJourneyPlanRequestVerboseEvent', @source_owner = N'dbo', @source_object = N'AddJourneyPlanRequestVerboseEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddJourneyPlanRequestVerboseEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddJourneyPlanResultsEvent', @source_owner = N'dbo', @source_object = N'AddJourneyPlanResultsEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddJourneyPlanResultsEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddJourneyPlanResultsVerboseEvent', @source_owner = N'dbo', @source_object = N'AddJourneyPlanResultsVerboseEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddJourneyPlanResultsVerboseEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddJourneyWebRequestEvent', @source_owner = N'dbo', @source_object = N'AddJourneyWebRequestEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddJourneyWebRequestEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddLandingPageEvent', @source_owner = N'dbo', @source_object = N'AddLandingPageEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddLandingPageEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddLocationRequestEvent', @source_owner = N'dbo', @source_object = N'AddLocationRequestEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddLocationRequestEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddLoginEvent', @source_owner = N'dbo', @source_object = N'AddLoginEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddLoginEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddMapAPIEvent', @source_owner = N'dbo', @source_object = N'AddMapAPIEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddMapAPIEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddMapEvent', @source_owner = N'dbo', @source_object = N'AddMapEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddMapEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddNoResultsEvent', @source_owner = N'dbo', @source_object = N'AddNoResultsEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddNoResultsEvent', @destination_owner = N'dbo'
GO



use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddOperationalEvent', @source_owner = N'dbo', @source_object = N'AddOperationalEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddOperationalEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddPageEntryEvent', @source_owner = N'dbo', @source_object = N'AddPageEntryEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddPageEntryEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddReferenceTransactionEvent', @source_owner = N'dbo', @source_object = N'AddReferenceTransactionEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddReferenceTransactionEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddReferenceTransactionEventSiteStatus', @source_owner = N'dbo', @source_object = N'AddReferenceTransactionEventSiteStatus', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddReferenceTransactionEventSiteStatus', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddRepeatVisitorEvent', @source_owner = N'dbo', @source_object = N'AddRepeatVisitorEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddRepeatVisitorEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddRepeatVisitorEventWebLogReader', @source_owner = N'dbo', @source_object = N'AddRepeatVisitorEventWebLogReader', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddRepeatVisitorEventWebLogReader', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddRetailerHandoffEvent', @source_owner = N'dbo', @source_object = N'AddRetailerHandoffEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddRetailerHandoffEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddRTTIInternalEvent', @source_owner = N'dbo', @source_object = N'AddRTTIInternalEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddRTTIInternalEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddStopEventRequestEvent', @source_owner = N'dbo', @source_object = N'AddStopEventRequestEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddStopEventRequestEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddUserFeedbackEvent', @source_owner = N'dbo', @source_object = N'AddUserFeedbackEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddUserFeedbackEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddUserPreferenceSaveEvent', @source_owner = N'dbo', @source_object = N'AddUserPreferenceSaveEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddUserPreferenceSaveEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'AddWorkloadEvent', @source_owner = N'dbo', @source_object = N'AddWorkloadEvent', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'AddWorkloadEvent', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'Archiver', @source_owner = N'dbo', @source_object = N'Archiver', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'Archiver', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'DeleteImportedStagingData', @source_owner = N'dbo', @source_object = N'DeleteImportedStagingData', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'DeleteImportedStagingData', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'DeleteStagingData', @source_owner = N'dbo', @source_object = N'DeleteStagingData', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'DeleteStagingData', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'Generate_JourneyPlan_Monthly_Extract', @source_owner = N'dbo', @source_object = N'Generate_JourneyPlan_Monthly_Extract', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'Generate_JourneyPlan_Monthly_Extract', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'Generate_MDV_Monthly_Extract', @source_owner = N'dbo', @source_object = N'Generate_MDV_Monthly_Extract', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'Generate_MDV_Monthly_Extract', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'Generate_Trapeze_Monthly_Extract', @source_owner = N'dbo', @source_object = N'Generate_Trapeze_Monthly_Extract', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'Generate_Trapeze_Monthly_Extract', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'GetLatestImported', @source_owner = N'dbo', @source_object = N'GetLatestImported', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetLatestImported', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'GetMailingProperties', @source_owner = N'dbo', @source_object = N'GetMailingProperties', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetMailingProperties', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'GetTableColumnLengths', @source_owner = N'dbo', @source_object = N'GetTableColumnLengths', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'GetTableColumnLengths', @destination_owner = N'dbo'
GO




use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'RTTIEventLog', @source_owner = N'dbo', @source_object = N'RTTIEventLog', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000008000001, @destination_table = N'RTTIEventLog', @destination_owner = N'dbo'
GO

use [SJPReportStaging]
exec sp_addarticle @publication = N'SJPReportStagingTransactionalPublication', @article = N'Generate_NO_RESULTS_EVENT_Monthly_Extract', @source_owner = N'dbo', @source_object = N'Generate_NO_RESULTS_EVENT_Monthly_Extract', @type = N'proc schema only', @description = null, @creation_script = null, @pre_creation_cmd = N'drop', @schema_option = 0x0000000048002001, @destination_table = N'Generate_NO_RESULTS_EVENT_Monthly_Extract', @destination_owner = N'dbo'
GO

