ALTER TABLE [dbo].[SJPPierVenueNavigationPath]
    ADD CONSTRAINT [FK_SJPPierVenueNavigationPath_TDPVenueAdditionalData] FOREIGN KEY ([VenueNaPTAN]) REFERENCES [dbo].[TDPVenueAdditionalData] ([VenueNaPTAN]) ON DELETE NO ACTION ON UPDATE NO ACTION;

