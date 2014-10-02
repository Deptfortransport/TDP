// *******************************************************************
// NAME                 : TDAutoPostback_w3cstyle.js 
// AUTHOR               : Atos Origin
// DATE CREATED         : 17/02/2005
// DESCRIPTION			: Can be  attached to a control to perform
//						  a postback and run detect javascript script.
// *******************************************************************
// 

function TDAutoPostback()
{
	ObjectDetection();
	document.forms[0].submit();
}