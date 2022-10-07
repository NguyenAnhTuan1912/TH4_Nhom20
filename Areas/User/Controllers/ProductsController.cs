using Microsoft.AspNetCore.Mvc;
using TH4_Nhom20.Data;
using TH4_Nhom20.Models;

namespace TTH4_Nhom20.Controllers
{
    [Area("User")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<CameraModel> camera = _db.CAMERA.ToList();
            ViewBag.Cameras = camera;
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
            ViewBag.ChiTiet = chiTietMayAnh[0];
            return View();
        }
    }
}
