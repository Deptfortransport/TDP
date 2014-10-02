// ***********************************************
// NAME     : MapIncidents.js
// AUTHOR   : Atos Origin
// ************************************************ 

// Variables used to determine display postion of 
// details div, relative to mouse cursor
var labelOffsetX = -5;
var labelOffsetY = -5;
var calculatedTotalOffsetLeft = -1;
var calculatedTotalOffsetTop = -1;

var ITEM_UID = 0;
var ITEM_SEVERITY = 1;
var ITEM_MODE = 2;
var ITEM_TYPE = 3;
var ITEM_LOCATION = 4;
var ITEM_DETAIL = 5;
var ITEM_REPORT = 6;
var ITEM_STARTED = 7;
var ITEM_LASTUPDATED = 8;
var ITEM_CLEARED = 9;
var ITEM_EXPIRY = 10;
var ITEM_PLANNED = 11;
var ITEM_CURRENT = 12;

var ITEM_WIDTH = 220;
var ITEM_HEIGHT = 180;

var imageMajor = new Image(); imageMajor.src = "/web2/images/gifs/Misc/MajorIncident.gif";
var imageNormal = new Image(); imageNormal.src = "/web2/images/gifs/Misc/Incident.gif";
var imageFutureMajor = new Image(); imageFutureMajor.src = "/web2/images/gifs/Misc/MajorFutureIncident.gif";
var imageFutureNormal = new Image(); imageFutureNormal.src = "/web2/images/gifs/Misc/FutureIncident.gif";

var noDate = "30/12/1899 00:00:00";
var incidentTableOpening = "<table cellpadding=\"1\" cellspacing=\"0\">";
var rowOpeningTags = "<tr><th align=\"left\" valign=\"top\" width=\"80px\">";
var cellSeparatingTags = "</th><td valign=\"top\">"
var rowEndingTags = "</td></tr>";
var incidentTableClosing = "</table>"

var currentlyShowingRecords;
var currentPage;

var currentMap = "";

function alreadyShowingTheseRecords(incidentRecords)
{
	// Need to decide whether currentlyShowingRecords is
	// the same as incidentRecords
	if (currentlyShowingRecords && incidentRecords)
	{
		if (currentlyShowingRecords.length != incidentRecords.length)
			return false;
		else
		{
			for ( var i = 0; i < currentlyShowingRecords.length; i++ )
			{
				if (currentlyShowingRecords[i] != incidentRecords[i])
					return false;
			}
		}
		return true;
	}
	else
		return false;
}

function updateDetails()
{
	var divElt = document.getElementById('incidentDetails');
	var detailItems = currentlyShowingRecords[currentPage].split("#");

	var html = "<table width=\"" + ITEM_WIDTH + "px\"><tr><td rowspan=2><p class=\"incidentHeading\"><img src=\"";
	
	// Add the correct image and text
	if ( detailItems[ITEM_PLANNED] == "true" )
	{
		// Future incident
		if ( (detailItems[ITEM_MODE] == "Road") ) 
		{
			html += imageNormal.src;
			html += "\" align=\"AbsMiddle\"/>&nbsp;</p></td><td align=\"left\" width=\"95%\"><p class=\"incidentHeading\">" + ResourceIdToString("IncidentMapping_Roadworks_Alt");
			html += "</p></td></tr><tr><td align=\"right\"><nobr>";
		}
		else
		{
			html += imageFutureNormal.src;
			html += "\" align=\"AbsMiddle\"/>&nbsp;</p></td><td align=\"left\" width=\"95%\"><p class=\"incidentHeading\">" + ResourceIdToString("IncidentMapping_PlannedRoadworks_Alt");
			html += "</p></td></tr><tr><td align=\"right\"><nobr>";

		}
	}
	else
	{
		// Current incident
		if ( (detailItems[ITEM_MODE] == "Road") )
		{
			html += imageMajor.src;
			html += "\" align=\"AbsMiddle\"/>&nbsp;</p></td><td align=\"left\" width=\"95%\"><p class=\"incidentHeading\">" + ResourceIdToString("IncidentMapping_RoadIncident_Alt");
			html += "</p></td></tr><tr><td align=\"right\"><nobr>";
		}
		else
		{
			html += imageFutureMajor.src;
			html += "\" align=\"AbsMiddle\"/>&nbsp;" + ResourceIdToString("IncidentMapping_PTIncident_Alt");
			html += "</p></td><td rowspan=2 align=\"right\"><nobr>";
		}
		
	}	
	
	if (currentlyShowingRecords.length > 1)
	{
		if (currentPage != 0)
			html += "<b><a href=\"javascript:changePage(-1)\"><<</a></b>";
		else	
			html += "<<";
			
		html += "&nbsp;&nbsp;" + (currentPage + 1) + " of " + currentlyShowingRecords.length + "&nbsp;&nbsp;";
		
		if ((currentPage + 1) != currentlyShowingRecords.length )
			html += "<b><a href=\"javascript:changePage(1)\">>></a></b></td>";
		else	
			html += ">>";
	}
	else
	{
		//html += "&nbsp;";
	}
	html += "</nobr></td></tr></table>";
	
	html += "<div class=\"incidentTable\">";
	html += incidentTableOpening
	html += rowOpeningTags + ResourceIdToString("TravelNews_MapPopup_Severity") + cellSeparatingTags + detailItems[ITEM_SEVERITY]    + rowEndingTags;
	html += rowOpeningTags + ResourceIdToString("TravelNews_MapPopup_Mode")     + cellSeparatingTags + detailItems[ITEM_MODE]        + rowEndingTags;
	html += rowOpeningTags + ResourceIdToString("TravelNews_MapPopup_Type")     + cellSeparatingTags + detailItems[ITEM_TYPE]        + rowEndingTags;
	html += rowOpeningTags + ResourceIdToString("TravelNews_MapPopup_Location") + cellSeparatingTags + detailItems[ITEM_LOCATION]    + rowEndingTags;
	html += rowOpeningTags + ResourceIdToString("TravelNews_MapPopup_Detail")   + cellSeparatingTags + detailItems[ITEM_DETAIL]      + rowEndingTags;
	html += rowOpeningTags + ResourceIdToString("TravelNews_MapPopup_StartDate")+ cellSeparatingTags + detailItems[ITEM_STARTED]     + rowEndingTags;
	if ((detailItems[ITEM_EXPIRY] != noDate) && ( detailItems[ITEM_PLANNED] == "true" ))
		html += rowOpeningTags + ResourceIdToString("TravelNews_MapPopup_EndDate")     + cellSeparatingTags + detailItems[ITEM_EXPIRY]     + rowEndingTags;
	if (detailItems[ITEM_LASTUPDATED] != noDate)
		html += rowOpeningTags + ResourceIdToString("TravelNews_MapPopup_LastUpdated") + cellSeparatingTags + detailItems[ITEM_LASTUPDATED] + rowEndingTags;
	html += incidentTableClosing;
	html += "</div>";
	
	divElt.innerHTML = html;
}

function changePage(increment)
{
	currentPage += increment;
	if (currentPage < 0)
		currentPage = 0;
	else if (currentPage >= currentlyShowingRecords.length)
		currentPage = (currentlyShowingRecords.length - 1);
	updateDetails();
}

// Fires when mouse over incindent
function displayIncident(mapID, x, y, incidentRecords) 
{	
	// Update the div if we're not already showing these records
	if (!alreadyShowingTheseRecords(incidentRecords))
	{
		currentlyShowingRecords = incidentRecords;
		currentPage = 0;
		updateDetails();
	}

	// Position and display DIV
	var divX = divY = 0;

	// Calculate offset position of map image
	CalulateOffsets(mapID);
	
	divX = x + calculatedTotalOffsetLeft;
	divY = y + calculatedTotalOffsetTop;		
	
	//Specify the left and top positions according to the mouse position and the browser width and height
	//First get the current browser/screen size.
	var documentWidth = 0;
	var documentHeight = 0;			
	//for IE
	if (navigator.userAgent.toLowerCase().indexOf("msie ") != -1)
	{
		if (navigator.userAgent.toLowerCase().indexOf("msie 6.") != -1)
		{
			documentWidth = document.documentElement.clientWidth;
			documentHeight = document.documentElement.clientHeight;
		}
		else 
		{
			documentWidth = document.body.clientWidth;
			documentHeight = document.body.clientHeight;
		}
	}
	//for netscape and others 
	else 
	{
		documentWidth = window.innerWidth;				
		documentHeight = window.innerHeight;
	}
	
	//Using the current browser/screen height and width, specify the location of the incident display.
	//Some incidents at the top may not be visible in the visible range 
	//if the user scrolls down the browser and then hover on the incident.
	if (documentWidth <= (divX + labelOffsetX + ITEM_WIDTH))
		document.getElementById('incidentDetails').style.left = "" + (divX + labelOffsetX - ITEM_WIDTH) + "px";
	else 
		document.getElementById('incidentDetails').style.left = "" + (divX + labelOffsetX) + "px";
		
	if (documentHeight <= (divY + labelOffsetY + ITEM_HEIGHT))
		document.getElementById('incidentDetails').style.top = "" + (divY + labelOffsetY - ITEM_HEIGHT) + "px";
	else 
		document.getElementById('incidentDetails').style.top = "" + (divY + labelOffsetY) + "px";	
		
		
	document.getElementById('incidentDetails').style.visibility = "visible";
}




function showPage(incidentPage)
{
	var elts = document.getElementById("incidentDetails").getElementsByTagName("DIV");
	for ( var i = 0; i < elts.length; i++ )
	{
		alert(elts[i].className);
		if ( elts[i].className == "incident" )
		{
			alert(elts[i].id);
			if ( elts[i].id == "incident" + incidentPage )
				elts[i].style.display = "";
			else
				elts[i].style.display = "none";
		}
	}
}


// If mouse not over incident then details div hidden
function clearIncident() 
{
	document.getElementById('incidentDetails').style.visibility = "hidden";
	currentlyShowingRecords = null;
}
		

// - Note - Fires continiously as the mouse is moving over the map image
function doMouseMove(fnEvent, mapID, checkIncidents) 
{
	var mouseX = mouseY = 0;
	
	// Try using global event variable
	// (x,y position refers to firing element)
	if(window.event && typeof window.event.offsetX == "number")
	{
		mouseX = window.event.offsetX;
		mouseY = window.event.offsetY;
	} 
	// Need to use local event variable
	// (x,y position refers to page, so need to calculate offset)
	else
	{
		CalulateOffsets(mapID);
		mouseX = fnEvent.pageX - calculatedTotalOffsetLeft;
		mouseY = fnEvent.pageY - calculatedTotalOffsetTop;
	}

	// Now check if x,y corresponds to an incident area
	if (checkIncidents) 
	{
		checkIfOverIncident(mouseX,mouseY);
	}
}

function CalulateOffsets(mapID)
{
	var Element = document.getElementById(mapID) ;
	calculatedTotalOffsetLeft = 0;
	calculatedTotalOffsetTop = 0;
	while (Element.offsetParent)
	{
		calculatedTotalOffsetLeft += Element.offsetLeft;                        
		calculatedTotalOffsetTop += Element.offsetTop;
		Element = Element.offsetParent;
	} 
}