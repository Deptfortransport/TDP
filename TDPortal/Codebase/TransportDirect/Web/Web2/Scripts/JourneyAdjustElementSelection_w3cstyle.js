// ***********************************************
// NAME     : JourneyAdjustElementSelection_w3cstyle.js
// AUTHOR   : Atos Origin
// ************************************************ 

var UnrefinedText = "unrefined";
var TagPrefixTop_Segment = "highlightCellTop";	// Prefix for cell tag in segment
var TagPrefixMiddle_Segment = "highlightCellMiddle";	// Prefix for cell tag in segment
var TagPrefixBottom_Segment = "highlightCellBottom";	// Prefix for cell tag in segment
var TagPrefix_Table = "highlightRow";	// Prefix for row tag in table
var TickPrefix_Table = "journeyAdjustTableGridControl_detailsRepeater__ctl";	// Prefix for tick image in table
var TickSuffix_Table = "_selectedTick";	// Suffix for tick image in table
var EmptySegmentTagClass = "departline";	// Class for unselected segment cell
var HighlightedSegmentTagClass = "bglinedotted";	// Class for highlighted segment cell
var HighlightedSegmentBottomTagClass = "bglinedottedbottom";	// Class for highlighted bottom segment cell
var HighlightedTableTagClass = "jdtrowhighlight";	// Class for highlighted table cell
var InterchangeIndicator = "_interchange";	// Interchange suffix constant
var SelectedLocationIsInterchange = false;
var SegmentViewMode = "segment";	// Segment view mode constant
var TableViewMode = "table";	// Table view mode constant

// Control function for highlighting user selections in table or segment mode
// legCount is total number of legs in journey.
// arriveBefore is initial user selection of arriveBefore / leaveLater.
function HighlightLocationSelection(legCount, arriveBefore)
{
	var viewMode = GetViewMode();
	
	if (viewMode == SegmentViewMode)
		HandleSegmentLocationSelection(legCount, arriveBefore);
	else
		HandleTableLocationSelection(legCount, arriveBefore);
}

// Control function for highlighting user selections in table or segment mode
function GetViewMode()
{
	// Look at the button - if it is offering "Show in table", it is in Segment mode, and vice versa
	var button = document.getElementById("buttonShowTableDiagram");
	var value = button.value;

	var viewMode = null;

	if (value.indexOf("table") > 0)
		viewMode = SegmentViewMode;
	else
		viewMode = TableViewMode;
		
	return viewMode;
}

// Highlights the appropriate legs based on the location selected in the dropdown
// and whether the search is a leaveAfter or arriveBefore.
// Specifically for diagram control (JourneyAdjustSegmentControl)
function HandleSegmentLocationSelection(legCount, arriveBefore)
{	
	// Clear previous highlighting
	ClearSegmentSelections(legCount);
	
	// Whether to plan an arrive before or leave later
	var selectedIsArriveBefore = GetSelectedIsArriveBefore(arriveBefore);

	// New highlighting from the location selected in the dropdown
	var selectedLegNumber = GetSelectedLegNumber(selectedIsArriveBefore);
	
	if (selectedLegNumber != -1 && selectedIsArriveBefore != "-1")
	{
		// Highlight
		DrawAdjustLines(selectedLegNumber, selectedIsArriveBefore, legCount);
	}
}

// Highlights the appropriate legs based on the location selected in the dropdown
// and whether the search is a leaveAfter or arriveBefore.
// Specifically for table control (JourneyAdjustTableGridControl)
function HandleTableLocationSelection(legCount, arriveBefore)
{		
	// Clear previous highlighting
	ClearTableSelections(legCount);

	// Whether to plan an arrive before or leave later
	var selectedIsArriveBefore = GetSelectedIsArriveBefore(arriveBefore);

	// New highlighting from the location selected in the dropdown
	var selectedLegNumber = GetSelectedLegNumber(selectedIsArriveBefore);

	if (selectedLegNumber != -1 && selectedIsArriveBefore != "-1")
	{
		HighlightAdjustRows(selectedLegNumber, selectedIsArriveBefore, legCount);
	}
}

// Clears all highlights on the page (segment mode)
function ClearSegmentSelections(legCount)
{
	// For each leg represented, get the 3 highlight cells for the leg and remove highlights
	for(i=0; i < legCount; i++)
	{
		var cellTop = document.getElementById(TagPrefixTop_Segment + i);
		if (cellTop != null)
			cellTop.className = '';

		var cellMiddle = document.getElementById(TagPrefixMiddle_Segment + i);
		if (cellMiddle != null)
			cellMiddle.className = '';

		var cellBottom = document.getElementById(TagPrefixBottom_Segment + i);
		if (cellBottom != null)
			cellBottom.className = EmptySegmentTagClass;
		
		// Clear any interchanges (have different ID and rarely present, so check for null)
		var interchangeElement = null;

		interchangeElement = document.getElementById(TagPrefixTop_Segment + i + InterchangeIndicator);
		if (interchangeElement != null)
			interchangeElement.className = '';

		interchangeElement = document.getElementById(TagPrefixBottom_Segment + i + InterchangeIndicator);
		if (interchangeElement != null)
			interchangeElement.className = EmptySegmentTagClass;

	}
}

// Clears all highlights on the page (table mode)
function ClearTableSelections(legCount)
{
	// For each leg represented, get the row and remove highlights
	for(i=0; i < legCount; i++)
	{
		var row = document.getElementById(TagPrefix_Table + i);
		if (row != null)
			row.className = '';

		var tickControlIndex = parseInt(i);
		tickControlIndex++;

		var tick = document.getElementById(TickPrefix_Table + tickControlIndex + TickSuffix_Table);
		if (tick != null)
			tick.style.display='none';
		
	}
}

// Gets the number of the selected location which also represents the leg from which to
// highlight legs selected to adjust from / to.
function GetSelectedLegNumber(selectedIsArriveBefore)
{
	
	// Get the handle to the dropdown
	var dropdown = document.getElementById("adjustOptionsControl_adjustLocations");
	
	// Get the currently selected index
	var selectedIndex = dropdown.selectedIndex;
	// Then the value for current selection
	var selectedValue = dropdown.options[selectedIndex].value;
	
	if (selectedValue.length != 0 && selectedValue != UnrefinedText)
	{
		// See if at interchange point
		var interchangePos = selectedValue.indexOf(InterchangeIndicator);
		if (interchangePos > 0)
		{
			// This is an interchange point - get the value alone and note for future highlighting on segment control
			selectedValue = selectedValue.substring(0, interchangePos);
		}
		
		// See if we user has included the interchange in the selection
		SelectedLocationIsInterchange = false;
		if (selectedIsArriveBefore == "False")
		{
			if (interchangePos > 0)
			{
				SelectedLocationIsInterchange = true;
			}
		}
		else
		{
			if (interchangePos == -1 && IndexHasInterchange(selectedValue))
			{
				SelectedLocationIsInterchange = true;
			}
		
		}
		
		return parseInt(selectedValue);
	}
	else
		return -1;
}

/// For a given index, sees if there is an equivalent value in the dropdown with an interchange,
/// ie, given "4", sees if there is a "4_interchange" value.
function IndexHasInterchange(index)
{
	// Get the handle to the dropdown
	var dropdown = document.getElementById("adjustOptionsControl_adjustLocations");
	var valueToFind = index + InterchangeIndicator;
	var hasInterchange = false;
	
	// Loop around the values & see if ours exists
	for (i = 0; i < dropdown.options.length; i++)
	{
		if (dropdown.options[i].value == valueToFind)
		{
			hasInterchange = true;
			break;
		}
	}
	
	return hasInterchange;
}

/// Notes the arrive before / leave later selection by the user in the timings dropdown
/// Each timings selection represents 2 values - eg
/// 1. ArriveBefore
/// 2. LeaveAfter
/// 3. [ArriveBefore / LeaveAfter] - dependent on initial user search, time adjustment value
function GetSelectedIsArriveBefore(arriveBefore)
{

	// Get the handle to the dropdown
	var dropdown = document.getElementById("adjustOptionsControl_adjustTimings");
	
	// Get the currently selected index
	var selectedIndex = dropdown.selectedIndex;
	// Then the value for current selection
	var selectedValue = dropdown.options[selectedIndex].value;

	if (selectedValue.length != 0 && selectedValue != UnrefinedText)
	{
		switch (selectedValue)
		{
			case "ArriveEarlier":
				return "True";
			case "LeaveLater":
				return "False";
			default:
				// Dependent on the search that the user performed initially
				return arriveBefore;
		}
	}
	else
		return "-1";

}

// Set classes on appropriate cells to draw highlighting lines against given legs
function DrawAdjustLines(selectedLegNumber, selectedIsArriveBefore, legCount)
{
	// If leave after search, then highlight all cells after the selected location, otherwise all cells before
	if (selectedIsArriveBefore == "False")	// (leave after)
	{
		for(i=selectedLegNumber; i < legCount; i++)
		{
			DrawAdjustLine(i, selectedIsArriveBefore, selectedLegNumber);
		}
	}
	else	// (arrive before)
	{
		for(i=selectedLegNumber - 1; i >= 0; i--)
		{
			DrawAdjustLine(i, selectedIsArriveBefore, selectedLegNumber);
		}
	}
}

// Set classes on appropriate cells to draw highlighting line against given leg
function DrawAdjustLine(index, selectedIsArriveBefore, selectedLegNumber)
{

	var cellTop = document.getElementById(TagPrefixTop_Segment + index);
	if (cellTop != null)
		cellTop.className = HighlightedSegmentTagClass;

	var cellMiddle = document.getElementById(TagPrefixMiddle_Segment + index);
	if (cellMiddle != null)
		cellMiddle.className = HighlightedSegmentTagClass;
	
	var cellBottom = document.getElementById(TagPrefixBottom_Segment + index);
	if (cellBottom != null)
		cellBottom.className = HighlightedSegmentBottomTagClass;
	
	
	// Mark any interchanges (have different ID and rarely present, so check for null)
	// If arriveBefore, highlight all interchanges with value less than selected leg and if
	// the selected location is an interchange highlight that too. Opposite for leaveAfter.
	var interchangeElement = null;

	var intIndex = parseInt(index);

	if (selectedIsArriveBefore == "False")	// leave after
	{
		if (SelectedLocationIsInterchange == true)
		{
			interchangeElement = document.getElementById(TagPrefixTop_Segment + index + InterchangeIndicator);
			if (interchangeElement != null)
				interchangeElement.className = HighlightedSegmentTagClass;

			interchangeElement = document.getElementById(TagPrefixBottom_Segment + index + InterchangeIndicator);
			if (interchangeElement != null)
				interchangeElement.className = HighlightedSegmentBottomTagClass;
			}
	}
	else
	{

		// Highlight the selected interchange (labelled a number higher than the selected leg)
		if ((SelectedLocationIsInterchange == true) && (i == (selectedLegNumber - 1)))
		{
			intIndex++;
			
			interchangeElement = document.getElementById(TagPrefixTop_Segment + intIndex + InterchangeIndicator);
			if (interchangeElement != null)
				interchangeElement.className = HighlightedSegmentTagClass;

			interchangeElement = document.getElementById(TagPrefixBottom_Segment + intIndex + InterchangeIndicator);
			if (interchangeElement != null)
				interchangeElement.className = HighlightedSegmentBottomTagClass;
		}

		// Highlight any interchange during the selected part of the journey
		interchangeElement = document.getElementById(TagPrefixTop_Segment + index + InterchangeIndicator);
		if (interchangeElement != null)
			interchangeElement.className = HighlightedSegmentTagClass;

		interchangeElement = document.getElementById(TagPrefixBottom_Segment + index + InterchangeIndicator);
		if (interchangeElement != null)
			interchangeElement.className = HighlightedSegmentBottomTagClass;
	}
	
}

// Set classes on appropriate table rows to highlight given legs
// Additionally, add a tick gif in the 'select' column for the highlighted legs
function HighlightAdjustRows(selectedLegNumber, selectedIsArriveBefore, legCount)
{	
	// If leave after search, then highlight all cells after the selected location, otherwise all cells before
	if (selectedIsArriveBefore == "False")	// (leave after)
	{
		for(i=selectedLegNumber; i < legCount; i++)
		{
			HighlightAdjustRow(i);
		}
	}
	else	// (arrive before)
	{
		for(i=selectedLegNumber - 1; i >= 0; i--)
		{
			HighlightAdjustRow(i);
		}
	}
}

// Set classes on appropriate table row to highlight given leg
function HighlightAdjustRow(index)
{
	// Update the class used for the row to set to highlight
	var row = document.getElementById(TagPrefix_Table + index);
	if (row != null)
		row.className = HighlightedTableTagClass;
	
	// Update the style used by the image in the select column to dictate whether tick image shown or not
	var tickControlIndex = parseInt(index);
	tickControlIndex++;
	
	var tick = document.getElementById(TickPrefix_Table + tickControlIndex + TickSuffix_Table);
	if (tick != null)
		tick.style.display='';
	
}

