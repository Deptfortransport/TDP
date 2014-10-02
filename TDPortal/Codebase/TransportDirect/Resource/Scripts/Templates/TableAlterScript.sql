
IF NOT EXISTS (SELECT 1
                 FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_NAME = N'xxxxxxxx'
                  AND COLUMN_NAME = N'myColName')
BEGIN

    ALTER TABLE xxxxxxxx
            ADD myColName nvarchar(20) NOT NULL CONSTRAINT xxxxxxxxmyColName DEFAULT 'X',
                myColName2 nvarchar(50) NULL


    ALTER TABLE xxxxxxxx
            DROP CONSTRAINT xxxxxxxxmyColName  -- Remove the constraint now as it's no longer required


END