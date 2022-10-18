using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Collections.Immutable;
using System.Security.Claims;
using TH4_Nhom20.Data;
using TH4_Nhom20.Models;

namespace TH4_Nhom20.Controllers
{
    [Area("User")]
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ShopController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<CameraModel> cameras = _db.CAMERA.ToList();
            foreach(CameraModel camera in cameras)
            {
                camera.DiscoutedPrice = (uint.Parse(camera.Price) * (100 - camera.Discount)) / 100;
            }
            ViewBag.Cameras = cameras;
            ViewBag.AmountOfCamera = cameras.Count();
            return View();
        }

        public IActionResult Details(int cameraId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            UserModel user = new UserModel();
            if(claim != null)
            {
                user = _db.USER.Where(u => u.Id == claim.Value).First();
            }
            CartModel cart = new CartModel
            {
                CameraId = cameraId,
                Camera = _db.CAMERA
                .Include(camera => camera.Brand)
                .Where(camera => camera.Id == cameraId)
                .First(),
                Amount = 1
            };
            IEnumerable<ReviewModel> reviewList = _db.REVIEWS.Include("User").Where(c => c.CameraId == cameraId).ToList();
            IEnumerable<CameraModel> cameras = _db.CAMERA
                .Where(camera => 
                        camera.Category == cart.Camera.Category
                        && camera.BrandName == cart.Camera.BrandName
                        && camera.Id != cameraId
            );
            ViewBag.relatedCamera = cameras;
            ViewBag.ReviewList = reviewList;
            ViewBag.Cart = cart;
            ViewBag.UserId = user.Id;
            return View();
        }

        public IActionResult Liked()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            List<string> likedProductIds = new List<string>();
            if (claim == null)
            {
                return View();
            }
            UserModel user = _db.USER
                .Where(user => user.Id == claim.Value)
                .First();
            if (user.LikedProduct == null)
            {
                user.LikedProduct = "";
            }
            likedProductIds = user.LikedProduct
            .Split(';')
            .ToList();
            IEnumerable<CameraModel> cameras = _db.CAMERA
                .Where(c => likedProductIds.Contains(c.Id.ToString())).ToList();
            ViewBag.LikedProductIds = likedProductIds;
            ViewBag.LikedProducts = cameras;
            return View();
        }

        [HttpPost]
        [Authorize(Roles="User")]
        public IActionResult Details(CartModel cart)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            cart.UserId = claim.Value;
            CartModel cartDb = _db.CART.FirstOrDefault(c => 
                c.CameraId == cart.CameraId
                && c.UserId == cart.UserId
            );
            CameraModel cameraDb = _db.CAMERA.FirstOrDefault(c =>
                c.Id == cart.CameraId
            );
            if(cartDb == null)
            {
                _db.CART.Add(cart);
            } else
            {
                cartDb.Amount += cart.Amount;
            }
            cameraDb.Amount -= cart.Amount;
            _db.SaveChanges();
            return RedirectToAction("Cart");
        }
    }
}