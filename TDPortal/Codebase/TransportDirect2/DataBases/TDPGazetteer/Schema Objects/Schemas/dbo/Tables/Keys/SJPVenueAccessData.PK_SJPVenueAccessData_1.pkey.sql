﻿  /****** Object:  Index [PK_TDPVenueAccessData]    Script Date: 08/12/2011 11:01:59 ******/
ALTER TABLE [dbo].[TDPVenueAccessData] ADD  CONSTRAINT [PK_TDPVenueAccessData] PRIMARY KEY CLUSTERED 
(
	[VenueNaPTAN] ASC,
	[AccessFrom] ASC,
	[AccessTo] ASC,
	[StationNaPTAN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

