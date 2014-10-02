// ***********************************************
// NAME     : AirportBrowseControl_w3cstyle.js
// AUTHOR   : Atos Origin
// ************************************************ 

var firstLine = "";
var airportsTitleLine = "";
var regionsTitleLine = "";
var separatorLine = "";

var codeType_Nothing = 0;
var codeType_Region = 1;
var codeType_Airport = 2;

updateOnEntry();

/* Called when the script is loaded to ensure the correct checkboxes and destination
 * airports are displayed.
 */
function updateOnEntry()
{
	var sourceBox = document.getElementById("airportBrowseFrom_dropMain");
	var targetBox = document.getElementById("airportBrowseTo_dropMain");

	if ( sourceBox != null )
		sourceBox.onchange();
	
	if ( targetBox != null )
		targetBox.onchange();

}

/* Event handler used for the div element that contains all of the region panels.
 * If an INPUT element of type CHECKBOX, or a LABEL element is clicked, the 
 * event handler for the dropdown is invoked. Otherwise the event is ignored.
 */
function handleRegionClick(e, controlName, targetControlName, updateTarget, operatorSelectionControlName)
{
	var targ;

	// Get the source element for the event - this is different for different browsers.
	if (e.target) 
		targ = e.target;
	else if (e.srcElement) 
		targ = e.srcElement;

	// Defeat Safari bug
	if (targ.nodeType == 3) 
		targ = targ.parentNode;

	// See what kind of element the source is
	if ( ((targ.tagName == "INPUT") && (targ.type == "checkbox")) || (targ.tagName == "LABEL") )
		if (targetControlName != "")
		{
			var controlObject = document.getElementById(controlName);
			var targetControlObject = document.getElementById(targetControlName);
			if (updateTarget == true)
			{
				updateDestinationControl(controlObject, targetControlObject);
			}

			var operatorSelectionControl = document.getElementById(operatorSelectionControlName);
			if (operatorSelectionControl != null)
				updateOperatorSelectionControl(operatorSelectionControl, controlObject, targetControlObject);
			
		}
}


/* Event handler used for the dropdown containing airports and regions, and for each
 * of the airport check boxes
 */
function handleAirportSelectionChanged(controlName, targetControlName, updateTarget, operatorSelectionControlName)
{
	// Get a reference to the control
	var controlObject = document.getElementById(controlName);
	var targetControlObject = null;
	// See if we already have the firstLine and separator lines. They have to be retrieved here to ensure
	// that they are subsequently displayed in the correct language.
	if (firstLine.length == 0)
	{
		// Try and retrieve the two standard entries from the specified listbox
		firstLine = controlObject.options[0].text;
		regionsTitleLine = controlObject.options[1].text;
		
		for (var index = 2; ( index < controlObject.options.length ) && ( separatorLine == "" ); index++)
		{
			if (controlObject.options[index].value == "")
			{
				separatorLine = controlObject.options[index].text;
				airportsTitleLine = controlObject.options[index + 1].text;
			}
		}
		
	}
	// Show/hide the appropriate region panels for this control
	showRegionPanel(controlObject);
	
	// If the target control is supplied, deal with that
	if (targetControlName != "")
	{
		targetControlObject = document.getElementById(targetControlName);
		if (updateTarget == true)
		{
			updateDestinationControl(controlObject, targetControlObject);
		}
	}
	
	// See if the operatorSelectionControlName represents a valid object
	var operatorSelectionControl = document.getElementById(operatorSelectionControlName);
	if (operatorSelectionControl != null)
		updateOperatorSelectionControl(operatorSelectionControl, controlObject, targetControlObject);
}

/* For each region in the controlObject dropdown, there will be a DIV element
 * containing airports for that region. This function finds them all and then
 * ensures that only the one corresponding to the current region selection
 * is visible. If a region is not selected, they are all hidden.
 */
function showRegionPanel(controlObject)
{
	// Get all the div elements that are siblings of the specified control.
	var regionPanelPrefix = controlObject.id + "_region_";
	var currSearchId = regionPanelPrefix + controlObject.options[controlObject.selectedIndex].value;
	var currentSelectionIsRegion = (getSelectedValueCodeType(controlObject.options[controlObject.selectedIndex].value) == codeType_Region);

	// IE likes parentElement, Netscape likes parentNode.
	if (controlObject.parentElement)
		allDivElements = controlObject.parentElement.getElementsByTagName("DIV");
	else if (controlObject.parentNode)
		allDivElements = controlObject.parentNode.getElementsByTagName("DIV");

	// If nothing was found, then stop now
	if (allDivElements == null)
		return;

	// Now loop through and ensure only the correct one is shown
	for (var index = 0 ; index < allDivElements.length ; index++)
	{
		if (allDivElements[index].id.substr(0, regionPanelPrefix.length) == regionPanelPrefix)
		{
			if ( (currentSelectionIsRegion == false) || (allDivElements[index].id != currSearchId) )
				allDivElements[index].style.display = "none";
			else if (allDivElements[index].id == currSearchId)
				allDivElements[index].style.display = "";
		}
	}
}

/* Retrieves an array containing all of the airport checkboxes for the control and
 * region specified in the parameters.
 */
function getRegionCheckBoxes(controlObject, regionCode)
{
	var regionPanel = document.getElementById(controlObject.id + "_region_" + regionCode);
	var returnData = new Array();
	var allInputElements = regionPanel.getElementsByTagName("INPUT");
	for (var index = 0; index < allInputElements.length; index++)
	{
		if (allInputElements[index].type == "checkbox")
			returnData[returnData.length] = allInputElements[index];
	}
	
	return returnData;
	
}

/* Retrieves an array of selected airport codes for the specified browse 
 * control object
 */
function getBrowseControlSelectedAirports(controlObject)
{
	var selection = controlObject.options[controlObject.selectedIndex].value;
	var selectionType = getSelectedValueCodeType(selection);
	var selectedAirports = new Array();

	// Now work out the selected origin airports from the dropdown and region panels
	if (selectionType == codeType_Region)
	{
		// A region was selected - work out the appropriate airports
		var regionCheckBoxes = getRegionCheckBoxes(controlObject, selection);
		if (regionCheckBoxes != null)
		{
			// Iterate through the checkboxes building a list of airports
			for (var index = 0; index < regionCheckBoxes.length; index++)
				if ( (regionCheckBoxes[index].disabled == false) && (regionCheckBoxes[index].checked == true) )
					selectedAirports[selectedAirports.length] = regionCheckBoxes[index].value;
			
		}
		// If we're here and have no airports, then we will use all airports for the given region
		if (selectedAirports.length == 0)
		{
			selectedAirports = regionAirports[selection];
		}
	}
	else if (selectionType == codeType_Airport)
	{
		selectedAirports[selectedAirports.length] = selection;
	}
	return selectedAirports;
}

/* Treats originControlObject as containing the selection for origin airports
 * and updates destinationControlObject with valid options according to the
 * routes table
 */
function updateDestinationControl(originControlObject, destinationControlObject)
{
	// Work out the selected origin airports from the dropdown and region panels
	var selectedOriginAirports = getBrowseControlSelectedAirports(originControlObject);
	
	// Get the currently selected item in the destination dropdown
	var destinationSelection = destinationControlObject.options[destinationControlObject.selectedIndex].value;
	
	/* First, clear all but the first entry from the destination dropdown
	 * Note: the options[index] = null syntax must be used rather than the 
	 * options.remove(index) syntax as Netscape only supports the former
	 */
	for (var index = destinationControlObject.options.length; index > 0; index--)
		destinationControlObject.options[index] = null;

	var validDestinationAirports = calculateValidDestinationAirports(selectedOriginAirports);
	var validDestinationRegions = calculateValidDestinationRegions(validDestinationAirports);
	
	var newOption;
	var currentRegionCheckboxes;
	var firstRegion = true;
	var firstAirport = true;
	
	// Add the valid regions to the dropdown
	for (var index = 0; index < regions.length; index++)
		if (validDestinationRegions[regions[index]] == true)
		{
			if (firstRegion == true)
			{
				// Add the region title
				newOption = document.createElement("OPTION");
				newOption.value = "";
				newOption.text = regionsTitleLine;
				destinationControlObject.options[destinationControlObject.options.length] = newOption;
				
				firstRegion = false;
			}
			
			// Add the region to the dropdown
			newOption = document.createElement("OPTION");
			newOption.value = regions[index];
			newOption.text = regionNames[regions[index]];
			destinationControlObject.options[destinationControlObject.options.length] = newOption;
			
			// Update the checkboxes for that region
			currentRegionCheckboxes = getRegionCheckBoxes(destinationControlObject, regions[index]);
			for (var cbIndex = 0; cbIndex < currentRegionCheckboxes.length; cbIndex++)
			{
				if (validDestinationAirports[currentRegionCheckboxes[cbIndex].value])
				{
					if (currentRegionCheckboxes[cbIndex].disabled == true)
					{
						currentRegionCheckboxes[cbIndex].checked = true;
						setCheckboxDisabled(currentRegionCheckboxes[cbIndex], false);
					}
				}
				else
				{
					setCheckboxDisabled(currentRegionCheckboxes[cbIndex], true);
					currentRegionCheckboxes[cbIndex].checked = false;
				}
			}
			
		}

	
	// Add the airport codes
	for (var index = 0; index < airports.length; index++)
		if (validDestinationAirports[airports[index]] == true)
		{
			if (firstAirport == true)
			{
				// Add the separators
				newOption = document.createElement("OPTION");
				newOption.value = "";
				newOption.text = separatorLine;
				destinationControlObject.options[destinationControlObject.options.length] = newOption;

				newOption = document.createElement("OPTION");
				newOption.value = "";
				newOption.text = airportsTitleLine;
				destinationControlObject.options[destinationControlObject.options.length] = newOption;

				firstAirport = false;			
			}
		
			newOption = document.createElement("OPTION");
			newOption.value = airports[index];
			newOption.text = airportNames[airports[index]];
			destinationControlObject.options[destinationControlObject.options.length] = newOption;
		}

	// Try and reinstate the original selection
	if (destinationSelection == "")
		destinationControlObject.selectedIndex = 0;
	else
	{
		// See if the specified value is still in the dropdown
		var foundIndex = 0;
		for (var index = 1; (index < destinationControlObject.options.length) && (foundIndex == 0); index++)
			if (destinationControlObject.options[index].value == destinationSelection)
				foundIndex = index;
		destinationControlObject.selectedIndex = foundIndex;
	}	

	handleAirportSelectionChanged(destinationControlObject.id, '');
}

// Identify whether the item specified in the origin control is:
// a) A "nothing" item (title row or separator) (return value codeType_Nothing)
// b) A region code (return value codeType_Region)
// c) An airport code (return value codeType_Airport)
function getSelectedValueCodeType(valueString)
{
	if (valueString == "")
	{
		return codeType_Nothing;
	}
	else if (isNaN(parseInt(valueString)))
	{
		if (valueString.length == 3)
			return codeType_Airport;
		else
			return codeType_Nothing;
	}
	else
	{
		return codeType_Region;
	}

}

/* Returns a "hashtable" type array with an entry for each airport. The value associated
 * with each airport is a boolean indicating whether the airport is valid or not. The
 * input parameter should be a standard array of airports
 */
function calculateValidDestinationAirports(selectedOriginAirports)
{
	var validAirports = new Array();
	for (var index = 0; index < airports.length; index++)
		validAirports[airports[index]] = false;


	var validDestinationsForOrigin;
	for (var originIndex = 0; originIndex < selectedOriginAirports.length; originIndex++)
	{
		// Find valid destinations from this airport
		validDestinationsForOrigin = routeTable[selectedOriginAirports[originIndex]];
		for (var currDestinationIndex = 0; currDestinationIndex < validDestinationsForOrigin.length; currDestinationIndex++)
			validAirports[validDestinationsForOrigin[currDestinationIndex]] = true;
	}
	return validAirports;
}

/* Given an input parameter returned from calculateValidDestinationAirports, 
 * builds a similar list of valid regions for the valid airports
 */
function calculateValidDestinationRegions(validDestinationAirports)
{
	var validRegions = new Array();
	var currRegionAirports;
		
	for (var regionIndex = 0; regionIndex < regions.length; regionIndex++)
	{
		// Get the airports in this region
		currRegionAirports = regionAirports[regions[regionIndex]];
		validRegions[regions[regionIndex]] = false;
		for (var airportIndex = 0; (airportIndex < airports.length) && !validRegions[regions[regionIndex]]; airportIndex++)
		{
			if (validDestinationAirports[airports[airportIndex]] == true)
				validRegions[regions[regionIndex]] = arrayContains(currRegionAirports, airports[airportIndex]);
		}
	}
	return validRegions;
}

/* Returns true if valueToFind is an element of arrayData, else false
 */
function arrayContains(arrayData, valueToFind)
{
	for (var index = 0; index < arrayData.length; index++)
	{
		if (arrayData[index] == valueToFind)
			return true;
	}
	return false;
}

/* Sets the disabled property of the given checkbox, and 
 * updates its class accordingly.
 */
function setCheckboxDisabled(checkBoxObject, disabledValue)
{
	checkBoxObject.disabled = disabledValue;
	// Need to set the class of the items accordingly
	// Class should be set on the checkbox and its neighbouring label
	var labelObject = checkBoxObject.nextSibling;
	if (disabledValue == true)
	{
		checkBoxObject.className = "txtseveng";
	}
	else
	{
		checkBoxObject.className = "txtseven";
	}
	if ((labelObject != null) && (labelObject.tagName == "LABEL"))
		labelObject.className = checkBoxObject.className;
}

/* Returns true if the two routes are equal, otherwise returns false
 */
function routesEqual(routeA, routeB)
{
	var originA = routeA.substring(0, 2);
	var destinationA = routeA.substring(3, 5);
	var originB = routeB.substring(0, 2);
	var destinationB = routeB.substring(3, 5);
	
	return ( ( (originA == originB) && (destinationA == destinationB) ) || ( (originA == destinationB) && (originB == destinationA) ) );
}

/* Validates the given route. If it is valid, the route as stored in the routes 
 * array is returned. If not, an empty string is returned.
 */
function validRoute(route)
{
	for (var index = 0; index < routes.length; index++)
	{
		if (routesEqual(route, routes[index]))
		{
			return routes[index];
		}
	}
	return "";
}

/* Finds routes involving the specified airport
 */
function getValidRoutesForAirports(airportCodes)
{
	var resultsArray = new Array();
	var currOrigin;
	var currDestination;
	for (var routeIndex = 0; routeIndex < routes.length; routeIndex++)
	{
		currOrigin = routes[routeIndex].substring(0, 3);
		currDestination = routes[routeIndex].substring(3, 6);
		
		// Now loop through the array of airports checking to see if each one is part of the route
		if (arrayContains(airportCodes, currOrigin) || arrayContains(airportCodes, currDestination))
			resultsArray[resultsArray.length] = routes[routeIndex];
	}
	return resultsArray;
}

/* Finds all routes between the specified origin and destination
 * airports.
 */
function getValidRoutes(originAirports, destinationAirports)
{
	var resultsArray = new Array();
	var currValidRoute;
	
	for (var indexA = 0; indexA < originAirports.length; indexA++)
	{
		for (var indexB = 0; indexB < destinationAirports.length; indexB++)
		{
			currValidRoute = validRoute(originAirports[indexA] + destinationAirports[indexB]);
			if ( currValidRoute != "" )
			{
				resultsArray[resultsArray.length] = currValidRoute;
			}
		}
	}
	return resultsArray;
}

/* Updates the available operators on the operator selection control.
 * Unavailable operators are greyed out.
 */
function updateOperatorSelectionControl(operatorSelectionControlObject, controlObjectA, controlObjectB)
{
	if ( (operatorSelectionControlObject == null) || ((controlObjectA == null) && (controlObjectB == null)) )
		return;

	// The operator selection control will be a table containing the checkboxes
	var allInputElements = operatorSelectionControlObject.getElementsByTagName("INPUT");
	if (allInputElements == null)
		return;

	// For each control, if it is a select element, it is a browse control. If it is a hidden input
	// element, it is a display control
	var airportsA;
	var airportsB;

	if (controlObjectA != null)
	{
		if (controlObjectA.tagName == "SELECT")
		{
			airportsA = getBrowseControlSelectedAirports(controlObjectA);
		}
		else if (controlObjectA.tagName == "INPUT")
		{
			// It will contain a space separated list of selected airport codes
			airportsA = controlObjectA.value.split(" ");
		}
	}
	else
	{
		airportsA = new Array();
	}

	if (controlObjectB != null)
	{
		if (controlObjectB.tagName == "SELECT")
		{
			airportsB = getBrowseControlSelectedAirports(controlObjectB);
		}
		else if (controlObjectB.tagName == "INPUT")
		{
			// It will contain a space separated list of selected airport codes
			airportsB = controlObjectB.value.split(" ");
		}
	}
	else
	{
		airportsB = new Array();
	}
	
	// Now calculate routes
	var allRoutes;
	if (airportsA.length != 0 && airportsB.length != 0)
	{
		allRoutes = getValidRoutes(airportsA, airportsB);
	}
	else if (airportsA.length != 0)
	{
		allRoutes = getValidRoutesForAirports(airportsA);
	}
	else if (airportsB.length != 0)
	{
		allRoutes = getValidRoutesForAirports(airportsB);
	}
	else
	{
		// No selection
		allRoutes = new Array();
	}
	// Now we have valid routes, get operators for those routes
	var validOperators = getValidOperators(allRoutes);

	// Finally, go through the operator checkboxes enabling/disabling
	// as necessary.
	for (var index = 0; index < allInputElements.length; index++)
	{
		// See if it's a checkbox
		if (allInputElements[index].type == "checkbox")
		{
			// See if it's valid
			if (validOperators[allInputElements[index].value] == true)
			{
				setCheckboxDisabled(allInputElements[index], false);
			}
			else
			{
				setCheckboxDisabled(allInputElements[index], true);
				allInputElements[index].checked = false;
			}
			
		}
	}
}


/* Finds operators that fly the given routes. The routes must match those in the routes array -
 * ie origin and destination must be the same way round.
 */
function getValidOperators(routesArray)
{
	var validOperators = new Array();
	for (index = 0; index < operators.length; index++)
		validOperators[operators[index]] = false;

	var currRouteOperators;
	for (var index = 0; index < routesArray.length; index++)
	{
		currRouteOperators = routeOperators[routesArray[index]];
		// Add the operators to the list of all valid operators
		for (var operatorIndex = 0; operatorIndex < currRouteOperators.length; operatorIndex++)
			validOperators[currRouteOperators[operatorIndex]] = true;
	}

	return validOperators;
}
