// ***********************************************
// NAME     : MapJourneyDisplayDetailsControl.js
// AUTHOR   : Atos Origin
// ************************************************ 

// These indicate which zoom method to use when zooming to a journey
var JOURNEY_ZOOM_JUNCTION = "J";
var JOURNEY_ZOOM_TOID = "T";
var JOURNEY_ZOOM_ENVELOPE = "E";
var JOURNEY_ZOOM_POINT = "P";
var JOURNEY_ZOOM_ALLROUTES = "AR";

// These indicate which add symbol method to use when adding symbols for a road jouney
var JOURNEY_ADDSYMBOL_JUNCTION = "J";
var JOURNEY_ADDSYMBOL_NODE = "N";
var JOURNEY_ADDSYMBOL_POINT = "P";

// Max scale at which road journey direction symbols should be visible
var maxMapRoadJourneyDirectionSymbolsScale = 8000;
var maxMapCycleJourneyDirectionSymbolsScale = 8000;
var mapRoadJourneyDirectionSymbolsAdded = false;
var mapRoadJourneyDirectionSymbolsCoordinates = new Array();
var mapCycleJourneyDirectionSymbolsAdded = false;
var mapFerryAndTollsSymbolsAdded = false;

// Function which zooms to a journey segment for a journey already shown on a map, using a zoom value 
// obtained from the journey segment drop down list
function zoomJourneySegmentOnMap(mapid, sessionId, routeNumber, journeyType, journeySegmentDropDownListId) {

    // Get the drop down list to find where to zoom to
    var dropDownList = document.getElementById(journeySegmentDropDownListId);

    if (dropDownList) {

        // Get the selected value
        var listValue = dropDownList.options[dropDownList.selectedIndex].value;

        zoomJourneySegment(mapid, sessionId, routeNumber, journeyType, listValue);
    }
    
    return false;
}

// Function which zooms to a road journey direction when the direction link is selected from 
// the road journey details page
function zoomRoadJourneyDetailMap(mapid, sessionId, routeNumber, journeyType, 
journeySegmentDropDownListId, journeySegmentDropDownListSelectedIndex, scrollToControl) {

    // Scroll page to the map
    scrollToElement(scrollToControl);
    
    // Change the journey segments drop down list, selected value to match the road journey details link clicked
    var dropDownList = document.getElementById(journeySegmentDropDownListId);

    if (dropDownList) {

        // Set the selected value to 
        dropDownList.selectedIndex = journeySegmentDropDownListSelectedIndex;

        // Then zoom the map
        zoomJourneySegmentOnMap(mapid, sessionId, routeNumber, journeyType, journeySegmentDropDownListId)
        
    }
    
    return false;
}

// Function which zooms to a cycle journey direction when the direction link is selected from 
// the cycle journey details page
function zoomCycleJourneyDetailMap(mapid, sessionId, routeNumber, journeyType,
journeySegmentDropDownListId, journeySegmentDropDownListSelectedIndex, scrollToControl) {

    // Scroll page to the map
    scrollToElement(scrollToControl);

    // Change the journey segments drop down list, selected value to match the road journey details link clicked
    var dropDownList = document.getElementById(journeySegmentDropDownListId);

    if (dropDownList) {

        // Set the selected value to 
        dropDownList.selectedIndex = journeySegmentDropDownListSelectedIndex;

        // Then zoom the map
        zoomJourneySegmentOnMap(mapid, sessionId, routeNumber, journeyType, journeySegmentDropDownListId)

    }

    return false;
}

// Function which zooms to a journey segment when the map icon is selected from the journey details page
function zoomJourneyDetailMap(mapid, sessionId, routeNumber, journeyType, zoomValue, 
journeySegmentDropDownListId, journeySegmentDropDownListSelectedIndex, scrollToControl) {

    // Zoom the map
    if (zoomValue) {
        
        zoomJourneySegment(mapid, sessionId, routeNumber, journeyType, zoomValue);
    }

    // Change the journey segments drop down list, selected value to match the journey details map link clicked
    var dropDownList = document.getElementById(journeySegmentDropDownListId);

    if (dropDownList) {

        // Set the selected value to 
        dropDownList.selectedIndex = journeySegmentDropDownListSelectedIndex;
        
    }

    // Scroll page to the map
    scrollToElement(scrollToControl);
    
    return false;
}


// Function which zooms to a journey segment for a journey already shown on a map
function zoomJourneySegment(mapid, sessionId, routeNumber, journeyType, zoomValue) {

    // Get the map to update
    var map = findMap(mapid);
        
    if (map) {
    
        try {

            // Split the drop down list item values into an array
            var zoomValuesArray = new Array();

            if (zoomValue.indexOf(",") > 0) {
                zoomValuesArray = zoomValue.split(",");
            }
            else {
                zoomValuesArray[0] = zoomValue;
            }


            // If its the first item in the drop down list, then zoom to the entire journey.
            // If theres > 1 item, then a different zoom method is used
            if (((zoomValue == undefined) || (zoomValue == null) || (zoomValuesArray[0] == 0))
                &&
                (zoomValuesArray.length == 1)
               ) {

                map.zoomToRoute(sessionId, routeNumber, journeyType);
                
            }
            // Determine the zoom method to use based on the second item in the array
            else if (zoomValuesArray.length >= 2) {
            
                // Position 2 indicates the zoom type
                var zoomType = zoomValuesArray[1];
                
                switch (zoomType) {

                    case JOURNEY_ZOOM_ALLROUTES:
                        zoomJourneyToAllAdded(map);
                        break;
                        
                    case JOURNEY_ZOOM_JUNCTION:
                        if (zoomValuesArray.length == 4) {
                        
                            // Get the ITN Nodes
                            var firstITNNode = zoomValuesArray[2];
                            var lastITNNode = zoomValuesArray[3];

                            zoomJourneyToJunction(map, firstITNNode, lastITNNode);    
                        }
                        break;
                        
                    case JOURNEY_ZOOM_TOID:
                        if (zoomValuesArray.length == 3) {
                        
                            // Get the ITN Node
                            var itnNode = zoomValuesArray[2];

                            zoomJourneyToITNNode(map, itnNode);    
                        }
                        break;

                    case JOURNEY_ZOOM_ENVELOPE:
                        if (zoomValuesArray.length == 6) {

                            // Get the coordiantes
                            var minX = zoomValuesArray[2];
                            var minY = zoomValuesArray[3];
                            var maxX = zoomValuesArray[4];
                            var maxY = zoomValuesArray[5];

                            zoomJourneyToEnvelope(map, minX, minY, maxX, maxY);
                        }
                        break;
                        
                    case JOURNEY_ZOOM_POINT:
                        if (zoomValuesArray.length == 4) {
                        
                            // Get the coordiante
                            var x = zoomValuesArray[2];
                            var y = zoomValuesArray[3];

                            zoomJourneyToPoint(map, x, y);
                        }
                        break;
                
                } // End switch
            }
        }
        catch (err) {

            //alert(err);

        }
    }
    
    return false;
}

// Function which will zoom the journey shown on a map to a junction based on the first and last ITNNode
function zoomJourneyToJunction(map, firstITNNode, lastITNNode){

    if (map) {
        try {
            if (firstITNNode && lastITNNode) {
                map.findJunctionPoint(firstITNNode, lastITNNode, 12, true);
            }
        }
        catch (err) {
            //alert(err);
        }
    }
}

// Function which will zoom the journey shown on a map to an ITNNode
function zoomJourneyToITNNode(map, itnNode) {

    if (map) {
        try {
            if (itnNode) {
                map.findITNNodePoint(itnNode, 12, true);
            }
        }
        catch (err) {
            //alert(err);
        }
    }
}

// Function which will zoom the journey shown on a map to a defined envelope
function zoomJourneyToEnvelope(map, minX, minY, maxX, maxY) {

    if (map) {
        try {
            if (minX && minY && maxX && maxY) {
                map.zoomToExtent(minX, minY, maxX, maxY, true)
            }
        }
        catch (err) {
            //alert(err);
        }
    }
}

// Function which will zoom the journey shown on a map to a defined point
function zoomJourneyToPoint(map, x, y) {

    if (map) {
        try {
            if (x && y) {
                map.zoomToScaleAndPoint(x, y, 4000);
            }
        }
        catch (err) {
            //alert(err);
        }
    }
}

// Function which will zoom to all added PT and Road journeys shown on the map
function zoomJourneyToAllAdded(map) {

    if (map) {
        try {

            map.zoomtoAllAddedRoutes();

        }
        catch (err) {
            //alert(err);
        }
    }
}

// Function which is called by registering to the onMapInitialiseComplete event
// Zooms the journey map to the specified envelope
function eventZoomJourneyToEnvelope(/*map argument passed by onMapInitialiseComplete event*/mapArgs,
minX, minY, maxX, maxY) {

    if (mapArgs.map) {

        zoomJourneyToEnvelope(mapArgs.map, minX, minY, maxX, maxY);

    }
}

// Function which is called by registering to the onMapInitialiseComplete event
// Zooms the journey map to all the routes (PT, Car) added
function eventZoomJourneyToAllAdded(/*map argument passed by onMapInitialiseComplete event*/mapArgs) {

    if (mapArgs.map) {

        zoomJourneyToAllAdded(mapArgs.map);

    }
}

// Function which checks if any ferry or toll symbols need to be added to the map and adds them
function addFerryAndTollSymbols() {

    if (!mapFerryAndTollsSymbolsAdded) {
    
        // Hidden control containing the symbols list
        var symbolsToDisplayControlOutward = "mapJourneyControlOutward_FerryAndTollSymbols";
        var symbolsToDisplayControlReturn = "mapJourneyControlReturn_FerryAndTollSymbols";

        // Get the hidden symbols list
        var outwardSymbolsControl = document.getElementById(symbolsToDisplayControlOutward);
        var returnSymbolsControl = document.getElementById(symbolsToDisplayControlReturn);

        addSymbolsToMap(outwardSymbolsControl, false);
        addSymbolsToMap(returnSymbolsControl, false);

        mapFerryAndTollsSymbolsAdded = true;
    }
}


// Function which is called by registering to the onMapExtentChange event
// Function which checks if any road journey direction symbols need to be added to the map and adds them
function addRoadJourneyDirectionSymbols(mapid, /*map argument passed by mapExtentChanged event*/mapArgs) {

    var map = findMap(mapid);
    
    if (map) {
        if (mapArgs.levelChange) {
            //if map scale is less than or equal to maxMapRoadJourneyDirections, show the symbols
            if (mapArgs.scale <= maxMapRoadJourneyDirectionSymbolsScale) {

                if (!mapRoadJourneyDirectionSymbolsAdded) {
                
                    // Hidden control containing the symbols list
                    var symbolsToDisplayControlOutward = "mapJourneyControlOutward_RoadJourneyDirectionSymbols";
                    var symbolsToDisplayControlReturn = "mapJourneyControlReturn_RoadJourneyDirectionSymbols";

                    // Get the hidden symbols list
                    var outwardSymbolsControl = document.getElementById(symbolsToDisplayControlOutward);
                    var returnSymbolsControl = document.getElementById(symbolsToDisplayControlReturn);

                    // No longer show direction symbols/popups on map. Code removed.                    
                }
            }
            else {
            
                // Only clear road direction symbols if flagged as being added.
                if (mapRoadJourneyDirectionSymbolsAdded) {
                    // Remove the symbols and directions
                    map.clearPoints('symbol');

                    mapRoadJourneyDirectionSymbolsAdded = false;

                    // Re-add the Ferry/Tolls if they were cleared
                    mapFerryAndTollsSymbolsAdded = false;
                    addFerryAndTollSymbols();
                }
            }
        }
    }
}

// Function which is called by registering to the onMapExtentChange event
// Function which checks if any cycle journey direction symbols need to be added to the map and adds them
function addCycleJourneyDirectionSymbols(mapid, /*map argument passed by mapExtentChanged event*/mapArgs) {

    var map = findMap(mapid);

    if (map) {
        if (mapArgs.levelChange) {
            //if map scale is less than or equal to maxMapCycleJourneyDirections, show the symbols
            if (mapArgs.scale <= maxMapCycleJourneyDirectionSymbolsScale) {

                if (!mapCycleJourneyDirectionSymbolsAdded) {

                    // Hidden control containing the symbols list
                    var symbolsToDisplayControlOutward = "mapJourneyControlOutward_CycleJourneyDirectionSymbols";
                    var symbolsToDisplayControlReturn = "mapJourneyControlReturn_CycleJourneyDirectionSymbols";

                    // Get the hidden symbols list
                    var outwardSymbolsControl = document.getElementById(symbolsToDisplayControlOutward);
                    var returnSymbolsControl = document.getElementById(symbolsToDisplayControlReturn);

                    // No longer show direction symbols/popups on map. Code removed.
                }
            }
            else {

                // Only clear road direction symbols if flagged as being added.
                if (mapCycleJourneyDirectionSymbolsAdded) {
                    // Remove the symbols and directions
                    map.clearPoints('symbol');

                    mapCycleJourneyDirectionSymbolsAdded = false;

                    // Re-add the Ferry/Tolls if they were cleared
                    mapFerryAndTollsSymbolsAdded = false;
                    addFerryAndTollSymbols();
                }
            }
        }
    }
}

// Function which accepts a control containing a list of symbols and ITNJunctionPoints/ITNNodes to display, in its value property.
// First item in the value property is the map id
function addSymbolsToMap(symbolsControl, isITNJunctionPoints) {

    if (symbolsControl) {

        // Split the list values into an array
        var symbols = new Array();

        if (symbolsControl.value.indexOf(":::") > 0) {

            symbols = symbolsControl.value.split(":::");

            // First item in list should be the map id
            var mapid = symbols[0];

            // Get the map to update
            var map = findMap(mapid);

            if (map) {

                if (isITNJunctionPoints) {

                    // Used to get an item out of a previously saved coordinates array
                    var x = 0;
                    
                    // Then remaining items are in groups - add symbol method, symbol, ITN 1, ITN 2, direction text
                    for (var i = 1; i < symbols.length; i = i + 5) {

                        var addSymbolType =  symbols[i];
                        var symbol = symbols[i + 1];
                        var itn1 = symbols[i + 2]; // value may also be a coordinate
                        var itn2 = symbols[i + 3]; // value may also be a coordinate
                        var directionText = symbols[i + 4];
                        
                        switch (addSymbolType) {

                            case JOURNEY_ADDSYMBOL_JUNCTION:
                            
                                // Check if coordinates were set up
                                if ((mapRoadJourneyDirectionSymbolsCoordinates != null)
                                    && (mapRoadJourneyDirectionSymbolsCoordinates.length >= (x + 1) )) {

                                    // Coordiantes were saved previously, so reuse to add the symbols
                                    // more quickly, rather than doing te longer ITN junction point lookup
                                    var coordX = mapRoadJourneyDirectionSymbolsCoordinates[x];
                                    var coordY = mapRoadJourneyDirectionSymbolsCoordinates[x + 1];
                                    map.addStartEndPt({ x: coordX, y: coordY, type: 'symbol', infoWindowRequired: true, content: directionText, label: ' ', symbolKey: symbol });
                                
                                }
                                else {
                                    // Coordinates haven't been set up, so call ITN method and add it
                                    addSymbolToMapITNJunctionPoint(map, symbol, itn1, itn2, directionText);
                                }

                                x = x + 2;
                                break;

                            case JOURNEY_ADDSYMBOL_POINT:
                                map.addStartEndPt({ x: itn1, y: itn2, type: 'symbol', infoWindowRequired: true, content: directionText, label: ' ', symbolKey: symbol });
                                break;
                        }
                    }
                }
                else {

                    // Then remaining items are in groups - add symbol method, symbol, ITNNode
                    for (var i = 1; i < symbols.length; i = i + 3) {

                        var addSymbolType = symbols[i];
                        var symbol = symbols[i + 1];
                        var itnNode = symbols[i + 2];

                        switch (addSymbolType) {

                            case JOURNEY_ADDSYMBOL_NODE:
                                addSymbolToMapITNNode(map, symbol, itnNode);
                                break;
                        }
                    }
                }
            }
        }
        // Else, control not correctly set so don't attempt
    }
}

// Function which adds a symbol icon to the map at the specified ITNNode
function addSymbolToMapITNNode(map, symbol, itnNode) {

    if (map) {
        try {
            if (itnNode) {

                // Get the coordinates of this ITNNode
                var mapPoint = map.findITNNodePoint(itnNode);

                if (mapPoint) {

                    var coordX = mapPoint.x;
                    var coordY = mapPoint.y;
                    
                    // Add the symbol icon
                    map.addStartEndPt({ x: coordX, y: coordY, type: 'symbol', infoWindowRequired: false, content: '', label: ' ', symbolKey: symbol });
                }
            }
        }
        catch (err) {
            //alert(err);
        }
    }
}

// Function which adds a symbol icon to the map at the specified ITNNode
function addSymbolToMapITNJunctionPoint(map, symbol, itn1, itn2, labeltext) {

    if (map) {
        try {
            if (itn1 && itn2) {

                // Get the coordinates of the junction of the ITNs
                var mapPoint = map.findJunctionPoint(itn1, itn2);

                if (mapPoint) {

                    var coordX = mapPoint.x;
                    var coordY = mapPoint.y;

                    // Add the symbol icon
                    map.addStartEndPt({ x: coordX, y: coordY, type: 'symbol', infoWindowRequired: true, content: labeltext, label: ' ', symbolKey: symbol });
                    
                    // Store the coordinates for use again if user zooms in/out on the map (quicker adding of symbols)
                    mapRoadJourneyDirectionSymbolsCoordinates.push(coordX);
                    mapRoadJourneyDirectionSymbolsCoordinates.push(coordY);
                }
            }
        }
        catch (err) {
            //alert(err);
        }
    }
}