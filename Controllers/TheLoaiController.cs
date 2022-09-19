using Microsoft.AspNetCore.Mvc;
using TH4_Nhom20.Data;
using TH4_Nhom20.Data.Migrations;
using TH4_Nhom20.Models;
using static System.Collections.Specialized.BitVector32;

namespace TH4_Nhom20.Controllers
{
    public class TheLoaiController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TheLoaiController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var theloai = _db.THELOAI.ToList();
            ViewBag.TheLoai = theloai;
            return View();
        }
        // Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ChiTietTheLoaiViewModel chiTietTheLoai)
        {
            if (ModelState.IsValid)
            {
                TheLoaiModel theLoai = new TheLoaiModel
                {
                    Name = chiTietTheLoai.Name,
                    DateCreated = chiTietTheLoai.DateCreated
                };
                _db.THELOAI.Add(theLoai);
                _db.SaveChanges();

                ChiTietTheLoaiModel chiTiet = new ChiTietTheLoaiModel
                {
                    Category = theLoai,
                    Description = chiTietTheLoai.Description,
                    AmountFilm = chiTietTheLoai.AmountFilm
                };
                _db.CHITIETTHELOAI.Add(chiTiet);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        // Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var chiTiet = (from ChiTiet in _db.CHITIETTHELOAI
                           join TheLoai in _db.THELOAI on ChiTiet.Category.Id equals TheLoai.Id
                           where TheLoai.Id == id
                           select new
                           {
                               CategoryId = TheLoai.Id,
                               CategoryDetailsId = ChiTiet.Id,
                               Name = TheLoai.Name,
                               Description = ChiTiet.Description,
                               AmountFilm = ChiTiet.AmountFilm,
                               DateCreated = TheLoai.DateCreated
                           }
                           ).ToList();
            ViewBag.ChiTiet = chiTiet[0];
            return View();
        }
        [HttpPost]
        public IActionResult Edit(ChiTietTheLoaiViewModel chiTietTheLoai)
        {
            if(ModelState.IsValid)
            {
                var oldTheLoai = _db.THELOAI.Where(o => o.Id == chiTietTheLoai.CategoryId).First();
                oldTheLoai.Name = chiTietTheLoai.Name;
                oldTheLoai.DateCreated = chiTietTheLoai.DateCreated;
                _db.SaveChanges();

                var oldChiTietTheLoai = _db.CHITIETTHELOAI.Where(o => o.Id == chiTietTheLoai.CategoryDetailsId).First();
                oldChiTietTheLoai.Category = oldTheLoai;
                oldChiTietTheLoai.Description = chiTietTheLoai.Description;
                oldChiTietTheLoai.AmountFilm = chiTietTheLoai.AmountFilm;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View();
        }

        // Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var chiTiet = (from ChiTiet in _db.CHITIETTHELOAI
                           join TheLoai in _db.THELOAI on ChiTiet.Category.Id equals TheLoai.Id
                           where TheLoai.Id == id
                           select new
                           {
                               Name = TheLoai.Name,
                               Description = ChiTiet.Description,
                               AmountFilm = ChiTiet.AmountFilm,
                               DateCreated = TheLoai.DateCreated
                           }
                           ).ToList();
            ViewBag.ChiTiet = chiTiet[0];
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
            var chiTiet = (from ChiTiet in _db.CHITIETTHELOAI
                           join TheLoai in _db.THELOAI on ChiTiet.Category.Id equals TheLoai.Id
                           where TheLoai.Id == id
                           select new
                           {
                               CategoryId = TheLoai.Id,
                               CategoryDetailsId = ChiTiet.Id,
                               Name = TheLoai.Name,
                               Description = ChiTiet.Description,
                               AmountFilm = ChiTiet.AmountFilm,
                               DateCreated = TheLoai.DateCreated
                           }
                           ).ToList();
            ViewBag.ChiTiet = chiTiet[0];
            return View();
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int CategoryId)
        {
            var theLoai = _db.THELOAI.Where(o => o.Id == CategoryId).First();
            if(theLoai == null)
            {
                return NotFound();
            }
            //_db.THELOAI
            _db.THELOAI.Remove(theLoai);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
   
}
