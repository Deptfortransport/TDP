﻿CREATE PROCEDURE [dbo].[SelectApplicationProperties]
	@AID char(50)
AS

SELECT PNAME, PVALUE FROM PROPERTIES P
	WHERE P.AID = @AID

RETURN 0