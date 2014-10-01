--ReportStaging Clean up

-- SQL removes all the temporary data from the Report Staging tables

USE [ReportStagingDB]

DELETE FROM [dbo].[CyclePlannerRequestEvent]
      WHERE [SessionId] like 'Test%'

DELETE FROM [dbo].[CyclePlannerResultEvent]
      WHERE [SessionId] like 'Test%'

DELETE FROM [dbo].[DataGatewayEvent]
      WHERE [SessionId] like 'Test%'

DELETE FROM [dbo].[GazetteerEvent]
      WHERE [SessionId] like 'Test%'

-- Can't delete as there is no session id column
--DELETE FROM [dbo].[GISQueryEvent]
      --WHERE [SessionId] like 'Test%'

DELETE FROM [dbo].[JourneyPlanRequestEvent]
      WHERE [SessionId] like 'Test%'

DELETE FROM [dbo].[JourneyPlanResultsEvent]
      WHERE [SessionId] like 'Test%'
      
DELETE FROM [dbo].[JourneyWebRequestEvent]
      WHERE [SessionId] like 'Test%'

DELETE FROM [dbo].[LandingPageEvent]
      WHERE [SessionId] like 'Test%'

DELETE FROM [dbo].[LocationRequestEvent]
      WHERE [JourneyPlanRequestId] like 'Test%'

DELETE FROM [dbo].[InternalRequestEvent]
      WHERE [SessionId] like 'Test%'
      
DELETE FROM [dbo].[OperationalEvent]
      WHERE [SessionId] like 'Test%'
	     OR [Message] like 'Test%'

DELETE FROM [dbo].[PageEntryEvent]
      WHERE [SessionId] like 'Test%'

DELETE FROM [dbo].[ReferenceTransactionEvent]
      WHERE [SessionId] like 'Test%'

DELETE FROM [dbo].[RepeatVisitorEvent]
      WHERE [SessionIdOld] like 'Test%'
		 OR [SessionIdNew] like 'Test%'

DELETE FROM [dbo].[RetailerHandoffEvent]
      WHERE [SessionId] like 'Test%'

DELETE FROM [dbo].[StopEventRequestEvent]
      WHERE [RequestId] like 'Test%'

DELETE FROM [dbo].[WorkloadEvent]
      WHERE [PartnerId] = -999
