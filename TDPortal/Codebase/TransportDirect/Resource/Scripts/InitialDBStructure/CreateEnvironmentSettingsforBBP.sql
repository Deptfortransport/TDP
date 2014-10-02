-- ***********************************************
-- NAME 	: CreateEnvironmentSettingsforBBP.sql
-- DESCRIPTION 	: Sets environment specific values into master
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/InitialDBStructure/CreateEnvironmentSettingsforBBP.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:42:20   mturner
--Initial revision.

USE [master]

IF EXISTS (select * from dbo.sysobjects where id = object_id(N'[dbo].[Environment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[Environment]
GO

CREATE TABLE [dbo].[Environment] (
	[ServerName] VARCHAR(20) NOT NULL ,
	[SecureDomainName] VARCHAR(20) NOT NULL ,
	[DMZDomainName] VARCHAR(20) NOT NULL ,
	[MISServerName] VARCHAR(20) NOT NULL  ,
	[GISServerName] VARCHAR(20) NOT NULL
)
GO

INSERT INTO Environment
VALUES ('BBP-DFT-D01', 'BBP-DFT-DOM', 'BBP-DFT-DOM3', 'BBP-DFT-M01', 'BBP-DFT-GI01' );