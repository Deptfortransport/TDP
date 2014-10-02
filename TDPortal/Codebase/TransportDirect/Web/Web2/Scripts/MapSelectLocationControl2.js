// ***********************************************
// NAME     : MapSelectLocationControl2.js
// AUTHOR   : Atos Origin
// ************************************************ 

var selectNewLocationUpdated = false;
var selectNewLocationEasting;
var selectNewLocationNorthing;
var selectNewLocationText;

// function which takes the selected location object from the locations drop down list,
// ands an info popup on to the map
function mapSelectLocationOK(mapid, selectLocationDropDownId,
selectLocationInfoDivId, selectLocationListDivId, selectLocationErrorDivId) {

    // Get the map to update - use findMap function contained in MapAPI.js
    var map = findMap(mapid);

    if (map) {

        try {

            // Clear any points preivously added
            map.clearPoints();

            
            // Add a point with info popup on the map of the selected location
            // Get the drop down list
            var selectLocationDropDown = document.getElementById(selectLocationDropDownId);

            if (selectLocationDropDown) {

                // Get the selected value (this will be a json object)
                var listValue = selectLocationDropDown.options[selectLocationDropDown.selectedIndex].value;
                var listText = selectLocationDropDown.options[selectLocationDropDown.selectedIndex].text;

                var locationObject = dojo.fromJson(listValue);

                if (locationObject) {

                    // Coordinates
                    var points = locationObject.points;

                    var easting = points[0].x;
                    var northing = points[0].y;

                    // Get current scale
                    var mapScale = map.getMapProperties().scale;

                    // Add to map and zoom to location
                    map.zoomToScaleAndPoint(easting, northing, mapScale, listText);

                    // Update the parameters to return back to previously selected location,
                    // for when user does another search
                    selectNewLocationEasting = easting;
                    selectNewLocationNorthing = northing;
                    selectNewLocationText = listText;
                    selectNewLocationUpdated = true;

                }

            }

            resetLocationInfoDiv(selectLocationInfoDivId, selectLocationListDivId, selectLocationErrorDivId);

        }
        catch (err) {

            //alert(err);

        }
    }

    return false;
}

// function which cancels the Selecing a new location on the map, removing any points added,
// and hiding the select location drop down
function mapSelectLocationCancel(mapid, selectLocationDropDownId,
selectLocationInfoDivId, selectLocationListDivId, selectLocationErrorDivId,
easting, northing, locationText, error) {

    // Get the map to update - use findMap function contained in MapAPI.js
    var map = findMap(mapid);

    if (map) {

        try {

            // Get the locations dropdown list to populate and reset
            var selectLocationDropDown = document.getElementById(selectLocationDropDownId);
            if (selectLocationDropDown != null) {
                selectLocationDropDown.length = 0;
            }

            resetLocationInfoDiv(selectLocationInfoDivId, selectLocationListDivId, selectLocationErrorDivId);

            // Clear any points added
            map.clearPoints();

            // if error, no locations were found, so leave user where they are
            if (!error) {

                var mapScale = map.getMapProperties().scale;
                
                // Go back to the original location, or the previously selected location
                if (!selectNewLocationUpdated) {
                    if (easting && northing && locationText) {
                        map.zoomToScaleAndPoint(easting, northing, mapScale, locationText);
                    }
                }
                else {
                    if (selectNewLocationEasting && selectNewLocationNorthing && selectNewLocationText) {
                        map.zoomToScaleAndPoint(selectNewLocationEasting, selectNewLocationNorthing, mapScale, selectNewLocationText);
                    }
                }
            }
        }
        catch (err) {

            //alert(err);

        }
    }
    
    return false;
}

// function which shows the location info divs and hides the list and error divs
function resetLocationInfoDiv(selectLocationInfoDivId, selectLocationListDivId, selectLocationErrorDivId) {

    // Show the location info div
    var selectLocationInfoDiv = document.getElementById(selectLocationInfoDivId);
    if (selectLocationInfoDiv != null) {
        selectLocationInfoDiv.className = selectLocationInfoDiv.className.replace("hide", "show");
    }

    // Hide the select location list div
    var selectLocationDiv = document.getElementById(selectLocationListDivId);
    if (selectLocationDiv != null) {
        selectLocationDiv.className = selectLocationDiv.className.replace("show", "hide");
    }

    // Hide the location list error div
    var selectLocationErrorDiv = document.getElementById(selectLocationErrorDivId);
    if (selectLocationErrorDiv != null) {
        selectLocationErrorDiv.className = selectLocationErrorDiv.className.replace("show", "hide");
    }
    
}