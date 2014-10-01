USE [ReportProperties]
GO

if not exists(select 'X' from sysusers where name='SJP_User')
BEGIN  EXEC('CREATE USER SJP_User FOR LOGIN SJP_User')  END

EXEC sp_addrolemember N'db_datareader', N'SJP_User'
GO

EXEC sp_addrolemember N'db_datawriter', N'SJP_User'
GO

DECLARE @name nvarchar(128)
DECLARE procCursor CURSOR FOR SELECT Name FROM sysobjects WHERE OBJECTPROPERTY(id, N'IsProcedure') = 1

OPEN procCursor
FETCH NEXT FROM procCursor INTO @name

WHILE  @@FETCH_STATUS = 0
BEGIN  
    EXEC ('grant execute on [' + @name + ']  to SJP_User')

    FETCH NEXT FROM procCursor INTO @name
END

CLOSE procCursor
DEALLOCATE procCursor
GO

USE [ReportingExtract]
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User FOR LOGIN SJP_User')  END
GO

EXEC sp_addrolemember N'db_datareader', N'SJP_User'
GO

EXEC sp_addrolemember N'db_datawriter', N'SJP_User'
GO

DECLARE @name nvarchar(128)
DECLARE procCursor CURSOR FOR SELECT Name FROM sysobjects WHERE OBJECTPROPERTY(id, N'IsProcedure') = 1

OPEN procCursor
FETCH NEXT FROM procCursor INTO @name

WHILE  @@FETCH_STATUS = 0
BEGIN  
    EXEC ('grant execute on [' + @name + ']  to SJP_User')

    FETCH NEXT FROM procCursor INTO @name
END

CLOSE procCursor
DEALLOCATE procCursor
GO
