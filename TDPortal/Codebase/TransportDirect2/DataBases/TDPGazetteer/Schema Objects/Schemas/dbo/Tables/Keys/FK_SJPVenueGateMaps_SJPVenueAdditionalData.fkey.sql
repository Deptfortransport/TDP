ALTER TABLE [dbo].[TDPVenueGateMaps]
    ADD CONSTRAINT [FK_TDPVenueGateMaps_TDPVenueAdditionalData] FOREIGN KEY ([VenueNaPTAN]) REFERENCES [dbo].[TDPVenueAdditionalData] ([VenueNaPTAN]) ON DELETE NO ACTION ON UPDATE NO ACTION;

