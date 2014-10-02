-- ***********************************************
-- AUTHOR		: Rich Broddle
-- NAME 		: MDS021_Special_Event_Update.sql
-- DESCRIPTION 	: Updates Special Event date data & content to indicate possible disruptions on note in journey output
-- SOURCE 		: TDP Support Team
-- ************************************************
--   Rev 1.0   05/07/2013	R Broddle
--Initial revision.
--

use TransientPortal
GO

-- Trash any existing entries first
truncate table dbo.SpecialEvents 
GO

-- Now add SpecialEvents data 

insert into dbo.SpecialEvents values ('2014-July-05 00:00:00','specialEventLabel.TourDeFrance')
insert into dbo.SpecialEvents values ('2014-July-06 00:00:00','specialEventLabel.TourDeFrance')
insert into dbo.SpecialEvents values ('2014-July-07 00:00:00','specialEventLabel.TourDeFrance')


GO
 
-- Add content for above data
use Content

EXEC AddtblContent 
	1, 1, 'langStrings','specialEventLabel.TourDeFrance',																						
'Note: Car journeys in Yorkshire (5/6 July) and Cambridgeshire, Essex and London (7 July) will be affected by a major cycling road race and some road closures may not be reflected in this journey plan.  For full details of planned closures and diversions, please visit <a target="_child" href="http://letour.yorkshire.com/road-closures"> http://letour.yorkshire.com/road-closures <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)"></img></a>',
'Note: Car journeys in Yorkshire (5/6 July) and Cambridgeshire, Essex and London (7 July) will be affected by a major cycling road race and some road closures may not be reflected in this journey plan.  For full details of planned closures and diversions, please visit <a target="_child" href="http://letour.yorkshire.com/road-closures"> http://letour.yorkshire.com/road-closures <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)"></img></a>'

EXEC AddtblContent 
	1, 1, 'langStrings','specialEventLabel.TourDeFrance.PrintFriendly',
'Note: Car journeys in Yorkshire (5/6 July) and Cambridgeshire, Essex and London (7 July) will be affected by a major cycling road race and some road closures may not be reflected in this journey plan.  For full details of planned closures and diversions, please visit <a target="_child" href="http://letour.yorkshire.com/road-closures"> http://letour.yorkshire.com/road-closures <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)"></img></a>',
'Note: Car journeys in Yorkshire (5/6 July) and Cambridgeshire, Essex and London (7 July) will be affected by a major cycling road race and some road closures may not be reflected in this journey plan.  For full details of planned closures and diversions, please visit <a target="_child" href="http://letour.yorkshire.com/road-closures"> http://letour.yorkshire.com/road-closures <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)"></img></a>'


----------------
-- Change Log --
----------------

USE PermanentPortal
GO

Declare @@value decimal(7,3)
Select @@value = left(right('$Revision:   1.6  $',8),7)


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 021 and VersionNumber = @@value)
BEGIN
	UPDATE dbo.MDSChangeCatalogue
	SET
		ChangeDate = getdate(),
		Summary = 'Updates for Special Events'
	WHERE ScriptNumber = 021 AND VersionNumber = @@value
END
ELSE
BEGIN
	INSERT INTO dbo.MDSChangeCatalogue
	(
		ScriptNumber,
		VersionNumber,
		Summary
	)
	VALUES
	(
		021,
		@@value,
		'Updates for Special Events'
	)
END
