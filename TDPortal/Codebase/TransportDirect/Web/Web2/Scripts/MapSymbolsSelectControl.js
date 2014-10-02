// ***********************************************
// NAME     : MapSymbolsSelectControl.js
// AUTHOR   : Atos Origin
// ************************************************ 

// Common properties to use with symbol control

var panelViews = new Array("_panelOnlyView", "_panelKeys");

var keyOtherOptionstables = new Array("tableAccommodation", "tableSport", "tableAttractions", "tableHealth", "tableEducation", "tableInfrastructure")

var keyOtherOptions = new Array("commandAccommodation", "commandSport", "commandAttractions", "commandHealth", "commandEducation", "commandInfrastructure");

var transportImageSelectedPartial = "checked.gif";

var transportImageUnSelectedPartial = "unchecked.gif";

var imageSelectedPartial = "_checked.gif";

var imageUnSelectedPartial = "_uncheck.gif";

var transportSelectAllPartial = "_commandTransport";

var transportTable = "_tableTransport";

var pointXContainerId = "_pointXKeysContainer";

var travelNewsContainerId = "_travelNewsContainer";

var toggleTravelNewsAndSymbolsId = "_buttonToggleTravelNewsAndSymbols";

var publicIncidentCheckboxId = "_publicIncidentsVisible";

var roadIncidentCheckboxId = "_roadIncidentsVisible";

// Max scale at which symbols should be visible
var maxMapSymbolsScale = 40000;

// Max scale at which bus symbols should be visible
var maxBusSymbolMapScale = 4000;

var setLayerForFirstTime = true;


// This function gets called whenever mapExtentChanged event fired to set
// wether panel only view or panel with all the keys should be visible
// This method also sets up default symbols to show on map

// This method will only be called if page subscribed to one of the event published by ESRIUKTDPAPI mapExtentChagned event handler

function setMapSymbolLayers(/*client id of the container of panel key and panel only control*/ctrl, /*client id of map*/mapid, /*map argument passed by mapExtentChanged event*/mapArgs) {
  
    
    var map = findMap(mapid);

    var panelOnlyViewElm = document.getElementById(ctrl + panelViews[0]);

    var panelKeysBoxElm = document.getElementById(ctrl + panelViews[1]);

    if (map) {
        if (mapArgs.levelChange || setLayerForFirstTime) { // modified as causing problems with find park and ride
             
            //if map scale is less than or equal to maxMapSymbolScale show the symbol keys
            if (mapArgs.scale <= maxMapSymbolsScale) {
                panelOnlyViewElm.className = panelOnlyViewElm.className.replace("show", "hide");
                panelKeysBoxElm.className = panelKeysBoxElm.className.replace("hide", "show");
                setLayers(ctrl, mapid, true); // Set the default keys on the map
            }
            else {
                hideLayers(mapid);
                panelOnlyViewElm.className = panelOnlyViewElm.className.replace("hide", "show");
                panelKeysBoxElm.className = panelKeysBoxElm.className.replace("show", "hide");
            }
            setLayerForFirstTime = false;
        }
    }

}

// Toggles the state of the transport key options checkboxes
function toggleTransportOptionsAll(ctrl, mapid) {
    
        // Transport options toggle check box
        var transportCheckBox = document.getElementById(ctrl + transportSelectAllPartial);

        // Transport key symbol options tabel
        var tableCtrl = document.getElementById(ctrl + transportTable);

        // Transport options check box state i.e. if checked all selected
        var isAllSelected = transportCheckBox.src.toLowerCase().indexOf(transportImageUnSelectedPartial) < 0;

        // Get the trasport table key symbol check boxes
        var transportCheckBoxes = tableCtrl.getElementsByTagName("input");

        // Toggle transport option toggle check box's image
        if(isAllSelected)
        {
           transportCheckBox.src = transportCheckBox.src.toLowerCase().replace(transportImageSelectedPartial, transportImageUnSelectedPartial);
        }
        else
        {

           transportCheckBox.src = transportCheckBox.src.toLowerCase().replace(transportImageUnSelectedPartial, transportImageSelectedPartial);
        }

        // Toggle the key symbol check box state
        for (var checkboxid in transportCheckBoxes) {
            if (transportCheckBoxes[checkboxid].type == "checkbox") {

                transportCheckBoxes[checkboxid].checked = !isAllSelected;
                           
            }
        }   
        
    
    
    return false;
}

// This function iterates through the table and set the table with tableId specified as visible to user
function showOtherOption(/*Container which hosts the key controls */ctrl, tableId) {

    for (keyTable in keyOtherOptionstables) {

        var keyTableValue = "_" + keyOtherOptionstables[keyTable]; // postfix for the key symbols table
        var commandKeyValue = "_" + keyOtherOptions[keyTable]; // postfix for the command radio button image for the table
        var tableCtrl = document.getElementById(ctrl + keyTableValue); // key symbols table
        var commandCtrl = document.getElementById(ctrl + commandKeyValue); // command radio button image for the table

        // If the tableId passed as argument is the id of the key table show that table
        // and set all the symbolkey check boxes checked
        if (tableId.indexOf(keyTableValue) > -1) {

            if (tableCtrl.className.indexOf(" hide") > -1) {
                tableCtrl.className = tableCtrl.className.replace(" hide", "");
                commandCtrl.src = commandCtrl.src.toLowerCase().replace(imageUnSelectedPartial, imageSelectedPartial);

                // Get check boxes in the key symbols table
                var tableCtrlCheckBoxes = tableCtrl.getElementsByTagName("input");

                for (var checkboxid in tableCtrlCheckBoxes) {
                    if (tableCtrlCheckBoxes[checkboxid].type == "checkbox") {
                        tableCtrlCheckBoxes[checkboxid].checked = true;
                    }
                }
            }
        }
        // Otherwise hide the table, and set all the symbolkey check boxes as unchecked
        else {
            if (tableCtrl.className.indexOf(" hide") < 0) {
                tableCtrl.className = tableCtrl.className + " hide";
                if (commandCtrl.src.toLowerCase().indexOf(imageUnSelectedPartial) < 0) {
                    commandCtrl.src = commandCtrl.src.toLowerCase().replace(imageSelectedPartial, imageUnSelectedPartial);
                }

                // Get check boxes in the key symbols table
                var tableCtrlCheckBoxes = tableCtrl.getElementsByTagName("input");

                for (var checkboxid in tableCtrlCheckBoxes) {
                    if (tableCtrlCheckBoxes[checkboxid].type == "checkbox") {
                        tableCtrlCheckBoxes[checkboxid].checked = false;
                    }
                }
            }
        }
    }
    
}

// Sets the Symbol keys on the map
function setLayers(/*Container which hosts the key controls */ctrl, mapid, isInit) {

    if (isInit == undefined) {
        isInit = false;
    }
    var mapSymbolsSelectControlId = "mapJourneyControlOutward_mapSymbolsSelectControl";
    var dateErrorPanel = document.getElementById(mapSymbolsSelectControlId + "_dateErrorPanel");
    var dateRowElement = document.getElementById(mapSymbolsSelectControlId + "_dateSelect_datePanel");

    // hide invalide date error at the start
    if (dateErrorPanel) {
        dateErrorPanel.style.display = "none";
        if (dateRowElement) {
            dateRowElement.className = "";
        }
    }

    var invalidDate = true;

    var dateDayElm = document.getElementById(mapSymbolsSelectControlId + "_dateSelect_listDays");

    var dateMonthYearElm = document.getElementById(mapSymbolsSelectControlId + "_dateSelect_listMonths");

    if (dateDayElm && dateMonthYearElm) {
        tnDateOnly = dateDayElm.value + "/" + dateMonthYearElm.value;

        invalidDate = isDate(tnDateOnly, /^([0-9]{1,2})[\/]([0-9]{1,2})[\/]([0-9]{1,4})$/, { d: 1, m: 2, y: 3 }) > 0;

        dateArr = tnDateOnly.split("/");
    }

    if (!invalidDate && dateDayElm && dateMonthYearElm) {
        try {
            // date is valid so create Date object and check if the date is in past
            invalidDate = isDateInPast(new Date(dateArr[2], new Number(dateArr[1]) - 1/*Date object takes 0-11 value for month*/, dateArr[0]));
        }
        catch (error) {
            // we got an error, set invalidDate to true;
            invalidDate = true;
        }
    }

    //if date selected for travel news is not valid display an error
    // validate date in dd/mm/yyyy format
    if (invalidDate && dateDayElm && dateMonthYearElm) {

        if (dateErrorPanel) {
            dateErrorPanel.style.display = "";
            if (dateRowElement) {
                dateRowElement.className = "alerterror";
            }
        }
    }
    else {
        var layerState = null;

        var map = findMap(mapid);

        if (map) {
            try {

                layerState = map.getLayerList(); // get the currently displayed layer(symbol keys) list

                var travelNewsContainer = document.getElementById(ctrl + travelNewsContainerId);

                var travelNewsVisible = false;

                if (travelNewsContainer) {
                    travelNewsVisible = (travelNewsContainer.className.indexOf("show") > 0);
                }

                var publicIncidentCheckbox = document.getElementById(ctrl + publicIncidentCheckboxId);

                var roadIncidentCheckbox = document.getElementById(ctrl + roadIncidentCheckboxId);

                setTransportLayers(ctrl, layerState, map); // set the transport key symbols to show on the map in the layerstate

                if (travelNewsVisible) {
                    for (var i in layerState.pointX) {
                        layerState.pointX[i].visible = false;
                    }

                    var dateDayElm = document.getElementById(ctrl + "_dateSelect_listDays");

                    var dateMonthYearElm = document.getElementById(ctrl + "_dateSelect_listMonths");

                    if (dateDayElm && dateMonthYearElm) {
                        var tnDate = dateDayElm.value + "/" + dateMonthYearElm.value + " 12:00";
                        //var tnDate = dateMonthYearElm.value.substr(0, 2) + "/" + dateDayElm.value + dateMonthYearElm.value.substr(2) + " 12/00";

                        var transportTypeParam = "all";

                        if (!publicIncidentCheckbox.checked) {
                            transportTypeParam = "road";
                        }

                        if (!roadIncidentCheckbox.checked) {
                            transportTypeParam = "public";
                        }

                        map.setTravelNews({
                            transportType: transportTypeParam,
                            incidentType: 'all',
                            severity: 'all',
                            timePeriod: 'datetime',
                            datetime: tnDate
                        });
                    }
                }
                else {
                    setPointXLayers(ctrl, layerState); // set the other key symbols to show on the map in the layerstate object

                    if (!isInit) {
                        //clear the transport symbols
                        map.setTravelNews({
                            transportType: 'none',
                            incidentType: 'all',
                            severity: 'all',
                            timePeriod: 'datetime',
                            datetime: '01/01/1501 00:00'
                        });
                    }
                }

                map.setLayerState(layerState); // set the layerstate object back on map to display symbols

                // Call TD map web service to log MapOverlay event
                TransportDirect.UserPortal.Web.TDMapWebService.LogMapEvent("MapOverlay", map.getMapProperties().scale);

            }
            catch (err) {
                //alert(err);
            }
        }
    }
    return false;
}

// This function clears all the symbol keys currently displaying on the map
function hideLayers(mapid) {
    var layerState = null;

    var map = findMap(mapid);

    if (map) {
        try {
            //clear the transport symbols
            map.setTravelNews({
                transportType: 'none',
                incidentType: 'all',
                severity: 'all',
                timePeriod: 'datetime',
                datetime: '01/01/1501 00:00'
            });
            
            layerState = map.getLayerList();

            layerState.publicIncidentsVisible = false;

            layerState.roadIncidentsVisible = false;

            layerState.carparkLayerVisible = false;

            for (var i in layerState.stops) {
                layerState.stops[i].visible = false;
            }

            for (var i in layerState.pointX) {
                layerState.pointX[i].visible = false;
            }
            
            map.setLayerState(layerState);
        }
        catch (err) {
            //alert(err);
        }
    }

    
}

// This function sets all the transport layers in the map layer state object
function setTransportLayers(/*key container control*/ctrl, /*Map layer state*/layerState, /*map widget*/map) {
    
    var tableCtrl = document.getElementById(ctrl + transportTable);

    var transportCheckBoxes = tableCtrl.getElementsByTagName("input");

    var transportAllCheckBox = document.getElementById(ctrl + transportSelectAllPartial);

    var isAllSelected = transportAllCheckBox.src.toLowerCase().indexOf(transportImageUnSelectedPartial) < 0;

    for (var checkboxid in transportCheckBoxes) {
        if (transportCheckBoxes[checkboxid].type == "checkbox") {

            var code = getLayerCode(transportCheckBoxes[checkboxid].name);
            
            // if code is for Car park set car park layer visible
            if (code == "CPK") {
                layerState.carparkLayerVisible = transportCheckBoxes[checkboxid].checked & isAllSelected;
            }
            else {
                 // otherwise iterate through the stop layers in the layerstate object and set the layer if the name is same as code
                 for(var i=0;i<layerState.stops.length;i++){
                     if (code == layerState.stops[i].name) {
                         if (code == "BCX") {
                             if (map) {
                                 try {
                                     if (map.getMapProperties().scale <= maxBusSymbolMapScale) {
                                         transportCheckBoxes[checkboxid].disabled = false;
                                         layerState.stops[i].visible = transportCheckBoxes[checkboxid].checked & isAllSelected;
                                     }
                                     else {
                                         transportCheckBoxes[checkboxid].disabled = true;
                                         layerState.stops[i].visible = false;
                                     }
                                 } catch (err) {
                                 }
                             }
                             else {
                                 transportCheckBoxes[checkboxid].disabled = true;
                                 layerState.stops[i].visible = false;
                             }
                         }
                         else {
                             layerState.stops[i].visible = transportCheckBoxes[checkboxid].checked & isAllSelected;
                         }
                        break;
                    }
                 }
                
            }
        }
    }
}

// Gets the Map layer code name from the check box id in the key symbols control
function getLayerCode(layerCtrlId) {
    var layerCode = layerCtrlId.slice(layerCtrlId.lastIndexOf("$") + 1); // slices the control name with '$' as identifier and gets the last element

    return layerCode;
}


// Sets the PointX symbox state in the map layer state object
function setPointXLayers(/*key container control*/ctrl, layerState) {
    
    for (keyTable in keyOtherOptionstables) {

        var keyTableValue = "_" + keyOtherOptionstables[keyTable];
        var commandKeyValue = "_" + keyOtherOptions[keyTable];
        var tableCtrl = document.getElementById(ctrl + keyTableValue); // table control for the current keytable
        var commandCtrl = document.getElementById(ctrl + commandKeyValue); // command radio image for the current key table

        var pointXCheckBoxes = tableCtrl.getElementsByTagName("input"); // get the check boxes showing in the current keytabel

        for (var checkboxid in pointXCheckBoxes) {
            if (pointXCheckBoxes[checkboxid].type == "checkbox") {

                var code = getLayerCode(pointXCheckBoxes[checkboxid].name); // get the code for the PointX layer
                for (var i = 0; i < layerState.pointX.length; i++) {
                    if (code == layerState.pointX[i].name) {
                        if (tableCtrl.className.indexOf("hide") > 0) {
                            layerState.pointX[i].visible = false;
                        }
                        else {
                            layerState.pointX[i].visible = pointXCheckBoxes[checkboxid].checked;
                        }
                        break;
                    }
                }
                                
            }
        }
        
    }
}

// Gets the Initial key symbols control state 
function getInitialControlState() {
    var state = new Array(7);
    
    // Layer state for transport key symbols
    state[0] = new Array(false,false,false,false,false,false,false);
    
    // Layer state for PointX key symbols 
    for(var i = 1; i<7; i++)
    {
        state[i] = new Array(false, false, false, false);
    }

    return state;
}

// Gets the Map Symbol key layer state
function getMapSymbolLayerState(/*Id of the key container control*/ctrl) {
    var layerState = null;

    // Get the initial control state to start with
    var controlLayerState = getInitialControlState(); 
    
    var pointXLayerState = new Array();

    var ctrlElm = document.getElementById(ctrl + panelViews[1]);

    if (ctrlElm && ctrlElm.className.indexOf("hide") > 0) {
        return controlLayerState;
    }

    if (ctrlElm) {
        // Get the Transport layer controls' state
        controlLayerState[0] = getTransportLayerState(ctrl);

        // Get the PointX layer control's state
        pointXLayerState = getPointXLayerState(ctrl);


        var count = 1;
        for (var pxLayerState in pointXLayerState) {
            // Replace inital PointX control layer state with the layer state return using getPointXLayerState method
            controlLayerState[count] = pointXLayerState[pxLayerState];
            count++;
        }
    }
    

    return controlLayerState;
}

// Get Transport key symbol control state
function getTransportLayerState(/*Id of the key container control*/ctrl) {

    var transportLayerState = new Array(false, false, false, false, false, false, false);
    
    var tableCtrl = document.getElementById(ctrl + transportTable); // transport key symbols table control

    if (tableCtrl) {
        var transportCheckBoxes = tableCtrl.getElementsByTagName("input"); //transport key symbols check boxes

        var count = 0;

        for (var checkboxid in transportCheckBoxes) {
            if (transportCheckBoxes[checkboxid].type == "checkbox") {

                transportLayerState[count] = transportCheckBoxes[checkboxid].checked;
                count++;
            }
        }
    }

    return transportLayerState;
}

// Get PointX key symbol control state
function getPointXLayerState(/*Id of the key container control*/ctrl) {
    var pointXLayerState = new Array(6);

    var tableCount = 0;
    
    for (keyTable in keyOtherOptionstables) {

        // initialise key symbol state for current key symbols table
        pointXLayerState[tableCount] = new Array(false, false, false, false);
        
        var keyTableValue = "_" + keyOtherOptionstables[keyTable];
        var commandKeyValue = "_" + keyOtherOptions[keyTable];
        var tableCtrl = document.getElementById(ctrl + keyTableValue); // table control for the current keytable
        var commandCtrl = document.getElementById(ctrl + commandKeyValue); // command radio image for the current key table

        var pointXCheckBoxes = tableCtrl.getElementsByTagName("input");

        var count = 0;
        for (var checkboxid in pointXCheckBoxes) {
            if (pointXCheckBoxes[checkboxid].type == "checkbox") {

                pointXLayerState[tableCount][count] = pointXCheckBoxes[checkboxid].checked;
                count++;
            }
        }

        tableCount++;
    }

    return pointXLayerState;
}

function toggleNewsAndPointX(ctrl,travelNewsText, pointXText) {

    var toggleButton = document.getElementById(ctrl + toggleTravelNewsAndSymbolsId);

    var travelNewsContainer = document.getElementById(ctrl + travelNewsContainerId);

    var pointXContainer = document.getElementById(ctrl + pointXContainerId);

    var travelNewsVisible = (travelNewsContainer.className.indexOf("show") > 0)

    if (travelNewsVisible) {
        pointXContainer.className = pointXContainer.className.replace("hide", "show");
        travelNewsContainer.className = travelNewsContainer.className.replace("show", "hide");
        toggleButton.value = travelNewsText;
    }
    else {
        travelNewsContainer.className = travelNewsContainer.className.replace("hide", "show");
        pointXContainer.className = pointXContainer.className.replace("show", "hide");
        toggleButton.value = pointXText;
    }
    
}



