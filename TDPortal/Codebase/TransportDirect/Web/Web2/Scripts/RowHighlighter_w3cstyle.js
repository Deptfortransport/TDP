// ************************************************************************ 
// NAME                 : RowHighlighter_w3cstyle.js 
// AUTHOR               : Atos Origin
// DATE CREATED         : 20/01/05
// DESCRIPTION			: JavaScript functionality for highlighting a row 
//						in a table
// ************************************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Scripts/RowHighlighter_w3cstyle.js-arc  $
//
//   Rev 1.3   Mar 19 2010 13:39:22   mmodi
//Added header to file
//Resolution for 5471: Maps - Code Review - Add headers to Javascript files
//
//   Rev 1.2   Mar 31 2008 13:26:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:28:40   mturner
//Initial revision.
//
//   Rev 1.3   Mar 14 2005 13:46:06   rgeraghty
//Corrected check for input type
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.2   Feb 22 2005 11:10:40   croberts
//Added comment markers for last check in

//   Rev 1.1   Feb 18 2005 15:24:52   COwczarek
//Made more generic, allowing CSS class of each row type to be
//supplied. Also no longer necessary to specify row id.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview

/// <summary>
///	Iterates through each row of the specified table setting the style of each
/// to one of the specified CSS classes as follows: odd rows set to rowClass,
/// even rows set to altRowClass and the row containing a radio button that is
/// checked is set to selectedRowClass. This function depends on there being only
/// on radio button per row.
/// </summary>
/// <param name="tabledId">ID of table in which selected row resides</param>
/// <param name="rowClass">CSS class to use for odd rows</param>
/// <param name="altRowClass">CSS class to use for even rows</param>
/// <param name="selectedRowClass">CSS class to use for row containing a radio button
/// that is checked</param>
function highlightSelectedItem( tableId, rowClass, altRowClass, selectedRowClass )
{ 	

	// find the required table
	var rowTable = document.getElementById(tableId);	

	// get rows from table
	var rows = rowTable.getElementsByTagName("tr");
							
	// highlight each row accordingly
	for (i=0; i<rows.length; i++)
	{		

		var element = rows[i].getElementsByTagName("INPUT");

		for( var j=0; j< element.length; j++)
		{

			if (element[j].type == "radio") {
			
				if (element[j].checked) {
					rows[i].className = selectedRowClass;
				} else if (i % 2 == 0) {
					rows[i].className = rowClass;
				} else {
					rows[i].className = altRowClass;
				}
			}		
		}
		
	}				
		
}