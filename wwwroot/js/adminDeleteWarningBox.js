function deleteRecord(row, url) {
    $.ajax({
        url: `${url}`,
        type: 'POST'
    }).done(() => {
        row.closest('tr').remove();
    });
}

const showDeleteWarningBox = (function () {
    return function(e) {
        const { currentTarget } = e;
        swal({
            title: "Bạn có chắc không?",
            text: currentTarget.getAttribute('data-message'),
            icon: "warning",
            buttons: true,
            dangerMode: true
        }).then(value => {
            if (value) deleteRecord(currentTarget, currentTarget.getAttribute('data-delete-action'));
            else return;
        });
    }
})();

$('.delete-button').on('click', showDeleteWarningBox);