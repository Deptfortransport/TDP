// ***********************************************
// NAME     : Locations.js
// AUTHOR   : Atos Origin
// ************************************************

// <summary>Enum to define wether to consider group/nongroup stations or both</summary>
stopType = { NonGroup: 0, Group: 1, All: 2 }

// <summary>stop object representing client side dropdownstop object</summary>
stop = function() {
    this.displayValue = '';

    this.naptan = null;
    this.name = null;
    this.displayName = null;
    this.shortCode = null;
    this.isGroup = false;

    // Instance method returns true if the current object is valid by checking naptan is not null or empty string
    this.IsValid = function() {
        if (this.naptan && typeof (naptan) == 'array') {
            if (naptan.length > 0)
                return true;
            else
                return false;
        }
    }

    // Returns display name  for the stop
    // if displayName is null or empty string returns name property value
    this.getDisplayName = function() {
        var nameToDisplay = this.displayName;
        if (this.displayName == null || this.displayName == '')
            nameToDisplay = this.name;
        return nameToDisplay;
    }

    // toString method for the stop object similar to ToString() method in .net
    this.toString = function() {

        var displayString = (this.displayValue != null && this.displayValue != '') ? this.displayValue : this.getDisplayName();

        if (this.shortCode != null && this.shortCode != '' && (this.displayName == null || this.displayName == ''))
            displayString = displayString + " (" + this.shortCode + ")";

        return displayString;
    }

    // <summary>test method is implemented to test stop objects with the search string entered by user</summary>
    // <param name="autoSuggestData">
    //  autoSuggestData is passed by AutoSuggest object with reference to itself and the search string entered by user
    // </param>
    // <return>Returns true if the current object matches to the search criteria otherwise returns false</return>
    this.test = function(autoSuggestData) {
        var searchValue = autoSuggestData.searchValue; // Search string entered by user
        var autoSuggestBox = autoSuggestData.autoSuggestObj; // Reference to the AutoSuggest object

        var codeMatch = false;

        // only do the shortcode match for the NPTG data and not for the alias data
        if (this.displayName == null || this.displayName == '') {
            if (this.shortCode && this.shortCode.trim() != "") {
                codeMatch = (this.shortCode.toLowerCase().trim() == searchValue.trim());
            }
            else {
                codeMatch = false;
            }
        }

        // Build regular expression to match the name/ displayname
        var regExp = new RegExp('^' + searchValue, 'i');

        descriptionMatch = regExp.test(this.getDisplayName());

        if (codeMatch) {
            this.displayValue = '(' + this.shortCode + ') ' + this.getDisplayName();
        }

        return codeMatch || descriptionMatch;
    }
}


// <summary>stop staic method enable to cast generic object to object of stop type</summary>
stop.cast = function(obj) {
    var stopObj = new stop();
	for(var prop in obj)
	{
		if(obj[prop] && typeof (obj[prop]) != "function") {
		    stopObj[prop] = obj[prop];
		}
	}
	
	obj = null;

	return stopObj;	
}

// <summary>
// Stop instance method returns clone of the current object
// Note: the object returned has a separate copy then current object
// </summary>
stop.prototype.clone = function() {
    var stopObj = new stop();
    var obj = this;
    for (var prop in obj) {
        if (obj[prop] && typeof (obj[prop]) != "function") {
            stopObj[prop] = obj[prop];
        }
    }
    return stopObj;
}

// <summary>
// Implementation of List functionality specific for stop object
// This resembles a List<T> in .net
// The stopList implements singleton pattern enables only one instance of stop list
// </summary>      
function stopList(){

    var stopArr = [];
    var stops_NoGroup = [];
   
    this.singletonInstance = null;

   
    
    // Get the instance of the stopList class
    // If there’s none, instanciate one

    var getInstance = function() {

      if (!this.singletonInstance) {

        this.singletonInstance = createInstance();

      }

      return this.singletonInstance;

    }

    
    // Create an instance of the LocationList class

    var createInstance = function () {

        // Here, you return all public methods and variables

        return {
            // Property giving access to all the stops in the list
            stops: stopArr,

            // Returns stop based on stopType specified
            getStops: function (stType) {
                var stopObjs = [];
                if (stType == stopType.NonGroup) { // filter only non group stops
                    for (stopKey in stopArr) {
                        if (stopArr[stopKey].isGroup == false)
                            stopObjs.push(stopArr[stopKey]);
                    }
                }
                else if (stType == stopType.Group) { // filter only group stops
                    for (stopKey in stopArr) {
                        if (stopArr[stopKey].isGroup == true)
                            stopObjs.push(stopArr[stopKey]);
                    }
                }
                else {
                    return stopArr; // return both non-group and group stops
                }
                return stopObjs;
            },

            // Adds stop to the list, if sort is true the method also sorts the data after adding
            add: function (stopObj, sort) {

                if (stopObj) {
                    stopArr.push(stopObj);
                    if (stopObj.isGroup == false)
                        stops_NoGroup.push(stopObj);
                }
                // sort the stops in list
                if (sort) {
                    stopArr = stopArr.sort(function (a, b) { return a.toString().localeCompare(b.toString()) });
                }

            },
            // Adds stop range to current list
            // Checks if the range is an array and the objects are type of stop
            addRange: function (stopObjArr) {
                if (stopObjArr instanceof (Array)) {
                    for (stopKey in stopObjArr) {
                        var stopObj = stop.cast((stopObjArr[stopKey])); // Cast object in array of type stop
                        if (stopObj && stopObj instanceof (stop)) {     // check if the object is of type stop
                            this.add(stopObj, false);
                        }
                    }
                }

            },
            // Sorts stop data in list with generice localeCompare method
            sort: function () {
                stopArr = stopArr.sort(function (a, b) { return a.toString().localeCompare(b.toString()) });
            },

            sortWithNoGroups: function () {
                stops_NoGroup = stops_NoGroup.sort(function (a, b) { return a.toString().localeCompare(b.toString()) });
            },

            searchStops: function (searchValue, maximumResult, includeGroup) {

                searchValue = searchValue.replace(/\\/gi, '');

                if (includeGroup == undefined) {
                    includeGroup = true;
                }

                var result = new Array();
                var resultCRS = new Array();
                var self = stopList.instance;
                var stIndex = 0;
                var C = removeSpecialCharacters(searchValue.toUpperCase().trim()) + "~";
                var shortedStopData = null;
                var stopObj = null;

                if (includeGroup)
                    shortedStopData = stopArr;
                else
                    shortedStopData = stops_NoGroup;

                if (shortedStopData.length > 0) {
                    if (searchValue.length >= 2) {
                        var i = shortedStopData.length;
                        while (i >= 0) {
                            if (shortedStopData[i] && shortedStopData[i].shortCode) {
                                if (shortedStopData[i].shortCode.toUpperCase().substr(0, searchValue.length) == searchValue.toUpperCase()) {
                                    stopObj = shortedStopData[i].clone();
                                    if (!stopObj.displayName || stopObj.displayName.trim() == '') {
                                        // stopObj.displayValue = '(' + stopObj.shortCode + ') ' + stopObj.getDisplayName();
                                        resultCRS.push(stopObj);
                                    }
                                }
                            }

                            --i;
                        }
                    }
                }

                //shortedStopData = stopArr.sort();
                stIndex = searchForStop(shortedStopData, searchValue, 0);
                if (shortedStopData.length > 0) {
                    for (var L = stIndex; removeSpecialCharacters(shortedStopData[L].toString().toUpperCase()) < C; L++) {
                        searchString = searchValue.trim().toUpperCase();
                        matchString = shortedStopData[L].toString().trim().toUpperCase().substring(0, searchString.length);


                        if ((searchValue.length == 2 || searchValue.length == 3) && shortedStopData[L] && shortedStopData[L].shortCode) {
                            if (shortedStopData[L].shortCode.toUpperCase().substring(0, searchString.length) == searchValue.toUpperCase()) {
                                // do nothing
                            }
                            else {
                                if (matchString == searchString) {
                                    stopObj = shortedStopData[L];
                                    result.push(stopObj);
                                }
                            }
                        }
                        else {
                            if (matchString == searchString) {
                                stopObj = shortedStopData[L];
                                result.push(stopObj);
                            }
                        }

                        if (L == shortedStopData.length - 1) break;
                    }
                }

                return result.concat(resultCRS);
            },

            searchStopsWithNoGroups: function (searchValue, maximumResult) {
                return stopList.instance.searchStops(searchValue, maximumResult, false);
            }


        }
    }
    return getInstance();

}




// <summary>Property providing access to instance of a stop list</summary>
stopList.instance = new stopList();

// <summary>Helper method converts array item to object with the properties specified</summary>
// <param name="arr">Array Item</param>
// <param name="properties">Properties to which array item values needs copying</param>
function arrToObject (arr, properties) {
    var obj = new Object();
    
    // Properties is a single property name 
    if (typeof properties == 'String') {
        obj[properties] = arr[0];
    }
    // Properties is an array of proprtynames so iterate through array assign values to each property of object in turn
    else if (properties instanceof Array) {
        for (var j in properties) {
            obj[properties[j]] = arr[j];
        }
    }
    

    return obj;
}


// <summary>
// This method registers stop data in the list so it can be used in auto-suggest functionality
// data should be type of Array otherwise  function throws an error
// </summary>
// <param name="mode">Mode of stop for which auto suggest data needs registering</param>
// <param name="data">Auto-Suggest dropdown data array</param>
// <note>Each item in auto-suggest array is an array representing stop object</note>
function registerLocationData(mode, data) {
    if (data instanceof (Array)) {
        // iterate through the data array, build stop objects and add them to stop list
        for (i in data) {
            var obj = arrToObject(data[i], ['name', 'displayName', 'shortCode', 'naptan', 'isGroup']);
            var stopObj = stop.cast(obj);
            if (stopObj && stopObj instanceof (stop)) {
                stopList.instance.add(stopObj);
            }  
        }
    }
    else {
        throw "DataError";
    }

    stopList.instance.sort();
    stopList.instance.sortWithNoGroups();
}


// <summary>Initialise auto-suggest functionality</summary>
// <param name="mode">mode (i.e. Rail) for which drop down showing</param>
// <param name="autoSuggestTextBox">Text box for which to extend auto-suggest functionality</param>
// <param name="hdnfieldId">Hidden field to which auto-suggest selected value needs copying</param>
// <param name="filterIds">Auto suggest filter control client ids</param>
// <param name="showGroup">True if Group stops needs showing in the auto-suggest dropdown</param>
// <param name="triggerLength">Number of characters to be entered to trigger auto-suggest dropdown</param>
// <param name="maxResults">Maximum number of result to be shown in auto-suggest dropdown list</param>
function initLocationDropDown(mode, autoSuggestTextBox, hdnfieldId, filterIds, showGroup, triggerLength, maxResults) {

    if (triggerLength === undefined)
        triggerLength = 0;

    if (maxResults === undefined)
        maxResults = 20;
         
    // if showGroup is not defined show group stops
    if (showGroup === undefined) {
        showGroup = true;
    }
    
    if (mode && mode.toLowerCase() == 'rail') {
        if (autoSuggestTextBox && document.getElementById(autoSuggestTextBox)) {
            //get stops data based on wheater group station to show or not
            // extend the autoSuggestTextBox by calling AutoSuggestExtender's extend method
            var dropdowndata = null;  //stopList.instance.getStops(showGroup ? stopType.All : stopType.NonGroup);
            // implements auto-suggest functionality
            AutoSuggestExtender.extend(autoSuggestTextBox, dropdowndata, triggerLength, null, filterIds.split(","), 'test', hdnfieldId, 'naptan', maxResults, 'stop', stopSortCallBack, showGroup ? stopList.instance.searchStops : stopList.instance.searchStopsWithNoGroups);
        }
    }


}

// <summary>
// Callback method gets called by AutoSuggest to custom sort the stops in dropdown
// </summary>
// <param name="data">Data needs to be sorted(In this case its an array of stop objects)</param>
// <param name="searchString">Search criteria entered by user</param>
// NOTE : We need a custom sort as follows:
//      * stop with matching short code will show first
//      * stop representing group will show after stop with matching short code
//      * Then the stop with matching description will show alphabetically
function stopSortCallBack(data, searchString) {

    searchString = searchString.toLowerCase();
    data = data.sort(function(a, b) {
        var matchValue = 0;
        
        // first check if both object got short codes
        if (a.shortCode != null && b.shortCode != null) {
            if (a.shortCode.toLowerCase() == searchString && b.shortCode.toLowerCase() != searchString)
                matchValue = -1;
            else if (a.shortCode.toLowerCase() != searchString && b.shortCode.toLowerCase() == searchString) {
                matchValue = 1;
            }
            else if (a.shortCode.toLowerCase() != searchString && b.shortCode.toLowerCase() != searchString && a.isGroup && !b.isGroup)
                matchValue = -1;
            else if (a.shortCode.toLowerCase() != searchString && b.shortCode.toLowerCase() != searchString && !a.isGroup && b.isGroup)
                matchValue = 1;
            else
                matchValue = a.getDisplayName().localeCompare(b.getDisplayName());
        }
        else if (a.shortCode == null && b.shortCode != null) { // only one got shortcode
        if (b.shortCode.toLowerCase() == searchString)
                matchValue = 1;
            else if (a.isGroup && !b.isGroup)
                matchValue = -1;
            else if (!a.isGroup && b.isGroup)
                matchValue = 1;
            else
                matchValue = a.getDisplayName().localeCompare(b.getDisplayName());
        }
        else if (a.shortCode != null && b.shortCode == null) { // only one got short code
        if (a.shortCode.toLowerCase() == searchString)
                matchValue = -1;
            else if (a.isGroup && !b.isGroup)
                matchValue = -1;
            else if (!a.isGroup && b.isGroup)
                matchValue = 1;
            else
                matchValue = a.getDisplayName().localeCompare(b.getDisplayName());
        } 
        else { // both not got short code
            if (a.isGroup && !b.isGroup)
                matchValue = -1;
            else if (!a.isGroup && b.isGroup)
                matchValue = 1;
            else
                matchValue = a.getDisplayName().localeCompare(b.getDisplayName());
        }

        return matchValue;
    });

    return data;
}

function searchForStop(stops, searchValue, startIndex, propName){

    // Strip off any "special" chars which affects the finding of the start index
    var I = removeSpecialCharacters(searchValue.toUpperCase().trim());
    
    var E = startIndex;

    var A = stops.length - 1;

    if (stops.length > 0) {
        while (E <= A) {

            var G = Math.floor((E + A) / 2);

            var B = stops[G].toString().toUpperCase().trim();

            if (propName !== undefined)
                B = stops[G][propName].toUpperCase().trim();

            // Strip off any "special" chars in the search compare object which
            // would affect finding an appropriate start index before the search value
            B = removeSpecialCharacters(B);
            
            if (B < I) {
                E = G + 1;
            } else {
                if (B > I) {
                    A = G - 1;
                } else {
                   
                    return G;
                }
            }
        }
    }

    // if below start index, then reset to 0 as may cause issue if start index is declared as -1
    if (A == -1)
        A = 0;

    return E > A ? A : E;
}

// <summary>Removes any characters from a text string which could affect the search for a stop</summary>
function removeSpecialCharacters(textString) {

    if (textString != null) {

        var editedString = textString;

        editedString = editedString.replace(/\'/gi, '');
        editedString = editedString.replace(/-/gi, '');

        return editedString;
    }
    else {
        return textString;    
    }
}