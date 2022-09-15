using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH4_Nhom20.Models
{
    public class ChiTietTheLoaiModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CategoryId")]
        public TheLoaiModel Category { get; set; }
        [Required]
        public string Description { get; set; }
        public int AmountFilm { get; set; }
    }
}
