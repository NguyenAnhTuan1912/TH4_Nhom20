using Microsoft.AspNetCore.Mvc;
using TH4_Nhom20.Models;

namespace TH4_Nhom20.Models
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            OtherBannerModel otherBannerModel = new OtherBannerModel
            {
                Title = "Blog",
                SubTitle = "Reading our blogs is the same as books."
            };
            return View(otherBannerModel);
        }

        public IActionResult Detail()
        {
            OtherBannerModel otherBannerModel = new OtherBannerModel
            {
                Title = "The Moment You Need To Remove Garlic From The Menu",
                SubTitle = "By Michael Scofield | January 14, 2019 | 8 comments"
            };
            return View(otherBannerModel);
        } 
    }
}
