// ***********************************************
// NAME     : LocationInput.js
// AUTHOR   : Atos
// ************************************************

$(document).ready(function() {

    displayJavascriptControls();

    setupLocationInput();
    setupEmptyLocationText();
    setupMoreOptions();

    // Following lines are to rebind the jquery plugins or js events after ajax update of the update panel
    try {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function() {
            
            displayJavascriptControls();
            setupEmptyLocationText();
            
        });
    } catch (error) {
        // Ignore error, page may not have ajax
    }
});

// Sets up the location input 
function setupLocationInput() {

    // Remove any error class when location input changes (event is run once only per locationBox input)
    $('.locationBox').one('keyup', function() {

        $(this).removeClass("alertboxerror");

        var $locationControl = $(this).closest('.locationControl');

        $locationControl.find('.ambiguityInputRow').addClass('hide');
    });

    // Clears the entered location text and/or grey watermark text when the input recieves focus
    $(document).on("click", "input[name*=locationInput].locationBox", function() {
        $(this).val('');
        $(this).removeClass("watermarkText");
        $(this).removeClass("alertboxerror");        
    });
    $(document).on("focus", "input[name*=locationInput].locationBox.watermarkText", function() {
        $(this).val('');
        $(this).removeClass("watermarkText");
        $(this).removeClass("alertboxerror");        
    });
}

// Add watermark to input fields
function setupEmptyLocationText() {

    $("input[name*=locationInput]").each(function() {

        // Only add empty location text if moreOptions button displayed
        var $locationControl = $(this).closest('.locationControl');

        var disableLocationSuggest = getJSHdnSettingFieldValue('locationSuggestDisabled', $locationControl);
        if (disableLocationSuggest == 'false') {
        
            if ($(this).val() == '') {
                $(this).val($(this).data('defaultvalue'));
                if ($(this).hasClass("locationBox")) {
                    $(this).addClass("watermarkText");
                }
            }
            else if ($(this).val() == $(this).data('defaultvalue')) {
                if ($(this).hasClass("locationBox")) {
                    $(this).addClass("watermarkText");
                }
            }
        }
    });
}

// Sets up the collapsible location gazetteer options
function setupMoreOptions() {

    $('.locationControl').each(function () {

        // Only hide "more options" if more options button exists
        if ($(this).find('.moreOptions').length > 0) {

            // Make location input box longer
            $(this).find('.locationBox').addClass('locationBoxLong');

            // Hide unsure spelling
            $(this).find('.unsureSpelling').addClass('hide');

            // Hide location gaz types
            $(this).find('.locationTypesRow').slideUp(0);

            // Hide find on map button
            showTDButton(false, $(this).find('.findOnMap'));

            // Show more options button
            showTDButton(true, $(this).find('.moreOptions'));

            // Show more options when the more button is clicked
            $(this).find('.moreOptions').click(function (event) {

                var $locationControl = $(this).closest('.locationControl');

                $locationControl.find('.locationBox').toggleClass('locationBoxLong');
                $locationControl.find('.unsureSpelling').toggleClass('hide');
                $locationControl.find('.locationTypesRow').slideToggle(300);

                // Remove watermark text
                var $locationBox = $locationControl.find('.locationBox');
                if ($locationBox.val() == $locationBox.data('defaultvalue')) {
                    $locationBox.val('');
                    $locationBox.removeClass("watermarkText");
                }

                // Show find on map button
                showTDButton(true, $locationControl.find('.findOnMap'));

                // Hide the more options button
                showTDButton(false, $(this));

                // Set hidden value to indicate more was clicked, to allow page to log
                $locationControl.find('input[id*=moreSelected]').val('true');

                event.preventDefault();
            });
        }
        else {

            // Display any items that are hidden by default and only enabled by javascript, e.g. map button

            // Show find on map button
            showTDButton(true, $(this).find('.findOnMap'));
        }
    });
}