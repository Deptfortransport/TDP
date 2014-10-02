IF NOT EXISTS(SELECT 1
                FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
               WHERE TABLE_NAME = 'myTable'
                 AND CONSTRAINT_TYPE = 'PRIMARY KEY')
BEGIN
    ALTER TABLE dbo.myTable
      ADD CONSTRAINT PK_myTable PRIMARY KEY CLUSTERED
          (
             myColumnName ASC
          ) ON [PRIMARY]
    
END
