// ***********************************************
// NAME     : JourneyPlannerInput.js
// AUTHOR   : Atos Origin
// ************************************************

$(document).ready(function() {

    setupAdvancedOptions();
    setupAccessibleOptions();
    setupInformationTips();
    updateOptionsSelectedLabel();

});

function setupAdvancedOptions() {

    // Hand cursor mouse pointer class
    $('.optionHeadingRow').addClass('optionCursor');

    // Find the advanced options heading text and replace with a dummy link button
    // so user can tab to and select using keyboard
    $('.optionHeadingRow .boxOptionHeading').each(function() {

        var optionHeading = $(this).find('span').html();

        var dummyBtn = $('<a href="#" onclick="return false;" class="plainlink" role="button">' + optionHeading + '</a>');

        $(this).find('span').hide().after(dummyBtn);
    });
    
    // Find all the advanced options heading rows and add a click event to toggle the options content
    $('.optionHeadingRow').click(function(event) {

        // Toggle arrow
        $(this).find('.boxArrowToggle').toggleClass('boxArrowToggleDown');

        // Toggle content
        var $contentControl = $(this).parent().find('.optionContentRow').slideToggle(300);

        // Set aria-expanded
        if ($(this).parent().find('.optionContentRow').attr('aria-expanded') == 'true') {
            $(this).parent().find('.optionContentRow').attr('aria-expanded', 'false');
        }
        else {
            $(this).parent().find('.optionContentRow').attr('aria-expanded', 'true');
        }
        
        // Bug - if there is a via location control, ensure its location types are hidden until needed
        // Only slide up if more options button exists and is displayed
        var $locationControl = $contentControl.find('.locationControl');
        var $moreOptionsControl = $locationControl.find('.moreOptions');
        if (($moreOptionsControl.length > 0) && ($moreOptionsControl.hasClass('hide') == false)) {
            $locationControl.find('.locationTypesRow').slideUp(0)
        };

        // Update options selected message
        updateOptionsSelectedLabel();

        event.stopPropagation();
        return false;
    });
}

function setupAccessibleOptions() {

    // Set up the fewer changes box enable/disable
    if ($('div.accessibleBox input[id*=noRequirement]').attr('checked')) {
        $('div.accessibleBox input[id*=checkFewestChanges]').attr('disabled', 'disabled').addClass('disabled');
    }

    $('div.accessibleBox input[name*=accessiblityOptions]').bind('click', function(event) {
        if ($('div.accessibleBox input[id*=noRequirement]').attr('checked')) {
            $('div.accessibleBox input[id*=checkFewestChanges]').attr('disabled', 'disabled').addClass('disabled');
        }
        else {
            $('div.accessibleBox input[id*=checkFewestChanges]').removeAttr('disabled').removeClass('disabled');
        }
    });

}

// Sets up information tips
function setupInformationTips() {

    // Info tooltips
    $('div.accessibleBox td.accessibleInfo a.tooltip_information').qtip({
        position: {
            my: 'left center',
            at: 'right center',
            container: $('.accessibleBox'),
            adjust: {
            x: 0,
                y: -5,
                resize: true
            }
        },
        style: {
            classes: 'ui-tooltip-tdp'
        },
        show: {
            event: 'click mouseover mouseenter'
        },
        hide: {
            event: 'click mouseleave',
            fixed: true
        }
    });
}

function updateOptionsSelectedLabel() {

    ShowOptionsSelectedAccessible();
    ShowOptionsSelectedTransportTypes();
    ShowOptionsChanges();
    ShowOptionsWalkSpeed();
    ShowOptionsVia();
    ShowOptionsCar();
}

function ShowOptionsSelectedAccessible() {
    var group = document.getElementsByName('accessibleOptionsControl$accessiblityOptions')
    var show = false;

    for (var i = 0; i < group.length; i++) {
        if (group[i].checked) {
            if (group[i].value != 'noRequirement') {
                show = true;
            }
        }
    }

    if (show) {
        document.getElementById('accessibleOptionsControl_labelOptionsSelected').style.display = 'inline';
    }
    else {
        document.getElementById('accessibleOptionsControl_labelOptionsSelected').style.display = 'none';
    }
}

function ShowOptionsSelectedTransportTypes() {
    var checkList = document.getElementById('transportTypesControl_checklistModesPublicTransport');
    var group = checkList.getElementsByTagName('input');
    var show = false;

    for (var i = 0; i < group.length; i++) {
        if (group[i].checked == false) {
            show = true;
        }
    }

    if (show) {
        document.getElementById('transportTypesControl_labelOptionsSelected').style.display = 'inline';
    }
    else {
        document.getElementById('transportTypesControl_labelOptionsSelected').style.display = 'none';
    }
}

function ShowOptionsChanges() {
    var changesDrop = document.getElementById('ptPreferencesControl_journeyChangesOptionsControl_listChangesShow');
    var show = false;

    if (changesDrop.selectedIndex != 0) {
        show = true;
    }

    var speedDrop = document.getElementById('ptPreferencesControl_journeyChangesOptionsControl_listChangesSpeed');

    if (speedDrop.selectedIndex != 0) {
        show = true;
    }

    if (show) {
        document.getElementById('ptPreferencesControl_journeyChangesOptionsControl_labelOptionsSelected').style.display = 'inline';
    }
    else {
        document.getElementById('ptPreferencesControl_journeyChangesOptionsControl_labelOptionsSelected').style.display = 'none';
    }
}

function ShowOptionsWalkSpeed() {
    var speeedDrop = document.getElementById('ptPreferencesControl_walkingSpeedOptionsControl_listWalkingSpeed');
    var show = false;

    if (speeedDrop.selectedIndex != 0) {
        show = true;
    }

    var timeDrop = document.getElementById('ptPreferencesControl_walkingSpeedOptionsControl_listWalkingTime');

    if (timeDrop.selectedIndex != 4) {
        show = true;
    }

    if (show) {
        document.getElementById('ptPreferencesControl_walkingSpeedOptionsControl_labelOptionsSelected').style.display = 'inline';
    }
    else {
        document.getElementById('ptPreferencesControl_walkingSpeedOptionsControl_labelOptionsSelected').style.display = 'none';
    }
}

function ShowOptionsVia() {
    var via = document.getElementById('ptPreferencesControl_locationControlVia_locationControl_locationInput');
    var show = false;

    if (via.value.length > 0) {
        if (!$(via).hasClass('watermarkText')) {
            show = true;
        }
    }

    if (show) {
        document.getElementById('ptPreferencesControl_locationControlVia_labelOptionsSelected').style.display = 'inline';
    }
    else {
        document.getElementById('ptPreferencesControl_locationControlVia_labelOptionsSelected').style.display = 'none';
    }
}

function ShowOptionsCar() {
    var typeDrop = document.getElementById('carPreferencesControl_listCarJourneyType');
    var show = false;

    if (typeDrop.selectedIndex != 0) {
        show = true;
    }

    var speedDrop = document.getElementById('carPreferencesControl_listCarSpeed');
    if (speedDrop.selectedIndex != 0) {
        show = true;
    }

    var noMs = document.getElementById('carPreferencesControl_doNotUseMotorwaysCheckBox');
    if (noMs.checked) {
        show = true;
    }

    var carSizeDrop = document.getElementById('carPreferencesControl_listCarSize');
    if (carSizeDrop.selectedIndex != 1) {
        show = true;
    }

    var fuelTypeDrop = document.getElementById('carPreferencesControl_listFuelType');
    if (fuelTypeDrop.selectedIndex != 0) {
        show = true;
    }

    var consumption = document.getElementById('carPreferencesControl_fuelConsumptionSelectRadio_1');
    if (consumption.checked) {
        show = true;
    }

    var cost = document.getElementById('carPreferencesControl_fuelCostSelectRadio_1');
    if (cost.checked) {
        show = true;
    }

    var tolls = document.getElementById('carPreferencesControl_journeyOptionsControl_avoidTollsCheckBox');
    if (tolls.checked) {
        show = true;
    }

    var ferries = document.getElementById('carPreferencesControl_journeyOptionsControl_avoidFerriesCheckBox');
    if (ferries.checked) {
        show = true;
    }

    var ms = document.getElementById('carPreferencesControl_journeyOptionsControl_avoidMotorwayCheckBox');
    if (ms.checked) {
        show = true;
    }

    var limited = document.getElementById('carPreferencesControl_journeyOptionsControl_banLimitedAccessCheckBox');
    if (limited.checked) {
        show = true;
    }

    var road1 = document.getElementById('carPreferencesControl_journeyOptionsControl_avoidRoadSelectControl1_unspecifiedRoad_boxTextRoad');
    if (road1.value.length > 0) {
        show = true;
    }

    var road2 = document.getElementById('carPreferencesControl_journeyOptionsControl_avoidRoadSelectControl2_unspecifiedRoad_boxTextRoad');
    if (road2.value.length > 0) {
        show = true;
    }

    var road3 = document.getElementById('carPreferencesControl_journeyOptionsControl_avoidRoadSelectControl3_unspecifiedRoad_boxTextRoad');
    if (road3.value.length > 0) {
        show = true;
    }

    var road4 = document.getElementById('carPreferencesControl_journeyOptionsControl_avoidRoadSelectControl4_unspecifiedRoad_boxTextRoad');
    if (road4.value.length > 0) {
        show = true;
    }

    var road5 = document.getElementById('carPreferencesControl_journeyOptionsControl_avoidRoadSelectControl5_unspecifiedRoad_boxTextRoad');
    if (road5.value.length > 0) {
        show = true;
    }

    var road6 = document.getElementById('carPreferencesControl_journeyOptionsControl_avoidRoadSelectControl6_unspecifiedRoad_boxTextRoad');
    if (road6.value.length > 0) {
        show = true;
    }

    var uroad1 = document.getElementById('carPreferencesControl_journeyOptionsControl_useRoadSelectControl1_unspecifiedRoad_boxTextRoad');
    if (uroad1.value.length > 0) {
        show = true;
    }

    var uroad2 = document.getElementById('carPreferencesControl_journeyOptionsControl_useRoadSelectControl2_unspecifiedRoad_boxTextRoad');
    if (uroad2.value.length > 0) {
        show = true;
    }

    var uroad3 = document.getElementById('carPreferencesControl_journeyOptionsControl_useRoadSelectControl3_unspecifiedRoad_boxTextRoad');
    if (uroad3.value.length > 0) {
        show = true;
    }

    var uroad4 = document.getElementById('carPreferencesControl_journeyOptionsControl_useRoadSelectControl4_unspecifiedRoad_boxTextRoad');
    if (uroad4.value.length > 0) {
        show = true;
    }

    var uroad5 = document.getElementById('carPreferencesControl_journeyOptionsControl_useRoadSelectControl5_unspecifiedRoad_boxTextRoad');
    if (uroad5.value.length > 0) {
        show = true;
    }

    var uroad6 = document.getElementById('carPreferencesControl_journeyOptionsControl_useRoadSelectControl6_unspecifiedRoad_boxTextRoad');
    if (uroad6.value.length > 0) {
        show = true;
    }

    var loc = document.getElementById('carPreferencesControl_journeyOptionsControl_locationControl_locationInput');
    if (loc.value.length > 0) {
        if (!$(loc).hasClass('watermarkText')) {
            show = true;
        }
    }

    if (show) {
        document.getElementById('carPreferencesControl_labelOptionsSelected').style.display = 'inline';
    }
    else {
        document.getElementById('carPreferencesControl_labelOptionsSelected').style.display = 'none';
    }
}
