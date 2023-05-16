// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ShowConfirmDialog(modalId, ctl, showDelete, rowsToShow) {

    /* Dynamische Befüllung des modalen Dialogs */

    var $row = $(ctl).parent().parent();
    var $columns = $row.find('td');


    /* Bestehende Inhalte im Modal-Body leeren. */

    var $modal = $('#' + modalId);
    var $modalBody = $modal.find('#modalBody');
    $modalBody.empty();


    /* Überschriften auslesen */

    var $thRows = $row.parent().parent().find('th');

    for (var i = 0; i < rowsToShow.length; i++) {
        $modalBody.append($('<dt class="col-md-3">').html($thRows[rowsToShow[i]].innerText));
        $modalBody.append($('<dd class="col-md-9">').html($columns[rowsToShow[i]].innerText));
    }

    /* Überschrift abhängig, ob Löschen oder Details anzeigen setzen */

    var $headerTitle = $modal.find('#ConfirmModalTitle');
    var $deleteControls = $modal.find('#DeleteControls');

    if (showDelete) {
        $headerTitle.text("Sind Sie sicher, das Sie diesen Eintrag löschen wollen?");
        $deleteControls.show();
        var itemId = $(ctl).attr('data-id');
        var $modalId = $modal.find('#Id');
        $modalId.val(itemId);
    } else {
        $headerTitle.text("Details");
        $deleteControls.hide();
    }

    var options = {
        "backdrop": "static",
        "keyboard": true
    };

    var modal = new bootstrap.Modal(document.getElementById(modalId), options);
    modal.show();

    
}