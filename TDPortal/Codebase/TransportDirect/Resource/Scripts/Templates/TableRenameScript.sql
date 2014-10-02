-- **********************************************************************
-- Rename the table myOldTablename table to myNewTablename
-- **********************************************************************
DECLARE @OldTablename nvarchar(128)
DECLARE @NewTablename nvarchar(128)

SET @OldTablename = N'myOldTablename'
SET @NewTablename = N'myNewTablename'

IF EXISTS (SELECT 1
             FROM INFORMATION_SCHEMA.TABLES
            WHERE TABLE_NAME = @OldTablename)
BEGIN
    EXEC sp_rename @OldTablename, @NewTablename
END
GO

