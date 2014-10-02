// ***********************************************
// NAME     : TravelNews.js
// AUTHOR   : Atos Origin
// ************************************************
//
//  ESRI Maps doesn't like working with css display style.
//  Changes made to make ESRI map working with visibility style 
//

// Variables needed setting up for the travelnews
var travelnewsMapTableContainerId = "mapTable";
var travelnewsDetailTableContainerId = "travelNewsDetailsContainer";

var tnMapId = "MapTravelNews_mapControl_mapControl";

var buttonSwitchViewId = "ButtonSwitchView";

var newsControlId = "ShowNewsControl";

var regionDropId = "_regionsList";

var transportTypeDropId = "_transportDropList";

var incidentTypeDropId = "_incidentTypeDropList";

var severityTypeDropId = "_delaysDropList";

var allUkImage = "Map_UKRegionMap.gif";

var selectedImage = "Map_Selected_";

var labelNoIncidentsId = "TravelNewsDetails_lblNoRecords";

var firstView = true;

var savePreferenceId = "_saveCheckBox";

var regionCoOrdinatesAll = [["All"], ["South West", "88896,6070,470176,202317"], ["South East", "415400,72654,646449,240800"], ["London", "495988,154654,552987,201698"], ["East Anglia", "504519,220813,659065,344636"], ["East Midlands", "403436,223568,558449,419669"], ["West Midlands", "324462,206181,451346,373062"], ["Yorkshire and Humber", "368493,373285,545519,520378"], ["North West", "288212,343552,407532,595231"], ["North East", "351299,506259,481020,673784"], ["Scotland", "49646,529028,420949,1048984"], ["Wales", "158900,163000,307100,396000"]];

//Setting up the elements need throught the script
var transportTypeElm = null;
var incidentTypeElm = null;
var severityTypeElm = null;
var regionDropElement = null;
var dateDayElm = null;
var dateMonthYearElm = null;
var mapContainer = null;
var tableContainer = null;
var txtSearch = null;
var backButton = null;
var currentNewsState = null; /* travel news state currently visible */
var lastNewsState = null; /* previous travel news state */
var labelTravelNewsHeading = null;
var searchPhraseRowElement = null;
var labelNoIncidents = null;
var savePreferenceElement = null;

// variable to store wether single incident showing
var showOneIncident = false;

// travel news heading text
var travelNewsHeading = null;
// variable to determine wether button to switch view between table and map is clicked
var viewChangeInProgress = false;

//Resource needed for the button text which switches the views between map and detail
var buttonSwitchToMap = "Show map";
var buttonSwitchToTable = "Show in Table";

var helpPanelTravelNewsNonMapId = "helpPanelTravelNewsNonMap";

var helpPanelTravelNewsId = "helpPanelTravelNews";

var helpPanelTravelNews = null;
var helpPanelTravelNewsNonMap = null;


//Sets up default text for the heading which gets changed dynamically as the travel news region changes
function setupRegionChangeResources(tnHeading) {
    travelNewsHeading = tnHeading;
}

//Sets MapRegionSelector controls image links clickable with instead of postback just firing a javascript code to set the region 
function setupMapRegionSelector(regionCoordinates) {

    //load page controls
    loadNewsControl(); 

    var mapRegionSelector = dojo.byId("regionSelector_imageMap1map");

    var mapRegionImage = dojo.byId("regionSelector_imageMap1");

    var mapRegionAreaNodes = mapRegionSelector.children;

    var allUkRegionButton = dojo.byId("regionSelector_selectAllUk");

    //remove the postback call - this cannot be done just using dojo.attr
    //create a copy button with the same attributes as allUkRegionButton but type as button
    //set the allUkRegionButton not to display and add the events to the button duplicated
    var allUkRegionButtonJs = dojo.create("input", { type: "button", className: "TDSelectAllUk", value: allUkRegionButton.value, onmouseout: allUkRegionButton.onmouseout, onmouseover: allUkRegionButton.onmouseover }, allUkRegionButton, "after");
    allUkRegionButton.style.display = "none";
    
    //add the javascript call to set region 'All'
    var btnHandle = dojo.connect(allUkRegionButtonJs, "onclick", function() {
            showOneIncident = false;
            selectOptionByValue(regionDropElement, "All");
            setupMapRegionImage("All");
            SetTravelNews(regionCoordinates);
            return false;
        });

    //remove postback from each maparea links and add javascript code instead
        dojo.forEach(mapRegionAreaNodes, function(n, i) {
            dojo.attr(n, "onclick", "");
            var handle = dojo.connect(n, "onclick", function() {
                showOneIncident = false;
                selectOptionByValue(regionDropElement, n.title);
                setupMapRegionImage(n.title);
                SetTravelNews(regionCoordinates);
                return false;
            });
        });

}


//sets up map region image based on whichever region get selected or clicked
function setupMapRegionImage(imageTitle) {

    var mapRegionImage = dojo.byId("regionSelector_imageMap1");
    
    var imagePath = mapRegionImage.src.substr(0,mapRegionImage.src.lastIndexOf("/")+1);

    if (imageTitle == "All") {
        mapRegionImage.src = imagePath + allUkImage;
    }
    else if (imageTitle == "Yorkshire and Humber") {
        mapRegionImage.src = imagePath + selectedImage + "Yorkshire.gif";
    }
    else {
        mapRegionImage.src = imagePath + selectedImage + imageTitle.replace(" ", "") + ".gif";
    }
}

//Generic function to select a value in the dropdown
//used to select the travel news region dropdown when the region selected from the mapregionselector control
 function selectOptionByValue(selObj, value){

     var count = 0;
     for (options in selObj.options){
         if (selObj[count].value == value) {
             selObj[count].selected = true;
             break;
         }
         count++;
     }

 }

//Generic function to get the querystring out of the url
 function queryStringValue(queryString) {
     queryStringPart = window.location.search.substring(1);
     queryStringArray = queryStringPart.split("&");
     for (i = 0; i < queryStringArray.length; i++) {
         nameValuePair = queryStringArray[i].split("=");
         if (nameValuePair[0] == queryString) {
             return nameValuePair[1];
         }
     }

     return null;
 }

//Travel news state object to store the state of the travel news show news control states
 var travelNewsState = function(regionCoordinates) {

     var transportTypeIndex = -1;
     var incidentTypeIndex = -1;
     var severityTypeIndex = -1;
     var regionIndex = -1;
     var searchPhrase = "";
     var dateDayIndex = -1;
     var dateMonthYearIndex = -1;
     var mapProperties = null;
     var mapshowing = false;

     this.isSingleIncident = false;

     this.regionCoordinates = regionCoordinates;
     /* storing single incident id*/
     this.incidentId = queryStringValue("uid");

     this.setTNState = function() {

         loadNewsControl();

         if (mapContainer.style.visibility.indexOf("hidden") > -1) {
             mapshowing = false;
         }
         else {
             mapshowing = true;
         }

         if (transportTypeElm) { transportTypeIndex = transportTypeElm.selectedIndex; }

         if (incidentTypeElm) { incidentTypeIndex = incidentTypeElm.selectedIndex; }

         if (severityTypeElm) { severityTypeIndex = severityTypeElm.selectedIndex; }

         if (regionDropElement) { regionIndex = regionDropElement.selectedIndex; }

         if (dateDayElm) { dateDayIndex = dateDayElm.selectedIndex; }

         if (dateMonthYearElm) { dateMonthYearIndex = dateMonthYearElm.selectedIndex; }

         if (txtSearch) { searchPhrase = txtSearch.value; }

         var map = GetTravelNewsMap();

         if (map) {
             mapProperties = map.getMapProperties();
         }



     }
     /* method to restore the state of travel news to control */
     this.loadTNState = function() {

         loadNewsControl();


         if (transportTypeElm) { transportTypeElm.selectedIndex = transportTypeIndex; }

         if (incidentTypeElm) { incidentTypeElm.selectedIndex = incidentTypeIndex; }

         if (severityTypeElm) { severityTypeElm.selectedIndex = severityTypeIndex; }

         if (regionDropElement) { regionDropElement.selectedIndex = regionIndex; }

         if (dateDayElm) { dateDayElm.selectedIndex = dateDayIndex; }

         if (dateMonthYearElm) { dateMonthYearElm.selectedIndex = dateMonthYearIndex; }

         if (txtSearch) { txtSearch.value = searchPhrase; }

         var map = GetTravelNewsMap();

         if (map & mapProperties) {
             /* can be used to restore the previous map */
         }
     }


 }

 /* helper object to save and load travel news states 
    - SaveState saves the  travel state visble before change to last state and sets the new travel state
    - loadLastState loads the last travel state
    - showSingleIncident sets that single incident is currently showing
 */
 var travelNewsHelper = {

     SaveState: function(regionCoordinates) {
         if (!regionCoordinates) {
             regionCoordinates = null;
         }
         lastNewsState = currentNewsState;
         currentNewsState = new travelNewsState(regionCoordinates);
         currentNewsState.setTNState();
         
         var b_version = navigator.appVersion;
         var version = parseFloat(b_version);

         /* sets the red box around the first five scale bars */
         dojo.query("div.sliderBottomBox").forEach(function(node, index, arr) {
             if ((navigator.appName == "Microsoft Internet Explorer") && (version >= 4)) {
                 node.style.height = "56px";
             }
             else {
                 node.style.height = "40.65px";
             }
         });
     },
     loadLastState: function() {
         if (lastNewsState) {
             lastNewsState.loadTNState();
             SetTravelNews(lastNewsState.regionCoordinates);
         }

     },
     showSingleIncident: function(show) {
         showOneIncident = show;
       
     }

 }

/* helper function to load all the control elements needed in the variables */
function loadNewsControl() {
    
    transportTypeElm = document.getElementById(newsControlId + transportTypeDropId);

    incidentTypeElm = document.getElementById(newsControlId + incidentTypeDropId);

    severityTypeElm = document.getElementById(newsControlId + severityTypeDropId);

    regionDropElement = document.getElementById(newsControlId + regionDropId);

    dateDayElm = document.getElementById(newsControlId + "_dateSelect_listDays");

    dateMonthYearElm = document.getElementById(newsControlId + "_dateSelect_listMonths");

    mapContainer = document.getElementById(travelnewsMapTableContainerId);

    tableContainer = document.getElementById(travelnewsDetailTableContainerId);

    txtSearch = document.getElementById(newsControlId + "_searchInputText");

    backButton = document.getElementById(newsControlId + "_backButton");

    searchPhraseRowElement = document.getElementById(newsControlId + "_searchPhraseRow");

    labelTravelNewsHeading = document.getElementById("lblLiveTravelNews");

    labelNoIncidents = document.getElementById(labelNoIncidentsId);

    savePreferenceElement = document.getElementById(newsControlId + savePreferenceId);
}

/* Sets the Travel news - This is the main function called to set the news */
function SetTravelNews(regionCoordinates) {


    loadNewsControl();

    clearSearchPhraseError();
    
    var dateErrorPanel = document.getElementById("dateErrorPanel");
    var dateRowElement = document.getElementById(newsControlId + "_dateSelect_datePanel");
    
    // hide invalide date error at the start
    if (dateErrorPanel) {
        dateErrorPanel.style.display = "none";
        if (dateRowElement) {
            dateRowElement.className = "";
        }
    }

    var invalidDate = true;
    
    var tnDateOnly = dateDayElm.value + "/" + dateMonthYearElm.value;
    
    invalidDate = isDate(tnDateOnly, /^([0-9]{1,2})[\/]([0-9]{1,2})[\/]([0-9]{1,4})$/, { d: 1, m: 2, y: 3 }) > 0;
    
    var dateArr = tnDateOnly.split("/")

    if (!invalidDate) {
        try{
        // date is valid so create Date object and check if the date is in past
        invalidDate = isDateInPast(new Date(dateArr[2], new Number(dateArr[1]) - 1/*Date object takes 0-11 value for month*/, dateArr[0]));
        }
        catch(error)
        {
            // we got an error, set invalidDate to true;
            invalidDate = true;
        }
    }

    //if date selected for travel news is not valid display an error
    // validate date in dd/mm/yyyy format
    if (invalidDate) {

        if (dateErrorPanel) {
            dateErrorPanel.style.display = "";
            if (dateRowElement) {
                dateRowElement.className = "alerterror";
            }
        }
    }
    // date is valid so continue setting the travel news
    else {

        if (backButton) {
            backButton.style.display = "";
        }

        try {
            /* Set travel news heading text */
            if (travelNewsHeading) {
                labelTravelNewsHeading.innerHTML = regionDropElement.options[regionDropElement.selectedIndex].text + " " + travelNewsHeading;
            }
        }
        catch (error) {
        }

        setupMapRegionImage(regionDropElement.value);

        travelNewsHelper.SaveState(regionCoordinates);

        /* Set travel news in map */
        if (tableContainer.style.visibility.indexOf("hidden") > -1) {
            SetTravelNewsInMap(regionCoordinates);
        }

        var searchPhraseErrorPanel = document.getElementById("searchPhraseErrorPanel");

        if (txtSearch && txtSearch.value.length > 0 && tableContainer.style.visibility.indexOf("hidden") > -1) {
            changeTNView('Details');
        }
        


        /* Set travel news in table */
        SetTravelNewsDetails();
    }

        
    return false;
}

/* Set travel news detail in map*/
function SetTravelNewsInMap(regionCoordinates) {

    destroyMap();
    loadNewsControl();

    var mapControl = CreateMapCore();
    
    var map = null;  //GetTravelNewsMap();

    if (transportTypeElm && incidentTypeElm && severityTypeElm) {

        if (mapControl) {

            var transportTypeParam = transportTypeElm.value.toLowerCase();

            var severityTypeParam = severityTypeElm.value.toLowerCase();

            var timePeriodParam = "date";

            if (transportTypeParam.indexOf("public") > -1)
            {
                transportTypeParam = "public";
            }
            
            if(severityTypeParam == "very severe")
            {
                severityTypeParam = "major";
            }

            if (severityTypeParam == "recent") {
                timePeriodParam = "recent";
                severityTypeParam = "all"
            }
            /* set the travel news date */
            var tnDate = dateDayElm.value + "/" + dateMonthYearElm.value + " 12/00";
            //var tnDate = dateMonthYearElm.value.substr(0, 2) + "/" + dateDayElm.value + dateMonthYearElm.value.substr(2) + " 12/00";

            var singleIncidentId = "";
            

           
            var mapExtent = GetMapExtent(regionCoordinates, regionDropElement);

            if (currentNewsState.incidentId && showOneIncident) {
               
                var singleIncidentId = document.getElementById("singleIncidentId");
                var singleIncidentEasting = document.getElementById("singleIncidentEasting");
                var singleIncidentNorthing = document.getElementById("singleIncidentNorthing");
                var singleIncidentScale = document.getElementById("singleIncidentScale");

                if (singleIncidentId && singleIncidentEasting && singleIncidentNorthing && singleIncidentScale
                    && singleIncidentId.value == queryStringValue("uid")) {
                    if (singleIncidentId.value != "" && singleIncidentEasting.value != "" && singleIncidentNorthing.value != ""
                        && singleIncidentScale != "") {

                        ShowSingleIncident(singleIncidentId.value, singleIncidentEasting.value, singleIncidentNorthing.value, singleIncidentScale.value, true);
                    }
                }
                else {
                    try {
                        map = new ESRIUK.Dijits.Map({ id: tnMapId, 'class': 'tundra', param_Tools: 'zoom,pan', param_Mode: 'none',
                            param_TravelNews: 'transportType:' + transportTypeParam + ',incidentType:' + incidentTypeElm.value.toLowerCase()
                                    + ',severity:' + severityTypeParam
                                    + ',timePeriod:' + timePeriodParam
                                    + ',datetime:' + tnDate
                        }, mapControl);
                    } catch (er) { }
                }
                
            }
            else {
                if (mapExtent) {
                    //map.zoomToExtent(mapExtent[0], mapExtent[1], mapExtent[2], mapExtent[3], false);
                    try{
                        map = new ESRIUK.Dijits.Map({ id: tnMapId, 'class': 'tundra', param_Tools: 'zoom,pan', param_Mode: 'none',
                                param_XMin: mapExtent[0],
                                param_YMin: mapExtent[1],    
                                param_XMax: mapExtent[2],
                                param_YMax: mapExtent[3],  
                                param_TravelNews: 'transportType:' + transportTypeParam +',incidentType:' + incidentTypeElm.value.toLowerCase()
                                    + ',severity:'+ severityTypeParam
                                    + ',timePeriod:'+ timePeriodParam
                                    + ',datetime:'+ tnDate
                                
                            }, mapControl);
                    } catch (er) { }
                }
                else {
                    // Couldnt determine extent, or map of all UK requested
                    //map.zoomToFullExtent();
                    try{
                        map = new ESRIUK.Dijits.Map({ id: tnMapId, 'class': 'tundra', param_Tools: 'zoom,pan', param_Mode: 'none',
                            param_TravelNews: 'transportType:' + transportTypeParam +',incidentType:' + incidentTypeElm.value.toLowerCase()
                                    + ',severity:'+ severityTypeParam
                                    + ',timePeriod:'+ timePeriodParam
                                    + ',datetime:'+ tnDate
                         }, mapControl);
                     } catch (er) { }
                }
            }
            

        }
    }
    
    if(map) {
        try{
            map.startup();
        } catch (er) { }
    }

}

/* Set the travel news detail in table*/
function SetTravelNewsDetails() {

    var savePreference = false;
    
    loadNewsControl();
    
    var tnDate = dateDayElm.value + "/" + dateMonthYearElm.value;

    var txtSearch = document.getElementById(newsControlId + "_searchInputText");

    var severityTypeParam = severityTypeElm.value.toLowerCase();

    if (severityTypeParam == "very severe") {
        severityTypeParam = "Major";
    }

    var singleIncidentId = "";

    if (currentNewsState.incidentId && showOneIncident) {
        singleIncidentId = currentNewsState.incidentId;
    }

    if (savePreferenceElement && savePreferenceElement.checked) {
        savePreference = true;
    }
    
    /* Call td map web service's GetTravelNews method using AJAX and get the result*/
    try {
        TransportDirect.UserPortal.Web.TDMapWebService.GetTravelNews(regionDropElement.value, transportTypeElm.value.replace(" ", ""), incidentTypeElm.value, severityTypeParam, tnDate, txtSearch.value, singleIncidentId, savePreference, SucceededTNCallbackWithContext, FailedTNCallback, newsControlId);
    } catch (Error) {
        alert("ajax error" + error.get_message() + error.get_stackTrace());
    }

    if (savePreferenceElement)
        savePreferenceElement.checked = false;
}

/* Get the handle to travel news map widget showing */
function GetTravelNewsMap() {
    //  will find map - only if map is directly a child of document.body
    var arr = dijit.findWidgets(document.body);
    var map = null;
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].declaredClass == "ESRIUK.Dijits.Map") {
            map = arr[i];
        }
    }

    return map;
}

/* Gets the map extent selected in region drop down to show from region Coordinates array */
function GetMapExtent(regionCoordinates, regionDropElement)
{
    var extent = new Array(4);
    
    
    for(var i in regionCoordinates)
    {
        
        if(regionCoordinates[i][0] == regionDropElement.value)
        {
            if (regionCoordinates[i].length == 2)
            {
                extent = regionCoordinates[i][1].split(",");
                break;
            }
            else
            {
                extent = null;
            }
        }
    }
    
    return extent;
}

function SucceededTNCallbackWithContext(/*result of the call*/result, /*Context set for the call*/userContext, /*Name of the server method called*/methodName) {

    //alert(result);

    var tablecontainer = dojo.byId("travelNewsDetailsContainer");

    var newsTables = tablecontainer.getElementsByTagName("table");

    var newsTable = null;

    /* Get the travel news table from travel news table container */
    var i = 0;
    for (var i = 0; i < newsTables.length; i++) {
        if (newsTables[i].className == "TravelIncidentsTable") {
            newsTable = newsTables[i];
            break;
        }
    }
    
    /* Remove all the rows from travel news table*/
    if (newsTable) {
        while (newsTable.rows.length > 1) {
            dojo.destroy(newsTable.rows[newsTable.rows.length - 1]);
        }
    }
    
    if (result) {
        if (result.length > 0) {

            /* Set the result in the table container 
            - Creates rows in table
            - Adds the links for the first column to show it on the map and zoom to it
            - Sets the css for alternative rows
            */
            for (var j = 0; j < result.length; j++) {

                var classNameStr = (j % 2 == 0) ? "travelIncident" : "travelIncidentAlt";

                var resultDataRow = result[j];

                var trow = dojo.create("tr", { className: classNameStr }, newsTable.tBodies[0], "last");

                var incidentCell = dojo.create("td", { headers: "headerIncident" }, trow, "last");
                var incidentCellLink = dojo.create("a", { innerHTML: resultDataRow[1] }, incidentCell);
                var linkScript = "javascript: ShowSingleIncident('" + resultDataRow[0] + "'," + resultDataRow[7] + "," + resultDataRow[8] + "," + resultDataRow[9] + ");";

                var detailsText = resultDataRow[3];
                // Check if TOIDs should be displayed
                var userType = document.getElementById("FooterControl1_hdnUserLevel");
                if ((userType) && (userType.value > 0)) {
                    detailsText = detailsText + "<br /><span class=\"cjperror\">" + resultDataRow[10] + "</span>";
                }
                
                dojo.attr(incidentCellLink, "href", linkScript);
                //dojo.connect(incidentCellLink, "onclick", function(evt) { ShowSingleIncident(resultDataRow[0], resultDataRow[7], resultDataRow[8], resultDataRow[9]); return false; });
                var affectedCell = dojo.create("td", { headers: "headerAffected", innerHTML: resultDataRow[2] }, trow, "last");
                var detailsCell = dojo.create("td", { headers: "headerDetails", innerHTML: detailsText }, trow, "last");
                var severityCell = dojo.create("td", { headers: "headerSeverity", innerHTML: resultDataRow[4] }, trow, "last");
                var occurredCell = dojo.create("td", { headers: "headerOccurred", innerHTML: resultDataRow[5] }, trow, "last");
                var updatedCell = dojo.create("td", { headers: "headerUpdated", innerHTML: resultDataRow[6] }, trow, "last");

                if (viewChangeInProgress) {
                    if (tableContainer.style.visibility.indexOf("none") > -1) {
                        if (currentNewsState.incidentId && showOneIncident) {
                            if (resultDataRow[0] == currentNewsState.incidentId) {
                                ShowSingleIncident(resultDataRow[0], resultDataRow[7], resultDataRow[8], resultDataRow[9]);
                                GetTravelNewsMap().zoomToXY(resultDataRow[7], resultDataRow[8]);
                            }
                        }
        
                    }
                }
                
                
            }

            // Hide no incidents label
            if (labelNoIncidents) {
                labelNoIncidents.className = labelNoIncidents.className.replace("show", "hide");
            }
        }
        else {
            // No incidents found, display message
            if (labelNoIncidents) {
                labelNoIncidents.className = labelNoIncidents.className.replace("hide", "show");
            }
        }


    }

    if (!viewChangeInProgress) {
       showOneIncident = false;
        
    }

    /* Reset view change in progress*/
    viewChangeInProgress = false;

}

// This is the callback function invoked if the Web service
// failed.
// It accepts the error object as a parameter.
function FailedTNCallback(error) {
    // Session may have timed out post back to same page to handle it
    __doPostBack('__Page', ''); 
}

/* shows the single incident on map */
function ShowSingleIncident(uid, easting, northing, mapZoomScale, viewChanged) {
    if (viewChanged == undefined || viewChanged == null)
        viewChanged = false;
        
    if(!viewChanged || dijit.byId(tnMapId))
        destroyMap();
    
    
    loadNewsControl();

    var map = null;  //GetTravelNewsMap();

    var mapControl = CreateMapCore();
    
    setMapToolbar();
    
    /* Zoom to scale and point of the single incident and switch to map view if tabel view is visible currently */
    try {
        if (mapControl) {
            
            timePeriodParam = "date";
            /* set the travel news date */
            var tnDate = dateDayElm.value + "/" + dateMonthYearElm.value + " 12/00";
            //var tnDate = dateMonthYearElm.value.substr(0, 2) + "/" + dateDayElm.value + dateMonthYearElm.value.substr(2) + " 12/00";

            if (transportTypeElm && incidentTypeElm && severityTypeElm) {

                transportTypeParam = transportTypeElm.value.toLowerCase();
                
                severityTypeParam = severityTypeElm.value.toLowerCase();

                if (transportTypeParam.indexOf("public") > -1) {
                    transportTypeParam = "public";
                }
                
                // Severity is set to all to ensure when showing a single incident, the incident is 
                // shown on the map regardless of the severity selected in dropdown
                severityTypeParam = "all";

                incidentTypeParam = incidentTypeElm.value.toLowerCase();
                

            }
            else {

                transportTypeParam = 'all';
                incidentTypeParam = 'all';
                severityTypeParam = 'all';
            }
            
            map = new ESRIUK.Dijits.Map({
                        id: tnMapId,    
                        'class': 'tundra',
                        param_LocationX: easting,
                        param_LocationY: northing,
                        param_Scale: mapZoomScale,
                        param_Tools: 'zoom,pan',
                        param_Mode: 'none',
                        param_TravelNews: 'transportType:' + transportTypeParam + ',incidentType:' + incidentTypeElm.value.toLowerCase()
                                                        + ',severity:'+ severityTypeParam
                                                        + ',timePeriod:'+ timePeriodParam
                                                        + ',datetime:'+ tnDate
                    },
                    mapControl
                  );
            map.startup();
            
            if (mapContainer.style.visibility.indexOf("hidden") > -1) {
                if(!viewChanged)
                    changeTNView('Map', true);

            }
             // Clear the search phrase error
             clearSearchPhraseError();
             setHelpPanels();
             try
             {
                 if (helpPanelTravelNews.style.display != 'none' || helpPanelTravelNewsNonMap.style.display != 'none') {
                     showTravelNewsHelp();
                 }
             } catch (err) { }
        }
    }
    catch(error) {      
    }
}

/* function to call when back button gets clicked to load the last travel news state */
function ShowPreviousNews() {
    try {
        travelNewsHelper.loadLastState();
    } catch (Error) {
    }
    
    return false;
}

/* function which displays the error panel at top when user tries to put search phrase and change the view to map*/
function showSearchPhraseError(error) {
    var searchPhraseErrorPanel = document.getElementById("searchPhraseErrorPanel");
    searchPhraseRowElement = document.getElementById(newsControlId + "_searchPhraseRow");
    
    if (searchPhraseErrorPanel) {
        searchPhraseErrorPanel.style.display = "";
        searchPhraseRowElement.className = "alerterror";
        setupTNViewSwitchButtons('Details');
    }
}

function clearSearchPhraseError() {
    var searchPhraseErrorPanel = document.getElementById("searchPhraseErrorPanel");
    searchPhraseRowElement = document.getElementById(newsControlId + "_searchPhraseRow");

    if (searchPhraseErrorPanel) {
        searchPhraseErrorPanel.style.display = "none";
        searchPhraseRowElement.className = "";
    }
}

/* function setting if the view change is in progress currently */
function SetViewChanging(changing) {
    viewChangeInProgress = changing;
}

/* Sets the resources needs when view change is in progress */
function setupViewChangeResources(mapbuttonText, tablebuttonText, regionCoOrdinates) {
    buttonSwitchToMap = mapbuttonText;
    buttonSwitchToTable = tablebuttonText;
    if(regionCoOrdinates)
        regionCoOrdinatesAll = regionCoOrdinates;
}

//Sets the Travel news server state selected view on server for printable maps
function setTravelNewsView() {
    var view = "Details";
    if (mapContainer.style.visibility.indexOf("hidden") < 0) {
        view = "Map";
    }

    // Calling the webservice method syncronously to set the current travel news view
    // This requires MapAPI.js which is loaded as part of the new maps
    var postdata = { "view": view };

    var result = GetSynchronousJSONResponse(getWebServiceUrl('~/webservices/TDMapWebService.asmx/SetTravelNewsSelectedView'), postdata);

    result = eval('(' + result + ')');
}

function setMapToolbar() {
    var b_version = navigator.appVersion;
    var version = parseFloat(b_version);

    /* sets the red box around the first five scale bars */
    dojo.query("div.sliderBottomBox").forEach(function(node, index, arr) {
        if ((navigator.appName == "Microsoft Internet Explorer") && (version >= 4)) {
            node.style.height = "56px";
        }
        else {
            node.style.height = "40.65px";
        }
    });
}


//Set the travelnews container div height based on wether map or table showing
// adds function to be run when page loaded
dojo.addOnLoad(function() {

    var parent = tableContainer.parentNode;
    if (mapContainer.style.visibility == 'hidden') {
        parent.style.height = tableContainer.clientHeight + "px";
    }
    else {
        parent.style.height = mapContainer.clientHeight + "px";
    }
    
});


//Set the travelnews container div height based on wether map or table showing
function setTNPosition() {

    var parent = tableContainer.parentNode;
    if (mapContainer.style.visibility == 'hidden') {
        parent.style.height = tableContainer.clientHeight  + "px";
    }
    else {
        parent.style.height = mapContainer.clientHeight  + "px";
    }
}


// Toggles the visibility of the element
// This is the modified version of toggleVisibility function in common.js
// ESRI map doesn't work properly with toggling visibility with css style display='none'
function toggleTNVisibility(elementID) {

    var thisElement = document.getElementById(elementID);

    if (thisElement.style.visibility == 'hidden')

        thisElement.style.visibility = '';

    else

        thisElement.style.visibility = 'hidden';

    setHelpPanels();
    try{
        if (helpPanelTravelNews.style.display != 'none' || helpPanelTravelNewsNonMap.style.display != 'none') {
            showTravelNewsHelp();
        }
    } catch (err) { }
}

// Shows the help panel depending on the current travel news view set
function showTravelNewsHelp() {
    setHelpPanels();
    
    // check for the current view visiblt    
    var view = 'Details';
    if (mapContainer.style.visibility.indexOf('hidden') < 0) {
        view = 'Map';
    }
    
    // show the help according to the view set currently
    try{
        if (view == 'Details') {
            helpPanelTravelNewsNonMap.style.display = '';
            helpPanelTravelNews.style.display = 'none';
        }
        else {
            helpPanelTravelNewsNonMap.style.display = 'none';
            helpPanelTravelNews.style.display = '';
        }
    } catch (err) { }
    return false;
    
}

// Hides all the help panels.
function hideTravelNewsHelp() {
    setHelpPanels();

    try {
        helpPanelTravelNews.style.display = 'none';
        helpPanelTravelNewsNonMap.style.display = 'none';
    } catch (err) { }

    return false;
}

// Sets help panels
function setHelpPanels() {
    helpPanelTravelNews = document.getElementById(helpPanelTravelNewsId);
    helpPanelTravelNewsNonMap = document.getElementById(helpPanelTravelNewsNonMapId);
}

// This method changes the travel news views
function changeTNView(view, singleIncidentClicked) {
    loadNewsControl();

    var viewChanged = false;
    
    if (singleIncidentClicked == undefined || singleIncidentClicked == null)
        singleIncidentClicked = false;
    
    searchPhraseRowElement = document.getElementById(newsControlId + "_searchPhraseRow");
   
    // check for the current view visiblt    
    var currentView = 'Details';
    if (mapContainer.style.visibility.indexOf('hidden') < 0) {
        currentView = 'Map';
        view = 'Details'
    }
    else {
        currentView = 'Details';
        view = 'Map';
    }

    if (currentView != view) {
        if (view == 'Map') {
            // /*&& !singleIncidentClicked*/ commented out so in future if user put the search phrase and click on single incident
            // user can view that perticular incident. If the user click on show map he will get an error !
            if (txtSearch && txtSearch.value.length > 0 && mapContainer.style.visibility.indexOf("hidden") > -1 && !singleIncidentClicked) {
                showSearchPhraseError(null);
                
            }
            else if (searchPhraseRowElement && searchPhraseRowElement.className.indexOf("alerterror") > -1 && mapContainer.style.visibility.indexOf("hidden") > -1 && !singleIncidentClicked) {
                showSearchPhraseError(null);
            }
            else {
                mapContainer.style.visibility = "";
                tableContainer.style.visibility = "hidden";
                viewChanged = true;
                if(!singleIncidentClicked)
                    SetTravelNewsInMap(regionCoOrdinatesAll);
            }
        }
        else {
            mapContainer.style.visibility =  "hidden";
            tableContainer.style.visibility = "";
            viewChanged = true;
        }
    
        if(viewChanged)
            setupTNViewSwitchButtons(view);
        setTNPosition();
        hideTravelNewsHelp();

       
    }

//    if (firstView) {
//        setTNForFirstTime();
//    }
    
    return false;
}

// Set Travel news view switch buttons
function setupTNViewSwitchButtons(currView) {
    var b_version = navigator.appVersion;
    var version = parseFloat(b_version);
    
    var isIE = (navigator.appName == "Microsoft Internet Explorer");

    var buttonSwitchViewElem = document.getElementById(buttonSwitchViewId);
 

    if (currView == 'Map') {
      
        buttonSwitchViewElem.value = buttonSwitchToTable;        
    }
    else {
       
        buttonSwitchViewElem.value = buttonSwitchToMap;
    }

}

// Destroys map
function destroyMap() {
    try {
        //var mapControl = document.getElementById("travelNewsMap");
        if (dijit.byId(tnMapId))
            dijit.byId(tnMapId).destroyRecursive();
    } catch (er) { }
    
}


function CreateMapCore() {
    try {
        mapControl = document.getElementById("travelNewsMap");
        mapControlCore = document.createElement("div");
        mapControlCore.id = "travelNewsMapCore";


        //For Stop Information page to work
        var hdnPageFieldId = tnMapId + "_PageId";
        var hdnPageField = document.getElementById(hdnPageFieldId);
        if (!hdnPageField) {
            hdnPageField = document.createElement("input");
            hdnPageField.type = "hidden";
            hdnPageField.id = hdnPageFieldId;
            hdnPageField.value = "TravelNews";
            mapControl.appendChild(hdnPageField);
        }
        
        mapControl.appendChild(mapControlCore);
    } catch (error) {
        mapControlCore = null;
    }

    return mapControlCore;
}

