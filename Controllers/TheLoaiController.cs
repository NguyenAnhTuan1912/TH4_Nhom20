using Microsoft.AspNetCore.Mvc;
using TH4_Nhom20.Data;
using TH4_Nhom20.Data.Migrations;
using TH4_Nhom20.Models;

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
            var theloai = _db.theLoai.ToList();
            ViewBag.theLoai = theloai;
            return View();
        }
        // Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TheLoaiModel theLoai)
        {
            _db.theLoai.Add(theLoai);
            _db.SaveChanges();
            return View();
        }

        // Edit
        [HttpGet]
        public IActionResult Edit(int id, string name, DateTime datecreated)
        {
            TheLoaiModel theLoai = new TheLoaiModel
            {
                Name = name,
                DateCreated = datecreated
            };
            return View(theLoai);
        }
        [HttpPost]
        public IActionResult Edit(TheLoaiModel theLoai)
        {
            if(ModelState.IsValid)
            {
                _db.theLoai.Update(theLoai);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View();
        }
    }
}
