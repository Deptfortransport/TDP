-- ***********************************************
-- NAME 		: DataChangeNotificationDBSetup.cs
-- AUTHOR 		: Rob Greenwood
-- DATE CREATED : 10-Jun-2004
-- DESCRIPTION 	: Sql that can be executed in a database
-- to prepare it for use with the Data Change Notification service
-- ************************************************

IF EXISTS (SELECT * FROM sysobjects WHERE xtype = 'U' AND NAME = 'ChangeNotification')
	BEGIN
		DROP TABLE ChangeNotification
		CREATE TABLE ChangeNotification ([Version] int, [Table] varchar(100))
	END
ELSE
	BEGIN
		CREATE TABLE ChangeNotification ([Version] int, [Table] varchar(100))
	END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE xtype = 'P' AND NAME = 'GetChangeTable')
	DROP PROCEDURE GetChangeTable
GO
	
	CREATE PROCEDURE GetChangeTable AS
	SELECT [Table], Version FROM ChangeNotification
	ORDER BY [Table]
GO


IF EXISTS (SELECT * FROM sysobjects WHERE xtype = 'P' AND NAME = 'UpdateChangeNotificationTable')
	DROP PROCEDURE UpdateChangeNotificationTable
GO

create procedure UpdateChangeNotificationTable 
        ( @tableList varchar(1000) ) 
as 
begin 

        create table #updateTables (tablename varchar(100)) 
        declare @currTable varchar(100), @pos int 
        
        set @tableList = LTRIM(RTRIM(@tableList)) + ',' 
        set @pos = CHARINDEX(',', @tableList, 1) 
        
        if REPLACE(@tableList, ',', '') <> '' 
        begin 
                while @pos > 0 
                begin 
                        set @currTable = LTRIM(RTRIM(LEFT(@tableList, @pos - 1))) 
                        if @currTable <> '' 
                        begin 
                                insert into #updateTables values (@currTable) 
                        end 
                        set @tableList = RIGHT(@tableList, LEN(@tableList) - @pos) 
                        set @pos = CHARINDEX(',', @tableList, 1) 
                end 
        end 

	begin transaction

	-- Update tables that did exist
    update ChangeNotification 
    set    version = version + 1 
    where  [table] in 
            (select tablename from #updateTables) 

	-- Add tables that didn't previously exist
	insert into ChangeNotification ([table], version)
	select 		tablename, 1 
	from		#updateTables
	where 		not tablename in 
				(select [table] from changenotification)
    
    commit transaction
    
end 

go