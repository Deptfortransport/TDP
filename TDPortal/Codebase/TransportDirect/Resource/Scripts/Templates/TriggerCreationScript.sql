IF EXISTS (SELECT * FROM sysobjects
            WHERE id = OBJECT_ID(N'[dbo].[xxxxxxxx]')
              AND OBJECTPROPERTY(id, N'IsTrigger') = 1)
BEGIN
    DROP TRIGGER [dbo].[xxxxxxxx]
END
GO 

CREATE TRIGGER xxxxxxxx
    ON dbo.myTableName FOR UPDATE, DELETE, INSERT
   NOT FOR REPLICATION AS
BEGIN
    DECLARE @i int
END
GO
