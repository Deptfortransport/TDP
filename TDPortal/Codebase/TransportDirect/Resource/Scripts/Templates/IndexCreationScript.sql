
--Drop and recreate index MyIndex on table MyTable
IF EXISTS(SELECT 1
            FROM sysindexes si
           INNER JOIN sysobjects so
                   ON so.id = si.id
           WHERE si.[Name] = N'MyIndex'  -- Index Name
             AND so.[Name] = N'MyTable')  -- Table Name
BEGIN
    DROP INDEX MyTable.MyIndex 
END
CREATE UNIQUE NONCLUSTERED INDEX MyIndex ON [dbo].MyTable 
(
	MyColumn1 ASC,
	MyColumn2 ASC
) ON [PRIMARY]
