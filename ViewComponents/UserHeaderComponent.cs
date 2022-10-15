﻿using Microsoft.AspNetCore.Mvc;
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
            CartViewModel cart = new CartViewModel()
            {
                CartList = _db.CART
                .Include("Camera")
                .Where(c => c.UserId == claim.Value)
                .ToList()
            };
            double Subtotal = 0;
            foreach(CartModel c in cart.CartList)
            {
                c.ProductPrice += c.Amount * int.Parse(c.Camera.Price);
                Subtotal += c.ProductPrice;
            }
            ViewBag.Carts = cart.CartList;
            ViewBag.Subtotal = Subtotal;
            return View("Default");
        }
    }
}