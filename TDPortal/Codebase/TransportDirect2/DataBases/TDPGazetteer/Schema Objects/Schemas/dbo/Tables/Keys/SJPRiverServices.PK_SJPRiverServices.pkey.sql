﻿ALTER TABLE [dbo].[SJPRiverServices]
    ADD CONSTRAINT [PK_SJPRiverServices] PRIMARY KEY CLUSTERED ([VenueNaPTAN] ASC, [VenuePierNaPTAN] ASC, [RemotePierNaPTAN] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


