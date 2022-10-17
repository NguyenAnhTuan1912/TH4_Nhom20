function likeProduct(target, url) {
    $.ajax({
        url: `${url}`,
        type: 'POST'
    }).done( value => {
        if(value.success) {
            const heartOfLikeButton = target.querySelector("i.fa.fa-heart");
            const heartNoti = document.getElementById("amountOfLikedProduct");
            let amountOfLikedProduct = parseInt(heartNoti.textContent);
            target.style.backgroundColor = "#7fad39";
            target.style.borderColor = "#7fad39";
            heartOfLikeButton.style.color = "#ffffff";
            heartOfLikeButton.style.transform = "rotate(360deg)";
            amountOfLikedProduct += 1;
            heartNoti.textContent = amountOfLikedProduct;
        }
    });
}

const handleLikeButtonClick = (function () {
    return function (e) {
        const { currentTarget } = e;
        const isLike = currentTarget.getAttribute("data-liked") === 'false' ? 'true' : 'false';
        const cameraId = currentTarget.getAttribute("data-cameraid");
        const url = `/User/InteractProduct/Like/?cameraId=${cameraId}&isLiked=${isLike}`
        likeProduct(currentTarget, url)
    }
})();
$(".like-btn").on("click", handleLikeButtonClick);