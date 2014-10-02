// ***********************************************
// NAME     : stationresultstable_w3cstyle.js
// AUTHOR   : Atos Origin
// ************************************************ 

// When tickAll is clicked, applies checked status to all checkboxes
// in the table
function tickAll_Click(tickAllRowIndex, firstCheckBoxRowIndex)
{
	// get table
	var stationResultsTable = document.getElementById("stationResultsTable");
	
	
	
	// get rows in table
	var allTableRows = stationResultsTable.getElementsByTagName("tr");
	
	
	//2nd row is where the tickAll check box is
	var rowTickAll = allTableRows[tickAllRowIndex];
	
	var rowAllElements = rowTickAll.getElementsByTagName("INPUT");
	

	
	var checked = true;
	for(var index= 0; index <rowAllElements.length ; index++)
	{
		if (rowAllElements[index].type == "checkbox")
		{
			checked = rowAllElements[index].checked
		}
	}
	
	for(var i=firstCheckBoxRowIndex; i<allTableRows.length ; i++)
	{	
		var rowElements = allTableRows[i].getElementsByTagName("INPUT");
		for( var j=0; j< rowElements.length; j++)
		{
			if (rowElements[j].type == "checkbox")
			{
				rowElements[j].checked = checked;
			}
		}
	}
	
	
	
}
