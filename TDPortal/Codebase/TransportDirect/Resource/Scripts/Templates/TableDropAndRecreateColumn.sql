DECLARE @TableName nvarchar(128)
DECLARE @ColumnName nvarchar(128)

SET @TableName = N'MyTablename'
SET @ColumnName = N'MyColumnName'

IF EXISTS (SELECT 1
             FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME  = @TableName
              AND COLUMN_NAME = @ColumnName
              AND DATA_TYPE   = N'MyOldDataType')
BEGIN

    ALTER TABLE MyTablename
           DROP COLUMN MyColumnname 

    ALTER TABLE MyTablename
            ADD MyColumnname mynewdatatype NOT NULL DEFAULT 0  -- NOTE: A default is required if the table already has 
                                                             --       data on and the column is defined as NOT NULL

    -- Add the column description
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'put the column description here' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=@TableName, @level2type=N'COLUMN', @level2name=@ColumnName

END
GO