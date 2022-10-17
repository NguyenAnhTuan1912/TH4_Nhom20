using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH4_Nhom20.Models
{
    public class CommentModel
    {
        [Key]
        public int Id { get; set; }
        public int CameraId { get; set; }
        [ForeignKey("CameraId")]
        [ValidateNever]
        public CameraModel Camera { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public UserModel User { get; set; }
        public string Comment { get; set; }
    }
}
