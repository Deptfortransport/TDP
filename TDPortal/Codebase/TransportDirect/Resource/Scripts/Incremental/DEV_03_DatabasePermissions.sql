USE PermanentPortal
GO

if not exists(select 'X' from sysusers where name='SJP_User')
BEGIN EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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

USE AirInterchange
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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

USE AirInterchange_Staging
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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

USE AirRouteMatrix
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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


USE ASPState
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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




USE AtosAdditionalData
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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

USE BatchJourneyPlanner
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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

USE CarParks
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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

USE Content
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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

USE GAZ
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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


USE GAZ_Staging
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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

USE InternationalData
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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

USE ProductAvailability
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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

USE Reporting
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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

USE ReportStagingDB
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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

USE TDUserInfo
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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

USE TransientPortal
GO

if not exists(select 'X' from sysusers where name='SJP_User')
  BEGIN  EXEC('CREATE USER SJP_User')  END
GO

EXEC sp_change_users_login 'Auto_Fix', 'SJP_User'
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
