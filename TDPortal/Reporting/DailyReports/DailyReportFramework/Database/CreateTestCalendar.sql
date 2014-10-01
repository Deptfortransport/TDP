-- ***********************************************
-- NAME 	: CreateTestCalendar.sql
-- DESCRIPTION 	: Creates Reporting database test calendar
-- ************************************************
-- $Log:   P:/TDPortal/archives/Reporting/CreateTestCalendar.sql-arc  $
--
--   Rev 1.1   Mar 06 2008 10:14:16   pscott
--No change.
--
--   Rev 1.0   Jun 19 2006 16:14:26   PScott
--Initial revision.
--
--   Rev 1.0   Jun 19 2006 16:09:48   PScott
--Initial revision.
--
--   Rev 1.0   Jun 19 2006 16:01:48   PScott
--Initial revision.


USE [Reporting]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TestCalendar]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[TestCalendar]
GO

-- create properties table
CREATE TABLE [dbo].[TestCalendar] (
	[TimeStarted] [datetime] NOT NULL,
	[TimeCompleted] [datetime] NOT NULL) ON [PRIMARY]
GO

GO

 CREATE  INDEX [IX_TestCalendar] ON [dbo].[TestCalendar]([TimeStarted]) ON [PRIMARY]
GO


ALTER TABLE[dbo].[TestCalendar] ADD CONSTRAINT
	PK_properties PRIMARY KEY CLUSTERED 
	(
	TimeStarted
	) ON [PRIMARY]

GO


Insert Into TestCalendar
(TimeStarted,TimeCompleted)
Select
convert(datetime,'29/06/2004 01:00',105),convert(datetime,'29/06/2004 06:00',105)
Union
Select
convert(datetime,'27/06/2004 23:00',105),convert(datetime,'28/06/2004 04:00',105)
Union
Select
convert(datetime,'26/06/2004 23:00:00',105),convert(datetime,'27/06/2004 04:00:00',105)
Union
Select
convert(datetime,'22/06/2004 23:00:00',105),convert(datetime,'23/06/2004 04:00:00',105)
Union
Select
convert(datetime,'10/06/2004 23:00:00',105),convert(datetime,'11/06/2004 04:00:00',105)
Union
Select
convert(datetime,'08/06/2004 23:00:00',105),convert(datetime,'09/06/2004 04:00:00',105)
Union
Select
convert(datetime,'07/06/2004 23:00:00',105),convert(datetime,'08/06/2004 04:00:00',105)
Union
Select
convert(datetime,'06/06/2004 23:00:00',105),convert(datetime,'07/06/2004 00:00:01',105)
Union
Select
convert(datetime,'04/06/2004 23:00:00',105),convert(datetime,'05/06/2004 04:00:00',105)
