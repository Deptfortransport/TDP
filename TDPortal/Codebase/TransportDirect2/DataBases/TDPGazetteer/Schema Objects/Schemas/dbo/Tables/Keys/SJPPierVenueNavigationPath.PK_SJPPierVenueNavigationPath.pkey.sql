﻿ALTER TABLE [dbo].[SJPPierVenueNavigationPath]
	ADD CONSTRAINT [PK_SJPPierVenueNavigationPath]
	PRIMARY KEY ([NavigationID]) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
