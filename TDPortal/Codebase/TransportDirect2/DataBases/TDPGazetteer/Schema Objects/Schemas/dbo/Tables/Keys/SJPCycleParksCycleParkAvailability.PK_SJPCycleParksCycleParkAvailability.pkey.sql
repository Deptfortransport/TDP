﻿ALTER TABLE [dbo].[SJPCycleParksCycleParkAvailability]
	ADD CONSTRAINT [PK_SJPCycleParksCycleParkAvailability]
	PRIMARY KEY ([CycleParkID],[AvailabilityID]) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);