﻿-- ---------------------------------------------------------------------------------------------


--DELETE FROM ReportServer.Reporting.dbo.OperationalEvents
--WHERE CONVERT(varchar(10), OETimeLogged, 121) = @Date

--INSERT INTO ReportServer.Reporting.dbo.OperationalEvents
--(
--	OEID,
--	OESessionID,
--	OEMessage,
--	OEMachineName,
--	OEAssemblyName,
--	OEMethodName,
--	OETypeName,
--	OELevel,
--	OECategory,
--	OETarget,
--	OETimeLogged
--)
--SELECT
--	[ID],
--	SessionID,
--	Message,
--	MachineName,
--	AssemblyName,
--	MethodName,
--	TypeName,
--	[Level],
--	Category,
--	Target,
--	TimeLogged
--FROM OperationalEvent
--WHERE CONVERT(varchar(10), TimeLogged, 121) = @Date