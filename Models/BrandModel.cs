using System.ComponentModel.DataAnnotations;

namespace TH4_Nhom20.Models
{
    public class BrandModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Categories { get; set; }
    }
}
