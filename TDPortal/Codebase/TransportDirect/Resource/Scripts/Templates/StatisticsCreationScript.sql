--Create statistics myTable_Stats on table myTable
IF EXISTS(SELECT 1
            FROM sys.stats
           WHERE [name] = 'myTable_Stats')
    BEGIN
        DROP STATISTICS myTable.myTable_Stats
    END
--END IF

CREATE STATISTICS [myTable_Stats] 
ON [dbo].[myTable]([Col1], [Col2], [Col3])
