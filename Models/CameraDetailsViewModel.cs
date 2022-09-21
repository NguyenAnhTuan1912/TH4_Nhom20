using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TH4_Nhom20.Models
{
    public class CameraDetailsViewModel
    {
        public int CameraId { get; set; }
        public int BrandId { get; set; }
        [Required(ErrorMessage="Tên máy không được để trống!")]
        [Display(Name="Tên máy ảnh")]
        public string CameraName { get; set; }
        [Required(ErrorMessage = "Tên hãng sản xuất không được để trống!")]
        [Display(Name = "Tên hãng")]
        public string BrandName { get; set; }
        [Required(ErrorMessage = "Loại máy ảnh không được để trống!")]
        [Display(Name = "Thể loại")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Giá của máy ảnh không được để trống!")]
        [Display(Name = "Giá")]
        public string CameraPrice { get; set; }
        [Required(ErrorMessage = "Nhập ít nhất 1 tính năng cho máy ảnh và không được để trống!")]
        [Display(Name = "Tính năng")]
        public string CameraFeatures { get; set; }
        [Required(ErrorMessage = "Phần giới thiệu của máy ảnh không được để trống!")]
        [Display(Name = "Giời thiệu về thiết bị")]
        public string CameraIntroduction { get; set; }
        [Required(ErrorMessage = "Hãy thêm vào ít nhất một ảnh!")]
        [Display(Name = "Ảnh")]
        public string ImageUrls { get; set; }
    }
}
