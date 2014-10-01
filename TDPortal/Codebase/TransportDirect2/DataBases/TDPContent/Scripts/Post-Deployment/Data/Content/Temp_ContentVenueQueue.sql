-- =============================================
-- TEMPORARY Content script 
-- Updates to change venues queue text to suggest arrival time should 
-- take into account venues will be busy
-- =============================================

-- THESE CHANGES HAVE NOW BEEN MERGED INTO THE MAIN CONTENT SCRIPTS

--USE [SJPContent]
--GO

--DECLARE @Group varchar(100) = 'General'
--DECLARE @Collection varchar(100) = 'General'
--DECLARE @CultEn varchar(2) = 'en'
--DECLARE @CultFr varchar(2) = 'fr'

---- Input page queue text
--EXEC AddContent @Group, @CultEn, @Collection, 'JourneyPlannerInput.locationLabel.Text', 'Enter your start location into the ''From'' box to generate a list of matches and then select from the drop-down menu or enter your postcode.<br /><br /><strong>Please enter the time you would like to get to your venue, taking into account <a href="http://www.london2012.com/visiting/at-the-venues">recommended arrival times.</a> Remember – the transport network will be extremely busy during the Games so you may want to allow additional time for travel.</strong>'
--EXEC AddContent @Group, @CultEn, @Collection, 'JourneyPlannerInput.locationLabel.travelBetweenVenues.Text', 'Select the venues you are travelling between and choose your date(s) of travel.<br /><br /><strong>Please enter the time you would like to get to your venue, taking into account <a href="http://www.london2012.com/visiting/at-the-venues">recommended arrival times.</a> Remember – the transport network will be extremely busy during the Games so you may want to allow additional time for travel.</strong>'
--EXEC AddContent @Group, @CultEn, @Collection, 'JourneyPlannerInput.locationLabel.travelFromVenue.Text', 'Enter your end location into the ''To'' box to generate a list of matches and then select from the drop-down menu or enter your postcode.<br /><br /><strong>Remember – the transport network will be extremely busy during the Games so you may want to allow additional time for travel.</strong>'

--	EXEC AddContent @Group, @CultFr, @Collection, 'JourneyPlannerInput.locationLabel.Text', 'Entrez votre point de départ dans la case ''Départ'' pour générer une liste de correspondances, puis sélectionnez dans le menu déroulant ou entrez le code postal.<br /><br /><strong>Please enter the time you would like to get to your venue, taking into account <a href="http://www.london2012.com/visiting/at-the-venues">recommended arrival times.</a> Remember – the transport network will be extremely busy during the Games so you may want to allow additional time for travel.</strong>'
--	EXEC AddContent @Group, @CultFr, @Collection, 'JourneyPlannerInput.locationLabel.travelBetweenVenues.Text', 'Sélectionnez les sites entre lesquels vous allez circuler ainsi que la/les date(s) de vos déplacements.<br /><br /><strong>Please enter the time you would like to get to your venue, taking into account <a href="http://www.london2012.com/visiting/at-the-venues">recommended arrival times.</a> Remember – the transport network will be extremely busy during the Games so you may want to allow additional time for travel.</strong>'
--	--EXEC AddContent @Group, @CultFr, @Collection, 'JourneyPlannerInput.locationLabel.travelFromVenue.Text', 'Enter your end location into the ''To'' box to generate a list of matches and then select from the drop-down menu or enter your postcode.<br /><br /><strong>Remember – the transport network will be extremely busy during the Games so you may want to allow additional time for travel.</strong>'

---- Queue image to walk image
--EXEC AddContent @Group, @CultEn, @Collection,'TransportMode.Queue.ImageUrl','presentation/jp_walk.png'

--GO


--DECLARE @Group varchar(100) = 'JourneyOutput'
--DECLARE @Collection varchar(100) = 'Journey'
--DECLARE @CultEn varchar(2) = 'en'
--DECLARE @CultFr varchar(2) = 'fr'

---- Result page queue text
--EXEC AddContent @Group, @CultEn, @Collection, 'JourneyOutput.Text.AllowTimeFor.ExitFromVenue.Outward', 'Remember – the transport network will be extremely busy during the Games so you may want to allow additional time for travel.'
--EXEC AddContent @Group, @CultEn, @Collection, 'JourneyOutput.Text.AllowTimeFor.EntranceToVenue.Outward', '<a href="http://www.london2012.com/visiting/at-the-venues">Have you checked the recommended arrival time for your venue?</a> Remember – the transport network will be extremely busy during the Games so you may want to allow additional time for travel.'
--EXEC AddContent @Group, @CultEn, @Collection, 'JourneyOutput.Text.AllowTimeFor.ExitFromVenue.Return', 'Remember – the transport network will be extremely busy during the Games so you may want to allow additional time for travel.'
--EXEC AddContent @Group, @CultEn, @Collection, 'JourneyOutput.Text.AllowTimeFor.EntranceToVenue.Return', '<a href="http://www.london2012.com/visiting/at-the-venues">Have you checked the recommended arrival time for your venue?</a> Remember – the transport network will be extremely busy during the Games so you may want to allow additional time for travel.'

--GO