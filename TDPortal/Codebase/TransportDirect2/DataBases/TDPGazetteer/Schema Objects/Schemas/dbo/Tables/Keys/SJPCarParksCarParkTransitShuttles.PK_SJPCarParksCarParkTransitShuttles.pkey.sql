﻿ALTER TABLE [dbo].[SJPCarParksCarParkTransitShuttles]
	ADD CONSTRAINT [PK_SJPCarParksCarParkTransitShuttles]
	PRIMARY KEY ([CarParkID],[TransitShuttleID]) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
