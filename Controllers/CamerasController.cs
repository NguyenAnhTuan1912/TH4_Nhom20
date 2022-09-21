using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TH4_Nhom20.Data;
using TH4_Nhom20.Data.Migrations;
using TH4_Nhom20.Models;
using static System.Collections.Specialized.BitVector32;

namespace TH4_Nhom20.Controllers
{
    public class CamerasController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CamerasController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<CameraModel> camera = _db.CAMERA.ToList();
            ViewBag.Cameras = camera;
            return View();
        }
        // Create
        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> brandNames = new List<SelectListItem>();
            List<string[]> categoryNamesOfEachBrand = new List<string[]>();
            var brands = _db.BRAND.ToList();
            foreach(var brand in brands)
            {
                string[] convertCategoryNamesToArray = brand.Categories.Split(';');
                categoryNamesOfEachBrand.Add(convertCategoryNamesToArray);
                SelectListItem brandSelectItem = new SelectListItem
                {
                    Value = brand.Id.ToString(),
                    Text = brand.Name
                };
                brandNames.Append(brandSelectItem);
            }
            ViewBag.BrandNames = brandNames;
            ViewBag.CategoryNamesOfEachBrand = categoryNamesOfEachBrand;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CameraDetailsViewModel chiTietMayAnh)
        {
            if (ModelState.IsValid)
            {
                var brand = _db.BRAND.Where(o => o.Id == chiTietMayAnh.BrandId).First();

                CameraModel camera = new CameraModel
                {
                    Brand = brand,
                    Name = chiTietMayAnh.CameraName,
                    Price = chiTietMayAnh.CameraPrice,
                    Features = chiTietMayAnh.CameraFeatures,
                    Introduction = chiTietMayAnh.CameraIntroduction,
                    ImageUrls = chiTietMayAnh.ImageUrls
                };
                _db.CAMERA.Add(camera);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        // Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var chiTietMayAnh = (from camera in _db.CAMERA
                           join brand in _db.BRAND on camera.Brand.Id equals brand.Id
                           where brand.Id == id
                           select new
                           {
                               CameraId = camera.Id,
                               BrandId = brand.Id,
                               CameraName = camera.Name,
                               BrandName = brand.Name,
                               Categories = camera.Category,
                               CameraPrice = camera.Price,
                               CameraFeatures = camera.Features,
                               CameraIntroduction = camera.Introduction,
                               ImageUrls = camera.ImageUrls
                           }
                           ).ToList();
            ViewBag.ChiTiet = chiTietMayAnh[0];
            return View();
        }
        [HttpPost]
        public IActionResult Edit(CameraDetailsViewModel chiTietMayAnh)
        {
            if(ModelState.IsValid)
            {
                var brand = _db.BRAND.Where(o => o.Id == chiTietMayAnh.BrandId).First();
                var oldInformationOfCamera = _db.CAMERA.Where(o => o.Id == chiTietMayAnh.CameraId).First();
                oldInformationOfCamera.Name = chiTietMayAnh.CameraName;
                oldInformationOfCamera.Brand = brand;
                oldInformationOfCamera.Price = chiTietMayAnh.CameraPrice;
                oldInformationOfCamera.Features = chiTietMayAnh.CameraFeatures;
                oldInformationOfCamera.Introduction = chiTietMayAnh.CameraIntroduction;
                oldInformationOfCamera.ImageUrls = chiTietMayAnh.ImageUrls;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View();
        }

        // Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var chiTietMayAnh = (from camera in _db.CAMERA
                                 join brand in _db.BRAND on camera.Brand.Id equals brand.Id
                                 where brand.Id == id
                                 select new
                                 {
                                     CameraName = camera.Name,
                                     BrandName = brand.Name,
                                     Categories = camera.Category,
                                     CameraPrice = camera.Price,
                                     CameraFeatures = camera.Features,
                                     CameraIntroduction = camera.Introduction,
                                     ImageUrls = camera.ImageUrls
                                 }
                           ).ToList();
            ViewBag.ChiTiet = chiTietMayAnh[0];
            return View();
        }

        // Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var camera = _db.CAMERA.Where(o => o.Id == id).First();
            _db.CAMERA.Remove(camera);
            return View();
        }
    }
}