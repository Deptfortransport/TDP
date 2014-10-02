// ***********************************************
// NAME     : JourneyReplanElementSelection_w3cstyle.js
// AUTHOR   : Atos Origin
// ************************************************ 

var allCheckboxes = new Object();
var BoxCount = 0;
var CheckedValues = new Object();
var CheckedValuesCount = 0;
var InterchangeIndicator = "_interchange";	// Interchange suffix constant

function HandleReplanSegmentChecks()
{		
	CheckReplanSelected();
	RemoveReplanLine();
	if (CheckedValuesCount == 1)
	{	
		DrawReplanSingleCellLine();
	}
	else if (CheckedValuesCount > 1)
	{
		DrawReplanLine();
	}
}

function HandleReplanTableChecks()
{		
	CheckReplanSelected();
	RemoveReplanHighlights();
	if (CheckedValuesCount == 1)
	{	
		HighlightReplanSingleRow();
	}
	else if (CheckedValuesCount > 1)
	{
		HighlightReplanArea();	
	}
}

function CheckReplanSelected()
{
    BoxCount = 0;
	CheckedValuesCount = 0;
	
	var allInputElements = document.getElementsByTagName("input");

	for (i = 0; i < allInputElements.length; i++)
	{
		if (allInputElements[i].type == "checkbox")
		{
			allCheckboxes[BoxCount] = allInputElements[i];
			BoxCount++;
			
			if(allInputElements[i].checked == true)
			{
				CheckedValues[CheckedValuesCount] = allInputElements[i].value;			
				CheckedValuesCount++;
			}
		}
	}
}

function RemoveReplanLine()
{
	var interchangeElement = null;
	var interchangeIndex = 0;

	// For each leg represented, get the 3 highlight cells for the leg and remove highlights
	for(i=0; i < BoxCount; i++)
	{
		document.getElementById("highlightCellTop" + allCheckboxes[i].value).className = '';
		document.getElementById("highlightCellMiddle" + allCheckboxes[i].value).className = '';
		document.getElementById("highlightCellBottom" + allCheckboxes[i].value).className = 'departline';

		// Clear any interchanges (have different ID and rarely present, so check for null)
		interchangeIndex = parseInt(allCheckboxes[i].value);

		// Clear replan line on interchange after the selected leg
		interchangeElement = document.getElementById("highlightCellTop" + interchangeIndex + InterchangeIndicator);
		if (interchangeElement != null)
			interchangeElement.className = '';

		interchangeElement = document.getElementById("highlightCellBottom" + interchangeIndex + InterchangeIndicator);
		if (interchangeElement != null)
			interchangeElement.className = 'departline';

		// Clear replan line on interchange before the selected leg
		interchangeIndex--;
		interchangeElement = document.getElementById("highlightCellTop" + interchangeIndex + InterchangeIndicator);
		if (interchangeElement != null)
			interchangeElement.className = '';

		interchangeElement = document.getElementById("highlightCellBottom" + interchangeIndex + InterchangeIndicator);
		if (interchangeElement != null)
			interchangeElement.className = 'departline';

		document.getElementById(allCheckboxes[i].id).disabled = false;
	}
}

function RemoveReplanHighlights()
{
	for(i=0; i < BoxCount; i++)
	{
		document.getElementById("highlightRow" + allCheckboxes[i].value).className = '';
		document.getElementById(allCheckboxes[i].id).disabled = false;
	}
}

function DrawReplanSingleCellLine()
{
	var boxCheckedValues = CheckedValues[0];
	document.getElementById("highlightCellTop" + boxCheckedValues).className = 'bglinedotted';
	document.getElementById("highlightCellMiddle" + boxCheckedValues).className = 'bglinedotted';
	document.getElementById("highlightCellBottom" + boxCheckedValues).className = 'bglinedottedbottom';
	
	// Draw associated interchange line (if present)
	var interchangeElement = null;

	// Draw replan line on interchange after the selected leg
	var interchangeIndex = parseInt(boxCheckedValues);
	
	interchangeElement = document.getElementById("highlightCellTop" + interchangeIndex + InterchangeIndicator);
	if (interchangeElement != null)
		interchangeElement.className = 'bglinedotted';

	interchangeElement = document.getElementById("highlightCellBottom" + interchangeIndex + InterchangeIndicator);
	if (interchangeElement != null)
		interchangeElement.className = 'bglinedottedbottom';

	// Draw replan line on interchange before the selected leg
	interchangeIndex--;
	
	interchangeElement = document.getElementById("highlightCellTop" + interchangeIndex + InterchangeIndicator);
	if (interchangeElement != null)
		interchangeElement.className = 'bglinedotted';

	interchangeElement = document.getElementById("highlightCellBottom" + interchangeIndex + InterchangeIndicator);
	if (interchangeElement != null)
		interchangeElement.className = 'bglinedottedbottom';

}	

function HighlightReplanSingleRow()
{	
	var boxCheckedValues = CheckedValues[0];
	document.getElementById("highlightRow" + boxCheckedValues).className = 'jdtrowhighlight';
}	

function DrawReplanLine()
{	
	var BeginCoordinate = CheckedValues[0];
	var EndCoordinate = CheckedValues[CheckedValuesCount-1];
	var interchangeElement = null;
	var interchangeIndex = 0;

	for(i=BeginCoordinate; i<=EndCoordinate; i++)
	{
		document.getElementById("highlightCellTop" + i).className = 'bglinedotted';
		document.getElementById("highlightCellMiddle" + i).className = 'bglinedotted';
		document.getElementById("highlightCellBottom" + i).className = 'bglinedottedbottom';
		
		// Draw associated interchange line (if present)
		interchangeIndex = parseInt(i);

		// Draw replan line on interchange after the selected leg
		interchangeElement = document.getElementById("highlightCellTop" + interchangeIndex + InterchangeIndicator);
		if (interchangeElement != null)
			interchangeElement.className = 'bglinedotted';

		interchangeElement = document.getElementById("highlightCellBottom" + interchangeIndex + InterchangeIndicator);
		if (interchangeElement != null)
			interchangeElement.className = 'bglinedottedbottom';

		// Draw replan line on interchange before the selected leg
		interchangeIndex--;
		interchangeElement = document.getElementById("highlightCellTop" + interchangeIndex + InterchangeIndicator);
		if (interchangeElement != null)
			interchangeElement.className = 'bglinedotted';

		interchangeElement = document.getElementById("highlightCellBottom" + interchangeIndex + InterchangeIndicator);
		if (interchangeElement != null)
			interchangeElement.className = 'bglinedottedbottom';

		
		if (i == BeginCoordinate || i == EndCoordinate)
		{
			document.getElementById(allCheckboxes[i].id).checked = true;
		}
		else
		{
			document.getElementById(allCheckboxes[i].id).checked = true;
			document.getElementById(allCheckboxes[i].id).disabled = true;
		}
	}
}

function HighlightReplanArea()
{	
	var BeginCoordinate = CheckedValues[0];
	var EndCoordinate = CheckedValues[CheckedValuesCount-1];
	for(i=BeginCoordinate; i<=EndCoordinate; i++)
	{
		document.getElementById("highlightRow" + i).className = 'jdtrowhighlight';
		if (i == BeginCoordinate || i == EndCoordinate)
		{
			document.getElementById(allCheckboxes[i].id).checked = true;
		}
		else
		{
			document.getElementById(allCheckboxes[i].id).checked = true;
			document.getElementById(allCheckboxes[i].id).disabled = true;
		}
	}
}
