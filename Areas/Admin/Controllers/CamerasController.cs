using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TH4_Nhom20.Data;
using TH4_Nhom20.Data.Migrations;
using TH4_Nhom20.Models;
using static System.Collections.Specialized.BitVector32;

namespace TH4_Nhom20.Controllers
{
    [Area("Admin")]
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

        public async void saveImage(IFormFile file, string pathName)
        {
            using(FileStream fs = new FileStream(pathName, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }
        }

        public string getImageUrl(string[] folderNames, string fileName)
        {
            string url = Path.Combine(folderNames);
            url = Path.Combine(url, fileName);
            return url.Replace(@"\", "/");
        }

        public string getPathName(IFormFile file, string[] folderNames)
        {
            string rootPath = this._webHostEnvironment.WebRootPath;
            foreach (string line in folderNames)
            {
                rootPath = Path.Combine(rootPath, line);
            }
            string fileName = Path.GetFileName(file.FileName);
            string path = Path.Combine(rootPath, fileName);
            return path;
        }

        public string getPathName(IFormFile file, string oldUrl)
        {
            string rootPath = this._webHostEnvironment.WebRootPath;
            rootPath = Path.Combine(rootPath, oldUrl);
            return rootPath;
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
        [DisableRequestSizeLimit, RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue,
        ValueLengthLimit = int.MaxValue)]
        public async Task<IActionResult> Create(CameraDetailsViewModel chiTietMayAnh)
        {
            if (ModelState.IsValid)
            {
                var brand = _db.BRAND.Where(o => o.Id == chiTietMayAnh.BrandId).First();
                List<string> pathNames = new List<string>();
                List<string> urls = new List<string>();
                ImageModel image = new ImageModel();
                if (chiTietMayAnh.ImageFile.Count > 0)
                {
                    string[] folderNames = { "images", brand.Name, chiTietMayAnh.Category };
                    foreach(IFormFile file in chiTietMayAnh.ImageFile)
                    {
                        pathNames.Add(getPathName(file, folderNames));
                        saveImage(file, pathNames[pathNames.Count - 1]);
                        urls.Add(getImageUrl(folderNames, Path.GetFileName(file.FileName)));
                        image.FileName += ";" + Path.GetFileNameWithoutExtension(file.FileName);
                    }
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
                    ImageUrls = (urls.Count == 0) ? "" : String.Join(";", urls.ToArray())
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
        [DisableRequestSizeLimit, RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue,
        ValueLengthLimit = int.MaxValue)]
        public async Task<IActionResult> Edit(CameraDetailsViewModel chiTietMayAnh)
        {
            var brand = _db.BRAND.Where(o => o.Id == chiTietMayAnh.BrandId).First();
            ModelState.Remove("ImageFile");
            if(ModelState.IsValid)
            {
                List<string> pathNames = new List<string>();
                List<string> urls = new List<string>();
                ImageModel image = new ImageModel();
                if (chiTietMayAnh.ImageFile != null)
                {
                    int i = 0;
                    string[] oldUrls = chiTietMayAnh.OldImageUrls.Split(";");
                    string[] folderNames = { "images", brand.Name, chiTietMayAnh.Category };
                    foreach (IFormFile file in chiTietMayAnh.ImageFile)
                    {
                        if(
                            chiTietMayAnh.OldImageUrls.Contains(brand.Name)
                            && chiTietMayAnh.OldImageUrls.Contains(chiTietMayAnh.Category)
                            && chiTietMayAnh.OldImageUrls.Contains(Path.GetFileNameWithoutExtension(file.FileName))
                        )
                        {
                            System.IO.File.Delete(getPathName(file, oldUrls[i]));
                            saveImage(file, getPathName(file, oldUrls[i]));
                        } 
                        else if (
                            chiTietMayAnh.OldImageUrls.Contains(brand.Name)
                            && chiTietMayAnh.OldImageUrls.Contains(chiTietMayAnh.Category)
                            && !chiTietMayAnh.OldImageUrls.Contains(Path.GetFileNameWithoutExtension(file.FileName))
                            && chiTietMayAnh.ImageFile.Count == 1
                        )
                        {
                            pathNames.Add(getPathName(file, folderNames));
                            saveImage(file, pathNames[pathNames.Count - 1]);
                            urls.Add(oldUrls[0]);
                            urls.Add(getImageUrl(folderNames, Path.GetFileName(file.FileName)));
                            string[] splitedOldUrl = oldUrls[0].Split(';');
                            image.FileName += splitedOldUrl[splitedOldUrl.Length - 1] + ";" + Path.GetFileNameWithoutExtension(file.FileName);
                        } 
                        else
                        {
                            System.IO.File.Delete(getPathName(file, oldUrls[i]));
                            pathNames.Add(getPathName(file, folderNames));
                            saveImage(file, pathNames[pathNames.Count - 1]);
                            urls.Add(getImageUrl(folderNames, Path.GetFileName(file.FileName)));
                            image.FileName += ";" + Path.GetFileNameWithoutExtension(file.FileName);
                        }
                        i += 1;
                    }
                }

                var oldInformationOfCamera = _db.CAMERA.Where(o => o.Id == chiTietMayAnh.CameraId).First();
                oldInformationOfCamera.Name = chiTietMayAnh.CameraName;
                oldInformationOfCamera.Brand = brand;
                oldInformationOfCamera.Category = chiTietMayAnh.Category;
                oldInformationOfCamera.Price = chiTietMayAnh.CameraPrice;
                oldInformationOfCamera.Features = chiTietMayAnh.CameraFeatures;
                oldInformationOfCamera.Introduction = chiTietMayAnh.CameraIntroduction;
                oldInformationOfCamera.ImageUrls = (urls.Count < 1) ? oldInformationOfCamera.ImageUrls : String.Join(";", urls.ToArray());
                oldInformationOfCamera.BrandName = brand.Name;

                var potentialImage = (from c in _db.CAMERA
                               join i in _db.IMAGE on c.Image.Id equals i.Id
                               where c.Id == chiTietMayAnh.CameraId
                               select i).First();
                var oldInformationOfImage = _db.IMAGE.Where(o => o.Id == potentialImage.Id).First();
                oldInformationOfImage.FileName = (image.FileName == null) ? oldInformationOfImage.FileName : image.FileName;

                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
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
            var imageIdIncameraDetails = (
                from c in _db.CAMERA
                join i in _db.IMAGE on c.Image.Id equals i.Id
                where c.Id == id
                select new
                {
                    ImageId = i.Id
                }
            ).ToList()[0];
            var camera = _db.CAMERA.Where(o => o.Id == id).First();
            var image = _db.IMAGE.Where(o => o.Id == imageIdIncameraDetails.ImageId).First();
            string rootPath = _webHostEnvironment.WebRootPath;
            string[] oldUrls = camera.ImageUrls.Split(";");
            foreach (string url in oldUrls)
            {
                if(url.Equals("")) continue;
                string path = Path.Combine(rootPath, url);
                System.IO.File.Delete(path);
            }
            
            _db.CAMERA.Remove(camera);
            _db.IMAGE.Remove(image);
            _db.SaveChanges();
            return Json(new {success = true});
        }
    }
}