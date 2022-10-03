function deleteRecord(id, row) {
    $.ajax({
        url: `Cameras/Delete/${id}`,
        type: 'POST'
    }).done(() => {
        row.closest('tr').remove();
    });
}

$('.delete-button').on('click', (e) => {
    const { target } = e;
    swal({
        title: "Bán có chắc không?",
        text: "Một khi bạn xoá sản phẩm này, thì mọi dữ liệu của nó sẽ biến mất vĩnh viễn.",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then(value => {
        if (value) deleteRecord(target.getAttribute('data-itemid'), target);
        else return;
    });
});

