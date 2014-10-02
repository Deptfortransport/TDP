-- ************************************************ 
-- TABLE xxxxxxxx
-- ************************************************ 
DECLARE @ObjectName AS varchar(128)
SET @ObjectName = N'xxxxxxxx'
IF NOT EXISTS(SELECT 1
                FROM INFORMATION_SCHEMA.TABLES
               WHERE TABLE_NAME = @ObjectName)
BEGIN

    CREATE TABLE dbo.xxxxxxxx
    (
	    [xxxxxxxxId]            int IDENTITY(1,1)   NOT NULL,
	    [Column2]               nchar(3)            NOT NULL,
	    [Column3]               bit                 NOT NULL,
        CONSTRAINT [PK_xxxxxxxx] PRIMARY KEY CLUSTERED 
        (
	        [xxxxxxxxId] ASC
        ) ON [PRIMARY]
    ) ON [PRIMARY]


    EXEC sp_addextendedproperty @name=N'Owner', @value=N'TDP' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'TABLE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Summary', @value=N'This table is to store data xyx. This information is used to override OBE information on this ticket type.' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'TABLE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Used By', @value=N'TDP.ServiceXyz' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'TABLE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Design', @value=N'SD00nn xyz Design' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'TABLE', @level1name=@ObjectName

    EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Id for row' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'TABLE', @level1name=@ObjectName, @level2type=N'COLUMN', @level2name=N'xxxxxxxxId'

END
GO


