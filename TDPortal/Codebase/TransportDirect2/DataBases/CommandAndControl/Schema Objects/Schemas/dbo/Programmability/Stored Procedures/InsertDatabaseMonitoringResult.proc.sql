﻿CREATE PROCEDURE [dbo].[InsertDatabaseMonitoringResult]
           (@MonitoringItemID int
           ,@SJP_Server varchar(50)
           ,@Description varchar(200)
           ,@CheckTime datetime
           ,@ValueAtCheck varchar(200)
           ,@IsInRed bit)
AS
IF NOT EXISTS (SELECT [MonitoringItemID],[SJP_Server],[Description],[CheckTime],[ValueAtCheck],[IsInRed]
  FROM [dbo].[SJPDatabaseMonitoringResults]
  WHERE [SJP_Server] = @SJP_Server AND [MonitoringItemID] = @MonitoringItemID AND [CheckTime] = @CheckTime)
BEGIN

INSERT INTO [dbo].[SJPDatabaseMonitoringResults]
           ([MonitoringItemID]
           ,[SJP_Server]
           ,[Description]
           ,[CheckTime]
           ,[ValueAtCheck]
           ,[IsInRed])
     VALUES
           (@MonitoringItemID 
           ,@SJP_Server 
           ,@Description 
           ,@CheckTime 
           ,@ValueAtCheck 
           ,@IsInRed )
END

RETURN 0