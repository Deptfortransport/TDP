// *********************************************** 
// NAME                 : MapLocationControl_W3CStyle.js 
// AUTHOR               : Atos Origin
// DATE CREATED         : 20/04/2004
// DESCRIPTION			: JavaScript functionality for MapLocationControl
// ************************************************ 
// 

//	Enum constants from JourneyMapState.
//
//	Default
//	Plan
//	Select_Option
//	Select
//	FindInformation


/// <summary>
///	Called by clicking Plan A Journey button on MapLocationControl.
/// Displays MapPlanJourneyLocationControl in "Plan" JourneyMapState.
/// </summary>
/// <param name="mapLocationControl">ID of parent mapLocationControl</param>
function Plan(mapLocationControl)
{
	SetZoomControlInstructions(mapLocationControl, false);

	// Display appropriate row
	document.getElementById(mapLocationControl+'_headerButtonRow').style.display='none';
	document.getElementById(mapLocationControl+'_headerTextRow').style.display='';

	// MapLocationControl
	document.getElementById(mapLocationControl+'_labelPlanAJourney').style.display='';
	document.getElementById(mapLocationControl+'_labelFindInformation').style.display='none';
	document.getElementById(mapLocationControl+'_labelSelectLocation').style.display='none';

	document.getElementById(mapLocationControl+'_labelOptions').style.display='none';
	document.getElementById(mapLocationControl+'_labelOptions2').style.display='none';

	// User Control Divs
	document.getElementById(mapLocationControl+'_mapPlanJourneyLocationControl_mapPlanJourneyLocationControl').style.display='';
	document.getElementById(mapLocationControl+'_mapFindInformationLocationControl_mapFindInformationLocationControl').style.display='none';
	document.getElementById(mapLocationControl+'_mapSelectLocationControl').style.display='none';

	// Set the JourneyMapState for allignment with server
	document.getElementById(mapLocationControl+'_hdnJourneyMapState').value='Plan';

	return false;
}

/// <summary>
///	Called by clicking Cancel button in MapPlanJourneyLocationControl.
/// Hides MapPlanJourneyLocationControl and displays MapLocation Control in "Default" state.
/// </summary>
/// <param name="mapLocationControl">ID of parent mapLocationControl</param>
function Plan_Cancel(mapLocationControl)
{
	// Display appropriate row
	document.getElementById(mapLocationControl+'_headerButtonRow').style.display='';
	document.getElementById(mapLocationControl+'_headerTextRow').style.display='none';

	// MapLocationControl
	document.getElementById(mapLocationControl+'_buttonPlanAJourney').style.display='';
	document.getElementById(mapLocationControl+'_buttonFindInformation').style.display='';
	document.getElementById(mapLocationControl+'_buttonSelectLocation').style.display='';

	document.getElementById(mapLocationControl+'_labelOptions').style.display='';
	document.getElementById(mapLocationControl+'_labelOptions2').style.display='';

	// MapPlanJourneyLocationControl
	document.getElementById(mapLocationControl+'_mapPlanJourneyLocationControl_mapPlanJourneyLocationControl').style.display='none';

	// Set the JourneyMapState for allignment with server
	document.getElementById(mapLocationControl+'_hdnJourneyMapState').value='Default';

	return false;
}

/// <summary>
///	Called by clicking Find Information button on MapLocationControl.
/// Displays MapFindInformationLocationControl in "FindInformation" state.
/// </summary>
/// <param name="mapLocationControl">ID of parent mapLocationControl</param>
/// <param name="locationUnresolved">Bool - true if location is unresolved on map</param>
function FindInformation(mapLocationControl, locationUnresolved)
{
	SetZoomControlInstructions(mapLocationControl, false);	

	// Display appropriate row
	document.getElementById(mapLocationControl+'_headerButtonRow').style.display='none';
	document.getElementById(mapLocationControl+'_headerTextRow').style.display='';

	//MapLocationControl
	document.getElementById(mapLocationControl+'_labelPlanAJourney').style.display='none';
	document.getElementById(mapLocationControl+'_labelFindInformation').style.display='';
	document.getElementById(mapLocationControl+'_labelSelectLocation').style.display='none';

	document.getElementById(mapLocationControl+'_labelOptions').style.display='none';
	document.getElementById(mapLocationControl+'_labelOptions2').style.display='none';

	// User Control Divs
	document.getElementById(mapLocationControl+'_mapPlanJourneyLocationControl_mapPlanJourneyLocationControl').style.display='none';
	document.getElementById(mapLocationControl+'_mapFindInformationLocationControl_mapFindInformationLocationControl').style.display='';
	document.getElementById(mapLocationControl+'_mapSelectLocationControl').style.display='none';

	// Set the JourneyMapState for allignment with server
	document.getElementById(mapLocationControl+'_hdnJourneyMapState').value='FindInformation';

	return false;
}

/// <summary>
///	Called by clicking Cancel button on MapFindInformationLocationControl.
/// Hides MapFindInformationLocationControl and displays MapLocation Control in "Default" state.
/// </summary>
/// <param name="mapLocationControl">ID of parent mapLocationControl</param>
/// <param name="outputMap">Bool - true if map is for journey results</param>
function FindInformation_Cancel(mapLocationControl, outputMap)
{
	// Display appropriate row
	document.getElementById(mapLocationControl+'_headerButtonRow').style.display='';
	document.getElementById(mapLocationControl+'_headerTextRow').style.display='none';

	// MapLocationControl
	if (outputMap)
	{
		document.getElementById(mapLocationControl+'_buttonPlanAJourney').style.display='none';
	}
	else
	{
		document.getElementById(mapLocationControl+'_buttonPlanAJourney').style.display='';
	}
	document.getElementById(mapLocationControl+'_buttonFindInformation').style.display='';
	document.getElementById(mapLocationControl+'_buttonSelectLocation').style.display='';

	document.getElementById(mapLocationControl+'_labelOptions').style.display='';
	document.getElementById(mapLocationControl+'_labelOptions2').style.display='';

	// MapFindInformationLocationControl
	document.getElementById(mapLocationControl+'_mapFindInformationLocationControl_mapFindInformationLocationControl').style.display='none';

	// Set the JourneyMapState for allignment with server
	document.getElementById(mapLocationControl+'_hdnJourneyMapState').value='Default';

	return false;
}

/// <summary>
///	Called by clicking Select Location button on MapLocationControl.
/// Displays MapSelectLocationControl in "Select" state.
/// </summary>
/// <param name="mapLocationControl">ID of parent mapLocationControl</param>
/// <param name="selectEnabled">Bool - true if zoom level is such that location can be selected</param>
function SelectLocation(mapLocationControl, selectEnabled)
{
	SetZoomControlInstructions(mapLocationControl, selectEnabled);

	// Display appropriate row
	document.getElementById(mapLocationControl+'_headerButtonRow').style.display='none';
	document.getElementById(mapLocationControl+'_headerTextRow').style.display='';

	// MapLocationControl
	document.getElementById(mapLocationControl+'_labelPlanAJourney').style.display='none';
	document.getElementById(mapLocationControl+'_labelFindInformation').style.display='none';
	document.getElementById(mapLocationControl+'_labelSelectLocation').style.display='';

	document.getElementById(mapLocationControl+'_labelOptions').style.display='none';
	document.getElementById(mapLocationControl+'_labelOptions2').style.display='none';

	// User Control Divs
	document.getElementById(mapLocationControl+'_mapPlanJourneyLocationControl_mapPlanJourneyLocationControl').style.display='none';
	document.getElementById(mapLocationControl+'_mapFindInformationLocationControl_mapFindInformationLocationControl').style.display='none';
	document.getElementById(mapLocationControl+'_mapSelectLocationControl').style.display='';

	// MapSelectLocationControl
	document.getElementById(mapLocationControl+'_mapSelectLocationControl_panelResolveLocation').style.display='none';
	document.getElementById(mapLocationControl+'_mapSelectLocationControl_tableOkCancel').style.display='';
	document.getElementById(mapLocationControl+'_mapSelectLocationControl_buttonOK').style.display='none';
	document.getElementById(mapLocationControl+'_mapSelectLocationControl_buttonSelectCancel').style.display='';
	
	if (selectEnabled==true)
	{
		document.getElementById(mapLocationControl+'_mapSelectLocationControl_panelInitial').style.display='';
		document.getElementById(mapLocationControl+'_mapSelectLocationControl_panelZoomLevel').style.display='none';
	}
	else
	{
		document.getElementById(mapLocationControl+'_mapSelectLocationControl_panelInitial').style.display='none';
		document.getElementById(mapLocationControl+'_mapSelectLocationControl_panelZoomLevel').style.display='';
	}

	// Set the JourneyMapState for allignment with server
	document.getElementById(mapLocationControl+'_hdnJourneyMapState').value='Select';

	return false;
}

/// <summary>
///	Called by clicking Cancel button on MapSelectLocationControl.
/// Hides MapSelectLocationControl and displays MapLocation Control in "Default" state.
/// </summary>
/// <param name="mapLocationControl">ID of parent mapLocationControl</param>
/// <param name="outputMap">Bool - true if map is for journey results</param>
function SelectLocation_Cancel(mapLocationControl, outputMap)
{
	SetZoomControlInstructions(mapLocationControl, false);

	// Display appropriate row
	document.getElementById(mapLocationControl+'_headerButtonRow').style.display='';
	document.getElementById(mapLocationControl+'_headerTextRow').style.display='none';
	// MapLocationControl
	if (outputMap)
	{
		document.getElementById(mapLocationControl+'_buttonPlanAJourney').style.display='none';
	}
	else
	{
		document.getElementById(mapLocationControl+'_buttonPlanAJourney').style.display='';
	}
	document.getElementById(mapLocationControl+'_buttonFindInformation').style.display='';
	document.getElementById(mapLocationControl+'_buttonSelectLocation').style.display='';

	document.getElementById(mapLocationControl+'_labelOptions').style.display='';
	document.getElementById(mapLocationControl+'_labelOptions2').style.display='';

	// MapSelectLocationControl
	document.getElementById(mapLocationControl+'_mapSelectLocationControl').style.display='none';

	// Set the JourneyMapState for allignment with server
	document.getElementById(mapLocationControl+'_hdnJourneyMapState').value='Default';

	return false;
}

/// <summary>
/// Ensures that text on zoom control is correct according to current JourneyMapState.
/// </summary>
/// <param name="mapLocationControl">ID of parent mapLocationControl</param>
/// <param name="selectEnabled">Bool - true if zoom level is such that location can be selected</param>
function SetZoomControlInstructions(mapLocationControl, selectEnabled)
{
	// Perform simple string manipulation to obtain zoom control parent
	var strControl, arrControl, strParent;
	strControl = new String (mapLocationControl);
	arrControl = strControl.split('_');
	strParent = arrControl[0];
	if (strParent != strControl) 
	{
		strParent = strParent + '_';
	}
	else
	{
		strParent = '';
	}

	if (selectEnabled)
	{
		document.getElementById(strParent+'theMapZoomControl_labelZoomControlInstructions1').style.display='none';
		document.getElementById(strParent+'theMapZoomControl_labelZoomControlInstructions2').style.display='';
	}
	else
	{
		document.getElementById(strParent+'theMapZoomControl_labelZoomControlInstructions1').style.display='';
		document.getElementById(strParent+'theMapZoomControl_labelZoomControlInstructions2').style.display='none';
	}
}