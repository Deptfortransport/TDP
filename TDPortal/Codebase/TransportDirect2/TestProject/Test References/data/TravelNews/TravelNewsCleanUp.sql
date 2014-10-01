--TDPTransientPortal Clean up

-- SQL removes all the temporary data from the TransientPortal Travel News tables

USE [TransientPortal]

--DELETE FROM [dbo].[TravelNewsVenue]
--      WHERE [UID] like 'TDP%'

DELETE FROM [dbo].[TravelNewsRegion]
      WHERE [RegionName] like 'TDPTest%'

DELETE FROM [dbo].[TravelNewsDataSource]
      WHERE [DataSourceId] like 'TDPTest%'

DELETE FROM [dbo].[TravelNewsDataSources]
      WHERE [DataSourceId] like 'TDPTest%'

DELETE FROM [dbo].[TravelNews]
      WHERE [HeadlineText] like 'TDPTest%'
	    AND [DetailText] like 'TDPTest%'
