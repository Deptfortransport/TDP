﻿ALTER TABLE [dbo].[SJPNonPostcodeLocations]
	ADD CONSTRAINT [PK_SJPNonPostcodeLocations]
	PRIMARY KEY ([ID]) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
