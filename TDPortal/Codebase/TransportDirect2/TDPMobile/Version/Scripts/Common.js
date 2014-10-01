var currentInputPagePosition = 0;

// Run straight away rather than waiting for page to be ready
setupDeviceStylesheet();

// COMMON PAGE READY FUNCTIONS
$(document).ready(function () {
    displayJavascriptControls();
    setupMenuNav();
    setContentHeight('div#MainContent', true, false);
    maps();
    setupMessagesControl();
    displayMessage();

    // Following lines are to rebind the jquery plugins or js events after ajax update of the update panel
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        displayJavascriptControls();        
        setupJQueryMobileStyling();
        setContentHeight('div#MainContent', true, false);
        maps();
    });
});

// Displays any controls which only work with javascript by removing the js css class
function displayJavascriptControls() {
    $('div').removeClass('jshide');
    $('a').removeClass('jshide');
    $('input').removeClass('jshide');
    $('select').removeClass('jsshow').removeClass('jsshowinline');
    $('div').removeClass('jsshow');
    $('input').removeClass('jsinput');
}

// Rebinds the jquery mobile styling the elements on the page. 
// Assumes there is only one "sjpForm"
function setupJQueryMobileStyling() {
    $('.sjpForm').trigger('create');
}

/// ---------------------------------------------------------
/// ------------------- MENU ----------- --------------------
// Setup the top menu, attaching a click event to the header menu link
function setupMenuNav() {

    $('a[id*=lnkMenuWrap]').toggle(
		function () { displayMenuNav(); }, // show
		function () { displayMenuNav(); } // hide
        );

    // Hide when anywhere on main area clicked
    $(document).on("click", "div[id=MainContent]", function () {
        displayMenuNav(false);
    });

    // Hide when link within menu clicked
    $(document).on("click", "a[id*=lnkMenuLink]", function () {
        displayMenuNav(false);
    });
}

// Show or hide the menu nav
function displayMenuNav(show) {

    var display = false;

    // If visible state is specified
    if (show != null) {
        display = show;
    }
    else {
        // Otherwise determine current state
        if ($('.menuWrap').hasClass("menuWrapUp")) {
            display = false;
        }
        else {
            display = true;
        }
    }

    if (display == true) {
            $('.menuContainer').fadeIn(250);
            $('.menuContainer').attr('aria-live', 'polite');
            $('.menuWrap').addClass("menuWrapUp");

            if (window.hideInputs)
                hideInputs();
        }
        else {
            if (window.showInputs)
                showInputs();

            $('.menuContainer').fadeOut(250);
            $('.menuContainer').attr('aria-live', 'off');
            $('.menuWrap').removeClass("menuWrapUp");
        }

    return false;
}

/// ---------------------------------------------------------
/// ------------------- COMMON FUNCTIONS --------------------

// Open page and return "true" if open was unsuccessful
function openWindow(url) {
    var x = window.open(url);
    return (x == null);
}

// Gets the html element with matching serverId
// This method may return multiple elements. To restrict result pass the closest parent container
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

// Utility function for key press
//Usage:
//$("#input").enterKey(function () {
//    alert('Enter!');
//})
$.fn.enterKey = function (fnc) {
    return this.each(function () {
        $(this).keypress(function (ev) {
            var keycode = (ev.keyCode ? ev.keyCode : ev.which);
            if (keycode == '13') {
                e.preventDefault();
                fnc.call(this, ev);
            }
        })
    })
}

/// ---------------- MESSAGE DISPLAY -------------------------

// Sets up the messages dialog box
function setupMessagesControl() {

    // Indicate the dialogue styling to apply
    $('div[id*=contentMessagesUpdater]').addClass("contentMessagesContainer");

    // Hide messages by default
    hideMessages();

    // Set the dialog position
    var contentHeight = $('div.contentMessages').outerHeight() -31;
    var contentWidth = $('div.contentMessages').outerWidth();
    var windowTop = ($(window).height() / 2) - (contentHeight / 2) - 57;
    var windowLeft = ($(window).width() / 2) - (contentWidth / 2);

    $('div.contentMessages').css({ top: windowTop + 'px' });
    $('div.contentMessages').css({ left: windowLeft + 'px' });
    $('div.contentMessages').css('min-height', contentHeight);

    var documentHeight = $(document).height();
    $('div.contentMessagesContainer').css({ height: documentHeight });

    // Close dialogue event
    $(document).on("click", "a.messageClose", function () {
        if ($(this).data('redirecturl')) {
            // Do redirect to specified url
            window.location.href = $(this).data('redirecturl');
        }
        else {
            hideMessages();
        }

        return false;
    });
}

// Hides the messages div
function hideMessages() {
    $('div.contentMessages div.messages').attr('aria-live', 'off');
    $('div.contentMessagesContainer').css('display', 'none');

    if (currentInputPagePosition >= 0) {
        $(document).scrollTop(currentInputPagePosition);
    }
}

// Displays the messages div, injected with message if required
function displayMessage(message) {

    if (message != null) {
        // Set up the message to display
        var messageLabel = $('<span class="error">' + message + '</span>')

        $('div.contentMessages div.messages').html("");
        $('div.contentMessages div.messages').append(messageLabel);
    }

    // Are there any messages to display?
    if ($('div.contentMessages span').length > 0) {

        currentInputPagePosition = window.pageYOffset;
        $(document).scrollTop(0);

        // Dialogue should have been setup, just display it
        $('div.messageHeaderDiv').css({ display: 'block' });
        $('div.contentMessagesContainer').css({ display: 'block' });

        // Set focus
        $('div.contentMessagesContainer').find('a').focus();

        $('div.contentMessages div.messages').attr('aria-live', 'assertive');
    }
}

/// ---------------------------------------------------------

/// ---------------- CONTENT HEIGHT -------------------------

//Sets the height of the Content Section so that the footer is always at the bottom of the screen
function setContentHeight(contentId, setMinHeight, setHeight, outerContentId) {

    var height = $(window).height()
        - $('div.header').outerHeight()
        - $('div[id*=footer]').outerHeight();

    if (setMinHeight) {
        // Reset
        $(contentId).css('min-height', '');

        // Only set the min-height if its more than the existing min-height
        var currentMinHeight = $(contentId).css('min-height');
        currentMinHeight = currentMinHeight.replace('px', '');

        if (height > currentMinHeight) {
            $(contentId).css('min-height', height + 'px');
        }

        // Ensure outer content page is at least same height (otherwise footer can be obscured) 
        if (outerContentId != null && outerContentId.length > 0) {
            if (height > currentMinHeight) {
                $(outerContentId).css('min-height', height + 'px');
            }
            else {
                $(outerContentId).css('min-height', currentMinHeight + 'px');
            }
        }
    }

    if (setHeight){
        $(contentId).css('height', height + 'px');
    }
}
/// ---------------------------------------------------------

/// ---------------------------------------------------------
/// ---------------------VENIE CYCLE MAP---------------------
// Functionality used in Venue maps and cycle park maps
function maps() {
    /* This function makes a div scrollable with android and iphone */
    $(document).ready(function () {

        function isTouchDevice() {
            /* Added Android 3.0 honeycomb detection because touchscroll.js breaks
            the built in div scrolling of android 3.0 mobile safari browser */
            if ((navigator.userAgent.match(/android 3/i)) ||
				(navigator.userAgent.match(/honeycomb/i)))
                return false;
            try {
                document.createEvent("TouchEvent");
                return true;
            } catch (e) {
                return false;
            }
        }

        function touchScroll(className) {
            if (isTouchDevice()) { //if touch events exist...
                $(className).each(function (index, el) {
                    var scrollStartPosY = 0;
                    var scrollStartPosX = 0;

                    if (el) {
                        el.addEventListener("touchstart", function (event) {
                            scrollStartPosY = this.scrollTop + event.touches[0].pageY;
                            scrollStartPosX = this.scrollLeft + event.touches[0].pageX;
                            //event.preventDefault(); // Keep this remarked so you can click on buttons and links in the div
                        }, false);

                        el.addEventListener("touchmove", function (event) {
                            // These if statements allow the full page to scroll (not just the div) if they are
                            // at the top of the div scroll or the bottom of the div scroll
                            // The -5 and +5 below are in case they are trying to scroll the page sideways
                            // but their finger moves a few pixels down or up.  The event.preventDefault() function
                            // will not be called in that case so that the whole page can scroll.
                            if ((this.scrollTop < this.scrollHeight - this.offsetHeight &&
						    this.scrollTop + event.touches[0].pageY < scrollStartPosY - 5) ||
						    (this.scrollTop != 0 && this.scrollTop + event.touches[0].pageY > scrollStartPosY + 5))
                                event.preventDefault();
                            if ((this.scrollLeft < this.scrollWidth - this.offsetWidth &&
						    this.scrollLeft + event.touches[0].pageX < scrollStartPosX - 5) ||
						    (this.scrollLeft != 0 && this.scrollLeft + event.touches[0].pageX > scrollStartPosX + 5))
                                event.preventDefault();
                            this.scrollTop = scrollStartPosY - event.touches[0].pageY;
                            this.scrollLeft = scrollStartPosX - event.touches[0].pageX;
                        }, false);
                    }
                });
            }
        }

        touchScroll(".wrapper");

        // May be more than one image to zoom
        $(".anchorzoomout").each(function (index, element) {
            $(element).css({ opacity: 0.4 });

            $(element).click(function () {
                $(".wrapper").each(function (i, ele) {
                    $(ele).removeClass("zoomIn");
                });

                $(".anchorzoomout").each(function (i, ele) {
                    $(ele).css({ opacity: 0.4 });
                });

                $(".anchorzoomin").each(function (i, ele) {
                    $(ele).css({ opacity: 1 });
                });
                window.scrollTo(0, 0);
            });
        });

        $(".anchorzoomin").each(function (index, element) {
            $(element).click(function () {
                $(".wrapper").each(function (i, ele) {
                    $(ele).addClass("zoomIn");
                });

                $(".anchorzoomin").each(function (i, ele) {
                    $(ele).css({ opacity: 0.4 });
                });

                $(".anchorzoomout").each(function (i, ele) {
                    $(ele).css({ opacity: 1 });
                });
            });
        });
    });
}
/// ---------------------------------------------------------

/// ---------------------------------------------------------
/// ----------------------- PAGES  --------------------------

// Displays hidden inline page
function displayPage(pageId, pageHideFunction) {

    if (window.hideMessages)
        hideMessages();
    
    setPageBackButton(pageId, pageHideFunction);

    setContentHeight(pageId, true, false, 'div#MainContent');
    $(pageId).css('top', $('div.header').outerHeight());
    $(pageId).css('width', $(window).width() + 'px');
    $(pageId).css('display', 'block');

    if (window.hideInputs)
        hideInputs();   
}

// Hides inline page
function hidePage(pageId) {

    removePageBackButton();

    if (window.showInputs)
        showInputs();

    $(pageId).css('display', 'none');

    setContentHeight('div#MainContent', true, false);

    $('html, body').animate({
        scrollTop: '0px'
    }, 'slow');
}

// Sets up the header page back button to redisplay input page and hide the overlaid page
function setPageBackButton(pageToHide, pageHideFunction) {

    $('.topNavRightDiv').addClass('jshide');

    // Add a temp button click event to disable the postback button click
    
    // Retain existing onClick action if there is one set
    var oldOnClick = $('input.topNavLeft').attr('onclick');
    $('input.topNavLeft').attr('onclick', 'return false;');

    $('input.topNavLeft').on('click.pageBackClick', { pageId: pageToHide, pageHide: pageHideFunction, linkClick: oldOnClick }, pageBackClick);

    // Update back button text if necessary
    var backtext = $(pageToHide).find('input[id*=hdnPageBackText]').val();
    if (backtext != null && backtext.length > 0) {
        $('input.topNavLeft').attr('title', backtext);
    }
}

// Clears the back click event
function removePageBackButton() {

    $('.topNavRightDiv').removeClass('jshide');

    // Remove the temp button click event
    $('input.topNavLeft').off('click.pageBackClick');
}

// Handles the page back button click
function pageBackClick(event) {

    var backtext = $('input.topNavLeft').data('backtext');
    $('input.topNavLeft').attr('title', backtext);

    // Reinsert existing onClick action if there was one set
    if (event.data.linkClick != null) {
        $('input.topNavLeft').attr('onclick', event.data.linkClick);
    }

    displayMenuNav(false);

    hidePage(event.data.pageId);

    if (event.data.pageHide != null) {
        // execute the supplied page hide function
        event.data.pageHide();
    }

    return false;
}


/// ----------------------------------------------------------

// Hide the input fields to prevent focus highlight issues on some devices
function hideInputs() {
    $('.locationsDiv').addClass('hide');
    $('.setdatebox').addClass('hide');
    $('.settimebox').addClass('hide');
    $('.setAdvancedOptionsBox').addClass('hide');
    $('.submittab').addClass('hide');
    //$('.footer').addClass('hide');
    $('a[id *= parkselected]').css('display', 'none');

    $('.dateSelect').addClass('transparent');
    $('.timePicker').addClass('transparent');
    $('.advancedOptions').addClass('transparent');
    $('.journeyHeadingContainer').addClass('transparent');
}
// Show the input fields
function showInputs() {
    $('.locationsDiv').removeClass('hide');
    $('.setdatebox').removeClass('hide');
    $('.settimebox').removeClass('hide');
    $('.setAdvancedOptionsBox').removeClass('hide');
    $('.submittab').removeClass('hide');
    //$('.footer').removeClass('hide');
    $('a[id *= parkselected]').css('display', 'inline-block');

    $('.dateSelect').removeClass('transparent');
    $('.timePicker').removeClass('transparent');
    $('.advancedOptions').removeClass('transparent');
    $('.journeyHeadingContainer').removeClass('transparent');
}

/// ---------------------------------------------------------

/// ----------------------------------------------------------
/// --------------------WAIT CONTROL--------------------------
// Sets up the wait dialog box
function setupWaitControl() {

    // Remove non js
    $('div.processMessage').removeClass('processMessageNonJS');

    var contentHeight = $('div.processMessage').outerHeight();
    var contentWidth = $('div.processMessage').outerWidth();
    var windowTop = ($(window).height() / 2) - (contentHeight / 2) - 50;
    var windowLeft = ($(window).width() / 2) - (contentWidth / 2);
    
    $('div.processMessage').css({ top: windowTop + 'px' });
    $('div.processMessage').css({ left: windowLeft + 'px' });
    $('div.processMessage').css('min-height', contentHeight);

    var documentHeight = $(document).height();
    $('div.journeyProgress').css({ display: 'block', height: documentHeight });

    $(document).scrollTop(0);
}

// Hides the wait div
function hideWait() {
    $('div.waitContainer').css('display', 'none');
}

// Displays the wait div, injected with message if required
function displayWait(message) {

    if (message == null) {
        // Display the default wait message
        message = $('div.processMessage span[id*=loadingMessage]').data('defaultmessage');
    }

    if (message != null) {
        // Set up the message to display
        $('div.processMessage span[id*=loadingMessage]').html(message);
    }

    if ($('div.waitContainer').length > 0) {
        // Dialogue should have been setup, just display it
        $('div.waitContainer').css({ display: 'block' });

        $('div.processMessage div.loadingMessageDiv').attr('aria-live', 'polite');
    }
}

/// ----------------------------------------------------------

/// ----------------------------------------------------------
/// ------------------- STYLE SHEET --------------------------

// Detects the device and requests updates to use the device appropriate stylesheet
function setupDeviceStylesheet() {
    //  css file based on the device
    var controlCss;
    //  get the device agent and conver to lover case
    var deviceAgent = navigator.userAgent.toLowerCase();

    // Android
    if (deviceAgent.match(/android/i)) {

        controlCss = "./version/styles/device/android-320.css"; // default 

        if (deviceAgent.match(/gt-i9300/i)) { // samsung galaxy s3
            controlCss = "./version/styles/device/android-360.css";
        }
        else if (deviceAgent.match(/gt-i9500/i)) { // samsung galaxy s4
            controlCss = "./version/styles/device/android-360.css";
        }
        else if (deviceAgent.match(/c5303/i)) { // sony xperia sp
            controlCss = "./version/styles/device/android-360.css";
        }
        else if (deviceAgent.match(/st26i/i)) { // sony xperia j
            controlCss = "./version/styles/device/android-320.css";
        }
        else if (deviceAgent.match(/htc_wildfires/i)) { // htc wildfire s
            controlCss = "./version/styles/device/android-320.css";
        }
        else if (deviceAgent.match(/htc magic/i)) { // htc magic
            controlCss = "./version/styles/device/android-320.css";
        }
        else if (deviceAgent.match(/shw-m110s/i)) { // samsung galaxy s
            controlCss = "./version/styles/device/android-320.css";
        }
        else if (deviceAgent.match(/s5830/i)) { // samsung galaxy ace s5830
            controlCss = "./version/styles/device/android-320.css";
        }
        else if (deviceAgent.match(/e10i/i)) { // sonyericsson x10 mini/e10i
            controlCss = "./version/styles/device/android-240.css";
        }
    }
    // ipad
    else if (deviceAgent.match(/ipad/i)) {
        controlCss = "./version/styles/device/ipad.css";
    }
    // Iphone
    else if (deviceAgent.match(/iphone/i)) {
        controlCss = "./version/styles/device/iphone.css";
    }
    // Blackberry
    else if (deviceAgent.match(/blackberry/i)) {

        controlCss = "./version/styles/device/android-320.css"; // default

        if (deviceAgent.match(/bb10/i)) { // blackberry q10
            controlCss = "./version/styles/device/android-320.css";
        }
        else if (deviceAgent.match(/9300/i)) { // blackberry curve 9300
            controlCss = "./version/styles/device/android-320.css";
        }
        else if (deviceAgent.match(/8520/i)) { // blackberry curve 8520
            controlCss = "./version/styles/device/android-320.css";
        }
    }
    // Windows phone
    else if (deviceAgent.match(/windows phone/i)) {

        controlCss = "./version/styles/device/windows-phone.css"; // default

        if (deviceAgent.match(/lumia 800/i)) { // nokia lumia 800
            controlCss = "./version/styles/device/windows-phone.css";
        }
        else if (deviceAgent.match(/lg-e900/i)) { // lg e900
            controlCss = "./version/styles/device/windows-phone.css";
        }
    }

    //alert(deviceAgent);

    if (controlCss) {
        //alert(controlCss);
        document.getElementById("cssLink").setAttribute("href", controlCss);
    }
}
/// ----------------------------------------------------------
