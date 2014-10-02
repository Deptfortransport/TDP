-- ***********************************************
-- AUTHOR      	: Rich Broddle
-- NAME 	: MDS018_EBC_Data.sql
-- DESCRIPTION 	: Updates the EBC MDS Data which is held on Permanent Portal DB
-- SOURCE 	: Manual Data Service
-- Version	: $Revision:   1.1  $
-- Additional Steps Required : None - updates via change notification.
-- ************************************************
--$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS018_EBC_Data.sql-arc  $
--
--   Rev 1.1   Dec 09 2009 14:18:30   rbroddle
--Updated with latest data
--
--   Rev 1.0   Dec 09 2009 10:54:54   rbroddle
--Initial revision.
--

USE [PermanentPortal]
GO

DELETE EnvBenRoadCategoryCost
DELETE EnvBenHighValueMotorway
DELETE EnvBenHighValueMotorwayJunction
DELETE EnvBenUnknownMotorwayJunction

---------------------------------------------------------------------
-- EnvBenRoadCategoryCost table

INSERT INTO EnvBenRoadCategoryCost VALUES ('MotorwayHigh', 'England', 86.0)
INSERT INTO EnvBenRoadCategoryCost VALUES ('MotorwayHigh', 'Scotland', 86.0)
INSERT INTO EnvBenRoadCategoryCost VALUES ('MotorwayHigh', 'Wales', 86.0)

INSERT INTO EnvBenRoadCategoryCost VALUES ('MotorwayStandard', 'England', 7.0)
INSERT INTO EnvBenRoadCategoryCost VALUES ('MotorwayStandard', 'Scotland', 7.0)
INSERT INTO EnvBenRoadCategoryCost VALUES ('MotorwayStandard', 'Wales', 7.0)

INSERT INTO EnvBenRoadCategoryCost VALUES ('RoadStandardA', 'England', 74.0)
INSERT INTO EnvBenRoadCategoryCost VALUES ('RoadStandardA', 'Scotland', 74.0)
INSERT INTO EnvBenRoadCategoryCost VALUES ('RoadStandardA', 'Wales', 74.0)

INSERT INTO EnvBenRoadCategoryCost VALUES ('RoadOther', 'England', 143.0)
INSERT INTO EnvBenRoadCategoryCost VALUES ('RoadOther', 'Scotland', 143.0)
INSERT INTO EnvBenRoadCategoryCost VALUES ('RoadOther', 'Wales', 143.0)

---------------------------------------------------------------------

---------------------------------------------------------------------
-- EnvBenHighValueMotorway table

INSERT INTO [EnvBenHighValueMotorway]([MotorwayName],[AllHighValue],[DuplicateMotorwayJunction]) VALUES ('M1',0,'')
INSERT INTO [EnvBenHighValueMotorway]([MotorwayName],[AllHighValue],[DuplicateMotorwayJunction]) VALUES ('M3',0,'')
INSERT INTO [EnvBenHighValueMotorway]([MotorwayName],[AllHighValue],[DuplicateMotorwayJunction]) VALUES ('M4',0,'M48')
INSERT INTO [EnvBenHighValueMotorway]([MotorwayName],[AllHighValue],[DuplicateMotorwayJunction]) VALUES ('M6',0,'M6 TOLL')
INSERT INTO [EnvBenHighValueMotorway]([MotorwayName],[AllHighValue],[DuplicateMotorwayJunction]) VALUES ('M8',0,'')
INSERT INTO [EnvBenHighValueMotorway]([MotorwayName],[AllHighValue],[DuplicateMotorwayJunction]) VALUES ('M25',1,'')
INSERT INTO [EnvBenHighValueMotorway]([MotorwayName],[AllHighValue],[DuplicateMotorwayJunction]) VALUES ('M42',0,'')
INSERT INTO [EnvBenHighValueMotorway]([MotorwayName],[AllHighValue],[DuplicateMotorwayJunction]) VALUES ('M60',1,'')
INSERT INTO [EnvBenHighValueMotorway]([MotorwayName],[AllHighValue],[DuplicateMotorwayJunction]) VALUES ('M62',0,'M60')


---------------------------------------------------------------------

---------------------------------------------------------------------
-- EnvBenHighValueMotorwayJunction table

INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M1','15','15a',4344,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M1','15a','16',6275,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M1','16','17',13837,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M1','28','29',10941,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M1','29','30',10941,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M1','30','31',9010,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M1','31','32',5310,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M1','32','33',3862,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M1','33','34',4344,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M1','34','35',6275,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M1','35','35a',2414,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M3','9','10',2253,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M3','10','11',2092,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M3','11','12',5632,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M3','12','13',2735,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M3','13','14',1931,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M4','4b','5',3379,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M4','5','6',5953,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M4','6','7',2735,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M4','30','32',9010,'Wales')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M4','32','33',5471,'Wales')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M6','4','4a',4344,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M6','4a','5',3379,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M6','5','6',5149,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M6','6','7',7080,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M6','7','8',805,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M6','8','9',4666,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M6','9','10',2414,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M6','10','10a',5632,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M6','15','16',15125,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M6','16','17',9815,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M6','17','18',5953,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M6','18','19',13033,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M6','19','20',7401,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M6','20','21',4988,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M6','21','21a',4666,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M8','8','9',1931,'Scotland')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M8','9','10',1448,'Scotland')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M8','10','11',1770,'Scotland')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M8','11','12',1931,'Scotland')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M8','12','13',1126,'Scotland')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M8','13','14',965,'Scotland')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M8','14','15',1126,'Scotland')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M8','15','16',965,'Scotland')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M8','16','17',1126,'Scotland')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M8','17','19',1126,'Scotland')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M42','3a','4',3379,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M42','4','5',3862,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M42','5','6',5792,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M42','6','7',3701,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M62','18','19',4827,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M62','19','20',3540,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M62','20','21',3540,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M62','26','27',7241,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M62','27','28',4827,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M62','28','29',4505,'England')
INSERT INTO [EnvBenHighValueMotorwayJunction]([MotorwayName],[JunctionStart],[JunctionEnd],[Distance],[Country]) VALUES ('M62','29','30',3540,'England')


---------------------------------------------------------------------

---------------------------------------------------------------------
-- EnvBenUnknownMotorwayJunction table

INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M1','A1(M)','','48','48')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M1','M18','','32','32')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M1','M25','','6a','6a')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M1','M45','','17','17')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M1','M6','','19','19')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M1','M62','','42','42')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M1','M621','','43','43')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M3','M25','','2','2')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M3','M27','','14','14')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M4','A329(M)','','10','10')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M4','A48(M)','','29','29')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M4','M25','','4b','4b')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M4','M48','21','21','21')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M4','M48','','23','23')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M4','M5','','20','20')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M40','M42','','17','17')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M42','M40','','3a','3a')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M42','M5','','0','0')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M42','M6','','7','7')
--Deliberately omitted:   INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M42','M6','','8','8')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M42','M6 TOLL','','8a','8a')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M45','M1','','0','0')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M56','M53','','15','15')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M56','M6','','9','9')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M56','M60','','1','1')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','A14','','0','0')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','A38(M)','','6','6')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','A601(M)','','35','35')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','A74(M)','','45','45')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','M1','','0','0')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','M42','','4','4')
--Deliberately omitted:   INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','M42','','4a','4a')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','M5','','8','8')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','M55','','32','32')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','M56','','20a','20a')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','M58','','26','26')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','M6 TOLL','','11a','11a')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','M6 TOLL','5','3a','3a')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','M61','','30','30')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','M62','','21a','21a')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','M65','','29','29')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M6','M69','','2','2')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M62','A1(M)','','32a','32a')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M62','A627(M)','','20','20')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M62','M1','','29','29')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M62','M18','','35','35')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M62','M6','','10','10')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M62','M60','12','12','12')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M62','M60','','18','18')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M62','M602','','12','12')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M62','M606','','26','26')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M62','M621','','27','27')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M62','M66','','18','18')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M69','M6','','0','0')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M73','M8','','2','2')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M77','M8','','0','0')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M8','A89','','8','8')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M8','M73','','8','8')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M8','M80','','13','13')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M8','M9','','2a','2a')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M80','M8','','1','1')
INSERT INTO [EnvBenUnknownMotorwayJunction] ([MotorwayName],[JoiningRoad],[JoiningJunction],[JunctionExit],[JunctionEntry]) VALUES ( 'M9','M8','','0','0')


---------------------------------------------------------------------
GO


----------------
-- Change Log --
----------------

USE PermanentPortal

Declare @@value decimal(7,3)
Select @@value = left(right('$Revision:   1.1  $',8),7)


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 018 and VersionNumber = @@value)
BEGIN
	UPDATE dbo.MDSChangeCatalogue
	SET
		ChangeDate = getdate(),
		Summary = 'Updated with latest EBC MDS data'
	WHERE ScriptNumber = 018 AND VersionNumber = @@value
END
ELSE
BEGIN
	INSERT INTO dbo.MDSChangeCatalogue
	(
		ScriptNumber,
		VersionNumber,
		Summary
	)
	VALUES
	(
		018,
		@@value,
		'Upadted with latest EBC MDS data'
	)
END