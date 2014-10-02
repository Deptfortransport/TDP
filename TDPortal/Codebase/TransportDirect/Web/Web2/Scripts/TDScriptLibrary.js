// ***********************************************
// NAME     : TDScriptLibrary.js
// AUTHOR   : Atos Origin
// ************************************************ 

function ShowCal(dateFieldId, dayFieldId  )
{
	// Calculate pop-up position relative to where icon was clicked
	var width = 240 + 20;
	var height = 170 + 20;
	var left = 50;
	var top = 50;
	//  TODO  This code needs to be converted to browser independent code.
	//var left = window.event.screenX - (width / 2);
	//var top = window.event.screenY - (height / 2);
	
	// Open new browser window passing field IDs and contents of date field in URL
	var dateVal = window.document.forms[0].elements[dateFieldId].value;
	window.open("/del3td01/Controls/CalPop.aspx?dateFieldId=" + dateFieldId + "&dayFieldId=" + dayFieldId + "&date="+dateVal,"NULL","toolbar=no,scrollbar=no,height=" + height + ",width=" + width + ",left=" + left +",top=" + top);
}

function SetDay(day1, day2, day3, day4, day5, day6, day7, dayFieldId,dateFieldId) 
{	

	// The array dayNames is intialaised to the internalionised day names passed in
	var dayNames = new Array(day1, day2, day3, day4, day5, day6, day7);
	var text = window.document.forms[0].elements[dateFieldId].value;

	// Check format is correct		
	fullDateRe = new RegExp("^[0-9][0-9]/[0-9][0-9]/[0-9]{4}$");
	if (!fullDateRe.test(text)) return;
	
	// Swap date and month around in text field value since Date object constructor
	// requires date in format mm/dd/yyyy.
	
	dateRe = new RegExp("^[0-9]{2}");
	monthRe = new RegExp("/[0-9]{2}/");
	yearRe = new RegExp("[0-9]{4}$");

	monthStr = monthRe.exec(text).toString().slice(1,3);
	
	var d = new Date(monthStr + "/" + dateRe.exec(text) + "/" + yearRe.exec(text));	
	
	// Data object may not have been created if date in text field is invalid
	if (!isNaN(d)) {
	    // The following code has been tested in I56, NS7, Mozilla 1.3 and Opera 7
		document.getElementById(dayFieldId).innerHTML = dayNames[d.getDay()];
	} else {
		document.getElementById(dayFieldId).innerHTML = "";
	}
	
}