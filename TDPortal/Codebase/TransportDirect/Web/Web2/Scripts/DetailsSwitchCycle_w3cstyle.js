// ***********************************************
// NAME     : DetailsSwitchCycle_w3cstyle.js
// AUTHOR   : Atos Origin
// ************************************************ 

var outwardShowMoreButton = document.getElementById("cycleAllDetailsControlOutward_cycleJourneyDetailsTableControl_buttonShowMore");
var outwardShowLessButton = document.getElementById("cycleAllDetailsControlOutward_cycleJourneyDetailsTableControl_buttonShowLess");
var inwardShowMoreButton = document.getElementById("cycleAllDetailsControlReturn_cycleJourneyDetailsTableControl_buttonShowMore");
var inwardShowLessButton = document.getElementById("cycleAllDetailsControlReturn_cycleJourneyDetailsTableControl_buttonShowLess");

var outwardShowMoreDiv = document.getElementById("cycleAllDetailsControlOutward_cycleJourneyDetailsTableControl_divShowMore");
var outwardShowLessDiv = document.getElementById("cycleAllDetailsControlOutward_cycleJourneyDetailsTableControl_divShowLess");
var inwardShowMoreDiv = document.getElementById("cycleAllDetailsControlReturn_cycleJourneyDetailsTableControl_divShowMore");
var inwardShowLessDiv = document.getElementById("cycleAllDetailsControlReturn_cycleJourneyDetailsTableControl_divShowLess");

var outwardShowMoreHidden = document.getElementById("cycleAllDetailsControlOutward_cycleJourneyDetailsTableControl_hdnShowDetailsState");
var inwardShowMoreHidden = document.getElementById("cycleAllDetailsControlReturn_cycleJourneyDetailsTableControl_hdnShowDetailsState");

function ShowDetails(hiddenInputId, callingControl)
{  
	var printButton;
	
	var currentShow;
	var targetShow;
	
	var isOutward = ((callingControl.id == outwardShowMoreButton.id) || (callingControl.id == outwardShowLessButton.id));
	var isReturn = false;
	
	if (inwardShowMoreButton != null)
	{
	    isReturn = ((callingControl.id == inwardShowMoreButton.id) || (callingControl.id == inwardShowLessButton.id));
	}
		
	var showValue = 'show'
	var hideValue = 'hide'
	
	// Get the current display mode
	if (isOutward)
	{
	    currentShow = outwardShowMoreHidden.value;
	}
	
	if (isReturn)
	{
		currentShow = inwardShowMoreHidden.value;
	}
	
	// Flip the hidden value to be opposite of current display
    if (currentShow == showValue)
	{
	    targetShow = hideValue;
	    
	    if (isOutward)
	    {
		    outwardShowMoreHidden.value = targetShow;
        }
        
        if (isReturn)
        {
            inwardShowMoreHidden.value = targetShow;
        }
	}
	else 
	{
	    targetShow = showValue;
	    
		if (isOutward)
	    {
		    outwardShowMoreHidden.value = targetShow;
        }
        
        if (isReturn)
        {
            inwardShowMoreHidden.value = targetShow;
        }
	}
	

    // Get the print button, to allow display mode to be appended
    printButton = document.getElementById('journeyChangeSearchControl_printerFriendlyPageButton_printButton');
	
	var cssclassTarget;
	var cssclassCurrent;

    // Set the new css class for the elements		
	if (targetShow == showValue)
	{
	    if (isOutward)
	    {
	        cssclassTarget = 'cycleJourneyDetailsAttributeShowOut';
	        cssclassCurrent = 'cycleJourneyDetailsAttributeHideOut';
	    }
	    
	    if (isReturn)
	    {
	        cssclassTarget = 'cycleJourneyDetailsAttributeShowRet';
	        cssclassCurrent = 'cycleJourneyDetailsAttributeHideRet';
	    }
	    
		var elements = document.getElementsByTagName('*');
        for (i=0; i<elements.length;i++)
        {
            if (elements[i].className == cssclassCurrent) 
            {
                elements[i].className = cssclassTarget;
            }
        }

    	ReplaceOnClickSubstring(printButton, 'details=hide', 'details=show');
	}	
	else
	{
	    if (isOutward)
	    {
	        cssclassTarget = 'cycleJourneyDetailsAttributeHideOut';
	        cssclassCurrent = 'cycleJourneyDetailsAttributeShowOut';
	    }
	    
	    if (isReturn)
	    {
	        cssclassTarget = 'cycleJourneyDetailsAttributeHideRet';
	        cssclassCurrent = 'cycleJourneyDetailsAttributeShowRet';
	    }
	    
		var elements = document.getElementsByTagName('*');
        for (i=0; i<elements.length;i++)
        {
            if (elements[i].className == cssclassCurrent) 
            {
                elements[i].className = cssclassTarget;
            }
        }

		ReplaceOnClickSubstring(printButton, 'details=show', 'details=hide');
	}
	
    // Set the Show/Hide div class
    if (isOutward)
    {
        outwardShowMoreDiv.className = cssclassCurrent + ' nopadding';
        outwardShowLessDiv.className = cssclassTarget + ' nopadding';
    }
    
    if (isReturn)
    {
        inwardShowMoreDiv.className = cssclassCurrent + ' nopadding';
        inwardShowLessDiv.className = cssclassTarget + ' nopadding';
    }
}

function ReplaceOnClickSubstring(inControl, oldString, newString)
{
	if (inControl != null)
	{
		var stringOnclick = inControl.onclick.toString();

		// Strip Anonymous function declaration
		var startOfFunction = stringOnclick.indexOf("\{")+1;
		var endOfFunction = stringOnclick.lastIndexOf("\}");
		var stringOnclick2 = stringOnclick.substring(startOfFunction, endOfFunction);

		var stringOnclick3 = stringOnclick2.replace(oldString, newString);

		inControl.onclick = new Function(stringOnclick3);
	}
}


