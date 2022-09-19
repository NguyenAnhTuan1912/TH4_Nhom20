using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TH4_Nhom20.Models
{
    public class ChiTietTheLoaiViewModel
    {
        public int CategoryId { get; set; }
        public int CategoryDetailsId { get; set; }
        [Required(ErrorMessage="Tên thể loại không được để trống!")]
        [Display(Name="Thể loại")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Không đúng định dạng ngày!")]
        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Phần mô tả thể loại không được để trống!")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Không được để trống số lương phim!")]
        [Display(Name = "Số lượng phim")]
        public int AmountFilm { get; set; }
    }
}
