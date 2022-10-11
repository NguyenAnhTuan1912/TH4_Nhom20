using Microsoft.AspNetCore.Mvc;
using TH4_Nhom20.Data;
using TH4_Nhom20.Models;

namespace TH4_Nhom20.ViewComponents
{
    [ViewComponent(Name = "BigBanner")]
    public class BigBannerViewComponent: ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public BigBannerViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke(string title = "", string subtitle = "")
        {
            OtherBannerModel bannerText = new OtherBannerModel
            {
                Title = title,
                SubTitle = subtitle
            };
            return View("Default", bannerText);
        }
    }
}
