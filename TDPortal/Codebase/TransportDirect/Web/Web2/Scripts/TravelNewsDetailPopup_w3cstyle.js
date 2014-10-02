// Variables used to determine display postion of 
// details div, relative to mouse cursor
var labelOffsetX = 20; //-5;
var labelOffsetY = -5;
var calculatedTotalOffsetLeft = -1;
var calculatedTotalOffsetTop = -1;

// Variables used to store the current cursor position
// where div should be displayed.
var hoverX = 0;
var hoverY = 0;

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



//this method will be called on mouseover event
// this will initiate ajax call to server
// when ajax request returns handleNewsServerResponse function will be called
function showTravelNewsDetail(e, uid)
{
  if(uid.length >0)
  {
    displayPopup(e.clientX,e.clientY);
    // AJAX call
    TransportDirect.UserPortal.Web.Controls.TravelNewsHeadlineControl.GetNewsDetails(uid,handleNewsServerResponse );
  }
  
}

// ajax callback - handles the response back from server
function handleNewsServerResponse(res)
{
     if(!res.error)
     {
        updateDisplay(res.value);
     }
}

// updates the incident display div details 
function updateDisplay(newsDetail)
{
    var divElt = document.getElementById('travelNewsHeadlines_incidentDetails');
	var detailItems = newsDetail.split("#");

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
	
	document.getElementById('travelNewsHeadlines_incidentDetails').style.display = "block";
    document.getElementById('travelNewsHeadlines_incidentDetails').style.visibility = "visible";

}


// displays popup where the mouse is hovered
function displayPopup(x, y)
{
    // Position and display DIV
	var divX = divY = 0;

		
	divX = x ;
	divY = y ;
	
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
		document.getElementById('travelNewsHeadlines_incidentDetails').style.left = "" + (divX + labelOffsetX - ITEM_WIDTH) + "px";
	else 
		document.getElementById('travelNewsHeadlines_incidentDetails').style.left = "" + (divX + labelOffsetX) + "px";
		
	if (documentHeight <= (divY + labelOffsetY + ITEM_HEIGHT))
		document.getElementById('travelNewsHeadlines_incidentDetails').style.top = "" + (divY + labelOffsetY - ITEM_HEIGHT) + "px";
	else 
		document.getElementById('travelNewsHeadlines_incidentDetails').style.top = "" + (divY + labelOffsetY) + "px";	
		
		
	
}


// hides the incidentdetail div
function hideTravelNewsDetail()
{
  var divElt = document.getElementById('travelNewsHeadlines_incidentDetails');
  
  divElt.style.display="none";
  divElt.style.visibility = "hidden";
}