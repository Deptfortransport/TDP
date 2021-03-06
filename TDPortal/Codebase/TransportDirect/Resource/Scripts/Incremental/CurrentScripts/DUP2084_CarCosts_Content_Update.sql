-- ***********************************************
-- NAME 		: DUP2084_CarCosts_Content_Update.sql
-- DESCRIPTION 	: Car Costs content update for RAC pdf link
-- AUTHOR		: Mitesh Modi
-- DATE			: 05 Nov 2013
-- ************************************************

USE [Content]
GO
------------------------------------------------

-- Mobile site link in footer
EXEC AddtblContent 1, 1, 'langStrings', 'CarJourneyItemisedCostsControl.labelRunningInstruction',
'Note: The running costs are based on information from the RAC for a car that is up to three years old and has averaged 12,000 miles/year. We assume you have a medium sized petrol-engined car unless you have specified your own values for car size and fuel type. More detailed information for running a petrol or diesel car can be obtained from the <a href="http://www.theaa.com/motoring_advice/running_costs/index.html" target="_blank">AA <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a> or <a href="http://www.emmerson-hill.co.uk/documents/RAC_Illustrative_Motoring_costs_April_2013.pdf" target="_blank" >RAC <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>.',
'Noder: Seilir y costau rhedeg ar wybodaeth oddi wrth yr RAC am gar sydd hyd at dair blwydd oed ac sydd wedi gwneud cyfartaledd o 12,000 o filltiroedd y flwyddyn, yn seiliedig ar faint eich car a''r math o danwydd os ydych wedi nodi''r rhain. Gellir cael mwy o wybodaeth fanwl am gostau oddi wrth yr <a href="http://www.theaa.com/motoring_advice/running_costs/index.html" target="_blank">AA <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a> neu''r <a href="http://www.emmerson-hill.co.uk/documents/RAC_Illustrative_Motoring_costs_April_2013.pdf" target="_blank">RAC <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a>.'

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2084, 'Car Costs content update for RAC pdf link'

GO