﻿ALTER TABLE [dbo].[SJPGNATAdminAreas]
	ADD CONSTRAINT [PK_SJPGNATAdminAreas]
	PRIMARY KEY CLUSTERED
	(
		[AdministrativeAreaCode]    ASC,
		[DistrictCode]              ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO