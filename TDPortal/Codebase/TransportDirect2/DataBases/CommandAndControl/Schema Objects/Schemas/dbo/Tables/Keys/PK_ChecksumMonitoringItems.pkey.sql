﻿ALTER TABLE [dbo].[ChecksumMonitoringItems]
    ADD CONSTRAINT [PK_ChecksumMonitoringItems] PRIMARY KEY CLUSTERED ([ItemID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

