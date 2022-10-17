function deleteRecordInRow(target, url) {
    $.ajax({
        url: `${url}`,
        type: 'POST'
    }).done(() => {
        const getCameraId = target.getAttribute("data-cameraid");
        const corresondingDiv = document.querySelector(`div.cart-item[data-cameraid='${getCameraId}']`);
        const corresondingTr = document.querySelector(`tr.cart-item[data-cameraid='${getCameraId}']`);
        const cartNotiAmount = document.getElementById("amountOfProductInCart");
        const cartAmountItemMessage = document.getElementById("cartAmountItemMessage");
        const cartTotal = document.getElementById("cartTotal");
        cartAmountItemMessage.textContent = "Hiện tại giỏ hàng đang rỗng.";
        cartTotal.textContent = "0 đ"; 
        corresondingDiv.remove();
        cartNotiAmount.textContent = document.querySelectorAll("div.cart-item[data-cameraid]").length;
        if (corresondingTr == null) return;
        corresondingTr.remove();
    });
}

const showDeleteWarningBoxFromRow = (function () {
    return function(e) {
        const { currentTarget } = e;
        swal({
            title: "Bạn có chắc không?",
            text: currentTarget.getAttribute('data-message'),
            icon: "warning",
            buttons: true,
            dangerMode: true
        }).then(value => {
            if (value) deleteRecordInRow(currentTarget, currentTarget.getAttribute('data-delete-action'));
            else return;
        });
    }
})();

$('.delete-button').on('click', showDeleteWarningBoxFromRow);