using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TH4_Nhom20.Data;
using TH4_Nhom20.Models;

namespace TH4_Nhom20.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            List<string> likedProductIds = new List<string>();
            if (claim != null)
            {
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
            };
            IEnumerable<CameraModel> cameras = _db.CAMERA.ToList();
            ViewBag.Cameras = cameras;
            ViewBag.LikedProductIds = likedProductIds;
            ViewBag.AmountOfCamera = cameras.Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}