using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TH4_Nhom20.Data;
using TH4_Nhom20.Models;

namespace TH4_Nhom20.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private List<string> status = null;

        public OrdersController(ApplicationDbContext db)
        {
            status = new List<string>() {
                "Đang xử lý",
                "Đang giao hàng",
                "Đã giao hàng"
            };
            _db = db;
        }

        [Authorize(Roles="Admin")]
        public IActionResult Index()
        {
            IEnumerable<OrderModel> orders = _db.ORDER.ToList();
            ViewBag.Orders = orders;
            ViewBag.StatusListItem = status;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Update(int orderId, string status)
        {
            OrderModel order = _db.ORDER.Where(o => o.Id == orderId).First();
            order.OrderStatus = status;
            _db.SaveChanges();
            return Json(new { success = true });
        }
    }
}
