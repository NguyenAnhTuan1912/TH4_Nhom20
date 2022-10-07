using Microsoft.AspNetCore.Mvc;
using TH4_Nhom20.Models;

namespace TH4_Nhom20.Controllers
{
    [Area("User")]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            OtherBannerModel otherBannerModel = new OtherBannerModel
            {
                Title = "Contact Us",
                SubTitle = "Express your opinions, feedbacks and thinkings."
            };
            return View(otherBannerModel);
        }
    }
}
