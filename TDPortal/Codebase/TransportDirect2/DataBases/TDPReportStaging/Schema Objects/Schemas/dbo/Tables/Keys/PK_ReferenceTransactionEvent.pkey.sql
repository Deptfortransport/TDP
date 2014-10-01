﻿ALTER TABLE [dbo].[ReferenceTransactionEvent]
    ADD CONSTRAINT [PK_ReferenceTransactionEvent] PRIMARY KEY NONCLUSTERED ([Submitted] ASC, [EventType] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];

