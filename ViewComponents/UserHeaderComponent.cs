using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TH4_Nhom20.Data;
using TH4_Nhom20.Models;

namespace TH4_Nhom20.ViewComponents
{
    [ViewComponent(Name = "UserHeader")]
    public class UserHeaderComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public UserHeaderComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return View("Default");
            }
            CartViewModel carts = new CartViewModel()
            {
                Carts = _db.CART
                .Include("Camera")
                .Where(c => c.UserId == claim.Value)
                .ToList()
            };
            foreach(CartModel cart in carts.Carts)
            {
                carts.Subtotal += cart.Amount * int.Parse(cart.Camera.Price);
            }
            ViewBag.Carts = carts.Carts;
            ViewBag.Subtotal = carts.Subtotal;
            return View("Default");
        }
    }
}
