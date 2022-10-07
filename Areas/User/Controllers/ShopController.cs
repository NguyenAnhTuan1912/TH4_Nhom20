using Microsoft.AspNetCore.Mvc;
using TH4_Nhom20.Models;

namespace TH4_Nhom20.Controllers
{
    [Area("User")]
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            OtherBannerModel otherBannerModel = new OtherBannerModel {
                Title = "Shop",
                SubTitle = "Let's buy something to make a delicous meal."
            };
            return View(otherBannerModel);
        }
        public IActionResult Detail()
        {
            OtherBannerModel otherBannerModel = new OtherBannerModel
            {
                Title = "Vegetable's package",
                SubTitle = "Fresh and Green."
            };
            return View(otherBannerModel);
        }
        public IActionResult Cart()
        {
            OtherBannerModel otherBannerModel = new OtherBannerModel
            {
                Title = "Shopping Cart",
                SubTitle = "Surplus is good, but enough is better."
            };
            return View(otherBannerModel);
        }
    }

}
