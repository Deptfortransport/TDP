-- ***********************************************
-- AUTHOR      : Phil Scott
-- NAME : MDS0012_UpdateEmissionsFactors.sql
-- DESCRIPTION : MDS0012_UpdateEmissionsFactors
-- SOURCE : Manual Data Service
-- Version: $Revision:   1.3  $
-- Additional Steps Required : IIS Reset Webservers
-- ************************************************
-- ************************************************
--//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS012_UpdateEmissionFactors.sql-arc  $
--
--   Rev 1.3   Feb 18 2011 14:47:32   PScott
--Updated with oct2010 figures supplie by dft
--
--   Rev 1.2   Oct 06 2010 13:26:18   nrankin
--ACP (C3330326)
--BBP (3291186)
--
--Load new CO2 Factors and associated PDF's
--
--   Rev 1.1   Sep 15 2010 13:27:22   nrankin
--Updated C02 Factors from Chris Pie (C3290719)
--
--   Rev 1.0   Sep 08 2008 16:20:44   pscott
--Initial revision.
--
--   Rev 1.0   May 20 2008 08:37:02   Pscott
--Initial revision.
--
--   Rev 1.0   May 20 2008 08:10:24   pscott
--Initial revision.


USE PermanentPortal

update JourneyEmissionsFactor
set FactorValue = 1714
where FactorType = 'AirDefault'

go

update JourneyEmissionsFactor
set FactorValue = 1714
where FactorType = 'AirLarge'

go

update JourneyEmissionsFactor
set FactorValue = 1714
where FactorType = 'AirSmall'

go

update JourneyEmissionsFactor
set FactorValue = 1714
where FactorType = 'AirMedium'

go

update JourneyEmissionsFactor
set FactorValue = 1339
where FactorType = 'BusDefault'

go

update JourneyEmissionsFactor
set FactorValue = 300
where FactorType = 'CoachDefault'

go

update JourneyEmissionsFactor
set FactorValue = 1152
where FactorType = 'FerryDefault'

go

update JourneyEmissionsFactor
set FactorValue = 768
where FactorType = 'LightRailDefault'

go

update JourneyEmissionsFactor
set FactorValue = 792
where FactorType = 'LightRailDL'

go

update JourneyEmissionsFactor
set FactorValue = 471
where FactorType = 'LightRailCR'

go

update JourneyEmissionsFactor
set FactorValue = 741
where FactorType = 'LightRailLU'

go

update JourneyEmissionsFactor
set FactorValue = 425
where FactorType = 'LightRailMA'

go


update JourneyEmissionsFactor
set FactorValue = 1261
where FactorType = 'LightRailTW'

go


update JourneyEmissionsFactor
set FactorValue = 534
where FactorType = 'RailDefault'

go

update properties
set pvalue = '1.09'
where pname = 'JourneyEmissions.AirDistanceFactor'
go

update properties
set pvalue = '71'
where pname = 'JourneyEmissions.Distance.Air.Small'
go

update properties
set pvalue = '480'
where pname = 'JourneyEmissions.Distance.Air.Medium'
go


update properties
set pvalue = '150'
where pname = 'JourneyEmissions.MinDistance.Air'
go

update properties
set pvalue = '1.25'
where pname = 'JourneyEmissions.BusDistanceFactor'
go


update properties
set pvalue = '1.03'
where pname = 'JourneyEmissions.CongestionAndUrbanDrivingFactor'
go


update properties
set pvalue = '5'
where pname = 'CarCosting.MinFuelConsumption'

update properties
set pvalue = '45'
where pname = 'CarCosting.MaxConsumptionLitresPer100Km'

update properties
set pvalue = '150'
where pname = 'CarCosting.MaxConsumptionMilesPerGallon'

update properties
set pvalue = '300'
where pname = 'CarCosting.MaxFuelCost'

update properties
set pvalue = '1'
where pname = 'CarCosting.MinFuelCost'

update properties
set pvalue = '600'
where pname = 'CarCosting.MaxCO2PerKm'


update CarCostFuelFactor
set Factor = 230
where fueltype = 'petrol'

update CarCostFuelFactor
set Factor = 264
where fueltype = 'diesel'

----------------
-- Change Log --
----------------

USE PermanentPortal

Declare @@value decimal(7,3)
Select @@value = left(right('$Revision:   1.3  $',8),7)


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 012 and VersionNumber = @@value)
BEGIN
UPDATE dbo.MDSChangeCatalogue
SET
ChangeDate = getdate(),
Summary = 'MDS0012_UpdateEmissionsFactors.sql'
WHERE ScriptNumber = 012 AND VersionNumber = @@value
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
012,
@@value,
'UMDS0012_UpdateEmissionsFactors.sql'
)
END