using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Details(int id)
        {
            var chiTietMayAnh = (from camera in _db.CAMERA
                                 join brand in _db.BRAND on camera.Brand.Id equals brand.Id
                                 where camera.Id == id
                                 select new
                                 {
                                     CameraName = camera.Name,
                                     BrandName = brand.Name,
                                     Category = camera.Category,
                                     CameraPrice = camera.Price,
                                     CameraFeatures = camera.Features,
                                     CameraIntroduction = camera.Introduction,
                                     ImageUrls = camera.ImageUrls
                                 }
                           ).ToList();
            IEnumerable<CameraModel> cameras = _db.CAMERA.Where(camera => 
            camera.Category == chiTietMayAnh[0].Category && camera.BrandName == chiTietMayAnh[0].BrandName &&
            camera.Id != id
            );
            ViewBag.relatedCamera = cameras;
            ViewBag.ChiTiet = chiTietMayAnh[0];
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
    }

}
