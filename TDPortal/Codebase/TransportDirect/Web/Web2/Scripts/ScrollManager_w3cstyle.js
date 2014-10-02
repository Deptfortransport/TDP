// ***********************************************
// NAME     : ScrollManager_w3cstyle.js
// AUTHOR   : Atos Origin
// ************************************************ 

// Note - the DOM properties/methods required for this functionality are not currently
// part of any standard, hence the requirement for some browser specific functionality.

function scrollToElement(elementId)
{
	var element = document.getElementById(elementId);
	if (element != null)
		element.scrollIntoView(true);		
}

function scrollToClick()
{
	var xPos = document.getElementsByName("ScrollManager_X")[0];
	var yPos = document.getElementsByName("ScrollManager_Y")[0];

	if ((xPos != null) && (yPos != null))
	{
		setXOffset(xPos.value);
		setYOffset(yPos.value);
	}
}

function scrollToTop()
{
	window.scroll(0, 0);
}

function wireStoreCoordinates()
{
	document.onclick = storeCoordinates;
}

function storeCoordinates()
{
	var xPos = document.getElementsByName("ScrollManager_X")[0];
	var yPos = document.getElementsByName("ScrollManager_Y")[0];

	if (xPos != null)
		xPos.value = getXOffset();
	
	if (yPos != null)
		yPos.value = getYOffset();
}

function getXOffset()
{
	if ( getUseDocumentElementScroll() ) 
		return document.documentElement.scrollLeft;
	else if ( getUseBodyScroll() )
		return document.body.scrollLeft;
	else if ( getUseXYOffsetScroll() )
		return window.pageXOffset;
}

function getYOffset()
{
	if ( getUseDocumentElementScroll() ) 
		return document.documentElement.scrollTop;
	else if ( getUseBodyScroll() )
		return document.body.scrollTop;
	else if ( getUseXYOffsetScroll() )
		return window.pageYOffset;
}

function setXOffset(value)
{
	if ( getUseDocumentElementScroll() ) 
		document.documentElement.scrollLeft = value;
	else if ( getUseBodyScroll() )
		document.body.scrollLeft = value;
	else if ( getUseXYOffsetScroll() )
		window.pageXOffset = value;
}

function setYOffset(value)
{
	if ( getUseDocumentElementScroll() ) 
		document.documentElement.scrollTop = value;
	else if ( getUseBodyScroll() )
		document.body.scrollTop = value;
	else if ( getUseXYOffsetScroll() )
		window.pageYOffset = value;
}

function getUseBodyScroll()
{
	return (document.body && (typeof document.body.scrollTop == 'number') );
}

function getUseDocumentElementScroll()
{
	return ( (typeof document.compatMode == 'string') && (document.compatMode.indexOf('CSS') >= 0) &&
		document.documentElement && (typeof document.documentElement.scrollTop == 'number') );
}

function getUseXYOffsetScroll()
{
	return typeof(window.pageYOffset) == 'number';
}