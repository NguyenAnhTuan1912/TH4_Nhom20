using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TH4_Nhom20.Data;

namespace TH4_Nhom20.Areas.User.Controllers
{
    [Area("User")]
    public class InteractProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public InteractProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Authorize(Roles="User")]
        public IActionResult Like(int cameraId, string isLiked)
        {
            return Json(new { success = true });
        }
    }
}
