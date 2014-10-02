// ***********************************************
// NAME     : TDInfoWindow.js
// AUTHOR   : Atos Origin
// ************************************************

// Content Strings
var roadWorks = "RoadWorks";
var roadIncident = "RoadIncident";

/* Utility Funcitons */
// Defines javasript namespace
function namespace(namespaceString) {
    var parts = namespaceString.split('.'),
        parent = window,
        currentPart = '';

    for (var i = 0, length = parts.length; i < length; i++) {
        currentPart = parts[i];
        parent[currentPart] = parent[currentPart] || {};
        parent = parent[currentPart];
    }

    return parent;
}

// Transport Direct client side namespace
var tdClientSide = namespace('TransportDirect.UserPortal.Client');

// Info window location enum
tdClientSide.InfoWindowAnchor = { UpperLeft: 0, UpperRight: 1, LowerLeft: 2, LowerRight: 3 };




// Transport Direct client side info window object defination
tdClientSide.InfoWindow = function() {

    this.templateString = "<div id=\"${id}\" class=\"tdinfowindow ${topClass} ${theme}\">\r\n<div style=\"position:relative;\">\r\n <div class=\"window\" id=\"${id}_window\" >\r\n<div class=\"top\">\r\n<div class=\"left\" id=\"${id}_topleft\"><div class=\"sprite\"></div></div>\r\n    \t\t<div class=\"right\" id=\"${id}_topright\">\r\n    \t\t\t<div class=\"sprite\"></div>\r\n    \t\t\t<div class=\"user\" id=\"${id}_user\">\r\n    \t\t\t<div class=\"titlebar\">\r\n    \t\t\t<a class=\"close\" id=\"${id}_hide\"><div class=\"sprite\"></div></a>\r\n<div class=\"title\">${title}</div>\r\n    \t\t\t </div>\r\n<div class=\"border\"></div>\r\n    \t\t\t<div class=\"layout content\" id=\"${id}_content\">${content}\r\n    \t\t\t</div>\r\n    \t\t\t</div>\r\n    \t\t</div>\r\n<div class=\"bottom\">\r\n<div class=\"left\" id=\"${id}_bottomleft\"><div class=\"sprite\"></div></div>\r\n\t\t<div class=\"right\" id=\"${id}_bottomright\"><div class=\"sprite\"></div></div\r\n        ></div\r\n      ></div\r\n    ></div\r\n    ><div class=\"pointer ${anchor}\" id=\"${id}_pointer\"><div id=\"${id}_sprite\" class=\"sprite\"></div></div\r\n  ></div\r\n></div>\r\n";
    this.anchor = tdClientSide.InfoWindowAnchor.UpperRight;
    this.isVisible = false;
    this.isContentVisible = true;
    this.IsHeaderVisible = true;
    this.width = 150;
    this.minheight = 75;
    this.id = null;
    this.theme = "transportDirect";
    this._hide = null;
    this.topClass = "";

    title = "TD info window";
    content = "";
    templateStart = "\\$\\{";
    templateEnd = "\\}";
    popupElem = null;
    idPostfix = "_infowindow";
    templatedString = null;
    winlocation = null;

    var container = document.createElement('div');

    var popupElemId = null, _window = null, _topleft = null, _topright = null, _user = null, _content = null, _bottomleft = null, _bottomright = null;
    var _sprite = null, _pointer = null;


    document.body.appendChild(container);
    
    // Applys the template specified value
    this.applyToTemplate = function(id, value) {
        var templateToMatch = templateStart + id + templateEnd;
        var templateRegex = new RegExp(templateToMatch, 'gi');

        templatedString = templatedString.replace(templateRegex, value);
    };

    // Gets the anchor enum value at which to display the popup
    this.getAnchorValue = function(anchor) {
        if (anchor == tdClientSide.InfoWindowAnchor.UpperLeft)
            return "upperleft";
        else if (anchor == tdClientSide.InfoWindowAnchor.UpperRight)
            return "upperright";
        else if (anchor == tdClientSide.InfoWindowAnchor.LowerLeft)
            return "lowerleft";
        else if (anchor == tdClientSide.InfoWindowAnchor.LowerRight)
            return "lowerright";
    }

    // Refreshed the template values
    this.refreshTemplate = function() {

        templatedString = this.templateString;

        this.applyToTemplate("id", this.id + idPostfix);

        this.applyToTemplate("title", this.title);

        this.applyToTemplate("content", this.content);

        this.applyToTemplate("anchor", this.getAnchorValue(this.anchor));

        this.applyToTemplate("theme", this.theme);

        this.applyToTemplate("topClass", this.topClass);


        container.innerHTML = templatedString;

        popupElemId = this.id + idPostfix;

        popupElem = document.getElementById(popupElemId);

        this._populateLayoutElements();

    };

    // Populates the information windows elements for manipulation
    this._populateLayoutElements = function() {
        _window = document.getElementById(popupElemId + "_window");
        _topleft = document.getElementById(popupElemId + "_topleft");
        _topright = document.getElementById(popupElemId + "_topright");
        _user = document.getElementById(popupElemId + "_user");
        _content = document.getElementById(popupElemId + "_content");
        _bottomleft = document.getElementById(popupElemId + "_bottomleft");
        _bottomright = document.getElementById(popupElemId + "_bottomright");
        _sprite = document.getElementById(popupElemId + "_sprite");
        _pointer = document.getElementById(popupElemId + "_pointer");
        this._hide = document.getElementById(popupElemId + "_hide");
    };

    // Gets the location at which to display information popup
    this._getPopupLocation = function(coord, displayAnchor) {
        var xPad = 30;
        var yPad = 30;
        var actualCoord = null;
        var actualAnchor = tdClientSide.InfoWindowAnchor.LowerRight;
        var screenHeight = window.screen.availHeight;
        var screenWidth = window.screen.availWidth;

        if (displayAnchor == tdClientSide.InfoWindowAnchor.UpperRight) {
            if (coord.X + this.width + xPad < screenWidth && coord.Y - this.height - yPad > 0)
                return { coordinates: coord, anchor: displayAnchor };
            else
                return this._getPopupLocation(coord, tdClientSide.InfoWindowAnchor.UpperLeft);
        }
        else if (displayAnchor == tdClientSide.InfoWindowAnchor.UpperLeft) {
            if (coord.X - this.width - xPad > 0 && coord.Y - this.height - yPad > 0)
                return { coordinates: coord, anchor: displayAnchor };
            else
                return this._getPopupLocation(coord, tdClientSide.InfoWindowAnchor.LowerLeft);
        }
        else if (displayAnchor == tdClientSide.InfoWindowAnchor.LowerLeft) {
            if (coord.X - this.width - xPad > 0 && coord.Y + this.height + yPad < screenHeight)
                return { coordinates: coord, anchor: displayAnchor };
            else
                return this._getPopupLocation(coord, tdClientSide.InfoWindowAnchor.LowerRight);
        }


        return { coordinates: coord, anchor: displayAnchor };


    }

    // Layout the popup information windows
    // Calculated if the anchor location specified is right to display popup or it needs changing
    this._layout = function(coord) {
        popupElem.style.zIndex = "9999";
        popupElem.style.display = "block";

        positionObj = this._getPopupLocation(coord, this.anchor);
        x = positionObj.coordinates.X;
        y = positionObj.coordinates.Y;
        displayAnchor = positionObj.anchor;

        _pointer.className = _pointer.className.replace(this.getAnchorValue(this.anchor), this.getAnchorValue(displayAnchor));

        popupElem.style.left = x + "px";
        var offset = (window.pageYOffset || document.documentElement.scrollTop);
        popupElem.style.top = (y + offset) + "px";

        _bottomright.style.width = (this.width - 5) + "px";
        _topright.style.width = this.width + "px";
        _user.style.width = (this.width - 8) + "px";

        var winheight = (_content.offsetHeight < this.minheight ? this.minheight : _content.offsetHeight) + 50;

        if (displayAnchor === tdClientSide.InfoWindowAnchor.UpperLeft) {
            _window.style.right = (this.width + 18) + "px";
            _window.style.bottom = (winheight + 50) + "px";
        }

        else {
            if (displayAnchor === tdClientSide.InfoWindowAnchor.UpperRight) {
                _window.style.left = "6px";
                _window.style.bottom = (winheight + 50) + "px";
            }

            else {
                if (displayAnchor === tdClientSide.InfoWindowAnchor.LowerRight) {
                    _window.style.right = "6px";
                    _window.style.top = "43px";
                }
                else {
                    if (displayAnchor === tdClientSide.InfoWindowAnchor.LowerLeft) {
                        _window.style.right = (this.width + 18) + "px";
                        _window.style.top = "43px";
                    }
                }
            }
        }

        _topleft.style.height = winheight + "px";
        _topleft.style.marginLeft = this.width + "px";

        _topright.style.height = winheight + "px";


        _content.style.height = winheight + "px";

        _bottomleft.style.marginLeft = this.width + "px";
        _bottomleft.style.marginTop = winheight + "px";

        _bottomright.style.marginTop = winheight + "px";

        this._hide.style.marginLeft = (this.width - 22) + "px";
    };

    // Destroys the information window
    this._destroy = function() {
        this.hide();
        container.innerHTML = "";
        document.body.removeChild(container);
    };
    
    // Close event
    this.onClose = function() { };
};

// Initialised the popup information window
tdClientSide.InfoWindow.prototype.init = function() {
    if (!this.id) {
        this.id = Math.floor(Math.random() * 100);
    }


    this.refreshTemplate();



    popupElem.style.display = "none";

}

// Displays the popup information window
tdClientSide.InfoWindow.prototype.show = function(x, y) {
    winlocation = { X: x, Y: y };
    this._layout(winlocation);
    var self = this;
    self._hide.onclick = function(e) { self.hide(); }
    this.isVisible = true;

}

// Hides the popup information window
tdClientSide.InfoWindow.prototype.hide = function() {
    this.onClose();
    popupElem.style.display = "none";
    this.isVisible = false;
}

// Sets the title to display for the information window
tdClientSide.InfoWindow.prototype.setTitle = function(title) {

    this.title = title;

    
}

// Sets the content to display in the information window
tdClientSide.InfoWindow.prototype.setContent = function(content) {
    this.content = content;


}

// Refreshes the information window
tdClientSide.InfoWindow.prototype.refresh = function() {

    this.refreshTemplate();

    if (this.isVisible) {
        this._layout(winlocation);

        var self = this;
        self._hide.onclick = function(e) { self.hide(); }
    }
}

// Destroys the information window
tdClientSide.InfoWindow.prototype.destroy = function() {

    this._destroy();

}



// Defines travel news information window
function TravelNewsInfoWindow() {
    var topWindow = null;

    var templatedString = null;
    var templateStart = "\\$\\{";
    var templateEnd = "\\}";

    
    
}

// Template for the content of the information window
TravelNewsInfoWindow.tnTemplate = "<div class=\"tnWindow-Content\"><div><table cellspacing=\"0\" cellpadding=\"1\"><tbody><tr><td class=\"travelNewsField\">Headline:</td><td class=\"travelNewsBody\">${Headline}</td></tr><tr><td class=\"travelNewsField\">Incident Type:</td><td class=\"travelNewsBody\">${Type}</td></tr><tr><td class=\"travelNewsField\">Severity:</td><td class=\"travelNewsBody\">${Severity}</td></tr><tr><td class=\"travelNewsField\">Detail:</td><td class=\"travelNewsBody\">${Detail}</td></tr><tr><td class=\"travelNewsField\">Start Date:</td><td class=\"travelNewsBody\">${StartDate}</td></tr><tr><td class=\"travelNewsField\">End Date:</td><td class=\"travelNewsBody\">${EndDate}</td></tr><tr><td class=\"travelNewsField\">Last Updated:</td><td class=\"travelNewsBody\">${LastUpdated}</td></tr><tr><td class=\"travelNewsField\"></td><td class=\"travelNewsBody\"><span class=\"cjperroreight\">${TOIDs}</span></td></tr></tbody></table></div></div>";

// Initialises the travel news information window content template
TravelNewsInfoWindow.initTemplate = function() {
    templatedString = this.tnTemplate;
}

// Method to apply the values to the travel news information content template
TravelNewsInfoWindow.applyToTemplate = function(id, value) {

    var templateToMatch = templateStart + id + templateEnd;
    var templateRegex = new RegExp(templateToMatch, 'gi');

    templatedString = templatedString.replace(templateRegex, value);
}

// Static method to show the Travel news information popup
TravelNewsInfoWindow.showTNInfo = function(evt, uid, title, loadingImageUrl) {
    var event = window.event || evt;
    var targ;

    if (event.target) targ = event.target;
    else if (event.srcElement) targ = event.srcElement;
    if (targ.nodeType == 3) // defeat Safari bug
        targ = targ.parentNode;

    var win = this.topWindow;

    if (win) {
        win.destroy();
        win = null;
    }
    
    // Create information window
    win = new TransportDirect.UserPortal.Client.InfoWindow();
    this.topWindow = win;
    win.topClass = "tninfowindow";
    win.height = 200;
    win.width = 300;
    win.init();
    win.setTitle(title);
    win.setContent("<strong><img src=\""+ loadingImageUrl + "\" alttext=\"Loading...\" title=\"Loading...\" /></strong>");
    win.refresh();
    win.show(event.clientX, event.clientY);

    // Ajax call to get the travel news details
    try {
        GetTravelNewsById(uid, this.updateInfoWindow, this.raiseError, this);
    } catch (error) {
        //alert("ajax error" + error.get_message() + error.get_stackTrace());
    }

}

// Method for ajax call back to get the details of the travel news item
function GetTravelNewsById(uid,/*method to call on success of the call*/ OnSucceeded, /*method to call on failure*/OnFailed, context) {

    var webServicePath = getPageServiceUrl('~/webservices/TDWebService.aspx')
    // Invoke the web service method
    Sys.Net.WebServiceProxy.invoke(webServicePath,
        "GetTravelNewsById", false,
        { "uid": uid},
        OnSucceeded, OnFailed, context, 3000);

}

// Builds and returns the url for the page service
function getPageServiceUrl(partialUrl) {

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


// Updates the travel news information window
TravelNewsInfoWindow.updateInfoWindow = function(/*result of the call*/result, /*Context set for the call*/userContext, /*Name of the server method called*/methodName) {

    userContext.initTemplate();

    userContext.applyToTemplate("Headline", result[3]);
    userContext.applyToTemplate("Type", result[1]);
    userContext.applyToTemplate("Severity", result[5]);
    userContext.applyToTemplate("Detail", result[4]);
    userContext.applyToTemplate("StartDate", result[6]);
    userContext.applyToTemplate("EndDate", result[7]);
    userContext.applyToTemplate("LastUpdated", result[8]);

    // Check if TOIDs should be displayed
    var userType = document.getElementById("FooterControl1_hdnUserLevel");
    if ((userType) && (userType.value > 0)){
            userContext.applyToTemplate("TOIDs", result[12]);    
    }
    else {
        userContext.applyToTemplate("TOIDs", "");
    }

    userContext.topWindow.setContent(templatedString);

    userContext.topWindow.refresh();

}


// Raises the error in case of the ajax call error
TravelNewsInfoWindow.raiseError = function(error, userContext) {
   
    userContext.topWindow.setContent("Error getting travel news item details. Please try again!");

    userContext.topWindow.refresh();
    
}

// Shows information window popup for the high traffic symbol in road journey detail
TravelNewsInfoWindow.showRoadQueuePopup = function(evt, popupTitle, popupContent) {
    var event = window.event || evt;
    var targ;

    if (event.target) targ = event.target;
    else if (event.srcElement) targ = event.srcElement;
    if (targ.nodeType == 3) // defeat Safari bug
        targ = targ.parentNode;

    var altText = targ.title;
    targ.title = "";


    var win = this.topWindow;

    if (win) {
        win.destroy();
        win = null;
    }

    win = new TransportDirect.UserPortal.Client.InfoWindow();
    this.topWindow = win;
    win.topClass = "tninfowindow";
    win.height = 100;
    win.width = 200;
    win.init();
    win.setTitle(popupTitle);
    win.setContent(popupContent);
    win.refresh();
    win.show(event.clientX, event.clientY);
}




