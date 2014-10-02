// ***********************************************
// NAME     : Common.js
// AUTHOR   : Atos Origin
// ************************************************ 

function ElementVisible(elementId, visibility)
{
	var thisElement = document.getElementById(elementId);
	if (thisElement && thisElement.style)
	{
		if (visibility == true)
		{
			thisElement.style.display='';
		}
		else
		{
			thisElement.style.display='none';
		}
	}
}


function toggleVisibility(elementID) {

    var thisElement = document.getElementById(elementID);

    if (thisElement.style.display == 'none')

        thisElement.style.display = '';

    else

        thisElement.style.display = 'none';
}

function toggleInputBoxValue(elementID, valDefault, valToggleTo) {
    
    var thisElement = document.getElementById(elementID);

    if (thisElement != undefined && thisElement) {
        if (thisElement.nodeName.toLowerCase() == 'input') {
            if (thisElement.value == valDefault)

                thisElement.value = valToggleTo;

            else
            
                thisElement.value = valDefault
        }
    }
}

function toggleCSSClass(cssclass1, cssclass2) {

    var elements = document.getElementsByTagName('*');
    for (i = 0; i < elements.length; i++) {
        if (elements[i].className == cssclass1) {
            elements[i].className = cssclass2;
        }
        else if (elements[i].className == cssclass2) {
            elements[i].className = cssclass1;
        }
    }
}

// sets the visibility of a TDButton, which has mouse out and over css styles
// Requires jQuery.js file
function showTDButton(show, $button) {

    if ($button.length > 0) {
        var classShow = 'show';
        var classHide = 'hide';

        if (!show) {
            // Hiding the button, switch the classes
            classShow = classHide;
            classHide = 'show';
        }

        $button.removeClass(classHide);
        $button.addClass(classShow);

        var cssOnMouseOut = $button.attr('onmouseout');
        var cssOnMouseOver = $button.attr('onmouseover');

        if (cssOnMouseOut.length > 0) {
            cssOnMouseOut = cssOnMouseOut.replace(classHide, classShow);
            $button.attr('onmouseout', cssOnMouseOut);
        }
        if (cssOnMouseOver.length > 0) {
            cssOnMouseOver = cssOnMouseOver.replace(classHide, classShow);
            $button.attr('onmouseover', cssOnMouseOver);
        }
    }
}

// Displays any controls which only work with javascript by removing the js css class
// Requires jQuery.js file
function displayJavascriptControls() {
    $('div').removeClass('jshide');
    $('a').removeClass('jshide');
    $('input').removeClass('jshide');
    $('div').removeClass('jsshow');
}



// Validates a date
//isDate(y: Integer, m: Integer, d: Integer): Integer
//    		Checks a date and returns 0 if it's valid or one of the error codes bellow.

//    y        year
//    m        month
//    d        day

//isDate(date: String, matcher: RegExp, map: Object): Integer
//    		Checks a date and returns 0 if it's valid or one of the error codes bellow.
//    date        date in a string form
//    matcher     regular expression responsible to find and store the day, month and year
//    map        object containing the position where each date component is localized inside the regular expression. Its format is the following: {d: positionOfTheDay, m: positionOfTheMonth, y: positionOfTheYear}
//
// returns error codes:
//      0 = Valid date
// 	    1 = Date format invalid (regular expression failed or amount of arguments != 3)
//	    2 = Day isn't between 1 and 31
// 	    3 = Month isn't between 1 and 12
// 	    4 = On April, June, September and November there isn't the day 31
// 	    5 = On February the month has only 28 days
// 	    6 = Leap year, February has only 29 days
//
// Reference : http://jsfromhell.com/geral/is-date

function isDate(y, m, d) {
    if (typeof y == "string" && m instanceof RegExp && d) {
        if (!m.test(y)) return 1;
        y = RegExp["$" + d.y], m = RegExp["$" + d.m], d = RegExp["$" + d.d];
    }
    d = Math.abs(d) || 0, m = Math.abs(m) || 0, y = Math.abs(y) || 0;
    return arguments.length != 3 ? 1 : d < 1 || d > 31 ? 2 : m < 1 || m > 12 ? 3 : /4|6|9|11/.test(m) && d == 31 ? 4
        : m == 2 && (d > ((y = !(y % 4) && (y % 1e2) || !(y % 4e2)) ? 29 : 28)) ? 5 + !!y : 0;
};

// Checks if date is in past
function isDateInPast(date) {
    if (!(date instanceof Date)) {
        return false;
    }
    else {
        var today = new Date();
        today.setHours(0, 0, 0, 0);
        return date.getTime() < today.getTime();
    }
}



// Open page and return "true" if open was unsuccessful
function openWindow(url) {
    var x = window.open(url);
    return (x == null);
}

// Gets the html element with matching serverId
// This method may return multiple elements. To restrict result pass the most closest parent container
function getAspElement(elemServerId, selector, container) {
    var elem = null;

    if (container) {
        elem  = $(container).find(selector + '[name$="' + elemServerId + '"]');
    }
    else
        elem = $(selector + '[name$="' + elemServerId + '"]');

    if ($(elem).length <= 0) {
        if (container) {
            elem = $(container).find(selector + '[id$="' + elemServerId + '"]');
        }
        else
            elem = $(selector + '[id$="' + elemServerId + '"]');
    }

    return $(elem);

}

// Gets the  hidden elements inside div with jssettings class
function getJSHdnSettingFieldValue(elemServerId, container) {

    var hdnElem = getAspElement(elemServerId, 'div.jssettings input:hidden', container);

    if (hdnElem)
        return hdnElem.val();

    return null;
}

// Gets the  hidden elements inside div with jssettings class
function setJSHdnSettingFieldValue(elemServerId, container, value) {

    var hdnElem = getAspElement(elemServerId, 'div.jssettings input:hidden', container);

    if (hdnElem)
        return hdnElem.val(value);

    return null;
}