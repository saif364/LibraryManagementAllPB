$(document).ready(function () {

    //#region Global toaster 
    var redirectUrl = "";
    var message = getQueryParam('message');
    if (message) {

        toastr.success(decodeURIComponent(message));
        removeQueryParam('message');
    }

    function getQueryParam(param) {
        var urlParams = new URLSearchParams(window.location.search);
        return urlParams.get(param);
    }

    function removeQueryParam(param) {
        var url = window.location.href;
        var urlWithoutParam = url.split('?')[0];
        window.history.replaceState({}, document.title, urlWithoutParam);
    }

    // Handle form submission
    $('form').on('submit', function (event) {
        debugger;
        event.preventDefault();

        var $form = $(this);
        var url = $form.attr('action');
        var methodType = $form.attr('method');
        var data = $form.serialize();

        $.ajax({
            url: url,
            type: methodType,
            data: data,
            success: function (response) {
                if (response.success) {
                    var redirectUrl = response.redirectUrl + "?message=" + encodeURIComponent(response.message);
                    window.location.href = redirectUrl;
                } else {
                    toastr.warning(response.message);
                }
            },
            error: function (xhr, status, error) {
                toastr.error('An unexpected error occurred: ' + error);
            }
        });
    });

    $('.GlobalAjax').on('click', function (event) {
        debugger;
        event.preventDefault();

        var $link = $(this); // this refers to the <a> tag
        var url = $link.attr('href'); // Get the href attribute for the URL

        $.ajax({
            url: url,
            type: 'GET', // Anchor links usually use GET requests
            success: function (response) {
                if (response.success) {
                    var redirectUrl = response.redirectUrl + "?message=" + encodeURIComponent(response.message);
                    window.location.href = redirectUrl;
                } else {
                    toastr.warning(response.message);
                }
            },
            error: function (xhr, status, error) {
                toastr.error('An unexpected error occurred: ' + error);
            }
        });
    });


    // Handle delete action
    // Handle delete action with toastr confirmation
    // Variable to hold the URL for deletion
    let deleteUrl = '';

    // Handle delete button click
    $('.delete-button').on('click', function (event) {
        event.preventDefault();
        deleteUrl = $(this).attr('href'); // Store the URL for deletion
        $('#deleteConfirmationModal').modal('show'); // Show the modal
    });

    // Handle the confirmation button click
    $('#confirmDeleteButton').on('click', function () {
        $.ajax({
            url: deleteUrl,
            type: 'DELETE',
            success: function (response) {
                if (response.success) {
                    toastr.success(response.message);
                    // Optionally, remove the deleted row
                    $('.delete-button[href="' + deleteUrl + '"]').closest('tr').remove();
                } else {
                    toastr.warning(response.message);
                }
            },
            error: function (xhr, status, error) {
                toastr.error('An unexpected error occurred: ' + error);
            }
        });
        $('#deleteConfirmationModal').modal('hide'); // Hide the modal after confirmation
    });


    //#endregion
});




