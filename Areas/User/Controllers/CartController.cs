﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TH4_Nhom20.Data;
using TH4_Nhom20.Models;

namespace TH4_Nhom20.Areas.User.Controllers
{
    [Area("User")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            CartViewModel cart = new CartViewModel()
            {
                CartList = _db.CART
                .Include("Camera")
                .Where(c => c.UserId == claim.Value)
                .ToList(),
                Order = new OrderModel()
            };
            foreach (CartModel c in cart.CartList)
            {
                c.ProductPrice = c.Amount * int.Parse(c.Camera.Price);
                cart.Order.Subtotal += c.ProductPrice;
            }
            ViewBag.Cart = cart;
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Index(int[] amount)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            List<CartModel> carts = _db.CART
                .Include("Camera")
                .Where(cart => cart.UserId == claim.Value)
                .ToList();
            for (int i = 0; i < amount.Length; i++)
            {
                if (amount[i] == 0)
                {
                    _db.CART.Remove(carts[i]);
                }
                carts[i].Amount = amount[i];
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(CartModel cart)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            cart.UserId = claim.Value;
            CartModel cartDb = _db.CART.FirstOrDefault(c =>
                c.CameraId == cart.CameraId
                && c.UserId == cart.UserId
            );
            if (cartDb == null)
            {
                _db.CART.Add(cart);
            }
            else
            {
                cartDb.Amount += cart.Amount;
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int cameraId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            CartModel cart = _db.CART
                .Include("Camera")
                .Where(cart => cart.UserId == claim.Value && cart.CameraId == cameraId)
                .First();
            _db.CART.Remove(cart);
            _db.SaveChanges();
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if(claim == null)
            {
                return View();
            }
            CartViewModel cart = new CartViewModel()
            {
                CartList = _db.CART
                .Include("Camera")
                .Where(c => c.UserId == claim.Value)
                .ToList(),
                Order = new OrderModel()
            };

            cart.Order.User = _db.USER.FirstOrDefault(user => user.Id == claim.Value);
            cart.Order.Name = cart.Order.User.Name;
            cart.Order.Address = cart.Order.User.Address;
            cart.Order.PhoneNumber = cart.Order.User.PhoneNumber;

            foreach (CartModel c in cart.CartList)
            {
                c.ProductPrice = c.Amount * int.Parse(c.Camera.Price);
                cart.Order.Subtotal += c.ProductPrice;
            }
            ViewBag.Cart = cart;
            return View();
        }
    }
}