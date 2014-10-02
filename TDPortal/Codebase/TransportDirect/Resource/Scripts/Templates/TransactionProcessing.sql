-- Results to Text
WHILE @@TranCount > 0 ROLLBACK TRAN

DELETE TranTest
GO
DECLARE @return int
BEGIN TRAN

    EXEC @return = TranTest_Insert 1
    PRINT 'First return: ' + Cast(@return AS varchar)
    IF @return <> 0
        BEGIN
            ROLLBACK TRAN
            GOTO Done
        END
    PRINT 'TranCount: ' + Cast(@@Trancount AS varchar)

    PRINT ''
    PRINT '------------------------------------------------------------------------'
    PRINT ''


    EXEC @return = TranTest_Insert 1  -- Issues a ROLLBACK which causes an error
   PRINT 'Second return: ' + Cast(@return AS varchar)
    IF @return <> 0
        BEGIN
            ROLLBACK TRAN -- Should check @@TRANCOUNT
          GOTO done
        END

COMMIT TRAN

Done:

PRINT 'Final TranCount: ' + Cast(@@Trancount AS varchar)

SELECT * FROM TranTest


/*

WHILE @@TRANCOUNT > 0
     ROLLBACK TRAN

*/
