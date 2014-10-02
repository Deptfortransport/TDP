-- ==================================================
-- Rename a column
-- ==================================================
DECLARE @OldColumnName nvarchar(128)
DECLARE @NewColumnName nvarchar(128)
DECLARE @TableName     nvarchar(128)

SET @OldColumnName = N'myOldColumnName'
SET @NewColumnName = N'myNewColumnName'
SET @TableName     = N'myTableName'


IF EXISTS (SELECT 1
             FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME  = @TableName
              AND COLUMN_NAME = @OldColumnName)

IF EXISTS(SELECT 1
            FROM INFORMATION_SCHEMA.COLUMNS
           WHERE COLUMN_NAME = @OldColumnName
             AND TABLE_NAME = @TableName)
BEGIN
    DECLARE @TableNameOldColumnName nvarchar(256)
    SET @TableNameOldColumnName = @TableName + '.' + @OldColumnName
    EXECUTE sp_rename @TableNameOldColumnName, @NewColumnName, 'COLUMN' 
END
GO
