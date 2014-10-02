DECLARE @ObjectName AS varchar(128)
SET @ObjectName = N'myView'
IF NOT EXISTS (SELECT 1
                 FROM dbo.sysobjects 
                WHERE id = Object_Id(@ObjectName) 
                  AND ObjectProperty(id, N'IsView') = 1)
BEGIN

    EXEC ('CREATE VIEW ' + @ObjectName + ' AS SELECT 1 X')
    EXEC sp_addextendedproperty @name=N'Design', @value=N'SD00? Spec' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'VIEW', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Owner', @value=N'WebTIS' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'VIEW', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'UsedBy', @value=N'WebTIS Service' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'VIEW', @level1name=@ObjectName

END
GO

GO
ALTER VIEW myView AS 
(
    -- Summary info
    SELECT 1 X
)
GO

