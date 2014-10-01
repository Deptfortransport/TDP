var currentVenueMap = null;
var currentPagePosition = null;
var loaded = false;

// Initialise javascript stuff
$(document).ready(function () {
    setupVenueMap();
    setupAdditionalNotesDisplay();
    setupAdditionalNotesDialogs($('tr.expanded'));
    setupCallingPoints();
    setupCallingPointsLink();
    setupLegLines();

    // Following lines are to rebind the jquery plugins or js events after ajax update of the update panel
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        setupAdditionalNotesDisplay();
        setupCallingPointsLink();
        setupLegLines();
    });
});


// Displays the intermediate calling points
function setupCallingPoints() {
    // Click event to show intermediate calling points
    $(document).on('click', 'div.legModeImgDivBorder', function (e) {
        displayCallingPoints(this, null, e);
        return false;
    });
}

// Displays the intermediate calling points
function setupCallingPointsLink() {
    $('div.legModeImgDivBorder').each(function () {

        // Set up a clickable link to show calling points
        var title = $(this).find('img').data('showtext');

        var s = "<a title=\"" + title + "\" href=\"#\" onclick=\"displayCallingPoints(this,'" + $(this).attr('id') + "', null); return false;\">"
                + $(this).html()
                + "</a>";

        $(this).html(s);
    });
}

// Display the intermediate calling points
function displayCallingPoints(control, controlId, event) {

    if (event != null) {
        if (event.target != control)
            return;
    }

    var $control;
    if (controlId != null) {
        $control = $('#' + controlId);
    }
    else {
        $control = $(control);
    }

    var $callingPointsRow = $control.parent().parent().parent().find('div[id*=callingPointsRow]');

    if ($callingPointsRow) {
        $callingPointsRow.slideToggle(300);

        var $image = $control.find('img');
        var $link = $control.find('a');

        var showText = $image.data('showtext');
        var hideText = $image.data('hidetext');

        // Update the title text
        if ($image.attr('title') == showText) {
            $image.attr('title', hideText);
            $link.attr('title', hideText);
            $control.attr('title', hideText);
        }
        else {
            $image.attr('title', showText);
            $link.attr('title', showText);
            $control.attr('title', showText);
        }
    }
}

// Resizes the leg node connecting line heights
function setupLegLines() {
    $("img[id*='legLineImage']").each(function () {
        //need great grandparent 
        $(this).height(10);
    });
    $("img[id*='legLineImage']").each(function () {
        //need great grandparent 
        var ggp = $(this).parent().parent().parent();
        $(this).height(ggp.height() + 5);

        var $mode = $(ggp).find('.columnMode img[id*=legMode]');
        if ($mode != null) {
            var modeAltText = $mode.prop('alt');
            if (modeAltText == 'Bus') {
                // Add a bit extra for mode legs
                $(this).height(ggp.height() + 15);
            }
        }
    });
    $("div.nodeImage").each(function () {
        var gp = $(this).parent().parent();
        var newHeight = gp.find('.columnLocation').height();

        if ($(this).height() < newHeight) {
            $(this).height(newHeight);
        }
    });
}

// Setup click event handling for the venue map button
function setupVenueMap() {
    // attaches the the click event of the view map button in the last leg
    $(document).on("click", "a[id*=endLocationMapLink]", function () {
        
        var documentHeight = $(document).height();

        $('div[id*=destinationVenueMapControl]').css({ display: 'block', height: documentHeight });
        $(document).scrollTop(0);
        return false;
    });

    // attaches the the click event of the view map button in the first leg
    $(document).on("click", "a[id*=locationMapLink]", function () {

        var documentHeight = $(document).height();

        $('div[id*=originVenueMapControl]').css({ display: 'block', height: documentHeight });
        $(document).scrollTop(0);
        return false;
    });

    // closes any open map pages
    $(document).on("click", "a[id*=closevenuemap]", function () {
        $('div[id*=VenueMapControl]').css('display', 'none');
        $('.venueMapImgDiv').css('width', '100%');
        $('.venueMapImgDiv').css('height', 'auto');
        return false;
    });
}

// Sets the visibility of the additional notes text 
function setupAdditionalNotesDisplay() {
    $('div[id*=divNotes]').css({ display: "none" });
    $('div[id*=travelNotesLinkDiv]').css({ display: "block" });
}

// Sets up the dialog boxes for journey web notes
function setupAdditionalNotesDialogs(container) {

    $(document).on("click", "a.travelNotesLink", function () {

        var title = $(this).text();
        var note = $(this.parentNode.parentNode).find('div.detailNote').html()

        $('div#additionaInfoDialog h2[id*=additionalInfoHeader]').html(title);
        $('div#additionaInfoDialog div#additionalInfoNotes').html(note);

        var documentHeight = $(document).height();
        var windowHeight = $(window).height() / 2;

        $('div#additionalInfoPage').css({ display: 'block', height: documentHeight });
        $('div#additionaInfoDialog').css({ top: windowHeight+'px'});
        currentPagePosition = window.pageYOffset;
        $(document).scrollTop(0);

        return false;
    });

    $(document).on("click", "a[id*=closeinfodialog]", function () {
        $('div#additionalInfoPage').css('display', 'none');
        $(document).scrollTop(currentPagePosition);
        return false;
    });
}
