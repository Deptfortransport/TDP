﻿ALTER TABLE [dbo].[SJPCarParkToids]
    ADD CONSTRAINT [PK_SJPCarParkToids] PRIMARY KEY CLUSTERED ([ParkAndRideID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

