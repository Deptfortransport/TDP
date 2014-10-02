// **************************************************** 
// NAME                 : ErrorAndTimeoutLinkHandler_w3cstyle.js 
// AUTHOR		        : Atos Origin
// DATE CREATED         : 16/05/2005
// DESCRIPTION			: JavaScript functionality for 
//						  Error and TimeOut pages
// ***************************************************** 

// <summary>
// Called by the Error and Time out pages to close a child browser window 
// e.g. printer friendly page and redirect the parent window to the 
// appropriate language home page. Note that the error page will always
// redirect to the default (english) home page
//</summary>
function CloseChildWindow(channelIndicator)
{
	// check to see if the fromTDP string is within the url to detect if the page has been 
	// generated the portal.
	if (document.location.href.lastIndexOf("fromTDP=true")  > -1 && window.opener != null)	
	{		
		if (channelIndicator == "cy-GB")
		{
			window.opener.location.href = '/TransportDirect/cy/?abandon=true';
		}
		else
		{
			window.opener.location.href = '/TransportDirect/en/?abandon=true';
		}						
		window.close();		
	}							
}


// Open page and return "true" if open was unsuccessful
function OpenWindow(url)
{
	var x = window.open(url);
	return (x == null);	
		
}							
