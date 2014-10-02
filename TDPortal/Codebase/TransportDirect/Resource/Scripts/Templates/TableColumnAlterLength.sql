IF NOT EXISTS (SELECT 1
                 FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_NAME = N'xxxxxxxx'
                  AND COLUMN_NAME = N'myColName'
                  AND CHARACTER_MAXIMUM_LENGTH = newLength)
BEGIN

    ALTER TABLE xxxxxxxx
          ALTER COLUMN  myColName nvarchar(newLength) NOT NULL

END
