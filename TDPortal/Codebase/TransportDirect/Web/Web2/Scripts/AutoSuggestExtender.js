// ***********************************************
// NAME     : AutoSuggestExtender.js
// AUTHOR   : Atos Origin
// ************************************************

// <summary>hash table implementation</summary>
// Ref: http://www.mojavelinux.com/articles/javascript_hashes.html
function hashTable() {
    this.length = 0;
    this.items = new Array();
    for (var i = 0; i < arguments.length; i += 2) {
        if (typeof (arguments[i + 1]) != 'undefined') {
            this.items[arguments[i]] = arguments[i + 1];
            this.length++;
        }
    }

    this.removeItem = function(in_key) {
        var tmp_previous;
        if (typeof (this.items[in_key]) != 'undefined') {
            this.length--;
            var tmp_previous = this.items[in_key];
            delete this.items[in_key];
        }

        return tmp_previous;
    }

    this.getItem = function(in_key) {
        return this.items[in_key];
    }

    this.setItem = function(in_key, in_value) {
        var tmp_previous;
        if (typeof (in_value) != 'undefined') {
            if (typeof (this.items[in_key]) == 'undefined') {
                this.length++;
            }
            else {
                tmp_previous = this.items[in_key];
            }

            this.items[in_key] = in_value;
        }

        return tmp_previous;
    }

    this.hasItem = function(in_key) {
        return typeof (this.items[in_key]) != 'undefined';
    }

    this.clear = function() {
        for (var i in this.items) {
            delete this.items[i];
        }

        this.length = 0;
    }
}

//<summary>AutoSuggestExtender object</summary>
function AutoSuggestExtender() {

}
//<summary>
//Provides static hastTable implementation enables single point of access 
//to get all the auto - suggest dropdown object on the page using the text box client id for which auto-suggest functionality implemented
//</summary>
AutoSuggestExtender.autoSuggestItems = new hashTable();

//<summary>Static method enables to extend textbox with auto-suggest functionality. Encapsulates core auto-suggest functionality</summary>
//<param name="elemToExtend">Textbox control id which needs auto-suggest functionality</param>
//<param name="autoSuggestData">Auto-suggest dropdown data</param>
//<param name="dropdownTriggerLength">Number of charactes which should be entered in the dropdown to trigger the auto-suggest</param>
//<param name="filterProperties">Properties of auto-suggest data item which should be used to match the search string</param>
//<param name="filterControls">Array of filter control ids whose values should be taken in to accoung when filtering data</param>
//<param name="filterTestFunction">Custom function to filter data</param>
//<param name="valueField">Control to which selected value should be copied apart from the text box extended (i.e. hidden field)</param>
//<param name="valueFieldSrc">Property name of the object whose value needs to be copied to valueField</param>
//<param name="maxResults">Maximum results needs showing in the dropdown</param>
//<param name="objType">Type of object of the autosuggest data</param>
//<param name="sortCallBack">Callback method to sort the filtered data</param>
AutoSuggestExtender.extend = function(elemToExtend, autoSuggestData, dropdownTriggerLength, filterProperties, filterControls, filterTestFunction, valueField, valueFieldSrc, maxResults, objType, sortCallBack, dataCallBack) {
if (elemToExtend && document.getElementById(elemToExtend)) {
        var autoSuggestBox = new autoSuggest(elemToExtend, autoSuggestData, dropdownTriggerLength, dataCallBack);

        autoSuggestBox.setFilterProperties(filterProperties);
        autoSuggestBox.setSuggestionObjType(objType);
        autoSuggestBox.setFilterTestFunction(filterTestFunction);
        autoSuggestBox.setFilterControls(filterControls);
        autoSuggestBox.setSelectedValueControl(valueField, valueFieldSrc);
        autoSuggestBox.setMaxResult(maxResults);
        if (sortCallBack) {
            autoSuggestBox.setSortCallBack(sortCallBack);
        }
        
        // Register auto - suggest functionality
        autoSuggestBox.register();
        
        // Add it to hash table with the key as id of the element auto-suggest extending
        AutoSuggestExtender.autoSuggestItems.setItem(elemToExtend, autoSuggestBox);

    }
}

//<summary>Enables/Disabled the auto-suggest functionality for auto - suggest target text box </summary>
//<param name="elemToExtend">Auto-suggest target textbox control client Id</param>
//<param name="enabled">When true enables the auto-suggest and disables when passed false </param>
AutoSuggestExtender.toggle = function(elemToExtend, enabled) {
    autoSuggestBox = AutoSuggestExtender.autoSuggestItems.getItem(elemToExtend);
    if (autoSuggestBox) {
        autoSuggestBox.enabled = enabled;
    }
}


//<summary>Extends regex object to Handles the Regex escape characters</summary>
//<param name="text">Search Criteria</param>
RegExp.escape = function(text) {
    if (!arguments.callee.sRE) {
        var specials = [
      '/', '.', '*', '+', '?', '|',
      '(', ')', '[', ']', '{', '}', '\\'
    ];
        arguments.callee.sRE = new RegExp(
      '(\\' + specials.join('|\\') + ')', 'g'
    );
    }
    return text.replace(arguments.callee.sRE, '\\$1');
}


//<summary></summary>
//<param name="elemToExtendId"></param>
//<param name="autoSuggestData"></param>
//<param name="dropdownTriggerLength"></param>
function autoSuggest(elemToExtendId, autoSuggestData, dropdownTriggerLength, autoSuggestDataCallBack) {
    
    /* #region  Private members */
        
    var elemToExtend = null;
    var divElem = null;
    var pickList = null;
    var registered = false;
    var visible = false;
    var highlighted = -1;

    var elemToExtent_hasfocus = false;
    var pickList_hasfocus = false;

    var maxResults = 20;

    var selectedItem = null;

    var self = this;

    var picklistItems = [];


    var displaySrc = 'toString';
    var valueSrc = 'name';
    var filterProperties = [];
    var filterControls = [];
    var filterCallBack = null;
    var suggestionType = 'Array';
    var sortCallBack = null;

    var selectedValueSrc = '';
    var hiddenValueId = '';
    
    var filterEscapeRegex = /^(\([A-Z,a-z]{3}\)\s{1}){1}/i;

    this.enabled = true;

    var adjustForScroll = true;

    var scrollToView = false;

    var searches = new Array();

    var mouseDrag = false;

    var currentWidth = 0;

    var scrollFirstView = true;
            

    
    /* #endregion  Private members */

    /* #region  Private functions */
    //<summary>
    // Initialises the auto-suggest functinality
    //</summary>
    function _init() {
        registered = _setup();
        if (registered) { _registerEvents(); };
        
    }

    //<summary>Registers events required for auto-suggest functionality</summary>
    function _registerEvents() {
        if (elemToExtend) {
            elemToExtend.onkeydown = _keyDown;
            elemToExtend.onkeyup = _keyUp;
            elemToExtend.onblur = _onBlur;
            if (divElem) {
                divElem.onclick = function(e) {
                    elemToExtend.focus();
                }

                divElem.onmousedown = function(e) {
                    elemToExtend.focus();
                    mouseDrag = true;

                }
                divElem.onmouseup = function(e) {
                    elemToExtend.focus();
                    mouseDrag = true;
                }
                divElem.onmouseup = function(e) {
                    mouseDrag = false;
                }


            }

            // registering mousedown event to body so we can know when to set mousedrag variable to false
            // so we can raise blur event
            // This is implemented as ie don't seemd to handle scrollbar correctly
            document.body.onmousedown = function(e) {
                if (visible) {
                    var elemId = null;
                    if (!e) var e = window.event;
                    var tg = (window.event) ? e.srcElement : e.target;
                    if (tg) {
                        elemId = tg.id;
                    }

                    if (elemId == elemToExtendId || (divElem && elemId == divElem.id) || isDescendant(divElem, tg)) {
                        // do nothing
                    }
                    else {
                        mouseDrag = false;
                        _onBlur();
                    }
                }

            }


            _addEvent(window, "resize", function() { _onResize(); });

        }

    }

    //<summary>The following written to add on resize event as it works different in ie and other browsers</summary>
    function _addEvent(elem, type, eventHandle) {
        if (elem == null || elem == undefined) return;
        if (elem.addEventListener) {
            elem.addEventListener(type, eventHandle, false);
        } else if (elem.attachEvent) {
            elem.attachEvent("on" + type, eventHandle);
        }
    };


    //<summary>Un register auto suggest functionality</summary>
    function _unregister() {
        divElem.innerHTML = '';
        elemToExtend.removeEventListener('onkeydown', _keyDown, false);
        elemToExtend.removeEventListener('onkeyup', _keyUp, false);
        elemToExtend.removeEventListener('onblur', _onBlur, false);
    }

    //<summary>Shows auto-Suggest dropdown</summary>
    function _show() {
        if (self.enabled) {
            _positionDropdown();
            visible = true;
            _toggleVisibility();
            _layout();
        }
        
    }

    //<summary>
    //Hides the auto-suggest and if item selected from the drop down 
    //copies the values to hidden field and text box for which auto-suggest functionality implented
    //</summary>
    function _hide() {
        var hdnField = null;
        if (hiddenValueId && hiddenValueId != '') {
            hdnField = document.getElementById(hiddenValueId);
        }
        if (selectedItem) {

            // elemToExtend.value = selectedItem.description;
            elemToExtend.value = self.selectValue(selectedItem).trim();

            if (hdnField) { // if hidden field provided copy the required value to it
                if (selectedValueSrc && selectedValueSrc != '') {
                    hdnField.value = _getPropertyValue(selectedItem, selectedValueSrc);
                }
                else {
                    hdnField.value = '';
                }
            }

        }
        
        highlighted = -1;
        picklistItems = [];
        searches = [];
        visible = false;
        _toggleVisibility();
        _hideIFrameHack();
    }

    //<summary>Sets up the necessary html objects for auto - suggest functionality</summary>
    function _setup() {
        /* create the div and unordered list and add on the page */
        elemToExtend = document.getElementById(elemToExtendId);
        if (elemToExtend) {
            elemToExtend.setAttribute("autocomplete", "off");
            _createContainer();
            
            elemToExtend.parentNode.appendChild(divElem);
            _ie6Hack();
            return true;
        }
        else {
            return false;
        }
    }

    //<summary>Filters the auto-suggest data and generates the actual list</summary>
    function _setupList() {
        // reset the highlighted and picklist items before we start filtering
        highlighted = -1;
        picklistItems = [];
        asData = [];
        clearTimeout(_generateListItems);
        if (elemToExtend) {
            elemValue = elemToExtend.value.toLowerCase();
            if (filterEscapeRegex != '')
                elemValue = RegExp.escape(elemToExtend.value.replace(filterEscapeRegex, ''));
            if (elemValue != '') {

                if (autoSuggestDataCallBack) {
                    picklistItems = autoSuggestDataCallBack(elemValue, maxResults);
                   
                }
                else {

                    if (searches.length == 0)
                        asData = autoSuggestData;
                    else
                        asData = searches[searches.length - 1];

                    duffFasterLoop8(asData, elemValue);
                }
               
                if (picklistItems.length > 0) {
                    
                    // Sort the items by calling the call back sort method if provided
                    if (sortCallBack && typeof (sortCallBack) == "function") {
                        picklistItems = sortCallBack(picklistItems, elemValue);
                    }
                    searches = searches.concat(picklistItems);
                    setTimeout(_generateListItems, 25);
                    
                }
                else { // we not got a single picklist item make sure we not showing the empty list 
                    selectedItem = null;
                    _hide(); 
                }
            }
            else {
                selectedItem = null;
                _hide();
            }
        }

        
    }

    function duffFasterLoop8(asData, elemValue) {
        var testVal = 0;
        
        var n = asData.length % 8;

        if (n > 0) {
            do {
                testAutoSuggestItem(asData[testVal++], elemValue);
            }
            while (--n); // n must be greater than 0 here
        }

        n = parseInt(asData.length / 8);
        if (n > 0) {
            do {
                testAutoSuggestItem(asData[testVal++], elemValue);
                testAutoSuggestItem(asData[testVal++], elemValue);
                testAutoSuggestItem(asData[testVal++], elemValue);
                testAutoSuggestItem(asData[testVal++], elemValue);
                testAutoSuggestItem(asData[testVal++], elemValue);
                testAutoSuggestItem(asData[testVal++], elemValue);
                testAutoSuggestItem(asData[testVal++], elemValue);
                testAutoSuggestItem(asData[testVal++], elemValue);
            }
            while (--n);
        }
    }
    
    

    function testAutoSuggestItem(autoSuggestObj, elemValue) {
        matchValue = false;

        if (filterCallBack) { // if filterCallBack not null call the filter Callback method 
            matchValue = _runObjectFunction(autoSuggestObj, filterCallBack, { 'autoSuggestObj': self, 'searchValue': elemValue });
        }
        else { // use default test function
            var regExp = new RegExp('^' + elemValue, 'i');
            matchValue = _test(regExp, autoSuggestObj);
        }

        if (matchValue) {
            // Check if the object values not matching to the filter controls
            if (!_testFilterControls(autoSuggestObj)) {
                picklistItems.push(autoSuggestObj);
            }
        }
    }

    


    //<summary> Renders list items to show in the drop down</summary>
    function _generateListItems() {
        resultCount = 0;
        _clearList();
        for (var item in picklistItems) {
            var listItem = document.createElement("li");
            listItem.innerHTML = self.displayValue(picklistItems[item]).trim();
            listItem.className = 'autoSuggestListItem';
            listItem.onclick = function(value) {
                return function() {
                    _onItemSelected(value);
                }
            } (picklistItems[item]);
            listItem.onmouseover = function(value) {
                return function() {
                    adjustForScroll = false;
                    scrollToView = false;
                    highlighted = value;
                    _updateSelected();
                }
            } (resultCount);
            pickList.appendChild(listItem);

            resultCount++;

            if (resultCount == maxResults)
                break;


        }

        
        _show();
        _layout();
    }

    //<summary>Default implementation to test auto suggest item to filter</summary>
    //<param name="regExp">Regex build using search criteria</param>
    //<param name="obj">Auto suggest data item</param>
    //<note>This method uses filterProperties to test the search criteria with</note>
    //<return>True if the one of the values of filterProperties matches with search criteria</return>
    function _test(regExp, obj) {
        var matchVal = false;
        for (i in filterProperties) {
            var prop = filterProperties[i];
            var currMatchVal = regExp.test(_getPropertyValue(obj, prop));
            if (currMatchVal) {
                matchVal = currMatchVal;
            }
        }

        return matchVal;

    }

    //<summary>This method checks if auto-suggest item text already showing in one of the controls of filter control</summary>
    //<param name="obj">auto-suggest dropdown object item</param>
    //<return>True if the item object value matches with filter controls</return>
    function _testFilterControls(obj) {
        var matchVal = false;
        for (ctrlCount in filterControls) {
            var ctrl = document.getElementById(filterControls[ctrlCount]);
            if (ctrl && ctrl.value) {
                if (self.selectValue(obj).trim().toLowerCase() == ctrl.value.trim().toLowerCase()) {
                    matchVal = true;
                }
            }
        }

        return matchVal;

    }

    //<summary>Event handler to handle item selected event</summary>
    function _onItemSelected(obj) {
        selectedItem = obj;  
        _hide();

    }

    //<summary>Clears the dropdown</summary>
    function _clearList() {
        pickList.innerHTML = "";
    }

    //<summary>Toggles visiblity of the auto-suggest dropdown</summary>
    function _toggleVisibility() {
        if (divElem) {
            divElem.style.display = visible ? 'block' : 'none';
        }
        _showIFrameHack();
    }

    //<summary>
    // Sets up div container in which unorder list shows which simulates like dropdown
    //</summary>
    function _createContainer() {
        divElem = document.createElement("div");
        divElem.id = elemToExtendId +  "_autoSuggestDiv";
        divElem.style.display = 'none';
        _positionDropdown();
        
        divElem.className = "autoSuggest";

        pickList = document.createElement("ul");
        pickList.style.width = divElem.style.minWidth;
        pickList.className = "autoSuggestList";
        divElem.appendChild(pickList);
    }

    function _ie6Hack() {
        var appVer = navigator.appVersion.toLowerCase();

        var iePos = appVer.indexOf('msie');

        if (iePos != -1) {

            var is_minor = parseFloat(appVer.substring(iePos + 5, appVer.indexOf(';', iePos)));

            var is_major = parseInt(is_minor);

        }

        if (navigator.appName.substring(0, 9) == "Microsoft") { // Check if IE version is 6 or older

            if (is_major <= 6) {

                //Hack to add an iframe under the menu which should hide the selects:

                var iframeShim = document.getElementById(elemToExtend.id + "_hvrShm");

                if (iframeShim != null) {

                    iframeShim.style.visibility = 'visible';

                } else {

                    var newFrame = document.createElement('<IFRAME style="position:absolute;z-index:0;"' +

				      ' src="javascript:false;" frameBorder="0" scrolling="no"' +

				      ' id="' + elemToExtend.id + '_hvrShm" />');

                    elemToExtend.parentNode.appendChild(newFrame);

                    var iframeShim = document.getElementById(elemToExtend.id + "_hvrShm");

                    if (iframeShim) {
                        iframeShim.style.top = divElem.style.top;

                        iframeShim.style.left = divElem.style.left;

                        iframeShim.style.width = (elemToExtend.clientWidth + 5) + 'px';

                        iframeShim.style.height = '0px';
                    }

                }
            }
        }
    }

    //<summary>Key down event handler</summary>
    function _keyDown(oEvent) {
        var hdnField = null;
        
        if (hiddenValueId && hiddenValueId != '')
            hdnField = document.getElementById(hiddenValueId);
            
        if (self.enabled) {
            if (!oEvent)
                var e = window.event;
            var code = (e && e.keyCode) ? e.keyCode : oEvent.keyCode;

            switch (code) {
                case 13: //enter
                    if (visible) {
                        if (highlighted == -1) {
                            highlighted = 0;
                            selectedItem = picklistItems[0];
                        }
                        _hide();
                    }
                    return false;
                    break;
                    
                case 9: //tab
                    //if (visible) {
                        if (highlighted == -1) {
                            highlighted = 0;
                            selectedItem = picklistItems[0];
                        }
                        _hide();
                    //}
                    return true;
                    break;

                case 27: // esc
                    selectedItem = null;
                    _hide();
                    return true;
                    break;

                case 33: //page up    
                case 34: //page down    
                case 36: //home    
                case 35: //end                    

                case 16: //shift    
                case 17: //ctrl    
                case 18: //alt    
                case 20: //caps lock
                case 8: //backspace
                    highlighted = 0;
                    if (hdnField) hdnField.value = '';
                    searches.pop();
                case 46: //delete
                    highlighted = 0;
                    if(hdnField) hdnField.value = '';
                    searches = [];
                case 37: //left arrow
                case 39: //right arrow 
                    return true;
                    break;

                case 38: //up arrow
                    if(hdnField) hdnField.value = '';
                    highlighted >= 0 ? highlighted-- : highlighted;
                    _updateSelected();
                    break;
                case 40: //down arrow
                    if(hdnField) hdnField.value = '';
                    highlighted <= picklistItems.length - 1 && highlighted < maxResults ? highlighted++ : highlighted;
                    _updateSelected();
                    break;


            }
        }



    };

    //<summary>
    // key up event handler
    //</summary>
    function _keyUp(oEvent) {
        clearTimeout(_setupList);
        if (self.enabled) {
            if (!oEvent)
                var e = window.event;
            var code = (e && e.keyCode) ? e.keyCode : oEvent.keyCode;
            switch (code) {
                case 9: //tab
                case 27: //esc
                case 33: //page up    
                case 34: //page down    
                case 36: //home    
                case 35: //end                    
                case 13: //enter key    
                case 16: //shift    
                case 17: //ctrl    
                case 18: //alt    
                case 20: //caps lock  
                case 37: //left arrow
                case 39: //right arrow 
                case 38: //up arrow
                case 40: //down arrow
                    _cancelEvent(oEvent);
                    return true;
                    break;

                case 8: //backspace
                case 46: //delete
                    if (elemToExtend.value.length < dropdownTriggerLength) {
                        selectedItem = null;
                        _hide();
                        _cancelEvent(oEvent);
                        return true;
                        break;
                    }
                default:
                    if (elemToExtend.value.length >= dropdownTriggerLength) {
                        //_clearList();
                        setTimeout(_setupList, 150); 
                        //_setupList();
                    }
                    else {
                        if (visible) _hide();
                    }
                    break;
            }
        }
        _cancelEvent(oEvent);
    }

    //<summary>
    // Updates selected item hightlighting in the auto-suggest list
    // mark an item as selected in the list and change its css class to hightlight it
    //</summary>
    function _updateSelected() {
        if (highlighted < picklistItems.length && highlighted < maxResults && highlighted > -1) {
            for (i in pickList.children) {
                if (i < pickList.children.length) {
                    lItem = pickList.children[i];
                    if (i == highlighted) {

                        if (lItem.className.indexOf('autoSuggestSelectedItem') < 0) {
                            lItem.className = lItem.className.replace('autoSuggestListItem', 'autoSuggestSelectedItem');
                            if(scrollToView)lItem.scrollIntoView(false);
                        }
                        selectedItem = picklistItems[highlighted];
                    }
                    else {
                        if (lItem.className && lItem.className.indexOf('autoSuggestSelectedItem') >= 0) {
                            lItem.className = lItem.className.replace('autoSuggestSelectedItem', 'autoSuggestListItem');
                        }
                    }
                }
            }
            _adjustScroll();
            
        }
    }

    //<summary>
    // Adjusts scroll in the list as the user navigating through items in a list using up and down arrows
    // This is effective only when auto-suggest dropdown showing scrollbar
    //</summary>
    function _adjustScroll() {
        if (adjustForScroll) {
            if (selectedItem) {
                if (selectedItem.offsetTop + selectedItem.clientHeight > divElem.clientHeight) {
                    divElem.scrollTop += selectedItem.scrollHeight;
                }

                if (selectedItem.offsetTop < divElem.scrollTop) {
                    divElem.scrollTop -= selectedItem.scrollHeight + selectedItem.clientHeight;
                }
            }
            adjustForScroll = true;
        }
        
    }

    //<summary>
    // Layouts the auto-suggest dropdown in the case when its near the end of the page
    // displays the drop down with hardcoded max height of 120 px with scrollbar
    //</summary>
    function _layout() {

        divElem.style.maxHeight = divElem.scrollHeight + 'px';

        if (divElem.clientHeight + divElem.offsetTop > screen.availHeight /*document.body.clientHeight*/) {
            divElem.style.maxHeight = "120px";
            divElem.style.overflow = "visible";
            divElem.style.overflowX = "hidden";
            divElem.style.overflowY = "scroll";
        }
        else {
            divElem.style.overflow = "visible";
            divElem.style.overflowX = "hidden";
            divElem.style.overflowY = "hidden";
            scrollToView = true;
        }

         
     
    }

    //<summary>
    // On Blur event handler
    //</summary>
    function _onBlur() {
        if (!mouseDrag) {
            setTimeout(_hideOnBlur, 250);
        }
        else {
            mouseDrag = false;
        }
    };

    //<summary>
    // Positions the dropdown
    //</summary>
    function _positionDropdown() {
        divElem.style.position = 'absolute';
        divElem.style.minWidth = (elemToExtend.clientWidth + 3) + 'px';

        divElem.style.left = getPos(elemToExtend).left;
        divElem.style.top = getPos(elemToExtend).top + elemToExtend.offsetHeight;

    }
    
    //<summary>
    // code to handle the windos resized
    //</summary>
    function _onResize() {
        
        _positionDropdown();
        _layout();
       
    }

    function _hideOnBlur() {
        highlighted = -1;
        picklistItems = [];
        visible = false;
        _toggleVisibility();
        _hideIFrameHack();
    }

    //<summary>
    // Gets property value of the object with name specified
    //</summary>
    //<param name="obj">Object whose property needs accessing</param>
    //<param name="propName">Name of the object property</param>
    _getPropertyValue = function(obj, propName) {
        if (obj && obj[propName] && typeof (obj[propName]) != "function")
            return obj[propName];
        
        return null;
    };

    //<summary>
    //  Implementation of calling method using reflection in javascript
    //</summary>
    //<param name="obj">Object whose method needs calling</param>
    //<param name="funcName">Name of the method to be called</param>
    //<param name="args">Arguments to be passed to method</param>
    //<note>If object is null the method will try to call global function with Name specified</note>
    _runObjectFunction = function(obj, funcName, args){
    
        if (obj) {
            if (obj[funcName] && typeof (obj[funcName]) == "function")
                if(args)
                    return obj[funcName]( args );
                else
                    return obj[funcName] ();
        }
        else {
            return eval(funcName + "(" + (args?args:'') + ")");
        }
        
        return null;
    };

    //<summary>
    //Cross browser cance event 
    //</summary>
    _cancelEvent = function(oEvent) {
        if (!oEvent)
            var e = window.event; // IE
        if (e)
            e.returnValue = false; // IE
        else if (oEvent.preventDefault) { oEvent.preventDefault(); } // Other browsers

        if (e)
            e.cancelBubble = true; // IE
        else if (oEvent.stopPropagation) { oEvent.stopPropagation(); } // Other browsers

    }

    function _hideIFrameHack() {

        //find and remove the iframe:

        var iframeShim = document.getElementById(elemToExtend.id + "_hvrShm");

        if (iframeShim != null) {

            iframeShim.style.visibility = 'hidden';

        }

    }

    function _showIFrameHack() {

        var iframeShim = document.getElementById(elemToExtend.id + "_hvrShm");

       
        if (iframeShim != null) {

            
            iframeShim.style.height = divElem.offsetHeight + 'px';

            iframeShim.style.visibility = 'visible';


        }

    }


    /* #endregion   Private functions */
    
    
    
    /* #region  Privileged functions */
    //<summary>
    // This function gets the text showing in the dropdown for each list item
    // It uses property/function define by displaySrc to call on object to get the text to be shown in dropdown
    //</summary>
    //<param name="obj">Object representing single dropdown item in list</param>
    this.displayValue = function(obj) {

        if (obj && obj[displaySrc] && typeof (obj[displaySrc]) != "function") // display src is a property
            return obj[displaySrc];
        else // display src is a function
            return _runObjectFunction(obj, displaySrc, null);
    };

    //<summary>
    // This function gets the value of selected item in the list
    // It uses property/function define by valueSrc to call on object to get the selected value
    //</summary>
    //<param name="obj">Object representing single dropdown item in list</param>
    this.selectValue = function(obj) {

        if (obj && obj[valueSrc] && typeof (obj[valueSrc]) != "function") 
            return obj[valueSrc];
        else
            return _runObjectFunction(obj, valueSrc, null);
    };

    //<summary>
    // Registers the auto-suggest functionality
    //</summary>
    this.register = function() {
        _init();
    };

    //<summary>
    // unregister the auto-suggest functionality wit the text box
    //</summary>
    this.dispose = function() {
        _unregister();
    };


    //<summary>
    // Source of the value to be shown as a list item in dropdown
    //</summary>
    //<param name="src">function/property name of object which should be called to get the value to show</param>
    this.setDisplaySrc = function(src) {
       
        if(src) displaySrc = src;
    }

    //<summary>
    // Array of property names representing which property values of object needs to look when filtering dropdown data
    //</summary>
    //<param name="filterPropArr">Array of property names</param>
    //<note>If filter test function provided this property will be ignored</note>
    this.setFilterProperties = function(filterPropArr) {
        filterProperties = filterPropArr;
    }

    //<summary>
    //  Sets control ids of the control whose value needs to be taken into account when filtering data to show in dropdown
    //</summary>
    //<param name="filterCtrlArr">Array of control Ids</param>
    //<note>Control Ids should be the client Ids and not the server Ids</note>
    this.setFilterControls = function(filterCtrlArr) {
        filterControls = filterCtrlArr;
    }

    //<summary>Maximum result to show in auto-suggest dropdown</summary>
    //<param name="maxR" type="int">Maximum result value</param>
    this.setMaxResult = function(maxR) {
        maxResults = maxR;
    }

    //<summary>
    // Sets test call back function which can be provided to customise
    // whether object is matching search criteria
    // The test function provided should be accepting Object as a paratmeter
    // this object will be in form {'autoSuggestObj': <reference to this AutoSuggest object>, 'searchValue': <search string>}
    //</summary>
    //<param name="func">Callback test function</param>
    this.setFilterTestFunction = function(func) {
        filterCallBack = func;
    }

    //<summary>Callback method to be called when sorting auto suggest data</summary>
    //<param name="func">Callback func which will sort the data</param>
    this.setSortCallBack = function(func) {
        sortCallBack = func;
    }

    //<summary>Type of object each auto suggest data item represent</summary>
    this.setSuggestionObjType = function(objType) {
        suggestionType = objType;
    }

    //<summary>
    // Sets control whose value needs to be set up upon selection in auto-suggest drop down with the 
    // object property specified with propValue
    //</summary>
    //<param name="ctrlId">Client id of the control whose value needs to be set</param>
    //<param name="propValue">Name of the property of the object which needs to be set as control value specified by ctrlId</param>
    this.setSelectedValueControl = function(ctrlId, propValue) {
        hiddenValueId = ctrlId;
        selectedValueSrc = propValue;
    }

    //<summary>Adds item in to pick list item array to display in drop down</summary>
    this.addPickListItem = function(obj) {
        if (obj) {
            picklistItems.push(obj);
        }
    }
    
    /* #endregion Public functions */

}

//<summary>
// Extends string object and adds method trim to remove leading and trailing spaces from the string
//</summary>
String.prototype.trim = function() {
    str = this.replace(/^\s+/, '');
    for (var i = str.length - 1; i >= 0; i--) {
        if (/\S/.test(str.charAt(i))) {
            str = str.substring(0, i + 1);
            break;
        }
    }
    return str;
}

//<summary>Gets absolute left and top position of the element as IE<8 is not very good to give actual left and top positions</summary>
//<param name="obj">html element obj on the screen</param>
function getPos(obj) {
    var output = new Object();
    var mytop = 0, myleft = 0;
    while (obj) {
        mytop += obj.offsetTop;
        myleft += obj.offsetLeft;
        obj = obj.offsetParent;
        
    }
    output.left = myleft;
    output.top = mytop;
    return output;
}

//<summary>Checks if the element is child element of the parent element</sumamry>
function isDescendant(parent, child) {
    var node = child.parentNode;
    while (node != null && node != document.body) {
        if (node == parent) {
            return true;
        }
        node = node.parentNode;
    }
    return false;
}






