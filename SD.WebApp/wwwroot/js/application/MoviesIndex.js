$(document).ready(function () {

    //var movieTable = document.querySelector('#MoviesTable');

    var $movieTable = $('#MoviesTable');
    var $deleteHyperLinks = $movieTable.find('a[data-name=Delete]');

    var $detailsHyperLinks = $movieTable.find('a[data-name=Details]');


    $deleteHyperLinks.on('click', function () {

        var rowsToShow = [0, 1, 2, 3, 4];
        ShowConfirmDialog("ConfirmModal", this, true, rowsToShow);

        return false;
    });

    $detailsHyperLinks.on('click', function () {

        var rowsToShow = [0, 1, 2, 3];
        ShowConfirmDialog("ConfirmModal", this, false, rowsToShow);

        return false;
    });


})

