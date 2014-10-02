USE REPORTING
-- **********************************************************************
-- PROCEDURE xxxxxxxx
-- **********************************************************************
DECLARE @ObjectName AS varchar(128)
SET @ObjectName = N'xxxxxxxx'
IF NOT EXISTS(SELECT 1 
                FROM INFORMATION_SCHEMA.ROUTINES
               WHERE ROUTINE_NAME = @ObjectName
                 AND ROUTINE_TYPE = N'PROCEDURE')
BEGIN
    EXEC ('CREATE PROCEDURE [dbo].xxxxxxxx AS BEGIN SELECT 1 END')

    EXEC sp_addextendedproperty @name=N'Design', @value=N'SD00?? ?????' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Owner', @value=N'TDP' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Used By', @value=N'TDP.??????' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName

END
GO


GO
-- **********************************************************************
-- PROCEDURE xxxxxxxx
-- Description: 
-- 
-- **********************************************************************
ALTER PROCEDURE [dbo].xxxxxxxx
AS
BEGIN
    SELECT 1
END
GO
