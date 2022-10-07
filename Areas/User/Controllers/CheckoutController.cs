using Microsoft.AspNetCore.Mvc;
using TH4_Nhom20.Models;

namespace TH4_Nhom20.Controllers
{
    [Area("User")]
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            OtherBannerModel otherBannerModel = new OtherBannerModel
            {
                Title = "Checkout",
                SubTitle = "By sending your information, we'll send your shopping's bill back."
            };
            return View(otherBannerModel);
        }
    }
}
