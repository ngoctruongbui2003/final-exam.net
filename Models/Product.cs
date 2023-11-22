using System.ComponentModel.DataAnnotations;

namespace shoes_final_exam.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? Image { get; set; }
		[Required]
		public string? Name { get; set; }
		public string? Description { get; set; }
		[Required]
		public float Size { get; set; }
		[Required]
		public long Price { get; set; }
		[Required]
		public int Quantity { get; set; }
		public DateTime CreatedDate { get; set; }

		// Relationship n-1 with Category
		public int CategoryId { get; set; }
		public Category? Category { get; set; }

		// Relationship 1-n with OrderDetail
		public ICollection<Category>? Categories { get; set; }

		// Relationship 1-n with ProductImage
		public ICollection<ProductImage>? ProductImages { get; set; }
	}
}
