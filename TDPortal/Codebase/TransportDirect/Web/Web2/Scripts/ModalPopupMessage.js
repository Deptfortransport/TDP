// ***********************************************
// NAME     : ModalPopupMessage.js
// AUTHOR   : Atos Origin
// ************************************************ 

/* Simple script to show and hide popup on the page */
var popupResult = false;

// Position and show the popup, relative to an anchor object
function attachPopupMessage(popupId, targetElementId) {

    var targetCtrl = document.getElementById(targetElementId);

    if (targetCtrl) {
        // set the target control's click event to show popup
        targetCtrl.onclick = function(e) { showPopupMessage(e, popupId, targetCtrl); };
    }

    
}

// show the popup
function showPopupMessage(e, popupId, targetControl) {
    // if popup result is false show popup and cancel the click event
    if (!popupResult) {

        showPopup(popupId);
        setupLayout(popupId);
        
        cancelEvent(e);
    }
    else {
        // popup result is true so let the click event to propagate and set popup result to be false for next time
        popupResult = false;
        try {
            document.location = targetControl.getAttribute('href');
        }
        catch (err) {
            //Handle errors here
        }
        
    }
}

// Sets the element in the middle of the display screen
// hides all the other controls
function setupLayout(element) {

    var popupDiv = document.getElementById(element + '_ModalPopupDiv');
    var foregroundElement = document.getElementById(element + '_ForeGround');

    if (popupDiv == null || foregroundElement == null) {
        return;
    }
    
    // Position the modal dialog 
    var scrollLeft = (document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft);
    var scrollTop = (document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop);

    if (scrollTop && scrollTop == 0) {
        scrollTop = getScrollHeight();
    }

    var clientWidth = -1;
    var clientHeight = -1;
    
    // getClientBounds must return dimensions excluding scrollbars, so cannot use window.innerWidth and window.innerHeight.
    if (document.compatMode == "CSS1Compat") {
        // Standards-compliant mode
        clientWidth = document.documentElement.clientWidth;
        clientHeight = document.documentElement.clientHeight;
    }
    else {
        // Quirks mode
        clientWidth = document.body.clientWidth;
        clientHeight = document.body.clientHeight;
    }

    layoutBackgroundElement(element);
    foregroundElementClientRect = foregroundElement.getBoundingClientRect();

    var xCoord = 0;
    var yCoord = 0;
       
    // Setting the left for the foreground element
    var foregroundelementwidth = foregroundElement.offsetWidth ? foregroundElement.offsetWidth : foregroundElement.scrollWidth;
    xCoord = ((clientWidth - foregroundelementwidth) / 2);
    
    if (foregroundElement.style.position == 'absolute') {
        xCoord += scrollLeft;
    }
    foregroundElement.style.left = xCoord + 'px';

   
    // Setting the right for the foreground element
    var foregroundelementheight = foregroundElement.offsetHeight ? foregroundElement.offsetHeight : foregroundElement.scrollHeight;
    yCoord = ((clientHeight - foregroundelementheight) / 2);
    
    //if (foregroundElement.style.position == 'absolute') {
        yCoord += scrollTop;
        //}
    foregroundElement.style.top = yCoord + 'px';

   
    // layout background element again to make sure it covers the whole background 
    // in case things moved around when laying out the foreground element
    layoutBackgroundElement(element);
}

// layout the background of the modal popup
// essentially hides the rest of the screen 
function layoutBackgroundElement(element) {

    var backgroundElement = document.getElementById(element + '_BackGround');

    backgroundElement.style.left = '0px';
    backgroundElement.style.top = '0px';
    
    var clientWidth = -1;
    var clientHeight = -1;

    // getClientBounds must return dimensions excluding scrollbars, so cannot use window.innerWidth and window.innerHeight.
    if (document.compatMode == "CSS1Compat") {
        // Standards-compliant mode
        clientWidth = document.documentElement.clientWidth;
        clientHeight = document.documentElement.clientHeight;
    }
    else {
        // Quirks mode
        clientWidth = document.body.clientWidth;
        clientHeight = document.body.clientHeight;
    }
    
    // In IE quirks mode, document.body.scrollWidth is the one to use,
    // which does not include scroll bars in its value.
    if (document.compatMode != "CSS1Compat") {
        backgroundElement.style.width = document.body.scrollWidth + "px";
        backgroundElement.style.height = document.body.scrollHeight + "px";
    }
    else {
        backgroundElement.style.width = Math.max(Math.max(document.documentElement.scrollWidth, document.body.scrollWidth), clientWidth) + 'px';
        backgroundElement.style.height = Math.max(Math.max(document.documentElement.scrollHeight, document.body.scrollHeight), clientHeight) + 'px';
    }
}


// Sets elements display property to display 
function showPopup(element) {
    var popupDiv = document.getElementById(element + '_ModalPopupDiv');

    if (popupDiv) {
        popupDiv.style.display = 'block';
    }
}

// Hide the popup
function hidePopupMessage(element, returnValue, targetElement) {

    var popupDiv = document.getElementById(element + '_ModalPopupDiv');

    var targetCtrl = document.getElementById(targetElement);

    if (popupDiv) {
        popupDiv.style.display = 'none';
    }

    if (targetCtrl) {
        // if true set the popup result to be true and fire the target control's click event again
        if (returnValue) {
            popupResult = true;
            fireEvent(targetCtrl, 'click')
        }
    }
}

//get the scroll height of the page 
function getScrollHeight() {
    var h = window.pageYOffset ||
           document.body.scrollTop ||
           document.documentElement.scrollTop;

    return h ? h : 0;
}

//Cancel the event - works differently in firefox and ie
function cancelEvent(e) {
    if (!e) e = window.event;
    if (e.preventDefault) {
        e.preventDefault();
    } else {
        e.returnValue = false;
    }
}

// events are fired in firefox and ie different way
// for hyperlinks firefox doesn't fire the normal control.click() event
// so following is a workaround
function fireEvent(obj, evt) {

    var fireOnThis = obj;
    if (fireOnThis.click) {
        fireOnThis.click();
    }
    else if (document.createEvent) {
        var evObj = document.createEvent('MouseEvents');
        evObj.initEvent(evt, true, false);
        fireOnThis.dispatchEvent(evObj);
    } else if (document.createEventObject) {
        var evtObj = document.createEventObject();
        fireOnThis.fireEvent('on' + evt, evtObj);
    }
}

