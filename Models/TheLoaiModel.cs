using System.ComponentModel.DataAnnotations;

namespace TH4_Nhom20.Models
{
    public class TheLoaiModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được để trống Tên thể loại!")]
        [Display(Name="Thể Loại")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Không đúng định dạng ngày!")]
        [Display(Name = "Ngày Tạo")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
