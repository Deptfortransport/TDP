var currentVenueControl = null;
var currentLocationInputText = null, currentLocationInputValue = null, currentLocationInputType = null;

// Initialise javascript stuff
$(document).ready(function () {
    setJSHdnSettingFieldValue('jsEnabled', $('div.locationControl'), 'true');
    setJSHdnSettingFieldValue('jsEnabled', $('div.eventDate'), 'true');

    // Locations
    setupInputAccess();
    setupCurrentLocation();
    setupClearLocation();
    setupVenueSelector();
    setEmptyLocationText();
    setupAmbiguityDrop();

    // Dates/times
    setupTimePicker();
    setupDatePicker();
    setupCalendar();
    setupNow();

    // Options
    setupAdvancedOptions();
    setupMobilityNotesDialogs();
    setupJourneyType();
    setupCycleParkMap();

    // Submit
    setupPlanJourney();
    setupWaitControl(); // Common.js
    scrollToMainContent();

    setupWindowResize();
    updateControlWidths();

    // Following lines are to rebind the jquery plugins or js events after ajax update of the update panel
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        setJSHdnSettingFieldValue('jsEnabled', $('div.locationControl'), 'true');
        scrollToMainContent();

        // Locations
        setupInputAccess();
        setupCurrentLocation();
        setupClearLocation();
        setEmptyLocationText();
        setupAmbiguityDrop();

        // Dates/times
        setupTimePicker();
        setupDatePicker();
        setupCalendar();
        setupNow();

        // Options
        setupMobilityNotesDialogs();
        setupJourneyType();

        // Submit        
        setupWaitControl(); // Common.js
        setupMessagesControl();
        displayMessage();

        updateControlWidths();
    });

});
/// ----------------------------------------------------------

// Sets up the collapsible journey type dropdown
function setupJourneyType() {

    var journeyTypeOptions = $('.journeyTypeRow .collapse')

    // Only collapse if journeyType options class indicates it should be
    if (journeyTypeOptions.length > 0) {
        $('.journeyTypeRow .ui-collapsible-content').slideUp(0);
    }

    // Show journey type options when the heading is clicked
    $('.journeyTypeRow .ui-collapsible-heading').click(function () {
        $('.journeyTypeRow .ui-collapsible-content').slideToggle(300);
    });

    // Update the selected journey type text
    $('.journeyTypeRow input[name*=journeyTypeRdo]').click(function () {
        var journeyTypeText = $(".journeyTypeRow input[name*=journeyTypeRdo]:checked").next().text();
        $('.journeyTypeRow h2').text(journeyTypeText);
        $('.journeyTypeRow .ui-collapsible-content').slideUp(300);
        // Update hidden value to allow server to detect user has manually updated
        $('input[id*=journeyTypeSelected]').val(journeyTypeText);
    });
}

// Displays updating locations in the inputs,
// used to provide immediate feed back. Function attached to be server side code
function toggleLocation() {

    // Can't disable the toggle location button as the server partialpostback does not work
    //$("input[id*=travelFromToToggle]").attr('disabled', 'disabled');

    // Disable the current location button
    //$("input[id*=currentLocationButton]").attr('disabled', 'disabled');

    // Update the location input box
    var fromLocationControl = $('#fromLocation');
    var toLocationControl = $('#toLocation');

    // Keep the current input hidden values
    var currentFromLocationText = $(fromLocationControl).find("input[id*=locationInput]:not(input[id*=hdn])").val();
    var currentFromLocationInputValue = $(fromLocationControl).find("input[id*=locationInput_hdnValue]").val();
    var currentFromLocationInputType = $(fromLocationControl).find("input[id*=locationInput_hdnType]").val();
    var currentToLocationText = $(toLocationControl).find("input[id*=locationInput]:not(input[id*=hdn])").val();
    var currentToLocationInputValue = $(toLocationControl).find("input[id*=locationInput_hdnValue]").val();
    var currentToLocationInputType = $(toLocationControl).find("input[id*=locationInput_hdnType]").val();

    var fromDefaultText = $(fromLocationControl).find("input[id*=locationInput]:not(input[id*=hdn])").data('inputdefaultvalue');
    var toDefaultText = $(toLocationControl).find("input[id*=locationInput]:not(input[id*=hdn])").data('inputdefaultvalue');

    if (fromDefaultText == currentFromLocationText) {
        currentFromLocationText = '';
    }
    if (toDefaultText == currentToLocationText) {
        currentToLocationText = '';
    }
    
    // Update the location input box, and reinsert the hidden values
//    $(fromLocationControl).find("input[id*=locationInput]").attr('disabled', 'disabled');
//    $(fromLocationControl).find("input[id*=locationInput]").val("Updating...");
//    $(fromLocationControl).find("input[id*=locationInput_hdnValue]").val(currentFromLocationInputValue);
//    $(fromLocationControl).find("input[id*=locationInput_hdnType]").val(currentFromLocationInputType);

//    $(toLocationControl).find("input[id*=locationInput]").attr('disabled', 'disabled');
//    $(toLocationControl).find("input[id*=locationInput]").val("Updating...");
//    $(toLocationControl).find("input[id*=locationInput_hdnValue]").val(currentToLocationInputValue);
//    $(toLocationControl).find("input[id*=locationInput_hdnType]").val(currentToLocationInputType);

    // Toggle is now done on client side only - this will need looking at if venues are introduced
    $(toLocationControl).find("input[id*=locationInput]").val(currentFromLocationText);
    $(toLocationControl).find("input[id*=locationInput_hdnValue]").val(currentFromLocationInputValue);
    $(toLocationControl).find("input[id*=locationInput_hdnType]").val(currentFromLocationInputType);

    $(fromLocationControl).find("input[id*=locationInput]").val(currentToLocationText);
    $(fromLocationControl).find("input[id*=locationInput_hdnValue]").val(currentToLocationInputValue);
    $(fromLocationControl).find("input[id*=locationInput_hdnType]").val(currentToLocationInputType);

    setEmptyLocationText();

    // Hide elements as required
    hideMessages();

    return false;
}

/// ----------------------------------------------------------

// Displays the please wait image when plan journey selected, 
// used to provide immediate feed back where the mobile device has slow network
function setupPlanJourney() {

    if ($('div.waitContainer').hasClass('hide')) {
        $('div.waitContainer').css('display', 'none');
    }

    $(document).on("click", "input[id*=planJourneyBtn]", function () {
        hideMessages();
        $('div.journeySummary').css('display', 'none');
        $('div.submittabreturn').css('display', 'none');
        displayWait();
    });
}

/// ----------------------------------------------------------

// Setsup the ambiguity dropdown
function setupAmbiguityDrop() {
    $(document).on("change", "select[id*=ambiguityDrop]", function () {
        setupAmbiguityDropStyle(this);
    });

    $('select[id*=ambiguityDrop]').each(function () {
        setupAmbiguityDropStyle(this);
    });
}

// Sets up the ambiguity dropdown stype
function setupAmbiguityDropStyle(e) {

    var $ambiguityDrop = null;
    if (e) {
        $ambiguityDrop = $(e);
    }
    else {
        $ambiguityDrop = $('select[id*=ambiguityDrop]');
    }


    if ($ambiguityDrop.length > 0) {

        if ($ambiguityDrop.val().length > 0) {
            if ($ambiguityDrop.val() != "Default") {
                // Remove dropdown arrow image if option selected (otherwise it obscures selected option)
                $ambiguityDrop.parent().addClass("ambiguityrowNoImage");
                $ambiguityDrop.removeClass("locationError");
            }
            else {
                $ambiguityDrop.parent().removeClass("ambiguityrowNoImage");
                $ambiguityDrop.addClass("locationError");
            }
        }

        // If ambiguity displayed, hide the toggle location
        $('div[id*=travelFromToToggleDiv]').addClass('hide');
    }
}

// Scrolls the screen to the main content area
function scrollToMainContent() {

    var scrollToElement = $('.journeyInput');

    if (scrollToElement.length > 0) {
        var waitShown = (($('div.waitContainer').length > 0) && !($('div.waitContainer').hasClass('hide')));
        var summaryShown = (($('div.journeySummary').length > 0));

        var mobilesummary = $('.mobilesummary');

        // If on mobile summary page and wait is displayed, scroll to it
        if ((mobilesummary.length > 0) && (waitShown)) {
            $('html, body').animate({
                scrollTop: (scrollToElement.offset().top) + 'px'
            }, 'fast');
        }
        // If on mobile summary page and summary is displayed, scroll to it
        else if ((mobilesummary.length > 0) && (summaryShown)) {
            $('html, body').animate({
                scrollTop: (scrollToElement.offset().top) + 'px'
            }, 'fast');
        }

    }
}
/// ----------------------------------------------------------

// Performs a postback to update the location parks dropdown
function updateLocationPark() {

    $('a[id*=parkselected]').text("Retrieving cycle parks...");

    // Get UpdatePanel containing the location dropdown
    var updateContainer = $('div.journeyInput div.locationParkRow');
    var updatePanel = getAspElement('updateContentPark', 'div', updateContainer);

    if (updatePanel.length > 0) {
        try {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            __doPostBack($(updatePanel).attr('id'), '');

        } catch (err) {
        }
    }
}
/// ---------------------------------------------------------

/// --------------- CLEAR LOCATION --------------------------
var clearButtonShown = false;
function setupClearLocation() {

    // Clears the grey watermark text when the input recieves focus
    $(document).on("click", "input[name*=locationInput].locationBox", function () {
        if ($(this).val() == $(this).data('inputdefaultvalue')) {
            $(this).val('');
        }
        // Display clear button
        $(this).parent().parent().find('input[id*=clearLocationButton]').removeClass("hide");
        $(this).addClass("locationClear");
        $(this).removeClass("watermark");
        $(this).removeClass("locationError");
        
        updateControlWidths();
    });
    $(document).on("focus", "input[name*=locationInput].locationBox", function () {
        if ($(this).val() == $(this).data('inputdefaultvalue')) {
            $(this).val('');
        }
        // Display clear button
        $(this).parent().parent().find('input[id*=clearLocationButton]').removeClass("hide");
        $(this).addClass("locationClear");
        $(this).removeClass("watermark");
        $(this).removeClass("locationError");

        updateControlWidths();
    });
    $(document).on("blur", "input[name*=locationInput].locationBox", function () {
        if ($(this).val() == "") {
            // Set the watermark text if nothing entered
            $(this).val($(this).data('inputdefaultvalue'));

            // Hide clear button
            $(this).removeClass("locationClear");
            $(this).parent().parent().find('input[id*=clearLocationButton]').addClass("hide");

            $(this).addClass("watermark");

            updateControlWidths();
        }
    });
    $(document).on("keypress", "input[name*=locationInput].locationBox", function () {
        if (!clearButtonShown) {
            if ($(this).val() != $(this).data('inputdefaultvalue')) {
                // Display clear button
                $(this).parent().parent().find('input[id*=clearLocationButton]').removeClass("hide");
                $(this).addClass("locationClear");
                clearButtonShown = true;

                updateControlWidths();
            }
        }
    });
        
    // Clear text when the "clear location" button is clicked
    $(document).on("click", "input[name*=clearLocationButton]", function () {
        var $locationInput = $(this).parent().find('input[id*=locationInput]')
        if ($locationInput.val() != $locationInput.data('inputdefaultvalue')) {
            $locationInput.val('');

            $(this).parent().parent().find("input[id*=locationInput_hdnValue]").val('');
            $(this).parent().parent().find("input[id*=locationInput_hdnType]").val('');
        }
        // Hide clear button
        $(this).addClass("hide");
        $locationInput.removeClass("locationClear");

        clearButtonShown = false;

        $locationInput.focus();

        updateControlWidths();

        // Return false to prevent button postback to server
        return false;
    });
}

/// --------------- CURRENT LOCATION ------------------------
// Initialises the current location button
function setupCurrentLocation() {

    // Display the current location button (hidden by default)
    $("input[id*=currentLocationButton]").css({ display: "block" });

    $("input[id*=currentLocationButton]").die();
    $("input[id*=currentLocationButton]").bind("click", function (event) {
        currentVenueControl = this.parentNode.parentNode.parentNode;
        // Disable the current location button
        $(this).attr('disabled', 'disabled');

        // Disable the toggle locaiton button
        $("input[id*=travelFromToToggle]").attr('disabled', 'disabled');

        // Keep the current input text, in case geolocation errors
        currentLocationInputText = $(currentVenueControl).find("input[id*=locationInput]:not(input[id*=hdn])").val();
        currentLocationInputValue = $(currentVenueControl).find("input[id*=locationInput_hdnValue]").val();
        currentLocationInputType = $(currentVenueControl).find("input[id*=locationInput_hdnType]").val();
        
        // Hide clear button
        $(currentVenueControl).find("input[id*=locationInput]").removeClass("locationClear");
        $(currentVenueControl).find('input[id*=clearLocationButton]').addClass("hide");
        clearButtonShown = false;

        // Update the location input box
        $(currentVenueControl).find("input[id*=locationInput]").attr('disabled', 'disabled');
        $(currentVenueControl).find("input[id*=locationInput]").val("Retrieving location...");
        $(currentVenueControl).find("input[id*=locationInput]").removeClass("locationError");

        // Display the wait dialogue
        displayWait($(currentVenueControl).find("input[id*=locationInput]").val());

        // Hide elements as required
        hideMessages();

        getGeolocation();

        // Return false to prevent button postback to server
        return false;
    });
}

// Calls the current location functionality
function getGeolocation() {
    try    {
        navigator.geolocation.getCurrentPosition(geolocationResult, geolocationError, { timeout: 7000 });
    }
    catch (error) {
        // if geolocation not supported (e.g. IE8 or earlier)
        geolocationError(error);
    }
}

// Current location error handler
function geolocationError(error) {

    // Update the location input box with previous values
    if (currentLocationInputText != null) {
        $(currentVenueControl).find("input[id*=locationInput]").val(currentLocationInputText);
        $(currentVenueControl).find("input[id*=locationInput_hdnValue]").val(currentLocationInputValue);
        $(currentVenueControl).find("input[id*=locationInput_hdnType]").val(currentLocationInputType);
    }

    // Re-enable the current location button
    $(currentVenueControl).find(".locationCurrent").removeAttr('disabled');
    $(currentVenueControl).find("input[id*=locationInput]").removeAttr('disabled');
    $("input[id*=travelFromToToggle]").removeAttr('disabled');

    switch (error.code) {
        case error.PERMISSION_DENIED:
            displayMessage("Sorry, Transport Direct is unable to detect your location, please select a location from the drop down list");
            break;

        case error.POSITION_UNAVAILABLE:
            displayMessage("Sorry, Transport Direct is unable to detect your location, please select a location from the drop down list");
            break;

        case error.TIMEOUT:
            displayMessage("Sorry, Transport Direct is unable to detect your location, please select a location from the drop down list.");
            break;

        default:
            displayMessage("Error, Transport Direct is unable to detect your location, please select a location from the drop down list.");
            break;
    }

    // Close the wait diaglogue
    hideWait();
}

// Current location handler
function geolocationResult(position) {
    
    var coord = position.coords.latitude + "," + position.coords.longitude;

    // Update the location input box
    $(currentVenueControl).find("input[id*=locationInput]").val("My Location");

    // Update the location control hidden field with the position coordinates
    $(currentVenueControl).find("input[id*=locationInput_hdnValue]").val(coord);
    $(currentVenueControl).find("input[id*=locationInput_hdnType]").val("CoordinateLL"); // Must be the TDPLocationType enum value

    // Remove watermark
    $(currentVenueControl).find("input[id*=locationInput]").removeClass("watermark");
    $(currentVenueControl).find("input[id*=locationInput]").removeClass("locationError");
    
    // Re-enable the current location button
    $(currentVenueControl).find(".locationCurrent").removeAttr('disabled');
    $(currentVenueControl).find("input[id*=locationInput]").removeAttr('disabled');
    $("input[id*=travelFromToToggle]").removeAttr('disabled');

    // Close the wait diaglogue
    hideWait();
}
/// ----------------------------------------------------------

/// -------------------------- DATE --------------------------
// Sets up the date calendar picker
function setupDatePicker() {

    // Set date styles
    $("input[name*=outwardDate]").addClass('dateEntryWithButton');
    $("input[name*=btnOutwardDate]").removeClass('hide');

    $(document).on("click", "input[name*=btnOutwardDate]", function () {
        displayCalendar();

        // Return to prevent page postback
        return false;
    });

    $(document).on("click", "input[name*=outwardDate]", function () {
        displayCalendar();
        return false;
    });

    // Enter key pressed on date input
    $(document).on("keydown", "input[name*=outwardDate]", function (e) {
        if (e.target.className != "searchtextbox") {
            if (e.keyCode == 13) { //Enter key
                e.preventDefault();
                displayCalendar();
                return false;
            }
            else
                return true;
        }
        else
            return true;
    });
    

    $(document).on("click", "input[name*=dataDate]", function () {
        // Set the selected date
        var date = $(this).attr('title');

        var todayDate = getJSHdnSettingFieldValue('todayDate', $(".eventDate"));

        if (date == todayDate) {
            // If date selected is today, then display "today" in the date box
            date = $('input[name*=outwardDate]').data('todaytext');
        }

        $('input[name*=outwardDate]').first().val(date);

        $(this).parent().addClass('daySelected');

        hidePage('#datepage');

        resetArriveByFlag();

        // Set focus back onto the button
        $('#datepage').parent().find('input[name*=btnOutwardDate]').focus();

        return false;
    });
}

// Displays the calendar page
function displayCalendar() {

    // Ensure no other date has selected style
    $('.collapseDate .daySelected').removeClass('daySelected');
    
    // Ensure selected date has style
    var selectedDate = $('input[name*=outwardDate]').val();
    var $input = $("input[name*=dataDate]").filter("input[title='" + selectedDate + "']").filter("input[disabled!='disabled']");

    // Otherwise default todays date
    if ($input == null || $input.length == 0){
        selectedDate = getJSHdnSettingFieldValue('todayDate', $(".eventDate"));

        $input = $("input[name*=dataDate]").filter("input[title='" + selectedDate + "']").filter("input[disabled!='disabled']");
    }
    
    $input.parent().addClass('daySelected');
            
    // Ensure selected month is shown
    $('.collapseDate .collapseDateDays').css('display', 'none');
    $input.parent().parent().slideDown(0);

    displayPage('#datepage', hideCalendar);

    // Focus to allow selection via keyboard
    var datepage = $('#datepage');
    if (datepage) {
        $input.focus();
    }
}

// Callback function for back page click
function hideCalendar() {
    // Set focus back onto the button
    $('#datepage').parent().find('input[name*=btnOutwardDate]').focus();
}

// Adds click event to calendar display
function setupCalendar() {
    // Hide all months
    $('.collapseDate .collapseDateDays').css('display', 'none');

    // Show month options when the heading is clicked,
    // allow navigable headings by adding as link
    $('.collapseDate h3').each(function () {

        // Set up a clickable link to show month options
        var title = $(this).html();

        var s = "<a title=\"" + title + "\" href=\"#\" onclick=\"showMonth('" + $(this).attr('id') + "'); return false;\">"
                + $(this).html()
                + "</a>";

        $(this).html(s);
    });
}

// Shows the month options
function showMonth(controlId) {

    // Only slide if needed
    if ($('.collapseDate h3#' + controlId).next().css('display') == 'none') {
        // Hide all month options
        $('.collapseDate .collapseDateDays').slideUp(300);
        // Show this month options
        $('.collapseDate h3#' + controlId).next().slideDown(300);
    }
}

// Updates the styling if the now button is shown
function setupNow() {

    // Resized widths
    var dateWidth = $('.dateSelect .setdatebox input.dateEntry').outerWidth() - 6 - 40;
    var timeWidth = $('.timePicker .settimebox input.timeInput').outerWidth() - 6 - 40;

    var deviceAgent = navigator.userAgent.toLowerCase();
    if (deviceAgent.match(/e10i/i)) { // sonyericsson x10 mini/e10i (android 240screen)
        dateWidth =  dateWidth - 20; // Check
        timeWidth = dateWidth - 20; // Check
    }

    if ($('.eventDateTime .nowSelect').length > 0)
    {
        // Now is shown, resize the date time inputs
        $('.dateSelect .setdatebox input.dateEntry').css('width', dateWidth);
        $('.timePicker .settimebox input.timeInput').css('width', timeWidth);
    }
}

/// ----------------------------------------------------------

/// ------------------------- TIME ---------------------------
// Sets up the time picker
function setupTimePicker() {

    // Set time styles
    $("input[name*=outwardTime]").addClass('timeInputWithButton');
    $("span[id*=outwardTimeType]").removeClass('hide');
    $("input[name*=btnOutwardTime]").removeClass('hide');

    // Time inputs
    $(document).on("click", "input[name*=btnOutwardTime]", function () {
        displayTime();
        // Return to prevent page postback
        return false;
    });

    $(document).on("click", "span[id*=outwardTimeType]", function () {
        displayTime();
    });

    $(document).on("click", "input[name*=outwardTime]", function () {
        displayTime();
    });

    // Enter key pressed on time input
    $(document).on("keydown", "input[name*=outwardTime]", function (e) {
        if (e.target.className != "searchtextbox") {
            if (e.keyCode == 13) { //Enter key
                e.preventDefault();
                displayTime();
                return false;
            }
            else
                return true;
        }
        else
            return true;
    });

    // Arrive/leave buttons
    $(document).on("click", "input[name*=btnLeaveAt]", function () {
        setSelectedArriveByStyle(false);
        // Return to prevent page postback
        return false;
    });
    $(document).on("click", "input[name*=btnArriveBy]", function () {
        setSelectedArriveByStyle(true);
        // Return to prevent page postback
        return false;
    });

    // Other times button
    $(document).on("click", "input[name*=btnOtherTimes]", function () {
        displayTimeDropDowns();
        // Return to prevent page postback
        return false;
    });

    // Clone the time dropdown onto the time page
    var otherTimesDropDiv = 'div.otherTimesDropDiv';
    $(otherTimesDropDiv).html('');
    $("select[name*=drpHoursListNonJS]").clone().appendTo(otherTimesDropDiv);
    $("select[name*=drpMinutesListNonJS]").clone().appendTo(otherTimesDropDiv);
    

    // Select from time dropdown
    $(document).on("change", "div.otherTimesDropDiv select[id*=drpHoursList]", function () {
        setSelectedTimeOption();
    });

    $(document).on("change", "div.otherTimesDropDiv select[id*=drpMinutesList]", function () {
        setSelectedTimeOption();
    });

    // Click on quick pick time confirms selection
    $(document).on("click", "input[name*=dataTime]", function () {
        // Set the selected date
        var time = $(this).val();

        $('input[name*=outwardTime]').first().val(time);

        // Set selected time in the drop down
        time = time.replace(/:/g, ''); // remove the colon
        $('div.otherTimesDropDiv select[id*=drpHoursList]').val(time.substring(0, 2));
        $('div.otherTimesDropDiv select[id*=drpMinutesList]').val(time.substring(2));

        $(this).parent().addClass('timeSelected');

        if (isArriveByStyleSelected()) {
            setArriveByFlag(true);
        }
        else {
            setArriveByFlag(false);
        }

        setTimeTypeLabel();

        hidePage('#timepage');

        resetArriveByFlag();

        // Set focus back onto the button
        $('#timepage').parent().find('input[name*=btnOutwardTime]').focus();
    });

    // Click on ok button confirms selection
    $(document).on("click", "input[name*=btnOKOutwardTime]", function () {

        var hour = $('div.otherTimesDropDiv select[id*=drpHoursList] option:selected').val();
        var minute = $('div.otherTimesDropDiv select[id*=drpMinutesList] option:selected').val();

        var time = hour + ":" + minute;

        // Set the selected time
        $('input[name*=outwardTime]').first().val(time);

        setSelectedTimeOption();

        if (isArriveByStyleSelected()) {
            setArriveByFlag(true);
        }
        else {
            setArriveByFlag(false);
        }

        setTimeTypeLabel();

        hidePage('#timepage');

        resetArriveByFlag();

        // Set focus back onto the button
        $('#timepage').parent().find('input[name*=btnOutwardTime]').focus();

        return false;
    });
}

// Displays the time page
function displayTime() {

    // Hide the time dropdowns by default
    $('.otherTimesDiv').removeClass('hide');
    $('.otherTimesDropContainer').addClass('hide');

    setSelectedArriveByStyle();
    setSelectedTimeOptionStyle();
        
    displayPage('#timepage', hideTime);

    // Focus onto the back link, to allow selection via keyboard
    var timepage = $('#timepage');
    if (timepage) {
        timepage.find('input.selected').filter(':visible:first').focus();
    }
}

// Callback function for back page click
function hideTime() {
    // Set focus back onto the button
    $('#timepage').parent().find('input[name*=btnOutwardTime]').focus();
}

// Sets the selected time from the time drop downs
function setSelectedTimeOption() {

    var hour = $('div.otherTimesDropDiv select[id*=drpHoursList] option:selected').val();
    var minute = $('div.otherTimesDropDiv select[id*=drpMinutesList] option:selected').val();

    var time = hour + ":" + minute;

    // Set selected time in the quick time select
    setSelectedTimeOptionStyle(time);
}

// Sets the selected style on the arrive by leave at buttons
function setSelectedArriveByStyle(arriveBy) {
    if (arriveBy != null) {
        if (arriveBy) {
            $('input[name*=btnLeaveAt]').removeClass('selected');
            $('input[name*=btnArriveBy]').addClass('selected');
        }
        else {
            $('input[name*=btnLeaveAt]').addClass('selected');
            $('input[name*=btnArriveBy]').removeClass('selected');
        }
    }
    else {
        // If null, determine which button should be shown as selected
        // Only arrive/leave selection doesnt have a confirms selection action, 
        // so determine what it should still be before displaying time page
        if ($('span[id*=outwardTimeType]').html() == $('input[name*=btnArriveBy]').val()) {
            setArriveByFlag(true);
            setSelectedArriveByStyle(true);
        }
        else {
            setArriveByFlag(false);
            setSelectedArriveByStyle(false);
        }
    }
}

// Determines if arrive by or leave after is selected by checking style
function isArriveByStyleSelected() {

    if ($('input[name*=btnArriveBy]').hasClass('selected')) {
        return true;
    }

    return false;
}

// Sets the selected style on the time quick pick list
function setSelectedTimeOptionStyle(time) {
    // Ensure no other time has selected style
    $('.timeSelectorContainer .timeSelected').removeClass('timeSelected');
    //$("input[name*=dataTime]:checked").parent().addClass('timeSelected');

    // Ensure selected time has style
    var selectedTime = $('input[name*=outwardTime]').val();

    if (time != null && time.length > 0) {
        selectedTime = time;
    }

    var $input = $("input[name*=dataTime]").filter("input[value='" + selectedTime + "']").filter("input[disabled!='disabled']");
    $input.parent().addClass('timeSelected');
}

// Sets the arrive/leave text on the time type label
function setTimeTypeLabel() {
    if (isArriveByStyleSelected()) {
        $('span[id*=outwardTimeType]').html($('input[name*=btnArriveBy]').val());
    }
    else {
        $('span[id*=outwardTimeType]').html($('input[name*=btnLeaveAt]').val());
    }
}

// Displays the time drop downs when "other times" clicked
function displayTimeDropDowns() {
    $('.otherTimesDiv').addClass('hide');
    $('.otherTimesDropContainer').removeClass('hide');
}

// Resets the arrive by date time flag
function resetArriveByFlag() {

    // Reset the now flag
    $('input[name*=isNowFlag]').val('false');

    // If location input is in to venue mode, then must reset
    // the arrive by flag to be "arrive by"
    if ($('input[name*=isArriveByFlag]').val() == "false"
            && $('input[name*=isToVenueFlag]').val() == "true") {

        $('input[name*=isArriveByFlag]').val('true');

        // Hide the leave at text and display the arrive by
        $('.eventDateTitleDiv span.hide').css('display', 'block');
        $('.eventDateTitleDiv span.show').css('display', 'none');
    }
}

// Resets the arrive by date time flag
function setArriveByFlag(selected) {
    if (selected) {
        $('input[name*=isArriveByFlag]').val('true');
    }
    else {
        $('input[name*=isArriveByFlag]').val('false');
    }
}
/// ----------------------------------------------------------

/// ---------------------- ADVANCED OPTIONS ------------------

// Sets up the advanced options
function setupAdvancedOptions() {

    $(document).on("click", "input[name*=btnAdvancedOptions]", function () {
        displayAdvancedOptions();
        // Return to prevent page postback
        return false;
    });
    $(document).on("click", "span[id*=lblAdvancedOptionsSelected]", function () {
        displayAdvancedOptions();
        return false;
    });

    $(document).on("click", ".transportModeRow", function (e) {
        setCheckbox(this, e);
    });
    $(document).on("click", ".transportModeLabelDiv", function (e) {
        setCheckbox(this, e);
    });
    $(document).on("click", ".transportModeImageDiv", function (e) {
        setCheckbox(this, e);
    });
    $(document).on("click", ".transportModeImageDiv", function (e) {
        var $chk = $(this).parent().parent().find('input[id*=chk]')
        setCheckbox(this, e, $chk);
    });
    $(document).on("click", ".transportModeImageDiv img", function (e) {
        var $chk = $(this).parent().parent().parent().find('input[id*=chk]')       
        setCheckbox(this, e, $chk);
    });
    $(document).on("click", ".transportModeCheckDiv", function (e) {
        setCheckbox(this, e);
    });

    $(document).on("click", ".mobilityOptionRow", function (e) {
        setCheckbox(this, e);
        setFewestChangesState();
    });
    $(document).on("click", ".mobilityOptionLabelDiv", function (e) {
        setCheckbox(this, e);
        setFewestChangesState();
    });
    $(document).on("click", ".mobilityOptionCheckDiv", function (e) {
        setCheckbox(this, e);
        setFewestChangesState();
    });


    $(document).on("change", "input[id*=chkStepFree]", function () {
        setFewestChangesState();
    });
    $(document).on("change", "input[id*=chkAssistance]", function () {
        setFewestChangesState();
    });

    
    // Click on ok button confirms selection
    $(document).on("click", "input[name*=btnOKAdvancedOptions]", function () {

        var selectedIds = '';
        var allModesSelected = true;
        var modeSelected = false;

        // Commit the selected transport modes
        $('div.transportModeRow').each(function () {
            var $chk = $(this).find('input[id*=chk]');
            if ($chk.prop('checked') == true) {
                selectedIds = selectedIds + ',' + $chk.attr('id');
                modeSelected = true;
            }
            else {
                allModesSelected = false;
            }
        });

        if (modeSelected == false) {
            displayMessage(getJSHdnSettingFieldValue('hdnValidationMessage', $(".advancedOptions")));
            return false;
        }

        setJSHdnSettingFieldValue('transportModesSelected', $(".advancedOptions"), selectedIds);

        var mobilityIds = '';
        var mobilityOptionSelected = false;

        // Commit the selected accessible options
        $('div.mobilityOptionRow').each(function () {
            var $chk = $(this).find('input[id*=chk]');
            if ($chk.prop('checked') == true) {
                mobilityIds = mobilityIds + ',' + $chk.attr('id');
                mobilityOptionSelected = true;
            }
        });

        setJSHdnSettingFieldValue('mobilityOptionsSelected', $(".accessibilityOptions"), mobilityIds);

        // Update the options label to indicate selection choices
        var lblSelectedOptions = 'span.advancedOptionsSelected';
        $(lblSelectedOptions).html('');

        var selectedtext = '';
        if (allModesSelected) {
            // Add only the images which indicate they should be displayed in the "all modes selected" state
            $('div.transportModeImageDiv img').each(function () {
                var show = $(this).data('showallmodes');
                if (show) {
                    $(this).clone().appendTo(lblSelectedOptions);
                }
            });
        }
        else if (modeSelected) {
            $('div.transportModeRow').each(function () {
                var $chk = $(this).find('input[id*=chk]');
                if ($chk.prop('checked') == true) {
                    $(this).find('div.transportModeImageDiv img').clone().appendTo(lblSelectedOptions);
                }
            });
        }
        if (mobilityOptionSelected) {
            selectedtext += "<br />" + $(lblSelectedOptions).data('txtmobility');
        }
        else {
            selectedtext += "<br />" + $(lblSelectedOptions).data('txtnomobility');
        }

        $(lblSelectedOptions).append(selectedtext);

        hidePage('#advancedoptionspage');

        // Set focus back onto the button
        $('#advancedoptionspage').parent().find('input[name*=btnAdvancedOptions]').focus();

        return false;
    });
}

// Displays the advanced options
function displayAdvancedOptions() {

    // Set initial checked state
    setCheckboxesSelected(getJSHdnSettingFieldValue('transportModesSelected', $(".advancedOptions")),
        '.transportModeRow');
    setCheckboxesSelected(getJSHdnSettingFieldValue('mobilityOptionsSelected', $(".accessibilityOptions")),
        '.mobilityOptionRow');
        
    // Set disabled state if necessary
    setFewestChangesState();

    displayPage('#advancedoptionspage', hideAdvancedOptions);

    // Focus onto the fist advanced option, to allow selection via keyboard
    var advancedoptionspage = $('#advancedoptionspage');
    if (advancedoptionspage) {
        advancedoptionspage.find('input').filter(':visible:first').focus();
    }
}

// Callback function for back page click
function hideAdvancedOptions() {

    // Reset the checkboxes to initial state
    setCheckboxesSelected(getJSHdnSettingFieldValue('transportModesSelected', $(".advancedOptions")),
        '.transportModeRow');
    setCheckboxesSelected(getJSHdnSettingFieldValue('mobilityOptionsSelected', $(".accessibilityOptions")),
        '.mobilityOptionRow');

    // Set focus back onto the button
    $('#advancedoptionspage').parent().find('input[name*=btnAdvancedOptions]').focus();
}

// Helper function to toggle the checkbox checked state
function setCheckbox(control, event, checkbox) {
    if (event.target != control)
        return;

    var $chk = null;

    if (checkbox != null) {
        $chk = checkbox;
    }
    else {
        $chk = $(control).find('input[id*=chk]');
    }

    if ($chk.length == 0) {
        //try finding from parent
        $chk = $(control).parent().find('input[id*=chk]')
    }

    if ($chk.prop('disabled') || $chk.prop('checked')) {
        $chk.prop('checked', false);
    }
    else {
        $chk.prop('checked', true);
    }
}

// Sets the selected initial checked states
function setCheckboxesSelected(selectedIds, container) {

    // Deselect all
    $(container + ' input[id*=chk]').prop('checked', false);

    if (selectedIds) {

        // Select the selected ids
        var selectedChkIds = selectedIds.split(',');

        for (var i = 0; i < selectedChkIds.length; i++) {

            var selectedChkId = selectedChkIds[i];

            var $chk = $('#' + selectedChkId);

            if ($chk) {
                $chk.prop('checked', true);
            }
        }
    }
}

function setFewestChangesState() {

    // Check if step free or assistance selected
    if ($('input[id*=chkStepFree]').prop('checked')
        || $('input[id*=chkAssistance]').prop('checked')) {
        $('input[id*=chkFewestChanges]').prop('disabled', false);
        $('label[id*=lblFewestChanges]').removeClass('disabled');
    }
    else {
        $('input[id*=chkFewestChanges]').prop('checked', false);
        $('input[id*=chkFewestChanges]').prop('disabled', 'disabled');
        $('label[id*=lblFewestChanges]').addClass('disabled');
    }
}

// Sets up the dialog boxes for journey web notes
function setupMobilityNotesDialogs() {

    $(document).on("click", "a.accessibleNotesLink", function () {

        // Set the info dialogue text
        var title = $(this).text();
        var note = $(this).parent().parent().parent().find('div.detailNote span').html()

        $('div#additionaInfoDialog h2[id*=additionalInfoHeader]').html(title);
        $('div#additionaInfoDialog div#additionalInfoNotes').html(note);

        if (title == null || title.length == 0) {
            $('div#additionaInfoDialog h2[id*=additionalInfoHeader]').css({ display: 'none' });
        }
        else {
            $('div#additionaInfoDialog h2[id*=additionalInfoHeader]').css({ display: 'block' });
        }

        // Set the dialog position
        var contentHeight = $('div#additionaInfoDialog').outerHeight();
        var windowTop = ($(window).height() / 2);
        var windowLeft = ($(window).width() / 2);

        $('div#additionaInfoDialog').css({ top: windowTop + 'px' });
        $('div#additionaInfoDialog').css({ left: windowLeft + 'px' });
        $('div#additionaInfoDialog').css('min-height', contentHeight);

        var documentHeight = $(document).height();
        $('div#additionalInfoPage').css({ display: 'block', height: documentHeight });

        $('div#additionalInfoNotes').attr('aria-live', 'assertive');

        currentInputPagePosition = window.pageYOffset;
        $(document).scrollTop(0);

        // Set focus
        $('div#additionalInfoPage').find('a').focus();
        
        return false;
    });

    $(document).on("click", "a[id*=closeinfodialog]", function () {

        $('div#additionalInfoNotes').attr('aria-live', 'off');

        $('div#additionalInfoPage').css('display', 'none');
        $(document).scrollTop(currentInputPagePosition);
        return false;
    });
}

/// ----------------------------------------------------------

/// ------------------------- VENUE ---------------------------
// Sets up the venue selector
function setupVenueSelector() {

    // Show venue page on venue location input click
    $(document).on("click", "input[name*=locationInput]:not(.locationBox)", function () {

        hideMessages();

        // this sets the location control that has 'focus' (for venue to venue cases)
        currentVenueControl = this.parentNode.parentNode;
        $('div#venuespage').css('display', 'block');

        document.activeElement.blur(); // Remove focus from the input field?

        hideInputs();
        
        // Focus onto the back link, to allow selection via keyboard
        var venuePage = $('div#venuespage');
        if (venuePage) {
            venuePage.find('.topNavLeft').focus();
        }
    });

    // Show venue page on venue link click
    $(document).on("click", "a[id*=venueInputPageLnk]", function () {

        hideMessages();

        currentVenueControl = this.parentNode.parentNode;
        $('div#venuespage').css('display', 'block');

        hideInputs();

        // Focus onto the back link, to allow selection via keyboard
        var venuePage = $('div#venuespage');
        if (venuePage) {
            venuePage.find('.topNavLeft').focus();
        }
        return false;
    });
        
    // Set selected venue when radio button selection changes
    $(document).on("click", "input[name*=locationSelector]", function () {
        var venueid = $(this).attr('id').substring(2);
        $(currentVenueControl).find('input[id*=locationInput]').val($(this).val());
        $(currentVenueControl).find('input[id*=locationInput_hdnValue]').val(venueid);
        $(currentVenueControl).find('input[id*=locationInput_hdnType]').val("Venue");

        // Remove watermark
        $(currentVenueControl).find("input[id*=locationInput]").removeClass("watermark");
        $(currentVenueControl).find("input[id*=locationInput]").removeClass("locationError");

        showInputs();

        $('div#venuespage').css('display', 'none');

        $('html, body').animate({
            scrollTop: '0px'
        }, 'slow');

        updateLocationPark();
    });
    
    // Sets visible focus style when tabbing through venue options
    $(document).on("focus", "input[name=locationSelector]", function () {
        $(this).next().css({ color: '#e6007e'});
        $(this).parent().css('border-color','#e6007e');
    });
    $(document).on("blur", "input[name=locationSelector]", function () {
        $(this).next().css('color', 'inherit');
        $(this).parent().css('border-color', '#cccccc');
    });

    // Close venue page
    $(document).on("click", "a[id*=closevenues]", function () {
        showInputs();
        $('div#venuespage').css('display', 'none');

        $('html, body').animate({
            scrollTop: '0px'
        }, 'slow');

        return false;
    });
}

// Add watermark to input fields
function setEmptyLocationText() {

    // Remove the non js watermark class
    $("input[name*=locationInput]").each(function () {
        $(this).removeClass("watermarkNonJS");
    });

    $("input[name*=locationInput]").each(function () {
        if ($(this).val() == '') {
            $(this).val($(this).data('inputdefaultvalue'));
            if ($(this).hasClass("locationBox")) {
                $(this).addClass("watermark");
            }
        }
        else if ($(this).val() != $(this).data('inputdefaultvalue')) {
            if ($(this).hasClass("locationBox")) {
                $(this).removeClass("watermark");
            }
        }
    });
}

// Sets readonly status on controls which do not allow user text entry
function setupInputAccess() {
    // makes the location input box readonly if the control is venue only
    if ($('div[id*=fromLocation]').find('input[id*=locationInput]').hasClass("locationBoxVenue")) {
        $('div[id*=fromLocation]').find('input[id*=locationInput]').attr('readonly', true);
        $('div[id*=fromLocation]').find('input[id*=locationInput]').removeClass('locationBox');
    }

    if ($('div[id*=toLocation]').find('input[id*=locationInput]').hasClass("locationBoxVenue")) {
        $('div[id*=toLocation]').find('input[id*=locationInput]').attr('readonly', true);
        $('div[id*=toLocation]').find('input[id*=locationInput]').removeClass('locationBox');
    }

    // Make field readonly always to prevent mobile devices from display text entry 
    $("input[name*=outwardDate]").attr('readonly', true);

    // Make field readonly always to prevent mobile devices from display text entry 
    $("input[name*=outwardTime]").attr('readonly', true);

    // Make field readonly always to prevent mobile devices from display text entry 
    $("input[name*=parkselected]").attr('readonly', true);
}
/// ----------------------------------------------------------

/// ---------------------- CYCLE MAP -------------------------
// Sets up the cycle map selection
function setupCycleParkMap() {
    $(document).on("click", "a[id*=parkselected]", function () {

        // Check if link has disabled styling
        if ($('div[id*=locationParkDiv]').hasClass('locationParkRowDisabled')) {
            // disabled, do not show map container
        }
        else {
            var documentHeight = $(document).height();

            $("div[id*=cycleParkMapContainer]").css({ display: 'block', height: documentHeight });

            hideInputs();

            $('html, body').animate({
                scrollTop: '0px'
            }, 'fast');
        }
        return false;
    });

    $(document).on("click", "a[id*=closeparkmap]", function () {
        showInputs();
        $('div[id*=cycleParkMapContainer]').css('display', 'none');
        return false;
    });

    // Handles the radio button list that appears if there is no map for the venue
    $(document).on("change", "input[name*=cycleParksNoMap]", function () {
        $('a[id*=parkselected]').text($("label[for*=" + $(this).attr('id') + "] span.ui-btn-text").text());
        $('input[id*=selectedCycleParkID]').val($(this).val());
        showInputs();
        $('div[id*=cycleParkMapContainer]').css('display', 'none');
        
        $('html, body').animate({
            scrollTop: '0px'
        }, 'slow');
    });

    $(document).on("click", "input[name*=parks]", function () {
        setCyclePark($(this).val(), $("label[for*=" + $(this).attr('id') + "]").text());
    });
}

// Sets the selected cycle park from the map
function setCyclePark(parkId, parkName) {
    $('a[id*=parkselected]').text(parkName);
    $('input[id*=selectedCycleParkID]').val(parkId);
    showInputs();
    $("div[id*=cycleParkMapContainer]").css('display', 'none');

    $('html, body').animate({
        scrollTop: '0px'
    }, 'slow');
}
/// ----------------------------------------------------------

// Resizes the controls when the window changes size
function setupWindowResize() {
    $(window).resize(function () {
        updateControlWidths();
    });
}

// Updates the location, date, time, accessible option widths 
function updateControlWidths() {
    var width = $(window).width();

    // Location inputs
    var $fromLocation = $('#fromLocation').find("input[id*=locationInput].locationBox:not(input[id*=hdn])");
    var $toLocation = $('#toLocation').find("input[id*=locationInput].locationBox:not(input[id*=hdn])");

    if ($fromLocation.hasClass('locationClear')) {
        $fromLocation.css('width', width - 120 + 'px');
    }
    else {
        $fromLocation.css('width', width - 90 + 'px');
    }

    if ($toLocation.hasClass('locationClear')) {
        $toLocation.css('width', width - 120 + 'px');
    }
    else {
        $toLocation.css('width', width - 90 + 'px');
    }

    // Location ambiguity
    var $ambiguityRow = $('div.ambiguityrow');
    var $ambiguityDrop = $('select.ambiguityDrop');

    $ambiguityRow.css('width', width - 119 + 'px');
    $ambiguityDrop.css('width', width - 82 + 'px');
    $ambiguityDrop.css('max-width', width - 82 + 'px');
       
    // Date/time inputs
    var $dateInput = $('input[id*=outwardDate].dateEntry');
    var $timeInput = $('input[id*=outwardTime].timeInput');

    if ($dateInput.hasClass('dateEntryWithButton')) {
        $dateInput.css('width', (width / 2) - 25 - 40 + 'px');        
    }
    else{
        $dateInput.css('width', (width / 2) + 'px');        
    }

    if ($timeInput.hasClass('timeInputWithButton')) {
        $timeInput.css('width', (width / 2) - 86 - 38 + 'px');        
    }
    else{
        $timeInput.css('width', (width / 2) + 'px');        
    }

    // Advanced options
    var $btnAdvancedOptions = $('input[id*=btnAdvancedOptions].btnAdvancedOptions');
    var $lblAdvancedOptions = $('span[id*=lblAdvancedOptionsSelected].advancedOptionsSelected');

    var btnWidth = (width / 4) + 10 ;
    var lblWidth = (width / 4) * 3 - 49;
    $btnAdvancedOptions.css('width', btnWidth + 'px');
    $lblAdvancedOptions.css('width', lblWidth + 'px');
    if (lblWidth > 410) {
        $lblAdvancedOptions.addClass('advancedOptionsSelectedWide');
    }
    else {
        $lblAdvancedOptions.removeClass('advancedOptionsSelectedWide');
    }

}