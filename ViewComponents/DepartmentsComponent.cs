using Microsoft.AspNetCore.Mvc;
using TH4_Nhom20.Data;
using TH4_Nhom20.Models;

namespace TH4_Nhom20.ViewComponents
{
    [ViewComponent(Name = "Departments")]
    public class DepartmentsComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public DepartmentsComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<string> brandNames = _db.BRAND.Select(brand => brand.Name);
            ViewBag.BrandNames = brandNames;
            return View();
        }
    }
}

