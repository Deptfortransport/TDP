-- **********************************************************************
-- $Workfile:   MDS013_CityToCityTables.sql  $
-- AUTHOR       : j.roberts
-- DATE CREATED : 15/09/2008
-- REVISION     : $Revision:   1.17  $
-- DESCRIPTION  : Update script for IF088_098_200809041340 
--                calls INFAddImportantPlace, 
--                INFAddNaptanTimeRelationship, 
--                INFAddImportantPlaceLocationRelationship
-- **********************************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS013_CityToCityTables.sql-arc  $
--
--   Rev 1.17   Jun 27 2011 09:59:16   PScott
--Corrected issue with quotes on NaptanTimeRelationship data.
--
--   Rev 1.16   Jun 22 2011 12:52:26   PScott
--Amendments supplied by Chris P.
--
--   Rev 1.15   Mar 23 2011 15:13:56   MTurner
--Updates supplied by Chris P
--
--   Rev 1.14   Sep 08 2010 13:45:20   nrankin
--City-2-city CarLocationPoints update 20100907 - Sent by Chris Pie
--
--   Rev 1.13   Aug 27 2010 11:48:04   mturner
--Updates from MDS supplied by Chris P
--
--   Rev 1.12   Aug 25 2010 13:11:48   mturner
--Updates from MDS supplied by Peter Arnold (UK:C3212212)
--
--   Rev 1.11   Jun 22 2010 07:59:50   MTurner
--Updates from MDS for some car coordinates (inc Birmingham and Nottingham)
--
--   Rev 1.10   May 18 2010 16:00:04   RBroddle
--Update from Chris Pietruski
--
--   Rev 1.9   Mar 24 2010 15:11:54   rhopkins
--Add Rochester to City-to-City and add additional stop for Chatham
--Resolution for 5466: Add Rochester to City-to-City
--Resolution for 5467: Add new coach stops for Rochester and Chatham for City-to-City
--
--   Rev 1.8   Nov 27 2009 11:11:36   nrankin
--Amended C2C Car Location Points (Chris Pie) 27/11/09
--
--   Rev 1.7   Aug 28 2009 12:24:42   nrankin
--City 2 city updates from MDS team
--
--   Rev 1.6   Jun 29 2009 12:25:02   jroberts
--IF088_098_200906241010
--
--   Rev 1.5   Mar 20 2009 14:06:04   nrankin
--Updated for Peter Arnold (C1604314)
--
--   Rev 1.4   Sep 16 2008 11:48:30   jroberts
--correct sproc
--
--   Rev 1.3   Sep 16 2008 11:15:02   jroberts
--.
--
--   Rev 1.2   Sep 16 2008 10:19:56   jroberts
--change catalogue
--
--   Rev 1.1   Sep 16 2008 09:34:22   jroberts
--Add sprocs and change catalogue
--
--   Rev 1.0   Sep 15 2008 18:27:30   jroberts
--Initial revision.
--
--
--
--


USE PermanentPortal
GO


GO
-- **********************************************************************
-- PROCEDURE INFAddImportantPlace
-- **********************************************************************
DECLARE @ObjectName AS varchar(128)
SET @ObjectName = N'INFAddImportantPlace'
IF NOT EXISTS(SELECT 1 
                FROM INFORMATION_SCHEMA.ROUTINES
               WHERE ROUTINE_NAME = @ObjectName
                 AND ROUTINE_TYPE = N'PROCEDURE')
BEGIN
    EXEC ('CREATE PROCEDURE [dbo].INFAddImportantPlace AS BEGIN SELECT 1 END')

    EXEC sp_addextendedproperty @name=N'Design', @value=N'SD00?? ?????' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Owner', @value=N'TDP' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Used By', @value=N'TDP.Infrastructure' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName

END
GO


GO
-- **********************************************************************
-- PROCEDURE INFAddImportantPlace
-- Description: 
-- 
-- **********************************************************************
ALTER PROCEDURE dbo.INFAddImportantPlace
(
	@TDNPGID int,
	@PlaceType varchar(20),
	@PlaceName varchar(50),
	@Locality varchar (8)
) AS
BEGIN
    SET NOCOUNT ON
    IF EXISTS(SELECT 1
                FROM dbo.ImportantPlace
               WHERE TDNPGID = @TDNPGID)
        BEGIN
          UPDATE dbo.ImportantPlace
             SET PlaceType = @PlaceType,
                 PlaceName = @PlaceName,
                 Locality  = @Locality
           WHERE TDNPGID   = @TDNPGID
           PRINT 'Updated ImportantPlace TDNPGID ' + Cast(@TDNPGID as varchar(10))
        END
    ELSE
        BEGIN
            INSERT INTO dbo.ImportantPlace 
            (
                TDNPGID,
                PlaceType,
                PlaceName,
                Locality
            )
            VALUES
            (
                @TDNPGID,
                @PlaceType,
                @PlaceName,
                @Locality            
            )
            PRINT 'Inserted ImportantPlace TDNPGID ' + Cast(@TDNPGID as varchar(10))
        END
    --END IF    
END
GO




GO
-- **********************************************************************
-- PROCEDURE INFAddNaptanTimeRelationship
-- **********************************************************************
DECLARE @ObjectName AS varchar(128)
SET @ObjectName = N'INFAddNaptanTimeRelationship'
IF NOT EXISTS(SELECT 1 
                FROM INFORMATION_SCHEMA.ROUTINES
               WHERE ROUTINE_NAME = @ObjectName
                 AND ROUTINE_TYPE = N'PROCEDURE')
BEGIN
    EXEC ('CREATE PROCEDURE [dbo].INFAddNaptanTimeRelationship AS BEGIN SELECT 1 END')

    EXEC sp_addextendedproperty @name=N'Design', @value=N'SD00?? ?????' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Owner', @value=N'TDP' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Used By', @value=N'TDP.Infrastructure' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName

END
GO


GO
-- **********************************************************************
-- PROCEDURE INFAddNaptanTimeRelationship
-- Description: 
-- 
-- **********************************************************************
ALTER PROCEDURE dbo.INFAddNaptanTimeRelationship
(
	@TDNPGID int,
	@Naptan varchar(12),
	@Mode varchar(10),
	@OSGREasting int,
	@OSGRNorthing int,
	@UseForFareEnquiries char(1),
	@UseDirectAir bit,
	@UseCombinedAir bit,
	@CombinedAirSecondaryModes varchar(50) = NULL
) AS 
SET NOCOUNT ON

BEGIN
  INSERT INTO dbo.NaptanTimeRelationship 
    (
        TDNPGID,
        Naptan,
        Mode,
        OSGREasting,
        OSGRNorthing,
        UseForFareEnquiries,
        UseDirectAir,
        UseCombinedAir,
        CombinedAirSecondaryModes
    )
    VALUES
    (
        @TDNPGID,
        @Naptan,
        @Mode,
        @OSGREasting,
        @OSGRNorthing,
        @UseForFareEnquiries,
        @UseDirectAir,
        @UseCombinedAir,
        @CombinedAirSecondaryModes
    )
  PRINT 'Inserted NaptanTimeRelationship TDNPGID ' + Cast(@TDNPGID as varchar(10)) + ', Naptan ' + @Naptan
END

GO




-- **********************************************************************
-- PROCEDURE dbo.INFAddImportantPlaceLocationRelationship
-- **********************************************************************
DECLARE @ObjectName AS varchar(128)
SET @ObjectName = N'INFAddImportantPlaceLocationRelationship'
IF NOT EXISTS(SELECT 1 
                FROM INFORMATION_SCHEMA.ROUTINES
               WHERE ROUTINE_NAME = @ObjectName
                 AND ROUTINE_TYPE = N'PROCEDURE')
BEGIN
    EXEC ('CREATE PROCEDURE [dbo].INFAddImportantPlaceLocationRelationship AS BEGIN SELECT 1 END')

    EXEC sp_addextendedproperty @name=N'Design', @value=N'SD00?? ?????' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Owner', @value=N'TDP' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Used By', @value=N'TDP.Infrastructure' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName

END
GO


GO
-- **********************************************************************
-- PROCEDURE dbo.INFAddImportantPlaceLocationRelationship
-- Description: 
-- 
-- **********************************************************************
ALTER PROCEDURE dbo.INFAddImportantPlaceLocationRelationship
(
    @TDNPGID      int,
    @Mode         varchar(10),
    @OSGREasting  int,
    @OSGRNorthing int,
    @StartTime    int,
    @EndTime      int,
    @DaysValid    varchar(7),
    @Comments     varchar(100)
) AS
BEGIN
 SET NOCOUNT ON
   INSERT INTO dbo.ImportantPlaceLocationRelationship 
   (
      [TDNPGID],
      [Mode],
      [OSGREasting],
      [OSGRNorthing],
      [StartTime],
      [EndTime],
      [DaysValid],
      [Comments]
   )
   VALUES
   (
      @TDNPGID,
      @Mode,
      @OSGREasting,
      @OSGRNorthing,
      @StartTime,
      @EndTime,
      @DaysValid,
      @Comments
   )
   PRINT 'Inserted ImportantPlaceLocationRelationship TDNPGID ' + Cast(@TDNPGID as varchar(10)) + ', Mode ' + @Mode + ', OSGREasting ' + Cast(@OSGREasting as varchar(10)) + ', OSGRNorthing ' + Cast(@OSGRNorthing as varchar(10))  + ', StartTime ' +  Cast(@StartTime as varchar(10))  +   ', EndTime ' +  Cast(@EndTime as varchar(10))  
END
GO

USE PermanentPortal
GO
EXEC dbo.INFAddImportantPlace 1,'City','Aberdeen','ES000011'
EXEC dbo.INFAddImportantPlace 2,'City','Aberystwyth','E0054103'
EXEC dbo.INFAddImportantPlace 8,'City','Ashford','E0056015'
EXEC dbo.INFAddImportantPlace 11,'City','Bangor','E0054265'
EXEC dbo.INFAddImportantPlace 12,'City','Barnsley','E0057864'
EXEC dbo.INFAddImportantPlace 13,'City','Barnstaple','E0045349'
EXEC dbo.INFAddImportantPlace 16,'City','Basingstoke','E0057416'
EXEC dbo.INFAddImportantPlace 17,'City','Bath Spa','E0054812'
EXEC dbo.INFAddImportantPlace 18,'City','Bedford','E0057294'
EXEC dbo.INFAddImportantPlace 19,'City','Berwick-upon-Tweed','E0056417'
EXEC dbo.INFAddImportantPlace 21,'City','Birmingham','E0057937'
EXEC dbo.INFAddImportantPlace 22,'City','Blackburn','E0057142'
EXEC dbo.INFAddImportantPlace 23,'City','Blackpool','E0057144'
EXEC dbo.INFAddImportantPlace 25,'City','Bolton','E0057777'
EXEC dbo.INFAddImportantPlace 26,'City','Bournemouth','E0057149'
EXEC dbo.INFAddImportantPlace 28,'City','Bradford','E0057948'
EXEC dbo.INFAddImportantPlace 30,'City','Brighton','E0057155'
EXEC dbo.INFAddImportantPlace 31,'City','Bristol','E0057160'
EXEC dbo.INFAddImportantPlace 33,'City','Burnley','E0057499'
EXEC dbo.INFAddImportantPlace 35,'City','Bury St Edmunds','E0025013'
EXEC dbo.INFAddImportantPlace 37,'City','Cambridge','E0055326'
EXEC dbo.INFAddImportantPlace 38,'City','Canterbury','E0057480'
EXEC dbo.INFAddImportantPlace 39,'City','Cardiff','E0035815'
EXEC dbo.INFAddImportantPlace 40,'City','Carlisle','E0055501'
EXEC dbo.INFAddImportantPlace 41,'City','Carmarthen','E0054033'
EXEC dbo.INFAddImportantPlace 42,'City','Chatham','E0039105'
EXEC dbo.INFAddImportantPlace 43,'City','Chelmsford','E0055790'
EXEC dbo.INFAddImportantPlace 46,'City','Chester','E0057329'
EXEC dbo.INFAddImportantPlace 47,'City','Chesterfield','E0057348'
EXEC dbo.INFAddImportantPlace 48,'City','Chichester','E0052092'
EXEC dbo.INFAddImportantPlace 50,'City','Colchester','E0055798'
EXEC dbo.INFAddImportantPlace 51,'City','Coventry','N0073053'
EXEC dbo.INFAddImportantPlace 52,'City','Crawley','E0057699'
EXEC dbo.INFAddImportantPlace 53,'City','Crewe','E0001658'
EXEC dbo.INFAddImportantPlace 56,'City','Darlington','E0054904'
EXEC dbo.INFAddImportantPlace 58,'City','Derby','E0054915'
EXEC dbo.INFAddImportantPlace 59,'City','Dewsbury','E0032592'
EXEC dbo.INFAddImportantPlace 60,'City','Doncaster','E0057875'
EXEC dbo.INFAddImportantPlace 61,'City','Dorchester','E0045812'
EXEC dbo.INFAddImportantPlace 62,'City','Dover','E0047158'
EXEC dbo.INFAddImportantPlace 63,'City','Dumfries','ES001098'
EXEC dbo.INFAddImportantPlace 64,'City','Dundee','ES003919'
EXEC dbo.INFAddImportantPlace 66,'City','Durham','E0055696'
EXEC dbo.INFAddImportantPlace 71,'City','Edinburgh','ES001268'
EXEC dbo.INFAddImportantPlace 72,'City','Ely','E0043670'
EXEC dbo.INFAddImportantPlace 74,'City','Exeter','E0055597'
EXEC dbo.INFAddImportantPlace 81,'City','Fishguard','E0055082'
EXEC dbo.INFAddImportantPlace 82,'City','Folkestone','E0015186'
EXEC dbo.INFAddImportantPlace 83,'City','Fort William','ES001435'
EXEC dbo.INFAddImportantPlace 85,'City','Gatwick Airport','N0061431'
EXEC dbo.INFAddImportantPlace 87,'City','Glasgow','ES003921'
EXEC dbo.INFAddImportantPlace 89,'City','Gloucester','E0055873'
EXEC dbo.INFAddImportantPlace 93,'City','Great Yarmouth','N0060683'
EXEC dbo.INFAddImportantPlace 95,'City','Grimsby','N0073052'
EXEC dbo.INFAddImportantPlace 96,'City','Guildford','E0056720'
EXEC dbo.INFAddImportantPlace 98,'City','Halifax','E0032788'
EXEC dbo.INFAddImportantPlace 101,'City','Harrogate','E0056344'
EXEC dbo.INFAddImportantPlace 102,'City','Hartlepool','E0054977'
EXEC dbo.INFAddImportantPlace 103,'City','Harwich','E0055830'
EXEC dbo.INFAddImportantPlace 104,'City','Hastings','E0057381'
EXEC dbo.INFAddImportantPlace 107,'City','Hereford','E0054981'
EXEC dbo.INFAddImportantPlace 109,'City','Holyhead','E0054333'
EXEC dbo.INFAddImportantPlace 110,'City','Huddersfield','E0032934'
EXEC dbo.INFAddImportantPlace 112,'City','Inverness','ES001879'
EXEC dbo.INFAddImportantPlace 113,'City','Ipswich','E0057632'
EXEC dbo.INFAddImportantPlace 115,'City','Kettering','E0057587'
EXEC dbo.INFAddImportantPlace 118,'City','Kings Lynn','E0017902'
EXEC dbo.INFAddImportantPlace 119,'City','Kingston-upon-Hull','E0055009'
EXEC dbo.INFAddImportantPlace 122,'City','Kyle of Lochalsh','ES002246'
EXEC dbo.INFAddImportantPlace 123,'City','Lancaster','E0057514'
EXEC dbo.INFAddImportantPlace 124,'City','Leamington Spa','N0072514'
EXEC dbo.INFAddImportantPlace 125,'City','Leeds','E0057974'
EXEC dbo.INFAddImportantPlace 126,'City','Leicester','E0057189'
EXEC dbo.INFAddImportantPlace 129,'City','Lichfield','E0051152'
EXEC dbo.INFAddImportantPlace 130,'City','Lincoln','E0057558'
EXEC dbo.INFAddImportantPlace 131,'City','Liverpool','E0057850'
EXEC dbo.INFAddImportantPlace 133,'City','Llandudno','E0054169'
EXEC dbo.INFAddImportantPlace 134,'City','London','N0060403'
EXEC dbo.INFAddImportantPlace 137,'City','Lowestoft','E0025243'
EXEC dbo.INFAddImportantPlace 138,'City','Luton','E0057190'
EXEC dbo.INFAddImportantPlace 140,'City','Maidstone','E0056051'
EXEC dbo.INFAddImportantPlace 141,'City','Manchester','E0057786'
EXEC dbo.INFAddImportantPlace 144,'City','Middlesbrough','E0039258'
EXEC dbo.INFAddImportantPlace 145,'City','Milton Keynes','E0053461'
EXEC dbo.INFAddImportantPlace 146,'City','Motherwell','ES002695'
EXEC dbo.INFAddImportantPlace 147,'City','Newark (Notts)','E0050159'
EXEC dbo.INFAddImportantPlace 148,'City','Newcastle-upon-Tyne','E0057900'
EXEC dbo.INFAddImportantPlace 149,'City','Newport','E0039779'
EXEC dbo.INFAddImportantPlace 150,'City','Newquay','E0044645'
EXEC dbo.INFAddImportantPlace 152,'City','Northampton','E0057589'
EXEC dbo.INFAddImportantPlace 153,'City','Norwich','E0057571'
EXEC dbo.INFAddImportantPlace 154,'City','Nottingham','E0057221'
EXEC dbo.INFAddImportantPlace 155,'City','Oban','ES002899'
EXEC dbo.INFAddImportantPlace 158,'City','Oxford','E0056504'
EXEC dbo.INFAddImportantPlace 160,'City','Pembroke','E0055088'
EXEC dbo.INFAddImportantPlace 161,'City','Penzance','E0044623'
EXEC dbo.INFAddImportantPlace 162,'City','Perth','ES003005'
EXEC dbo.INFAddImportantPlace 163,'City','Peterborough','E0055098'
EXEC dbo.INFAddImportantPlace 164,'City','Plymouth','N0073197'
EXEC dbo.INFAddImportantPlace 167,'City','Portsmouth','E0057225'
EXEC dbo.INFAddImportantPlace 168,'City','Preston','E0057520'
EXEC dbo.INFAddImportantPlace 169,'City','Reading','N0071726'
EXEC dbo.INFAddImportantPlace 174,'City','Ripon','E0056347'
EXEC dbo.INFAddImportantPlace 177,'City','Rotherham','E0057882'
EXEC dbo.INFAddImportantPlace 178,'City','Royal Leamington Spa','N0072514'
EXEC dbo.INFAddImportantPlace 179,'City','Royal Tunbridge Wells','E0056103'
EXEC dbo.INFAddImportantPlace 183,'City','Salisbury','E0056881'
EXEC dbo.INFAddImportantPlace 184,'City','Scarborough','E0057580'
EXEC dbo.INFAddImportantPlace 185,'City','Scunthorpe','E0055067'
EXEC dbo.INFAddImportantPlace 188,'City','Sheffield','E0057890'
EXEC dbo.INFAddImportantPlace 189,'City','Shrewsbury','E0021804'
EXEC dbo.INFAddImportantPlace 190,'City','Skegness','E0057556'
EXEC dbo.INFAddImportantPlace 192,'City','Southampton','E0057247'
EXEC dbo.INFAddImportantPlace 193,'City','Southend-on-Sea','E0057248'
EXEC dbo.INFAddImportantPlace 194,'City','Southport','E0029882'
EXEC dbo.INFAddImportantPlace 195,'City','St Albans','E0014212'
EXEC dbo.INFAddImportantPlace 196,'City','St Davids','E0055091'
EXEC dbo.INFAddImportantPlace 199,'City','Stafford','E0056658'
EXEC dbo.INFAddImportantPlace 202,'City','Stevenage','E0056006'
EXEC dbo.INFAddImportantPlace 203,'City','Stirling','ES003486'
EXEC dbo.INFAddImportantPlace 204,'City','Stockport','E0057819'
EXEC dbo.INFAddImportantPlace 205,'City','Stockton-on-Tees','E0042097'
EXEC dbo.INFAddImportantPlace 206,'City','Stoke-on-Trent','N0073188'
EXEC dbo.INFAddImportantPlace 209,'City','Stranraer','ES003513'
EXEC dbo.INFAddImportantPlace 211,'City','Stratford-upon-Avon','E0051992'
EXEC dbo.INFAddImportantPlace 212,'City','Sunderland','E0057917'
EXEC dbo.INFAddImportantPlace 215,'City','Swansea','E0042411'
EXEC dbo.INFAddImportantPlace 216,'City','Swindon','E0042483'
EXEC dbo.INFAddImportantPlace 218,'City','Taunton','E0023200'
EXEC dbo.INFAddImportantPlace 219,'City','Torquay','E0042739'
EXEC dbo.INFAddImportantPlace 220,'City','Truro','E0044512'
EXEC dbo.INFAddImportantPlace 221,'City','Wakefield','E0057988'
EXEC dbo.INFAddImportantPlace 224,'City','Warrington','E0057278'
EXEC dbo.INFAddImportantPlace 226,'City','Watford','E0057472'
EXEC dbo.INFAddImportantPlace 227,'City','Wells','E0050824'
EXEC dbo.INFAddImportantPlace 229,'City','Weston-super-Mare','E0040103'
EXEC dbo.INFAddImportantPlace 230,'City','Weymouth','E0009884'
EXEC dbo.INFAddImportantPlace 232,'City','Wigan','E0057846'
EXEC dbo.INFAddImportantPlace 233,'City','Winchester','E0055970'
EXEC dbo.INFAddImportantPlace 238,'City','Wolverhampton','E0057945'
EXEC dbo.INFAddImportantPlace 240,'City','Worcester','E0056907'
EXEC dbo.INFAddImportantPlace 244,'City','York','E0055251'
EXEC dbo.INFAddImportantPlace 245,'City','Salford','N0076110'
EXEC dbo.INFAddImportantPlace 246,'City','Hull','E0055009'
EXEC dbo.INFAddImportantPlace 247,'City','Tunbridge Wells','E0056103'
EXEC dbo.INFAddImportantPlace 248,'City','Banbury','E0020676'
EXEC dbo.INFAddImportantPlace 250,'City','Frome','E0050788'
EXEC dbo.INFAddImportantPlace 251,'City','Grantham','E0017367'
EXEC dbo.INFAddImportantPlace 256,'City','Telford','E0055178'
EXEC dbo.INFAddImportantPlace 258,'City','Yeovil','E0051002'
EXEC dbo.INFAddImportantPlace 259,'City','Birmingham International & NEC','N0078076'
EXEC dbo.INFAddImportantPlace 261,'City','Heathrow Airport','E0034495'
EXEC dbo.INFAddImportantPlace 262,'City','Luton Airport','N0060524'
EXEC dbo.INFAddImportantPlace 263,'City','Manchester Airport','N0075057'
EXEC dbo.INFAddImportantPlace 264,'City','Stansted Airport','N0071618'
EXEC dbo.INFAddImportantPlace 265,'City','Westminster','E0034961'
EXEC dbo.INFAddImportantPlace 266,'City','Rochester','N0072095'




-- **************************************************
-- ** Clear car coordinates table as every data feed 
-- ** is a complete list
-- **************************************************
TRUNCATE TABLE dbo.ImportantPlaceLocationRelationship



EXEC dbo.INFAddImportantPlaceLocationRelationship '1','car','394160','805870','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '2','car','258495','281690','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '8','car','600829','142531','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '11','car','257600','372063','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '12','car','434350','406450','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '13','car','255933','133226','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '16','car','463795','151877','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '17','car','375105','164383','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '18','car','504704','249752','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '19','car','399645','653095','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '21','car','406900','287060','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '22','car','368160','428280','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '23','car','330932','436352','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '25','car','371550','409265','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '26','car','408603','91213','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '28','car','416350','433225','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '30','car','531123','104093','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '31','car','358640','173086','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '33','car','384000','432395','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '35','car','585310','264188','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '37','car','545209','258271','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '38','car','614866','158004','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '39','car','318435','176430','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '40','car','340240','555880','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '41','car','241330','219950','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '42','car','575726','168036','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '43','car','570330','207140','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '46','car','340782','366362','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '47','car','438350','370969','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '48','car','485897','104848','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '50','car','599950','225170','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '51','car','433345','278752','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '52','car','526915','136540','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '53','car','370527','355721','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '56','car','429199','514426','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '58','car','435498','336162','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '59','car','424588','421718','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '60','car','457615','403238','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '61','car','368952','90693','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '62','car','631720','141700','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '63','car','297200','576200','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '64','car','340546','730545','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '66','car','427400','542503','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '71','car','325384','673872','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '72','car','554203','280390','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '74','car','292308','92903','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '81','car','195772','237000','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '82','car','622673','135983','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '83','car','210400','774145','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '85','car','528767','141324','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '87','car','259008','665305','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '89','car','383700','218500','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '93','car','652595','307614','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '95','car','526890','409270','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '96','car','499700','149580','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '98','car','409227','425346','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '101','car','430435','455070','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '102','car','450790','532670','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '103','car','626080','232408','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '104','car','581550','109410','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '107','car','351085','240070','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '109','car','224680','382700','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '110','car','414365','416970','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '112','car','266748','845386','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '113','car','616357','244628','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '115','car','486800','278580','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '118','car','562060','320115','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '119','car','509265','428832','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '122','car','176140','827377','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '123','car','347650','461584','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '124','car','432070','265231','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '125','car','430163','433593','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '126','car','458856','304893','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '129','car','411915','309318','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '130','car','497703','370974','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '131','car','334500','390500','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '133','car','278580','382120','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '134','car','530062','180514','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '137','car','654870','292982','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '138','car','509200','221600','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '140','car','575946','155848','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '141','car','384244','398379','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '144','car','449560','520382','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '145','car','485183','238755','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '146','car','275200','656900','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '147','car','479873','353879','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '148','car','424775','564450','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '149','car','330475','188170','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '150','car','181695','61875','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '152','car','475526','260653','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '153','car','623275','308585','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '154','car','457252','339766','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '155','car','186000','729900','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '158','car','451220','206535','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '160','car','198695','201337','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '161','car','147125','30545','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '162','car','311740','723470','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '163','car','518844','298755','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '164','car','247616','54692','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '167','car','464290','100300','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '168','car','354137','429358','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '169','car','471457','173460','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '174','car','431155','471140','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '177','car','442840','392785','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '178','car','432070','265231','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '179','car','558405','139075','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '183','car','414441','129929','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '184','car','504340','488445','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '185','car','489890','411320','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '188','car','435330','387238','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '189','car','349290','312660','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '190','car','556359','363268','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '192','car','441985','111777','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '193','car','588320','185322','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '194','car','333596','417244','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '195','car','514756','207288','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '196','car','175305','225275','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '199','car','392253','323079','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '202','car','523726','224020','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '203','car','279680','693500','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '204','car','389310','390250','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '205','car','444450','519020','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '206','car','387657','345220','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '209','car','206000','560600','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '211','car','420103','254865','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '212','car','439738','557020','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '215','car','265417','192987','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '216','car','415103','184936','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '218','car','322730','124513','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '219','car','291728','63945','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '220','car','182418','45051','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '221','car','433267','420812','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '224','car','360420','388300','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '226','car','511057','196363','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '227','car','354762','145615','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '229','car','332075','161225','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '230','car','368020','79170','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '232','car','358218','405651','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '233','car','448225','129490','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '238','car','391766','298636','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '240','car','384970','255075','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '244','car','460100','452260','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '245','car','380935','399000','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '246','car','509265','428832','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '247','car','558405','139075','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '248','car','445810','240610','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '250','car','377605','147733','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '251','car','491490','335620','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '256','car','369874','308655','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '258','car','355685','115985','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '259','car','418950','283750','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '261','car','507500','175700','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '262','car','511800','221350','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '263','car','381960','385435','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '264','car','555820','223640','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '265','car','530059','179567','0','2400','MTWTFSS',NULL
EXEC dbo.INFAddImportantPlaceLocationRelationship '266','car','574386','168428','0','2400','MTWTFSS',NULL



-- **************************************************
-- ** Clear the Naptans table as every data feed 
-- ** is a complete list
-- **************************************************
TRUNCATE TABLE dbo.NaptanTimeRelationship



EXEC INFAddNaptanTimeRelationship '1','900090017','coach','394196','805940','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '1','9100ABRDEEN','rail','394127','805872','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '1','9200ABZ','air','387850','812250','Y','1','1','rail'
EXEC INFAddNaptanTimeRelationship '2','900012183','coach','258546','281639','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '2','9100ABRYSTH','rail','258500','281600','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '8','900058016','coach','601188','142875','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '8','9100ASHFKY','rail','601282','142204','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '248','900048023','coach','445928','240639','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '248','9100BNBR','rail','446160','240427','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '248','9200BHX','air','418360','283939','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '248','9200LTN','air','511800','221360','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '11','900011621','coach','258184','372183','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '11','9100BANGOR','rail','257500','371600','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '11','9200VLY','air','231055','376075','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '12','900025212','coach','434695','406463','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '12','9100BNSLY','rail','434740','406569','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '12','9200LBA','air','422450','441450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '13','900042025','coach','256031','133075','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '13','9100BRNSTPL','rail','255561','132541','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '16','900062036','coach','463899','152329','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '16','9100BSNGSTK','rail','463774','152529','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '16','9200LGW','air','528575','141320','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '16','9200LHR','air','507546','176188','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '16','9200SOU','air','445050','116950','Y','0','1','rail'
EXEC INFAddNaptanTimeRelationship '17','900087025','coach','375076','164376','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '17','9100BATHSPA','rail','375248','164353','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '17','9200BRS','air','350626','165532','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '18','900052444','coach','504685','249860','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '18','9100BEDFDM','rail','504167','249744','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '18','9200LHR','air','507546','176188','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '18','9200LTN','air','511800','221360','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '18','9200STN','air','555650','223750','Y','0','1','coach'
EXEC INFAddNaptanTimeRelationship '19','900069032','coach','399717','652953','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '19','9100BRWCKUT','rail','399400','653500','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '21','900033023','coach','407636','286311','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '21','900098018','coach','407375','287100','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '21','9100BHAMMRS','rail','407400','286800','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '21','9100BHAMNWS','rail','407000','286655','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '21','9100BHAMSNH','rail','406950','287275','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '21','9200BHX','air','418360','283939','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '21','9200EMA','air','445255','325694','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '259','900033343','coach','418312','283929','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '259','9100BHAMINT','rail','418728','283685','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '259','9200BHX','air','418360','283939','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '22','900073021','coach','368400','427958','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '22','9100BLKB','rail','368500','427900','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '22','9200MAN','air','381950','384950','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '23','900077131','coach','330975','436500','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '23','900077134','coach','330926','434949','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '23','9100BLCKPLN','rail','331024','436693','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '23','9100BLCKS','rail','331008','434110','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '25','900021041','coach','371395','409054','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '25','9100BOLTON','rail','371900','408700','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '25','9200MAN','air','381950','384950','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '26','900065026','coach','409624','91955','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '26','9100BOMO','rail','409700','92000','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '26','9200BOH','air','411850','97550','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '28','900076122','coach','416544','432796','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '28','9100BRADFS','rail','416370','433430','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '28','9100BRADIN','rail','416594','432780','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '28','9200LBA','air','422450','441450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '30','900059076','coach','531267','103899','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '30','9100BRGHTN','rail','531000','104920','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '30','9200LGW','air','528575','141320','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '31','900041065','coach','358920','173520','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '31','9100BRSTLTM','rail','359700','172400','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '31','9200BRS','air','350626','165532','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '33','900073141','coach','384276','432472','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '33','9100BURNLYC','rail','383960','433050','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '33','9100BURNMR','rail','383700','432100','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '33','9200LBA','air','422450','441450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '33','9200MAN','air','381950','384950','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '35','900053654','coach','585175','264313','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '35','9100BSTEDMS','rail','585300','265200','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '35','9200NWI','air','621750','313050','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '35','9200STN','air','555650','223750','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '37','900052074','coach','545612','258254','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '37','9100CAMBDGE','rail','546200','257300','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '37','9200LTN','air','511800','221360','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '37','9200STN','air','555650','223750','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '38','900058056','coach','615027','157540','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '38','9100CNTBE','rail','614645','157285','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '38','9100CNTBW','rail','614552','158395','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '39','900015035','coach','318125','175968','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '39','900098020','coach','318031','176467','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '39','9100CRDFCEN','rail','318194','175889','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '39','9200CWL','air','306950','167450','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '40','900068051','coach','340296','555906','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '40','9100CARLILE','rail','340200','555500','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '41','900014035','coach','241105','219931','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '41','9100CMTHN','rail','241280','219710','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '42','900058766','coach','579343','163347','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '42','9100CHATHAM','rail','575547','167608','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '42','9200LCY','air','542363','180377','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '42','9200LGW','air','528575','141320','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '43','900054014','coach','570455','207042','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '43','9100CHLMSFD','rail','570552','207068','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '43','9200LCY','air','542363','180377','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '43','9200LTN','air','511800','221360','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '43','9200STN','air','555650','223750','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '46','900011081','coach','340916','366181','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '46','9100CHST','rail','341335','366985','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '46','9200LPL','air','343157','382650','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '46','9200MAN','air','381950','384950','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '47','900028034','coach','438206','370980','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '47','9100CHFD','rail','438800','371400','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '47','9200EMA','air','445255','325694','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '47','9200MAN','air','381950','384950','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '48','900063456','coach','485992','104316','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '48','9100CHCHSTR','rail','485890','104320','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '48','9200LGW','air','528575','141320','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '48','9200SOU','air','445050','116950','Y','1','1','rail'
EXEC INFAddNaptanTimeRelationship '50','900053074','coach','599954','225162','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '50','9100CLCHSTR','rail','599100','226417','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '50','9200LCY','air','542363','180377','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '50','9200STN','air','555650','223750','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '51','900034013','coach','433630','279250','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '51','900098178','coach','430440','276770','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '51','9100COVNTRY','rail','433200','278200','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '51','9200BHX','air','418360','283939','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '51','9200EMA','air','445255','325694','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '52','900063066','coach','526940','136498','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '52','9100CRAWLEY','rail','527029','136332','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '52','9200LGW','air','528575','141320','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '53','900020591','coach','370426','355738','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '53','9100CREWE','rail','371100','354800','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '53','9200LPL','air','343157','382650','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '53','9200MAN','air','381950','384950','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '56','900070142','coach','428991','514382','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '56','9100DLTN','rail','429400','514000','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '56','9200MME','air','436800','513000','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '58','900027044','coach','435588','336170','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '58','9100DRBY','rail','436201','335550','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '58','9200BHX','air','418360','283939','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '58','9200EMA','air','445255','325694','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '59','900022222','coach','424433','421543','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '59','9100DWBY','rail','424325','421805','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '59','9200LBA','air','422450','441450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '59','9200MAN','air','381950','384950','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '60','900081012','coach','457185','403417','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '60','9100DONC','rail','457100','403100','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '60','9200HUY','air','509420','410880','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '60','9200LBA','air','422450','441450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '61','900045395','coach','369201','90041','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '61','9100DRCHS','rail','369223','90053','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '61','9100DRCHW','rail','368850','90240','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '61','9200BOH','air','411850','97550','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '61','9200SOU','air','445050','116950','Y','0','1','rail'
EXEC INFAddNaptanTimeRelationship '62','900058136','coach','632026','141747','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '62','9100DOVERP','rail','631380','141464','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '63','900067407','coach','296956','576066','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '63','9100DUMFRES','rail','297660','576520','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '64','900090097','coach','340591','730559','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '64','9100DUNDETB','rail','340242','729793','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '64','9200DND','air','337500','729555','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '64','9200EDI','air','314910','673671','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '66','900070022','coach','426972','542569','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '66','9100DRHM','rail','427000','542800','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '66','9200MME','air','436800','513000','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '66','9200NCL','air','418650','571450','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '71','900066157','coach','325705','674233','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '71','9100EDINBUR','rail','325900','673900','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '71','9200EDI','air','314910','673671','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '72','900052074','coach','545612','258254','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '72','9100ELYY','rail','554350','279440','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '72','9200STN','air','555650','223750','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '74','900044035','coach','292502','92896','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '74','9100EXETERC','rail','291875','92995','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '74','9100EXETRSD','rail','291175','93320','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '74','9200EXT','air','300120','93340','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '81','900014705','coach','195111','238771','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '81','9100FGDHBR','rail','195200','239000','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '82','900058886','coach','622606','135930','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '82','9100FLKSTNC','rail','622084','136286','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '82','9200MSE','air','633950','166150','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '83','900067387','coach','210558','774236','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '83','9100FRTWLM','rail','210531','774186','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '250','900087355','coach','377682','148085','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '250','9100FROME','rail','378450','147610','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '250','9200BRS','air','350626','165532','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '85','900063057','coach','527576','141785','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '85','900063086','coach','528598','141332','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '85','9100GTWK','rail','528700','141300','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '85','9200LGW','air','528575','141320','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '87','900067157','coach','259188','665834','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '87','9100GLGC','rail','258794','665257','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '87','9100GLGCLL','rail','258771','665344','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '87','9100GLGQHL','rail','259190','665515','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '87','9100GLGQLL','rail','259241','665531','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '87','9200GLA','air','247868','666178','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '87','9200PIK','air','235210','627005','Y','0','1','rail'
EXEC INFAddNaptanTimeRelationship '89','900017263','coach','383498','218573','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '89','9100GLOSTER','rail','383664','218564','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '251','900031074','coach','491432','335543','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '251','9100GTHM','rail','491400','335200','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '251','9200EMA','air','445255','325694','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '93','900038174','coach','652601','307605','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '93','9100YARMTH','rail','652000','308100','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '93','9200NWI','air','621750','313050','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '95','900030434','coach','526984','409463','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '95','9100GRMSBYT','rail','526800','409200','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '95','9200HUY','air','509420','410880','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '96','900060176','coach','497564','149921','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '96','9100GUILDFD','rail','499200','149600','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '96','9200LGW','air','528575','141320','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '96','9200LHR','air','507546','176188','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '98','900022032','coach','409430','425406','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '98','9100HLFX','rail','409760','424960','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '98','9200LBA','air','422450','441450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '98','9200MAN','air','381950','384950','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '101','900074132','coach','430362','455444','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '101','9100HAGT','rail','430413','455336','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '101','9200LBA','air','422450','441450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '102','900071232','coach','450961','532427','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '102','9100HRTLEPL','rail','451200','532700','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '102','9200MME','air','436800','513000','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '103','900053074','coach','599954','225162','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '103','9100HARWICH','rail','625987','232397','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '104','900059766','coach','581648','109418','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '104','9100HASTING','rail','581431','109616','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '104','9200LGW','air','528575','141320','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '261','900057286','coach','507542','175857','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '261','9100HTRWAPT','rail','507453','175848','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '261','9100HTRWTE4','rail','507993','174516','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '261','9100HTRWTM4','rail','508100','174400','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '261','9200LHR','air','507546','176188','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '107','900016013','coach','351426','240285','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '107','9100HEREFRD','rail','351543','240546','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '109','900011171','coach','224954','382255','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '109','9100HLYH','rail','224800','382200','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '109','9200VLY','air','231055','376075','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '110','900022152','coach','414196','416624','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '110','9100HDRSFLD','rail','414330','416910','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '110','9200LBA','air','422450','441450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '110','9200MAN','air','381950','384950','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '246','900082032','coach','509108','428927','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '246','9100HULL','rail','509200','428800','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '246','9200HUY','air','509420','410880','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '112','900090147','coach','266671','845617','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '112','9100IVRNESS','rail','266788','845473','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '112','9200INV','air','276567','851824','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '113','900053234','coach','616379','244431','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '113','9100IPSWICH','rail','615700','243800','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '113','9200NWI','air','621750','313050','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '113','9200STN','air','555650','223750','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '115','900039364','coach','486661','278385','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '115','9100KETR','rail','486411','278038','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '115','9200EMA','air','445255','325694','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '115','9200LTN','air','511800','221360','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '118','900037084','coach','562031','320081','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '118','9100KLYNN','rail','562278','320064','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '119','900082032','coach','509108','428927','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '119','9100HULL','rail','509200','428800','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '119','9200HUY','air','509420','410880','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '119','9200LBA','air','422450','441450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '122','900093077','coach','176257','827201','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '122','9100KYLELSH','rail','176247','827123','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '123','900072111','coach','347674','461898','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '123','9100LANCSTR','rail','347200','461700','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '124','900040073','coach','432254','266025','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '124','9100LMNGTNS','rail','431735','265251','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '124','9200BHX','air','418360','283939','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '124','9200EMA','air','445255','325694','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '125','900076052','coach','430682','433483','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '125','9100LEEDS','rail','429870','433352','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '125','9200LBA','air','422450','441450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '126','900035064','coach','458681','304984','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '126','9100LESTER','rail','459300','304100','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '126','9200BHX','air','418360','283939','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '126','9200EMA','air','445255','325694','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '129','900033483','coach','411915','309318','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '129','9100LCHC','rail','411900','309175','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '129','9100LCHTTVH','rail','413600','309930','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '129','9100LCHTTVL','rail','413601','309930','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '129','9200BHX','air','418360','283939','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '129','9200EMA','air','445255','325694','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '130','900030044','coach','497686','370963','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '130','9100LINCLNC','rail','497571','370888','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '130','9200EMA','air','445255','325694','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '130','9200HUY','air','509420','410880','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '131','900010091','coach','335259','390823','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '131','9100LVRPLCL','rail','335001','390200','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '131','9100LVRPLSH','rail','335100','390500','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '131','9100LVRPLSL','rail','335100','390600','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '131','9200LPL','air','343157','382650','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '133','900011241','coach','278794','382100','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '133','9100LUDO','rail','278400','382000','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '133','9200VLY','air','231055','376075','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','900057366','coach','528542','178760','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','900057888','coach','528760','178876','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9100BLFR','rail','531714','180914','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9100CANONST','rail','532620','180890','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9100CHRX','rail','530235','180455','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9100EUSTON','rail','529545','182675','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9100FENCHRS','rail','533410','180940','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9100KNGX','rail','530300','183000','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9100LIVST','rail','533216','181641','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9100LNDNBDC','rail','532930','180190','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9100MARYLBN','rail','527550','182000','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9100MRGT','rail','532693','181683','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9100PADTON','rail','526600','181300','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9100STPX','rail','530000','183160','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9100VICTRIC','rail','528900','179000','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9100WATRLMN','rail','531060','179950','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9200LCY','air','542363','180377','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '134','9200LGW','air','528575','141320','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '134','9200LHR','air','507546','176188','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '134','9200LTN','air','511800','221360','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '134','9200STN','air','555650','223750','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '137','900086134','coach','654904','293245','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '137','9100LOWSTFT','rail','654747','292890','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '137','9200NWI','air','621750','313050','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '137','9200STN','air','555650','223750','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '138','900051114','coach','509271','221487','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '138','9100LUTON','rail','509259','221604','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '138','9200LHR','air','507546','176188','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '138','9200LTN','air','511800','221360','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '138','9200STN','air','555650','223750','Y','0','1',coach
EXEC INFAddNaptanTimeRelationship '262','900051224','coach','511865','221333','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '262','9100LUTOAPY','rail','510533','220534','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '262','9200LTN','air','511800','221360','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '140','900058706','coach','576339','155626','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '140','9100MSTONEE','rail','575949','156206','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '140','9100MSTONEW','rail','575592','155374','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '140','9200LCY','air','542363','180377','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '140','9200LGW','air','528575','141320','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '141','900021131','coach','384370','397963','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '141','9100MNCROXR','rail','384035','397505','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '141','9100MNCRPIC','rail','384772','397873','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '141','9100MNCRVIC','rail','384000','399000','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '141','9200MAN','air','381950','384950','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '263','900021251','coach','381929','385252','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '263','9100MNCRIAP','rail','381932','385387','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '263','9200MAN','air','381950','384950','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '144','900071062','coach','449251','520360','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '144','9100MDLSBRO','rail','449564','520702','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '144','9200MME','air','436800','513000','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '145','900050124','coach','486493','239855','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '145','900098026','coach','484224','238082','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '145','900098027','coach','485606','239055','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '145','9100MKNSCEN','rail','484189','238029','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '145','9200LHR','air','507546','176188','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '145','9200LTN','air','511800','221360','Y','1','1',coach
EXEC INFAddNaptanTimeRelationship '145','9200STN','air','555650','223750','Y','0','1',coach
EXEC INFAddNaptanTimeRelationship '146','900067188','coach','272218','655266','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '146','9100MOTHRWL','rail','275054','657175','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '146','9200EDI','air','314910','673671','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '146','9200GLA','air','247868','666178','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '147','900083024','coach','479720','354139','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '147','9100NEWANG','rail','480470','354524','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '147','9100NWRKCAS','rail','479600','354300','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '148','900069092','coach','424252','563737','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '148','900098017','coach','424572','563888','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '148','9100NWCSTLE','rail','424600','563820','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '148','9200NCL','air','418650','571450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '149','900015255','coach','331231','188077','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '149','900015440','coach','329478','195549','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '149','9100NWPTRTG','rail','330914','188350','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '149','9200BRS','air','350626','165532','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '149','9200CWL','air','306950','167450','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '150','900088035','coach','180850','61570','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '150','9100NEWQUAY','rail','181590','61770','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '150','9200NQY','air','186735','65143','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '152','900039074','coach','475442','260758','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '152','9100NMPTN','rail','474759','260486','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '152','9200BHX','air','418360','283939','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '152','9200LTN','air','511800','221360','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '153','900038334','coach','622980','308000','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '153','9100NRCH','rail','623900','308400','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '153','9200NWI','air','621750','313050','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '154','900029044','coach','457435','339427','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '154','900098177','coach','457290','340527','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '154','9100NTNG','rail','457455','339195','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '154','9200EMA','air','445255','325694','Y','1','1',coach
EXEC INFAddNaptanTimeRelationship '155','900067987','coach','185841','729872','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '155','9100OBAN','rail','185795','729888','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '158','900049073','coach','451003','206380','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '158','9100OXFD','rail','450477','206341','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '160','900014665','coach','196891','203414','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '160','9100PEMBROK','rail','199160','201130','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '161','900043015','coach','147668','30577','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '161','9100PENZNCE','rail','147600','30600','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '161','9200NQY','air','186735','65143','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '161','9200PZE','air','148650','31350','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '162','900090337','coach','311347','723209','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '162','900098007','coach','308728','722592','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '162','9100PERTH','rail','311220','723133','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '162','9200DND','air','337500','729555','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '162','9200EDI','air','314910','673671','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '163','900036114','coach','518903','298849','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '163','9100PBRO','rail','518700','298900','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '164','900043345','coach','248176','54559','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '164','9100PLYMTH','rail','247700','55300','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '164','9200PLH','air','250206','60194','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '167','900064186','coach','463038','100170','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '167','9100PSEA','rail','464164','100275','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '167','9200SOU','air','445050','116950','Y','1','1','rail'
EXEC INFAddNaptanTimeRelationship '168','900077061','coach','354182','429638','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '168','9100PRST','rail','353409','429177','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '168','9200BLK','air','331450','431800','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '168','9200LPL','air','343157','382650','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '168','9200MAN','air','381950','384950','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '169','900061206','coach','465358','171847','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '169','900061266','coach','471455','173792','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '169','9100RDNG4AB','rail','471528','173813','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '169','9100RDNGSTN','rail','471528','173813','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '169','9200LGW','air','528575','141320','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '169','9200LHR','air','507546','176188','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '174','900074262','coach','431338','471301','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '174','9100HAGT','rail','430413','455336','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '174','9100NLRTN','rail','436430','493200','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '174','9100THIRSK','rail','441000','481570','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '174','9200LBA','air','422450','441450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '177','900025062','coach','442890','393209','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '177','9100ROTHCEN','rail','442590','393020','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '177','9200LBA','air','422450','441450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '177','9200MAN','air','381950','384950','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '178','900040073','coach','432254','266025','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '178','9100LMNGTNS','rail','431735','265251','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '178','9200BHX','air','418360','283939','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '178','9200EMA','air','445255','325694','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '179','900058586','coach','558476','139549','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '179','9100TUNWELL','rail','558428','139207','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '179','9200LGW','air','528575','141320','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '245','900021131','coach','384370','397963','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '245','9100SLFDCT','rail','381800','398910','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '245','9100SLFDORD','rail','383186','398515','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '245','9200MAN','air','381950','384950','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '183','900089055','coach','414541','130135','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '183','9100SLSBRY','rail','413662','130153','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '183','9200BOH','air','411850','97550','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '183','9200SOU','air','445050','116950','Y','1','1','rail'
EXEC INFAddNaptanTimeRelationship '184','900075102','coach','503968','488325','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '184','9100SCARBRO','rail','503900','488300','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '185','900024022','coach','489840','411330','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '185','9100SCNTHRP','rail','489400','410800','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '185','9200HUY','air','509420','410880','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '185','9200LBA','air','422450','441450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '188','900025072','coach','435841','387202','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '188','9100SHEFFLD','rail','435880','386953','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '188','9200EMA','air','445255','325694','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '188','9200LBA','air','422450','441450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '189','900012283','coach','349227','312863','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '189','9100SHRWBY','rail','349445','312960','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '190','900031134','coach','556278','363170','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '190','9100SKEGNES','rail','556200','363200','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '192','900065136','coach','441547','112023','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '192','9100SOTON','rail','441327','112161','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '192','9200SOU','air','445050','116950','Y','1','1','rail'
EXEC INFAddNaptanTimeRelationship '193','900055084','coach','588391','185384','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '193','9100STHCENT','rail','588162','185503','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '193','9100STHVIC','rail','588128','185997','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '193','9200LCY','air','542363','180377','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '193','9200STN','air','555650','223750','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '194','900010461','coach','333514','417201','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '194','9100SOUTHPT','rail','333832','417135','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '194','9200LPL','air','343157','382650','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '194','9200MAN','air','381950','384950','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '195','900056086','coach','514859','207399','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '195','9100STALBCY','rail','515549','207076','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '195','9100STALBNA','rail','514526','206414','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '195','9200LHR','air','507546','176188','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '195','9200LTN','air','511800','221360','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '195','9200STN','air','555650','223750','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '196','900014765','coach','195484','215960','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '196','9100HAVRFDW','rail','196000','215700','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '199','900020233','coach','391880','322946','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '199','9100STAFFRD','rail','391870','322933','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '199','9200BHX','air','418360','283939','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '199','9200EMA','air','445255','325694','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '199','9200MAN','air','381950','384950','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '264','900054174','coach','555851','223680','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '264','9100STANAIR','rail','555683','223530','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '264','9200STN','air','555650','223750','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '202','900051055','coach','523743','224096','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '202','9100STEVNGE','rail','523448','224090','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '202','9200LTN','air','511800','221360','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '202','9200STN','air','555650','223750','Y','0','1',coach
EXEC INFAddNaptanTimeRelationship '203','900090357','coach','279834','693387','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '203','9100STIRLNG','rail','279757','693585','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '203','9200EDI','air','314910','673671','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '204','900021110','coach','389270','390230','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '204','9100STKP','rail','389260','389870','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '204','9200MAN','air','381950','384950','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '205','900071002','coach','444586','518490','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '205','9100STOCTON','rail','444156','519591','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '205','9200MME','air','436800','513000','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '206','900020141','coach','388476','347442','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '206','9100STOKEOT','rail','387953','345645','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '206','9200BHX','air','418360','283939','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '206','9200MAN','air','381950','384950','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '209','900067297','coach','206320','560955','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '209','9100STRNRR','rail','206200','561400','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '209','9200GLA','air','247868','666178','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '209','9200PIK','air','235210','627005','Y','1','1','rail'
EXEC INFAddNaptanTimeRelationship '211','900084053','coach','420467','255318','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '211','9100SOAV','rail','419490','255150','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '211','9200BHX','air','418360','283939','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '212','900071112','coach','439583','556578','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '212','9100SNDRLND','rail','439705','556910','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '212','9200NCL','air','418650','571450','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '215','900014225','coach','265403','192763','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '215','9100SWANSEA','rail','265700','193600','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '215','9200CWL','air','306950','167450','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '216','900046093','coach','415121','185078','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '216','900098006','coach','418838','186586','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '216','9100SDON','rail','414964','185199','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '216','9200BRS','air','350626','165532','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '216','9200LHR','air','507546','176188','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '216','9200SOU','air','445050','116950','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '218','900042205','coach','322534','124567','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '218','9100TAUNTON','rail','322755','125460','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '218','9200BRS','air','350626','165532','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '218','9200EXT','air','300120','93340','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '256','900019143','coach','369766','308788','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '256','9100TELFRDC','rail','370285','309359','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '256','9200BHX','air','418360','283939','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '219','900044155','coach','291076','65108','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '219','9100TORQUAY','rail','290555','63505','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '219','9200EXT','air','300120','93340','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '219','9200PLH','air','250206','60194','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '220','900043595','coach','182800','44732','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '220','9100TRURO','rail','181667','44924','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '220','9200NQY','air','186735','65143','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '220','9200PZE','air','148650','31350','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '247','900058586','coach','558476','139549','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '247','9100TUNWELL','rail','558428','139207','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '247','9200LGW','air','528575','141320','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '221','900079022','coach','433291','421173','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '221','9100WKFLDKG','rail','433880','420365','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '221','9100WKFLDWG','rail','432785','420690','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '221','9200LBA','air','422450','441450','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '224','900010171','coach','360415','388266','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '224','9100WRGT','rail','360700','388500','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '224','9100WRGTNBQ','rail','360033','387854','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '224','9200LPL','air','343157','382650','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '224','9200MAN','air','381950','384950','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '226','900056166','coach','511020','197273','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '226','9100WATFDJ','rail','511040','197343','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '226','9200LHR','air','507546','176188','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '226','9200LTN','air','511800','221360','Y','0','1','coach'
EXEC INFAddNaptanTimeRelationship '227','900087135','coach','354622','145543','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '227','9100BATHSPA','rail','375248','164353','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '227','9100BRSTLTM','rail','359700','172400','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '227','9100CCARY','rail','363490','133520','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '227','9100WSMARE','rail','332420','161030','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '227','9200BRS','air','350626','165532','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '265','900057366','coach','528542','178760','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','900057888','coach','528760','178876','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9100BLFR','rail','531714','180914','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9100CANONST','rail','532620','180890','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9100CHRX','rail','530235','180455','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9100EUSTON','rail','529545','182675','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9100FENCHRS','rail','533410','180940','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9100KNGX','rail','530300','183000','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9100LIVST','rail','533216','181641','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9100LNDNBDC','rail','532930','180190','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9100MARYLBN','rail','527550','182000','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9100MRGT','rail','532693','181683','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9100PADTON','rail','526600','181300','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9100STPX','rail','530000','183160','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9100VICTRIC','rail','528900','179000','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9100WATRLMN','rail','531060','179950','N','0','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9200LCY','air','542363','180377','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '265','9200LGW','air','528575','141320','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '265','9200LHR','air','507546','176188','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '265','9200LTN','air','511800','221360','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '265','9200STN','air','555650','223750','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '229','900041245','coach','332453','161207','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '229','9100WSMARE','rail','332420','161030','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '229','9200BRS','air','350626','165532','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '230','900045345','coach','368023','79337','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '230','9100WEYMTH','rail','367960','79610','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '230','9200BOH','air','411850','97550','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '232','900010201','coach','358122','405792','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '232','9100WIGANNW','rail','358133','405411','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '232','9100WIGANWL','rail','358140','405540','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '232','9200LPL','air','343157','382650','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '232','9200MAN','air','381950','384950','Y','0','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '233','900065576','coach','448482','129331','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '233','900098010','coach','448869','128340','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '233','9100WNCHSTR','rail','447765','129984','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '233','9200SOU','air','445050','116950','Y','1','1','rail'
EXEC INFAddNaptanTimeRelationship '238','900019063','coach','391819','298676','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '238','9100WVRMPTN','rail','392000','298900','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '238','9200BHX','air','418360','283939','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '240','900018133','coach','384768','255015','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '240','900018150','coach','388935','257107','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '240','9100WORCSFS','rail','384940','255210','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '240','9100WORCSSH','rail','385783','255186','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '240','9200BHX','air','418360','283939','Y','1','1','rail,coach'
EXEC INFAddNaptanTimeRelationship '258','900045155','coach','356027','116048','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '258','9100YOVILJN','rail','357050','114100','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '258','9100YOVILPM','rail','357000','116300','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '258','9200BOH','air','411850','97550','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '258','9200BRS','air','350626','165532','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '258','9200EXT','air','300120','93340','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '244','900074092','coach','459674','451698','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '244','9100YORK','rail','459600','451700','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '206','900098274','coach','387954','345691','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '74','900098236','coach','291187','93342','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '266','9100RCHT','rail','574771','168157','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '266','900058766','coach','579343','163347','Y','0','0',NULL
EXEC INFAddNaptanTimeRelationship '266','9200LCY','air','542363','180377','Y','1','0',NULL
EXEC INFAddNaptanTimeRelationship '266','9200LGW','air','528575','141320','Y','1','0','rail,coach'

GO



----------------
-- Change Log --
----------------

USE PermanentPortal

Declare @@value decimal(7,3)
Select @@value = left(right('$Revision:   1.17  $',8),7)


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 013 and VersionNumber = @@value)
BEGIN
UPDATE dbo.MDSChangeCatalogue
SET
ChangeDate = getdate(),
Summary = 'MDS013_CityToCityTables.sql'
WHERE ScriptNumber = 013 AND VersionNumber = @@value
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
013,
@@value,
'MDS013_CityToCityTables.sql'
)
END
GO