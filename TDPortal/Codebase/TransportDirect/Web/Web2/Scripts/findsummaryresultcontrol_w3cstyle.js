// *********************************************** 
// NAME                 : FindSummaryResultControl_IE4Style.js 
// AUTHOR               : Atos Origin
// DATE CREATED         : 27/09/2004
// DESCRIPTION			: JavaScript functionality for FindSummaryResultControl
// ************************************************ 
// 

/// <summary>
///	Called by the onload event of the selected radio button in the control
///	
/// </summary>
/// <param name="rowId">ID of selected row in table</param>
function showSelectedItem( RowId )
{ 
	if (document.getElementById(RowId))
	{
		var selectedRow = document.getElementById(RowId);
		// This will scroll the DIV so that the selected 
		// row is at the top	
		selectedRow.scrollIntoView(true);
	}
	// Scroll back to the top of the page, as if dealing
	// with the return results, focus will always stay on 
	// return results control
	window.scroll(0, 0);
}
