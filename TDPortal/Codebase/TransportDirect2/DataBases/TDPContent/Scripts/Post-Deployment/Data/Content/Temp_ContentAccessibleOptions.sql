-- =============================================
-- TEMPORARY Content script 
-- Updates to change accessible options info button text
-- =============================================

USE [SJPContent]
GO

DECLARE @Group varchar(100) = 'General'
DECLARE @Collection varchar(100) = 'General'
DECLARE @CultEn varchar(2) = 'en'
DECLARE @CultFr varchar(2) = 'fr'


EXEC AddContent @Group, @CultEn, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.StepFreeAndAssistance.Text','I need a journey that is level and suitable for a wheelchair user and I will also require staff assistance' 
EXEC AddContent @Group, @CultEn, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.StepFreeAndAssistance.Information.ToolTip','Choose this option if you require step-free access and staff assistance.  This option includes any stations, stops and piers that are step-free and where staff assistance is available.  London Buses are not included in this option.  Please note that you should always book assistance at National Rail stations in advance and only stations where assistance is guaranteed at Games-time are included. All London Underground stations are staffed during operating hours and assistance can be requested from staff on arrival – it does not need to be booked in advance.  Assistance with luggage and buggies cannot be guaranteed.'

EXEC AddContent @Group, @CultEn, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.StepFree.Text','I need a journey that is suitable for a wheelchair user'
EXEC AddContent @Group, @CultEn, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.StepFree.Information.ToolTip','This option includes London Buses, recommended National Rail stations as well as step-free London Underground, DLR stations and piers in London. Buses in London are low-level with ramps designed for all customers to get on and off easily. Assistance to board National Rail stations should always be booked in advance.'

EXEC AddContent @Group, @CultEn, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.Assistance.Text','I need a journey with staff assistance at stations, stops and piers'
EXEC AddContent @Group, @CultEn, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.Assistance.Information.ToolTip','Choose this option if you require staff assistance on your journey, but not a step-free journey. This option includes any stations, stops and piers where staff assistance is provided. London Buses are not included in this option. Please note you should always book assistance at National Rail stations in advance and only stations where assistance can be guaranteed at Games-time are included. All London Underground stations are staffed during operating hours and assistance can be requested from staff on arrival – it does not need to be booked in advance.  Assistance with luggage and buggies cannot be guaranteed.'

EXEC AddContent @Group, @CultEn, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.ExcludeUnderground.Text','I do not want to use London Underground'
EXEC AddContent @Group, @CultEn, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.ExcludeUnderground.Information.ToolTip','Choose this option if you wish to avoid using the London Underground.'

EXEC AddContent @Group, @CultEn, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.NoMobilityNeeds.Text','I do not have any accessibility requirements'
EXEC AddContent @Group, @CultEn, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.NoMobilityNeeds.Information.ToolTip','Choose this option if you do not require a step-free journey or assistance. This is the default option.'



	EXEC AddContent @Group, @CultFr, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.StepFreeAndAssistance.Text','J''ai besoin d''un trajet sans escalier, qui est adapté aux utilisateurs de fauteuil roulant et comprenant un service d''assistance'
	EXEC AddContent @Group, @CultFr, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.StepFreeAndAssistance.Information.ToolTip','Choose this option if you require step-free access and staff assistance.  This option includes any stations, stops and piers that are step-free and where staff assistance is available.  London Buses are not included in this option.  Please note that you should always book assistance at National Rail stations in advance and only stations where assistance is guaranteed at Games-time are included. All London Underground stations are staffed during operating hours and assistance can be requested from staff on arrival – it does not need to be booked in advance.  Assistance with luggage and buggies cannot be guaranteed.'

	EXEC AddContent @Group, @CultFr, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.StepFree.Text','I need a journey that is suitable for a wheelchair user'
	EXEC AddContent @Group, @CultFr, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.StepFree.Information.ToolTip','Cette option comprend d’une part les bus londoniens et les gares recommandées du National Rail, d’autre part les stations du métro londonien et du DLR, et quais des navettes fluviales à Londres, qui sont de plain pied ou équipés d’ascenseurs. Dans certaines stations du métro londonien vous pourrez vous déplacer de l’entrée jusqu’au quai sans avoir à monter ou descendre de marches, mais dans d’autres il existe une différence de niveau et un espace entre le métro et le quai. Les bus londoniens sont à plancher bas et équipés d’une rampe amovible pour permettre à tous les voyageurs de monter et descendre facilement. Si vous avez besoin d’assistance dans les gares du National Rail vous devez la réserver à l’avance.'

	EXEC AddContent @Group, @CultFr, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.Assistance.Text','J''ai besoin d''un trajet incluant une assistance aux gares, arrêts et embarcadères'
	EXEC AddContent @Group, @CultFr, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.Assistance.Information.ToolTip','Choose this option if you require staff assistance on your journey, but not a step-free journey. This option includes any stations, stops and piers where staff assistance is provided. London Buses are not included in this option. Please note you should always book assistance at National Rail stations in advance and only stations where assistance can be guaranteed at Games-time are included. All London Underground stations are staffed during operating hours and assistance can be requested from staff on arrival – it does not need to be booked in advance.  Assistance with luggage and buggies cannot be guaranteed.'

	EXEC AddContent @Group, @CultFr, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.ExcludeUnderground.Text','Je ne veux pas utiliser le métro de Londres'
	EXEC AddContent @Group, @CultFr, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.ExcludeUnderground.Information.ToolTip','Choisissez cette option si vous souhaitez éviter de prendre le métro.'

	EXEC AddContent @Group, @CultFr, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.NoMobilityNeeds.Text','Je n''ai pas toutes les exigences d''accessibilité'
	EXEC AddContent @Group, @CultFr, @Collection,'JourneyOptionTabContainer.PublicJourneyOptions.NoMobilityNeeds.Information.ToolTip','Choisissez cette option si vous n’avez pas besoin d’un trajet qui ne comporte pas de marches ou que vous n’avez pas besoin d’assistance. Ceci est l’option par défaut.'
	
GO