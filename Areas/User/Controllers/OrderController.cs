using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TH4_Nhom20.Data;
using TH4_Nhom20.Models;

namespace TH4_Nhom20.Areas.User.Controllers
{
    [Area("User")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if(claim == null)
            {
                return View();
            }
            IEnumerable<OrderModel> orderlList = _db.ORDER.Include("User").ToList();
            ViewBag.OrderlList = orderlList;
            return View();
        }

        [Authorize]
        public IActionResult Details(int orderId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            List<OrderDetailsModel> orderDetailList = _db.ORDERDETAILS
                .Include("Camera")
                .Where(o => o.OrderId ==  orderId)
                .ToList();
            OrderModel order = _db.ORDER.Include("User")
                .Where(o => o.Id == orderId).First();
            ViewBag.Order = order;
            ViewBag.OrderDetailList = orderDetailList;
            return View();
        }
    }
}
