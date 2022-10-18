using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TH4_Nhom20.Data;
using TH4_Nhom20.Models;

namespace TH4_Nhom20.Areas.User.Controllers
{
    [Area("User")]
    public class InteractProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public InteractProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult Like(int cameraId, string isLiked)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return Json(new { success = false });
            }
            UserModel user = _db.USER.Where(user => user.Id == claim.Value).First();
            if (user.LikedProduct == null)
            {
                user.LikedProduct = cameraId.ToString();
            }
            else
            {
                List<string> likedProduct = user.LikedProduct.Split(';').ToList();
                if (isLiked == "true")
                {
                    likedProduct.Add(cameraId.ToString());
                }
                else
                {
                    likedProduct.Remove(cameraId.ToString());
                }
                user.LikedProduct = string.Join(";", likedProduct);
            }
            _db.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult Review(int cameraId, string review)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return Json(new { success = false });
            };
            UserModel user = _db.USER.Where(u => u.Id == claim.Value).First();
            ReviewModel newReview = new ReviewModel()
            {
                CameraId = cameraId,
                UserId = claim.Value,
                Review = review,
                ReviewedDate = DateTime.Now,
            };
            _db.REVIEWS.Add(newReview);
            _db.SaveChanges();
            ReviewModel lastestAddedReview = _db.REVIEWS
                .Where(c => c.CameraId == cameraId && c.UserId == claim.Value)
                .First();
            return Json(new { 
                success = true,
                UserName =  user.Name,
                ReviewedDate = newReview.ReviewedDate,
                CameraId = cameraId,
                ReviewId = lastestAddedReview.Id
            });
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult DeleteReview(int cameraId, int reviewId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            ReviewModel review = _db.REVIEWS.Where(r =>
                r.Id == reviewId
                && r.CameraId == cameraId
                && r.UserId == claim.Value
            ).First();
            _db.REVIEWS.Remove(review);
            _db.SaveChanges();
            return Json(new { success = true });
        }

    }
}
