function likeProduct(target, url) {
    $.ajax({
        url: `${url}`,
        type: 'POST'
    }).done( value => {
        if (value.success) {
            const heartNoti = document.getElementById("amountOfLikedProductIds");
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

function reviewProduct(target, url) {
    $.ajax({
        url: `${url}`,
        type: 'POST'
    }).done(value => {
        if (value.success) {
            const review = document.createElement("div");
            const deleteBtnContainer = document.createElement("div");
            const deleteBtn = document.createElement("a");
            const xIcon = document.createElement("span");
            review.classList.add("review", "row", "mb-3");
            deleteBtnContainer.classList.add("button" ,"col-lg-1");
            deleteBtn.classList.add("delete-review-button", "text-dark");
            xIcon.classList.add("material-symbols-outlined");
            xIcon.textContent = "close";

            review.setAttribute("data-reviewid", value.reviewId);

            deleteBtn.style.cursor = "pointer";
            deleteBtn.setAttribute("data-cameraid", value.cameraId);
            deleteBtn.setAttribute("data-reviewid", value.reviewId);
            deleteBtn.addEventListener("click", handleDeleteReviewButtonClick);
            
            const template = `
                <div class="review-user-avatar col-lg-1"></div>
                <div class="review-text col-lg-9">
                    <p class="review-user-name">${value.userName}</p>
                    <p class="review-date">Lúc ${new Date(value.reviewedDate).toLocaleString().replace(",", "")}</p>
                    <p class="review-text mt-2">${document.getElementById("inputReviewBox").value}</p>
                </div>
            `;
            console.log(deleteBtnContainer);
            document.getElementById("inputReviewBox").value = "";
            review.innerHTML = template;
            deleteBtnContainer.append(deleteBtn);
            deleteBtn.append(xIcon);
            review.append(deleteBtnContainer);
            target.prepend(review);
        }
    });
}

function deleteReview(target, url) {
    $.ajax({
        url: `${url}`,
        type: 'POST'
    }).done(value => {
        if (value.success) {
            swal({
                titel: "Hoàn tất!",
                text: "Bạn đã xoá review thành công!",
                icon: "success",
                button: "Tôi biết rồi"
            });
            target.closest("div.review.row.mb-3").remove();
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

const handleSendReviewButtonClick = (function () {
    return function (e) {
        const inputReviewBox = document.getElementById("inputReviewBox");
        const cameraId = inputReviewBox.getAttribute("data-cameraid");
        const reviewBoxs = document.getElementById("reviewBox");
        const url = `/User/InteractProduct/Review/?cameraId=${cameraId}&review=${inputReviewBox.value}`;
        reviewProduct(reviewBoxs, url);
    }
})();

const handleDeleteReviewButtonClick = (function () {
    return function (e) {
        const { currentTarget } = e;
        const reviewId = currentTarget.getAttribute("data-reviewid");
        const cameraId = currentTarget.getAttribute("data-cameraid");
        const reviewBox = document.querySelector(`div.review[data-reviewid="${reviewId}"]`);
        const url = `/User/InteractProduct/DeleteReview/?cameraId=${cameraId}&reviewId=${reviewId}`;
        deleteReview(reviewBox, url);
    }
})();

$(".like-btn").on("click", handleLikeButtonClick);
$("#sendReviewBtn").on("click", handleSendReviewButtonClick);
$(".delete-review-button").on("click", handleDeleteReviewButtonClick);