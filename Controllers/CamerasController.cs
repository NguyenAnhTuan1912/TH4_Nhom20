using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TH4_Nhom20.Data;
using TH4_Nhom20.Data.Migrations;
using TH4_Nhom20.Models;
using static System.Collections.Specialized.BitVector32;

namespace TH4_Nhom20.Controllers
{
    public class CamerasController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CamerasController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this._db = db;
            this._webHostEnvironment = webHostEnvironment;
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
            IEnumerable<SelectListItem> brandNames = _db.BRAND.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name
            });
            string[] categoryNames = _db.BRAND.Where(o => o.Id == 1).First().Categories.Split(';');
            IEnumerable<SelectListItem> categories = categoryNames.Select(item => new SelectListItem
            {
                Value = item,
                Text = item
            });
            ViewBag.BrandNames = brandNames;
            ViewBag.CategoryNamesOfEachBrand = categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CameraDetailsViewModel chiTietMayAnh)
        {
            if (ModelState.IsValid)
            {
                // Query ra record(s) brand theo BrandID mà người dùng chọn, lấy record đầu tiên
                var brand = _db.BRAND.Where(o => o.Id == chiTietMayAnh.BrandId).First();
                // Tạo instance cho ImageModel
                ImageModel image = new ImageModel();
                string path = "";
                // Tên nhập từ form sẽ xoá các khoảng trống đi và gôm lại với nhau, dùng để lưu tên ảnh.
                string fileName = string.Join("", chiTietMayAnh.CameraName.Split(' '));
                // Nếu như có file gửi về server thì thực hiện câu lệnh trong If, còn lại thì bỏ qua.
                if (chiTietMayAnh.ImageFile != null)
                {
                    // Lấy ra đường dẫn mà server đang được đặt. Ví dụ server đang ở trong ô D thì 
                    // có đường dẫn là D:/[project-name]
                    string rootPath = _webHostEnvironment.WebRootPath;
                    // Lấy ra đuổi file.
                    string extension = Path.GetExtension(chiTietMayAnh.ImageFile.FileName);
                    fileName += extension;
                    path = Path.Combine(
                        rootPath + "/images/" + brand.Name + '/' + chiTietMayAnh.Category + '/' + fileName
                    );
                    // Mở một đường ống tới đúng địa chỉ trong path, tạo một file mới.
                    // Bước này quan trọng.
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        await chiTietMayAnh.ImageFile.CopyToAsync(fs);
                    }
                    image.FileName = fileName;
                }

                CameraModel camera = new CameraModel
                {
                    Brand = brand,
                    BrandName = brand.Name,
                    Image = image,
                    Name = chiTietMayAnh.CameraName,
                    Price = chiTietMayAnh.CameraPrice,
                    Category = chiTietMayAnh.Category,
                    Features = chiTietMayAnh.CameraFeatures,
                    Introduction = chiTietMayAnh.CameraIntroduction,
                    ImageUrls = (String.IsNullOrEmpty(path)) ? "" : "/images/" + brand.Name + '/' + chiTietMayAnh.Category + '/' + fileName
                };

                _db.IMAGE.Add(image);
                _db.CAMERA.Add(camera);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        // Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            IEnumerable<SelectListItem> brandNames = _db.BRAND.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name
            });
            string[] categoryNames = _db.BRAND.Where(o => o.Id == 1).First().Categories.Split(';');
            IEnumerable<SelectListItem> categories = categoryNames.Select(item => new SelectListItem
            {
                Value = item,
                Text = item
            });
            var chiTietMayAnh = (from camera in _db.CAMERA
                           join brand in _db.BRAND on camera.Brand.Id equals brand.Id
                           where camera.Id == id
                           select new
                           {
                               CameraId = camera.Id,
                               BrandId = brand.Id,
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
            ViewBag.BrandNames = brandNames;
            ViewBag.CategoryNamesOfEachBrand = categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CameraDetailsViewModel chiTietMayAnh)
        {
            if(ModelState.IsValid)
            {
                var brand = _db.BRAND.Where(o => o.Id == chiTietMayAnh.BrandId).First();
                string fileName = string.Join("", chiTietMayAnh.CameraName.Split(' '));
                string fullFileName = fileName + Path.GetExtension(chiTietMayAnh.ImageFile.FileName);
                string rootPath = _webHostEnvironment.WebRootPath;
                string path = "";
                ImageModel image = new ImageModel();
                if (chiTietMayAnh.ImageFile != null)
                {
                    //  Neu nhu chi co thay doi anh, khong thay doi hang, loai va ten may.
                    if (
                        chiTietMayAnh.OldImageUrls.Contains(brand.Name)
                        && chiTietMayAnh.OldImageUrls.Contains(chiTietMayAnh.Category)
                        && chiTietMayAnh.OldImageUrls.Contains(fileName)
                    )
                    {
                        path = Path.Combine(
                            rootPath + chiTietMayAnh.OldImageUrls
                        );
                        System.IO.File.Delete(path);
                        using (FileStream fs = new FileStream(path, FileMode.Create))
                        {
                            await chiTietMayAnh.ImageFile.CopyToAsync(fs);
                        }
                        image.FileName = fullFileName;
                    } else
                    {
                        // Neu nhu doi 1 tong 3 gia tri la hang, loai hoac ten may
                        System.IO.File.Delete(rootPath + chiTietMayAnh.OldImageUrls);
                        string extension = Path.GetExtension(chiTietMayAnh.ImageFile.FileName);
                        fileName += extension;
                        path = Path.Combine(
                            rootPath + "/images/" + brand.Name + '/' + chiTietMayAnh.Category + '/' + fullFileName
                        );
                        using (FileStream fs = new FileStream(path, FileMode.Create))
                        {
                            await chiTietMayAnh.ImageFile.CopyToAsync(fs);
                        }
                        image.FileName = fullFileName;
                    }
                }
                var oldInformationOfCamera = _db.CAMERA.Where(o => o.Id == chiTietMayAnh.CameraId).First();
                oldInformationOfCamera.Name = chiTietMayAnh.CameraName;
                oldInformationOfCamera.Brand = brand;
                oldInformationOfCamera.Price = chiTietMayAnh.CameraPrice;
                oldInformationOfCamera.Features = chiTietMayAnh.CameraFeatures;
                oldInformationOfCamera.Introduction = chiTietMayAnh.CameraIntroduction;
                oldInformationOfCamera.ImageUrls = "/images/" + brand.Name + '/' + chiTietMayAnh.Category + '/' + fullFileName;
                oldInformationOfCamera.BrandName = brand.Name;

                var potentialImage = (from c in _db.CAMERA
                               join i in _db.IMAGE on c.Image.Id equals i.Id
                               where c.Id == chiTietMayAnh.CameraId
                               select i).First();
                var oldInformationOfImage = _db.IMAGE.Where(o => o.Id == potentialImage.Id).First();
                oldInformationOfImage.FileName = fileName;

                await _db.SaveChangesAsync();
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

        // Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var cameraDetails = (
                from c in _db.CAMERA
                join i in _db.IMAGE on c.Image.Id equals i.Id
                where c.Id == id
                select new
                {
                    ImageId = i.Id
                }
            ).ToList()[0];
            var camera = _db.CAMERA.Where(o => o.Id == id).First();
            var image = _db.IMAGE.Where(o => o.Id == cameraDetails.ImageId).First();
            string rootPath = _webHostEnvironment.WebRootPath;
            string path = Path.Combine(
                rootPath + camera.ImageUrls
            );
            System.IO.File.Delete(path);
            _db.CAMERA.Remove(camera);
            _db.IMAGE.Remove(image);
            _db.SaveChanges();
            return Json(new {success = true});
        }
    }
}