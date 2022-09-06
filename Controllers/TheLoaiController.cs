using Microsoft.AspNetCore.Mvc;
using TH4_Nhom20.Data;

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
            ViewBag.TheLoai = theloai;
            return View();
        }
    }
}
