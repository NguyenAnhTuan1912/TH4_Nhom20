function likeProduct(target, url) {
    $.ajax({
        url: `${url}`,
        type: 'POST'
    }).done( value => {
        if (value.success) {
            const heartNoti = document.getElementById("amountOfLikedProduct");
            let amountOfLikedProduct = parseInt(heartNoti.textContent);
            const prevLikeStatus = target.getAttribute("data-liked");
            if(prevLikeStatus == "true") {
                target.setAttribute("data-liked", "false");
                target.classList.remove("active");
                amountOfLikedProduct -= 1;
                heartNoti.textContent = amountOfLikedProduct;
            } else {
                target.classList.add("active");
                amountOfLikedProduct += 1;
                heartNoti.textContent = amountOfLikedProduct;
                target.setAttribute("data-liked", "true");
            }
        }
    });
}

const handleLikeButtonClick = (function () {
    return function (e) {
        const { currentTarget } = e;
        const isLiked = currentTarget.getAttribute("data-liked") == 'false' ? 'true' : 'false';
        const cameraId = currentTarget.getAttribute("data-cameraid");
        const url = `/User/InteractProduct/Like/?cameraId=${cameraId}&isLiked=${isLiked}`
        likeProduct(currentTarget, url)
    }
})();
$(".like-btn").on("click", handleLikeButtonClick);