﻿CREATE TABLE [dbo].[SJPEventDates]
(
	EventDate datetime NOT NULL, 
	CONSTRAINT [PK_SJPEventDates] PRIMARY KEY CLUSTERED 
	(
		[EventDate] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]