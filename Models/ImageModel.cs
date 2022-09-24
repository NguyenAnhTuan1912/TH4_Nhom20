using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH4_Nhom20.Models
{
	public class ImageModel
	{
		[Key]
		public int Id { get; set; }
		public string FileName { get; set; }
	}
}
