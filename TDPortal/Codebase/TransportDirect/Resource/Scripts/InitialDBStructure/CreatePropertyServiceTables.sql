-- ***********************************************
-- NAME 	: CreatePropertyServiceTables.sql
-- DESCRIPTION 	: Creates the Properties tables, indexes, constraints,
--		: sprocs and functions.
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/InitialDBStructure/CreatePropertyServiceTables.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:42:22   mturner
--Initial revision.

USE [PermanentPortal]
GO

/* Create Properties Table */
 CREATE TABLE properties(
      pName       	VARCHAR(255) NOT NULL,
      pValue	  	VARCHAR(2000) NOT NULL,
      AID	  	VARCHAR(50) NOT NULL,
      GID		VARCHAR(50) NOT NULL
 )
GO

/* Create primary key constraints */
ALTER TABLE dbo.properties ADD CONSTRAINT
	PK_properties PRIMARY KEY CLUSTERED 
	(
	pName,
	AID,
	GID
	) ON [PRIMARY]
GO

/* Create stored procedures */

-- GetVersion
CREATE PROCEDURE GetVersion
AS
 SELECT PVALUE FROM PROPERTIES
 WHERE PNAME='propertyservice.version'
GO

-- SelectApplicationProperties
CREATE PROCEDURE SelectApplicationProperties @AID char(50)
AS
SELECT PNAME, PVALUE
FROM PROPERTIES P
WHERE P.AID = @AID
GO

-- SelectGlobalProperties
CREATE PROCEDURE SelectGlobalProperties
AS
SELECT PNAME, PVALUE
FROM PROPERTIES P
WHERE P.AID = '<DEFAULT>'
AND P.GID = '<DEFAULT>'
GO

-- SelectGroup Properties
CREATE PROCEDURE SelectGroupProperties @GID char(50)
AS
SELECT PNAME, PVALUE
FROM PROPERTIES P
WHERE P.GID = @GID
GO