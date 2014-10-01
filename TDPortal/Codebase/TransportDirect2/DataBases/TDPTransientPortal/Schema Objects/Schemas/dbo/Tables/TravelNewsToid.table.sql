﻿CREATE TABLE [dbo].[TravelNewsToid]
(
	[TOID] [varchar](25) NOT NULL,
	[UID] [varchar](25) NOT NULL
 CONSTRAINT [PK_TravelNewsToid] PRIMARY KEY CLUSTERED 
(
	[TOID] ASC,
	[UID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
