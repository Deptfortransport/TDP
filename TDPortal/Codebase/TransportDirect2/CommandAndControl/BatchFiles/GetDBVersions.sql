/****** Script for SelectTopNRows command from SSMS  ******/
Use SJPConfiguration
SELECT TOP 1000 [DatabaseVersionInfo]
  FROM [SJPConfiguration].[dbo].[VersionInfo]
  
  Use SJPContent
SELECT TOP 1000 [DatabaseVersionInfo]
  FROM [SJPContent].[dbo].[VersionInfo]
  
  Use SJPGazetteer
SELECT TOP 1000 [DatabaseVersionInfo]
  FROM [SJPGazetteer].[dbo].[VersionInfo]
  
  Use SJPTransientPortal
SELECT TOP 1000 [DatabaseVersionInfo]
  FROM [SJPTransientPortal].[dbo].[VersionInfo]

