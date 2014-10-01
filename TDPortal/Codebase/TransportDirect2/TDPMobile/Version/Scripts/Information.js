
// Initialise javascript stuff
$(document).ready(function () {
    setupLegLines();

    // Following lines are to rebind the jquery plugins or js events after ajax update of the update panel
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        setupLegLines();
    });
});

// Resizes the leg node connecting line heights
function setupLegLines() {
    $("div.nodeImage").each(function () {
        var gp = $(this).parent().parent();
        var newHeight = gp.find('.columnLocation').height();

        if ($(this).height() < newHeight) {
            $(this).height(newHeight);
        }
    });
}