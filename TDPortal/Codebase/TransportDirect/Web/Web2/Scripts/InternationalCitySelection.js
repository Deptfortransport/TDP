// ***********************************************
// NAME     : InternationalCitySelection.js
// AUTHOR   : Atos Origin
// ************************************************

var codeType_Nothing = 0;
var codeType_City = 1;

updateOnEntry();

/* Called when the script is loaded to ensure the correct origin and destination
*  cities are displayed */
function updateOnEntry() {

    var sourceBox = document.getElementById("locationsControl_fromLocationControl_placeControl_listPlacesScriptable");
    var targetBox = document.getElementById("locationsControl_toLocationControl_placeControl_listPlacesScriptable");

    if (sourceBox != null)
        sourceBox.onchange();

    if (targetBox != null)
        targetBox.onchange();

}

/* Event handler used for the dropdown containing international cities */
function handleInternationalCitySelectionChanged(controlName, targetControlName, updateTarget) {
    // Get a reference to the control
    var controlObject = document.getElementById(controlName);
    var targetControlObject = null;
    
    // If the target control is supplied, deal with that
    if (targetControlName != "") {
        targetControlObject = document.getElementById(targetControlName);
        
        if ((updateTarget == true) && (targetControlObject)) {
            updateDestinationControl(controlObject, targetControlObject);
        }        
    }
}

/* Treats originControlObject as containing the selection for origin cities
*  and updates destinationControlObject with valid options according to the
*  routes table found in InternationalDataDeclarations_W3C_STYLE.js*/
function updateDestinationControl(originControlObject, destinationControlObject) {
    // Work out the selected origin city from the dropdown
    var selectedOriginCity = getControlSelectedCities(originControlObject);

    // Get the currently selected item in the destination dropdown
    var destinationSelection = destinationControlObject.options[destinationControlObject.selectedIndex].value;

    // First, clear all but the first entry from the destination dropdown
    for (var index = destinationControlObject.options.length; index > 0; index--)
        destinationControlObject.options[index] = null;

    var validDestinationCities = calculateValidDestinationCities(selectedOriginCity);

    var newOption;

    // Add the city codes
    for (var index = 0; index < cities.length; index++)
        if (validDestinationCities[cities[index]] == true) {
        
        newOption = document.createElement("OPTION");
        newOption.value = cities[index];
        newOption.text = cityNames[cities[index]];
        destinationControlObject.options[destinationControlObject.options.length] = newOption;
    }

    // Try and reinstate the original selection
    if (destinationControlObject.options.length == 2) {
        // If there is only 1 city in the list, automatically select it
        destinationControlObject.selectedIndex = 1;
    }
    else if (destinationSelection == "") {
        destinationControlObject.selectedIndex = 0;
    }
    else {
        // See if the specified value is still in the dropdown
        var foundIndex = 0;
        for (var index = 1; (index < destinationControlObject.options.length) && (foundIndex == 0); index++) {
            if (destinationControlObject.options[index].value == destinationSelection)
                foundIndex = index;
        }

        destinationControlObject.selectedIndex = foundIndex;
    }
}


/* Retrieves an array of selected city codes for the specified selection
*  control object */
function getControlSelectedCities(controlObject) {
    var selection = controlObject.options[controlObject.selectedIndex].value;
    var selectionType = getSelectedValueCodeType(selection);
    var selectedCities = new Array();

    // Allows scope in future to group cities together based on the selected city,
    // hence return an array of cities
    if (selectionType == codeType_City) {
        selectedCities[selectedCities.length] = selection;
    }

    return selectedCities;
}

/* Identify whether the item specified in the origin control is:
*  a) A "nothing" item (title row or separator) (return value codeType_Nothing)
*  b) A city code (return value codeType_City) */
function getSelectedValueCodeType(valueString) {
    if (valueString == "") {
        return codeType_Nothing;
    }
    else {
        return codeType_City;
    }
}

/* Returns a "hashtable" type array with an entry for each city. The value associated
*  with each city is a boolean indicating whether the city is valid or not. The
*  input parameter should be a standard array of cities */
function calculateValidDestinationCities(selectedOriginCities) {
    
    var validCities = new Array();
    
    for (var index = 0; index < cities.length; index++)
        validCities[cities[index]] = false;


    var validDestinationsForOrigin;
    for (var originIndex = 0; originIndex < selectedOriginCities.length; originIndex++) {
        // Find valid destinations from this city
        validDestinationsForOrigin = cityRouteTable[selectedOriginCities[originIndex]];
        for (var currDestinationIndex = 0; currDestinationIndex < validDestinationsForOrigin.length; currDestinationIndex++)
            validCities[validDestinationsForOrigin[currDestinationIndex]] = true;
    }
    return validCities;
}