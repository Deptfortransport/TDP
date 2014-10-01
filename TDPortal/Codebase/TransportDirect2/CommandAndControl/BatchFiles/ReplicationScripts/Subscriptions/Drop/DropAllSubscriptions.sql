
--AirInterchange-----------------------------------------------------------------------
-- Dropping the snapshot subscriptions
use [AirInterchange]
exec sp_dropsubscription @publication = N'AirInterchangeSnapshotPublication', 
@subscriber = N'$(WorkUnit)', @destination_db = N'AirInterchange', @article = N'all'
GO

--CP_Routing-----------------------------------------------------------------------
-- Dropping the snapshot subscriptions
use [CP_Routing]
exec sp_dropsubscription @publication = N'CP_RoutingSnapshotPublication', 
@subscriber = N'$(WorkUnit)', @destination_db = N'CP_Routing', @article = N'all'
GO

--NPTG-----------------------------------------------------------------------
-- Dropping the snapshot subscriptions
use [NPTG]
exec sp_dropsubscription @publication = N'NPTGSnapshotPublication', 
@subscriber = N'$(WorkUnit)', @destination_db = N'NPTG', @article = N'all'
GO

--Routing-----------------------------------------------------------------------
-- Dropping the snapshot subscriptions
use [Routing]
exec sp_dropsubscription @publication = N'RoutingSnapshotPublication', 
@subscriber = N'$(WorkUnit)', @destination_db = N'Routing', @article = N'all'
GO

--SJPConfiguration-----------------------------------------------------------------------
-- Dropping the transactional subscriptions
use [SJPConfiguration]
exec sp_dropsubscription @publication = N'SJPConfigurationTransactionalPublication', 
@subscriber = N'$(WorkUnit)', @destination_db = N'SJPConfiguration', @article = N'all'
GO

--SJPContent-----------------------------------------------------------------------
-- Dropping the transactional subscriptions
use [SJPContent]
exec sp_dropsubscription @publication = N'SJPContentTransactionalPublication', 
@subscriber = N'$(WorkUnit)', @destination_db = N'SJPContent', @article = N'all'
GO

--SJPGazetteer-----------------------------------------------------------------------
-- Dropping the transactional subscriptions
use [SJPGazetteer]
exec sp_dropsubscription @publication = N'SJPGazetteerTransactionalPublication', 
@subscriber = N'$(WorkUnit)', @destination_db = N'SJPGazetteer', @article = N'all'
GO

--SJPTransientPortal-----------------------------------------------------------------------
-- Dropping the transactional subscriptions
use [SJPTransientPortal]
exec sp_dropsubscription @publication = N'SJPTransientPortalTransactionalPublication', 
@subscriber = N'$(WorkUnit)', @destination_db = N'SJPTransientPortal', @article = N'all'
GO

