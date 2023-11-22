using System.ComponentModel.DataAnnotations;

namespace shoes_final_exam.Models
{
	public class ProductImage
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? UrlImage { get; set; }

		// Relationship n-1 with Product
		public int ProductId { get; set; }
		public Product? Product { get; set; }
	}
}
