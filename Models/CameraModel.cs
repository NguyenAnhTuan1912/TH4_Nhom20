using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH4_Nhom20.Models
{
    public class CameraModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CategoryId")]
        public BrandModel Brand { get; set; }
        [Required]
        public string Name { get; set; }
        public string Category { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string Features { get; set; }
        [Required]
        public string Introduction { get; set; }
        [Required]
        public string ImageUrls { get; set; }

    }
}
