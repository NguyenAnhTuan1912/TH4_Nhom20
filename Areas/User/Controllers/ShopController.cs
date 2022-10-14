using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return View();
        }

        [HttpGet]
        public IActionResult Details(int cameraId)
        {
            CartModel cart = new CartModel
            {
                CameraId = cameraId,
                Camera = _db.CAMERA.Include(camera => camera.Brand).Where(camera => camera.Id == cameraId).First(),
                Amount = 1
            };
            IEnumerable<CameraModel> cameras = _db.CAMERA.Where(camera => 
            camera.Category == cart.Camera.Category && camera.BrandName == cart.Camera.BrandName &&
            camera.Id != cameraId
            );
            ViewBag.relatedCamera = cameras;
            ViewBag.Cart = cart;
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(CartModel cart)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            cart.UserId = claim.Value;
            CartModel cartDb = _db.CART.FirstOrDefault(c => 
                c.CameraId == cart.CameraId
                && c.UserId == cart.UserId
            );
            if(cartDb == null)
            {
                _db.CART.Add(cart);
            } else
            {
                cartDb.Amount += cart.Amount;
            }
            _db.SaveChanges();
            return RedirectToAction("Cart");
        }

        [Authorize]
        public IActionResult Cart()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            CartViewModel carts = new CartViewModel()
            {
                Carts = _db.CART
                .Include("Camera")
                .Where(c => c.UserId == claim.Value)
                .ToList()

            };
            foreach(CartModel cart in carts.Carts)
            {
                cart.ProductPrice = cart.Amount * int.Parse(cart.Camera.Price);
            }
            ViewBag.Carts = carts.Carts;
            return View();
        }
    }
}
