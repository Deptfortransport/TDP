﻿ALTER TABLE [dbo].[ChangeCatalogue]
    ADD CONSTRAINT [PK_ChangeCatalogue] PRIMARY KEY CLUSTERED ([ScriptNumber] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

