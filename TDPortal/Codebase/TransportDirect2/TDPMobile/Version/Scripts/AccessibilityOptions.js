// Initialise javascript stuff
firstRun = true;

$(document).ready(function () {

    setupPlanJourney();
    setupStopsList();
});

// Sets the plan journey button enabled disabled state
function setupPlanJourney() {
    if (firstRun) {
        // never enable on first run... could be displaying nothing found error
        firstRun = false;
    }
    else {
        enable = true;

        if (($('select[id*=originStopList]').length > 0) &&
        ($('select[id*=originStopList] option:selected').val() = "")) {
            enable = false;
        }

        if (($('select[id*=destinationStopList]').length > 0) &&
        ($('select[id*=destinationStopList] option:selected').val() = "")) {
            enable = false;
        }

        if (enable) {
            $('a[id*=planJourneyBtn]').removeClass('buttonMagDisabled');
            $('a[id*=planJourneyBtn]').removeAttr('disabled');
        }
        else {
            if (!$('a[id*=planJourneyBtn]').hasClass('buttonMagDisabled')) {
                $('a[id*=planJourneyBtn]').addClass('buttonMagDisabled');
                $('a[id*=planJourneyBtn]').attr('disabled', 'disabled');
            }
        }
    }
}

// Sets up the stop lists
function setupStopsList() {

    $(document).on("change", "select[id*=originStopList]", function () {
        setupPlanJourney();
    });

    $(document).on("change", "select[id*=destinationStopList]", function () {
        setupPlanJourney();
    });
}
