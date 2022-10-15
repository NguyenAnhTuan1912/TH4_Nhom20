using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH4_Nhom20.Models
{
    public class OrderDetailsModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        [ValidateNever]
        public OrderModel Order { get; set; }
        public int CameraId { get; set; }
        [ForeignKey("CameraId")]
        [ValidateNever]
        public CameraModel Camera { get; set; }
        public int Amount { get; set; }
        public double ProductPrice { get; set; }

    }
}
