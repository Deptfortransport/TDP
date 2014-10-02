// *********************************************** 
// NAME                 : DetectJavaScript.js 
// AUTHOR               : Atos Origin
// DATE CREATED         : 20/04/2004
// DESCRIPTION			: Generic script do determine what type of 
//						  object model is supported by the browser.
// ************************************************ 
// 

/// <summary>
///	Detects the browsers level of DOM support.
/// Records this using the two hidden input controls embedded in every page.
/// </summary>
function ObjectDetection()
{
	if (document.getElementById)
	{
		// browser implements part of W3C DOM HTML
		// Gecko, Internet Explorer 5+, Opera 5+
		document.getElementsByName("hdnTest")[0].value="true";
		document.getElementsByName("hdnDOMStyle")[0].value="W3C_STYLE";
	}
	else if (document.all)
	{
		// Internet Explorer 4 or Opera with IE user agent
		//document.all["hdnTest"].value="true";
		//document.all["hdnDOMStyle"].value="IE4_STYLE";
	}
	else if	 (document.layers)
	{
		// Navigator 4
		//document.layers["hdnTest"].value="true";
		//document.layers["hdnDOMStyle"].value="NN4_STYLE";
	}
	else
	{
		// No valid DOM type detected so do nothing.
		// Hidden fields do not get set so JavaScript not used.
	}
}



