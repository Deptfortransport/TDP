-- **********************************************************************
-- TABLE VALUED FUNCTION xxxxxxxx
-- **********************************************************************
DECLARE @ObjectName AS varchar(128)
SET @ObjectName = N'xxxxxxxx'
IF NOT EXISTS(SELECT 1 
                FROM INFORMATION_SCHEMA.ROUTINES
               WHERE ROUTINE_NAME = @ObjectName 
                 AND ROUTINE_TYPE = 'FUNCTION')
BEGIN
    EXEC ('CREATE FUNCTION [dbo].xxxxxxxx () RETURNS @table TABLE (VALUE nchar(1)) AS BEGIN RETURN END')

    EXEC sp_addextendedproperty @name=N'Design', @value=N'SD00nn Service xyz' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=@ObjectName 
    EXEC sp_addextendedproperty @name=N'Owner', @value=N'TDP' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=@ObjectName 
    EXEC sp_addextendedproperty @name=N'UsedBy', @value=N'TDP.faqs' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=@ObjectName 
END
GO
-- *******************************************************************
-- FUNCTION xxxxxxxx
-- Description:
-- 
-- *******************************************************************
ALTER FUNCTION [dbo].xxxxxxxx
(	
	@i      int
)
RETURNS @Table TABLE (VALUE nvarchar(50))
AS
BEGIN
        
    RETURN
END
GO




-- **********************************************************************
-- SCALAR VALUED FUNCTION yyyyyyyy
-- **********************************************************************
DECLARE @ObjectName AS varchar(128)
SET @ObjectName = N'yyyyyyyy'
IF NOT EXISTS(SELECT 1 
                FROM sysobjects 
               WHERE name = @ObjectName
                 AND xtype = 'FN')
BEGIN
    EXEC ('CREATE FUNCTION [dbo].yyyyyyyy () RETURNS varchar(1) AS BEGIN RETURN ''X'' END')

    EXEC sp_addextendedproperty @name=N'Design', @value=N'SD00nn Service xyz' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Owner', @value=N'TDP' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Usedby', @value=N'TDP.xyz' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=@ObjectName
END
GO
-- **********************************************************************
-- FUNCTION yyyyyyyy
-- Description:
-- 
-- **********************************************************************
ALTER FUNCTION [dbo].yyyyyyyy
(	
	@i  int
)
RETURNS nvarchar(2000) 
AS
BEGIN 

    DECLARE @ReturnString   nvarchar(2000)    
    RETURN (@ReturnString)    

END
GO
