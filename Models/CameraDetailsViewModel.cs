using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TH4_Nhom20.Models
{
    public class CameraDetailsViewModel
    {
        public int CameraId { get; set; }
        [Required(ErrorMessage = "Hãng máy ảnh không được để trống")]
        [Display(Name = "Hãng")]
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Loại máy ảnh không được để trống!")]
        [Display(Name = "Thể loại")]
        public string Category { get; set; }
        [Required(ErrorMessage="Tên máy không được để trống!")]
        [Display(Name="Tên máy ảnh")]
        public string CameraName { get; set; }
        [Required(ErrorMessage = "Giá của máy ảnh không được để trống!")]
        [Display(Name = "Giá")]
        public string CameraPrice { get; set; }
        [Required(ErrorMessage = "Nhập ít nhất 1 tính năng cho máy ảnh và không được để trống!")]
        [Display(Name = "Tính năng")]
        public string CameraFeatures { get; set; }
        [Required(ErrorMessage = "Phần giới thiệu của máy ảnh không được để trống!")]
        [Display(Name = "Giời thiệu về thiết bị")]
        public string CameraIntroduction { get; set; }
        [Display(Name = "Tải ảnh lên")]
        [NotMapped]
        [ValidateNever]
        public IFormFile ImageFile { get; set; }
        [ValidateNever]
        public string OldImageUrls { get; set; }
    }
}
