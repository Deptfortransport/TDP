// ***********************************************
// NAME     : MapAPI.js
// AUTHOR   : Atos Origin
// ************************************************

// ESRI Error filter array
var ignoreESRIError = new Array('_ae is undefined', '_b3 is undefined', '\'errorStatusCode\' is null or not an object');

var MapAltTextToDisplay = 'This is a map of a journey';
var ESRIUKTDPAPI = {
    useAsStartPoint: function(x, y, text) {
        //  summary:
        //  UC 1.4 - User Defined Location - Use as start of journey
        //  replace inner function code here to link to external code
        //alert('Start Point: \n' + x + ' \n' + y + '\n ' + text);
        
        var currentPageId = findCurrentPageId();
        showRedirecting();
        TransportDirect.UserPortal.Web.TDMapWebService.SetStartLocation(parseInt(x), parseInt(y), text, currentPageId, SucceededCallbackWithContext, FailedCallback, currentPageId);
    },
    useAsViaPoint: function(x, y, text) {
        //  summary:
        //  UC 1.4 - User Defined Location - Use as journey via pt
        //  replace inner function code here to link to external code
        //alert('Via Point: \n' + x + ' \n' + y + '\n ' + text);

        var currentPageId = findCurrentPageId();
        showRedirecting();
        TransportDirect.UserPortal.Web.TDMapWebService.SetViaLocation(parseInt(x), parseInt(y), text, currentPageId, SucceededCallbackWithContext, FailedCallback, currentPageId);
    },
    useAsEndPoint: function(x, y, text) {
        //  summary:
        //  UC 1.4 - User Defined Location - Use as end of journey
        //  replace inner function code here to link to external code
        //alert('End Point: \n' + x + ' \n' + y + '\n ' + text);

        var currentPageId = findCurrentPageId();
        showRedirecting();
        TransportDirect.UserPortal.Web.TDMapWebService.SetEndLocation(parseInt(x), parseInt(y), text, currentPageId, SucceededCallbackWithContext, FailedCallback, currentPageId);
    },
    showCarParkInformation: function(id) {
        //  summary:
        //  UC 1.1 - Client-side graphics - Car Parks Layer
        //  replace inner function code here to link to external code
        //alert('Car Park Id: ' + id);

        var currentPageId = findCurrentPageId();
        showRedirecting();
        TransportDirect.UserPortal.Web.TDMapWebService.SetCarParkInformation(id, currentPageId, SucceededCallbackWithContext, FailedCallback, "CarParkInformation");
    },
    showStopsInformation: function(id) {
        //  summary:
        //  UC 1.1 - Client-side graphics - Stops Layer
        //  replace inner function code here to link to external code
        //alert('Stops id: ' + id);

        var currentPageId = findCurrentPageId();
        showRedirecting();
        TransportDirect.UserPortal.Web.TDMapWebService.SetStopInformation(id, currentPageId, SucceededCallbackWithContext, FailedCallback, "StopInformation");
    },
    selectNearbyPointResult: function(data) {
        //  summary:
        //    UC 1.4 - Select nearby point - raw data sent to harness
        //    data consisits of an array of click Points, point x etc...
        //    replace inner function code here to link to external code  
        //    example link to external handler
        //alert(dojo.toJson(data, true));

        var util = ESRIUK.util();
        //  will find map - only if map is directly a child of document.body
        var arr = dijit.findWidgets(document.body);
        var map = null;
        for (var i = 0; i < arr.length; i++) {
            if (arr[i].declaredClass == "ESRIUK.Dijits.Map") {
                map = arr[i];
            }
        }
        map.clearPoints('symbol');

        var selectLocationDropDownId = "selectLocationDropDownList";

        // Reset the locations dropdown list to populate
        var selectLocationDropDown = document.getElementById(selectLocationDropDownId);
        if (selectLocationDropDown != null) {
            selectLocationDropDown.length = 0;
        }

        // Constants used for the location text (note, all in English, language handling not supported).
        var constRoad = " (Road)";
        var constStop = " (Stop)";
        var constCarPark = " (Car park)";
        var constLocalRoad = "LOCAL ROAD";

        var count = 0;

        // Temp variables to read in the data
        var points = null;
        var type = null;
        var values = null;
        var easting = 0;
        var northing = 0;
        var locationText = "";

        // Array to prevent duplicate locations being added
        var locationsArray = new Array();
        var location
        for (var obj in data) {

            points = data[obj].points;
            type = data[obj].type;
            values = data[obj].values;

            easting = points[0].x;
            northing = points[0].y;

            // Build up the location display text
            locationText = "";
            switch (type) {

                case "CarParks":
                    if ((values[1] != null) && (values[1].length > 0)) {
                        locationText = values[1] + constCarPark;
                    }
                    break;

                case "Stops":
                    locationText = values[3] + constStop;
                    break;

                case "ITN":
                    if ((values[1] == null) || (values[1].length == 0)) {
                        locationText = constLocalRoad + constRoad;
                    }
                    else {
                        locationText = values[1] + constRoad;
                    }
                    break;

                case "PointX":
                    if (values[0] != null) {
                        locationText = values[0];
                    }
                    break;
            }

            if (locationText.length > 0) {

                try {

                    // Decode any escaped characters
                    locationText = locationText.replace("&amp;", "&");
                    locationText = locationText.replace("&apos;", "'");

                    if (!contains(locationsArray, locationText)) {

                        count++;

                        // Add it to the locations array list to prevent duplicate item being shown 
                        locationsArray.push(locationText);

                        // Add the location to the drop down list
                        if (selectLocationDropDown) {

                            var opt = document.createElement("option");
                            opt.text = locationText;
                            opt.value = dojo.toJson(data[obj]);
                            selectLocationDropDown.options.add(opt);

                        }
                        else {

                            //map.addStartEndPt({ x: easting, y: northing, type: 'symbol', label: locationText, symbolKey: 'CIRCLE' + count });
                            map.addIconAndInfoWindow(easting, northing, locationText);

                        }
                    }

                } catch (err) {
                    //alert(err.description);
                }

            }
        }

        // Determine if we're showing an journey input map or a find map, and set the controlIds,
        // Assume journey input map.
        var journeyInputMap = true;
        var selectLocationInfoDivId = "mapInputControl_mapSelectLocationControl_panelSelectLocationInfo";
        var selectLocationListDivId = "mapInputControl_mapSelectLocationControl_panelSelectLocationList";
        var selectLocationErrorDivId = "mapInputControl_mapSelectLocationControl_panelSelectLocationError";

        var selectLocationInfoDiv = document.getElementById(selectLocationInfoDivId);

        if (selectLocationInfoDiv == null) {
            // Couldnt get the div, so switch all ids to find a map ids and try again
            selectLocationInfoDivId = "mapFindControl_mapSelectLocationControl_panelSelectLocationInfo";
            selectLocationListDivId = "mapFindControl_mapSelectLocationControl_panelSelectLocationList";
            selectLocationErrorDivId = "mapFindControl_mapSelectLocationControl_panelSelectLocationError";

            selectLocationInfoDiv = document.getElementById(selectLocationInfoDivId);

            journeyInputMap = false;
        }

        // Hide the select locations info
        if (selectLocationInfoDiv != null) {
            selectLocationInfoDiv.className = selectLocationInfoDiv.className.replace("show", "hide");
        }

        // Show the locations list div or the error message div
        var selectLocationListDiv = document.getElementById(selectLocationListDivId);
        var selectLocationErrorDiv = document.getElementById(selectLocationErrorDivId);
        if (count > 0) {

            if (selectLocationListDiv != null) {
                selectLocationListDiv.className = selectLocationListDiv.className.replace("hide", "show");
            }
            if (selectLocationErrorDiv != null) {
                selectLocationErrorDiv.className = selectLocationErrorDiv.className.replace("show", "hide");
            }
        }
        else {

            if (selectLocationErrorDiv != null) {
                selectLocationErrorDiv.className = selectLocationErrorDiv.className.replace("hide", "show");
            }
            if (selectLocationListDiv != null) {
                selectLocationListDiv.className = selectLocationListDiv.className.replace("show", "hide");
            }
        }

        // Ensure the locations dropdown is in view
        if (!journeyInputMap) {
            scrollToTop();
        }

    },
    miscError: function(error) {
        //alert("misc:" + dojo.toJson(error));
        logErrorToTD("MiscError", error, "");
    },
    queryError: function(/*Object*/error, /*response*/response) {
        //alert("query:" + dojo.toJson(error));
        logErrorToTD("QueryError", error, response.xmlHTTP.responseText);
    },
    mapError: function(/*Object*/error, /*response*/response) {
        //alert("map: " + dojo.toJson(error));
        if (isSessionError(error, response)) {
            // If its a session error/timeout, postback the page which then allows the server to perform
            // session error handling
            FailedCallback(error);
        }
        else {
            logErrorToTD("MapError", error, response.xmlHTTP.responseText);
        }
    },
    mapGraphicsCount: function(/*Number*/num) {
        //  summary:
        //    Called on each response from the Query Extent method
        //  params:
        //    num (number) : number of graphics returned (-1 = number of graphics exceeded)
        if (num == -1) { /*alert('Too many graphics at this scale');*/ }
    },
    onMapExtentChange: function(/*esri.geometry.Extent*/extent, /*boolean*/levelChange, /*integer*/scale, /*string*/ovURL) {
        //  summary:
        //    Event fired when the map extent is changed (pan, zoom etc)
        //  params:
        //    extent = esri.geometry.Extent - object with properties xmax, xmin, ymax, ymin
        //      reference: http://resources.esri.com/help/9.3/arcgisserver/apis/javascript/arcgis/help/jsapi_start.htm#jsapi/extent.htm
        //    levelChange = boolean - indicates if the map scale level has changed
        //    scale = integer - value reflecting scale of map e.g. 50000
        //    ovUrl = string - representing the new url fo the overview image



        //  This is an example of using the map parameters to update another element on the page
        //  here we use the overview url to show the overview map & parameters
        //  only on the basic map test page


        try {
            trySetMapAltText();
        } catch (error) { }


        // Publish events so javascript can register to this event whenever map extent is changed and 
        // used this events to execute different code accordingly

        // event published for outward journey map
        dojo.publish("outwardMap", [{ extent: extent, levelChange: levelChange, scale: scale}]);

        // event published for return journey map
        dojo.publish("returnMap", [{ extent: extent, levelChange: levelChange, scale: scale}]);

        // event published for the maps like travel news etc.
        dojo.publish("Map", [{ extent: extent, levelChange: levelChange, scale: scale}]);


    },
    onMapInitialiseComplete: function(/*ESRIUK.Dijits.Map*/map) {

        try {
            trySetMapAltText();
        } catch (error) { }

        // Publish events so javascript can register to this event whenever map extent is changed and
        // used this events to execute different code accordingly

        // event published for the maps like travel news etc.
        dojo.publish("mapInitialised", [{ map: map}]);


    },
    // Zooms the map to level specified as arguments and shows an information window at x(Easting)
    // and y(Northing)
    zoomToLevelAndPoint: function(/*Client side id of map*/mapid, number, x, y, /*Zoom level*/level, /*Information window text*/text, content) {

        // Find a map dojo widget
        var map = findMap(mapid);

        if (map) {
            // Call maps to zoom at level and show info window
            map.zoomToLevelAndPoint(x, y, level);

            var handle = dojo.subscribe("Map", function(mapArgs) {
                map.addStartEndPt({ x: x, y: y, type: 'symbol', infoWindowRequired: true, content: content, main: true, label: text, symbolKey: 'CIRCLE' + number });
                dojo.unsubscribe(handle);
            });


        }
    },
    // Calls TDMapWebService to log page entry event for the PageId specified as argument
    logPageEntryEvent: function(pageId) {

        TransportDirect.UserPortal.Web.TDMapWebService.LogPageEntryEvent(pageId);

    }
};

function trySetMapAltText() {
    
    try {
        //  will find map - only if map is directly a child of document.body
        var arr = dijit.findWidgets(document.body);
        var map = null;
        for (var i = 0; i < arr.length; i++) {
            if (arr[i].declaredClass == "ESRIUK.Dijits.Map") {
                map = arr[i];
            }
        }

        if (map) {
            if (map.attachMap && map.attachMap._map) {


                if (map.attachMap._map.getLayer('layer0') && map.attachMap._map.getLayer('layer0')._img) {

                    map.attachMap._map.getLayer('layer0')._img.alt = MapAltTextToDisplay;

                }
                else {
                   setTimeout(trySetMapAltText, 1500);
                }
               

            }
         
        }
    } catch (error) {/*alert(error);*/}
}

function setMapAltText() {
    
    //  will find map - only if map is directly a child of document.body
    var arr = dijit.findWidgets(document.body);
    var map = null;
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].declaredClass == "ESRIUK.Dijits.Map") {
            map = arr[i];
        }
    }
    
    try{
    
        if (map) {
            if(map.attachMap._map && map.attachMap._map.getLayer('layer0') && map.attachMap._map.getLayer('layer0')._img)
                map.attachMap._map.getLayer('layer0')._img.alt = MapAltTextToDisplay;
        }
    } catch (error) { }

}

function InitMapAltText(mapAltText) {
    MapAltTextToDisplay = mapAltText;
}

// This method gets call when a successfull call comes back from TDMapWebService call
function SucceededCallbackWithContext(/*result of the call*/result, /*Context set for the call*/userContext, /*Name of the server method called*/methodName) {

    var url;

    // Builds url from the result
    // Follwoing call builds the root part for the url for example 'http://localhost/web2'
    // if the web2 setup gets changed following code will needed to be changed
    
    if (result.length > 0) {
        var j = result.substring(1);
        var w = window.location;
        var m = w.protocol + "//" + w.host;
        var a = w.pathname.split("/");
        
        //Getting the pathname upto second level i.e. upto Web2
        for (var i = 1; i < 2; i++) {
            m += '/' + a[i];
        }

        url = m + j;

    }

    // if the user context is 'FindMapResult' or method name is 'SetStopInformation'
    //or 'SetCarParkInformation' navigate the page to generated url
    
    if (userContext == "FindMapResult" || methodName == "SetStopInformation" || methodName == "SetCarParkInformation") {
        window.location.href = url;
    }
    else {
        // post back to the same page otherwise
        __doPostBack('__Page', ''); 
    }

}

// This is the callback function invoked if the Web service
// failed.
// It accepts the error object as a parameter.
function FailedCallback(error) {
    // Session may have timed out post back to same page to handle it
    __doPostBack('__Page', ''); 
}


// Detects if the Map API error is a session error/timeout
function isSessionError(error, responseText) {

    if ((error) && (responseText)) {
        // Error 550 "Map state cannot be loaded" indicates the session is missing the map session part,
        // which is most likely a session timeout. 
        if (responseText.result.errorStatusCode == '550') {
            return true;
        }
    }

    return false;
}


// This function gets handle to map dijit widget using the client side map id supplied
function findMap(mapid) {

    if (dijit.byId(mapid) == undefined) {
        map = null;
    }
    else {
        map = dijit.byId(mapid); // get the dijit widget
    }

    return map;
}

// This function gets the url of map image, state of the symbols on the map, view type text from the mapViewTypeControl and 
// call a TD map web service method to set this information for printer friendly page.
function setMapViewState(mapid, width, height, dpi, mapSymbolControl, mapViewTypeControl, outward) {

    var map = findMap(mapid);

    // Set default values
    var mapPrintUrl = "";
    var mapSymbolState = getInitialSymbolControlState();
    var mapViewTypeElmText = "";

    if (map) {
        
        // Get print image url for specified width, height and resolution
        var url = map.getPrintImage(width, height, dpi); 

        if (url != null) {

            mapPrintUrl = url;
            
        }

        if (mapSymbolControl.length > 0) {

            // Get the state of the symbols showing on the map
            mapSymbolState = getMapSymbolLayerState(mapSymbolControl);
               
        }

        if (mapViewTypeControl.length > 0) {

            var mapViewTypeElm = document.getElementById(mapViewTypeControl);

            if (mapViewTypeElm != undefined && mapViewTypeElm) {

                // drop down with only one value selectable
                if (mapViewTypeElm.type == "select-one") 
                {
                    mapViewTypeElmText = mapViewTypeElm.options[mapViewTypeElm.selectedIndex].text;
                }
            }

        }

        // Call web service to set up map information for printer friendly page
        //TransportDirect.UserPortal.Web.TDMapWebService.SaveMapViewState(mapPrintUrl, mapSymbolState, mapViewTypeElmText, outward);  //, SucceededCallbackWithContext, FailedCallback, "SaveMapState");

        var postdata = { "mapUrl": mapPrintUrl,
            "mapSymbolsState": mapSymbolState,
            "mapViewTypeText": mapViewTypeElmText,
            "isOutward": outward
        };

        var result = GetSynchronousJSONResponse(getWebServiceUrl('~/webservices/TDMapWebService.asmx/SaveMapViewState'), postdata);
        
        result = eval('(' + result + ')');
    }

}

// Gets the Initial key symbols control state 
function getInitialSymbolControlState() {
    var state = new Array(7);

    // Layer state for transport key symbols
    state[0] = new Array(false, false, false, false, false, false, false);

    // Layer state for PointX key symbols 
    for (var i = 1; i < 7; i++) {
        state[i] = new Array(false, false, false, false);
    }

    return state;
}

// This function gets the url of map print image, state of the symbols on the map, view type text from the mapViewTypeControl, info of map print tile images and 
// call a TD map web service method to set this information for printer friendly page.
function setMapTileViewState(mapid, width, height, dpi, defaultTileScale, targetNoOfTiles, mapSymbolControl, mapViewTypeControl, outward) {
    
    var map = findMap(mapid);

    // Set default values
    var mapPrintUrl = "";
    var mapSymbolState = getInitialControlState();
    var mapViewTypeElmText = "";

    var tileObjs;

    var tileJson = "";

    // Get the map level information form the config file
    var maplods = ESRIUK._config.map.lods;

    // Start with level 0
    var currentMaplevel = maplods[0].level;

    var currentMapScale = maplods[0].scale;

    // Go through the levels and find the current scale and level for default tile scale
    for (currentMaplevel; currentMapScale > defaultTileScale; currentMaplevel++) {

        currentMapScale = maplods[currentMaplevel].scale;
    }

    if (map) {

        // Get the whole map print image
        var url = map.getPrintImage(width, height, dpi);

        if (url != null) {

            mapPrintUrl = url;

        }

        // Get the symbol state
        if (mapSymbolControl.length > 0) {

            mapSymbolState = getMapSymbolLayerState(mapSymbolControl);

        }

        // Get the View Type text
        if (mapViewTypeControl.length > 0) {

            var mapViewTypeElm = document.getElementById(mapViewTypeElm);

            if (mapViewTypeElm != undefined && mapViewTypeElm) {

                if (mapViewTypeElm.type == "select-one") // drop down with only one value selectable
                {
                    mapViewTypeElmText = mapViewTypeElm.options[mapViewTypeElm.selectedIndex].text;
                }
            }

        }

        // Get the number of cycle images for current map scale
        var noOfImages = map.getNumberOfCycleImages(currentMapScale);

        // Get the No of images nearest to the target no of tiles
        for (currentMaplevel; noOfImages <= targetNoOfTiles && currentMaplevel < maplods.length; currentMaplevel++) {
            noOfImages = map.getNumberOfCycleImages(maplods[currentMaplevel].scale); 
        }

        // Get the CyclePrintDetail objects array with print tile images information for the 
        // scale at which no of images neares to the targe no of tiles
        if (noOfImages > 0) {
            tileObjs = map.getCyclePrintDetails(maplods[currentMaplevel-1].scale);

        }

        // Generate a json object string from the cycle print detail array object
        if (tileObjs != undefined && tileObjs != null) {
            tileJson = dojo.toJson(tileObjs);
        }

        // Call the TD map web service to save Cycle map printer friendly images
        //TransportDirect.UserPortal.Web.TDMapWebService.SaveCycleMapTileViewState(mapPrintUrl, tileJson, maplods[currentMaplevel - 1].scale, mapSymbolState, mapViewTypeElmText, outward);

        var postdata = {"mapUrl":mapPrintUrl
            ,"mapTiles": tileJson
            ,"tileScale": maplods[currentMaplevel - 1].scale
            ,"mapSymbolsState": mapSymbolState
            ,"mapViewTypeText": mapViewTypeElmText
            ,"isOutward": outward
        };
        
        var result = GetSynchronousJSONResponse(getWebServiceUrl('~/webservices/TDMapWebService.asmx/SaveCycleMapTileViewState'), postdata);

        result = eval('(' + result + ')');
    }
}

// This function finds the current map page id from the map widget generated on the page
function findCurrentPageId() {
    var util = ESRIUK.util();
    //  will find map - only if map is directly a child of document.body
    var arr = dijit.findWidgets(document.body);
    var map = null;

    var pageId;

    for (var i = 0; i < arr.length; i++) {
        if (arr[i].declaredClass == "ESRIUK.Dijits.Map") {
            map = arr[i];
        }
    }

    if (map) {
        // Get the hidden page id field
        var hdnPageId = document.getElementById(map.id + "_PageId");

        if (hdnPageId) {
            pageId = hdnPageId.value;
        }
    }
    
    return pageId;
}

// This function changes the 'Loading..' text showing on the map to 'Redirecting...'
// This is to show user visibly that the javascript is calling a web service event and then 
// post back or page redirection is going on.
function showRedirecting() {
    var loadingGif = dijit.byId("ESRIUK_Dijits_Loading_0");
    if (loadingGif != undefined && loadingGif) {
        
        // Use lastChild as supported by both IE and FF
        loadingGif.domNode.lastChild.innerHTML = "&nbsp;&nbsp;Redirecting&nbsp;...&nbsp;"
        loadingGif.domNode.style.display = "block";
    }
}

// Function which determines if an array contains the specified object
function contains(a, obj) {
    var i = a.length;
    while (i--) {
        if (a[i] === obj) {
            return true;
        }
    }
    return false;
}

//Sets the red slider box around top 5 zoom bars
function setSliderBox() {
    var b_version = navigator.appVersion;
    var version = parseFloat(b_version);

    dojo.query("div.sliderBottomBox").forEach(function(node, index, arr) {
        if ((navigator.appName == "Microsoft Internet Explorer") && (version >= 4)) {
            node.style.height = "56px";
        }
        else {
            node.style.height = "40.65px";
        }
    });
}


function GetSynchronousJSONResponse(url, postData) {
    var xmlhttp = null;
    if (window.XMLHttpRequest)
        xmlhttp = new XMLHttpRequest();
    else if (window.ActiveXObject) {
        if (new ActiveXObject("Microsoft.XMLHTTP"))
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
        else
            xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
    }
    // to be ensure non-cached version of response
    url = url;   //+ "?rnd=" + Math.random();

    xmlhttp.open("POST", url, false); //false means synchronous
    xmlhttp.setRequestHeader("Content-Type", "application/json; charset=utf-8");
    xmlhttp.send(dojo.toJson(postData));
    var responseText = xmlhttp.responseText;
    return responseText;
}


function getWebServiceUrl(partialUrl) {

    var url;

    // Builds url from the result
    // Follwoing call builds the root part for the url for example 'http://localhost/web2'
    // if the web2 setup gets changed following code will needed to be changed

    if (partialUrl.length > 0) {
        var j = partialUrl.substring(1);
        var w = window.location;
        var m = w.protocol + "//" + w.host;
        var a = w.pathname.split("/");

        //Getting the pathname upto second level i.e. upto Web2
        for (var i = 1; i < 2; i++) {
            m += '/' + a[i];
        }

        url = m + j;

    }

    return url;
}

// Map api got an error. Log in to Transport Direct logs by calling TDMapWebService's LogESRIError method
function logErrorToTD(errorType, error, responseText) {

    try
    {
        if (logEsriError(error)) {
        
            if (error.innerException) {
                var errorJSON = dojo.toJson(error.innerException);

                var errorMsg = "error:\n" + errorJSON + "\n\nresponse:\n" + responseText;

                TransportDirect.UserPortal.Web.TDMapWebService.LogESRIError(errorType, errorMsg);
            }
        }
    } catch (err) {
    
        // In case of exception try atleast logging error message
        if(error && error.message) {

            try {
                var errorMsg = "error:\n" + error.message + "\n\nresponse:\n" + responseText;

                TransportDirect.UserPortal.Web.TDMapWebService.LogESRIError(errorType, errorMsg);
            } catch (err) { }
        }
    }

}

// function gets called when user clicks ok button in send to friend tab
function setupEmailImage(imageResolution)
{
    var width = 754;
    var height = 500;

    // get the default dimensions from the ESRI config file
    var defaultDimensions = ESRIUK._config.map.defaultDimensions;

    if (defaultDimensions) {
        height = defaultDimensions.height;
        width = defaultDimensions.width;
    }

    // set the resolution
    // 192 is the resoultion set to the property InteractiveMapping.MapImageResolution 
    var resolution = 192;

    if (imageResolution && imageResolution != 0) {
        resolution = imageResolution;
    }

    // find map on the page. There must be only one.
    var arr = dijit.findWidgets(document.body);
    var map = null;
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].declaredClass == "ESRIUK.Dijits.Map") {
            map = arr[i];
        }
    }

    var id = null;

    var outward = true;
    
    if (map) {

        id = map.id;
        
        // if id is for return map containing the 'return' word in id set outward as false
        if (id.toLowerCase().indexOf('return') > 0) {
            outward = false;
        }
    }

    // call the setMapViewState method
    setMapViewState(id, width, height, resolution, "", "", outward);
    
    
}

// Checks if ESRI error raise is supposed to be log or is one from the errors to be ignored array defined at top of this page
function logEsriError(error)
{
    var logError = true;

    var errorMessage = '';
    var errorInnerMessage = '';

    try {
        errorMessage = error.message.trim();
        errorInnerMessage = error.innerException.message;
    }
    catch (error) {
    }

    //alert("errorMessage: " + errorMessage + ". errorInnerMessage: " + errorInnerMessage);
    
    for(var counter = 0; counter<ignoreESRIError.length; counter++) {
        
        var ignoreMessage = ignoreESRIError[counter].trim();
        
        if (ignoreMessage == errorMessage) {
            logError = false;
            break;
        }
        else if (errorInnerMessage.indexOf(ignoreMessage) >= 0) {
            logError = false;
            break;
        }
    }
    
    return logError;
}


$(document).ready(function() {

    // Shop find on map button
    showTDButton(true, $('.accessibleLocationControl .findOnMap'));
    
    // Following lines are to rebind the jquery plugins or js events after ajax update of the update panel
    try {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function() {

            // Shop find on map button
            showTDButton(true, $('.accessibleLocationControl .findOnMap'));
            
            // If the map control is only created/displayed by the server during an AJAX page postback,
            // then it will not automatically be set (as the page load event will not fire again). 
            // So have to manually create the map using javascript.
            AJAXPOSTBACK.setupAJAXPostbackMap();

        });
    } catch (error) {
        // Ignore error, page may not have ajax
    }

});


// Control which indicates if an a map should be created following AJAX postback
var ajaxMapContainerId;
// Control which will contain the map and its parameters (currently only used for mapNearestControl)
var ajaxMapControlId = "mapNearestControl_mapControl_mapControl";

var AJAXPOSTBACK = {

    // Displays a map on page, after an AJAX postback
    setupAJAXPostbackMap: function() {

        // Find the control containing the ajax map to display
        if (document.getElementById(ajaxMapControlId + "_AJAXId")) {
            ajaxMapContainerId = document.getElementById(ajaxMapControlId + "_AJAXId").value;

            // Destroy existing map, otherwise won't be able to create a new map with same id
            AJAXPOSTBACK.destroyMap();

            var mapControl = AJAXPOSTBACK.createMapCore();

            var map = null;

            if (mapControl) {

                // Find the map control created by the server, this should have all the map attributes set
                var $mc = $('div#' + ajaxMapControlId);
                var p_Tools = $mc.attr('param_Tools');
                var p_Mode = $mc.attr('param_Mode');
                var p_XMin = $mc.attr('param_XMin');
                var p_XMax = $mc.attr('param_XMax');
                var p_YMin = $mc.attr('param_YMin');
                var p_YMax = $mc.attr('param_YMax');
                var p_Width = $mc.attr('param_Width');
                var p_Height = $mc.attr('param_Height');
                var p_Symbols = $mc.attr('param_Symbols');

                map = new ESRIUK.Dijits.Map({
                    id: ajaxMapControlId,
                    'class': 'tundra',
                    param_Tools: p_Tools,
                    param_Mode: p_Mode,
                    param_XMin: p_XMin,
                    param_XMax: p_XMax,
                    param_YMin: p_YMin,
                    param_YMax: p_YMax,
                    param_Width: p_Width,
                    param_Height: p_Height,
                    param_Symbols: p_Symbols
                }, mapControl);
            }

            // And start the map
            if (map) {
                try {
                    map.startup();
                } catch (er) { }
            }
        }
    },

    // Destroys map
    destroyMap: function() {
        try {
            if (dijit.byId(ajaxMapControlId))
                dijit.byId(ajaxMapControlId).destroyRecursive();
        } catch (er) { }

    },

    // Creates map control
    createMapCore: function() {

        mapControlCore = null;

        // Check if the AJAX visible container has been added to page
        if (document.getElementById(ajaxMapContainerId)) {
            try {
                mapControl = document.getElementById(ajaxMapControlId);
                mapControlCore = document.createElement("div");
                mapControlCore.id = "mapCore";

                mapControl.appendChild(mapControlCore);

            } catch (error) {
                mapControlCore = null;
            }
        }

        return mapControlCore;
    }
};

// selects the index in the location dropdown, used on the find nearest accessible stop page
// for a map info popup with a select this location link
function selectLocationInDropdown(dropdown, index, scrollto) {

    try {
        // Select the index in the dropdown
        $('#' + dropdown + ' option')[index].selected = true;

        // Scroll to the element specified
        var scrollToElement = $('#' + scrollto);

        if (scrollToElement != null) {
            $('html, body').animate({
                scrollTop: (scrollToElement.offset().top - 10) + 'px'
            }, 'fast');
        }
    }
    catch (error) {
    }

    return false;   
}