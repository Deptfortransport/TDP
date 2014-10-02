-- ***********************************************
-- NAME 	: CreateDataGatewayExportTables.sql
-- DESCRIPTION 	: Creates Data Gateway Export tables, indexes, and constraints.
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/build/DataGatewayApps/Gateway/InitialSetup/DataExport.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 11:53:42   mturner
--Initial revision.

USE [TransientPortal]
GO

BEGIN TRANSACTION

-- Remove if table already exists.

IF EXISTS
 (
  SELECT * FROM sysobjects
  WHERE id = OBJECT_ID(N'[DataGatewayExport]')
  AND OBJECTPROPERTY(id, N'IsUserTable') = 1
 )
 DROP TABLE [DataGatewayExport]
GO

-- Insert new entries.

CREATE TABLE DataGatewayExport
 (
 server_src      varchar( 20) NOT NULL,
 server_dst      varchar( 20) NOT NULL,
 feed_name       varchar( 16) NOT NULL,
 feed_file       varchar(128) NOT NULL,
 time_logged     datetime     NOT NULL,
 time_transfer   datetime     NOT NULL,
 status_import   int          NOT NULL,
 status_transfer int          NOT NULL
 ) ON [PRIMARY]
GO

ALTER TABLE DataGatewayExport ADD CONSTRAINT
 DF_DataGatewayExport_logged DEFAULT '01-Jan-2000' FOR time_logged
GO

ALTER TABLE DataGatewayExport ADD CONSTRAINT
 DF_DataGatewayExport_processed DEFAULT '01-Jan-2000' FOR time_transfer
GO

ALTER TABLE DataGatewayExport ADD CONSTRAINT
 DF_DataGatewayExport_status_import DEFAULT 0 FOR status_import
GO

ALTER TABLE DataGatewayExport ADD CONSTRAINT
 DF_DataGatewayExport_status_transfer DEFAULT 0 FOR status_transfer
GO

ALTER TABLE DataGatewayExport ADD CONSTRAINT
 PK_GatewayExport PRIMARY KEY CLUSTERED 
 (
 feed_file, time_logged
 ) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_GatewayExport_File ON DataGatewayExport
 (
 server_dst, feed_file
 ) ON [PRIMARY]
GO

COMMIT
GO

-- End of Data Gateway Export Create Tables Script.
-- Update properties.

USE [PermanentPortal]
GO

BEGIN TRANSACTION
GO

DELETE FROM properties WHERE pName IN (
 'Gateway.Export.ServerID' ,
 'Gateway.Export.BBP.ftp'  ,
 'Gateway.Export.BBP.uid'  ,
 'Gateway.Export.BBP.pwd'
)
GO

INSERT INTO properties VALUES ('Gateway.Export.ServerID', 'BBP', '', 'DataGateway')
INSERT INTO properties VALUES ('Gateway.Export.BBP.ftp', '127.0.0.1', '', 'DataGateway')
INSERT INTO properties VALUES ('Gateway.Export.BBP.uid', 'TDP28Nov', '', 'DataGateway')
INSERT INTO properties VALUES ('Gateway.Export.BBP.pwd', 'sI1732#3-', '', 'DataGateway')
GO

COMMIT
GO

-- End of properties update.
