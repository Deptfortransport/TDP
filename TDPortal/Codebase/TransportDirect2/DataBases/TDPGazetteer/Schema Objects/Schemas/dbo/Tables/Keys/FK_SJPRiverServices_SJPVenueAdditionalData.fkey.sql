ALTER TABLE [dbo].[SJPRiverServices]
    ADD CONSTRAINT [FK_SJPRiverServices_TDPVenueAdditionalData] FOREIGN KEY ([VenueNaPTAN]) REFERENCES [dbo].[TDPVenueAdditionalData] ([VenueNaPTAN]) ON DELETE NO ACTION ON UPDATE NO ACTION;

