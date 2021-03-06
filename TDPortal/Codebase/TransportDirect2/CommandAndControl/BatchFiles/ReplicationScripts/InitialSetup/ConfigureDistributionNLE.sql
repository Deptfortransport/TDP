/****** Scripting replication configuration. Script Date: 22/06/2011 15:21:28 ******/
/****** Please Note: For security reasons, all password parameters were scripted with either NULL or an empty string. ******/

/****** Begin: Script to be run at Publisher ******/

/****** Installing the server as a Distributor. Script Date: 22/06/2011 15:21:28 ******/
use master
exec sp_adddistributor @distributor = N'SJP-NLE-MIS-01', @password = N''
GO

-- Adding the agent profiles
-- Updating the agent profile defaults
exec sp_MSupdate_agenttype_default @profile_id = 1
GO
exec sp_MSupdate_agenttype_default @profile_id = 2
GO
exec sp_MSupdate_agenttype_default @profile_id = 4
GO
exec sp_MSupdate_agenttype_default @profile_id = 6
GO
exec sp_MSupdate_agenttype_default @profile_id = 11
GO

-- Adding the distribution databases
use master
exec sp_adddistributiondb @database = N'distribution', @data_folder = N'g:\MSSQL10_50.MSSQLSERVER\MSSQL\Data', @data_file = N'distribution.MDF', @data_file_size = 18, @log_folder = N'h:\MSSQL10_50.MSSQLSERVER\MSSQL\Data', @log_file = N'distribution.LDF', @log_file_size = 8, @min_distretention = 0, @max_distretention = 72, @history_retention = 48, @security_mode = 1
GO

-- Adding the distribution publishers
exec sp_adddistpublisher @publisher = N'SJP-NLE-MIS-01', @distribution_db = N'distribution', @security_mode = 0, @login = N'sa', @password = N'', @working_directory = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\ReplData', @trusted = N'false', @thirdparty_flag = 0, @publisher_type = N'MSSQLSERVER'
GO

exec sp_addsubscriber @subscriber = N'SJP-NLE-WU-01', @type = 0, @description = N''
GO
exec sp_addsubscriber @subscriber = N'SJP-NLE-WU-02', @type = 0, @description = N''
GO


/****** End: Script to be run at Publisher ******/


